using System;
using System.Net;
using System.Net.Mail;

namespace Nextgal.ECare.WebTeleasistencia.UserControls
{
    public partial class MoreInfo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Panel1.Visible = true;
            Panel2.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Button1.Enabled = false;
                Button1.Text = "Enviando...";
                try
                {
                    string body = "Fecha: " + DateTime.Now + "\nNombre: " + TextBoxName.Text.Trim() + "\nEMail: " +
                           TextBoxEMail.Text.Trim() + "\nComentarios:\n " + TextBoxComments.Text.Trim() + "\n\n--------------------------------------------------";
                    MailMessage mailObj = new MailMessage("info@nextgal.es", "mlagos@nextgal.es", "Formulario de contacto enviado desde Family eCare", body);
                    var smtp = new SmtpClient();
                    {
                        smtp.Host = "mail.nextgal.es";
                        smtp.Credentials = new NetworkCredential("info@nextgal.es", "oDjcyh4nd7djf9");
                        smtp.Timeout = 20000;
                    }
                    smtp.Send(mailObj);
                    Panel1.Visible = false;
                    Panel2.Visible = true;
                    Literal1.Text =
                        "<p style='color:green;'>Mensaje enviado!! Muchas gracias por tu colaboración, pronto nos pondremos en contacto contigo.</p>";
                }
                catch (Exception ex)
                {
                    Panel1.Visible = false;
                    Panel2.Visible = true;
                    Literal1.Text =
                        "<p style='color:red;'>Ohh!! No se ha podido enviar el mensaje. Por favor, inténtalo de nuevo transcurridos unos minutos. Disculpa las molestias.</p>";
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/");
        }
    }
}