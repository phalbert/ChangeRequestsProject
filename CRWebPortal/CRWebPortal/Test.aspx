<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="CRWebPortal.Test" %>

<!DOCTYPE html>
<html>
<head>

    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>CR Login</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" rel="stylesheet">
    <link href="Styles/style.css" rel="stylesheet">
</head>
<body cz-shortcut-listen="true" class="content-wrapper">
    <form runat="server">

        <div style="width: 30%; margin: 0 auto;">
            <div id="result" style="font-family: Tahoma; font-size: 0.9em; color: darkgray; margin-top: 230px; padding-bottom: 5px">
                Initializing and Preparing...
            </div>

            <div id="progressbar" style="width: 300px; height: 15px"></div>
            <br />
        </div>
        <!-- jQuery -->
        <script src="https://code.jquery.com/jquery-3.3.1.min.js"></script>
        <script src="Scripts/jquery.signalR-2.2.2.min.js"></script>
        <script src="signalr/hubs"></script>

        <!-- Bootstrap Core JavaScript -->
        <script type="text/jscript" src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.bundle.min.js"></script>
        <!-- plugin -->


        <script type="text/javascript">

            $(document).ready(function () {

                // initialize progress bar
                //$("#progressbar").progressbar({ value: 0 });

                // initialize the connection to the server
                var progressNotifier = $.connection.DDProcess;

                // client-side sendMessage function that will be called from the server-side
                progressNotifier.client.sendMessage = function (message) {
                    // update progress
                    UpdateProgress(message);
                };

                $.connection.hub.qs = { "TbarId": "TBPA-636660567289641609" };
                // establish the connection to the server and start server-side operation
                $.connection.hub.start().done(function () {
                    // call the method CallLongOperation defined in the Hub
                    progressNotifier.server.callLongOperation();
                });
            });

            function UpdateProgress(message) {
                // get result div
                var result = $("#result");
                // set message
                result.html(message);

                if (message.toLowerCase().indexOf("done") >= 0) {
                    var arr = message.split(':');
                    var imageURL = arr[1];
                    result.html(imageURL);

                    var img = document.createElement("img");
                    img.src = imageURL;
                    document.body.appendChild(img);
                }

            }

        </script>
    </form>
</body>
</html>
