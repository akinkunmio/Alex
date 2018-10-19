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
    public partial class setup_boarder_fees : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DropDownYear();
                BoarderfeeBindData();
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

        public void DropDownYear()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_active_acad_year_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            try
            {
                con.Open();
                ddlBoarderFeeSetupYear.DataSource = cmd.ExecuteReader();
                ddlBoarderFeeSetupYear.DataTextField = "acad_year";
                ddlBoarderFeeSetupYear.DataValueField = "acad_year_id";
                ddlBoarderFeeSetupYear.DataBind();
                ddlBoarderFeeSetupYear.Items.Insert(0, new ListItem("Please select Year", ""));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        protected void ddlBoarderFeeSetupYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlBoarderFeeSetupTerm.Items.Clear();
            ddlBoarderFeeSetupTerm.Items.Add(new ListItem("Please Select Term", ""));
            ddlBoarderFeeSetupTerm.AppendDataBoundItems = true;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            String strQuery = "select ay_term_id, term_name from ms_acad_year_term " +
                      "where acad_year_id=@acad_year_id";

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@acad_year_id",
                 ddlBoarderFeeSetupYear.SelectedItem.Value);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strQuery;
            cmd.Connection = con;
            try
            {
                con.Open();
                ddlBoarderFeeSetupTerm.DataSource = cmd.ExecuteReader();
                ddlBoarderFeeSetupTerm.DataTextField = "term_name";
                ddlBoarderFeeSetupTerm.DataValueField = "ay_term_id";
                ddlBoarderFeeSetupTerm.DataBind();

                if (ddlBoarderFeeSetupTerm.Items.Count > 1)
                {
                    ddlBoarderFeeSetupTerm.Enabled = true;
                    ddlBoarderFeeSetupBrType.Enabled = true;
                }
                else
                {
                    ddlBoarderFeeSetupTerm.Enabled = false;
                    ddlBoarderFeeSetupBrType.Enabled = false;
                }
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

        protected void ddlBoarderFeeSetupTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlBoarderFeeSetupBrType.Items.Clear();
            ddlBoarderFeeSetupBrType.Items.Add(new ListItem("Select Type", ""));
            ddlBoarderFeeSetupBrType.AppendDataBoundItems = true;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ms_boarder_fees_types_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@acad_year", ddlBoarderFeeSetupYear.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@term_name", ddlBoarderFeeSetupTerm.SelectedItem.ToString());
            try
            {

                ddlBoarderFeeSetupBrType.DataSource = cmd.ExecuteReader();
                ddlBoarderFeeSetupBrType.DataTextField = "type_description";
                ddlBoarderFeeSetupBrType.DataValueField = "type_description";
                ddlBoarderFeeSetupBrType.DataBind();
                if (ddlBoarderFeeSetupBrType.Items.Count > 1)
                {
                    ddlBoarderFeeSetupBrType.Enabled = true;
                    tbBoarderFeeSetupAmount.Enabled = true;
                    BtnSaveBoarderFeSetup.Enabled = true;
                }
                else
                {
                    ddlBoarderFeeSetupBrType.Items.Remove(new ListItem("Select Type", ""));
                    ddlBoarderFeeSetupBrType.Items.Add(new ListItem("Fee has been setup for all the Boarder Types", ""));
                    ddlBoarderFeeSetupBrType.Enabled = false;
                    tbBoarderFeeSetupAmount.Enabled = false;
                    BtnSaveBoarderFeSetup.Enabled = false;
                    //BoarderfeeBindData();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        protected void BoarderfeeBindData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                //sp_ms_settings_fees_search_year_term
                SqlCommand cmd = new SqlCommand("sp_ms_boarder_fees_active_year_term", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@acad_year", ddlAcademicYear.SelectedItem.ToString());
                //cmd.Parameters.AddWithValue("@term_name", ddlTerm.SelectedItem.ToString());

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        GridViewBoarderFee.DataSource = dt;
                        if (dt.Rows.Count == 0)
                        {
                            lblZeroRecords.Visible = true;
                            lblZeroRecords.Text = "No Boarder Fee found ";
                            GridViewBoarderFee.Visible = false;
                        }
                        else
                        {
                            GridViewBoarderFee.Visible = true; 
                            GridViewBoarderFee.DataBind();
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

        protected void BtnSaveBoarderFeSetup_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_boarder_fees_add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@acad_year", ddlBoarderFeeSetupYear.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@term_name", ddlBoarderFeeSetupTerm.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@type_description", ddlBoarderFeeSetupBrType.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@amount", tbBoarderFeeSetupAmount.Text.ToString());
                cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                cmd.ExecuteNonQuery();
                //BoarderfeeBindData();
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Fee Saved Successfully');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Fee Saved Successfully');window.location = '../../pages/admin/setup_boarder_fees.aspx';", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
                ClearData(this);
            }
        }

        protected void GridViewBoarderFee_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewBoarderFee.EditIndex = e.NewEditIndex;
            BoarderfeeBindData();
        }

        protected void GridViewBoarderFee_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GridViewBoarderFee.EditIndex = -1;
            BoarderfeeBindData();
        }

        protected void GridViewBoarderFee_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ms_boarder_fees_edit", con);
            GridViewRow row = GridViewBoarderFee.Rows[e.RowIndex] as GridViewRow;
            TextBox txtAmount = row.FindControl("tbAmount") as TextBox;
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "acad_year", Value = GridViewBoarderFee.Rows[e.RowIndex].Cells[0].Text });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "term_name", Value = GridViewBoarderFee.Rows[e.RowIndex].Cells[1].Text });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "type_description", Value = GridViewBoarderFee.Rows[e.RowIndex].Cells[2].Text });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "amount", Value = txtAmount.Text });
            cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            con.Close();
            GridViewBoarderFee.EditIndex = -1;
            BoarderfeeBindData();
        }

        protected void GridViewBoarderFee_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_boarder_fees_delete", con);
                GridViewRow row = GridViewBoarderFee.Rows[e.RowIndex] as GridViewRow;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "board_fee_id", Value = GridViewBoarderFee.Rows[e.RowIndex].Cells[3].Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "board_type_id", Value = GridViewBoarderFee.Rows[e.RowIndex].Cells[4].Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "acad_y_term_id", Value = GridViewBoarderFee.Rows[e.RowIndex].Cells[5].Text });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
                BoarderfeeBindData();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot Delete,Boarder Fee having a Registration"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Unable to delete fee, as it has student registrations.')", true);
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
    }
}