using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;


namespace KPMAMS
{
    public partial class LiveChat : System.Web.UI.Page
    {
        public string UserName = "admin";
        public string UserImage = "img/logoKPM.png";
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["userGUID"] != null))
            {
                Response.Redirect("Login.aspx");
            }
            if (Session["fullName"] != null)
            {
                UserName = Session["fullName"].ToString();
                if (Session["role"].Equals("t"))
                {
                    GetUserImage(UserName);
                }
            }
            else
                Response.Redirect("Login.aspx");
        }
        public void GetUserImage(string Username)
        {
            if (Username != null && Session["role"].Equals("t"))
            {
                try {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("SELECT ProfilePic from Teacher where FullName='" + UserName + "'", con);

                    SqlDataReader dr = cmd.ExecuteReader();

                    String ImageName = "";

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ImageName = dr.GetValue(0).ToString();
                            /*UserImage = ImageName;*/
                        }
                    }
                    con.Close();
                } catch (Exception ex) {

                    Response.Write(ex.Message);

                }
            }
        }




        private MemoryStream BytearrayToStream(byte[] arr)
        {
            return new MemoryStream(arr, 0, arr.Length);
        }


    }

}