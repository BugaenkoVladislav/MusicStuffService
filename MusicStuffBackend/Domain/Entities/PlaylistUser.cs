using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class PlaylistUser
{
    [Key]
    [Required]
    public  long IdPlaylistUser { get; set; }
    [Required]
    public long IdPlaylist { get; set; }
    [Required]
    public long IdUser { get; set; }
    [Required]
    public bool IsCreator { get; set; }
    
    [ForeignKey("IdPlaylist")]
    public Playlist Playlist { get; set; }
    [ForeignKey("IdUser")]
    public User User { get; set; }
}