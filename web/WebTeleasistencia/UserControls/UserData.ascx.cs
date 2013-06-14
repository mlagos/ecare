using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nextgal.ECare.Model.Assisted.Dto;
using Nextgal.ECare.Model.Assisted.Facade;

namespace Nextgal.ECare.WebTeleasistencia.UserControls
{
    public partial class UserData : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AssistedDto assistedDto = AssistedFacade.GetInstance().FindAssistedByIdActive(
                            Convert.ToInt32(Request.QueryString["idActive"]));

                nombreLB.Text = assistedDto.Name;
                apellidosLB.Text = assistedDto.Surname;
                nacimientoLB.Text = assistedDto.BirthDate.ToString("dd/MM/yyyy");
                identificadorLB.Text = assistedDto.Identifier;
                telefonoLB.Text = assistedDto.Phone;
                //foto.ImageUrl = assistedDto.ImagePath;

                if ((assistedDto.ImagePath == null) || (assistedDto.ImagePath.Trim().Count() == 0))
                    foto.ImageUrl = "~/" + ConfigurationManager.AppSettings["AssistedImageUnavailable"];
                else
                    foto.ImageUrl = "~/"+assistedDto.ImagePath;

                foto.Height = 90;
            }
        }
    }
}