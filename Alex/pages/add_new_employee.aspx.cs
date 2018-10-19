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

namespace Alex.pages
{
    public partial class add_new_employee : System.Web.UI.Page
    {
        int lvl = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
           Level();
            ManageCookies.VerifyAuthentication();
            if (lvl == 1)
            {
               if (!Page.IsPostBack)
            {
                DropDownDepartment();
                DropDownNationality();
                ddlNationality.SelectedValue = "Nigerian";
                ddlCountry.SelectedValue = "Nigeria";
               // ddlLGA.SelectedValue = "Ikeja";
                DropDownEthnicity();
                DropDownReligion();
                DropDownCountry();
                DropDownBloodGroup();
                DropDownCity();
                DropDownState();
            }
            }
            else if (lvl == 2)
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
        public void DropDownNationality()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_nationality_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlNationality.DataSource = ddlValues;
            ddlNationality.DataValueField = "nationality";
            ddlNationality.DataTextField = "nationality";
            ddlNationality.DataBind();
            //ddlNationality.Items.Insert(0, new ListItem("Please select Nationality", "NA"));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void DropDownState()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_nigerian_states_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlState.DataSource = ddlValues;
            ddlState.DataValueField = "state";
            ddlState.DataTextField = "state";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("Please select state", ""));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
        public void DropDownCity()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_nigeria_lagos_state_lga_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlLGA.DataSource = ddlValues;
            ddlLGA.DataValueField = "lga";
            ddlLGA.DataTextField = "lga";
            ddlLGA.DataBind();
            ddlLGA.Items.Insert(0, new ListItem("Please select LGA", ""));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
        public void DropDownCountry()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_countries_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlCountry.DataSource = ddlValues;
            ddlCountry.DataValueField = "country";
            ddlCountry.DataTextField = "country";
            ddlCountry.DataBind();
            //ddlNationality.Items.Insert(0, new ListItem("Please select Nationality", "NA"));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void DropDownBloodGroup()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_blood_group_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlBloodGroup.DataSource = ddlValues;
            ddlBloodGroup.DataValueField = "status_name";
            ddlBloodGroup.DataTextField = "status_name";
            ddlBloodGroup.DataBind();
            ddlBloodGroup.Items.Insert(0, new ListItem("Please select Blood Group", ""));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
        public void DropDownEthnicity()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_nigeria_ethnicity_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlEthnicity.DataSource = ddlValues;
            ddlEthnicity.DataValueField = "ethnicity";
            ddlEthnicity.DataTextField = "ethnicity";
            ddlEthnicity.DataBind();
            ddlEthnicity.Items.Insert(0, new ListItem("Please select Ethnicity", ""));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
        public void DropDownReligion()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_status_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@category", "religion");
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlReligion.DataSource = ddlValues;
            ddlReligion.DataValueField = "status_name";
            ddlReligion.DataTextField = "status_name";
            ddlReligion.DataBind();
            ddlReligion.Items.Insert(0, new ListItem("Please select Religion", ""));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
        public void DropDownDepartment()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_hr_department_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlDepartment.DataSource = ddlValues;
            ddlDepartment.DataValueField = "dept_name";
            ddlDepartment.DataTextField = "dept_name";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("Please select Department", ""));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_hr_employees_add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                cmd.Parameters.AddWithValue("@f_name", tbFName.Text.ToString());
                cmd.Parameters.AddWithValue("@m_name", tbMName.Text.ToString());
                cmd.Parameters.AddWithValue("@l_name", tbLName.Text.ToString());
                cmd.Parameters.AddWithValue("@title", ddlTitle.Text.ToString());
                cmd.Parameters.AddWithValue("@dob", DOB.Text.ToString());
                cmd.Parameters.AddWithValue("@gender", ddlGender.Text.ToString());
                cmd.Parameters.AddWithValue("@nationality", ddlNationality.Text.ToString());
                cmd.Parameters.AddWithValue("@ethnicity", ddlEthnicity.Text.ToString());
                cmd.Parameters.AddWithValue("@religion", ddlReligion.Text.ToString());
                cmd.Parameters.AddWithValue("@dept_name", ddlDepartment.Text.ToString());
                cmd.Parameters.AddWithValue("@hire_date", tbHiredDate.Text.ToString());
                cmd.Parameters.AddWithValue("@contact_no1", MobileTb.Text.ToString());
                cmd.Parameters.AddWithValue("@contact_no2", HomeNoTb.Text.ToString());
                cmd.Parameters.AddWithValue("@email_add", EmailTb.Text.ToString());
                cmd.Parameters.AddWithValue("@created_date", DBNull.Value);
                cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                //cmd.Parameters.AddWithValue("@updated_by", DBNull.Value);
                cmd.Parameters.AddWithValue("@start_date", DBNull.Value);
                cmd.Parameters.AddWithValue("@add_emp_id", DBNull.Value);
                cmd.Parameters.AddWithValue("@emp_dept_id", DBNull.Value);
                //cmd.Parameters.AddWithValue("@dept_id","1");
                cmd.Parameters.AddWithValue("@end_date", DBNull.Value);
                cmd.Parameters.AddWithValue("@next_of_kin", tbEmergencyContactName.Text.ToString());
                cmd.Parameters.AddWithValue("@next_of_kin_email_add", tbEmergencyEmail.Text.ToString());
                cmd.Parameters.AddWithValue("@next_of_kin_contact_no", tbEmergencyContactNumber.Text.ToString());
                cmd.Parameters.AddWithValue("@address_line1", TbAddressLine1.Text.ToString());
                cmd.Parameters.AddWithValue("@address_line2", TbAddressLine2.Text.ToString());
              
                cmd.Parameters.AddWithValue("@lga_city", ddlLGA.Text.ToString());
                cmd.Parameters.AddWithValue("@zip_postal_code", DBNull.Value);
                cmd.Parameters.AddWithValue("@country", ddlCountry.Text.ToString());
                cmd.Parameters.AddWithValue("@state ", ddlState.Text.ToString());
                cmd.Parameters.AddWithValue("@status", "Active");
                cmd.Parameters.AddWithValue("@blood_group", ddlBloodGroup.Text.ToString());
                cmd.Parameters.AddWithValue("@notes", tbNotes.Text.ToString());
                cmd.ExecuteNonQuery();
                GetId();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
                //ClearData(this);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('SUCESSFULLY RECORD SAVED');", true);
                //ClearTextBoxes(this);
                //Response.Redirect("~/pages/employees.aspx");
            }
        }


        public void GetId()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_employee_profile_latest", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                int EmployeeId = (Int32)cmd.ExecuteScalar();
                cmd.Dispose();
                Response.Redirect("~/pages/employee_profile.aspx?EmployeeId=" + EmployeeId,false);
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
    }
}