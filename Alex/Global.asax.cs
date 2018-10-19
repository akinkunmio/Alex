using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace WebApplicationAlex
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            Application["OnlineVisitors"] = 0;

            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
            {
                Path = "~/scripts/jquery-1.4.1.min.js",
                DebugPath = "~/scripts/jquery-1.4.1.js",
                CdnPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.4.1.min.js",
                CdnDebugPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.4.1.js"
            });
        }
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

            // look if any security information exists for this request
            if (HttpContext.Current.User != null)
            {
                // see if this user is authenticated, any authenticated cookie (ticket) exists for this user
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    // see if the authentication is done using FormsAuthentication
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        // Get the roles stored for this request from the ticket
                        // get the identity of the user
                        FormsIdentity identity = (FormsIdentity)HttpContext.Current.User.Identity;

                        // get the forms authetication ticket of the user
                        FormsAuthenticationTicket ticket = identity.Ticket;

                        // get the roles stored as UserData into the ticket
                        string[] roles = ticket.UserData.Split(',');

                        // create generic principal and assign it to the current request
                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(identity, roles);
                    }
                }
            }
        }

        void Application_Error(object sender, EventArgs e)
        {
            //Code that runs when an unhandled error occurs
            Response.Redirect("~/pages/404.aspx", true);
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
           
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
          //  Session.Abandon();
          //  Session.Clear();
            //Response.Redirect("~/pages/logout.aspx", true);
           
        }
    }
}

 