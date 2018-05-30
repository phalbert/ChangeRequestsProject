<%@ Page Language="C#" MasterPageFile="~/LoggedInMaster.Master" AutoEventWireup="true" CodeBehind="Finished.aspx.cs" Inherits="CRWebPortal.Finished" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <div class="row">
        <div class="col-lg-1"></div>
        <div class="col-lg-10 text-center">
            <span style="font-size: 20vw; color: Green"><i class="fa fa-check-circle"></i></span>
            <hr />
            <h2>PLEASE WAIT FOR APPROVAL</h2>
            <hr />
            <div class="row text-center">
                <asp:Button ID="btnBack" Text="Go Back Home" CssClass="btn btn-block btn-success" runat="server" OnClick="btnBack_Click" />
            </div>
        </div>
        <div class="col-lg-1"></div>
    </div>
</asp:Content>
