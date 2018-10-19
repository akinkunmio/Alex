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
    public partial class add_new_applicant : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // ManageCookies.VerifyAuthentication();
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
                    GridViewAddNewApplicant.Visible = true;
                    GridViewAddNewApplicant.DataSource = dt;
                    GridViewAddNewApplicant.DataBind();
                    DivAddProfile.Visible = false;
                    DivNotFound.Visible = true;

                }
                else
                {
                    DivAddProfile.Visible = true;
                    GridViewAddNewApplicant.Visible = false;
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
      
        protected void GridViewAddNewApplicant_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hl = (HyperLink)e.Row.FindControl("HyperLinkPeople");
                if (hl != null)
                {
                    DataRowView drv = (DataRowView)e.Row.DataItem;
                    string keyword = drv["person_id"].ToString();
                    hl.NavigateUrl = "~/pages/profile.aspx?PersonId=" + keyword + "&action=app";
                }
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }
    }
}