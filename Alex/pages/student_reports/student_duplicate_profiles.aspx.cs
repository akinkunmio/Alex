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
    public partial class student_duplicate_profiles : System.Web.UI.Page
    {
        int lvl = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            Level();
            if (!Page.IsPostBack && lvl != 4 && lvl != 5)
            {
               SchoolBind();
               DuplicateStudentsDataBind();
                
            }
            else 
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
       
       
       

        private void DuplicateStudentsDataBind()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_find_identical_or_duplicate_profiles", con))
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
                            GridViewDuplicateStudents.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                lblZeroRecords.Visible = true;
                                lblZeroRecords.Text = "No  Expenses Found ";
                                GridViewDuplicateStudents.Visible = false;
                                btnPrint.Visible = false;
                            }
                            else
                            {
                                lblZeroRecords.Visible = false;
                                GridViewDuplicateStudents.DataBind();
                                GridViewDuplicateStudents.Visible = true;
                                imgSchool.ImageUrl = "~/pages/ImageHandler.ashx?roll_no=1";
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
    }
}