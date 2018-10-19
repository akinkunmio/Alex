using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using Alex.App_code;


namespace Alex.pages.admin
{
    public partial class setup_assessmentweight : System.Web.UI.Page
    {
        string CurrentYear = string.Empty;
        string CurrentTerm = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            if (!Page.IsPostBack)
            {
                CurrentYearTerm();
                DropDownYear();
                DropDownTerm();
                DropDownSection();
                ddlSection.SelectedIndex = 1;
                NextTermStartDate();
                if (ddlAcademicYear.SelectedItem.Text == CurrentYear && ddlTerm.SelectedItem.Text == CurrentTerm)
                {
                    AssessmentTotalBind();
                    GradeBindData();
                    divCurrentTerm.Visible = true;
                    divPreviousTerm.Visible = false;
                }
                else
                { 
                PublishBindData();
                GradeYearTermBindData();
                divCurrentTerm.Visible = false;
                divPreviousTerm.Visible = true;
                }
            }

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


        protected void AssessmentTotalBind()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_assessment_weight_list_all", con);
                cmd.Parameters.AddWithValue("@section", ddlSection.SelectedItem.ToString());
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
                            GridViewWeighting.FooterRow.Cells[1].Text = "Total";
                            GridViewWeighting.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                            GridViewWeighting.FooterRow.Cells[2].Text = total.Value.ToString("D");
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
                cmd.ExecuteNonQuery();
                GridViewWeighting.EditIndex = -1;
                AssessmentTotalBind();
                
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

            try
            {
                int Grandtotal = 0;
                int exa = 0;
                int Ass = 0;
                foreach (GridViewRow row in GridViewWeighting.Rows)
                {
                    TextBox txtCategory = row.FindControl("tbTotalMarks") as TextBox;
                    TextBox txtAssessmentType = row.FindControl("tbAssessment") as TextBox;
                    if (txtAssessmentType.Text == "Final Exam") { exa = txtCategory.Text.Length; }
                    if (txtAssessmentType.Text == "Assessment 1") { Ass = txtCategory.Text.Length; }
                    if (!string.IsNullOrWhiteSpace(txtCategory.Text))
                    {
                        Grandtotal = Grandtotal + Convert.ToInt32(txtCategory.Text);
                    }
                    else if (exa == 0) { ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", "alert('Exam field is mandatory');", true); }
                    else if (Ass == 0) { ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", "alert('Assessment 1 field is mandatory');", true); }
                }
                if (Grandtotal < 100 || Grandtotal > 100)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", "alert('Assessemts will not work, Total should be 100');", true);
                }
                else if (exa != 0 && Ass != 0)
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

        protected void btnPublish_Click(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((Button)sender).Parent.Parent)
            {
                string PublishText = row.Cells[4].Text;
                Button btntext = row.FindControl("btnPublish") as Button;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_publish_assessment", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@publish_no", SqlDbType.VarChar).Value = PublishText;
                        cmd.Parameters.Add("@updated_by", SqlDbType.VarChar).Value = HttpContext.Current.User.Identity.Name;
                        if (btntext.Text == "Publish")
                        {
                            cmd.Parameters.Add("@commit", SqlDbType.VarChar).Value = "Y";
                        }
                        else
                        {
                            cmd.Parameters.Add("@commit", SqlDbType.VarChar).Value = "N";
                        }
                        cmd.ExecuteNonQuery();
                        AssessmentTotalBind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Success');", true);
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
        }

        protected void GridViewWeighting_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (GridViewWeighting.Rows.Count != -1)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string Value = e.Row.Cells[5].Text;
                    Button btntext = e.Row.FindControl("btnPublish") as Button;
                    switch (Value)
                    {
                        case "Y":
                            // e.Row.BackColor = System.Drawing.Color.LightBlue;
                            btntext.Text = "UnPublish";
                            break;
                    }
                    var TxtAssessmentmarks = e.Row.FindControl("tbTotalMarks") as TextBox;
                    var AssessTxtBox = TxtAssessmentmarks.Text;
                    switch (AssessTxtBox)
                    {
                        case "":
                            btntext.CssClass = "btn-block";
                            btntext.Enabled = false;
                            // btntext.Attributes["class"] = "btn-block";
                            break;
                    }
                }
            }
        }


        public void DropDownTerm()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_term_dropdown_v2", con);
            cmd.Parameters.Add("@acad_year", SqlDbType.VarChar).Value = ddlAcademicYear.SelectedItem.ToString();
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
                ddlTerm.DataSource = ddlValues;
                ddlTerm.DataValueField = "term_name";
                ddlTerm.DataTextField = "term_name";
                ddlTerm.DataBind();
                ddlTerm.Items.Insert(0, new ListItem("Please select Term", ""));
                ddlTerm.SelectedValue = TermSelectedValue;

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


        public void DropDownYear()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_acad_year_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            try
            {
                SqlDataReader ddlValues;
                ddlValues = cmd.ExecuteReader();
                string YearSelectedValue = null;

                while (ddlValues.Read())
                {
                    YearSelectedValue = ddlValues[0].ToString();
                    int DefaultValue = Convert.ToInt32(ddlValues[1]);
                    if (DefaultValue == 1)
                        break;
                }
                ddlValues.Close();
                ddlValues = cmd.ExecuteReader();

                ddlAcademicYear.DataSource = ddlValues;
                ddlAcademicYear.DataValueField = "acad_year";
                ddlAcademicYear.DataTextField = "acad_year";
                ddlAcademicYear.DataBind();
                ddlAcademicYear.Items.Insert(0, new ListItem("Select Academic Year", ""));
                ddlAcademicYear.SelectedValue = YearSelectedValue;
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


        protected void PublishBindData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
               SqlCommand cmd = new SqlCommand("sp_ms_assessments_published_info_acadyear_term", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@acad_year", ddlAcademicYear.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@term_name", ddlTerm.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@section", ddlSection.SelectedItem.ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        GridViewPublish.DataSource = dt;
                        if (dt.Rows.Count == 0)
                        {
                            lblZeroRecords.Visible = true;
                            lblZeroRecords.Text = "No Publish Information found ";
                            GridViewPublish.Visible = false;
                        }
                        else
                        {
                            GridViewPublish.DataBind();
                            GridViewPublish.Visible = true;
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

        protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            //PublishBindData();
            //GradeYearTermBindData();
            CurrentYearTerm();
            if (ddlAcademicYear.SelectedItem.Text == CurrentYear && ddlTerm.SelectedItem.Text == CurrentTerm)
            {
                AssessmentTotalBind();
                GradeBindData();
                divCurrentTerm.Visible = true;
                divPreviousTerm.Visible = false;
            }
            else
            {
                PublishBindData();
                GradeYearTermBindData();
                divCurrentTerm.Visible = false;
                divPreviousTerm.Visible = true;
            }
        }

        protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            //PublishBindData();
            //GradeYearTermBindData();
            CurrentYearTerm();
            if (ddlAcademicYear.SelectedItem.Text == CurrentYear && ddlTerm.SelectedItem.Text == CurrentTerm)
            {
                AssessmentTotalBind();
                GradeBindData();
                divCurrentTerm.Visible = true;
                divPreviousTerm.Visible = false;
            }
            else
            {
                PublishBindData();
                GradeYearTermBindData();
                divCurrentTerm.Visible = false;
                divPreviousTerm.Visible = true;
            }
        }


        protected void GradeYearTermBindData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {

                SqlCommand cmd = new SqlCommand("sp_ms_assessment_grade_acadyear_term", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@acad_year", ddlAcademicYear.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@term_name", ddlTerm.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@section", ddlSection.SelectedItem.ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        GridViewYearTermGrade.DataSource = dt;
                        if (dt.Rows.Count == 0)
                        {
                            lblYearTermGrade.Visible = true;
                            lblYearTermGrade.Text = "No Grade Information found ";
                            GridViewYearTermGrade.Visible = false;
                        }
                        else
                        {
                            GridViewYearTermGrade.DataBind();
                            GridViewYearTermGrade.Visible = true;
                            lblYearTermGrade.Visible = false;
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


        protected void GradeBindData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {

                SqlCommand cmd = new SqlCommand("sp_ms_assessment_grade_list_all", con);
                cmd.Parameters.AddWithValue("@section", ddlSection.SelectedItem.ToString());
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        GridViewGrade.DataSource = dt;
                        if (dt.Rows.Count == 0)
                        {
                            lblZeroGrade.Visible = true;
                            lblZeroGrade.Text = "No Grade Information found ";
                            GridViewGrade.Visible = false;
                        }
                        else
                        {
                            GridViewGrade.DataBind();
                            GridViewGrade.Visible = true;
                            lblZeroGrade.Visible = false;
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

        protected void GridViewGrade_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewGrade.EditIndex = e.NewEditIndex;
            GradeBindData();
        }

        protected void GridViewGrade_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GridViewGrade.EditIndex = -1;
            GradeBindData();
        }

        protected void GridViewGrade_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_assessment_grade_list_edit", con);
                GridViewRow row = GridViewGrade.Rows[e.RowIndex] as GridViewRow;

                TextBox txtGrade = row.FindControl("tbGrade") as TextBox;
                TextBox txtGradeUpper = row.FindControl("tbGradeUpper") as TextBox;
                TextBox txtGradeLower = row.FindControl("tbGradeLower") as TextBox;
                TextBox txtGradeClassification = row.FindControl("tbclassification") as TextBox;
                TextBox txtGradeRemarks= row.FindControl("tbremarks") as TextBox;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "ayt_id", Value = GridViewGrade.Rows[e.RowIndex].Cells[1].Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "g_id", Value = GridViewGrade.Rows[e.RowIndex].Cells[0].Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "grade", Value = txtGrade.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "grade_upper_level", Value = txtGradeUpper.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "grade_lower_level", Value = txtGradeLower.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "classification", Value = txtGradeClassification.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "remarks", Value = txtGradeRemarks.Text });
                cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
               
                GridViewGrade.EditIndex = -1;
                GradeBindData();
                GradeYearTermBindData();
               
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

        protected void CurrentYearTerm()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open(); 
                SqlCommand cmd = new SqlCommand("sp_ms_rep_current_year_term", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read()){
                     CurrentYear = dr["acad_year"].ToString();
                     CurrentTerm = dr["term_name"].ToString();
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

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentYearTerm();
            if (ddlAcademicYear.SelectedItem.Text == CurrentYear && ddlTerm.SelectedItem.Text == CurrentTerm)
            {
                AssessmentTotalBind();
                GradeBindData();
                divCurrentTerm.Visible = true;
                divPreviousTerm.Visible = false;
            }
            else
            {
                PublishBindData();
                GradeYearTermBindData();
                divCurrentTerm.Visible = false;
                divPreviousTerm.Visible = true;
            }
        }

        protected void NextTermStartDate()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_assessment_acad_year_term_next_term ", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@acad_year", ddlAcademicYear.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@term_name", ddlTerm.SelectedItem.ToString());
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridViewNxtTrmSrtDt.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                //lblZeroAttendanceBreakDown.Visible = true;
                                //lblZeroAttendanceBreakDown.Text = "No Attendance Record Found ";
                            }
                            else
                            {
                                GridViewNxtTrmSrtDt.DataBind();
                                //lblZeroAttendanceBreakDown.Visible = false;
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

        protected void GridViewNxtTrmSrtDt_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewNxtTrmSrtDt.EditIndex = e.NewEditIndex;
            NextTermStartDate();
        }

        protected void GridViewNxtTrmSrtDt_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GridViewNxtTrmSrtDt.EditIndex = -1;
            NextTermStartDate();
       
        }

        protected void GridViewNxtTrmSrtDt_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_settings_acad_year_term_next_term_edit", con);
                GridViewRow row = GridViewNxtTrmSrtDt.Rows[e.RowIndex] as GridViewRow;
                TextBox txtStartDt = row.FindControl("tbNxtTrmSrtDt") as TextBox;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "ay_term_id", Value = GridViewNxtTrmSrtDt.Rows[e.RowIndex].Cells[0].Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "next_term_start_date", Value = txtStartDt.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "updated_by", Value = HttpContext.Current.User.Identity.Name });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                GridViewNxtTrmSrtDt.EditIndex = -1;
                NextTermStartDate();
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
    }
}