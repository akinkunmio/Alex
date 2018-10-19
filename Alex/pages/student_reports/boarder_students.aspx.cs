using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Alex.App_code;

namespace Alex.pages.student_reports
{
    public partial class boarder_students : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownYear();
                DropDownTerm();
                SchoolBind();
                DropDownBoarderTypes();
                ddlBoarderType.SelectedIndex = 1;
                BoardersDataBind();
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

        public void DropDownBoarderTypes()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_boarder_type_drpdwn", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlBoarderType.DataSource = ddlValues;
            ddlBoarderType.DataValueField = "boarder";
            ddlBoarderType.DataTextField = "boarder";
            ddlBoarderType.DataBind();
            ddlBoarderType.Items.Insert(0, new ListItem("Please select Boarder Type", ""));
            ddlBoarderType.Items.Insert(1, new ListItem("ALL"));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        private void BoardersDataBind()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_rep_students_on_boarders", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@acad_year", SqlDbType.VarChar).Value = ddlAcademicYear.SelectedItem.ToString();
                    cmd.Parameters.Add("@term_name", SqlDbType.VarChar).Value = ddlTerm.SelectedItem.ToString();
                    cmd.Parameters.Add("@type_description", SqlDbType.VarChar).Value =ddlBoarderType.SelectedItem.ToString();
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {

                            sda.Fill(dt);
                            GridViewBoarderStudents.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                GridViewBoarderStudents.Visible = false;
                                lblZeroStudents.Visible = true;
                                lblZeroStudents.Text = "No  Students Found ";
                                DivStudents.Visible = false;
                            }
                            else
                            {
                                DivStudents.Visible = true;
                                lblZeroStudents.Visible = false;
                                imgSchool.ImageUrl = "~/pages/ImageHandler.ashx?roll_no=1";
                                lblyearSelected.Text = ddlAcademicYear.SelectedItem.ToString();
                                lblTermSelected.Text = ddlTerm.SelectedItem.ToString();
                                lblBoarderTypeSelected.Text = ddlBoarderType.SelectedItem.ToString();
                                GridViewBoarderStudents.DataBind();
                                GridViewBoarderStudents.Visible = true;
                                btnPrint.Visible = true;

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

        public void SchoolBind()
        {
            SqlDataAdapter adp = new SqlDataAdapter();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select school_name from ms_school", con);
                cmd.Dispose();
                SqlDataReader dr = cmd.ExecuteReader();
                string SchoolName = string.Empty;
                while (dr.Read())
                {
                    SchoolName = dr["school_name"].ToString();
                    lblName.Text = SchoolName.ToString();
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

      

        protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownTerm();
            if (ddlTerm.Items.Count > 1)
            {
                ddlTerm.SelectedIndex = 1;
                BoardersDataBind();
            }

        }

        protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            BoardersDataBind();
        }

        protected void ddlBoarderType_SelectedIndexChanged(object sender, EventArgs e)
        { BoardersDataBind(); }
    }
}