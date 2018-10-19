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
    public partial class employee_payslip : System.Web.UI.Page
    {
        string EmpId = string.Empty;
        string PayrollId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            EmpId = Request.QueryString["Id"].ToString();
            PayrollId = Request.QueryString["UnewQWCSBQ"].ToString();
            if (!Page.IsPostBack)
            {
                Payslip();
            }
         }

        private void Payslip()
        {
            var PayrollId = Request.QueryString["UnewQWCSBQ"].ToString();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            try
                {
                   using (SqlCommand cmd = new SqlCommand("sp_ms_hr_employee_payroll_payslip", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@payroll_id", SqlDbType.Int).Value = PayrollId;
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                GridViewAllowances.DataSource = dt;
                                GridViewDedcutions.DataSource = dt;
                                if (dt.Rows.Count == 0)
                                {
                                   // lblZeroRecords.Visible = true;
                                   // lblZeroRecords.Text = "No Payment(s) Found ";
                                }
                                else
                                {
                                   // lblZeroRecords.Visible = false;
                                    GridViewAllowances.DataBind();
                                    GridViewDedcutions.DataBind();
                                    lblEmployeeName.Text = dt.Rows[0]["Full Name"].ToString();
                                    lblMonth.Text = dt.Rows[0]["month"].ToString();
                                    lblYear.Text = dt.Rows[0]["year"].ToString();
                                   lblBasicPay.Text = dt.Rows[0]["basic_pay"].ToString();
                                   lblGrossPay.Text  = dt.Rows[0]["Gross Pay"].ToString();
                                    lblTotalAllowances.Text = dt.Rows[0]["Total Allowances"].ToString();
                                    lblTotalDeductions.Text = dt.Rows[0]["total_deductions"].ToString();
                                   lblNetPay.Text  = dt.Rows[0]["Net Pay"].ToString();
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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/employee_profile.aspx?EmployeeId=" + EmpId + "&action=payroll");
        }
    }
}