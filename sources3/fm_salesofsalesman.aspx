<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_salesofsalesman.aspx.cs" Inherits="fm_salesofsalesman" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  
        <script>
            function CUSTSelected(sender, e) {
                $get('<%=hdcust.ClientID%>').value = e.get_value();
                $get('<%=btsl.ClientID%>').click();
            }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Sales Report</div>
    <div class="h-divider"></div>
    <asp:UpdatePanel ID="UpdatePanel5" runat="server" >
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row clearfix">
                    <div class="col-md-offset-2 col-md-8 col-sm-offset-1 col-sm-10">
                        <div class="clearfix form-group">
                            <label class="control-label col-sm-2">Salespoint</label>
                            <div class="col-sm-10">
                                <div class="drop-down">
                                    <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="clearfix form-group">
                             <asp:Label ID="Label1" runat="server" Text="Report Type" CssClass="control-label col-sm-2"></asp:Label>
                            <div class="col-sm-10">
                                <div class="drop-down">
                                    <asp:DropDownList ID="cbreporttype" runat="server" CssClass="form-control" OnSelectedIndexChanged="cbreporttype_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                                <asp:CheckBox ID="chsumm" runat="server" ForeColor="Red"  Text="Summary" Visible="False" CssClass="checkbox no-margin-top no-margin-bottom" />
                            </div>
                            <div class="col-sm-offset-2 col-sm-10  margin-top ">
                                <div class="well well-sm no-margin">
                                    <asp:RadioButtonList ID="rditemprod" runat="server" RepeatDirection="Horizontal"  CssClass="radio radio-inline no-margin radio-space-around" OnSelectedIndexChanged="rditemprod_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Value="0">BY PRODUCT GROUP</asp:ListItem>
                                        <asp:ListItem Value="1">BY ITEM</asp:ListItem>
                                    </asp:RadioButtonList>          
                                </div>
                            </div>
                        </div>

                        <div class="clearfix form-group">
                            <div class="col-md-6 no-padding clearfix">
                                 <asp:Label ID="lbldt1" runat="server" Text="Start Date" CssClass="control-label col-md-4 col-sm-2"></asp:Label>
                                <div class="col-md-8 col-sm-10">
                                    <div class="drop-down-date">
                                        <asp:TextBox ID="txdt1" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <asp:CalendarExtender CssClass="date" ID="txdt1_CalendarExtender" runat="server" BehaviorID="txdt1_CalendarExtender" Format="d/M/yyyy" TargetControlID="txdt1">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-md-6 no-padding clearfix">
                                <asp:Label ID="lbldt2" runat="server" Text="End Date" CssClass="control-label col-md-4 col-sm-2"></asp:Label>
                                <div class="col-md-8 col-sm-10">
                                    <div class="drop-down-date">
                                        <asp:TextBox ID="txdt2" runat="server" CssClass="form-control"></asp:TextBox>
                            
                                    </div>
                                    <asp:CalendarExtender CssClass="date" ID="txdt2_CalendarExtender" runat="server" BehaviorID="txdt2_CalendarExtender" TargetControlID="txdt2" Format="d/M/yyyy">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                        </div>
                
                        <div class="clearfix form-group">
                            <asp:Label ID="Label2" runat="server" Text="Cust Name" CssClass="control-label col-sm-2"></asp:Label>
                            <div class="col-sm-10">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
                                    <asp:HiddenField ID="hdcust" runat="server" />
                                        <asp:TextBox ID="txcust" runat="server" AutoPostBack="True" Enabled="False" CssClass=" form-control"></asp:TextBox>  
                          
                 
                                        <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txcust_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txcust" UseContextKey="True" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" CompletionListElementID="divwidths" OnClientItemSelected="CUSTSelected">
                                        </asp:AutoCompleteExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div id="divwidths"  style="font-size:small;font-family:Calibri;"></div>
                            </div>
                        </div>

                        <div class="clearfix form-group">
                            <asp:Label ID="lbsalesman" runat="server" Text="Salesman" CssClass="control-label col-sm-2"></asp:Label>
                            <div class="col-sm-10 ">
                                <div class="drop-down">
                                    <asp:DropDownList ID="cbsalesman" runat="server" CssClass=" form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="clearfix form-group">
                            <asp:Label ID="Label3" runat="server" Text="Outlet Type" CssClass="control-label col-sm-2"></asp:Label>
                            <div class="col-sm-10 ">
                                <div class="drop-down">
                                    <asp:DropDownList ID="cboutlettype" runat="server" Enabled="False" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="clearfix form-group">
                            <div class="no-padding clearfix col-sm-6 form-group">
                                <asp:Label ID="Label4" runat="server" Text="Item" CssClass="control-label col-sm-4"></asp:Label>
                                <div class="col-sm-8 ">
                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server" class="drop-down">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cbitem_cdFr" runat="server" CssClass="form-control chosen-select" AppendDataBoundItems="True" AutoPostBack="True"  data-placeholder="Choose a Item" Enabled="False" >
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="no-padding clearfix col-sm-6">
                                <asp:Label ID="Label5" runat="server" Text="to Item" CssClass="control-label col-sm-4"></asp:Label>
                                <div class="col-sm-8 ">
                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server" class="drop-down">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cbitem_cdTo" runat="server" CssClass="form-control" Enabled="False">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>

                        <div class="clearfix form-group">
                            <div class="clearfix col-sm-6 no-padding form-group">
                                <asp:Label ID="Label6" runat="server" Text="Group" CssClass="control-label col-sm-4"></asp:Label>
                                <div class="col-sm-8 ">
                                     <asp:UpdatePanel ID="UpdatePanel18" runat="server" class="drop-down">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cbProd_cdFr" runat="server" CssClass="form-control" Enabled="False">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="clearfix col-sm-6 no-padding">
                                <asp:Label ID="Label7" runat="server" Text="to Group" CssClass="control-label col-sm-4"></asp:Label>
                                <div class="col-sm-8 ">
                                     <asp:UpdatePanel ID="UpdatePanel20" runat="server" class="drop-down">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cbProd_cdTo" runat="server" CssClass="form-control" Enabled="False">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="navi row margin-bottom">
                     <asp:Button ID="btreport" runat="server" Text="Print" OnClick="btreport_Click" CssClass="btn-info btn btn-print" />
                     <asp:Button ID="btsl" runat="server" OnClick="btsl_Click" style="display:none" />
                 </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>


