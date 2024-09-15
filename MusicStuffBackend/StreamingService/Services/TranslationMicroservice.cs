using Grpc.Core;
using Microsoft.Extensions.Caching.Memory;
using NAudio.Wave;

namespace StreamingService.Services;

public class TranslationMicroservice(ILogger<TranslationMicroservice> logger):Translation.TranslationBase
{
    private readonly  TranslationMicroservice _client;

    

    public override async Task StreamMusic(MusicRequest request, IServerStreamWriter<AudioChunk> responseStream, ServerCallContext context)
    {
        const int chunkSize = 1024 * 64;
        //find object 
        await using var fileStream = new FileStream(request.SongPath, FileMode.Open, FileAccess.Read, FileShare.Read, chunkSize, useAsync: true);
        var buffer = new byte[chunkSize];
        int bytesRead;
        while ((bytesRead = await fileStream.ReadAsync(buffer)) > 0)
        {
            var chunk = new AudioChunk
            {
                Data = Google.Protobuf.ByteString.CopyFrom(buffer, 0, bytesRead)
            };
            // Отправьте часть клиенту
            await responseStream.WriteAsync(chunk);
        }
    }
}