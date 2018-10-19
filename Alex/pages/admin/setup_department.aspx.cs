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
    public partial class setup_department : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            if (!Page.IsPostBack)
            {
                DepartmentBindData();
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
        protected void BtnSaveDepartment_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_hr_department_add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                cmd.Parameters.AddWithValue("@dept_name", tbDepartment.Text.ToString());
                cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
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
                ClearData(this);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Message", "alert('Department Saved Successfully');", true);
                DepartmentBindData();

            }          
        }

        protected void DepartmentBindData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_hr_department_list_all", con);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        GridViewDepartment.DataSource = dt;
                        GridViewDepartment.DataBind();
                    }
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('SUCESSFULLY RECORD SAVED');", true);
            }
        }

        protected void GridViewDepartment_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewDepartment.EditIndex = e.NewEditIndex;
            DepartmentBindData();
        }

        protected void GridViewDepartment_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GridViewDepartment.EditIndex = -1;
            DepartmentBindData();
        }

        protected void GridViewDepartment_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ms_hr_department_edit", con);
            GridViewRow row = GridViewDepartment.Rows[e.RowIndex] as GridViewRow;
            TextBox tbdepartment = row.FindControl("tbDepartment") as TextBox;
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "dept_id", Value = GridViewDepartment.Rows[e.RowIndex].Cells[0].Text });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "dept_name", Value = tbdepartment.Text });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "updated_by", Value = HttpContext.Current.User.Identity.Name });
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            con.Close();
            GridViewDepartment.EditIndex = -1;
            DepartmentBindData();
        }

        protected void GridViewDepartment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
               SqlCommand cmd = new SqlCommand("sp_ms_hr_department_delete", con);
               GridViewRow row = GridViewDepartment.Rows[e.RowIndex] as GridViewRow;
               TextBox tbdepartment = row.FindControl("tbDepartment") as TextBox;
               cmd.Parameters.Add(new SqlParameter() { ParameterName = "dept_id", Value = GridViewDepartment.Rows[e.RowIndex].Cells[0].Text });
               //cmd.Parameters.Add(new SqlParameter() { ParameterName = "dept_name", Value = tbdepartment.Text });
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.ExecuteNonQuery();
               con.Close();
               DepartmentBindData();
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Message", "alert('Unable to delete this department because an employee has been assigned to it.');", true);
            }
            finally
            {
                con.Close();
            }
        }
    }
}