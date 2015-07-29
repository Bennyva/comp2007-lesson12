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
    public partial class departments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if loading page for first time, populate the student grid
            if (!IsPostBack)
            {
                Session["SortColumn"] = "DepartmentID";
                Session["SortDirection"] = "ASC";
                GetDepartments();
            }
        }

        protected void GetDepartments()
        {
            try
            {
                using (comp2007Entities db = new comp2007Entities())
                {
                    String SortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();

                    //query the students table using EF and LINQ
                    var Dept = from d in db.Departments
                               select d;


                    //bind he result to the gridview
                    grdDepts.DataSource = Dept.AsQueryable().OrderBy(SortString).ToList();
                    grdDepts.DataBind();


                }
            }
            catch (Exception)
            {
                Server.Transfer("/error.aspx");
            }

        }

        protected void grdDepts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //store which row was clicked
            Int32 selectedRow = e.RowIndex;

            //get the selected studentID using the grid's data key collection
            Int32 DepartmentID = Convert.ToInt32(grdDepts.DataKeys[selectedRow].Values["DepartmentID"]);

            try
            {
                //use Entity Framework to remove the selected student from the db
                using (comp2007Entities db = new comp2007Entities())
                {
                    Department d = (from objS in db.Departments
                                    where objS.DepartmentID == DepartmentID
                                    select objS).FirstOrDefault();

                    //do the delete
                    db.Departments.Remove(d);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                Server.Transfer("/error.aspx");
            }

            //refresh the grid
            GetDepartments();

        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdDepts.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            GetDepartments();
        }

        protected void grdDepts_Sorting(object sender, GridViewSortEventArgs e)
        {
            //get the column to sort by
            Session["SortColumn"] = e.SortExpression;

            GetDepartments();

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

        protected void grdDepts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    Image SortImage = new Image();

                    for (int i = 0; i <= grdDepts.Columns.Count - 1; i++)
                    {
                        if (grdDepts.Columns[i].SortExpression == Session["SortColumn"].ToString())
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