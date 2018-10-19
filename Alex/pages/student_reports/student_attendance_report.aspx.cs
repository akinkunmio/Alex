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
namespace Alex.pages.student_reports
{
    public partial class student_attendance_report : System.Web.UI.Page
    {
        int lvl = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Level();
            if (lvl == 1 || lvl == 2 || lvl == 3)
            {
                if (!Page.IsPostBack)
                {
                   
                    DropDownFormClass();
                    ddlClass.SelectedIndex = 1;
                    SchoolBind();
                   // AttendnaceStatus();
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
       
      
        public void DropDownFormClass()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_form_class_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlClass.DataSource = ddlValues;
            ddlClass.DataValueField = "form_class";
            ddlClass.DataTextField = "form_class";
            ddlClass.DataBind();
            ddlClass.Items.Insert(0, new ListItem("Please select Class", ""));
            ddlClass.Items.Insert(1, new ListItem("All", "All"));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
        
        private void AttendnaceStatus()
        {
            string DateYear = tbAttMonth.Text;
            string Year = string.Empty;
            string Month = string.Empty;
            if (!string.IsNullOrEmpty(DateYear)) { 
            Year = DateYear.Split(' ')[1];
            Month = DateYear.Split(' ')[0];
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_attendance_report", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@att_status", SqlDbType.VarChar).Value = ddlStatus.SelectedItem.ToString();
                    cmd.Parameters.Add("@class", SqlDbType.VarChar).Value = ddlClass.SelectedItem.ToString();
                    cmd.Parameters.AddWithValue("@att_date", tbAttDate.Text);
                    cmd.Parameters.AddWithValue("@year",Year);
                    cmd.Parameters.AddWithValue("@month", Month);
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {

                            sda.Fill(dt);
                            GridViewAttendance.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {

                                lblZeroStudents.Visible = true;
                                lblZeroStudents.Text = "No Attendance records found ";
                                DivStudents.Visible = false;
                                GridViewAttendance.Visible = false;
                                btnPrint.Visible = false;
                            }
                            else
                            {
                                DivStudents.Visible = true;
                                lblZeroStudents.Visible = false;

                                imgSchool.ImageUrl = "~/pages/ImageHandler.ashx?roll_no=1";
                                lblStatusSelected.Text = ddlStatus.SelectedItem.ToString();
                                lblClassSelected.Text = ddlClass.SelectedItem.ToString();
                                GridViewAttendance.DataBind();
                                GridViewAttendance.Visible = true;
                                btnPrint.Visible = true;
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

        public void SchoolBind()
        {
            SqlDataAdapter adp = new SqlDataAdapter();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select school_name from ms_school", con);
                cmd.Dispose();
                SqlDataReader dr = cmd.ExecuteReader();
                string SchoolName = string.Empty;
                while (dr.Read())
                {
                    SchoolName = dr["school_name"].ToString();
                    lblName.Text = SchoolName.ToString();
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

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            AttendnaceStatus();

        }

       

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            AttendnaceStatus();
        }

        protected void tbAttDate_TextChanged(object sender, EventArgs e)
        {
            AttendnaceStatus();
        }

        protected void tbAttMonth_TextChanged(object sender, EventArgs e)
        {
            AttendnaceStatus();
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
        protected void ddlDateOrMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDateOrMonth.SelectedValue == "date")
            {
                ClearData(this);
                GridViewAttendance.Visible = false;
                DivDate.Visible = true;
                tbAttDate.Visible = true;
                DivMonth.Visible = false;
                tbAttMonth.Visible = false;
                RowFields.Visible = true;
            }
            else
            {
                ClearData(this);
                GridViewAttendance.Visible = false;
                DivDate.Visible = false;
                tbAttDate.Visible = false;
                DivMonth.Visible = true;
                tbAttMonth.Visible = true;
                RowFields.Visible = true;
            }
        }

       
    }
}