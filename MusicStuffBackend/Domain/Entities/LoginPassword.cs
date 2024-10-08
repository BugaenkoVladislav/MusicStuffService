﻿using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;
[Index(nameof(Login),IsUnique = true)]
public class LoginPassword
{
    [Key]
    [Required]
    public long IdLoginPassword { get; set; }
    [Required]
    public string Login { get; set; }
    [Required]
    public string Password { get; set; }
}