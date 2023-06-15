<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bankcleareanceho.aspx.cs" Inherits="bankcleareanceho" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style>
    #mydiv {
    position:fixed;
    top: 30%;
    left: 50%;
    width:40em;
    height:25em;
    margin-top: -9em; /*set to a negative number 1/2 of your height*/
    margin-left: -20em; /*set to a negative number 1/2 of your width*/
    border: 1px solid #ccc;
    background-color: #f3f3f3;
    padding:20px 20px 20px 20px;
}
     
     
     
        .auto-style1 {
            color: #FF0000;
        }
     
     
     
        .auto-style2 {
            height: 18px;
        }
     
     
     
        .auto-style3 {
            color: #FFFFFF;
        }
        .auto-style4 {
            background-color: #0033CC;
        }
        </style>
    <link href="css/sweetalert.css" rel="stylesheet" />
    <script src="js/sweetalert-dev.js"></script>
    <script src="js/sweetalert.min.js"></script>
</head>
<body style="font-size:small;font-family:Verdana">
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div>
        <div id="mydiv">
            Cheque Clearance
        
        <img src="div2.png" style="width:100%"/>
            <table>
             <tr>
                 <td style="background-color:darkblue;color:white" colspan="3"><strong><span class="auto-style4">CHEQUE DETAIL</span></strong></td>
             </tr>
             <tr>
                 <td class="auto-style1">&nbsp;Transfer/Cheque No.</td>
                 <td class="auto-style1">:</td>
                 <td class="auto-style1">
                     <asp:Label ID="lbdepositno" runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label>
                 </td>
             </tr>
                <tr>
                    <td>Account No.</td>
                    <td>:</td>
                    <td>
                        <asp:Label ID="lbbankaccountno" runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label></td>
                </tr>
                <tr>
                    <td>Bank Name</td>
                    <td>:</td>
                    <td>
                        <asp:Label ID="lbbankname" runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label></td>
                </tr>
                <tr>
                    <td>Amount</td>
                    <td>:</td>
                    <td>
                        <asp:Label ID="lbamount" runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="background-color: #0033CC;color:white"><strong>DEPOSIT IN</strong></td>
                </tr>
                 <tr>
                    <td class="auto-style2">Bank Account</td>
                    <td class="auto-style2">:</td>
                    <td class="auto-style2">
                        <asp:DropDownList ID="cbbankto" runat="server" OnSelectedIndexChanged="cbbankto_SelectedIndexChanged" Width="20em">
                        </asp:DropDownList>
                     </td>
                </tr>
                 <tr>
                    <td colspan="3" style="background-color: #0033CC" class="auto-style2"></td>
                </tr>
                 <tr>
                    <td class="auto-style2">Cleareance Date</td>
                    <td class="auto-style2">:</td>
                    <td class="auto-style2">
                        <asp:TextBox ID="dtclear" runat="server" CssClass="makeitreadonly"></asp:TextBox>
                        <asp:CalendarExtender ID="dtclear_CalendarExtender" runat="server" TargetControlID="dtclear">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>Clearance By</td>
                    <td>:</td>
                    <td>
                        <asp:Label ID="lbcreated" runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">Remark</td>
                    <td class="auto-style1">:</td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txremark" runat="server" CssClass="makeitreadwrite"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div>Do you want to <span class="auto-style1"><strong>Reject</strong></span> Or <span class="auto-style1"><strong>Clearance</strong></span> this Deposit Bank ?</div>
            <div class="navi">
                <asp:Button ID="btcancel" runat="server" Text="Reject" CssClass="button2 delete" OnClick="btcancel_Click" />
                <asp:Button ID="btyes" runat="server" Text="Yes" CssClass="button2 add" OnClick="btyes_Click" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
