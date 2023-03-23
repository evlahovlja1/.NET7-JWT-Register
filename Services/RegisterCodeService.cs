namespace WebApi.Services;

using AutoMapper;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using WebApi.Authorization;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Users;

public interface IRegisterCodeService
{
    public Task SaveCodeAsync(RegisterCode code);
    public Task<bool> ActivateCodeAsync(string code, string username);
}

public class RegisterCodeService : IRegisterCodeService
{
    private DataContext _context;

    public RegisterCodeService(
        DataContext context)
    {
        _context = context;
    }

    public async Task SaveCodeAsync(RegisterCode code){
        _context.RegistrationCodes.Add(code);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ActivateCodeAsync(string code, string username) {
        var registerCode = await _context.RegistrationCodes.Include(rc => rc.User).FirstOrDefaultAsync(rc => rc.Value == code && rc.User.Username == username && rc.Activated == false);
        if (registerCode is null) {
            return false;
        }
        registerCode.Activated = true;
        _context.RegistrationCodes.Update(registerCode);
        await _context.SaveChangesAsync();

        return true;
    }
}