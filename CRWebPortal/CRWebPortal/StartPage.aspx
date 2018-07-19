<%@ Page Title="" Language="C#" MasterPageFile="~/LoggedInMaster.Master" AutoEventWireup="true" CodeBehind="StartPage.aspx.cs" Inherits="CRWebPortal.StartPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" style="padding-top:2em;">
        <div class="col-lg-4">
        </div>
        <div class="col-lg-4">
            <div class="card card-profile">
                <div class="card-avatar">
                    <a href="#pablo">
                        <img class="img" src="assets/img/SampleUserPic.png" />
                    </a>
                </div>
                <div class="card-body">
                    <h6 class="card-category text-gray"><asp:Label ID="lblUserRole" runat="server">CEO / Co-Founder</asp:Label></h6>
                    <h4 class="card-title"><asp:Label ID="lblFullName" runat="server">Alec Thompson</asp:Label></h4>
                    <p class="card-description">
                        In time you will know what it’s like to lose. To feel so desperately that you’re the right, and to fail all the same. Dread it...run from it, Destiny will arrive none the Less. Embrace it, Love life. Live
                    </p>
                    <a href="#pablo" class="btn btn-primary btn-round">Greatness Begins</a>
                </div>
            </div>
        </div>
        <div class="col-lg-4">
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerJsContent" runat="server">
</asp:Content>
