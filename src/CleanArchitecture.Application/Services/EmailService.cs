﻿using FluentEmail.Core;
using System.Net;
using System.Net.Mail;
using TS.Result;

namespace CleanArchitecture.Application.Services;

public static class EmailService
{
    //My SendEmailCode
    public static async Task<Result<string>> SendEmailAsync(string fromEmail, string toEmail, string toEmailConfirmCode, string subject, CancellationToken cancellationToken)
    {
        using (MailMessage mail = new MailMessage())
        {
            mail.From = new MailAddress(fromEmail);
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = EmailBody(toEmailConfirmCode);
            mail.IsBodyHtml = true;
            using (SmtpClient smtp = new SmtpClient("localhost", 2525))
            {
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("", "");
                smtp.EnableSsl = false;
                //smtp.Port = 25;
                await smtp.SendMailAsync(mail);
            }
        }

        return Result<string>.Succeed("Email başarıyla gönderildi");
    }

    //FluentEmail SendEmailCode
    public static async Task<Result<string>> SendFluentEmailAsync(IFluentEmail fluentEmail, string toEmail, string toEmailConfirmCode, string subject, CancellationToken cancellationToken)
    {
        var _body = EmailService.EmailBody(toEmailConfirmCode);
        var _subject = subject;

        await fluentEmail.To(toEmail).Subject(_subject).Body(_body).SendAsync(cancellationToken);

        return Result<string>.Succeed("Email başarıyla gönderildi");
    }

    public static string EmailBody(string emailConfirmCode)
    {
        string body = @"
                        <!DOCTYPE html>
                        <html lang=""en"">
                        <head>
                            <meta charset=""UTF-8"">
                            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                            <title>Email Confirmation Code</title>
                            <style>
                                /* Stil özellikleri */
                                body {
                                    font-family: Arial, sans-serif;
                                    background-color: #f4f4f4;
                                    padding: 20px;
                                }
                                .container {
                                    max-width: 600px;
                                    margin: 0 auto;
                                    background-color: #fff;
                                    border-radius: 10px;
                                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                                    padding: 20px;
                                    text-align: center;
                                    justify-content: center;
                                    align-items: center;
                                }
                                .confirmation-code {
                                    display: flex;
                                    justify-content: center;
                                    align-items: center;
                                    margin-top: 20px;
                                    margin-left: 50px;
                                }
                                .digit-container {
                                    display: flex;
                                    width: auto; /* Kutu genişliğini artır */
                                    height: auto;
                                    border: 2px solid #007bff;
                                    border-radius: 10px;
                                    margin-right: 10px;
                                    font-size: 55px;
                                    font-weight: bold;
                                    color: #007bff;
                                    text-align: center;
                                    inherit: text-align;
                                }
                            </style>
                        </head>
                        <body>
                            <div class=""container"">
                                <h2 style=""color: #007bff;"">Email Confirmation Code</h2>
                                <p>Please use the following code to confirm your email:</p>
                                <div class=""confirmation-code"">
                                    <!-- Her bir rakam için ayrı bir kutu oluşturuluyor -->
                                    <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + emailConfirmCode[0] + @" </div></div>
                                    <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + emailConfirmCode[1] + @" </div></div>
                                    <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + emailConfirmCode[2] + @" </div></div>
                                    <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + emailConfirmCode[3] + @" </div></div>
                                    <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + emailConfirmCode[4] + @" </div></div>
                                    <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + emailConfirmCode[5] + @" </div></div>
                                </div>
                                <p style=""margin-top: 20px;"">This code will expire in 10 minutes.</p>
                            </div>
                        </body>
                        </html>
                        ";

        return body;
    }
}
