namespace NGOT.Common.Constants;

public static class Template
{
    public const string ForgotPasswordEmailTemplate = @"<!DOCTYPE html>
    <html>
      <head>
        <meta charset="" UTF-8"">
        <title>Forgot Password</title>
      </head>
      <body style="" font-family: Arial, sans-serif;"">
        <div style="" background-color: #f4f4f4; padding: 20px; text-align: center;"">
          <h2>Forgot Password</h2>
          <p>Dear {0},</p>
          <p>We received a request to reset your password. Please click the link below to reset your password:</p>
          <p>
            <a href=""{1}"" style="" background-color: #4CAF50; color: white; padding: 10px 20px; text-decoration: none; border-radius: 4px;"">Reset Password</a>
          </p>
          <p>If you didn't make this request, you can ignore this email and your password will not be changed.</p>
          <p>Thank you,</p>
        </div>
      </body>
    </html>";
}