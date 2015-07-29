<%@ Page Title="Students" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="students.aspx.cs" Inherits="lesson12.students" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Students</h1>

    <a href="student.aspx">Add Student</a>

       <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
        <asp:ListItem Value="3" Text="3" />
        <asp:ListItem Value="5" Text="5" />
        <asp:ListItem Value="99999" Text="All" />
    </asp:DropDownList>

    <asp:GridView ID="grdStudents" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false" PageSize="3" 
        OnRowDeleting="grdStudents_RowDeleting" DataKeyNames="StudentID" AllowPaging="true" AllowSorting="true" OnSorting="grdStudents_Sorting" OnRowDataBound="grdStudents_RowDataBound">
        <Columns>
            <asp:BoundField DataField="StudentID" HeaderText="Student ID" visible="false"/>
            <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName"/>
            <asp:BoundField DataField="FirstMidName" HeaderText="First Name" SortExpression="FirstMidName"/>
            <asp:BoundField DataField="EnrollmentDate" HeaderText="Enrollment Data" DataFormatString="{0:MM-dd-yyyy}" SortExpression="EnrollmentDate" />
            <asp:HyperLinkField HeaderText="Edit" Text="Edit" NavigateUrl="student.aspx" DataNavigateUrlFields="StudentID" 
                DataNavigateUrlFormatString="student.aspx?StudentID={0}"/>
            <asp:CommandField HeaderText="Delete" DeleteText="Delete" ShowDeleteButton="true" /><%--ButtonType="Button" ControlStyle-CssClass="button btn-danger"--%>
        </Columns>
    </asp:GridView>
</asp:Content>
