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
using ClosedXML.Excel;
using System.Drawing;


namespace Alex.pages.student_reports
{
    public partial class student_payments_selected_dates : System.Web.UI.Page
    {
        int lvl = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            Level();
            if (lvl == 1)
            {
               if (!Page.IsPostBack)
                {
                    DropDownPaymentMethod();
                   DropDownBankName();
                    DateTime today = DateTime.Today;
                    DateTime TwentyEightDaysEarlier = today.AddDays(-28);
                    string TodayDate = DateTime.Today.ToString("dd-MM-yyyy");
                    tbStartDate.Text = TwentyEightDaysEarlier.ToString("dd-MM-yyyy");
                    tbEndDate.Text = TodayDate;
                    SchoolBind();
                    ddlPaymentMethod.SelectedIndex = 0;
                    StudentPayments();

                }
            }
            else if (lvl == 2 || lvl == 3 || lvl == 4 || lvl == 5)
            {
                Response.Redirect("~/pages/ForbiddenAccess.aspx", false);
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
       
        protected void BtnSearchFeePayments_Click(object sender, EventArgs e)
        {
            StudentPayments();
        }

        public void DropDownPaymentMethod()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_payment_method_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlPaymentMethod.DataSource = ddlValues;
            ddlPaymentMethod.DataValueField = "payment_method";
            ddlPaymentMethod.DataTextField = "payment_method";
            ddlPaymentMethod.DataBind();

            //Adding "Please select" option in dropdownlist for validation
            ddlPaymentMethod.Items.Insert(0, new ListItem("All", ""));

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }


        public void DropDownBankName()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_status_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@category", "Bank");
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlBankName.DataSource = ddlValues;
            ddlBankName.DataValueField = "status_name";
            ddlBankName.DataTextField = "status_name";
            ddlBankName.DataBind();
            ddlBankName.Items.Insert(0, new ListItem("Please select Bank Name", ""));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
        private void StudentPayments()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_rep_person_school_fee_payments_list_date_filter", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (ddlBankName.SelectedItem.Text == "Please select Bank Name") { cmd.Parameters.Add("@bank_name ", SqlDbType.VarChar).Value = DBNull.Value; bank.Visible = false; }
                    else { cmd.Parameters.Add("@bank_name ", SqlDbType.VarChar).Value = ddlBankName.SelectedItem.ToString(); }
                    cmd.Parameters.Add("@payment_method ", SqlDbType.VarChar).Value = ddlPaymentMethod.SelectedItem.ToString();
                    cmd.Parameters.Add("@pay_date_start", SqlDbType.VarChar).Value = tbStartDate.Text.ToString();
                    cmd.Parameters.Add("@pay_date_end", SqlDbType.VarChar).Value = tbEndDate.Text.ToString();
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {

                            sda.Fill(dt);
                            GridViewFeePayments.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                DivStudents.Visible = false; 
                                lblZeroRecords.Visible = true;
                                lblZeroRecords.Text = "No Payments Found ";
                                GridViewFeePayments.Visible = false;
                                btnPrint.Visible = false;
                                btnExcelDownload.Visible = false;
                            }
                            else
                            {
                                DivStudents.Visible = true;
                                GridViewFeePayments.Visible = true;
                                imgSchool.ImageUrl = "~/pages/ImageHandler.ashx?roll_no=1";
                                lblDateSelected.Text = tbStartDate.Text.ToString();
                                lblEndDateSelected.Text = tbEndDate.Text.ToString();
                                lblMethodSelected.Text = ddlPaymentMethod.SelectedItem.Text;
                                lblZeroRecords.Visible = false;
                                if (ddlPaymentMethod.SelectedItem.Text != "Bank") { GridViewFeePayments.Columns[11].Visible = false; } else { GridViewFeePayments.Columns[11].Visible = true; }
                                if (ddlPaymentMethod.SelectedItem.Text != "All") { GridViewFeePayments.Columns[10].Visible = false; } else { GridViewFeePayments.Columns[10].Visible = true; }
                                GridViewFeePayments.DataBind();
                                btnExcelDownload.Visible = true;
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
                bank.Visible = true;
            }
        }

        public void SchoolBind()
        {
            SqlDataAdapter adp = new SqlDataAdapter();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select school_name from ms_school", con);
                cmd.Dispose();
                SqlDataReader dr = cmd.ExecuteReader();
                string SchoolName = string.Empty;
                while (dr.Read())
                {
                    SchoolName = dr["school_name"].ToString();
                    lblName.Text = SchoolName.ToString();
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

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btnExcelDownload_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename= " + ddlPaymentMethod.SelectedItem.ToString()+" " + "from" + tbStartDate.Text.ToString() + "to" + tbEndDate.Text.ToString() + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridViewFeePayments.AllowPaging = false;
                this.StudentPayments();
                GridViewFeePayments.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GridViewFeePayments.HeaderRow.Cells)
                {
                    cell.BackColor = GridViewFeePayments.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridViewFeePayments.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridViewFeePayments.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridViewFeePayments.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridViewFeePayments.RenderControl(hw);

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