<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="course.aspx.cs" Inherits="lesson12.course" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h1>Add Course</h1>


    <fieldset>
        <label for="txtTitle" class="col-sm-2">Course Name:</label>
        <asp:TextBox ID="txtTitle" runat="server" required="true" MaxLength="50"></asp:TextBox>
    </fieldset>
    <fieldset>
        <label for="txtCredits" class="col-sm-2">Credits:</label>
        <asp:TextBox ID="txtCredits" runat="server" required="true" MaxLength="50"></asp:TextBox>
    </fieldset>
    <fieldset>
        <label for="ddDepartment" class="col-sm-2">Department:</label>
        <asp:DropDownList ID="ddDepartment" runat="server" ></asp:DropDownList>
    </fieldset>

    <div class="col-sm-offset-2">
        <asp:Button id="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click"
            />
    </div>
    <h1>Students</h1>
    <asp:GridView ID="grdStudent" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false" 
        OnRowDeleting="grdStudent_RowDeleting" DataKeyNames="EnrollmentID">
        
        <Columns>
            <asp:BoundField DataField="StudentID" HeaderText="Student ID:"/>
            <asp:BoundField DataField="LastName" HeaderText="Last Name:" />
            <asp:BoundField DataField="FirstMidName" HeaderText="First Name:" />
            <asp:BoundField DataField="EnrollmentDate" HeaderText="Enrollment Data" DataFormatString="{0:MM-dd-yyyy}" />
            
            <asp:CommandField HeaderText="Delete" DeleteText="Delete" ShowDeleteButton="true" /><%--ButtonType="Button" ControlStyle-CssClass="button btn-danger"--%>

        </Columns>
    </asp:GridView>

</asp:Content>
