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

namespace Alex.pages
{
    public partial class SalaryPay : System.Web.UI.Page
    {
        string EmpId = string.Empty;
        string Year = string.Empty;
        string Month = string.Empty;
        string PayrollId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            EmpId = Request.QueryString["EmpId"].ToString();
            Year = Request.QueryString["y"].ToString();
            Month = Request.QueryString["m"].ToString();
            PayrollId = Request.QueryString["UnewQWCSBQ"].ToString();
            if (!IsPostBack)
            {
                GetPaySalaryList(EmpId, Month, Year);
            }
            btnOK.Visible = false;
        }

        public void GetPaySalaryList(string EmpID, string Month, string Year)
        {

            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_hr_employee_payroll_year_list", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@emp_id", SqlDbType.VarChar).Value = EmpID;
                    cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = Year;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        {
                            sda.Fill(dt);
                            var query = from r in dt.AsEnumerable()
                                        where r.Field<string>("month").Equals(Month)
                                        select new
                                        {
                                            Name = r["name"].ToString(),
                                            Salary = r["Net Pay"].ToString(),
                                            TotalSalaryPaid = r["total_amount_paid"].ToString(),
                                            Balance = r["balance"].ToString(),
                                        };
                            foreach (var a in query)
                            {
                                string a1 = a.Salary.ToString();
                                string a2 = a.TotalSalaryPaid.ToString();
                                string a3 = a.Balance.ToString();
                            }
                            DetailsViewPayment.DataSource = query;
                            DetailsViewPayment.DataBind();

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

        public void ClearData(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if ((c.GetType() == typeof(TextBox)))
                {
                    ((TextBox)c).Text = "";
                }
                if ((c.GetType() == typeof(DropDownList)))
                {
                    ((DropDownList)c).SelectedIndex = -1;
                    //((DropDownList)c).Items.Clear();
                }
                if (c.HasControls())
                {
                    ClearData(c);
                }
            }
        }

        protected void FeePay_Click(object sender, EventArgs e)
        {
            lblResult.Text = "Payment Successful";
            //Amount = Session["Amount"].ToString();
            //CreatedBy = "";
            //SaveFeeInfo(RegID, Amount, CreatedBy);
            //StatementOfAccount(Personid, RegID);
            SaveFeeInfo();
            btnOK.Visible = true;
            btnSubmit.Visible = false;
            divPayFields.Visible = false;
        }
        public void SaveFeeInfo()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_hr_payment_add", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@emp_id", SqlDbType.VarChar).Value = EmpId;
                    cmd.Parameters.Add("@amount", SqlDbType.Decimal).Value = tbPayingAmount.Text.ToString();
                    cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = Year;
                    cmd.Parameters.Add("@month", SqlDbType.VarChar).Value = Month;
                    cmd.Parameters.AddWithValue("@date_received", tbDate.Text.ToString());
                    cmd.Parameters.AddWithValue("@payment_method", ddlPaymentMethod.Text.ToString());
                    cmd.Parameters.AddWithValue("@payment_method_ref", tbPMReference.Text.ToString());
                    cmd.Parameters.AddWithValue("@payroll_id", PayrollId);
                    cmd.Parameters.Add("@created_by", SqlDbType.VarChar).Value = HttpContext.Current.User.Identity.Name;
                    //cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery(); //this was missing.

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);

            }
            finally
            {
                ClearData(this);
                GetPaySalaryList(EmpId, Month, Year);
                con.Close();
            }
        }


        protected void Result_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("~/pages/employee_profile.aspx?EmployeeId=" + EmpId + "&Month=" + Month + "&year=" + Year + "&action=payroll");
           
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/employee_profile.aspx?EmployeeId=" + EmpId + "&Month=" + Month + "&year=" + Year + "&action=payroll");
        }
    }
}