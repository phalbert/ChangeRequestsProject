<%@ Page Language="C#" MasterPageFile="~/LoggedInMaster.Master" AutoEventWireup="true" CodeBehind="TbarRdpWebAccess.aspx.cs" Inherits="CRWebPortal.TbarRdpWebAccess" %>

<%@ MasterType VirtualPath="~/LoggedInMaster.Master" %>
<%@ Import Namespace="CRWebPortal.CRSystemAPI" %>
<asp:Content runat="server" ContentPlaceHolderID="headContentPlaceHolder" ID="headContentPlaceHolder">
    <link href="Styles/timeTo.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">


    <div class="row">
        <div class="col-lg-1"></div>
        <div class="col-lg-10">


            <asp:MultiView ActiveViewIndex="1" ID="Multiview1" runat="server">

                <asp:View ID="AccessDeniedView" runat="server">

                    <div class="alert alert-danger text-center">
                        <p>
                            <asp:Label ID="lblErrorMsg" runat="server">Access Denied. Please Request for Time Bound Access Formally!!.</asp:Label>
                        </p>
                    </div>

                </asp:View>


                <asp:View ID="RequestView" runat="server">


                    <%------------ General Details Section---------  --%>
                    <div class="card border-primary text-white  mb-3">
                        <div class="card-header bg-primary">
                            RDP Details
                        </div>
                        <div class="card-body bg-default">
                            <div class="row">
                                <div class="col-lg-12">
                                    <label>TIME LEFT</label>
                                    <div class="count-down-timer"></div>
                                </div>
                            </div>
                            <br />
                        </div>
                        <div class="card-footer bg-default text-center">
                            <asp:Button ID="btnCloseWndw" Text="Close RDP Session" CssClass="btn btn-md btn-danger btnCloseWndw" OnClick="btnCloseWndw_Click" OnClientClick="return false;" runat="server" />
                            <asp:Button ID="btnOpenWndw" Text="Start RDP Session" CssClass="btn btn-md btn-success btnOpenWndw" OnClientClick="return false;" runat="server" />
                        </div>
                    </div>

                </asp:View>

            </asp:MultiView>

        </div>
        <div class="col-lg-1"></div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="footerJsContent" ID="footer">
    <script type="text/javascript" src="Scripts/jquery.time-to.js"></script>
    <script type="text/javascript">
        <% 
        var Tbar = Session["TBAR"] as TimeBoundAccessRequest;

        if (Tbar != null)
        {
            DateTime maxDate = Tbar.StartTime.AddMinutes(Tbar.DurationInMinutes);
            DateTime currentDate = DateTime.Now;
            int minutesLeft = ((int)maxDate.Subtract(currentDate).TotalMinutes) + 1;//add an extra minute for network latency
    %>
        // Store a variable for your window
        var yourWindow;
        $(function () {
            $('.btnOpenWndw').click(function () {
                yourWindow = window.open('http://localhost:8019/RdpWeb/', 'My Server', '');
            });

            $('.btnCloseWndw').click(function () {
                yourWindow.close();
            });
        });

        $('.count-down-timer').timeTo(<%=minutesLeft * 60%>, function ()
        {
            yourWindow.close();
            window.location.replace("]<%=Server.MapPath("~/ChooseTbarMethod.aspx")%>");
        });
        // If you are leaving the page, close the child window (if applicable)
        window.onbeforeunload = function () {
            yourWindow.close();
        }
    </script>
    <%} %>
</asp:Content>
