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
    /// Summary description for EmpProfilePicHandler
    /// </summary>
    public class EmpProfilePicHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            string EmpId = HttpContext.Current.Request.QueryString["EmployeeId"];
            if (EmpId != null)
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
                using (SqlCommand cmd = new SqlCommand("sp_ms_hr_employee_profile_picture", conn))
                {
                    cmd.Parameters.Add("@emp_id", SqlDbType.Int).Value = EmpId;
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    object data = cmd.ExecuteScalar();
                    conn.Close();
                    if (data.GetType().Name == "Byte[]")
                    { context.Response.BinaryWrite((byte[])data); }
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