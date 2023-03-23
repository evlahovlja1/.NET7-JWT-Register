namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Authorization;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.RegisterCodes;
using WebApi.Models.Users;
using WebApi.Services;

[Authorize]
[ApiController]
[Route("[controller]")]
public class RegisterController : ControllerBase
{
    private IUserService _userService;
    private IRegisterCodeService _registerCodeService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public RegisterController(
        IUserService userService,
        IRegisterCodeService registerCodeService, 
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _userService = userService;
        _registerCodeService = registerCodeService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest model)
    {
        // var email = new MimeMessage();
        // email.From.Add(MailboxAddress.Parse("yasmin11@ethereal.email"));
        // email.To.Add(MailboxAddress.Parse("evlahovlja1@etf.unsa.ba"));
        // email.Subject = "Testirung bitte";
        // email.Body = new TextPart(MimeKit.Text.TextFormat.Text) {Text = "Testirung bitte"};

        // using var smtp = new SmtpClient();
        // smtp.Connect("smtp.ethereal.email", 587, MailKit.Security.SecureSocketOptions.StartTls);
        // smtp.Authenticate("yasmin11@ethereal.email", "VFGeaGQ2KEpkyBJThZ");
        // smtp.Send(email);
        // smtp.Disconnect(true);


        var user =  _userService.Register(model);

        Random random = new Random();
        String code = random.Next(1000,9999).ToString();

        RegisterCode registerCode = new RegisterCode {
            Id = new Guid(),
            Value = code,
            Activated = false,
            User = user
        };

        await _registerCodeService.SaveCodeAsync(registerCode);

        EmailSender emailSender = new EmailSender();
        await emailSender.SendEmailAsync(user.Email, code);
        
        
        return Ok(new { message = "Registration successful" });
    }

    [AllowAnonymous]
    [HttpPost("confirm")]
    public async Task<IActionResult> Confirm(ConfirmationRequest confirmationRequest) {
        bool activationResult = await _registerCodeService.ActivateCodeAsync(confirmationRequest.Code, confirmationRequest.Username);
        if (activationResult) {
            return Ok(new { message = "Activation successful" });
        }
        else {
            return BadRequest(new { message = "Username or code incorrect!" });
        }
    }
}