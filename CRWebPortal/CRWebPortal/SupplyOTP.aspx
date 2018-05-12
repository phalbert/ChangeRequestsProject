<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupplyOTP.aspx.cs" Inherits="CRWebPortal.SupplyOTP" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="row" style="padding-top: 15%">
        <div class="col-lg-2"></div>
        <div class="col-lg-8">
            <div class="card border-primary text-white  mb-3">
                <div class="card-header bg-primary text-center">
                    Supply Your One Time Password Below
                </div>
                <div class="card-body bg-default">
                    <asp:TextBox ID="txtOTP" placeholder="Enter Your One Time Password" Style="border-color: #428BCA" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="card-footer bg-default text-center">
                    <asp:Button ID="btnBack" Text="Go Back" CssClass="btn btn-md btn-danger" runat="server" OnClick="btnBack_Click" />
                    <asp:Button ID="btnResend" Text="Resend OTP" CssClass="btn btn-md btn-info" runat="server" OnClick="btnResend_Click" />
                     <asp:Button ID="btnNext" Text="Verify OTP" CssClass="btn btn-md btn-success" runat="server" OnClick="btnNext_Click" />
                </div>
            </div>
        </div>
        <div class="col-lg-2"></div>
    </div>
</asp:Content>

