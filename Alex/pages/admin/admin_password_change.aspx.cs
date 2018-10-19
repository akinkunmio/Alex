using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alex.App_code;

namespace Alex.pages.admin
{
    public partial class admin_password_change : System.Web.UI.Page
    {
        string Email = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
           //if (!IsPostBack)
           // {
                Email = Request.QueryString["Email"].ToString();

                lblEmail.Text = Email;
            //}
           
        }

        protected void btnSetupPassword_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_settings_login_admin_change_password", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@email_add", SqlDbType.NVarChar).Value = lblEmail.Text;
                    cmd.Parameters.Add("@newpassword1", SqlDbType.VarChar).Value = tbPass.Text.ToString();
                    cmd.Parameters.Add("@newpassword2", SqlDbType.VarChar).Value = tbPass2.Text.ToString();
                    cmd.ExecuteNonQuery();
                   // ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Password changed Succesfully');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Password changed Succesfully');window.location = '/pages/admin/manage_users.aspx';", true);

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);

            }
            finally
            {
              con.Close();
              //Response.Redirect("~/pages/admin/manage_users.aspx");
             }
        }
    }
}