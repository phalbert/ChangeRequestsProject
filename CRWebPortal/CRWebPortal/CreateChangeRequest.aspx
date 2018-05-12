<%@ Page Language="C#" MasterPageFile="~/LoggedInMaster.Master" AutoEventWireup="true" CodeBehind="CreateChangeRequest.aspx.cs" Inherits="CRWebPortal.CreateChangeRequest" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <div class="row">
        <div class="col-12">
            <ul class='nav nav-wizard'>

                <li class='active'><a href="CreateChangeRequest.aspx" data-toggle="tab">CR Details</a></li>
                <li><a href="AttachSystemsAffected.aspx" data-toggle="tab">Systems Affected</a></li>
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
                    Supply Change Request Details Below
                </div>


                <div class="card-body bg-default">

                    <%------------ General Details Section -----------  --%>
                    <div class="card border-primary text-white  mb-3">
                        <div class="card-header bg-primary">
                            General Information
                        </div>
                        <div class="card-body bg-default">
                            <div class="row">
                                <div class="col-lg-12" style="padding-bottom: 10px">
                                    <label>
                                        Title Of Change Request</label>
                                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"
                                        placeholder="Enter text" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <%------------ Change Description Section -----------  --%>
                    <div class="card border-primary text-white  mb-3">
                        <div class="card-header bg-primary">
                            Change Description (What is the Change About)
                        </div>
                        <div class="card-body bg-default">
                            <div class="row">
                                <div class="col-lg-12" style="padding-bottom: 10px">
                                    <label>
                                        Current Problem Description</label>
                                    <asp:TextBox TextMode="MultiLine" ID="txtProblemDesc" runat="server" CssClass="form-control"
                                        placeholder="Enter text" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12" style="padding-bottom: 10px">
                                    <label>
                                        Solution Description</label>
                                    <asp:TextBox TextMode="MultiLine" ID="txtSolutionDesc" runat="server" CssClass="form-control"
                                        placeholder="Enter text" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12" style="padding-bottom: 10px">
                                    <label>
                                        Change Category</label>
                                    <asp:DropDownList runat="server" CssClass="form-control">
                                        <asp:ListItem>New System/Service</asp:ListItem>
                                        <asp:ListItem>Enhancement/Update Of Existing System</asp:ListItem>
                                        <asp:ListItem>Termination Of Existing System/Service</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%------------ Justification Details Section---------  --%>
                    <div class="card border-primary text-white  mb-3">
                        <div class="card-header bg-primary">
                            Justify why the proposed changes should be implemented
                        </div>
                        <div class="card-body bg-default">
                            <div class="row">
                                <div class="col-lg-12" style="padding-bottom: 10px">
                                    <label>
                                        Justification</label>
                                    <asp:TextBox TextMode="MultiLine" ID="txtJustification" runat="server" CssClass="form-control"
                                        placeholder="Enter text" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12" style="padding-bottom: 10px">
                                    <label>
                                        Impact of Not Implementing</label>
                                    <asp:TextBox TextMode="MultiLine" ID="TextBox2" runat="server" CssClass="form-control"
                                        placeholder="Enter text" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <%------------ Requestor Details Section ---------  --%>
                    <div class="card border-primary text-white  mb-3">
                        <div class="card-header bg-primary">
                            Requestor Details (Who Requested for this)
                        </div>
                        <div class="card-body bg-default">

                            <div class="row">
                                <div class="col-lg-6" style="padding-bottom: 10px">
                                    <label>
                                        Requestor Name</label>
                                    <asp:TextBox ID="txtCustName" runat="server" CssClass="form-control"
                                        placeholder="Enter text" />
                                </div>
                                <div class="col-lg-6" style="padding-bottom: 10px">
                                    <label>
                                        Requestor Email</label>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"
                                        placeholder="Enter text" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6" style="padding-bottom: 10px">
                                    <label>
                                        Requestor Physical Address</label>
                                    <asp:TextBox ID="txtReqAddress" runat="server" CssClass="form-control"
                                        placeholder="Enter text" />
                                </div>
                                <div class="col-lg-6" style="padding-bottom: 10px">
                                    <label>
                                        Date of Request</label>
                                    <asp:TextBox ID="txtDateOfRequest" runat="server" CssClass="form-control"
                                        placeholder="Enter text" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <%------------ Implementer Details Section---------  --%>
                    <div class="card border-primary text-white  mb-3">
                        <div class="card-header bg-primary">
                            Implementer Details (Who is implenting this)
                        </div>
                        <div class="card-body bg-default">
                            <div class="row">
                                <div class="col-lg-6" style="padding-bottom: 10px">
                                    <label>
                                        Implementer Name</label>
                                    <asp:TextBox ID="txtImplementerName" runat="server" CssClass="form-control"
                                        placeholder="Enter text" />
                                </div>
                                <div class="col-lg-6" style="padding-bottom: 10px">
                                    <label>
                                        Implementer Email</label>
                                    <asp:TextBox ID="txtImplementerEmail" runat="server" CssClass="form-control"
                                        placeholder="Enter text" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6" style="padding-bottom: 10px">
                                    <label>
                                        Implementer Phone</label>
                                    <asp:TextBox ID="txtImplementerPhone" runat="server" CssClass="form-control"
                                        placeholder="Enter text" />
                                </div>
                                <div class="col-lg-6" style="padding-bottom: 10px">
                                    <label>
                                        Date of Implementation</label>
                                    <asp:TextBox ID="txtDateOfImplementation" runat="server" CssClass="form-control"
                                        placeholder="Enter text" />
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="card-footer bg-default text-center">
                    <asp:Button ID="btnBack" Text="Go Back" CssClass="btn btn-md btn-danger" runat="server" OnClick="btnBack_Click" />
                    <asp:Button ID="btnSubmit" Text="Next Step" CssClass="btn btn-md btn-success" runat="server" OnClick="btnSubmit_Click" />
                </div>
            </div>
        </div>
        <div class="col-lg-1"></div>
    </div>
</asp:Content>
