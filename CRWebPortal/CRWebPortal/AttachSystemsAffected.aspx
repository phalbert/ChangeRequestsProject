<%@ Page Language="C#" MasterPageFile="~/LoggedInMaster.Master" AutoEventWireup="true" CodeBehind="AttachSystemsAffected.aspx.cs" Inherits="CRWebPortal.AttachSystemsAffected" %>

<%@ MasterType VirtualPath="~/LoggedInMaster.Master" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <div class="row">
        <div class="col-12">
            <ul class='nav nav-wizard'>

                <li><a href="CreateChangeRequest.aspx" data-toggle="tab">Change Details</a></li>
                <li class="active"><a href="AttachSystemsAffected.aspx" data-toggle="tab">Systems Affected</a></li>
                <li><a href="AttachItemToChangeRequest.aspx" data-toggle="tab">Any Attachments</a></li>
                <li><a href="AttachApproversToCR.aspx" data-toggle="tab">Assign Approvers</a></li>
                <li><a href="Finished.aspx" data-toggle="tab">Done</a></li>

            </ul>
        </div>
    </div>
    <br />

    <div class="row">
        <div class="col-lg-1"></div>
        <div class="col-lg-10">
            <div class="card border-primary text-white  mb-3">
                <div class="card-header bg-primary text-center">
                    Specify Systems that will be Affected by this Change
                </div>
                <div class="card-body bg-default">

                    <%------------ General Details Section---------  --%>
                    <div class="card border-primary text-white  mb-3">
                        <div class="card-header bg-primary">
                            Systems Affected
                        </div>
                        <div class="card-body bg-default">
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
                        <div class="card-footer">
                            <asp:Button ID="btnSubmit" Text="Save Details" CssClass="btn btn-md btn-info pull-left" runat="server" OnClick="btnSubmit_Click" />
                            <br />
                            <br />
                        </div>
                    </div>

                    <%------------ View Uploaded Files---------  --%>
                    <div class="card border-primary text-white  mb-3">
                        <div class="card-header bg-primary">
                            List Of Affected Systems
                        </div>
                        <div class="card-body bg-default">
                            <div class="row text-center">
                                <asp:GridView runat="server" Width="100%" CssClass="table table-bordered"
                                    ID="dataGridResults" OnRowCommand="dataGridResults_RowCommand" ForeColor="black">
                                    <AlternatingRowStyle BackColor="#BFE4FF" />
                                    <HeaderStyle BackColor="#115E9B" Font-Bold="false" ForeColor="white" Font-Italic="False"
                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
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
                <div class="card-footer bg-default text-center">
                    <asp:Button ID="btnBack" Text="Go Back" CssClass="btn btn-md btn-danger" runat="server" OnClick="btnBack_Click" />
                    <asp:Button ID="btnNextStep" Text="Next Step" CssClass="btn btn-md btn-success" runat="server" OnClick="btnNextStep_Click" />
                </div>
            </div>

        </div>
        <div class="col-lg-1"></div>
    </div>
</asp:Content>
