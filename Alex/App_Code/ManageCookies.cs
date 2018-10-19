using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Alex.App_code
{
    public class ManageCookies
    {
        public static void VerifyAuthentication(bool isLogout = false)
        {
            if (isLogout)
            {
                if (HttpContext.Current.Request.Cookies["loginCookie"] != null)
                {
                    HttpCookie loginCookie = new HttpCookie("loginCookie");
                    loginCookie.Value = "";
                    loginCookie.Expires = DateTime.Now.AddYears(-1);
                    HttpContext.Current.Response.Cookies.Add(loginCookie);
                }
                HttpContext.Current.Response.Redirect("~/pages/login.aspx", true);
            }
                else
                {
                    //No Cache on Page
                    HttpContext.Current.Response.AddHeader("Cache-control", "no-store, must-revalidate, private,no-cache");
                    HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
                    HttpContext.Current.Response.AddHeader("Expires", "0");
                    //Response.Cache.SetCacheability(HttpCacheability.NoCache);

                    //Verify if cookie exists

                    //FormsIdentity identity = (FormsIdentity)HttpContext.Current.User.Identity;pk
                    //FormsAuthenticationTicket ticket = identity.Ticket;pk
                    //string[] roles = ticket.UserData.Split(',');pk

                    if (HttpContext.Current.Request.Cookies["loginCookie"] == null)
                    {
                        //Authentication failed. redirect User to Login Page
                        HttpContext.Current.Response.Redirect("~/pages/login.aspx", true);
                    }
                    //else if (!roles.Contains("UserManagement"))pk
                    else if (HttpContext.Current.Request.Cookies["loginCookie"].Value != "UserManagement")
                    {

                        //Authentication failed. redirect User to Login Page
                        HttpContext.Current.Response.Redirect("~/pages/login.aspx", true);
                    }
            }
        }

        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.Encoding.UTF8.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }
        public static string DecodeFrom64(string encodedData)
        {
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);
            string returnValue = System.Text.Encoding.UTF8.GetString(encodedDataAsBytes);
            return returnValue;
        }


    }
}