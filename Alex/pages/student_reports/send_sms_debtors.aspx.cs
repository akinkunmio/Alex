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

namespace Alex.pages.student_reports
{
    public partial class send_sms_debtors : System.Web.UI.Page
    {
        string Numbers = string.Empty;
        int count = 0;
        int AvaliableSms = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SmsAvaliable();
                lblSmsAvaliable.Text = "Avaliable SMS : " + " " + AvaliableSms.ToString();
                DropDownGetDebtorsPhoneNo();
            }
        }

        public void DropDownGetDebtorsPhoneNo()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_student_all_debtors_list", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                SqlDataReader ddlValues;
                ddlValues = cmd.ExecuteReader();

                ddcbDebtors.DataSource = ddlValues;
                ddcbDebtors.DataValueField = "p_g_contact_no1";
                ddcbDebtors.DataTextField = "name";
                ddcbDebtors.DataBind();

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

        protected void SendMessage()
        {
            try
            {

                string smsUrl = "http://www.smsmobile24.com/index.php?option=com_spc&comm=spc_api&username=" + "kandula" + "&password=" + "LOndon4321$.cop" + "&sender=" + "---" + lblName.Text + "&recipient=" + lblPhoneNumbers.Text + "&message=" +
                    // Joe190118 1817 "http://smsmobile24.com/components/com_spc/smsapi.php?username=" + "kandula" + "&password=" + "LOndon4321$.cop" + "&sender=" + "---" + lblName.Text + "&recipient=" + lblPhoneNumbers.Text + "&message=" +
                    tbSmsMessage.Text;

                WebRequest request = WebRequest.Create(smsUrl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                lblResponce.Text = responseFromServer;
                if (lblResponce.Text.Contains("OK"))
                {
                    SaveSmsLog();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('SMS Sent Successfully.');window.location = '/pages/student_reports/student_debtors_year.aspx';", true);
                }
                else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('SMS Sending Failed');", true); }

                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('SMS Sent Successfully.');", true);
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

        private void GetPhoneNumbers()
        {
            foreach (ListItem item in ddcbDebtors.Items)
            {
                if (item.Selected)
                {
                    Numbers += item.Value + ",";
                    count++;
                }
            } lblPhoneNumbers.Text = Numbers;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            GetPhoneNumbers();
            SchoolBind();
            SmsAvaliable();
            if (count < AvaliableSms)
            {
                SendMessage();
            }
            else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Insufficient SMS Balance, Please Top Up !');", true); }

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
                double total_cost = count * 2.4;
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_sms_history_add", con);
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "recipient", Value = "Debtors" });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "message", Value = tbSmsMessage.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "phone_number", Value = lblPhoneNumbers.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "count_sms", Value = count });
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
    }
}