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
namespace Alex.pages
{
    public partial class reportcard_primary : System.Web.UI.Page
    {
        string PersonId = string.Empty;
        string RegID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PersonId = Request.QueryString["rptID_card"].ToString();
                RegID = Request.QueryString["PID"].ToString();
                Info();
            }
        }

        private MyStuDataSet GetData()
        {
            PersonId = Request.QueryString["rptID_card"].ToString();
            MyStuDataSet dt = new MyStuDataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_person_report_card_breakdown", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@person_id", PersonId);
                        cmd.Parameters.Add("@reg_id", SqlDbType.Int).Value = RegID;
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            dt.sp_ms_person_report_card_breakdown.Clear();
                            dt.EnforceConstraints = false;
                            sda.Fill(dt.Tables["sp_ms_person_report_card_breakdown"]);
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
                    using (SqlCommand cmd = new SqlCommand("sp_ms_person_report_card_assessment2", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@reg_id", SqlDbType.Int).Value = RegID;
                        // cmd.Parameters.Add("@person_id", SqlDbType.Int).Value = PersonId;
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            dt.sp_ms_person_report_card_assessment2.Clear();
                            dt.EnforceConstraints = false;
                            sda.Fill(dt.Tables["sp_ms_person_report_card_assessment2"]);
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
                    using (SqlCommand cmd = new SqlCommand("sp_ms_person_report_card_attendance_summary", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@reg_id", SqlDbType.Int).Value = RegID;
                        // cmd.Parameters.Add("@person_id", SqlDbType.Int).Value = PersonId;
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            dt.sp_ms_person_report_card_attendance_summary.Clear();
                            dt.EnforceConstraints = false;
                            sda.Fill(dt.Tables["sp_ms_person_report_card_attendance_summary"]);
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
                    using (SqlCommand cmd = new SqlCommand("sp_ms_person_report_card_summary", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@reg_id", SqlDbType.Int).Value = RegID;
                        // cmd.Parameters.Add("@person_id", SqlDbType.Int).Value = PersonId;
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            dt.sp_ms_person_report_card_summary.Clear();
                            dt.EnforceConstraints = false;
                            sda.Fill(dt.Tables["sp_ms_person_report_card_summary"]);
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
                    using (SqlCommand cmd = new SqlCommand("sp_ms_grade_report_card_legend", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@reg_id", SqlDbType.Int).Value = RegID;
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            dt.sp_ms_grade_report_card_legend.Clear();
                            dt.EnforceConstraints = false;
                            sda.Fill(dt.Tables["sp_ms_grade_report_card_legend"]);
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
            RptViewerRptCard.LocalReport.ReportPath = Server.MapPath("../Report/ReportCard_Primary.rdlc");
            MyStuDataSet dsAssessment1 = GetData();
            MyStuDataSet dsAssessment2 = GetAssessement2Data();
            MyStuDataSet dsAttendance = GetAttendanceData();
            MyStuDataSet dsTotals = GetTotalsData();
            MyStuDataSet dsGrade = GetGradeLegend();
            ReportDataSource datasource = new ReportDataSource("SubjectsDataSet", dsAssessment1.Tables[11]);
            ReportDataSource datasource2 = new ReportDataSource("AsDataset", dsAssessment2.Tables[12]);
            ReportDataSource datasource3 = new ReportDataSource("AttendanceDataSet", dsAttendance.Tables[13]);
            ReportDataSource datasource4 = new ReportDataSource("TotalsDataSet", dsTotals.Tables[14]);
            ReportDataSource datasource5 = new ReportDataSource("GradeDataSet", dsGrade.Tables[15]);
            RptViewerRptCard.LocalReport.DataSources.Clear();
            RptViewerRptCard.LocalReport.DataSources.Add(datasource);
            RptViewerRptCard.LocalReport.DataSources.Add(datasource2);
            RptViewerRptCard.LocalReport.DataSources.Add(datasource3);
            RptViewerRptCard.LocalReport.DataSources.Add(datasource4);
            RptViewerRptCard.LocalReport.DataSources.Add(datasource5);

        }



    }
}