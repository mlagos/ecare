using System;
using System.Web.UI;

namespace Nextgal.ECare.WebTeleasistencia
{
    public partial class Loading : Page
    {
        protected string newPath;
        protected void Page_Load(object sender, EventArgs e)
        {
            newPath = Request.ServerVariables["QUERY_STRING"];
        }
    }
}