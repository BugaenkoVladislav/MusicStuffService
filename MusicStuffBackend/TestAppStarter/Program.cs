using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Caching.Memory;
using NAudio.Wave;

var channel = GrpcChannel.ForAddress("http://localhost:5119");
var _client = new Translation.TranslationClient(channel);
var serverData =  _client.StreamMusic(new MusicRequest(){SongPath = "C:\\Users\\CoffeeCup\\Music\\LinkinPark-Numb.mp3"});
// получаем поток сервера
var responseStream = serverData.ResponseStream;

// с помощью итераторов извлекаем каждое сообщение из потока
/*await foreach(var response in responseStream.ReadAllAsync())
{
    string text = response.Data.ToStringUtf8();
    Console.WriteLine(text);
}*/


