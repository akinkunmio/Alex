using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.SessionState;
using Alex.App_code;

namespace Alex.pages
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            { loginHistoryOFF(); }
            
            if (Request.Cookies["loginCookie"] != null)
            {
                ManageCookies.VerifyAuthentication(true);
            }
            else
            {
                Response.Redirect("~/pages/login.aspx", false);
            }
           
        }

       
        private void loginHistoryOFF()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                int history_id = (int)(Session["history_id"]);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_login_history_session_off", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@session_id", DBNull.Value);
                cmd.Parameters.AddWithValue("@log_hist_id", history_id);
                cmd.ExecuteNonQuery();
                
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