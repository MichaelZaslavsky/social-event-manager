using System.Net.Sockets;
using MailKit.Net.Smtp;
using MimeKit;
using Serilog;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Tests.Common.Helpers;

internal static class EmailHelpers
{
    public static async Task SendEmailAsync(SmtpClient client, MimeMessage message, IEnumerable<int> ports)
    {
        foreach (int port in ports)
        {
            // To make the localhost SMTP port work, run the "smtp4dev" command before running the email tests.
            // To make the fake SMTP port work, write "SimpleSmtpServer.Start(EmailData.FakePort);" at the beginning of the test.
            try
            {
                await client.ConnectAsync(GlobalConstants.Localhost, port, false);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (SocketException ex)
                when (ex.Message == ExceptionConstants.NoConnectionCouldBeMade
                    || ex.Message == ExceptionConstants.ConnectionRefused
                    || ex.Message == ExceptionConstants.CannotAssignRequestedAddress)
            {
                Log.Information("Email SMTP server does not set for port {port}.", port);
            }
        }
    }
}
