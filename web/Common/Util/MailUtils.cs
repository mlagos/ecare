using System;
using System.Net;
using System.Net.Mail;

namespace Nextgal.ECare.Common.Util
{
    public class MailUtils
    {
        private static string SMTP_SERVER = "";
        private static string SMTP_USER = "";
        private static string SMTP_PASSWORD = "";
        private static string FROM = "";

        public MailUtils(string smtp_server, string smtp_user, string smtp_password_enc, string from)
        {
            SMTP_SERVER = smtp_server;
            SMTP_USER = smtp_user;
            SMTP_PASSWORD = EncryptionUtils.Decrypt(smtp_password_enc);
            FROM = from;
        }

        /// <summary>
        /// Envia un e-mail a to.
        /// </summary>
        /// <param name="to">Puede ser una dirección de correo o varias separadas por ';'</param>
        /// <param name="subject">Asunto del e-mail</param>
        /// <param name="body">Cuerpo del e-mail</param>
        public void SendMail(string to, string subject, string body)
        {
            try
            {
                var msg = new MailMessage();
                foreach (string para in to.Split(';'))
                {
                    msg.To.Add(para.Trim());
                }

                msg.Subject = subject;
                msg.SubjectEncoding = System.Text.Encoding.UTF8;
                msg.Body = body;
                msg.BodyEncoding = System.Text.Encoding.UTF8;
                msg.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();

                smtp.UseDefaultCredentials = false;
                NetworkCredential credencial = new NetworkCredential(SMTP_USER, SMTP_PASSWORD);
                smtp.Credentials = credencial;
                msg.From = new MailAddress(FROM);

                smtp.Port = 25;
                smtp.Host = SMTP_SERVER;

                smtp.Send(msg);
            }
            catch (Exception ex)
            {
            }
        }
    }
}