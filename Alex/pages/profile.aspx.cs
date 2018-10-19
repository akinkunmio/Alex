using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Alex.App_code;
using System.IO;
using System.Text.RegularExpressions;
using ClosedXML.Excel;


namespace Alex.pages
{
    
    public partial class profile : System.Web.UI.Page
    {
        int lvl = 0; 
        double total = 0;
        double Btotal = 0;
        double BoarderTotal=0;
        double TotalPaid = 0;
        int GridSno = 0;
        int Boarder_Count = 0;
        string pId = HttpContext.Current.Request.QueryString["Personid"];
       // bool GridColorChnage = false;
        //string pId = ManageCookies.DecodeFrom64(HttpContext.Current.Request.QueryString["Personid"]);


        string rId = HttpContext.Current.Request.QueryString["Regid"];
        string action = HttpContext.Current.Request.QueryString["action"];
        string SchoolFee = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            //string aa = ManageCookies.EncodeTo64(HttpContext.Current.Request.QueryString["Personid"]);
            //string bb = ManageCookies.DecodeFrom64(aa);
            //EncodeTo64
            lblZeroRecords.Text = "";
            Level(); if (lvl == 4 || lvl == 5) { BtnStatmentofAccount.Visible = false; divStatement.Visible = false; divPurchases.Visible = false; BtnPurchases.Visible = false; }
            GetPersonId();
            ProfilePicture.ImageUrl = "ProfilePicHandler.ashx?PersonId=" + pId;
            if (DetailsViewProfile.CurrentMode == DetailsViewMode.Edit)
            {
                var UserName = HttpContext.Current.User.Identity.Name;
                SqlDataSourceProfile.UpdateParameters["updated_by"].DefaultValue = UserName;
            }
            if (DetailsViewAddress.CurrentMode == DetailsViewMode.Edit)
            {
                var UserName = HttpContext.Current.User.Identity.Name;
                SqlDataSourceAddress.UpdateParameters["updated_by"].DefaultValue = UserName;
            }
            if (DetailsViewAddress.CurrentMode == DetailsViewMode.Insert)
            {
                var UserName = HttpContext.Current.User.Identity.Name;
                SqlDataSourceAddress.InsertParameters["created_by"].DefaultValue = UserName;
            }
            if (DetailsViewAddress.Rows[0].Cells.Count == 1)
            {
                divEndAddress.Visible = false;
            }
            else
            {
                divEndAddress.Visible = true;
            }
            if (!IsPostBack)
            {
                
                divProfilePic.Visible = true;
                diviQOnlineKey.Visible = true;
                StudentID();
                ParentAccessStatus();
                DropDownSaleItem();
                DropDownPaySaleItem();
                DropDownBankName();
                PurchasesAccountSummary();
                if (!string.IsNullOrEmpty(action))
                {
                    if (action == "soa" & lvl != 4 & lvl != 5)
                    {
                       StatementOfAccountbtn();
                    }
                    else if (action == "ast")
                    {
                        AssesessmentBtn();
                    }
                    else if (action == "app")
                    {
                        ApplicationBtn();
                    }
                    else if (action == "reg")
                    {
                        registrationsBtn();
                    }
                    else if (action == "att")
                    {
                        AttendanceBtn();
                    }
                    else if (action == "add")
                    {
                        AddressBtn();
                    }
                    else if (action == "3REe8GwY6X" & lvl != 4 & lvl != 5)
                    {
                        PurchasesBtn();
                    }
                    else if (action == "note")
                    {
                        
                         NoteBtn(); 
                    }
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnProfile');", true);
                }
            }


         
        }

        public void Level()
        {
            try
            {
                lvl = (int)(Session["level_of_access"]);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            }
        }
        private void GetPersonId()
        {
            var personId = Request.QueryString["PersonId"];
            //string personId = HttpContext.Current.Request.QueryString["Personid"];
            //var personId = ManageCookies.DecodeFrom64(HttpContext.Current.Request.QueryString["Personid"]);
            string status = string.Empty;

            if (PreviousPage != null)
            {
                if (PreviousPageViewState != null)
                {
                    status = PreviousPageViewState["Status"].ToString();
                }
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot Delete Profile Because Profile Has Either An Address, Application or Registration.')", true);
            }

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_person_profile", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@person_id", SqlDbType.VarChar).Value = personId;

                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                //if (dt.Rows.Count > 0)
                                {
                                    sda.Fill(dt);
                                    LabelProfileName.Text = dt.Rows[0]["fullname"].ToString();
                                }
                                //ScriptManager.RegisterStartupScript(

                            }
                        }

                    }
                }
            }
            catch
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + "," + "');", true);
            }
        }
        private void Address()
        {
            var personId = Request.QueryString["PersonId"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_person_address_list_all", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@person_id", SqlDbType.VarChar).Value = personId;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridViewAddress.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                lblZeroAddress.Visible = true;
                                lblZeroAddress.Text = "No address history found ";
                            }
                            else
                            {
                                GridViewAddress.DataBind();
                                lblZeroAddress.Visible = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
            }
        }
        private void App_search()
        {
            var personId = Request.QueryString["PersonId"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_person_application_history", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@person_id", SqlDbType.VarChar).Value = personId;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {

                            sda.Fill(dt);
                            GridViewApplication.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                GridViewApplication.Visible = false;
                                lblZeroApplications.Visible = true;
                                lblZeroApplications.Text = "No Admission History Found ";
                            }
                            else
                            {
                                GridViewApplication.DataBind();
                                lblZeroApplications.Visible = false;
                                GridViewApplication.Visible = true;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
            }
        }
        private void reg_search()
        {
            var personId = Request.QueryString["PersonId"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_person_registration_history", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@person_id", SqlDbType.VarChar).Value = personId;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridViewRegistration.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                GridViewRegistration.Visible = false;
                                lblZeroRegistrations.Visible = true;
                                lblZeroRegistrations.Text = "No Registration History Found ";

                            }
                            else
                            {
                                GridViewRegistration.Visible = true;
                                GridViewRegistration.DataBind();
                                lblZeroRegistrations.Visible = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
            }
        }
        protected void BtnAddress_Click(object sender, EventArgs e)
        {
            AddressBtn();
        }

        private void AddressBtn()
        {
            Address();
            divAddress.Visible = true;
            DetailsViewProfile.Visible = false;
            diviQOnlineKey.Visible = false;
            divGridviewAddress.Visible = true;
            DetailsViewAddress.Visible = true;
            divApplication.Visible = false;
            divAssessment.Visible = false;
            divRegistration.Visible = false;
            divStatement.Visible = false;
            divAttendance.Visible = false;
            divPurchases.Visible = false;
            divProfilePic.Visible = false;
            divNotes.Visible = false;
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnAddress');", true);

        }
        protected void BtnProfile_Click(object sender, EventArgs e)
        {
            DetailsViewProfile.Visible = true;
            diviQOnlineKey.Visible = true;
            divAddress.Visible = false;
            divApplication.Visible = false;
            divRegistration.Visible = false;
            divAssessment.Visible = false;
            divStatement.Visible = false;
            divAttendance.Visible = false;
            divPurchases.Visible = false;
            divProfilePic.Visible = true;
            divNotes.Visible = false;
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnProfile');", true);
        }
        protected void BtnApplication_Click(object sender, EventArgs e)
        {
            ApplicationBtn();
        }
        protected void ApplicationBtn()
        {
            divAddress.Visible = false;
            DetailsViewApplication.Visible = true;
            DetailsViewProfile.Visible = false;
            diviQOnlineKey.Visible = false;
            divRegistration.Visible = false;
            divApplication.Visible = true;
            divAssessment.Visible = false;
            App_search();
            divStatement.Visible = false;
            divAttendance.Visible = false;
            divPurchases.Visible = false;
            divProfilePic.Visible = false;
            divNotes.Visible = false;
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnApplication');", true);
        }
        protected void BtnRegistration_Click(object sender, EventArgs e)
        {
            registrationsBtn();
        }
        protected void registrationsBtn()
        {
            divAddress.Visible = false;
            DetailsViewProfile.Visible = false;
            diviQOnlineKey.Visible = false;
            divRegistration.Visible = true;
            divGridviewAddress.Visible = false;
            DetailsViewAddress.Visible = false;
            divAssessment.Visible = false;
            reg_search();
            divApplication.Visible = false;
            divStatement.Visible = false;
            divAttendance.Visible = false;
            divPurchases.Visible = false;
            divProfilePic.Visible = false;
            divNotes.Visible = false;
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnRegistration');", true);
        }

        protected void BtnStatmentofAccount_Click(object sender, EventArgs e)
        {
            //StatementOfAccountbtn();
            Statement();
            DetailsViewAddress.Visible = false;
            divAddress.Visible = false;
            divApplication.Visible = false;
            divGridviewAddress.Visible = false;
            DetailsViewProfile.Visible = false;
            diviQOnlineKey.Visible = false;
            DetailsViewApplication.Visible = false;
            divRegistration.Visible = false;

            GridViewApplication.Visible = false;
            divAssessment.Visible = false;
            divStatement.Visible = true;
            divAttendance.Visible = false;
            divPurchases.Visible = false;
            divProfilePic.Visible = false;
            divNotes.Visible = false;
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnStatmentofAccount');", true);
        }
        private void StatementOfAccountbtn()
        {

         GridViewRow row = GridViewStatement.SelectedRow;
          Statement();
          StatementBreakDown();

            DetailsViewAddress.Visible = false;
            divAddress.Visible = false;
            divApplication.Visible = false;
            divGridviewAddress.Visible = false;
            DetailsViewProfile.Visible = false;
            diviQOnlineKey.Visible = false;
            DetailsViewApplication.Visible = false;
            divRegistration.Visible = false;

            GridViewApplication.Visible = false;
            divAssessment.Visible = false;
            divStatement.Visible = true;
            divAttendance.Visible = false;
            divPurchases.Visible = false;
            divProfilePic.Visible = false;
            divNotes.Visible = false;
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnStatmentofAccount');", true);
        }

        protected void BtnAssessment_Click(object sender, EventArgs e)
        {
            //AssesessmentBtn();
            Assessments();

            DetailsViewAddress.Visible = false;
            divAddress.Visible = false;
            divApplication.Visible = false;
            diviQOnlineKey.Visible = false;
            divGridviewAddress.Visible = false;
            DetailsViewProfile.Visible = false;
            DetailsViewApplication.Visible = false;
            divRegistration.Visible = false;

            GridViewApplication.Visible = false;
            divStatement.Visible = false;
            divAssessment.Visible = true;
            divAttendance.Visible = false;
            divPurchases.Visible = false;
            divProfilePic.Visible = false;
            divNotes.Visible = false;
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnAssessment');", true);
        }
        public void AssesessmentBtn()
        {
            DetailsViewAddress.Visible = false;
            divAddress.Visible = false;
            divApplication.Visible = false;
            diviQOnlineKey.Visible = false;
            divGridviewAddress.Visible = false;
            DetailsViewProfile.Visible = false;
            DetailsViewApplication.Visible = false;
            divRegistration.Visible = false;

            GridViewApplication.Visible = false;
            divStatement.Visible = false;
            divAssessment.Visible = true;
            divAttendance.Visible = false;
            divPurchases.Visible = false;
            divProfilePic.Visible = false;
            Assessments();
            AssessmentsBreakDown();
            Assessments2BindGrid();
            divNotes.Visible = false;
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnAssessment');", true);
        }

        protected void BtnAttendance_Click(object sender, EventArgs e)
        {
            //AttendanceBtn();
            Attendance();
            DetailsViewAddress.Visible = false;
            divAddress.Visible = false;
            divApplication.Visible = false;
            diviQOnlineKey.Visible = false;
            divGridviewAddress.Visible = false;
            DetailsViewProfile.Visible = false;
            DetailsViewApplication.Visible = false;
            divRegistration.Visible = false;

            GridViewApplication.Visible = false;
            divStatement.Visible = false;
            divAssessment.Visible = false;
            divAttendance.Visible = true;
            divPurchases.Visible = false;
            divProfilePic.Visible = false;
            divNotes.Visible = false;
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnAttendance');", true);
        }
        public void AttendanceBtn()
        {
            DetailsViewAddress.Visible = false;
            divAddress.Visible = false;
            divApplication.Visible = false;
            diviQOnlineKey.Visible = false;
            divGridviewAddress.Visible = false;
            DetailsViewProfile.Visible = false;
            DetailsViewApplication.Visible = false;
            divRegistration.Visible = false;

            GridViewApplication.Visible = false;
            divStatement.Visible = false;
            divAssessment.Visible = false;
            divAttendance.Visible = true;
            divPurchases.Visible = false;
            divProfilePic.Visible = false;
            Attendance();
            AttendanceBreakDown();
            divNotes.Visible = false;
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnAttendance');", true);
        }

        private void PurchasesBtn()
        {
            PurchasesBreakDown();
            //PurchaseListBindData();
            PurchasesAccountSummary();
            DetailsViewAddress.Visible = false;
            divAddress.Visible = false;
            divApplication.Visible = false;
            diviQOnlineKey.Visible = false;
            divGridviewAddress.Visible = false;
            DetailsViewProfile.Visible = false;
            DetailsViewApplication.Visible = false;
            divRegistration.Visible = false;

            GridViewApplication.Visible = false;
            divStatement.Visible = false;
            divAssessment.Visible = false;
            divAttendance.Visible = false;
            divPurchases.Visible = true;
            divProfilePic.Visible = false;
            divNotes.Visible = false;
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnPurchases');", true);
        }
        protected void BtnPurchases_Click(object sender, EventArgs e)
        {
            PurchasesAccountSummary();
            DetailsViewAddress.Visible = false;
            divAddress.Visible = false;
            divApplication.Visible = false;
            diviQOnlineKey.Visible = false;
            divGridviewAddress.Visible = false;
            DetailsViewProfile.Visible = false;
            DetailsViewApplication.Visible = false;
            divRegistration.Visible = false;

            GridViewApplication.Visible = false;
            divStatement.Visible = false;
            divAssessment.Visible = false;
            divAttendance.Visible = false;
            divPurchases.Visible = true;
            divProfilePic.Visible = false;
            divNotes.Visible = false;
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnPurchases');", true);
        }


        private void NoteBtn()
        {
            NoteBreakDown();
            NotesBind();
            DetailsViewAddress.Visible = false;
            divAddress.Visible = false;
            divApplication.Visible = false;
            diviQOnlineKey.Visible = false;
            divGridviewAddress.Visible = false;
            DetailsViewProfile.Visible = false;
            DetailsViewApplication.Visible = false;
            divRegistration.Visible = false;

            GridViewApplication.Visible = false;
            divStatement.Visible = false;
            divAssessment.Visible = false;
            divAttendance.Visible = false;
            divPurchases.Visible = false;
            divNotes.Visible = true;
            divProfilePic.Visible = false;
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnNotes');", true);

        }
        protected void BtnNotes_Click(object sender, EventArgs e)
        {
            
            NotesBind();
            DetailsViewAddress.Visible = false;
            divAddress.Visible = false;
            divApplication.Visible = false;
            diviQOnlineKey.Visible = false;
            divGridviewAddress.Visible = false;
            DetailsViewProfile.Visible = false;
            DetailsViewApplication.Visible = false;
            divRegistration.Visible = false;

            GridViewApplication.Visible = false;
            divStatement.Visible = false;
            divAssessment.Visible = false;
            divAttendance.Visible = false;
            divPurchases.Visible = false;
            divNotes.Visible = true;
            divProfilePic.Visible = false;
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnNotes');", true);
        }

        protected void SqlDataSourceProfile_Updated(object sender, SqlDataSourceStatusEventArgs e)
        {
            var personId = Request.QueryString["PersonId"];
            Response.Redirect("profile.aspx?PersonId=" + personId);
        }

        public StateBag ReturnViewState()
        {
            return ViewState;
        }
        private StateBag PreviousPageViewState
        {
            get
            {
                StateBag returnValue = null;
                if (PreviousPage != null)
                {
                    Object objPreviousPage = (Object)PreviousPage;
                    MethodInfo objMethod = objPreviousPage.GetType().GetMethod("ReturnViewState");
                    return (StateBag)objMethod.Invoke(objPreviousPage, null);
                }
                return returnValue;
            }
        }

        private double ParseDouble(string value)
        {
            double d = 0;
            if (!double.TryParse(value, out d))
            {
                return 0;
            }
            return d;
        }

        private void Statement()
        {
            var personId = Request.QueryString["PersonId"];
            string RegId = string.Empty;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_person_statement_of_account_summary", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@person_id", SqlDbType.VarChar).Value = personId;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {

                            sda.Fill(dt);
                            GridViewStatement.DataSource = dt;
                           
                           foreach (DataRow row in dt.Rows)
                            {
                                if (row[2].ToString() == rId)
                                {
                                    GridSno = Convert.ToInt32(row[0]);
                                }
                            }

                            if (dt.Rows.Count > 0)
                            {
                                RegId = (dt.Rows[0].ItemArray[2]).ToString();
                            }
                            if (dt.Rows.Count == 0)
                            {
                                lblZeroRecords.Text = "No Statement(s) Found ";
                            }
                            else
                            {
                                GridViewStatement.DataBind();
                                lblClickRecords.Text = "Click on Academic Year below For Details";
                                lblClickRecords.ForeColor = System.Drawing.Color.White;
                                lblClickRecords.BackColor = System.Drawing.ColorTranslator.FromHtml("#18a689");
                            }
                        }
                    }
                }

               if (!string.IsNullOrEmpty(personId.ToString()) && !string.IsNullOrEmpty(RegId))
                {
                    StatementBreakDown(personId.ToString(), RegId);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
            }
        }
        public void StatementBreakDown()
        {
            var personId = Request.QueryString["PersonId"];
            var regid = Request.QueryString["Regid"];
            TotalPaid = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_person_statement_of_account_breakdown", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@person_id", SqlDbType.VarChar).Value = personId;
                    cmd.Parameters.Add("@reg_id", SqlDbType.VarChar).Value = regid;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;

                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridViewStatementOfAccount.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                lblZeroRecords.Visible = true;
                                lblZeroRecords.Text = "No Payment(s) Found ";
                                divAccountDetails.Visible = false;
                                GridViewStatementOfAccount.Visible = false;
                                btnPrintStatement.Visible = false;
                            }
                            else
                            {

                                lblFormName.Text = dt.Rows[0]["form_name"].ToString();
                                lblAcademicYear.Text = dt.Rows[0]["acad_year"].ToString();
                                lblTermName.Text = dt.Rows[0]["term_name"].ToString();
                                SchoolFee = dt.Rows[0]["School Fees"].ToString();
                                GridViewStatementOfAccount.DataBind();
                                GridViewStatementOfAccount.Visible = true;
                                lblZeroRecords.Visible = false;
                                divAccountDetails.Visible = true;
                                btnPrintStatement.Visible = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
            }
        }

        public void StatementBreakDown(string personId, string regid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_person_statement_of_account_breakdown", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@person_id", SqlDbType.VarChar).Value = personId;
                    cmd.Parameters.Add("@reg_id", SqlDbType.VarChar).Value = regid;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridViewStatementOfAccount.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                lblZeroRecords.Visible = true;
                                lblZeroRecords.Text = "No Payment(s) Found ";
                                divAccountDetails.Visible = false;
                                GridViewStatementOfAccount.Visible = false;
                                btnPrintStatement.Visible = false;
                            }
                            else
                            {
                                lblFormName.Text = dt.Rows[0]["form_name"].ToString();
                                lblAcademicYear.Text = dt.Rows[0]["acad_year"].ToString();
                                lblTermName.Text = dt.Rows[0]["term_name"].ToString();
                                SchoolFee = dt.Rows[0]["School Fees"].ToString();
                                GridViewStatementOfAccount.DataBind();
                                GridViewStatementOfAccount.Visible = true;
                                lblZeroRecords.Visible = false;
                                divAccountDetails.Visible = true;
                                btnPrintStatement.Visible = true;
                            }
                        }
                    }
                }





            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
            }
        }

        protected void GridViewStatement_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = GridViewStatement.Rows[e.NewSelectedIndex];
        }

        protected void GridViewStatement_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = GridViewStatement.SelectedRow; 

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblB = (Label)e.Row.FindControl("lblBalance");
                var Balance = ParseDouble(lblB.Text);
                total = total + Balance;

                Label lblBB = (Label)e.Row.FindControl("lblBBalance");
                var BBalance = ParseDouble(lblBB.Text);
                Btotal = Btotal + BBalance;

                Label lblBoarderFee = (Label)e.Row.FindControl("lblBorderBalance");
                var BoarderBalance = ParseDouble(lblBoarderFee.Text);
                BoarderTotal = BoarderTotal + BoarderBalance;

                Label lblBC = (Label)e.Row.FindControl("lblBoarderCount");
                Boarder_Count = Convert.ToInt32(lblBC.Text);
                if (Boarder_Count >= 1)
                {
                    GridViewStatement.Columns[7].Visible = true;
                    GridViewStatement.Columns[8].Visible = true; 
                    GridViewStatement.Columns[9].Visible = true;
                    GridViewStatement.Columns[10].Visible = true;
                    GridViewStatement.Columns[11].Visible = true;
                    GridViewStatement.Columns[17].Visible = true;
                    GridViewStatement.Columns[12].Visible = true;
                }
                else
                {
                    GridViewStatement.Columns[7].Visible = false;
                    GridViewStatement.Columns[8].Visible = false; 
                    GridViewStatement.Columns[9].Visible = false;
                    GridViewStatement.Columns[10].Visible = false;
                    GridViewStatement.Columns[11].Visible = false;
                    GridViewStatement.Columns[12].Visible = false;
                    GridViewStatement.Columns[17].Visible = false;
                }

                var TxtBoarderFee = e.Row.Cells[7].Text; //row.Cells[7].Text; //e.Row.FindControl("tbTotalMarks") as TextBox;
                //var AssessTxtBox = TxtAssessmentmarks.Text;
                Button btntext = e.Row.FindControl("btnBoarderPayFee") as Button;
                switch (TxtBoarderFee)
                {
                    case ""      :btntext.Visible = false; break;
                    case "&nbsp;":btntext.Visible = false; break;
                    case "N/A": btntext.Visible = false; break;
                }

                if ((GridSno) - 1 == e.Row.RowIndex)
                {
                    e.Row.BackColor = Color.LightBlue;
                }
                else
                {
                    e.Row.BackColor = Color.FromArgb(0xf3f3f4);
                }
                if (e.Row.RowIndex == 0 && rId == null)
                {
                    e.Row.BackColor = Color.LightBlue;
                   // GridColorChnage = true;
                }
            }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    Label lblTotalBalance = (Label)e.Row.FindControl("lblTotalBalance");
                    Label lblTotalBBalance = (Label)e.Row.FindControl("lblTotalBBalance");
                    Label lblTotalBoarderBalance = (Label)e.Row.FindControl("lblTotalBoarderBalance");
                    if (total > 0)
                    {
                        lblTotalBalance.ForeColor = Color.Red;
                        lblTotalBBalance.ForeColor = Color.Red;
                        lblTotalBoarderBalance.ForeColor = Color.Red;
                    }
                    lblTotalBalance.Text = "₦" + String.Format("{0:n}", total);
                    lblTotalBBalance.Text = "₦" + String.Format("{0:n}", Btotal);
                    lblTotalBoarderBalance.Text = "₦" + String.Format("{0:n}", BoarderTotal);

                }
            
        }

        protected void GridViewStatementOfAccount_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               //for (int i=0; i <= 2; i++) 
               //     {
               //      if (e.Row.Cells[i].Text == null || e.Row.Cells[i].Text == "" || e.Row.Cells[i].Text == "&nbsp;")
               //         {
               //             GridViewStatementOfAccount.Columns[i].Visible = false;
               //         }
               //     }
                Label lblB = (Label)e.Row.FindControl("lblAmountPaid");
                var Balance = ParseDouble(lblB.Text);
                TotalPaid = TotalPaid + Balance;
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                Label lblFee = (Label)e.Row.FindControl("lblFeeA");
                //double TermFeeToatal = Double.Parse(SchoolFee);
                lblFee.Text = "Fee: ₦" + "   " + String.Format("{0:n}", SchoolFee);
                lblFee.Font.Bold = false;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPaid = (Label)e.Row.FindControl("lblTotalPaid");
                lblTotalPaid.Text = "Total Paid:  ₦" + "  " + String.Format("{0:n}", TotalPaid);
                //extracalucation for labelbalancedOwed
                Label lblBalanceOwed = (Label)e.Row.FindControl("lblBalanceOwed");
                //double TermFeeToatal = Double.Parse(lblFee.Text);
                double TermFeeToatal = Double.Parse(SchoolFee);
                lblBalanceOwed.Text = "Balance Owed: ₦" + String.Format("{0:n}", TermFeeToatal - TotalPaid);
            }
        }
        protected void BtnEndDate_Click(object sender, EventArgs e)
        {
            var personId = Request.QueryString["PersonId"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_person_end_date_edit", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@person_id", SqlDbType.VarChar).Value = personId;
                    cmd.Parameters.AddWithValue("@end_date", tbEndDate.Text.ToString());
                    cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
                    //cmd.Parameters.Add("@end_date", SqlDbType.VarChar).Value = BtnEndDate;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {

                            sda.Fill(dt);
                            GridViewAddress.DataSource = dt;
                            GridViewAddress.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
                Response.Redirect("~/pages/profile.aspx?PersonId=" + personId + "&action=add");

            }
        }
        protected void btnPayFee_Click(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((Button)sender).Parent.Parent)
            {
                string RegId = row.Cells[13].Text;
                string Amount = row.Cells[6].Text;

                Response.Redirect("FeePay.aspx?reg_id=" + RegId.ToString() + "&pId=" + pId.ToString());
            }
        }


        protected void btnBoarderPayFee_Click(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((Button)sender).Parent.Parent)
            {
                string RegId = row.Cells[13].Text;
               // string Amount = row.Cells[6].Text;

                Response.Redirect("boarder_feepay.aspx?reg_id=" + RegId.ToString() + "&pId=" + pId.ToString());
            }
        }

        protected void btnReceipt_Click(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((Button)sender).Parent.Parent)
            {

                string Feetype = row.Cells[5].Text;
                string PaymentID = row.Cells[8].Text;
                int Pay_id = Convert.ToInt32(PaymentID);
                //string url = "fee_receipt.aspx?pay_id=" + Pay_id.ToString();
                //string s = "window.open('" + url + "', 'popup_window', 'width=600,height=400,left=100,top=100,resizable=yes');";
                //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                Server.Transfer("fee_receipt.aspx?FeeType=" + Feetype + "&pay_id=" + Pay_id.ToString());

            }
        }


        protected void btnPrintStatement_Click(object sender, EventArgs e)
        {
            var PerID = Request.QueryString["PersonId"];
            var RegID = GridViewStatementOfAccount.Rows[0].Cells[9].Text;
            Server.Transfer("fee_statement_receipt.aspx?PID=" + PerID + "&RID=" + RegID.ToString());
        }


        private void Assessments()
        {
            var personId = Request.QueryString["PersonId"];
            string RegId = string.Empty;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_person_assessment_summaryv2", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@person_id", SqlDbType.VarChar).Value = personId;

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataSet dt = new DataSet())
                        {
                            sda.Fill(dt);
                            GridViewAssesments.DataSource = dt.Tables[1];
                            if (dt.Tables[1].Rows.Count > 0)
                            {
                                RegId = (dt.Tables[1].Rows[0].ItemArray[1]).ToString();
                            }
                            if (dt.Tables[1].Rows.Count == 0)
                            {
                                lblZeroAssessments.Visible = true;
                                lblZeroAssessments.Text = "No Assessments Found ";
                                divAssessBreakdown.Visible = false;

                            }
                            else
                            {

                                GridViewAssesments.DataBind();
                                lblClickAssemntsYear.Text = "Click on Academic Year below For Details";
                                lblClickAssemntsYear.ForeColor = System.Drawing.Color.White;
                                lblClickAssemntsYear.BackColor = System.Drawing.ColorTranslator.FromHtml("#18a689");
                                lblZeroAssessments.Visible = false;
                                divAssessBreakdown.Visible = true;
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(personId.ToString()) && !string.IsNullOrEmpty(RegId))
                {
                    AssessmentsBreakDown(personId.ToString(), RegId);
                    Assessments2BindGrid(RegId);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);

            }
            finally
            {
                con.Close();
            }
        }

        public void AssessmentsBreakDown(string personId, string regid)
        {
            string status = string.Empty;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_person_assessment_breakdown_v3", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@person_id", SqlDbType.VarChar).Value = personId;
                    cmd.Parameters.Add("@reg_id", SqlDbType.VarChar).Value = regid;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            GridViewAssessmentsBreakDown.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                lblZeroAssessmentsBreakDown.Visible = true;
                                lblZeroAssessmentsBreakDown.Text = "No  Assessments Found ";

                            }
                            else
                            {
                                lblFormAssessment.Text = dt.Rows[0]["form_name"].ToString() + dt.Rows[0]["class_name"].ToString();
                                lblAcademicAssessment.Text = dt.Rows[0]["acad_year"].ToString();
                                lblTermAssessment.Text = dt.Rows[0]["term_name"].ToString();
                                
                                lblFormAssessment2.Text = dt.Rows[0]["form_name"].ToString() + dt.Rows[0]["class_name"].ToString();
                                lblAcademicAssessment2.Text = dt.Rows[0]["acad_year"].ToString();
                                lblTermAssessment2.Text = dt.Rows[0]["term_name"].ToString();
                               
                                Count(regid);
                                GridViewAssessmentsBreakDown.DataBind();
                                SqlCommand Ass1 = new SqlCommand("SELECT assessment_weight FROM ms_assessment_weight  where [ayt_id] = (select ay_term_id from ms_registrations where   reg_id=" + regid + ")and assessment = 'Assessment 1' and section = (select section2 from ms_forms,ms_registrations where ms_forms.form_id = ms_registrations.form_id and reg_id =" + regid + ") ", con);
                                SqlCommand Ass2 = new SqlCommand("SELECT assessment_weight FROM ms_assessment_weight  where [ayt_id] = (select ay_term_id from ms_registrations where  reg_id=" + regid + ")and assessment = 'Assessment 2' and section = (select section2 from ms_forms,ms_registrations where ms_forms.form_id = ms_registrations.form_id and reg_id =" + regid + ") ", con);
                                SqlCommand Ass3 = new SqlCommand("SELECT assessment_weight FROM ms_assessment_weight  where [ayt_id] = (select ay_term_id from ms_registrations where  reg_id=" + regid + ")and assessment = 'Assessment 3' and section = (select section2 from ms_forms,ms_registrations where ms_forms.form_id = ms_registrations.form_id and reg_id =" + regid + ") ", con);
                                SqlCommand Ass4 = new SqlCommand("SELECT assessment_weight FROM ms_assessment_weight  where [ayt_id] = (select ay_term_id from ms_registrations where  reg_id=" + regid + ")and assessment = 'Assessment 4' and section = (select section2 from ms_forms,ms_registrations where ms_forms.form_id = ms_registrations.form_id and reg_id =" + regid + ") ", con);
                                SqlCommand Ass5 = new SqlCommand("SELECT assessment_weight FROM ms_assessment_weight  where [ayt_id] = (select ay_term_id from ms_registrations where  reg_id=" + regid + ")and assessment = 'Final Exam' and section = (select section2 from ms_forms,ms_registrations where ms_forms.form_id = ms_registrations.form_id and reg_id =" + regid + ") ", con);
                                var Assessment1 = Ass1.ExecuteScalar();
                                var Assessment2 = Ass2.ExecuteScalar();
                                var Assessment3 = Ass3.ExecuteScalar();
                                var Assessment4 = Ass4.ExecuteScalar();
                                var Assessment5 = Ass5.ExecuteScalar();


                                GridViewAssessmentsBreakDown.HeaderRow.Cells[1].Text = "Assessment1  (" + Assessment1.ToString() + ")";
                                GridViewAssessmentsBreakDown.HeaderRow.Cells[2].Text = "Assessment2  (" + Assessment2.ToString() + ")";
                                GridViewAssessmentsBreakDown.HeaderRow.Cells[3].Text = "Assessment3  (" + Assessment3.ToString() + ")";
                                GridViewAssessmentsBreakDown.HeaderRow.Cells[4].Text = "Assessment4  (" + Assessment4.ToString() + ")";
                                GridViewAssessmentsBreakDown.HeaderRow.Cells[5].Text = "Final Exam  (" + Assessment5.ToString() + ")";

                                lblZeroAssessmentsBreakDown.Visible = false;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
            }
        }
        protected void Count(string regid)
        {
            //var regid = Request.QueryString["Regid"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("assessment_profile_weight_count", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@reg_id", SqlDbType.VarChar).Value = regid;
                con.Open();
                int count = (Int32)cmd.ExecuteScalar();
                if (count == 2)
                {
                    GridViewAssessmentsBreakDown.Columns[2].Visible = false;
                    GridViewAssessmentsBreakDown.Columns[3].Visible = false;
                    GridViewAssessmentsBreakDown.Columns[4].Visible = false;
                }
                else if (count == 3)
                {
                    GridViewAssessmentsBreakDown.Columns[3].Visible = false;
                    GridViewAssessmentsBreakDown.Columns[4].Visible = false;
                }
                else if (count == 4)
                {
                    GridViewAssessmentsBreakDown.Columns[4].Visible = false;
                }
                else if (count == 5)
                {
                    GridViewAssessmentsBreakDown.Columns[2].Visible = true;
                    GridViewAssessmentsBreakDown.Columns[3].Visible = true;
                    GridViewAssessmentsBreakDown.Columns[4].Visible = true;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
            }
        }

        protected void Count()
        {
            var regid = Request.QueryString["Regid"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("assessment_profile_weight_count", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@reg_id", SqlDbType.VarChar).Value = regid;
                con.Open();
                int count = (Int32)cmd.ExecuteScalar();
                if (count == 2)
                {
                    GridViewAssessmentsBreakDown.Columns[2].Visible = false;
                    GridViewAssessmentsBreakDown.Columns[3].Visible = false;
                    GridViewAssessmentsBreakDown.Columns[4].Visible = false;

                }
                else if (count == 3)
                {
                    GridViewAssessmentsBreakDown.Columns[3].Visible = false;
                    GridViewAssessmentsBreakDown.Columns[4].Visible = false;
                }
                else if (count == 4)
                {
                    GridViewAssessmentsBreakDown.Columns[4].Visible = false;
                }
                else if (count == 5)
                {
                    GridViewAssessmentsBreakDown.Columns[2].Visible = true;
                    GridViewAssessmentsBreakDown.Columns[3].Visible = true;
                    GridViewAssessmentsBreakDown.Columns[4].Visible = true;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
            }
        }

        public void AssessmentsBreakDown()
        {

            string status = string.Empty;
            var personId = Request.QueryString["PersonId"];
            var regid = Request.QueryString["Regid"];

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_person_assessment_breakdown_v3", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@person_id", SqlDbType.VarChar).Value = personId;
                    cmd.Parameters.Add("@reg_id", SqlDbType.VarChar).Value = regid;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            GridViewAssessmentsBreakDown.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                lblZeroAssessmentsBreakDown.Visible = true;
                                lblZeroAssessmentsBreakDown.Text = "No  Assessments Found ";

                            }
                            else
                            {
                                lblFormAssessment.Text = dt.Rows[0]["form_name"].ToString() + dt.Rows[0]["class_name"].ToString();
                                lblAcademicAssessment.Text = dt.Rows[0]["acad_year"].ToString();
                                lblTermAssessment.Text = dt.Rows[0]["term_name"].ToString();

                                lblFormAssessment2.Text = dt.Rows[0]["form_name"].ToString() + dt.Rows[0]["class_name"].ToString();
                                lblAcademicAssessment2.Text = dt.Rows[0]["acad_year"].ToString();
                                lblTermAssessment2.Text = dt.Rows[0]["term_name"].ToString();
                               
                                Count();
                                GridViewAssessmentsBreakDown.DataBind();
                                SqlCommand Ass1 = new SqlCommand("SELECT assessment_weight FROM ms_assessment_weight  where [ayt_id] = (select ay_term_id from ms_registrations where   reg_id=" + regid + ")and assessment = 'Assessment 1' and section = (select section2 from ms_forms,ms_registrations where ms_forms.form_id = ms_registrations.form_id and reg_id =" + regid + ") ", con);
                                SqlCommand Ass2 = new SqlCommand("SELECT assessment_weight FROM ms_assessment_weight  where [ayt_id] = (select ay_term_id from ms_registrations where  reg_id=" + regid + ")and assessment = 'Assessment 2' and section = (select section2 from ms_forms,ms_registrations where ms_forms.form_id = ms_registrations.form_id and reg_id =" + regid + ") ", con);
                                SqlCommand Ass3 = new SqlCommand("SELECT assessment_weight FROM ms_assessment_weight  where [ayt_id] = (select ay_term_id from ms_registrations where  reg_id=" + regid + ")and assessment = 'Assessment 3' and section = (select section2 from ms_forms,ms_registrations where ms_forms.form_id = ms_registrations.form_id and reg_id =" + regid + ") ", con);
                                SqlCommand Ass4 = new SqlCommand("SELECT assessment_weight FROM ms_assessment_weight  where [ayt_id] = (select ay_term_id from ms_registrations where  reg_id=" + regid + ")and assessment = 'Assessment 4' and section = (select section2 from ms_forms,ms_registrations where ms_forms.form_id = ms_registrations.form_id and reg_id =" + regid + ") ", con);
                                SqlCommand Ass5 = new SqlCommand("SELECT assessment_weight FROM ms_assessment_weight  where [ayt_id] = (select ay_term_id from ms_registrations where  reg_id=" + regid + ")and assessment = 'Final Exam' and section = (select section2 from ms_forms,ms_registrations where ms_forms.form_id = ms_registrations.form_id and reg_id =" + regid + ") ", con);
                                var Assessment1 = Ass1.ExecuteScalar();
                                var Assessment2 = Ass2.ExecuteScalar();
                                var Assessment3 = Ass3.ExecuteScalar();
                                var Assessment4 = Ass4.ExecuteScalar();
                                var Assessment5 = Ass5.ExecuteScalar();


                                GridViewAssessmentsBreakDown.HeaderRow.Cells[1].Text = "Assessment1  (" + Assessment1.ToString() + ")";
                                GridViewAssessmentsBreakDown.HeaderRow.Cells[2].Text = "Assessment2  (" + Assessment2.ToString() + ")";
                                GridViewAssessmentsBreakDown.HeaderRow.Cells[3].Text = "Assessment3  (" + Assessment3.ToString() + ")";
                                GridViewAssessmentsBreakDown.HeaderRow.Cells[4].Text = "Assessment4  (" + Assessment4.ToString() + ")";
                                GridViewAssessmentsBreakDown.HeaderRow.Cells[5].Text = "Final Exam  (" + Assessment5.ToString() + ")";

                                lblZeroAssessmentsBreakDown.Visible = false;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
            }
        }

        private void Assessments2BindGrid(string regid)
        {
            // var regid = Request.QueryString["Regid"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_profile_assessment2_list_all", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@reg_id", regid);
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            DvAssessments2.DataSource = dt;
                            DvAssessments2_1.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                DvAssessments2.Visible = false;
                                DvAssessments2_1.Visible = false;
                                lblComment1.Text = "";
                                lblComment2.Text = "";
                            }
                            else
                            {
                                lblComment1.Text = dt.Rows[0]["comment1"].ToString();
                                lblComment2.Text = dt.Rows[0]["comment2"].ToString();
                                DvAssessments2.Visible = true;
                                DvAssessments2.DataBind();
                                DvAssessments2_1.Visible = true;
                                DvAssessments2_1.DataBind();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);

            }
            finally
            {
                con.Close();
            }
        }

        private void Assessments2BindGrid()
        {
            var regid = Request.QueryString["Regid"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_profile_assessment2_list_all", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@reg_id", regid);
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            DvAssessments2.DataSource = dt;
                            DvAssessments2_1.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                DvAssessments2.Visible = false;
                                DvAssessments2_1.Visible = false;
                                lblComment1.Text = "";
                                lblComment2.Text = "";

                            }
                            else
                            {
                                lblComment1.Text = dt.Rows[0]["comment1"].ToString();
                                lblComment2.Text = dt.Rows[0]["comment2"].ToString();
                                DvAssessments2.Visible = true;
                                DvAssessments2.DataBind();
                                DvAssessments2_1.Visible = true;
                                DvAssessments2_1.DataBind();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);

            }
            finally
            {
                con.Close();
            }
        }



        protected void SqlDataSourceApplication_Deleted(object sender, SqlDataSourceStatusEventArgs e)
        {
            ApplicationBtn();
        }



        protected void SqlDataSourceRegistration_Updated(object sender, SqlDataSourceStatusEventArgs e)
        {
            string RegistrationId = DetailsViewRegistration.Rows[1].Cells[1].Text;
            try
            {
                TextBox Reg_Date = DetailsViewRegistration.FindControl("tbRegDate") as TextBox; string RegDate = Reg_Date.Text.ToString();
                DropDownList status = DetailsViewRegistration.FindControl("ddlRegStatus") as DropDownList; string RegStatus = status.SelectedValue.ToString();
                //string created_by = "";
                Label AcadYear = DetailsViewRegistration.FindControl("lblRegAcademicYear") as Label; string Acad_Year = AcadYear.Text.ToString();
                Label TermName = DetailsViewRegistration.FindControl("lblRegTerm") as Label; string Term_Name = TermName.Text.ToString();
                Label FormName = DetailsViewRegistration.FindControl("lblRegForm") as Label; string Form_Name = FormName.Text.ToString();
                Label ClassName = DetailsViewRegistration.FindControl("ddlRegClass") as Label; string Class_Name = ClassName.Text.ToString();
                DropDownList BoarderTypeID = DetailsViewRegistration.FindControl("ddlRegBoarderType") as DropDownList; string RegBoarderType = BoarderTypeID.SelectedItem.ToString();

                SqlDataSource d = new SqlDataSource();
                d.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                d.ProviderName = ConfigurationManager.ConnectionStrings["conStr"].ProviderName;
                d.UpdateCommand = "sp_ms_person_registration_edit";

                d.UpdateCommandType = SqlDataSourceCommandType.StoredProcedure;
                d.UpdateParameters.Add("reg_id", RegistrationId);
                //d.UpdateParameters.Add("app_id", DBNull.Value.ToString());
                d.UpdateParameters.Add("reg_date", RegDate);
                d.UpdateParameters.Add("term_name", Term_Name);
                d.UpdateParameters.Add("class_name", Class_Name);
                d.UpdateParameters.Add("form_name", Form_Name);
                d.UpdateParameters.Add("acad_year", Acad_Year);
                d.UpdateParameters.Add("status", RegStatus);
                d.UpdateParameters.Add("created_by", HttpContext.Current.User.Identity.Name);
                d.UpdateParameters.Add("type_description", RegBoarderType);
                d.UpdateParameters.Add("from", "Edit");

                d.Update();

                bool OtherProcessSucceeded = true;
                if (e.Exception != null)
                {
                    OtherProcessSucceeded = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot update')", true);
                }
                if (OtherProcessSucceeded)
                {
                    e.Command.Transaction.Commit();
                    //Response.Write("The record was updated successfully");
                }
                else
                {
                    e.Command.Transaction.Rollback();
                    //Response.Write("The record was not updated");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("NULL"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot update Registration the Term or Class does not Exist')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot Update Registration, Please check Academic Year,Term,Class')", true);
                }

            }
        }
        protected void SqlDataSourceRegistration_Updating(object sender, SqlDataSourceCommandEventArgs e)
        {
            e.Command.Connection.Open();
            e.Command.Transaction = e.Command.Connection.BeginTransaction();
        }

        protected void SqlDataSourceRegistration_Deleted(object sender, SqlDataSourceStatusEventArgs e)
        {
            var personId = Request.QueryString["PersonId"];
            string RegistrationId = DetailsViewRegistration.Rows[1].Cells[1].Text;
            try
            {
                SqlDataSource d = new SqlDataSource();
                d.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                d.ProviderName = ConfigurationManager.ConnectionStrings["conStr"].ProviderName;
                d.UpdateCommand = "sp_ms_person_registration_delete";

                d.UpdateCommandType = SqlDataSourceCommandType.StoredProcedure;
                d.UpdateParameters.Add("reg_id", RegistrationId);

                d.UpdateParameters.Add("from", "Delete");

                d.Update();

                bool OtherProcessSucceeded = true;
                if (e.Exception != null)
                {
                    OtherProcessSucceeded = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot Delete')", true);
                }
                if (OtherProcessSucceeded)
                {
                    e.Command.Transaction.Commit();
                    //Response.Write("The record was updated successfully");
                }
                else
                {
                    e.Command.Transaction.Rollback();
                    //Response.Write("The record was not updated");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("FK_ms_payments_ms_registrations"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot Delete, registration has Payments')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot Delete, registration has Attendance or Payments')", true);
                }
            }
            finally
            {
                //Response.Redirect("~/pages/profile.aspx?PersonId=" + personId + "&action=reg"); 
                //registrationsBtn();
            }
        }
        protected void SqlDataSourceRegistration_Deleting(object sender, SqlDataSourceCommandEventArgs e)
        {
            e.Command.Connection.Open();
            e.Command.Transaction = e.Command.Connection.BeginTransaction();

        }

        protected void SqlDataSourceApplication_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            //ApplicationBtn();
            //string a = DetailsViewApplication.Rows[1].Cells[1].Text;

            //DropDownList theOrderNumberLabel = DetailsViewApplication.FindControl("ddlAppAcademicYear") as DropDownList;

            //a = theOrderNumberLabel.SelectedValue.ToString();
            //a = theOrderNumberLabel.ToString();

            try
            {
                TextBox App_Date = DetailsViewApplication.FindControl("tbAppDate") as TextBox; string AppDate = App_Date.Text.ToString();
                DropDownList status = DetailsViewApplication.FindControl("ddlAppStatus") as DropDownList; string AppStatus = status.SelectedValue.ToString();
                //string created_by = "";
                DropDownList AcadYear = DetailsViewApplication.FindControl("ddlAppAcademicYear") as DropDownList; string Acad_Year = AcadYear.SelectedValue.ToString();
                DropDownList TermName = DetailsViewApplication.FindControl("ddlAppTermInsert") as DropDownList; string Term_Name = TermName.SelectedValue.ToString();
                //DropDownList ClassName = DetailsViewRegistration.FindControl("ddlAppClass") as DropDownList; string Class_Name = ClassName.SelectedValue.ToString();
                DropDownList FormName = DetailsViewApplication.FindControl("ddlAppForm") as DropDownList; string Form_Name = FormName.SelectedValue.ToString();
                //DetailsViewRegistration.FindControl("app_id") as  TextBox //string RegDate = Reg_Date.ToString();

                SqlDataSource d = new SqlDataSource();
                //d.InsertCommand.Lengt
                d.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                d.ProviderName = ConfigurationManager.ConnectionStrings["conStr"].ProviderName;
                d.InsertCommand = "sp_ms_person_application_add";

                int aaaa = Convert.ToInt32(pId);
                //@person_id int, -- Value to be passed from previous page
                //@app_id int, -- should be reg_id but app existed already so reused for attendance
                //@reg_date VARCHAR(50),
                //@status VARCHAR(50),
                //@created_by varchar(50), --Value to be passed from variable of username from login page
                //@acad_year varchar(50),
                //@term_name NCHAR(10),
                //@class_name varchar(50),
                //@form_name varchar(50)
                d.InsertCommandType = SqlDataSourceCommandType.StoredProcedure;
                d.InsertParameters.Add("person_id", pId);

                d.InsertParameters.Add("app_date", AppDate);
                d.InsertParameters.Add("application_status", AppStatus);
                d.InsertParameters.Add("created_by", HttpContext.Current.User.Identity.Name);
                d.InsertParameters.Add("acad_year", Acad_Year);
                d.InsertParameters.Add("term_name", Term_Name);
                //d.InsertParameters.Add("class_name", Class_Name);
                d.InsertParameters.Add("form_name", Form_Name);
                d.InsertParameters.Add("from", "Insert");
                d.Insert();

                bool OtherProcessSucceeded = true;
                if (e.Exception != null)
                {
                    OtherProcessSucceeded = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot Add dupicate Application')", true);
                }
                if (OtherProcessSucceeded)
                {
                    e.Command.Transaction.Commit();
                    //Response.Write("The record was updated successfully");
                }
                else
                {
                    e.Command.Transaction.Rollback();
                    //Response.Write("The record was not updated");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("NULL"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot Add Application the Term or Class does not Exist')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot Add dupicate Application')", true);
                }
            }
            finally
            {
                ApplicationBtn();
            }
        }
        protected void SqlDataSourceApplication_Inserting(object sender, SqlDataSourceCommandEventArgs e)
        {
            e.Command.Connection.Open();
            e.Command.Transaction = e.Command.Connection.BeginTransaction();
        }
        private void Attendance()
        {

            var personId = Request.QueryString["PersonId"];
            string RegId = string.Empty;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_person_attendance_summary", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@person_id", SqlDbType.VarChar).Value = personId;

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridViewAttendance.DataSource = dt;
                            if (dt.Rows.Count > 0)
                            {
                                RegId = (dt.Rows[0].ItemArray[1]).ToString();
                            }
                            if (dt.Rows.Count == 0)
                            {
                                lblZeroAttendance.Visible = true;
                                lblZeroAttendance.Text = "No Attendance Found ";

                            }
                            else
                            {

                                GridViewAttendance.DataBind();
                                lblClickBelow.Text = "Click on Academic Year below For Details";
                                lblClickBelow.ForeColor = System.Drawing.Color.White;
                                lblClickBelow.BackColor = System.Drawing.ColorTranslator.FromHtml("#18a689");
                                lblZeroAttendance.Visible = false;

                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(personId.ToString()) && !string.IsNullOrEmpty(RegId))
                {
                    AttendanceBreakDown(personId.ToString(), RegId);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);

            }
            finally
            {
                con.Close();
            }
        }

        public void AttendanceBreakDown(string personId, string regid)
        {

            string status = string.Empty;


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_person_attendance_breakdown", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@person_id", SqlDbType.VarChar).Value = personId;
                    cmd.Parameters.Add("@reg_id", SqlDbType.VarChar).Value = regid;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            GridViewAttendanceBreakDown.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                lblZeroAttendanceBreakDown.Visible = true;
                                lblZeroAttendanceBreakDown.Text = "No Attendance Record Found ";

                            }
                            else
                            {
                                lblAttForm.Text = dt.Rows[0]["form_name"].ToString() + dt.Rows[0]["class_name"].ToString(); ;
                                lblAttAcademicYear.Text = dt.Rows[0]["acad_year"].ToString();
                                lblAttTerm.Text = dt.Rows[0]["term_name"].ToString();
                                //lblAttClass.Text = dt.Rows[0]["class_name"].ToString();
                                GridViewAttendanceBreakDown.DataBind();
                                BindGridColor();
                                lblZeroAttendanceBreakDown.Visible = false;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
            }
        }

        public void AttendanceBreakDown()
        {

            string status = string.Empty;
            var personId = Request.QueryString["PersonId"];
            var regid = Request.QueryString["Regid"];

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_person_attendance_breakdown", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@person_id", SqlDbType.VarChar).Value = personId;
                    cmd.Parameters.Add("@reg_id", SqlDbType.VarChar).Value = regid;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            GridViewAttendanceBreakDown.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                lblZeroAttendanceBreakDown.Visible = true;
                                lblZeroAttendanceBreakDown.Text = "No Attendance Record Found ";

                            }
                            else
                            {
                                lblAttForm.Text = dt.Rows[0]["form_name"].ToString() + dt.Rows[0]["class_name"].ToString();
                                lblAttAcademicYear.Text = dt.Rows[0]["acad_year"].ToString();
                                lblAttTerm.Text = dt.Rows[0]["term_name"].ToString();
                                //lblAttClass.Text = dt.Rows[0]["class_name"].ToString();
                                GridViewAttendanceBreakDown.DataBind();
                                BindGridColor();
                                lblZeroAttendanceBreakDown.Visible = false;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
            }
        }

        private void BindGridColor()
        {
            foreach (GridViewRow row in GridViewAttendanceBreakDown.Rows)
            {
                //int i = 0;
                string text = "";
                for (int i = 1; i <= 5; i++)
                {
                    text = "dd" + i;
                    string Droptest = ((TextBox)row.FindControl(text)).Text;
                    //P -- Green (Present)
                    //L -- Yellow (Late)
                    //A -- Red (Absent)
                    //N -- White (Not Applicable)
                    if (Droptest == "P")
                    {
                        ((TextBox)row.FindControl(text)).BackColor = Color.Green;
                        ((TextBox)row.FindControl(text)).ForeColor = Color.White;
                    }
                    if (Droptest == "L")
                    {
                        ((TextBox)row.FindControl(text)).BackColor = Color.FromArgb(0xFF8200);
                        ((TextBox)row.FindControl(text)).ForeColor = Color.White;
                    }
                    if (Droptest == "A")
                    {
                        ((TextBox)row.FindControl(text)).BackColor = Color.Red;
                        ((TextBox)row.FindControl(text)).ForeColor = Color.White;
                    }
                    if (Droptest == "H")
                    {
                        ((TextBox)row.FindControl(text)).BackColor = Color.FromArgb(0x0000ff);
                        ((TextBox)row.FindControl(text)).ForeColor = Color.White;
                    }

                    //ddlMultiColor.BackColor = Color.FromName(ddlMultiColor.SelectedItem.Text);
                }
            }
        }



        protected void SqlDataSourceApplication_Updated(object sender, SqlDataSourceStatusEventArgs e)
        {
            string AppId = DetailsViewApplication.Rows[1].Cells[1].Text;
            try
            {
                TextBox App_Date = DetailsViewApplication.FindControl("tbAppDate") as TextBox; string AppDate = App_Date.Text.ToString();
                DropDownList status = DetailsViewApplication.FindControl("ddlAppStatus") as DropDownList; string AppStatus = status.SelectedValue.ToString();
                //string created_by = ""; 
                DropDownList AcadYear = DetailsViewApplication.FindControl("ddlAppAcademicYearEdit") as DropDownList; string Acad_Year = AcadYear.SelectedValue.ToString();
                DropDownList TermName = DetailsViewApplication.FindControl("ddlAppTermEdit") as DropDownList; string Term_Name = TermName.SelectedValue.ToString();
                //DropDownList ClassName = DetailsViewRegistration.FindControl("ddlAppClass") as DropDownList; string Class_Name = ClassName.SelectedValue.ToString();
                DropDownList FormName = DetailsViewApplication.FindControl("ddlAppForm") as DropDownList; string Form_Name = FormName.SelectedValue.ToString();
                //DetailsViewRegistration.FindControl("app_id") as  TextBox //string RegDate = Reg_Date.ToString();

                SqlDataSource d = new SqlDataSource();
                d.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                d.ProviderName = ConfigurationManager.ConnectionStrings["conStr"].ProviderName;
                d.UpdateCommand = "sp_ms_person_application_edit";

                d.UpdateCommandType = SqlDataSourceCommandType.StoredProcedure;
                d.UpdateParameters.Add("updated_by", HttpContext.Current.User.Identity.Name);
                d.UpdateParameters.Add("application_status", AppStatus);
                d.UpdateParameters.Add("app_id", AppId);
                d.UpdateParameters.Add("term_name", Term_Name);
                d.UpdateParameters.Add("form_name", Form_Name);
                d.UpdateParameters.Add("acad_year", Acad_Year);
                d.UpdateParameters.Add("app_date", AppDate);
                d.UpdateParameters.Add("from", "Edit");

                d.Update();

                bool OtherProcessSucceeded = true;
                if (e.Exception != null)
                {
                    OtherProcessSucceeded = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot update')", true);
                }
                if (OtherProcessSucceeded)
                {
                    e.Command.Transaction.Commit();
                    //Response.Write("The record was updated successfully");
                }
                else
                {
                    e.Command.Transaction.Rollback();
                    //Response.Write("The record was not updated");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("NULL"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot update Applicationthe Term does not Exist')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot Update Application, Please check Academic Year,Term,Class')", true);
                }

            }
        }

        protected void SqlDataSourceApplication_Updating(object sender, SqlDataSourceCommandEventArgs e)
        {
            e.Command.Connection.Open();
            e.Command.Transaction = e.Command.Connection.BeginTransaction();
        }

        protected void GridViewAddress_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                var personId = Request.QueryString["PersonId"];
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_person_address_delete", con);
                GridViewRow row = GridViewAddress.Rows[e.RowIndex] as GridViewRow;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "add_id", Value = GridViewAddress.Rows[e.RowIndex].Cells[0].Text });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                Response.Redirect("~/pages/profile.aspx?PersonId=" + personId + "&action=add");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);

            }
            finally
            {
                con.Close();
            }

        }

        protected void BtnKeyGenerate_Click(object sender, EventArgs e)
        {
            iQOnlineKeyGenerate();
            BtnKeyGenerate.Visible = false;
        }

        protected void iQOnlineKeyGenerate()
        {
            var personId = Request.QueryString["PersonId"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_iqo_generate_login", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@person_id", personId);
                cmd.Parameters.AddWithValue("@count", DBNull.Value);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        DetailsViewiQKeyPassword.DataSource = dt;
                        if (dt.Rows.Count == 0)
                        {
                            DivDetailsView.Visible = false;
                            btnPrint.Visible = false;
                            btniQPwdReset.Visible = false;

                        }
                        else
                        {
                            DivDetailsView.Visible = true;
                            btnPrint.Visible = true;
                            btniQPwdReset.Visible = true;
                            DetailsViewiQKeyPassword.DataBind();

                        }
                    }
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            }
            finally
            {
                con.Close();
            }
        }

        public void DropDownSaleItem()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_sales_items_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlSaleItem.DataSource = ddlValues;
            ddlSaleItem.DataValueField = "item_name";
            ddlSaleItem.DataTextField = "item_name";
            ddlSaleItem.DataBind();

            ddlSaleItem.Items.Insert(0, new ListItem("Please select Item", ""));

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }


        public void DropDownPaySaleItem()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_sales_items_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlPayItemName.DataSource = ddlValues;
            ddlPayItemName.DataValueField = "item_name";
            ddlPayItemName.DataTextField = "item_name";
            ddlPayItemName.DataBind();

            ddlPayItemName.Items.Insert(0, new ListItem("Please select Item", ""));

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void ClearData(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if ((c.GetType() == typeof(TextBox)))
                {
                    ((TextBox)c).Text = "";
                }
                if ((c.GetType() == typeof(DropDownList)))
                {
                    ((DropDownList)c).SelectedIndex = -1;
                    //((DropDownList)c).Items.Clear();
                }
                if (c.HasControls())
                {
                    ClearData(c);
                }
            }
        }

        public void DropDownBankName()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_status_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@category", "Bank");
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlBankName.DataSource = ddlValues;
            ddlBankName.DataValueField = "status_name";
            ddlBankName.DataTextField = "status_name";
            ddlBankName.DataBind();
            ddlBankName.Items.Insert(0, new ListItem("Please select Bank Name", ""));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
        protected void BtnPurchaseForm1_Click(object sender, EventArgs e)
        {
            var personId = Request.QueryString["PersonId"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_purchases_add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@person_id", personId);
                cmd.Parameters.AddWithValue("@item_name", ddlSaleItem.Text.ToString());
                cmd.Parameters.AddWithValue("@quantity", tbQuantity.Text.ToString());
                cmd.Parameters.AddWithValue("@purchase_date", tbPurchasedDate.Text.ToString());
                cmd.Parameters.AddWithValue("@additional_fee", tbAdditionalFee.Text.ToString());
                cmd.ExecuteNonQuery();
                PurchasesAccountSummary();
                PurchaseForm1.Visible = false;
                btnPurchase1.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Purchase List  Saved Successfully');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Cannot Add Duplicate Record');", true);
            }
            finally
            {
                con.Close();
                ClearData(this);
            }
        }


        protected void BtnPurchaseForm2_Click(object sender, EventArgs e)
        {
            var personId = Request.QueryString["PersonId"];
            string ReplaceCommaAmount = Regex.Replace(tbPayingAmount.Text.ToString(), "[^.0-9]", "");
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_person_item_purchase_and_pay_add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@person_id", personId);
                cmd.Parameters.AddWithValue("@item_name", ddlPayItemName.Text.ToString());
                cmd.Parameters.AddWithValue("@quantity", tbPayQty.Text.ToString());
                cmd.Parameters.AddWithValue("@purchase_date", tbPayPurchaseDate.Text.ToString());
                cmd.Parameters.Add("@amount", SqlDbType.Decimal).Value = ReplaceCommaAmount;
                cmd.Parameters.AddWithValue("@received_date", tbPayPurchaseDate.Text.ToString());
                cmd.Parameters.AddWithValue("@payment_method", ddlPaymentMethod.Text.ToString());
                cmd.Parameters.AddWithValue("@payment_method_ref", tbPMReference.Text.ToString());
                cmd.Parameters.AddWithValue("@bank_name", ddlBankName.Text.ToString());
                cmd.Parameters.AddWithValue("@invoice_no", tbInvoiceNumber.Text.ToString());
                cmd.Parameters.AddWithValue("@teller_no", tbReceiptNumber.Text.ToString());
                cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                cmd.Parameters.AddWithValue("@additional_fee", tbPayAddtionalFee.Text.ToString());
                cmd.ExecuteNonQuery();
                PurchasesAccountSummary();

                btnPurchase1.Visible = true;
                btnPurchase2.Visible = true;
                btnPurchase3.Visible = true;
                PurchaseForm2.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Purchase List  Saved Successfully');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Cannot Add Duplicate Record');", true);
            }
            finally
            {
                con.Close();
                ClearData(this);
            }
        }


        protected void BtnPurchaseForm3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                foreach (GridViewRow row in GridViewBulkPurchases.Rows)
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_person_item_purchase_and_pay_bulk_add", con))
                    {
                        CheckBox ChkBoxHeader = (CheckBox)GridViewBulkPurchases.HeaderRow.FindControl("chkboxSelectAll1");
                        var personId = Request.QueryString["PersonId"];
                        DropDownList ddlQty = row.FindControl("ddlBulkQuantity") as DropDownList;
                        TextBox txtBulkAmount = row.FindControl("tbBulkPurAmount") as TextBox;
                        string ReplaceCommaAmount = Regex.Replace(txtBulkAmount.Text.ToString(), "[^.0-9]", "");
                        DropDownList ddlPayMethod = row.FindControl("ddlPaymentMethod") as DropDownList;
                        TextBox txtBulkRef = row.FindControl("tbBulkPurReference") as TextBox;
                        DropDownList ddlBulkBank = row.FindControl("ddlBankBulkPur") as DropDownList;
                        TextBox txtBulkInv = row.FindControl("tbBulkInvNo") as TextBox;
                        TextBox txtBulkTeller = row.FindControl("tbBulkRecNo") as TextBox;
                        TextBox txtBulkAddFee = row.FindControl("tbBulkAddCharges") as TextBox;
                        CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkStudent");
                        string ItemName = Server.HtmlDecode(row.Cells[2].Text);
                        if (ChkBoxRows.Checked)
                        {
                            cmd.Parameters.AddWithValue("@person_id", personId);
                            cmd.Parameters.AddWithValue("@msi_id", row.Cells[1].Text);
                            cmd.Parameters.AddWithValue("@item_name", ItemName);
                            cmd.Parameters.AddWithValue("@quantity", ddlQty.SelectedValue);
                            cmd.Parameters.AddWithValue("@purchase_date", DateTime.Now);
                            cmd.Parameters.Add("@amount", SqlDbType.Decimal).Value = ReplaceCommaAmount;
                            cmd.Parameters.AddWithValue("@received_date", DateTime.Now);
                            cmd.Parameters.AddWithValue("@payment_method", ddlPayMethod.Text.ToString());
                            cmd.Parameters.AddWithValue("@payment_method_ref", txtBulkRef.Text.ToString());
                            cmd.Parameters.AddWithValue("@bank_name", ddlBulkBank.Text.ToString());
                            cmd.Parameters.AddWithValue("@invoice_no", txtBulkInv.Text.ToString());
                            cmd.Parameters.AddWithValue("@teller_no", txtBulkTeller.Text.ToString());
                            cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                            cmd.Parameters.AddWithValue("@additional_fee", txtBulkAddFee.Text.ToString());
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                            con.Close();
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                         
            }
            finally
            {
                con.Close();
                PurchasesAccountSummary();

                btnPurchase1.Visible = true;
                btnPurchase2.Visible = true;
                btnPurchase3.Visible = true;
                PurchaseForm3.Visible = false;// ClearData(this);
                
            }

            
        }


        protected void btnPurchase1_Click(object sender, EventArgs e)
        {
            btnPurchase1.Visible = false;
            btnPurchase2.Visible = false;
            btnPurchase3.Visible = false;
            PurchaseForm1.Visible = true;
            PurchaseForm2.Visible = false;
            PurchaseForm3.Visible = false;
        }

        protected void btnPurchase2_Click(object sender, EventArgs e)
        {
            btnPurchase1.Visible = false;
            btnPurchase2.Visible = false;
            btnPurchase3.Visible = false;
            PurchaseForm1.Visible = false;
            PurchaseForm2.Visible = true;
            PurchaseForm3.Visible = false;
        }

        protected void btnPurchase3_Click(object sender, EventArgs e)
        {
            btnPurchase1.Visible = false;
            btnPurchase2.Visible = false;
            btnPurchase3.Visible = false;
            PurchaseForm1.Visible = false;
            PurchaseForm2.Visible = false;
            PurchaseForm3.Visible = true;
          
            BulkPurchasesBindGrid();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnPurchases');", true);
        }

        protected void BtnCancelPurchaseForm1_Click(object sender, EventArgs e)
        {
            ClearData(form); 
            PurchaseForm1.Visible = false;
            btnPurchase1.Visible = true;
            btnPurchase2.Visible = true;
            btnPurchase3.Visible = true;
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnPurchases');", true);
          
        }



        protected void BtnCancelPurchaseForm2_Click(object sender, EventArgs e)
        {
            ClearData(form1); 
            PurchaseForm2.Visible = false;
            btnPurchase1.Visible = true;
            btnPurchase2.Visible = true;
            btnPurchase3.Visible = true;
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnPurchases');", true);
           
        }


        protected void BtnCancelPurchaseForm3_Click(object sender, EventArgs e)
        {
            ClearData(this);
            PurchaseForm3.Visible = false;
            btnPurchase1.Visible = true;
            btnPurchase2.Visible = true;
            btnPurchase3.Visible = true;
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnPurchases');", true);
           
        }



        private void PurchasesAccountSummary()
        {
            var personId = Request.QueryString["PersonId"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_person_items_statement_of_account_summary", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@person_id", SqlDbType.VarChar).Value = personId;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {

                            sda.Fill(dt);
                            GridViewPurchasesSummary.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                lblPurchasesSummary.Text = "No Purchase(s) Found ";
                                btnPrintPurchases.Visible = false;
                            }
                            else
                            {
                                GridViewPurchasesSummary.DataBind();
                                btnPrintPurchases.Visible = true;
                                lblPurchasesSummary.Text = "Click on Item below For Details";
                                lblPurchasesSummary.ForeColor = System.Drawing.Color.White;
                                lblPurchasesSummary.BackColor = System.Drawing.ColorTranslator.FromHtml("#18a689");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
            }
        }


        public void PurchasesBreakDown()
        {
            var personId = Request.QueryString["PersonId"];
            var Purchaseid = Request.QueryString["purch_id"];

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_person_items_statement_of_account_breakdown", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@person_id", SqlDbType.VarChar).Value = personId;
                    cmd.Parameters.Add("@purch_id", SqlDbType.VarChar).Value = Purchaseid;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridViewPurchaseBreakDown.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                lblItemDetails.Visible = true;
                                lblItemDetails.Text = "No Payment(s) Found ";

                            }
                            else
                            {

                                GridViewPurchaseBreakDown.DataBind();

                                lblItemName.Text = dt.Rows[0]["item_name"].ToString();
                                lblItemDetails.Visible = false;
                                divPurchases.Visible = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
            }
        }

        protected void GridViewPurchaseBreakDown_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblB = (Label)e.Row.FindControl("lblAmountPaid");
                var Balance = ParseDouble(lblB.Text);
                TotalPaid = TotalPaid + Balance;
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPaid = (Label)e.Row.FindControl("lblTotalPaid");
                lblTotalPaid.Text = "Total Paid:  ₦" + "  " + String.Format("{0:n}", TotalPaid);
            }


        }

        protected void btnPayItems_Click(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((Button)sender).Parent.Parent)
            {
                string PurchaseId = row.Cells[9].Text;
                Response.Redirect("item_pay.aspx?PurId=" + PurchaseId.ToString() + "&pId=" + pId.ToString());
            }
        }

        protected void GridViewPurchasesSummary_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewPurchasesSummary.EditIndex = e.NewEditIndex;
            PurchasesAccountSummary();
        }

        protected void GridViewPurchasesSummary_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GridViewPurchasesSummary.EditIndex = -1;
            PurchasesAccountSummary();
        }

        protected void GridViewPurchasesSummary_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_purchases_edit", con);
                GridViewRow row = GridViewPurchasesSummary.Rows[e.RowIndex] as GridViewRow;
                TextBox txtQuantity = row.FindControl("tbquantity") as TextBox;
                TextBox txtPurchaseDate = row.FindControl("tbPurchaseDate") as TextBox;
                TextBox txtAdditionalfee = row.FindControl("tbAdditionalfee") as TextBox;
                string strContent = txtAdditionalfee.Text.Replace("₦", "").Replace(",", "");
               // string ReplaceAmount = txtAdditionalfee.Text.Replace("₦", string.Empty);
                double i = Convert.ToDouble(strContent);
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "purch_id", Value = GridViewPurchasesSummary.Rows[e.RowIndex].Cells[9].Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "msi_id", Value = GridViewPurchasesSummary.Rows[e.RowIndex].Cells[10].Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "purchase_date", Value = txtPurchaseDate.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "additional_fee", Value = i });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "quantity", Value = txtQuantity.Text });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
                GridViewPurchasesSummary.EditIndex = -1;
                PurchasesAccountSummary();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Cannot delete Item(s), it has payments');", true);
            }
        }

        protected void GridViewPurchasesSummary_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var personId = Request.QueryString["PersonId"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_purchases_delete", con);
                GridViewRow row = GridViewPurchasesSummary.Rows[e.RowIndex] as GridViewRow;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "purch_id", Value = GridViewPurchasesSummary.Rows[e.RowIndex].Cells[9].Text });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                //PurchasesAccountSummary();
                Response.Redirect("~/pages/profile.aspx?PersonId=" + personId + "&action=3REe8GwY6X");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("FK_ms_item_payments_ms_purchases"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Cannot delete Item(s), it has payments');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                }
            }
            finally
            {
                con.Close();
            }

        }

        private void BulkPurchasesBindGrid()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_sales_items_bulk_list_all", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;

                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridViewBulkPurchases.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                GridViewBulkPurchases.Visible = false;
                                lblZeroBulkPurchases.Visible = true;
                                lblZeroBulkPurchases.Text = "No Items found, Please add Items in Settings ";
                            }
                            else
                            {
                               GridViewBulkPurchases.Visible = true;
                               GridViewBulkPurchases.DataBind();
                               lblZeroBulkPurchases.Visible = false;
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);

            }
            finally
            {
                con.Close();
            }
        }

        protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)GridViewBulkPurchases.HeaderRow.FindControl("chkboxSelectAll");
            foreach (GridViewRow row in GridViewBulkPurchases.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkStudent");
                TextBox txtBulkRef = (TextBox)row.FindControl("tbBulkPurReference");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                    if (txtBulkRef.Text.Length == 0)
                    { txtBulkRef.Attributes["required"] = "true"; }
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
        }

        protected void DetailsViewAddress_ItemCreated(object sender, EventArgs e)
        {
            if (DetailsViewAddress.CurrentMode == DetailsViewMode.ReadOnly)
            {
                int commandRowIndex = DetailsViewAddress.Rows.Count - 1;
                if (commandRowIndex > 0)
                {
                    DetailsViewRow commandRow = DetailsViewAddress.Rows[commandRowIndex];
                    DataControlFieldCell cell = (DataControlFieldCell)commandRow.Controls[0];
                    if (cell != null)
                    {
                        foreach (Control ctrl in cell.Controls)
                        {
                            if (ctrl is LinkButton)
                            {
                                if (((LinkButton)ctrl).CommandName == "New")
                                {
                                    ctrl.Visible = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void SqlDataSourceAddress_Deleted(object sender, SqlDataSourceStatusEventArgs e)
        {
            var personId = Request.QueryString["PersonId"];
            Response.Redirect("~/pages/profile.aspx?PersonId=" + personId + "&action=add");
        }

        protected void lnkBtnAddInsertCancel_Click(object sender, EventArgs e)
        {
            var personId = Request.QueryString["PersonId"];
            Response.Redirect("~/pages/profile.aspx?PersonId=" + personId + "&action=add");
        }

        protected void SqlDataSourceProfile_Deleting(object sender, SqlDataSourceCommandEventArgs e)
        {
            e.Command.Connection.Open();
            e.Command.Transaction = e.Command.Connection.BeginTransaction();
        }

        protected void SqlDataSourceProfile_Deleted(object sender, SqlDataSourceStatusEventArgs e)
        {
            try
            {
                //    if (!string.IsNullOrEmpty(e.Exception.Message))
                //    {
                var personId = Request.QueryString["PersonId"];
                //        ViewState["Status"] = e.Exception.Message;
                //        //Server.Transfer("profile.aspx");
                //        Response.Redirect("profile.aspx");
                //    }
                //}
                //catch (Exception ex)
                //{
                //    //var personId = Request.QueryString["PersonId"]; 
                //    Response.Redirect("profile.aspx");
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                //}


                SqlDataSource d = new SqlDataSource();
                d.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                d.ProviderName = ConfigurationManager.ConnectionStrings["conStr"].ProviderName;
                d.DeleteCommand = "sp_ms_person_delete";

                d.DeleteCommandType = SqlDataSourceCommandType.StoredProcedure;

                d.DeleteParameters.Add("person_id", personId.ToString());
                d.DeleteParameters.Add("from", "Delete");
                d.Delete();

                bool OtherProcessSucceeded = true;
                if (e.Exception != null)
                {
                    OtherProcessSucceeded = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot update')", true);
                }
                if (OtherProcessSucceeded)
                {
                    e.Command.Transaction.Commit();
                    //Response.Write("The record was updated successfully");
                }
                else
                {
                    e.Command.Transaction.Rollback();
                    //Response.Write("The record was not updated");
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Profile was deleted successfully');window.location = 'people.aspx';", true);
                // Response.Redirect("people.aspx");
            }

            catch (Exception ex)
            {
                if (ex.Message.Contains("FK_ms_applications_ms_people"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot Delete Profile, Admission exist for this profile')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot Delete Profile, Previous Address or Registration or Attendance exist for this profile')", true);
                }

            }
            finally
            {
                //con.Close();
            }
        }

        protected void ddlAppAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_term_dropdown_v2", con);

            DropDownList Item = DetailsViewApplication.FindControl("ddlAppAcademicYear") as DropDownList;
            cmd.Parameters.Add("@acad_year", SqlDbType.VarChar).Value = Item.SelectedItem.ToString();
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                SqlDataReader ddlValues;
                ddlValues = cmd.ExecuteReader();
                string TermSelectedValue = null;
                while (ddlValues.Read())
                {
                    TermSelectedValue = ddlValues[0].ToString();
                    int DefaultValue = Convert.ToInt32(ddlValues[1]);
                    if (DefaultValue == 1)
                        break;
                }
                ddlValues.Close();
                ddlValues = cmd.ExecuteReader();
                var ddlAppTerm = (DropDownList)DetailsViewApplication.FindControl("ddlAppTermInsert");
                ddlAppTerm.Items.Clear();
                ddlAppTerm.DataSource = ddlValues;
                ddlAppTerm.DataValueField = "term_name";
                ddlAppTerm.DataTextField = "term_name";
                ddlAppTerm.Items.Insert(0, new ListItem("Please select Term", ""));
                if (ddlAppTerm != null)
                {
                    ddlAppTerm.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        protected void ddlAppAcademicYear_SelectedIndexChangedEdit(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_term_dropdown_v2", con);

            DropDownList Item = DetailsViewApplication.FindControl("ddlAppAcademicYearEdit") as DropDownList;
            cmd.Parameters.Add("@acad_year", SqlDbType.VarChar).Value = Item.SelectedItem.ToString();
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                SqlDataReader ddlValues;
                ddlValues = cmd.ExecuteReader();
                string TermSelectedValue = null;
                while (ddlValues.Read())
                {
                    TermSelectedValue = ddlValues[0].ToString();
                    int DefaultValue = Convert.ToInt32(ddlValues[1]);
                    if (DefaultValue == 1)
                        break;
                }
                ddlValues.Close();
                ddlValues = cmd.ExecuteReader();
                var ddlAppTermEdit = (DropDownList)DetailsViewApplication.FindControl("ddlAppTermEdit");
                ddlAppTermEdit.Items.Clear();
                ddlAppTermEdit.DataSource = ddlValues;
                ddlAppTermEdit.DataValueField = "term_name";
                ddlAppTermEdit.DataTextField = "term_name";
                ddlAppTermEdit.Items.Insert(0, new ListItem("Please select Term", ""));
                if (ddlAppTermEdit != null)
                {
                    ddlAppTermEdit.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }



        protected void DetailsViewApplication_DataBound(object sender, EventArgs e)
        {
            DataRowView myView = (DataRowView)DetailsViewApplication.DataItem;
            if (DetailsViewApplication.DataItemCount == 0) { divAppRegistration.Visible = false; }
            if (DetailsViewApplication.CurrentMode == DetailsViewMode.Edit)
            {
                divAppRegistration.Visible = false;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
                SqlCommand cmd = new SqlCommand("sp_ms_rep_term_dropdown_v2", con);
                DropDownList Item = DetailsViewApplication.FindControl("ddlAppAcademicYearEdit") as DropDownList;
                cmd.Parameters.Add("@acad_year", SqlDbType.VarChar).Value = Item.SelectedItem.ToString();
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    SqlDataReader ddlValues;
                    ddlValues = cmd.ExecuteReader();
                    ddlValues.Close();
                    ddlValues = cmd.ExecuteReader();
                    var ddlAppTermEdit = (DropDownList)DetailsViewApplication.FindControl("ddlAppTermEdit");
                    ddlAppTermEdit.Items.Clear();
                    ddlAppTermEdit.DataSource = ddlValues;
                    ddlAppTermEdit.DataValueField = "term_name";
                    ddlAppTermEdit.DataTextField = "term_name";
                    if (ddlAppTermEdit != null)
                    {
                        ddlAppTermEdit.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
            else if (DetailsViewApplication.CurrentMode != DetailsViewMode.Edit && DetailsViewApplication.CurrentMode == DetailsViewMode.ReadOnly && myView != null) 
               {
                   App2RegClassNameDropDown(); 
                   divAppRegistration.Visible = true;
               }
           
        }

        protected void ddlRegAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_term_dropdown_v2", con);

            DropDownList Item = DetailsViewRegistration.FindControl("ddlRegAcademicYear") as DropDownList;
            cmd.Parameters.Add("@acad_year", SqlDbType.VarChar).Value = Item.SelectedItem.ToString();
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                SqlDataReader ddlValues;
                ddlValues = cmd.ExecuteReader();
                //string TermSelectedValue = null;
                //while (ddlValues.Read())
                //{
                //    TermSelectedValue = ddlValues[0].ToString();
                //    int DefaultValue = Convert.ToInt32(ddlValues[1]);
                //    if (DefaultValue == 1)
                //        break;
                //}
                ddlValues.Close();
                ddlValues = cmd.ExecuteReader();
                var ddlRegTerm = (DropDownList)DetailsViewRegistration.FindControl("ddlRegTerm");
                ddlRegTerm.Items.Clear();
                ddlRegTerm.DataSource = ddlValues;
                ddlRegTerm.DataValueField = "term_name";
                ddlRegTerm.DataTextField = "term_name";
                ddlRegTerm.Items.Insert(0, new ListItem("Please select Term", ""));
                if (ddlRegTerm != null)
                {
                    ddlRegTerm.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        protected void ddlRegForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_class_name_dropdown", con);

            DropDownList Item = DetailsViewRegistration.FindControl("ddlRegForm") as DropDownList;
            cmd.Parameters.Add("@form_name", SqlDbType.VarChar).Value = Item.SelectedItem.ToString();
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                SqlDataReader ddlValues;
                ddlValues = cmd.ExecuteReader();
                ddlValues.Close();
                ddlValues = cmd.ExecuteReader();
                var ddlClassName = (DropDownList)DetailsViewRegistration.FindControl("ddlRegClass");
                ddlClassName.Items.Clear();
                ddlClassName.DataSource = ddlValues;
                ddlClassName.DataValueField = "class_name";
                ddlClassName.DataTextField = "class_name";
                ddlClassName.Items.Insert(0, new ListItem("Please select Arm", ""));
                if (ddlClassName != null)
                {
                    ddlClassName.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        protected void GridViewRegistration_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_person_registration_delete", con);
                GridViewRow row = GridViewRegistration.Rows[e.RowIndex] as GridViewRow;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "reg_id", Value = GridViewRegistration.Rows[e.RowIndex].Cells[0].Text });
                cmd.Parameters.AddWithValue("@from", "delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                // con.Close();
                reg_search();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("FK_ms_assessments_ms_registrations") || ex.Message.Contains("FK_ms_payments_ms_registration") || ex.Message.Contains("FK_assessment2_ms_registrations"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Unable to delete registration, student registration has either Payments, Attendance or Assessments')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                }
            }
            finally
            {
                con.Close();
            }
        }

        protected void GridViewRegistration_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRegistration.EditIndex = e.NewEditIndex;
            reg_search();
        }


        protected void GridViewRegistration_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GridViewRegistration.EditIndex = -1;
            reg_search();
        }

        protected void GridViewRegistration_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_person_registration_edit", con);
                GridViewRow row = GridViewRegistration.Rows[e.RowIndex] as GridViewRow;
                Label txtEditRegAcadYear = row.FindControl("lblRegEditAcademicYear") as Label;
                Label txtRegEditTerm = row.FindControl("lblRegEditTerm") as Label;
                Label txtRegEditClass = row.FindControl("lblRegEditClass") as Label;
                Label txtRegEditClassName = row.FindControl("lblRegEditClassName") as Label;
                DropDownList ddlRegEditStatus = row.FindControl("ddlRegEditStatus") as DropDownList;
                DropDownList ddlBoarderType = row.FindControl("ddlBoarderType") as DropDownList;
                TextBox txttbRegEditDate = row.FindControl("tbRegEditDate") as TextBox;

                cmd.Parameters.Add(new SqlParameter() { ParameterName = "reg_id", Value = GridViewRegistration.Rows[e.RowIndex].Cells[0].Text });

                cmd.Parameters.Add(new SqlParameter() { ParameterName = "reg_date", Value = txttbRegEditDate.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "term_name", Value = txtRegEditTerm.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "class_name", Value = txtRegEditClassName.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "form_name", Value = txtRegEditClass.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "acad_year", Value = txtEditRegAcadYear.Text });
                // cmd.Parameters.Add(new SqlParameter() { ParameterName = "class_name", Value = txtEndDate.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "status", Value = ddlRegEditStatus.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "created_by", Value = HttpContext.Current.User.Identity.Name });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "from", Value = "Edit" });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "type_description", Value = ddlBoarderType.Text });


                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
                GridViewRegistration.EditIndex = -1;
                reg_search();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            }
            finally
            {
                con.Close();
            }
        }

        private Boolean InsertUpdateData(SqlCommand cmd)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);

            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                return false;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
        protected void Upload_Click(object sender, EventArgs e)
        {
            var personId = Request.QueryString["PersonId"];
            // Read the file and convert it to Byte Array
            string filePath = FileUpload1.PostedFile.FileName;
            //Is the file too big to upload?
            int fileSize = FileUpload1.PostedFile.ContentLength;
            if (fileSize > (200 * 1024))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Filesize of image is too large. Maximum file size permitted is 200 KB')", true);
                return;
            }
            string filename = Path.GetFileName(filePath);
            string ext = Path.GetExtension(filename);
            string contenttype = String.Empty;

            //Set the contenttype based on File Extension
            switch (ext)
            {

                case ".jpg":
                    contenttype = "image/jpg";
                    break;
                case ".JPG":
                    contenttype = "image/jpg";
                    break;
                case ".png":
                    contenttype = "image/png";
                    break;
                case ".gif":
                    contenttype = "image/gif";
                    break;
                case ".PNG":
                    contenttype = "image/PNG";
                    break;
                case ".jpeg":
                    contenttype = "image/jpeg";
                    break;
            }
            if (contenttype != String.Empty)
            {

                Stream fs = FileUpload1.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                Byte[] bytes = br.ReadBytes((Int32)fs.Length);

                //insert the file into database
                string strQuery = "update ms_people set picture=@picture where person_id =" + personId;
                SqlCommand cmd = new SqlCommand(strQuery);
                // cmd.Parameters.Add("@upload_id", SqlDbType.Int).Value = DBNull.Value;
                cmd.Parameters.Add("@picture", SqlDbType.Binary).Value = bytes;
                InsertUpdateData(cmd);
                //lblStatus.ForeColor = System.Drawing.Color.Green;
                //lblStatus.Text = "Logo Uploaded Successfully";
                cmd.Connection.Close();
                cmd.Connection.Dispose();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Uploaded Successfully')", true);

            }
            else
            {
                //lblStatus.ForeColor = System.Drawing.Color.Red;
                //lblStatus.Text = "File format not recognised." +
                //  " Upload JPEG/JPG/PNG/GIF formats";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('File format not recognised Upload JPEG/JPG/PNG/GIF formats')", true);
            }

        }


        public void StudentID()
        {
            var personId = Request.QueryString["PersonId"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                string strQuery = "SELECT stu_id from [dbo].[ms_people] WHERE person_id=" + personId;
                {
                    SqlCommand cmd = new SqlCommand(strQuery);
                    // cmd.Parameters.Add("@person_id", SqlDbType.VarChar).Value = personId;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            if (dt.Rows.Count != 0 && !string.IsNullOrEmpty(dt.Rows[0]["stu_id"].ToString()))
                            {
                                lblStudentId.Text = dt.Rows[0]["stu_id"].ToString();
                            }
                            else { lblStudentId.Text = "Not Assigned"; }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);

            }
            finally
            {
                con.Close();
            }
        }

        protected void SqlDataSourceRegistration_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {

            string a = DetailsViewRegistration.Rows[1].Cells[1].Text;

            DropDownList theOrderNumberLabel = DetailsViewRegistration.FindControl("ddlRegAcademicYear") as DropDownList;

            a = theOrderNumberLabel.SelectedValue.ToString();
            a = theOrderNumberLabel.ToString();

            try
            {
                TextBox Reg_Date = DetailsViewRegistration.FindControl("tbRegDate") as TextBox; string RegDate = Reg_Date.Text.ToString();
                DropDownList status = DetailsViewRegistration.FindControl("ddlRegStatus") as DropDownList; string RegStatus = status.SelectedValue.ToString();
                //string created_by = "";
                DropDownList AcadYear = DetailsViewRegistration.FindControl("ddlRegAcademicYear") as DropDownList; string Acad_Year = AcadYear.SelectedValue.ToString();
                DropDownList TermName = DetailsViewRegistration.FindControl("ddlRegTerm") as DropDownList; string Term_Name = TermName.SelectedValue.ToString();
                DropDownList ClassName = DetailsViewRegistration.FindControl("ddlRegClass") as DropDownList; string Class_Name = ClassName.SelectedValue.ToString();
                DropDownList FormName = DetailsViewRegistration.FindControl("ddlRegForm") as DropDownList; string Form_Name = FormName.SelectedValue.ToString();
                DropDownList BoarderTypeID = DetailsViewRegistration.FindControl("ddlRegBoarderType") as DropDownList; string RegBoarderType = BoarderTypeID.SelectedItem.ToString();
                //DetailsViewRegistration.FindControl("app_id") as  TextBox //string RegDate = Reg_Date.ToString();

                SqlDataSource d = new SqlDataSource();
                //d.InsertCommand.Lengt
                d.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                d.ProviderName = ConfigurationManager.ConnectionStrings["conStr"].ProviderName;
                d.InsertCommand = "sp_ms_person_registration_add";

                int aaaa = Convert.ToInt32(pId);
                //@person_id int, -- Value to be passed from previous page
                //@app_id int, -- should be reg_id but app existed already so reused for attendance
                //@reg_date VARCHAR(50),
                //@status VARCHAR(50),
                //@created_by varchar(50), --Value to be passed from variable of username from login page
                //@acad_year varchar(50),
                //@term_name NCHAR(10),
                //@class_name varchar(50),
                //@form_name varchar(50)
                d.InsertCommandType = SqlDataSourceCommandType.StoredProcedure;
                d.InsertParameters.Add("person_id", pId);
                d.InsertParameters.Add("app_id", DBNull.Value.ToString());
                d.InsertParameters.Add("reg_date", RegDate);
                d.InsertParameters.Add("status", RegStatus);
                d.InsertParameters.Add("created_by", HttpContext.Current.User.Identity.Name);
                d.InsertParameters.Add("acad_year", Acad_Year);
                d.InsertParameters.Add("term_name", Term_Name);
                d.InsertParameters.Add("class_name", Class_Name);
                d.InsertParameters.Add("form_name", Form_Name);
                d.InsertParameters.Add("type_description", RegBoarderType);
                d.InsertParameters.Add("from", "Insert");
                d.Insert();

                bool OtherProcessSucceeded = true;
                if (e.Exception != null)
                {
                    OtherProcessSucceeded = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot Add dupicate Registration')", true);
                }
                if (OtherProcessSucceeded)
                {
                    e.Command.Transaction.Commit();
                    //Response.Write("The record was updated successfully");
                }
                else
                {
                    e.Command.Transaction.Rollback();
                    //Response.Write("The record was not updated");
                }
                registrationsBtn();
                lblRegistrationSummaryCurrent.Text = "Current Registration";
                dvStaticRegistration.Visible = false;
                lblRegistrationSummaryPrevious.Visible = false;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("NULL"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot Add Registration the Term or Class does not Exist')", true);
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot Add dupicate Registration')", true);
                   // Response.Redirect("~/pages/profile.aspx?PersonId=" + pId + "&action=reg");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Cannot Add dupicate Registration');window.location = '/pages/profile.aspx?PersonId=" + pId + "&action=reg';", true); 
                }
            }
            finally
            {

            }
        }

        protected void SqlDataSourceRegistration_Inserting(object sender, SqlDataSourceCommandEventArgs e)
        {
            e.Command.Connection.Open();
            e.Command.Transaction = e.Command.Connection.BeginTransaction();
        }
        protected void LinkButtonNew_Click(object sender, EventArgs e)
        {
            dvStaticRegistration.Visible = true;
            lblRegistrationSummaryCurrent.Text = "New Registration";
            lblRegistrationSummaryPrevious.Visible = true;
        }

        protected void LinkButtonCancel_Click(object sender, EventArgs e)
        {
            lblRegistrationSummaryCurrent.Text = "Current Registration";
            var personId = Request.QueryString["PersonId"];
            Response.Redirect("~/pages/profile.aspx?PersonId=" + personId + "&action=reg");
            lblRegistrationSummaryPrevious.Visible = false;
        }

        //protected void LinkButtonInsert_Click(object sender, EventArgs e)
        //{
        //    lblRegistrationSummaryCurrent.Text = "Current Registration";
        //    var personId = Request.QueryString["PersonId"];
            
        //    Response.Redirect("~/pages/profile.aspx?PersonId=" + personId + "&action=reg");
        //    lblRegistrationSummaryPrevious.Visible = false;
        //}

       

        protected void InsertRegistration_Click(object sender, EventArgs e)
        {
            dvStaticRegistration.Visible = true;
            lblRegistrationSummaryCurrent.Text = "New Registration";
            lblRegistrationSummaryPrevious.Visible = false;
        }

        protected void btnIdCardPrint_Click(object sender, EventArgs e)
        {
           var personId = Request.QueryString["PersonId"];
           Server.Transfer("studentId_card.aspx?stuID_card=" + personId);
        }

        protected void btniQPwdReset_Click(object sender, EventArgs e)
        {
            var personId = Request.QueryString["PersonId"]; 
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_iQ_login_password_reset", con);
              
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "updated_by", Value = HttpContext.Current.User.Identity.Name });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "person_id", Value = personId });


                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            }
            finally
            {
                con.Close();
                iQOnlineKeyGenerate();
            }
        }

        protected void btnReportCard_Click(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((Button)sender).Parent.Parent)
            {
                string RegID = GridViewAssesments.DataKeys[row.RowIndex].Values["reg_id"].ToString();
                var personId = Request.QueryString["PersonId"];
                if (GridViewAssesments.Rows[row.RowIndex].Cells[6].Text == "Senior Secondary" || GridViewAssesments.Rows[row.RowIndex].Cells[6].Text ==  "Junior Secondary")
                   { Server.Transfer("report_card.aspx?rptID_card=" + personId + "&PID=" + RegID); }
                //else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! Report Card not found for section: " + GridViewAssesments.Rows[row.RowIndex].Cells[6].Text.ToString() + "');", true); }
                else if (GridViewAssesments.Rows[row.RowIndex].Cells[6].Text == "Primary") { Server.Transfer("reportcard_primary.aspx?rptID_card=" + personId + "&PID=" + RegID); }
                else if (GridViewAssesments.Rows[row.RowIndex].Cells[6].Text == "Nursery") { Server.Transfer("reportcard_nursery.aspx?rptID_card=" + personId + "&PID=" + RegID); }
            }
        }

       

        private void App2RegClassNameDropDown()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_class_name_dropdown", con);
            Label Item = DetailsViewApplication.FindControl("lblAppForm") as Label;
           // DropDownList Item = DetailsViewApplication.FindControl("ddlAppForm") as DropDownList;
            cmd.Parameters.Add("@form_name", SqlDbType.VarChar).Value = Item.Text;
            cmd.CommandType = CommandType.StoredProcedure;
            try
                {
                con.Open();
                SqlDataReader ddlValues;
                ddlValues = cmd.ExecuteReader();
                ddlValues.Close();
                ddlValues = cmd.ExecuteReader();
               //var ddlClassName = (DropDownList)DetailsViewApplication.FindControl("ddlCN");
                ddlCN.Items.Clear();
                ddlCN.DataSource = ddlValues;
                ddlCN.DataValueField = "class_name";
                ddlCN.DataTextField = "class_name";
                ddlCN.Items.Insert(0, new ListItem("Please select Arm", ""));
                if (ddlCN != null)
                {
                    ddlCN.DataBind();
                }
                }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        protected void btnApp2Reg_Click(object sender, EventArgs e)
        {
            var personId = Request.QueryString["PersonId"];
            Label App2RegAcadYear = DetailsViewApplication.FindControl("lblAppAcademicYear") as Label;
            Label App2RegTerm = DetailsViewApplication.FindControl("lblAppTerm") as Label;
            Label App2RegClass = DetailsViewApplication.FindControl("lblAppForm") as Label;
            string RegBoarderType = ddlAppRegBoarderType.SelectedItem.ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_person_registration_add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                cmd.Parameters.AddWithValue("@person_id", personId);
                cmd.Parameters.AddWithValue("@app_id", DBNull.Value);
                cmd.Parameters.AddWithValue("@reg_date", DateTime.Today.ToString("dd/MM/yyyy"));
                cmd.Parameters.AddWithValue("@status", "Active");
                cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                cmd.Parameters.AddWithValue("@acad_year", App2RegAcadYear.Text);
                cmd.Parameters.AddWithValue("@term_name", App2RegTerm.Text);
                cmd.Parameters.AddWithValue("@class_name", ddlCN.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@form_name", App2RegClass.Text);
                cmd.Parameters.AddWithValue("type_description", RegBoarderType);
                cmd.Parameters.AddWithValue("@from", "Insert");
                cmd.ExecuteNonQuery();
                AppStatusUpdate();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Registered successfully');window.location ='/pages/profile.aspx?PersonId= "+ personId + "&action=reg'; ", true);
             }
            catch (Exception ex)
            {
                if (ex.Message.Contains("unique_msregistrations_personid_aytermid_formid"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Registration exist already for this Application')", true);
                }
                else 
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                }
                
           }
            finally
            {
                con.Close();
                ClearData(this);
            }
        }


        protected void AppStatusUpdate()
        {

            string AppId = DetailsViewApplication.Rows[1].Cells[1].Text; 
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_person_app2reg_edit", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@app_id", AppId);
                cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
               ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            }
            finally
            {
                con.Close();
               
            }
        }

        private void ParentAccessStatus()
        {
          var personId = Request.QueryString["PersonId"];
           SqlDataAdapter adp = new SqlDataAdapter();
           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select [status] from [dbo].[ms_parent_login] where person_id ="+ personId, con);
                cmd.CommandType = CommandType.Text;
               
                cmd.Dispose();

                SqlDataReader dr = cmd.ExecuteReader();
                string status= string.Empty;
                 while (dr.Read())
                {
                    status = dr["status"].ToString();
                }
                btnParentEnable.Text = status;
                btnParentEnable.Enabled = true;
                if (btnParentEnable.Text == "ENABLE") { btnParentEnable.Text = "DISABLE"; btnParentEnable.CssClass = "btn btn-success btn-xs"; }
                else if (btnParentEnable.Text == "DISABLE") { btnParentEnable.Text = "ENABLE"; btnParentEnable.CssClass = "btn btn-primary btn-xs"; }
                else { btnParentEnable.Enabled = false; btnParentEnable.Text = "N/A"; }
                    
             }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
            }

        }

        protected void btnParentEnable_Click(object sender, EventArgs e)
        {
            var personId = Request.QueryString["PersonId"];
            Button btntext = btnParentEnable.FindControl("btnParentEnable") as Button;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_parent_login_edit", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@person_id", SqlDbType.Int).Value = personId;
                        cmd.Parameters.Add("@updated_by", SqlDbType.VarChar).Value = HttpContext.Current.User.Identity.Name;
                        if (btntext.Text == "ENABLE")
                        {
                            cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = "ENABLE";
                        }
                        else
                        {
                            cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = "DISABLE";
                        }
                        cmd.ExecuteNonQuery();
                        ParentAccessStatus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Success');", true);
                    }

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                }
                finally
                {
                    con.Close();

                }
            
        }

        protected void chkStudent_CheckedChanged(object sender, EventArgs e)
        {
          
          try
            {
                foreach (GridViewRow row in GridViewBulkPurchases.Rows)
                {
                    CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkStudent");
                    TextBox txtBulkRef = (TextBox)row.FindControl("tbBulkPurReference");
                    RequiredFieldValidator rfv = (RequiredFieldValidator)row.FindControl("rfvtbBulkPurReference");
                    if (ChkBoxRows.Checked == true)
                    {
                        if (txtBulkRef.Text.Length == 0)
                        {
                            txtBulkRef.Attributes["required"] = "true";
                            
                        }
                    }
                   
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            }

        }

        protected void btnPrintPurchases_Click(object sender, EventArgs e)
        {
            var PerID = Request.QueryString["PersonId"];
            Server.Transfer("purchases_summary_receipt.aspx?PID=" + PerID );
        }

        protected void GridViewAssesments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = GridViewAssesments.SelectedRow;
            string TxtTerm = string.Empty;
              if (e.Row.RowType == DataControlRowType.DataRow)
              {
                  TxtTerm = e.Row.Cells[1].Text;
                  Button btntext = e.Row.FindControl("btnCmRptCard") as Button;
                  if (TxtTerm == "3rd")
                  {
                      GridViewAssesments.Columns[7].Visible = true;
                      btntext.Visible = true;
                  }
                  else { btntext.Visible = false;  }
                    
              }
             
        }

        protected void btnCmRptCard_Click(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((Button)sender).Parent.Parent)
            {
                string RegID = GridViewAssesments.DataKeys[row.RowIndex].Values["reg_id"].ToString();
                var personId = Request.QueryString["PersonId"];
                if (GridViewAssesments.Rows[row.RowIndex].Cells[6].Text == "Senior Secondary" || GridViewAssesments.Rows[row.RowIndex].Cells[6].Text == "Junior Secondary")
                { Server.Transfer("report_card_cumulative.aspx?rptID_card=" + personId + "&PID=" + RegID); }
                //else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! Report Card not found for section: " + GridViewAssesments.Rows[row.RowIndex].Cells[6].Text.ToString() + "');", true); }
                else if (GridViewAssesments.Rows[row.RowIndex].Cells[6].Text == "Primary") { Server.Transfer("reportcard_primary.aspx?rptID_card=" + personId + "&PID=" + RegID); }
                else if (GridViewAssesments.Rows[row.RowIndex].Cells[6].Text == "Nursery") { Server.Transfer("reportcard_nursery.aspx?rptID_card=" + personId + "&PID=" + RegID); }
            }
        }

        public void NotesBind()
        {
            var personId = Request.QueryString["PersonId"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_person_notes_summary", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@person_id", SqlDbType.VarChar).Value = personId;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            GridViewNotes.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                lblZeroNotes.Visible = true;
                                lblZeroNotes.Text = "No Notes Found ";
                                GridViewNotes.Visible = false;

                            }
                            else
                            {
                                GridViewNotes.DataBind();
                                GridViewNotes.Visible = true;
                                lblZeroNotes.Visible = false;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
            }
        }

        protected void btnAddNotes_Click(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((Button)sender).Parent.Parent)
            {
                string RegId = row.Cells[5].Text;
                var personId = Request.QueryString["PersonId"];
                //Server.Transfer("~/pages/profile.aspx?PersonId=26");DetailsViewRegistration
                Server.Transfer("~/pages/notes_add.aspx?reg_id=" + RegId + "&PID=" + personId);
            }
        }

        protected void GridViewNotesBrkDown_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewNotesBrkDown.EditIndex = e.NewEditIndex;
            NoteBreakDown();
        }

        protected void GridViewNotesBrkDown_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GridViewNotesBrkDown.EditIndex = -1;
            NoteBreakDown();
        }

        protected void GridViewNotesBrkDown_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_person_notes_edit", con);
                GridViewRow row = GridViewNotesBrkDown.Rows[e.RowIndex] as GridViewRow;
                TextBox txtComment1 = row.FindControl("tbComment1") as TextBox;
                //TextBox txtComment2 = row.FindControl("tbComment2") as TextBox;
                TextBox txtDate = row.FindControl("tbNoteDate") as TextBox;
                TextBox txtType = row.FindControl("tbNoteType") as TextBox;
               // DropDownList ddlEditSection = row.FindControl("ddlEditSection") as DropDownList;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "reg_id", Value = GridViewNotesBrkDown.Rows[e.RowIndex].Cells[0].Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "note_id", Value = GridViewNotesBrkDown.Rows[e.RowIndex].Cells[1].Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "comment_1", Value = txtComment1.Text });
               // cmd.Parameters.Add(new SqlParameter() { ParameterName = "comment_2", Value = txtComment2.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "note_date", Value = txtDate.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "note_type", Value = txtType.Text });
                cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
                GridViewNotesBrkDown.EditIndex = -1;
                NoteBreakDown();
            }
            catch
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "clentscript", "alert('Cannot update, Notes already exist');", true);
            }
            finally
            {
                con.Close();
            }
        }

        protected void GridViewNotesBrkDown_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_person_notes_delete", con);
                GridViewRow row = GridViewNotesBrkDown.Rows[e.RowIndex] as GridViewRow;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "reg_id", Value = GridViewNotesBrkDown.Rows[e.RowIndex].Cells[0].Text });
                 cmd.Parameters.Add(new SqlParameter() { ParameterName = "note_id", Value = GridViewNotesBrkDown.Rows[e.RowIndex].Cells[1].Text });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
                NoteBreakDown();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Cannot Delete, Class was used in Class Name');", true);
            }
            finally
            {
                con.Close();
            }
        }

        protected void NoteBreakDown()
        {
            var personId = Request.QueryString["PersonId"];
            var regid = Request.QueryString["Regid"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_person_notes_list_all", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@person_id", SqlDbType.VarChar).Value = personId;
                    cmd.Parameters.Add("@reg_id", SqlDbType.VarChar).Value = regid;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridViewNotesBrkDown.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                GridViewNotesBrkDown.Visible = false;
                                divNoteslabel.Visible = false;
                                lblZeroBrkDown.Visible = true;
                                lblZeroBrkDown.Text = "No Notes History Found ";

                            }
                            else
                            {
                                GridViewNotesBrkDown.Visible = true;
                                divNoteslabel.Visible = true;
                                lblNoteClass.Text = dt.Rows[0]["form_name"].ToString();
                                lblNoteAcademic.Text = dt.Rows[0]["acad_year"].ToString();
                                lblNoteTerm.Text = dt.Rows[0]["term_name"].ToString();
                                GridViewNotesBrkDown.DataBind();
                                lblZeroBrkDown.Visible = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
            }
        }

       

       

       
      
        
       
       
















    }
}