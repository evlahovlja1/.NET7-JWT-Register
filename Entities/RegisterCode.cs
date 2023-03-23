namespace WebApi.Entities;

using System.Text.Json.Serialization;

public class RegisterCode
{

    public Guid Id { get; set; }
    public string Value { get; set; }
      
    public Guid UserId { get; set; }
    public UserRegister User { get; set; }   
    public bool Activated { get; set; }
}