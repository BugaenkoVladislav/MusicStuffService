using System.Net.Mime;
using Domain.Domain.Entities;
using Grpc.Core;
using Infrastructure.Infrastructure;
using MusicStuffBackend;
using Album = MusicStuffBackend.Album;
using Music = Domain.Domain.Entities.Music;
using String = MusicStuffBackend.String;

namespace MusicManipulationService.Services;

public class AlbumMicroservice(ILogger<AlbumMicroservice> logger, UnitOfWork uow):Album.AlbumBase
{
    private Converter _converter = new Converter(uow);
    
    public override async Task<FullAlbumInfo> GetAlbum(Id request, ServerCallContext context)
    {
        var album = await uow.AlbumRepository.FindEntityByAsync(x => x.IdAlbum == request.MessageId);
        var albumCreators = await uow.AlbumCoPublisherRepository.FindEntitiesByAsync(x => x.IdAlbum == request.MessageId);
        var music = await uow.MusicRepository.FindEntitiesByAsync(x => x.IdAlbum == request.MessageId);
        var tracks = await _converter.ConvertMusicListToTracks(music);
        
        return await Task.FromResult(new FullAlbumInfo()
        {
            Tracks = { tracks },
            AlbumInfo = new AlbumMessage()
            {
                Name = album.Name,
                CoPublishers =
                {
                    albumCreators.Select(x=> x.IdCoPublisher)
                },
                PathPhoto = album.PathPhoto
            }
        });
    }

    public override async Task<Result> RemoveAlbum(Id request, ServerCallContext context)
    {
        var album = await uow.AlbumRepository.FindEntityByAsync(x => x.IdAlbum == request.MessageId);
        uow.AlbumRepository.RemoveEntity(album);
        await uow.SaveAllChanges();
        return await Task.FromResult(new Result()
        {

        });
    }

    public override async Task<Result> AddNewAlbum(AlbumMessage request, ServerCallContext context)
    {
        uow.AlbumRepository.AddEntity(new Domain.Domain.Entities.Album()
        {
            PathPhoto = request.PathPhoto,
            Name = request.Name,
        });
        await uow.SaveAllChanges();
        var album = await uow.AlbumRepository.FindEntityByAsync(x =>
            x.Name == request.Name && x.PathPhoto == request.PathPhoto);
        foreach (var i in request.CoPublishers)
        {
            var user = await uow.UserRepository.FindEntityByAsync(x => x.IdUser == i);
            uow.AlbumCoPublisherRepository.AddEntity(new AlbumCoPublisher()
            {
                Album = album,
                User = user,
                IdAlbum = album.IdAlbum,
                IdCoPublisher = user.IdUser
            });
            await uow.SaveAllChanges();
        }
        return await Task.FromResult(new Result()
        {
            
        });
    }

    public override async Task<Albums> FindAlbumsByName(String request, ServerCallContext context)
    {
        var albums = await uow.AlbumRepository.FindEntitiesByAsync(x => x.Name == request.Word);
        return await Task.FromResult(new Albums()
        {
            Albums_ = { await _converter.ConvertAlbumsToFullAlbumsInfos(albums) }
        });
    }

    public override async Task<Albums> FindAlbumsByAuthor(String request, ServerCallContext context)
    {
        var albumsCoPublishers = await uow.AlbumCoPublisherRepository.FindEntitiesByAsync(x => x.User.Name == request.Word);
        var albums = albumsCoPublishers.Select(x => x.Album).ToList();
        return await Task.FromResult(new Albums()
        {
            Albums_ = { await _converter.ConvertAlbumsToFullAlbumsInfos(albums) }
        });
    }
}