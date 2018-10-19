using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Alex.pages.student_reports
{
    public partial class report_cards_comments : System.Web.UI.Page
    {
        string FindClassSection = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownYear();
                DropDownTerm();
                DropDownFormClass();

                //ddlTerm.SelectedIndex = 1;
                ddlFormClass.SelectedIndex = 1;
                FindSection();
                if (lblFindClassSection.Text == "Senior Secondary" || lblFindClassSection.Text == "Junior Secondary" || lblFindClassSection.Text == "Primary") { Info(); }
                else
                {
                    NurseryReportBind();
                }

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
                    TermSelectedValue = ddlValues[2].ToString();
                    int DefaultValue = Convert.ToInt32(ddlValues[1]);
                    if (DefaultValue == 1)
                        break;
                }
                ddlValues.Close();
                ddlValues = cmd.ExecuteReader();
                ddlTerm.DataSource = ddlValues;
                ddlTerm.DataValueField = "ay_term_id";
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

        public void FindSection()
        {
            SqlDataAdapter adp = new SqlDataAdapter();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_find_class_section", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@class", ddlFormClass.SelectedItem.Text);
                cmd.Dispose();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    FindClassSection = dr["section2"].ToString();
                    lblFindClassSection.Text = FindClassSection;
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
        private MyStuDataSet GetData()
        {

            MyStuDataSet dt = new MyStuDataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_person_assessment_breakdown_v4", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ay_term_id", ddlTerm.SelectedValue);
                        cmd.Parameters.AddWithValue("@class", ddlFormClass.SelectedItem.Text);
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            dt.sp_ms_person_assessment_breakdown_v4.Clear();
                            dt.EnforceConstraints = false;
                            sda.Fill(dt.Tables["sp_ms_person_assessment_breakdown_v4"]);
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
            return dt;
        }


        private MyStuDataSet GetAssessement2Data()
        {
            MyStuDataSet dt = new MyStuDataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_profile_assessment2_list_all_v4", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ay_term_id", ddlTerm.SelectedValue);
                        cmd.Parameters.AddWithValue("@class", ddlFormClass.SelectedItem.Text);
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            dt.sp_ms_profile_assessment2_list_all_v4.Clear();
                            dt.EnforceConstraints = false;
                            sda.Fill(dt.Tables["sp_ms_profile_assessment2_list_all_v4"]);
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
            return dt;
        }


        private MyStuDataSet GetAttendanceData()
        {
            MyStuDataSet dt = new MyStuDataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_person_attendance_summary_v4", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ay_term_id", ddlTerm.SelectedValue);
                        cmd.Parameters.AddWithValue("@class", ddlFormClass.SelectedItem.Text);
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            dt.sp_ms_person_attendance_summary_v4.Clear();
                            dt.EnforceConstraints = false;
                            sda.Fill(dt.Tables["sp_ms_person_attendance_summary_v4"]);
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
            return dt;
        }

        private MyStuDataSet GetTotalsData()
        {
            MyStuDataSet dt = new MyStuDataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_person_assessment_summary_v4", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ay_term_id", ddlTerm.SelectedValue);
                        cmd.Parameters.AddWithValue("@class", ddlFormClass.SelectedItem.Text);
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            dt.sp_ms_person_assessment_summary_v4.Clear();
                            dt.EnforceConstraints = false;
                            sda.Fill(dt.Tables["sp_ms_person_assessment_summary_v4"]);
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
            return dt;
        }

        private MyStuDataSet GetGradeLegend()
        {
            MyStuDataSet dt = new MyStuDataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_grade_legend", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ay_term_id", ddlTerm.SelectedValue);
                        cmd.Parameters.AddWithValue("@class", ddlFormClass.SelectedItem.Text);
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            dt.sp_ms_grade_legend.Clear();
                            dt.EnforceConstraints = false;
                            sda.Fill(dt.Tables["sp_ms_grade_legend"]);
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
            return dt;
        }

        private MyStuDataSet GetNurseryRatings()
        {
            MyStuDataSet dt = new MyStuDataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_person_report_card_nursery_assessment_all_v4", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ay_term_id", ddlTerm.SelectedValue);
                        cmd.Parameters.AddWithValue("@class", ddlFormClass.SelectedItem.Text);
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            dt.sp_ms_person_report_card_nursery_assessment_all_v4.Clear();
                            dt.EnforceConstraints = false;
                            sda.Fill(dt.Tables["sp_ms_person_report_card_nursery_assessment_all_v4"]);
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
            return dt;
        }


        private void Info()
        {
            RptViewerRptCard.ProcessingMode = ProcessingMode.Local;
            RptViewerRptCard.LocalReport.ReportPath = Server.MapPath("~/Report/ReportCards_Students_Comments.rdlc");
            MyStuDataSet dsAssessment1 = GetData();
            MyStuDataSet dsAssessment2 = GetAssessement2Data();
            MyStuDataSet dsAttendance = GetAttendanceData();
            MyStuDataSet dsTotals = GetTotalsData();
            MyStuDataSet dsGrade = GetGradeLegend();
            ReportDataSource datasource = new ReportDataSource("SubjectsDataSet", dsAssessment1.Tables[6]);
            ReportDataSource datasource2 = new ReportDataSource("AsDataset", dsAssessment2.Tables[7]);
            ReportDataSource datasource3 = new ReportDataSource("AttendanceDataSet", dsAttendance.Tables[8]);
            ReportDataSource datasource4 = new ReportDataSource("TotalsDataSet", dsTotals.Tables[9]);
            ReportDataSource datasource5 = new ReportDataSource("GradeDataSet", dsGrade.Tables[10]);
            RptViewerRptCard.LocalReport.DataSources.Clear();
            RptViewerRptCard.LocalReport.DataSources.Add(datasource);
            RptViewerRptCard.LocalReport.DataSources.Add(datasource2);
            RptViewerRptCard.LocalReport.DataSources.Add(datasource3);
            RptViewerRptCard.LocalReport.DataSources.Add(datasource4);
            RptViewerRptCard.LocalReport.DataSources.Add(datasource5);

        }

        private void NurseryReportBind()
        {
            RptViewerRptCard.ProcessingMode = ProcessingMode.Local;
            RptViewerRptCard.LocalReport.ReportPath = Server.MapPath("~/Report/ReportCards_Nursery_Comments.rdlc");
            MyStuDataSet dsAssessment1 = GetData();
            MyStuDataSet dsAssessment2 = GetAssessement2Data();
            MyStuDataSet dsAttendance = GetAttendanceData();
            MyStuDataSet dsTotals = GetTotalsData();
            MyStuDataSet dsGrade = GetGradeLegend();
            MyStuDataSet dsNurseryRatings = GetNurseryRatings();
            ReportDataSource datasource = new ReportDataSource("SubjectsDataSet", dsAssessment1.Tables[6]);
            ReportDataSource datasource2 = new ReportDataSource("AsDataset", dsAssessment2.Tables[7]);
            ReportDataSource datasource3 = new ReportDataSource("AttendanceDataSet", dsAttendance.Tables[8]);
            ReportDataSource datasource4 = new ReportDataSource("TotalsDataSet", dsTotals.Tables[9]);
            ReportDataSource datasource5 = new ReportDataSource("GradeDataSet", dsGrade.Tables[10]);
            ReportDataSource datasource6 = new ReportDataSource("RatingsDataSet", dsNurseryRatings.Tables[17]);
            RptViewerRptCard.LocalReport.DataSources.Clear();
            RptViewerRptCard.LocalReport.DataSources.Add(datasource);
            RptViewerRptCard.LocalReport.DataSources.Add(datasource2);
            RptViewerRptCard.LocalReport.DataSources.Add(datasource3);
            RptViewerRptCard.LocalReport.DataSources.Add(datasource4);
            RptViewerRptCard.LocalReport.DataSources.Add(datasource5);
            RptViewerRptCard.LocalReport.DataSources.Add(datasource6);

        }

        //protected void RptViewerRptCard_Load(object sender, EventArgs e)
        //{
        //    //string exportOption = "Word";
        //    ////string exportOption1 = "Word";
        //    //// string exportOption = "PDF";
        //    //RenderingExtension extension = RptViewerRptCard.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase));
        //    //if (extension != null)
        //    //{
        //    //    System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        //    //    fieldInfo.SetValue(extension, false);
        //    //}

        //}

        protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownTerm();
            if (ddlTerm.Items.Count > 1)
            {
                ddlTerm.SelectedIndex = 1;

            }
        }

        protected void btnReportCard_Click(object sender, EventArgs e)
        {
            FindSection();
            if (lblFindClassSection.Text == "Senior Secondary" || lblFindClassSection.Text == "Junior Secondary" || lblFindClassSection.Text == "Primary") { Info(); }
            else
            {
                NurseryReportBind();
            }
        }





    }
}