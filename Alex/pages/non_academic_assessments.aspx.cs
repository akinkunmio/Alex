using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Configuration;
using Alex.App_code;

namespace Alex.pages
{
    public partial class non_academic_assessments : System.Web.UI.Page
    {
         int lvl = 0;
        
         protected void Page_Load(object sender, EventArgs e)
         {
             ManageCookies.VerifyAuthentication();
             Level();
             if (lvl == 1 || lvl == 2 || lvl == 3 || lvl == 4)
             {
                 if (!IsPostBack)
                 {
                     DropDownFormClass();
                     divEdit.Visible = false;
                 }
             }
             else if ( lvl == 5)
             {
                 Response.Redirect("~/pages/ForbiddenAccess.aspx", false);
             }
             else
             {
                 Response.Redirect("~/pages/login.aspx", false);
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



         public void DropDownFormClass()
         {
             SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            String strQuery = "SELECT DISTINCT form_name+ [class_name] as form_class,class_id FROM [dbo].[ms_classes], ms_forms where ms_classes.form_id =ms_forms.form_id order by form_class";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strQuery;
            cmd.Connection = con;
            try
            {
               con.Open();
               ddlFormClass.DataSource = cmd.ExecuteReader();
               ddlFormClass.DataValueField = "class_id";
               ddlFormClass.DataTextField = "form_class";
               ddlFormClass.DataBind();
               ddlFormClass.Items.Insert(0, new ListItem("Please select Class", ""));
             }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
         }



        

         private void BindGrid()
         {
             SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
             try
             {
                 using (SqlCommand cmd = new SqlCommand("sp_ms_assessment2_list_all", con))
                 {
                     using (SqlDataAdapter sda = new SqlDataAdapter())
                     {
                         con.Open();
                         cmd.Connection = con;
                         cmd.CommandType = CommandType.StoredProcedure;

                         cmd.Parameters.AddWithValue("@class", ddlFormClass.SelectedItem.ToString());
                        
                         
                         sda.SelectCommand = cmd;
                         using (DataTable dt = new DataTable())
                         {
                             sda.Fill(dt);
                             GridViewAssessments.DataSource = dt;
                             if (dt.Rows.Count == 0)
                             {
                                 GridViewAssessments.Visible = false;
                                 lblZeroRecords.Visible = true;
                                 lblZeroRecords.Text = "No student registrations found for this class to Mark Non-Academic Assessments, please register students to the class";
                                 btnEdit.Visible = false;
                                 btnUploadAssessments.Visible = false;
                             }
                             else
                             {
                                 GridViewAssessments.Visible = true;
                                 GridViewAssessments.DataBind();
                                
                                
                                 lblZeroRecords.Visible = false;
                                 divEdit.Visible = true;
                                 btnEdit.Visible = true;
                                 btnUploadAssessments.Visible = false;
                                 btnCancel.Visible = false;
                                
                             }
                         }
                     }
                 }
             }
             catch (Exception ex)
             {
                 ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);

             }
             finally
             {
                 con.Close();
             }
         }
         protected void GridViewAssessments_RowDataBound(object sender, GridViewRowEventArgs e)
         {

             if (e.Row.RowType == DataControlRowType.DataRow)
             {
                 HyperLink hl = (HyperLink)e.Row.FindControl("HyperLinkPeople");
                 if (hl != null)
                 {
                     DataRowView drv = (DataRowView)e.Row.DataItem;
                     string keyword = drv["person_id"].ToString();
                     string regid = drv["reg_id"].ToString();
                     hl.NavigateUrl = "~/pages/profile.aspx?PersonId=" + keyword + "&Regid=" + regid + "&action=ast";
                 }
             }
         }



         protected void btnUploadAssessments_Click(object sender, EventArgs e)
         {
             SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
             try
             {
                 foreach (GridViewRow row in GridViewAssessments.Rows)
                 {
                    var comment1 = row.FindControl("tbAssessment1") as TextBox;
                    var comment2 = row.FindControl("tbAssessment2") as TextBox;
                    var textbox1 = row.FindControl("dd1") as DropDownList;
                    var Attentiveness = textbox1.Text;

                    var textbox2 = row.FindControl("dd2") as DropDownList;
                    var Honesty = textbox2.Text;

                    var textbox3 = row.FindControl("dd3") as DropDownList;
                    var Neatness = textbox3.Text;

                    var textbox4 = row.FindControl("dd4") as DropDownList;
                    var Politeness = textbox4.Text;

                    var textbox5 = row.FindControl("dd5") as DropDownList;
                    var Punctuality = textbox5.Text;

                    var textbox6 = row.FindControl("dd6") as DropDownList;
                    var Relationship_with_others = textbox6.Text;

                    var textbox7 = row.FindControl("dd7") as DropDownList;
                    var Club_societies = textbox7.Text;

                    var textbox8 = row.FindControl("dd8") as DropDownList;
                    var Drawing_and_painting = textbox8.Text;

                    var textbox9 = row.FindControl("dd9") as DropDownList;
                    var Hand_writing = textbox9.Text;
                     
                    var textbox10 = row.FindControl("dd10") as DropDownList;
                    var Hobbies = textbox10.Text;

                    var textbox11 = row.FindControl("dd11") as DropDownList;
                    var Speech_fluency = textbox11.Text;

                    var textbox12 = row.FindControl("dd12") as DropDownList;
                    var Sports_and_games = textbox12.Text;


                    using (SqlCommand cmd = new SqlCommand("sp_ms_assessment2_edit", con))
                     {
                         con.Open();
                         string RegId = row.Cells[0].Text;
                         cmd.Parameters.Add("@reg_id", SqlDbType.VarChar, 50).Value = RegId;
                         cmd.Parameters.Add("@comment1", SqlDbType.VarChar, 120).Value = comment1.Text.ToString();
                         cmd.Parameters.Add("@comment2", SqlDbType.VarChar, 120).Value = comment2.Text.ToString();
                         cmd.Parameters.Add("@attentiveness", SqlDbType.VarChar, 50).Value = Attentiveness;
                         cmd.Parameters.Add("@honesty", SqlDbType.VarChar, 50).Value = Honesty;
                         cmd.Parameters.Add("@neatness", SqlDbType.VarChar, 50).Value = Neatness;
                         cmd.Parameters.Add("@politeness", SqlDbType.VarChar, 50).Value = Politeness;
                         cmd.Parameters.Add("@punctuality", SqlDbType.VarChar, 50).Value = Punctuality;
                         cmd.Parameters.Add("@relationship_with_others", SqlDbType.VarChar, 50).Value =Relationship_with_others ;
                         cmd.Parameters.Add("@club_societies", SqlDbType.VarChar, 50).Value = Club_societies;
                         cmd.Parameters.Add("@drawing_and_painting", SqlDbType.VarChar, 50).Value = Drawing_and_painting;
                         cmd.Parameters.Add("@hand_writing", SqlDbType.VarChar, 50).Value = Hand_writing;
                         cmd.Parameters.Add("@hobbies", SqlDbType.VarChar, 50).Value = Hobbies ;
                         cmd.Parameters.Add("@speech_fluency", SqlDbType.VarChar, 50).Value = Sports_and_games;
                         cmd.Parameters.Add("@sports_and_games", SqlDbType.VarChar, 50).Value = Sports_and_games;
                          
                        
                         cmd.Parameters.AddWithValue("@updated_by", HttpContext.Current.User.Identity.Name);
                         


                         cmd.CommandType = CommandType.StoredProcedure;
                         cmd.ExecuteNonQuery();

                         BindGrid();
                         btnEdit.Visible = true;
                         btnCancel.Visible = false;
                         btnUploadAssessments.Visible = false;
                         con.Close();
                     }
                 }
             }

             catch (Exception ex)
             {
                 if (ex.Message.Contains("Object cannot be cast from DBNull to other types"))
                 {
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Cannot save assessments, weighting should setup for the term assessment ')", true);
                 }
                 else
                 {
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                 }
             }
             finally
             {
                 con.Close();
             }
         }
         protected void btnEdit_Click(object sender, EventArgs e)
         {
             btnUploadAssessments.Visible = true;
             btnEdit.Visible = false;
             btnCancel.Visible = true;
             foreach (GridViewRow row in GridViewAssessments.Rows)
             {
                 ((TextBox)row.FindControl("tbAssessment1")).Enabled = true;
                 ((TextBox)row.FindControl("tbAssessment2")).Enabled = true;
                 string text = "";
                 for (int i = 1; i <= 12; i++)
                 {
                     text = "dd" + i;
                     ((DropDownList)row.FindControl(text)).Enabled = true;
                 } 

             }
         }


         protected void ddlFormClass_SelectedIndexChanged(object sender, EventArgs e)
         {
             BindGrid();
             btnCancel.Visible = false;
         }

         protected void btnCancel_Click(object sender, EventArgs e)
         {
             BindGrid();
             btnCancel.Visible = false;
             btnEdit.Visible = true;
             btnUploadAssessments.Visible = false;
         }
    }
}