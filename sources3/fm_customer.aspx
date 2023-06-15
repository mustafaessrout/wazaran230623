<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_customer.aspx.cs" Inherits="fm_customer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="css/jquery-1.9.1.js"></script>
    <script src="css/jquery-ui.js"></script>
    <script>
        function openwindow() {
            var oNewWindow = window.open("fm_lookup_customer.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }

        function updpnl() {
            document.getElementById('<%=bttmp.ClientID%>').click();
            return (false);
        }
    </script>
     <script type="text/javascript">
         $(function () {
             $("#<%=txtBirthday.ClientID%>").datepicker({ dateFormat: "mm/dd/yy" }).val();
         });
      </script>
    <script type="text/javascript">
        $(function () {
            $("#<%=txtidentityExpireDate.ClientID%>").datepicker({ dateFormat: "mm/dd/yy" }).val();
         });
      </script>
     <script type="text/javascript">
         $(function () {
             $("#<%=txtCreateDT.ClientID%>").datepicker({ dateFormat: "mm/dd/yy" }).val();
        });
      </script>
       
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <strong>CUSTOMER</strong>
    <img src="div2.png" class="divid" />
    <table style="width: 118%; margin-right: 0px;">
        <tr>
            <td>Customer CD</td>
            <td>
                
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <strong>
                                    <asp:TextBox ID="txtCustCD" runat="server"></asp:TextBox>
                                    <asp:Button ID="btsearch" runat="server" CssClass="button2 search" OnClientClick="openwindow();return(false);" style="left: 0px; top: 7px" Text="Search" />
                                    </strong>
                                </ContentTemplate>
                                <Triggers><asp:AsyncPostBackTrigger ControlID="bttmp" EventName="Click" /></Triggers>
                            </asp:UpdatePanel>
            </td>
            <asp:Button ID="bttmp" runat="server" Text="Button" OnClick="bttmp_Click" style="display:none" />
            <td>Sales Point</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtSalesPointCD" runat="server"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>CustomerNm</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtCustNM" runat="server" Width="248px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>Group CD</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbCusGrCD" runat="server" Height="25px" Width="130px">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>Short Name</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtCustSN" runat="server" Width="246px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>Customer Class</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbCusClCD" runat="server" Height="25px" Width="130px">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>Bill to Name</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtBillNM" runat="server" Width="247px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>Customer Map CD</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtCustMapCd" runat="server"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>Countact Person</td>
            <td class="auto-style13">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtCperson" runat="server" Width="247px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>Bill To</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtBillCD" runat="server"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>Birt Date</td>
            <td class="auto-style13">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtBirthday" runat="server" Width="247px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>Ship To</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtShipCD" runat="server"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
            </td>
            <td>Price Level</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbPrLevCD" runat="server" Height="25px" Width="130px">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>Adress</td>
            <td class="auto-style13">
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtAddr1" runat="server" Width="247px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>Outlet Type</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbOtlCD" runat="server" Height="16px" Width="130px">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td class="auto-style13">
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtAddr2" runat="server" Width="247px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>Payment Term</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbTermCD" runat="server" Height="16px" Width="130px">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td class="auto-style13">
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtAddr3" runat="server" Width="247px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>Tax Code</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtTaxCD" runat="server"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>Phone</td>
            <td class="auto-style13">
                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtPhone" runat="server" Width="82px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
                Fax
                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtFax" runat="server" Width="84px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>GL Account</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel34" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtActCD" runat="server"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>Region</td>
            <td class="auto-style13">
                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtRegion" runat="server" Width="247px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>Credit Limit</td>
            <td>
                        <asp:TextBox ID="txtCrLimit" runat="server"></asp:TextBox>
                    </td>
        </tr>
        <tr>
            <td>Area</td>
            <td class="auto-style13">
                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtArea" runat="server" Width="247px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>ID NO</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel35" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtidentityID" runat="server"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>City</td>
            <td class="auto-style13">
                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtCity" runat="server" Width="247px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>Expire Date ID</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel36" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtidentityExpireDate" runat="server"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>Distric</td>
            <td class="auto-style13">
                <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtdistCD" runat="server" Width="247px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>Signature</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel37" runat="server">
                    <ContentTemplate>
                        <asp:FileUpload ID="uplsignature" runat="server" Height="23px" Width="305px" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>Country</td>
            <td class="auto-style13">
                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtCountry" runat="server" Width="247px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>custRegister</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel38" runat="server">
                    <ContentTemplate>
                        <asp:FileUpload ID="uplcustRegPath" runat="server" Height="23px" Width="305px" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>Block</td>
            <td class="auto-style13">
                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtBlokCD" runat="server" Width="247px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>custMap</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel39" runat="server">
                    <ContentTemplate>
                        <asp:FileUpload ID="uplcustMapPath" runat="server" Height="23px" Width="305px" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>Route Delivery</td>
            <td class="auto-style13">
                <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtDlvrCD" runat="server" Width="247px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>custID</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel40" runat="server">
                    <ContentTemplate>
                        <asp:FileUpload ID="uplcustIDPath" runat="server" Height="23px" Width="305px" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>Zip Code</td>
            <td class="auto-style13">
                <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtPostCD" runat="server" Width="247px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>custShop</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel41" runat="server">
                    <ContentTemplate>
                        <asp:FileUpload ID="uplcustShopPath" runat="server" Height="23px" Width="305px" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>Market</td>
            <td class="auto-style13">
                <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtMarketCD" runat="server" Width="247px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>Municipalitypermit</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel42" runat="server">
                    <ContentTemplate>
                        <asp:FileUpload ID="uplcustMunicipalitypermit" runat="server" Height="23px" Width="305px" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td class="auto-style13">
                &nbsp;</td>
            <td>Register Date</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel43" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtCreateDT" runat="server"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>Coordinat Lat</td>
            <td>
            &nbsp;&nbsp;<asp:UpdatePanel ID="UpdatePanel22" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtLatitude" runat="server" Width="70px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
                &nbsp;&nbsp; long
                <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtLongitude" runat="server" Width="77px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>Distance</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel44" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtdistance" runat="server"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnew" runat="server" CssClass="button2 add" OnClick="btnew_Click" Text="New" />
            </td>
            <td>
                <asp:Button ID="btsave" runat="server" Text="Save" Width="107px" OnClick="btsave_Click" />
            </td>
            <td>&nbsp;</td>
            <td>
                <asp:Image ID="img" runat="server" Height="167px" style="margin-left: 0px" />
            </td>
        </tr>
    </table>
</asp:Content>

