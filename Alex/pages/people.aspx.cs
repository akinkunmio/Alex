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
//using Alex.App_code;


namespace Alex.pages
{
    public partial class people : System.Web.UI.Page
    {
     protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TbSearch.Focus();
            }
            string SearchInfo = String.Empty;
            try
            {
                SearchInfo = Session["SearchText"].ToString();
            }
            catch { }
            if (SearchInfo != string.Empty)
            {
                Session.Remove("SearchText");
                Search(SearchInfo);
                divAlpha.Visible = false;
            }
            else
            {
                //ManageCookies.VerifyAuthentication();
                this.ViewState["CurrentAlphabet"] = "ALL";
                this.GenerateAlphabets();
                BindGrid(this.ViewState["CurrentAlphabet"].ToString());
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

                //BindGrid(e.ToString());

                ViewState["CurrentAlphabet"] = lnkAlphabet.Text;
                string StartAlpha = lnkAlphabet.Text;
                this.GenerateAlphabets();
                BindGrid(StartAlpha);
            }
        }
       

        private void BindGrid(string StartAlpha)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                string SP_Name = "sp_ms_people_list_all";

                if (StartAlpha != "ALL")
                {
                    SP_Name = "sp_ms_people_list_all_alphabetic";
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
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            GridViewPeople.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                GridViewPeople.Visible = false;
                                lblZeroRecords.Visible = true;
                                lblZeroRecords.Text = "No Records found ";
                            }
                            else
                            {
                                GridViewPeople.Visible = true;
                                GridViewPeople.DataBind();
                                lblProfilePageHeading.Text = "List of Profiles";
                                divBacktoProfiles.Visible = false;
                                lblZeroRecords.Visible = false;
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
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        //      try
        //      {
        //          using (SqlCommand cmd = new SqlCommand("sp_ms_people_list_all", con))
        //          {
        //              using (SqlDataAdapter sda = new SqlDataAdapter())
        //              {
        //                  con.Open();
        //                  cmd.Connection = con;
        //                  sda.SelectCommand = cmd;
        //                  using (DataTable dt = new DataTable())
        //                  {
        //                      sda.Fill(dt);
        //                      GridViewPeople.DataSource = dt;
        //                      GridViewPeople.DataBind();
        //                  }
        //              }
        //          }
        //    }
        //    catch (Exception ex)
        //      {
        //          ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //          // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
        //      }
        //    finally
        //      {
        //          con.Close();
        //      }
        //}

        //private void BindGrid1(int pageIndex =0 )
        //   {
        //     SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        //       try
        //       {
        //           using (SqlCommand cmd = new SqlCommand("sp_ms_people_list_all", con))
        //           {
        //               using (SqlDataAdapter sda = new SqlDataAdapter())
        //               {
        //                   con.Open();
        //                   cmd.Connection = con;
        //                   sda.SelectCommand = cmd;
        //                   using (DataTable dt = new DataTable())
        //                   {
        //                       sda.Fill(dt);
        //                       GridViewPeople.DataSource = dt;
        //                       GridViewPeople.PageIndex = pageIndex;
        //                       GridViewPeople.DataBind();
        //                   }
        //               }
        //           }
        //       }

        //       catch (Exception ex)
        //       {
        //           ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
        //           // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
        //       }
        //       finally
        //       {
        //           con.Close();

        //       }
        //  }
        //protected void BtnMorePeople_Click(object sender, EventArgs e)
        //  {
        //      BtnMorePeople.Visible = false;
        //      BindGrid1(1);
        //  }

        private void Search(string SearchName)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_people_search", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@f_name", SqlDbType.VarChar).Value = SearchName;
                cmd.Parameters.AddWithValue("@l_name", SqlDbType.VarChar).Value = SearchName;
                cmd.Parameters.AddWithValue("@dob", SqlDbType.VarChar).Value = SearchName;
                //cmd.Parameters.AddWithValue("@dob", SqlDbType.VarChar).Value = "%" + TbSearch.Text + "%";
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                lblProfilePageHeading.Text = "Search Results Profiles";
                divBacktoProfiles.Visible = true;
                if (dt.Rows.Count > 0)
                {
                    GridViewPeople.Visible = true;
                    GridViewPeople.DataSource = dt;
                    GridViewPeople.DataBind();
                }
                else
                {
                    GridViewPeople.Visible = false;
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

        protected void GridViewPeople_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPeople.PageIndex = e.NewPageIndex;
            BindGrid("ALL");
        }

        protected void BtnSearch_Click1(object sender, EventArgs e)
        {
            string SearchInfo = TbSearch.Text;
            
            //if (!string.IsNullOrWhiteSpace(SearchInfo))
            //{
                Search(SearchInfo);
                divAlpha.Visible = false;
            //}
            //else
            //{
            //    GridViewPeople.Visible = false;
            //    divAlpha.Visible = false;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Please enter Name or Date of Birth ');", true);
            //}
            
        }

        protected void GridViewPeople_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {//for footer page display count 
                e.Row.Cells[0].Text = "Page " + (GridViewPeople.PageIndex + 1) + " of " + GridViewPeople.PageCount;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hl = (HyperLink)e.Row.FindControl("HyperLinkPeople");
                if (hl != null)
                {
                    DataRowView drv = (DataRowView)e.Row.DataItem;
                    string keyword = drv["person_id"].ToString();
                    hl.NavigateUrl = "~/pages/profile.aspx?PersonId=" + keyword;
                    //hl.NavigateUrl = "/pages/profile.aspx?PersonId=" + Uri.EscapeUriString("keyword");
                }
            }
        }

    }
}