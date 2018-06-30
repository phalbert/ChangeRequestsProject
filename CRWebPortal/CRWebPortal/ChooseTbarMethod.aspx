<%@ Page Title="" Language="C#" MasterPageFile="~/LoggedInMaster.Master" AutoEventWireup="true" CodeBehind="ChooseTbarMethod.aspx.cs" Inherits="CRWebPortal.ChooseTbarMethod" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-lg-12 text-center" style="padding-top: 50px;">
                <div class="alert alert-info">
                    SO YOU WANT ACCESS, WHICH KIND OF ACCESS DO YOU NEED?
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12 text-center" style="padding-top: 50px;">
                <p>
                    <a href="ExecuteQuery.aspx" class="btn btn-lg btn-primary">
                        USE T.B.A.R FOR DB ACCESS
                        
                    </a>
                    <a href="TbarRdpWebAccess.aspx" class="btn btn-lg btn-success">
                        USE T.B.A.R FOR RDP ACCESS
                        
                    </a>
                </p>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerJsContent" runat="server">
</asp:Content>
