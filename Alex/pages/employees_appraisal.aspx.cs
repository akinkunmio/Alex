using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Configuration;
//using Alex.App_code;

namespace Alex.pages
{
    public partial class employees_appraisal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DropDownYear();
                DropDownTerm();
                ddlApprisal.SelectedIndex = 1;
                AppraisalListBindData();
                btnUploadAppraosal.Visible = false;
            }
        }


        public void DropDownYear()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_acad_year_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            try
            {
                SqlDataReader ddlValues;
                ddlValues = cmd.ExecuteReader();
                string YearSelectedValue = null;

                while (ddlValues.Read())
                {
                    YearSelectedValue = ddlValues[0].ToString();
                    int DefaultValue = Convert.ToInt32(ddlValues[1]);
                    if (DefaultValue == 1)
                        break;
                }
                ddlValues.Close();
                ddlValues = cmd.ExecuteReader();

                ddlAcademicYear.DataSource = ddlValues;
                ddlAcademicYear.DataValueField = "acad_year";
                ddlAcademicYear.DataTextField = "acad_year";
                ddlAcademicYear.DataBind();
                ddlAcademicYear.Items.Insert(0, new ListItem("Select Academic Year", ""));
                ddlAcademicYear.SelectedValue = YearSelectedValue;
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

        public void DropDownTerm()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_term_dropdown_v2", con);
            cmd.Parameters.Add("@acad_year", SqlDbType.VarChar).Value = ddlAcademicYear.SelectedItem.ToString();
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                SqlDataReader ddlValues;
                ddlValues = cmd.ExecuteReader();
                string TermSelectedValue = null;
                while (ddlValues.Read())
                {
                    TermSelectedValue = ddlValues[0].ToString();
                    int DefaultValue = Convert.ToInt32(ddlValues[1]);
                    if (DefaultValue == 1)
                        break;
                }
                ddlValues.Close();
                ddlValues = cmd.ExecuteReader();
                ddlTerm.DataSource = ddlValues;
                ddlTerm.DataValueField = "term_name";
                ddlTerm.DataTextField = "term_name";
                ddlTerm.DataBind();
                ddlTerm.Items.Insert(0, new ListItem("Please select Term", ""));
                ddlTerm.SelectedValue = TermSelectedValue;

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



        protected void AppraisalListBindData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_hr_performance_appraisal_list_all_year_term", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@acad_year", ddlAcademicYear.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@term_name", ddlTerm.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@appraisal_no", ddlApprisal.SelectedItem.ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        GridViewAppraisal.DataSource = dt;
                        if (dt.Rows.Count == 0)
                        {
                            GridViewAppraisal.Visible = false;
                            lblZeroRecords.Visible = true;
                            lblZeroRecords.Text = "No Records found ";
                            divEditUpdate.Visible = false;
                        }
                        else
                        {
                            GridViewAppraisal.Visible = true;
                            GridViewAppraisal.DataBind();
                            lblZeroRecords.Visible = false;
                            divEditUpdate.Visible = true;
                            btnEdit.Visible = true;
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

        protected void ddlApprisal_SelectedIndexChanged(object sender, EventArgs e)
        {
            AppraisalListBindData();
        }

        protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            AppraisalListBindData();
        }

        protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            AppraisalListBindData();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
           
            btnCancel.Visible = true;
            btnEdit.Visible = false;
            btnUploadAppraosal.Visible = true;

            foreach (GridViewRow row in GridViewAppraisal.Rows)
            {
                ((TextBox)row.FindControl("tbNotes")).Enabled = true;
                ((DropDownList)row.FindControl("ddlRating")).Enabled = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            AppraisalListBindData();
            btnCancel.Visible = false;
            btnEdit.Visible = true;
            btnUploadAppraosal.Visible = false;
        }

        protected void btnUploadAppraosal_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                foreach (GridViewRow row in GridViewAppraisal.Rows)
                {
                    var txtNotes = row.FindControl("tbNotes") as TextBox;
                    var ddlRating = row.FindControl("ddlRating") as DropDownList;
                    var txtRating = ddlRating.Text;
                   
                    using (SqlCommand cmd = new SqlCommand("sp_ms_hr_performance_appraisal_edit", con))
                    {
                        con.Open();
                        string PerApp_id = row.Cells[0].Text;
                        cmd.Parameters.AddWithValue("@pa_id", PerApp_id);
                        cmd.Parameters.AddWithValue("@notes", txtNotes.Text);
                        if (!string.IsNullOrWhiteSpace(txtRating))
                        {
                            cmd.Parameters.AddWithValue("@rating", txtRating);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@rating", DBNull.Value);
                        }
                     
                        cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                btnCancel.Visible = false;
                btnEdit.Visible = true;
                btnUploadAppraosal.Visible = false;
                AppraisalListBindData();
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

        protected void GridViewAppraisal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hl = (HyperLink)e.Row.FindControl("HyperLinkEmployee");
                if (hl != null)
                {
                    DataRowView drv = (DataRowView)e.Row.DataItem;
                    string keyword = drv["emp_id"].ToString();

                    hl.NavigateUrl = "~/pages/employee_profile.aspx?EmployeeId=" + keyword;
                }
            }
        }
    }
}