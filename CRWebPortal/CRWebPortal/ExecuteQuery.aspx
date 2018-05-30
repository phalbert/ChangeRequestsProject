<%@ Page Language="C#" MasterPageFile="~/LoggedInMaster.Master" AutoEventWireup="true" CodeBehind="ExecuteQuery.aspx.cs" Inherits="CRWebPortal.ExecuteQuery" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">


    <div class="row">
        <div class="col-lg-1"></div>
        <div class="col-lg-10">
            <div class="card border-primary text-white  mb-3">

                <div class="card-body bg-default">

                    <%------------ General Details Section---------  --%>
                    <div class="card border-primary text-white  mb-3">
                        <div class="card-header bg-primary">
                            Query Details
                        </div>
                        <div class="card-body bg-default">
                            <div class="row">
                                <div class="col-lg-12">
                                    <label>
                                        Enter Query</label>
                                    <asp:TextBox ID="txtReason" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row" id="resultsDiv" runat="server">
                                <div class="col-lg-12">
                                    <label>
                                        Query Results</label>
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" Width="100%" CssClass="table table-bordered table-hover"
                                            ID="dataGridResults" OnRowCommand="dataGridResults_RowCommand">
                                            <AlternatingRowStyle BackColor="#BFE4FF" />
                                            <HeaderStyle BackColor="#115E9B" Font-Bold="false" ForeColor="white" Font-Italic="False"
                                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Height="30px" />

                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-footer bg-default text-center">
                            <asp:Button ID="btnCancel" Text="Cancel" CssClass="btn btn-md btn-danger" runat="server" OnClick="btnCancel_Click" />
                            <asp:Button ID="btnExecute" Text="Execute" CssClass="btn btn-md btn-success" runat="server" OnClick="btnExecute_Click" />
                        </div>
                    </div>

                </div>

            </div>
            <div class="col-lg-1"></div>
        </div>
    </div>
</asp:Content>
