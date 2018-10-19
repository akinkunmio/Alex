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
    public partial class student_fee_summary : System.Web.UI.Page
    {
        int lvl = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            Level();
            if (!Page.IsPostBack)
            {
                DropDownYear();
                DropDownTerm();
                //ddlTerm.SelectedIndex = 1;
                SchoolBind();
                FeeSummary2();
                FeeSummary1();
                FeeSummary3();
            }
            if (lvl == 2 || lvl==3)
            {
                Response.Redirect("~/pages/ForbiddenAccess.aspx", false);
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
        //protected void BtnStudentStats_Click(object sender, EventArgs e)
        //{
        //    FeeSummary2();
        //    FeeSummary1();
        //    FeeSummary3();
        //}

        private void FeeSummary3()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_rep_termly_fee_summary3", con))
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
                                lblAcademicTotalOutstanding.Text = dt.Rows[0]["cummulative_outstandaing_debts_all_academic_years"].ToString();
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
        private void FeeSummary2()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_rep_termly_fee_summary2", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@acad_year", SqlDbType.VarChar).Value = ddlAcademicYear.SelectedItem.ToString();
                        cmd.Parameters.Add("@term_name", SqlDbType.VarChar).Value = ddlTerm.SelectedItem.ToString();
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                if (dt.Rows.Count == 0)
                                {
                                    lblZeroStudents.Visible = true;
                                    lblZeroStudents.Text = "No  Students Found ";
                                    DivStudents.Visible = false;
                                }
                                else
                                {
                                    DivStudents.Visible = true;
                                    lblZeroStudents.Visible = false;

                                    lblExpected.Text = dt.Rows[0]["Total_Amount_of_Fees_Expected"].ToString();
                                    lblReceived.Text = dt.Rows[0]["Total_Amount_of_Fees_Received"].ToString();
                                    lblOutstanding.Text = dt.Rows[0]["Total_Amount_of_Fees_Outstanding"].ToString();
                                   
                                    imgSchool.ImageUrl = "~/pages/ImageHandler.ashx?roll_no=1";
                                    lblyearSelected.Text = ddlAcademicYear.SelectedItem.ToString();
                                    lblTermSelected.Text = ddlTerm.SelectedItem.ToString();
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

        private void FeeSummary1()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_rep_termly_fee_summary1", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@acad_year", SqlDbType.VarChar).Value = ddlAcademicYear.SelectedItem.ToString();
                    cmd.Parameters.Add("@term_name", SqlDbType.VarChar).Value = ddlTerm.SelectedItem.ToString();
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {

                            sda.Fill(dt);
                            GridViewFeeReportsSummary1.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                GridViewFeeReportsSummary1.Visible = false;
                                btnPrint.Visible = false;
                            }
                            else
                            {
                                GridViewFeeReportsSummary1.DataBind();
                                GridViewFeeReportsSummary1.Visible = true;
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
                FeeSummary2();
                FeeSummary1();
                FeeSummary3();
            }
        }

        protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            FeeSummary2();
            FeeSummary1();
            FeeSummary3();
        }
    }
}