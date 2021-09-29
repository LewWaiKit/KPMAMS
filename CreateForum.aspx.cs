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
    public partial class CreateForum : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {

                BindClasses();
            }
            
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Validation();
        }
        protected void Validation()
        {

            if (dlClassList.SelectedValue != String.Empty && tbTitle.Text.Trim() != String.Empty && tbContent.Text.Trim() != String.Empty)
            {
                createForum();
            }
            else
            {
                Response.Write("<script>alert('Please select class and fill up Content or Title');</script>");
            }
        }
        protected void BindClasses()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT tc.ClassroomGUID, Class From Teacher_Classroom tc LEFT JOIN Classroom c ON c.ClassroomGUID = tc.ClassroomGUID WHERE TeacherGUID=@TeacherGUID", con);
                cmd.Parameters.AddWithValue("@TeacherGUID", Session["userGUID"]);
                SqlDataReader dr = cmd.ExecuteReader();

             
                if (dr.HasRows)
                {
                    dt.Load(dr);
                    dlClassList.DataTextField = "Class";
                    dlClassList.DataValueField = "ClassroomGUID";
                    dlClassList.DataSource = dt;
                    dlClassList.DataBind();
                    dlClassList.Items.Insert(0, new ListItem("---Please Select---", String.Empty));

                }
                else
                {
                    Response.Write("<script language='javascript'>alert('Error:No Class, can't create forum');</script>");
                    Server.Transfer("ForumList.aspx", true);
                }
                con.Close();

            } catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void createForum()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                Guid ForumGUID = Guid.NewGuid();
                SqlCommand cmd = new SqlCommand("INSERT INTO Forum(ForumGUID,ClassroomGUID,AuthorGUID,CreateDate,LastUpdateDate,Content,Title) values (@ForumGUID,@ClassroomGUID,@AuthorGUID,@CreateDate,@LastUpdateDate,@Content,@Title)", con);
                cmd.Parameters.AddWithValue("@ForumGUID",ForumGUID);
                cmd.Parameters.AddWithValue("@ClassroomGUID",dlClassList.SelectedValue);
                cmd.Parameters.AddWithValue("@AuthorGUID",Session["userGUID"].ToString());
                cmd.Parameters.AddWithValue("@CreateDate",DateTime.Now);
                cmd.Parameters.AddWithValue("@LastUpdateDate",DateTime.Now);
                cmd.Parameters.AddWithValue("@Content",tbContent.Text.Trim());
                cmd.Parameters.AddWithValue("@Title",tbTitle.Text.Trim());

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                Response.Write("<script language='javascript'>alert('Forum created successfully');</script>");
                Server.Transfer("ForumList.aspx", true);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}