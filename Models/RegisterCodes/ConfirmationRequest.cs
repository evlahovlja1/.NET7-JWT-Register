namespace WebApi.Models.RegisterCodes;

using System.ComponentModel.DataAnnotations;

public class ConfirmationRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Code { get; set; }
}