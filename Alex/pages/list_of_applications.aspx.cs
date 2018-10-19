using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Configuration;
//using Alex.App_code;


namespace Alex.pages
{
    public partial class list_of_applications : System.Web.UI.Page
    {
        //string selectedValue = string.Empty;
        //private static string currentYear = DateTime.Now.Year.ToString();
        //private static string prevYear = (Convert.ToInt32(currentYear) - 1).ToString();
        //private static string yearFormat = prevYear + "/" + currentYear.Substring(2);
        protected void Page_Load(object sender, EventArgs e)
        {
            //ManageCookies.VerifyAuthentication();
            lblZeroRecords.Text = "";
            GridViewListOfApplications.Visible = false;
            if (!Page.IsPostBack)
            {
                DropDownYear();
                DropDownTerm();
                AppStatusDropDown();
               // ddlTerm.SelectedIndex = 1;
                ddlStatus.SelectedIndex = 1;
               
                this.ViewState["CurrentAlphabet"] = "ALL";
                this.GenerateAlphabets();
                BindGrid(this.ViewState["CurrentAlphabet"].ToString());
                
            }
        }

        private void GenerateAlphabets()
        {
            List<ListItem> alphabets = new List<ListItem>();
            ListItem alphabet = new ListItem();
            alphabet.Value = "ALL";
            alphabet.Selected = alphabet.Value.Equals(ViewState["CurrentAlphabet"]);
            alphabets.Add(alphabet);
            for (int i = 65; i <= 90; i++)
            {
                alphabet = new ListItem();
                alphabet.Value = Char.ConvertFromUtf32(i);
                alphabet.Selected = alphabet.Value.Equals(ViewState["CurrentAlphabet"]);
                alphabets.Add(alphabet);
            }
            rptAlphabets.DataSource = alphabets;
            rptAlphabets.DataBind();
        }

        protected void Alphabet_Click(object sender, EventArgs e)
        {
            if (this.ViewState["CurrentAlphabet"] != null)
            {
                LinkButton lnkAlphabet = (LinkButton)sender;
                ViewState["CurrentAlphabet"] = lnkAlphabet.Text;
                string StartAlpha = lnkAlphabet.Text;
                this.GenerateAlphabets();
                BindGrid(StartAlpha);
            }
        }

        public void DropDownTerm()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_term_dropdown_v2", con);
            cmd.Parameters.Add("@acad_year", SqlDbType.VarChar).Value = ddlAcademicYear.SelectedItem.ToString();
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
               con.Open();
               SqlDataReader ddlValues;
               ddlValues = cmd.ExecuteReader();
               string TermSelectedValue = null;
            while (ddlValues.Read())
            {
                TermSelectedValue = ddlValues[0].ToString();
                int DefaultValue = Convert.ToInt32(ddlValues[1]);
                if (DefaultValue == 1)
                    break;
            }
               ddlValues.Close();
               ddlValues = cmd.ExecuteReader();
               ddlTerm.DataSource = ddlValues;
               ddlTerm.DataValueField = "term_name";
               ddlTerm.DataTextField = "term_name";
               ddlTerm.DataBind();
               ddlTerm.Items.Insert(0, new ListItem("Please select Term", ""));
               ddlTerm.SelectedValue = TermSelectedValue;

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
        public void AppStatusDropDown()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_status_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@category", "Applications");
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlStatus.DataSource = ddlValues;
            ddlStatus.DataValueField = "status_name";
            ddlStatus.DataTextField = "status_name";
            ddlStatus.DataBind();
            //Adding "Please select" option in dropdownlist for validation
            ddlStatus.Items.Insert(0, new ListItem("Please select Status", ""));

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
       

        private void BindGrid(string StartAlpha)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);

            try
            {
                string SP_Name = "sp_ms_rep_application_list_all_year_term_status";
                if (StartAlpha != "ALL")
                {
                    SP_Name = "sp_ms_rep_application_list_all_year_term_status_alphabetic";
                }
                using (SqlCommand cmd = new SqlCommand(SP_Name, con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        if (StartAlpha != "ALL")
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            con.Open();
                            cmd.Parameters.AddWithValue("@lastname", StartAlpha);
                            //cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            con.Open();
                            cmd.Connection = con;
                        }
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@acad_year", ddlAcademicYear.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@term_name", ddlTerm.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedItem.ToString());
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            GridViewListOfApplications.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                GridViewListOfApplications.Visible = false;
                                lblZeroRecords.Visible = true;
                                lblZeroRecords.Text = "No Records found ";
                            }
                            else
                            {
                                GridViewListOfApplications.Visible = true;
                                GridViewListOfApplications.DataBind();
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
        //private void ApplicationsList()
        //{
        //   SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        //        try
        //        {
        //            SqlCommand cmd = new SqlCommand("sp_ms_rep_application_list_all_year", con);
        //             cmd.CommandType = CommandType.StoredProcedure;

        //             cmd.Parameters.AddWithValue("@acad_year", ddlAcademicYear.SelectedItem.ToString());

        //             using (SqlDataAdapter sda = new SqlDataAdapter())
        //             {
        //                 con.Open();
        //                 cmd.Connection = con;
        //                 sda.SelectCommand = cmd;
        //                 using (DataTable dt = new DataTable())
        //                 {
        //                     sda.Fill(dt);
        //                     GridViewListOfApplications.DataSource = dt;
        //                     if (dt.Rows.Count == 0)
        //                     {
        //                         lblZeroRecords.Text = "No Records found for the selected Year";
        //                     }
        //                     else
        //                     {
        //                         GridViewListOfApplications.DataBind();
        //                         GridViewListOfApplications.Visible = true;
        //                     }
        //                 }
        //             }
        //         }
        //        catch (Exception ex)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //            // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
        //        }
        //        finally
        //        {
        //            con.Close();
        //        }
        //    }
       
     public void DropDownYear()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_acad_year_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            try
            {
              SqlDataReader ddlValues;
              ddlValues = cmd.ExecuteReader();
              string YearSelectedValue = null;

              while (ddlValues.Read())
              {
                YearSelectedValue = ddlValues[0].ToString();
                int DefaultValue = Convert.ToInt32(ddlValues[1]);
                if (DefaultValue == 1)
                    break;
              }
             ddlValues.Close();
             ddlValues = cmd.ExecuteReader();

             ddlAcademicYear.DataSource = ddlValues;
             ddlAcademicYear.DataValueField = "acad_year";
             ddlAcademicYear.DataTextField = "acad_year";
             ddlAcademicYear.DataBind();
             ddlAcademicYear.Items.Insert(0, new ListItem("Select Academic Year", ""));
             ddlAcademicYear.SelectedValue = YearSelectedValue;
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
        protected void GridViewListOfApplications_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hl = (HyperLink)e.Row.FindControl("HyperLinkPeople");
                if (hl != null)
                {
                    DataRowView drv = (DataRowView)e.Row.DataItem;
                    string keyword = drv["person_id"].ToString();
                    hl.NavigateUrl = "~/pages/profile.aspx?PersonId=" + keyword;
                }
            }
        }

        //protected void btnSearchAcadmicYear_Click(object sender, EventArgs e)
        //{
        //    this.ViewState["CurrentAlphabet"] = "ALL";
        //    this.GenerateAlphabets();
        //    BindGrid(this.ViewState["CurrentAlphabet"].ToString());
        //}

        protected void GridViewListOfApplications_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewListOfApplications.PageIndex = e.NewPageIndex;
            //ApplicationsList();
            BindGrid("ALL");
        }

        protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownTerm();
            if (ddlTerm.Items.Count > 1)
            {
                ddlTerm.SelectedIndex = 1;
                BindGrid("ALL");
            }
          
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ViewState["CurrentAlphabet"] = "ALL";
            this.GenerateAlphabets();
            BindGrid(this.ViewState["CurrentAlphabet"].ToString());
        }

        protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ViewState["CurrentAlphabet"] = "ALL";
            this.GenerateAlphabets();
            BindGrid(this.ViewState["CurrentAlphabet"].ToString());
        }
    }
}