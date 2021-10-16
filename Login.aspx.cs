using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPMAMS
{
    public partial class Login : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
               
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * from Teacher where TeacherUserID='' AND Password=''", con); 
                if (tbUserID.Text.Substring(0, 1) == "t")
                {
                    cmd = new SqlCommand("SELECT * from Teacher where TeacherUserID='" + tbUserID.Text.Trim() + "' AND Password='" + tbPassword.Text.Trim() + "'", con);
                }
                else if(tbUserID.Text.Substring(0, 1) == "p")
                {
                    cmd = new SqlCommand("SELECT * from Parent where ParentUserID='" + tbUserID.Text.Trim() + "' AND Password='" + tbPassword.Text.Trim() + "'", con);
                }
                else if (tbUserID.Text.Substring(0, 1) == "s")
                {
                    cmd = new SqlCommand("SELECT * from Student where StudentUserID='" + tbUserID.Text.Trim() + "' AND Password='" + tbPassword.Text.Trim() + "'", con);
                }
                SqlDataReader dr = cmd.ExecuteReader();
                
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Session["userGUID"] = dr.GetValue(0).ToString();
                        Session["fullName"] = dr.GetValue(3).ToString();
                        Session["role"] = tbUserID.Text.Substring(0, 1);
                    }
                    Response.Redirect("ForumList.aspx");
                }
                else
                {
                    Response.Write("<script>alert('Invalid UserID or Password');</script>");
                }
                
                    con.Close();
                
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
                Response.Write("<script>alert('Plase insert userID or password');</script>");
            }
        }


    }
}