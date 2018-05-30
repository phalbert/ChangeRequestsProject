<%@ Page Language="C#" MasterPageFile="~/LoggedInMaster.Master" AutoEventWireup="true" CodeBehind="ChangeRiskAnalysis.aspx.cs" Inherits="CRWebPortal.ChangeRiskAnalysis" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

   

    <div class="row">
        <div class="col-lg-1"></div>
        <div class="col-lg-10">
            <div class="card border-primary text-white  mb-3">
                <div class="card-header bg-primary text-center">
                    Specify Reasons for this Access
                </div>
                <div class="card-body bg-default">

                    <%------------ General Details Section---------  --%>
                    <div class="card border-primary text-white  mb-3">
                        <div class="card-header bg-primary">
                            Database Affected details
                        </div>
                        <div class="card-body bg-default">
                            <div class="row">
                                <div class="col-lg-6" style="padding-bottom: 10px">
                                    <label>
                                        Databases</label>
                                    <asp:DropDownList ID="ddDatabases" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="DATABASE">Database</asp:ListItem>
                                        <asp:ListItem Value="SERVER">Server</asp:ListItem>
                                        <asp:ListItem Value="FIREWALL">Firewall</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="col-lg-6" style="padding-bottom: 10px">
                                    <label>
                                        Type Of Access</label>
                                    <asp:DropDownList ID="ddTypeOfAccess" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="UPDATE">Update table Details</asp:ListItem>
                                        <asp:ListItem Value="DELETE">Delete table data</asp:ListItem>
                                        <asp:ListItem Value="INSERT">Insert table Details</asp:ListItem>
                                        <asp:ListItem Value="CREATE">Create Database Objects</asp:ListItem>
                                        <asp:ListItem Value="ANY">Full DB Access</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-6" style="padding-bottom: 10px">
                                    <label>
                                        Start Date And Time</label>
                                    <asp:TextBox ID="txtStartDateTime" CssClass="form-control" runat="server"></asp:TextBox>
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

                            <div class="row">
                                <div class="col-lg-12">
                                    <label>
                                        Reason</label>
                                    <asp:TextBox ID="txtReason" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-footer bg-default text-center">
                    <asp:Button ID="btnNextStep" Text="Next Step" CssClass="btn btn-md btn-success" runat="server" OnClick="btnNextStep_Click"  />
                </div>
            </div>

        </div>
        <div class="col-lg-1"></div>
    </div>
</asp:Content>