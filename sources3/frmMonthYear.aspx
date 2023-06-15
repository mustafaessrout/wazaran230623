<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="frmMonthYear.aspx.cs" Inherits="frmMonthYear" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <link href="css/jquery-ui.min.css" rel="stylesheet" />
    <script src="css/jquery-1.9.1.js"></script>
    <script src="css/jquery-ui.js"></script>
    <script>
        function openwindow() {
            var oNewWindow = window.open("fm_lookup_Year.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }

        function updpnl() {
            document.getElementById('<%=bttmp.ClientID%>').click();
            return (false);
        }
    </script>
    <script type="text/javascript">
         $(function () {
             $("#<%=txyeaStart.ClientID%>").datepicker({ dateFormat: "mm/dd/yy" }).val();
         });
      </script>
    <script type="text/javascript">
        $(function () {
            $("#<%=txyeaEnd.ClientID%>").datepicker({ dateFormat: "mm/dd/yy" }).val();
         });
      </script>
    <script type="text/javascript">
        $(function () {
            $("#<%=txymtStart.ClientID%>").datepicker({ dateFormat: "mm/dd/yy" }).val();
         });
      </script>
    <script type="text/javascript">
        $(function () {
            $("#<%=txymtEnd.ClientID%>").datepicker({ dateFormat: "mm/dd/yy" }).val();
        });
      </script>
    <style type="text/css">

        .auto-style17 {
            height: 31px;
        }
            
.button2.search {
    background:  url('css/search.png') no-repeat;
    background-size:33px 30px;
    padding-left: 30px;
    border-radius:8px;
}

.button2 {
    color: #6e6e6e;
    font: bold 12px Helvetica, Arial, sans-serif;
    text-decoration: none;
    padding: 7px 12px;
    position: relative;
    display: inline-block;
    text-shadow: 0 1px 0 #fff;
    -webkit-transition: border-color .218s;
    -moz-transition: border .218s;
    -o-transition: border-color .218s;
    transition: border-color .218s;
    background: #f3f3f3;
    background: -webkit-gradient(linear,0% 40%,0% 70%,from(#F5F5F5),to(#F1F1F1));
    background: -moz-linear-gradient(linear,0% 40%,0% 70%,from(#F5F5F5),to(#F1F1F1));
    border: solid 1px #dcdcdc;
    border-radius: 2px;
    -webkit-border-radius: 2px;
    -moz-border-radius: 2px;
    margin-right: 10px;
}
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3> FORM MONTH YEAR</h3>
    <p> 
        <table style="width:100%;">
            <tr>
                <td>Year CD</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txYearCD" runat="server" CssClass="auto-style3" Width="176px" OnTextChanged="txYearCD_TextChanged"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers><asp:AsyncPostBackTrigger ControlID="bttmp" EventName="Click" /></Triggers>
                    </asp:UpdatePanel>
                </td>
                <td>
                                    <strong>
                                    <asp:Button ID="btsearch" runat="server" CssClass="button2 search" OnClientClick="openwindow();return(false);" style="left: 0px; top: 7px" Text="Search" OnClick="btsearch_Click" />
                                    </strong>               
                </td>
                <asp:Button ID="bttmp" runat="server" Text="Button" OnClick="bttmp_Click" style="display:none" />
            </tr>
            <tr>
                <td>Year Start</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txyeaStart" runat="server" CssClass="auto-style3" Width="176px"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Year End</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txyeaEnd" runat="server" CssClass="auto-style3" Width="176px"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>&nbsp;</td>
            </tr>
            <td colspan="6">
                <tr>
                    <td>NO</td>
                    <td>MontCD</td>
                    <td>Period Name</td>
                    <td>Start Date</td>
                    <td>End Date</td>
                    <td>

                <asp:Button ID="btWiz" runat="server" Height="24px" Text="..." OnClick="btWiz_Click" CssClass="auto-style17" Width="49px" />

                       </td>
                </tr>
            </td>
                <tr>
                    <td>
                            <asp:TextBox ID="txseqID" runat="server" CssClass="auto-style3" Width="176px"></asp:TextBox>
                        </td>
                    <td>
                            <asp:TextBox ID="txMonthCD" runat="server" CssClass="auto-style3" Width="176px"></asp:TextBox>
                        </td>
                    <td>
                            <asp:TextBox ID="txymtName" runat="server" CssClass="auto-style3" Width="176px"></asp:TextBox>
                        </td>
                    <td>
                            <asp:TextBox ID="txymtStart" runat="server" CssClass="auto-style3" Width="176px"></asp:TextBox>
                        </td>
                    <td>
                            <asp:TextBox ID="txymtEnd" runat="server" CssClass="auto-style3" Width="176px"></asp:TextBox>
                        </td>
                    <td>

                <asp:Button ID="btAdd" runat="server" Height="24px" Text="&gt;&gt;&gt;" OnClick="btAdd_Click" CssClass="auto-style17" Width="49px" />

                    </td>
                   
                    
                </tr>
            
                <tr>
                    <td colspan="6">

                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="grdMonth" runat="server" AutoGenerateColumns="False" OnRowDeleted="grdMonth_RowDeleted" OnSelectedIndexChanged="grdMonth_SelectedIndexChanged" Width="1031px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbseqID" runat="server" Text='<%# Eval("seqID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Month CD">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMonthCD" runat="server" Text='<%# Eval("MonthCD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Period Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblymtName" runat="server" Text='<%# Eval("ymtName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Start Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblymtStart" runat="server" Text='<%# Eval("ymtStart","{0:dd-MM-yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="End Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblymtEnd" runat="server" Text='<%# Eval("ymtEnd","{0:dd-MM-yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField HeaderText="Action" ShowDeleteButton="True" ShowSelectButton="True" />
                                                </Columns>
                                                <SelectedRowStyle BackColor="#99CCFF" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
       
                    </td>
                </tr>
            <tr>
        <td colspan="4" style="text-align:center">
                <asp:Button ID="btsave" runat="server" Text="SAVE" Width="80px" OnClick="btsave_Click" />
            </td>
        </tr>
            </table>
    </p>
    
    
</asp:Content>


