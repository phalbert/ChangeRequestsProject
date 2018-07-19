<%@ Page Language="C#" Async="true" MasterPageFile="~/LoggedInMaster.Master" AutoEventWireup="true" CodeBehind="TbarRdpWebAccess.aspx.cs" Inherits="CRWebPortal.TbarRdpWebAccess" %>

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
                            <asp:Label ID="lblErrorMsg" runat="server">
                                Access Denied. Please Request for Time Bound Access Using this <a href="ApplyForTBPAccess.aspx"><b>LINK</b></a>.
                            </asp:Label>
                        </p>
                    </div>

                </asp:View>


                <asp:View ID="RequestView" runat="server">


                    <%------------ General Details Section---------  --%>
                    <div class="card border-primary text-white  mb-3">
                        <div class="card-header card-header-info">
                            Oh Ya. Lets RDP This !!
                        </div>
                        <div class="card-body bg-default">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card border-primary text-white mb-3">
                                        <div class="card-header card-header-info">
                                            Time Left
                                   
                                        </div>
                                        <div class="card-body bg-default">
                                            <div class="count-down-timer"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card border-primary text-white mb-3">
                                        <div class="card-header card-header-info">
                                            START RDP SESSION AND USE ANY DESK TO LOGIN
                                        </div>
                                        <div class="card-body bg-default">
                                            <div class="row">
                                                <div class="col-lg-6" style="color: black"><b>SYSTEM NAME :</b></div>
                                                <div class="col-lg-6" style="color: black">
                                                    <asp:Label ID="lblSystemName" runat="server">AWAITING START RDP SESSION</asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-6" style="color: black"><b>COMPUTER ID :</b></div>
                                                <div class="col-lg-6" style="color: black">
                                                    <asp:Label ID="lblUsername" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-6" style="color: black"><b>PASSWORD:</b></div>
                                                <div class="col-lg-6" style="color: black">
                                                    <asp:Label ID="lblPassword" runat="server">AWAITING START RDP SESSION</asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />

                        </div>
                        <div class="card-footer bg-default text-center">
                            <asp:Button ID="btnCloseWndw" Text="Close RDP Session" CssClass="btn btn-md btn-danger btnCloseWndw" OnClick="btnCloseWndw_Click" runat="server" />
                            <asp:Button ID="btnOpenWndw" Text="Start RDP Session" CssClass="btn btn-md btn-success btnOpenWndw" OnClick="btnOpenWndw_Click" runat="server" />
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
        
        $('.count-down-timer').timeTo(<%=minutesLeft * 60%>, function () { location.reload(); });
        
    </script>
    <%} %>
</asp:Content>
