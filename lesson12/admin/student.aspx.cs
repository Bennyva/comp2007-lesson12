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
    public partial class student : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if save wasn't clicked AND we have a studentID in the URL
            if (!IsPostBack)
            {
                if (Request.QueryString.Count > 0)
                {
                    GetDepartments();
                    GetStudent();
                }
                else
                    pnlCourses.Visible = false;
            }
        }

        protected void GetDepartments()
        {
            try
            {
                using (comp2007Entities db = new comp2007Entities())
                {
                    

                    //query the students table using EF and LINQ
                    var Dept = from d in db.Departments
                               orderby d.Name
                               select d;

                    ddlDepartment.DataSource = Dept.ToList();
                    ddlDepartment.DataBind();

                    ListItem newItem = new ListItem("-Select-", "0");
                    ddlDepartment.Items.Insert(0, newItem);
                    ddlCourse.Items.Insert(0, newItem);

                }
            }
            catch (Exception)
            {
                Server.Transfer("/error.aspx");
            }
        }

        protected void GetStudent()
        {
            //populate form with existing student record
            Int32 StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

            try
            {
                //connect to db using entity framework
                using (comp2007Entities db = new comp2007Entities())
                {
                    //populate a student instance with the studentID from the URL paramater
                    Student s = (from objS in db.Students
                                 where objS.StudentID == StudentID
                                 select objS).FirstOrDefault();

                    //map the student properties to the form controls if we found a record
                    if (s != null)
                    {
                        txtLastName.Text = s.LastName;
                        txtFirstMidName.Text = s.FirstMidName;
                        txtEnrollmentDate.Text = s.EnrollmentDate.ToString("yyyy-MM-dd");
                    }
                    //enrollments - this code goes in the same method that populates the student form but below the existing code that's already in GetStudent()               
                    var objE = (from en in db.Enrollments
                                join c in db.Courses on en.CourseID equals c.CourseID
                                join d in db.Departments on c.DepartmentID equals d.DepartmentID
                                where en.StudentID == StudentID
                                select new { en.EnrollmentID, en.Grade, c.Title, d.Name });

                    grdCourses.DataSource = objE.ToList();
                    grdCourses.DataBind();
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
                    Student s = new Student();
                    Int32 StudentID = 0;
                    //check the query string for an id so we can determine add / update
                    if (Request.QueryString["StudentID"] != null)
                    {
                        //get the ID from the URL
                        StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

                        //get the current student from Entity Framework
                        s = (from objS in db.Students
                             where objS.StudentID == StudentID
                             select objS).FirstOrDefault();
                    }
                    s.LastName = txtLastName.Text;
                    s.FirstMidName = txtFirstMidName.Text;
                    s.EnrollmentDate = Convert.ToDateTime(txtEnrollmentDate.Text);

                    //call add only if we have no student ID
                    if (StudentID == 0)
                        db.Students.Add(s);

                    db.SaveChanges();
                    //redirect to the updated students page
                    Response.Redirect("students.aspx");
                }
            }
            catch (Exception)
            {
                Server.Transfer("/error.aspx");
            }
        }

        protected void grdCourses_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //get the selected studentID using the grid's data key collection
            Int32 EnrollmentID = Convert.ToInt32(grdCourses.DataKeys[e.RowIndex].Values["EnrollmentID"]);


            try
            {
                //use Entity Framework to remove the selected student from the db
                using (comp2007Entities db = new comp2007Entities())
                {
                    Enrollment objE = (from en in db.Enrollments
                                       where en.EnrollmentID == EnrollmentID
                                       select en).FirstOrDefault();

                    //do the delete
                    db.Enrollments.Remove(objE);
                    db.SaveChanges();
                }

                //refresh the grid
                GetStudent();
            }
            catch (Exception)
            {
                Server.Transfer("/error.aspx");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (comp2007Entities db = new comp2007Entities())
            {
                Enrollment objE = new Enrollment();
                objE.StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);
                objE.CourseID = Convert.ToInt32(ddlCourse.SelectedValue);

                db.Enrollments.Add(objE);
                db.SaveChanges();

                //add to the enrollments table
                GetEnrollments();

            }
        }

        protected void GetEnrollments()
        {
            int StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

            using (comp2007Entities db = new comp2007Entities())
            {
                var objE = (from en in db.Enrollments
                            join c in db.Courses on en.CourseID equals c.CourseID
                            join d in db.Departments on c.DepartmentID equals d.DepartmentID
                            where en.StudentID == StudentID
                            select new { en.EnrollmentID, en.Grade, c.Title, d.Name });

                grdCourses.DataSource = objE.ToList();
                grdCourses.DataBind();

            }

        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 DepartmentID = Convert.ToInt32(ddlDepartment.SelectedIndex);

            using(comp2007Entities db = new comp2007Entities())
            {
                var objC = from c in db.Courses
                           where c.DepartmentID == DepartmentID
                           orderby c.Title
                           select c;

                //populate course dropdown
                ddlCourse.DataSource = objC.ToList();
                ddlCourse.DataBind();

                ListItem newItem = new ListItem("-SELECT-", "0");
                ddlCourse.Items.Insert(0, newItem);
            }

            
        }
    }
}