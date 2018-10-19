using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alex.App_code;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Configuration;

namespace Alex.pages
{
    public partial class batch_registrations_term : System.Web.UI.Page
    {
        int lvl = 0;
        string FormName = string.Empty;
        string ClassName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            Level();
            if (lvl == 1 || lvl == 2 || lvl == 3)
            {
                lblZeroRecords.Text = "";
                if (!Page.IsPostBack)
                {
                    DropDownBRFromYear();
                    DropDownBRTerm();
                    DropDownBRegYear();
                    DropDownBRegTerm();
                    BindGrid();
                }
            }
            else if (lvl == 4 || lvl == 5)
            {
                Response.Redirect("~/pages/ForbiddenAccess.aspx", false);
            }
            else
            {
                Response.Redirect("~/pages/logout.aspx", false);
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

        public void DropDownBRFromYear()
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

                ddlFromBRYear.DataSource = ddlValues;
                ddlFromBRYear.DataValueField = "acad_year";
                ddlFromBRYear.DataTextField = "acad_year";
                ddlFromBRYear.DataBind();
                ddlFromBRYear.Items.Insert(0, new ListItem("Please select Year", ""));
                ddlFromBRYear.SelectedValue = YearSelectedValue;
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

        public void DropDownBRegYear()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_acad_year_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();
            ddlYearBReg.DataSource = ddlValues;
            ddlYearBReg.DataValueField = "acad_year";
            ddlYearBReg.DataTextField = "acad_year";
            ddlYearBReg.DataBind();
            ddlYearBReg.Items.Insert(0, new ListItem("Please select year", ""));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
       



        public void DropDownBRTerm()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_term_dropdown_v2", con);
            cmd.Parameters.Add("@acad_year", SqlDbType.VarChar).Value = ddlFromBRYear.SelectedItem.ToString();
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
                ddlFromBRTerm.DataSource = ddlValues;
                ddlFromBRTerm.DataValueField = "term_name";
                ddlFromBRTerm.DataTextField = "term_name";
                ddlFromBRTerm.DataBind();
                ddlFromBRTerm.Items.Insert(0, new ListItem("Please select Term", ""));
                ddlFromBRTerm.SelectedValue = TermSelectedValue;

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

        public void DropDownBRegTerm()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_term_dropdown_v2", con);
            cmd.Parameters.Add("@acad_year", SqlDbType.VarChar).Value = ddlYearBReg.SelectedItem.ToString();
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
                ddlTermBReg.DataSource = ddlValues;
                ddlTermBReg.DataValueField = "term_name";
                ddlTermBReg.DataTextField = "term_name";
                ddlTermBReg.DataBind();
                ddlTermBReg.Items.Insert(0, new ListItem("Please select Term", ""));
                ddlTermBReg.SelectedValue = TermSelectedValue;

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

      

        public void FormClassNames(string FormClass)
        {
            SqlDataAdapter adp = new SqlDataAdapter();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_form_class_split_dropdown", con);
                cmd.Parameters.AddWithValue("@class_form", FormClass);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Dispose();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    FormName = dr["form_name"].ToString();
                    ClassName = dr["class_name"].ToString();

                }
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

        private void BindGrid()
        {
            //string FormClass = ddlClass.SelectedItem.ToString();
            ////string Class = FormClass.Split(' ')[1];
            ////string Form = FormClass.Split(' ')[0] + " " + Class.Substring(0,1);
            ////string Arm = Class.Substring(1, 1);
            //FormClassNames(FormClass);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_batch_reg_list_all_year_term", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@acad_year", ddlFromBRYear.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@term_name", ddlFromBRTerm.SelectedItem.ToString());
                   
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;

                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            GridViewBatchRegistrations.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                divProcessNow.Visible = false;
                                GridViewBatchRegistrations.Visible = false;
                                lblZeroRecords.Visible = true;
                                lblZeroRecords.Text = "No Records found ";
                            }
                            else
                            {
                                divProcessNow.Visible = true;
                                GridViewBatchRegistrations.Visible = true;
                                GridViewBatchRegistrations.DataBind();
                                lblZeroRecords.Visible = false;
                            }
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

       

        protected void GridViewBatchRegistrations_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hl = (HyperLink)e.Row.FindControl("HyperLinkPeople");
                if (hl != null)
                {
                    DataRowView drv = (DataRowView)e.Row.DataItem;
                    string keyword = drv["person_id"].ToString();
                    hl.NavigateUrl = "~/pages/profile.aspx?PersonId=" + keyword;
                }
            }
        }

      


        protected void GridViewBatchRegistrations_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewBatchRegistrations.PageIndex = e.NewPageIndex;
            BindGrid();

        }

        protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)GridViewBatchRegistrations.HeaderRow.FindControl("chkboxSelectAll");
            foreach (GridViewRow row in GridViewBatchRegistrations.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkStudent");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
        }

        protected void btnBatchRegistrations_Click(object sender, EventArgs e)
        {
            //string FormClass = ddlClassBReg.SelectedItem.ToString();
            ////string Class = FormClass.Split(' ')[1];
            ////string Form = FormClass.Split(' ')[0] + " " + Class.Substring(0, 1);
            ////string Arm = Class.Substring(1, 1); 
            //FormClassNames(FormClass);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                foreach (GridViewRow row in GridViewBatchRegistrations.Rows)
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_person_registration_add", con))
                    {
                        CheckBox ChkBoxHeader = (CheckBox)GridViewBatchRegistrations.HeaderRow.FindControl("chkboxSelectAll1");
                        //CheckBox ChkBoxHeader = (CheckBox)GridViewProcessApplications.FindControl("chkStudent"); 
                        string PerId = row.Cells[1].Text;
                        FormName = row.Cells[5].Text; 
                        ClassName = row.Cells[6].Text;
                        string BoarderType = row.Cells[9].Text;
                        CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkStudent");
                        if (ChkBoxRows.Checked)
                        {
                            cmd.Parameters.Add("@person_id", SqlDbType.NVarChar, 40).Value = PerId;
                            cmd.Parameters.Add("@app_id", SqlDbType.NVarChar, 40).Value = DBNull.Value;
                            cmd.Parameters.Add("@reg_date", SqlDbType.NVarChar, 40).Value = DateTime.Now.ToString();
                            cmd.Parameters.AddWithValue("@status", "Active");//ddlStatusBReg.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                            cmd.Parameters.AddWithValue("@acad_year", ddlYearBReg.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@term_name", ddlTermBReg.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@class_name", ClassName);
                            cmd.Parameters.AddWithValue("@form_name", FormName);
                            cmd.Parameters.AddWithValue("@from", "Insert");
                            cmd.Parameters.AddWithValue("@type_description", BoarderType);
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            int rowaffected = cmd.ExecuteNonQuery(); //this was missing.
                            con.Close();
                        }
                    }
                    using (SqlCommand cmd = new SqlCommand("sp_ms_batch_registration_update_status_completed", con))
                    {
                        CheckBox ChkBoxHeader = (CheckBox)GridViewBatchRegistrations.HeaderRow.FindControl("chkboxSelectAll1");
                        //CheckBox ChkBoxHeader = (CheckBox)GridViewProcessApplications.FindControl("chkStudent"); 
                        string RegId = row.Cells[2].Text;
                        CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkStudent");
                        if (ChkBoxRows.Checked)
                        {
                            cmd.Parameters.Add("@reg_id", SqlDbType.NVarChar, 40).Value = RegId;
                            cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            int rowaffected = cmd.ExecuteNonQuery(); //this was missing.
                            con.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Check Term,Class,Class Name either doesnot exists for Academic Year in settings or Cannot register for already registered students');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
                // ClearData(this);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Student(s) Registered Successfully');", true);
            }

            GridViewBatchRegistrations.Visible = false;
        }

        protected void ddlFromBRYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownBRTerm();
            ddlFromBRTerm.SelectedIndex = 1;
            BindGrid();

        }

        protected void ddlYearBReg_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownBRegTerm();
            ddlTermBReg.SelectedIndex = 0;
        }

        protected void ddlFromBRTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ddlForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

    }
}