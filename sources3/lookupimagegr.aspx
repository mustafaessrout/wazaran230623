<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupimagegr.aspx.cs" Inherits="lookupimagegr" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lookup Image</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/jquery.floatThead.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="containter">
            <div class="form-group">
                <div class="col-sm-12" style="width:100%;height=100%">
                    <asp:Image ID="img" Width="100%" Height="100%" runat="server" AlternateText="No Image Found for this good received" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
