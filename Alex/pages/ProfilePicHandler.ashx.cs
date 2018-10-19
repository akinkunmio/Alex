using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Alex.pages
{
    /// <summary>
    /// Summary description for ProfilePicHandler
    /// </summary>
    public class ProfilePicHandler : IHttpHandler
    {
       
        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            string personId = HttpContext.Current.Request.QueryString["PersonId"];
            if (personId != null)
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
                using (SqlCommand cmd = new SqlCommand("sp_ms_person_profile_picture", conn))
                {
                    cmd.Parameters.Add("@person_id", SqlDbType.Int).Value = personId;
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    object data = cmd.ExecuteScalar();
                    conn.Close();
                    if (data.GetType().Name == "Byte[]")
                    { context.Response.BinaryWrite((byte[])data); }
                    //else
                    //{
                        //need  to display from local image (~\images\profileimage.png) !!?
                    //}
                    
                }
            }
           
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}