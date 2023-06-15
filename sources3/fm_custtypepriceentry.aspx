<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_custtypepriceentry.aspx.cs" Inherits="fm_custtypepriceentry" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  
    <script>
        function ItemSelected(sender, e)
        {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            $get('<%=btadd.ClientID%>').click();
        }
    </script>
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
        <div class="form-horizontal">
        <div class="divheader">Item Price Level</div>
        <div class="h-divider"></div>
        <div class="container-fluid">
            <div class="row">
                <div class="form-group">
            
                    <div class=" col-md-6 col-sm-10 "> 
                        <div class="input-search">
                        
                            <div class="col-sm-2">
                                <label class="control-label">Item</label>
                            </div>

                            <div class="col-sm-9">
                                <div class="input-group">
                                    <asp:HiddenField ID="hditem" runat="server" />
                                    <asp:TextBox ID="txsearchitem" runat="server" CssClass="form-control" style="border-radius:5px 0 0 5px;"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="txsearchitem_AutoCompleteExtender" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txsearchitem" UseContextKey="True" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="ItemSelected" MinimumPrefixLength="1" CompletionListElementID="divw">
                                    </asp:AutoCompleteExtender>
                                    <div class="input-group-btn">
                                         <button type="submit" class="btn btn-primary btn-search" runat="server" id="btadd" onserverclick="btadd_Click">
                                            <i class="fa fa-search" aria-hidden="true"></i>
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
           
                </div>
            </div>
            <div class="row">

            
                <div class="h-divider"></div>
                <div class="">
                     <div class="overflow-y" style="max-height:290px; width:100%;">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grdprice" runat="server" AutoGenerateColumns="False" CellPadding="1"  GridLines="None" CssClass="table table-striped table-fix mygrid" >
                                    <AlternatingRowStyle/>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lbcusttyp" runat="server" Text='<%# Eval("fld_valu") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name" >
                                            <ItemTemplate>
                                                <%# Eval("fld_desc") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Price">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txunitprice" runat="server"  CssClass="form-control input-sm ro" Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Effective Date">
                                            <ItemTemplate>
                                                <asp:CalendarExtender ID="CalendarExtender1" CssClass="date" runat="server" TargetControlID="dteffective" Format="d/M/yyyy"></asp:CalendarExtender>
                                                <asp:TextBox ID="dteffective" runat="server" TextMode="SingleLine" CssClass="form-control input-sm "></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="End Date">
                                            <ItemTemplate>
                                                 <asp:CalendarExtender ID="CalendarExtender2" CssClass="date" runat="server" TargetControlID="dtend" Format="d/M/yyyy"></asp:CalendarExtender>
                                                <asp:TextBox ID="dtend" CssClass="form-control input-sm" runat="server" TextMode="SingleLine"  ></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle CssClass="table-edit" />
                                    <FooterStyle CssClass="table-footer" />
                                    <HeaderStyle CssClass="table-header" />
                                    <PagerStyle CssClass="table-page" />
                                    <RowStyle  />
                                    <SelectedRowStyle CssClass="table-selected" />
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
            <div id="divw"  ></div>
            <div class="navi row margin-bottom">
                <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn-warning btn btn-save" OnClick="btsave_Click" />
                <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
            </div>
        </div>
    </div>
         
    
        
</asp:Content>

