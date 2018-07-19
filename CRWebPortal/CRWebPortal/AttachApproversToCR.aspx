<%@ Page Language="C#" MasterPageFile="~/LoggedInMaster.Master" AutoEventWireup="true" CodeBehind="AttachApproversToCR.aspx.cs" Inherits="CRWebPortal.AttachApproversToCR" %>

<%@ MasterType VirtualPath="~/LoggedInMaster.Master" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <div class="row">
        <div class="col-12">
            <ul class='nav nav-wizard'>

                <li><a href="CreateChangeRequest.aspx" data-toggle="tab">CR Details</a></li>
                <li><a href="AttachSystemsAffected.aspx" data-toggle="tab">Systems Affected</a></li>
                <li><a href="AttachPostChangeTests.aspx" data-toggle="tab">Post Change Tests</a></li>
                <li><a href="AttachItemToChangeRequest.aspx" data-toggle="tab">Any Attachments</a></li>
                <li><a class="active" href="AttachApproversToCR.aspx" data-toggle="tab">Assign Approvers</a></li>
                <li><a href="Finished.aspx" data-toggle="tab">Done</a></li>

            </ul>
        </div>
    </div>
    <br />

    <div class="row">
        <div class="col-lg-1"></div>
        <div class="col-lg-10">
            <div class="card border-primary text-white  mb-3">
                <div class="card-header card-header-info">
                    Specify Approvers/Reviewers For this Change Request
                </div>
                <div class="card-body bg-default">

                    <%------------ General Details Section---------  --%>
                    <div class="card border-primary text-white  mb-3">
                        <div class="card-header card-header-info">
                            Select Approvers/Reviewers
                        </div>
                        <div class="card-body bg-default">
                            <div class="row">
                                <div class="col-lg-6" style="padding-bottom: 10px">
                                    <label>
                                        Stage of CR Approval</label>
                                    <asp:DropDownList ID="ddTypeOfApprover" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="REVIEWER">Reviewer</asp:ListItem>
                                        <asp:ListItem Value="APPROVER">Approver</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-6" style="padding-bottom: 10px">
                                    <label>
                                        Name Of Person</label>
                                    <asp:DropDownList ID="ddNameOfApprover" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="nsubugak">Nsubuga Kasozi</asp:ListItem>
                                        <asp:ListItem Value="paul.kavule@pegasus.co.ug">Paul Kavule</asp:ListItem>
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
                        <div class="card-header card-header-info">
                            Selected Change Control Board
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
                <div class="card-footer text-center">
                    <asp:Button ID="btnBack" Text="Go Back" CssClass="btn btn-md btn-danger" runat="server" OnClick="btnBack_Click" />
                    <asp:Button ID="btnNextStep" Text="Next Step" CssClass="btn btn-md btn-success" runat="server" OnClick="btnNextStep_Click" />
                </div>
            </div>

        </div>
        <div class="col-lg-1"></div>
    </div>
</asp:Content>
