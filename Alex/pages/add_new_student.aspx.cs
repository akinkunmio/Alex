using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
//using Alex.App_code;

namespace Alex.pages
{
    public partial class add_new_student : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ManageCookies.VerifyAuthentication();
            TbSearch.Focus();
            
            
        }

        private void Search()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_people_search", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@f_name", SqlDbType.VarChar).Value = TbSearch.Text;
                cmd.Parameters.AddWithValue("@l_name", SqlDbType.VarChar).Value = TbSearch.Text;
                cmd.Parameters.AddWithValue("@dob", SqlDbType.VarChar).Value = TbSearch.Text;
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    GridViewAddNewStudent.Visible = true;
                    GridViewAddNewStudent.DataSource = dt;
                    GridViewAddNewStudent.DataBind();
                    DivAddProfile.Visible = false;
                    DivNotFound.Visible = true;

                }
                else
                {
                    DivAddProfile.Visible = true;
                    GridViewAddNewStudent.Visible = false;
                    DivNotFound.Visible = false;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('No Results Found ');", true);
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
        //private void AddNewStudent()
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("sp_ms_advanced_search", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        //cmd.Parameters.AddWithValue("@f_name", SqlDbType.VarChar).Value = tbAdvancedSearchFName.Text;
        //        //cmd.Parameters.AddWithValue("@l_name", SqlDbType.VarChar).Value = tbAdvancedSearchLName.Text;
        //        //cmd.Parameters.AddWithValue("@dob", SqlDbType.VarChar).Value = tbAdvancedSearchDOB.Text;
        //        //cmd.Parameters.AddWithValue("@gender", SqlDbType.VarChar).Value = tbAdvancedSearchDOB.Text;
        //        //cmd.Parameters.AddWithValue("@acad_year", SqlDbType.VarChar).Value = tbAdvancedSearchDOB.Text;
        //        //cmd.Parameters.AddWithValue("@class_name", SqlDbType.VarChar).Value = tbAdvancedSearchDOB.Text;
        //        //cmd.Parameters.AddWithValue("@form_name", SqlDbType.VarChar).Value = tbAdvancedSearchDOB.Text;
        //        //cmd.Parameters.AddWithValue("@lga_city", SqlDbType.VarChar).Value = tbAdvancedSearchDOB.Text;
        //        //cmd.Parameters.AddWithValue("@zip_postal_code", SqlDbType.VarChar).Value = tbAdvancedSearchDOB.Text;
        //        //cmd.Parameters.AddWithValue("@state", SqlDbType.VarChar).Value = tbAdvancedSearchDOB.Text;
        //        //cmd.Parameters.AddWithValue("@status", SqlDbType.VarChar).Value = tbAdvancedSearchDOB.Text;
        //        //cmd.Parameters.AddWithValue("@country", SqlDbType.VarChar).Value = tbAdvancedSearchDOB.Text;
        //        con.Open();
        //        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        sda.Fill(dt);
        //        if (dt.Rows.Count > 0)
        //        {
        //            GridViewAddNewStudent.Visible = true;
        //            GridViewAddNewStudent.DataSource = dt;
        //            GridViewAddNewStudent.DataBind();

        //        }
        //        else
        //        {
        //            GridViewAddNewStudent.Visible = false;
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('No Results Found ');", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //        // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
        //    }
        //    finally
        //    {
        //        con.Close();

        //    }

        //}
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected void GridViewAddNewStudent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hl = (HyperLink)e.Row.FindControl("HyperLinkPeople");
                if (hl != null)
                {
                    DataRowView drv = (DataRowView)e.Row.DataItem;
                    string keyword = drv["person_id"].ToString();
                    hl.NavigateUrl = "~/pages/profile.aspx?PersonId=" + keyword + "&action=reg";
                }
            }
        }
    }
}