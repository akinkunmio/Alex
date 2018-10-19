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

namespace Alex.pages
{
    public partial class broadsheet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblZeroRecords.Text = "";
            GridViewListOfStudents.Visible = false;
            if (!Page.IsPostBack)
            {
                DropDownYear();
                DropDownTerm();
                DropDownFormClass();
                ddlFormClass.SelectedIndex = 1;
                BindGrid();
            }
        }

       
        public void DropDownFormClass()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_form_class_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                SqlDataReader ddlValues;
                ddlValues = cmd.ExecuteReader();

                ddlFormClass.DataSource = ddlValues;
                ddlFormClass.DataValueField = "form_class";
                ddlFormClass.DataTextField = "form_class";
                ddlFormClass.DataBind();
                ddlFormClass.Items.Insert(0, new ListItem("Please select Class"));
               
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


        private void BindGrid()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_assessment_broadsheet", con);
                cmd.Parameters.AddWithValue("@year", ddlAcademicYear.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@term", ddlTerm.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@class", ddlFormClass.SelectedItem.ToString());
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);

                        GridViewListOfStudents.DataSource = dt;
                        if (dt.Rows.Count == 0)
                        {
                            GridViewListOfStudents.Visible = false;
                            lblZeroRecords.Visible = true;
                            lblZeroRecords.Text = "No Records found ";
                        }
                        else
                        {
                            GridViewListOfStudents.Visible = true;
                            GridViewListOfStudents.DataBind();
                           
                            lblSelectedText.Text = "Broadsheet for " + ddlFormClass.SelectedItem.ToString()  +", " + ddlTerm.SelectedItem.ToString() + " Term " + ddlAcademicYear.SelectedItem.ToString();
                            lblZeroRecords.Visible = false;
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
        public void DropDownYear()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_acad_year_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
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
                ddlAcademicYear.Items.Insert(0, new ListItem("Please select Year", ""));
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

        //protected void GridViewListOfStudents_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        HyperLink hl = (HyperLink)e.Row.FindControl("HyperLinkPeople");
        //        if (hl != null)
        //        {
        //            DataRowView drv = (DataRowView)e.Row.DataItem;
        //            string keyword = drv["person_id"].ToString();
        //            hl.NavigateUrl = "~/pages/profile.aspx?PersonId=" + keyword;
        //        }
        //    }
        //}



        protected void GridViewListOfStudents_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewListOfStudents.PageIndex = e.NewPageIndex;
            //StudentsList();
            BindGrid();
        }

        protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownTerm();
            if (ddlTerm.Items.Count > 1)
            {
                ddlTerm.SelectedIndex = 1;
                BindGrid();
            }
        }

        protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ddlFormClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void GridViewListOfStudents_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Visible = false; 
                for (int i = 5; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Text = "<div class=\"headerText\">" + e.Row.Cells[i].Text + "</div>";
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Visible = false; 
                var PersonName = e.Row.Cells[2];
                var PersonId = e.Row.Cells[1];
                PersonName.Controls.Clear();
                PersonName.Controls.Add(new HyperLink { NavigateUrl = "~/pages/profile.aspx?PersonId=" + PersonId.Text + "&action=ast", Text = PersonName.Text });
            }
        }
    }
}