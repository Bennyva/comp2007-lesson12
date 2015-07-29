<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="register1.aspx.cs" Inherits="lesson12.register1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Register</h1>


    <div>
        <h4 style="font-size: medium">Enter the following information</h4>
        <hr />
        <div class="form-group-lg">
            <asp:label id="lblStatus" runat="server" cssclass="label label-danger" />
        </div>         
        <div class="form-group">
        <label for="txtUsername" class="col-sm-2">Username:</label>
        <asp:textbox id="txtUsername" runat="server" />
        </div>
        <div class="form-group">
            <label for="txtPassword" class="col-sm-2">Password:</label>
            <asp:textbox id="txtPassword" runat="server" textmode="password" />
        </div>
        <div class="form-group">
            <label for="txtConfirm" class="col-sm-2">Confirm:</label>
            <asp:textbox id="txtConfirm" runat="server" textmode="password" />
            <asp:comparevalidator runat="server" operator="Equal" controltovalidate="txtPassword"
            controltocompare="txtConfirm" cssclass="label label-danger" errormessage="Passwords must match" />
        </div>
        <div class="col-sm-offset-2">
            <asp:button id="btnRegister" runat="server" text="Register" cssclass="btn btn-primary" 
            OnClick="btnRegister_Click" />
        </div>
    </div>
</asp:Content>
