using Grpc.Core;
using Infrastructure.Infrastructure;
using MusicStuffBackend;

namespace MusicManipulationService.Services;

public class MusicMicroservice(ILogger<AlbumMicroservice> logger, UnitOfWork uow):Music.MusicBase
{
    public override Task<Result> RemoveTrack(Id request, ServerCallContext context)
    {
        return base.RemoveTrack(request, context);
    }

    public override Task<Result> AddNewTrack(Track request, ServerCallContext context)
    {
        return base.AddNewTrack(request, context);
    }

    public override Task<Track> GetTrackById(Id request, ServerCallContext context)
    {
        return base.GetTrackById(request, context);
    }
}