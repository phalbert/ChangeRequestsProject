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
                                <div class="row"><label><h4>AWAITING START RDP SESSION</h4></label></div><br />
                                <div class="row text-center"><img id="imgLoading" src="Images/processing.gif" alt="" /><p class="help-block"></p></div>

                            </div>
                            <div class="row" id="imgDiv" runat="server" visible="false">
                                <p class="help-block">SEE BELOW TV DETAILS</p>
                                <asp:Image ID="tvImageDesktop" class="img-fluid" Height="500px" Width="500px" runat="server" />
                                <p class="help-block"></p>
                            </div>
                        </div>
                        <div class="card-footer bg-default text-center">
                            <asp:Button ID="btnCloseWndw" Text="Close RDP Session" CssClass="btn btn-md btn-danger btnCloseWndw" OnClick="btnCloseWndw_Click"  runat="server" />
                            <asp:Button ID="btnOpenWndw" Text="Start RDP Session" CssClass="btn btn-md btn-success btnOpenWndw" OnClick="btnOpenWndw_Click" runat="server" />
                        </div>
                    </div>

                </asp:View>
                <asp:View runat="server" ID="PollingView">
                    <script type="text/javascript">

                        $(document).ready(function () {
                            SetTime();
                            setTimeout(CallHandler, 10000);
                        });

                        function CallHandler() {

                            var obj = {
                                'VendorCode': $('.txtVendorCode').val(),
                                'Pswd': $('.txtPassword').val(),
                                'MerchantId': $('.txtMerchantId').val(),
                                'TranId': $('.txtVendorId').val(),
                                'ReturnUrl': $('.txtReturnUrl').val()
                            }

                            $.ajax({
                                url: "PollRequestHandler.ashx",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                data: obj,
                                responseType: "json",
                                success: OnComplete,
                                error: OnFail
                            });

                            return false;
                        }

                        function ConfirmCancel() {
                            if (confirm('Are you sure that you want To Cancel this Ongoing Transaction?') == true) {
                                return true;
                            }
                            return false;
                        }

                        function SetTime() {
                            var dt = new Date();
                            var time = dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();
                            $('#lastQueryTime').text(time);
                            $('#lastQueryTime').css('color', '#449D44');
                        }

                        function OnComplete(Status) {

                            var StatusCode = Status.StatusCode;
                            var StatusDesc = Status.StatusDesc;
                            var ReturnUrl = Status.ReturnUrl;

                            //success
                            if (StatusCode == "0") {
                                SetTime();
                                $('#StatusDesc').text('Transaction was Successfull');
                                changeClass('alert-info', 'alert-success');
                                window.location = ReturnUrl;
                            }
                            //pending
                            else if (StatusCode == "122" || StatusCode == "1000") {
                                SetTime();
                                $('#StatusDesc').text(StatusDesc);
                                changeClass('alert-info', 'alert-info');
                                setTimeout(CallHandler, 3000);
                            }
                            //failed
                            else {
                                SetTime();
                                $('#StatusDesc').text("Transaction Failed: " + StatusDesc);
                                changeClass('alert-info', 'alert-danger');
                                $("#imgLoading").hide();
                                window.location = ReturnUrl;
                            }
                        }

                        function OnFail(result) {
                            SetTime();
                            $('#StatusDesc').text('Request Failed. Check your Internet Connection. Retrying...');
                            changeClass('alert-info', 'alert-danger');
                            setTimeout(CallHandler, 3000);
                        }

                        function changeClass(oldClassName, newClassName) {
                            $("#StatusDesc." + oldClassName).removeClass(oldClassName).addClass(newClassName);
                        }
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
                            <br />
                                        (You may need to approve a debit prompt sent to your phone [
                            <label id="lblPhone" runat="server"></label>
                                        ]).
                                    </h4>
                                    <br />
                                    <h6 style="color: black">Time of Last Status Check:
                            <label id="lastQueryTime" class="alert alert-success">
                            </label>
                                        Status:
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
