<%@ Page Language="C#" MasterPageFile="~/LoggedInMaster.Master" AutoEventWireup="true" CodeBehind="ApplyForTBPAccess.aspx.cs" Inherits="CRWebPortal.ApplyForTBPAccess" %>

<%@ MasterType VirtualPath="~/LoggedInMaster.Master" %>


<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

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
                                System Type</label>
                            <asp:DropDownList ID="ddSystemTypes" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddSystemTypes_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-6" style="padding-bottom: 10px">
                            <label>
                                System Name</label>
                            <asp:DropDownList ID="ddSystems" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-6" style="padding-bottom: 10px">
                            <label>
                                Type Of Access</label>
                            <asp:DropDownList ID="ddTypeOfAccess" runat="server" CssClass="form-control">
                                <asp:ListItem Value="UPDATE">Update table Details</asp:ListItem>
                                <asp:ListItem Value="DELETE">Delete table data</asp:ListItem>
                                <asp:ListItem Value="INSERT">Insert table Details</asp:ListItem>
                                <asp:ListItem Value="CREATE">Create Database Objects</asp:ListItem>
                                <asp:ListItem Value="FULL">Full DB Access</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-6" style="padding-bottom: 10px">
                            <label>
                                Approver</label>
                            <asp:DropDownList ID="ddApprover" runat="server" CssClass="form-control">
                                <asp:ListItem Value="kasozi.nsubuga@pegasus.co.ug">Nsubuga Kasozi</asp:ListItem>
                                <asp:ListItem Value="paul.kavule@pegasus.co.ug">Paul Kavule</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>


                    <br />
                    <div class="row">

                        <div class="col-lg-6" style="padding-bottom: 10px">
                            <label>
                                Start Date And Time</label>
                            <asp:TextBox ID="txtStartDateTime" CssClass="form-control form_datetime" runat="server"></asp:TextBox>
                        </div>

                        <div class="col-lg-6" style="padding-bottom: 10px">
                            <label>
                                Duration</label>
                            <asp:DropDownList ID="ddDuration" runat="server" CssClass="form-control">
                                <asp:ListItem Value="15">15 minutes</asp:ListItem>
                                <asp:ListItem Value="30">30 minutes</asp:ListItem>
                                <asp:ListItem Value="60">1 hour</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>


                    <br />
                    <div class="row">
                        <div class="col-lg-12">
                            <label>
                                Reason</label>
                            <asp:TextBox ID="txtReason" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
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
