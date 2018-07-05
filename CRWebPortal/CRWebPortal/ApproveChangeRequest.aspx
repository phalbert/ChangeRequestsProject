<%@ Page Title="ApproveChangeRequest" Language="C#" AutoEventWireup="true" CodeBehind="ApproveChangeRequest.aspx.cs" Inherits="CRWebPortal.ApproveChangeRequest" %>


<!DOCTYPE html>
<html>
<head>

    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>CR Login</title>
    <!-- Tell the browser to be responsive to screen width -->
    <script type="text/jscript" src="Scripts/pace.min.js"></script>
    <link href="Styles/pace-theme-bounce.css" rel="stylesheet" />
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
                                <div class="col-xs-12">
                                    <blockquote>
                                        <small>
                                            <asp:Label ID="lblMsg" runat="server">Use your pegasus Email e.g kasozi.nsubuga@pegasus.co.ug</asp:Label>
                                        </small>
                                    </blockquote>
                                </div>
                            </div>
                            <asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">
                                <asp:View ID="EmptyView" runat="server">
                                </asp:View>
                                <asp:View ID="ReasonView" runat="server">
                                    <div class="form-group text-center m-t-20">
                                        <asp:TextBox ID="txtReason" Height="100px" TextMode="MultiLine" placeholder="Enter The Reason For Rejection" Style="border-color: #428BCA" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </asp:View>
                            </asp:MultiView>
                            <div class="form-group text-center m-t-20">
                                <div class="col-xs-12">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-success btn-lg btn-block text-uppercase waves-effect waves-light" OnClick="btnSave_Click"></asp:Button>
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
