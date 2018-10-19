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
    public partial class setup_class : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            if (!Page.IsPostBack)
            {
                DropDownForm();
                ClassBindData();
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
                //cmd.Parameters.Add(new SqlParameter() { ParameterName = "form_name", Value = txtForm.Text });
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
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "clentscript", "alert('Cannot update, Arm already exist');", true);
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Arm Saved Successfully');", true);

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
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_settings_classes_delete", con);
                GridViewRow row = GridViewClass.Rows[e.RowIndex] as GridViewRow;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "class_id", Value = GridViewClass.Rows[e.RowIndex].Cells[0].Text });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
                ClassBindData();
            }
             catch (Exception ex)
            {
                if (ex.Message.Contains("FK_ms_class_subjects_ms_classes"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Unable to delete class, as it has subject or assessments assigned to it.')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Unable to delete this class, because a student has been assigned to it.');", true);
                }
            }
            finally
            {
                con.Close();
            }
        }
    }
}