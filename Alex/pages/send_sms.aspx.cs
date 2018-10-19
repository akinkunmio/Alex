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

namespace Alex.pages.admin
{
    public partial class send_sms : System.Web.UI.Page
    {
        int count = 0;
        int lvl = 0;
        int AvaliableSms = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Level(); SmsAvaliable();
            lblSmsAvaliable.Text = "Avaliable SMS : " + " " + AvaliableSms.ToString();
            //Joe 230118 2025 Enabled Send SMS for Level 2 
            //if (lvl == 1)
            if (lvl == 1 || lvl == 2)
            {
                if (!IsPostBack)
                {
                    DropDownFormClass();
                }
            }
            else
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            GetPhoneNumbers(ddlSmsGroup.SelectedValue.ToString());
            SchoolBind();
            SmsAvaliable();
            if (count < AvaliableSms)
            {
                SendMessage();
            }
            else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Insufficient SMS Balance, Please Top Up !');", true); }

        }

        protected void SendMessage()
        {
            try
            {

                string smsUrl = "http://www.smsmobile24.com/index.php?option=com_spc&comm=spc_api&username=" + "kandula" + "&password=" + "LOndon4321$.cop" + "&sender=" + lblName.Text + "&recipient=" + lblPhoneNumbers.Text + "&message=" +
                    // Joe190118 1817  "http://smsmobile24.com/components/com_spc/smsapi.php?username=" + "kandula" + "&password=" + "LOndon4321$.cop" + "&sender="+ lblName.Text + "&recipient=" + lblPhoneNumbers.Text + "&message=" +
                tbSmsMessage.Text;

                WebRequest request = WebRequest.Create(smsUrl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                lblResponce.Text = responseFromServer;
                if (lblResponce.Text.Contains("OK"))
                {
                    SaveSmsLog(); SmsAvaliable();
                    lblSmsAvaliable.Text = "Avaliable SMS : " + " " + AvaliableSms.ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('SMS Sent Successfully.');", true);
                }
                else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('SMS Sending Failed');", true); }

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
                SqlCommand cmd = new SqlCommand("sp_ms_sms_mgt_fee_alert_list", con);
                cmd.Dispose();
                SqlDataReader dr = cmd.ExecuteReader();
                string SchoolName = string.Empty;
                while (dr.Read())
                {
                    SchoolName = dr["phone_no"].ToString();
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

                ddlSmsGroup.DataSource = ddlValues;
                ddlSmsGroup.DataValueField = "form_class";
                ddlSmsGroup.DataTextField = "form_class";
                ddlSmsGroup.DataBind();
                ddlSmsGroup.Items.Insert(0, new ListItem("Please select Group", ""));
                ddlSmsGroup.Items.Insert(1, new ListItem("All Parents"));
                ddlSmsGroup.Items.Insert(2, new ListItem("All Students"));
                ddlSmsGroup.Items.Insert(3, new ListItem("All Employees"));
                // ddlSmsGroup.Items.Add(new ListItem("Other Number", "-1"));

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

        private void GetPhoneNumbers(string Group)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                if (Group == "Other Number")
                {
                    lblPhoneNumbers.Text = tbPhoneNumbers.Text;
                    count = lblPhoneNumbers.Text.Split(',').Length;
                }
                else
                {
                    string SP_Name = "";

                    if (Group == "All Employees")
                    {
                        SP_Name = "sp_ms_sms_employees_list_all";
                    }

                    else if (Group == "All Parents")
                    {
                        SP_Name = "sp_ms_sms_parents_list_all";
                    }
                    else if (Group == "All Students")
                    {
                        SP_Name = "sp_ms_sms_students_list_all";
                    }

                    else if (Group != "All Students" && Group != "All Employees" && Group != "All Parents" && Group != "Other Number")
                    {
                        SP_Name = "sp_ms_form_class_contacts";
                    }
                    using (SqlCommand cmd = new SqlCommand(SP_Name, con))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            if (Group != "All Students" && Group != "All Employees" && Group != "All Parents" && Group != "Other Number")
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                con.Open();

                                cmd.Parameters.AddWithValue("@form_class", Group);
                                //cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                con.Open();
                                cmd.Connection = con;
                                cmd.CommandType = CommandType.StoredProcedure;
                            }

                            // cmd.CommandType = CommandType.Text;
                            sda.SelectCommand = cmd;
                            SqlDataReader dr = cmd.ExecuteReader();
                            string Contacts = string.Empty;

                            while (dr.Read())
                            {

                                Contacts = Contacts + ",234" + dr["contacts"].ToString();
                                lblPhoneNumbers.Text = Contacts.ToString();
                                count++;
                            }
                            //dr.NextResult();
                            //while (dr.Read()) { count = (Int32)dr["SmsCount"]; }
                            //lblCount.Text = count.ToString();
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
                {//AvaliableSms = (Int32)dr["available_sms"];
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
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "recipient", Value = ddlSmsGroup.SelectedValue.ToString() });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "message", Value = tbSmsMessage.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "phone_number", Value = "0" + lblPhoneNumbers.Text });
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