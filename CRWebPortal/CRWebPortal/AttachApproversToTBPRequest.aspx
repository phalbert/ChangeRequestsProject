<%@ Page Language="C#" MasterPageFile="~/LoggedInMaster.Master" AutoEventWireup="true" CodeBehind="AttachApproversToTBPRequest.aspx.cs" Inherits="CRWebPortal.AttachApproversToTBPRequest" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="row">
        <div class="col-12">
            <ul class='nav nav-wizard'>

                <li><a href="CreateChangeRequest.aspx" data-toggle="tab">Privilleged Access Details</a></li>
                <li class="active"><a href="AttachApproversToCR.aspx" data-toggle="tab">Assign Approvers</a></li>
                <li><a href="Finished.aspx" data-toggle="tab">Done</a></li>

            </ul>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-lg-1"></div>
        <div class="col-lg-10">
            <div class="card border-primary text-white  mb-3">
                <div class="card-header bg-primary">
                    Specify Approvers Time Bound Privelleged Access Request
                </div>
                <div class="card-body bg-default">

                    <%------------ General Details Section---------  --%>
                    <div class="card border-primary text-white  mb-3">
                        <div class="card-header bg-primary">
                            Select Approvers/Reviewers
                        </div>
                        <div class="card-body bg-default">
                            <div class="row">
                                <div class="col-lg-6" style="padding-bottom: 10px">
                                    <label>
                                        Name Of Person</label>
                                    <asp:DropDownList ID="ddNameOfApprover" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="APPROVER">Nsubuga Kasozi</asp:ListItem>
                                        <asp:ListItem Value="APPROVER">Paul Kavule</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
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
