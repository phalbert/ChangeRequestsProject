<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupplyOTP.aspx.cs" Inherits="CRWebPortal.SupplyOTP" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="row">
        <div class="col-lg-4"></div>
        <div class="col-lg-4">
            <div class="panel panel-primary">
                <div class="panel-heading text-center">
                    Supply Your One Time Password Below
                </div>
                <div class="panel-body  text-center">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-lg-4"></div>
                            <div class="col-lg-4">
                                <asp:TextBox ID="txtOTP" placeholder="Enter Your One Time Password" Style="border-color: #428BCA" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-lg-4"></div>
                        </div>
                    </div>
                </div>
                <div class="panel-footer text-center">
                    <asp:Button ID="btnBack" Text="Go Back" CssClass="btn btn-md btn-danger" runat="server" OnClick="btnBack_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnSubmit" Text="Send OTP" CssClass="btn btn-md btn-success" runat="server" OnClick="btnSubmit_Click" />
                </div>
            </div>

        </div>
        <div class="col-lg-4"></div>
    </div>
</asp:Content>

