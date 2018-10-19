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
    public partial class setup_assessments_type : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication(); 
            if (!Page.IsPostBack)
            {
               
                AssessmentTypeBindData();
                
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

        protected void BtnSaveAssesssmentType_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_settings_assessment_types_add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@assessment_type",tbAssessmentType.Text.ToString());
                cmd.Parameters.AddWithValue("@assessment_name", tbDescription.Text.ToString());
                cmd.Parameters.AddWithValue("@assessment_max_grade", tbAssesssmentMaxGrade.Text.ToString());
                cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                cmd.ExecuteNonQuery();
                AssessmentTypeBindData();
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                         
            }
            finally
            {
                con.Close();
                ClearData(this);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Assessment Type Saved Successfully');", true);
           }
        }

        protected void AssessmentTypeBindData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_settings_assessment_types_list_all", con); 
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        GridViewAssesssmentType.DataSource = dt;
                        GridViewAssesssmentType.DataBind();
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

        protected void GridViewAssesssmentType_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewAssesssmentType.EditIndex = e.NewEditIndex;
            AssessmentTypeBindData();
        }

        protected void GridViewAssesssmentType_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GridViewAssesssmentType.EditIndex = -1;
            AssessmentTypeBindData();
        }

        protected void GridViewAssesssmentType_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ms_settings_assessment_types_edit", con);
            GridViewRow row = GridViewAssesssmentType.Rows[e.RowIndex] as GridViewRow;
            TextBox txtAssessmentType= row.FindControl("tbAssessmentType") as TextBox;
            TextBox txtDescription = row.FindControl("tbDescription") as TextBox;
            TextBox txtMaxGrade = row.FindControl("tbMaxGrade") as TextBox;
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "assessment_type_id", Value = GridViewAssesssmentType.Rows[e.RowIndex].Cells[0].Text });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "assessment_type", Value = txtAssessmentType.Text });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "assessment_name", Value = txtDescription.Text });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "assessment_max_grade", Value = txtMaxGrade.Text });
            cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            con.Close();
            GridViewAssesssmentType.EditIndex = -1;
            AssessmentTypeBindData();
            
        }

        protected void GridViewAssesssmentType_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ms_settings_assessment_types_delete", con);
            GridViewRow row = GridViewAssesssmentType.Rows[e.RowIndex] as GridViewRow;
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "assessment_type_id", Value = GridViewAssesssmentType.Rows[e.RowIndex].Cells[0].Text });
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            con.Close();
            AssessmentTypeBindData();
        }
    }
}