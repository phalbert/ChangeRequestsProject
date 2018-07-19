<%@ Page Title="" Language="C#" MasterPageFile="~/LoggedInMaster.Master" AutoEventWireup="true" CodeBehind="ApplyForGoLive.aspx.cs" Inherits="CRWebPortal.ApplyForGoLive" %>
<%@ MasterType VirtualPath="~/LoggedInMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-12">
            <ul class='nav nav-wizard'>
                <li class="active"><a href="ApplyForTBPAccess.aspx" data-toggle="tab">Privilleged Access Details</a></li>
                <li><a href="Finished.aspx" data-toggle="tab">Done</a></li>

            </ul>
        </div>
    </div>
    <br />

    <div class="row">
        <div class="col-lg-1"></div>
        <div class="col-lg-10">

            <%------------ General Details Section---------  --%>
            <div class="card border-primary text-white  mb-3">
                <div class="card-header card-header-info">
                    System Access details
                </div>
                <div class="card-body bg-default">
                    <div class="row">
                        <div class="col-lg-6" style="padding-bottom: 10px">
                            <label>
                                Change Request ID</label>
                            <asp:DropDownList ID="ddChangeRequests" runat="server" CssClass="form-control" >
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-6" style="padding-bottom: 10px">
                            <label>
                                Go Live Date And Time</label>
                           <asp:TextBox ID="txtStartDateTime" CssClass="form-control form_datetime" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-6" style="padding-bottom: 10px">
                            <label>
                                Approver</label>
                            <asp:DropDownList ID="ddApprover" runat="server" CssClass="form-control">
                                <asp:ListItem Value="kasozi.nsubuga@pegasus.co.ug">Nsubuga Kasozi</asp:ListItem>
                                <asp:ListItem Value="paul.kavule@pegasus.co.ug">Paul Kavule</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="card-footer bg-default text-center">
                    <asp:Button ID="btnNextStep" Text="Request For Access" CssClass="btn btn-md btn-success" runat="server" OnClick="btnNextStep_Click" />
                </div>

            </div>
        </div>

        <div class="col-lg-1"></div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerJsContent" runat="server">
</asp:Content>
