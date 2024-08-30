using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Playlist
{
    [Key]
    [Required]
    public long IdPlaylist { get; set; }
    [Required]
    public string PlaylistName { get; set; } 
    [Required]
    public string PhotoPath { get; set; }
}
    
