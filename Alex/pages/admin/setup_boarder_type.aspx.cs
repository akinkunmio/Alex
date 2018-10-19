using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Alex.pages.admin
{
    public partial class setup_boarder_type : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BorderTypeBind(); ;
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
        protected void BtnSaveBoarderType_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_boarder_type_add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@type", tbBoarderType.Text.ToString());
                cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                cmd.ExecuteNonQuery();
                BorderTypeBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Boarder Type Saved Successfully');", true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Cannot Add Duplicate Record');", true);
            }
            finally
            {
                con.Close();
                ClearData(this);
            }
        }

        protected void BorderTypeBind()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_boarder_type_list_all", con);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        GridViewBoarderType.DataSource = dt;
                        if (dt.Rows.Count == 0)
                        {
                            GridViewBoarderType.Visible = false;
                            lblZeroRecords.Visible = true;
                            lblZeroRecords.Text = "No Boarder Type Records found ";
                        }
                        else
                        {
                            GridViewBoarderType.Visible = true;
                            GridViewBoarderType.DataBind();
                            lblZeroRecords.Visible = false;
                        }
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

        protected void GridViewBoarderType_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewBoarderType.EditIndex = e.NewEditIndex;
            BorderTypeBind();
        }

        protected void GridViewBoarderType_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GridViewBoarderType.EditIndex = -1;
            BorderTypeBind();
        }

        protected void GridViewBoarderType_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_boarder_type_edit", con);
                GridViewRow row = GridViewBoarderType.Rows[e.RowIndex] as GridViewRow;
                TextBox txtBoarderType = row.FindControl("tbBoarderDescription") as TextBox;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "boarder_type_id", Value = GridViewBoarderType.Rows[e.RowIndex].Cells[0].Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "type_description", Value = txtBoarderType.Text });
                cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
                GridViewBoarderType.EditIndex = -1;
                BorderTypeBind();
            }
            catch (Exception Ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + Ex.Message.ToString() + "');", true);
                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "clentscript", "alert('Cannot update, Class already exist');", true);
            }
            finally
            {
                con.Close();
            }
        }

        protected void GridViewBoarderType_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_boarder_type_delete", con);
                GridViewRow row = GridViewBoarderType.Rows[e.RowIndex] as GridViewRow;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "boarder_type_id", Value = GridViewBoarderType.Rows[e.RowIndex].Cells[0].Text });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
                BorderTypeBind();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Cannot Delete, Boarder Type was used in Registrations');", true);
            }
            finally
            {
                con.Close();
            }
        }
    }
}