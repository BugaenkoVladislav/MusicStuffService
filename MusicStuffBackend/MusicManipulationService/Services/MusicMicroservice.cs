using System.Net.Mime;
using Grpc.Core;
using Infrastructure.Infrastructure;
using MusicStuffBackend;
using String = MusicStuffBackend.String;

namespace MusicManipulationService.Services;

public class MusicMicroservice(ILogger<AlbumMicroservice> logger, UnitOfWork uow):Music.MusicBase
{
    private Converter _converter = new Converter(uow);
    public override async Task<Result> RemoveTrack(Id request, ServerCallContext context)
    {
        var music = await uow.MusicRepository.FindEntityByAsync(x => x.IdMusic == request.MessageId);
        uow.MusicRepository.RemoveEntity(music);
        await uow.SaveAllChanges();
        return await Task.FromResult(new Result());
    }

    public override async Task<Result> AddNewTrack(Track request, ServerCallContext context)
    {
        var album = await uow.AlbumRepository.FindEntityByAsync(x => x.IdAlbum == request.IdAlbum);
        uow.MusicRepository.AddEntity(new Domain.Domain.Entities.Music()
        {
            Album = album,
            IdAlbum = request.IdAlbum,
            NameOfTrack = request.NameOfTrack,
            PathOfTrack = request.PathOfTrack,
            Duration = request.Duration
        });
        await uow.SaveAllChanges();
        return await Task.FromResult(new Result()
        {
            
        });
    }

    public override async Task<Track> GetTrackById(Id request, ServerCallContext context)
    {
        var music = await uow.MusicRepository.FindEntityByAsync(x => x.IdMusic == request.MessageId);
        var coPublishers =
            await uow.TrackMusicCoPublisherRepository.FindEntitiesByAsync(x => x.IdTrack == music.IdMusic);
        return await Task.FromResult(new Track()
        {
            Duration = music.Duration,
            NameOfTrack = music.NameOfTrack,
            PathOfTrack = music.PathOfTrack,
            IdAlbum = music.IdAlbum,
            CoPublishers = { coPublishers.Select(x=> x.IdCoPublisher) }
        });
    }

    public override async Task<Tracks> FindTracksByAuthor(String request, ServerCallContext context)
    {
        var musicsCoPublishers = await uow.TrackMusicCoPublisherRepository.FindEntitiesByAsync(x => x.User.Name == request.Word);
        var musics = new List<Domain.Domain.Entities.Music>();
        foreach (var i in musicsCoPublishers)
        {
            var actualTrack = await uow.MusicRepository.FindEntityByAsync(x => x.IdMusic == i.IdTrack);
            musics.Add(actualTrack);
        }
        return await Task.FromResult(new Tracks()
        {
            Track = { await _converter.ConvertMusicsToTracks(musics) }
        });
    }

    public override async Task<Tracks> FindTracksByName(String request, ServerCallContext context)
    {
        var musics = await uow.MusicRepository.FindEntitiesByAsync(x=>x.NameOfTrack == request.Word);
        return await Task.FromResult(new Tracks()
        {
            Track = { await _converter.ConvertMusicsToTracks(musics) }
        });
    }

    
    
}