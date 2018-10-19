using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.SessionState;
using System.Net;


namespace Alex.pages
{
    public partial class login : System.Web.UI.Page
    {
        int loginId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            TbUserName.Focus();

            if (!IsPostBack)
            {
                // Clean up Session database                
                Session.Abandon();
                Session.Clear();
                Session.RemoveAll();
            }
        }
        //protected void BtnLogin_Click(object sender, EventArgs e)
        //{
        //    SqlDataAdapter adp = new SqlDataAdapter();
        //    bool isAuthenticated = false;
        //    int usercount = 0; int level_of_access = 0;
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        //    try
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("sp_ms_login_check_and_access_level", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@email_add", TbUserName.Text.Trim());
        //        cmd.Parameters.AddWithValue("@password", TbPassword.Text.Trim());
        //        //;
        //        cmd.Dispose();
        //        SqlDataReader dr = cmd.ExecuteReader();

        //        while (dr.Read())
        //        {
        //            usercount = (Int32)dr["count"];
        //            level_of_access = (Int32)dr["level_of_access"];
        //        }
        //        if (dr != null)
        //        {
        //            isAuthenticated = true;
        //        }
        //        //}
        //        if (isAuthenticated)
        //        {
        //            //List<string> roles = new List<string>();
        //            string roles = string.Empty;
        //            string UserName = TbUserName.Text.Trim();
        //            //UserName = "gophani@gmail.com";
        //            if (UserName.Equals("gophani@gmail.com")) // I am setting the role explicitely for this user
        //            {
        //                roles = "Admin,Moderator";
        //            }

        //            roles = roles + ",UserManagement";

        //            UserName = TbUserName.Text.Trim();
        //            // Create forms authentication ticket
        //            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
        //            1, // Ticket version
        //            UserName, // Username to be associated with this ticket
        //            DateTime.Now, // Date/time ticket was issued
        //            DateTime.Now.AddMinutes(20), // Date and time the cookie will expire
        //            true, // if user has chcked rememebr me then create persistent cookie
        //            roles.ToString(), // store the user data, in this case roles of the user
        //            FormsAuthentication.FormsCookiePath); // Cookie path specified in the web.config file in <Forms> tag if any.
        //            // To give more security it is suggested to hash it
        //            string hashCookies = FormsAuthentication.Encrypt(ticket);
        //            //HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashCookies); // Hashed ticket
        //            HttpCookie cookie = new HttpCookie("loginCookie", hashCookies); // Hashed ticket
        //            // Add the cookie to the response 
        //            Response.Cookies.Add(cookie);
        //            // Get the requested page from the url
        //            //string returnUrl = Request.QueryString["ReturnUrl"];
        //            // check if it exists, if not then redirect to default page specified in the web.config file
        //            //if (returnUrl == null) returnUrl = FormsAuthentication.DefaultUrl;
        //            //Response.Redirect(returnUrl);
        //            if ((usercount > 0) && (level_of_access == 1))
        //            {
        //                Response.Redirect("~/pages/dashboard.aspx", false);
        //            }
        //            else if ((usercount > 0) && (level_of_access == 2))
        //            {
        //                Response.Redirect("~/pages/dash_board.aspx", false);
        //            }
        //        }

        //        //isAuthenticated = true;
        //        //FormsAuthentication.SetAuthCookie(TbUserName.Text.Trim(), true);
        //        //Session.Add("UserName", TbUserName.Text.Trim().ToString());
        //        //string a = Session["UserName"].ToString();
        //        //HttpCookie loginCookie = new HttpCookie("loginCookie");
        //        //loginCookie.Value = "UserManagement";
        //        //loginCookie.Expires = DateTime.Now.AddHours(6);
        //        //Response.Cookies.Add(loginCookie);

        //        // }
        //        //else if ((usercount > 0) && (level_of_access == 2))
        //        //{
        //        //    FormsAuthentication.SetAuthCookie(TbUserName.Text.Trim(), true);
        //        //    Session.Add("UserName", TbUserName.Text.Trim().ToString());
        //        //    string a = Session["UserName"].ToString();
        //        //    HttpCookie loginCookie = new HttpCookie("loginCookie");
        //        //    loginCookie.Value = "UserManagement";
        //        //    loginCookie.Expires = DateTime.Now.AddHours(6);
        //        //    Response.Cookies.Add(loginCookie);

        //        //    Response.Redirect("~/pages/dash_board.aspx", false);
        //        //}
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Wrong Username/Password');", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //        // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }

        //}
        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adp = new SqlDataAdapter();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_login_check_and_access_level_version1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email_add", TbUserName.Text.Trim());
                cmd.Parameters.AddWithValue("@password", TbPassword.Text.Trim());
                cmd.Dispose();

                //int usercount = (Int32)cmd.ExecuteScalar();
                //int  level_of_access = (Int32)cmd.ExecuteScalar();



                SqlDataReader dr = cmd.ExecuteReader();
                int usercount = 0;
                int levelAccess = 0;

                string AdminChangePassword = string.Empty;
                string PasswordExpired = string.Empty;
                string Pwdnotify = string.Empty;
                string Wiz = string.Empty;
                int NoOfAttemps = 0;

                while (dr.Read())
                {
                    usercount = (Int32)dr["count"];
                    levelAccess = (Int32)dr["level_of_access"];

                    AdminChangePassword = dr["first_time_login"].ToString();
                    PasswordExpired = dr["password_expired"].ToString();

                    Wiz = dr["wizard"].ToString();
                    //int level = (int)(Session["level_of_access"]);
                    //Session["level_of_access"] = level; 
                    Session["level_of_access"] = levelAccess;
                    Session["wizard"] = Wiz;
                    loginId = (Int32)dr["id"];
                    NoOfAttemps = (Int32)dr["no_of_attempts"];
                    Pwdnotify = dr["Password_Expired_notify"].ToString();
                }


                if ((usercount > 0) && (levelAccess == 1) && (AdminChangePassword == "no") && (PasswordExpired == "no"))
                {
                    FormsAuthentication.SetAuthCookie(TbUserName.Text.Trim(), true);
                    Session.Add("UserName", TbUserName.Text.Trim().ToString());
                    string a = Session["UserName"].ToString();
                    HttpCookie loginCookie = new HttpCookie("loginCookie");
                    loginCookie.Value = "UserManagement";
                    loginCookie.Expires = DateTime.Now.AddHours(6);
                    Response.Cookies.Add(loginCookie);
                    loginHistory();
                    if (Wiz == "yes")
                    {
                        Response.Redirect("~/pages/wizard.aspx", false);
                    }
                    else if (Pwdnotify == "yes")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Password about to expire, Please update your password');window.location = '/pages/admin/reset_password.aspx';", true); 
                    }
                    else
                    {
                        Response.Redirect("~/pages/dashboard.aspx", false);
                    }

                }
                else if ((usercount > 0) && (levelAccess == 2) && (AdminChangePassword == "no") && (PasswordExpired == "no"))
                {
                    FormsAuthentication.SetAuthCookie(TbUserName.Text.Trim(), true);
                    Session.Add("UserName", TbUserName.Text.Trim().ToString());
                    string a = Session["UserName"].ToString();
                    HttpCookie loginCookie = new HttpCookie("loginCookie");
                    loginCookie.Value = "UserManagement";
                    loginCookie.Expires = DateTime.Now.AddHours(6);
                    Response.Cookies.Add(loginCookie);
                    loginHistory();
                    if (Pwdnotify == "yes")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Password about to expire, Please update your password');window.location = '/pages/admin/reset_password.aspx';", true);
                    }
                    else
                    {
                        Response.Redirect("~/pages/dashboard2.aspx", false);
                    }
                }

                else if ((usercount > 0) && ((levelAccess == 3) || (levelAccess == 4) || (levelAccess == 5)) && (AdminChangePassword == "no") && (PasswordExpired == "no"))
                {
                    FormsAuthentication.SetAuthCookie(TbUserName.Text.Trim(), true);
                    Session.Add("UserName", TbUserName.Text.Trim().ToString());
                    string a = Session["UserName"].ToString();
                    HttpCookie loginCookie = new HttpCookie("loginCookie");
                    loginCookie.Value = "UserManagement";
                    loginCookie.Expires = DateTime.Now.AddHours(6);
                    Response.Cookies.Add(loginCookie);
                    loginHistory();
                    if (Pwdnotify == "yes")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Password about to expire, Please update your password');window.location = '/pages/admin/reset_password.aspx';", true);
                    }
                    else
                    {
                        Response.Redirect("~/pages/dashboard2.aspx", false);
                    }
                }

                else if ((usercount > 0) && (levelAccess == 1) && (AdminChangePassword == "yes") && (PasswordExpired == "no"))
                {
                    FormsAuthentication.SetAuthCookie(TbUserName.Text.Trim(), true);
                    Session.Add("UserName", TbUserName.Text.Trim().ToString());
                    string a = Session["UserName"].ToString();
                    HttpCookie loginCookie = new HttpCookie("loginCookie");
                    loginCookie.Value = "UserManagement";
                    loginCookie.Expires = DateTime.Now.AddHours(6);
                    Response.Cookies.Add(loginCookie);
                    loginHistory();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Welcome back to iQ, Please reset your password');window.location = '/pages/reset_password.aspx';", true);
                    //Response.Redirect("~/pages/admin/reset_password.aspx", false);

                }

                else if ((usercount > 0) && (levelAccess == 2) && (AdminChangePassword == "yes") && (PasswordExpired == "no"))
                {
                    FormsAuthentication.SetAuthCookie(TbUserName.Text.Trim(), true);
                    Session.Add("UserName", TbUserName.Text.Trim().ToString());
                    string a = Session["UserName"].ToString();
                    HttpCookie loginCookie = new HttpCookie("loginCookie");
                    loginCookie.Value = "UserManagement";
                    loginCookie.Expires = DateTime.Now.AddHours(6);
                    Response.Cookies.Add(loginCookie);
                    loginHistory();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Welcome back to iQ, Please reset your password');window.location ='/pages/reset_password.aspx';", true);
                    //Response.Redirect("~/pages/admin/reset_password.aspx", false);

                }

                else if ((usercount > 0) && ((levelAccess == 3) || (levelAccess == 4) || (levelAccess == 5)) && (AdminChangePassword == "yes") && (PasswordExpired == "no"))
                {
                    FormsAuthentication.SetAuthCookie(TbUserName.Text.Trim(), true);
                    Session.Add("UserName", TbUserName.Text.Trim().ToString());
                    string a = Session["UserName"].ToString();
                    HttpCookie loginCookie = new HttpCookie("loginCookie");
                    loginCookie.Value = "UserManagement";
                    loginCookie.Expires = DateTime.Now.AddHours(6);
                    Response.Cookies.Add(loginCookie);
                    loginHistory();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Welcome back to iQ, Please reset your password');window.location ='/pages/reset_password.aspx';", true);
                    //Response.Redirect("~/pages/admin/reset_password.aspx", false);

                }
                else if ((usercount > 0) && (levelAccess == 1) && (AdminChangePassword == "no") && (PasswordExpired == "yes"))
                {
                    Session.Clear();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Password Expired Contact Administrator');", true);
                }

                else if ((usercount > 0) && (levelAccess == 2) && (AdminChangePassword == "no") && (PasswordExpired == "yes"))
                {
                    Session.Clear();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Password Expired Contact Administrator');", true);
                }
                else if ((usercount > 0) && ((levelAccess == 3) || (levelAccess == 4) || (levelAccess == 5)) && (AdminChangePassword == "no") && (PasswordExpired == "yes"))
                {
                    Session.Clear();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Password Expired Contact Administrator');", true);
                }
                else if ((usercount > 0) && (levelAccess == 0) && (AdminChangePassword == "no") && (PasswordExpired == "no"))
                {
                    Session.Clear();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Account Disabled Contact Admin');", true);
                }
                else
                {
                    //lblStatus.Text = "Wrong Username/Password";
                    //Or show in messagebox using
                    Session.Clear();
                    if (NoOfAttemps == 4)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Account Disabled for Security Purpose,Contact Administrator');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Wrong Username/Password');", true);
                    }
                       // ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Wrong Username/Password');", true);

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

        private void loginHistory()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                string IPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (IPAddress == "" || IPAddress == null)
                    IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_login_history_session_on", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@login_id", loginId);
                cmd.Parameters.AddWithValue("@email_add", TbUserName.Text);
                cmd.Parameters.AddWithValue("@ip_address", IPAddress);
                cmd.ExecuteNonQuery();
                loginHistorySessionId();
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

        private void loginHistorySessionId()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select max (log_hist_id) as history_id from ms_login_history", con);
                cmd.CommandType = CommandType.Text;
                // cmd.ExecuteNonQuery();
                cmd.Dispose();
                int HistoryId = (Int32)cmd.ExecuteScalar();
                Session["history_id"] = HistoryId;
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


