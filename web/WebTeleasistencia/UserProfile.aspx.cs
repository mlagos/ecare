using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.Web.UI.WebControls;
using Nextgal.ECare.Controller;
using Nextgal.ECare.Model.Message.Dto;
using Nextgal.ECare.Model.Assisted.Dto;
using Nextgal.ECare.Model.Assisted.Facade;
using Nextgal.ECare.WebTeleasistencia.App_Code.MessageManager;

namespace Nextgal.ECare.WebTeleasistencia
{
    public partial class UserProfile : Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["idActive"]))
                {

                    AssistedDto dto = AssistedFacade.GetInstance().FindAssistedByIdActive(Convert.ToInt32(Request.QueryString["idActive"]));

                    nombreTB.Text = dto.Name;
                    apellidosTB.Text = dto.Surname;
                    fechaTB.Text = dto.BirthDate.ToString("dd/MM/yyyy");
                    identificadorTB.Text = dto.Identifier;
                    telefonoTB.Text = dto.Phone;
                    sosNumero1.Text = dto.Sos1_Phone;
                    sosNombre1.Text = dto.Sos1_Name;
                    sosRecall1.Text = dto.Sos1_Recall_Count.ToString();
                    sosActivado1.Checked = dto.Sos1_Enabled;
                    sosNumero2.Text = dto.Sos2_Phone;
                    sosNombre2.Text = dto.Sos2_Name;
                    sosRecall2.Text = dto.Sos2_Recall_Count.ToString();
                    sosActivado2.Checked = dto.Sos2_Enabled;
                    sosNumero3.Text = dto.Sos3_Phone;
                    sosNombre3.Text = dto.Sos3_Name;
                    sosRecall3.Text = dto.Sos3_Recall_Count.ToString();
                    sosActivado3.Checked = dto.Sos3_Enabled;
                    allowMinimize.Checked = dto.AllowMinimize;

                    if ((dto.ImagePath == null) || (dto.ImagePath.Trim().Count() == 0))
                        imagen.ImageUrl = ConfigurationManager.AppSettings["AssistedImageUnavailable"];
                    else
                        imagen.ImageUrl = dto.ImagePath;
                    imagen.Height = 90;

                    guardar.Text = "Actualizar";
                }
                else
                {
                    imagen.ImageUrl = ConfigurationManager.AppSettings["AssistedImageUnavailable"];
                }
            }
        }

        private bool wrongImageSize(HttpPostedFile file)
        {
            {
                using (Bitmap bitmap = new Bitmap(file.InputStream, false))
                {
                    if (bitmap.Width <= 60 && bitmap.Height <= 90)
                        return false;
                    return true;
                }
            }
        }

        protected void guardar_Click(object sender, EventArgs e)
        {
            msg.Visible = false;
            bool imageTooBig = false;
            bool sosError = false;
            try
            {
                int idActive;
                AssistedDto assistDto;

                if ((subeImagen.HasFile) && (wrongImageSize(subeImagen.PostedFile)))
                {
                    imageTooBig = true;
                    throw new Exception();
                }


                if (sosRecall1.Text.Trim().Count() == 0) sosRecall1.Text = "0";
                if (sosRecall2.Text.Trim().Count() == 0) sosRecall2.Text = "0";
                if (sosRecall3.Text.Trim().Count() == 0) sosRecall3.Text = "0";
                if (!String.IsNullOrEmpty(Request.QueryString["idActive"]))
                {
                    idActive = Convert.ToInt32(Request.QueryString["idActive"]);
                }
                else
                {
                    idActive = -1;
                }

                AssistedDto dto = new AssistedDto(idActive, SessionManager.GetIdFamily(Context),
                                                  nombreTB.Text,
                                                  apellidosTB.Text,
                                                  DateTime.Parse(fechaTB.Text),
                                                  "",
                                                  identificadorTB.Text,
                                                  telefonoTB.Text,
                                                  sosNumero1.Text,
                                                  sosNombre1.Text,
                                                  Convert.ToInt32(sosRecall1.Text),
                                                  sosActivado1.Checked,
                                                  sosNumero2.Text,
                                                  sosNombre2.Text,
                                                  Convert.ToInt32(sosRecall2.Text),
                                                  sosActivado2.Checked,
                                                  sosNumero3.Text,
                                                  sosNombre3.Text,
                                                  Convert.ToInt32(sosRecall3.Text),
                                                  sosActivado3.Checked,
                                                  allowMinimize.Checked,
                                                  ""
                    );



                if (sosActivado2.Checked && (sosNumero2.Text.Trim().Count() == 0) ||
                    (sosRecall2.Text.Trim().Count() == 0))
                {
                    sosN2.Visible = true;
                    sosR2.Visible = true;
                    sosError = true;

                }
                else
                {
                    sosN2.Visible = false;
                    sosR2.Visible = false;
                }

                if (sosActivado3.Checked && (sosNumero3.Text.Trim().Count() == 0) ||
                    (sosRecall3.Text.Trim().Count() == 0))
                {
                    sosN3.Visible = true;
                    sosR3.Visible = true;
                    sosError = true;
                }
                else
                {
                    sosN3.Visible = false;
                    sosR3.Visible = false;
                }

                if (!sosError)
                {
                    if (!String.IsNullOrEmpty(Request.QueryString["idActive"]))
                    {
                        AssistedDto oldAssisted =
                            AssistedFacade.GetInstance().FindAssistedByIdActive(
                                Convert.ToInt32(Request.QueryString["idActive"]));



                        dto.ImagePath = "images/assisteds/" + Request.QueryString["idActive"] + ".jpg";
                        assistDto = AssistedFacade.GetInstance().UpdateAssisted(dto);
                        if (File.Exists(Server.MapPath("images/assisteds/") + assistDto.IdActive + ".jpg"))
                        {
                            if (subeImagen.HasFile)
                            {
                                File.Delete(Server.MapPath("images/assisteds/") + assistDto.IdActive + ".jpg");
                            }
                        }
                        if (subeImagen.HasFile)
                            subeImagen.SaveAs(Server.MapPath("images/assisteds/") + assistDto.IdActive + ".jpg");
                        imagen.ImageUrl = dto.ImagePath;

                        if (
                            (oldAssisted.Sos1_Enabled != assistDto.Sos1_Enabled) ||
                            (oldAssisted.Sos1_Phone != assistDto.Sos1_Phone) ||
                            (oldAssisted.Sos1_Recall_Count != assistDto.Sos1_Recall_Count) ||
                            (oldAssisted.Sos2_Enabled != assistDto.Sos2_Enabled) ||
                            (oldAssisted.Sos2_Phone != assistDto.Sos2_Phone) ||
                            (oldAssisted.Sos2_Recall_Count != assistDto.Sos2_Recall_Count) ||
                            (oldAssisted.Sos3_Enabled != assistDto.Sos3_Enabled) ||
                            (oldAssisted.Sos3_Phone != assistDto.Sos3_Phone) ||
                            (oldAssisted.Sos3_Recall_Count != assistDto.Sos3_Recall_Count)
                            )
                        {
                            SendSosSms(assistDto);
                        }

                        if (oldAssisted.AllowMinimize != assistDto.AllowMinimize)
                        {
                            SendConfigurationSms(assistDto);
                        }

                        msg.CssClass = "labelOk";
                        msg.Visible = true;
                        msg.Text = Resources.Resources.updateDataOk;
                    }
                    else
                    {
                        assistDto = AssistedFacade.GetInstance().CreateAssisted(dto);
                        if (subeImagen.HasFile)
                        {
                            assistDto.ImagePath = "images/assisteds/" + assistDto.IdActive + ".jpg";
                            AssistedFacade.GetInstance().UpdateAssisted(dto);
                            subeImagen.SaveAs(Server.MapPath("images/assisteds/") + assistDto.IdActive + ".jpg");
                            imagen.ImageUrl = assistDto.ImagePath;
                        }


                        SendSosSms(assistDto);
                        SendConfigurationSms(assistDto);
                        msg.CssClass = "labelOk";
                        msg.Visible = true;
                        msg.Text = Resources.Resources.saveDataOk;
                    }

                }
            }
            catch (Exception)
            {

                msg.CssClass = "labelError";
                msg.Visible = true;
                if (imageTooBig)
                    msg.Text = Resources.Resources.requiredFilterImage;
                else
                    msg.Text = Resources.Resources.errorData;

            }



        }

        private void SendSosSms(AssistedDto assistDto)
        {
             string sosTxt = "";

            if (sosActivado1.Checked)
            {
                sosTxt += sosNumero1.Text + ";" + sosRecall1.Text + ";";
            }
            if (sosActivado2.Checked)
            {
                sosTxt += sosNumero2.Text + ";" + sosRecall2.Text + ";";
            }
            if (sosActivado3.Checked)
            {
                sosTxt += sosNumero3.Text + ";" + sosRecall3.Text + ";";
            }

            if (sosTxt.Length > 0) sosTxt=sosTxt.Substring(0, sosTxt.Length - 1);


            CustomMessageDto cmDto = new CustomMessageDto(-1, SessionManager.GetIdUser(Context),
                                                          assistDto.IdActive,DateTime.Now, DateTime.Now, sosTxt, 
                                                          assistDto.Phone );
            Gsm.GetInstance().EnviarEmergencia(cmDto);
        }

        private void SendConfigurationSms(AssistedDto assistDto)
        {
            string cfgTxt;

            if (allowMinimize.Checked)
            {
                cfgTxt = "1,1";
            } 
            else
            {
                cfgTxt = "1,0";
            }
            CustomMessageDto cmDto = new CustomMessageDto(-1, SessionManager.GetIdUser(Context),
                                                          assistDto.IdActive, DateTime.Now, DateTime.Now, cfgTxt,
                                                          assistDto.Phone);
            Gsm.GetInstance().EnviarConfiguracion(cmDto);
        }

        protected void volver_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["returnUrl"]))
            {
                string s = Request.QueryString["returnUrl"];
                s += "?";
                if (!String.IsNullOrEmpty(Request.QueryString["idActive"]))
                {
                    s += "&idActive=" + Request.QueryString["idActive"];
                }
                if (!String.IsNullOrEmpty(Request.QueryString["endDate"]))
                {
                    s += "&endDate=" + Request.QueryString["endDate"];
                }
                if (!String.IsNullOrEmpty(Request.QueryString["startDate"]))
                {
                    s += "&startDate=" + Request.QueryString["startDate"];
                }
                if (!String.IsNullOrEmpty(Request.QueryString["startHour"]))
                {
                    s += "&startHour=" + Request.QueryString["startHour"];
                }
                if (!String.IsNullOrEmpty(Request.QueryString["endHour"]))
                {
                    s += "&endHour=" + Request.QueryString["endHour"];
                }
                s = s.Replace("?&", "?");
                Response.Redirect(s);
            }
        }
    }
}
