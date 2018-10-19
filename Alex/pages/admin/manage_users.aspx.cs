using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Alex.App_code;

namespace Alex.pages.admin
{
    public partial class manage_users : System.Web.UI.Page
    {
        int lvl = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            if (!IsPostBack)
            {
                Level();
                if (lvl == 3 || lvl == 4)
                {
                    Response.Redirect("~/pages/ForbiddenAccess.aspx", false);
                }
               
                ManageUsersBindData();
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
        protected void ManageUsersBindData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_settings_login_list_all", con);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        GridViewManageUsers.DataSource = dt;
                        GridViewManageUsers.DataBind();
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

        protected void GridViewManageUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewManageUsers.EditIndex = e.NewEditIndex;
            ManageUsersBindData();
        }

        protected void GridViewManageUsers_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GridViewManageUsers.EditIndex = -1;
            ManageUsersBindData();
        }

        protected void GridViewManageUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ms_settings_login_edit", con);
            GridViewRow row = GridViewManageUsers.Rows[e.RowIndex] as GridViewRow;
            TextBox txtFirstName = row.FindControl("tbFirstName") as TextBox;
            TextBox txtLastName = row.FindControl("tbLastName") as TextBox;
            TextBox txtEmail = row.FindControl("tbEmail") as TextBox;
            DropDownList txtLevel = row.FindControl("ddlLevelOfAccess") as DropDownList;
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "id", Value = GridViewManageUsers.Rows[e.RowIndex].Cells[0].Text });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "f_name", Value = txtFirstName.Text });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "l_name", Value = txtLastName.Text });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "email_add", Value = txtEmail.Text });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "position", Value = txtLevel.Text });
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            GridViewManageUsers.EditIndex = -1;
            ManageUsersBindData();
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

        protected void btnAccess_Click(object sender, EventArgs e)
        {

            using (GridViewRow row = (GridViewRow)((Button)sender).Parent.Parent)
            {
                string ID = row.Cells[0].Text;

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
                Button Button = row.FindControl("btnAccess") as Button;
                string ButtonName = Button.Text;
                   // (Button)e.row.FindControl("btnAccess").ToString();

                if (ButtonName == "Disable")
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_ms_disable_user", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@login_id", SqlDbType.VarChar).Value = ID;
                            cmd.ExecuteNonQuery();
                            ManageUsersBindData();
                        }

                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                    }
                    finally
                    {
                        con.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Disabled Sucessfully');", true);
                    }
                }
                else
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_ms_enable_user", con))
                        {
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@login_id", SqlDbType.VarChar).Value = ID;
                            cmd.ExecuteNonQuery();
                            ManageUsersBindData();
                        }

                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                    }
                    finally
                    {
                        con.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Enabled Sucessfully');", true);
                    }
                }
            }

        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((Button)sender).Parent.Parent)
            {
                string Email = (row.Cells[0].FindControl("lblEmail") as Label).Text;

                //string Email = row.Cells[3].Text;
                Response.Redirect("admin_password_change.aspx?Email=" + Email.ToString());
            }

        }

        protected void GridViewManageUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
          SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ms_settings_login_delete", con);
            GridViewRow row = GridViewManageUsers.Rows[e.RowIndex] as GridViewRow;
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "id", Value = GridViewManageUsers.Rows[e.RowIndex].Cells[0].Text });
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            ManageUsersBindData();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("FK_ms_login_history_ms_login_history"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot delete this user, Contact iQ Support')", true);
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

        protected void GridViewManageUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btn = (Button)e.Row.FindControl("btnAccess");
                string AC = ((Button)e.Row.FindControl("btnAccess")).Text;
                if (AC == "Enable")
                    btn.CssClass = "btn-sm btn-success m-t-n-xs";
                //btn.Style.Add("background-color", "yellow");
                else if ((AC == "Disable"))
                    btn.CssClass = "btn-sm btn-primary m-t-n-xs";
                Label tbExDt = e.Row.FindControl("lblPwdExp") as Label;
                switch (tbExDt.Text)
                {
                    case "YES":
                        e.Row.CssClass = "text-danger";
                        break;
                }
               
            }
        }

    }
}