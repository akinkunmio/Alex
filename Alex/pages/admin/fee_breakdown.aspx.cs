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
    public partial class fee_breakdown : System.Web.UI.Page
    {
        string FeeID = string.Empty;
        string Amount = string.Empty;
        string FormClass = string.Empty;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            FeeID = Request.QueryString["Fee_ID"].ToString();
            Amount = Request.QueryString["FAm"].ToString();
            FormClass = Request.QueryString["FC"].ToString();
            if (!IsPostBack)
            {
                lblFee.Text = Amount;
                lblClass.Text = FormClass;
                lblBrkDwnFeeClass.Text = FormClass;
                FeeBrkDwnBindData();
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

        protected void BtnSaveSetupFeeBrkDwn_Click(object sender, EventArgs e)
        {
            CheckingAmount();

            
         }
       public void SaveBrkDownFee(){
                
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_fee_breakdown_add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@fee_id", FeeID);
                cmd.Parameters.AddWithValue("@item", tbSetupFeeItem.Text.ToString());
                cmd.Parameters.AddWithValue("@amount", tbAmount.Text.ToString());
                cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                cmd.ExecuteNonQuery();
               FeeBrkDwnBindData();
               
               ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Fee Breakdown Saved Successfully');", true);

            }
            catch (Exception ex)
            {
               ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
              
            }
            finally
            {
                con.Close();
                ClearData(this);
            }
        }

        protected void FeeBrkDwnBindData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_fee_breakdown_list_fee_id", con);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@fee_id", FeeID);
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        GridViewSetupFeeBrkDwn.DataSource = dt;
                        if (dt.Rows.Count == 0)
                        {
                            lblZeroRecords.Visible = true;
                            lblZeroRecords.Text = "No BreakDown Fee Records Found ";
                            GridViewSetupFeeBrkDwn.Visible = false;
                        }
                        else
                        {
                            
                            lblZeroRecords.Visible = false;
                          GridViewSetupFeeBrkDwn.DataBind();
                          GridViewSetupFeeBrkDwn.Visible = true;
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

        protected void GridViewSetupFeeBrkDwn_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewSetupFeeBrkDwn.EditIndex = e.NewEditIndex;
            FeeBrkDwnBindData();
        }

        protected void GridViewSetupFeeBrkDwn_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GridViewSetupFeeBrkDwn.EditIndex = -1;
            FeeBrkDwnBindData();
        }

        protected void GridViewSetupFeeBrkDwn_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_fee_breakdown_delete", con);
                GridViewRow row = GridViewSetupFeeBrkDwn.Rows[e.RowIndex] as GridViewRow;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "fb_id", Value = GridViewSetupFeeBrkDwn.Rows[e.RowIndex].Cells[0].Text });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
                FeeBrkDwnBindData();
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

        protected void GridViewSetupFeeBrkDwn_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_fee_breakdown_edit", con);
                GridViewRow row = GridViewSetupFeeBrkDwn.Rows[e.RowIndex] as GridViewRow;
                TextBox tbItem = row.FindControl("txtItem") as TextBox;
                TextBox txtAmount = row.FindControl("tbFeeAmount") as TextBox;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "fb_id", Value = GridViewSetupFeeBrkDwn.Rows[e.RowIndex].Cells[0].Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "item", Value = tbItem.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "amount", Value = txtAmount.Text });
                cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
                GridViewSetupFeeBrkDwn.EditIndex = -1;
                FeeBrkDwnBindData();
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
        protected void CheckingAmount()
        {
            int RemAmount;
            RemAmount = Convert.ToInt32(tbAmount.Text);
            double txtAmount = Convert.ToDouble(RemAmount);
            double Price = (Convert.ToDouble(Amount) - Convert.ToDouble(Session["TotalAmount"]));
            if (txtAmount > Price)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Amount Is Exceeding, Please Try With Some Another Amount...');", true);
            }
            else { SaveBrkDownFee(); }
        }

        int TotalAmount;
        protected void GridViewSetupFeeBrkDwn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType==DataControlRowType.DataRow)
            {
                TotalAmount += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Amount"));
                Session["TotalAmount"] = TotalAmount;
            }
            else if(e.Row.RowType==DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total Amount";
                e.Row.Cells[0].Font.Bold = true;

                e.Row.Cells[1].Text = String.Format("{0,0:N2}",TotalAmount.ToString());
                e.Row.Cells[1].Font.Bold = true;
                
            }

            
        }
    }
}