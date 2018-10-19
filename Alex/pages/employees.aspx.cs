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


namespace Alex.pages
{
    public partial class hr_payroll : System.Web.UI.Page
    {
        int lvl = 0;
       protected void Page_Load(object sender, EventArgs e)
        {
            Level();
            ManageCookies.VerifyAuthentication();
            if (lvl == 1)
            {
                lblZeroRecords.Text = "";
                GridViewHr.Visible = false;
                if (!Page.IsPostBack)
                {
                    ddlEmployeeStatus.SelectedIndex = 1;
                    this.ViewState["CurrentAlphabet"] = "ALL";
                    this.GenerateAlphabets();
                    BindGrid(this.ViewState["CurrentAlphabet"].ToString());
                }
            }
            else if (lvl == 2 || lvl == 3 || lvl == 4 || lvl == 5)
            {
                Response.Redirect("~/pages/ForbiddenAccess.aspx", false);
            }
            else
            {
                Response.Redirect("~/pages/login.aspx", false);
            }
        }

       public void Level()
       {
           try
           {
           
             lvl = (int)(Session["level_of_access"]);
           }
           catch (Exception ex)
           {
               ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
           }
       }

       private void GenerateAlphabets()
       {
           List<ListItem> alphabets = new List<ListItem>();
           ListItem alphabet = new ListItem();
           alphabet.Value = "ALL";
           alphabet.Selected = alphabet.Value.Equals(ViewState["CurrentAlphabet"]);
           alphabets.Add(alphabet);
           for (int i = 65; i <= 90; i++)
           {
               alphabet = new ListItem();
               alphabet.Value = Char.ConvertFromUtf32(i);
               alphabet.Selected = alphabet.Value.Equals(ViewState["CurrentAlphabet"]);
               alphabets.Add(alphabet);
           }
           rptAlphabets.DataSource = alphabets;
           rptAlphabets.DataBind();
       }

       protected void Alphabet_Click(object sender, EventArgs e)
       {
           if (this.ViewState["CurrentAlphabet"] != null)
           {
               LinkButton lnkAlphabet = (LinkButton)sender;
               ViewState["CurrentAlphabet"] = lnkAlphabet.Text;
               string StartAlpha = lnkAlphabet.Text;
               this.GenerateAlphabets();
               BindGrid(StartAlpha);
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
        private void Search()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_hr_employees_search", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@f_name", SqlDbType.VarChar).Value = TbSearch.Text;
                cmd.Parameters.AddWithValue("@l_name", SqlDbType.VarChar).Value = TbSearch.Text;
                cmd.Parameters.AddWithValue("@dob", SqlDbType.VarChar).Value = TbSearch.Text;
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                lblProfilePageHeading.Text = "Search Results Employees";
                divBacktoProfiles.Visible = true;
                if (dt.Rows.Count > 0)
                {
                    GridViewHr.DataSource = dt;
                    this.GridViewHr.Columns[4].Visible = true; 
                    GridViewHr.DataBind(); 
                    GridViewHr.Visible = true;
                }
                else
                {
                    GridViewHr.Visible = false;
                    this.GridViewHr.Columns[4].Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('No Results Found ');", true);
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

        private void BindGrid(string StartAlpha)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                string SP_Name = "sp_ms_hr_employees_list_current_n_previous";

                if (StartAlpha != "ALL")
                {
                    SP_Name = "sp_ms_hr_employees_list_current_n_previous_alphabetic";
                }
                using (SqlCommand cmd = new SqlCommand(SP_Name, con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        if (StartAlpha != "ALL")
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            con.Open();
                            cmd.Parameters.AddWithValue("@lastname", StartAlpha);
                            //cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            con.Open();
                            cmd.Connection = con;
                        }
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@status", ddlEmployeeStatus.SelectedItem.ToString());
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            GridViewHr.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                GridViewHr.Visible = false;
                                lblZeroRecords.Visible = true;
                                lblZeroRecords.Text = "No Records found ";
                            }
                            else
                            {
                               if (ddlEmployeeStatus.SelectedValue.ToString() == "Current")
                                {
                                    this.GridViewHr.Columns[4].Visible = false;
                                }
                                else
                                {
                                    this.GridViewHr.Columns[4].Visible = true;
                                }
                                GridViewHr.Visible = true;
                                GridViewHr.DataBind();
                                lblProfilePageHeading.Text = "List of Employees";
                                lblZeroRecords.Visible = false;
                                divBacktoProfiles.Visible = false;
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
        //private void EmployeeList()
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("sp_ms_hr_employees_list_all", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        using (SqlDataAdapter sda = new SqlDataAdapter())
        //        {
        //            con.Open();
        //            cmd.Connection = con;
        //            sda.SelectCommand = cmd;
        //            using (DataTable dt = new DataTable())
        //            {
        //                sda.Fill(dt);
        //                GridViewHr.DataSource = dt;
        //                GridViewHr.DataBind();
                        
        //                }
        //            }
        //        }
          

        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //        // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        protected void GridViewHr_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {//for footer page display count 
                e.Row.Cells[0].Text = "Page " + (GridViewHr.PageIndex + 1) + " of " + GridViewHr.PageCount;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hl = (HyperLink)e.Row.FindControl("HyperLinkEmployee");
                if (hl != null)
                {
                    DataRowView drv = (DataRowView)e.Row.DataItem;
                    string keyword = drv["emp_id"].ToString();
                   
                    hl.NavigateUrl = "~/pages/employee_profile.aspx?EmployeeId=" + keyword;
                }
            }
        }

        protected void GridViewHr_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewHr.PageIndex = e.NewPageIndex;
            //EmployeeList();
            BindGrid("ALL");
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            Search(); 
            divAlpha.Visible = false;
            ClearData(this);
        }

        //protected void btnEmployeeListByStatus_Click(object sender, EventArgs e)
        //{
        //    divAlpha.Visible = true;
        //    this.ViewState["CurrentAlphabet"] = "ALL";
        //    this.GenerateAlphabets();
        //    BindGrid(this.ViewState["CurrentAlphabet"].ToString());
        //}

        protected void ddlEmployeeStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            divAlpha.Visible = true;
            this.ViewState["CurrentAlphabet"] = "ALL";
            this.GenerateAlphabets();
            BindGrid(this.ViewState["CurrentAlphabet"].ToString());
        }
      
    }
}