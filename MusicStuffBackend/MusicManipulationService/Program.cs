using Infrastructure.Infrastructure;
//using MusicManipulationService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddGrpc();
builder.Services.InfrastructureExt();
var app = builder.Build();

// Configure the HTTP request pipeline.
//app.MapGrpcService<GreeterService>();

app.Run();