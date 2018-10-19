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
    public partial class studentId_card : System.Web.UI.Page
    {
        string PersonId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PersonId = Request.QueryString["stuID_card"].ToString(); 
                Info();
            }
        }

        private MyStuDataSet GetData()
        {
            PersonId = Request.QueryString["stuID_card"].ToString(); 
            MyStuDataSet dt = new MyStuDataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_person_students_id_card", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@person_id", PersonId);
                        // cmd.Parameters.Add("@person_id", SqlDbType.Int).Value = PersonId;
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            sda.Fill(dt.Tables["sp_ms_person_students_id_card"]);
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
            RptViewerIdCard.ProcessingMode = ProcessingMode.Local;
            RptViewerIdCard.LocalReport.ReportPath = Server.MapPath("../Report/StudentIdCard.rdlc");
            // ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Report/EmployeePayslip.rdlc");
            MyStuDataSet dsIdCard = GetData();
            ReportDataSource datasource = new ReportDataSource("MyStuDataSet", dsIdCard.Tables[2]);
            RptViewerIdCard.LocalReport.DataSources.Clear();
            RptViewerIdCard.LocalReport.DataSources.Add(datasource);
        }

    }
}