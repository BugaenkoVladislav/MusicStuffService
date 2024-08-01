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
    private async Task<List<Track>> ConvertMusicListToTracks(List<Music> music)
    {
        var tracks = new List<Track>();
        foreach(var i in music)
        {
            var trackCreators =
                await uow.TrackMusicCoPublisherRepository.FindEntitiesByAsync(x => x.IdTrack == i.IdMusic);
            var actualTrack = new Track()
            {
                CoPublishers =
                {
                    trackCreators.Select(x=>x.IdCoPublisher)
                },
                Duration = i.Duration,
                NameOfTrack = i.NameOfTrack,
                PathOfTrack = i.PathOfTrack,
                NameAlbum = i.Album.Name
            };
            tracks.Add(actualTrack);
        }
        return tracks;
    }
    
    public override async Task<FullAlbumInfo> GetAlbum(Id request, ServerCallContext context)
    {
        var album = await uow.AlbumRepository.FindEntityByAsync(x => x.IdAlbum == request.MessageId);
        var albumCreators = await uow.AlbumCoPublisherRepository.FindEntitiesByAsync(x => x.IdAlbum == request.MessageId);
        var music = await uow.MusicRepository.FindEntitiesByAsync(x => x.IdAlbum == request.MessageId);
        var tracks = await ConvertMusicListToTracks(music);
        
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
        var fullAlbumsInfos = await ConvertAlbumsToFullAlbumsInfos(albums);
        return await Task.FromResult(new Albums()
        {
            Albums_ = { fullAlbumsInfos }
        });
    }

    public override async Task<Albums> FindAlbumsByAuthor(String request, ServerCallContext context)
    {
        var albumsCoPublishers = await uow.AlbumCoPublisherRepository.FindEntitiesByAsync(x => x.User.Name == request.Word);
        var albums = albumsCoPublishers.Select(x => x.Album).ToList();
        var fullAlbumsInfos = await ConvertAlbumsToFullAlbumsInfos(albums);
        return await base.FindAlbumsByAuthor(request, context);
    }

    private async Task<List<FullAlbumInfo>> ConvertAlbumsToFullAlbumsInfos(List<Domain.Domain.Entities.Album> albums)
    {
        var fullAlbumsInfos = new List<FullAlbumInfo>();
        foreach (var i in albums)
        {
            var music = await uow.MusicRepository.FindEntitiesByAsync(x => x.IdAlbum == i.IdAlbum);
            var tracks = await ConvertMusicListToTracks(music);
            var albumCreators = await uow.AlbumCoPublisherRepository.FindEntitiesByAsync(x => x.IdAlbum == i.IdAlbum);
            
            fullAlbumsInfos.Add(new FullAlbumInfo()
            {
                Tracks =
                {
                    tracks
                },
                AlbumInfo = new AlbumMessage()
                {
                    Name = i.Name,
                    PathPhoto = i.PathPhoto,
                    CoPublishers =
                    {
                        albumCreators.Select(x=> x.IdCoPublisher)
                    }
                }
            });
        }
        return fullAlbumsInfos;
    }
}