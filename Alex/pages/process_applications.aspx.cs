using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Configuration;
using Alex.App_code;
namespace Alex.pages
{
    public partial class process_applications : System.Web.UI.Page
    {
        int lvl = 0;
        //string selectedValue = string.Empty;
        //private static string currentYear = DateTime.Now.Year.ToString();
        //private static string prevYear = (Convert.ToInt32(currentYear) - 1).ToString();
        //private static string yearFormat = prevYear + "/" + currentYear.Substring(2);
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            Level();
            if (lvl == 1 || lvl ==2 || lvl == 3 )
            {
               lblZeroRecords.Text = "";
            //GridViewProcessApplications.Visible = false;
                if (!Page.IsPostBack)
              {
                DropDownYear();
                DropDownTerm();
                AppStatusDropDown();
                AppConvertStatusDropDown();
                DropDownForm();
                ddlForm.SelectedIndex = 1;
                //ddlTerm.SelectedIndex = 1;
                ddlStatus.SelectedIndex = 1;
                ddlStatusConvert.SelectedIndex = 1;
               // ddlAcademicYear.SelectedIndex = 1;
                BindGrid();
              
              }
            }
            else if (lvl == 4  )
            {
                Response.Redirect("~/pages/ForbiddenAccess.aspx", false);
            }
            else
            {
                Response.Redirect("~/pages/login.aspx", false);
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
        public void DropDownForm()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_form_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlForm.DataSource = ddlValues;
            ddlForm.DataValueField = "form_name";
            ddlForm.DataTextField = "form_name";
            ddlForm.DataBind();
            ddlForm.Items.Insert(0, new ListItem("Please select Class", "0"));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
        public void DropDownYear()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_active_acad_year_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            try
            {
                SqlDataReader ddlValues;
                ddlValues = cmd.ExecuteReader();
                string YearSelectedValue = null;

                while (ddlValues.Read())
                {
                    YearSelectedValue = ddlValues[1].ToString();
                    int DefaultValue = Convert.ToInt32(ddlValues[2]);
                    if (DefaultValue == 1)
                        break;
                }
                ddlValues.Close();
                ddlValues = cmd.ExecuteReader();

                ddlAcademicYear.DataSource = ddlValues;
                ddlAcademicYear.DataValueField = "acad_year";
                ddlAcademicYear.DataTextField = "acad_year";
                ddlAcademicYear.DataBind();
                ddlAcademicYear.Items.Insert(0, new ListItem("Select Academic Year", ""));
                ddlAcademicYear.SelectedValue = YearSelectedValue;
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
        public void DropDownTerm()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_term_dropdown_v2", con);
            cmd.Parameters.Add("@acad_year", SqlDbType.VarChar).Value = ddlAcademicYear.SelectedItem.ToString();
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
                ddlTerm.DataSource = ddlValues;
                ddlTerm.DataValueField = "term_name";
                ddlTerm.DataTextField = "term_name";
                ddlTerm.DataBind();
                ddlTerm.Items.Insert(0, new ListItem("Please select Term", ""));
                ddlTerm.SelectedValue = TermSelectedValue;
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
        public void AppStatusDropDown()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_status_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@category", "Applications");
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlStatus.DataSource = ddlValues;
            ddlStatus.DataValueField = "status_name";
            ddlStatus.DataTextField = "status_name";
            ddlStatus.DataBind();
             //Adding "Please select" option in dropdownlist for validation
            ddlStatus.Items.Insert(0, new ListItem("Please select Status", "0"));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void AppConvertStatusDropDown()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_status_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@category", "Applications");
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlStatusConvert.DataSource = ddlValues;
            ddlStatusConvert.DataValueField = "status_name";
            ddlStatusConvert.DataTextField = "status_name";
            ddlStatusConvert.DataBind();
            //Adding "Please select" option in dropdownlist for validation
            ddlStatusConvert.Items.Remove(ddlStatus.Items.FindByValue("All"));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
       

        private void BindGrid()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_rep_application_list_all_year_term_status_form", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@acad_year", ddlAcademicYear.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@term_name", ddlTerm.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@form", ddlForm.SelectedItem.ToString());
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;

                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            GridViewProcessApplications.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                GridViewProcessApplications.Visible = false;
                                lblZeroRecords.Visible = true;
                                lblZeroRecords.Text = "No Records found ";
                            }
                            else
                            {
                                GridViewProcessApplications.Visible = true;
                                GridViewProcessApplications.DataBind();
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
       
     
        protected void GridViewProcessApplications_RowDataBound(object sender, GridViewRowEventArgs e)
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

        //protected void btnSearchAcadmicYear_Click(object sender, EventArgs e)
        //{
        //    //ApplicationsList();
        //    //BindGrid("All");
        //    BindGrid();
        //}

        protected void GridViewProcessApplications_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewProcessApplications.PageIndex = e.NewPageIndex;
            //ApplicationsList();
            //BindGrid("All");
            BindGrid();

        }

        protected void chkboxSelectAll_CheckedChanged1(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)GridViewProcessApplications.HeaderRow.FindControl("chkboxSelectAll1");
            foreach (GridViewRow row in GridViewProcessApplications.Rows)
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


        protected void GridConvert_click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            foreach (GridViewRow row in GridViewProcessApplications.Rows)
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_person_application_batch_status_edit", conn))
                {
                    CheckBox ChkBoxHeader = (CheckBox)GridViewProcessApplications.HeaderRow.FindControl("chkboxSelectAll1");
                    //CheckBox ChkBoxHeader = (CheckBox)GridViewProcessApplications..FindControl("chkStudent"); 
                    string ApplicationStatus = row.Cells[6].Text;
                    string AppID = row.Cells[1].Text;
                    string Status = ddlStatusConvert.SelectedValue.ToString();
                   // CheckBox chkdel = (CheckBox)GridViewProcessApplications.FindControl("chkStudent");
                    CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkStudent");
                    if (ChkBoxRows.Checked)
                    {
                        cmd.Parameters.Add("@application_status", SqlDbType.NVarChar, 40).Value = Status;
                        cmd.Parameters.Add("@app_id", SqlDbType.NVarChar, 40).Value = AppID;
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        int rowaffected = cmd.ExecuteNonQuery(); //this was missing.
                        conn.Close();
                    }
                }
            }
           ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Processed Application(s) Successfully ');", true);
           BindGrid();
        }

        protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownTerm();
            if (ddlTerm.Items.Count > 1)
            {
                ddlTerm.SelectedIndex = 1;
                BindGrid();
            }
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ddlForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
    }


}
