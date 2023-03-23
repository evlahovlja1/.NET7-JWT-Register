namespace WebApi.Entities;

using System.Text.Json.Serialization;

public class UserRegister
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }

    public RegisterCode RegisterCode { get; set; }
}