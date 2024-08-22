using Grpc.Core;
using Microsoft.Extensions.Caching.Memory;
using NAudio.Wave;

namespace StreamingService.Services;

public class TranslationMicroservice(ILogger<TranslationMicroservice> logger):Translation.TranslationBase
{
    private readonly  TranslationMicroservice _client;

    private MemoryCacheEntryOptions _entryOptions = new MemoryCacheEntryOptions()
        .SetPriority(CacheItemPriority.High)
        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
        .SetSlidingExpiration(TimeSpan.FromSeconds(120));

    public override async Task StreamMusic(MusicRequest request, IServerStreamWriter<AudioChunk> responseStream, ServerCallContext context)
    {
        string[] messages = { "Привет", "Как дела?", "Че молчишь?", "Ты че, спишь?", "Ну пока" };
        foreach (var message in messages)
        {
            await responseStream.WriteAsync(new AudioChunk(){ Data = message});
            // для имитации работы делаем задержку в 1 секунду
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}