<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_SalesmanTargetAchievement.aspx.cs" Inherits="fm_SalesmanTargetAchievement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="css/animate.css" />
    <link rel="stylesheet" href="css/font-awesome.min.css" />
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form-horizontal">
        <h4 class="jajarangenjang">Salesman Target & Achievement</h4>
        <div class="h-divider"></div>
        <div class="container">
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-md-2">User Name</label>
                    <div class="col-md-4">
                        <asp:Label runat="server" ID="lblUserName"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <label class="control-label col-md-2">Email ID</label>
                    <div class="col-md-4">
                        <asp:Label runat="server" ID="lblUserEmail"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <label class="control-label col-md-2">Employee Number</label>
                    <div class="col-md-4">
                        <asp:Label runat="server" ID="lblEmpNo"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <label class="control-label col-md-2">Wazaran Period</label>
                    <div class="col-md-4">
                        <asp:Label runat="server" ID="lblPeriod"></asp:Label>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <div class="container">
        <div class="form-group">
            <div class="row">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="1" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="prod_cds" HeaderText="Product Code" />
                                <asp:BoundField DataField="prod_nm" HeaderText="Product Name" />
                                <asp:BoundField DataField="HOTarget" HeaderText="HO Target" />
                            </Columns>

                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


    
    <div class="navi">
        <asp:Button ID="btnGenerate" runat="server" Text="Show Update Data" CssClass="button2 save" OnClick="btnGenerate_Click" />

    </div>
    <div class="container">
        <div class="form-group">
            <div class="row">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="1" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="PDFFile" Target="_blank" HeaderText="Download PDF"
                                    DataNavigateUrlFormatString="/images/PriorityTarget/{0}" Text="View PDF" />
                                <asp:HyperLinkField DataNavigateUrlFields="ExcelFile" Target="_blank" HeaderText="Download Excel"
                                    DataNavigateUrlFormatString="/images/PriorityTarget/{0}" Text="View Excel" />
                                <asp:BoundField DataField="emailSendDateTime" HeaderText="Report Date" />
                                <asp:BoundField DataField="ProductName" HeaderText="Product Code" />
                                <asp:BoundField DataField="SubjectMail" HeaderText="Document Subject" />
                            </Columns>

                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>

</asp:Content>
