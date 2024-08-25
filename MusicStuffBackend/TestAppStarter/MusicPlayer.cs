using System.Diagnostics;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using NAudio.Wave;

namespace TestAppStarter;

public class MusicPlayer
{
    private static readonly IMemoryCache Cache = new MemoryCache(new MemoryCacheOptions(){SizeLimit = 1024});
    private static Stopwatch _stopwatch = new Stopwatch();

    private static async Task<MemoryStream?> GetFromCache(string key)
    {
        return await Cache.GetOrCreateAsync(key, async entry =>
        {
            entry.SetAbsoluteExpiration(TimeSpan.FromHours(1))
                .SetSlidingExpiration(TimeSpan.FromMinutes(45))
                .SetPriority(CacheItemPriority.Normal)
                .SetSize(1);

            // Если данные отсутствуют в кеше, запросите их
            var buffer = new MemoryStream();
            var channel = GrpcChannel.ForAddress("http://localhost:5119");
            var client = new Translation.TranslationClient(channel);
            var serverData = client.StreamMusic(new MusicRequest { SongPath = key });
        
            // Read the stream data
            var responseStream = serverData.ResponseStream;
            await foreach (var response in responseStream.ReadAllAsync())
            {
                var data = response.Data.ToByteArray();
                await buffer.WriteAsync(data, 0, data.Length); // Write asynchronously
            }
        
            // Reset the buffer position to the beginning
            buffer.Position = 0;
            return buffer;
        });
    }

    public static async Task PlayMusic(string songPath)
    {
        try
        {
            _stopwatch.Start();
            var trackMemoryStream = await GetFromCache(songPath);
            _stopwatch.Stop();
            if (trackMemoryStream == null || trackMemoryStream.Length == 0)
            {
                Console.WriteLine("Error: Music stream is null or empty.");
                return;
            }
            trackMemoryStream.Position = 0; // Ensure stream is at the beginning
            
            Console.WriteLine("Time execution:"+ _stopwatch.Elapsed);
            await using var reader = new WaveFileReader(trackMemoryStream);
            using var outputDevice = new WaveOutEvent();
            outputDevice.Init(reader);
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