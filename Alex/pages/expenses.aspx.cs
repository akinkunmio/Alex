using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Alex.App_code;
namespace Alex.pages
{
    public partial class expenses : System.Web.UI.Page
    {
        int lvl = 0;
        private static string currentmonth = DateTime.Now.Month.ToString();
        private static string currentYear = DateTime.Now.Year.ToString();
        string selectedValue = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            Level();
            if (lvl == 1 || lvl == 2)
            {
                if (!Page.IsPostBack)
                {
                    DropDownExpensesYear();
                    //for (int year = 2015; year <= DateTime.Now.Year; year++)
                    //ddlYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
                    ddlYear.SelectedValue = currentYear;
                    ddlYear.Items.Insert(0, new ListItem("Select Year", ""));
                    Month();
                    ddlMonth.SelectedValue = currentmonth;
                    ExpensesBindData();
                    DropDownYear();
                    DropDownTerm();
                    DropDownExpensesCategory();

                }
            }
            else if (lvl == 3 || lvl == 4 || lvl == 5)
            {
                Response.Redirect("~/pages/ForbiddenAccess.aspx", false);
            }
            else
            {
                Response.Redirect("~/pages/logout.aspx", false);
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

        private void Month()
        {
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
            for (int i = 1; i < 13; i++)
            {
                ddlMonth.Items.Add(new ListItem(info.GetMonthName(i), i.ToString()));
            }
            ddlMonth.Items.Insert(0, new ListItem("Select Month", ""));
        }

        public void DropDownExpensesYear()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Select DISTINCT LEFT(exp_date, 4) as expenses_year from [dbo].[ms_expenses]", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlYear.DataSource = ddlValues;
            ddlYear.DataValueField = "expenses_year";
            ddlYear.DataTextField = "expenses_year";
            ddlYear.DataBind();
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
        public void DropDownYear()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_acad_year_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();
            ddlAcademicYear.DataSource = ddlValues;
            ddlAcademicYear.DataValueField = "acad_year";
            ddlAcademicYear.DataTextField = "acad_year";
            ddlAcademicYear.DataBind();
            ddlAcademicYear.Items.Insert(0, new ListItem("Please select a Year", ""));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        public void DropDownTerm()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_rep_term_dropdown_v2", con);
            cmd.Parameters.Add("@acad_year", SqlDbType.VarChar).Value = ddlAcademicYear.SelectedItem.ToString();
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Open();
                SqlDataReader ddlValues;
                ddlValues = cmd.ExecuteReader();
                string TermSelectedValue = null;
                while (ddlValues.Read())
                {
                    TermSelectedValue = ddlValues[0].ToString();
                    int DefaultValue = Convert.ToInt32(ddlValues[1]);
                    if (DefaultValue == 1)
                        break;
                }
                ddlValues.Close();
                ddlValues = cmd.ExecuteReader();
                ddlTerm.DataSource = ddlValues;
                ddlTerm.DataValueField = "term_name";
                ddlTerm.DataTextField = "term_name";
                ddlTerm.DataBind();
                ddlTerm.Items.Insert(0, new ListItem("Please select Term", ""));
                ddlTerm.SelectedValue = TermSelectedValue;

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
        public void DropDownExpensesCategory()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_status_dropdown_orderby_name", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@category", "Expenses_type");
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlCategory.DataSource = ddlValues;
            ddlCategory.DataValueField = "status_name";
            ddlCategory.DataTextField = "status_name";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("Please select Category", ""));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
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
        protected void BtnSaveExpenses_Click(object sender, EventArgs e)
        {
            SaveExpenses();
            btnAddExpenses.Visible = true;
            divAddExpenses.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record saved successfully');window.location = '/pages/expenses.aspx';", true);
        }

        private void SaveExpenses()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_expenses_add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                cmd.Parameters.AddWithValue("@type_refund_payment", ddlPaymentType.Text.ToString());
                cmd.Parameters.AddWithValue("@category", ddlCategory.Text.ToString());
                cmd.Parameters.AddWithValue("@exp_date", tbExpensesDate.Text.ToString());
                cmd.Parameters.AddWithValue("@amount", tbAmount.Text.ToString());
                cmd.Parameters.AddWithValue("@description", tbDescription.Text.ToString());
                cmd.Parameters.AddWithValue("@reciept_ref", tbRecieptRef.Text.ToString());
                cmd.Parameters.AddWithValue("@acad_year", ddlAcademicYear.Text.ToString());
                cmd.Parameters.AddWithValue("@term", ddlTerm.Text.ToString());
                cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Record saved successfully');", true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Cannot Add Duplicate Record');", true);
            }
            finally
            {
                con.Close();
                ClearData(this);

            }
        }
        protected void ExpensesBindData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_expenses_list_month", con);
                cmd.Parameters.AddWithValue("@received_month", ddlMonth.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@received_year", ddlYear.SelectedItem.ToString());
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    con.Open();
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        GridViewExpenses.DataSource = dt;
                        if (dt.Rows.Count == 0)
                        {
                            GridViewExpenses.Visible = false;
                            lblZeroRecords.Visible = true;

                            lblZeroRecords.Text = "No Records found ";

                        }
                        else
                        {
                            GridViewExpenses.Visible = true;
                            GridViewExpenses.DataBind();
                            lblZeroRecords.Visible = false;

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

        protected void GridViewExpenses_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewExpenses.EditIndex = e.NewEditIndex;
            ExpensesBindData();
        }

        protected void GridViewExpenses_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GridViewExpenses.EditIndex = -1;
            ExpensesBindData();
        }

        protected void GridViewExpenses_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_expenses_edit", con);
                GridViewRow row = GridViewExpenses.Rows[e.RowIndex] as GridViewRow;
                DropDownList txtPaymentRefundType = row.FindControl("ddlPaymentType") as DropDownList;
                DropDownList txtCategory = row.FindControl("ddlCategory") as DropDownList;
                TextBox txtExpensesDate = row.FindControl("tbExpensesDate") as TextBox;
                TextBox txtAmount = row.FindControl("tbAmount") as TextBox;
                TextBox txtDescription = row.FindControl("tbdescription") as TextBox;
                TextBox txtReciept = row.FindControl("tbRecieptReference") as TextBox;
                DropDownList txtAcadYear = row.FindControl("ddlAcadamicYear") as DropDownList;
                DropDownList txtTerm = row.FindControl("ddlTerm") as DropDownList;
                //DropDownList txtStatus = row.FindControl("ddlStatus") as DropDownList;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "exp_id", Value = GridViewExpenses.Rows[e.RowIndex].Cells[0].Text });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "type_refund_payment", Value = txtPaymentRefundType.Text.ToString() });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "category", Value = txtCategory.Text.ToString() });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "exp_date", Value = txtExpensesDate.Text.ToString() });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "amount", Value = txtAmount.Text.ToString() });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "description", Value = txtDescription.Text.ToString() });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "reciept_ref", Value = txtReciept.Text.ToString() });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "acad_year", Value = txtAcadYear.Text.ToString() });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "term", Value = txtTerm.Text.ToString() });
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "updated_by", Value = HttpContext.Current.User.Identity.Name });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                //con.Close();
                GridViewExpenses.EditIndex = -1;
                ExpensesBindData();
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", "alert('Cannot update,  already exist');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            }
            finally
            {
                con.Close();
            }
        }

        protected void GridViewExpenses_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ms_expenses_delete", con);
                GridViewRow row = GridViewExpenses.Rows[e.RowIndex] as GridViewRow;
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "exp_id", Value = GridViewExpenses.Rows[e.RowIndex].Cells[0].Text });
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
                ExpensesBindData();
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





        protected void btnAddExpenses_Click(object sender, EventArgs e)
        {
            divAddExpenses.Visible = true;
            btnAddExpenses.Visible = false;
        }

        protected void BtnCancelExpenses_Click(object sender, EventArgs e)
        {
            divAddExpenses.Visible = false;
            btnAddExpenses.Visible = true;
        }

        //protected void btnSearchExpenses_Click(object sender, EventArgs e)
        //{
        //    ExpensesBindData();
        //}

        protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownTerm();
            ddlTerm.SelectedIndex = 0;
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExpensesBindData();
        }

        protected void BtnSaveNAddExp_Click(object sender, EventArgs e)
        {
            SaveExpenses();
            ExpensesBindData();
            divAddExpenses.Visible = true;
            btnAddExpenses.Visible = false;
        }
    }
}
