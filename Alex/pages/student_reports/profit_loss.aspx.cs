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

namespace Alex.pages.student_reports
{
    public partial class profit_loss : System.Web.UI.Page
    {
        int lvl = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Level();
            if (lvl == 1)
            {
                if (!Page.IsPostBack)
                {
                    SchoolBind();
                    //var now = DateTime.Now;
                    //var FirstDayofYear = new DateTime(now.Year, 1, 1);
                    //tbStartDate.Text = FirstDayofYear.ToString("dd/MM/yyyy");
                    //tbEndDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    DateTime today = DateTime.Today;
                    DateTime TwentyEightDaysEarlier = today.AddDays(-28);
                    string TodayDate = DateTime.Today.ToString("dd-MM-yyyy");
                    tbStartDate.Text = TwentyEightDaysEarlier.ToString("dd-MM-yyyy");
                    tbEndDate.Text = TodayDate;
                    TurnOver();
                    TurnOverBreakDown();
                    ExpensesTotal();
                    ExpensesBreakDown();
                    ProfitLossTotal();
                    StaffCostTotal();
                    StaffCostBreakDown();
                }
            }
            else if (lvl == 2 || lvl ==3 || lvl ==4)
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
        private void TurnOver()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_profit_loss_turnover_total", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@date_start", SqlDbType.VarChar).Value = tbStartDate.Text.ToString();
                        cmd.Parameters.Add("@date_end", SqlDbType.VarChar).Value = tbEndDate.Text.ToString();
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                GridViewTrunOverSummary.DataSource = dt;
                                if (dt.Rows.Count == 0)
                                {
                                    //lblTurnover.Text = "NOT AVAILABLE";
                                    GridViewTrunOverSummary.Visible = false;
                                    btnPrint.Visible = false;
                                }
                                else
                                {
                                    //lblTurnover.Text = dt.Rows[0]["turnover"].ToString();
                                    GridViewTrunOverSummary.DataBind();
                                    GridViewTrunOverSummary.Visible = true;
                                    imgSchool.ImageUrl = "~/pages/ImageHandler.ashx?roll_no=1";
                                    lblDateSelected.Text = tbStartDate.Text.ToString();
                                    lblEndDateSelected.Text = tbEndDate.Text.ToString();
                                    btnPrint.Visible = true;
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

        private void TurnOverBreakDown()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_profit_loss_turnover_breakdown", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@date_start", SqlDbType.VarChar).Value = tbStartDate.Text.ToString();
                        cmd.Parameters.Add("@date_end", SqlDbType.VarChar).Value = tbEndDate.Text.ToString();
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                GridViewTrunOver.DataSource = dt;
                                if (dt.Rows.Count == 0)
                                {
                                    GridViewTrunOver.Visible = false;
                                    lblZeroRecords.Visible = true;
                                    lblZeroRecords.Text = "No Expenses Found ";

                                }
                                else
                                {
                                    GridViewTrunOver.DataBind();
                                    GridViewTrunOver.Visible = true;
                                    lblZeroRecords.Visible = false;
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

        private void ExpensesTotal()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_profit_loss_expenses_total", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@date_start", SqlDbType.VarChar).Value = tbStartDate.Text.ToString();
                        cmd.Parameters.Add("@date_end", SqlDbType.VarChar).Value = tbEndDate.Text.ToString();
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
               
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                GridViewExpensesSummary.DataSource = dt;
                                if (dt.Rows.Count == 0)
                                {
                                    GridViewExpensesSummary.Visible = false;
                                }
                                else
                                {
                                    GridViewExpensesSummary.DataBind();
                                    GridViewExpensesSummary.Visible = true;
                                   
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

        private void ExpensesBreakDown()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_profit_loss_expenses_breakdown", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@date_start", SqlDbType.VarChar).Value = tbStartDate.Text.ToString();
                        cmd.Parameters.Add("@date_end", SqlDbType.VarChar).Value = tbEndDate.Text.ToString();
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                GridViewExpenses.DataSource = dt;
                                if (dt.Rows.Count == 0)
                                {
                                    GridViewExpenses.Visible = false;
                                    lblZeroRecords.Visible = true;
                                    lblZeroRecords.Text = "No Expenses Found ";
                                   
                                }
                                else
                                {
                                    GridViewExpenses.DataBind();
                                    GridViewExpenses.Visible = true;
                                    lblZeroRecords.Visible = false;
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

        private void StaffCostTotal()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_profit_loss_staff_cost_total", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@date_start", SqlDbType.VarChar).Value = tbStartDate.Text.ToString();
                        cmd.Parameters.Add("@date_end", SqlDbType.VarChar).Value = tbEndDate.Text.ToString();
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                GridViewStaffCostTotal.DataSource = dt;
                                if (dt.Rows.Count == 0)
                                {
                                    GridViewStaffCostTotal.Visible = false;
                                    //lblZeroRecords.Visible = true;
                                    // lblZeroRecords.Text = "No Expenses Found ";

                                }
                                else
                                {
                                    GridViewStaffCostTotal.DataBind();
                                    GridViewStaffCostTotal.Visible = true;
                                    //lblZeroRecords.Visible = false;
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
        private void StaffCostBreakDown()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_profit_loss_staff_cost_breakdown", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@date_start", SqlDbType.VarChar).Value = tbStartDate.Text.ToString();
                        cmd.Parameters.Add("@date_end", SqlDbType.VarChar).Value = tbEndDate.Text.ToString();
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                GridViewStaffCost.DataSource = dt;
                                if (dt.Rows.Count == 0)
                                {
                                    GridViewStaffCost.Visible = false;
                                    lblZeroRecords.Visible = true;
                                   // lblZeroRecords.Text = "No Expenses Found ";

                                }
                                else
                                {
                                    GridViewStaffCost.DataBind();
                                    GridViewStaffCost.Visible = true;
                                    //lblZeroRecords.Visible = false;
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

        private void ProfitLossTotal()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_profit_loss_total", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@date_start", SqlDbType.VarChar).Value = tbStartDate.Text.ToString();
                        cmd.Parameters.Add("@date_end", SqlDbType.VarChar).Value = tbEndDate.Text.ToString();
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                if (dt.Rows.Count == 0)
                                {
                                    lblProfit.Text = "NOT AVAILABLE";
                                }
                                else
                                {
                                    lblProfit.Text = dt.Rows[0]["sum_total"].ToString();
                                    string checktotal = dt.Rows[0]["sum_total2"].ToString();
                                   
                                    lblProfit.ForeColor = Color.Blue;
                                    double total;
                                    double.TryParse(checktotal, out total);
                                    //total= Convert.ToInt32(checktotal);
                                    if (total <= 0)
                                        {
                                            lblProfit.ForeColor = Color.Red;
                                        }
                                    
                                    
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

        protected void BtnSearchProfitLoss_Click(object sender, EventArgs e)
        {
            SchoolBind();
            TurnOver();
            TurnOverBreakDown();
            ExpensesTotal();
            ExpensesBreakDown();
            StaffCostTotal();
            StaffCostBreakDown();
            ProfitLossTotal();
        }
    }
}