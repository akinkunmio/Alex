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
    public partial class report_cumulative : System.Web.UI.Page
    {
        string PersonId = string.Empty;
        string RegID = string.Empty;
        string AcadYr = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            var t = Request.Form["__EVENTTARGET"];
            if (!IsPostBack)
            {
                PersonId = Request.QueryString["rptID_card"].ToString();
                RegID = Request.QueryString["PID"].ToString();
                Info();
            }
        }

        public void RunSP()
        {
            PersonId = Request.QueryString["rptID_card"].ToString();
            AcadYr = Request.QueryString["acad"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlTransaction transaction;
            con.Open();
            transaction = con.BeginTransaction();
            try
            {
              SqlCommand cmd1 = new SqlCommand("sp_ms_person_report_card_breakdown_1stb ", con);
              SqlCommand cmd2 = new SqlCommand("sp_ms_person_report_card_breakdown_2ndb ", con);
              SqlCommand cmd3 = new SqlCommand("sp_ms_person_report_card_breakdown_3rdb", con);
               cmd1.CommandType = CommandType.StoredProcedure;
               cmd2.CommandType = CommandType.StoredProcedure;
               cmd3.CommandType = CommandType.StoredProcedure;
                 cmd1.Parameters.AddWithValue("@person_id", PersonId);
                        cmd1.Parameters.AddWithValue("@year", AcadYr);
                        cmd1.Parameters.AddWithValue("@acad_year_id", DBNull.Value);
                        cmd1.Parameters.AddWithValue("@section", DBNull.Value);
                        cmd1.Parameters.Add("@ay_term_id", SqlDbType.Int).Value = DBNull.Value;
                 cmd2.Parameters.AddWithValue("@person_id", PersonId);
                        cmd2.Parameters.AddWithValue("@year", AcadYr);
                        cmd2.Parameters.AddWithValue("@acad_year_id", DBNull.Value);
                        cmd2.Parameters.AddWithValue("@section", DBNull.Value);
                        cmd2.Parameters.Add("@ay_term_id", SqlDbType.Int).Value = DBNull.Value;
                 cmd3.Parameters.AddWithValue("@person_id", PersonId);
                        cmd3.Parameters.AddWithValue("@year", AcadYr);
                        cmd3.Parameters.AddWithValue("@acad_year_id", DBNull.Value);
                        cmd3.Parameters.AddWithValue("@section", DBNull.Value);
                        cmd3.Parameters.Add("@ay_term_id", SqlDbType.Int).Value = DBNull.Value;
                        cmd1.ExecuteNonQuery();
                        cmd2.ExecuteNonQuery();
                        cmd3.ExecuteNonQuery();
                        transaction.Commit();
            }

            catch (SqlException )
            {
                transaction.Rollback();
            }

            finally
            {
                con.Close();
                con.Dispose();
            }
        }
        private MyStuDataSet GetData()
        {
            RunSP();
            PersonId = Request.QueryString["rptID_card"].ToString();
            AcadYr = Request.QueryString["acad"].ToString();
            MyStuDataSet dt = new MyStuDataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_person_report_card_breakdown_cumm_v4", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@person_idr", PersonId);
                        cmd.Parameters.AddWithValue("@yearr", AcadYr);
                        cmd.Parameters.AddWithValue("@acad_year_id", DBNull.Value);
                        cmd.Parameters.AddWithValue("@section", DBNull.Value);
                        cmd.Parameters.Add("@ay_term_id", SqlDbType.Int).Value = DBNull.Value;
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            dt.sp_ms_person_report_card_breakdown_cumm_v4.Clear();
                            dt.EnforceConstraints = false;
                            sda.Fill(dt.Tables["sp_ms_person_report_card_breakdown_cumm_v4"]);
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
            try
            {
                RptViewerRptCard.ProcessingMode = ProcessingMode.Local;
                RptViewerRptCard.LocalReport.ReportPath = Server.MapPath("../Report/ReportCard_Cumulative.rdlc");
                MyStuDataSet dsAssessment1 = GetData();
                MyStuDataSet dsAssessment2 = GetAssessement2Data();
                MyStuDataSet dsAttendance = GetAttendanceData();
                MyStuDataSet dsTotals = GetTotalsData();
                MyStuDataSet dsGrade = GetGradeLegend();
                ReportDataSource datasource = new ReportDataSource("SubjectsDataSet", dsAssessment1.Tables[20]);
                ReportDataSource datasource2 = new ReportDataSource("AsDataSet", dsAssessment2.Tables[12]);
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

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            }

            finally
            {
                //con.Close();
            }
        }



    }
}