<%@ Page Language="C#" MasterPageFile="~/LoggedInMaster.Master" AutoEventWireup="true" CodeBehind="SupplyToken.aspx.cs" Inherits="CRWebPortal.SupplyToken" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="row" style="padding-top: 15%">
        <div class="col-lg-2"></div>
        <div class="col-lg-8">

            <div class="card border-primary text-white  mb-3">
                <div class="card-header bg-primary text-center">
                    Supply Your Token Below
                </div>
                <div class="card-body bg-default">
                    <asp:TextBox ID="txtToken" placeholder="Enter Your Token" Style="border-color: #428BCA" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="card-footer bg-default text-center">
                    <asp:Button ID="btnSubmit" Text="Verify Token" CssClass="btn btn-md btn-success" runat="server" OnClick="btnSubmit_Click" />
                </div>
            </div>

        </div>
        <div class="col-lg-2"></div>
    </div>
</asp:Content>
