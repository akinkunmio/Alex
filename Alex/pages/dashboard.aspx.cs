using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.DataVisualization.Charting;
using Alex.App_code;

//using Alex.Demo;

namespace Alex.pages
{
    public partial class dashboard : System.Web.UI.Page
    {
        int lvl = 0;
        string WizValue;
        string DashboardMK = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
           ManageCookies.VerifyAuthentication();
            //remove down this after authentication
            Level();
            if ( (lvl == 1) && (WizValue== "N"))
            {
            SchoolTimeline();
            MillionThousand();
            RecentApps();
            RecentRegis();
            RecentPays();
            StudentsBirthdays();
            EmployeesBirthdays();
            if (DashboardMK == "M") { chartRev.ChartAreas["ChartArea1"].AxisY.LabelStyle.Format = "0,,.0M"; }
            else { chartRev.ChartAreas["ChartArea1"].AxisY.LabelStyle.Format = "{0:0,}K"; }
            Chart1.Series[0].ChartType = SeriesChartType.Funnel;
              //Chart1.ChartAreas["ChartAreaPie"].Area3DStyle.Enable3D = true;
            Chart1.Legends[0].Enabled = true;
            }
            else if ( (lvl == 1) && (WizValue== "Y"))
            {
                Response.Redirect("~/pages/wizard.aspx", false);
            }
            else if ((lvl == 2) || (lvl == 3))
            
            {
              Response.Redirect("~/pages/dashboard2.aspx", false);
            }

            else
            {
                Response.Redirect("~/pages/students.aspx", false);
            }
        }

        public void Level()
        {
         SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
          try
           {
              lvl = (int)(Session["level_of_access"]);
              con.Open();
              SqlCommand cmd = new SqlCommand("select wizard from [dbo].[ms_login] where id = 1", con);
              cmd.CommandType = CommandType.Text;
              cmd.Dispose();
              SqlDataReader dr = cmd.ExecuteReader();
              while (dr.Read())
              { WizValue = dr["wizard"].ToString(); }
              //WizValue = (string)(Session["wizard"]);
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
        protected void SchoolTimeline()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_dashboard_event_calendar_active_year_term", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                repEvents.DataSource = dt;
                repEvents.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('SUCESSFULLY RECORD SAVED');", true);
                //Response.Write("~/pages/admin/setup_fee.aspx");  
            }
        }

        protected void StudentsBirthdays()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_person_students_upcoming_birthdays", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                repStudentsBirthdays.DataSource = dt;
                repStudentsBirthdays.DataBind();
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

        protected void EmployeesBirthdays()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_hr_employees_upcoming_birthdays", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                repEmployeesBirthday.DataSource = dt;
                repEmployeesBirthday.DataBind();
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
        protected void RecentApps()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_dashboard_application_list_all_last5", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                GridViewApplications.DataSource = dt;
                GridViewApplications.DataBind();
               
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('SUCESSFULLY RECORD SAVED');", true);
                //Response.Write("~/pages/admin/setup_fee.aspx");  
            }
        }


        protected void RecentRegis()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_dashboard_registrations_last5", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                GridViewRegistrations.DataSource = dt;
                GridViewRegistrations.DataBind();
               
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('SUCESSFULLY RECORD SAVED');", true);
                //Response.Write("~/pages/admin/setup_fee.aspx");  
            }
        }

        protected void RecentPays()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_dashboard_payments_list_all_last5", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                GridViewPayments.DataSource = dt;
                GridViewPayments.DataBind();
               }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('SUCESSFULLY RECORD SAVED');", true);
                //Response.Write("~/pages/admin/setup_fee.aspx");  
            }
        }

        public void MillionThousand()
        {
            SqlDataAdapter adp = new SqlDataAdapter();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_dashboard_MillionThousand", con);
                cmd.Dispose();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DashboardMK = dr["MillionThousand"].ToString();
                    //lblName.Text = SchoolName.ToString();
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

       

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            string Search = TbSearch.Text;
            Session["SearchText"] = Search; // (, "Search");
            string aaa = (string)(Session["SearchText"]);
            Response.Redirect("people.aspx");
            
        }

    }
}