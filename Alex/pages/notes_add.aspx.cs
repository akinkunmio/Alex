using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
//using Alex.App_code;

namespace Alex.pages
{
    public partial class notes_add : System.Web.UI.Page
    {
        string RegID = string.Empty;
        string PersonID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //RegID = Request.QueryString["reg_id"].ToString();
        }

        protected void btnAddNotesForm_Click(object sender, EventArgs e)
        {
            RegID = Request.QueryString["reg_id"].ToString();
            PersonID = Request.QueryString["PID"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ms_person_notes_add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@reg_id", RegID);
                cmd.Parameters.AddWithValue("@comment_1", tbComment1.Text.ToString());
                //cmd.Parameters.AddWithValue("@comment_2", tbComment2.Text.ToString());
                cmd.Parameters.AddWithValue("@note_date", tbAddNotesDate.Text.ToString());
                cmd.Parameters.AddWithValue("@note_type", tbNoteType.Text.ToString());
                cmd.Parameters.AddWithValue("@created_by", HttpContext.Current.User.Identity.Name);
                cmd.ExecuteNonQuery();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Notes Saved Successfully');", true);
                Response.Redirect("~/pages/profile.aspx?Personid=" + PersonID + "&Regid=" + RegID + "&action=note");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Cannot Add Duplicate Record');", true);
            }
            finally
            {
                con.Close();
            }

        }

        protected void btnAddNotesCancel_Click(object sender, EventArgs e)
        {
            RegID = Request.QueryString["reg_id"].ToString();
            PersonID = Request.QueryString["PID"].ToString();
            Response.Redirect("~/pages/profile.aspx?Personid=" + PersonID + "&Regid=" + RegID + "&action=note");
        }
    }
}