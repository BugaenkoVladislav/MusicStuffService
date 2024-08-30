using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class TrackCoPublisher
{
    [Key]
    [Required]
    public long IdTrackCoPublisher { get; set; }
    [Required]
    public long IdTrack { get; set; }
    [Required]
    public long IdCoPublisher { get; set; }
    
    [ForeignKey("IdTrack")]
    public Music Track { get; set; }
    [ForeignKey("IdCoPublisher")]
    public User User { get; set; }
    
}