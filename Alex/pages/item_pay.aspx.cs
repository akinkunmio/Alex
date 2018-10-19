using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alex.App_code;
using System.Text.RegularExpressions;

namespace Alex.pages
{
    public partial class item_pay : System.Web.UI.Page
    {
        string PurId = string.Empty;
        string Personid = string.Empty;
        string Amount = string.Empty;
        string CreatedBy = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //ManageCookies.VerifyAuthentication();
            
            PurId = Request.QueryString["PurId"].ToString();
            Personid = Request.QueryString["pId"].ToString();
            if (!IsPostBack)
            { AccountSummary(Personid, PurId); 
              DropDownBankName();
              SchoolBind();
              imgSchool.ImageUrl = "~/pages/ImageHandler.ashx?roll_no=1";
              btnPrint.Visible = false;
              DivToPrint.Visible = false;
              divPayInfo.Visible = false;
            }
            
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
        public void AccountSummary(string PersonId, string PurId)
        {
            int aaaa = Convert.ToInt32(PurId);
            //var personId = Request.QueryString["PersonId"];
            //var PurId = Request.QueryString["PurId"];
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_person_items_statement_of_account_summary", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@person_id", SqlDbType.VarChar).Value = PersonId;
                    //cmd.Parameters.Add("@reg_id", SqlDbType.VarChar).Value = PurId;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        {
                            sda.Fill(dt);
                            var query = from r in dt.AsEnumerable()
                                        where r.Field<Int32>("Purch_id").Equals(aaaa)
                                        select new
                                        {
                                            Name = r["fullname"].ToString(),
                                            ItemName = r["item_name"].ToString(),
                                            Price = r["Total Price Due#"].ToString(),
                                            TotalPaid = r["Total Amount_Paid"].ToString(),
                                            SBalance = r["SBalance"].ToString(),
                                        };
                            foreach (var a in query)
                            {
                                string a1 = a.Price.ToString();
                                string a2 = a.TotalPaid.ToString();
                                string a3 = a.SBalance.ToString();
                                string a4 = a.ItemName.ToString();
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

        public void PaymentInfo()
        {
            PurId = Request.QueryString["PurId"].ToString();
            Personid = Request.QueryString["pId"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_person_items_statement_of_account_breakdown", con);
                cmd.Parameters.AddWithValue("@purch_id", PurId);
                cmd.Parameters.AddWithValue("@person_id", Personid);
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
                        DetailsViewPaymentInfo.DataBind();
 

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
        protected void FeePay_Click(object sender, EventArgs e)
        {
            lblResult.Text = "Payment Successful";
            lblResult.ForeColor = System.Drawing.Color.Blue;
            lblResult.CssClass = "h3";
            CreatedBy = HttpContext.Current.User.Identity.Name;
            SaveFeeInfo(PurId, Amount, CreatedBy);
            btnPrint.Visible = true;
            btnSubmit.Visible = false;
            divPayFields.Visible = false;
            PaymentInfo();
            divPayInfo.Visible = true;
            DivToPrint.Visible = true;
            divDetails.Visible = false;
        }
        public void SaveFeeInfo(string PurId, string Amount, string CreatedBy)
        {
            string ReplaceAmount = Regex.Replace(tbPayingAmount.Text.ToString(), "[^.0-9]", "");
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_person_item_payments_add", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@purch_id", SqlDbType.VarChar).Value = PurId;
                    cmd.Parameters.Add("@amount", SqlDbType.Decimal).Value = ReplaceAmount;
                   
                    cmd.Parameters.AddWithValue("@received_date", tbDate.Text.ToString());
                    cmd.Parameters.AddWithValue("@payment_method", ddlPaymentMethod.Text.ToString());
                    cmd.Parameters.AddWithValue("@payment_method_ref", tbPMReference.Text.ToString());
                    cmd.Parameters.AddWithValue("@bank_name", ddlBankName.Text.ToString());
                    cmd.Parameters.AddWithValue("@invoice_no", tbInvoiceNumber.Text.ToString());
                    cmd.Parameters.AddWithValue("@teller_no", tbReceiptNumber.Text.ToString());
                    cmd.Parameters.Add("@created_by", SqlDbType.VarChar).Value = CreatedBy;
                    cmd.ExecuteNonQuery(); 

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);

            }
            finally
            {
                ClearData(this);
                AccountSummary(Personid, PurId);
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

        protected void Result_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/pages/profile.aspx?PersonId=" + Personid);
            Response.Redirect("~/pages/profile.aspx?Personid=" + Personid + "&purch_id=" + PurId + "&action=3REe8GwY6X");
            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/profile.aspx?Personid=" + Personid + "&purch_id=" + PurId + "&action=3REe8GwY6X");
        }
    }
}