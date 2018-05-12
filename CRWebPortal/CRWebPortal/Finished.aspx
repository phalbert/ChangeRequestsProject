<%@ Page Language="C#" MasterPageFile="~/LoggedInMaster.Master" AutoEventWireup="true" CodeBehind="Finished.aspx.cs" Inherits="CRWebPortal.Finished" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    
    <div class="row">
        <div class="col-12">
            <ul class='nav nav-wizard'>

                <li><a href="CreateChangeRequest.aspx" data-toggle="tab">Change Details</a></li>
                <li><a href="AttachSystemsAffected.aspx" data-toggle="tab">Systems Affected</a></li>
                <li><a href="AttachItemToChangeRequest.aspx" data-toggle="tab">Any Attachments</a></li>
                <li><a href="AttachApproversToCR.aspx" data-toggle="tab">Assign Approvers</a></li>
                <li class="active"><a href="Finished.aspx" data-toggle="tab">Done</a></li>

            </ul>
        </div>
    </div>
    <br />

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
