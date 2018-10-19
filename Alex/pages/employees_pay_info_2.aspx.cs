using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
//using Alex.App_code;
using System.IO;
using ClosedXML.Excel;


namespace Alex.pages
{
    public partial class employees_pay_info_2 : System.Web.UI.Page
    {
        int lvl = 0;
        string selectedValue = string.Empty;
        private static string currentYear = DateTime.Now.Year.ToString();
        private static string currentmonth = DateTime.Now.ToString("MMMM");
        protected void Page_Load(object sender, EventArgs e)
        {
            Level();
            if (lvl == 1)
            {

                //ManageCookies.VerifyAuthentication();
                lblZeroRecords.Text = "";
                GridViewEmployee_payrollList.Visible = false;
                if (!Page.IsPostBack)
                {
                    btnRunDelete.Visible = true;
                    divdrop.Visible = true;
                    DropDownPayrollYear();
                    ddlYear.SelectedIndex = 1;
                    DropDownPayrollMonth();
                    //for (int year = 2012; year <= DateTime.Now.Year; year++)
                    //    ddlYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
                    //ddlYear.SelectedValue = currentYear;
                    //Month();
                    ddlMonth.SelectedValue = currentmonth;
                    Employee_payrollList();
                    GridViewSalaryPaid.Visible = false;
                    //PayrollCount();
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
                //lblUserName.Text = 

                lvl = (int)(Session["level_of_access"]);
                //lbllvl.Text =lvl.ToString();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            }

        }

        private void Month()
        {
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
            for (int i = 1; i < 13; i++)
            {
                ddlMonth.Items.Add(new System.Web.UI.WebControls.ListItem(info.GetMonthName(i), i.ToString()));
            }
        }

        public void DropDownPayrollMonth()
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
                ddlMonth.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select Month", ""));
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

        public void DropDownPayrollYear()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_bab_payroll_year_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlYear.DataSource = ddlValues;
            ddlYear.DataValueField = "year";
            ddlYear.DataTextField = "year";
            ddlYear.DataBind();
            ddlYear.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please select Year", ""));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        private void PayrollCount()
        {
            if (ddlMonth.SelectedValue == currentmonth)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_ms_payroll_allow_delete_check", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // cmd.Parameters.AddWithValue("@year", ddlYear.SelectedItem.ToString());
                    //cmd.Parameters.AddWithValue("@month", ddlMonth.SelectedItem.ToString());
                    btnRunDelete.Visible = true;
                    int Count = (Int32)cmd.ExecuteScalar();
                    if (Count == 1)
                    {
                        btnRunPayroll.Visible = false;
                        btnDeletePayroll.Visible = true;
                        btnPaySalaries.Visible = true;
                    }
                    else
                    {
                        btnRunPayroll.Visible = true;
                        btnDeletePayroll.Visible = false;
                        btnPaySalaries.Visible = false;
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
            else
            {
                btnRunDelete.Visible = false;
            }

        }
        private void Employee_payrollList()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_hr_bab_payroll_monthly_view_year_month", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@year", ddlYear.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@month", ddlMonth.SelectedItem.ToString());

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        GridViewEmployee_payrollList.DataSource = dt;
                        PayrollCount();
                        SalaryPaid();
                        lblDelete.Visible = false;
                        divBackPayroll.Visible = false;
                        if (dt.Rows.Count == 0)
                        {
                            lblZeroRecords.Text = "No Records found for the selected Year and Month because payroll has not been run !";
                            lblZeroRecords.ForeColor = System.Drawing.Color.Blue;
                            GridViewSalaryPaid.Visible = false;
                            divPayDeleteSalaries.Visible = false;
                            btnDownload.Visible = false;
                            //btnRunDelete.Visible = false;
                        }
                        else
                        {
                            GridViewEmployee_payrollList.DataBind();
                            //divGrid.Visible = true;
                            btnDownload.Visible = true;
                            GridViewEmployee_payrollList.Visible = true;
                            GridViewSalaryPaid.Visible = false;
                            divPayDeleteSalaries.Visible = true;
                            //btnRunDelete.Visible = true;
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GridViewEmployee_payrollList.ClientID + "', 400, 1170 , 42 ,true); </script>", false);
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
        //protected void btnSearchYear_Click(object sender, EventArgs e)
        //{
        //    Employee_payrollList();
        //}

        protected void GridViewEmployee_payrollList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hl = (HyperLink)e.Row.FindControl("HyperLinkEmployee");
                if (hl != null)
                {
                    DataRowView drv = (DataRowView)e.Row.DataItem;
                    string keyword = drv["emp_id"].ToString();

                    hl.NavigateUrl = "~/pages/employee_profile.aspx?EmployeeId=" + keyword;
                }
            }
        }



        protected void btnRunPayroll_Click(object sender, EventArgs e)
        {
            //SqlCommand sqlcmd = new SqlCommand("select month from ms_hr_payroll_runs_month ", con);

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_hr_bab_payroll_monthly_run", con);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        GridViewEmployee_payrollList.DataSource = dt;
                        GridViewEmployee_payrollList.DataBind();
                        divGrid.Visible = true;
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
                Employee_payrollList();
            }
        }

        //protected void GridViewEmployee_payrollList_DataBound(object sender, EventArgs e)
        //{
        //    if (GridViewEmployee_payrollList.Rows.Count != 0)
        //    {
        //        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        //        TableCell cell0 = new TableCell();
        //        cell0.Text = "";
        //        cell0.ColumnSpan = 2;
        //        row.Controls.Add(cell0);

        //        TableCell cell1 = new TableCell();
        //        cell1.Text = "Allowances";
        //        cell1.HorizontalAlign = HorizontalAlign.Center;
        //        cell1.ColumnSpan = 7;
        //        row.Controls.Add(cell1);

        //        TableCell cell2 = new TableCell();
        //        cell2.Text = "";
        //        cell2.ColumnSpan = 1;
        //        row.Controls.Add(cell2);

        //        TableCell cell3 = new TableCell();
        //        cell3.ColumnSpan = 7;
        //        cell3.Text = "Deductions";
        //        cell3.HorizontalAlign = HorizontalAlign.Center;
        //        row.Controls.Add(cell3);

        //        TableCell cell4 = new TableCell();
        //        cell4.ColumnSpan = 1;
        //        cell4.Text = "";
        //        cell4.HorizontalAlign = HorizontalAlign.Center;
        //        row.Controls.Add(cell4);

        //        row.BackColor = ColorTranslator.FromHtml("#283C4C");
        //        row.ForeColor = ColorTranslator.FromHtml("white");
        //        //GridViewEmployee_payrollList.Controls[0].Controls.AddAt(0, row);

        //        GridViewEmployee_payrollList.HeaderRow.Parent.Controls.AddAt(0, row);
        //    }
        //}

        protected void btnDeletePayroll_Click(object sender, EventArgs e)
        {
            PayrollDelete();
            //PayrollCount();
            // Employee_payrollList();

        }


        private void PayrollDelete()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_bab_payroll_last_run_delete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@year", ddlYear.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@month", ddlMonth.SelectedItem.ToString());
                cmd.ExecuteNonQuery();
                PayrollCount();
                Employee_payrollList();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("FK_ms_hr_payment_ms_hr_payroll"))
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot Delete Payroll, Salary has been paid for this month')", true);
                    lblDelete.Visible = true;
                    lblDelete.ForeColor = System.Drawing.Color.Red;
                    lblDelete.Text = "Cannot Delete Payroll, an employee's salary has been paid for this month." + "<br />" + "Please see the list below for employees salaries which have been paid.";
                    PayrollCount();
                    SalaryPaid();
                    btnRunDelete.Visible = false;
                    divdrop.Visible = false;
                    divBackPayroll.Visible = true;
                    divGrid.Visible = false;
                }
                else
                {
                    divGrid.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                    // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
                }
            }
            finally
            {
                con.Close();
            }
        }
        protected void GridViewSalaryPaid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hl = (HyperLink)e.Row.FindControl("HLEmployee");
                if (hl != null)
                {
                    DataRowView drv = (DataRowView)e.Row.DataItem;
                    string keyword = drv["emp_id"].ToString();

                    hl.NavigateUrl = "~/pages/employee_profile.aspx?EmployeeId=" + keyword;
                }
            }
        }


        private void SalaryPaid()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_payroll_paid_list", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@year", ddlYear.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@month", ddlMonth.SelectedItem.ToString());

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        GridViewSalaryPaid.DataSource = dt;

                        if (dt.Rows.Count == 0)
                        {
                            //lblZeroRecords.Text = "No Records found for the selected Year and Month";
                            btnDeleteSalaries.Visible = false;
                            btnPaySalaries.Visible = true;
                        }
                        else
                        {
                            GridViewSalaryPaid.DataBind();
                            GridViewSalaryPaid.Visible = true;
                            btnDeleteSalaries.Visible = true;
                            btnPaySalaries.Visible = false;
                            //divGrid.Visible = false;
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

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownPayrollMonth();
            //ddlMonth.SelectedValue = currentmonth;
            //Employee_payrollList();

        }

        protected void btnPaySalaries_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_hr_bab_payment_add_all", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@year", ddlYear.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@month", ddlMonth.SelectedItem.ToString());
                cmd.ExecuteNonQuery();
                btnPaySalaries.Visible = false;
                btnDeleteSalaries.Visible = true;
                Employee_payrollList();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Salaries Paid Successfully');", true);
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

        protected void btnDeleteSalaries_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_hr_payment_delete_all", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@year", ddlYear.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@month", ddlMonth.SelectedItem.ToString());
                cmd.ExecuteNonQuery();
                btnPaySalaries.Visible = true;
                btnDeleteSalaries.Visible = false;
                Employee_payrollList();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Salaries Deleted Successfully');", true);
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

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            Employee_payrollList();
        }


        //protected void btnDownload_Click(object sender, EventArgs e)
        //{
        //    Response.ContentType = "application/pdf";
        //    Response.AddHeader("content-disposition",
        //     "attachment;filename=GridViewExport.pdf");
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter hw = new HtmlTextWriter(sw);
        //   GridViewEmployee_payrollList.AllowPaging = false;
        //   GridViewEmployee_payrollList.DataBind();
        //   GridViewEmployee_payrollList.RenderControl(hw);
        //    StringReader sr = new StringReader(sw.ToString());
        //    Document pdfDoc = new Document(PageSize.A4, 88f, 88f, 10f, 10f);
        //    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //    pdfDoc.Open();
        //    htmlparser.Parse(sr);
        //    pdfDoc.Close();
        //    Response.Write(pdfDoc);
        //    Response.End();
        //}
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename= " + ddlMonth.SelectedItem.ToString() + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridViewEmployee_payrollList.AllowPaging = false;
                this.Employee_payrollList();
                GridViewEmployee_payrollList.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GridViewEmployee_payrollList.HeaderRow.Cells)
                {
                    cell.BackColor = GridViewEmployee_payrollList.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridViewEmployee_payrollList.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridViewEmployee_payrollList.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridViewEmployee_payrollList.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridViewEmployee_payrollList.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }

        }
    }
}