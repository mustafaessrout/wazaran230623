<%@ Page Title="" Language="C#" MasterPageFile="~/reporting/reporting.master" AutoEventWireup="true" CodeFile="frmPrnStock.aspx.cs" Inherits="frmPrnStock" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="css/anekabutton.css"
    <link rel="Stylesheet" href="css/chosen.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.4/jquery.min.js" type="text/javascript"></script>
    <script src="css/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        var config = {
            '.chosen-select': {},
            '.chosen-select-deselect': { allow_single_deselect: true },
            '.chosen-select-no-single': { disable_search_threshold: 10 },
            '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
            '.chosen-select-width': { width: "95%" }
        }
        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <div class="divheader">Print Stock Report</div>
    <div class="h-divider"></div>
    <%--<div class="h-divider"></div> --%>
    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-6 ">
                <div class="clearfix form-group">
                    <label class="control-label col-sm-2">Period</label>
                    <div class="col-sm-10">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="drop-down">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbMonthCD" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbMonthCD_SelectedIndexChanged" CssClass="form-control  ">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                         
                    </div>
                </div>
                <div class="clearfix form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                    <div class="no-padding col-xs-5">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txfrom" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender ID="txfrom_CalendarExtender" runat="server" TargetControlID="txfrom" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <p class="col-xs-2 text-center text-bold" style="margin-top: 8px;">To</p>

                    <div class="col-xs-5 no-padding">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txto" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender ID="txto_CalendarExtender" runat="server" TargetControlID="txto" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                </div>
            </div>

            <div class="col-sm-6 clearfix form-group">
                <label class="control-label col-sm-2">Sales Pont CD</label>
                <div class="col-sm-10 drop-down">
                    <asp:updatepanel runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbSalesPointCD" runat="server" AutoPostBack="True" CssClass="makeitreadonly ro form-control  " Enabled="False">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:updatepanel>
                </div>
            </div>
        </div>

        <div class="row padding-top margin-top">
            <div class="col-sm-12 clearfix form-group">
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lbsitetype" runat="server" Text="SiteType" CssClass="control-label col-sm-1"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="col-sm-11 ">
                    <asp:updatepanel runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbsiteDest" runat="server" OnSelectedIndexChanged="cbsiteDest_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control  ">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:updatepanel>
               
                </div>
            </div>

            <div class="col-sm-6 clearfix form-group">
                
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lbwhs" runat="server" Text="Warehouse" CssClass="control-label col-sm-2"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
                <div class="col-sm-10">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbwhs_cd" runat="server" OnSelectedIndexChanged="cbwhs_cd_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control  ">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="col-sm-6 clearfix form-group">
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lbbin" runat="server" Text="BIN" CssClass="control-label col-sm-2"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="col-sm-10">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbBINFr" runat="server" CssClass="form-control  "   >
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
           
                </div>
            </div>
        </div>


        <div class="row padding-top margin-top">
            <div class="col-sm-6 clearfix form-group">
                <label class="col-sm-2 control-label">Item</label>
                <div class="col-sm-10">
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server" class="drop-down">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbitem_cdFr" runat="server"  AppendDataBoundItems="True" AutoPostBack="True" class="form-control   chosen-select" data-placeholder="Choose a Item">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                     
                </div>
            </div>

            <div class="col-sm-6 clearfix form-group">
                <label class="col-sm-2 control-label">to Item</label>
                <div class="col-sm-10">
                    <asp:UpdatePanel ID="UpdatePanel15" runat="server" class="drop-down">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbitem_cdTo" runat="server" CssClass="form-control  ">
                                </asp:DropDownList>
                            </ContentTemplate>
                    </asp:UpdatePanel>
                     
                </div>
            </div>

            <div class="col-sm-6 clearfix form-group">
                <label class="col-sm-2 control-label">Group</label>
                <div class="col-sm-10">
                    <asp:UpdatePanel ID="UpdatePanel18" runat="server" class="drop-down">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbProd_cdFr" runat="server" CssClass="form-control  ">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                     
                </div>
            </div>

            <div class="col-sm-6 clearfix form-group">
                <label class="col-sm-2 control-label">to Group</label>
                <div class="col-sm-10">
                    <asp:UpdatePanel ID="UpdatePanel20" runat="server" class="drop-down">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbProd_cdTo" runat="server" CssClass="form-control  ">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                     
                </div>
            </div>
        </div>


        <div class="row padding-top ">
            <div class="text-center">
                 <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rbRpt" runat="server" AutoPostBack="True" DataValueField="D" OnSelectedIndexChanged="rbRpt_SelectedIndexChanged" RepeatDirection="Horizontal" CssClass="radio radio-inline">
                            <asp:ListItem Value="D">Detail</asp:ListItem>
                            <asp:ListItem Value="S">Summary</asp:ListItem>
                            <asp:ListItem Value="J">Jared</asp:ListItem>
                            <asp:ListItem Value="F">Form</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="navi row padding-bottom padding-top margin-top">
             <asp:Button ID="btprint" runat="server" CssClass="btn-info btn btn-print" Text="Print" OnClick="btprint_Click" />
        </div>
         
        <div class="row">
            <div class="form-group">
            
                <div class="divheader subheader subheader-bg">CHECKING DIFFERENCE DATA</div>
            </div>
        </div>

        <div class="container">
            <div class="row">
                <div class="form-group">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Data Found" ShowHeaderWhenEmpty="true" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="SP CD.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbsalespointcd" runat="server" Text='<%# Eval("salespointcd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                            
                                    <asp:TemplateField HeaderText="Salespoint Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbsp_nm" runat="server" Text='<%# Eval("sp_nm") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Data In HO">
                                        <ItemTemplate>
                                            <asp:Label ID="lbqtyho" runat="server" Text='<%# Eval("qtyho") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Data In Branch">
                                        <ItemTemplate>
                                            <asp:Label ID="lbqty_branch" runat="server" Text='<%# Eval("qty_branch") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Difference">
                                        <ItemTemplate>
                                            <asp:Label ID="lbdiff" runat="server" Text='<%# Eval("diff") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Closing Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbclosing_dt" runat="server" Text='<%# Eval("closing_dt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="table-footer" />
                                <HeaderStyle CssClass="table-header" />
                                <PagerStyle CssClass="table-page" />
                                <RowStyle  />
                                <SelectedRowStyle CssClass="table-edit" />
                            </asp:GridView>
                         </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txto" EventName="TextChanged"/>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>

