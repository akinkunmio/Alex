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

namespace Alex.pages
{
    public partial class fee_receipt : System.Web.UI.Page
    {

        string PayID = string.Empty;
        string FeeType = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
           ManageCookies.VerifyAuthentication();
           PayID = Request.QueryString["pay_id"].ToString();
           FeeType = Request.QueryString["FeeType"].ToString();
           if (!IsPostBack)
            {
              SchoolBind();
              imgSchool.ImageUrl = "~/pages/ImageHandler.ashx?roll_no=1";
              PaymentInfo();
            }
        }

        public void SchoolBind()
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
                    SchoolName = dr["school_name"].ToString();
                    string Address = dr["address_line1"].ToString();
                    string Address2 = dr["address_line2"].ToString();
                    string City = dr["lga_city"].ToString();
                    string State = dr["state"].ToString();
                    string country = dr["country"].ToString();
                    string Postcode = dr["zip_postal_code"].ToString();
                    string Email = dr["contact_email"].ToString();
                    string PhoneNo = dr["contact_no1"].ToString();

                    lblSchoolName.Text = SchoolName.ToString();
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


        public void PaymentInfo()
        {
            PayID = Request.QueryString["pay_id"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_person_statement_of_account_breakdown_receipt", con);
                cmd.Parameters.AddWithValue("@payment_id", PayID);
                cmd.Parameters.AddWithValue("@fee_type", FeeType);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        lblFormName.Text = dt.Rows[0]["form_name"].ToString();
                        lblAcademicYear.Text = dt.Rows[0]["acad_year"].ToString();
                        lblStuName.Text = dt.Rows[0]["fullname"].ToString();
                        lblTermName.Text = dt.Rows[0]["term_name"].ToString();
                        lblFeeType.Text = dt.Rows[0]["Fee_Type"].ToString();
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

        protected void btnCloseWindow_Click(object sender, EventArgs e)
        {
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
        }

        protected void DetailsViewPaymentInfo_DataBound(object sender, EventArgs e)
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