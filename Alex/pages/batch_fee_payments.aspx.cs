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
using System.Text.RegularExpressions;
using System.IO;
using System.Net;

namespace Alex.pages
{
    public partial class batch_fee_payments : System.Web.UI.Page
    {
        string FeeSmstStatus = string.Empty;
        int AvaliableSms = 0;
        string SchoolName = string.Empty;
        string SchoolFullName = string.Empty;
        string ReplaceCommaAmount = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DropDownYear();
                DropDownTerm();
                DropDownFormClass();
                ddlFormClass.SelectedIndex = 1;
                BindGrid();
            }
        }

        public void DropDownFormClass()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_form_class_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                SqlDataReader ddlValues;
                ddlValues = cmd.ExecuteReader();

                ddlFormClass.DataSource = ddlValues;
                ddlFormClass.DataValueField = "form_class";
                ddlFormClass.DataTextField = "form_class";
                ddlFormClass.DataBind();
                ddlFormClass.Items.Insert(0, new ListItem("Please select Class"));
                //ddlFormClass.Items.Insert(1, new ListItem("ALL"));
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
        public void DropDownTerm()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_term_dropdown_v2", con);
            cmd.Parameters.Add("@acad_year", SqlDbType.VarChar).Value = ddlAcademicYear.SelectedItem.ToString();
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                SqlDataReader ddlValues;
                ddlValues = cmd.ExecuteReader();
                string TermSelectedValue = null;
                while (ddlValues.Read())
                {
                    TermSelectedValue = ddlValues[0].ToString();
                    int DefaultValue = Convert.ToInt32(ddlValues[1]);
                    if (DefaultValue == 1)
                        break;
                }
                ddlValues.Close();
                ddlValues = cmd.ExecuteReader();
                ddlTerm.DataSource = ddlValues;
                ddlTerm.DataValueField = "term_name";
                ddlTerm.DataTextField = "term_name";
                ddlTerm.DataBind();
                ddlTerm.Items.Insert(0, new ListItem("Please select Term", ""));
                ddlTerm.SelectedValue = TermSelectedValue;

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
        public void DropDownYear()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_acad_year_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                SqlDataReader ddlValues;
                ddlValues = cmd.ExecuteReader();
                string YearSelectedValue = null;

                while (ddlValues.Read())
                {
                    YearSelectedValue = ddlValues[0].ToString();
                    int DefaultValue = Convert.ToInt32(ddlValues[1]);
                    if (DefaultValue == 1)
                        break;
                }
                ddlValues.Close();
                ddlValues = cmd.ExecuteReader();

                ddlAcademicYear.DataSource = ddlValues;
                ddlAcademicYear.DataValueField = "acad_year";
                ddlAcademicYear.DataTextField = "acad_year";
                ddlAcademicYear.DataBind();
                ddlAcademicYear.Items.Insert(0, new ListItem("Please select Year", ""));
                ddlAcademicYear.SelectedValue = YearSelectedValue;
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



        private void BindGrid()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_batch_statement_of_account_summary", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@acad_year", ddlAcademicYear.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@term_name", ddlTerm.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@form_class", ddlFormClass.SelectedItem.ToString());

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;

                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            GridViewBatchPayments.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {

                                GridViewBatchPayments.Visible = false;
                                BtnBlukPayFee.Visible = false;
                                lblZeroRecords.Visible = true;
                                lblZeroRecords.Text = "No Student has been registered to selected class or the Tuition fee for the class has not been setup.";
                            }
                            else
                            {

                                GridViewBatchPayments.Visible = true;
                                BtnBlukPayFee.Visible = true;
                                GridViewBatchPayments.DataBind();
                                lblZeroRecords.Visible = false;
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

        protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownTerm();
            if (ddlTerm.Items.Count > 1)
            {
                ddlTerm.SelectedIndex = 1;
                BindGrid();
            }
        }

        protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ddlFormClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)GridViewBatchPayments.HeaderRow.FindControl("chkboxSelectAll");
            foreach (GridViewRow row in GridViewBatchPayments.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkStudent");
                TextBox txtBulkRef = (TextBox)row.FindControl("tbBulkFeePayReference");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                    if (txtBulkRef.Text.Length == 0)
                    { txtBulkRef.Attributes["required"] = "true"; }
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
        }


        protected void chkStudent_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in GridViewBatchPayments.Rows)
                {
                    CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkStudent");
                    TextBox txtBulkRef = (TextBox)row.FindControl("tbBulkFeePayReference");
                    if (ChkBoxRows.Checked == true)
                    {
                        if (txtBulkRef.Text.Length == 0)
                        {
                            txtBulkRef.Attributes["required"] = "true";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            }

        }

        protected void BtnBlukPayFee_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                foreach (GridViewRow row in GridViewBatchPayments.Rows)
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_person_payments_add", con))
                    {
                        CheckBox ChkBoxHeader = (CheckBox)GridViewBatchPayments.HeaderRow.FindControl("chkboxSelectAll1");
                        TextBox txtdate = row.FindControl("tbBatchFeeDate") as TextBox;
                        TextBox txtBulkAmount = row.FindControl("tbBatchFeeAmount") as TextBox;
                        ReplaceCommaAmount = Regex.Replace(txtBulkAmount.Text.ToString(), "[^.0-9]", "");
                        DropDownList ddlPayMethod = row.FindControl("ddlPaymentMethod") as DropDownList;
                        TextBox txtBulkRef = row.FindControl("tbBulkFeePayReference") as TextBox;
                        DropDownList ddlBulkBank = row.FindControl("ddlBankBulkFeePay") as DropDownList;
                        TextBox txtBulkInv = row.FindControl("tbBulkInvNo") as TextBox;
                        TextBox txtBulkTeller = row.FindControl("tbBulkRecNo") as TextBox;
                        TextBox txtBulkAddFee = row.FindControl("tbBulkAddCharges") as TextBox;
                        DropDownList ddlBulkVerify = row.FindControl("ddlBankVerify") as DropDownList;
                        CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkStudent");
                        if (ChkBoxRows.Checked)
                        {
                            cmd.Parameters.Add("@reg_id", SqlDbType.VarChar).Value = row.Cells[2].Text;
                            cmd.Parameters.Add("@amount", SqlDbType.Decimal).Value = ReplaceCommaAmount;
                            cmd.Parameters.Add("@created_by", SqlDbType.VarChar).Value = HttpContext.Current.User.Identity.Name;
                            cmd.Parameters.AddWithValue("@created_date", txtdate.Text.ToString());
                            cmd.Parameters.AddWithValue("@payment_method", ddlPayMethod.SelectedValue);
                            cmd.Parameters.AddWithValue("@payment_method_ref", txtBulkRef.Text.ToString());
                            cmd.Parameters.AddWithValue("@bank_name", ddlBulkBank.SelectedValue);
                            cmd.Parameters.AddWithValue("@invoice_no", txtBulkInv.Text.ToString());
                            cmd.Parameters.AddWithValue("@teller_no", txtBulkTeller.Text.ToString());
                            cmd.Parameters.AddWithValue("@status", ddlBulkVerify.SelectedValue);
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                            con.Close();
                            // SmsAvaliable();
                            // FeeAlertStatus();
                            // SendMessage();
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
                BindGrid();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Thank you! Your payment(s) was processed successfully.');", true);
            }
        }

        protected void SendMessage()
        {
            try
            {
                if (FeeSmstStatus == "Enabled")
                {
                    if (AvaliableSms >= 1)
                    {
                        string smsUrl = "http://www.smsmobile24.com/index.php?option=com_spc&comm=spc_api&username=" + "kandula" + "&password=" + "LOndon4321$.cop" + "&sender=" + lblSmsSchoolName.Text + "&recipient=" + "234" + lblPhoneNumber.Text + "&message=" +
                            // Joe190118 1817 "http://smsmobile24.com/components/com_spc/smsapi.php?username=" + "kandula" + "&password=" + "LOndon4321$.cop" + "&sender=" + lblSmsSchoolName.Text + "&recipient=" + "234" + lblPhoneNumber.Text + "&message=" +
                            "Dear Parent, Thank you for the payment of N" + ReplaceCommaAmount + " for " + GridViewBatchPayments.Rows[0].Cells[2].Text + "." + "The Balance of your school fees is N" + GridViewBatchPayments.Rows[0].Cells[6].Text.Replace("₦", string.Empty);
                        //kandula&password=LOndon4321$.cop&sender=Alex&recipient=08111591118&message=" + "'Hi this is test messae'";
                        WebRequest request = WebRequest.Create(smsUrl);

                        // Get the response.
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                        // Get the stream containing content returned by the server.
                        Stream dataStream = response.GetResponseStream();
                        // Open the stream using a StreamReader for easy access.
                        StreamReader reader = new StreamReader(dataStream);
                        // Read the content.
                        string responseFromServer = reader.ReadToEnd();
                        lblResponce.Text = responseFromServer;
                        //return responseFromServer;
                        if (lblResponce.Text.Contains("OK"))
                        {
                            SaveSmsLog();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('SMS Sent Successfully.');", true);
                        }
                        else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('SMS Sending Failed');", true); }
                    }
                    else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Insufficient SMS Balance, Please Top Up or Disable SMS !');", true); }
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);

            }
            finally
            {
                //ClearData(this);
            }
        }



        private void FeeAlertStatus()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("select [description] from ms_status where status_name = 'Fees Payment SMS Alert'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                { FeeSmstStatus = dr["description"].ToString(); }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Cannot Add Duplicate Record');", true);
            }
            finally
            {
                con.Close();
                //ClearData(this);

            }
        }

        private void SmsAvaliable()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_sms_available_sms", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string GetAvaliableSMS = dr["available_sms"].ToString();
                    AvaliableSms = Int32.Parse(GetAvaliableSMS);
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

        private void SaveSmsLog()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                double total_cost = 2.4;
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_sms_history_add", con);
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "recipient", Value = GridViewBatchPayments.Rows[0].Cells[2].Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "message", Value = "Dear Parent, Thanks for your payment of N" + ReplaceCommaAmount + "." + "The Balance of your school fees is N " + GridViewBatchPayments.Rows[0].Cells[2].Text.Replace("₦", string.Empty) });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "phone_number", Value = "0" + lblPhoneNumber.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "count_sms", Value = 1 });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "sms_total_cost", Value = total_cost });
                cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();

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

        public void SchoolBind()
        {
            SqlDataAdapter adp = new SqlDataAdapter();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_sms_mgt_fee_alert_list", con);
                cmd.Dispose();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    SchoolName = dr["phone_no"].ToString();
                    lblSmsSchoolName.Text = SchoolName.ToString();
                }
                con.Close();
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select p_g_contact_no1 from ms_people where person_id=" + GridViewBatchPayments.Rows[0].Cells[1].Text, con);
                cmd.Dispose();
                SqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read()) { string PhoneNumber = dr1["p_g_contact_no1"].ToString(); lblPhoneNumber.Text = PhoneNumber.ToString(); }
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
}