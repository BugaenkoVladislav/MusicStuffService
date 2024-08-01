using Grpc.Core;
using Infrastructure.Infrastructure;
using MusicStuffBackend;

namespace MusicManipulationService.Services;

public class PlaylistMicroservice(ILogger<AlbumMicroservice> logger, UnitOfWork uow): Playlist.PlaylistBase
{
    
}