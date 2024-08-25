using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Caching.Memory;
using NAudio.Wave;
using TestAppStarter;

await MusicPlayer.PlayMusic("C:\\Users\\CoffeeCup\\Music\\LinkinPark-Numb.wav");

await Task.Delay(10000); 
await MusicPlayer.PlayMusic("C:\\Users\\CoffeeCup\\Music\\LinkinPark-Numb.wav");