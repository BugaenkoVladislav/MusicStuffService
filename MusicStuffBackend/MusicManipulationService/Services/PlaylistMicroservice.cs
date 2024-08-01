using Grpc.Core;
using Infrastructure.Infrastructure;
using MusicStuffBackend;

namespace MusicManipulationService.Services;

public class PlaylistMicroservice(ILogger<AlbumMicroservice> logger, UnitOfWork uow): Playlist.PlaylistBase
{
    public override Task<MusicFromPlayList> GetPlaylist(Id request, ServerCallContext context)
    {
        return base.GetPlaylist(request, context);
    }

    public override Task<Result> RemovePlaylist(Id request, ServerCallContext context)
    {
        return base.RemovePlaylist(request, context);
    }

    public override Task<Result> AddNewPlaylist(PlayList request, ServerCallContext context)
    {
        return base.AddNewPlaylist(request, context);
    }

    public override Task<Result> AddOtherPlaylist(IdPlayListIdUser request, ServerCallContext context)
    {
        return base.AddOtherPlaylist(request, context);
    }

    public override Task<Result> RemoveOtherPlaylist(IdPlayListIdUser request, ServerCallContext context)
    {
        return base.RemoveOtherPlaylist(request, context);
    }

    public override Task<Result> AddTrackToPlaylist(PlayListIdAndTrackId request, ServerCallContext context)
    {
        return base.AddTrackToPlaylist(request, context);
    }

    public override Task<Result> RemoveTrackFromPlaylist(PlayListIdAndTrackId request, ServerCallContext context)
    {
        return base.RemoveTrackFromPlaylist(request, context);
    }
}