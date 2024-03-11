using CleanArchitecture.Application.Features.Auth.Register;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Presentation.Abstractions;
using FluentEmail.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers;
public sealed class AuthController : ApiController
{
    public AuthController(IMediator mediator) : base(mediator){}

    [HttpPost]
    public async Task<IActionResult> Register(RegisterCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> SendTestEmail(CancellationToken cancellationToken)
    {
        //using (MailMessage mail = new MailMessage())
        //{
        //    mail.From = new MailAddress("test@test.com");
        //    mail.To.Add("admin@demo.com");
        //    mail.Subject ="subject";
        //    mail.Body = "body";
        //    mail.IsBodyHtml = true;
        //    using (SmtpClient smtp = new SmtpClient("localhost", 2525))
        //    {
        //        smtp.UseDefaultCredentials = false;
        //        //smtp.Credentials = new NetworkCredential("", "");
        //        smtp.EnableSsl = false;
        //        //smtp.Port = 25;
        //        await smtp.SendMailAsync(mail);
        //    }
        //}
        //var subject = EmailService.Subject;
        var fromEmail = "admin@admin.com";
        var toEmail = "test@admin.com";
        var body = EmailService.Body("");
        var response = await EmailService.SendEmailAsync(fromEmail, toEmail, cancellationToken);
        //var email = await Email.From(fromEmail).To(toEmail).Subject(subject).Body(body).SendAsync(cancellationToken);

        return Ok(response);
    }
}
