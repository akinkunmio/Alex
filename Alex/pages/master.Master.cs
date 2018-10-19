using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.SessionState;

namespace Alex.pages
{
    public partial class master : System.Web.UI.MasterPage
    {
        int lvl = 0;
        //string UserName = "Admin";//string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Refresh", Convert.ToString(Session.Timeout * 60) + "; URL= ../../pages/logout.aspx");
            if (!IsPostBack)
            {
                Level();
                SchoolBind();
                GetUserName();
                Version();
                imgSchool.ImageUrl = "ImageHandler.ashx?roll_no=1";
                if (lvl == 1 || lvl ==2)
                {
                    DashboardPage.Visible = true;
                    PeoplesPage.Visible = true;
                    AddmissionPage.Visible = true;
                    RegistrationPage.Visible = true;
                    PaymentsBatch.Visible = true;
                    PaymentsPage.Visible = true;
                    PaymentsFee.Visible = true;
                    PaymentsItem.Visible = true;
                    PaymentsVerify.Visible = true;
                    PaymentsExpenses.Visible = true;
                    AssesmentsPage.Visible = true;
                    employeePage.Visible = true;
                    hrPage.Visible = true;
                    ReportsPage.Visible = true;
                    SmsPage.Visible = true;
                    SettingsPage.Visible = true;
                    Settingslink.Visible = true;
                }
                else if (lvl == 3)
                {
                    DashboardPage.Visible = true; 
                    PeoplesPage.Visible = true;
                    AddmissionPage.Visible = true;
                    RegistrationPage.Visible = true;
                    PaymentsBatch.Visible = true;
                    PaymentsPage.Visible = true;
                    PaymentsFee.Visible = false;
                    PaymentsItem.Visible = false;
                    PaymentsVerify.Visible = false;
                    PaymentsExpenses.Visible = true;
                    AssesmentsPage.Visible = true;
                    employeePage.Visible = false;
                    hrPage.Visible = false;
                    ReportsPage.Visible = true;
                    SmsPage.Visible = false;
                    SettingsPage.Visible = true;
                    Settingslink.Visible = true;
                }
                else if (lvl == 4)
                {
                    DashboardPage.Visible = false;
                    PeoplesPage.Visible = false;
                    AddmissionPage.Visible = false;
                    RegistrationPage.Visible = false;
                    PaymentsBatch.Visible = false;
                    PaymentsPage.Visible = false;
                    PaymentsFee.Visible = false;
                    PaymentsItem.Visible = false;
                    PaymentsVerify.Visible = false;
                    PaymentsExpenses.Visible = false;
                    AssesmentsPage.Visible = true;
                    employeePage.Visible = false;
                    hrPage.Visible = false;
                    ReportsPage.Visible = false;
                    SmsPage.Visible = false;
                    SettingsPage.Visible = false;
                    Settingslink.Visible = false;
                }
                else if (lvl == 5)
                {
                    DashboardPage.Visible = false; 
                    PeoplesPage.Visible = false;
                    AddmissionPage.Visible = false;
                    RegistrationPage.Visible = false;
                    PaymentsBatch.Visible = false;
                    PaymentsPage.Visible = false;
                    PaymentsFee.Visible = false;
                    PaymentsItem.Visible = false;
                    PaymentsVerify.Visible = false;
                    PaymentsExpenses.Visible = false;
                    AssesmentsPage.Visible = false;
                    employeePage.Visible = false;
                    hrPage.Visible = false;
                    ReportsPage.Visible = false;
                    SmsPage.Visible = false;
                    SettingsPage.Visible = false;
                    Settingslink.Visible = false;
                }
              else
                {
                    Response.Redirect("~/pages/logout.aspx", false);//Response.Redirect("~/pages/ForbiddenAccess.aspx", false); 
                }
            }
        }

        public void Level()
        {
            try
            {
                //lblUserName.Text = 

                lvl = (int)(Session["level_of_access"]);

                //lbllvl.Text =lvl.ToString();

            }
            catch //(Exception ex)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
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
                    lblSchoolName.Text = SchoolName.ToString();
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

       

        public void GetUserName()
        {
            string UserName = HttpContext.Current.User.Identity.Name;
           // string UserName = Session["UserName"].ToString();
            if (UserName != null)
            {
                string[] Myinfo = UserName.Split('@');
                string LUserName = Myinfo[0].ToString().ToLower();
                lblUserName.Text = UppercaseFirst(LUserName); //Myinfo[0].ToString();//UserName;
            }
            else
            {
                LblLoginUserName1.Text = "Guest";
            }
            // ****USE THIS FOR RENEW USER*******
            //string UserName = HttpContext.Current.User.Identity.Name;
            //if (UserName != null)
            //{
            //    String info = RenewUser(UserName);
            //    string[] Myinfo = UserName.Split('@');
            //    //lblUserName.Text = Myinfo[0].ToString();
            //    string LUserName = Myinfo[0].ToString().ToLower();
            //    lblUserName.Text = UppercaseFirst(LUserName); //Myinfo[0].ToString();//UserName;
            //    if (info != null)
            //    {
            //        lblAccountExpired.Text = "Your password at to Expried with in 3 months";
            //    }
            //}
            //else
            //{
            //    lblUserName.Text = "Guest";
            //}
        }

        static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        private void Version()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_version", con))
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
                             if (dt.Rows.Count != 0)
                                {
                                  lblVersion.Text = dt.Rows[0]["Version"].ToString();
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

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            string Search = TbSearch.Text;
            Session["SearchText"] = Search; // (, "Search");
            string aaa = (string)(Session["SearchText"]);
            Response.Redirect("~/pages/people.aspx", false);
        }

        protected void btnRenewYourAccount_Click(object sender, EventArgs e)
        {
            //"select id,email_add from ms_login where password_expiry_change_date <= CONVERT (DATE, GETDATE())";

        }
        protected string RenewUser(string UserName)
        {
            SqlDataAdapter adp = new SqlDataAdapter();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            
                con.Open();
                
                string id = string.Empty;
                //Renew User 
                SqlCommand cmd1 = new SqlCommand("demotest1", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@email_add", UserName);
                //   cmd1.Parameters.AddWithValue("@password", TbPassword.Text.Trim());
                cmd1.Dispose();
                SqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    id = dr1["id"].ToString();
                }
                dr1.Close();
                return id;
            //}


        }
    }
}