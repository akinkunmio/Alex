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

namespace Alex.pages.admin
{
    public partial class setup_academic_year : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            if (!Page.IsPostBack)
            {
                AcademicYearBindData();
            }
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
        protected void BtnSaveAcademicYear_Click(object sender, EventArgs e)
        {
          SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
           try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_settings_acad_year_add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                cmd.Parameters.AddWithValue("@acad_year", tbAcademicYear.Text.ToString());
                cmd.Parameters.AddWithValue("@acad_y_start_date", tbAcademicStartDate.Text.ToString());
                cmd.Parameters.AddWithValue("@acad_y_end_date", tbAcademicEndDate.Text.ToString());
                //cmd.Parameters.AddWithValue("@created_date", tbCreatedDate.Text.ToString());
                //cmd.Parameters.AddWithValue("@update_date", tbUpdatedDate.Text.ToString());
                cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                cmd.ExecuteNonQuery();
                AcademicYearBindData();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Academic Year Saved Successfully');", true);
            }
           catch //(Exception ex)
           {
               //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
               ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Cannot Add Duplicate Record');", true);          
           }
            finally
            {
                con.Close();
                ClearData(this);
               
            }
        }
        protected void AcademicYearBindData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_settings_acad_year_list_all", con);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        GridViewAcademicYear.DataSource = dt;
                        if (dt.Rows.Count == 0)
                        {
                            GridViewAcademicYear.Visible = false;
                            lblZeroRecords.Visible = true;
                            lblSetupAcademicYear.Visible = true;
                            lblZeroRecords.Text = "No Records found ";
                            lblSetupAcademicYear.Text = "No Academic Year Has Been Set Up Fill In Below To Create New Academic Year ";
                        }
                        else
                        {
                            GridViewAcademicYear.Visible = true;
                            GridViewAcademicYear.DataBind();
                            lblZeroRecords.Visible = false;
                            lblSetupAcademicYear.Visible = false;
                            //GridViewRow firstRow = GridViewAcademicYear.Rows[0];
                            //firstRow.CssClass = "text-red";
                            //firstRow.ToolTip = "Current Academic Year";
                            //firstRow.Attributes.Add("tbAcademicYear", "Current Academic Year");
                            
                        }

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

        protected void GridViewAcademicYear_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewAcademicYear.EditIndex = e.NewEditIndex;
            AcademicYearBindData();
        }

        protected void GridViewAcademicYear_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GridViewAcademicYear.EditIndex = -1;
            AcademicYearBindData();
        }

        protected void GridViewAcademicYear_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
             {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_settings_acad_year_edit", con);
                GridViewRow row = GridViewAcademicYear.Rows[e.RowIndex] as GridViewRow;
                TextBox txtAcademicYear = row.FindControl("tbEditAcademicYear") as TextBox;
                TextBox txtAcademicYearSD = row.FindControl("tbAcademicYearSD") as TextBox;
                TextBox txtAcademicYearED = row.FindControl("tbAcademicYearED") as TextBox;
                //DropDownList txtStatus = row.FindControl("ddlStatus") as DropDownList;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "acad_year_id", Value = GridViewAcademicYear.Rows[e.RowIndex].Cells[0].Text });
                //cmd.Parameters.Add(new SqlParameter() { ParameterName = "term_name", Value = GridViewFee.Rows[e.RowIndex].Cells[1].Text });
                //cmd.Parameters.Add(new SqlParameter() { ParameterName = "acad_year", Value = GridViewFee.Rows[e.RowIndex].Cells[2].Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "acad_year", Value = txtAcademicYear.Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "acad_y_start_date", Value = txtAcademicYearSD.Text.ToString() });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "acad_y_end_date", Value = txtAcademicYearED.Text.ToString() });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "status", Value = GridViewAcademicYear.Rows[e.RowIndex].Cells[4].Text });
                cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                //con.Close();
                GridViewAcademicYear.EditIndex = -1;
                AcademicYearBindData();
            }
          catch
          {
              ScriptManager.RegisterStartupScript(Page, typeof(Page), "clentscript", "alert('Cannot update, Academic Year already exist');", true);
          }
          finally
          {
              con.Close();
          }
       }

        protected void GridViewAcademicYear_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);


            GridViewRow row = GridViewAcademicYear.Rows[e.RowIndex] as GridViewRow;
            string Rownumber = GridViewAcademicYear.Rows[e.RowIndex].Cells[0].Text;
            if (Rownumber != null)
            {
                using (SqlCommand cmdd = new SqlCommand("select * from dbo.ms_acad_year_term  WHERE acad_year_id = " + Rownumber, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmdd.ExecuteReader())
                    {
                        // Check is the reader has any rows at all before starting to read.
                        if (reader.HasRows)
                        {
                            Rownumber = "false";
                        }
                    }
                    con.Close();
                }

            }
            if (Rownumber != "false")
            {
                SqlCommand cmd = new SqlCommand("sp_ms_settings_acad_year_delete", con);
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "acad_year_id", Value = GridViewAcademicYear.Rows[e.RowIndex].Cells[0].Text });
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
                AcademicYearBindData();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot Delete Academic Year, it has Term(s) Registered up')", true);
            }
            
        }

       

        protected void btnStatus_Click(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((Button)sender).Parent.Parent)
            {
                string ID = row.Cells[0].Text;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_settings_acad_year_set_active", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@acad_year_id", SqlDbType.Int).Value = ID;
                        cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
                        cmd.ExecuteNonQuery();
                        AcademicYearBindData();
                        
                    }

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                }
                finally
                {
                    con.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Academic Year Active Sucessfully');", true);
                }
            }
        }

        protected void GridViewAcademicYear_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (GridViewAcademicYear.Rows.Count != -1)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string Value = e.Row.Cells[4].Text;
                    //Button btntext = e.Row.FindControl("btnStatus") as Button;
                    switch (Value)
                    {
                        case "Active":
                            e.Row.BackColor = System.Drawing.Color.LightBlue;
                           // btntext.Text = "Activated";
                            break;
                    }
                }
            }
        
        }
    }
}

