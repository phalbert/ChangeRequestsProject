<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupplyUsername.aspx.cs" Inherits="CRWebPortal.SupplyUsername" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="row" style="padding-top: 15%">
        <div class="col-lg-2"></div>
        <div class="col-lg-8">

            <div class="card border-primary text-white  mb-3">
                <div class="card-header bg-primary text-center">
                    Supply Your Username Below
                </div>
                <div class="card-body bg-default">
                    <asp:TextBox ID="txtUsername" placeholder="Enter Your Username" Style="border-color: #428BCA" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="card-footer bg-default text-center">
                    <asp:Button ID="btnSubmit" Text="Sign In" CssClass="btn btn-md btn-success" runat="server" OnClick="btnSubmit_Click" />
                     <asp:Button ID="btnRegister" Text="Sign Up" CssClass="btn btn-md btn-danger" runat="server"  />
                </div>
            </div>
        </div>
        <div class="col-lg-2"></div>
    </div>
</asp:Content>
