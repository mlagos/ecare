using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.MobileControls;
using System.Web.UI.WebControls;
using Nextgal.ECare.Model.Message.Dto;
using Nextgal.ECare.Model.Message.Facade;
using Obout.Grid;
using Nextgal.ECare.Controller;
using System.Globalization;
using System.Configuration;
using System.IO;
using Nextgal.ECare.Common.Util;

namespace Nextgal.ECare.WebTeleasistencia
{
    public partial class Messages : System.Web.UI.Page
    {
        private Grid Grid1 = new Grid();

        private DetailGrid DetGrid1= new DetailGrid();

        protected void Page_Load(object sender, EventArgs e)
        {

            Grid1.ID = "grid1";
            //Grid1.Width = new Unit("100%");
            Grid1.Width = new Unit("768px");
            Grid1.Serialize = true;
            Grid1.AutoGenerateColumns = false;
            Grid1.FolderStyle = "~/css/obout/gridstyles/grand_gray";
            Grid1.FolderLocalization = "~/css/obout/gridlocalizations/";
            string uiCulture = "";
            try
            {
                uiCulture = Request.UserLanguages[0];
                if (String.IsNullOrEmpty(uiCulture))
                {
                    uiCulture = "es";
                }
                else
                {
                    if (uiCulture.Contains("-"))
                    {
                        uiCulture = uiCulture.Split('-')[0];
                    }

                }
                if (uiCulture != "es" && uiCulture != "gl")
                    uiCulture = "es";
            }
            catch (Exception)
            {
                uiCulture = "es";
            }
            Grid1.Language = uiCulture;
            Grid1.AllowFiltering = true;
            Grid1.AllowAddingRecords = false;
            Grid1.AllowGrouping = false;
            Grid1.AllowRecordSelection = false;
            Grid1.PageSize = 50;



            //Creating the columns
            Column oCol1 = new Column();
            oCol1.DataField = "IdMessage";
            oCol1.ReadOnly = false;
            oCol1.HeaderText = "ID";
            oCol1.Visible = false;
            oCol1.ID = "IdMessage";
            

            Column oCol3 = new Column();
            oCol3.DataField = "Text";
            oCol3.HeaderText = Resources.Datagrid.text;
            oCol3.Width = "26%";
            oCol3.Wrap = true;

            //Column oCol4 = new Column();
            //oCol4.HeaderText = "Respuesta(s)";
            //oCol4.Width = "28%";
            //oCol4.Wrap = true;

            Column oCol8 = new Column();
            oCol8.DataField = "SendBy";
            oCol8.HeaderText = Resources.Datagrid.sendBy;
            oCol8.Width = "16%";

            Column oCol9 = new Column();
            oCol9.DataField = "Date";
            oCol9.HeaderText = Resources.Datagrid.sendDate;
            oCol9.Width = "23%";

            Column oCol10 = new Column();
            oCol10.HeaderText = Resources.Datagrid.sendConfirmed;
            oCol10.DataField = "Confirmed";
            oCol10.Width = "12%";

            Column oCol11 = new Column();
            oCol11.DataField = "DateReceived";
            oCol11.HeaderText = Resources.Datagrid.sendDateReceived;
            oCol11.Width = "23%";

            // Add columns to the Grid Column collection
            Grid1.Columns.Add(oCol1);
            Grid1.Columns.Add(oCol3);
            //Grid1.Columns.Add(oCol4);
            Grid1.Columns.Add(oCol8);
            Grid1.Columns.Add(oCol9);
            Grid1.Columns.Add(oCol10);
            Grid1.Columns.Add(oCol11);
            


            //DetGrid1 = new DetailGrid();
            Column oColDet0 = new Column();
            oColDet0.DataField = "IdMessage";
            oColDet0.ReadOnly = true;
            oColDet0.HeaderText = "ID";
            oColDet0.Visible = false;
            Column oColDet1 = new Column();
            oColDet1.DataField = "Date";
            oColDet1.HeaderText = Resources.Datagrid.date;
            oColDet1.Width = "30%";
            Column oColDet2 = new Column();
            oColDet2.DataField = "Text";
            oColDet2.HeaderText = Resources.Datagrid.answer;
            oColDet2.Width = "70%";

            DetGrid1.Columns.Add(oColDet0);
            DetGrid1.Columns.Add(oColDet1);
            DetGrid1.Columns.Add(oColDet2);

            Grid1.MasterDetailSettings.LoadingMode = DetailGridLoadingMode.OnCallback;

            //DetGrid1.ID = "DetGrid1";
            DetGrid1.ForeignKeys = "IdMessage";
            DetGrid1.Width = new Unit("95%");
            DetGrid1.Serialize = true;
            DetGrid1.AutoGenerateColumns = false;
            DetGrid1.FolderStyle = "~/css/obout/gridstyles/grand_gray";
            DetGrid1.FolderLocalization = "~/css/obout/gridlocalizations/";
            DetGrid1.Language = uiCulture;
            DetGrid1.AllowFiltering = false;
            DetGrid1.AllowAddingRecords = false;
            DetGrid1.AllowGrouping = false;
            DetGrid1.AllowRecordSelection = false;

            

            Grid1.DetailGrids.Add(DetGrid1);
            phGrid1.Controls.Add(Grid1);
            //Grid1.RowDataBound += grid1_RowDataBound;
            

            if (!IsPostBack)
            {
                LoadData();
                //LoadDetails();
                //rellenaGrid1();
                
            }
            else
            {
                LoadDetails();
            }
        }



        //private void rellenaGrid1()
        //{

        //    for (int i = 0; i < Grid1.Rows.Count; i++)
        //    {
        //        if (String.IsNullOrEmpty(Grid1.Rows[i].Cells[3].Text))
        //        {
        //            Grid1.Rows[i].Cells[3].Text = "";
        //        }
        //        else
        //        {
        //            Grid1.Rows[i].Cells[3].Text = Utils.ConvertToLclTime(DateTime.Parse(Grid1.Rows[i].Cells[3].Text)).ToString("dd/MM/yyyy HH:mm:ss");

        //        }

        //        if (String.IsNullOrEmpty(Grid1.Rows[i].Cells[5].Text))
        //        {
        //            Grid1.Rows[i].Cells[5].Text = "";
        //        }
        //        else
        //        {
        //            Grid1.Rows[i].Cells[5].Text = Utils.ConvertToLclTime(DateTime.Parse(Grid1.Rows[i].Cells[5].Text)).ToString("dd/MM/yyyy HH:mm:ss");

        //        }
        //    }
        //}

        //private void rellenaDetGrid1()
        //{
        //    DetailGrid g = (DetailGrid)Grid1.FindControl("DetGrid1");
        //    for (int i = 0; i < g.Rows.Count; i++)
        //    {
        //        if (String.IsNullOrEmpty(g.Rows[i].Cells[1].Text))
        //        {
        //            g.Rows[i].Cells[1].Text = "";
        //        }
        //        else
        //        {
        //            g.Rows[i].Cells[1].Text = Utils.ConvertToLclTime(DateTime.Parse(g.Rows[i].Cells[1].Text)).ToString("dd/MM/yyyy HH:mm:ss");

        //        }


        //    }
        //}

        //public void grid1_RowDataBound(object sender, GridRowEventArgs e)
        //{


        //    if (String.IsNullOrEmpty(e.Row.Cells[3].Text))
        //    {
        //        e.Row.Cells[3].Text = "";
        //    }
        //    else
        //    {
        //        e.Row.Cells[3].Text = Utils.ConvertToLclTime(DateTime.Parse(e.Row.Cells[3].Text)).ToString("dd/MM/yyyy HH:mm:ss");

        //    }

        //    if (String.IsNullOrEmpty(e.Row.Cells[5].Text))
        //    {
        //        e.Row.Cells[5].Text = "";
        //    }
        //    else
        //    {
        //        e.Row.Cells[5].Text = Utils.ConvertToLclTime(DateTime.Parse(e.Row.Cells[5].Text)).ToString("dd/MM/yyyy HH:mm:ss");

        //    }

        //    DetailGrid g = (DetailGrid)e.Row.FindControl("DetGrid1");
        //    for (int i = 0; i < g.Rows.Count; i++)
        //    {
        //        if (String.IsNullOrEmpty(DetGrid1.Rows[i].Cells[1].Text))
        //        {
        //            g.Rows[i].Cells[1].Text = "";
        //        }
        //        else
        //        {
        //            g.Rows[i].Cells[1].Text = Utils.ConvertToLclTime(DateTime.Parse(g.Rows[i].Cells[1].Text)).ToString("dd/MM/yyyy HH:mm:ss");

        //        }
        //    }


        //}

        //public void detgrid1_RowDataBound(object sender, GridRowEventArgs e)
        //{
        //    if (String.IsNullOrEmpty(e.Row.Cells[1].Text))
        //    {
        //        e.Row.Cells[1].Text = "";
        //    }
        //    else
        //    {
        //        e.Row.Cells[1].Text = Utils.ConvertToLclTime(DateTime.Parse(e.Row.Cells[1].Text)).ToString("dd/MM/yyyy HH:mm:ss");
        //    }

        //}


        private void LoadData()
        {
            try
            {
                ArrayList customMsgs = new ArrayList();
                int idActive = Convert.ToInt32(Request.QueryString["idActive"]);
                List<CustomMessageDto> messages = MessageFacade.GetInstance().GetAllMessageByIdActive(idActive);
                foreach (CustomMessageDto dto in messages)
                {
                    CustomMsgDto cDto;
                    if (dto.DateReceived == null)
                    {
                        cDto = new CustomMsgDto(dto.IdMessage, dto.IdUser, dto.IdActive, dto.Date.ToString("dd/MM/yyyy HH:mm:ss"), "", dto.Text, dto.Phone, dto.SendBy, dto.AssistedName, dto.Replies, dto.Confirmed);

                    }
                    else
                    {
                        cDto = new CustomMsgDto(dto.IdMessage, dto.IdUser, dto.IdActive, dto.Date.ToString("dd/MM/yyyy HH:mm:ss"), DateTime.Parse(dto.DateReceived.ToString()).ToString("dd/MM/yyyy HH:mm:ss"), dto.Text, dto.Phone, dto.SendBy, dto.AssistedName, dto.Replies, dto.Confirmed);

                    }
                    customMsgs.Add(cDto);
                }
                
                Grid1.DataSource = customMsgs;
                Grid1.DataBind();
                
            }
            catch(Exception ex){}
        }

        private void LoadDetails()
        {
            ArrayList customReplies = new ArrayList();
            List<ReplyDto> replies = MessageFacade.GetInstance().GetMessageReplies();
            foreach (ReplyDto dto in replies)
            {
                CustomReplyDto cDto = new CustomReplyDto(dto.IdReply, dto.IdMessage, dto.Date.ToString("dd/MM/yyyy HH:mm:ss"), dto.Text);
                customReplies.Add(cDto);
            }
            
            DetGrid1.DataSource = customReplies;
            DetGrid1.DataBind();
            
            
        }

        

        protected void back(object sender, EventArgs e)
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
                s=s.Replace("?&", "?");
                Response.Redirect(s);
            }
        }
    }


    class CustomReplyDto
    {
        private int m_IdReply;

        public int IdReply
        {
            get { return m_IdReply; }
            set { m_IdReply = value; }
        }

        private int m_IdMessage;

        public int IdMessage
        {
            get { return m_IdMessage; }
            set { m_IdMessage = value; }
        }

        private string m_Date;

        public string Date
        {
            get { return m_Date; }
            set { m_Date = value; }
        }


        private string m_Text;

        public string Text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }

        public CustomReplyDto(int m_IdReply, int m_IdMessage, string m_Date, string m_Text)
        {
            this.m_IdMessage = m_IdMessage;
            this.m_IdReply = m_IdReply;
            this.m_Date = m_Date;
            this.m_Text = m_Text;
        }

        
    }

    class CustomMsgDto
    {
        private int m_IdMessage;

        public int IdMessage
        {
            get { return m_IdMessage; }
            set { m_IdMessage = value; }
        }

        private int m_IdUser;

        public int IdUser
        {
            get { return m_IdUser; }
            set { m_IdUser = value; }
        }

        private int m_IdActive;

        public int IdActive
        {
            get { return m_IdActive; }
            set { m_IdActive = value; }
        }

        private string m_Date;

        public string Date
        {
            get { return m_Date; }
            set { m_Date = value; }
        }

        private string m_DateReceived;

        public string DateReceived
        {
            get { return m_DateReceived; }
            set { m_DateReceived = value; }
        }

        private string m_Text;

        public string Text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }

        private string m_Phone;

        public string Phone
        {
            get { return m_Phone; }
            set { m_Phone = value; }
        }

        private string m_SendBy;

        public string SendBy
        {
            get { return m_SendBy; }
            set { m_SendBy = value; }
        }

        private string m_AssistedName;

        public string AssistedName
        {
            get { return m_AssistedName; }
            set { m_AssistedName = value; }
        }

        private List<ReplyDto> m_Replies;

        public List<ReplyDto> Replies
        {
            get { return m_Replies; }
            set { m_Replies = value; }
        }

        private string m_Confirmed;

        public string Confirmed
        {
            get { return m_Confirmed; }
            set { m_Confirmed = value; }
        }

        

        public CustomMsgDto(int m_IdMessage, int m_IdUser, int m_IdActive, string m_Date, 
            string m_DateReceived, string m_Text, string m_Phone,string sendby, string assname,
            List<ReplyDto> replies,string confirmed)
        {
            this.m_IdMessage = m_IdMessage;
            this.m_IdUser = m_IdUser;
            this.m_IdActive = m_IdActive;
            this.m_Date = m_Date;
            this.m_DateReceived = m_DateReceived;
            this.m_Text = m_Text;
            this.m_Phone = m_Phone;
            this.m_SendBy = sendby;
            this.m_AssistedName = assname;
            this.m_Replies = replies;
            this.Confirmed = confirmed;
        }

       
    }

    
}
