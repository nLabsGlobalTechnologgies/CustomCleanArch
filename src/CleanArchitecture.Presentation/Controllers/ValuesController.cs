using CleanArchitecture.Application.Services;
using FluentEmail.Core;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public sealed class ValuesController : ControllerBase
{
    public readonly IFluentEmail _fluentEmail;

    public ValuesController(IFluentEmail fluentEmail)
    {
        _fluentEmail = fluentEmail;
    }

    [HttpGet]
    public async Task<IActionResult> SendTestEmail(CancellationToken cancellationToken)
    {

        var fromEmail = "admin@admin.com";
        var toEmail = "test@admin.com";
        var toEmailConfirmCode = "123456";
        var subject = "";

        var response = await EmailService.SendEmailAsync(fromEmail, toEmail, toEmailConfirmCode, subject, cancellationToken);

        return Ok(response);

        //var response2 = await EmailService.SendFluentEmailAsync(_fluentEmail, toEmail, toEmailConfirmCode, subject, cancellationToken);

        //return Ok(response2);

    }
}
