using System.ComponentModel.DataAnnotations;

namespace Domain.Domain.Entities;

public class Role
{
    [Key]
    [Required]
    public long IdRole { get; set; }
    [Required]
    public string RoleName { get; set; }
}