using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alex.App_code;

namespace Alex.pages.admin
{
    public partial class setup_payroll : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            if (DetailsViewSetupPayroll.CurrentMode == DetailsViewMode.Edit)
            {
                var UserName = HttpContext.Current.User.Identity.Name;
                SqlDataSourceSetupPayroll.UpdateParameters["updated_by"].DefaultValue = UserName;
            }
            if (DetailsViewSetupPayroll.CurrentMode == DetailsViewMode.Insert)
            {
                var UserName = HttpContext.Current.User.Identity.Name;
                SqlDataSourceSetupPayroll.InsertParameters["created_by"].DefaultValue = UserName;
            }
        }

        protected void DetailsViewSetupPayroll_ItemCreated(object sender, EventArgs e)
        {
            if (DetailsViewSetupPayroll.CurrentMode == DetailsViewMode.ReadOnly)
            {
                int commandRowIndex = DetailsViewSetupPayroll.Rows.Count - 1;
                if (commandRowIndex > 0)
                {
                    DetailsViewRow commandRow = DetailsViewSetupPayroll.Rows[commandRowIndex];
                    DataControlFieldCell cell = (DataControlFieldCell)commandRow.Controls[0];
                    if (cell != null)
                    {
                        foreach (Control ctrl in cell.Controls)
                        {
                            if (ctrl is LinkButton)
                            {
                                if (((LinkButton)ctrl).CommandName == "New")
                                {
                                    ctrl.Visible = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        private double ParseDouble(string value)
        {
            double d = 0;
            if (!double.TryParse(value, out d))
            {
                return 0;
            }
            return d;
        }
        //protected void DetailsViewSetupPayroll_DataBound(object sender, EventArgs e)
        //{

        //    if (DetailsViewSetupPayroll.DataItemCount != 0)
            
        //    {
        //        string r0 = DetailsViewSetupPayroll.Rows[0].Cells[1].Text;
        //        string r1 = DetailsViewSetupPayroll.Rows[1].Cells[1].Text;
        //        string r2 = DetailsViewSetupPayroll.Rows[2].Cells[1].Text;
        //        string r3 = DetailsViewSetupPayroll.Rows[3].Cells[1].Text;
        //        string r4 = DetailsViewSetupPayroll.Rows[4].Cells[1].Text;
        //        string r5 = DetailsViewSetupPayroll.Rows[5].Cells[1].Text;
        //        string r6 = DetailsViewSetupPayroll.Rows[6].Cells[1].Text;
        //        string r7 = DetailsViewSetupPayroll.Rows[7].Cells[1].Text;
        //        string r8 = DetailsViewSetupPayroll.Rows[8].Cells[1].Text;
        //        string r9 = DetailsViewSetupPayroll.Rows[9].Cells[1].Text;
        //        string r10 = DetailsViewSetupPayroll.Rows[10].Cells[1].Text;

        //        string r11 = DetailsViewSetupPayroll.Rows[11].Cells[1].Text;
        //        string r12 = DetailsViewSetupPayroll.Rows[12].Cells[1].Text;
        //        var Balance = ParseDouble(r0 + r1 + r2 + r3 + r4 + r5 + r6 + r7 + r8 + r9 + r10 + r11 + r12);

        //        //Label lblTotal = (Label)DetailsViewSetupPayroll.Rows[0].Cells[1].Controls[0];
        //        Double lblTotal = Balance;
        //    }
           
            
           
       // }
    }
}