using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//reference the ef models
using lesson12.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;

namespace lesson12
{
    public partial class students : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if loading page for first time, populate the student grid
            if (!IsPostBack)
            {
                Session["SortColumn"] = "LastName";
                Session["SortDirection"] = "ASC";
                GetStudents();
            }
        }

        protected void GetStudents()
        {
            try
            {
                using (comp2007Entities db = new comp2007Entities())
                {
                    String SortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();


                    //query the students table using EF and LINQ
                    var Students = from s in db.Students
                                   select s;

                    //bind he result to the gridview
                    grdStudents.DataSource = Students.AsQueryable().OrderBy(SortString).ToList();
                    grdStudents.DataBind();

                }
            }
            catch (Exception)
            {
                Server.Transfer("/error.aspx");
            }
        }

        protected void grdStudents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //get the selected studentID using the grid's data key collection
            Int32 StudentID = Convert.ToInt32(grdStudents.DataKeys[e.RowIndex].Values["StudentID"]);

            try
            {
                //use Entity Framework to remove the selected student from the db
                using (comp2007Entities db = new comp2007Entities())
                {
                    Student s = (from objS in db.Students
                                 where objS.StudentID == StudentID
                                 select objS).FirstOrDefault();

                    //do the delete
                    db.Students.Remove(s);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                Server.Transfer("/error.aspx");
            }

            //refresh the grid
            GetStudents();



        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdStudents.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            GetStudents();
        }

        protected void grdStudents_Sorting(object sender, GridViewSortEventArgs e)
        {
            //get the column to sort by
            Session["SortColumn"] = e.SortExpression;

            GetStudents();

            //toggle sort direction
            if (Session["SortDirection"].ToString() == "ASC")
            {
                Session["SortDirection"] = "DESC";
            }
            else
            {
                Session["SortDirection"] = "ASC";
            }
        }

        protected void grdStudents_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    Image SortImage = new Image();

                    for (int i = 0; i <= grdStudents.Columns.Count - 1; i++)
                    {
                        if (grdStudents.Columns[i].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["SortDirection"].ToString() == "DESC")
                            {
                                SortImage.ImageUrl = "images/desc.jpg";
                                SortImage.AlternateText = "Sort Descending";
                            }
                            else
                            {
                                SortImage.ImageUrl = "images/asc.jpg";
                                SortImage.AlternateText = "Sort Ascending";
                            }

                            e.Row.Cells[i].Controls.Add(SortImage);

                        }
                    }
                }

            }
        }
    }
}