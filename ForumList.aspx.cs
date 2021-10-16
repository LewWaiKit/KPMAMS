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
    public partial class ForumList : System.Web.UI.Page
    {
        string strCon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                if (!(Session["userGUID"]!=null))
                {
                    Response.Redirect("Login.aspx");
                }
                CheckRole();
            }
        }
        protected void BindClasses()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(strCon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * from Forum where ForumGUID =''", con);
                if (Session["role"].Equals("s"))
                {
                    cmd = new SqlCommand("SELECT s.ClassroomGUID, Class From Student s LEFT JOIN Classroom c ON c.ClassroomGUID = s.ClassroomGUID WHERE StudentGUID=@StudentGUID", con);
                    cmd.Parameters.AddWithValue("@StudentGUID", Session["userGUID"]);
                }
                else
                {
                    cmd = new SqlCommand("SELECT tc.ClassroomGUID, Class From Teacher_Classroom tc LEFT JOIN Classroom c ON c.ClassroomGUID = tc.ClassroomGUID WHERE TeacherGUID=@TeacherGUID", con);
                    cmd.Parameters.AddWithValue("@TeacherGUID", Session["userGUID"]);
                }
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dt.Load(dr);
                    dlClassList.DataTextField = "Class";
                    dlClassList.DataValueField = "ClassroomGUID";
                    dlClassList.DataSource = dt;
                    dlClassList.DataBind();
                    BindGridView();
                }
                else
                {
                    Response.Write("<script>alert('Error:no forum exist');</script>");
                }
                con.Close();

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void CheckRole()
        {
            if (Session["role"].Equals("t"))
            {
                btnCreateForum.Visible = true;
                dlClassList.Visible = true;
            }
                BindClasses();
        }

        protected void btnCreateForum_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateForum.aspx");
;        }

        protected void GvForumList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hyperLink = e.Row.FindControl("hlView") as HyperLink;
                if (hyperLink != null)
                    hyperLink.Attributes["href"] = "ForumDetails.aspx" + "?ForumGUID=" + DataBinder.Eval(e.Row.DataItem, "ForumGUID");
            }
        }


        protected void BindGridView()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(strCon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                
                //String strSelect = "SELECT f.ForumGUID, convert(VARCHAR(20),f.CreateDate,100) as CreateDate, convert(VARCHAR(20),f.LastUpdateDate,100) as LastUpdateDate, ClassroomGUID, Title, Count(CommentGUID) As NoOfComment From Forum f LEFT JOIN Comment c ON f.ForumGUID = c.ForumGUID Where ClassroomGUID=@ClassroomGUID GROUP BY CommentGUID, f.ForumGUID, f.CreateDate, f.LastUpdateDate, ClassroomGUID, Title";
                String strSelect = "SELECT f.ForumGUID, '<b>'+FullName+'</b>'+' On '+convert(VARCHAR(20),f.CreateDate,100) as CreateBy, convert(VARCHAR(20),f.LastUpdateDate,100) as LastUpdateDate, f.ClassroomGUID, Title, Count(CommentGUID) As NoOfComment From Teacher t LEFT JOIN Teacher_Classroom tc ON t.TeacherGUID =tc.TeacherGUID LEFT JOIN Classroom cl ON tc.ClassroomGUID = cl.ClassroomGUID LEFT JOIN Forum f ON cl.ClassroomGUID = f.ClassroomGUID LEFT JOIN Comment c ON f.ForumGUID = c.ForumGUID Where f.ClassroomGUID=@ClassroomGUID AND t.TeacherGUID=AuthorGUID GROUP BY f.ForumGUID, f.CreateDate, f.LastUpdateDate, f.ClassroomGUID, Title, FullName";
                SqlCommand cmd = new SqlCommand(strSelect, con);
                cmd.Parameters.AddWithValue("@ClassroomGUID", dlClassList.SelectedValue);
                SqlDataReader dr = cmd.ExecuteReader();
                lbClass.Text = "Class : " +dlClassList.SelectedItem;
                dt.Load(dr);
                con.Close();

                if (dt.Rows.Count == 0)
                {
                    //lblNoData.Visible = true;
                    GvForumList.DataSource = dt;
                    GvForumList.DataBind();
                }
                else
                {
                    GvForumList.DataSource = dt;
                    GvForumList.DataBind();
                }

                con.Close();
            }
            catch (SqlException ex)
            {
   
                string msg = ex.Message;
                Response.Write(msg);
            }
        }

        protected void dlClassList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView();
        }
    }
    
}