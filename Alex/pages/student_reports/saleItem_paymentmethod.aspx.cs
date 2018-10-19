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


namespace Alex.pages.student_reports
{
    public partial class saleItem_paymentmethod : System.Web.UI.Page
    {
        int lvl = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
          
           if (!Page.IsPostBack)
              {
                Level();
                ManageCookies.VerifyAuthentication(); 
                DropDownPaySaleItem(); DropDownBankName();
                SchoolBind();
               
                DateTime today = DateTime.Today;
                DateTime TwentyEightDaysEarlier = today.AddDays(-28);
                string TodayDate = DateTime.Today.ToString("dd-MM-yyyy");
                tbStartDate.Text = TwentyEightDaysEarlier.ToString("dd-MM-yyyy");
                tbEndDate.Text = TodayDate;
                ddlItemName.SelectedIndex = 1;
                ddlPaymentMethod.SelectedIndex = 5;
                if (ddlPaymentMethod.SelectedValue == "Bank")
                {
                    ddlBankName.SelectedIndex = 0; bank.Visible = true;
                }
                else { bank.Visible = false; }
                PurchasesSummary();

                
            }
            else if (lvl == 2 || lvl == 3 || lvl == 4 || lvl ==5)
            {
                Response.Redirect("~/pages/ForbiddenAccess.aspx", false);
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

        public void DropDownBankName()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_status_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@category", "Bank");
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlBankName.DataSource = ddlValues;
            ddlBankName.DataValueField = "status_name";
            ddlBankName.DataTextField = "status_name";
            ddlBankName.DataBind();
            ddlBankName.Items.Insert(0, new ListItem("Please select Bank Name", ""));
            ddlBankName.Items.Insert(1, new ListItem("All", "ALL"));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
        protected void BtnSearchPurchases_Click(object sender, EventArgs e)
        {
            PurchasesSummary();
            //if (ddlPaymentMethod.SelectedValue == "Bank")
            //{
            //    bank.Visible = true;
            //}
            //else { bank.Visible = false; }
        }

        private void PurchasesSummary()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_rep_item_payments_list_date_filterv2", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //if (ddlBankName.SelectedItem.Text == "Please select Bank Name") { cmd.Parameters.Add("@bank_name ", SqlDbType.VarChar).Value = DBNull.Value; bank.Visible = false; }
                        //else { bank.Visible = true; cmd.Parameters.Add("@bank_name ", SqlDbType.VarChar).Value = ddlBankName.SelectedItem.ToString(); }
                        cmd.Parameters.Add("@item_name", SqlDbType.VarChar).Value = ddlItemName.SelectedItem.ToString();
                        cmd.Parameters.Add("@payment_method ", SqlDbType.VarChar).Value = ddlPaymentMethod.SelectedItem.ToString();
                        cmd.Parameters.Add("@pay_date_start", SqlDbType.VarChar).Value = tbStartDate.Text.ToString();
                        cmd.Parameters.Add("@pay_date_end", SqlDbType.VarChar).Value = tbEndDate.Text.ToString();
                        cmd.Parameters.Add("@bank_name ", SqlDbType.VarChar).Value = ddlBankName.SelectedItem.ToString(); 
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                GridViewPurchases.DataSource = dt;
                                if (dt.Rows.Count == 0)
                                {
                                    DivStudents.Visible = false;
                                    lblZeroRecords.Visible = true;
                                    lblZeroRecords.Text = "No Purchases Found ";
                                    GridViewPurchases.Visible = false;
                                    btnPrint.Visible = false;
                                }
                                else
                                {
                                    DivStudents.Visible = true;
                                    
                                    imgSchool.ImageUrl = "~/pages/ImageHandler.ashx?roll_no=1";
                                    lblItemSelected.Text = ddlPaymentMethod.SelectedItem.ToString();
                                    lblDateSelected.Text = tbStartDate.Text.ToString();
                                    lblEndDateSelected.Text = tbEndDate.Text.ToString();
                                    lblZeroRecords.Visible = false;
                                    GridViewPurchases.Visible = true;
                                    btnPrint.Visible = true;
                                    if (ddlPaymentMethod.SelectedItem.Text != "Bank") { GridViewPurchases.Columns[6].Visible = false; } else { GridViewPurchases.Columns[6].Visible = true; }
                                    if (ddlPaymentMethod.SelectedItem.Text != "All") { GridViewPurchases.Columns[7].Visible = false; } else { GridViewPurchases.Columns[7].Visible = true; }
                                    GridViewPurchases.DataBind();
                                }
                            }
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
                    //bank.Visible = true;
                }
        }

       
        public void SchoolBind()
        {
            SqlDataAdapter adp = new SqlDataAdapter();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select school_name from ms_school", con);
                cmd.Dispose();
                SqlDataReader dr = cmd.ExecuteReader();
                string SchoolName = string.Empty;
                while (dr.Read())
                {
                    SchoolName = dr["school_name"].ToString();
                    lblName.Text = SchoolName.ToString();
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

        protected void ddlPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DivStudents.Visible = false;
            //btnPrint.Visible = false;
            GridViewPurchases.Visible = false;
            if (ddlPaymentMethod.SelectedValue == "Bank")
            {
               ddlBankName.SelectedIndex = 0; bank.Visible = true;
            }
            else { bank.Visible = false; }
            //PurchasesSummary();
        }

        public void DropDownPaySaleItem()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("sp_ms_sales_items_dropdown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader ddlValues;
            ddlValues = cmd.ExecuteReader();

            ddlItemName.DataSource = ddlValues;
            ddlItemName.DataValueField = "item_name";
            ddlItemName.DataTextField = "item_name";
            ddlItemName.DataBind();

            ddlItemName.Items.Insert(0, new ListItem("Please select Item", ""));
            ddlItemName.Items.Insert(1, new ListItem("All"));
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

    }
}