using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class AlbumCoPublisher
{
    [Key]
    [Required]
    public long IdAlbumCoPublisher { get; set; }
    [Required]
    public long IdAlbum { get; set; }
    [Required]
    public long IdCoPublisher { get; set; }
    
    [ForeignKey("IdAlbum")]
    public Album Album { get; set; }
    [ForeignKey("IdCoPublisher")]
    public User User { get; set; }
}