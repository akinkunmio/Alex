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

namespace Alex.pages.employee_reports
{
    public partial class employees_payslip2 : System.Web.UI.Page
    {
        int lvl = 0;
        private static string currentmonth = DateTime.Now.ToString("MMMM");
        protected void Page_Load(object sender, EventArgs e)
        {
            Level();
            if (lvl == 1)
            {
                if (!IsPostBack)
                {
                    //ManageCookies.VerifyAuthentication();
                    DropDownPaymentSalaryYear();
                    ddlYear.SelectedIndex = 1;
                    DropDownPaymentSalaryMonth();
                    ddlMonth.SelectedValue = currentmonth;
                    Info();
                }
            }
            else if (lvl == 2 || lvl == 3 || lvl == 4)
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

        private MyStuDataSet GetData()
        {
            MyStuDataSet dt = new MyStuDataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_hr_bab_list_all_employee_payslip", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = ddlYear.SelectedItem.ToString();
                        cmd.Parameters.Add("@month", SqlDbType.VarChar).Value = ddlMonth.SelectedItem.ToString();
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            dt.Tables["sp_ms_hr_bab_list_all_employee_payslip"].Clear();
                            sda.Fill(dt.Tables["sp_ms_hr_bab_list_all_employee_payslip"]);
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
            RptViewerPayslipAll.ProcessingMode = ProcessingMode.Local;
            RptViewerPayslipAll.LocalReport.ReportPath = Server.MapPath("~/Report/Payslips2.rdlc");
            MyStuDataSet dsEmployees = GetData();
            ReportDataSource datasource = new ReportDataSource("MyStuDataSet", dsEmployees.Tables[19]);
            RptViewerPayslipAll.LocalReport.DataSources.Clear();
            RptViewerPayslipAll.LocalReport.DataSources.Add(datasource);
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownPaymentSalaryMonth();
            if (ddlMonth.Items.Count > 1)
            {
                Info();
            }
        }
        //protected void BtnSearchPayslips_Click(object sender, EventArgs e)
        //{
        //    Info();
        //}

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            Info();
        }




    }
}