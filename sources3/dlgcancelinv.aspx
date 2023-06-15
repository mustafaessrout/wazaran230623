<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dlgcancelinv.aspx.cs" Inherits="dlgcancelinv" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Confirm dialog</title>
    <link href="admin/css/bootstrap.min.css" rel="stylesheet" />
    <script src="admin/js/bootstrap.min.js"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
   
    <link href="css/sweetalert.css" rel="stylesheet" />
    <script src="js/sweetalert.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
    <div class="form-horizontal">
        <h3>Confirmation Invoice Cancellation</h3>
        <div class="form-group">
        <label class="control-label col-md-1">Sys No:</label>
        <div class="col-md-3">
            <asp:Label ID="lbsysno" runat="server" CssClass="form-control"></asp:Label>
        </div>
        </div>
        <div class="form-group">
        <label class="control-label col-md-1">Customer:</label>
        <div class="col-md-3">
            <asp:Label ID="lbcustcode" runat="server" CssClass="form-control"></asp:Label>
        </div>
        </div>
        <div class="form-group">
        <label class="control-label col-md-1">Name:</label>
        <div class="col-md-3">
            <asp:Label ID="lbcustname" runat="server" CssClass="form-control"></asp:Label>
        </div>
        </div>
        <div class="form-group">
        <label class="control-label col-md-1">Salesman:</label>
        <div class="col-md-3">
            <asp:Label ID="lbsalesman" runat="server"></asp:Label>
        </div>
        </div>
         <div class="form-group">
        <label class="control-label col-md-1">Total Amt:</label>
        <div class="col-md-3">
            <asp:Label ID="lbtotamt" runat="server"></asp:Label>
        </div>
        </div>

      <div class="form-group">
        <label class="control-label col-md-1">Reason:</label>
        <div class="col-md-3">
            <asp:DropDownList ID="cbreason" runat="server" CssClass="form-control-static" Width="100%"></asp:DropDownList>
        </div>
        </div>
    </div>
    </div>
         <div class="navi">
                <asp:Button ID="btcancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="btcancel_Click" />
                <asp:Button ID="btexecute" runat="server" Text="Execute Now" CssClass="btn btn-default" OnClick="btexecute_Click" />
        </div>
    </form>
</body>
</html>
