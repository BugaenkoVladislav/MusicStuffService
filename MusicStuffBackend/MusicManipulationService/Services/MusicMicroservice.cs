using Grpc.Core;
using Infrastructure.Infrastructure;
using MusicStuffBackend;

namespace MusicManipulationService.Services;

public class MusicMicroservice(ILogger<AlbumMicroservice> logger, UnitOfWork uow):Music.MusicBase
{
    
}