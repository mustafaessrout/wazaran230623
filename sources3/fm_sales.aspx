<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_sales.aspx.cs" Inherits="fm_sales" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function ItemSelectedsalesman_cd(sender, e) {

            $get('<%=hdsalesman_cd.ClientID%>').value = e.get_value();
        }
    </script>
  
    <link href="v4-alpha/docs.min.css" rel="stylesheet" />
    

    <script src="v4-alpha/docs.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divheader">Sales Report</div>
    <div class="h-divider"></div>
    <div class="container-fluid">
     
        <div class="row">
            <div class="clearfix " >
                <div class="col-sm-12  no-padding">
                    <div class="clearfix margin-bottom">
                        <label class="col-sm-2 control-label">Branch</label>
                        <div class="col-sm-10 drop-down">
                            <asp:DropDownList ID="cbbranch" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                     <div class="clearfix margin-bottom">
                        <label class="col-sm-2 control-label">Report</label>
                        <div class="col-sm-10 drop-down">
                            <asp:DropDownList ID="cbreport" CssClass="form-control input-group-lg" runat="server" >
                            <asp:ListItem Value="0">NET SALES FOR SALESMAN</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="clearfix margin-bottom">
                        <label for="Salesman" class="col-sm-2 control-label">Salesman</label>
                        <div class="col-sm-8">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txsalesman" runat="server"  CssClass="form-control " ></asp:TextBox>
                                    <div id="divwidth" style="font-size: small;  position: absolute; width=100% !important;"></div>
                                    <asp:HiddenField ID="hdsalesman_cd" runat="server" />
                                    <ajaxToolkit:AutoCompleteExtender ID="txsalesman_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txsalesman" UseContextKey="True"
                                        CompletionListElementID="divwidth" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="ItemSelectedsalesman_cd">
                                    </ajaxToolkit:AutoCompleteExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                         <div class="col-sm-2">
                            <asp:Button ID="btadd" runat="server" Text="Add"  CssClass="btn btn-block btn-success" OnClick="btadd_Click" />
                        </div>
                    </div>
                </div>
            </div>
           
            <div class="margin-bottom">                   
                <div >
                    <asp:GridView ID="grdsl" runat="server" AutoGenerateColumns="False" OnRowDeleting="grdsl_RowDeleting" CssClass="table table-striped table-bordered table-hover"  CellPadding="4" ForeColor="#333333" GridLines="None" HorizontalAlign="Justify" BorderColor="Black" BorderWidth="1px">
                        <AlternatingRowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbsalesman_cd" runat="server" Text='<%# Eval("salesman_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salesman Name">
                                <ItemTemplate><%# Eval("salesman_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="Action" ShowDeleteButton="True" />
                        </Columns>
                        <EditRowStyle CssClass="table-edit"/>
                        <FooterStyle CssClass="table-footer"/>
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page"/>
                        <RowStyle />
                        <SelectedRowStyle CssClass="table-edit" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                </div>                        
            </div>

            <div class="row clearfix">
                <div class="col-sm-6 clearfix margin-bottom">
                    <label for="startDate" class="col-sm-4 control-label titik-dua">Start Date</label>
                    <div class="col-sm-8 drop-down-date">
                        <asp:TextBox ID="dtstart" runat="server" CssClass="form-control " ></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="dtstart_CalendarExtender" CssClass="date" runat="server" BehaviorID="dtstart_CalendarExtender" Format="d/M/yyyy" TargetControlID="dtstart">
                        </ajaxToolkit:CalendarExtender>
                    </div>
                </div>

                <div class="col-sm-6 clearfix margin-bottom">
                <label class="col-sm-4 control-label titik-dua">End Date</label>
                <div class="col-sm-8 drop-down-date">                        
                        <asp:TextBox ID="dtend" runat="server" CssClass="form-control " ></asp:TextBox>
                        <ajaxToolkit:CalendarExtender CssClass="date" ID="dtend_CalendarExtender" runat="server" BehaviorID="dtend_CalendarExtender" Format="d/M/yyyy" TargetControlID="dtend">
                        </ajaxToolkit:CalendarExtender>
                    </div>
                </div>

                <div class="col-sm-6 clearfix">
                    <label class="col-sm-4 control-label titik-dua">Group from</label>
                    <div class="col-sm-8 drop-down">
                        <asp:DropDownList ID="cbProd_cdFr" CssClass="form-control input-group-lg" runat="server" >
                            </asp:DropDownList>
                        </div>
                </div>

                <div class="col-sm-6 clearfix">
                    <label class="col-sm-4 control-label titik-dua">To</label>
                    <div class="col-sm-8 drop-down">
                    <asp:DropDownList ID="cbProd_cdTo" CssClass="form-control input-group-lg" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        
      
            <div class="row">

                <div class="form-group">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                
                                <asp:GridView ID="grd" EnableEventValidation="false" runat="server" CssClass="table table-striped table-bordered table-hover"  CellPadding="4" ForeColor="#333333" GridLines="None" HorizontalAlign="Justify">
                                    <AlternatingRowStyle BackColor="White" />
                                    <EditRowStyle CssClass="table-edit"/>
                                    <FooterStyle CssClass="table-footer"/>
                                    <HeaderStyle CssClass="table-header" />
                                    <PagerStyle CssClass="table-page"/>
                                    <RowStyle />
                                    <SelectedRowStyle CssClass="table-edit" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                    
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>


            </div>
            <div class="row margin-top padding-top">
                <div class="navi margin-top margin-bottom">
                    <asp:Button ID="btprint" runat="server" Text="Grid to Excel" CssClass="btn btn-info btn-print" OnClick="btprint_Click" />
                    <asp:Button ID="btpdf" runat="server" Text="Grid to PDF" CssClass="btn btn-info btn-print" OnClick="btpdf_Click" Visible="false" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

