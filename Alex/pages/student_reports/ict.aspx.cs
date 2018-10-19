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
using System.IO;
using System.Drawing;
using ClosedXML.Excel;

namespace Alex.pages.student_reports
{
    public partial class ict : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                Info();
                DisableUnwantedExportFormat(RptViewerICTCard, "Excel");
                DisableUnwantedExportFormat(RptViewerICTCard, "Word");
                DisableUnwantedExportFormat(RptViewerICTCard, "WORDOPENXML");
                DisableUnwantedExportFormat(RptViewerICTCard, "EXCELOPENXML");
            }
        }
        private MyStuDataSet GetData()
        {
            MyStuDataSet dt = new MyStuDataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_ict_all", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            sda.Fill(dt.Tables["sp_ms_ict_all"]);
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
            RptViewerICTCard.ProcessingMode = ProcessingMode.Local;
            RptViewerICTCard.LocalReport.ReportPath = Server.MapPath("~/Report/Ict.rdlc");
            // ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Report/EmployeePayslip.rdlc");
            MyStuDataSet dsIdCard = GetData();
            ReportDataSource datasource = new ReportDataSource("MyStuDataSet", dsIdCard.Tables[5]);
            RptViewerICTCard.LocalReport.DataSources.Clear();
            RptViewerICTCard.LocalReport.DataSources.Add(datasource);
        }


       
        public void DisableUnwantedExportFormat(ReportViewer ReportViewerID, string strFormatName)
        {
            System.Reflection.FieldInfo info;
            foreach (RenderingExtension extension in ReportViewerID.LocalReport.ListRenderingExtensions())
            {
                if (extension.Name.Trim().ToUpper() == strFormatName.Trim().ToUpper())
                {
                    info = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                    info.SetValue(extension, false);
                }
            }

        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_iqo_all_current_student_login_details"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt, "iQ ICT");

                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=ICT.xlsx");
                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    Response.End();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}