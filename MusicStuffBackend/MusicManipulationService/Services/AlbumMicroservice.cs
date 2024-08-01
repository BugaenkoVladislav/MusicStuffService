using Grpc.Core;
using Infrastructure.Infrastructure;
using MusicStuffBackend;

namespace MusicManipulationService.Services;

public class AlbumMicroservice(ILogger<AlbumMicroservice> logger, UnitOfWork uow):Album.AlbumBase
{
    
}