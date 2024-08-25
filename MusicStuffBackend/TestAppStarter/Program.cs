using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Caching.Memory;
using NAudio.Wave;

//WRITE CACHING AND PART PLAYING 

var channel = GrpcChannel.ForAddress("http://localhost:5119");
var _client = new Translation.TranslationClient(channel);
var serverData =  _client.StreamMusic(new MusicRequest(){SongPath = "C:\\Users\\CoffeeCup\\Music\\LinkinPark-Numb.wav"});


// Buffer for the incoming data
using var buffer = new MemoryStream();

// Read the stream data
var responseStream = serverData.ResponseStream;
//Востонавливаем трек из чанков (потом убери)
await foreach (var response in responseStream.ReadAllAsync())
{
    // Write each chunk to the buffer
    var data = response.Data.ToByteArray();
    buffer.Write(data, 0, data.Length);
}
// Reset the buffer position to the beginning
buffer.Position = 0;
// Validate and play the audio
try
{
    await using var reader = new WaveFileReader(buffer);
    using var outputDevice = new WaveOutEvent();
    outputDevice.Init(reader);
    outputDevice.Play();

    Console.WriteLine("Playing audio... Press any key to exit.");
    Console.ReadKey();

    outputDevice.Stop();
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}


