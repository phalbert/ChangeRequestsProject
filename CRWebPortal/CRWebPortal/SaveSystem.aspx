<%@ Page Language="C#" MasterPageFile="~/LoggedInMaster.Master" AutoEventWireup="true" CodeBehind="SaveSystem.aspx.cs" Inherits="CRWebPortal.SaveSystem" %>

<%@ MasterType VirtualPath="~/LoggedInMaster.Master" %>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <%------------ General Details Section---------  --%>
    <div class="card border-primary text-white  mb-3">
        <div class="card-header card-header-info">
            Save Details
                                   
        </div>
        <div class="card-body bg-default">

            <div class="row">
                <div class="col-lg-6">
                    <label>
                        System Name</label>
                    <asp:TextBox ID="txtSysName" CssClass="form-control auto-complete" Style="background-color: white" runat="server" ></asp:TextBox>
                </div>
                <div class="col-lg-6">
                    <label>
                        System Code</label>
                    <asp:TextBox ID="txtSysCode" CssClass="form-control auto-complete" Style="background-color: white" runat="server" ></asp:TextBox>
                </div>
            </div>

            <br />
            <div class="row">
                <div class="col-lg-6">
                    <label>
                        System Type</label>
                    <asp:DropDownList ID="ddSysTypes" runat="server" CssClass="form-control">
                        
                    </asp:DropDownList>
                </div>
                <div class="col-lg-6">
                    <label>
                        Connection String</label>
                    <asp:TextBox ID="txtConnectionString" CssClass="form-control auto-complete" Style="background-color: white" runat="server" ></asp:TextBox>
                </div>
            </div>

            <br />
            <div class="row" id="resultsDiv" runat="server">
                <%------------ General Details Section---------  --%>
                <div class="col-lg-12">
                    <div class="card border-primary text-white mb-3">
                        <div class="card-header card-header-info">
                            Systems
                                   
                        </div>
                        <div class="card-body bg-default">
                            <div class="table-responsive">
                                <asp:GridView runat="server" Width="100%" CssClass="table table-bordered table-hover"
                                    ID="dataGridResults" OnRowCommand="dataGridResults_RowCommand" ForeColor="Black" AllowPaging="true" PageSize="15">
                                    <AlternatingRowStyle BackColor="#BFE4FF" />
                                    <HeaderStyle BackColor="#115E9B" Font-Bold="false" ForeColor="white" Font-Italic="False"
                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Height="30px" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card-footer bg-default text-center">
                <asp:Button ID="btnSave" Text="Save" CssClass="btn btn-md btn-success" runat="server" OnClick="btnSave_Click" />
            </div>
        </div>
    </div>
</asp:Content>
