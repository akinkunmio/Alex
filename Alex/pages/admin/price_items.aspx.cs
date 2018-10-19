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
    public partial class price_items : System.Web.UI.Page
    {
        string SaleItemId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            SaleItemId = Request.QueryString["Itne"].ToString();
            if (!Page.IsPostBack)
            {
                SaleItemName();
                 //DropDownYear();
                //DropDownTerm();
                //DropDownSaleItem();
                PriceListBindData();
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


        //public void DropDownYear()
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        //    String strQuery = "select acad_year_id, acad_year from ms_acad_year";

        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = strQuery;
        //    cmd.Connection = con;
        //    try
        //    {
        //        con.Open();
        //        ddlAcademicYear.DataSource = cmd.ExecuteReader();
        //        ddlAcademicYear.DataTextField = "acad_year";
        //        ddlAcademicYear.DataValueField = "acad_year_id";
        //        ddlAcademicYear.DataBind();
        //        ddlAcademicYear.Items.Insert(0, new ListItem("Please select Year", ""));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        con.Close();
        //        con.Dispose();
        //    }
        //}

       

        //public void DropDownSaleItem()
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        //    SqlCommand cmd = new SqlCommand("sp_ms_sales_items_dropdown", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    con.Open();
        //    SqlDataReader ddlValues;
        //    ddlValues = cmd.ExecuteReader();

        //    ddlSaleItem.DataSource = ddlValues;
        //    ddlSaleItem.DataValueField = "item_name";
        //    ddlSaleItem.DataTextField = "item_name";
        //    ddlSaleItem.DataBind();

        //    ddlSaleItem.Items.Insert(0, new ListItem("Please select Item", ""));

        //    cmd.Connection.Close();
        //    cmd.Connection.Dispose();
        //}   

        private void SaleItemName()
        {
           
            int ID = Convert.ToInt32(SaleItemId);
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("ms_sales_items_term_price", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@msi_id", SqlDbType.Int).Value = ID;
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                               if (dt.Rows.Count == 0)
                                {
                                    // lblZeroRecords.Visible = true;
                                    // lblZeroRecords.Text = "No Payment(s) Found ";
                                }
                                else
                                {
                                    lblItemName.Text  = dt.Rows[0]["item_name"].ToString();
                                    lblItemPrice.Text = dt.Rows[0]["price"].ToString();
                                    //lblNetPay.Text  = dt.Rows[0]["Net_Pay"].ToString();
                                }
                            }
                        }

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
        protected void BtnSavePrice_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(SaleItemId);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("ms_sales_items_term_price_list_add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                //cmd.Parameters.AddWithValue("@term_name", ddlPriceTerm.SelectedItem.ToString());
                //cmd.Parameters.AddWithValue("@acad_year", ddlAcademicYear.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@msi_id ", ID);
                cmd.Parameters.AddWithValue("@price", tbPrice.Text.ToString());
                cmd.ExecuteNonQuery();
                SaleItemName();
                PriceListBindData();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Price Saved Successfully');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Cannot Add Duplicate Record');", true);
            }
            finally
            {
                con.Close();
                ClearData(this);
            }
        }

        protected void PriceListBindData()
        {
            int ID = Convert.ToInt32(SaleItemId);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("ms_sales_items_term_price_list_history", con);
                cmd.Parameters.AddWithValue("@msi_id", ID);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        GridViewPriceList.DataSource = dt;
                        if (dt.Rows.Count == 0)
                        {
                            lblZeroRecords.Visible = true;
                            lblZeroRecords.Text = "No Price List Found ";
                            GridViewPriceList.Visible = false;
                        }
                        else
                        {
                            GridViewPriceList.DataBind();
                            lblZeroRecords.Visible = false;
                            GridViewPriceList.Visible = true;
                            
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

        //protected void GridViewPriceList_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    GridViewPriceList.EditIndex = e.NewEditIndex;
        //    PriceListBindData();
        //}

        //protected void GridViewPriceList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    e.Cancel = true;
        //    GridViewPriceList.EditIndex = -1;
        //    PriceListBindData();
        //}

        //protected void GridViewPriceList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        //    try
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("ms_sales_items_term_price_list_edit", con);
        //        GridViewRow row = GridViewPriceList.Rows[e.RowIndex] as GridViewRow;
        //        TextBox txtPrice = row.FindControl("tbEditPrice") as TextBox;
               
        //        cmd.Parameters.Add(new SqlParameter() { ParameterName = "msi_pri_id", Value = GridViewPriceList.Rows[e.RowIndex].Cells[0].Text });

        //        cmd.Parameters.Add(new SqlParameter() { ParameterName = "price", Value = txtPrice.Text });
                

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //        GridViewPriceList.EditIndex = -1;
        //        PriceListBindData();
                
        //    }
        //    catch //(Exception ex)
        //    {
        //       // ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", "alert('Cannot update, Sale Item already exist');", true);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        //protected void GridViewPriceList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        //    try
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("ms_sales_items_term_price_list_delete", con);
        //        GridViewRow row = GridViewPriceList.Rows[e.RowIndex] as GridViewRow;
        //        cmd.Parameters.Add(new SqlParameter() { ParameterName = "msi_pri_id", Value = GridViewPriceList.Rows[e.RowIndex].Cells[0].Text });
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //        PriceListBindData();
        //    }
        //    catch
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Cannot Delete, Purchases was made);", true);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        //protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ddlPriceTerm.Items.Clear();
        //    ddlPriceTerm.Items.Add(new ListItem("Please Select Term", ""));
        //    //ddlFeeSetupForm.Items.Clear();
        //    //ddlFeeSetupForm.Items.Add(new ListItem("Select Class", ""));

        //    ddlPriceTerm.AppendDataBoundItems = true;
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        //    String strQuery = "select ay_term_id, term_name from ms_acad_year_term " +
        //              "where acad_year_id=@acad_year_id";

        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Parameters.AddWithValue("@acad_year_id",
        //         ddlAcademicYear.SelectedItem.Value);
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = strQuery;
        //    cmd.Connection = con;
        //    try
        //    {
        //        con.Open();
        //        ddlPriceTerm.DataSource = cmd.ExecuteReader();
        //        ddlPriceTerm.DataTextField = "term_name";
        //        ddlPriceTerm.DataValueField = "ay_term_id";
        //        ddlPriceTerm.DataBind();
        //        if (ddlPriceTerm.Items.Count > 1)
        //        {
        //            ddlPriceTerm.Enabled = true;
                    
        //        }
        //        else
        //        {
        //            ddlPriceTerm.Enabled = false;
                    
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //    }
        //    finally
        //    {
        //        con.Close();
        //        con.Dispose();
        //    }
        //}
    }
}