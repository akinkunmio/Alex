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
using Microsoft.Reporting.WebForms;

namespace Alex.pages
{
    public partial class employee_payslip2 : System.Web.UI.Page
    {
        string EmpId = string.Empty;
        string PayrollId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ManageCookies.VerifyAuthentication();
                EmpId = Request.QueryString["Id"].ToString();
                PayrollId = Request.QueryString["UnewQWCSBQ"].ToString();
                Info();
            }
        }
        private MyStuDataSet GetData()
        {
            var PayrollId = Request.QueryString["UnewQWCSBQ"].ToString();

            MyStuDataSet dt = new MyStuDataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_hr_bab_employee_payroll_payslip", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@payroll_id", SqlDbType.Int).Value = PayrollId;
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            sda.Fill(dt.Tables["sp_ms_hr_bab_employee_payroll_payslip"]);
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
            return dt;
        }

        private void Info()
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Report/Employee_Payslip2.rdlc");
            // ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Report/EmployeePayslip.rdlc");
            MyStuDataSet dsCustomers = GetData();
            ReportDataSource datasource = new ReportDataSource("MyStuDataSet", dsCustomers.Tables[18]);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
        }


        protected void btnBack_Click(object sender, EventArgs e)
        {
            EmpId = Request.QueryString["Id"].ToString();
            Response.Redirect("~/pages/employee_profile.aspx?EmployeeId=" + EmpId + "&action=payroll");
        }



    }
}