using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
//using Alex.App_code;

namespace Alex.pages.admin
{
    public partial class school_details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ManageCookies.VerifyAuthentication();
            //DetailsViewSchoolDetails.DefaultMode = DetailsViewMode.Edit;
            if (DetailsViewSchoolDetails.CurrentMode == DetailsViewMode.Edit)
            {
                var UserName = HttpContext.Current.User.Identity.Name;
                SqlDataSourceSchoolDetails.UpdateParameters["updated_by"].DefaultValue = UserName;
            }
            if (DetailsViewSchoolDetails.CurrentMode == DetailsViewMode.Insert)
            {
                var UserName = HttpContext.Current.User.Identity.Name;
                SqlDataSourceSchoolDetails.InsertParameters["created_by"].DefaultValue = UserName;
                //SqlDataSourceSchoolDetails.InsertParameters["updated_by"].DefaultValue = UserName;
            }
         }

        protected void DetailsViewSchoolDetails_ItemCreated(object sender, EventArgs e)
        {
            if (DetailsViewSchoolDetails.CurrentMode == DetailsViewMode.ReadOnly)
            {
                int commandRowIndex = DetailsViewSchoolDetails.Rows.Count - 1;
                if (commandRowIndex > 0)
                {
                    DetailsViewRow commandRow = DetailsViewSchoolDetails.Rows[commandRowIndex];
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

        

    }
}