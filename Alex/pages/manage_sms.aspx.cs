using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Alex.App_code;
using System.Net.Mail;
using System.IO;
using System.Net;
using System.Text;


namespace Alex.pages
{
    public partial class manage_sms : System.Web.UI.Page
    {
        int AvaliableSms = 0;
        string MonthlySMS = string.Empty; 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SmsMgtDataBind();
                SmsSchoolName();
                SmsTransactionBind();
                FeePayAlertStatus();
                BirthdayAlertStatus();
                SmsAvaliable();
                InclusiveSms();
                lblInclusiveSms.Text = MonthlySMS;
                lblSmsAvaliable.Text = AvaliableSms.ToString();
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


        private void InclusiveSms()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select [description] from ms_status where category = 'sms_mgt_info'", con);
                cmd.CommandType = CommandType.Text;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    MonthlySMS = dr["description"].ToString();
                   
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

        private void SmsMgtDataBind()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_sms_mgt_list_all", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridViewManageSms.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                //lblZeroAttendanceBreakDown.Visible = true;
                                //lblZeroAttendanceBreakDown.Text = "No Attendance Record Found ";
                            }
                            else
                            {
                               GridViewManageSms.DataBind();
                               //lblZeroAttendanceBreakDown.Visible = false;
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

        //protected void GridViewManageSms_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        var dr = e.Row.DataItem as DataRowView;
        //        if (dr["phone_no"].ToString() == "" || dr["phone_no"].ToString() == string.Empty)
        //        {
        //           e.Row.Enabled = false;  //OR dr.Enabled = false; 
        //           //DISABLED Controls only     
        //            //((TextBox)e.Row.FindControl("TextBox1")).Enabled = false;
        //        }
                
        //        Button btn = (Button)e.Row.FindControl("btnStatus");
        //        string AC = ((Button)e.Row.FindControl("btnStatus")).Text;
        //        if (AC == "Enabled")
        //            btn.CssClass = "btn-sm btn-primary m-t-n-xs";
        //        //btn.Style.Add("background-color", "yellow");
        //        else if ((AC == "Disabled"))
        //            btn.CssClass = "btn-sm btn-success m-t-n-xs";
        //    }
        //}

        protected void GridViewManageSms_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewManageSms.EditIndex = e.NewEditIndex;
            SmsMgtDataBind();
        }

        protected void GridViewManageSms_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GridViewManageSms.EditIndex = -1;
            SmsMgtDataBind();
        }

        protected void GridViewManageSms_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_sms_mgt_list_edit", con);
                GridViewRow row = GridViewManageSms.Rows[e.RowIndex] as GridViewRow;
                TextBox txtPhoneNumber = row.FindControl("tbMobileNo") as TextBox;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "status_id", Value = GridViewManageSms.Rows[e.RowIndex].Cells[0].Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "phone_no", Value = txtPhoneNumber.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "updated_by", Value =  HttpContext.Current.User.Identity.Name  });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                GridViewManageSms.EditIndex = -1;
                SmsMgtDataBind();
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

        protected void btnStatus_Click(object sender, EventArgs e)
        {
             using (GridViewRow row = (GridViewRow)((Button)sender).Parent.Parent)
             {
                string StatusID = row.Cells[0].Text;
                Button btntext = row.FindControl("btnStatus") as Button;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_sms_mgt_enabled_disabled", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@status_id", SqlDbType.Int).Value = StatusID;
                        cmd.Parameters.Add("@updated_by", SqlDbType.VarChar).Value = HttpContext.Current.User.Identity.Name;
                        if (btntext.Text == "Disabled")
                        {
                            cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = "Enabled";
                        }
                        else
                        {
                            cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = "Disabled";
                        }
                        cmd.ExecuteNonQuery();
                        SmsMgtDataBind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Success');", true);
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
        }

        private void BirthdayAlertStatus()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_sms_status_alert_type", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AlertType", SqlDbType.VarChar).Value = "Birthday SMS Alert";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    btnBirthdayAlert.Text = dr["description"].ToString();
                }
                if (btnBirthdayAlert.Text == "Disabled") { btnBirthdayAlert.Text = "Enable"; btnBirthdayAlert.CssClass = "btn-sm btn-primary m-t-n-xs"; lblBDA.Text = "(Click to Enable):"; }
                else { btnBirthdayAlert.Text = "Disable"; btnBirthdayAlert.CssClass = "btn-sm btn-success m-t-n-xs"; lblBDA.Text = "(Click to Disable):"; }
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

        private void FeePayAlertStatus()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_sms_status_alert_type", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AlertType", SqlDbType.VarChar).Value = "Fees Payment SMS Alert";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    btnFeeAlert.Text = dr["description"].ToString();
                }
                if (btnFeeAlert.Text == "Disabled") { btnFeeAlert.Text = "Enable"; btnFeeAlert.CssClass = "btn-sm btn-primary m-t-n-xs"; lblPA.Text = "(Click to Enable):"; }
                else { btnFeeAlert.Text = "Disable"; btnFeeAlert.CssClass = "btn-sm btn-success m-t-n-xs"; lblPA.Text = "(Click to Disable):"; }
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
        private void SmsTransactionBind()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_sms_transaction_all", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridViewTransaction.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                lblZeroTransaction.Visible = true;
                                lblZeroTransaction.Text = "No Transaction Record Found ";
                            }
                            else
                            {
                                GridViewTransaction.DataBind();
                                lblZeroTransaction.Visible = false;
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

        protected void btnBirthdayAlert_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_sms_mgt_enabled_disabled", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@status_name", SqlDbType.VarChar).Value = "Birthday SMS Alert";
                    cmd.Parameters.Add("@updated_by", SqlDbType.VarChar).Value = HttpContext.Current.User.Identity.Name;
                    if (btnBirthdayAlert.Text == "Disable")
                    {
                        cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = "Disabled";
                       
                    }
                    else
                    {
                        cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = "Enabled";
                        
                    }
                    cmd.ExecuteNonQuery();
                    BirthdayAlertStatus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Success');", true);
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

        protected void btnFeeAlert_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                //FeePayAlertStatus();
                using (SqlCommand cmd = new SqlCommand("sp_ms_sms_mgt_enabled_disabled", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@status_name", SqlDbType.VarChar).Value = "Fees Payment SMS Alert";
                    cmd.Parameters.Add("@updated_by", SqlDbType.VarChar).Value = HttpContext.Current.User.Identity.Name;
                    if (btnFeeAlert.Text == "Disable")
                    {
                        cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = "Disabled";
                        
                    }
                    else
                    {
                        cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = "Enabled";
                       
                    }
                    cmd.ExecuteNonQuery();
                    FeePayAlertStatus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Success');", true);
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



        private void SmsSchoolName()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_sms_mgt_fee_alert_list", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridViewSchoolName.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                //lblZeroAttendanceBreakDown.Visible = true;
                                //lblZeroAttendanceBreakDown.Text = "No Attendance Record Found ";
                            }
                            else
                            {
                                GridViewSchoolName.DataBind();
                                //lblZeroAttendanceBreakDown.Visible = false;
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


        protected void GridViewSchoolName_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewSchoolName.EditIndex = e.NewEditIndex;
            SmsSchoolName();
        }

        protected void GridViewSchoolName_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GridViewSchoolName.EditIndex = -1;
            SmsSchoolName();
        }

        protected void GridViewSchoolName_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_sms_mgt_list_edit", con);
                GridViewRow row = GridViewSchoolName.Rows[e.RowIndex] as GridViewRow;
                TextBox txtPhoneNumber = row.FindControl("tbSchoolName") as TextBox;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "status_id", Value = GridViewSchoolName.Rows[e.RowIndex].Cells[0].Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "phone_no", Value = txtPhoneNumber.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "updated_by", Value = HttpContext.Current.User.Identity.Name });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                GridViewSchoolName.EditIndex = -1;
                SmsSchoolName();
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

        protected void btnSmsRequiredSubmit_Click(object sender, EventArgs e)
        {
            //if (Convert.ToDouble(tbSmsRequired.Text) >= 200)
            //{
                SchoolEmail();
                SendMail();
                ClearData(this);
            //}
            //else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Minimum 200 SMS Units required to request');", true); }
        }

        public void SendMail()
        {
            try
            {
                Double Cost  = Convert.ToDouble(tbSmsRequired.Text) * 2.4;
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.Host = "relay-hosting.secureserver.net"; //"smtp.gmail.com";
                client.EnableSsl = false;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials =false;
                client.Credentials = new System.Net.NetworkCredential("donotreplyiqschools@gmail.com", "Jysp!2016");

                //MailMessage mm = new MailMessage(tbEmail.Text, tbEmail.Text, tbSubject.Text, tbBody.Text);
                MailMessage mm = new MailMessage();
               // MailAddress fromAddress = new MailAddress(lblSchoolEmail.Text);
                MailAddress fromAddress = new MailAddress("project.alexandra@torilo.co.uk");
                // mm.From = new MailAddress("AdremiSchools@gmail.com");
                mm.From = fromAddress;
                mm.To.Add(new MailAddress("iqsupport@torilo.co.uk"));
                mm.To.Add(new MailAddress("torilo.ng@torilo.co.uk"));
                mm.Subject = lblSchoolName.Text  +"  "+ "SMS Request";
                mm.Body = "Hi iQ Support Team," + Environment.NewLine
                    + Environment.NewLine + "Please, Can you add" + "  " + tbSmsRequired.Text + "  "
                    + "SMS" +"(COST:₦" + Cost +") to our school Application. " + Environment.NewLine + Environment.NewLine 
                    + "Thank you," + Environment.NewLine + lblSchoolName.Text + Environment.NewLine + lblSchoolEmail.Text;
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                client.Send(mm);
             
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Message sent to iQ Support successfull, Please Make a payment of ₦" + Cost + "');", true);
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
                SqlCommand cmd = new SqlCommand("sp_ms_settings_school_detail", con);
                cmd.Dispose();
                SqlDataReader dr = cmd.ExecuteReader();
                string SchoolEmail = string.Empty;
                string SchoolName = string.Empty;
                while (dr.Read())
                {
                    SchoolEmail = dr["email_add"].ToString();
                    lblSchoolEmail.Text = SchoolEmail.ToString();
                    SchoolName = dr["school_name"].ToString();
                    lblSchoolName.Text = SchoolName.ToString();
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

    }
}