<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupplyUsername.aspx.cs" Inherits="CRWebPortal.SupplyUsername" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="row">
        <div class="col-lg-4"></div>
        <div class="col-lg-4">
            <div class="panel panel-primary">
                <div class="panel-heading text-center">
                    Supply Your Username Below
                </div>
                <div class="panel-body  text-center">
                    <div class="form-group">
                        <br />
                        <asp:TextBox ID="txtUsername" placeholder="Enter Your Username" style="border-color:#428BCA" runat="server" CssClass="form-control"></asp:TextBox>
                        <br />
                        <asp:Button ID="btnSubmit" Text="Submit" CssClass="btn btn-lg btn-success" runat="server" OnClick="btnSubmit_Click" />
                    </div>
                </div>
            </div>

        </div>
        <div class="col-lg-4"></div>
    </div>
</asp:Content>
