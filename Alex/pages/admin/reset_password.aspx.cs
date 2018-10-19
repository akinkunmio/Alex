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
    public partial class reset_password : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
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

        protected void btnSetupNewPassword_Click(object sender, EventArgs e)
        {
            string str = null;
            byte up =0;
            string Email = string.Empty;
            SqlCommand com;
            Email = Session["UserName"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                str = "select * from ms_login where email_add='" + Email + "'";
                com = new SqlCommand(str, con);
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    if (tbOldPassword.Text == reader["password"].ToString())
                    {
                        up = 1;
                    }
                }
                reader.Close();
                con.Close();
                if (up == 1)
                {
                    SqlCommand cmd = new SqlCommand("sp_ms_settings_login_user_change_password", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@email_add", Email);
                    cmd.Parameters.AddWithValue("@oldpassword", tbOldPassword.Text.ToString());
                    cmd.Parameters.AddWithValue("@newpassword", tbconfirmpassword.Text.ToString());
                    cmd.Parameters.AddWithValue("@newpassword2", tbconfirmpassword.Text.ToString());
                    cmd.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Password changed Succesfully');window.location = '/pages/dashboard.aspx';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please enter correct Current password');", true);
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
                ClearData(this);
            }
        }
    }
}