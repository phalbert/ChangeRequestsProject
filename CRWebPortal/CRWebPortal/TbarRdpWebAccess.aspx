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
                        <div class="card-header bg-primary">
                            RDP Details
                        </div>
                        <div class="card-body bg-default">
                            <div class="row">
                                <div class="col-lg-12">
                                    <label>TIME LEFT</label>
                                    <div class="count-down-timer"></div>
                                    <p class="help-block"></p>
                                </div>
                            </div>
                            <div class="container" id="processingDiv" runat="server">
                                <div class="row">
                                    <h4 style="color: black">
                                        <asp:Label ID="lblSystemState" runat="server">AWAITING START RDP SESSION</asp:Label>
                                    </h4>
                                </div>
                                <div class="row">
                                    <h4 style="color: black">
                                        <b>SYSTEM NAME :</b> <asp:Label ID="lblSystemName" runat="server">AWAITING START RDP SESSION</asp:Label>
                                    </h4>
                                </div>
                                <div class="row">
                                    <h4 style="color: black">
                                       <b>COMPUTER ID :</b> <asp:Label ID="lblUsername" runat="server">AWAITING START RDP SESSION</asp:Label>
                                    </h4>
                                </div>
                                <div class="row">
                                    <h4 style="color: black">
                                        <b>PASSWORD:</b> <asp:Label ID="lblPassword" runat="server">AWAITING START RDP SESSION</asp:Label>
                                    </h4>
                                </div>
                            </div>
                        </div>
                        <div class="card-footer bg-default text-center">
                            <asp:Button ID="btnCloseWndw" Text="Close RDP Session" CssClass="btn btn-md btn-danger btnCloseWndw" OnClick="btnCloseWndw_Click" runat="server" />
                            <asp:Button ID="btnOpenWndw" Text="Start RDP Session" CssClass="btn btn-md btn-success btnOpenWndw" OnClick="btnOpenWndw_Click" runat="server" />
                        </div>
                    </div>

                </asp:View>
                <asp:View runat="server" ID="PollingView">
                    <script type="text/javascript">

                        $(function () {

                            // Reference the auto-generated proxy for the hub.
                            var progress = $.connection.progressHub;
                            console.log(progress);

                            // Create a function that the hub can call back to display messages.
                            progress.client.addProgress = function (message, percentage) {
                                //at this point server side had send message and percentage back to the client
                                //and then we handle progress bar any way we want it

                                //Using a function in Helper.js file we show modal and display text and percentage
                                $('#StatusDesc').text(message);


                                //closing modal when the progress gets to 100%
                                if (percentage == "100%") {
                                    ProgressBarModal();
                                }
                            };

                            //Before doing anything with our hub we must start it
                            $.connection.hub.start().done(function () {

                                //getting the connection ID in case you want to display progress to the specific user
                                //that started the process in the first place.
                                var connectionId = $.connection.hub.id;
                                console.log(connectionId);
                            });

                        });
                    </script>
                    <div class="row text-center w-100" style="padding-top: 50px;">
                        <div class="col-lg-2"></div>
                        <div class="col-lg-8">
                            <%------------------------------------------ Order Summary  ---------------------------------------%>
                            <div class="card sbuColors text-white" style="border-radius: 10px; overflow: hidden; width: 100%;">
                                <div class="card-header sbuColors">
                                    <div class="text-center">
                                        <asp:Label ID="Label2" runat="server">Processing. Please Wait...</asp:Label>
                                    </div>
                                </div>
                                <div class="card-body bg-default text-black text-center">
                                    <h4 style="color: black">Processing Your Request Please Wait (Max wait Time is 8 minutes):
                                    </h4>
                                    <br />
                                    <h6>
                                        <label id="StatusDesc" runat="server" class="alert alert-info">
                                            PENDING
                                        </label>
                                    </h6>
                                    <br />
                                    <img id="imgLoading" src="Images/processing.gif" alt="" />
                                </div>
                                <div class="card-footer bg-default">
                                    <div class="row">
                                        <div class="col-lg-3"></div>
                                        <div class="col-lg-3 text-center">
                                            <asp:Button runat="server" ID="btnCancelLoad" Text="Cancel Payment" CssClass="btn btn-danger" OnClick="btnCancelLoad_Click" OnClientClick="return confirm('Are you sure that you want To Cancel this Ongoing Transaction?')" />
                                            <p class="help-block"></p>
                                        </div>
                                        <div class="col-lg-3 text-center">
                                            <asp:Button runat="server" ID="btnPoll" Text="Poll For Status" CssClass="btn btn-success" OnClick="btnPoll_Click" />
                                            <p class="help-block"></p>
                                        </div>
                                        <div class="col-lg-3"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2"></div>
                    </div>
                    <input type="hidden" id="txtVendorCode" runat="server" class="form-control txtVendorCode" />
                    <br />
                    <input type="hidden" id="txtMerchantId" runat="server" class="form-control txtMerchantId" />
                    <br />
                    <input type="hidden" id="txtVendorId" runat="server" class="form-control txtVendorId" />
                    <br />
                    <input type="hidden" id="txtPassword" runat="server" class="form-control txtPassword" />
                    <br />
                    <input type="hidden" id="txtReturnUrl" runat="server" class="form-control txtReturnUrl" />
                    <br />
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

        $('.count-down-timer').timeTo(<%=minutesLeft * 60%>, function () {
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
