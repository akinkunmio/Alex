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

namespace Alex.pages.admin
{
    public partial class setup_bank_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            if (!Page.IsPostBack)
            {
                BankNameBindData();
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
        protected void BtnSaveBankName_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_bank_list_add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@status_name", tbBankName.Text.ToString());

                cmd.ExecuteNonQuery();
                BankNameBindData();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Bank Name Saved Successfully');", true);
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


        protected void BankNameBindData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_bank_list_all", con);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        GridViewBankName.DataSource = dt;
                        GridViewBankName.DataBind();
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
        protected void GridViewBankName_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewBankName.EditIndex = e.NewEditIndex;
            BankNameBindData();
        }

        protected void GridViewBankName_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GridViewBankName.EditIndex = -1;
            BankNameBindData();
        }

        protected void GridViewBankName_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_bank_list_edit", con);
                GridViewRow row = GridViewBankName.Rows[e.RowIndex] as GridViewRow;
                TextBox txtCategory = row.FindControl("tbBankName") as TextBox;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "status_id", Value = GridViewBankName.Rows[e.RowIndex].Cells[0].Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "status_name", Value = txtCategory.Text });
                cmd.CommandType = CommandType.StoredProcedure;
                int Affectedrows = cmd.ExecuteNonQuery();
                GridViewBankName.EditIndex = -1;
                BankNameBindData();
                if (Affectedrows == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Cannot update, Bank name was used in Payments');", true);
                }
           }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", "alert('Cannot update, Bank used in payments');", true);
            }
            finally
            {
                con.Close();
            }
        }

        protected void GridViewBankName_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_bank_list_delete", con);
                GridViewRow row = GridViewBankName.Rows[e.RowIndex] as GridViewRow;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "status_id", Value = GridViewBankName.Rows[e.RowIndex].Cells[0].Text });
                cmd.CommandType = CommandType.StoredProcedure;
                int Affectedrows = cmd.ExecuteNonQuery();
                BankNameBindData();
                if (Affectedrows == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Cannot Delete, Bank name was used in Payments');", true);
                }
                //con.Close();
               
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Cannot Delete, Bank name was used in Payments);", true);
            }
            finally
            {
                con.Close();
            }
        }
    }
}