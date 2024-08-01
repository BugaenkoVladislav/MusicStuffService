using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Domain.Entities;

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
    
