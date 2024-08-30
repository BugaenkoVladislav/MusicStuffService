using Domain.Entities;
using Grpc.Core;
using Infrastructure;
using Infrastructure;
using MusicStuffBackend;
using Music = Domain.Entities.Music;
using Playlist = MusicStuffBackend.Playlist;
using String = MusicStuffBackend.String;

namespace MusicManipulationService.Services;

public class PlaylistMicroservice(ILogger<AlbumMicroservice> logger, UnitOfWork uow): Playlist.PlaylistBase
{
    private Converter _converter = new Converter(uow);
    public override async Task<FullPlayListInfo> GetPlaylist(Id request, ServerCallContext context)
    {
        var playlist = await uow.PlaylistRepository.FindEntityByAsync(x => x.IdPlaylist == request.MessageId);
        var tracksIDsFromMusic = await uow.PlaylistMusicRepository.FindEntitiesByAsync(x => x.IdPlaylist == request.MessageId);
        var tracks = await _converter.ConvertPlaylistMusicsToMusicList(tracksIDsFromMusic);
        
        return await Task.FromResult(new FullPlayListInfo()
        {
            Track =
            {
                tracks
            },
            PlaylistInfo = new PlayList()
            {
                IdCreator = (await uow.PlaylistUserRepository.FindEntityByAsync(x=>x.IsCreator == true && x.IdPlaylist == request.MessageId)).IdUser,
                PhotoPath = playlist.PhotoPath,
                PlaylistName = playlist.PlaylistName
            }
        });
    }

    public override async Task<Result> RemovePlaylist(Id request, ServerCallContext context)
    {
        var playlist = await uow.PlaylistRepository.FindEntityByAsync(x => x.IdPlaylist == request.MessageId);
        uow.PlaylistRepository.RemoveEntity(playlist);
        await uow.SaveAllChanges();
        return await Task.FromResult(new Result());
    }

    public override async Task<Result> AddNewPlaylist(PlayList request, ServerCallContext context)
    {
        uow.PlaylistRepository.AddEntity(new Domain.Entities.Playlist()
        {
            PlaylistName = request.PlaylistName,
            PhotoPath = request.PhotoPath
        });
        await uow.SaveAllChanges();
        var playlist = await uow.PlaylistRepository.FindEntityByAsync(x =>
            x.PlaylistName == request.PlaylistName && x.PhotoPath == request.PhotoPath);
        var user = await uow.UserRepository.FindEntityByAsync(x => x.IdUser == request.IdCreator);
        uow.PlaylistUserRepository.AddEntity(new PlaylistUser()
        {
            User = user,
            Playlist = playlist,
            IdPlaylist = playlist.IdPlaylist,
            IdUser = user.IdUser,
            IsCreator = true
        });
        await uow.SaveAllChanges();
        return await Task.FromResult(new Result());
    }

    public override async Task<Result> AddOtherPlaylist(IdPlayListIdUser request, ServerCallContext context)
    {
        var user = await uow.UserRepository.FindEntityByAsync(x => x.IdUser == request.IdUser);
        var playlist = await uow.PlaylistRepository.FindEntityByAsync(x => x.IdPlaylist == request.IdPlaylist);
        uow.PlaylistUserRepository.AddEntity(new PlaylistUser()
        {
            User = user,
            IdPlaylist = playlist.IdPlaylist,
            IsCreator = false,
            Playlist = playlist,
            IdUser = user.IdUser
        });
        await uow.SaveAllChanges();
        return await Task.FromResult(new Result());
    }

    public override async Task<Result> RemoveOtherPlaylist(IdPlayListIdUser request, ServerCallContext context)
    {
        var playlistUser = await uow.PlaylistUserRepository.FindEntityByAsync(x =>
            x.IdUser == request.IdUser && x.IdPlaylist == request.IdPlaylist);
        uow.PlaylistUserRepository.RemoveEntity(playlistUser);
        await uow.SaveAllChanges();
        return await Task.FromResult(new Result());
    }

    public override async Task<Result> AddTrackToPlaylist(PlayListIdAndTrackId request, ServerCallContext context)
    {
        var playlist = await uow.PlaylistRepository.FindEntityByAsync(x => x.IdPlaylist == request.PlaylistId);
        var track = await uow.MusicRepository.FindEntityByAsync(x => x.IdMusic == request.TrackId);
        uow.PlaylistMusicRepository.AddEntity(new PlaylistMusic()
        {
            Playlist = playlist,
            Music = track,
            IdPlaylist = playlist.IdPlaylist,
            IdMusic = track.IdMusic
        });
        await uow.SaveAllChanges();
        return await Task.FromResult(new Result());
    }
    public override async Task<Result> RemoveTrackFromPlaylist(PlayListIdAndTrackId request, ServerCallContext context)
    {
        var playlistMusic = await uow.PlaylistMusicRepository.FindEntityByAsync(x =>
            x.IdPlaylist == request.PlaylistId && x.IdMusic == request.TrackId);
        uow.PlaylistMusicRepository.RemoveEntity(playlistMusic);
        await uow.SaveAllChanges();
        return await Task.FromResult(new Result());
    }

    public override async Task<Playlists> FindPlaylistsByAuthor(String request, ServerCallContext context)
    {
        var playlists = await uow.PlaylistRepository.FindEntitiesByAsync(x=>x.PlaylistName == request.Word);
        return await Task.FromResult(new Playlists()
        {
            Playlists_ = { await _converter.ConvertPlaylistMusicsToMusicList(playlists) }
        });
    }

    public override async Task<Playlists> FindPlaylistsByName(String request, ServerCallContext context)
    {
        var playlistsCreators = await uow.PlaylistUserRepository.FindEntitiesByAsync(x => x.User.Name == request.Word);
        var playlists = playlistsCreators.Select(x => x.Playlist).ToList();
        return await Task.FromResult(new Playlists()
        {
            Playlists_ = { await _converter.ConvertPlaylistMusicsToMusicList(playlists) }
        });
    }

    
}