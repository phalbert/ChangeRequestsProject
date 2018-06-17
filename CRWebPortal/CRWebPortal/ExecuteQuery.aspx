<%@ Page Language="C#" MasterPageFile="~/LoggedInMaster.Master" AutoEventWireup="true" CodeBehind="ExecuteQuery.aspx.cs" Inherits="CRWebPortal.ExecuteQuery" %>

<%@ MasterType VirtualPath="~/LoggedInMaster.Master" %>
<%@ Import Namespace="CRWebPortal.CRSystemAPI" %>
<asp:Content runat="server" ContentPlaceHolderID="headContentPlaceHolder" ID="headContentPlaceHolder">
    <link href="Styles/timeTo.css" rel="Stylesheet" />
    <link href="Styles/easy-autocomplete.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">


    <div class="row">
        <div class="col-lg-1"></div>
        <div class="col-lg-10">
            <div class="card border-primary text-white  mb-3">

                <asp:MultiView ActiveViewIndex="1" ID="Multiview1" runat="server">

                    <asp:View ID="AccessDeniedView" runat="server">

                        <div class="alert alert-danger text-center">
                            <p>Access Denied. Please Request for Time Bound Access Formally!!.</p>
                        </div>

                    </asp:View>


                    <asp:View ID="RequestView" runat="server">


                        <div class="card-body bg-default">
                            <%------------ General Details Section---------  --%>
                            <div class="card border-primary text-white  mb-3">
                                <div class="card-header bg-primary">
                                    Query Details
                                   
                                </div>
                                <div class="card-body bg-default">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <label>TIME LEFT</label>
                                            <div class="count-down-timer"></div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <label>
                                                Enter Query</label>
                                            <asp:TextBox ID="txtQuery" CssClass="form-control auto-complete" Style="background-color: white" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row" id="resultsDiv" runat="server">
                                        <div class="col-lg-12">
                                            <label>
                                                Query Results</label>
                                            <div class="table-responsive">
                                                <asp:GridView runat="server" Width="100%" CssClass="table table-bordered table-hover"
                                                    ID="dataGridResults" OnRowCommand="dataGridResults_RowCommand" ForeColor="Black">
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
                                    <asp:Button ID="btnComplete" Text="Confirm Execution" CssClass="btn btn-md btn-success" runat="server" OnClick="btnComplete_Click" />
                                </div>
                            </div>
                        </div>


                    </asp:View>

                </asp:MultiView>
            </div>
        </div>
        <div class="col-lg-1"></div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="footerJsContent" ID="footer">
    <script type="text/javascript" src="Scripts/jquery.time-to.js"></script>
    <script type="text/javascript" src="Scripts/jquery.a-tools-1.4.1.js"></script>
    <script type="text/javascript" src="Scripts/jquery.asuggest.js"></script>
    <% 
        var Tbar = Session["TBAR"] as TimeBoundAccessRequest;
        var Tables = Session["Tables"] as string;
        if (Tbar != null)
        {
    %>
    <script type="text/javascript">

        //https://lexxus.github.io/jq-timeTo/
        $('.count-down-timer').timeTo(<%=Tbar.DurationInMinutes * 60%>, function () { });

        //http://imankulov.github.io/asuggest/
        var suggests = [<%=Tables%>];
        $(".auto-complete").asuggest(suggests);
    </script>
    <%} %>
</asp:Content>
