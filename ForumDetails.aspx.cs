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
    public partial class WebForm1 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        DateTime lastUpdateDate;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                LoadForum();
                BindGridView();
            }
        }

        protected void LoadForum()
        {
            try { 
                if (!(Request.QueryString["ForumGUID"] == null))
                {
                    SqlCommand cmd = new SqlCommand();
                    DataTable dt = new DataTable();
                    string strCon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                    SqlConnection con = new SqlConnection(strCon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    String select = "Select FullName, ProfilePic, cl.Class, Title, f.Content, convert(VARCHAR(20),f.CreateDate,100), convert(VARCHAR(20),f.LastUpdateDate,100) , AuthorGUID From Teacher t LEFT JOIN Teacher_Classroom tc ON t.TeacherGUID = tc.TeacherGUID LEFT JOIN Classroom cl ON tc.ClassroomGUID = cl.ClassroomGUID LEFT JOIN Forum f ON cl.ClassroomGUID = f.ClassroomGUID Where t.TeacherGUID = AuthorGUID AND ForumGUID = @ForumGUID";
                    cmd = new SqlCommand(select, con);
                    cmd.Parameters.AddWithValue("@ForumGUID", Request.QueryString["ForumGUID"]);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dt.Load(dr);
                        con.Close();
                        //Stored to label here
                        lbTitle.Text = dt.Rows[0][3].ToString();
                        lbContent.Text = dt.Rows[0][4].ToString();
                        lbUserName.Text = "Created by " + dt.Rows[0][0].ToString();
                        DateTime createDate = Convert.ToDateTime(dt.Rows[0][5].ToString());
                        DateTime dateNow = DateTime.Now;
                        int totalTime = Convert.ToInt32((dateNow - createDate).TotalDays);
                        String dateFormat = "";
                        if (totalTime >= 365)
                        {
                            totalTime = totalTime / 365;
                            dateFormat = "years";
                        }
                        else if (totalTime >= 30)
                        {
                            totalTime = totalTime / 30;
                            dateFormat = "months";
                        }
                        else if (totalTime >= 7)
                        {
                            totalTime = totalTime / 7;
                            dateFormat = "weeks";
                        }
                        else
                        {
                            if (totalTime <= 0)
                            {
                                totalTime = Convert.ToInt32((dateNow - createDate).TotalMinutes) ;
                                if (totalTime >= 60)
                                {
                                    totalTime = totalTime/60;
                                    dateFormat = "hours";
                                }
                                else
                                {
                                    dateFormat = "minutes";
                                }
                            }
                            else
                            {
                                dateFormat = "days";
                            }
                        }
                        lbCreated.Text = "Created " + totalTime + " " + dateFormat + " ago";
                        lbClass.Text = "Class " + dt.Rows[0][2].ToString();
                        lbCreatedDate.Text = "Created " + dt.Rows[0][5].ToString();
                        lbLastUpdate.Text = "Last Update " + dt.Rows[0][6].ToString();
                        lastUpdateDate = Convert.ToDateTime(dt.Rows[0][6].ToString());
                        if (dt.Rows[0][1] != null)
                        {
                            String image = ConfigurationManager.AppSettings["imageShowPath"].ToString() + dt.Rows[0][1].ToString();
                            ImgProfilePic.ImageUrl = image;
                        }
                        if (Session["userGUID"].ToString() == dt.Rows[0][7].ToString())
                        {
                            lbMenu.Visible = true;
                        }
                    }
                    else
                    {
                        Response.Redirect("ForumList.aspx");
                    }
                }
                else
                {
                    Response.Redirect("ForumList.aspx");
                }
                }
                catch(Exception ex)
            {
                Response.Write(ex.Message);
            }


        }

        private bool CheckComment()
        {
            
            SqlConnection con = new SqlConnection(strcon); 
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("Select * from Comment Where ForumGUID='" + Request.QueryString["ForumGUID"] + "';", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                con.Close();
                return true;
            }
            else
            {
                con.Close();
                return false;
            }
        }

        protected void DeleteForum()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                String strDelete = "";
                SqlCommand cmd = new SqlCommand(strDelete,con);
                if (CheckComment() == true)
                {
                    cmd = new SqlCommand("DELETE From Comment where ForumGUID='" + Request.QueryString["ForumGUID"] + "';", con);
                    cmd.ExecuteNonQuery();
                }
                cmd = new SqlCommand("DELETE From Forum where ForumGUID='" + Request.QueryString["ForumGUID"] + "';", con);
                cmd.ExecuteNonQuery();
                con.Close();

                Response.Write("<script language='javascript'>alert('Forum deleted successfully');</script>");
                Server.Transfer("ForumList.aspx", true);
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void UpdateForum()
        {
            if (tbContent.Text.Trim() != "" & tbTitle.Text.Trim() != "")
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("UPDATE Forum SET LastUpdateDate=@LastUpdateDate, Title=@Title, Content=@Content where ForumGUID=@ForumGUID", con);
                    cmd.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Title", tbTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@Content", tbContent.Text.Trim());
                    cmd.Parameters.AddWithValue("@ForumGUID", Request.QueryString["ForumGUID"]);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    RefreshPage();
                    Response.Write("<script>alert('Forum updated successfully');</script>");
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
            else
            {
                Response.Write("<script>alert('Title and content of forum cannot be blank');</script>");
            }
            
        }
        protected void RefreshPage()
        {
            tbContent.Visible = false;
            tbTitle.Visible = false;
            lbTitle.Visible = true;
            lbContent.Visible = true;
            lbMenu.Visible = true;
            btnUpdate.Visible = false;
            btnCancel.Visible = false;
            LoadForum();
        }

        protected void lbModify_Click(object sender, EventArgs e)
        {
            tbContent.Visible = true;
            tbTitle.Visible = true;
            lbTitle.Visible = false;
            lbContent.Visible= false;
            lbMenu.Visible = false;
            tbTitle.Text = lbTitle.Text;
            tbContent.Text = lbContent.Text;
            btnUpdate.Visible = true;
            btnCancel.Visible = true;   
        }

        protected void lbDelete_Click(object sender, EventArgs e)
        {
            DeleteForum();
            
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateForum();
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            RefreshPage();
        }

        protected void BindGridView()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                
                String strSelect = "SELECT c.CommentGUID , convert(VARCHAR(20),c.CreateDate,100) as CreateDate, CommentBy, c.Content From Comment c LEFT JOIN Forum f ON c.ForumGUID=f.ForumGUID Where f.ForumGUID=@ForumGUID";
                SqlCommand cmd = new SqlCommand(strSelect, con);
                cmd.Parameters.AddWithValue("@ForumGUID", Request.QueryString["ForumGUID"]);
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                con.Close();

                if (dt.Rows.Count == 0)
                {
                    lblNoData.Visible = true;
                    GvCommentList.DataSource = dt;
                    GvCommentList.DataBind();
                    GvCommentList.Visible = false;
                }
                else
                {
                    GvCommentList.DataSource = dt;
                    GvCommentList.DataBind();
                }
            }
            catch (SqlException ex)
            {

                string msg = ex.Message;
                Response.Write(msg);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (tbComment.Text != "")
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    Guid CommentGUID = Guid.NewGuid();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Comment(CommentGUID,ForumGUID,CreateDate,Content,CommentBy) values (@CommentGUID,@ForumGUID,@CreateDate,@Content,@CommentBy)", con);
                    cmd.Parameters.AddWithValue("@CommentGUID", CommentGUID);
                    cmd.Parameters.AddWithValue("@ForumGUID", Request.QueryString["ForumGUID"]);
                    cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Content", tbComment.Text.Trim());
                    cmd.Parameters.AddWithValue("@CommentBy", Session["fullName"]);

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    con.Close();
                    tbComment.Text = "";
                    Page.Response.Redirect(Page.Request.Url.ToString(), false);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
            else {
                Response.Write("<script>alert('Comment cannot be blank');</script>");
            }
        }
    }
}