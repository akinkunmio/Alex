using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
//using Alex.App_code; 

namespace Alex.pages.admin
{
    public partial class school_logo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ManageCookies.VerifyAuthentication();
        }

        private Boolean InsertUpdateData(SqlCommand cmd)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                return false;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
        
        protected void Upload_Click(object sender, EventArgs e)
        {
            // Read the file and convert it to Byte Array
            string filePath = FileUpload1.PostedFile.FileName;
            //Is the file too big to upload?
            int fileSize = FileUpload1.PostedFile.ContentLength;
            if (fileSize > (200 * 1024))
            {
                lblStatuslogo.Visible = true;
                lblStatuslogo.ForeColor = System.Drawing.Color.Red;
                lblStatuslogo.Text = "Filesize of image is too large. Maximum file size permitted is " + 200 + "KB";
                return;
            }
            string filename = Path.GetFileName(filePath);
            string ext = Path.GetExtension(filename);
            string contenttype = String.Empty;

            //Set the contenttype based on File Extension
            switch (ext)
            {
                //case ".doc":
                //    contenttype = "application/vnd.ms-word";
                //    break;
                //case ".docx":
                //    contenttype = "application/vnd.ms-word";
                //    break;
                //case ".xls":
                //    contenttype = "application/vnd.ms-excel";
                //    break;
                //case ".xlsx":
                //    contenttype = "application/vnd.ms-excel";
                //    break;
                case ".jpg":
                    contenttype = "image/jpg";
                    break;
                case ".JPG":
                    contenttype = "image/JPG";
                    break;
                case ".png":
                    contenttype = "image/png";
                    break;
                case ".gif":
                    contenttype = "image/gif";
                    break;
                //case ".pdf":
                //    contenttype = "application/pdf";
                //    break;
                case ".jpeg":
                    contenttype = "image/jpeg";
                    break;
            }
            if (contenttype != String.Empty)
            {

                Stream fs = FileUpload1.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                Byte[] bytes = br.ReadBytes((Int32)fs.Length);

                //insert the file into database
                string strQuery = "delete from ms_school_logo where logo_id = 1" + "insert into ms_school_logo(logo_id,logo) values (@logo_id,@logo)";
                SqlCommand cmd = new SqlCommand(strQuery);
                cmd.Parameters.Add("@logo_id", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@logo", SqlDbType.Binary).Value = bytes;
                InsertUpdateData(cmd);
                lblStatuslogo.ForeColor = System.Drawing.Color.Green;
                lblStatuslogo.Text = "Logo Uploaded Successfully";
                cmd.Connection.Close();
                cmd.Connection.Dispose();

            }
            else
            {
                lblStatuslogo.ForeColor = System.Drawing.Color.Red;
                lblStatuslogo.Text = "File format not recognised." +
                  " Upload JPEG/JPG/PNG/GIF formats";
            }

        }

        protected void btnUploadBgLogo_Click(object sender, EventArgs e)
        {
            string filePath = FileUpload2.PostedFile.FileName;
            //Is the file too big to upload?
            int fileSize = FileUpload1.PostedFile.ContentLength;
            if (fileSize > (200 * 1024))
            {
                lblStatus.Visible = true;
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = "Filesize of image is too large. Maximum file size permitted is " + 200 + "KB";
                return;
            }
            string filename = Path.GetFileName(filePath);
            string ext = Path.GetExtension(filename);
            string contenttype = String.Empty;

            //Set the contenttype based on File Extension
            switch (ext)
            {
               
                case ".jpg":
                    contenttype = "image/jpg";
                    break;
                case ".JPG":
                    contenttype = "image/JPG";
                    break;
                case ".png":
                    contenttype = "image/png";
                    break;
                case ".gif":
                    contenttype = "image/gif";
                    break;
                case ".jpeg":
                    contenttype = "image/jpeg";
                    break;
               
            }
            if (contenttype != String.Empty)
            {

                Stream fs = FileUpload2.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                Byte[] bytes = br.ReadBytes((Int32)fs.Length);

                //insert the file into database
                string strQuery = "update ms_school_logo set bg_logo=@bg_logo where logo_id = 1";
                SqlCommand cmd = new SqlCommand(strQuery);
               // cmd.Parameters.Add("@logo_id", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@bg_logo", SqlDbType.Binary).Value = bytes;
                InsertUpdateData(cmd);
                lblStatus.ForeColor = System.Drawing.Color.Green;
                lblStatus.Text = "Background Image Uploaded Successfully";
                cmd.Connection.Close();
                cmd.Connection.Dispose();

            }
            else
            {
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = "File format not recognised." +
                  " Upload JPEG/JPG/PNG/GIF formats";
            }
        }

    }
}