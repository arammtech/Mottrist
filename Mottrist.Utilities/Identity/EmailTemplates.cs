using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Mottrist.Utilities.Identity
{
    public static class EmailTemplates
    {
        
        public static string GetVerificationCodeEmailBody(string verificationCode,int VerificationCodeKeyTime)
        {
                            return $@"
                <!DOCTYPE html>
                <html lang='ar'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            background-color: #f4f4f4;
                            margin: 0;
                            padding: 0;
                        }}
                        .container {{
                            max-width: 600px;
                            margin: 20px auto;
                            background-color: #ffffff;
                            border-radius: 8px;
                            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
                            overflow: hidden;
                        }}
                        .header {{
                            background-color: #007bff;
                            color: #ffffff;
                            padding: 20px;
                            text-align: center;
                        }}
                        .content {{
                            padding: 20px;
                            text-align: right; /* Align text to the right for Arabic */
                        }}
                        .code {{
                            display: inline-block;
                            font-size: 24px;
                            font-weight: bold;
                            color: #007bff;
                            padding: 10px 20px;
                            border: 2px solid #007bff;
                            border-radius: 5px;
                            margin: 20px 0;
                        }}
                        .footer {{
                            padding: 10px;
                            text-align: center;
                            font-size: 12px;
                            color: #999999;
                            background-color: #f9f9f9;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h1>تحقق من حسابك</h1>
                        </div>
                        <div class='content'>
                            <p>مرحبًا،</p>
                            <p>تم طلب رمز التحقق لحسابك. استخدم الرمز أدناه لإكمال عملية التحقق:</p>
                            <p>رمز التحقق ستنتهي صلاحيته بعد {VerificationCodeKeyTime} دقيقة من الآن.</p>
                            <div class='code'>{verificationCode}</div>
                            <p>إذا لم تكن قد طلبت هذا، يرجى تجاهل هذه الرسالة.</p>
                            <p>شكرًا لك!</p>
                        </div>
                        <div class='footer'>
                            <p>حقوق النشر © {DateTime.Now.Year} شركتنا. جميع الحقوق محفوظة.</p>
                        </div>
                    </div>
                </body>
                </html>
                ";
        }

        public static string GetEmailConfirmEmailBody(string callbackUrl)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <title>Email Confirmation</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            text-align: center;
            padding: 20px;
        }}
        .container {{
            background: white;
            padding: 20px;
            border-radius: 8px;
            max-width: 500px;
            margin: auto;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
        }}
        .button {{
            display: inline-block;
            padding: 12px 20px;
            font-size: 16px;
            color: white !important;
            background-color: #007bff;
            text-decoration: none;
            border-radius: 5px;
            margin-top: 20px;
        }}
        .button:hover {{
            background-color: #0056b3;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <h2>Confirm Your Email Address</h2>
        <p>Thank you for signing up! Please confirm your email address by clicking the button below:</p>
        <a href='{System.Net.WebUtility.HtmlEncode(callbackUrl)}' class='button'>Confirm Email</a>
        <p>If you didn’t request this, you can safely ignore this email.</p>
    </div>
</body>
</html>";
        }

    }
}
