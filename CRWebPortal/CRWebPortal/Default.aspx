<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="CRWebPortal._Default" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="row">
        <div class="col-lg-12 alert alert-danger text-center">
            <asp:Label ID="msg" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
