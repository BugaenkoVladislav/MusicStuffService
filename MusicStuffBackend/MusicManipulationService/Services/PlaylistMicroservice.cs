using Domain.Domain.Entities;
using Grpc.Core;
using Infrastructure.Infrastructure;
using MusicStuffBackend;
using Music = Domain.Domain.Entities.Music;
using Playlist = MusicStuffBackend.Playlist;

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
        uow.PlaylistRepository.AddEntity(new Domain.Domain.Entities.Playlist()
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
}