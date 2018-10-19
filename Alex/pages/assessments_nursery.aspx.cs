using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Configuration;
using Alex.App_code;

namespace Alex.pages
{
    public partial class assessments_nursery : System.Web.UI.Page
    {
        int lvl = 0;
        //int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            ManageCookies.VerifyAuthentication();
            Level();
            if (lvl == 1 || lvl == 2 || lvl == 3 || lvl == 4)
            {
                if (!IsPostBack)
                {
                    DropDownFormClass();
                    ddlSubject.Items.Add(new ListItem("Select Class first", ""));
                    //ddlAcademicYear.SelectedIndex = ddlActive.Items.FindByVlaue('1'));
                    divEdit.Visible = false;
                }
            }
            else if (lvl == 5)
            {
                Response.Redirect("~/pages/ForbiddenAccess.aspx", false);
            }
            else
            {
                Response.Redirect("~/pages/login.aspx", false);
            }
        }

        public void Level()
        {
            try
            {

                lvl = (int)(Session["level_of_access"]);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            }
        }



        public void DropDownFormClass()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            String strQuery = "SELECT DISTINCT form_name+ [class_name] as form_class,class_id FROM [dbo].[ms_classes], ms_forms where ms_classes.form_id =ms_forms.form_id  and section2= 'Nursery' order by form_class";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strQuery;
            cmd.Connection = con;
            try
            {
                con.Open();
                ddlFormClass.DataSource = cmd.ExecuteReader();
                ddlFormClass.DataValueField = "class_id";
                ddlFormClass.DataTextField = "form_class";
                ddlFormClass.DataBind();
                ddlFormClass.Items.Insert(0, new ListItem("Please select Class", ""));
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



        //protected void Count()
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("assessment_weight_count", con);
        //        cmd.Parameters.AddWithValue("@class", ddlFormClass.SelectedItem.ToString());
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        con.Open();
        //        count = (Int32)cmd.ExecuteScalar();
        //        if (count == 0)
        //        {
        //            GridViewAssessments.Visible = false;
        //            divEdit.Visible = false;
        //            lblZeroWeighting.Visible = true;
        //            lblZeroWeighting.Text = "Weighting has not been setup, please go to settings for setup";
        //        }
        //        if (count == 1)
        //        {
        //            lblZeroWeighting.Visible = false;
        //            divEdit.Visible = true;
        //            GridViewAssessments.Columns[3].Visible = false;
        //            GridViewAssessments.Columns[4].Visible = false;
        //            GridViewAssessments.Columns[5].Visible = false;
        //            GridViewAssessments.Columns[6].Visible = false;
        //        }
        //        if (count == 2)
        //        {
        //            lblZeroWeighting.Visible = false;
        //            divEdit.Visible = true;
        //            GridViewAssessments.Columns[3].Visible = false;
        //            GridViewAssessments.Columns[4].Visible = false;
        //            GridViewAssessments.Columns[5].Visible = false;
        //        }
        //        else if (count == 3)
        //        {
        //            lblZeroWeighting.Visible = false;
        //            divEdit.Visible = true;
        //            GridViewAssessments.Columns[4].Visible = false;
        //            GridViewAssessments.Columns[5].Visible = false;
        //        }
        //        else if (count == 4)
        //        {
        //            lblZeroWeighting.Visible = false;
        //            divEdit.Visible = true;
        //            GridViewAssessments.Columns[5].Visible = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //        // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        //protected void AssessmentTotalBind()
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("sp_ms_assessment_weighting_all", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        using (SqlDataAdapter sda = new SqlDataAdapter())
        //        {
        //            con.Open();
        //            cmd.Connection = con;
        //            sda.SelectCommand = cmd;
        //            using (DataTable dt = new DataTable())
        //            {
        //                sda.Fill(dt);
        //                if (dt.Rows.Count != 0)
        //                {
        //                    lblAss1.Text = dt.Rows[0]["description"].ToString();
        //                    lblAss2.Text = dt.Rows[1]["description"].ToString();
        //                    lblAss3.Text = dt.Rows[2]["description"].ToString();
        //                    lblAss4.Text = dt.Rows[3]["description"].ToString();
        //                    lblAss5.Text = dt.Rows[4]["description"].ToString();
        //                    //lblAss6.Text = dt.Rows[5]["description"].ToString();
        //                }
        //            }
        //        }
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //        // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        private void BindGrid()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_assessment_class_subject_list_all", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@class", ddlFormClass.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@subject_name", ddlSubject.SelectedItem.ToString());

                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridViewAssessments.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                GridViewAssessments.Visible = false;
                                lblZeroRecords.Visible = true;
                                lblZeroRecords.Text = "No student registrations found for this class to Mark assessments, please register students to the class";
                                btnEdit.Visible = false;
                                btnUploadAssessments.Visible = false;
                            }
                            else
                            {
                                GridViewAssessments.Visible = true;
                                GridViewAssessments.DataBind();
                                //SqlCommand Ass1 = new SqlCommand("SELECT assessment_weight FROM ms_assessment_weight  where [ayt_id] = (select ay_term_id from ms_acad_year_term where status = 'Active') and assessment = 'Assessment 1' and section = (select section from ms_forms, ms_classes where ms_forms.form_id = ms_classes.form_id and concat(form_name, class_name)  =" + "'" + ddlFormClass.SelectedItem.ToString() + "'" + ")", con);
                                //SqlCommand Ass2 = new SqlCommand("SELECT assessment_weight FROM ms_assessment_weight  where [ayt_id] = (select ay_term_id from ms_acad_year_term where status = 'Active') and assessment = 'Assessment 2' and section = (select section from ms_forms, ms_classes where ms_forms.form_id = ms_classes.form_id and concat(form_name, class_name)  =" + "'" + ddlFormClass.SelectedItem.ToString() + "'" + ")", con);
                                //SqlCommand Ass3 = new SqlCommand("SELECT assessment_weight FROM ms_assessment_weight  where [ayt_id] = (select ay_term_id from ms_acad_year_term where status = 'Active') and assessment = 'Assessment 3' and section = (select section from ms_forms, ms_classes where ms_forms.form_id = ms_classes.form_id and concat(form_name, class_name)  =" + "'" + ddlFormClass.SelectedItem.ToString() + "'" + ")", con);
                                //SqlCommand Ass4 = new SqlCommand("SELECT assessment_weight FROM ms_assessment_weight  where [ayt_id] = (select ay_term_id from ms_acad_year_term where status = 'Active') and assessment = 'Assessment 4' and section = (select section from ms_forms, ms_classes where ms_forms.form_id = ms_classes.form_id and concat(form_name, class_name)  =" + "'" + ddlFormClass.SelectedItem.ToString() + "'" + ")", con);
                                //SqlCommand Ass5 = new SqlCommand("SELECT assessment_weight FROM ms_assessment_weight  where [ayt_id] = (select ay_term_id from ms_acad_year_term where status = 'Active') and assessment = 'Final Exam' and section = (select section from ms_forms, ms_classes where ms_forms.form_id = ms_classes.form_id and concat(form_name, class_name)    =" + "'" + ddlFormClass.SelectedItem.ToString() + "'" + ")", con);
                                //var Assessment1 = Ass1.ExecuteScalar();
                                //var Assessment2 = Ass2.ExecuteScalar();
                                //var Assessment3 = Ass3.ExecuteScalar();
                                //var Assessment4 = Ass4.ExecuteScalar();
                                //var Assessment5 = Ass5.ExecuteScalar();



                                //GridViewAssessments.HeaderRow.Cells[2].Text = "Assesment1 \n (Max Score" + Assessment1.ToString() + ")";
                                //GridViewAssessments.HeaderRow.Cells[3].Text = "Assesment2  (Max Score" + Assessment2.ToString() + ")";
                                //GridViewAssessments.HeaderRow.Cells[4].Text = "Assesment3  (Max Score" + Assessment3.ToString() + ")";
                                //GridViewAssessments.HeaderRow.Cells[5].Text = "Assesment4  (Max Score" + Assessment4.ToString() + ")";
                                //GridViewAssessments.HeaderRow.Cells[6].Text = "Final Exam  (Max Score" + Assessment5.ToString() + ")";
                                lblZeroWeighting.Visible = false;
                                lblZeroRecords.Visible = false;
                                divEdit.Visible = true;
                                btnEdit.Visible = true;
                                btnUploadAssessments.Visible = false;
                                btnCancel.Visible = false;
                                //Count();
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
        protected void GridViewAssessments_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hl = (HyperLink)e.Row.FindControl("HyperLinkPeople");
                if (hl != null)
                {
                    DataRowView drv = (DataRowView)e.Row.DataItem;
                    string keyword = drv["person_id"].ToString();
                    string regid = drv["reg_id"].ToString();
                    hl.NavigateUrl = "~/pages/profile.aspx?PersonId=" + keyword + "&Regid=" + regid + "&action=ast";
                }
            }
        }

        //protected void btnSearchFormClass_Click(object sender, EventArgs e)
        //{
        //    BindGrid();
        //}

        protected void btnUploadAssessments_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                foreach (GridViewRow row in GridViewAssessments.Rows)
                {
                    var textbox1 = row.FindControl("dd1") as DropDownList;
                    
                    // var textbox6 = row.FindControl("tbAssessment6") as TextBox;
                    using (SqlCommand cmd = new SqlCommand("sp_ms_batch_assessment3_class_subject_type_edit", con))
                    {
                        con.Open();
                        string AssessmentId = row.Cells[0].Text;
                        //cmd.Parameters.Add("@assessment_id", SqlDbType.VarChar, 50).Value = AssessmentId;
                        cmd.Parameters.AddWithValue("@assessment_id", AssessmentId);
                        cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
                        cmd.Parameters.AddWithValue("@publish8", textbox1.Text);
                        //cmd.Parameters.AddWithValue("@publish8", textbox2.Text);
                        //cmd.Parameters.AddWithValue("@publish8", textbox3.Text);
                        //cmd.Parameters.AddWithValue("@publish8", textbox4.Text);
                        //cmd.Parameters.AddWithValue("@publish8", textbox5.Text);


                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();

                        BindGrid();
                        btnEdit.Visible = true;
                        btnCancel.Visible = false;
                        btnUploadAssessments.Visible = false;
                        con.Close();
                    }
                }
            }

            catch (Exception ex)
            {
                if (ex.Message.Contains("Object cannot be cast from DBNull to other types"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot save assessments, weighting should setup for the term assessment ')", true);
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
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            btnUploadAssessments.Visible = true;
            btnEdit.Visible = false;
            btnCancel.Visible = true;
            foreach (GridViewRow row in GridViewAssessments.Rows)
            {
                ((DropDownList)row.FindControl("dd1")).Enabled = true;
                
            }
        }

        protected void ddlFormClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSubject.Items.Clear();
            ddlSubject.Items.Add(new ListItem("Please Select Subject", ""));
            //ddlFeeSetupForm.Items.Clear();
            //ddlFeeSetupForm.Items.Add(new ListItem("Select Class", ""));

            ddlSubject.AppendDataBoundItems = true;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            //String strQuery = "select class_subjects_id, cs.class_id, cs.subject_id, class_name, subject_name from [ms_class_subjects], [ms_classes], [ms_subjects]" +
            //                   "where cs.class_id = c.class_id and cs.subject_id = s.subject_id and c.class_id = @class_id order by subject_name";
            String strQuery = "sp_ms_class_nursery_assessment_subject_dropdown";
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@class_id",
                 ddlFormClass.SelectedItem.Value);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = strQuery;
            cmd.Connection = con;
            try
            {
                con.Open();
                ddlSubject.DataSource = cmd.ExecuteReader();
                ddlSubject.DataTextField = "subject_name";
                ddlSubject.DataValueField = "subject_id";
                ddlSubject.DataBind();
                if (ddlSubject.Items.Count > 1)
                {
                    ddlSubject.Enabled = true;
                    // btnSearchFormClass.Enabled = true;

                }
                else
                {
                    ddlSubject.Items.Remove(new ListItem("Please Select Subject", ""));
                    ddlSubject.Items.Add(new ListItem("No Subject(s) Added", ""));
                    divEdit.Visible = false;
                    GridViewAssessments.Visible = false;
                    ddlSubject.Enabled = false;
                    // btnSearchFormClass.Enabled = false;

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

        protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
            btnCancel.Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            BindGrid();
            btnCancel.Visible = false;
            btnEdit.Visible = true;
            btnUploadAssessments.Visible = false;
        }
    }
}