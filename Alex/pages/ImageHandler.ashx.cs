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
    /// Summary description for ImageHandler
    /// </summary>
    public class ImageHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            using (SqlCommand cmd = new SqlCommand("sp_ms_settings_school_logo_show", conn))
            {
                cmd.Parameters.Add("@school_id", SqlDbType.NVarChar, 40).Value = "1";
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                     object data = cmd.ExecuteScalar(); 
                conn.Close();
                context.Response.BinaryWrite((byte[])data);
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