using Grpc.Core;
using Infrastructure.Infrastructure;
using MusicStuffBackend;

namespace MusicManipulationService.Services;

public class AlbumMicroservice(ILogger<AlbumMicroservice> logger, UnitOfWork uow):Album.AlbumBase
{
    public override Task<FullAlbumInfo> GetAlbum(Id request, ServerCallContext context)
    {
        return base.GetAlbum(request, context);
    }

    public override Task<Result> RemoveAlbum(Id request, ServerCallContext context)
    {
        return base.RemoveAlbum(request, context);
    }

    public override Task<Result> AddNewAlbum(AlbumMessage request, ServerCallContext context)
    {
        return base.AddNewAlbum(request, context);
    }
}