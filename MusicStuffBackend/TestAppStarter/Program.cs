using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Caching.Memory;

var channel = GrpcChannel.ForAddress("http://localhost:5119");
var _client = new Translation.TranslationClient(channel);
var serverData =  _client.StreamMusic(new MusicRequest(){SongPath = "123"});

// получаем поток сервера
var responseStream = serverData.ResponseStream;
// с помощью итераторов извлекаем каждое сообщение из потока
await foreach(var response in responseStream.ReadAllAsync())
{
    Console.WriteLine(response.Data);
}


