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
using Alex.App_code;

namespace Alex.pages
{
    public partial class students : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownFormClass();
                ddlFormClass.SelectedIndex = 1;
                this.ViewState["CurrentAlphabet"] = "ALL";
                this.GenerateAlphabets();
                BindGrid(this.ViewState["CurrentAlphabet"].ToString());
                this.GridViewStudents.Columns[2].Visible = true;
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

        public void DropDownFormClass()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_form_class_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlFormClass.DataSource = ddlValues;
            ddlFormClass.DataValueField = "form_class";
            ddlFormClass.DataTextField = "form_class";
            ddlFormClass.DataBind();
            ddlFormClass.Items.Insert(0, new ListItem("Please select Class"));
            ddlFormClass.Items.Insert(1, new ListItem("ALL"));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
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
                string SP_Name = "sp_ms_person_students_list_all_filtered_by_class";

                if (StartAlpha != "ALL")
                {
                    SP_Name = "sp_ms_person_students_list_all_filtered_by_class_alphabetic";
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
                        cmd.Parameters.AddWithValue("@form_class", ddlFormClass.SelectedItem.ToString());
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridViewStudents.DataSource = dt;
                            if (dt.Rows.Count == 0)
                            {
                                GridViewStudents.Visible = false;
                                lblZeroRecords.Visible = true;
                                lblZeroRecords.Text = "No Student found";
                            }
                            else
                            {
                                GridViewStudents.Visible = true;
                                GridViewStudents.DataBind();
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

        private void Search()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_person_students_search_by_f_name_l_name_dob", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@f_name", SqlDbType.VarChar).Value = TbSearch.Text;
                cmd.Parameters.AddWithValue("@l_name", SqlDbType.VarChar).Value = TbSearch.Text;
                cmd.Parameters.AddWithValue("@dob", SqlDbType.VarChar).Value = TbSearch.Text;
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    GridViewStudents.Visible = true;
                    this.GridViewStudents.Columns[2].Visible = true;
                    GridViewStudents.DataSource = dt;
                    GridViewStudents.DataBind();

                }
                else
                {
                    GridViewStudents.Visible = false;
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

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            divAlpha.Visible = false;
            Search();
            ClearData(this);

        }

        protected void GridViewStudents_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hl = (HyperLink)e.Row.FindControl("HyperLinkPeople");
                if (hl != null)
                {
                    DataRowView drv = (DataRowView)e.Row.DataItem;
                    string keyword = drv["person_id"].ToString();
                    //keyword = HttpUtility.UrlEncode(ManageCookies.EncodeTo64(keyword));
                    hl.NavigateUrl = "~/pages/profile.aspx?PersonId=" + keyword;

                    //hl.NavigateUrl = "/pages/profile.aspx?PersonId=" + Uri.EscapeUriString("keyword");
                }
            }
        }

        protected void GridViewStudents_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewStudents.PageIndex = e.NewPageIndex;
            BindGrid("ALL");

        }

        //protected void btnSearchFormClass_Click(object sender, EventArgs e)
        //{
        //    divAlpha.Visible = true;
        //    this.ViewState["CurrentAlphabet"] = "ALL";
        //    this.GenerateAlphabets();
        //    BindGrid(this.ViewState["CurrentAlphabet"].ToString());
        //    this.GridViewStudents.Columns[3].Visible = false;
        //}

        protected void ddlFormClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            divAlpha.Visible = true;
            this.ViewState["CurrentAlphabet"] = "ALL";
            this.GenerateAlphabets();
            this.GridViewStudents.Columns[2].Visible = false;
            if (ddlFormClass.Items.FindByText("ALL").Selected == true)
            {
                this.GridViewStudents.Columns[2].Visible = true;
            }
            BindGrid(this.ViewState["CurrentAlphabet"].ToString());
        }
    }
}