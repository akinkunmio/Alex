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

namespace Alex.pages
{
    public partial class sales_items : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            if (!Page.IsPostBack)
            {
               ItemBindData();
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
        
        protected void BtnSaveItem_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_sales_items_add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@item_name", tbItemName.Text.ToString());
                cmd.Parameters.AddWithValue("@price", tbPrice.Text.ToString());
                cmd.Parameters.AddWithValue("@description", tbDescription.Text.ToString());
                cmd.Parameters.AddWithValue("@status", ddlStatus.Text.ToString());
                cmd.Parameters.AddWithValue("@add_msi_id", DBNull.Value);
                cmd.ExecuteNonQuery();
                ItemBindData();
               
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Sales Item Saved Successfully');", true);
            }
            catch //(Exception ex)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Cannot Add Duplicate Record');", true);
            }
            finally
            {
                con.Close();
                ClearData(this);
            }
        }

        protected void ItemBindData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_sales_items_list_all", con);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        GridViewItem.DataSource = dt;
                        GridViewItem.DataBind();
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

        protected void GridViewItem_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewItem.EditIndex = e.NewEditIndex;
            ItemBindData();
        }

        protected void GridViewItem_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GridViewItem.EditIndex = -1;
            ItemBindData();
        }

        protected void GridViewItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_sales_items_edit", con);
                GridViewRow row = GridViewItem.Rows[e.RowIndex] as GridViewRow;
                TextBox txtSaleItem = row.FindControl("tbItem") as TextBox;
                TextBox txtDescription = row.FindControl("tbEditDescription") as TextBox;
                DropDownList txtStatus = row.FindControl("ddlEditStatus") as DropDownList;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "msi_id", Value = GridViewItem.Rows[e.RowIndex].Cells[0].Text });

                cmd.Parameters.Add(new SqlParameter() { ParameterName = "item_name", Value = txtSaleItem.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "description", Value = txtDescription.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "status", Value = txtStatus.Text });

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
                GridViewItem.EditIndex = -1;
                ItemBindData();
                //TermActiveBindData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", "alert('Cannot update, Sale Item already exist');", true);
            }
            finally
            {
                con.Close();
            }
        }

        protected void GridViewItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_sales_items_delete", con);
                GridViewRow row = GridViewItem.Rows[e.RowIndex] as GridViewRow;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "msi_id", Value = GridViewItem.Rows[e.RowIndex].Cells[0].Text });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
                ItemBindData();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("FK_ms_purchases_ms_sales_items"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Unable to delete sales item, a profile has purchased the item.')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                }
                
            }
            finally
            {
                con.Close();
            }
        }

        protected void btnSetPrice_Click(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((Button)sender).Parent.Parent)
            {
                string Id = row.Cells[0].Text;
                //Response.Redirect("price_items.aspx?KOYB7Dr3REe8GwY6X-JZtHLveaKCnAtyGxSfU/edit#" + "&Itne=" + Id.ToString());
                Response.Redirect("price_items.aspx?KOYB7Dr3REe8GwY6X-JZtHLveaKCnAtyGxSfU/edit" + "&Itne=" + Id.ToString() + "&orders/14459507787493959349111/items/144595077874939593");
               
            }
        }
    }
}