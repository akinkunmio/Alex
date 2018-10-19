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

namespace Alex.pages.admin
{
    public partial class export_data : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnCurrentProfile_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_download_all_current_students_with_address"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt, "iQ Current Profiles");

                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=Current_Profiles.xlsx");
                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    Response.End();
                                }
                            }
                        }
                    }
                }
            }
        }

        //private void ProfileBind()
        //{
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
        //        try
        //        {
        //            using (SqlCommand cmd = new SqlCommand("sp_ms_download_all_profiles_with_address", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                using (SqlDataAdapter sda = new SqlDataAdapter())
        //                {
        //                    con.Open();
        //                    cmd.Connection = con;
        //                    sda.SelectCommand = cmd;
        //                    using (DataTable dt = new DataTable())
        //                    {
        //                        sda.Fill(dt);
        //                        GridViewProfile.DataSource = dt;
        //                        GridViewProfile.DataBind();
        //                    }
        //                }

        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //        }

        //        finally
        //        {
        //            con.Close();
        //        }
        //}
        protected void btnProfile_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_download_all_profiles_with_address"))
                {
                    cmd.CommandType = CommandType.StoredProcedure; 
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt, "iQ Profiles");

                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=Profiles.xlsx");
                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    Response.End();
                                }
                            }
                        }
                    }
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        //private void RegistrationsBind()
        //{
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
        //        try
        //        {
        //            using (SqlCommand cmd = new SqlCommand("sp_ms_download_registrations_list_all", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                using (SqlDataAdapter sda = new SqlDataAdapter())
        //                {
        //                    con.Open();
        //                    cmd.Connection = con;
        //                    sda.SelectCommand = cmd;
        //                    using (DataTable dt = new DataTable())
        //                    {
        //                        sda.Fill(dt);
        //                        GridViewReg.DataSource = dt;
        //                        GridViewReg.DataBind();
        //                    }
        //                }

        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //        }

        //        finally
        //        {
        //            con.Close();
        //        }
        //}
        protected void btnReg_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_download_registrations_list_all"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt, "IQ Registrations");

                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=Registrations.xlsx");
                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    Response.End();
                                }
                            }
                        }
                    }
                }
            }
        }

        //private void AppBind()
        //{
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
        //        try
        //        {
        //            using (SqlCommand cmd = new SqlCommand("sp_ms_download_applications_list_all", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                using (SqlDataAdapter sda = new SqlDataAdapter())
        //                {
        //                    con.Open();
        //                    cmd.Connection = con;
        //                    sda.SelectCommand = cmd;
        //                    using (DataTable dt = new DataTable())
        //                    {
        //                        sda.Fill(dt);
        //                        GridViewApp.DataSource = dt;
        //                        GridViewApp.DataBind();
        //                    }
        //                }

        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //        }

        //        finally
        //        {
        //            con.Close();
        //        }
        //}
        protected void btnApp_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_download_applications_list_all"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt, "IQ Admissions");

                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=Admissions.xlsx");
                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    Response.End();
                                }
                            }
                        }
                    }
                }
            }
        }

        //private void ExpBind()
        //{
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
        //        try
        //        {
        //            using (SqlCommand cmd = new SqlCommand("sp_ms_download_list_all_expenses", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                using (SqlDataAdapter sda = new SqlDataAdapter())
        //                {
        //                    con.Open();
        //                    cmd.Connection = con;
        //                    sda.SelectCommand = cmd;
        //                    using (DataTable dt = new DataTable())
        //                    {
        //                        sda.Fill(dt);
        //                        GridViewExp.DataSource = dt;
        //                        GridViewExp.DataBind();
        //                    }
        //                }

        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //        }

        //        finally
        //        {
        //            con.Close();
        //        }
        //}
        protected void btnExp_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_download_list_all_expenses"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt, "iQ Expenses");

                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=Expenses.xlsx");
                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    Response.End();
                                }
                            }
                        }
                    }
                }
            }
        }

        //private void PurchasesBind()
        //{
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
        //        try
        //        {
        //            using (SqlCommand cmd = new SqlCommand("sp_ms_download_all_purchases_sold_Items", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                using (SqlDataAdapter sda = new SqlDataAdapter())
        //                {
        //                    con.Open();
        //                    cmd.Connection = con;
        //                    sda.SelectCommand = cmd;
        //                    using (DataTable dt = new DataTable())
        //                    {
        //                        sda.Fill(dt);
        //                        GridViewPurchases.DataSource = dt;
        //                        GridViewPurchases.DataBind();
        //                    }
        //                }

        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //        }

        //        finally
        //        {
        //            con.Close();
        //        }
        //}
        protected void btnPurchases_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_download_all_purchases_sold_Items"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt, "iQ Purchases");

                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=Purchases_SoldItems.xlsx");
                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    Response.End();
                                }
                            }
                        }
                    }
                }
            }
        }

        //private void FeeBind()
        //{
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
        //        try
        //        {
        //            using (SqlCommand cmd = new SqlCommand("sp_ms_download_all_school_fees_payments", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                using (SqlDataAdapter sda = new SqlDataAdapter())
        //                {
        //                    con.Open();
        //                    cmd.Connection = con;
        //                    sda.SelectCommand = cmd;
        //                    using (DataTable dt = new DataTable())
        //                    {
        //                        sda.Fill(dt);
        //                        GridViewFee.DataSource = dt;
        //                        GridViewFee.DataBind();
        //                    }
        //                }

        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //        }

        //        finally
        //        {
        //            con.Close();
        //        }
        //}
        protected void btnFee_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_download_all_school_fees_payments"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt, "iQ Fee Payments");

                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=Fee Payments.xlsx");
                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    Response.End();
                                }
                            }
                        }
                    }
                }
            }

        }

        //private void SalaryBind()
        //{
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
        //        try
        //        {
        //            using (SqlCommand cmd = new SqlCommand("sp_ms_download_hr_employees_salary", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                using (SqlDataAdapter sda = new SqlDataAdapter())
        //                {
        //                    con.Open();
        //                    cmd.Connection = con;
        //                    sda.SelectCommand = cmd;
        //                    using (DataTable dt = new DataTable())
        //                    {
        //                        sda.Fill(dt);
        //                        GridViewSalary.DataSource = dt;
        //                        GridViewSalary.DataBind();
        //                    }
        //                }

        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //        }

        //        finally
        //        {
        //            con.Close();
        //        }
        //}
        protected void btnSalary_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_download_hr_employees_salary"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt, "iQ Employees Salary");

                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=Employees Salary.xlsx");
                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    Response.End();
                                }
                            }
                        }
                    }
                }
            }
        }

        //private void AttendanceBind()
        //{
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
        //        try
        //        {
        //            using (SqlCommand cmd = new SqlCommand("sp_ms_download_all_attendance", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                using (SqlDataAdapter sda = new SqlDataAdapter())
        //                {
        //                    con.Open();
        //                    cmd.Connection = con;
        //                    sda.SelectCommand = cmd;
        //                    using (DataTable dt = new DataTable())
        //                    {
        //                        sda.Fill(dt);
        //                        GridViewAttendance.DataSource = dt;
        //                        GridViewAttendance.DataBind();
        //                    }
        //                }

        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //        }

        //        finally
        //        {
        //            con.Close();
        //        }
        //}
        protected void btnAttendance_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_download_all_attendance"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt, "iQ Attendance");

                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=Attendance.xlsx");
                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    Response.End();
                                }
                            }
                        }
                    }
                }
            }

        }

        //private void AssessmentsBind()
        //{
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
        //        try
        //        {
        //            using (SqlCommand cmd = new SqlCommand("sp_ms_download_all_assessment", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                using (SqlDataAdapter sda = new SqlDataAdapter())
        //                {
        //                    con.Open();
        //                    cmd.Connection = con;
        //                    sda.SelectCommand = cmd;
        //                    using (DataTable dt = new DataTable())
        //                    {
        //                        sda.Fill(dt);
        //                        GridViewAssessments.DataSource = dt;
        //                        GridViewAssessments.DataBind();
        //                    }
        //                }

        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //        }

        //        finally
        //        {
        //            con.Close();
        //        }
        //}
        protected void btnAssess_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_download_all_assessment"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt, "iQ Assessments");

                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=Assessments.xlsx");
                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    Response.End();
                                }
                            }
                        }
                    }
                }
            }
        }

        //private void  EmployeesBind()
        //{
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
        //        try
        //        {
        //            using (SqlCommand cmd = new SqlCommand("sp_ms_download_all_profiles_with_address", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                using (SqlDataAdapter sda = new SqlDataAdapter())
        //                {
        //                    con.Open();
        //                    cmd.Connection = con;
        //                    sda.SelectCommand = cmd;
        //                    using (DataTable dt = new DataTable())
        //                    {
        //                        sda.Fill(dt);
        //                        GridViewEmp.DataSource = dt;
        //                        GridViewEmp.DataBind();
        //                    }
        //                }

        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //        }

        //        finally
        //        {
        //            con.Close();
        //        }
        //}
        protected void btnEmp_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_download_all_employees_and_address"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt, "iQ Employees");

                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=Employees.xlsx");
                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    Response.End();
                                }
                            }
                        }
                    }
                }
            }

        }

        //private void PayrollBind()
        //{
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
        //        try
        //        {
        //            using (SqlCommand cmd = new SqlCommand("sp_ms_download_all_payroll", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                using (SqlDataAdapter sda = new SqlDataAdapter())
        //                {
        //                    con.Open();
        //                    cmd.Connection = con;
        //                    sda.SelectCommand = cmd;
        //                    using (DataTable dt = new DataTable())
        //                    {
        //                        sda.Fill(dt);
        //                        GridViewPayroll.DataSource = dt;
        //                        GridViewPayroll.DataBind();
        //                    }
        //                }

        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //        }

        //        finally
        //        {
        //            con.Close();
        //        }
        //}
        protected void btnPayroll_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_download_all_payroll"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt, "iQ Payroll ");

                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=Payroll.xlsx");
                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    Response.End();
                                }
                            }
                        }
                    }
                }
            }

        }
        //protected void btnPayroll_Click(object sender, EventArgs e)
        //{
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.AddHeader("content-disposition", "attachment;filename=Payroll.xls");
        //    Response.Charset = "";
        //    Response.ContentType = "application/vnd.ms-excel";
        //    using (StringWriter sw = new StringWriter())
        //    {
        //        HtmlTextWriter hw = new HtmlTextWriter(sw);

        //        //To Export all pages
        //        GridViewPayroll.AllowPaging = false;
        //        this.PayrollBind();

        //        GridViewPayroll.HeaderRow.BackColor = Color.White;
        //        foreach (TableCell cell in GridViewPayroll.HeaderRow.Cells)
        //        {
        //            cell.BackColor = GridViewPayroll.HeaderStyle.BackColor;
        //        }
        //        foreach (GridViewRow row in GridViewPayroll.Rows)
        //        {
        //            row.BackColor = Color.White;
        //            foreach (TableCell cell in row.Cells)
        //            {
        //                if (row.RowIndex % 2 == 0)
        //                {
        //                    cell.BackColor = GridViewPayroll.AlternatingRowStyle.BackColor;
        //                }
        //                else
        //                {
        //                    cell.BackColor = GridViewPayroll.RowStyle.BackColor;
        //                }
        //                cell.CssClass = "textmode";
        //            }
        //        }

        //        GridViewPayroll.RenderControl(hw);

        //        //style to format numbers to string
        //        string style = @"<style> .textmode { } </style>";
        //        Response.Write(style);
        //        Response.Output.Write(sw.ToString());
        //        Response.Flush();
        //        Response.End();
        //    }

        //}
    }
}