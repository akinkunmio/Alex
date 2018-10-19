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

namespace Alex.pages.student_reports
{
    public partial class expenses_category_date : System.Web.UI.Page
    {
        int lvl = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            Level();
            if (!Page.IsPostBack)
            {
                DropDownExpensesCategory();
                ddlExpensesCategory.SelectedIndex = 1;
                DateTime today = DateTime.Today;
                DateTime TwentyEightDaysEarlier = today.AddDays(-28);
                string TodayDate = DateTime.Today.ToString("dd-MM-yyyy");
                tbStartDate.Text = TwentyEightDaysEarlier.ToString("dd-MM-yyyy");
                tbEndDate.Text = TodayDate;
                SchoolBind();
                Expenses();
                ExpensesSummary();
            }
            else if (lvl == 4)
            {
                Response.Redirect("~/pages/ForbiddenAccess.aspx", false);
            }
            else if (lvl == 2 || lvl == 3)
            {
                TotalExpensesLabel.Visible = false;
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
        public void DropDownExpensesCategory()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_status_dropdown_orderby_name", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@category", "Expenses_type");
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlExpensesCategory.DataSource = ddlValues;
            ddlExpensesCategory.DataValueField = "status_name";
            ddlExpensesCategory.DataTextField = "status_name";
            ddlExpensesCategory.DataBind();
            ddlExpensesCategory.Items.Insert(0, new ListItem("Please select Category", ""));
            ddlExpensesCategory.Items.Insert(1, new ListItem("All"));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
        protected void BtnSearchExpenses_Click(object sender, EventArgs e)
        {
            ExpensesSummary();
            Expenses();
        }

        private void ExpensesSummary()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_expense_summary_date_category_total", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@category", SqlDbType.VarChar).Value = ddlExpensesCategory.SelectedItem.ToString();
                        cmd.Parameters.Add("@exp_date_start", SqlDbType.VarChar).Value = tbStartDate.Text.ToString();
                        cmd.Parameters.Add("@exp_date_end", SqlDbType.VarChar).Value = tbEndDate.Text.ToString();
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
                                    DivStudents.Visible = false;
                                }
                                else
                                {
                                    DivStudents.Visible = true;
                                    lblToalExpenses.Text = dt.Rows[0]["sum_total"].ToString();
                                    imgSchool.ImageUrl = "~/pages/ImageHandler.ashx?roll_no=1";
                                    lblCategorySelected.Text = ddlExpensesCategory.SelectedItem.ToString();
                                    lblDateSelected.Text = tbStartDate.Text.ToString();
                                    lblEndDateSelected.Text = tbEndDate.Text.ToString();
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

        private void Expenses()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_expense_summary_date_category", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@category", SqlDbType.VarChar).Value = ddlExpensesCategory.SelectedItem.ToString();
                    cmd.Parameters.Add("@exp_date_start", SqlDbType.VarChar).Value = tbStartDate.Text.ToString();
                    cmd.Parameters.Add("@exp_date_end", SqlDbType.VarChar).Value = tbEndDate.Text.ToString();
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
                                lblZeroRecords.Visible = true;
                                lblZeroRecords.Text = "No  Expenses Found "; 
                                GridViewExpenses.Visible = false;
                                btnPrint.Visible = false;
                            }
                            else
                            {
                                lblZeroRecords.Visible = false; 
                                GridViewExpenses.DataBind();
                                GridViewExpenses.Visible = true;
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
    }
}