using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nextgal.ECare.Common.Util;
using Nextgal.ECare.Controller;
using Nextgal.ECare.Model.User.Dto;
using Nextgal.ECare.Model.User.Facade;

namespace Nextgal.ECare.WebTeleasistencia
{
    public partial class UpdateUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            passTB.Attributes.Add("value", passTB.Text);
            if (!IsPostBack)
            {
                UserDto dto = UserFacade.GetInstance().FindUser(SessionManager.GetIdUser(Context));
                loginTB.Text = dto.Login;
                nameTB.Text = dto.Name;
                surnameTB.Text = dto.Surname;
                passTB.Text = EncryptionUtils.Decrypt(dto.Password);
                phoneTB.Text = dto.Telephone;
                emailTB.Text = dto.Email;
                passTB.Attributes.Add("value", passTB.Text);
            }
        }

        protected void guardar_Click(object sender, EventArgs e)
        {
            string password = "";
            if (passTB.Text.Trim().Count()==0)
            {
                password = UserFacade.GetInstance().FindUser(SessionManager.GetIdUser(Context)).Password;
            }else
            {
                password = EncryptionUtils.Encrypt(passTB.Text.Trim());
            }
            UserDto dto = new UserDto(SessionManager.GetIdUser(Context), loginTB.Text.Trim(),
                password, SessionManager.GetIdFamily(Context), nameTB.Text.Trim(),
                surnameTB.Text.Trim(),phoneTB.Text.Trim(),emailTB.Text.Trim());
            UserFacade.GetInstance().UpdateUser(dto);
            msg.Text = Resources.Resources.saveDataOk;
        }

    }
}
