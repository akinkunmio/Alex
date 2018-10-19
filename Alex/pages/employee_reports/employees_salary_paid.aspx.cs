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
using ClosedXML.Excel;


namespace Alex.pages.employee_reports
{
    public partial class employees_salary_paid : System.Web.UI.Page
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
                  
                    SchoolBind();
                 
                    ddlMonth.SelectedValue = currentmonth;
                   
                    SalaryPayments();
                }
            }
            else if (lvl == 2 || lvl == 3 || lvl == 4 || lvl == 5)
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


        //protected void BtnSearchPaidPayments_Click(object sender, EventArgs e)
        //{
           
        //    SalaryPayments();
        //}

       
        private void SalaryPayments()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_rep_employees_salaries_paid_month_year", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = ddlYear.SelectedItem.ToString();
                    cmd.Parameters.Add("@month", SqlDbType.VarChar).Value = ddlMonth.SelectedItem.ToString();
                   
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        DataSet ds = new DataSet();
                        sda.Fill(ds);
                            GridViewBankPayments.DataSource = ds.Tables[0];
                            if (ds.Tables[0].Rows.Count == 0)
                            {
                                DivRptList.Visible = false;
                                lblZeroRecords.Visible = true;
                                lblZeroRecords.Text = "No Salary's Paid for selected Year and Month ";
                                GridViewBankPayments.Visible = false;
                                btnPrint.Visible = false;
                            }
                            else
                            {
                                DivRptList.Visible = true;
                                imgSchool.ImageUrl = "~/pages/ImageHandler.ashx?roll_no=1";
                                lblYearSelected.Text = ddlYear.SelectedItem.ToString();
                                lblMonthSelected.Text = ddlMonth.SelectedItem.ToString();
                                lblZeroRecords.Visible = false;
                                GridViewBankPayments.DataBind();
                                GridViewBankPayments.Visible = true;
                                btnPrint.Visible = true;
                                lblNoOfEmployees.Text = ds.Tables[1].Rows[0]["No_Of_Emp_Paid"].ToString();
                                lblTotalSalaryPaid4ThisMonth.Text = ds.Tables[1].Rows[0]["Total_Salary_Paid_ThisMonth"].ToString();
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
            if (ddlMonth.Items.Count > 1)
            {
               SalaryPayments();
            }
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            SalaryPayments();
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

       
        private void ExportGridToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Employees" + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridViewBankPayments.GridLines = GridLines.Both;
            GridViewBankPayments.HeaderStyle.Font.Bold = true;
            GridViewBankPayments.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();   
        }

        protected void btnDownloadExcel_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();
        }  
    }
}