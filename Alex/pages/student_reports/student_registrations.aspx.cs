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
    public partial class student_registrations : System.Web.UI.Page
    {
        int lvl = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Level();
            if (lvl == 1 || lvl == 2 || lvl == 3)
            {
                if (!Page.IsPostBack)
                {
                    DropDownYear();
                    DropDownTerm();
                    DropDownFormClass();
                    ddlClass.SelectedIndex = 1;
                    SchoolBind();
                    RegistrationsList();
                    RegistrationsStatus();
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

        public void DropDownFormClass()
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
           ddlClass.Items.Insert(0, new ListItem("Please select Class",""));
           cmd.Connection.Close();
           cmd.Connection.Dispose();
        }
        //protected void BtnStudentRegistrations_Click(object sender, EventArgs e)
        //{
        //    RegistrationsStatus();
        //    RegistrationsList();
        //}

        private void RegistrationsStatus()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_rep_registered_students2", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@acad_year", SqlDbType.VarChar).Value = ddlAcademicYear.SelectedItem.ToString();
                        cmd.Parameters.Add("@term_name", SqlDbType.VarChar).Value = ddlTerm.SelectedItem.ToString();
                        cmd.Parameters.Add("@formclass", SqlDbType.VarChar).Value = ddlClass.SelectedItem.ToString();
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
                                    lblZeroStudents.Text = "No  Registered Students Found ";
                                    DivStudents.Visible = false;
                                }
                                else
                                {
                                    DivStudents.Visible = true;
                                    lblZeroStudents.Visible = false;
                                    lblActive.Text = dt.Rows[0]["Active"].ToString();
                                    lblWithdrawn.Text = dt.Rows[0]["Withdrawn"].ToString();
                                    lblCompleted.Text = dt.Rows[0]["Completed"].ToString();

                                    imgSchool.ImageUrl = "~/pages/ImageHandler.ashx?roll_no=1";
                                    lblyearSelected.Text = ddlAcademicYear.SelectedItem.ToString();
                                    lblTermSelected.Text = ddlTerm.SelectedItem.ToString();
                                    lblClassSelected.Text = ddlClass.SelectedItem.ToString();
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

        private void RegistrationsList()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_rep_registered_students", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@acad_year", SqlDbType.VarChar).Value = ddlAcademicYear.SelectedItem.ToString();
                    cmd.Parameters.Add("@term_name", SqlDbType.VarChar).Value = ddlTerm.SelectedItem.ToString();
                    cmd.Parameters.Add("@formclass", SqlDbType.VarChar).Value = ddlClass.SelectedItem.ToString();
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {

                            sda.Fill(dt);
                            GridViewRegisteredStudents.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                               
                                GridViewRegisteredStudents.Visible = false;
                                btnPrint.Visible = false;
                            }
                            else
                            {
                                GridViewRegisteredStudents.DataBind();
                                GridViewRegisteredStudents.Visible = true;
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
                RegistrationsList();
                RegistrationsStatus();
            }
            
        }

        protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegistrationsStatus();
            RegistrationsList();
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegistrationsStatus();
            RegistrationsList();
        }
    }
}