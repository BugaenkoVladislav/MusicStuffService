using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Domain.Entities;

public class Album
{
    [Key] 
    [Required] 
    public long IdAlbum { get; set; }
    [Required] 
    public string Name { get; set; }
    [Required] 
    public long IdCreator { get; set; }
    [Required] 
    public string PathPhoto { get; set; }
    public List<long> CoPublishersIds { get; set; }
    [ForeignKey("IdCreator")] 
    public User Creator { get; set; }
}