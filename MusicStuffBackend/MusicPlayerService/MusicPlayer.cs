using System.Diagnostics;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using NAudio.Wave;

namespace TestAppStarter;

public static class MusicPlayer
{
    private static readonly IMemoryCache Cache = new MemoryCache(new MemoryCacheOptions(){SizeLimit = 1024});
    private static async Task<IAsyncStreamReader<AudioChunk>?> GetFromCacheStream(string key)
    {
        return await Cache.GetOrCreateAsync(key, async entry =>
        {
            entry.SetAbsoluteExpiration(TimeSpan.FromHours(1))
                .SetSlidingExpiration(TimeSpan.FromMinutes(45))
                .SetPriority(CacheItemPriority.Normal)
                .SetSize(1);

            var channel = GrpcChannel.ForAddress("http://localhost:5119");
            var client = new Translation.TranslationClient(channel);
            var serverData = client.StreamMusic(new MusicRequest { SongPath = key });
            // Возвращаем поток данных, не загружая весь трек в память
            return serverData.ResponseStream;
        });
    }

    public static async Task PlayMusic(string songPath)
    {
        try
        {
            var trackStream = await GetFromCacheStream(songPath);
            if (trackStream == null)
            {
                throw new NullReferenceException("Error: Music stream is null or empty.");
            }
            var waveFormat = new WaveFormat(44100, 16, 2); // Убедитесь, что формат соответствует формату потока
            using var outputDevice = new WaveOutEvent();
            await using var waveProvider = new StreamingWaveProvider(trackStream, waveFormat);
            outputDevice.Init(waveProvider);
            outputDevice.Play();

            Console.WriteLine("Playing audio... Press any key to exit.");
            Console.ReadKey();

            outputDevice.Stop();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
public class StreamingWaveProvider(IAsyncStreamReader<AudioChunk> stream, WaveFormat waveFormat) : WaveStream
{
    public override WaveFormat WaveFormat => waveFormat;
    public override int Read(byte[] buffer, int offset, int count)
    {
        return Task.Run(() => ReadAsync(buffer, offset, count, CancellationToken.None)).GetAwaiter().GetResult();
    }

    public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        if (buffer == null) throw new ArgumentNullException(nameof(buffer));
        if (offset < 0 || count < 0) throw new ArgumentOutOfRangeException("Offset and count must be non-negative.");
        if (offset + count > buffer.Length) throw new ArgumentException("Offset and count exceed buffer size.");

        int totalBytesRead = 0;

        await foreach (var chunk in stream.ReadAllAsync(cancellationToken))
        {
            var data = chunk.Data.ToByteArray();
            int bytesToCopy = Math.Min(count - totalBytesRead, data.Length);

            Array.Copy(data, 0, buffer, offset + totalBytesRead, bytesToCopy);
            totalBytesRead += bytesToCopy;

            if (totalBytesRead >= count)
            {
                break;
            }
        }
        return totalBytesRead;
    }
    
    //размер потока
    public override long Length { get;} 

    //Текущее положение в потоке.
    public override long Position { get; set; }
}
