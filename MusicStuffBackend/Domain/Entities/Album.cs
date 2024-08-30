using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Album
{
    [Key] 
    [Required] 
    public long IdAlbum { get; set; }
    [Required] 
    public string Name { get; set; }
    [Required] 
    public string PathPhoto { get; set; }
}