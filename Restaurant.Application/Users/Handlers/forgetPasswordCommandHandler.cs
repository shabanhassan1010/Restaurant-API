using MailKit.Net.Smtp;
using MailKit.Security;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Restaurant.Application.EmailSettings;
using Restaurant.Application.Users.Commands.ForgetPassword;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exepections;
using System.IO.Pipelines;
using System.Net;

namespace Restaurant.Application.Users.Handlers
{
    public class forgetPasswordCommandHandler : IRequestHandler<forgetPasswordCommand, bool>
    {
        #region Constructor
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly ILogger<forgetPasswordCommandHandler> logger;
        private readonly EmailSetting emailSetting;

        public forgetPasswordCommandHandler(UserManager<User> userManager , IConfiguration configuration , 
             IOptions<EmailSetting> options , ILogger<forgetPasswordCommandHandler> logger)
        {
            _userManager = userManager;
            _config = configuration;
            this.logger = logger;
            emailSetting = options.Value;
        }
        #endregion

        public async Task<bool> Handle(forgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.forgotPasswordDto.Email);
            if (user == null)
                throw new NotFoundExepection("This Email Is Not Found");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            Console.WriteLine($"token: {token}");

            var encodedToken = Uri.EscapeDataString(token);
            var frontendUrl = _config["FrontendUrl"] ?? "http://localhost:4200";

            var resetLink = $"{frontendUrl}/reset-password?email={WebUtility.UrlEncode(request.forgotPasswordDto.Email)}&token={encodedToken}";

            var sendResult = await SendEmail(request.forgotPasswordDto.Email, resetLink);
            logger.LogInformation($"Token {token} Sent to Email {request.forgotPasswordDto.Email}");
            logger.LogInformation("Password reset email sent to {Email}", request.forgotPasswordDto.Email);
            return true;
        }

        #region Helper Method
        private async Task<string> SendEmail(string email, string resetLink)
        {
            try
            {
                using var client = new SmtpClient();
                await client.ConnectAsync(emailSetting.Host, emailSetting.Port, SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync(emailSetting.FromEmail, emailSetting.Password);

                var bodyBuilder = BuildResetPasswordEmailBody(resetLink);

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress("ToDoTask", emailSetting.FromEmail));
                mimeMessage.To.Add(new MailboxAddress("", email));
                mimeMessage.Subject = "إعادة تعيين كلمة المرور";
                mimeMessage.Body = bodyBuilder.ToMessageBody();

                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);

                return "Email sent successfully";
            }
            catch (Exception ex)
            {
                // Log the error (you should implement proper logging)
                return $"Failed to send email: {ex.Message}";
            }
        }
        private BodyBuilder BuildResetPasswordEmailBody(string resetLink)
        {
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $@"
            <div style=""font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; max-width: 600px; margin: auto; border: 1px solid #ddd; border-radius: 8px; padding: 20px; background-color: #f9f9f9; color: #333;"">
              <h3 style=""color: #007bff; text-align: center;"">إعادة تعيين كلمة المرور</h3>
              <p style=""font-size: 16px; line-height: 1.5;"">
                عزيزي المستخدم،
              </p>
              <p style=""font-size: 16px; line-height: 1.5;"">
                اضغط على الزر أدناه لإعادة تعيين كلمة المرور الخاصة بحسابك:
              </p>
              <p style=""text-align: center; margin: 30px 0;"">
                <a href=""{resetLink}"" style=""background-color: #007bff; color: white; text-decoration: none; padding: 14px 28px; border-radius: 6px; font-weight: bold; display: inline-block; font-size: 16px;"">
                  إعادة تعيين كلمة المرور
                </a>
              </p>
              <p style=""font-size: 14px; color: #555;"">
                إن لم تطلب إعادة تعيين كلمة المرور، يمكنك تجاهل هذا البريد.
              </p>
              <hr style=""border: none; border-top: 1px solid #eee; margin: 30px 0;"">
              <p style=""font-size: 12px; color: #999; text-align: center;"">
                هذا البريد الإلكتروني تم إرساله تلقائياً، الرجاء عدم الرد عليه.
              </p>
            </div>"
            };

            return bodyBuilder;
        }
        #endregion
    }
}
