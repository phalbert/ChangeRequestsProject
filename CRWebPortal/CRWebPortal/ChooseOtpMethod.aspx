<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChooseOtpMethod.aspx.cs" Inherits="CRWebPortal.ChooseOtpMethod" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="row" style="padding-top: 15%">
        <div class="col-lg-2"></div>
        <div class="col-lg-8">
            <div class="card border-primary text-white  mb-3">

                <div class="card-header bg-primary text-center">
                    Select a Method for Recieving Your One Time Password
                </div>
                <div class="card-body bg-default">
                    <div class="row text-center">
                        <div class="col-lg-4"></div>
                        <div class="col-lg-4">
                            <asp:RadioButton ID="rdEmail" Text="&nbsp;Email" runat="server" GroupName="OTPType" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdPhone" Text="&nbsp;Phone" runat="server" GroupName="OTPType" />
                        </div>
                        <div class="col-lg-4"></div>
                    </div>
                </div>
                <div class="card-footer text-center">
                    <asp:Button ID="btnBack" Text="Go Back" CssClass="btn btn-md btn-danger" runat="server" OnClick="btnBack_Click" />
                    <asp:Button ID="btnSubmit" Text="Send OTP" CssClass="btn btn-md btn-success" runat="server" OnClick="btnSubmit_Click" />
                </div>
            </div>
    </div>
    <div class="col-lg-2"></div>
    </div>
</asp:Content>
