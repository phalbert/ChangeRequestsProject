<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateChangeRequest.aspx.cs" Inherits="CRWebPortal.CreateChangeRequest" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="row">
        <div class="col-lg-4"></div>
        <div class="col-lg-4">
            <div class="panel panel-primary">
                <div class="panel-heading text-center">
                    Supply Change Request Details Below
                </div>


                <div class="panel-body">

                    <%------------ General Details Section---------  --%>
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            General Information
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12" style="padding-bottom: 10px">
                                    <label>
                                        Title Of Change Request</label>
                                    <asp:TextBox ID="txtTitle"  runat="server" CssClass="form-control"
                                        placeholder="Enter text" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <%------------ Change Description Section---------  --%>
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            Change Description (What is the Change About)
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12" style="padding-bottom: 10px">
                                    <label>
                                        Current Problem Description</label>
                                    <asp:TextBox TextMode="MultiLine" ID="txtProblemDesc"  runat="server" CssClass="form-control"
                                        placeholder="Enter text" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12" style="padding-bottom: 10px">
                                    <label>
                                        Solution Description</label>
                                    <asp:TextBox TextMode="MultiLine" ID="txtSolutionDesc"  runat="server" CssClass="form-control"
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
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            Justify why the proposed changes should be implemented
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12" style="padding-bottom: 10px">
                                    <label>
                                        Justification</label>
                                    <asp:TextBox TextMode="MultiLine" ID="txtJustification"  runat="server" CssClass="form-control"
                                        placeholder="Enter text" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12" style="padding-bottom: 10px">
                                    <label>
                                        Impact of Not Implementing</label>
                                    <asp:TextBox TextMode="MultiLine" ID="TextBox2"  runat="server" CssClass="form-control"
                                        placeholder="Enter text" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <%------------ Requestor Details Section---------  --%>
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            Requestor Details (Who Requested for this)
                        </div>
                        <div class="panel-body">

                            <div class="row">
                                <div class="col-lg-6" style="padding-bottom: 10px">
                                    <label>
                                        Requestor Name</label>
                                    <asp:TextBox ID="txtCustName"  runat="server" CssClass="form-control"
                                        placeholder="Enter text" />
                                </div>
                                <div class="col-lg-6" style="padding-bottom: 10px">
                                    <label>
                                        Requestor Email</label>
                                    <asp:TextBox ID="txtEmail"  runat="server" CssClass="form-control"
                                        placeholder="Enter text" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6" style="padding-bottom: 10px">
                                    <label>
                                        Requestor Physical Address</label>
                                    <asp:TextBox ID="txtReqAddress"  runat="server" CssClass="form-control"
                                        placeholder="Enter text" />
                                </div>
                                <div class="col-lg-6" style="padding-bottom: 10px">
                                    <label>
                                        Date of Request</label>
                                    <asp:TextBox ID="txtDateOfRequest"  runat="server" CssClass="form-control"
                                        placeholder="Enter text" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <%------------ Implementer Details Section---------  --%>
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            Implementer Details (Who is implenting this)
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-6" style="padding-bottom: 10px">
                                    <label>
                                        Implementer Name</label>
                                    <asp:TextBox ID="txtImplementerName"  runat="server" CssClass="form-control"
                                        placeholder="Enter text" />
                                </div>
                                <div class="col-lg-6" style="padding-bottom: 10px">
                                    <label>
                                        Implementer Email</label>
                                    <asp:TextBox ID="txtImplementerEmail"  runat="server" CssClass="form-control"
                                        placeholder="Enter text" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6" style="padding-bottom: 10px">
                                    <label>
                                        Implementer Phone</label>
                                    <asp:TextBox ID="txtImplementerPhone"  runat="server" CssClass="form-control"
                                        placeholder="Enter text" />
                                </div>
                                <div class="col-lg-6" style="padding-bottom: 10px">
                                    <label>
                                        Date of Implementation</label>
                                    <asp:TextBox ID="txtDateOfImplementation"  runat="server" CssClass="form-control"
                                        placeholder="Enter text" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-footer text-center">
                    <asp:Button ID="btnBack" Text="Go Back" CssClass="btn btn-md btn-danger" runat="server" OnClick="btnBack_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnSubmit" Text="Go to Next Step" CssClass="btn btn-md btn-success" runat="server" OnClick="btnSubmit_Click" />
                </div>
            </div>

        </div>
        <div class="col-lg-4"></div>
    </div>
</asp:Content>
