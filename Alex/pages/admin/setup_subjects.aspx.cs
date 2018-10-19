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
    public partial class setup_subjects : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ManageCookies.VerifyAuthentication();
                SubjectsBindData();
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

        protected void BtnSaveSetupSubject_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_settings_subject_add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@subject_name", tbSetupSubject.Text.ToString());
                cmd.Parameters.AddWithValue("@grading_type", ddlGrading.SelectedItem.Text.ToString());
                cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                cmd.ExecuteNonQuery();
                SubjectsBindData();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Subject Saved Successfully');", true);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Violation of UNIQUE KEY constraint 'UNQ_subjects'"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Unable to add subject, Subject already exist')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                } 
            }
            finally
            {
                con.Close();
                ClearData(this);
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
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_settings_subject_edit", con);
                GridViewRow row = GridViewSubjects.Rows[e.RowIndex] as GridViewRow;
                TextBox txtValue = row.FindControl("tbSubjectName") as TextBox;
                DropDownList ddlEditGrading = row.FindControl("ddlEditGrading") as DropDownList;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "subject_id", Value = GridViewSubjects.Rows[e.RowIndex].Cells[0].Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "subject_name", Value = txtValue.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "grading_type", Value = ddlEditGrading.SelectedItem.Text });
                cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                GridViewSubjects.EditIndex = -1;
                SubjectsBindData();
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clientscript", "alert('Cannot update, Subject name already exist');", true);
            }
            finally
            {
                con.Close();
            }
        }


        protected void GridViewSubjects_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_settings_subject_delete", con);
                GridViewRow row = GridViewSubjects.Rows[e.RowIndex] as GridViewRow;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "subject_id", Value = GridViewSubjects.Rows[e.RowIndex].Cells[0].Text });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                SubjectsBindData();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("FK_ms_class_subjects_ms_subjects"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Unable to delete subject, Subject has been assigned to class')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Unable to delete subject because subject has a marked assessment');", true);
                }
            }
           
            finally
            {
                con.Close();
            }
        }
    }
}