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


namespace Alex.pages.admin
{
    public partial class setup_new_user : System.Web.UI.Page
    {
        private string password = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            if (!Page.IsPostBack)
            {
                DropDownLevel();
                ddlLevelOfAccess.Items.Remove(ddlLevelOfAccess.Items.FindByText("0"));
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

        public void DropDownLevel()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_level_of_access_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();
            ddlLevelOfAccess.DataSource = ddlValues;
            ddlLevelOfAccess.DataValueField = "status_name";
            ddlLevelOfAccess.DataTextField = "status_name";
            ddlLevelOfAccess.DataBind();
            ddlLevelOfAccess.Items.Insert(0, new ListItem("Please select a level", ""));


            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
        protected void BtnCancelSetupForm_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/settings.aspx");
        }

        private void SetupNewUser()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_settings_login_add_version2", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                cmd.Parameters.AddWithValue("@f_name", tbSetupUserFName.Text.ToString());

                cmd.Parameters.AddWithValue("@l_name", tbSetupUserLName.Text.ToString());
                cmd.Parameters.AddWithValue("@email_add", tbSetupUserEmail.Text.ToString());
                //cmd.Parameters.AddWithValue("@dob",DOB.Text.ToString());
                cmd.Parameters.AddWithValue("@level_of_access", ddlLevelOfAccess.SelectedValue.ToString());
                //cmd.Parameters.AddWithValue("@position", ddlSetupUserPosition.Text.ToString());
                cmd.Parameters.AddWithValue("@created_by", "null");
                cmd.Parameters.Add("@password", SqlDbType.NChar, 8);
                cmd.Parameters["@password"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
               
                password = "Password for this user is : " + cmd.Parameters["@password"].Value;
               
                lblPassword.Text = password;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('User Created successfully ');", true);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("unique_mslogin_email_add"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Email exists already, Please see in Manage Users.')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                    // Response.Write("Oops!! following error occured: " +ex.Message.ToString());       
                }
            }
            finally
            {
                con.Close();
                ClearData(this);
               
                //Response.Redirect("~/pages/admin/manage_users.aspx");
               
            }
              
        }

        protected void btnCreateSetupUser_Click(object sender, EventArgs e)
        {
            SetupNewUser();
            //lblPassword.Visible = true;
        } 
    }
}