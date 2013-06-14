using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Nextgal.ECare.WebTeleasistencia.UserControls
{
    public partial class Scheduler : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture =
                    CultureInfo.CreateSpecificCulture("es-ES");
                month.Text = DateTime.Now.ToString("MMMM, yyyy");
                CalculateDays();
            }
        }

        private void CalculateDays()
        {
            System.Drawing.Color noMonth = System.Drawing.Color.FromArgb(150,150,150);
            int i=1;
            Label1.Text = "30";
            Label1.ForeColor = noMonth;
            Label2.Text = "31";
            Label2.ForeColor = noMonth;
            Label3.Text = "" + i++;
            Label4.Text = "" + i++;
            Label5.Text = "" + i++;
            Label6.Text = "" + i++;
            Label7.Text = "" + i++;
            Label8.Text = "" + i++;
            Label9.Text = "" + i++;
            Label10.Text = "" + i++;
            Label11.Text = "" + i++;
            Label12.Text = "" + i++;
            Label13.Text = "" + i++;
            Label14.Text = "" + i++;
            Label15.Text = "" + i++;
            Label16.Text = "" + i++;
            Label17.Text = "" + i++;
            Label18.Text = "" + i++;
            Label19.Text = "" + i++;
            Label20.Text = "" + i++;
            Label21.Text = "" + i++;
            Label22.Text = "" + i++;
            Label23.Text = "" + i++;
            Label24.Text = "" + i++;
            Label25.Text = "" + i++;
            Label26.Text = "" + i++;
            Label27.Text = "" + i++;
            Label28.Text = "" + i++;
            Label29.Text = "" + i++;
            Label30.Text = "" + i++;
            Label31.Text = "" + i++;
            Label32.Text = "" + i++;
            i = 1;
            Label33.Text = "" + i++;
            Label33.ForeColor = noMonth;
            Label34.Text = "" + i++;
            Label34.ForeColor = noMonth;
            Label35.Text = "" + i++;
            Label35.ForeColor = noMonth;
            Label36.Text = "" + i++;
            Label36.ForeColor = noMonth;
            Label37.Text = "" + i++;
            Label37.ForeColor = noMonth;
            Label38.Text = "" + i++;
            Label38.ForeColor = noMonth;
            Label39.Text = "" + i++;
            Label39.ForeColor = noMonth;
            Label40.Text = "" + i++;
            Label40.ForeColor = noMonth;
            Label41.Text = "" + i++;
            Label41.ForeColor = noMonth;
            Label42.Text = "" + i++;
            Label42.ForeColor = noMonth;

        }

        protected void saveEventBT_Click(object sender, EventArgs e)
        {

            Label lb = new Label();
            lb.Text = nameEvt.Text;
            lb.BackColor = Color.LightBlue;
            PlaceHolder1.Controls.Add(lb);
        }
    }

  
}