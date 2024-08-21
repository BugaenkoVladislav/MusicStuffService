using Grpc.Core;
using Microsoft.Extensions.Caching.Memory;
using NAudio.Wave;

namespace StreamingService.Services;

public class TranslationMicroservice(ILogger<TranslationMicroservice> logger, IMemoryCache cache):Translation.TranslationBase
{
    private readonly  TranslationMicroservice _client;
    private IMemoryCache _cache = cache;

    private MemoryCacheEntryOptions _entryOptions = new MemoryCacheEntryOptions()
        .SetPriority(CacheItemPriority.High)
        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
        .SetSlidingExpiration(TimeSpan.FromSeconds(120));
    public override async Task StreamMusic(MusicRequest request, IServerStreamWriter<AudioChunk> responseStream, ServerCallContext context)
    {
        //размер куска 64 КБ
        const int chunkSize = 64 * 1024;

        //поток для чтения файла
        await using var fileStream = new FileStream(request.SongPath, FileMode.Open, FileAccess.Read, FileShare.Read, chunkSize, useAsync: true);
        
        var buffer = new byte[chunkSize];
        int bytesRead;
        // Читайте и отправляйте файл частями
        while ((bytesRead = await fileStream.ReadAsync(buffer)) > 0)
        {
            // Создайте объект AudioChunk и установите его данные
            var chunk = new AudioChunk
            {
                Data = Google.Protobuf.ByteString.CopyFrom(buffer, 0, bytesRead)
            };
            // Отправьте часть клиенту
            await responseStream.WriteAsync(chunk);
        }
    }
}