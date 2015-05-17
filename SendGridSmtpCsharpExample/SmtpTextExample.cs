using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using SendGrid.SmtpApi;

namespace SendGridSmtpCsharpExample
{
    class SmtpTextExample
    {
        // 設定情報
        static String USERNAME = ConfigurationManager.AppSettings["SENDGRID_USERNAME"];
        static String PASSWORD = ConfigurationManager.AppSettings["SENDGRID_PASSWORD"];
        static List<String> TOS = new List<String>(ConfigurationManager.AppSettings["TOS"].Split(','));
        static List<String> NAMES = new List<String>(ConfigurationManager.AppSettings["NAMES"].Split(','));
        static String FROM = ConfigurationManager.AppSettings["FROM"];

        static void Main(string[] args)
        {
            MailMessage msg = new MailMessage();
            SmtpClient sc = new SmtpClient("smtp.sendgrid.com", 587);
            try
            {
                // From
                msg.From = new MailAddress(FROM);
                // ダミーの宛先(X-SMTPAPIの宛先が優先される)
                msg.To.Add(new MailAddress(FROM));
                // Subject
                msg.Subject = "こんにちはSendGrid";
                // Body
                msg.Body = "こんにちは、nameさん\r\nようこそ〜テキストメールの世界へ！";
                // X-SMTPAPIヘッダ
                msg.Headers.Add("X-SMTPAPI", createSmtpapi(TOS, NAMES));

                // 送信
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                //ユーザー名とパスワードを設定する
                sc.Credentials = new NetworkCredential(USERNAME, PASSWORD);
                //メッセージを送信する
                sc.Send(msg);
            }
            finally
            {
                msg.Dispose();
                sc.Dispose();
            }
        }

        private static String createSmtpapi(List<String> tos, List<String> names)
        {
            // Create a smtpapi instance
            var smtpapi = new Header();
            smtpapi.SetTo(tos);
            smtpapi.AddSubstitution("name", names);
            smtpapi.SetCategory("category1");
            Console.WriteLine(smtpapi.JsonString());
            return smtpapi.JsonString();
        }
    }
}
