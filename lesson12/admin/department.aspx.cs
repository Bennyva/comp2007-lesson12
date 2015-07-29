using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//reference the ef models
using lesson12.Models;
using System.Web.ModelBinding;

namespace lesson12
{
    public partial class department : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if save wasn't clicked AND we have a studentID in the URL
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                GetDepartment();
            }
        }

        protected void GetDepartment()
        {
            //populate form with existing student record
            Int32 DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

            try
            {
                //connect to db using entity framework
                using (comp2007Entities db = new comp2007Entities())
                {
                    //populate a student instance with the studentID from the URL paramater
                    Department d = (from objS in db.Departments
                                    where objS.DepartmentID == DepartmentID
                                    select objS).FirstOrDefault();

                    //map the student properties to the form controls if we found a record

                    txtDeptName.Text = d.Name;
                    txtBudget.Text = d.Budget.ToString();

                    //enrollments - this code goes in the same method that populates the student form but below the existing code that's already in GetStudent()               
                    var objE = (from c in db.Courses
                                join de in db.Departments on c.DepartmentID equals DepartmentID
                                where c.DepartmentID == DepartmentID
                                select new { c.CourseID, c.Title, c.Credits });

                    grdDepartment.DataSource = objE.ToList();
                    grdDepartment.DataBind();
                }
            }
            catch (Exception)
            {
                Server.Transfer("/error.aspx");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //use EF to connect to SQL server
                using (comp2007Entities db = new comp2007Entities())
                {
                    //use the student model to save the new record
                    Department d = new Department();
                    Int32 DepartmentID = 0;
                    //check the query string for an id so we can determine add / update
                    if (Request.QueryString["DepartmentID"] != null)
                    {
                        //get the ID from the URL
                        DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

                        //get the current student from Entity Framework
                        d = (from objS in db.Departments
                             where objS.DepartmentID == DepartmentID
                             select objS).FirstOrDefault();
                    }
                    d.Name = txtDeptName.Text;
                    d.Budget = Convert.ToDecimal(txtBudget.Text);

                    //call add only if we have no student ID
                    if (DepartmentID == 0)
                        db.Departments.Add(d);

                    db.SaveChanges();
                    //redirect to the updated students page
                    Response.Redirect("departments.aspx");
                }
            }
            catch (Exception)
            {
                Server.Transfer("/error.aspx");
            }

        }



        protected void grdDepartment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //get the selected studentID using the grid's data key collection
            Int32 CourseID = Convert.ToInt32(grdDepartment.DataKeys[e.RowIndex].Values["CourseID"]);


            try
            {
                //use Entity Framework to remove the selected student from the db
                using (comp2007Entities db = new comp2007Entities())
                {
                    var objE = from en in db.Enrollments
                               where en.CourseID == CourseID
                               select en;

                    //do the delete
                    foreach (var enrollments in objE)
                    {
                        db.Enrollments.Remove(enrollments);
                    }

                    db.SaveChanges();


                    Course objC = (from c in db.Courses
                                   where c.CourseID == CourseID
                                   select c).FirstOrDefault();

                    //do the delete
                    db.Courses.Remove(objC);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                Server.Transfer("/error.aspx");
            }

            //refresh the grid
            GetDepartment();
        }
    }
}