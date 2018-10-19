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
    public partial class term_wizard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            if (!Page.IsPostBack)
            {
                DropDownTermYear();
                TermBindData();
                DropDownYear();
                feeBindData();
                DropDownBatchFeeFromYear();
                DropDownBatchFeeToYear();
                DropDownBatchFeeFromTerm();
                DropDownBatchFeeToTerm();
                AssessmentTotalBind();
                DropDownBRFromYear();
                DropDownBRTerm();
                DropDownClass();
                DropDownBRegYear();
                DropDownBRegForm();
                DropDownBRegTerm();
                DropDownBRegClass();
                DropDownForm();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Wizard1.ActiveStepIndex == 0)
            {
                Wizard1.HeaderText = "Set Up Term";
            }
            else if (Wizard1.ActiveStepIndex == 1)
            {
                Wizard1.HeaderText = "Set Up Fees";
            }
            else if (Wizard1.ActiveStepIndex == 2)
            {
                Wizard1.HeaderText = "Review Assessments Weights";
                //AssessmentTotalBind();
            }
            else if (Wizard1.ActiveStepIndex == 3)
            {
                Wizard1.HeaderText = "Batch Registration";
            }
            else if (Wizard1.ActiveStepIndex == 4)
            {
                Wizard1.HeaderText = "Term Active Decision";
            }

        }
        protected void Wizard1_CancelButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/settings.aspx", false);
        }

        protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            Response.Redirect("~/pages/settings.aspx", false);
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
        public void DropDownTermYear()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_acad_year_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlAcademicYear.DataSource = ddlValues;
            ddlAcademicYear.DataValueField = "acad_year";
            ddlAcademicYear.DataTextField = "acad_year";
            ddlAcademicYear.DataBind();

            //Adding "Please select" option in dropdownlist for validation
            ddlAcademicYear.Items.Insert(0, new ListItem("Please select a Year", ""));

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
        protected void BtnSaveTerm_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_settings_acad_year_term_add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@acad_year", ddlAcademicYear.Text.ToString());
                cmd.Parameters.AddWithValue("@term_name", ddlTermName.SelectedItem.Text.ToString());
                cmd.Parameters.AddWithValue("@ay_term_start_date", tbStartDate.Text.ToString());
                cmd.Parameters.AddWithValue("@ay_term_end_date", tbEndDate.Text.ToString());
                cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                cmd.ExecuteNonQuery();
                TermBindData();
                divSetupTermForm.Visible = false;
                AcademicYearTermActive();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Term Saved & Active Successfully');", true);
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

        protected void AcademicYearTermActive()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                if (GridViewTerm.Rows.Count != 0)
                {
                    string ID = GridViewTerm.Rows[0].Cells[0].Text;
                    using (SqlCommand cmd = new SqlCommand("sp_ms_settings_acad_year_term_set_active", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ay_term_id", SqlDbType.Int).Value = ID;
                        cmd.ExecuteNonQuery();
                        TermBindData();
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
        protected void TermBindData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_settings_acad_year_term_list_all", con);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        GridViewTerm.DataSource = dt;
                        GridViewTerm.DataBind();
                        GridViewTermActive.DataSource = dt;
                        GridViewTermActive.DataBind();
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

        protected void GridViewTerm_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewTerm.EditIndex = e.NewEditIndex;
            TermBindData();
        }

        protected void GridViewTerm_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GridViewTerm.EditIndex = -1;
            TermBindData();
        }

        protected void GridViewTerm_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_settings_acad_year_term_edit", con);
                GridViewRow row = GridViewTerm.Rows[e.RowIndex] as GridViewRow;

                DropDownList txtTerm = row.FindControl("ddlEditTermName") as DropDownList;
                TextBox txtStartDate = row.FindControl("tbTermStartDate") as TextBox;
                TextBox txtEndDate = row.FindControl("tbTermEndDate") as TextBox;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "ay_term_id", Value = GridViewTerm.Rows[e.RowIndex].Cells[0].Text });

                cmd.Parameters.Add(new SqlParameter() { ParameterName = "term_name", Value = txtTerm.SelectedValue });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "ay_term_start_date", Value = txtStartDate.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "ay_term_end_date", Value = txtEndDate.Text });
                cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
                GridViewTerm.EditIndex = -1;
                TermBindData();
                //TermActiveBindData();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "clentscript", "alert('Cannot update, Term already exist');", true);
            }
            finally
            {
                con.Close();
            }
        }

        protected void GridViewTerm_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_settings_acad_year_term_delete", con);
                GridViewRow row = GridViewTerm.Rows[e.RowIndex] as GridViewRow;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "ay_term_id", Value = GridViewTerm.Rows[e.RowIndex].Cells[0].Text });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
                TermBindData();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Cannot Delete, Term has either fee setup or Registrations or assessments');", true);
            }
            finally
            {
                con.Close();
            }
        }


        protected void btnStatus_Click(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((Button)sender).Parent.Parent)
            {
                string ID = row.Cells[0].Text;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_settings_acad_year_term_set_active", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ay_term_id", SqlDbType.Int).Value = ID;
                        cmd.ExecuteNonQuery();
                        TermBindData();
                    }

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                }
                finally
                {
                    con.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Term Active Sucessfully');", true);
                }
            }
        }

        protected void GridViewTerm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (GridViewTerm.Rows.Count != -1)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string Value = e.Row.Cells[5].Text;
                    //Button btntext = e.Row.FindControl("btnStatus") as Button;
                    switch (Value)
                    {
                        case "Active":
                            e.Row.BackColor = System.Drawing.Color.LightBlue;
                            //btntext.Text = "Activated";
                            break;
                    }
                }
            }
        }

        protected void btnSetupFeeManual_Click(object sender, EventArgs e)
        {
            divSetupFeeCopyPrevious.Visible = false;
            divSetupFeeManual.Visible = true;
            divBtnFeeSeutupOptions.Visible = false;
        }

        protected void btnSetupFeeCopyPrevious_Click(object sender, EventArgs e)
        {
            divSetupFeeCopyPrevious.Visible = true;
            divSetupFeeManual.Visible = false;
            divBtnFeeSeutupOptions.Visible = false;
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
                feeBindData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
                //ClearData(this);
            }
        }


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

        public void DropDownBatchFeeFromYear()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_acad_year_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlFromBatchFeeYear.DataSource = ddlValues;
            ddlFromBatchFeeYear.DataValueField = "acad_year";
            ddlFromBatchFeeYear.DataTextField = "acad_year";
            ddlFromBatchFeeYear.DataBind();

            //ddlAcademicYear.SelectedValue = yearFormat;
            ddlFromBatchFeeYear.Items.Insert(0, new ListItem("Please select year", ""));

            //Adding "Please select" option in dropdownlist for validation
            //ddlAcademicYear.Items.Insert(0, new ListItem("Please select a Year", "0"));

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void DropDownBatchFeeToYear()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_acad_year_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();
            ddlToBatchFeeYear.DataSource = ddlValues;
            ddlToBatchFeeYear.DataValueField = "acad_year";
            ddlToBatchFeeYear.DataTextField = "acad_year";
            ddlToBatchFeeYear.DataBind();
            ddlToBatchFeeYear.Items.Insert(0, new ListItem("Please select year", ""));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void DropDownBatchFeeFromTerm()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_term_dropdown_v2", con);
            cmd.Parameters.Add("@acad_year", SqlDbType.VarChar).Value = ddlFromBatchFeeYear.SelectedItem.ToString();
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                SqlDataReader ddlValues;
                ddlValues = cmd.ExecuteReader();
                string TermSelectedValue = null;
                while (ddlValues.Read())
                {
                    TermSelectedValue = ddlValues[0].ToString();
                    int DefaultValue = Convert.ToInt32(ddlValues[1]);
                    if (DefaultValue == 1)
                        break;
                }
                ddlValues.Close();
                ddlValues = cmd.ExecuteReader();
                ddlFromBatchFeeTerm.DataSource = ddlValues;
                ddlFromBatchFeeTerm.DataValueField = "term_name";
                ddlFromBatchFeeTerm.DataTextField = "term_name";
                ddlFromBatchFeeTerm.DataBind();
                ddlFromBatchFeeTerm.Items.Insert(0, new ListItem("Please select Term", ""));
                ddlFromBatchFeeTerm.SelectedValue = TermSelectedValue;

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

        public void DropDownBatchFeeToTerm()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_term_dropdown_v2", con);
            cmd.Parameters.Add("@acad_year", SqlDbType.VarChar).Value = ddlToBatchFeeYear.SelectedItem.ToString();
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                SqlDataReader ddlValues;
                ddlValues = cmd.ExecuteReader();
                string TermSelectedValue = null;
                while (ddlValues.Read())
                {
                    TermSelectedValue = ddlValues[0].ToString();
                    int DefaultValue = Convert.ToInt32(ddlValues[1]);
                    if (DefaultValue == 1)
                        break;
                }
                ddlValues.Close();
                ddlValues = cmd.ExecuteReader();
                ddlToBatchFeeTerm.DataSource = ddlValues;
                ddlToBatchFeeTerm.DataValueField = "term_name";
                ddlToBatchFeeTerm.DataTextField = "term_name";
                ddlToBatchFeeTerm.DataBind();
                ddlToBatchFeeTerm.Items.Insert(0, new ListItem("Please select Term", ""));
                ddlToBatchFeeTerm.SelectedValue = TermSelectedValue;

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

        protected void ddlFromBatchFeeYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownBatchFeeFromTerm();
            ddlFromBatchFeeTerm.SelectedIndex = 0;
        }

        protected void ddlToBatchFeeYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownBatchFeeToTerm();
            ddlToBatchFeeTerm.SelectedIndex = 0;
        }

        protected void btnBatchFeeSetup_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_settings_fees_term_transfer", con))
                {

                    cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                    cmd.Parameters.AddWithValue("@acad_year_old", ddlFromBatchFeeYear.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@term_name_old", ddlFromBatchFeeTerm.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@acad_year_new", ddlToBatchFeeYear.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@term_name_new", ddlToBatchFeeTerm.SelectedItem.ToString());
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Fee Set Up Successfully');", true);
                    BatchfeeBindData();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            }
            finally
            {
                con.Close();
                // ClearData(this);
            }

        }


        protected void BatchfeeBindData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                //sp_ms_settings_fees_search_year_term
                SqlCommand cmd = new SqlCommand("sp_ms_settings_fees_search_year_term", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@acad_year", ddlFromBatchFeeYear.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@term_name", ddlFromBatchFeeTerm.SelectedItem.ToString());

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        GridViewBatchFee.DataSource = dt;
                        if (dt.Rows.Count == 0)
                        {
                            // lblZeroRecords.Visible = true;
                            //lblZeroRecords.Text = "No Fee found ";
                            GridViewBatchFee.Visible = false;
                        }
                        else
                        {
                            GridViewBatchFee.DataBind();
                            GridViewBatchFee.Visible = true;
                            // lblZeroRecords.Visible = false;
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
        ///////////////////////////////////////////////////////////////////////////////////END OF SETUP FEE WIZARD//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        protected void AssessmentTotalBind()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_assessment_weight_list_all", con);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        GridViewWeighting.DataSource = dt;
                        if (dt.Rows.Count == 0)
                        {
                            divEditUpdate.Visible = false;
                            lblZeroAssessments.Visible = true;
                            lblZeroAssessments.Text = "No Registrations found for this term, Please register students to setup Assessment Weighting ";
                        }
                        else
                        {
                            lblZeroAssessments.Visible = false;
                            divEditUpdate.Visible = true;
                            GridViewWeighting.DataBind();
                            int? total = dt.AsEnumerable().Sum(r => r.Field<int?>("assessment_weight") ?? 0);
                            GridViewWeighting.FooterRow.Cells[0].Text = "Total";
                            GridViewWeighting.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                            GridViewWeighting.FooterRow.Cells[1].Text = total.Value.ToString("D");
                            Session["total"] = total.Value.ToString("D");
                        }

                        //((TextBox)row.FindControl("tbTotalMarks")).BackColor = Color.White;
                        //((TextBox)row.FindControl("tbTotalMarks")).ForeColor = Color.Black;
                        //Calculate Sum and display in Footer Row
                        // not working becuase of null       //int total = dt.AsEnumerable().Sum(row => row.Field<int>("assessment_weight"));

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
        protected void GridViewWeighting_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewWeighting.EditIndex = e.NewEditIndex;
            AssessmentTotalBind();
        }

        protected void GridViewWeighting_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GridViewWeighting.EditIndex = -1;
            AssessmentTotalBind();
        }

        protected void GridViewWeighting_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_assessment_weight_edit", con);
                GridViewRow row = GridViewWeighting.Rows[e.RowIndex] as GridViewRow;
                TextBox txtCategory = row.FindControl("tbTotalMarks") as TextBox;

                cmd.Parameters.Add(new SqlParameter() { ParameterName = "aw_id", Value = GridViewWeighting.Rows[e.RowIndex].Cells[0].Text });
                if (txtCategory.Text == "")
                {
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "assessment_weight", Value = DBNull.Value });
                }
                else
                { cmd.Parameters.Add(new SqlParameter() { ParameterName = "assessment_weight", Value = txtCategory.Text }); }
                cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.UpdatedRowSource.ToString();
                cmd.ExecuteNonQuery();
                GridViewWeighting.EditIndex = -1;
                AssessmentTotalBind();
                //int total = Convert.ToInt32(Session["total"]);
                //if (total < 100 || total > 100)
                //{
                //    cmd.Transaction.Rollback();
                //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", "alert('Cannot update, Total should be 100');", true);

                //}
                //else
                //{
                //    cmd.Transaction.Commit();
                //}
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

        protected void btnEdit_Click(object sender, EventArgs e)
        {

            btnCancel.Visible = true;
            btnEdit.Visible = false;
            btnUpdate.Visible = true;
            GridViewWeighting.Columns[3].Visible = false;
            foreach (GridViewRow row in GridViewWeighting.Rows)
            {
                ((TextBox)row.FindControl("tbTotalMarks")).Enabled = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            AssessmentTotalBind();
            btnCancel.Visible = false;
            btnEdit.Visible = true;
            btnUpdate.Visible = false;
            GridViewWeighting.Columns[3].Visible = true;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            //cmd.Transaction = cmd.Connection.BeginTransaction();
            try
            {
                foreach (GridViewRow row in GridViewWeighting.Rows)
                {
                    TextBox txtCategory = row.FindControl("tbTotalMarks") as TextBox;
                    using (SqlCommand cmd = new SqlCommand("sp_ms_assessment_weight_edit", con))
                    {
                        con.Open();
                        string AwId = row.Cells[0].Text;
                        cmd.Parameters.Add(new SqlParameter() { ParameterName = "aw_id", Value = AwId });
                        if (txtCategory.Text == "")
                        {
                            cmd.Parameters.Add(new SqlParameter() { ParameterName = "assessment_weight", Value = DBNull.Value });
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter() { ParameterName = "assessment_weight", Value = txtCategory.Text });
                        }
                        cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                        btnCancel.Visible = false;
                        btnEdit.Visible = true;
                        btnUpdate.Visible = false;
                        con.Close();
                        AssessmentTotalBind();
                        GridViewWeighting.Columns[3].Visible = true;

                    }
                }
                int total = Convert.ToInt32(Session["total"]);
                if (total < 100 || total > 100)
                {
                    //cmd.Transaction.Rollback();
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", "alert('Assessemts will not work, Total should be 100');", true);
                }
                else
                {
                    //cmd.Transaction.Commit();
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

        //////////////////////////////////////////////////////////////////////////////////////END OF ASSESSMENT WEIGHTING WIZARD /////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public void DropDownBRFromYear()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_acad_year_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlFromBRYear.DataSource = ddlValues;
            ddlFromBRYear.DataValueField = "acad_year";
            ddlFromBRYear.DataTextField = "acad_year";
            ddlFromBRYear.DataBind();

            //ddlFromBRYear.SelectedValue = yearFormat;
            ddlFromBRYear.Items.Insert(0, new ListItem("Please select year", ""));

            //Adding "Please select" option in dropdownlist for validation
            //ddlFromBRYear.Items.Insert(0, new ListItem("Please select a Year", "0"));

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void DropDownBRegYear()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_acad_year_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();
            ddlYearBReg.DataSource = ddlValues;
            ddlYearBReg.DataValueField = "acad_year";
            ddlYearBReg.DataTextField = "acad_year";
            ddlYearBReg.DataBind();
            ddlYearBReg.Items.Insert(0, new ListItem("Please select year", ""));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
        public void DropDownForm()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_form_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlForm.DataSource = ddlValues;
            ddlForm.DataValueField = "form_name";
            ddlForm.DataTextField = "form_name";
            ddlForm.DataBind();
            ddlForm.Items.Insert(0, new ListItem("Please select Class", ""));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void DropDownBRegForm()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_form_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();
            ddlFormBReg.DataSource = ddlValues;
            ddlFormBReg.DataValueField = "form_name";
            ddlFormBReg.DataTextField = "form_name";
            ddlFormBReg.DataBind();
            ddlFormBReg.Items.Insert(0, new ListItem("Please select Class", ""));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }




        public void DropDownBRTerm()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_term_dropdown_v2", con);
            cmd.Parameters.Add("@acad_year", SqlDbType.VarChar).Value = ddlFromBRYear.SelectedItem.ToString();
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                SqlDataReader ddlValues;
                ddlValues = cmd.ExecuteReader();
                string TermSelectedValue = null;
                while (ddlValues.Read())
                {
                    TermSelectedValue = ddlValues[0].ToString();
                    int DefaultValue = Convert.ToInt32(ddlValues[1]);
                    if (DefaultValue == 1)
                        break;
                }
                ddlValues.Close();
                ddlValues = cmd.ExecuteReader();
                ddlFromBRTerm.DataSource = ddlValues;
                ddlFromBRTerm.DataValueField = "term_name";
                ddlFromBRTerm.DataTextField = "term_name";
                ddlFromBRTerm.DataBind();
                ddlFromBRTerm.Items.Insert(0, new ListItem("Please select Term", ""));
                ddlFromBRTerm.SelectedValue = TermSelectedValue;

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

        public void DropDownBRegTerm()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_term_dropdown_v2", con);
            cmd.Parameters.Add("@acad_year", SqlDbType.VarChar).Value = ddlYearBReg.SelectedItem.ToString();
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                SqlDataReader ddlValues;
                ddlValues = cmd.ExecuteReader();
                string TermSelectedValue = null;
                while (ddlValues.Read())
                {
                    TermSelectedValue = ddlValues[0].ToString();
                    int DefaultValue = Convert.ToInt32(ddlValues[1]);
                    if (DefaultValue == 1)
                        break;
                }
                ddlValues.Close();
                ddlValues = cmd.ExecuteReader();
                ddlTermBReg.DataSource = ddlValues;
                ddlTermBReg.DataValueField = "term_name";
                ddlTermBReg.DataTextField = "term_name";
                ddlTermBReg.DataBind();
                ddlTermBReg.Items.Insert(0, new ListItem("Please select Term", ""));
                ddlTermBReg.SelectedValue = TermSelectedValue;

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

        public void DropDownClass()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_class_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlClass.DataSource = ddlValues;
            ddlClass.DataValueField = "class_name";
            ddlClass.DataTextField = "class_name";
            ddlClass.DataBind();
            //Adding "Please select" option in dropdownlist for validation
            ddlClass.Items.Insert(0, new ListItem("Please select Arm", ""));

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void DropDownBRegClass()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_class_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlClassBReg.DataSource = ddlValues;
            ddlClassBReg.DataValueField = "class_name";
            ddlClassBReg.DataTextField = "class_name";
            ddlClassBReg.DataBind();
            //Adding "Please select" option in dropdownlist for validation
            ddlClassBReg.Items.Insert(0, new ListItem("Please select Arm", ""));

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        private void BindGrid()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_registrations_list_all_year_term_status_form_class", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@acad_year", ddlFromBRYear.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@term_name", ddlFromBRTerm.SelectedItem.ToString());
                    // cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@form", ddlForm.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@class", ddlClass.SelectedItem.ToString());
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;

                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            GridViewBatchRegistrations.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                divProcessNow.Visible = false;
                                GridViewBatchRegistrations.Visible = false;
                                lblZeroBatchRegistrations.Visible = true;
                                lblZeroBatchRegistrations.Text = "No Records found ";
                            }
                            else
                            {
                                divProcessNow.Visible = true;
                                GridViewBatchRegistrations.Visible = true;
                                GridViewBatchRegistrations.DataBind();
                                lblZeroBatchRegistrations.Visible = false;
                            }
                        }
                    }
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

        protected void GridViewBatchRegistrations_RowDataBound(object sender, GridViewRowEventArgs e)
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
        }


        protected void GridViewBatchRegistrations_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewBatchRegistrations.PageIndex = e.NewPageIndex;
            //BindGrid("All");
            BindGrid();

        }

        protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)GridViewBatchRegistrations.HeaderRow.FindControl("chkboxSelectAll");
            foreach (GridViewRow row in GridViewBatchRegistrations.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkStudent");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
        }

        protected void btnBatchRegistrations_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                foreach (GridViewRow row in GridViewBatchRegistrations.Rows)
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_batch_registration_add", con))
                    {
                        CheckBox ChkBoxHeader = (CheckBox)GridViewBatchRegistrations.HeaderRow.FindControl("chkboxSelectAll1");
                        //CheckBox ChkBoxHeader = (CheckBox)GridViewProcessApplications..FindControl("chkStudent"); 
                        string PerId = row.Cells[1].Text;
                        CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkStudent");
                        if (ChkBoxRows.Checked)
                        {
                            cmd.Parameters.Add("@person_id", SqlDbType.NVarChar, 40).Value = PerId;
                            cmd.Parameters.Add("@app_id", SqlDbType.NVarChar, 40).Value = DBNull.Value;
                            //cmd.Parameters.Add("@reg_date", SqlDbType.NVarChar, 40).Value = DateTime.Now.ToString();
                            // cmd.Parameters.AddWithValue("@status", ddlStatusBReg.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                            cmd.Parameters.AddWithValue("@acad_year", ddlYearBReg.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@term_name", ddlTermBReg.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@class_name", ddlClassBReg.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@form_name", ddlFormBReg.SelectedItem.ToString());
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            int rowaffected = cmd.ExecuteNonQuery(); //this was missing.
                            con.Close();
                        }
                    }
                    using (SqlCommand cmd = new SqlCommand("sp_ms_batch_registration_update_status_completed", con))
                    {
                        CheckBox ChkBoxHeader = (CheckBox)GridViewBatchRegistrations.HeaderRow.FindControl("chkboxSelectAll1");
                        //CheckBox ChkBoxHeader = (CheckBox)GridViewProcessApplications..FindControl("chkStudent"); 
                        string RegId = row.Cells[2].Text;
                        CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkStudent");
                        if (ChkBoxRows.Checked)
                        {
                            cmd.Parameters.Add("@reg_id", SqlDbType.NVarChar, 40).Value = RegId;
                            cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            int rowaffected = cmd.ExecuteNonQuery(); //this was missing.
                            con.Close();
                        }
                    }
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Check Term,Class,ClassName either doesnot exists for Academic Year in settings or Cannot register for already registered students');", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
                // ClearData(this);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Student(s) Registered Successfully');", true);
            }

            GridViewBatchRegistrations.Visible = false;
        }

        protected void ddlFromBRYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownBRTerm();
            ddlFromBRTerm.SelectedIndex = 1;
            BindGrid();

        }

        protected void ddlYearBReg_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownBRegTerm();
            ddlTermBReg.SelectedIndex = 0;
        }

        protected void ddlFromBRTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ddlForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {

        }

        protected void btnNext_Click(object sender, EventArgs e)
        {

        }
    }
}