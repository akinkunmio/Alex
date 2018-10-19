﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Alex.pages.application_reports
{
    public partial class made_year_term : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Applications_total_by_year_term();
            
        }

       

        private void Applications_total_by_year_term()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_rep_applications_total_by_year_term", con);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        GridViewReportApplicationTotalByYearTerm.DataSource = dt;
                        GridViewReportApplicationTotalByYearTerm.DataBind();
                    }
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
            }
            finally
            {
                con.Close();
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('SUCESSFULLY RECORD SAVED');", true);
            }
        }
    }
}