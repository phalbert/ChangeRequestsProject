<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AttachSystemsAffected.aspx.cs" Inherits="CRWebPortal.AttachSystemsAffected" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="row">
        <div class="col-lg-4"></div>
        <div class="col-lg-4">
            <div class="panel panel-primary">
                <div class="panel-heading text-center">
                    Specify Systems that will be Affected by this Change
                </div>
                <div class="panel-body">

                    <%------------ General Details Section---------  --%>
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            Systems Affected
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-4" style="padding-bottom: 10px">
                                    <label>
                                        Type Of System</label>
                                    <asp:DropDownList ID="ddTypeOfSystem" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="DATABASE">Database</asp:ListItem>
                                        <asp:ListItem Value="SERVER">Server</asp:ListItem>
                                        <asp:ListItem Value="FIREWALL">Firewall</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-4" style="padding-bottom: 10px">
                                    <label>
                                        Name Of System</label>
                                    <asp:TextBox ID="txtNameOfSystem" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-lg-4" style="padding-bottom: 10px">
                                    <label>
                                        Type Of Change</label>
                                    <asp:DropDownList ID="ddTypeOfChange" runat="server" CssClass="form-control">
                                        <asp:ListItem>Addition/Installation of New System/Service/FirewallRule</asp:ListItem>
                                        <asp:ListItem>Update/Change Of Existing System/Service/FirewallRule</asp:ListItem>
                                        <asp:ListItem>Deletion/Stopping Of Existing System/Service/FirewallRule</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="panel-footer">
                            <asp:Button ID="btnSubmit" Text="Save Details" CssClass="btn btn-md btn-info pull-left" runat="server" OnClick="btnSubmit_Click" />
                            <br />
                            <br />
                        </div>
                    </div>

                    <%------------ View Uploaded Files---------  --%>
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            List Of Affected Systems
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="row">
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" Width="100%" CssClass="table table-bordered table-hover"
                                            ID="dataGridResults" OnRowCommand="dataGridResults_RowCommand">
                                            <AlternatingRowStyle BackColor="#BFE4FF" />
                                            <HeaderStyle BackColor="#115E9B" Font-Bold="false" ForeColor="white" Font-Italic="False"
                                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Height="30px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Systems">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnDeleteSystem" runat="server" Text="Delete" CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="panel-footer text-center">
                    <asp:Button ID="btnBack" Text="Go Back" CssClass="btn btn-md btn-danger" runat="server" OnClick="btnBack_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnNextStep" Text="Next Step" CssClass="btn btn-md btn-success" runat="server" OnClick="btnNextStep_Click" />
                </div>
            </div>

        </div>
        <div class="col-lg-4"></div>
    </div>
</asp:Content>
