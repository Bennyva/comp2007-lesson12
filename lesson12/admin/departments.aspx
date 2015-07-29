<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="departments.aspx.cs" Inherits="lesson12.departments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="department.aspx">Add Department</a>

    <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
        <asp:ListItem Value="3" Text="3" />
        <asp:ListItem Value="5" Text="5" />
        <asp:ListItem Value="99999" Text="All" />
    </asp:DropDownList>

    <asp:GridView ID="grdDepts" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false" PageSize="3"
        OnRowDeleting="grdDepts_RowDeleting" DataKeyNames="DepartmentID" AllowPaging="true" AllowSorting="true" OnSorting="grdDepts_Sorting" OnRowDataBound="grdDepts_RowDataBound">
        <Columns>
            <asp:BoundField DataField="DepartmentID" HeaderText="DepartmentID" SortExpression="DepartmentID" />
            <asp:BoundField DataField="Name" HeaderText="Deptartment Name" SortExpression="Name"/>
            <asp:BoundField DataField="Budget" HeaderText="Budget" DataFormatString="{0:c}" SortExpression="Budget" />
            <asp:HyperLinkField HeaderText="Edit" Text="Edit" NavigateUrl="student.aspx" DataNavigateUrlFields="DepartmentID" 
                DataNavigateUrlFormatString="department.aspx?DepartmentID={0}"/>
            <asp:CommandField HeaderText="Delete" DeleteText="Delete" ShowDeleteButton="true" /><%--ButtonType="Button" ControlStyle-CssClass="button btn-danger"--%>
        </Columns>
    </asp:GridView>
</asp:Content>  
