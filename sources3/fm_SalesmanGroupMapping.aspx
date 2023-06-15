<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_SalesmanGroupMapping.aspx.cs" Inherits="fm_SalesmanGroupMapping" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
         .auto-style1 {
             width: 100%;
         }
     </style>
            <script>
                function salesmanSelected(sender, e) {
                    $get('<%=hdemp.ClientID%>').value = e.get_value();
                    document.getElementById('<%=bttmp.ClientID%>').click();


                }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class="divheader">Sales Group Mapping</div>
    <div class="h-divider"></div>
   
    <div class="container-fluid">
        <div class="row clearfix">
            <div class="col-md-offset-2 col-sm-offset-1 col-md-8 col-sm-10 ">
                <div class=" clearfix">
                    <div class="col-sm-12 form-group">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblSalesma" runat="server" Text="Salesman" CssClass="col-sm-2 control-label"></asp:Label> 
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txsalesman" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:HiddenField ID="hdemp" runat="server" />
                                    <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListItemCssClass="auto-complate-item" CompletionListHighlightedItemCssClass="auto-complate-hover" ID="txsalesman_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList1" TargetControlID="txsalesman" UseContextKey="True" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" CompletionListElementID="divwidths" OnClientItemSelected="salesmanSelected">
                                    </asp:AutoCompleteExtender>                        
                                </div>
                                <asp:Button ID="bttmp" runat="server" Text="Button" OnClick="bttmp_Click" Style="display: none" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class=" clearfix">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grd" CssClass="table table-striped table-hover text-left mygrid" DataKeyNames="emp_cd" runat="server" AutoGenerateColumns="False"  CellPadding="1"  GridLines="None" OnRowDataBound="grd_RowDataBound">
                                <AlternatingRowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Salesman ">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdempCD" Value='<%# Eval("emp_cd") %>' runat="server" />
                                            <asp:Label ID="lbEmployeeName" runat="server" Text='<%# Eval("emp_desc") %>'></asp:Label>
                                            <asp:HiddenField ID="hdfisVan" Value='<%# Eval("isVan") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                               
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkisVan" runat="server" CssClass="checkbox no-margin checkbox-block-center" />
                                        </ItemTemplate>

                                        <HeaderTemplate>
                                            <p class="text-center no-margin">isVan</p>
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                               

                                </Columns>

                                <EditRowStyle CssClass="table-edit" />
                                <FooterStyle CssClass="table-footer" />
                                <HeaderStyle CssClass="table-header" />
                                <PagerStyle CssClass="table-page" />
                                <RowStyle  />
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

        <div class="navi row margin-bottom margin-top">
            <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn-warning btn btn-save" OnClick="btsave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn-danger btn btn-cancel" OnClick="btCancel_Click" />
        </div>

    </div>
    
    
</asp:Content>

