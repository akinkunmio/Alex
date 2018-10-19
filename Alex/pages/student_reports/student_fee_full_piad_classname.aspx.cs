using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Configuration;

namespace Alex.pages.student_reports
{
    public partial class student_fee_full_piad_classname : System.Web.UI.Page
    {
        String ClassName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClassName = Request.QueryString["id"];
                StudentsInClass();
            }
        }

        private void StudentsInClass()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_rep_student_all_debtors_list_by_class", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@class", SqlDbType.VarChar).Value = ClassName;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        con.Open();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {

                            sda.Fill(dt);
                            GridViewClassStudents.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                GridViewClassStudents.Visible = false;

                            }
                            else
                            {
                                GridViewClassStudents.DataBind();
                                GridViewClassStudents.Visible = true;
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


        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("student_fee_full_paid.aspx");
        }
    }
}