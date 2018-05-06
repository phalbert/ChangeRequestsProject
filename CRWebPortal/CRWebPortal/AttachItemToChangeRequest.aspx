<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AttachItemToChangeRequest.aspx.cs" Inherits="CRWebPortal.AttachItemToChangeRequest" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="row">
        <div class="col-lg-4"></div>
        <div class="col-lg-4">
            <div class="panel panel-primary">
                <div class="panel-body">

                    <%------------ General Details Section---------  --%>
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            Upload Any Attachments to the Change Request
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12" style="padding-bottom: 10px">
                                    <label>
                                        Browse For File</label>
                                    <asp:FileUpload ID="fuAttachment" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <%------------ View Uploaded Files---------  --%>
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            View Attachments
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
                                                <asp:TemplateField HeaderText="Attachment">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnDeleteAttachment" runat="server" Text="Delete" CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
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
                    <asp:Button ID="btnSubmit" Text="Upload Attachment" CssClass="btn btn-md btn-primary" runat="server" OnClick="btnSubmit_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnNextStep" Text="Next Step" CssClass="btn btn-md btn-success" runat="server" OnClick="btnNextStep_Click" />
                </div>
            </div>

        </div>
        <div class="col-lg-4"></div>
    </div>
</asp:Content>
