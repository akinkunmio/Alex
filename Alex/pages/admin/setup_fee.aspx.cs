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
    public partial class setup_fee : System.Web.UI.Page
    {
        //string selectedValue = string.Empty;
        //private static string currentYear = DateTime.Now.Year.ToString();
        //private static string prevYear = (Convert.ToInt32(currentYear) - 1).ToString();
        //private static string yearFormat = prevYear + "/" + currentYear.Substring(2);
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            if (!Page.IsPostBack)
            {
                //DropDownFeeYear();
                //DropDownTerm();
                DropDownYear();

                feeBindData();
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
                ddlFeeSetupYear.DataSource = cmd.ExecuteReader();
                ddlFeeSetupYear.DataTextField = "acad_year";
                ddlFeeSetupYear.DataValueField = "acad_year_id";
                ddlFeeSetupYear.DataBind();
                ddlFeeSetupYear.Items.Insert(0, new ListItem("Please select Year", ""));
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


        //public void DropDownFeeYear()
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        //    SqlCommand cmd = new SqlCommand("sp_ms_rep_active_acad_year_dropdown", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    con.Open();
        //    SqlDataReader ddlValues;
        //    ddlValues = cmd.ExecuteReader();
        //    string YearSelectedValue = null;

        //    while (ddlValues.Read())
        //    {
        //        YearSelectedValue = ddlValues[1].ToString();
        //        int DefaultValue = Convert.ToInt32(ddlValues[2]);
        //        if (DefaultValue == 1)
        //            break;
        //    }
        //    ddlValues.Close();
        //    ddlValues = cmd.ExecuteReader();

        //    ddlAcademicYear.DataSource = ddlValues;
        //    ddlAcademicYear.DataValueField = "acad_year";
        //    ddlAcademicYear.DataTextField = "acad_year";
        //    ddlAcademicYear.DataBind();
        //    ddlAcademicYear.Items.Insert(0, new ListItem("Select Academic Year", ""));
        //    ddlAcademicYear.SelectedValue = YearSelectedValue;
        //    cmd.Connection.Close();
        //    cmd.Connection.Dispose();
        //}


        //public void DropDownTerm()
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        //    SqlCommand cmd = new SqlCommand("sp_ms_rep_term_dropdown_v2", con);
        //    cmd.Parameters.Add("@acad_year", SqlDbType.VarChar).Value = ddlAcademicYear.SelectedItem.ToString();
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    con.Open();
        //    SqlDataReader ddlValues;
        //    ddlValues = cmd.ExecuteReader();
        //    string TermSelectedValue = null;
        //    while (ddlValues.Read())
        //    {
        //        TermSelectedValue = ddlValues[0].ToString();
        //        int DefaultValue = Convert.ToInt32(ddlValues[1]);
        //        if (DefaultValue == 1)
        //            break;
        //    }
        //    ddlValues.Close();
        //    ddlValues = cmd.ExecuteReader();
        //    ddlTerm.DataSource = ddlValues;
        //    ddlTerm.DataValueField = "term_name";
        //    ddlTerm.DataTextField = "term_name";
        //    ddlTerm.DataBind();
        //    ddlTerm.Items.Insert(0, new ListItem("Please select Term", ""));
        //    ddlTerm.SelectedValue = TermSelectedValue;

        //    cmd.Connection.Close();
        //    cmd.Connection.Dispose();
        //}

       
        protected void BtnSaveFeeSetup_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_settings_fees_add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@acad_year", ddlFeeSetupYear.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@term_name", ddlFeeSetupTerm.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@form_name", ddlFeeSetupForm.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@amount", tbFeeSetupAmount.Text.ToString());
                cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                cmd.ExecuteNonQuery();
                //feeBindData();
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Fee Saved Successfully');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Fee Saved Successfully');window.location = '../../pages/admin/setup_fee.aspx';", true);
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

        //protected void btnSearchAcadmicYear_Click(object sender, EventArgs e)
        //{
        //    feeBindData();
        //}

        protected void feeBindData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                //sp_ms_settings_fees_search_year_term
                SqlCommand cmd = new SqlCommand("sp_ms_settings_fees_active_year_term", con);
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
                        GridViewFee.DataSource = dt;
                        if (dt.Rows.Count == 0)
                        {
                            lblZeroRecords.Visible = true;
                            lblZeroRecords.Text = "No Fee found ";
                            GridViewFee.Visible = false;
                        }
                        else
                        {
                            GridViewFee.DataBind();
                            GridViewFee.Visible = true;
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
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('SUCESSFULLY RECORD SAVED');", true);
            }
        }

        protected void GridViewFee_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewFee.EditIndex = e.NewEditIndex;
            feeBindData();
        }

        protected void GridViewFee_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GridViewFee.EditIndex = -1;
            feeBindData();
        }

        protected void GridViewFee_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ms_settings_fees_edit", con);
            GridViewRow row = GridViewFee.Rows[e.RowIndex] as GridViewRow;
            TextBox txtAmount = row.FindControl("tbAmount") as TextBox;
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "acad_year", Value = GridViewFee.Rows[e.RowIndex].Cells[0].Text });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "term_name", Value = GridViewFee.Rows[e.RowIndex].Cells[1].Text });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "form_name", Value = GridViewFee.Rows[e.RowIndex].Cells[2].Text });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "amount", Value = txtAmount.Text });
            cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            con.Close();
            GridViewFee.EditIndex = -1;
            feeBindData();
        }

      

        protected void GridViewFee_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
            SqlCommand cmd = new SqlCommand("sp_ms_settings_fees_delete", con);
            GridViewRow row = GridViewFee.Rows[e.RowIndex] as GridViewRow;
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "fee_id", Value = GridViewFee.Rows[e.RowIndex].Cells[3].Text });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "form_id", Value = GridViewFee.Rows[e.RowIndex].Cells[4].Text });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "acad_y_term_id", Value = GridViewFee.Rows[e.RowIndex].Cells[5].Text });
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            con.Close();
            feeBindData();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot Delete,Fee having a Registration"))
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

        protected void ddlFeeSetupYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlFeeSetupTerm.Items.Clear();
            ddlFeeSetupTerm.Items.Add(new ListItem("Please Select Term", ""));
            //ddlFeeSetupForm.Items.Clear();
            //ddlFeeSetupForm.Items.Add(new ListItem("Select Class", ""));

            ddlFeeSetupTerm.AppendDataBoundItems = true;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            String strQuery = "select ay_term_id, term_name from ms_acad_year_term " +
                      "where acad_year_id=@acad_year_id";

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@acad_year_id",
                 ddlFeeSetupYear.SelectedItem.Value);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strQuery;
            cmd.Connection = con;
            try
            {
                con.Open();
                ddlFeeSetupTerm.DataSource = cmd.ExecuteReader();
                ddlFeeSetupTerm.DataTextField = "term_name";
                ddlFeeSetupTerm.DataValueField = "ay_term_id";
                ddlFeeSetupTerm.DataBind();
              
                if (ddlFeeSetupTerm.Items.Count > 1)
                {
                    ddlFeeSetupTerm.Enabled = true;
                    ddlFeeSetupForm.Enabled = true;
                }
                else
                {
                    ddlFeeSetupTerm.Enabled = false;
                    ddlFeeSetupForm.Enabled = false;
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

        protected void ddlFeeSetupTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlFeeSetupForm.Items.Clear();
            ddlFeeSetupForm.Items.Add(new ListItem("Select Class", ""));
            ddlFeeSetupForm.AppendDataBoundItems = true;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ms_fee_setup_classes_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@acad_year", ddlFeeSetupYear.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@term_name", ddlFeeSetupTerm.SelectedItem.ToString());
          try
            {
               
                ddlFeeSetupForm.DataSource = cmd.ExecuteReader();
                ddlFeeSetupForm.DataTextField = "form_name";
                ddlFeeSetupForm.DataValueField = "form_name";
                ddlFeeSetupForm.DataBind();
                if (ddlFeeSetupForm.Items.Count > 1)
                {
                    ddlFeeSetupForm.Enabled = true;
                    tbFeeSetupAmount.Enabled = true;
                    BtnSaveFeSetup.Enabled = true;
                }
                else
                {
                    ddlFeeSetupForm.Items.Remove(new ListItem("Select Class", ""));
                    ddlFeeSetupForm.Items.Add(new ListItem("Fee has been setup for all the class", ""));
                    ddlFeeSetupForm.Enabled = false;
                    tbFeeSetupAmount.Enabled = false;
                    BtnSaveFeSetup.Enabled = false;
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

        protected void btnFeeBreakDown_Click(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((Button)sender).Parent.Parent)
            {
              string FeeID = GridViewFee.Rows[row.RowIndex].Cells[3].Text;
              string FormClass = GridViewFee.Rows[row.RowIndex].Cells[2].Text;
              Label Fee_Amount = (Label)row.FindControl("lblAmount");
              string FeeAmount = Fee_Amount.Text.ToString();
              Server.Transfer("fee_breakdown.aspx?Fee_ID=" + FeeID + "&FAm=" + FeeAmount + "&FC=" + FormClass);
            }
        }

      
        
        
        //protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //ddlTerm.Items.Clear();
        //    //ddlTerm.Items.Add(new ListItem("Please Select Term", ""));


        //    //ddlTerm.AppendDataBoundItems = true;
        //    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        //    //String strQuery = "select ay_term_id, term_name from ms_acad_year_term " +
        //    //          "where acad_year_id=@acad_year_id";

        //    //SqlCommand cmd = new SqlCommand();
        //    //cmd.Parameters.AddWithValue("@acad_year_id",
        //    //     ddlAcademicYear.SelectedItem.Value);
        //    //cmd.CommandType = CommandType.Text;
        //    //cmd.CommandText = strQuery;
        //    //cmd.Connection = con;
        //    //try
        //    //{
        //    //    con.Open();
        //    //    ddlTerm.DataSource = cmd.ExecuteReader();
        //    //    ddlTerm.DataTextField = "term_name";
        //    //    ddlTerm.DataValueField = "ay_term_id";
        //    //    ddlTerm.DataBind();

        //    //    if (ddlTerm.Items.Count > 1)
        //    //    {
        //    //        ddlTerm.Enabled = true;
                    
        //    //    }
        //    //    else
        //    //    {
        //    //        ddlTerm.Enabled = false;
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //    //}
        //    //finally
        //    //{
        //    //    con.Close();
        //    //    con.Dispose();
        //    //}
        //    DropDownTerm();
        //    ddlTerm.SelectedIndex = 1;
        //    feeBindData();
        //}

       

    }
}