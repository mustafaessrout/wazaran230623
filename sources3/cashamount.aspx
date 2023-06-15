<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cashamount.aspx.cs" Inherits="cashamount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script src="js/jquery-1.3.1.min.js"></script>
    <link href="css/sweetalert.css" rel="stylesheet" />
    <script src="js/sweetalert-dev.js"></script>
    <script src="js/sweetalert.min.js"></script>
     <style>
       #mydiv {
    position:fixed;
    top: 30%;
    left: 50%;
    width:40em;
    height:30em;
    margin-top: -9em; /*set to a negative number 1/2 of your height*/
    margin-left: -20em; /*set to a negative number 1/2 of your width*/
    border: 1px solid #ccc;
    background-color: #f3f3f3;
    padding:20px 20px 20px 20px;
}
     
     
     
        </style>
</head>
<body style="font-size:small;font-family:Verdana">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
    <div id="mydiv">
        <div class="divheader">Entry Nominal Amount</div>
    <img src="div2.png" class="divid" />
        <table>
            <tr style="background-color:silver">
                <td>
                    Salesman</td>
                <td>:</td>
                <td>
                             <asp:Label ID="lbsalesman" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                <td class="auto-style2">
                    &nbsp;</td>
            </tr>
            <tr style="background-color:silver">
                <td>
                    Total Paid</td>
                <td>:</td>
                <td>
                             <asp:Label ID="lbtotalpaid" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                <td class="auto-style2">
                    &nbsp;</td>
            </tr>
            <tr style="background-color:silver">
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    Amount</td>
                <td class="auto-style2">
                    Total</td>
            </tr>
            <tr>
                <td>
                    Amount of 500
                </td>
                <td>:</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                             <asp:TextBox ID="txamt500" runat="server" AutoPostBack="True" OnTextChanged="txamt500_TextChanged"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                   
                </td>
                <td class="auto-style2">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                             <asp:Label ID="lbtot500" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txamt500" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                   
                </td>
            </tr>
            <tr>
                <td>
                    Amount Of 100</td>
                <td>:</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <ContentTemplate>
                             <asp:TextBox ID="txamt100" runat="server" OnTextChanged="txamt100_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                   
                </td>
                <td class="auto-style2">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbtot100" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txamt100" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    
                </td>
            </tr>
            <tr>
                <td>
                    Amount Of
                    50</td>
                <td>:</td>
                <td>
                     <asp:TextBox ID="txamt50" runat="server" AutoPostBack="True" OnTextChanged="txamt50_TextChanged"></asp:TextBox>
                </td>
                <td class="auto-style2">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbtot50" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txamt50" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    Amount Of 20</td>
                <td>:</td>
                <td>
                    
                             <asp:TextBox ID="txamt20" runat="server" AutoPostBack="True" OnTextChanged="txamt20_TextChanged"></asp:TextBox>
                       
                   
                </td>
                <td class="auto-style2">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                              <asp:Label ID="lbtot20" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txamt20" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                  
                </td>
            </tr>
            <tr>
                <td>
                    Amount Of 10</td>
                <td>:</td>
                <td>
                   <asp:TextBox ID="txamt10" runat="server" AutoPostBack="True" OnTextChanged="txamt10_TextChanged"></asp:TextBox>
                 </td>
                <td class="auto-style2">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                             <asp:Label ID="lbtot10" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txamt10" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                   
                </td>
            </tr>
            <tr>
                <td>
                    Amount Of 5</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txamt5" runat="server" AutoPostBack="True" OnTextChanged="txamt5_TextChanged"></asp:TextBox>
                </td>
                <td class="auto-style2">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbtot5" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txamt5" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    
                </td>
            </tr>
            <tr>
                <td>
                    Amount Of 1</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txamt1" runat="server" AutoPostBack="True" OnTextChanged="txamt1_TextChanged"></asp:TextBox>
                </td>
                <td class="auto-style2">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbtot1" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txamt1" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    
                </td>
            </tr>
            <tr>
                <td>
                    Amount Of 0.50</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txamthalf" runat="server" AutoPostBack="True" OnTextChanged="txamthalf_TextChanged"></asp:TextBox>
                </td>
                <td class="auto-style2">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbtot05" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txamthalf" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    
                </td>
            </tr>
            <tr>
                <td>Amount Of 0.25</td>
                 <td>:</td>
                 <td>
                     <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                         <ContentTemplate>
                             <asp:TextBox ID="txamt025" runat="server" AutoPostBack="True" OnTextChanged="txamt025_TextChanged"></asp:TextBox>
                         </ContentTemplate>
                     </asp:UpdatePanel>
                </td>
                 <td>
                            <asp:Label ID="lbtot025" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
            </tr>
            <tr>
                <td>Amount Of 0.10</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txamt01" runat="server" AutoPostBack="True" OnTextChanged="txamt01_TextChanged"></asp:TextBox>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbtot01" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    </td>
                <td></td>
                <td>
                    </td>
                <td>
                    
                    -----------</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td class="auto-style1" style="text-align: right">
                    <strong>TOTAL :</strong></td>
                <td class="auto-style2">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbtotal" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                            
                    </td>
            </tr>
            </table>
         <div class="navi">
            <asp:Button ID="btsave" runat="server" Text="Save" CssClass="button2 save" OnClick="btsave_Click" />
                     <asp:Button ID="btclose" runat="server" CssClass="button2" OnClick="btclose_Click" Text="Close" />
        </div>
     </div>
        
    </form>
</body>
</html>
