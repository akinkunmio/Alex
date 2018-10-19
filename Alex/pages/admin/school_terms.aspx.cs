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
    public partial class school_terms : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            if (!Page.IsPostBack)
            {
                DropDownYear();
                TermBindData();
                //TermActiveBindData();
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
        public void DropDownYear()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_acad_year_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlAcademicYear.DataSource = ddlValues;
            ddlAcademicYear.DataValueField = "acad_year";
            ddlAcademicYear.DataTextField = "acad_year";
            ddlAcademicYear.DataBind();

            //Adding "Please select" option in dropdownlist for validation
            ddlAcademicYear.Items.Insert(0, new ListItem("Please select a Year", ""));

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
                cmd.Parameters.AddWithValue("@acad_year", ddlAcademicYear.Text.ToString());
                cmd.Parameters.AddWithValue("@term_name", ddlTermName.SelectedItem.Text.ToString());
                cmd.Parameters.AddWithValue("@ay_term_start_date", tbStartDate.Text.ToString());
                cmd.Parameters.AddWithValue("@ay_term_end_date", tbEndDate.Text.ToString());
                cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                cmd.ExecuteNonQuery();
                TermBindData();
                //TermActiveBindData();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Term Saved Successfully');", true);
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
                        GridViewTerm.DataBind();
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

                cmd.Parameters.Add(new SqlParameter() { ParameterName = "term_name", Value = txtTerm.SelectedValue});
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "ay_term_start_date", Value = txtStartDate.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "ay_term_end_date", Value = txtEndDate.Text });
                cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
                GridViewTerm.EditIndex = -1;
                TermBindData();
                //TermActiveBindData();
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Cannot Delete, Term has either fee setup or Registrations or assessments');", true);
            }
            finally
            {
                con.Close();
            }
        }

        //protected void TermActiveBindData()
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("sp_ms_settings_acad_year_term_list_active", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        using (SqlDataAdapter sda = new SqlDataAdapter())
        //        {
        //            con.Open();
        //            cmd.Connection = con;
        //            sda.SelectCommand = cmd;
        //            using (DataTable dt = new DataTable())
        //            {

        //                sda.Fill(dt);
        //                GridViewTermActive.DataSource = dt;
        //                GridViewTermActive.DataBind();
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
        //        // ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('SUCESSFULLY RECORD SAVED');", true);
        //    }
        //}
        protected void btnStatus_Click(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((Button)sender).Parent.Parent)
            {
                string ID = row.Cells[0].Text;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_settings_acad_year_term_set_active", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ay_term_id", SqlDbType.Int).Value = ID;
                        cmd.ExecuteNonQuery();
                        TermBindData();
                    }

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                }
                finally
                {
                    con.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Term Active Sucessfully');", true);
                }
            }
        }

        protected void GridViewTerm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (GridViewTerm.Rows.Count != -1)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string Value = e.Row.Cells[5].Text;
                    Button btntext = e.Row.FindControl("btnStatus") as Button;
                    switch (Value)
                    {
                        case "Active":
                            e.Row.BackColor = System.Drawing.Color.LightBlue;
                            btntext.Text = "Activated";
                            break;
                    }
                }
            }
        }
    }
}