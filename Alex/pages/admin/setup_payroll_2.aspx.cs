using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alex.App_code;


namespace Alex.pages.admin
{
    public partial class setup_payroll_2 : System.Web.UI.Page
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
    }
}