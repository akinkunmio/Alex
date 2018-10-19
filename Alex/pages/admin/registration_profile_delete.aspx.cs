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
    public partial class registration_profile_delete : System.Web.UI.Page
    {
        int lvl = 0;
      
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            Level();
            if (lvl == 1 || lvl == 2 || lvl == 3)
            {
                lblZeroRecords.Text = "";
                if (!Page.IsPostBack)
                {
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


        protected void GvBatchPrOrReg_RowDataBound(object sender, GridViewRowEventArgs e)
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

        private void BindGrid()
        {
            //string FormClass = ddlClass.SelectedItem.ToString();
            //string Class = FormClass.Split(' ')[1];
            //string Form = FormClass.Split(' ')[0] + " " + Class.Substring(0, 1);
            //string Arm = Class.Substring(1, 1);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_registrations_with_no_payment_attendance_assessment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;

                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            GvBatchPrOrReg.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                divProcessNow.Visible = false;
                                GvBatchPrOrReg.Visible = false;
                                lblZeroRecords.Visible = true;
                                lblZeroRecords.Text = "No Records found ";
                            }
                            else
                            {
                                divProcessNow.Visible = true;
                                GvBatchPrOrReg.Visible = true;
                                GvBatchPrOrReg.DataBind();
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


        protected void GvBatchPrOrReg_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvBatchPrOrReg.PageIndex = e.NewPageIndex;
            //BindGrid("All");
            BindGrid();

        }

        protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)GvBatchPrOrReg.HeaderRow.FindControl("chkboxSelectAll");
            foreach (GridViewRow row in GvBatchPrOrReg.Rows)
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

        protected void btnProfOrRegDelete_Click(object sender, EventArgs e)
        {
            if (ddlRegOrProf.SelectedValue == "1") 
            { 
                DeleteRegistrations();
                BindGrid();
            } 
            else 
            {
                 DeleteProfiles();
                 BindGrid();
            }
        }


        private void DeleteRegistrations()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                foreach (GridViewRow row in GvBatchPrOrReg.Rows)
                {
                    string RegId = row.Cells[2].Text;
                    using (SqlCommand cmd = new SqlCommand("sp_ms_person_registration_delete", con))
                    {
                        CheckBox ChkBoxHeader = (CheckBox)GvBatchPrOrReg.HeaderRow.FindControl("chkboxSelectAll1");
                        //CheckBox ChkBoxHeader = (CheckBox)GridViewProcessApplications.FindControl("chkStudent"); 

                        CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkStudent");
                        if (ChkBoxRows.Checked)
                        {
                            cmd.Parameters.Add("@reg_id", SqlDbType.Int).Value = RegId;

                            cmd.Parameters.AddWithValue("@from", "Delete");
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            int rowaffected = cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }

                } ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Un-Registered Successfully');", true);
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
               
            }

            //GvBatchPrOrReg.Visible = false;
        }

        private void DeleteProfiles()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                foreach (GridViewRow row in GvBatchPrOrReg.Rows)
                {
                    string RegId = row.Cells[2].Text;
                    using (SqlCommand cmd = new SqlCommand("sp_ms_person_registration_delete", con))
                    {
                        CheckBox ChkBoxHeader = (CheckBox)GvBatchPrOrReg.HeaderRow.FindControl("chkboxSelectAll1");
                        //CheckBox ChkBoxHeader = (CheckBox)GridViewProcessApplications.FindControl("chkStudent"); 

                        CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkStudent");
                        if (ChkBoxRows.Checked)
                        {
                            cmd.Parameters.Add("@reg_id", SqlDbType.Int).Value = RegId;

                            cmd.Parameters.AddWithValue("@from", "Delete");
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            int rowaffected = cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    using (SqlCommand cmd = new SqlCommand("sp_ms_person_delete", con))
                    {
                        CheckBox ChkBoxHeader = (CheckBox)GvBatchPrOrReg.HeaderRow.FindControl("chkboxSelectAll1");
                        //CheckBox ChkBoxHeader = (CheckBox)GridViewProcessApplications.FindControl("chkStudent"); 
                        string PerId = row.Cells[1].Text;
                        CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkStudent");
                        if (ChkBoxRows.Checked)
                        {
                            cmd.Parameters.Add("@person_id", SqlDbType.NVarChar, 40).Value = PerId;
                            cmd.Parameters.AddWithValue("@from", "Delete");
                            con.Open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            int rowaffected = cmd.ExecuteNonQuery(); //this was missing.
                            con.Close();
                        }
                    }
                } ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Profile(s) Deleted Successfully');", true);
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
                
            }

           // GvBatchPrOrReg.Visible = false;
        }
    }
}