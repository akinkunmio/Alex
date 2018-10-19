using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alex.App_code;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Configuration;

namespace Alex.pages
{
    public partial class verify_payments : System.Web.UI.Page
    {
        int lvl = 0; 
        protected void Page_Load(object sender, EventArgs e)
        {
             Level(); 
            ManageCookies.VerifyAuthentication();
            Page.MaintainScrollPositionOnPostBack = true;
            if (lvl == 1 || lvl== 2)
            {
                if (!Page.IsPostBack)
                {
                    DropDownBank();
                    ddlPaymentMethod.SelectedValue = "All";
                    if (ddlPaymentMethod.SelectedValue == "Bank")
                    {
                        ddlBankName.SelectedIndex = 0; bank.Visible = true;
                    }
                    else { bank.Visible = false; }
                    DateTime today = DateTime.Today;
                    DateTime TwentyEightDaysEarlier = today.AddDays(-28);
                    string TodayDate = DateTime.Today.ToString("dd-MM-yyyy");
                    tbStartDate.Text = TwentyEightDaysEarlier.ToString("dd-MM-yyyy");
                    tbEndDate.Text = TodayDate;
                    BindGrid();
                }
            }
            else if (lvl == 3 || lvl == 4 || lvl == 5)
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

        public void DropDownBank()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT status_name FROM ms_status Where category = 'Bank'", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();
            ddlBankName.DataSource = ddlValues;
            ddlBankName.DataValueField = "status_name";
            ddlBankName.DataTextField = "status_name";
            ddlBankName.DataBind();
            ddlBankName.Items.Insert(0, new ListItem("Please select Bank", ""));
            ddlBankName.Items.Insert(1, new ListItem("All", "All"));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
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

        private void BindGrid()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_payments_list_all_bankname_v2", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status ", ddlStatus.SelectedItem.ToString());
                   
                    cmd.Parameters.Add("@payment_method ", SqlDbType.VarChar).Value = ddlPaymentMethod.SelectedItem.ToString();
                    cmd.Parameters.Add("@pay_date_start", SqlDbType.VarChar).Value = tbStartDate.Text.ToString();
                    cmd.Parameters.Add("@pay_date_end", SqlDbType.VarChar).Value = tbEndDate.Text.ToString();
                    cmd.Parameters.Add("@bank_name ", SqlDbType.VarChar).Value = ddlBankName.SelectedItem.ToString(); 
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;

                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridViewBnkPayVerify.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                divProcessNow.Visible = false;
                                GridViewBnkPayVerify.Visible = false;
                                lblZeroRecords.Visible = true;
                                lblZeroRecords.Text = "No Records found ";
                            }
                            else
                            {
                                divProcessNow.Visible = true;
                                GridViewBnkPayVerify.Visible = true;
                                if (ddlPaymentMethod.SelectedItem.Text != "Bank") { GridViewBnkPayVerify.Columns[8].Visible = false; } else { GridViewBnkPayVerify.Columns[6].Visible = true; }
                                GridViewBnkPayVerify.DataBind();
                                lblZeroRecords.Visible = false;
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

        protected void btnBatchPayVerify_Click(object sender, EventArgs e)
        {
           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                foreach (GridViewRow row in GridViewBnkPayVerify.Rows)
                {
                    string FeeId = row.Cells[2].Text;
                    string PurId = row.Cells[3].Text;
                    if (!string.IsNullOrWhiteSpace(FeeId) && FeeId != "&nbsp;") 
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_ms_fee_payments_bank_set_verified", con))
                        {
                            CheckBox ChkBoxHeader = (CheckBox)GridViewBnkPayVerify.HeaderRow.FindControl("chkboxSelectAll1");
                            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkStudent");
                            if (ChkBoxRows.Checked)
                            {
                                cmd.Parameters.AddWithValue("@payment_id", FeeId);
                                cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
                                con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                    else
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_ms_item_payments_bank_set_verified", con))
                        {
                            CheckBox ChkBoxHeader = (CheckBox)GridViewBnkPayVerify.HeaderRow.FindControl("chkboxSelectAll1");
                            //CheckBox ChkBoxHeader = (CheckBox)GridViewProcessApplications..FindControl("chkStudent"); 

                            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkStudent");
                            if (ChkBoxRows.Checked)
                            {
                                cmd.Parameters.AddWithValue("@item_pay_id", PurId);
                                cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
                                 con.Open();
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                   
                
                }
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Check Term,Class,Class Name either doesnot exists for Academic Year in settings or Cannot register for already registered students');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
                // ClearData(this);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Bank Payments Verified Successfully');", true);
            }

            BindGrid();
        }

        protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void GridViewBnkPayVerify_RowDataBound(object sender, GridViewRowEventArgs e)
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


        protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)GridViewBnkPayVerify.HeaderRow.FindControl("chkboxSelectAll");
            foreach (GridViewRow row in GridViewBnkPayVerify.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkStudent");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
        }


        protected void ddlPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewBnkPayVerify.Visible = false;
            divProcessNow.Visible = false;
            if (ddlPaymentMethod.SelectedValue == "Bank")
            {
                ddlBankName.SelectedIndex = 0; bank.Visible = true; 
            }
            else { bank.Visible = false; }
         
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ddlBankName_SelectedIndexChanged1(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void tbStartDate_TextChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void tbEndDate_TextChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        //protected void BtnSearchPayments_Click(object sender, EventArgs e)
        //{
        //    BindGrid();
        //}
    }
}