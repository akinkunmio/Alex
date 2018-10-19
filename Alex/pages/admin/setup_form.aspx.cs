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
    public partial class setup_form : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication(); 
            if (!IsPostBack)
            {
                DropDownSection();
                FormBindData();
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Class Saved Successfully');", true);

            }
            catch 
            {
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());   
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Cannot Delete, Class was used in Class Name');", true);
            }
            finally
            {
                con.Close();
            }
        }

        protected void GridViewForm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && GridViewForm.EditIndex == e.Row.RowIndex)
            {
                DropDownList ddlEditSection = (DropDownList)e.Row.FindControl("ddlEditSection") as DropDownList;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
                SqlCommand cmd = new SqlCommand("sp_ms_section_dropdown", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader ddlValues;
                ddlValues = cmd.ExecuteReader();

                ddlEditSection.DataSource = ddlValues;
                ddlEditSection.DataValueField = "form_display_order";
                ddlEditSection.DataTextField = "section2";
                ddlEditSection.DataBind();

                string SectionSelectedItem = DataBinder.Eval(e.Row.DataItem, "section2").ToString();
                ddlEditSection.Items.FindByText(SectionSelectedItem).Selected = true;
               
                cmd.Connection.Close();
                cmd.Connection.Dispose();

            }
        }

       
    }
}