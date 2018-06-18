<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplyOTP.aspx.cs" Inherits="CRWebPortal.SupplyOTP" %>

<!DOCTYPE html>
<html>
<head>

    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>CR Login</title>
    <!-- Tell the browser to be responsive to screen width -->
    <script type="text/jscript" src="Scripts/pace.min.js"></script>
    <link href="Styles/pace-theme.css" rel="stylesheet" />
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" rel="stylesheet">
    <link href="Styles/style.css" rel="stylesheet">
</head>
<body cz-shortcut-listen="true" class="content-wrapper">
    <form runat="server">
        <div class="preloader" style="display: none;">
            <div class="cssload-speeding-wheel"></div>
        </div>
        <div id="wrapper">

            <section id="wrapper" class="login-register">
                <div class="login-box login-sidebar">
                    <div class="white-box">

                        <div class="form-horizontal">
                            <a href="javascript:void(0)" class="text-center db">
                                <img width="300" height="300" src="Images/pegasus.png" alt="Home"><br>
                            </a>


                            <div class="form-group text-center m-t-20">
                                <asp:TextBox ID="txtOTP" placeholder="Enter Your One Time Password" TextMode="Password" Style="border-color: #428BCA" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group text-center m-t-20">
                                <div class="col-xs-12">
                                    <asp:Button ID="btnBack" Text="Go Back" CssClass="btn btn-md btn-danger" runat="server" OnClick="btnBack_Click" />
                                    <asp:Button ID="btnResend" Text="Resend OTP" CssClass="btn btn-md btn-info" runat="server" OnClick="btnResend_Click" />
                                    <asp:Button ID="btnVerify" Text="Verify OTP" CssClass="btn btn-md btn-success" runat="server" OnClick="btnVerifyOTP_Click" />
                                </div>
                            </div>

                            <div class="form-group text-center m-t-20">
                                <div class="col-xs-12">
                                    <blockquote>
                                        <small>
                                            <asp:Label ID="lblMsg" runat="server">Check your Trash/Spam folders on Email or SMS depending on the delivery method selected</asp:Label>
                                        </small>
                                    </blockquote>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
        <!-- jQuery -->
        <script type="text/jscript" src="https://code.jquery.com/jquery-3.3.1.min.js"></script>
        <!-- Bootstrap Core JavaScript -->
        <script type="text/jscript" src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.bundle.min.js"></script>
        <!-- plugin -->
    </form>
</body>
</html>

