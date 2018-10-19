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
    public partial class purchases_date_item : System.Web.UI.Page
    {
        int lvl = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Level();
            ManageCookies.VerifyAuthentication();
            if (!Page.IsPostBack)
            {
                DropDownPaySaleItem();
                SchoolBind();
                //string TodayDate= DateTime.Today.ToString("dd-MM-yyyy");
                //var now = DateTime.Now;
                //var FirstDayofMonth = new DateTime(now.Year, now.Month, 1);
                //tbStartDate.Text = FirstDayofMonth.ToString("dd-MM-yyyy");
                //tbEndDate.Text = TodayDate;
                DateTime today = DateTime.Today;
                DateTime TwentyEightDaysEarlier = today.AddDays(-28);
                string TodayDate = DateTime.Today.ToString("dd-MM-yyyy");
                tbStartDate.Text = TwentyEightDaysEarlier.ToString("dd-MM-yyyy");
                tbEndDate.Text = TodayDate;
                ddlItemName.SelectedIndex = 1;
                Purchases();
                PurchasesSummary();
                
            }
            else if (lvl == 4)
            {
                Response.Redirect("~/pages/ForbiddenAccess.aspx", false);
            }
            else if (lvl == 2 || lvl == 3)
            {
                ToalAmountLabel.Visible = false;
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
       
        protected void BtnSearchPurchases_Click(object sender, EventArgs e)
        {
            PurchasesSummary();
            Purchases();
        }

        private void PurchasesSummary()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ms_purchases_filtered_by_date_item_summary", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@item_name", SqlDbType.VarChar).Value = ddlItemName.SelectedItem.ToString();
                        cmd.Parameters.Add("@received_date_start", SqlDbType.VarChar).Value = tbStartDate.Text.ToString();
                        cmd.Parameters.Add("@received_date_end", SqlDbType.VarChar).Value = tbEndDate.Text.ToString();
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            con.Open();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                if (dt.Rows.Count == 0)
                                {
                                   DivStudents.Visible = false;
                                }
                                else
                                {
                                    DivStudents.Visible = true;
                                    //lblUnitPrice.Text = dt.Rows[0]["unit_price"].ToString();
                                    //lblTotalQuanityPurchased.Text = dt.Rows[0]["total_quantity_purchased"].ToString();
                                    lblTotalAmountRecieved.Text = dt.Rows[0]["total_amount_recieved"].ToString();
                                    imgSchool.ImageUrl = "~/pages/ImageHandler.ashx?roll_no=1";
                                    lblItemSelected.Text = ddlItemName.SelectedItem.ToString();
                                    lblDateSelected.Text = tbStartDate.Text.ToString();
                                    lblEndDateSelected.Text = tbEndDate.Text.ToString();
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
                }
        }

        private void Purchases()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ms_purchases_filtered_by_date_item", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@item_name", SqlDbType.VarChar).Value = ddlItemName.SelectedItem.ToString();
                    cmd.Parameters.Add("@received_date_start", SqlDbType.VarChar).Value = tbStartDate.Text.ToString();
                    cmd.Parameters.Add("@received_date_end", SqlDbType.VarChar).Value = tbEndDate.Text.ToString();
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
                                lblZeroRecords.Visible = true;
                                lblZeroRecords.Text = "No Purchases Found ";
                                GridViewPurchases.Visible = false;
                                btnPrint.Visible = false;
                            }
                            else
                            {
                                lblZeroRecords.Visible = false;
                                GridViewPurchases.DataBind();
                                GridViewPurchases.Visible = true;
                                btnPrint.Visible = true;
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

       
    }
}