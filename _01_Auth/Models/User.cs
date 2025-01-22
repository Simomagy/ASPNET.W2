using MSSTU.DB.Utility;

namespace _01_Auth.Models;

public class User : Entity
{
    public string Username { get; set; } = string.Empty;
    public string Pwd { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public DateTime Dob { get; set; }
    public string Propic { get; set; } = string.Empty;
}
