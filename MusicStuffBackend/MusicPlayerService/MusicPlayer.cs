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
            var call = await GetFromCacheStream(songPath);
            if (call == null)
            {
                Console.WriteLine("Ошибка: поток данных не доступен.");
                return;
            }

            using var outputDevice = new WaveOutEvent();
            var waveFormat = new WaveFormat(44100, 16, 2); // Установите правильные параметры формата WAV
            var bufferedWaveProvider = new BufferedWaveProvider(waveFormat);
            outputDevice.Init(bufferedWaveProvider);
            outputDevice.Play();

            await foreach (var response in call.ReadAllAsync())
            {
                var data = response.Data.ToByteArray();
                bufferedWaveProvider.AddSamples(data, 0, data.Length);
                await Task.Delay(1000);
                //воспроизводит частями но с буфером беда, либо перегружается либо ставишь делей и не слушабельно
            }

            // Ожидание завершения воспроизведения
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}


