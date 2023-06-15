<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_rptstockForm.aspx.cs" Inherits="fm_rptstockForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script src="js/jquery-1.3.1.min.js"></script>
    <link href="css/sweetalert.css" rel="stylesheet" />
    <script src="js/sweetalert-dev.js"></script>
    <script src="js/sweetalert.min.js"></script>
</head>
<body style="font-size:small;font-family:Verdana">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
        <div class="divheader">STOCK REPORT FORM </div>
    <img src="div2.png" class="divid" />
        <table>
            <tr style="background-color:silver">
                <td>
                    Warehouse</td>
                <td>:</td>
                <td>
                             <asp:Label ID="lbwhs_nm" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                
            </tr>
            <tr style="background-color:silver">
                <td>
                    Bin</td>
                <td>:</td>
                <td>
                             <asp:Label ID="lbbin_nm" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                
            </tr> 
            <tr style="background-color:silver">
                <td>
                    Date</td>
                <td>:</td>
                <td>
                             <asp:Label ID="lbdatefr" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                <td>:</td>
                <td>
                    <asp:Label ID="lbdateto" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
                
            </tr>
            
            </table>
        <div class="divgrid">
                <asp:GridView ID="grd" runat="server" Width="100%" ForeColor="#333333" GridLines="None" CellPadding="0"  ShowFooter="True" >
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                     
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
        </div>
         <div class="navi">
             <asp:Button ID="btclose" runat="server" CssClass="button2" OnClick="btclose_Click" Text="Close" />
             <asp:Button ID="bttoExcel" runat="server" CssClass="button2" Text="Grid to Excel" onclick="bttoExcel_Click" />
        </div>
    </form>
</body>
</html>
