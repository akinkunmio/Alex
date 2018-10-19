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

namespace Alex.pages
{
    public partial class fees : System.Web.UI.Page
    {
        //string selectedValue = string.Empty;
        //private static string currentYear = DateTime.Now.Year.ToString();
        //private static string prevYear = (Convert.ToInt32(currentYear) - 1).ToString();
        //private static string yearFormat = prevYear + "/" + currentYear.Substring(2);

        protected void Page_Load(object sender, EventArgs e)
        {
            //ManageCookies.VerifyAuthentication();
            lblZeroRecords.Text = "";
            GridViewFee.Visible = false;

            if (!Page.IsPostBack)
            {
                DropDownYear();
                DropDownTerm();
                //ddlTerm.SelectedIndex = 1;
                FeeAcademicYear();
               
            }

        }

        //protected void btnSearchAcadmicYear_Click(object sender, EventArgs e)
        //{
        //    FeeAcademicYear();
        //}

        private void FeeAcademicYear()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_settings_fees_search_year_term", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@acad_year", ddlAcademicYear.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@term_name", ddlTerm.SelectedItem.ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        GridViewFee.DataSource = dt;
                        if (dt.Rows.Count == 0)
                        {
                            lblZeroRecords.Text = "No Fee has been setup for this year";
                        }
                        else
                        {
                            GridViewFee.DataBind();
                            GridViewFee.Visible = true;
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

        public void DropDownYear()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_acad_year_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
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

            ddlAcademicYear.DataSource = ddlValues;
            ddlAcademicYear.DataValueField = "acad_year";
            ddlAcademicYear.DataTextField = "acad_year";
            ddlAcademicYear.DataBind();
            ddlAcademicYear.Items.Insert(0, new ListItem("Select Academic Year", ""));
            ddlAcademicYear.SelectedValue = YearSelectedValue;
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void DropDownTerm()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_term_dropdown_v2", con);
            cmd.Parameters.Add("@acad_year", SqlDbType.VarChar).Value = ddlAcademicYear.SelectedItem.ToString();
            cmd.CommandType = CommandType.StoredProcedure;
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

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownTerm();
            ddlTerm.SelectedIndex = 1;
            FeeAcademicYear();
            
        }

        protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            FeeAcademicYear();
        }
    }
}