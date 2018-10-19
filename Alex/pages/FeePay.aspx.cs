using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
//using Alex.App_code;

namespace Alex.pages
{
    public partial class FeePay : System.Web.UI.Page
    {
        string RegID = string.Empty;
        string Personid = string.Empty;
        string Amount = string.Empty;
        string CreatedBy = string.Empty;
        string FeeSmstStatus = string.Empty;
        string ReplaceAmount = string.Empty;
        string SchoolName = string.Empty;
        string SchoolFullName = string.Empty;
        int AvaliableSms = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //ManageCookies.VerifyAuthentication();

            RegID = Request.QueryString["reg_id"].ToString();
            Personid = Request.QueryString["pId"].ToString();

            if (!IsPostBack)
            {
                StatementOfAccount(Personid, RegID);
                DropDownBankName();
                SchoolBind();
                SchoolAddress();
                imgSchool.ImageUrl = "~/pages/ImageHandler.ashx?roll_no=1";
                btnPrint.Visible = false;
                DivToPrint.Visible = false;
                divPayInfo.Visible = false;
            }
            //  btnOK.Visible = false;

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
        public void StatementOfAccount(string PersonId, string Regid)
        {
            int aaaa = Convert.ToInt32(Regid);
            //var personId = Request.QueryString["PersonId"];
            //var regid = Request.QueryString["Regid"];
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_person_statement_of_account_summary", con))//sp_ms_person_statement_of_account_summary
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@person_id", SqlDbType.VarChar).Value = PersonId;
                    //cmd.Parameters.Add("@reg_id", SqlDbType.VarChar).Value = Regid;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        {
                            sda.Fill(dt);
                            var query = from r in dt.AsEnumerable()
                                        where r.Field<Int32>("reg_id").Equals(aaaa)
                                        select new
                                        {
                                            Name = r["Name"].ToString(),
                                            Fees = r["Fees"].ToString(),
                                            Total = r["Fees_Total_Amount_Paid"].ToString(),
                                            SBalance = "₦" + r["Fee_Balance"].ToString(),
                                            // WithOutNiraBalance = r["Balance"].ToString(),

                                        };
                            foreach (var a in query)
                            {
                                string a1 = a.Fees.ToString();
                                string a2 = a.Total.ToString();
                                string a3 = a.SBalance.ToString();
                                //string a4 = a.WithOutNiraBalance.ToString();
                            }
                            DetailsViewPayment.DataSource = query;
                            DetailsViewPayment.DataBind();
                            DetailsViewPayment1.DataSource = query;
                            DetailsViewPayment1.DataBind();


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

        public void PaymentInfo()
        {
            RegID = Request.QueryString["reg_id"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_person_school_fee_paid_reference", con);
                cmd.Parameters.AddWithValue("@reg_id", RegID);

                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        DetailsViewPaymentInfo.DataSource = dt;
                        //if (dt.Rows.Count > 0)
                        //{
                        //    DetailsViewPaymentInfo.Visible = false;

                        //}
                        //else
                        //{
                        // DetailsViewPaymentInfo.Visible = true;
                        DetailsViewPaymentInfo.DataBind();

                        //}

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

        protected void FeePay_Click(object sender, EventArgs e)
        {
            lblResult.Text = "Payment Successful";
            lblResult.ForeColor = System.Drawing.Color.Blue;
            lblResult.CssClass = "h3";
            CreatedBy = HttpContext.Current.User.Identity.Name;
            SaveFeeInfo(RegID, Amount, CreatedBy);
            btnPrint.Visible = true;
            btnSubmit.Visible = false;
            divPayFields.Visible = false;
            PaymentInfo();
            divPayInfo.Visible = true;
            DivToPrint.Visible = true;
            divDetails.Visible = false;

        }
        public void SaveFeeInfo(string Regid, string Amount, string CreatedBy)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            // ReplaceAmount = Regex.Replace(tbPayingAmount.Text.ToString(), "[^(-).0-9]", "");
            ReplaceAmount = tbPayingAmount.Text.Replace(",", string.Empty);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_person_payments_add", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@reg_id", SqlDbType.VarChar).Value = Regid;
                    cmd.Parameters.Add("@amount", SqlDbType.Decimal).Value = ReplaceAmount;
                    cmd.Parameters.Add("@created_by", SqlDbType.VarChar).Value = CreatedBy;
                    cmd.Parameters.AddWithValue("@created_date", tbDate.Text.ToString());
                    cmd.Parameters.AddWithValue("@payment_method", ddlPaymentMethod.Text.ToString());
                    cmd.Parameters.AddWithValue("@payment_method_ref", tbPMReference.Text.ToString());
                    cmd.Parameters.AddWithValue("@bank_name", ddlBankName.Text.ToString());
                    cmd.Parameters.AddWithValue("@invoice_no", tbInvoiceNumber.Text.ToString());
                    cmd.Parameters.AddWithValue("@teller_no", tbReceiptNumber.Text.ToString());
                    cmd.Parameters.AddWithValue("@status", ddlBankVerify.Text.ToString());
                    cmd.ExecuteNonQuery();
                    StatementOfAccount(Personid, RegID);
                    SmsAvaliable();
                    //ScholarshipExpensesAdd();
                    FeeAlertStatus();
                    SendMessage();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Thank you! Your payment was processed successfully.');", true);
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
                            "Dear Parent, Thank you for the payment of N" + ReplaceAmount + " for " + DetailsViewPayment.Rows[0].Cells[1].Text + "." + "The Balance of your school fees is N" + DetailsViewPayment1.Rows[3].Cells[1].Text.Replace("₦", string.Empty);
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
                ClearData(this);
            }
        }

        protected void Result_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/pages/profile.aspx?PersonId=" + Personid);
            Response.Redirect("~/pages/profile.aspx?Personid=" + Personid + "&Regid=" + RegID + "&action=soa");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/profile.aspx?Personid=" + Personid + "&Regid=" + RegID + "&action=soa");
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
                    SchoolFullName = dr["SchoolName"].ToString();
                    lblSchoolName.Text = SchoolFullName.ToString();
                    lblSmsSchoolName.Text = SchoolName.ToString();
                }
                con.Close();
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select p_g_contact_no1 from ms_people where person_id=" + Personid, con);
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

        public void SchoolAddress()
        {
            SqlDataAdapter adp = new SqlDataAdapter();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select school_name,address_line1,address_line2,lga_city,state,country,zip_postal_code,contact_email,contact_no1 from ms_school", con);
                cmd.Dispose();
                SqlDataReader dr = cmd.ExecuteReader();
                string SchoolName = string.Empty;
                while (dr.Read())
                {
                    //SchoolName = dr["school_name"].ToString();
                    string Address = dr["address_line1"].ToString();
                    string Address2 = dr["address_line2"].ToString();
                    string City = dr["lga_city"].ToString();
                    string State = dr["state"].ToString();
                    string country = dr["country"].ToString();
                    string Postcode = dr["zip_postal_code"].ToString();
                    string Email = dr["contact_email"].ToString();
                    string PhoneNo = dr["contact_no1"].ToString();

                    //lblSchoolName.Text = SchoolName.ToString();
                    lblAddress.Text = Address.ToString() + Address2.ToString();
                    lblCity.Text = City.ToString();
                    lblState.Text = State.ToString();
                    lblCountry.Text = country.ToString();
                    lblPostCode.Text = Postcode.ToString();
                    lblEmail.Text = Email.ToString();
                    lblPhoneNo.Text = PhoneNo.ToString();

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
                ClearData(this);

            }
        }
        //private void ScholarshipExpensesAdd()
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("sp_ms_expenses_add", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        con.Open();

        //        cmd.Parameters.AddWithValue("@type_refund_payment", "Payment");
        //        cmd.Parameters.AddWithValue("@category", "Scholarship");
        //        cmd.Parameters.AddWithValue("@exp_date", DateTime.Today.ToString("dd-MM-yyyy"));
        //        cmd.Parameters.AddWithValue("@amount", tbPayingAmount.Text.ToString());
        //        cmd.Parameters.AddWithValue("@description", "Financial aid for a student to further their education");
        //        cmd.Parameters.AddWithValue("@reciept_ref", tbReceiptNumber.Text.ToString());
        //        cmd.Parameters.AddWithValue("@acad_year", DBNull.Value);
        //        cmd.Parameters.AddWithValue("@term", DBNull.Value);
        //        cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
        //        cmd.ExecuteNonQuery();

        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Cannot Add Duplicate Record');", true);
        //    }
        //    finally
        //    {
        //        con.Close();
        //        ClearData(this);

        //    }
        //}

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
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "recipient", Value = DetailsViewPayment.Rows[0].Cells[1].Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "message", Value = "Dear Parent, Thanks for your payment of N" + ReplaceAmount + "." + "The Balance of your school fees is N " + DetailsViewPayment1.Rows[3].Cells[1].Text.Replace("₦", string.Empty) });
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

        protected void DetailsViewPaymentInfo_OnDataBound(object sender, EventArgs e)
        {
            String data;

            foreach (DetailsViewRow r in DetailsViewPaymentInfo.Rows)
            {
                if (r.Cells.Count > 1)
                {
                    data = r.Cells[1].Text;
                }
                else
                {
                    data = r.Cells[0].Text;
                }

                data = data.Replace("&nbsp;", "").Trim();
                if (data == null || data == "")
                {
                    r.Visible = false;
                }
            }
        }
    }
}