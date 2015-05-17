using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using SendGrid.SmtpApi;

namespace SendGridSmtpCsharpExample
{
    class SmtpMultipartExample
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
                // Html
                String htmlBody =
                    "<html>" +
                    "<body bgcolor=\"#d9edf7\" style=\"background-color: #d9edf7;\">" +
                    "こんにちは、nameさん<br>ようこそ〜HTMLメールの世界へ！<br>" +
                    "<img src=\"cid:123@456\">" +
                    "</body></html>";
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(
                    htmlBody, null, MediaTypeNames.Text.Html);
                //HTML内の画像のLinkedResourceを作成
                LinkedResource res = new LinkedResource(@"..\..\gif.gif", "image/gif");
                res.ContentId = "123@456";
                htmlView.LinkedResources.Add(res);

                msg.AlternateViews.Add(htmlView);
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
