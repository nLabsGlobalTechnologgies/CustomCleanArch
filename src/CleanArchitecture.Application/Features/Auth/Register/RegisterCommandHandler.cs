using CleanArchitecture.Application.Services;
using CleanArchitecture.Application.Utilities;
using CleanArchitecture.Domain.Entities;
using FluentEmail.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace CleanArchitecture.Application.Features.Auth.Register;

public sealed class RegisterCommandHandler(UserManager<AppUser> userManager, IFluentEmail fluentEmail) : IRequestHandler<RegisterCommand, Result<string>>
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

        Random random = new();
        int emailConfirmCode = random.Next(111111, 999999);
        bool isEmailConfirmCodeExists = true;
        while (isEmailConfirmCodeExists)
        {
            isEmailConfirmCodeExists = await userManager.Users.AnyAsync(u => u.EmailConfirmCode == emailConfirmCode, cancellationToken);
            if (isEmailConfirmCodeExists)
            {
                emailConfirmCode = random.Next(111111, 999999);
            }
        }

        AppUser? user = new()
        {
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            Email = request.Email.ToLower().Trim(),
            UserName = request.UserName.ReplaceAllTurkishCharacters().ToLower().Trim(),
            EmailConfirmCode = emailConfirmCode
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errorMessages = result.Errors.Select(e => e.Description).ToList();
            return Result<string>.Failure(errorMessages);
        }

        var subject = "Verify Your Email";
        var response = 
            await EmailService.
            SendFluentEmailAsync(fluentEmail,user.Email,user.EmailConfirmCode.ToString() ?? "",subject,cancellationToken);
            
            
           //var response2 = await fluentEmail
           // .To(user.Email)
           // .Subject("Verify Your Email")
           // .Body(EmailService.EmailBody(emailConfirmCode.ToString()) ?? "", true)
           // .SendAsync(cancellationToken);

        return Result<string>.Succeed("User created successfully");
    }

    
}