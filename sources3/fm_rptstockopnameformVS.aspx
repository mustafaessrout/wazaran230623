<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_rptstockopnameformVS.aspx.cs" Inherits="fm_rptstockopnameformVS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/metro.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet"/>


    <script src="js/jquery-1.3.1.min.js"></script>
    <link href="css/sweetalert.css" rel="stylesheet" />
    <script src="js/sweetalert-dev.js"></script>
    <script src="js/sweetalert.min.js"></script>
    <script src="js/jquery.floatThead.js"></script>
    <script src="js/index.js"></script>

</head>
<body >
    <form id="form1" runat="server" class="no-margin-bottom">
        <div class="containers bg-white">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
            <div class="divheader">STOCK OPNAME VAN </div>
            <div class="h-divider"></div>
            <table class="margin-bottom">
                <tr >
                    <td class="block padding-top-4 titik-dua text-bold" style="padding-right:20px;">Stock Opname No</td>
                    <td style="padding-left:20px;">
                        <asp:Label ID="lbstockno" runat="server" CssClass="text-bold text-red padding-top-4 "></asp:Label>
                    </td>
                </tr>
                <tr >
                    <td class="block padding-top-4 titik-dua text-bold" style="padding-right:20px;">Date</td>
                    <td style="padding-left:20px;">
                        <asp:Label ID="lbdate" runat="server" CssClass="text-bold text-red padding-top-4 "></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="block padding-top-4 titik-dua text-bold" style="padding-right:20px;">Van Code</td>
                    <td style="padding-left:20px;">
                         <asp:Label ID="lbwhs_cd" runat="server" CssClass="text-bold text-red padding-top-4 "></asp:Label>
                    </td>        
                </tr> 
                </table>
            <div class="divgrid margin-bottom">
                <div class="overflow-y overflow-x" style="max-height:410px;">
                    <asp:GridView ID="grd" runat="server" CssClass="table table-bordered table-striped table-fix mygrid" GridLines="None" CellPadding="0"  ShowFooter="True" >
                        <AlternatingRowStyle  />
                        <Columns>
                     
                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page" />
                        <RowStyle  />
                        <SelectedRowStyle CssClass="table-edit" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </div>
            </div>
            <div class="navi margin-bottom">
                 <asp:Button ID="btclose" runat="server" CssClass="btn-danger btn bnt-close" OnClick="btclose_Click" Text="Close" />
                 <asp:Button ID="bttoExcel" runat="server" CssClass="btn-primary btn btn-download" Text="Grid to Excel" onclick="bttoExcel_Click" />
            </div>
        </div>
    </form>
</body>
</html>
