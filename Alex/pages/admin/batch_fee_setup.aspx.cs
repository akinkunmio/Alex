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

namespace Alex.pages.admin
{
    public partial class batch_fee_setup : System.Web.UI.Page
    {
        int lvl = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
             Level();
             if (lvl == 1)
             {
                 if (!Page.IsPostBack)
                 {
                     DropDownBatchFeeFromYear();
                     DropDownBatchFeeToYear();
                     DropDownBatchFeeFromTerm();
                     DropDownBatchFeeToTerm();
                 }
             }
             else if (lvl == 2 || lvl == 3 || lvl == 4)
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

        public void DropDownBatchFeeFromYear()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_acad_year_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

           ddlFromBatchFeeYear.DataSource = ddlValues;
           ddlFromBatchFeeYear.DataValueField = "acad_year";
           ddlFromBatchFeeYear.DataTextField = "acad_year";
           ddlFromBatchFeeYear.DataBind();

            //ddlAcademicYear.SelectedValue = yearFormat;
           ddlFromBatchFeeYear.Items.Insert(0, new ListItem("Please select year", ""));

            //Adding "Please select" option in dropdownlist for validation
            //ddlAcademicYear.Items.Insert(0, new ListItem("Please select a Year", "0"));

            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void DropDownBatchFeeToYear()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_acad_year_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();
            ddlToBatchFeeYear.DataSource = ddlValues;
            ddlToBatchFeeYear.DataValueField = "acad_year";
            ddlToBatchFeeYear.DataTextField = "acad_year";
            ddlToBatchFeeYear.DataBind();
            ddlToBatchFeeYear.Items.Insert(0, new ListItem("Please select year", ""));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void DropDownBatchFeeFromTerm()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_term_dropdown_v2", con);
            cmd.Parameters.Add("@acad_year", SqlDbType.VarChar).Value =ddlFromBatchFeeYear.SelectedItem.ToString();
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
               ddlFromBatchFeeTerm.DataSource = ddlValues;
               ddlFromBatchFeeTerm.DataValueField = "term_name";
               ddlFromBatchFeeTerm.DataTextField = "term_name";
               ddlFromBatchFeeTerm.DataBind();
               ddlFromBatchFeeTerm.Items.Insert(0, new ListItem("Please select Term", ""));
               ddlFromBatchFeeTerm.SelectedValue = TermSelectedValue;

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

        public void DropDownBatchFeeToTerm()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_term_dropdown_v2", con);
            cmd.Parameters.Add("@acad_year", SqlDbType.VarChar).Value = ddlToBatchFeeYear.SelectedItem.ToString();
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
                ddlToBatchFeeTerm.DataSource = ddlValues;
                ddlToBatchFeeTerm.DataValueField = "term_name";
                ddlToBatchFeeTerm.DataTextField = "term_name";
                ddlToBatchFeeTerm.DataBind();
                ddlToBatchFeeTerm.Items.Insert(0, new ListItem("Please select Term", ""));
                ddlToBatchFeeTerm.SelectedValue = TermSelectedValue;

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

        protected void ddlFromBatchFeeYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownBatchFeeFromTerm();
           ddlFromBatchFeeTerm.SelectedIndex = 0;
        }

        protected void ddlToBatchFeeYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownBatchFeeToTerm();
            ddlToBatchFeeTerm.SelectedIndex = 0;
        }

        protected void btnBatchFeeSetup_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_settings_fees_term_transfer", con))
                {

                    cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                    cmd.Parameters.AddWithValue("@acad_year_old",ddlFromBatchFeeYear.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@term_name_old",ddlFromBatchFeeTerm.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@acad_year_new", ddlToBatchFeeYear.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@term_name_new", ddlToBatchFeeTerm.SelectedItem.ToString());
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Fee Set Up Successfully');", true);
                    BatchfeeBindData();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            }
            finally
            {
                con.Close();
               // ClearData(this);
            }

        }


        protected void BatchfeeBindData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                //sp_ms_settings_fees_search_year_term
                SqlCommand cmd = new SqlCommand("sp_ms_settings_fees_search_year_term", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@acad_year",ddlFromBatchFeeYear.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@term_name",ddlFromBatchFeeTerm.SelectedItem.ToString());

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        GridViewBatchFee.DataSource = dt;
                        if (dt.Rows.Count == 0)
                        {
                           // lblZeroRecords.Visible = true;
                            //lblZeroRecords.Text = "No Fee found ";
                            GridViewBatchFee.Visible = false;
                        }
                        else
                        {
                            GridViewBatchFee.DataBind();
                            GridViewBatchFee.Visible = true;
                           // lblZeroRecords.Visible = false;
                        }


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

    }
}