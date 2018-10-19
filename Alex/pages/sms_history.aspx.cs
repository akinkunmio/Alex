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
using System.Drawing;
using System.IO;


namespace Alex.pages.student_reports
{
    public partial class sms_history : System.Web.UI.Page
    {
        int lvl = 0;
        double total = 0;
        double Btotal = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Level();
            ManageCookies.VerifyAuthentication();
            //Joe 230118 2025 Enabled Send SMS for Level 2 
            //if (lvl == 1)
            if (lvl == 1 || lvl == 2)
            {
                if (!Page.IsPostBack)
                {
                    SchoolBind();

                    DateTime today = DateTime.Today;
                    DateTime TwentyEightDaysEarlier = today.AddDays(-28);
                    string TodayDate = DateTime.Today.ToString("dd-MM-yyyy");
                    tbStartDate.Text = TwentyEightDaysEarlier.ToString("dd-MM-yyyy");
                    tbEndDate.Text = TodayDate;
                    SmsHistoryBind();
                }
            }

            else
            {
                Response.Redirect("~/pages/ForbiddenAccess.aspx", false);
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


        protected void BtnSearchSmsHistory_Click(object sender, EventArgs e)
        {
            SmsHistoryBind();
        }



        private void SmsHistoryBind()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_sms_history", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@received_date_start", SqlDbType.VarChar).Value = tbStartDate.Text.ToString();
                    cmd.Parameters.Add("@received_date_end", SqlDbType.VarChar).Value = tbEndDate.Text.ToString();
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {

                            sda.Fill(dt);
                            GridViewSmsHistory.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                DivStudents.Visible = false;
                                lblZeroRecords.Visible = true;
                                lblZeroRecords.Text = "No SMS Found ";
                                GridViewSmsHistory.Visible = false;
                                btnPrint.Visible = false;
                            }
                            else
                            {
                                DivStudents.Visible = true;
                                imgSchool.ImageUrl = "~/pages/ImageHandler.ashx?roll_no=1";
                                lblDateSelected.Text = tbStartDate.Text.ToString();
                                lblEndDateSelected.Text = tbEndDate.Text.ToString();
                                lblZeroRecords.Visible = false;
                                GridViewSmsHistory.DataBind();
                                GridViewSmsHistory.Visible = true;
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
                SqlCommand cmd = new SqlCommand("sp_ms_settings_school_detail", con);
                cmd.Dispose();
                SqlDataReader dr = cmd.ExecuteReader();
                string SchoolName = string.Empty;
                while (dr.Read())
                {
                    SchoolName = dr["school_name"].ToString();
                    string Address = dr["address_line1"].ToString();
                    string Address2 = dr["address_line2"].ToString();
                    string City = dr["lga_city"].ToString();
                    string State = dr["state"].ToString();
                    string country = dr["country"].ToString();
                    string Postcode = dr["zip_postal_code"].ToString();
                    string Email = dr["contact_email"].ToString();
                    string PhoneNo = dr["contact_no1"].ToString();

                    lblSchoolName.Text = SchoolName.ToString();
                    lblAddress.Text = Address.ToString() + Address2.ToString();
                    lblCity.Text = City.ToString();
                    lblState.Text = State.ToString();
                    lblCountry.Text = country.ToString();
                    lblPostCode.Text = Postcode.ToString();
                    lblEmail.Text = Email.ToString();
                    lblPhoneNo.Text = PhoneNo.ToString();

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

        private double ParseDouble(string value)
        {
            double d = 0;
            if (!double.TryParse(value, out d))
            {
                return 0;
            }
            return d;
        }
        protected void GridViewSmsHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = GridViewSmsHistory.SelectedRow;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblB = (Label)e.Row.FindControl("lblBalance");
                var Balance = ParseDouble(lblB.Text);
                total = total + Balance;

                Label lblBB = (Label)e.Row.FindControl("lblBBalance");
                var BBalance = ParseDouble(lblBB.Text);
                Btotal = Btotal + BBalance;
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalBalance = (Label)e.Row.FindControl("lblTotalBalance");
                Label lblTotalBBalance = (Label)e.Row.FindControl("lblTotalBBalance");
                if (total > 0)
                {
                    lblTotalBalance.ForeColor = Color.Red;

                }
                lblTotalBalance.Text = "₦" + String.Format("{0:n}", total);
                lblTotalBBalance.Text = String.Format("{0:n}", Btotal);



            }

        }


    }
}