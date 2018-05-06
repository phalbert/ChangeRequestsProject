<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Finished.aspx.cs" Inherits="CRWebPortal.Finished" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="row">
        <div class="col-lg-4"></div>
        <div class="col-lg-4 text-center">
            <span style="font-size: 25vw; color: Green" class="glyphicon glyphicon-check"></span>
            <hr />
            <h2>PLEASE WAIT FOR APPROVAL</h2>
            <hr />
            <div class="row">
                <asp:Button ID="btnBack" Text="Go Back Home" CssClass="btn btn-lg btn-danger" runat="server" OnClick="btnBack_Click" />
            </div>
        </div>
        <div class="col-lg-4"></div>
    </div>
</asp:Content>
