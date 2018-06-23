<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChooseOtpMethod.aspx.cs" Inherits="CRWebPortal.ChooseOtpMethod" %>

<!DOCTYPE html>
<html>
<head>

    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>CR Login</title>
    <!-- Tell the browser to be responsive to screen width -->
    <script>
        window.paceOptions = {
            ajax: {
                trackMethods: ['GET', 'POST']
            }
        }
    </script>
    <script type="text/jscript" src="Scripts/pace.min.js"></script>
    <link href="Styles/pace-theme-bounce.css" rel="stylesheet" />
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" rel="stylesheet">
    <link href="Styles/style.css" rel="stylesheet">
</head>
<body class="pjax-container" cz-shortcut-listen="true" class="content-wrapper">
    <form  runat="server">
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
                                <label>Pick OTP Delivery Method</label>
                                <br />
                                <asp:RadioButton ID="rdEmail" Text="&nbsp;Email" Checked="true" runat="server" GroupName="OTPType" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdPhone" Text="&nbsp;Phone" runat="server" GroupName="OTPType" />
                            </div>

                            <div class="form-group text-center m-t-20">
                                <div class="col-xs-12">
                                    <asp:Button ID="btnBack" Text="Go Back" CssClass="btn btn-md btn-primary" runat="server" OnClick="btnBack_Click" />
                                    <asp:Button ID="btnSubmit" Text="Send OTP" CssClass="btn btn-md btn-success" runat="server" OnClick="btnSubmit_Click" />
                                </div>
                            </div>

                            <div class="form-group text-center m-t-20">
                                <div class="col-xs-12">
                                    <blockquote>
                                        <small>
                                            <asp:Label ID="lblMsg" runat="server">Selecting Email means that the OTP will be sent to the email you used on Registration. Selecting Phone means that the OTP will be sent to the phone you used on Registration</asp:Label>
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
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery.pjax/2.0.1/jquery.pjax.min.js"></script>
        <!-- Bootstrap Core JavaScript -->
        <script type="text/jscript" src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.bundle.min.js"></script>
        <!-- plugin -->

        <script>
            $(document).on('submit', 'form[data-pjax]', function (event) {
                $.pjax.submit(event, '.pjax-container')
            })
        </script>
    </form>
</body>
</html>

