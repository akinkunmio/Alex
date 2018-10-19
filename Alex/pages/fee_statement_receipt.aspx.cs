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
    public partial class fee_statement_receipt : System.Web.UI.Page
    {
       //double total = 0;
       double TotalPaid = 0;
       string SchoolFee = string.Empty;
       string PersonID = string.Empty;
       string RegID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            PersonID = Request.QueryString["PID"].ToString();
            RegID = Request.QueryString["RID"].ToString();
            if (!IsPostBack)
            {
                SchoolBind();
                imgSchool.ImageUrl = "~/pages/ImageHandler.ashx?roll_no=1";
                StatementBreakDown();   
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
        public void StatementBreakDown()
        {
            var personId = Request.QueryString["PID"];
            var regid = Request.QueryString["RID"];
            TotalPaid = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_person_statement_of_account_breakdown", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@person_id", SqlDbType.VarChar).Value = personId;
                    cmd.Parameters.Add("@reg_id", SqlDbType.VarChar).Value = regid;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;

                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridViewStatementOfAccount.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                lblZeroRecords.Visible = true;
                                lblZeroRecords.Text = "No Payment(s) Found ";
                                GridViewStatementOfAccount.Visible = false;
                                
                            }
                            else
                            {
                                lblStuName.Text = dt.Rows[0]["fullname"].ToString();
                                lblFormName.Text = dt.Rows[0]["form_name"].ToString();
                                lblAcademicYear.Text = dt.Rows[0]["acad_year"].ToString();
                                lblTermName.Text = dt.Rows[0]["term_name"].ToString();
                                SchoolFee = dt.Rows[0]["School Fees"].ToString();
                                GridViewStatementOfAccount.DataBind();
                                GridViewStatementOfAccount.Visible = true;
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

        private double ParseDouble(string value)
        {
            double d = 0;
            if (!double.TryParse(value, out d))
            {
                return 0;
            }
            return d;
        }
        protected void GridViewStatementOfAccount_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               Label lblB = (Label)e.Row.FindControl("lblAmountPaid");
                var Balance = ParseDouble(lblB.Text);
                TotalPaid = TotalPaid + Balance;
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                Label lblFee = (Label)e.Row.FindControl("lblFeeA");
                //double TermFeeToatal = Double.Parse(SchoolFee);
                lblFee.Text = "Fee: ₦" + "   " + String.Format("{0:n}", SchoolFee);
                lblFee.Font.Bold = false;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPaid = (Label)e.Row.FindControl("lblTotalPaid");
                lblTotalPaid.Text = "Total Paid:  ₦" + "  " + String.Format("{0:n}", TotalPaid);
                //extracalucation for labelbalancedOwed
                Label lblBalanceOwed = (Label)e.Row.FindControl("lblBalanceOwed");
                //double TermFeeToatal = Double.Parse(lblFee.Text);
                double TermFeeToatal = Double.Parse(SchoolFee);
                lblBalanceOwed.Text = "Balance Owed: ₦" + String.Format("{0:n}", TermFeeToatal - TotalPaid);
            }
        }


        protected void btnCloseWindow_Click(object sender, EventArgs e)
        {
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
        }
    }
}