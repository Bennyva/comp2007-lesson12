<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="department.aspx.cs" Inherits="lesson12.department" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h1>Department Details</h1>

    
    <fieldset>
        <label for="txtDeptName" class="col-sm-2">Department Name:</label>
        <asp:TextBox ID="txtDeptName" runat="server" required="true" MaxLength="50"></asp:TextBox>
    </fieldset>
    <fieldset>
        <label for="txtBudget" class="col-sm-2">Budget:</label>
        <asp:TextBox ID="txtBudget" runat="server" required="true" MaxLength="50"></asp:TextBox>
    </fieldset>
    

    <div class="col-sm-offset-2">
        <asp:Button id="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click"
            />
    </div>
    <h1>Courses</h1>
     <asp:GridView ID="grdDepartment" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false" 
        OnRowDeleting="grdDepartment_RowDeleting" DataKeyNames="CourseID">
        
        <Columns>
            
            <asp:BoundField DataField="Title" HeaderText="Course" />
            <asp:BoundField DataField="Credits" HeaderText="Credits" />

            <asp:CommandField HeaderText="Delete" DeleteText="Delete" ShowDeleteButton="true" /><%--ButtonType="Button" ControlStyle-CssClass="button btn-danger"--%>

        </Columns>
    </asp:GridView>

</asp:Content>
