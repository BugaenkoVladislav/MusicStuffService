using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class User
{
    [Key]
    [Required]
    public long IdUser { get; set; }
    [Required]
    public long IdRole { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public long IdLoginPassword { get; set; }
    
    [ForeignKey("IdLoginPassword")]
    public LoginPassword LoginPassword { get; set; }
    
    [ForeignKey("IdRole")]
    public Role Role { get; set; }
    
}