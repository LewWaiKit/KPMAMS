using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPMAMS
{
    public partial class TSPSite : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["role"].Equals(""))
                {
                    Response.Write("<script>alert('Please login first!');</script>");
                    Response.Redirect("Login.aspx");
                } else if (Session["role"].Equals("p"))
                {
                    hlForum.Visible = false;
                }
                lbProfile.Text = "Hello " + Session["fullName"].ToString();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

            String activePage = Request.RawUrl;
            if (activePage.Contains("ForumList.aspx") || activePage.Contains("ForumDetails") || activePage.Contains("CreateForum")){
                hlForum.Attributes.Add("class", "nav-link active");
            }
        }

        protected void lbLogout_Click(object sender, EventArgs e)
        {
            Session["userGUID"] = "";
            Session["fullName"] = "";
            Session["role"] = "";
            Response.Redirect("Login.aspx");
        }
    }
}