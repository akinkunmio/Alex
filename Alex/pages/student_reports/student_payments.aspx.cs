using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Alex.App_code;

namespace Alex.pages.student_reports
{
    public partial class student_payments : System.Web.UI.Page
    {
        int lvl = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            Level();
            if (!Page.IsPostBack)
            {
               SchoolBind();
               DropDownFormClass();
               ddlFormClass.SelectedIndex = 1;
               ddlView.SelectedValue = "Tuition Fee Payments Recieved Today";
               PaymentSummary(); 
                ViewBind();
            }
            if (lvl == 2 || lvl == 3 || lvl == 4 || lvl == 5)
            {
                Response.Redirect("~/pages/ForbiddenAccess.aspx", false);
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
                ddlFormClass.Items.Insert(1, new ListItem("ALL"));
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

        private void PaymentSummary()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_payments_list_today_week_month2", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@view", SqlDbType.VarChar).Value = ddlView.SelectedItem.ToString();
                        cmd.Parameters.AddWithValue("@form_class", ddlFormClass.SelectedItem.ToString());
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);


                                lblTotalAmountPaid.Text = dt.Rows[0]["Total_Amount_Paid"].ToString();
                                lblBank.Text = dt.Rows[0]["Bank"].ToString();
                                lblCash.Text = dt.Rows[0]["Cash"].ToString();
                                lblCheque.Text = dt.Rows[0]["Cheque"].ToString();
                                lblOther.Text = dt.Rows[0]["Other"].ToString();
                                                                 
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


        private void ViewBind()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_payments_list_today_week_month", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@view", SqlDbType.VarChar).Value = ddlView.SelectedItem.ToString();
                    cmd.Parameters.AddWithValue("@form_class", ddlFormClass.SelectedItem.ToString());
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {

                            sda.Fill(dt);
                            GridViewStudentPayments.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                GridViewStudentPayments.Visible = false;
                                btnPrint.Visible = false;
                                lblZeroStudents.Visible = true;
                                lblZeroStudents.Text = "No fee payments found ";
                                DivStudents.Visible = false;
                            }
                            else
                            {
                                DivStudents.Visible = true;
                                lblZeroStudents.Visible = false;
                                if (ddlView.SelectedItem.ToString() == "Fee Payments Made Today")
                                {
                                    this.GridViewStudentPayments.Columns[0].Visible = false;
                                }
                                else { this.GridViewStudentPayments.Columns[0].Visible = true; }
                                GridViewStudentPayments.DataBind();
                                GridViewStudentPayments.Visible = true;
                                imgSchool.ImageUrl = "~/pages/ImageHandler.ashx?roll_no=1";
                                lblViewSelected.Text = ddlView.SelectedItem.ToString();
                                btnPrint.Visible = true;
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

        public void SchoolBind()
        {
            SqlDataAdapter adp = new SqlDataAdapter();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_settings_school_detail", con);
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

        protected void ddlView_SelectedIndexChanged(object sender, EventArgs e)
        {
            PaymentSummary();
            ViewBind();
        }

        protected void ddlFormClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            PaymentSummary();
            ViewBind();
        }
       
    }
}