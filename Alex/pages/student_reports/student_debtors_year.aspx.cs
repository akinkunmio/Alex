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
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace Alex.pages.student_reports
{
    public partial class student_debtors_year : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        
        {
            //imgSchool.ImageUrl = "~/pages/ImageHandler.ashx?roll_no=1";

           ManageCookies.VerifyAuthentication();
            if (!Page.IsPostBack)
            {
                System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                img.ImageUrl = "~/Images/asc.gif";
                GridViewReportDebtors.HeaderRow.Cells[1].Controls.Add(new LiteralControl(""));
                GridViewReportDebtors.HeaderRow.Cells[1].Controls.Add(img);

                System.Web.UI.WebControls.Image img1 = new System.Web.UI.WebControls.Image();
                img1.ImageUrl = "~/Images/asc.gif";
                GridViewReportDebtors.HeaderRow.Cells[2].Controls.Add(new LiteralControl(""));
                GridViewReportDebtors.HeaderRow.Cells[2].Controls.Add(img1);

                System.Web.UI.WebControls.Image img2 = new System.Web.UI.WebControls.Image();
                img2.ImageUrl = "~/Images/asc.gif";
                GridViewReportDebtors.HeaderRow.Cells[3].Controls.Add(new LiteralControl(""));
                GridViewReportDebtors.HeaderRow.Cells[3].Controls.Add(img2);
                SchoolBind();

                DebtorsBind();         
           }
        }

    //private DataTable GetData()
    //{

    //   DataTable table = new DataTable();
    //     // get the connection
    //  using ( SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
    //   {
    //       using (SqlCommand cmd = new SqlCommand("sp_ms_rep_student_all_debtors_list", con))
    //            {
    //                cmd.CommandType = CommandType.StoredProcedure;
    //               // instantiate the command object to fire
    //                con.Open();
    //                cmd.Connection = con;
    //                // get the adapter object and attach the command object to it
    //                using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
    //                  {
    //                      ad.SelectCommand = cmd;
    //                    // fire Fill method to fetch the data and fill into DataTable
    //                      ad.Fill(table);
    //                   }
    //           }
    //   }
    //   return table;
    //}
        private void DebtorsBind()
        {
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            //try
            //{
            //    using (SqlCommand cmd = new SqlCommand("sp_ms_rep_student_all_debtors_list", con))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        using (SqlDataAdapter sda = new SqlDataAdapter())
            //        {
            //            con.Open();
            //            cmd.Connection = con;
            //            sda.SelectCommand = cmd;
            //            using (DataTable dt = new DataTable())
            //            {

            //                sda.Fill(dt);
            //                GridViewReportDebtors.DataSource = dt;
            //                if (dt.Rows.Count == 0)
            //                {
            //                    GridViewReportDebtors.Visible = false;
            //                    lblZeroStudents.Visible = true;
            //                    lblZeroStudents.Text = "No  Students Found ";
            //                    DivStudents.Visible = false;
            //                }
            //                else
            //                {
            //                    DivStudents.Visible = true;
            //                    lblZeroStudents.Visible = false;
                                imgSchool.ImageUrl = "~/pages/ImageHandler.ashx?roll_no=1";

            //                    GridViewReportDebtors.DataBind();
            //                    GridViewReportDebtors.Visible = true;
            //                    btnPrint.Visible = true;

            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            //    Response.Write("Oops!! following error occured: " + ex.Message.ToString());
            //}
            //finally
            //{
            //    con.Close();
            //}
        }

        public void SchoolBind()
        {
            SqlDataAdapter adp = new SqlDataAdapter();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_settings_school_detail", con);
                cmd.Dispose();
                SqlDataReader dr = cmd.ExecuteReader();
                string SchoolName = string.Empty;
                while (dr.Read())
                {
                    SchoolName = dr["school_name"].ToString();
                    string Address = dr["address_line1"].ToString();
                    string Address2 = dr["address_line2"].ToString();
                    string City = dr["lga_city"].ToString();
                    string State = dr["state"].ToString();
                    string country = dr["country"].ToString();
                    string Postcode = dr["zip_postal_code"].ToString();
                    string Email = dr["contact_email"].ToString();
                    string PhoneNo = dr["contact_no1"].ToString();

                    lblSchoolName.Text = SchoolName.ToString();
                    lblAddress.Text = Address.ToString() + Address2.ToString();
                    lblCity.Text = City.ToString();
                    lblState.Text = State.ToString();
                    lblCountry.Text = country.ToString();
                    lblPostCode.Text = Postcode.ToString();
                    lblEmail.Text = Email.ToString();
                    lblPhoneNo.Text = PhoneNo.ToString();

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

        protected void btnSendDebSms_Click(object sender, EventArgs e)
        {
            Server.Transfer("~/pages/student_reports/send_sms_debtors.aspx");
        }


        //private const string ASCENDING = " ASC";
        //private const string DESCENDING = " DESC";

        //public SortDirection GridViewSortDirection
        //{
        //    get
        //    {
        //        if (ViewState["sortDirection"] == null)
        //            ViewState["sortDirection"] = SortDirection.Ascending;

        //        return (SortDirection)ViewState["sortDirection"];
        //    }
        //    set { ViewState["sortDirection"] = value; }
        //}

        //protected void GridViewReportDebtors_Sorting(object sender, GridViewSortEventArgs e)
        //{
        //    string sortExpression = e.SortExpression;

        //    if (GridViewSortDirection == SortDirection.Ascending)
        //    {
        //        GridViewSortDirection = SortDirection.Descending;
        //        SortGridView(sortExpression, DESCENDING);
        //    }
        //    else
        //    {
        //        GridViewSortDirection = SortDirection.Ascending;
        //        SortGridView(sortExpression, ASCENDING);
        //    }   
        //}

        //private void SortGridView(string sortExpression, string direction)
        //{
        //    //  You can cache the DataTable for improving performance
        //    DataTable dt = GetData();

        //    DataView dv = new DataView(dt);
        //    dv.Sort = sortExpression + direction;

        //    GridViewReportDebtors.DataSource = dv;
        //    GridViewReportDebtors.DataBind();
        //}


        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void btnExportPDF_Click(object sender, EventArgs e)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    DivStudents.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=Student_Debtors.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
       
        
    }
}