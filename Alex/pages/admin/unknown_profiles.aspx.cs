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
using System.Drawing;
using Alex.App_code;

namespace Alex.pages.admin
{
    public partial class unknown_profiles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            { UnknownProfilesBind(); }
        }

        private void UnknownProfilesBind()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_people_dirty_data", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        GridViewUnknownProfiles.DataSource = dt;
                        if (dt.Rows.Count == 0)
                        {
                            lblZeroRecords.Text = "No Unknow Information Profiles Found";
                        }
                        else
                        {
                            GridViewUnknownProfiles.DataBind();
                            GridViewUnknownProfiles.Visible = true;
                        }
                    }
                }
               cmd.ExecuteNonQuery();
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
        protected void GridViewUnknownProfiles_RowDataBound(object sender, GridViewRowEventArgs e)
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
            if (GridViewUnknownProfiles.Rows.Count != -1)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TableCell cell = e.Row.Cells[2];
                    string dob = cell.Text.ToString();
                    if(dob == "01 Jan 1900")
                    {
                        cell.BackColor = Color.FromName("#F12C2C");
                    }
                    TableCell cellGender = e.Row.Cells[3];
                    string Gender = cellGender.Text.ToString();
                    if (Gender == "Not Known")
                    {
                        cellGender.BackColor = Color.FromName("#F12C2C");
                    }
                    TableCell cellEthnicity = e.Row.Cells[4];
                    string Ethnicity = cellEthnicity.Text.ToString();
                    if (Ethnicity == "Not Known" || Ethnicity == "&nbsp;")
                    {
                        cellEthnicity.BackColor = Color.FromName("#F12C2C");
                    }
                    TableCell cellContactNo = e.Row.Cells[5];
                    string ContactNo = cellContactNo.Text.ToString();
                    if (ContactNo == "8000000000")
                    {
                        cellContactNo.BackColor = Color.FromName("#F12C2C");
                    }


                }
            }

        }
    }
}