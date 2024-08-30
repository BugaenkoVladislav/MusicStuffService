using Domain.Entities;
using Infrastructure;
using Infrastructure;
using MusicStuffBackend;
using Album = Domain.Entities.Album;
using Music = Domain.Entities.Music;
using Playlist = MusicStuffBackend.Playlist;

namespace MusicManipulationService.Services;

public class Converter(UnitOfWork uow)
{
     public async Task<List<Track>> ConvertMusicsToTracks(List<Music> musics)
    {
        var tracks = new List<Track>();
        foreach (var i in musics)
        {
            var trackCreators = await uow.TrackMusicCoPublisherRepository.FindEntitiesByAsync(x => x.IdTrack == i.IdMusic);
            tracks.Add(new Track()
            {
                Duration = i.Duration,
                NameOfTrack = i.NameOfTrack,
                PathOfTrack = i.PathOfTrack,
                CoPublishers = { trackCreators.Select(x=>x.IdCoPublisher) },
                IdAlbum = i.IdAlbum
            });
        }
        return tracks;
    }
    public async Task<List<Track>> ConvertMusicListToTracks(List<Music> music)
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
                IdAlbum = i.Album.IdAlbum
            };
            tracks.Add(actualTrack);
        }
        return tracks;
    }
    public async Task<List<FullAlbumInfo>> ConvertAlbumsToFullAlbumsInfos(List<Album> albums)
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

    public async Task<List<Track>> ConvertPlaylistMusicsToMusicList(List<PlaylistMusic> list)
    {
        var musics = new List<Music>();
        foreach (var i in list)
        {
            var track = await uow.MusicRepository.FindEntityByAsync(x => x.IdMusic == i.IdMusic);
            musics.Add(track);
        }
        var tracks = await ConvertMusicListToTracks(musics);
        return tracks;
    }

    public async Task<List<FullPlayListInfo>> ConvertPlaylistMusicsToMusicList(List<Domain.Entities.Playlist> list)
    {
        var playlists = new List<FullPlayListInfo>();
        foreach (var i in list)
        {
            var creator =
                await uow.PlaylistUserRepository.FindEntityByAsync(x =>
                    x.IsCreator == true && x.IdPlaylist == i.IdPlaylist);
            var musics = await uow.PlaylistMusicRepository.FindEntitiesByAsync(x => x.IdPlaylist == i.IdPlaylist);
            var tracks = await ConvertPlaylistMusicsToMusicList(musics);
            playlists.Add(new FullPlayListInfo()
            {
                Track = { tracks },
                PlaylistInfo = new PlayList()
                {
                    PlaylistName = i.PlaylistName,
                    PhotoPath = i.PhotoPath,
                    IdCreator = creator.IdUser
                }
            });
        }
        return playlists;

    }
    
}