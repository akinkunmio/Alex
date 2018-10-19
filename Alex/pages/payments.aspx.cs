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
using Alex.App_code;


namespace Alex.pages
{
    public partial class payments : System.Web.UI.Page
    {
        int lvl = 0; 
        private static string currentmonth = DateTime.Now.Month.ToString();
        private static string currentYear = DateTime.Now.Year.ToString();
        private static string TodayDate = DateTime.Today.ToString("dd");
        string selectedValue = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Level(); 
            ManageCookies.VerifyAuthentication();
            Page.MaintainScrollPositionOnPostBack = true;
            if (lvl == 1 || lvl== 2|| lvl == 3)
            {
                if (!Page.IsPostBack)
                {
                    DropDownPaymentsYear(); if (ddlYear.Items.Count != 0) { 
                    DropDownPaymentMethod();
                    Month();
                    ddlMonth.SelectedValue = currentmonth;
                    ddlYear.SelectedValue = currentYear;
                    ddlYear.Items.Insert(0, new ListItem("Select Year", ""));
                    this.ViewState["CurrentNumber"] = TodayDate; 
                    this.GenerateNumbers();
                    BindGrid(this.ViewState["CurrentNumber"].ToString());
                    }
                }
            }
            else if ( lvl == 4 )
            {
                Response.Redirect("~/pages/ForbiddenAccess.aspx", false);
            }
            else
            {
                Response.Redirect("~/pages/logout.aspx", false);
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
        private void Month()
        {
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
            for (int i = 1; i < 13; i++)
            {
                ddlMonth.Items.Add(new ListItem(info.GetMonthName(i), i.ToString()));
            }
           ddlMonth.Items.Insert(0, new ListItem("Select Month", ""));
        }

        public void DropDownPaymentsYear()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_person_school_fee_payments_year_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlYear.DataSource = ddlValues;
            ddlYear.DataValueField = "year_Payment_Received_Date";
            ddlYear.DataTextField = "year_Payment_Received_Date";
            ddlYear.DataBind();

            //ddlFeeSetupYear.SelectedValue = yearFormat;

            //Adding "Please select" option in dropdownlist for validation
            //ddlYear.Items.Insert(0, new ListItem("Please select Year", ""));

            cmd.Connection.Close();
            cmd.Connection.Dispose();
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

        private void BindGrid(string StartAlpha)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                string SP_Name = "sp_ms_person_school_fee_payments_list_month_year_date_filter";
                if (StartAlpha == "ALL")
                {
                    SP_Name = "sp_ms_person_school_fee_payments_list_month_year";
                }
                using (SqlCommand cmd = new SqlCommand(SP_Name, con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        if (StartAlpha != "ALL")
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            con.Open();
                            cmd.Parameters.AddWithValue("@received_month", ddlMonth.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@received_year", ddlYear.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@payment_method", ddlPaymentMethod.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@received_date", StartAlpha);
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                GridViewPayments.DataSource = dt;
                                lblDateSelected.Text = this.ViewState["CurrentNumber"].ToString() + "/" + ddlMonth.SelectedValue + "/" + ddlYear.SelectedValue;
                                if (dt.Rows.Count == 0)
                                {
                                    GridViewPayments.Visible = false;
                                    lblZeroRecords.Visible = true;
                                    lblZeroRecords.Text = "No Payments found ";
                                }
                                else
                                {
                                    GridViewPayments.Visible = true;
                                    GridViewPayments.DataBind();
                                    lblZeroRecords.Visible = false;
                                }
                            }
                        }
                        else
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            con.Open();
                            cmd.Parameters.AddWithValue("@received_month", ddlMonth.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@received_year", ddlYear.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@payment_method", ddlPaymentMethod.SelectedItem.ToString());
                            //cmd.Parameters.AddWithValue("@received_date", StartAlpha);
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                GridViewPayments.DataSource = dt;
                                lblDateSelected.Text = this.ViewState["CurrentNumber"].ToString();
                                if (dt.Rows.Count == 0)
                                {
                                    GridViewPayments.Visible = false;
                                    lblZeroRecords.Visible = true;
                                    lblZeroRecords.Text = "No Payments found ";
                                }
                                else
                                {
                                    GridViewPayments.Visible = true;
                                    GridViewPayments.DataBind();
                                    lblZeroRecords.Visible = false;
                                }
                            }
                        }
                   
                    } cmd.ExecuteNonQuery();
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

        //protected void PaymentsBindData()
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("sp_ms_person_school_fee_payments_list_month_year", con);
        //        cmd.Parameters.AddWithValue("@received_month", ddlMonth.SelectedItem.ToString());
        //        cmd.Parameters.AddWithValue("@received_year", ddlYear.SelectedItem.ToString());
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        using (SqlDataAdapter sda = new SqlDataAdapter())
        //        {
        //            con.Open();
        //            cmd.Connection = con;
        //            sda.SelectCommand = cmd;
        //            using (DataTable dt = new DataTable())
        //            {

        //                sda.Fill(dt);
        //                GridViewPayments.DataSource = dt;
        //                if (dt.Rows.Count == 0)
        //                {
        //                    GridViewPayments.Visible = false;
        //                    lblZeroRecords.Visible = true;
        //                    lblZeroRecords.Text = "No Payments found ";
        //                }
        //                else
        //                {
        //                    GridViewPayments.Visible = true;
        //                    GridViewPayments.DataBind();
        //                    lblZeroRecords.Visible = false;
        //                }
                       
        //            }
        //        }


        //        cmd.ExecuteNonQuery();
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



        protected void GridViewPayments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hl = (HyperLink)e.Row.FindControl("HyperLinkPeople");
                if (hl != null)
                {
                    DataRowView drv = (DataRowView)e.Row.DataItem;
                    string keyword = drv["person_id"].ToString();
                    hl.NavigateUrl = "~/pages/profile.aspx?PersonId=" + keyword;
                }
            }
        }

        protected void GridViewPayments_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewPayments.EditIndex = e.NewEditIndex;
            Session.Add("RowPostion", e.NewEditIndex.ToString());
            if (TbSearch.Text != null && TbSearch.Text != "")
            {
                Search();
            }
            else
            {
                //PaymentsBindData();
                BindGrid(this.ViewState["CurrentNumber"].ToString());
            }
        }

        protected void GridViewPayments_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GridViewPayments.EditIndex = -1;
            if (TbSearch.Text != null && TbSearch.Text != "")
            {
                Search();
            }
            else
            {
                //PaymentsBindData();
                BindGrid(this.ViewState["CurrentNumber"].ToString());
            }
        }

        protected void GridViewPayments_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var timeStamp =  string.Format("{0:HH:mm:ss tt}", DateTime.Now);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_person_school_fee_payments_edit", con);
                GridViewRow row = GridViewPayments.Rows[e.RowIndex] as GridViewRow;
                Label lblPaymentId = row.FindControl("lblPaymentId") as Label;
                TextBox txtAmount = row.FindControl("tbAmount") as TextBox;
                TextBox txtDate = row.FindControl("tbPaymentReceivedDate") as TextBox;
                var dtime = (txtDate.Text +' '+ timeStamp).ToString();
                DropDownList ddlPayMethod = row.FindControl("ddlPaymentMethod") as DropDownList;
                TextBox txtReference = row.FindControl("tbPaymentReference") as TextBox;
                DropDownList txtBankName = row.FindControl("ddlBankName") as DropDownList;
                TextBox txtInvoice = row.FindControl("tbInvoiceNo") as TextBox;
                TextBox txtReceipt = row.FindControl("tbReceiptNo") as TextBox;
                string UpdatedBy = HttpContext.Current.User.Identity.Name;
                string Feetype = row.Cells[4].Text;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "payment_id", Value = lblPaymentId.Text }); //GridViewPayments.Rows[e.RowIndex].Cells[1].Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "amount", Value = txtAmount.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "received_date", Value = dtime });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "payment_method", Value = ddlPayMethod.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "payment_method_ref", Value = txtReference.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "bank_name", Value = txtBankName.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "invoice_no", Value = txtInvoice.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "teller_no", Value = txtReceipt.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "updated_by", Value = UpdatedBy });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "fee_type", Value = Feetype });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
                GridViewPayments.EditIndex = -1;
                if (TbSearch.Text != null && TbSearch.Text != "")
                {
                    Search();
                }
                else
                {
                    //PaymentsBindData();
                    BindGrid(this.ViewState["CurrentNumber"].ToString());
                }
            }
            catch (Exception ex)
            {

                 ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            }
        }

        protected void GridViewPayments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                GridViewRow row = GridViewPayments.Rows[e.RowIndex] as GridViewRow;
                Label lblPaymentId = row.FindControl("lblPaymentId") as Label;
                string Feetype = row.Cells[4].Text;
                SqlCommand cmd = new SqlCommand("sp_ms_person_school_fee_payments_delete", con);
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "payment_id", Value = lblPaymentId.Text }); //GridViewPayments.Rows[e.RowIndex].Cells[1].Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "fee_type", Value = Feetype });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
               // PaymentsBindData();
                BindGrid(this.ViewState["CurrentNumber"].ToString());
            }
            catch (Exception ex)
            {
               ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            }
        }

        //protected void btnSearchPayments_Click(object sender, EventArgs e)
        //{
        //    divNumber.Visible = true; 
        //    BindGrid(this.ViewState["CurrentNumber"].ToString());
        //}

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            divNumber.Visible = false; 
            Search();
           // ClearData(this);
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

        private void Search()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_person_school_fee_payments_search", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@search", SqlDbType.VarChar).Value = TbSearch.Text;
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    GridViewPayments.Visible = true;
                    //this.GridViewStudents.Columns[3].Visible = true;
                    GridViewPayments.DataSource = dt;
                    GridViewPayments.DataBind();

                }
                else
                {
                    GridViewPayments.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('No Results Found ');", true);
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

        protected void GridViewPayments_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPayments.PageIndex = e.NewPageIndex;
           // PaymentsBindData();
            BindGrid(this.ViewState["CurrentNumber"].ToString());
        }

        private void GenerateNumbers()
        {
            try
            {
                List<ListItem> NumbersList = new List<ListItem>();
                ListItem Number = new ListItem();
                Number.Value = "ALL";
                Number.Selected = Number.Value.Equals(ViewState["CurrentNumber"]);
                NumbersList.Add(Number);
                int year = Convert.ToInt32(ddlYear.SelectedValue);
                int days = DateTime.DaysInMonth(year, ddlMonth.SelectedIndex);
                for (int i = 1; i <= days; i++)
                {
                    Number = new ListItem();
                    Number.Value = i.ToString("D2");
                    Number.Selected = Number.Value.Equals(ViewState["CurrentNumber"]);
                    NumbersList.Add(Number);
                }
                rptNumbers.DataSource = NumbersList;
                rptNumbers.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            }
        }

        protected void Number_Click(object sender, EventArgs e)
        {
            if (this.ViewState["CurrentNumber"] != null)
            {
                LinkButton lnkNumber = (LinkButton)sender;
                ViewState["CurrentNumber"] = lnkNumber.Text;
                string StartAlpha = lnkNumber.Text;
                this.GenerateNumbers();
                BindGrid(this.ViewState["CurrentNumber"].ToString());
            }
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            divNumber.Visible = true;
            ViewState["CurrentNumber"] = "01".ToString();
            string StartAlpha = "01".ToString();
            this.GenerateNumbers();
            BindGrid(this.ViewState["CurrentNumber"].ToString());
        }

        protected void ddlPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            divNumber.Visible = true;
            //ViewState["CurrentNumber"] = "01".ToString();
            //string StartAlpha = "01".ToString();
            //this.GenerateNumbers();
            BindGrid(this.ViewState["CurrentNumber"].ToString());
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            divNumber.Visible = true;
            BindGrid(this.ViewState["CurrentNumber"].ToString());
        }

    } 
}