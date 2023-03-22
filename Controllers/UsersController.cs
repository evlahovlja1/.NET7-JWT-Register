namespace WebApi.Controllers;

using AutoMapper;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;
using WebApi.Authorization;
using WebApi.Helpers;
using WebApi.Models.Users;
using WebApi.Services;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public UsersController(
        IUserService userService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _userService = userService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        var response = _userService.Authenticate(model);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest model)
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


        _userService.Register(model);

        EmailSender emailSender = new EmailSender();
        emailSender.SendEmailAsync("evlahovlja1@etf.unsa.ba", "http://localhost:4000/users/confirm").GetAwaiter().GetResult();
        
        
        return Ok(new { message = "Registration successful" });
    }

    [AllowAnonymous]
    [HttpGet("confirm")]
    public IActionResult Confirm() {
        return Ok(new { message = "Confirmation successful" });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var user = _userService.GetById(id);
        return Ok(user);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateRequest model)
    {
        _userService.Update(id, model);
        return Ok(new { message = "User updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _userService.Delete(id);
        return Ok(new { message = "User deleted successfully" });
    }
}