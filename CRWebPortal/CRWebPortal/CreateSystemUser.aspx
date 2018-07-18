<%@ Page Title="" Language="C#" MasterPageFile="~/LoggedInMaster.Master" AutoEventWireup="true" CodeBehind="CreateSystemUser.aspx.cs" Inherits="CRWebPortal.CreateSystemUser" %>

<%@ MasterType VirtualPath="~/LoggedInMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContentPlaceHolder" runat="server">
</asp:Content>

<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <%------------ General Details Section---------  --%>
    <div class="card border-primary text-white  mb-3">
        <div class="card-header bg-primary">
            Save SystemUser Details
        </div>

        <div class="card-body bg-default">

            <div class="row">
                <div class="col-lg-6">
                    <label>
                        Full Name
                    </label>
                    <asp:TextBox ID="txtFullName" CssClass="form-control auto-complete" Style="background-color: white" runat="server"></asp:TextBox>
                </div>
                <div class="col-lg-6">
                    <label>
                        User Name
                    </label>
                    <asp:TextBox ID="txtUsername" CssClass="form-control auto-complete" Style="background-color: white" runat="server"></asp:TextBox>
                </div>
            </div>
            <br />

            <div class="row">
                <div class="col-lg-6">
                    <label>
                        User Role
                    </label>
                    <asp:DropDownList ID="ddRoles" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-lg-6">
                    <label>
                        Email
                    </label>
                    <asp:TextBox ID="txtEmail" CssClass="form-control auto-complete" Style="background-color: white" runat="server"></asp:TextBox>
                </div>
            </div>
            <br />

            <div class="row">
                <div class="col-lg-6">
                    <label>
                        Phone
                    </label>
                    <asp:TextBox ID="txtPhoneNumber" CssClass="form-control auto-complete" Style="background-color: white" runat="server"></asp:TextBox>

                </div>
                <div class="col-lg-6">
                    <label>
                        Domain Account
                    </label>
                    <asp:TextBox ID="txtDomainAccount" CssClass="form-control auto-complete" Style="background-color: white" runat="server"></asp:TextBox>
                </div>
            </div>
            <br />

            <div class="row" id="resultsDiv" runat="server">
                <%------------ General Details Section---------  --%>
                <div class="col-lg-12">
                    <div class="card border-primary text-white mb-3">
                        <div class="card-header bg-primary">
                            System Users
                                   
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
<asp:Content ID="Content3" ContentPlaceHolderID="footerJsContent" runat="server">
</asp:Content>
