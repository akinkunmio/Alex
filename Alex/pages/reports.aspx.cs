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
    public partial class reports : System.Web.UI.Page
    {
        int lvl = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            Level();
            if (lvl == 1)
            {
                divStudents.Visible = true;
                divSales.Visible = true;
                divEmployee.Visible = true;
                divAccounts.Visible = true;
                divrep1.Visible = true;
                divrep2.Visible = true;
                divFeePaymentsDates.Visible = true;
                divProfit_loss.Visible = true;
                divSaleItemPayments.Visible = true;
                divSalesDates.Visible = true;
                divMonthlySalary.Visible = true;
            }
            else if (lvl == 2)
            {
                //divStudents.Visible = true;
                //divSales.Visible = true;
                //divSalesDates.Visible = false;
                //divEmployee.Visible = false;
                //divAccounts.Visible = true;
                //divrep2.Visible = false;
                //divrep1.Visible = false;
                //divFeePaymentsDates.Visible = false;
                //divProfit_loss.Visible = false;
                //divSaleItemPayments.Visible = false;
                //divMonthlySalary.Visible = false;
                divStudents.Visible = true;
                DivAccFee.Visible = false;
                divSales.Visible = false;
                divAccounts.Visible = false;
                divEmployee.Visible = false;
            }
            else if (lvl == 3)
            {
                divStudents.Visible = true;
                DivAccFee.Visible = false;
                divSales.Visible = false;
                divAccounts.Visible = false;
                divEmployee.Visible = false;
            }
           else
            {
                Response.Redirect("~/pages/404.aspx", false);
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
    }
       

    
}