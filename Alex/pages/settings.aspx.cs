using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alex.App_code;

namespace Alex.pages
{
    public partial class settings : System.Web.UI.Page
    {
        int lvl = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            ManageCookies.VerifyAuthentication();
            Level();
            //Joe 230118 removed User Settings from level 2
            //if (lvl == 1 || lvl == 2 )
            if (lvl == 1)
            {
                divUserSettings.Visible = true;
            }
            else if (lvl == 3 || lvl == 2) //added level 2 to not see user settings
            {
                divUserSettings.Visible = false;
            }
            else if (lvl == 4)
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
    }
}