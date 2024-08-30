using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class PlaylistMusic
{
    [Key]
    [Required]
    public  long IdPlaylistMusic { get; set; }
    [Required]
    public long IdPlaylist { get; set; }
    [Required]
    public long IdMusic { get; set; }
    
    [ForeignKey("IdPlaylist")]
    public Playlist Playlist { get; set; }
    [ForeignKey("IdMusic")]
    public Music Music { get; set; }
}