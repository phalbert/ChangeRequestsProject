<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChooseOtpMethod.aspx.cs" Inherits="CRWebPortal.ChooseOtpMethod" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="row">
        <div class="col-lg-4"></div>
        <div class="col-lg-4">
            <div class="panel panel-primary">
                <div class="panel-heading text-center">
                    Select a Method for Recieving Your One Time Password
                </div>
                <div class="panel-body  text-center">
                    <div class="form-group text-center">
                        <div class="row">
                            <div class="col-lg-4"></div>
                            <div class="col-lg-4">
                                <asp:RadioButton ID="rdEmail" Text="&nbsp;Email" runat="server" GroupName="OTPType" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdPhone" Text="&nbsp;Phone" runat="server" GroupName="OTPType" />
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
