<%@ Page AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ICSSoft.STORMNET.Web.Default" Language="C#" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="icon" href="/Myfavicon.ico" type="image/x-icon">
    <link rel="shortcut icon" href="/Myfavicon.ico" type="image/x-icon">
    <div>
        <asp:Localize Text="<%$ Resources: Resource, Home_Page %>" runat="server"></asp:Localize><br />
    </div>
</asp:Content>