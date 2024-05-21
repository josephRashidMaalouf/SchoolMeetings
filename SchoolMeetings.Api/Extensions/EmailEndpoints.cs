using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace SchoolMeetings.Api.Extensions;

public static class EmailEndpoints
{
    public static IEndpointRouteBuilder MapEmailEndPoints(this IEndpointRouteBuilder app, WebApplicationBuilder builder)
    {
        var group = app.MapGroup("/email");

        //group.MapPost("", SendConfirmationEmailToParentAsync);
        group.MapPost("", async (string emailAddress, string message) =>
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("bokalararen@gmail.com"));
            email.To.Add(MailboxAddress.Parse(emailAddress));
            email.Subject = "Bokningsbekräftelse för ditt utvecklingssamtal";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = message
            };

            var password = builder.Configuration["Gmail:Password"];
            

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("bokalararen@gmail.com", password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

            return Results.Ok();
        });

        return app;
    }

    public static async Task<IResult> SendConfirmationEmailToParentAsync(string emailAddress, string message)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse("bokalararen@gmail.com"));
        email.To.Add(MailboxAddress.Parse("emailAddress"));
        email.Subject = "Bokningsbekräftelse för ditt utvecklingssamtal";
        email.Body = new TextPart(TextFormat.Html)
        {
            Text = message
        };

        //builder.Configuration["Gmail:Password"]
         

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync("smtp.gmail.com", 465, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync("username", "");
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);

        return Results.Ok();
    }
}