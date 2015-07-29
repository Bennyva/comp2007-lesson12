<%@ Page Title="Add Student" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="student.aspx.cs" Inherits="lesson12.student" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    


    <h1>Student Details</h1>

    <fieldset>
        <label for="txtLastName" class="col-sm-2">Last Name:</label>
        <asp:TextBox ID="txtLastName" runat="server" required="true" MaxLength="50"></asp:TextBox>
    </fieldset>
    <fieldset>
        <label for="txtFirstMidName" class="col-sm-2">First Name:</label>
        <asp:TextBox ID="txtFirstMidName" runat="server" required="true" MaxLength="50"></asp:TextBox>
    </fieldset>
    <fieldset>
        <label for="txtEnrollmentDate" class="col-sm-2">Enrollment Date:</label>
        <asp:TextBox ID="txtEnrollmentDate" runat="server" required="true" MaxLength="50"></asp:TextBox>
        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Must be a date" ControlToValidate="txtEnrollmentDate"
            CssClass="alert alert-danger" Type="Date" MinimumValue="2000-01-01" MaximumValue="2999-12-31"></asp:RangeValidator>
    </fieldset>

    <div class="col-sm-offset-2">
        <asp:Button id="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click"
            />
    </div>
     <asp:Panel ID="pnlCourses" runat="server">
    <h1>Courses</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:GridView ID="grdCourses" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false" 
                OnRowDeleting="grdCourses_RowDeleting" DataKeyNames="EnrollmentID" >
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Department" />
                    <asp:BoundField DataField="Title" HeaderText="Course" />
                    <asp:BoundField DataField="Grade" HeaderText="Grade" />
                    <asp:CommandField HeaderText="Delete" DeleteText="Delete" ShowDeleteButton="true" />
                    <%--ButtonType="Button" ControlStyle-CssClass="button btn-danger"--%>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
        <%-- this will trigger an update to courses grid when the add button is clicked --%>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="updAdd" runat="server">
        <ContentTemplate>
            <table class="table table-striped table-hover">
                <thead>
                    <th>Department</th>
                    <th>Course</th>
                    <th>Add</th>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlDepartment" runat="server"
                                DataTextField="Name" DataValueField="DepartmentID"
                                autoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                            </asp:DropDownList>
                            <%--<asp:RangeValidator ControlToValidate="ddlDepartment" runat="server"
                                Type="Integer" MinimumValue="1" MaximumValu="999999"
                                ErrorMessage="Required" CssClass="label label-danger" />--%>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCourse" runat="server"
                                DataTextField="Title" DataValueField="CourseID"></asp:DropDownList>
                            <%--<asp:RangeValidator ControlToValidate="ddlCourse" runat="server"
                                Type="Integer" MinimumValue="1" MaximumValu="999999"
                                ErrorMessage="Required" CssClass="label label-danger" />--%>
                        </td>
                        <td>
                            <asp:Button ID="btnAdd" runat="server"
                                Text="Save" CssClass="btn btn-primary" OnClick="btnAdd_Click"></asp:Button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
     
    </asp:Panel>
</asp:Content>
