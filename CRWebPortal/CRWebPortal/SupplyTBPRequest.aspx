<%@ Page Language="C#" MasterPageFile="~/LoggedInMaster.Master" AutoEventWireup="true" CodeBehind="SupplyTBPRequest.aspx.cs" Inherits="CRWebPortal.SupplyTBPRequest" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <div class="row">
        <div class="col-lg-1"></div>
        <div class="col-lg-10">
            <div class="card border-primary text-white  mb-3">
                <div class="card-header bg-primary text-center">
                    Time Bound Privileged DB Access Details
                </div>
                <div class="card-body bg-default">

                    <div class="row">
                        <div class="col-lg-6" style="padding-bottom: 10px">
                            <label>
                                Database</label>
                            <asp:DropDownList ID="ddDatabases" runat="server" CssClass="form-control">
                                <asp:ListItem Value="DATABASE">Database</asp:ListItem>
                                <asp:ListItem Value="SERVER">Server</asp:ListItem>
                                <asp:ListItem Value="FIREWALL">Firewall</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-6" style="padding-bottom: 10px">
                            <label>
                                Table</label>
                            <asp:DropDownList ID="ddTable" runat="server" CssClass="form-control">
                                <asp:ListItem Value="DATABASE">Database</asp:ListItem>
                                <asp:ListItem Value="SERVER">Server</asp:ListItem>
                                <asp:ListItem Value="FIREWALL">Firewall</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <div class="row text-center">
                         <label>
                                Reason For Access</label>
                        <asp:TextBox ID="txtReason" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>

                </div>
                <div class="card-footer bg-default text-center">
                    <asp:Button ID="btnSubmit" Text="Request For Access" CssClass="btn btn-md btn-success" runat="server" OnClick="btnSubmit_Click" />
                </div>
            </div>
        </div>
        <div class="col-lg-1"></div>
    </div>
</asp:Content>
