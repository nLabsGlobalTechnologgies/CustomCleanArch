using FluentEmail.Core;

namespace CleanArchitecture.Application.Services;
public static class EmailService
{
    private static IFluentEmail _fluentEmail;

    public static void Initialize(IFluentEmail fluentEmail)
    {
        _fluentEmail = fluentEmail;
    }

    public static async Task<string> SendEmailAsync(string fromEmail, string toEmail, CancellationToken cancellationToken)
    {
        if (_fluentEmail == null)
        {
            // Hata yönetimi, FluentEmail başlatılmamışsa bir hata fırlatılabilir.
            throw new InvalidOperationException("FluentEmail is not initialized.");
        }

        var subject = Subject;
        var body = Body(fromEmail);
        var email = await _fluentEmail
            .To(toEmail)
            .Subject(subject)
            .Body(body, true)
            .SendAsync(cancellationToken);

        return "Mail gönderme işlemi başarıyla tamamlandı!";
    }

    public static string Subject => "Test Emaili";

    public static string Body(string fromEmail)
    {
        var body = $@"
                <h1>bu bir <span style=""color: red"">bu email {fromEmail} tarafından gönderildi <br>test mailidir</span></h1>
            ";
        return body;
    }
}
