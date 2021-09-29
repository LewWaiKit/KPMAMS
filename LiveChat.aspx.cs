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

        /*protected void btnSignOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }*/

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

        /*protected void btnChangePicModel_Click(object sender, EventArgs e)
        {

            string serverPath = HttpContext.Current.Server.MapPath("~/");
            //path = serverPath + path;
            if (FileUpload1.HasFile)
            {
                string FileWithPat = serverPath + @"images/DP/" + UserName + FileUpload1.FileName;

                FileUpload1.SaveAs(FileWithPat);
                SD.Image img = SD.Image.FromFile(FileWithPat);
                SD.Image img1 = RezizeImage(img, 151, 150);
                img1.Save(FileWithPat);
                if (File.Exists(FileWithPat))
                {
                    FileInfo fi = new FileInfo(FileWithPat);
                    string ImageName = fi.Name;
                    string query = "update tbl_Users set Photo='" + ImageName + "' where UserName='" + UserName + "'";
                    if (ConnC.ExecuteQuery(query))
                        UserImage = "images/DP/" + ImageName;
                }
            }
        }


        #region Resize Image With Best Qaulity

        private SD.Image RezizeImage(SD.Image img, int maxWidth, int maxHeight)
        {
            if (img.Height < maxHeight && img.Width < maxWidth) return img;
            using (img)
            {
                Double xRatio = (double)img.Width / maxWidth;
                Double yRatio = (double)img.Height / maxHeight;
                Double ratio = Math.Max(xRatio, yRatio);
                int nnx = (int)Math.Floor(img.Width / ratio);
                int nny = (int)Math.Floor(img.Height / ratio);
                Bitmap cpy = new Bitmap(nnx, nny, SD.Imaging.PixelFormat.Format32bppArgb);
                using (Graphics gr = Graphics.FromImage(cpy))
                {
                    gr.Clear(Color.Transparent);

                    // This is said to give best quality when resizing images
                    gr.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    gr.DrawImage(img,
                        new Rectangle(0, 0, nnx, nny),
                        new Rectangle(0, 0, img.Width, img.Height),
                        GraphicsUnit.Pixel);
                }
                return cpy;
            }

        }*/

        private MemoryStream BytearrayToStream(byte[] arr)
        {
            return new MemoryStream(arr, 0, arr.Length);
        }

        /*#endregion*/

    }

}