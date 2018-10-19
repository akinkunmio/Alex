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
using Alex.App_code;
using System.Drawing;

namespace Alex.pages.admin
{
    public partial class unknown_address : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            { UnknownAddressBind(); }
        }
        private void UnknownAddressBind()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_address_dirty_data", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        GridViewUnknownAddress.DataSource = dt;
                        if (dt.Rows.Count == 0)
                        {
                            lblZeroRecords.Text = "No Unknow Address Profiles Found";
                        }
                        else
                        {
                            GridViewUnknownAddress.DataBind();
                            GridViewUnknownAddress.Visible = true;
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
        protected void GridViewUnknownAddress_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hl = (HyperLink)e.Row.FindControl("HyperLinkPeople");
                if (hl != null)
                {
                    DataRowView drv = (DataRowView)e.Row.DataItem;
                    string keyword = drv["person_id"].ToString();
                    hl.NavigateUrl = "~/pages/profile.aspx?PersonId=" + keyword + "&action=add";
                }
            }
            if (GridViewUnknownAddress.Rows.Count != -1)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TableCell cell = e.Row.Cells[2];
                    string Address = cell.Text.ToString();
                    if (Address == "Not Known")
                    {
                        cell.BackColor = Color.FromName("#F12C2C");
                    }
                    TableCell cellLGA = e.Row.Cells[3];
                    string LGA = cellLGA.Text.ToString();
                    if (LGA == "Not Known")
                    {
                        cellLGA.BackColor = Color.FromName("#F12C2C");
                    }
                    TableCell cellState = e.Row.Cells[4];
                    string State= cellState.Text.ToString();
                    if (State == "Not Known")
                    {
                        cellState.BackColor = Color.FromName("#F12C2C");
                    }
                    TableCell cellCountry = e.Row.Cells[5];
                    string Country = cellCountry.Text.ToString();
                    if (Country == "Not Known")
                    {
                        cellCountry.BackColor = Color.FromName("#F12C2C");
                    }


                }
            }

        }
    }
}