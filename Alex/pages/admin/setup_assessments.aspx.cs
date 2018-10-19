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

namespace Alex.pages.admin
{
    public partial class setup_assessments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownFormClass();
                //DropDownAssessmentType();
            }
        }

        public void DropDownFormClass()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_form_class_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlFormClass.DataSource = ddlValues;
            ddlFormClass.DataValueField = "form_class";
            ddlFormClass.DataTextField = "form_class";
            ddlFormClass.DataBind();
            ddlFormClass.Items.Insert(0, new ListItem("Please select Class", ""));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }



        protected void AssessmentListBindData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_class_subject_offered_list_all", con);
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.AddWithValue("@assessment_type", ddlAssessmentType.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@class_name", ddlFormClass.SelectedItem.ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        GridViewListAssesssmentType.DataSource = dt;
                        if (dt.Rows.Count == 0)
                        {
                            GridViewListAssesssmentType.Visible = false;
                            lblZeroRecords.Visible = true;
                            lblZeroRecords.Text = "No Records found ";
                        }
                        else
                        {
                            GridViewListAssesssmentType.Visible = true;
                            GridViewListAssesssmentType.DataBind();
                            lblZeroRecords.Visible = false;
                        }


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



        protected void GridViewListAssesssmentType_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_class_subject_delete", con);
                GridViewRow row = GridViewListAssesssmentType.Rows[e.RowIndex] as GridViewRow;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@class_subjects_id", Value = GridViewListAssesssmentType.Rows[e.RowIndex].Cells[0].Text });
                //cmd.Parameters.Add(new SqlParameter() { ParameterName = "form_class", Value = ddlFormClass.SelectedItem.ToString() });
                //cmd.Parameters.Add(new SqlParameter() { ParameterName = "assessment_type", Value = ddlAssessmentType.SelectedItem.ToString() });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                AssessmentListBindData();
                AvailableSubBindGrid();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Unable to delete this Subject, because subject is used in Assessments.');", true);
            }
            finally
            {
                con.Close();
            }
        }

        private void AvailableSubBindGrid()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_class_available_subjects", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.AddWithValue("@assessment_type", ddlAssessmentType.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@form_class", ddlFormClass.SelectedItem.ToString());
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;

                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            GridViewCreateAssessments.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                GridViewCreateAssessments.Visible = false;
                                lblZeroRecords.Visible = true;
                                btnCreateAssessments.Visible = false;
                                lblZeroSubjects.Text = "No Subjects found ";
                            }
                            else
                            {
                                GridViewCreateAssessments.Visible = true;
                                GridViewCreateAssessments.DataBind();
                                lblZeroSubjects.Visible = false;
                                btnCreateAssessments.Visible = true;
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
        protected void BtnSearchAvailSub_Click(object sender, EventArgs e)
        {
            AvailableSubBindGrid();
            AssessmentListBindData();
            lblClass.Text = ddlFormClass.SelectedItem.ToString();
            lblClassName.Text = ddlFormClass.SelectedItem.ToString();
            divCreateSubjects.Visible = true;
            divListAssessments.Visible = true;
        }

        protected void chkboxSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)GridViewCreateAssessments.HeaderRow.FindControl("chkboxSelect");
            foreach (GridViewRow row in GridViewCreateAssessments.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkSubject");
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

        protected void btnCreateAssessments_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            foreach (GridViewRow row in GridViewCreateAssessments.Rows)
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_class_subject_add", conn))
                {
                    CheckBox ChkBoxHeader = (CheckBox)GridViewCreateAssessments.HeaderRow.FindControl("chkboxSelect");
                    string Subject = System.Net.WebUtility.HtmlDecode(row.Cells[2].Text); 
                    CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkSubject");

                    if (ChkBoxRows != null && ChkBoxRows.Checked)
                    {
                        // cmd.Parameters.AddWithValue("@assessment_type_name", ddlAssessmentType.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@class_name", ddlFormClass.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@subject_name", Subject);
                        cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        int rowaffected = cmd.ExecuteNonQuery(); //this was missing.
                        conn.Close();
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Subject(s) assigned to class Successfully ');", true);
            AvailableSubBindGrid();
            AssessmentListBindData();
        }

    }
}