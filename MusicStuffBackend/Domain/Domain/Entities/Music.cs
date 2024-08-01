using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Domain.Entities;

public class Music
{
    [Key]
    [Required]
    public long IdMusic { get; set; }
    [Required]
    public string NameOfTrack { get; set; } 
    [Required]
    public long IdAlbum {get;set;}
    [Required]
    public double Duration {get;set;}
    [Required]
    public string PathOfTrack {get;set;}
    
    [ForeignKey("IdAlbum")]
    public Album Album { get; set; }
    
}
    
