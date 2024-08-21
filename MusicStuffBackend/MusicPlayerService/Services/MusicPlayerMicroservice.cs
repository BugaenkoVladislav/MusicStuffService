using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Caching.Memory;
using NAudio.Wave;

namespace MusicPlayerService;

public class MusicPlayerMicroservice(IMemoryCache cache, string addressGrpc)
{
    private IMemoryCache _cache = cache;

    private MemoryCacheEntryOptions _entryOptions = new MemoryCacheEntryOptions()
        .SetPriority(CacheItemPriority.High)
        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
        .SetSlidingExpiration(TimeSpan.FromSeconds(120));
    
    public async Task PlayMusicAsync(string trackPath)
    {
        var channel = GrpcChannel.ForAddress(addressGrpc);
        var _client = new Translation.TranslationClient(channel);

        
        if (_cache.TryGetValue(trackPath, out byte[] audioData))
        {
            // Воспроизведение кешированных данных
            await PlayAudioDataAsync(audioData);
        }
        else // Как работает этот код
        {
            using var call = _client.StreamMusic(new MusicRequest { SongPath = trackPath });

            // Создайте поток памяти и поток для воспроизведения
            using var memoryStream = new MemoryStream();
            using var waveOut = new WaveOutEvent();
            var waveStream = new BufferedWaveProvider(new WaveFormat(44100, 16, 2)); // Замените формат на тот, который вам нужен

            waveOut.Init(waveStream);
            waveOut.Play();

            await foreach (var chunk in call.ResponseStream.ReadAllAsync())
            {
                var chunkData = chunk.Data.ToByteArray();
                memoryStream.Write(chunkData, 0, chunkData.Length);
                waveStream.AddSamples(chunkData, 0, chunkData.Length);
            }

            // Завершите воспроизведение после завершения загрузки
            waveOut.Stop();
        }
    }
    private async Task PlayAudioDataAsync(byte[] audioData)
    {
        using var memoryStream = new MemoryStream(audioData);
        // поток WAV из MemoryStream
        await using var waveStream = new WaveFileReader(memoryStream);
        // Инициализируйте WaveOutEvent для воспроизведения
        using var waveOut = new WaveOutEvent();
        // Настройте WaveOutEvent на воспроизведение данных из WaveStream
        waveOut.Init(waveStream);
        // Запустите воспроизведение
        waveOut.Play();
        // Ожидание завершения воспроизведения
        await Task.Run(() => 
        {
            while (waveOut.PlaybackState == PlaybackState.Playing)
            {
                Task.Delay(100).Wait(); // Подождите 100 миллисекунд перед проверкой состояния
            }
        });
    }

}