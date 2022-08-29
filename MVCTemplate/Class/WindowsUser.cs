using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;
using System.Web.UI;
using MVCTemplate.Models;
using System.Collections;
namespace MVCTemplate.Class
{
    public class WindowsUser
    {
        public WindowsUserViewModel FindUser(string Username, string Password)
        {
            try
            {
                DirectoryEntry entry = new DirectoryEntry("LDAP://" + "AUTO", Username, Password);
                DirectorySearcher mySearcher = new DirectorySearcher(entry);
                mySearcher.Filter = "(&(objectClass=user)(|(cn=" + Username + ")(sAMAccountName=" + Username + ")))";
                SearchResult result = mySearcher.FindOne();

                if (result != null)
                {
                    try
                    {
                        return new WindowsUserViewModel
                        {
                            WindowsUser = result.Properties["samaccountname"][0].ToString(),
                            Name = result.Properties["displayname"][0].ToString(),
                            Mail = result.Properties["mail"][0].ToString(),
                            UserType = "Normal",
                            Department = result.Properties["department"][0].ToString()
                        };
                    }
                    catch (Exception)
                    {

                        return new WindowsUserViewModel
                        {
                            WindowsUser = result.Properties["samaccountname"][0].ToString(),
                            Name = result.Properties["displayname"][0].ToString(),
                            Mail = "josue.valenzuela@continental-corporation.com",
                            UserType = "Normal",
                            Department = ""

                        };
                    }
                }
            }
            catch (DirectoryServicesCOMException)
            {
            }
            return null;
        }

        public void SetUserSession(string username, string password, string LoginPage, string RedirectPage)
        {
            HttpContext.Current.Session["WindowsUser"] = null;

            try
            {

                WindowsUserViewModel WindowsUser;
                WindowsUser = FindUser(username, password);
                //Set user type here

                if (WindowsUser != null)
                {
                    //Valid Windows User
                    HttpContext.Current.Session["WindowsUser"] = WindowsUser;

                    if (HttpContext.Current.Session["url"] != null)
                    {
                        HttpContext.Current.Response.Redirect(HttpContext.Current.Session["url"].ToString(), false);
                        HttpContext.Current.Session["url"] = null;
                    }
                    else
                    {
                        //HttpContext.Current.Response.Redirect(RedirectPage, false);
                    }
                }
                else
                {
                    //Windows User not found or wrong password 
                    //HttpContext.Current.Response.Redirect(LoginPage + "?Err=NotFound", false);
                }

            }
            catch
            { //HttpContext.Current.Response.Redirect(LoginPage); }
            }
        }
    }
}