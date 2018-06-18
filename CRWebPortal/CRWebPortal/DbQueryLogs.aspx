<%@ Page Title="" Language="C#" MasterPageFile="~/LoggedInMaster.Master" AutoEventWireup="true" CodeBehind="DbQueryLogs.aspx.cs" Inherits="CRWebPortal.DbQueryLogs" %>

<%@ MasterType VirtualPath="~/LoggedInMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-3">
            <label>User Id</label>
            <asp:DropDownList ID="ddUsers" runat="server" CssClass="form-control">
            </asp:DropDownList>
        </div>
        <div class="col-sm-3">
            <label>Start DateTime</label>
            <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control form_datetime" placeholder="Enter text" />
        </div>
        <div class="col-sm-3">
            <label>End DateTime</label>
            <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control form_datetime" placeholder="Enter text" />
        </div>
         <div class="col-sm-3" style="padding-top:30px;">
            <asp:Button ID="btnSubmit" runat="server" Text="Search" Width="200px" CssClass="btn btn-success btn-md" OnClick="btnSubmit_Click" />
        </div>
    </div>

   
    <asp:MultiView runat="server" ID="Multiview2" ActiveViewIndex="1">
        <asp:View runat="server" ID="resultView">
         
            <hr />
            <div class="row">
                <div class="table-responsive">
                    <asp:GridView runat="server" Width="100%" CssClass="table table-bordered table-hover" ID="dataGridResults">
                        <AlternatingRowStyle BackColor="#BFE4FF" />
                        <HeaderStyle BackColor="#115E9B" Font-Bold="false" ForeColor="white" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Height="30px" />
                    </asp:GridView>
                </div>
            </div>
        </asp:View>
        <asp:View runat="server" ID="EmptyView">
        </asp:View>

    </asp:MultiView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerJsContent" runat="server">
</asp:Content>
