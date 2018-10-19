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
using System.IO;
using System.Drawing;


namespace Alex.pages.employee_reports
{
    public partial class monthly_salary_bank : System.Web.UI.Page
    {
        int lvl = 0;
        private static string currentmonth = DateTime.Now.ToString("MMMM");
        protected void Page_Load(object sender, EventArgs e)
        {
            Level();
            ManageCookies.VerifyAuthentication();
            if (lvl == 1)
            {
                if (!Page.IsPostBack)
                {
                    DropDownPaymentSalaryYear();
                    ddlYear.SelectedIndex = 1;
                    DropDownPaymentSalaryMonth();
                    DropDownPaymentSalaryBank();
                    SchoolBind();
                    ddlBank.SelectedValue = "All";
                    ddlMonth.SelectedValue = currentmonth;
                    SalaryPaymentsSummary();
                    SalaryPayments();
                }
            }
            else if (lvl == 2 || lvl == 3 || lvl==4 || lvl==5)
            {
                Response.Redirect("~/pages/404.aspx", false);
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
        public void DropDownPaymentSalaryYear()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_payroll_year_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();
            ddlYear.DataSource = ddlValues;
            ddlYear.DataValueField = "year";
            ddlYear.DataTextField = "year";
            ddlYear.DataBind();
            ddlYear.Items.Insert(0, new ListItem("Please select Year", ""));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void DropDownPaymentSalaryMonth()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_payroll_month_dropdown", con);
            cmd.Parameters.AddWithValue("@year", ddlYear.SelectedItem.ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                SqlDataReader ddlValues;
                ddlValues = cmd.ExecuteReader();

                ddlMonth.DataSource = ddlValues;
                ddlMonth.DataValueField = "month";
                ddlMonth.DataTextField = "month";
                ddlMonth.DataBind();
                ddlMonth.Items.Insert(0, new ListItem("Please select Month", ""));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        public void DropDownPaymentSalaryBank()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select status_name from ms_status where category = 'Bank' ORDER BY status_name", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlBank.DataSource = ddlValues;
            ddlBank.DataValueField = "status_name";
            ddlBank.DataTextField = "status_name";
            ddlBank.DataBind();

            ddlBank.Items.Insert(0, new ListItem("Please select bank", ""));
            ddlBank.Items.Insert(1, new ListItem("All"));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
        //protected void BtnSearchBankPayments_Click(object sender, EventArgs e)
        //{
        //    SalaryPaymentsSummary();
        //    SalaryPayments();
        //}

        private void SalaryPaymentsSummary()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_rep_monthly_salary_bank_payment_summary", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@year", SqlDbType.VarChar).Value =ddlYear.SelectedItem.ToString();
                        cmd.Parameters.Add("@month", SqlDbType.VarChar).Value = ddlMonth.SelectedItem.ToString();
                        cmd.Parameters.Add("@bank ", SqlDbType.VarChar).Value =ddlBank.SelectedItem.ToString();
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
                                    //lblUnitPrice.Text = dt.Rows[0]["unit_price"].ToString();
                                    //lblTotalQuanityPurchased.Text = dt.Rows[0]["total_quantity_purchased"].ToString();
                                    lblTotalAmountRecieved.Text = dt.Rows[0]["Total_Net_Pay"].ToString();
                                    imgSchool.ImageUrl = "~/pages/ImageHandler.ashx?roll_no=1";
                                    lblYearSelected.Text =ddlYear.SelectedItem.ToString();
                                    lblMonthSelected.Text = ddlMonth.SelectedItem.ToString();
                                    lblBankSelected.Text =ddlBank.SelectedItem.ToString();
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

        private void SalaryPayments()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_rep_monthly_salary_bank_payment_list", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@year", SqlDbType.VarChar).Value =ddlYear.SelectedItem.ToString();
                    cmd.Parameters.Add("@month", SqlDbType.VarChar).Value = ddlMonth.SelectedItem.ToString();
                    cmd.Parameters.Add("@bank ", SqlDbType.VarChar).Value =ddlBank.SelectedItem.ToString();
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {

                            sda.Fill(dt);
                           GridViewBankPayments.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                lblZeroRecords.Visible = true;
                                lblZeroRecords.Text = "No Salary's Found ";
                               GridViewBankPayments.Visible = false;
                                btnPrint.Visible = false;
                            }
                            else
                            {
                                lblZeroRecords.Visible = false;
                                GridViewBankPayments.DataBind();
                                GridViewBankPayments.Visible = true;
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

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownPaymentSalaryMonth();
            SalaryPaymentsSummary();
            SalaryPayments();
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            SalaryPaymentsSummary();
            SalaryPayments();
        }

        protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            SalaryPaymentsSummary();
            SalaryPayments();
        }
        //protected void ExportToExcel(object sender, EventArgs e)
        //{
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
        //    Response.Charset = "";
        //    Response.ContentType = "application/vnd.ms-excel";
        //    using (StringWriter sw = new StringWriter())
        //    {
        //        HtmlTextWriter hw = new HtmlTextWriter(sw);

        //        //To Export all pages
        //        GridViewBankPayments.AllowPaging = false;
        //        this.SalaryPayments();

        //        GridViewBankPayments.HeaderRow.BackColor = Color.White;
        //        foreach (TableCell cell in GridViewBankPayments.HeaderRow.Cells)
        //        {
        //            cell.BackColor = GridViewBankPayments.HeaderStyle.BackColor;
        //        }
        //        foreach (GridViewRow row in GridViewBankPayments.Rows)
        //        {
        //            row.BackColor = Color.White;
        //            foreach (TableCell cell in row.Cells)
        //            {
        //                if (row.RowIndex % 2 == 0)
        //                {
        //                    cell.BackColor = GridViewBankPayments.AlternatingRowStyle.BackColor;
        //                }
        //                else
        //                {
        //                    cell.BackColor = GridViewBankPayments.RowStyle.BackColor;
        //                }
        //                cell.CssClass = "textmode";
        //            }
        //        }

        //        GridViewBankPayments.RenderControl(hw);

        //        //style to format numbers to string
        //        string style = @"<style> .textmode { } </style>";
        //        Response.Write(style);
        //        Response.Output.Write(sw.ToString());
        //        Response.Flush();
        //        Response.End();
        //    }
        //}

        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //    /* Verifies that the control is rendered */
        //}

    }
}