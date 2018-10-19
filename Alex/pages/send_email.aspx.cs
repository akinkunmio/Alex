using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.IO;
using System.Net;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Alex.pages
{
    public partial class send_email : System.Web.UI.Page
    {
        int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownFormClass();
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

                ddlEmailGroup.DataSource = ddlValues;
                ddlEmailGroup.DataValueField = "form_class";
                ddlEmailGroup.DataTextField = "form_class";
                ddlEmailGroup.DataBind();
                ddlEmailGroup.Items.Insert(0, new ListItem("Please select Group"));
                ddlEmailGroup.Items.Insert(1, new ListItem("All Students"));
                ddlEmailGroup.Items.Insert(2, new ListItem("All Employees"));
                // ddlEmailGroup.Items.Add(new ListItem("Other Number", "-1"));

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


        private void GetEmailAddress(string Group)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                if (Group == "Other Email")
                {
                    lblEmailNumbers.Text = tbPhoneNumbers.Text;
                    count = lblEmailNumbers.Text.Split(',').Length;
                }
                else
                {
                    string SP_Name = "";

                    if (Group == "All Employees")
                    {
                        SP_Name = "sp_ms_email_employees_list_all";
                    }

                    else if (Group == "All Students")
                    {
                        SP_Name = "sp_ms_email_students_list_all";
                    }

                    else if (Group != "All Students" && Group != "All Employees" && Group != "Other Email")
                    {
                        SP_Name = "sp_ms_form_class_emails";
                    }
                    using (SqlCommand cmd = new SqlCommand(SP_Name, con))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            if (Group != "All Students" && Group != "All Employees" && Group != "Other Email")
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

                                Contacts = Contacts + "," + dr["email"].ToString();
                                lblEmailNumbers.Text = Contacts.ToString();
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



        public void SendMail()
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.Host = "relay-hosting.secureserver.net"; //"smtp.gmail.com";
                client.EnableSsl = false;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("donotreplyiqschools@gmail.com", "Jysp!2016");

                //MailMessage mm = new MailMessage(tbEmail.Text, tbEmail.Text, tbSubject.Text, tbBody.Text);
                MailMessage mm = new MailMessage();
                MailAddress fromAddress = new MailAddress(lblName.Text);
               // mm.From = new MailAddress("AdremiSchools@gmail.com");
                mm.From = fromAddress;
                mm.To.Add(new MailAddress(lblEmailNumbers.Text));
                mm.Subject = tbSubject.Text;
                mm.Body = tbBody.Text;
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                client.Send(mm);
               
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Sent successfull');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            }
            finally
            {


            }
        }


        public void SchoolEmail()
        {
            SqlDataAdapter adp = new SqlDataAdapter();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(" select email_add FROM [dbo].[ms_school]", con);
                cmd.Dispose();
                SqlDataReader dr = cmd.ExecuteReader();
                string SchoolEmail= string.Empty;
                while (dr.Read())
                {
                    SchoolEmail = dr["email_add"].ToString();
                    lblName.Text = SchoolEmail.ToString();
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

        protected void BtnSend_Click(object sender, EventArgs e)
        {
            GetEmailAddress(ddlEmailGroup.SelectedValue.ToString());
            SchoolEmail();
            SendMail();
            ClearData(this);
        }
    }
}