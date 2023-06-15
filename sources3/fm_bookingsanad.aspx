<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_bookingsanad.aspx.cs" Inherits="fm_bookingsanad" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function RefreshData(val)
        {
            $get('<%=hdbookno.ClientID%>').value = val;
            $get('<%=btrefresh.ClientID%>').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        Booking&nbsp; Document Period : <asp:Label ID="lbperiod" runat="server" ForeColor="Red"></asp:Label>
    </div>
    <img src="div2.png" class="divid" />
    <div>
        <table>
            <tr>
                <td>
                    Booking Code
                </td>
                <td>
                    :
                </td>
                <td style="margin-left: 120px">

                    <asp:TextBox ID="txbookingno" runat="server" Width="20em"></asp:TextBox>
                    <asp:Button ID="btsearch" runat="server" CssClass="button2 search" OnClick="btsearch_Click" />
                    <asp:HiddenField ID="hdbookno" runat="server" />

                </td>
                <td>

                    Receiving Date</td>
                <td>

                    :</td>
                <td>

                    <asp:TextBox ID="dtbooking" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="dtbooking_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtbooking" CssClass="teamCalendar">
                    </asp:CalendarExtender>

                </td>
            </tr>
            <tr>
                <td>
                    Book No.</td>
                <td>
                    :</td>
                <td style="margin-left: 120px">

                    <asp:TextBox ID="txbookno" runat="server" Width="20em"></asp:TextBox>

                </td>
                <td>

                    &nbsp;</td>
                <td>

                    &nbsp;</td>
                <td>

                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Emp Received</td>
                <td>
                    :</td>
                <td style="margin-left: 120px">

                    <asp:DropDownList ID="cbsalesman" runat="server" Width="20em">
                    </asp:DropDownList>

                </td>
                <td>

                    Type Of

                    Document</td>
                <td>

                    :</td>
                <td>

                    <asp:DropDownList ID="cbdoctype" runat="server" Width="20em">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Start Manual No.</td>
                <td>
                    :</td>
                <td style="margin-left: 120px">

                    <asp:TextBox ID="txstartno" runat="server"></asp:TextBox>

                </td>
                <td>

                    End Manual No</td>
                <td>

                    :</td>
                <td>

                    <asp:TextBox ID="txendno" runat="server"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td>
                    Received Doc</td>
                <td>
                    :</td>
                <td style="margin-left: 120px">

                    <asp:FileUpload ID="upl" runat="server" Width="20em" />
                </td>
                <td>

                    &nbsp;</td>
                <td>

                    &nbsp;</td>
                <td>

                    &nbsp;</td>
            </tr>
        </table>
    </div>
    <img src="div2.png" class="divid" />
    <div class="navi">
        <asp:Button ID="btrefresh" runat="server" Text="Button" OnClick="btrefresh_Click" CssClass="divhid" />
        <asp:button runat="server" text="New" CssClass="button2 add" ID="btnew" OnClick="btnew_Click" />
        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="button2 save" OnClick="btsave_Click" />
        <asp:Button ID="btprint" runat="server" CssClass="button2 print" OnClick="btprint_Click" Text="Print" />
    </div>
</asp:Content>

