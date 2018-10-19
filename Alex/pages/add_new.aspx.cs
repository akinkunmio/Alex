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
    public partial class add_new : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
                if (!IsPostBack)
                 {

                     DropDownNationality();
                     ddlNationality.SelectedValue = "Nigerian";
                     ddlCountry.SelectedValue = "Nigeria";
                     //ddlLGA.SelectedValue = "Ikeja";
                     DropDownEthnicity();
                     DropDownReligion();
                     DropDownCountry();
                     DropDownCity();
                     DropDownGender();
                     DropDownTitle();
                     DropDownPGTitle();
                     DropDownRelation();
                     DropDownBloodGroup();
                     DropDownState();
                     ddlP_g_title.SelectedIndex = 3;
                     ddlLGA.SelectedValue = "Lagos Mainland";
                     ddlRelation.SelectedIndex = 1;
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

        public void DropDownTitle()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_status_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@category", "Title");
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlTitle.DataSource = ddlValues;
            ddlTitle.DataValueField = "status_name";
            ddlTitle.DataTextField = "status_name";
            ddlTitle.DataBind();
            ddlTitle.Items.Insert(0, new ListItem("Please select Title", ""));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
        public void DropDownPGTitle()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_status_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@category", "PG Title");
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlP_g_title.DataSource = ddlValues;
            ddlP_g_title.DataValueField = "status_name";
            ddlP_g_title.DataTextField = "status_name";
            ddlP_g_title.DataBind();
            ddlP_g_title.Items.Insert(0, new ListItem("Please select Title", ""));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void DropDownRelation()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_status_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@category", "PG Relationship");
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlRelation.DataSource = ddlValues;
            ddlRelation.DataValueField = "status_name";
            ddlRelation.DataTextField = "status_name";
            ddlRelation.DataBind();
            ddlRelation.Items.Insert(0, new ListItem("Please select Relationship", ""));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void DropDownGender()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_status_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@category", "Gender");
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlGender.DataSource = ddlValues;
            ddlGender.DataValueField = "status_name";
            ddlGender.DataTextField = "status_name";
            ddlGender.DataBind();
            ddlGender.Items.Insert(0, new ListItem("Please select Gender", ""));
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
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
             SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                //Label CreatedName = this.Master.FindControl("lblUserName") as Label;
                //<%@ MasterType VirtualPath="~/pages/master.Master" %>-- on aspx
                string UserName = HttpContext.Current.User.Identity.Name;
                SqlCommand cmd = new SqlCommand("sp_ms_person_add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
             
                cmd.Parameters.AddWithValue("@f_name",tbFName.Text.ToString());
                cmd.Parameters.AddWithValue("@m_name",tbMName.Text.ToString());
                cmd.Parameters.AddWithValue("@l_name",tbLName.Text.ToString());
                cmd.Parameters.AddWithValue("@title",ddlTitle.Text.ToString());
                cmd.Parameters.AddWithValue("@dob", DOB.Text.ToString());
                cmd.Parameters.AddWithValue("@gender",ddlGender.Text.ToString());
                cmd.Parameters.AddWithValue("@nationality", ddlNationality.Text.ToString());
                cmd.Parameters.AddWithValue("@ethnicity",ddlEthnicity.Text.ToString());
                cmd.Parameters.AddWithValue("@religion",ddlReligion.Text.ToString());
                cmd.Parameters.AddWithValue("@parent_guardian_fname", tbP_g_FName.Text.ToString());
                cmd.Parameters.AddWithValue("@parent_guardian_lname", tbP_g_LName.Text.ToString());
                cmd.Parameters.AddWithValue("@p_g_relationship",ddlRelation.Text.ToString());
                cmd.Parameters.AddWithValue("@p_g_title",ddlP_g_title.Text.ToString());
                cmd.Parameters.AddWithValue("@p_g_contact_no1",MobileTb.Text.ToString());
                cmd.Parameters.AddWithValue("@p_g_contact_no2",HomeNoTb.Text.ToString());
                cmd.Parameters.AddWithValue("@person_phone_no", tbStudentPhoneNo.Text.ToString());
                cmd.Parameters.AddWithValue("@p_g_email_add",EmailTb.Text.ToString());
                cmd.Parameters.AddWithValue("@updated_date",DBNull.Value);
                cmd.Parameters.AddWithValue("@created_by", UserName);
                cmd.Parameters.AddWithValue("@updated_by", DBNull.Value);
                cmd.Parameters.AddWithValue("@start_date", DBNull.Value);
                cmd.Parameters.AddWithValue("@add_person_id", DBNull.Value);
                cmd.Parameters.AddWithValue("@end_date", DBNull.Value);
                cmd.Parameters.AddWithValue("@emergency_contact_f_name", tbEmergencyContactFName.Text.ToString());
                cmd.Parameters.AddWithValue("@emergency_contact_l_name", tbEmergencyContactLName.Text.ToString());
                cmd.Parameters.AddWithValue("@emergency_contact_no", tbEmergencyContactNumber.Text.ToString());
                cmd.Parameters.AddWithValue("@address_line1", TbAddressLine1.Text.ToString());
                cmd.Parameters.AddWithValue("@address_line2", TbAddressLine2.Text.ToString());
                cmd.Parameters.AddWithValue("@address_line3", TbAddressLine3.Text.ToString());
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
                ClearData(this);
                //Response.Redirect("~/pages/profile.aspx?PersonId=" + ID, false);
            }
      }


        public void GetId()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_person_profile_latest", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                int StudentId = (Int32)cmd.ExecuteScalar();
                cmd.Dispose();
                Response.Redirect("~/pages/profile.aspx?PersonId=" + StudentId,false);
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

        protected void ddlTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            var value = ddlTitle.SelectedValue;
            if (value == "Master")
            {
                ddlGender.SelectedIndex = 1;
            }
            else if (value == "Miss")
            {
                ddlGender.SelectedIndex = 2;
            }
            else if (value == "Not Known")
            {
                ddlGender.SelectedIndex = 3;
            }
        }

              
   }
}