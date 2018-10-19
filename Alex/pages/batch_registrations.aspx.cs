using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alex.App_code;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Configuration;
namespace Alex.pages
{
    public partial class batch_registrations : System.Web.UI.Page
    {
        int lvl = 0;
        string FormName = string.Empty;
        string ClassName = string.Empty;
        //string selectedValue = string.Empty;
        //private static string currentYear = DateTime.Now.Year.ToString();
        //private static string prevYear = (Convert.ToInt32(currentYear) - 1).ToString();
        //private static string yearFormat = prevYear + "/" + currentYear.Substring(2);
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            Level();
            if (lvl == 1 || lvl == 2 || lvl == 3)
            {
                lblZeroRecords.Text = "";
                if (!Page.IsPostBack)
                {
                    DropDownBRFromYear();
                    DropDownBRTerm();
                    DropDownClass();

                    DropDownBRegYear();
                    //DropDownBRegForm();

                    DropDownBRegTerm();
                    DropDownBRegClass();
                    //DropDownForm();
                }
            }
            else if (lvl == 4 || lvl == 5)
            {
                Response.Redirect("~/pages/ForbiddenAccess.aspx", false);
            }
            else
            {
                Response.Redirect("~/pages/logout.aspx", false);
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

        public void DropDownBRFromYear()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_acad_year_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                SqlDataReader ddlValues;
                ddlValues = cmd.ExecuteReader();
                string YearSelectedValue = null;

                while (ddlValues.Read())
                {
                    YearSelectedValue = ddlValues[0].ToString();
                    int DefaultValue = Convert.ToInt32(ddlValues[1]);
                    if (DefaultValue == 1)
                        break;
                }
                ddlValues.Close();
                ddlValues = cmd.ExecuteReader();

                ddlFromBRYear.DataSource = ddlValues;
                ddlFromBRYear.DataValueField = "acad_year";
                ddlFromBRYear.DataTextField = "acad_year";
                ddlFromBRYear.DataBind();
                ddlFromBRYear.Items.Insert(0, new ListItem("Please select Year", ""));
                ddlFromBRYear.SelectedValue = YearSelectedValue;
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

        public void DropDownBRegYear()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_acad_year_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();
            ddlYearBReg.DataSource = ddlValues;
            ddlYearBReg.DataValueField = "acad_year";
            ddlYearBReg.DataTextField = "acad_year";
            ddlYearBReg.DataBind();
            ddlYearBReg.Items.Insert(0, new ListItem("Please select year", ""));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
        //public void DropDownForm()
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        //    SqlCommand cmd = new SqlCommand("sp_ms_rep_form_dropdown", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    con.Open();
        //    SqlDataReader ddlValues;
        //    ddlValues = cmd.ExecuteReader();

        //    ddlForm.DataSource = ddlValues;
        //    ddlForm.DataValueField = "form_name";
        //    ddlForm.DataTextField = "form_name";
        //    ddlForm.DataBind();
        //    ddlForm.Items.Insert(0, new ListItem("Please select Class", ""));
        //    cmd.Connection.Close();
        //    cmd.Connection.Dispose();
        //}

        //public void DropDownBRegForm()
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        //    SqlCommand cmd = new SqlCommand("sp_ms_rep_form_dropdown", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    con.Open();
        //    SqlDataReader ddlValues;
        //    ddlValues = cmd.ExecuteReader();
        //    ddlFormBReg.DataSource = ddlValues;
        //    ddlFormBReg.DataValueField = "form_name";
        //    ddlFormBReg.DataTextField = "form_name";
        //    ddlFormBReg.DataBind();
        //    ddlFormBReg.Items.Insert(0, new ListItem("Please select Class", ""));
        //    cmd.Connection.Close();
        //    cmd.Connection.Dispose();
        //}




        public void DropDownBRTerm()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_term_dropdown_v2", con);
            cmd.Parameters.Add("@acad_year", SqlDbType.VarChar).Value = ddlFromBRYear.SelectedItem.ToString();
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
                ddlFromBRTerm.DataSource = ddlValues;
                ddlFromBRTerm.DataValueField = "term_name";
                ddlFromBRTerm.DataTextField = "term_name";
                ddlFromBRTerm.DataBind();
                ddlFromBRTerm.Items.Insert(0, new ListItem("Please select Term", ""));
                ddlFromBRTerm.SelectedValue = TermSelectedValue;

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

        public void DropDownBRegTerm()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_term_dropdown_v2", con);
            cmd.Parameters.Add("@acad_year", SqlDbType.VarChar).Value = ddlYearBReg.SelectedItem.ToString();
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
                ddlTermBReg.DataSource = ddlValues;
                ddlTermBReg.DataValueField = "term_name";
                ddlTermBReg.DataTextField = "term_name";
                ddlTermBReg.DataBind();
                ddlTermBReg.Items.Insert(0, new ListItem("Please select Term", ""));
                ddlTermBReg.SelectedValue = TermSelectedValue;

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

        public void DropDownClass()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_form_class_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlClass.DataSource = ddlValues;
            ddlClass.DataValueField = "form_class";
            ddlClass.DataTextField = "form_class";
            ddlClass.DataBind();
            //Adding "Please select" option in dropdownlist for validation
            ddlClass.Items.Insert(0, new ListItem("Please select Class", ""));

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void DropDownBRegClass()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_form_class_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlClassBReg.DataSource = ddlValues;
            ddlClassBReg.DataValueField = "form_class";
            ddlClassBReg.DataTextField = "form_class";
            ddlClassBReg.DataBind();
            //Adding "Please select" option in dropdownlist for validation
            ddlClassBReg.Items.Insert(0, new ListItem("Please select Class", ""));

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
        //public void RegStatusDropDown()
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        //    SqlCommand cmd = new SqlCommand("sp_ms_status_dropdown", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    con.Open();
        //    cmd.Parameters.AddWithValue("@category", "Registrations");
        //    SqlDataReader ddlValues;
        //    ddlValues = cmd.ExecuteReader();

        //    ddlStatus.DataSource = ddlValues;
        //    ddlStatus.DataValueField = "status_name";
        //    ddlStatus.DataTextField = "status_name";
        //    ddlStatus.DataBind();

        //    ddlStatus.Items.Insert(0, new ListItem("Please select Status", ""));
        //    cmd.Connection.Close();
        //    cmd.Connection.Dispose();
        //}

        //public void DropDownStatusBReg()
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        //    SqlCommand cmd = new SqlCommand("sp_ms_status_dropdown", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    con.Open();
        //    cmd.Parameters.AddWithValue("@category", "Registrations");
        //    SqlDataReader ddlValues;
        //    ddlValues = cmd.ExecuteReader();

        //    ddlStatusBReg.DataSource = ddlValues;
        //    ddlStatusBReg.DataValueField = "status_name";
        //    ddlStatusBReg.DataTextField = "status_name";
        //    ddlStatusBReg.DataBind();

        //    ddlStatusBReg.Items.Insert(0, new ListItem("Please select Status", ""));
        //    cmd.Connection.Close();
        //    cmd.Connection.Dispose();
        //}

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

        //protected void DataListAlpha_ItemCommand(object source, DataListCommandEventArgs e)
        //{
        //    LinkButton lbkbtnPaging = (LinkButton)e.CommandSource;
        //    BindGrid(e.CommandArgument.ToString());
        //    this.ViewState["SelectedText"] = e.CommandArgument.ToString();
        //    CreateAlphaPagings();
        //}
        //protected void DataListAlpha_ItemDataBound(object sender, DataListItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        //    {
        //        if (this.ViewState["SelectedText"] != null)
        //        {
        //            LinkButton lbkbtnPaging = (LinkButton)e.Item.FindControl("lnkbtnPaging");
        //            if (this.ViewState["SelectedText"].ToString() == lbkbtnPaging.Text)
        //                lbkbtnPaging.Enabled = false;
        //        }
        //    }
        //}

        public void FormClassNames(string FormClass)
        {
            SqlDataAdapter adp = new SqlDataAdapter();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_form_class_split_dropdown", con);
                cmd.Parameters.AddWithValue("@class_form", FormClass);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Dispose();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    FormName = dr["form_name"].ToString();
                    ClassName = dr["class_name"].ToString();

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

        private void BindGrid()
        {
            string FormClass = ddlClass.SelectedItem.ToString();
            //string Class = FormClass.Split(' ')[1];
            //string Form = FormClass.Split(' ')[0] + " " + Class.Substring(0,1);
            //string Arm = Class.Substring(1, 1);
            FormClassNames(FormClass);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_registrations_list_all_year_term_status_form_class", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@acad_year", ddlFromBRYear.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@term_name", ddlFromBRTerm.SelectedItem.ToString());
                    // cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@form", FormName);
                    cmd.Parameters.AddWithValue("@class", ClassName);
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;

                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            GridViewBatchRegistrations.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                divProcessNow.Visible = false;
                                GridViewBatchRegistrations.Visible = false;
                                lblZeroRecords.Visible = true;
                                lblZeroRecords.Text = "No Records found ";
                            }
                            else
                            {
                                divProcessNow.Visible = true;
                                GridViewBatchRegistrations.Visible = true;
                                GridViewBatchRegistrations.DataBind();
                                lblZeroRecords.Visible = false;
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

        //private void BindGrid(string StartAlpha)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);

        //    try
        //    {
        //        string SP_Name = "sp_ms_registrations_list_all_year_term_status_form_class";
        //        if (StartAlpha != "All")
        //        {
        //            SP_Name = "sp_ms_registrations_list_all_year_term_status_form_class_alphabetic";
        //        }
        //        using (SqlCommand cmd = new SqlCommand(SP_Name, con))
        //        {
        //            using (SqlDataAdapter sda = new SqlDataAdapter())
        //            {
        //                if (StartAlpha != "All")
        //                {
        //                    cmd.CommandType = CommandType.StoredProcedure;
        //                    con.Open();
        //                    cmd.Parameters.AddWithValue("@lastname", StartAlpha);
        //                    //cmd.ExecuteNonQuery();
        //                }
        //                else
        //                {
        //                    con.Open();
        //                    cmd.Connection = con;
        //                }
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@acad_year", ddlFromBRYear.SelectedItem.ToString());
        //                cmd.Parameters.AddWithValue("@term_name", ddlFromBRTerm.SelectedItem.ToString());
        //                cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedItem.ToString());
        //                cmd.Parameters.AddWithValue("@form", ddlForm.SelectedItem.ToString());
        //                cmd.Parameters.AddWithValue("@class", ddlClass.SelectedItem.ToString());
        //                sda.SelectCommand = cmd;
        //                using (DataTable dt = new DataTable())
        //                {
        //                    sda.Fill(dt);

        //                    GridViewBatchRegistrations.DataSource = dt;
        //                    if (dt.Rows.Count == 0)
        //                    {
        //                        GridViewBatchRegistrations.Visible = false;
        //                        lblZeroRecords.Visible = true;
        //                        lblZeroRecords.Text = "No Records found ";
        //                    }
        //                    else
        //                    {
        //                        GridViewBatchRegistrations.Visible = true;
        //                        GridViewBatchRegistrations.DataBind();
        //                        lblZeroRecords.Visible = false;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //        // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        protected void GridViewBatchRegistrations_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hl = (HyperLink)e.Row.FindControl("HyperLinkPeople");
                if (hl != null)
                {
                    DataRowView drv = (DataRowView)e.Row.DataItem;
                    string keyword = drv["person_id"].ToString();
                    hl.NavigateUrl = "~/pages/profile.aspx?PersonId=" + keyword;
                }
            }
        }

        //protected void btnSearchFilters_Click(object sender, EventArgs e)
        //{
        //   BindGrid();
        //}


        protected void GridViewBatchRegistrations_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewBatchRegistrations.PageIndex = e.NewPageIndex;
            //BindGrid("All");
            BindGrid();

        }

        protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)GridViewBatchRegistrations.HeaderRow.FindControl("chkboxSelectAll");
            foreach (GridViewRow row in GridViewBatchRegistrations.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkStudent");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
        }

        protected void btnBatchRegistrations_Click(object sender, EventArgs e)
        {
            string FormClass = ddlClassBReg.SelectedItem.ToString();
            //string Class = FormClass.Split(' ')[1];
            //string Form = FormClass.Split(' ')[0] + " " + Class.Substring(0, 1);
            //string Arm = Class.Substring(1, 1); 
            FormClassNames(FormClass);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                foreach (GridViewRow row in GridViewBatchRegistrations.Rows)
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_batch_registration_add", con))
                    {
                        CheckBox ChkBoxHeader = (CheckBox)GridViewBatchRegistrations.HeaderRow.FindControl("chkboxSelectAll1");
                        //CheckBox ChkBoxHeader = (CheckBox)GridViewProcessApplications.FindControl("chkStudent"); 
                        string PerId = row.Cells[1].Text;
                        CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkStudent");
                        if (ChkBoxRows.Checked)
                        {
                            cmd.Parameters.Add("@person_id", SqlDbType.NVarChar, 40).Value = PerId;
                            cmd.Parameters.Add("@app_id", SqlDbType.NVarChar, 40).Value = DBNull.Value;
                            //cmd.Parameters.Add("@reg_date", SqlDbType.NVarChar, 40).Value = DateTime.Now.ToString();
                            // cmd.Parameters.AddWithValue("@status", "Active");//ddlStatusBReg.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                            cmd.Parameters.AddWithValue("@acad_year", ddlYearBReg.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@term_name", ddlTermBReg.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@class_name", ClassName);
                            cmd.Parameters.AddWithValue("@form_name", FormName);
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            int rowaffected = cmd.ExecuteNonQuery(); //this was missing.
                            con.Close();
                        }
                    }
                    using (SqlCommand cmd = new SqlCommand("sp_ms_batch_registration_update_status_completed", con))
                    {
                        CheckBox ChkBoxHeader = (CheckBox)GridViewBatchRegistrations.HeaderRow.FindControl("chkboxSelectAll1");
                        //CheckBox ChkBoxHeader = (CheckBox)GridViewProcessApplications.FindControl("chkStudent"); 
                        string RegId = row.Cells[2].Text;
                        CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkStudent");
                        if (ChkBoxRows.Checked)
                        {
                            cmd.Parameters.Add("@reg_id", SqlDbType.NVarChar, 40).Value = RegId;
                            cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            int rowaffected = cmd.ExecuteNonQuery(); //this was missing.
                            con.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Check Term,Class,Class Name either doesnot exists for Academic Year in settings or Cannot register for already registered students');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
                // ClearData(this);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Student(s) Registered Successfully');", true);
            }

            GridViewBatchRegistrations.Visible = false;
        }

        protected void ddlFromBRYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownBRTerm();
            //Joe 250118 2352 Changed value from 1 to 0 so that the term dropdown can start from the please select term list so as not to throw error when please select year is selected 
            //ddlFromBRTerm.SelectedIndex = 1;
            ddlFromBRTerm.SelectedIndex = 0;
            BindGrid();

        }

        protected void ddlYearBReg_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownBRegTerm();
            ddlTermBReg.SelectedIndex = 0;
        }

        protected void ddlFromBRTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ddlForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

    }
}