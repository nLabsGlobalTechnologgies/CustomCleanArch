using CleanArchitecture.Application.Utilities;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace CleanArchitecture.Application.Features.Auth.Register;

public sealed class RegisterCommandHandler(UserManager<AppUser> userManager) : IRequestHandler<RegisterCommand, Result<string>>
{
    public async Task<Result<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var isEmailExists = await userManager.Users.AnyAsync(u => u.Email == request.Email);

        if (isEmailExists)
        {
            return Result<string>.Failure("Email address already Exists");
        }

        var isUserNameExists = await userManager.Users.AnyAsync(u => u.UserName == request.UserName);

        if (isUserNameExists)
        {
            return Result<string>.Failure("UserName already Exists");
        }

        if (request.RePassword != request.Password)
        {
            return Result<string>.Failure("Password and password repeat do not match");
        }


        AppUser? user = new()
        {
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            Email = request.Email.ToLower().Trim(),
            UserName = request.UserName.ReplaceAllTurkishCharacters().ToLower().Trim()
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errorMessages = result.Errors.Select(e => e.Description).ToList();
            return Result<string>.Failure(errorMessages);
        }

        return Result<string>.Succeed("User created successfully");
    }
}