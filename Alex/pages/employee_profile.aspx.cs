using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Alex.App_code;
using System.IO;


namespace Alex.pages
{
    public partial class employee_profile : System.Web.UI.Page
    {
        string selectedValue = string.Empty;
        private static string currentYear = DateTime.Now.Year.ToString();
        string action = HttpContext.Current.Request.QueryString["action"];
        string employeeId = HttpContext.Current.Request.QueryString["EmployeeId"];
        string EndOrRenewEmployee = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();

            HideBtnEndEmployee();
            if (DetailsViewProfile.CurrentMode == DetailsViewMode.Edit)
            {
                var UserName = HttpContext.Current.User.Identity.Name;
                SqlDataSourceEmployeeProfile.UpdateParameters["updated_by"].DefaultValue = UserName;
            }
            if (DetailsViewAddress.CurrentMode == DetailsViewMode.Edit)
            {
                var UserName = HttpContext.Current.User.Identity.Name;
                SqlDataSourceAddress.UpdateParameters["updated_by"].DefaultValue = UserName;
            }
            if (DetailsViewAddress.CurrentMode == DetailsViewMode.Insert)
            {
                var UserName = HttpContext.Current.User.Identity.Name;
                SqlDataSourceAddress.InsertParameters["created_by"].DefaultValue = UserName;
            }
            if (DetailsViewAddress.Rows[0].Cells.Count == 1)
            {
                divEndAddress.Visible = false;
            }
            else
            {
                divEndAddress.Visible = true;
            }
            if (!IsPostBack)
            {
                GetEmployeeId();
                ProfilePicture.ImageUrl = "EmpProfilePicHandler.ashx?EmployeeId=" + employeeId;
                divProfilePic.Visible = true;
                QualificationBindData();
                ViewAddress.Visible = false;
                ViewPayroll.Visible = false;
                ViewPayroll2.Visible = false;
                for (int year = 2015; year <= DateTime.Now.Year; year++)
                    ddlYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
                for (int year2 = 2015; year2 <= DateTime.Now.Year; year2++)
                    ddlYear2.Items.Add(new ListItem(year2.ToString(), year2.ToString()));
                ddlYear.SelectedValue = currentYear;
                ddlYear2.SelectedValue = currentYear;
                if (!string.IsNullOrEmpty(action))
                {
                    if (action == "add")
                    {
                        AddressBtn();
                    }
                    else if (action == "payroll")
                    {
                        PayrollBtn();
                    }
                    else if (action == "payroll/14fbc9442fd6c984")
                    {
                        PayrollTab();
                    }
                    else if (action == "payroll2/14fbc9442fd6c984")
                    {
                        PayrollTab2();
                    }
                    else if (action == "Qlfc")
                    {
                        QulificationsBtn();
                    }
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnProfile');", true);
                }

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
        private void GetEmployeeId()
        {
            var employeeId = Request.QueryString["EmployeeId"];
            string status = string.Empty;

            if (PreviousPage != null)
            {
                if (PreviousPageViewState != null)
                {
                    status = PreviousPageViewState["Status"].ToString();
                }
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot Delete Profile because profile has Address')", true);
            }

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_hr_employee_profile", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@emp_id", SqlDbType.VarChar).Value = employeeId;

                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                {
                                    sda.Fill(dt);
                                    LabelProfileName.Text = dt.Rows[0]["Full Name"].ToString();
                                    EndOrRenewEmployee = dt.Rows[0]["end_date"].ToString();
                                }
                            }
                        }

                    }
                }
            }
            catch
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + "," + "');", true);
            }
            finally
            {
                if (!string.IsNullOrWhiteSpace(EndOrRenewEmployee))
                { lblEndEmployee.Text = "Re-Employ a previous Employee"; btnEndEmployee.Text = "Re-Employ"; }
                else
                { lblEndEmployee.Text = "End an Employee's Employment"; btnEndEmployee.Text = "End Employement"; }
            }
        }

        private StateBag PreviousPageViewState
        {
            get
            {
                StateBag returnValue = null;
                if (PreviousPage != null)
                {
                    Object objPreviousPage = (Object)PreviousPage;
                    MethodInfo objMethod = objPreviousPage.GetType().GetMethod("ReturnViewState");
                    return (StateBag)objMethod.Invoke(objPreviousPage, null);
                }
                return returnValue;
            }
        }

        public StateBag ReturnViewState()
        {
            return ViewState;
        }

        protected void SqlDataSourceEmployeeProfile_Updated(object sender, SqlDataSourceStatusEventArgs e)
        {
            var employeeId = Request.QueryString["EmployeeId"];
            Response.Redirect("employee_profile.aspx?EmployeeId=" + employeeId);
        }

        protected void SqlDataSourceEmployeeProfile_Deleting(object sender, SqlDataSourceCommandEventArgs e)
        {
            e.Command.Connection.Open();
            e.Command.Transaction = e.Command.Connection.BeginTransaction();
        }
        protected void SqlDataSourceEmployeeProfile_Deleted(object sender, SqlDataSourceStatusEventArgs e)
        {
            var employeeId = Request.QueryString["EmployeeId"];
            try
            {
                //if (!string.IsNullOrEmpty(e.Exception.Message))
                // ViewState["Status"] = e.Exception.Message;
                //   Server.Transfer("employee_profile.aspx?EmployeeId=" + employeeId);
                // Response.Redirect("employee_profile.aspx?EmployeeId=" + employeeId);
                SqlDataSource d = new SqlDataSource();
                d.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                d.ProviderName = ConfigurationManager.ConnectionStrings["conStr"].ProviderName;
                d.DeleteCommand = "sp_ms_hr_employees_delete";

                d.DeleteCommandType = SqlDataSourceCommandType.StoredProcedure;

                d.DeleteParameters.Add("emp_id", employeeId.ToString());
                //d.DeleteParameters.Add("dept_id", "23");
                d.DeleteParameters.Add("from", "Delete");
                d.Delete();

                bool OtherProcessSucceeded = true;
                if (e.Exception != null)
                {
                    OtherProcessSucceeded = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot update')", true);
                }
                if (OtherProcessSucceeded)
                {
                    e.Command.Transaction.Commit();
                    //Response.Write("The record was updated successfully");
                }
                else
                {
                    e.Command.Transaction.Rollback();
                    //Response.Write("The record was not updated");
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Profile was deleted successfully');window.location = 'employees.aspx';", true);
                // Response.Redirect("people.aspx");
            }

            catch (Exception ex)
            {
                if (ex.Message.Contains("FK_ms_hr_emp_quals_ms_hr_emp_quals"))
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot Delete Profile, Payroll exist for this profile')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot Delete Profile, Qualifications exist for this profile')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot Delete Profile, Previous Address or Payroll exist for this profile')", true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                }
            }

        }


        //private void Profile()
        //{
        //    var employeeId = Request.QueryString["EmployeeId"];
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        //    try
        //    {
        //        using (SqlCommand cmd = new SqlCommand("sp_ms_hr_employee_profile", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            cmd.Parameters.Add("@emp_id", SqlDbType.VarChar).Value = employeeId;
        //            using (SqlDataAdapter sda = new SqlDataAdapter())
        //            {
        //                con.Open();
        //                cmd.Connection = con;
        //                sda.SelectCommand = cmd;
        //                using (DataTable dt = new DataTable())
        //                {

        //                    sda.Fill(dt);
        //                    DetailsViewProfile.DataSource = dt;
        //                    DetailsViewProfile.DataBind();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //        // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        protected void BtnProfile_Click(object sender, EventArgs e)
        {
            divProfilePic.Visible = true;
            ViewAddress.Visible = false;
            ViewProfile.Visible = true;
            ViewPayroll.Visible = false;
            ViewPayroll2.Visible = false;
            divSetSalary.Visible = false;
            divSetSalary2.Visible = false;
            divEndDateEmployee.Visible = false;
            divQualifications.Visible = false;
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnProfile');", true);
        }

        //private void Address()
        //{
        //    var employeeId = Request.QueryString["EmployeeId"];
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        //    try
        //    {
        //        using (SqlCommand cmd = new SqlCommand("sp_ms_hr_employee_address_current", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            cmd.Parameters.Add("@emp_id", SqlDbType.VarChar).Value = employeeId;
        //            using (SqlDataAdapter sda = new SqlDataAdapter())
        //            {
        //                con.Open();
        //                cmd.Connection = con;
        //                sda.SelectCommand = cmd;
        //                using (DataTable dt = new DataTable())
        //                {

        //                    sda.Fill(dt);
        //                    DetailsViewAddress.DataSource = dt;
        //                    DetailsViewAddress.DataBind();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //        // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        protected void BtnAddress_Click(object sender, EventArgs e)
        {
            AddressBtn();
        }

        private void AddressBtn()
        {
            divProfilePic.Visible = false;
            ViewAddress.Visible = true;
            ViewProfile.Visible = false;
            ViewPayroll.Visible = false;
            ViewPayroll2.Visible = false;
            divSetSalary.Visible = false;
            divSetSalary2.Visible = false;
            divEndDateEmployee.Visible = false;
            divQualifications.Visible = false;
            Address();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnAddress');", true);
        }




        //protected void BtnSalarySave_Click(object sender, EventArgs e)
        //{
        //    var employeeId = Request.QueryString["EmployeeId"];
        //    string SalaryId = string.Empty;
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        //    try
        //    {
        //        if (DetailsViewCurrentSalary.Rows.Count != 0)
        //        {
        //            SalaryId = DetailsViewCurrentSalary.Rows[0].Cells[1].Text;
        //        }
        //        else
        //        {
        //            SalaryId = DBNull.Value.ToString();
        //        }
        //        SqlCommand cmd = new SqlCommand("sp_ms_hr_employee_update_salary", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        con.Open();
        //        cmd.Parameters.Add("@emp_id", SqlDbType.VarChar).Value = employeeId;
        //        cmd.Parameters.AddWithValue("@amount", tbSalary.Text.ToString());
        //        cmd.Parameters.AddWithValue("@sal_id", SalaryId);
        //        cmd.Parameters.AddWithValue("@housing_allow", tbHousingAllowance.Text.ToString());
        //        cmd.Parameters.AddWithValue("@transport_allow", tbTransportAllownace.Text.ToString());
        //        cmd.Parameters.AddWithValue("@medical_allow", tbMedicalAllowance.Text.ToString());
        //        cmd.Parameters.AddWithValue("@meal_allow", tbMealAllowance.Text.ToString());
        //        cmd.Parameters.AddWithValue("@utility_allow", tbUtlityAllowance.Text.ToString());
        //        cmd.Parameters.AddWithValue("@other_allow", tbOtherAllowance.Text.ToString());
        //        cmd.Parameters.AddWithValue("@tax_deduct", tbTaxDeduction.Text.ToString());
        //        cmd.Parameters.AddWithValue("@pension_deduct", tbPensionDeduction.Text.ToString());
        //        cmd.Parameters.AddWithValue("@coop_deduct", tbCoOperativeDeduction.Text.ToString());
        //        cmd.Parameters.AddWithValue("@loan_deduct", tbLoanDeduction.Text.ToString());
        //        cmd.Parameters.AddWithValue("@other_deduct", tbOtherDeduction.Text.ToString());
        //        cmd.ExecuteNonQuery();
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('SUCESSFULLY SALARY SAVED');", true);

        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //        // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
        //    }
        //    finally
        //    {
        //        con.Close();
        //        Response.Redirect("~/pages/employee_profile.aspx?EmployeeId=" + employeeId + "&action=payroll");
        //    }
        //}

        //protected void btnCancelSave_Click(object sender, EventArgs e)
        //{
        //    var employeeId = Request.QueryString["EmployeeId"];
        //    Response.Redirect("~/pages/employee_profile.aspx?EmployeeId=" + employeeId + "&action=payroll");
        //}

        protected void btnEndEmployee_Click(object sender, EventArgs e)
        {
            ViewAddress.Visible = false;
            ViewProfile.Visible = false;
            divProfilePic.Visible = false;
            ViewPayroll.Visible = false;
            ViewPayroll2.Visible = false;
            divSetSalary.Visible = false;
            divEndDateEmployee.Visible = true;
            divQualifications.Visible = false;
        }

        private void Address()
        {
            var employeeId = Request.QueryString["EmployeeId"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_hr_employee_address_list_all", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@emp_id", SqlDbType.VarChar).Value = employeeId;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridViewEmployeeAddress.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                lblZeroAddress.Visible = true;
                                lblZeroAddress.Text = "No address history found ";
                            }
                            else
                            {
                                GridViewEmployeeAddress.DataBind();
                                lblZeroAddress.Visible = false;
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
        protected void btnEndDateEmployee_Click(object sender, EventArgs e)
        {
            GetEmployeeId();
            var employeeId = Request.QueryString["EmployeeId"];
            if (!string.IsNullOrWhiteSpace(EndOrRenewEmployee))
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ms_hr_employees_status_add", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.Add("@emp_id", SqlDbType.VarChar).Value = employeeId;
                    cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                    cmd.Parameters.AddWithValue("@hire_date", tbEndDateEmployee.Text.ToString());
                    cmd.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee Successfully Re-employed');window.location = '/pages/employee_profile.aspx?EmployeeId=" + employeeId + "';", true);
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
            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ms_hr_employee_status_update_end_date", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.Add("@emp_id", SqlDbType.VarChar).Value = employeeId;
                    cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
                    cmd.Parameters.AddWithValue("@end_date", tbEndDateEmployee.Text.ToString());
                    cmd.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee Successfully Ended');window.location = '/pages/employee_profile.aspx?EmployeeId=" + employeeId + "';", true);
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

        public void Payroll()
        {
            var employeeId = Request.QueryString["EmployeeId"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_hr_employee_payroll_year_list", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@emp_id", SqlDbType.VarChar).Value = employeeId;
                    cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = ddlYear.SelectedItem.ToString();
                    //cmd.Parameters.AddWithValue("@year", ddlYear.SelectedItem.ToString());
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {

                            sda.Fill(dt);
                            GridViewPayroll.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                lblZeroRecords.Visible = true;
                                lblZeroRecords.Text = "No Payment History";
                                GridViewPayroll.Visible = false;
                                GridViewPayrollBreakDown.Visible = false;
                            }
                            else
                            {
                                GridViewPayroll.DataBind();
                                lblClickRecords.Text = "Click on Month below For Payment Details";
                                lblClickRecords.ForeColor = System.Drawing.Color.White;
                                lblClickRecords.BackColor = System.Drawing.ColorTranslator.FromHtml("#18a689");
                                lblZeroRecords.Visible = false;
                                GridViewPayroll.Visible = true;
                                //GridViewPayrollBreakDown.Visible = true;
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


        private void PayrollBreakDown()
        {
            var employeeId = Request.QueryString["EmployeeId"];
            var year = Request.QueryString["year"];
            var month = Request.QueryString["month"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_hr_employee_payroll_month_list_payments", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@emp_id", SqlDbType.VarChar).Value = employeeId;
                    cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = year;
                    cmd.Parameters.Add("@month", SqlDbType.VarChar).Value = month;
                    //cmd.Parameters.AddWithValue("@year", ddlYear.SelectedItem.ToString());
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {

                            sda.Fill(dt);
                            GridViewPayrollBreakDown.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                lblZeroBreakDown.Visible = true;
                                lblZeroBreakDown.Text = "No Payment History";
                                GridViewPayrollBreakDown.Visible = false;
                            }
                            else
                            {
                                GridViewPayrollBreakDown.DataBind();
                                lblZeroBreakDown.Visible = false;
                                GridViewPayrollBreakDown.Visible = true;
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

        protected void BtnPayroll_Click(object sender, EventArgs e)
        {
            PayrollTab();
        }

        protected void PayrollTab()
        {
            Payroll();
            CurrentSalary();
            ViewProfile.Visible = false;
            divProfilePic.Visible = false;
            ViewAddress.Visible = false;
            ViewPayroll.Visible = true;
            ViewPayroll2.Visible = false;
            divSetSalary.Visible = false;
            divSetSalary2.Visible = false;
            divEndDateEmployee.Visible = false;
            divQualifications.Visible = false;
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnPayroll');", true);
        }

        private void PayrollBtn()
        {
            PayrollBreakDown();
            Payroll();
            CurrentSalary();
            ViewProfile.Visible = false;
            divProfilePic.Visible = false;
            ViewAddress.Visible = false;
            ViewPayroll.Visible = true;
            ViewPayroll2.Visible = false;
            divSetSalary.Visible = false;
            divSetSalary2.Visible = false;
            divEndDateEmployee.Visible = false;
            divQualifications.Visible = false;
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnPayroll');", true);
        }


        protected void btnSetSalary_Click(object sender, EventArgs e)
        {
            ViewAddress.Visible = false;
            ViewProfile.Visible = false;
            divProfilePic.Visible = false;
            ViewPayroll.Visible = false;
            ViewPayroll2.Visible = false;
            divSetSalary.Visible = true;
            divSetSalary2.Visible = false;
            divEndDateEmployee.Visible = false;
            divQualifications.Visible = false;
            DetailsViewSalaryPayroll.DefaultMode = DetailsViewMode.Edit;
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnPayroll');", true);
        }
        protected void btnPayrollByYear_Click(object sender, EventArgs e)
        {
            Payroll();
        }

        protected void BtnQualifications_Click(object sender, EventArgs e)
        {
            QulificationsBtn();
        }

        private void QulificationsBtn()
        {
            divQualifications.Visible = true;
            ViewAddress.Visible = false;
            divProfilePic.Visible = false;
            ViewProfile.Visible = false;
            ViewPayroll.Visible = false;
            ViewPayroll2.Visible = false;
            divSetSalary.Visible = false;
            divSetSalary2.Visible = false;
            divEndDateEmployee.Visible = false;
            //QualificationBindData();

            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnQualifications');", true);
        }

        protected void BtnAddressEndDate_Click(object sender, EventArgs e)
        {
            var employeeId = Request.QueryString["EmployeeId"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_hr_employee_address_end_date_edit", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@emp_id", SqlDbType.VarChar).Value = employeeId;
                    cmd.Parameters.AddWithValue("@end_date", tbAddressEndDate.Text.ToString());
                    cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
                    //cmd.Parameters.Add("@end_date", SqlDbType.VarChar).Value = BtnEndDate;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {

                            sda.Fill(dt);
                            GridViewEmployeeAddress.DataSource = dt;
                            GridViewEmployeeAddress.DataBind();
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
                Response.Redirect("~/pages/employee_profile.aspx?EmployeeId=" + employeeId + "&action=add");

            }
        }

        protected void btnPaySalary_Click(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((Button)sender).Parent.Parent)
            {
                string emp_id = row.Cells[1].Text;
                string month = row.Cells[2].Text;
                string year = row.Cells[3].Text;
                string PayrollId = row.Cells[4].Text;

                Response.Redirect("SalaryPay.aspx?EmpId=" + emp_id.ToString() + "&y=" + year.ToString() + "&m=" + month.ToString() + "&UnewQWCSBQ=" + PayrollId.ToString());

            }
        }

        protected void CurrentSalary()
        {
            var employeeId = Request.QueryString["EmployeeId"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_hr_employee_current_salary", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@emp_id", employeeId);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        DetailsViewCurrentSalary.DataSource = dt;
                        if (dt.Rows.Count == 0)
                        {
                            DetailsViewCurrentSalary.Visible = false;
                            lblNoCurrentSalary.Visible = true;
                            //DetailsViewCurrentSalary.Visible = true;
                            //lblZeroQualifications.Text = "No Records found ";
                            btnSetSalary.Text = "Set Salary";
                            lblNoCurrentSalary.Text = "No Salary has been Set Up, Please setup salary ";
                        }
                        else
                        {
                            DetailsViewCurrentSalary.Visible = true;
                            DetailsViewCurrentSalary.DataBind();
                            btnSetSalary.Text = "Update Salary and Payroll";
                            //lblZeroQualifications.Visible = false;
                        }
                    }
                }
                cmd.ExecuteNonQuery();
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
        protected void BtnAddQualification_Click(object sender, EventArgs e)
        {
            var employeeId = Request.QueryString["EmployeeId"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_hr_emp_quals_add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                cmd.Parameters.AddWithValue("@emp_id", employeeId);
                cmd.Parameters.AddWithValue("@qualification", tbQualification.Text.ToString());
                cmd.Parameters.AddWithValue("@date_attained", tbDateAttained.Text.ToString());
                //cmd.Parameters.AddWithValue("@created_date", tbCreatedDate.Text.ToString());
                //cmd.Parameters.AddWithValue("@update_date", tbUpdatedDate.Text.ToString());
                cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                cmd.ExecuteNonQuery();
                QualificationBindData();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Record saved successfully ');", true);
                btnAddShow.Visible = true;
                divAddQualification.Visible = false;

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Cannot Add Duplicate Record');", true);          
            }
            finally
            {
                con.Close();
                ClearData(this);
            }
        }

        protected void QualificationBindData()
        {
            var employeeId = Request.QueryString["EmployeeId"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_hr_emp_quals_search", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@emp_id", employeeId);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        GridViewQualifications.DataSource = dt;
                        if (dt.Rows.Count == 0)
                        {
                            GridViewQualifications.Visible = false;
                            lblZeroQualifications.Visible = true;
                            GridViewQualifications.Visible = true;
                            lblZeroQualifications.Text = "No Records found ";
                            //lblSetupAcademicYear.Text = "No Academic Year Has Been Set Up Fill In Below To Create New Academic Year ";
                        }
                        else
                        {
                            GridViewQualifications.Visible = true;
                            GridViewQualifications.DataBind();
                            lblZeroQualifications.Visible = false;
                            //lblSetupAcademicYear.Visible = false;
                        }

                    }
                }
                cmd.ExecuteNonQuery();
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

        protected void GridViewQualifications_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewQualifications.EditIndex = e.NewEditIndex;
            QualificationBindData();
        }

        protected void GridViewQualifications_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GridViewQualifications.EditIndex = -1;
            QualificationBindData();
        }

        protected void GridViewQualifications_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_hr_emp_quals_edit", con);
                GridViewRow row = GridViewQualifications.Rows[e.RowIndex] as GridViewRow;
                TextBox txtQualification = row.FindControl("tbQualification") as TextBox;
                TextBox txtDateAttained = row.FindControl("tbDateAttained") as TextBox;

                cmd.Parameters.Add(new SqlParameter() { ParameterName = "q_id", Value = GridViewQualifications.Rows[e.RowIndex].Cells[0].Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "emp_id", Value = GridViewQualifications.Rows[e.RowIndex].Cells[1].Text });
                cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "qualification", Value = txtQualification.Text.ToString() });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "date_attained", Value = txtDateAttained.Text.ToString() });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                //con.Close();
                GridViewQualifications.EditIndex = -1;
                QualificationBindData();
            }
            catch //(Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "clentscript", "alert('Cannot update, Academic Year already exist');", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            }
            finally
            {
                con.Close();
            }
        }

        protected void GridViewQualifications_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var employeeId = Request.QueryString["EmployeeId"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_hr_emp_quals_delete", con);
                GridViewRow row = GridViewQualifications.Rows[e.RowIndex] as GridViewRow;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "q_id", Value = GridViewQualifications.Rows[e.RowIndex].Cells[0].Text });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                QualificationBindData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            }
            finally
            {
                con.Close();
                Response.Redirect("~/pages/employee_profile.aspx?EmployeeId=" + employeeId + "&action=Qlfc");
            }

        }

        protected void btnAddShow_Click(object sender, EventArgs e)
        {
            divAddQualification.Visible = true;
            btnAddShow.Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            divAddQualification.Visible = false;
            btnAddShow.Visible = true;
        }

        private void HideBtnEndEmployee()
        {
            try
            {
                if (DetailsViewProfile.Rows[4].Cells.Count != 0)
                {
                    Label txt = (Label)DetailsViewProfile.FindControl("lblEndDate");
                    if (txt.Text != DBNull.Value.ToString())
                    {
                        btnEndEmployee.Visible = false;
                    }
                }
            }
            catch { }

        }

        public void DetailsViewSalaryPayroll_ItemCreated(object sender, EventArgs e)
        {
            if (DetailsViewSalaryPayroll.CurrentMode == DetailsViewMode.ReadOnly)
            {
                int commandRowIndex = DetailsViewSalaryPayroll.Rows.Count - 1;
                if (commandRowIndex > 0)
                {
                    DetailsViewRow commandRow = DetailsViewSalaryPayroll.Rows[commandRowIndex];
                    DataControlFieldCell cell = (DataControlFieldCell)commandRow.Controls[0];
                    if (cell != null)
                    {
                        foreach (Control ctrl in cell.Controls)
                        {
                            if (ctrl is LinkButton)
                            {
                                if (((LinkButton)ctrl).CommandName == "New")
                                {
                                    ctrl.Visible = false;
                                }
                            }
                        }
                    }
                }
            }
            //TextBox txtMessageTitle = DetailsViewSalaryPayroll.FindControl("tbBankAccountNumber") as TextBox;

            //DropDownList ddlBankName = DetailsViewSalaryPayroll.FindControl("ddlBankName") as DropDownList;

            //string text = txtMessageTitle.Text;

            //string ddlBankName1 = ddlBankName.Text;

        }

        protected void DetailsViewSalaryPayroll_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
        {
            var employeeId = Request.QueryString["EmployeeId"];
            Response.Redirect("~/pages/employee_profile.aspx?EmployeeId=" + employeeId + "&action=payroll/14fbc9442fd6c984");
        }

        protected void DetailsViewSalaryPayroll_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("cancel", StringComparison.CurrentCultureIgnoreCase))
            {
                var employeeId = Request.QueryString["EmployeeId"];
                Response.Redirect("~/pages/employee_profile.aspx?EmployeeId=" + employeeId + "&action=payroll/14fbc9442fd6c984");
            }
        }

        protected void DetailsViewSalaryPayroll_DataBound(object sender, EventArgs e)
        {
            if (DetailsViewSalaryPayroll.Rows.Count == 0)
                DetailsViewSalaryPayroll.ChangeMode(DetailsViewMode.Insert);
        }

        protected void btnfill_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_hr_payroll_sttings_list_all", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
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
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "clentscript", "alert('Please Setup Payroll in settings to get Default Payroll');", true);
                            }
                            else
                            {
                                TextBox Rent = DetailsViewSalaryPayroll.FindControl("tbHousingAllowance") as TextBox;
                                Rent.Text = dt.Rows[0]["add_rent_allow_percent"].ToString();

                                TextBox Transport = DetailsViewSalaryPayroll.FindControl("tbTransportAllowance") as TextBox;
                                Transport.Text = dt.Rows[0]["add_transport_allow_percent"].ToString();


                                TextBox Medical = DetailsViewSalaryPayroll.FindControl("tbMedicalAllowance") as TextBox;
                                Medical.Text = dt.Rows[0]["add_medical_allow_percent"].ToString();
                                TextBox Meal = DetailsViewSalaryPayroll.FindControl("tbMealAllowance") as TextBox;
                                Meal.Text = dt.Rows[0]["add_meal_allow_percent"].ToString();
                                TextBox Utility = DetailsViewSalaryPayroll.FindControl("tbUtilityAllowance") as TextBox;
                                Utility.Text = dt.Rows[0]["add_utility_allow_percent"].ToString();
                                //TextBox Other = DetailsViewSalaryPayroll.FindControl("tbOtherAllowance") as TextBox;
                                //Other.Text = dt.Rows[0]["add_other_allow_percent"].ToString();
                                TextBox Tax = DetailsViewSalaryPayroll.FindControl("tbTaxDeduction") as TextBox;
                                Tax.Text = dt.Rows[0]["deduct_tax_percent"].ToString();
                                TextBox Pension = DetailsViewSalaryPayroll.FindControl("tbPensionDeduction") as TextBox;
                                Pension.Text = dt.Rows[0]["deduct_pesion_scheme_percent"].ToString();
                                //TextBox Welfare = DetailsViewSalaryPayroll.FindControl("tbWelfareDeduction") as TextBox;
                                //Welfare.Text = dt.Rows[0]["deduct_staff_welfare_percent"].ToString();
                                //TextBox Cooperative = DetailsViewSalaryPayroll.FindControl("tbCooperativeDeduction") as TextBox;
                                //Cooperative.Text = dt.Rows[0]["deduct_staff_coop_percent"].ToString();
                                //TextBox Loan = DetailsViewSalaryPayroll.FindControl("tbLoanDeduction") as TextBox;
                                //Loan.Text = dt.Rows[0]["deduct_staff_loan_percent"].ToString();
                                //TextBox OtherDedcution = DetailsViewSalaryPayroll.FindControl("tbOtherDeduction") as TextBox;
                                //OtherDedcution.Text = dt.Rows[0]["deduct_staff_others_percent"].ToString();
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

        protected void DetailsViewSalaryPayroll_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            var employeeId = Request.QueryString["EmployeeId"];
            Response.Redirect("~/pages/employee_profile.aspx?EmployeeId=" + employeeId + "&action=payroll/14fbc9442fd6c984");
        }

        protected void SqlDataSourceAddress_Deleted(object sender, SqlDataSourceStatusEventArgs e)
        {
            var employeeId = Request.QueryString["EmployeeId"];
            Response.Redirect("~/pages/employee_profile.aspx?EmployeeId=" + employeeId + "&action=add");
        }

        protected void GridViewPayrollBreakDown_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                GridViewRow row = GridViewPayrollBreakDown.Rows[e.RowIndex] as GridViewRow;
                string txtHrPayId = GridViewPayrollBreakDown.Rows[e.RowIndex].Cells[0].Text.ToString();
                SqlCommand cmd = new SqlCommand("sp_ms_hr_payment_delete", con);
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "hr_pay_id", Value = txtHrPayId }); //GridViewPayments.Rows[e.RowIndex].Cells[1].Text });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                PayrollBreakDown();
                Payroll();
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

        protected void btnViewPayslip_Click(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((Button)sender).Parent.Parent)
            {
                string emp_id = row.Cells[1].Text;
                // string month = row.Cells[2].Text;
                // string year = row.Cells[3].Text;
                string PayrollId = row.Cells[4].Text;
                // int iStID = int32.Parse(e.CommandArgument.ToString());

                Response.Redirect("employee_payslip.aspx?Id=" + emp_id.ToString() + "&UnewQWCSBQ=" + PayrollId.ToString());

            }
        }



        protected void DetailsViewSalaryPayroll_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            TextBox txtBankAcc = DetailsViewSalaryPayroll.FindControl("tbBankAccountNumber") as TextBox;

            DropDownList ddlBankName = DetailsViewSalaryPayroll.FindControl("ddlBankName") as DropDownList;

            int AccouuntLength = txtBankAcc.Text.Length;

            string ddlBankName1 = ddlBankName.SelectedItem.ToString();
            if (AccouuntLength > 1 && ddlBankName1 == "Select")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Please Select Bank')", true);
                e.Cancel = true;
            }
            if (AccouuntLength <= 0 && ddlBankName1 != "Select")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Bank Account number required')", true);
                e.Cancel = true;
            }

        }

        protected void DetailsViewSalaryPayroll_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            if (DetailsViewSalaryPayroll.Rows.Count != 0)
            {
                TextBox txtBankAcc = DetailsViewSalaryPayroll.FindControl("tbBankAccountNumber") as TextBox;

                DropDownList ddlBankName = DetailsViewSalaryPayroll.FindControl("ddlBankName") as DropDownList;

                int AccouuntLength = txtBankAcc.Text.Length;

                string ddlBankName1 = ddlBankName.SelectedItem.ToString();
                if (AccouuntLength > 1 && ddlBankName1 == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Please Select Bank')", true);
                    e.Cancel = true;
                }
                if (AccouuntLength <= 0 && ddlBankName1 != "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Bank Account number required')", true);
                    e.Cancel = true;
                }
            }
        }

        protected void DetailsViewAddress_ItemCreated(object sender, EventArgs e)
        {
            if (DetailsViewAddress.CurrentMode == DetailsViewMode.ReadOnly)
            {
                int commandRowIndex = DetailsViewAddress.Rows.Count - 1;
                if (commandRowIndex > 0)
                {
                    DetailsViewRow commandRow = DetailsViewAddress.Rows[commandRowIndex];
                    DataControlFieldCell cell = (DataControlFieldCell)commandRow.Controls[0];
                    if (cell != null)
                    {
                        foreach (Control ctrl in cell.Controls)
                        {
                            if (ctrl is LinkButton)
                            {
                                if (((LinkButton)ctrl).CommandName == "New")
                                {
                                    ctrl.Visible = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        private Boolean InsertUpdateData(SqlCommand cmd)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);

            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                return false;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
        protected void Upload_Click(object sender, EventArgs e)
        {
            var EmpId = Request.QueryString["EmployeeId"];
            // Read the file and convert it to Byte Array
            string filePath = FileUpload1.PostedFile.FileName;
            string filename = Path.GetFileName(filePath);
            string ext = Path.GetExtension(filename);
            string contenttype = String.Empty;

            //Set the contenttype based on File Extension
            switch (ext)
            {

                case ".jpg":
                    contenttype = "image/jpg";
                    break;
                case ".png":
                    contenttype = "image/png";
                    break;
                case ".gif":
                    contenttype = "image/gif";
                    break;
                case ".PNG":
                    contenttype = "image/PNG";
                    break;
                case ".jpeg":
                    contenttype = "image/jpeg";
                    break;
            }
            if (contenttype != String.Empty)
            {

                Stream fs = FileUpload1.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                Byte[] bytes = br.ReadBytes((Int32)fs.Length);

                //insert the file into database
                string strQuery = "update ms_hr_employees set picture=@picture where emp_id =" + EmpId;
                SqlCommand cmd = new SqlCommand(strQuery);
                // cmd.Parameters.Add("@upload_id", SqlDbType.Int).Value = DBNull.Value;
                cmd.Parameters.Add("@picture", SqlDbType.Binary).Value = bytes;
                InsertUpdateData(cmd);
                //lblStatus.ForeColor = System.Drawing.Color.Green;
                //lblStatus.Text = "Logo Uploaded Successfully";
                cmd.Connection.Close();
                cmd.Connection.Dispose();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Uploaded Successfully')", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('File format not recognised Upload JPEG/JPG/PNG/GIF formats')", true);
            }
        }
        //Payroll2------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void Payroll2()
        {
            var employeeId = Request.QueryString["EmployeeId"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_hr_bab_employee_payroll_year_list", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@emp_id", SqlDbType.VarChar).Value = employeeId;
                    cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = ddlYear2.SelectedItem.ToString();
                    //cmd.Parameters.AddWithValue("@year", ddlYear.SelectedItem.ToString());
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {

                            sda.Fill(dt);
                            GridViewPayroll2.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                lblZeroRecords2.Visible = true;
                                lblZeroRecords2.Text = "No Payment History";
                                GridViewPayroll2.Visible = false;
                                GridViewPayrollBreakDown2.Visible = false;
                            }
                            else
                            {
                                GridViewPayroll2.DataBind();
                                lblClickRecords2.Text = "Click on Month below For Payment Details";
                                lblClickRecords2.ForeColor = System.Drawing.Color.White;
                                lblClickRecords2.BackColor = System.Drawing.ColorTranslator.FromHtml("#18a689");
                                lblZeroRecords2.Visible = false;
                                GridViewPayroll2.Visible = true;
                                //GridViewPayrollBreakDown.Visible = true;
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


        private void PayrollBreakDown2()
        {
            var employeeId = Request.QueryString["EmployeeId"];
            var year = Request.QueryString["year"];
            var month = Request.QueryString["month"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_hr_employee_payroll_month_list_payments", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@emp_id", SqlDbType.VarChar).Value = employeeId;
                    cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = year;
                    cmd.Parameters.Add("@month", SqlDbType.VarChar).Value = month;
                    //cmd.Parameters.AddWithValue("@year", ddlYear.SelectedItem.ToString());
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {

                            sda.Fill(dt);
                            GridViewPayrollBreakDown2.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                lblZeroBreakDown2.Visible = true;
                                lblZeroBreakDown2.Text = "No Payment History";
                                GridViewPayrollBreakDown2.Visible = false;
                            }
                            else
                            {
                                GridViewPayrollBreakDown2.DataBind();
                                lblZeroBreakDown2.Visible = false;
                                GridViewPayrollBreakDown2.Visible = true;
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

        protected void BtnPayroll2_Click(object sender, EventArgs e)
        {
            PayrollTab2();
        }

        protected void PayrollTab2()
        {
            Payroll2();
            CurrentSalary2();
            ViewProfile.Visible = false;
            divProfilePic.Visible = false;
            ViewAddress.Visible = false;
            ViewPayroll.Visible = false;
            ViewPayroll2.Visible = true;
            divSetSalary.Visible = false;
            divSetSalary2.Visible = false;
            divEndDateEmployee.Visible = false;
            divQualifications.Visible = false;
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnPayroll2');", true);
        }

        private void PayrollBtn2()
        {
            PayrollBreakDown2();
            Payroll2();
            CurrentSalary2();
            ViewProfile.Visible = false;
            divProfilePic.Visible = false;
            ViewAddress.Visible = false;
            ViewPayroll.Visible = false;
            divSetSalary.Visible = false;
            ViewPayroll2.Visible = true;
            divSetSalary2.Visible = false;
            divEndDateEmployee.Visible = false;
            divQualifications.Visible = false;
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnPayroll2');", true);
        }


        protected void btnSetSalary2_Click(object sender, EventArgs e)
        {
            ViewAddress.Visible = false;
            ViewProfile.Visible = false;
            divProfilePic.Visible = false;
            ViewPayroll.Visible = false;
            divSetSalary.Visible = false;
            ViewPayroll2.Visible = false;
            divSetSalary2.Visible = true;
            divEndDateEmployee.Visible = false;
            divQualifications.Visible = false;
            DetailsViewSalaryPayroll2.DefaultMode = DetailsViewMode.Edit;
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(Page), "MenuSelection_" + UniqueID, "menuselection('#MainContent_BtnPayroll2');", true);
        }
        protected void btnPayrollByYear2_Click(object sender, EventArgs e)
        {
            Payroll2();
        }

        protected void btnPaySalary2_Click(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((Button)sender).Parent.Parent)
            {
                string emp_id = row.Cells[1].Text;
                string month = row.Cells[2].Text;
                string year = row.Cells[3].Text;
                string PayrollId = row.Cells[4].Text;

                Response.Redirect("SalaryPay.aspx?EmpId=" + emp_id.ToString() + "&y=" + year.ToString() + "&m=" + month.ToString() + "&UnewQWCSBQ=" + PayrollId.ToString());

            }
        }

        protected void CurrentSalary2()
        {
            var employeeId = Request.QueryString["EmployeeId"];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_hr_bab_employee_current_salary", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@emp_id", employeeId);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        DetailsViewCurrentSalary2.DataSource = dt;
                        if (dt.Rows.Count == 0)
                        {
                            DetailsViewCurrentSalary2.Visible = false;
                            lblNoCurrentSalary2.Visible = true;
                            //DetailsViewCurrentSalary.Visible = true;
                            //lblZeroQualifications.Text = "No Records found ";
                            btnSetSalary2.Text = "Set Salary";
                            lblNoCurrentSalary2.Text = "No Salary has been Set Up, Please setup salary ";
                        }
                        else
                        {
                            DetailsViewCurrentSalary2.Visible = true;
                            DetailsViewCurrentSalary2.DataBind();
                            btnSetSalary2.Text = "Update Salary and Payroll";
                            //lblZeroQualifications.Visible = false;
                        }
                    }
                }
                cmd.ExecuteNonQuery();
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

        public void DetailsViewSalaryPayroll2_ItemCreated(object sender, EventArgs e)
        {
            if (DetailsViewSalaryPayroll2.CurrentMode == DetailsViewMode.ReadOnly)
            {
                int commandRowIndex = DetailsViewSalaryPayroll2.Rows.Count - 1;
                if (commandRowIndex > 0)
                {
                    DetailsViewRow commandRow = DetailsViewSalaryPayroll2.Rows[commandRowIndex];
                    DataControlFieldCell cell = (DataControlFieldCell)commandRow.Controls[0];
                    if (cell != null)
                    {
                        foreach (Control ctrl in cell.Controls)
                        {
                            if (ctrl is LinkButton)
                            {
                                if (((LinkButton)ctrl).CommandName == "New")
                                {
                                    ctrl.Visible = false;
                                }
                            }
                        }
                    }
                }
            }
            //TextBox txtMessageTitle = DetailsViewSalaryPayroll.FindControl("tbBankAccountNumber") as TextBox;

            //DropDownList ddlBankName = DetailsViewSalaryPayroll.FindControl("ddlBankName") as DropDownList;

            //string text = txtMessageTitle.Text;

            //string ddlBankName1 = ddlBankName.Text;

        }

        protected void DetailsViewSalaryPayroll2_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
        {
            var employeeId = Request.QueryString["EmployeeId"];
            Response.Redirect("~/pages/employee_profile.aspx?EmployeeId=" + employeeId + "&action=payroll2/14fbc9442fd6c984");
        }

        protected void DetailsViewSalaryPayroll2_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("cancel", StringComparison.CurrentCultureIgnoreCase))
            {
                var employeeId = Request.QueryString["EmployeeId"];
                Response.Redirect("~/pages/employee_profile.aspx?EmployeeId=" + employeeId + "&action=payroll2/14fbc9442fd6c984");
            }
        }

        protected void DetailsViewSalaryPayroll2_DataBound(object sender, EventArgs e)
        {
            if (DetailsViewSalaryPayroll2.Rows.Count == 0)
                DetailsViewSalaryPayroll2.ChangeMode(DetailsViewMode.Insert);
        }

        protected void btnfill2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_hr_bab_payroll_sttings_list_all", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
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
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "clentscript", "alert('Please Setup Payroll in settings to get Default Payroll');", true);
                            }
                            else
                            {
                                TextBox Gross = DetailsViewSalaryPayroll2.FindControl("tbGross") as TextBox;
                                Gross.Text = dt.Rows[0]["add_gross"].ToString();

                                TextBox BasicSalary = DetailsViewSalaryPayroll2.FindControl("tbBasic") as TextBox;
                                BasicSalary.Text = dt.Rows[0]["add_basic_salary"].ToString();

                                TextBox CostOFLiving = DetailsViewSalaryPayroll2.FindControl("tbCostOfLiving") as TextBox;
                                CostOFLiving.Text = dt.Rows[0]["add_cost_of_living_allowance"].ToString();

                                TextBox Cola = DetailsViewSalaryPayroll2.FindControl("tbCola") as TextBox;
                                Cola.Text = dt.Rows[0]["add_cola"].ToString();

                                TextBox Enhance = DetailsViewSalaryPayroll2.FindControl("tbEnhance") as TextBox;
                                Enhance.Text = dt.Rows[0]["add_enhance"].ToString();

                                TextBox Transport = DetailsViewSalaryPayroll2.FindControl("tbTransport") as TextBox;
                                Transport.Text = dt.Rows[0]["add_transport"].ToString();

                                TextBox Productivity = DetailsViewSalaryPayroll2.FindControl("tbProductivity") as TextBox;
                                Productivity.Text = dt.Rows[0]["add_productivity"].ToString();

                                TextBox Responsibility = DetailsViewSalaryPayroll2.FindControl("tbResponsibility") as TextBox;
                                Responsibility.Text = dt.Rows[0]["add_responsibility"].ToString();

                                TextBox Housing = DetailsViewSalaryPayroll2.FindControl("tbHousing") as TextBox;
                                Housing.Text = dt.Rows[0]["add_housing"].ToString();

                                TextBox Personalized = DetailsViewSalaryPayroll2.FindControl("tbPersonalized") as TextBox;
                                Personalized.Text = dt.Rows[0]["add_personalized"].ToString();

                                TextBox ChildAllowance = DetailsViewSalaryPayroll2.FindControl("tbChild") as TextBox;
                                ChildAllowance.Text = dt.Rows[0]["add_child_allowance"].ToString();

                                TextBox Utility = DetailsViewSalaryPayroll2.FindControl("tbUtility") as TextBox;
                                Utility.Text = dt.Rows[0]["add_utility"].ToString();

                                TextBox OtherAllowance = DetailsViewSalaryPayroll2.FindControl("tbOtherAllowance") as TextBox;
                                OtherAllowance.Text = dt.Rows[0]["add_others"].ToString();

                                TextBox Payee = DetailsViewSalaryPayroll2.FindControl("tbPayee") as TextBox;
                                Payee.Text = dt.Rows[0]["deduct_paye"].ToString();

                                TextBox Tithe = DetailsViewSalaryPayroll2.FindControl("tbTithe") as TextBox;
                                Tithe.Text = dt.Rows[0]["deduct_tithe"].ToString();

                                TextBox Pencom = DetailsViewSalaryPayroll2.FindControl("tbPencom") as TextBox;
                                Pencom.Text = dt.Rows[0]["deduct_pencom"].ToString();

                                TextBox Cooperative = DetailsViewSalaryPayroll2.FindControl("tbCooperative") as TextBox;
                                Cooperative.Text = dt.Rows[0]["deduct_cooperative"].ToString();

                                TextBox Social = DetailsViewSalaryPayroll2.FindControl("tbSocial") as TextBox;
                                Social.Text = dt.Rows[0]["deduct_socials"].ToString();

                                TextBox PersonalAccount = DetailsViewSalaryPayroll2.FindControl("tbPersonalAccount") as TextBox;
                                PersonalAccount.Text = dt.Rows[0]["deduct_personal_account"].ToString();

                                TextBox Rent = DetailsViewSalaryPayroll2.FindControl("tbRent") as TextBox;
                                Rent.Text = dt.Rows[0]["deduct_rent_comeback"].ToString();

                                TextBox OtherDeductions = DetailsViewSalaryPayroll2.FindControl("tbOtherDeductions") as TextBox;
                                OtherDeductions.Text = dt.Rows[0]["deduct_others"].ToString();

                                TextBox CreatedBy = DetailsViewSalaryPayroll2.FindControl("tbCreated_by") as TextBox;
                                CreatedBy.Text = HttpContext.Current.User.Identity.Name;
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

        protected void DetailsViewSalaryPayroll2_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            var employeeId = Request.QueryString["EmployeeId"];
            Response.Redirect("~/pages/employee_profile.aspx?EmployeeId=" + employeeId + "&action=payroll2/14fbc9442fd6c984");
        }



        protected void GridViewPayrollBreakDown2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                GridViewRow row = GridViewPayrollBreakDown2.Rows[e.RowIndex] as GridViewRow;
                string txtHrPayId = GridViewPayrollBreakDown2.Rows[e.RowIndex].Cells[0].Text.ToString();
                SqlCommand cmd = new SqlCommand("sp_ms_hr_payment_delete", con);
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "hr_pay_id", Value = txtHrPayId }); //GridViewPayments.Rows[e.RowIndex].Cells[1].Text });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                PayrollBreakDown2();
                Payroll2();
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

        protected void btnViewPayslip2_Click(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((Button)sender).Parent.Parent)
            {
                string emp_id = row.Cells[1].Text;
                // string month = row.Cells[2].Text;
                // string year = row.Cells[3].Text;
                string PayrollId = row.Cells[4].Text;
                // int iStID = int32.Parse(e.CommandArgument.ToString());

                Response.Redirect("employee_payslip2.aspx?Id=" + emp_id.ToString() + "&UnewQWCSBQ=" + PayrollId.ToString());

            }
        }



        protected void DetailsViewSalaryPayroll2_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            TextBox txtBankAcc = DetailsViewSalaryPayroll2.FindControl("tbBankAccountNumber") as TextBox;

            DropDownList ddlBankName = DetailsViewSalaryPayroll2.FindControl("ddlBankName") as DropDownList;

            int AccouuntLength = txtBankAcc.Text.Length;

            string ddlBankName1 = ddlBankName.SelectedItem.ToString();
            if (AccouuntLength > 1 && ddlBankName1 == "Select")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Please Select Bank')", true);
                e.Cancel = true;
            }
            if (AccouuntLength <= 0 && ddlBankName1 != "Select")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Bank Account number required')", true);
                e.Cancel = true;
            }

        }

        protected void DetailsViewSalaryPayroll2_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            if (DetailsViewSalaryPayroll2.Rows.Count != 0)
            {
                TextBox txtBankAcc = DetailsViewSalaryPayroll2.FindControl("tbBankAccountNumber") as TextBox;

                DropDownList ddlBankName = DetailsViewSalaryPayroll2.FindControl("ddlBankName") as DropDownList;

                int AccouuntLength = txtBankAcc.Text.Length;

                string ddlBankName1 = ddlBankName.SelectedItem.ToString();
                if (AccouuntLength > 1 && ddlBankName1 == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Please Select Bank')", true);
                    e.Cancel = true;
                }
                if (AccouuntLength <= 0 && ddlBankName1 != "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Bank Account number required')", true);
                    e.Cancel = true;
                }
            }
        }
    }
}