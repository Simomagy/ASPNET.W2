using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace _03_Api_Solo.Models;

public class UserDto
{
    [Required]
    [SwaggerSchema(description: "Username or Email")]
    public string Username { get; set; } = string.Empty;

    [Required]
    [StringLength(36)]
    [SwaggerSchema(description: "Max 36 Chars")]
    public string Pwd { get; set; } = string.Empty;

    [Required]
    [StringLength(5)]
    [SwaggerSchema(description: "'admin' or 'user'")]
    public string Role { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    [SwaggerSchema(description: "Max 100 Chars")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    [SwaggerSchema(description: "Max 100 Chars")]
    public string Surname { get; set; } = string.Empty;

    [Required]
    [SwaggerSchema(description: "mm/dd/yyyy")]
    public DateTime Dob { get; set; }

    [SwaggerSchema(description: "Direct link")]
    public string Propic { get; set; } = string.Empty;
}
