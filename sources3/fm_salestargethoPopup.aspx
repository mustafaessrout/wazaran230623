<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_salestargethoPopup.aspx.cs" Inherits="fm_salestargethoPopup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<body style="font-family: Tahoma,Verdana; font-size: small" onbeforeunload="ConfirmClose()" onunload="HandleOnClose()">
     <script>
         function closewin() {
             window.opener.updpnl();
             window.close();
         }
        
         function ConfirmClose() {
             alert("sd3");
             if (event.clientY < 0) {
                 alert("sd1");
             }
         }

         function HandleOnClose() {
             alert("sd4");
             if (myclose == true) {
                 alert("sd2");
             }
         }
         function OpenAddControl() { }
            </script>
    <form id="form1" runat="server" onclose="quit();">
        <asp:ToolkitScriptManager ID="tsmanager" runat="server">
        </asp:ToolkitScriptManager>
        <div class="div-table">
            <link href="css/lightbox.css" rel="stylesheet" />
            <link href="css/sweetalert.css" rel="stylesheet" />
            <link rel="stylesheet" type="text/css" href="css/component.css" />
            <script src="js/modernizr.custom.js"></script>

            <script src="js/sweetalert.min.js"></script>
            <script src="js/sweetalert-dev.js"></script>
            <script src="js/modernizr.js"></script>
            <link href="css/anekabutton.css" rel="stylesheet" />

            <strong>Sales group Mapping</strong>
            <link href="css/anekabutton.css" rel="stylesheet" />
            <script>
                function closewin() {
                    window.opener.updpnl();
                    window.close();
                }
                function quit() {
                    //alert("sd");
                }
                function ConfirmClose() {
                    alert("sd3");
                    if (event.clientY < 0) {
                        alert("sd1");
                    }
                }

                function HandleOnClose() {
                    alert("sd4");
                    if (myclose == true) {
                        alert("sd2");
                    }
                }
                function OpenAddControl() { }
            </script>
            <style>
            </style>
            <div class="div-table">
                <div class="div-table-row">
                    <div class="div-table-col1" align="center"></div>
                    <div class="div-table-col2">
                    </div>
                    <div class="div-table-col3"></div>
                    <div class="div-table-col4"></div>
                </div>
                <div class="div-table-row">
                    <div class="div-table-col1" align="center">Branch Name</div>
                    <div class="div-table-col2">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblBranchName" Text="" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="div-table-col3"></div>
                    <div class="div-table-col4"></div>
                </div>
                <div class="div-table-row">
                    <div class="div-table-col1" align="center">Period</div>
                    <div class="div-table-col2">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblPeriod" Text="" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="div-table-col3"></div>
                    <div class="div-table-col4"></div>
                </div>
                <div class="div-table-row">
                    <div class="div-table-col1" align="center">Product</div>
                    <div class="div-table-col2">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblProduct" Text="" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="div-table-col3"></div>
                    <div class="div-table-col4"></div>
                </div>
                <div class="div-table-row">
                    <div class="div-table-col1" align="center">Sales Group</div>
                    <div class="div-table-col2">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlSalesGroup" Width="110%" runat="server">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="div-table-col3"><asp:Button ID="btsave" runat="server" Text="Save" CssClass="button2 save" OnClick="btsave_Click" style="margin-left: 67px;" /></div>
                    <div class="div-table-col4"></div>
                </div>
                <div class="div-table-row">
                    <div class="div-table-col1" align="center">Mapping Details</div>
                    <div class="div-table-colFull">
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grd" DataKeyNames="prod_cd" OnRowDeleting="grd_RowDeleting" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="1" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Product Name ">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdprodcd" Value='<%# Eval("prod_cd") %>' runat="server"  />
                                                <asp:Label ID="lbprodname" runat="server" Text='<%# Eval("prodName") %>' ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Period ">
                                            <ItemTemplate>
                                                <asp:Label ID="lbPeriod" runat="server" Text='<%# Eval("period") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Group Salesman ">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdGroupSalesman" Value='<%# Eval("GroupSalesman") %>' runat="server" />
                                                <asp:Label ID="lbGroupSalesmanValue" runat="server" Text='<%# Eval("GroupSalesmanValue") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Branch ">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdSalesPointCD" Value='<%# Eval("SalesPointCD") %>' runat="server" />
                                                <asp:Label ID="lbsalespointName" runat="server" Text='<%# Eval("salespointName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:CommandField ShowDeleteButton="True" HeaderText="Action" />
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
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>   
                                    <asp:HiddenField ID="hfdProductID" runat="server" />
                                    <asp:HiddenField ID="hdfSalesPointID" runat="server" />
                                    <asp:HiddenField ID="hdfPeriod" runat="server" /> </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div> 
                <div class="div-table-row">
                     <div class="div-table-colFull">
                    <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button2 save" OnClick="btnClose_Click" style="margin-left: 67px;" />
                         </div>
                    </div>
            </div>

        </div>
    </form>
</body>
