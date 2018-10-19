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
    public partial class wizard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            if (DetailsViewSchoolDetails.CurrentMode == DetailsViewMode.Edit)
            {
                var UserName = HttpContext.Current.User.Identity.Name;
                SqlDataSourceSchoolDetails.UpdateParameters["updated_by"].DefaultValue = UserName;
            }
            if (DetailsViewSchoolDetails.CurrentMode == DetailsViewMode.Insert)
            {
                var UserName = HttpContext.Current.User.Identity.Name;
                SqlDataSourceSchoolDetails.InsertParameters["created_by"].DefaultValue = UserName;
            }
            if (!Page.IsPostBack)
            {
                FormBindData();
                DropDownSection();
                DropDownForm();
                ClassBindData();
                DropDownFeeSetupForm();
                DropDownTerm();
                feeBindData();
                DropDownYear1();
                feeBindData();
                SubjectsBindData();
                AcademicYearBindData();
                TermDropDownYear();
                TermBindData();
            }
        }

        protected void DetailsViewSchoolDetails_ItemCreated(object sender, EventArgs e)
        {
            if (DetailsViewSchoolDetails.CurrentMode == DetailsViewMode.ReadOnly)
            {
                int commandRowIndex = DetailsViewSchoolDetails.Rows.Count - 1;
                if (commandRowIndex > 0)
                {
                    DetailsViewRow commandRow = DetailsViewSchoolDetails.Rows[commandRowIndex];
                    DataControlFieldCell cell = (DataControlFieldCell)commandRow.Controls[0];
                    if (cell != null)
                    {
                        foreach (Control ctrl in cell.Controls)
                        {
                            if (ctrl is LinkButton)
                            {
                                if (((LinkButton)ctrl).CommandName == "New")
                                {
                                    ctrl.Visible = false;
                                }
                            }
                        }
                    }
                }
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            //FormBindData();
            DropDownForm();
            //ClassBindData();
            DropDownFeeSetupForm();
            DropDownTerm();
            //feeBindData();
            DropDownYear1();
            //feeBindData();
            //SubjectsBindData();
            //AcademicYearBindData();
            TermDropDownYear();
            //TermBindData();
            if (Wizard1.ActiveStepIndex == 0)
            {
                Wizard1.HeaderText = "Setup School Details";
            }
            else if (Wizard1.ActiveStepIndex == 1)
            {
                Wizard1.HeaderText = "Setup Academic Year";
            }
            else if (Wizard1.ActiveStepIndex == 2)
            {
                Wizard1.HeaderText = "Setup Term";
            }
            else if (Wizard1.ActiveStepIndex == 3)
            {
                Wizard1.HeaderText = "Setup Class";
            }
            else if (Wizard1.ActiveStepIndex == 4)
            {
                Wizard1.HeaderText = "Setup Class Name";
            }
            else if (Wizard1.ActiveStepIndex == 5)
            {
                Wizard1.HeaderText = "Setup Fee";
            }
            else if (Wizard1.ActiveStepIndex == 6)
            {
                Wizard1.HeaderText = "Setup Subject";
            }
            else if (Wizard1.ActiveStepIndex == 7)
            {
                Wizard1.HeaderText = "Payment Card Details";
            }
        }

        protected void Wizard1_CancelButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/dashboard.aspx", false);
        }

        protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_wizard_complete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Wizard complete successful');window.location = '/pages/settings.aspx';", true);
                // Response.Redirect("~/pages/settings.aspx", false);
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            }

            finally
            {
                con.Close();
                SaveCardDetailsForm();
            }

        }

        protected void BtnSaveAcademicYear_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_settings_acad_year_add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                cmd.Parameters.AddWithValue("@acad_year", tbAcademicYear.Text.ToString());
                cmd.Parameters.AddWithValue("@acad_y_start_date", tbAcademicStartDate.Text.ToString());
                cmd.Parameters.AddWithValue("@acad_y_end_date", tbAcademicEndDate.Text.ToString());
                cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                cmd.ExecuteNonQuery();
                AcademicYearBindData();
                divAcademicYear.Visible = false;

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Record saved successfully ');", true);
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
        protected void AcademicYearBindData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_settings_acad_year_list_all", con);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        GridViewAcademicYear.DataSource = dt;
                        if (dt.Rows.Count == 0)
                        {
                            GridViewAcademicYear.Visible = false;
                            lblZeroRecords.Visible = true;
                            divAcademicYear.Visible = true;
                            lblZeroRecords.Text = "No Academic Year Record found ";
                        }
                        else
                        {
                            GridViewAcademicYear.Visible = true;
                            GridViewAcademicYear.DataBind();
                            lblZeroRecords.Visible = false;
                            divAcademicYear.Visible = false;
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

        protected void GridViewAcademicYear_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewAcademicYear.EditIndex = e.NewEditIndex;
            AcademicYearBindData();
        }

        protected void GridViewAcademicYear_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GridViewAcademicYear.EditIndex = -1;
            AcademicYearBindData();
        }

        protected void GridViewAcademicYear_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_settings_acad_year_edit", con);
                GridViewRow row = GridViewAcademicYear.Rows[e.RowIndex] as GridViewRow;
                TextBox txtAcademicYear = row.FindControl("tbEditAcademicYear") as TextBox;
                TextBox txtAcademicYearSD = row.FindControl("tbAcademicYearSD") as TextBox;
                TextBox txtAcademicYearED = row.FindControl("tbAcademicYearED") as TextBox;
                //DropDownList txtStatus = row.FindControl("ddlStatus") as DropDownList;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "acad_year_id", Value = GridViewAcademicYear.Rows[e.RowIndex].Cells[0].Text });
                cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "acad_year", Value = txtAcademicYear.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "acad_y_start_date", Value = txtAcademicYearSD.Text.ToString() });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "acad_y_end_date", Value = txtAcademicYearED.Text.ToString() });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "status", Value = GridViewAcademicYear.Rows[e.RowIndex].Cells[4].Text });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                //con.Close();
                GridViewAcademicYear.EditIndex = -1;
                AcademicYearBindData();

            }
            catch
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "clentscript", "alert('Cannot update, Academic Year already exist');", true);
            }
            finally
            {
                con.Close();
                TermBindData();
            }
        }

        protected void GridViewAcademicYear_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);


            GridViewRow row = GridViewAcademicYear.Rows[e.RowIndex] as GridViewRow;
            string Rownumber = GridViewAcademicYear.Rows[e.RowIndex].Cells[0].Text;
            if (Rownumber != null)
            {
                using (SqlCommand cmdd = new SqlCommand("select * from dbo.ms_acad_year_term  WHERE acad_year_id = " + Rownumber, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmdd.ExecuteReader())
                    {
                        // Check is the reader has any rows at all before starting to read.
                        if (reader.HasRows)
                        {
                            Rownumber = "false";
                        }
                    }
                    con.Close();
                }

            }
            if (Rownumber != "false")
            {
                SqlCommand cmd = new SqlCommand("sp_ms_settings_acad_year_delete", con);
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "acad_year_id", Value = GridViewAcademicYear.Rows[e.RowIndex].Cells[0].Text });
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
                AcademicYearBindData();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot Delete Academic Year, it has Term(s) Registered up')", true);
            }

        }


        public void TermDropDownYear()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_acad_year_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlTermAcademicYear.DataSource = ddlValues;
            ddlTermAcademicYear.DataValueField = "acad_year";
            ddlTermAcademicYear.DataTextField = "acad_year";
            ddlTermAcademicYear.DataBind();

            //Adding "Please select" option in dropdownlist for validation
            ddlTermAcademicYear.Items.Insert(0, new ListItem("Please select a Year", ""));

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
                cmd.Parameters.AddWithValue("@acad_year", ddlTermAcademicYear.Text.ToString());
                cmd.Parameters.AddWithValue("@term_name", ddlTermName.SelectedItem.Text.ToString());
                cmd.Parameters.AddWithValue("@ay_term_start_date", tbStartDate.Text.ToString());
                cmd.Parameters.AddWithValue("@ay_term_end_date", tbEndDate.Text.ToString());
                cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                cmd.ExecuteNonQuery();
                TermBindData();
                AcademicYearTermActive();
                divTerms.Visible = false;

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Record Saved Successfully');", true);
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
                        if (dt.Rows.Count == 0)
                        {
                            GridViewTerm.Visible = false;
                            lblZeroTerm.Visible = true;
                            divTerms.Visible = true;
                            lblZeroTerm.Text = "No Term Record found ";
                        }
                        else
                        {
                            GridViewTerm.Visible = true;
                            GridViewTerm.DataBind();
                            lblZeroTerm.Visible = false;
                            divTerms.Visible = false;
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
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "clentscript", "alert('Cannot Delete, Term has fee setup');", true);
            }
            finally
            {
                con.Close();
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
        protected void BtnSaveSetupForm_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_settings_forms_add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@form_name", tbSetupForm.Text.ToString());
                cmd.Parameters.AddWithValue("@section", ddlSection.SelectedItem.Text.ToString());
                cmd.Parameters.AddWithValue("@form_display_order", ddlSection.SelectedValue);
                cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                cmd.ExecuteNonQuery();
                FormBindData();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Record Saved Successfully');", true);

            }
            catch
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

        protected void FormBindData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_settings_forms_list_all", con);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        GridViewForm.DataSource = dt;
                        GridViewForm.DataBind();
                        GridViewForm.Visible = true;
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

        protected void GridViewForm_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewForm.EditIndex = e.NewEditIndex;
            FormBindData();
        }

        protected void GridViewForm_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GridViewForm.EditIndex = -1;
            FormBindData();
        }

        protected void GridViewForm_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_settings_forms_edit", con);
                GridViewRow row = GridViewForm.Rows[e.RowIndex] as GridViewRow;
                TextBox txtValue = row.FindControl("txtValue") as TextBox;
                DropDownList ddlEditSection = row.FindControl("ddlEditSection") as DropDownList;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "form_id", Value = GridViewForm.Rows[e.RowIndex].Cells[0].Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "form_name", Value = txtValue.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "section", Value = ddlEditSection.SelectedItem.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "form_display_order", Value = ddlEditSection.SelectedValue });
                cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
                GridViewForm.EditIndex = -1;
                FormBindData();
            }
            catch
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "clentscript", "alert('Cannot update, Class already exist');", true);
            }
            finally
            {
                con.Close();
            }
        }


        protected void GridViewForm_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_settings_forms_delete", con);
                GridViewRow row = GridViewForm.Rows[e.RowIndex] as GridViewRow;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "form_id", Value = GridViewForm.Rows[e.RowIndex].Cells[0].Text });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
                FormBindData();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "clentscript", "alert('Cannot Delete, Class was used in Class Name ');", true);
            }
            finally
            {
                con.Close();
            }
        }

        public void DropDownForm()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_form_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlFormName.DataSource = ddlValues;
            ddlFormName.DataValueField = "form_name";
            ddlFormName.DataTextField = "form_name";
            ddlFormName.DataBind();
            ddlFormName.Items.Insert(0, new ListItem("Please select Class", ""));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void DropDownSection()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_section_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlSection.DataSource = ddlValues;
            ddlSection.DataValueField = "form_display_order";
            ddlSection.DataTextField = "section2";
            ddlSection.DataBind();

            //Adding "Please select" option in dropdownlist for validation
            ddlSection.Items.Insert(0, new ListItem("Please select a Section", ""));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        protected void ClassBindData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_settings_classes_list_all", con);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        GridViewClass.DataSource = dt;
                        GridViewClass.DataBind();
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

        protected void GridViewClass_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewClass.EditIndex = e.NewEditIndex;
            ClassBindData();
        }

        protected void GridViewClass_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GridViewClass.EditIndex = -1;
            ClassBindData();

        }

        protected void GridViewClass_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_settings_classes_edit", con);
                GridViewRow row = GridViewClass.Rows[e.RowIndex] as GridViewRow;
                //TextBox txtForm = row.FindControl("tbForm") as TextBox;
                TextBox txtClass = row.FindControl("tbClass") as TextBox;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "class_id", Value = GridViewClass.Rows[e.RowIndex].Cells[0].Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "form_name", Value = GridViewClass.Rows[e.RowIndex].Cells[1].Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "class_name", Value = txtClass.Text });
                cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
                GridViewClass.EditIndex = -1;
                ClassBindData();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "clentscript", "alert('Cannot update, Class Name already exist');", true);
            }
            finally
            {
                con.Close();
            }
        }

        protected void btnSaveClass_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_settings_classes_add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@class_name", tbSetupClass.Text.ToString());
                cmd.Parameters.AddWithValue("@form_name", ddlFormName.Text.ToString());
                cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                cmd.ExecuteNonQuery();
                ClassBindData();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Record Saved Successfully');", true);

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

        protected void GridViewClass_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ms_settings_classes_delete", con);
            GridViewRow row = GridViewClass.Rows[e.RowIndex] as GridViewRow;
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "class_id", Value = GridViewClass.Rows[e.RowIndex].Cells[0].Text });
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            con.Close();
            ClassBindData();
        }


        public void DropDownYear1()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_acad_year_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlFeeSetupYear.DataSource = ddlValues;
            ddlFeeSetupYear.DataValueField = "acad_year";
            ddlFeeSetupYear.DataTextField = "acad_year";
            ddlFeeSetupYear.DataBind();

            //ddlFeeSetupYear.SelectedValue = yearFormat;

            //Adding "Please select" option in dropdownlist for validation
            //ddlFeeSetupYear.Items.Insert(0, new ListItem("Please select a Year", ""));


            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
        public void DropDownFeeSetupForm()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_form_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlFeeSetupForm.DataSource = ddlValues;
            ddlFeeSetupForm.DataValueField = "form_name";
            ddlFeeSetupForm.DataTextField = "form_name";
            ddlFeeSetupForm.DataBind();

            //ddlFeeSetupYear.SelectedValue = yearFormat;

            //Adding "Please select" option in dropdownlist for validation
            ddlFeeSetupForm.Items.Insert(0, new ListItem("Please select Class", ""));

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }



        public void DropDownTerm()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_term_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlFeeSetupTerm.DataSource = ddlValues;
            ddlFeeSetupTerm.DataValueField = "term_name";
            ddlFeeSetupTerm.DataTextField = "term_name";
            ddlFeeSetupTerm.DataBind();

            cmd.Connection.Close();
            cmd.Connection.Dispose();
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
                cmd.Parameters.AddWithValue("@form_name", ddlFeeSetupForm.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@term_name", ddlFeeSetupTerm.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@amount", tbFeeSetupAmount.Text.ToString());
                cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                cmd.ExecuteNonQuery();
                feeBindData();
            }
            catch
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
        protected void feeBindData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_settings_fees_active_year_term", con);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        GridViewFee.DataSource = dt;
                        GridViewFee.DataBind();
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
            cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "amount", Value = txtAmount.Text });
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            con.Close();
            GridViewFee.EditIndex = -1;
            feeBindData();
        }



        protected void GridViewFee_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ms_settings_fees_delete", con);
            GridViewRow row = GridViewFee.Rows[e.RowIndex] as GridViewRow;
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "fee_id", Value = GridViewFee.Rows[e.RowIndex].Cells[3].Text });
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            con.Close();
            feeBindData();
        }


        protected void BtnSaveSetupSubject_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_settings_subject_add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@subject_name", tbSetupSubject.Text.ToString());
                cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                cmd.ExecuteNonQuery();
                SubjectsBindData();

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
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Record Saved Successfully');", true);
            }
        }


        protected void SubjectsBindData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_settings_subject_list_all", con);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        GridViewSubjects.DataSource = dt;
                        GridViewSubjects.DataBind();
                        GridViewSubjects.Visible = true;
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

        protected void GridViewSubjects_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewSubjects.EditIndex = e.NewEditIndex;
            SubjectsBindData();
        }

        protected void GridViewSubjects_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GridViewSubjects.EditIndex = -1;
            SubjectsBindData();
        }

        protected void GridViewSubjects_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ms_settings_subject_edit", con);
            GridViewRow row = GridViewSubjects.Rows[e.RowIndex] as GridViewRow;
            TextBox txtValue = row.FindControl("tbSubjectName") as TextBox;
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "subject_id", Value = GridViewSubjects.Rows[e.RowIndex].Cells[0].Text });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "subject_name", Value = txtValue.Text });
            cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            con.Close();
            GridViewSubjects.EditIndex = -1;
            SubjectsBindData();
        }


        protected void GridViewSubjects_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ms_settings_subject_delete", con);
            GridViewRow row = GridViewSubjects.Rows[e.RowIndex] as GridViewRow;
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "subject_id", Value = GridViewSubjects.Rows[e.RowIndex].Cells[0].Text });
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            con.Close();
            SubjectsBindData();
        }



        protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            //if (DetailsViewSchoolDetails.Rows.Count == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please Setup School Details');", true);
            //}
            //else if (GridViewAcademicYear.Rows.Count == 0)
            //{
            //   ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please Add Academic Year');", true);
            //}
            //else if (GridViewTerm.Rows.Count == 0)
            //{
            //    if (Wizard1.ActiveStepIndex == 2)
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please Setup Term');", true);
            //        //Response.Redirect("~/pages/wizard.aspx".StartsWith(Wizard1.ActiveStepIndex=2))
            //    } 
            //}
            //else if (GridViewForm.Rows.Count == 0)
            //{

            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please Setup Class ');", true);
            //}









            //if (DetailsViewSchoolDetails.Rows.Count == 0)
            //{
            //    Wizard1.ActiveStepIndex = 0;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please Setup School Details');", true);
            //}
            //else if (GridViewAcademicYear.Rows.Count == 0)
            //{
            //    Wizard1.ActiveStepIndex = 1;
            //    //Button nextButton = (Button)Wizard1.FindControl("StepNavigationTemplateContainerID").FindControl("btnNext");
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please Add Academic Year');", true);
            //}
            //else if (GridViewTerm.Rows.Count == 0)
            //{
            //    Wizard1.ActiveStepIndex = 2;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please Setup Term ');", true);
            //}
            //else if (GridViewForm.Rows.Count == 0)
            //{
            //    Wizard1.ActiveStepIndex = 3;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please Setup Class ');", true);
            //}
            //else if (GridViewClass.Rows.Count == 0)
            //{
            //    Wizard1.ActiveStepIndex = 4;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please Setup Class Name ');", true);
            //}
            //else if (GridViewFee.Rows.Count == 0)
            //{
            //    Wizard1.ActiveStepIndex = 5;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please Setup Fee ');", true);
            //}
            //else if (GridViewSubjects.Rows.Count == 0)
            //{
            //    Wizard1.ActiveStepIndex = 6;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please Setup Subjects');", true);
            //}
        }


        protected void SaveCardDetailsForm()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_client_information_add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@first_name", tbBillFName.Text.ToString());
                cmd.Parameters.AddWithValue("@last_name", tbBillLastName.Text.ToString());
                cmd.Parameters.AddWithValue("@email", tbBillEmail.Text.ToString());
                cmd.Parameters.AddWithValue("@billing_address", tbBillAddress.Text.ToString());
                cmd.Parameters.AddWithValue("@city", tbBillCity.Text.ToString());
                cmd.Parameters.AddWithValue("@state", tbBillState.Text.ToString());
                cmd.Parameters.AddWithValue("@zip_code ", tbBillZip.Text.ToString());
                cmd.Parameters.AddWithValue("@account_type", ddlCardType.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Phone", tbBillPhone.Text.ToString());
                cmd.Parameters.AddWithValue("@cardholder_name", tbNameOnCard.Text.ToString());
                cmd.Parameters.AddWithValue("@card_number", tbCardNumber.Text.ToString());
                cmd.Parameters.AddWithValue("@expiration_date", tbExpireDate.Text.ToString());
                cmd.Parameters.AddWithValue("@cvs", tbCVCode.Text.ToString());

                cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                cmd.ExecuteNonQuery();



            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);

            }
            finally
            {
                con.Close();
                ClearData(this);
            }
        }

    }
}