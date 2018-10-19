using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
//using Alex.App_code;

namespace Alex.pages.application_reports
{
    public partial class status_year_term : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ManageCookies.VerifyAuthentication();
            if(!Page.IsPostBack)
            {
                DropDownYear();
                AppStatusDropDown();
            }
        }
        public void DropDownYear()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_acad_year_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();
            ddlAcademicYear.DataSource = ddlValues;
            ddlAcademicYear.DataValueField = "acad_year";
            ddlAcademicYear.DataTextField = "acad_year";
            ddlAcademicYear.DataBind();
            //ddlFeeSetupYear.SelectedValue = yearFormat;

            //Adding "Please select" option in dropdownlist for validation

            ddlAcademicYear.Items.Insert(0, new ListItem("Please select a Year", "0"));


            cmd.Connection.Close();
            cmd.Connection.Dispose();
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
        private void Applications_status_by_year()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_rep_applications_total_status_by_year_term", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@acad_year", ddlAcademicYear.Text.ToString());
                cmd.Parameters.AddWithValue("@status", ddlStatus.Text.ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        GridViewReportApplicationStatusByYear.DataSource = dt;
                        if (dt.Rows.Count == 0)
                        {
                            GridViewReportApplicationStatusByYear.Visible = false;
                            lblZeroRecords.Visible = true;
                            lblZeroRecords.Text = "No Records found ";
                        }
                        else
                        {
                            GridViewReportApplicationStatusByYear.Visible = true;
                            GridViewReportApplicationStatusByYear.DataBind();
                            lblZeroRecords.Visible = false;
                        }
                       
                    }
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('SUCESSFULLY RECORD SAVED');", true);
            }
        }

        protected void BtnListYearTerm_Click(object sender, EventArgs e)
        {
            Applications_status_by_year();
            
        }
    }
}