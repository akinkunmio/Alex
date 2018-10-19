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
    public partial class setup_school_timeline : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            if (!Page.IsPostBack)
            {
                DropDownYear(); 
                TimeLineBindData();
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
        protected void BtnSaveTimeLine_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_dashboard_event_calendar_add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                cmd.Parameters.AddWithValue("@cal_date", tbTimelineDate.Text.ToString());
                cmd.Parameters.AddWithValue("@term_name ", ddlTerm.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@acad_year", ddlAcademicYear.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@event", tbEvent.Text.ToString());
                cmd.Parameters.AddWithValue("@description",tbDescription.Text.ToString());
                cmd.Parameters.AddWithValue("@category", tbCategory.Text.ToString());
                cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                cmd.ExecuteNonQuery();
                TimeLineBindData();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Event Saved Successfully');", true);
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
            }       
        }

        protected void TimeLineBindData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_dashboard_event_calendar_active_year_term", con);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        GridViewTimeLine.DataSource = dt;
                        GridViewTimeLine.DataBind();
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

        protected void GridViewTimeLine_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewTimeLine.EditIndex = e.NewEditIndex;
            TimeLineBindData();
        }

        protected void GridViewTimeLine_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GridViewTimeLine.EditIndex = -1;
            TimeLineBindData();
        }

        protected void GridViewTimeLine_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ms_dashboard_event_calendar_edit", con);
            GridViewRow row = GridViewTimeLine.Rows[e.RowIndex] as GridViewRow;
            //TextBox tbDAY = row.FindControl("tbDay") as TextBox;
            //TextBox tbMONTH = row.FindControl("tbMonth") as TextBox;
            TextBox tbDATE = row.FindControl("tbDate") as TextBox;
            TextBox tbEVENT = row.FindControl("tbEvent") as TextBox;
            TextBox tbDESCRIPTION = row.FindControl("tbDescription") as TextBox;

            cmd.Parameters.Add(new SqlParameter() { ParameterName = "cal_id", Value = GridViewTimeLine.Rows[e.RowIndex].Cells[0].Text });
            //cmd.Parameters.Add(new SqlParameter() { ParameterName = "term_name", Value = GridViewFee.Rows[e.RowIndex].Cells[1].Text });
            //cmd.Parameters.Add(new SqlParameter() { ParameterName = "acad_year", Value = GridViewFee.Rows[e.RowIndex].Cells[2].Text });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "cal_date", Value = tbDATE.Text });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "event", Value = tbEVENT.Text });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "description", Value = tbDESCRIPTION.Text });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "updated_by", Value = HttpContext.Current.User.Identity.Name });
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            con.Close();
            GridViewTimeLine.EditIndex = -1;
            TimeLineBindData();
        }

        protected void GridViewTimeLine_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ms_dashboard_event_calendar_delete", con);
            GridViewRow row = GridViewTimeLine.Rows[e.RowIndex] as GridViewRow;
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "cal_id", Value = GridViewTimeLine.Rows[e.RowIndex].Cells[0].Text });
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            con.Close();
            TimeLineBindData();
        }

        public void DropDownYear()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_active_acad_year_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            try
            {
                con.Open();
                ddlAcademicYear.DataSource = cmd.ExecuteReader();
                ddlAcademicYear.DataTextField = "acad_year";
                ddlAcademicYear.DataValueField = "acad_year_id";
                ddlAcademicYear.DataBind();
                ddlAcademicYear.Items.Insert(0, new ListItem("Please select Year", ""));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlTerm.Items.Clear();
            ddlTerm.Items.Add(new ListItem("Please Select Term", ""));
            //ddlFeeSetupForm.Items.Clear();
            //ddlFeeSetupForm.Items.Add(new ListItem("Select Class", ""));

            ddlTerm.AppendDataBoundItems = true;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            String strQuery = "select ay_term_id, term_name from ms_acad_year_term " +
                      "where acad_year_id=@acad_year_id";

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@acad_year_id",
                 ddlAcademicYear.SelectedItem.Value);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strQuery;
            cmd.Connection = con;
            try
            {
                con.Open();
                ddlTerm.DataSource = cmd.ExecuteReader();
                ddlTerm.DataTextField = "term_name";
                ddlTerm.DataValueField = "ay_term_id";
                ddlTerm.DataBind();

                if (ddlTerm.Items.Count > 1)
                {
                    ddlTerm.Enabled = true;
                    
                }
                else
                {
                    ddlTerm.Enabled = false;
                    
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

      
    }
}