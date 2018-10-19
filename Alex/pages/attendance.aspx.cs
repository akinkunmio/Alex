using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Configuration;
using System.Globalization;
//using Alex.App_code;
using System.Drawing;

namespace Alex.pages
{
    public partial class attendance : System.Web.UI.Page
    {
       // private static string currentYear = DateTime.Now.Year.ToString();
      //  private static string currentmonth = DateTime.Now.ToString("MMMM");
       
       protected void Page_Load(object sender, EventArgs e)
        {
            //ManageCookies.VerifyAuthentication();
            lblZeroRecords.Text = "";

            if (!Page.IsPostBack)
            {
               // DropDownAttendanceYear();
               // ddlYear.SelectedValue = currentYear;
              //  DropDownAttendanceMonth();
               // ddlMonth.SelectedValue = currentmonth;
                DropDownFormClass();
                ddlFormClass.SelectedIndex = 1;
                BindGridColor();
               // btnEdit.Visible = false;
                int y = DateTimeExtensions.WeekNumber();
                DropDownWeekNumber();
                ddlWeek.SelectedValue = y.ToString();
                BindGrid();
                
            }
        }

       public static class DateTimeExtensions
       {
           public static int WeekNumber()
           {
               //var  value = System.Data.DbType.DateTime;
               var value = DateTime.Today;
               int Week = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
               return Week;
           }
       }
        private void BindGridColor()
        {
            foreach (GridViewRow row in GridViewAttendance.Rows)
            {
                //int i = 0;
                string text = "";
                for (int i = 1; i <= 5; i++)
                {
                    text = "dd" + i;
                    string Droptest = ((DropDownList)row.FindControl(text)).Text;
                    //P -- Green (Present)
                    //L -- Yellow (Late)
                    //A -- Red (Absent)
                    //N -- White (Not Applicable)
                    if (Droptest == "P")
                    {
                        ((DropDownList)row.FindControl(text)).BackColor = Color.Green;
                        ((DropDownList)row.FindControl(text)).ForeColor = Color.White;
                    }
                    if (Droptest == "L")
                    {
                        ((DropDownList)row.FindControl(text)).BackColor = Color.FromArgb(0xFF8200);
                        ((DropDownList)row.FindControl(text)).ForeColor = Color.White;
                    }
                    if (Droptest == "A")
                    {
                        ((DropDownList)row.FindControl(text)).BackColor = Color.Red;
                        ((DropDownList)row.FindControl(text)).ForeColor = Color.White;
                    }
                    if (Droptest == "H")
                    {
                        ((DropDownList)row.FindControl(text)).BackColor = Color.FromArgb(0x0000FF);
                        ((DropDownList)row.FindControl(text)).ForeColor = Color.White;
                    }

                    //ddlMultiColor.BackColor = Color.FromName(ddlMultiColor.SelectedItem.Text);
                }
            }
        }

        //private void DropDownListColorBind()
        //{
        //    //foreach (GridViewRow row in GridViewAttendance.Rows)
        //    {
        //       // var textbox10 = row.FindControl("dd1") as DropDownList;

        //        //dd.Attributes.Add("style", "background-color:" + "Red");  
        //        //row.Attributes.Add("style", "background-color:" + "Red");

        //        for (int row = 0; row < ddlMultiColor.Items.Count - 1; row++)
        //        {
        //            //string a = dd.Items[row].Value;
        //            ddlMultiColor.Items[row].Attributes.Add("style", "background-color:" + ddlMultiColor.Items[row].Value);
        //        }
        //        ddlMultiColor.BackColor = Color.FromName(ddlMultiColor.SelectedItem.Value);


        //    }

        //}

        public void DropDownFormClass()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_form_class_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlFormClass.DataSource = ddlValues;
            ddlFormClass.DataValueField = "form_class";
            ddlFormClass.DataTextField = "form_class";
            ddlFormClass.DataBind();
            ddlFormClass.Items.Insert(0, new ListItem("Please select Class", ""));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        //public void DropDownAttendanceYear()
        //{
        //    ddlYear.Items.Clear();
        //    ddlYear.Items.Add(new ListItem("Please select Year", ""));
        //    ddlYear.AppendDataBoundItems = true;
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        //    SqlCommand cmd = new SqlCommand("sp_ms_attendance_year_dropdown", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    try
        //    {
        //        con.Open();
        //        SqlDataReader ddlValues;
        //        ddlValues = cmd.ExecuteReader();
        //        ddlYear.DataSource = ddlValues;
        //        ddlYear.DataValueField = "year";
        //        ddlYear.DataTextField = "year";
        //        ddlYear.DataBind();
        //     }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //    }
        //    finally
        //    {
        //        con.Close();
        //        con.Dispose();
        //    }
        //}

        //public void DropDownAttendanceMonth()
        //{
        //    ddlMonth.Items.Clear();
        //    ddlMonth.Items.Add(new ListItem("Please Select Month", ""));
        //    ddlMonth.AppendDataBoundItems = true;
        //    GridViewAttendance.Visible = false;
        //    btnEdit.Visible = false;
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        //    SqlCommand cmd = new SqlCommand("sp_ms_attendance_month_dropdown", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@year", ddlYear.SelectedItem.ToString());
        //    try
        //    {
        //        con.Open();
        //        SqlDataReader ddlValues;
        //        ddlValues = cmd.ExecuteReader();
        //        ddlMonth.DataSource = ddlValues;
        //        ddlMonth.DataValueField = "month";
        //        ddlMonth.DataTextField = "month";
        //        ddlMonth.DataBind();
        //        //if (ddlMonth.Items.Count > 1)
        //        //{
        //        //    ddlMonth.Enabled = true;
        //        //    btnSearchFormClass.Enabled = true;

        //        //}
        //        //else
        //        //{
        //        //    ddlSubject.Items.Remove(new ListItem("Please Select Subject", ""));
        //        //    ddlSubject.Items.Add(new ListItem("No Subject(s) Added", ""));
        //        //    divEdit.Visible = false;
        //        //    GridViewAssessments.Visible = false;
        //        //    ddlSubject.Enabled = false;
        //        //    btnSearchFormClass.Enabled = false;

        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //    }
        //    finally
        //    {
        //        con.Close();
        //        con.Dispose();
        //    }
        //}

        public void DropDownWeekNumber()
        {
            ddlWeek.Items.Clear();
            ddlWeek.Items.Add(new ListItem("Please Select Week no", ""));
            ddlWeek.AppendDataBoundItems = true;
            GridViewAttendance.Visible = false;
            btnEdit.Visible = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_attendance_weekno_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
           // cmd.Parameters.AddWithValue("@month", ddlMonth.SelectedItem.ToString());
            try
            {
                con.Open();
                SqlDataReader ddlValues;
                ddlValues = cmd.ExecuteReader();
                ddlWeek.DataSource = ddlValues;
                ddlWeek.DataValueField = "weekno";
                ddlWeek.DataTextField = "weekno_date";
                ddlWeek.DataBind();
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
        protected void GridViewAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hl = (HyperLink)e.Row.FindControl("HyperLinkPeople");
                if (hl != null)
                {
                    DataRowView drv = (DataRowView)e.Row.DataItem;
                    string keyword = drv["person_id"].ToString();
                    string regid = drv["reg_id"].ToString();
                    hl.NavigateUrl = "~/pages/profile.aspx?PersonId=" + keyword + "&Regid=" + regid + "&action=att";
                }


            }
        }


        private void BindGrid()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_attendance_class_monthly_list_all", con))
                //using (SqlCommand cmd = new SqlCommand("sp_ms_back", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                     //   cmd.Parameters.AddWithValue("@year", ddlYear.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@weekno", ddlWeek.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@class", ddlFormClass.SelectedItem.ToString());
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridViewAttendance.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                GridViewAttendance.Visible = false;
                                lblZeroRecords.Visible = true;
                                divEditUpdate.Visible = false;
                                lblZeroRecords.Text = "No Records found ";
                            }
                            else
                            {
                                GridViewAttendance.Visible = true;
                                GridViewAttendance.DataBind();
                                BindGridColor();
                                //ddlMultiColor.BackColor = Color.FromName(ddlMultiColor.SelectedItem.Text);
                                lblZeroRecords.Visible = false;
                                divEditUpdate.Visible = true;
                                btnEdit.Visible = true;

                            }
                        }
                    }
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
        //protected void btnFilterAttendance_Click(object sender, EventArgs e)
        //{
        //    divEditRow.Visible = false;
        //    btnCancel.Visible = false;
        //    btnEdit.Visible = true;
        //    BindGrid();
        //}

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            divEditRow.Visible = true;
            btnCancel.Visible = true;
            btnEdit.Visible = false;
            btnUpdate.Visible = true;

            foreach (GridViewRow row in GridViewAttendance.Rows)
            {
                //int i = 0;
                string text = "";
                for (int i = 1; i <= 5; i++)
                {
                    text = "dd" + i;
                    ((DropDownList)row.FindControl(text)).Enabled = true;
                    ((DropDownList)row.FindControl(text)).BackColor = Color.White;
                    ((DropDownList)row.FindControl(text)).ForeColor = Color.Black;
                }
            }

            //  gdvauthors.EditIndex = 3;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                foreach (GridViewRow row in GridViewAttendance.Rows)
                {


                    //TextBox txtScoreInfo = (TextBox)GridViewAssessments.FindControl("tbScore");
                    //string txtScore = txtScoreInfo.Text;

                    //var a = row.FindControl("tbScore");

                    var textbox1 = row.FindControl("dd1") as DropDownList;
                    var txtScore1 = textbox1.Text;
                    var textbox2 = row.FindControl("dd2") as DropDownList;
                    var txtScore2 = textbox2.Text;
                    var textbox3 = row.FindControl("dd3") as DropDownList;
                    var txtScore3 = textbox3.Text;
                    var textbox4 = row.FindControl("dd4") as DropDownList;
                    var txtScore4 = textbox4.Text;
                    var textbox5 = row.FindControl("dd5") as DropDownList;
                    var txtScore5 = textbox5.Text;
                    //var textbox6 = row.FindControl("dd6") as DropDownList;
                    //var txtScore6 = textbox6.Text;
                    //var textbox7 = row.FindControl("dd7") as DropDownList;
                    //var txtScore7 = textbox7.Text;
                    //var textbox8 = row.FindControl("dd8") as DropDownList;
                    //var txtScore8 = textbox8.Text;
                    //var textbox9 = row.FindControl("dd9") as DropDownList;
                    //var txtScore9 = textbox9.Text;

                    //var textbox10 = row.FindControl("dd10") as DropDownList;
                    //var txtScore10 = textbox10.Text;
                    //var textbox11 = row.FindControl("dd11") as DropDownList;
                    //var txtScore11 = textbox11.Text;
                    //var textbox12 = row.FindControl("dd12") as DropDownList;
                    //var txtScore12 = textbox12.Text;
                    //var textbox13 = row.FindControl("dd13") as DropDownList;
                    //var txtScore13 = textbox13.Text;
                    //var textbox14 = row.FindControl("dd14") as DropDownList;
                    //var txtScore14 = textbox14.Text;
                    //var textbox15 = row.FindControl("dd15") as DropDownList;
                    //var txtScore15 = textbox15.Text;
                    //var textbox16 = row.FindControl("dd16") as DropDownList;
                    //var txtScore16 = textbox16.Text;
                    //var textbox17 = row.FindControl("dd17") as DropDownList;
                    //var txtScore17 = textbox17.Text;
                    //var textbox18 = row.FindControl("dd18") as DropDownList;
                    //var txtScore18 = textbox18.Text;
                    //var textbox19 = row.FindControl("dd19") as DropDownList;
                    //var txtScore19 = textbox19.Text;
                    //var textbox20 = row.FindControl("dd20") as DropDownList;
                    //var txtScore20 = textbox20.Text;
                    //var textbox21 = row.FindControl("dd21") as DropDownList;
                    //var txtScore21 = textbox21.Text;
                    //var textbox22 = row.FindControl("dd22") as DropDownList;
                    //var txtScore22 = textbox22.Text;
                    //var textbox23 = row.FindControl("dd23") as DropDownList;
                    //var txtScore23 = textbox23.Text;
                    //var textbox24 = row.FindControl("dd24") as DropDownList;
                    //var txtScore24 = textbox24.Text;
                    //var textbox25 = row.FindControl("dd25") as DropDownList;
                    //var txtScore25 = textbox25.Text;
                    //var textbox26 = row.FindControl("dd26") as DropDownList;
                    //var txtScore26 = textbox26.Text;
                    //var textbox27 = row.FindControl("dd27") as DropDownList;
                    //var txtScore27 = textbox27.Text;
                    //var textbox28 = row.FindControl("dd28") as DropDownList;
                    //var txtScore28 = textbox28.Text;
                    //var textbox29 = row.FindControl("dd29") as DropDownList;
                    //var txtScore29 = textbox29.Text;
                    //var textbox30 = row.FindControl("dd30") as DropDownList;
                    //var txtScore30 = textbox30.Text;
                    //var textbox31 = row.FindControl("dd31") as DropDownList;
                    //var txtScore31 = textbox31.Text;

                    //var txtScore = textbox1.Text;

                    using (SqlCommand cmd = new SqlCommand("sp_ms_attendance_class_monthly_update", con))
                    {


                        string RegId = row.Cells[0].Text;
                        // string txtScore = row.Cells[3].Text;


                        cmd.Parameters.Add("@reg_id", SqlDbType.NVarChar, 40).Value = RegId;
                       // cmd.Parameters.AddWithValue("@year", ddlYear.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@weekno", ddlWeek.SelectedValue.ToString());
                        cmd.Parameters.Add("@1", SqlDbType.NVarChar, 40).Value = txtScore1;
                        cmd.Parameters.Add("@2", SqlDbType.NVarChar, 40).Value = txtScore2;
                        cmd.Parameters.Add("@3", SqlDbType.NVarChar, 40).Value = txtScore3;
                        cmd.Parameters.Add("@4", SqlDbType.NVarChar, 40).Value = txtScore4;
                        cmd.Parameters.Add("@5", SqlDbType.NVarChar, 40).Value = txtScore5;
                        cmd.Parameters.Add("@6", SqlDbType.NVarChar, 40).Value = DBNull.Value;//txtScore6;
                        cmd.Parameters.Add("@7", SqlDbType.NVarChar, 40).Value = DBNull.Value; //txtScore7;
                        cmd.Parameters.Add("@8", SqlDbType.NVarChar, 40).Value = DBNull.Value;//txtScore8;
                        cmd.Parameters.Add("@9", SqlDbType.NVarChar, 40).Value = DBNull.Value;//txtScore9;
                        cmd.Parameters.Add("@10", SqlDbType.NVarChar, 40).Value = DBNull.Value;// txtScore10;
                        cmd.Parameters.Add("@11", SqlDbType.NVarChar, 40).Value = DBNull.Value;//txtScore11;
                        cmd.Parameters.Add("@12", SqlDbType.NVarChar, 40).Value = DBNull.Value;//txtScore12;
                        cmd.Parameters.Add("@13", SqlDbType.NVarChar, 40).Value = DBNull.Value;//txtScore13;
                        cmd.Parameters.Add("@14", SqlDbType.NVarChar, 40).Value = DBNull.Value;//txtScore14;
                        cmd.Parameters.Add("@15", SqlDbType.NVarChar, 40).Value = DBNull.Value;//txtScore15;
                        cmd.Parameters.Add("@16", SqlDbType.NVarChar, 40).Value = DBNull.Value;//txtScore16;
                        cmd.Parameters.Add("@17", SqlDbType.NVarChar, 40).Value = DBNull.Value;//txtScore17;
                        cmd.Parameters.Add("@18", SqlDbType.NVarChar, 40).Value = DBNull.Value;//txtScore18;
                        cmd.Parameters.Add("@19", SqlDbType.NVarChar, 40).Value = DBNull.Value;//txtScore19;
                        cmd.Parameters.Add("@20", SqlDbType.NVarChar, 40).Value = DBNull.Value;//txtScore20;
                        cmd.Parameters.Add("@21", SqlDbType.NVarChar, 40).Value = DBNull.Value;//txtScore21;
                        cmd.Parameters.Add("@22", SqlDbType.NVarChar, 40).Value = DBNull.Value;//txtScore22;
                        cmd.Parameters.Add("@23", SqlDbType.NVarChar, 40).Value = DBNull.Value;//txtScore23;
                        cmd.Parameters.Add("@24", SqlDbType.NVarChar, 40).Value = DBNull.Value;//txtScore24;
                        cmd.Parameters.Add("@25", SqlDbType.NVarChar, 40).Value = DBNull.Value;//txtScore25;
                        cmd.Parameters.Add("@26", SqlDbType.NVarChar, 40).Value = DBNull.Value;//txtScore26;
                        cmd.Parameters.Add("@27", SqlDbType.NVarChar, 40).Value = DBNull.Value;//txtScore27;
                        cmd.Parameters.Add("@28", SqlDbType.NVarChar, 40).Value = DBNull.Value;//txtScore28;
                        cmd.Parameters.Add("@29", SqlDbType.NVarChar, 40).Value = DBNull.Value;//txtScore29;
                        cmd.Parameters.Add("@30", SqlDbType.NVarChar, 40).Value = DBNull.Value;//txtScore30;
                        cmd.Parameters.Add("@31", SqlDbType.NVarChar, 40).Value = DBNull.Value;//txtScore31;
                        cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        int rowaffected = cmd.ExecuteNonQuery(); //this was missing.
                        con.Close();
                        divEditRow.Visible = false;
                        btnCancel.Visible = false;
                        btnEdit.Visible = true;
                        btnUpdate.Visible = false;
                        BindGrid();

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

                //Response.Redirect("~/pages/attendance.aspx");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Attendance uploaded Successfully');", true);
            }

        }


        protected void btnFill_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in GridViewAttendance.Rows)
                {

                    {
                        string Status = ddlOnTopStatus.SelectedValue.ToString();
                        string Day = ddlOnTopDay.SelectedValue.ToString();
                        int DayLop = 0;
                        //string textbox1 = GridViewAttendance.Rows[0].Cells[3].ToString();
                        //Status = textbox1;
                        // String Monday =row.Cells[3].Text;
                        //GridViewAttendance.Rows[0].Cells[3] = ddlOnTopStatus.SelectedValue

                        //int GridRowCount = row.Cells.Count;
                        //List<string> a = new List<string>();
                        switch (Day)
                        {
                            case "M":
                                DayLop = 1;
                                break;
                            case "T":
                                DayLop = 2;
                                break;
                            case "W":
                                DayLop = 3;
                                break;
                            case "TH":
                                DayLop = 4;
                                break;
                            case "F":
                                DayLop = 5;
                                break;

                        }

                        for (int i = DayLop; i <= DayLop; i++)
                        {
                            DropDownList textbox1 = row.FindControl("dd" + i) as DropDownList;

                            //string aa = textbox1.Text.ToString();
                            textbox1.SelectedValue = Status;



                            //a.Add(row.Cells[i].Text.ToString());
                            ////a.Add(row.ID.ToString());
                            //row.Cells[i].Text = Status;
                        }
                    }

                }
            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);

            }
            finally
            {

            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            BindGrid();
            btnCancel.Visible = false;
            btnEdit.Visible = true;
            divEditRow.Visible = false;
            btnUpdate.Visible = false;
        }

        protected void ddlFormClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            divEditRow.Visible = false;
            btnCancel.Visible = false;
            btnEdit.Visible = true;
            BindGrid();
        }

        protected void ddlWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            divEditRow.Visible = false;
            btnCancel.Visible = false;
            btnEdit.Visible = true;
            BindGrid();
        }

        //protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DropDownAttendanceMonth();
        //}

        //protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DropDownWeekNumber();
        //}

    }
}