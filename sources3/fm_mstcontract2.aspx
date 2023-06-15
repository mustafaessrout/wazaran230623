<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstcontract2.aspx.cs" Inherits="fm_mstcontract2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function CustSelected(sender, e)
        {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
        }
        function ItemSelected(sender, e) {
            $get('<%=hditem.ClientID%>').value = e.get_value();
                }
        function ItemFreeSelected(sender, e) {
            $get('<%=hditemfree.ClientID%>').value = e.get_value();
                }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">BUSINESS AGREEMENT</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row margin-bottom text-small">
            <div class="clearfix form-group col-sm-5">
                <label class="col-sm-4 control-label-sm">Contract No.</label>
                <div class="col-sm-8">
                     <asp:TextBox ID="txcontractno" runat="server" CssClass="ro form-control input-sm" Enabled="false">NEW</asp:TextBox>
                </div>
            </div>
            <div class="clearfix form-group col-sm-offset-2 col-sm-5">
                <label class="col-sm-4 control-label-sm">Date</label>
                <div class="col-sm-8 drop-down drop-down-date">
                    <asp:TextBox ID="dtcontract" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    <asp:CalendarExtender CssClass="date" ID="dtcontract_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtcontract">
                    </asp:CalendarExtender>
                 
                </div>
            </div>
            <div class="clearfix form-group col-sm-5">
                <label class="col-sm-4 control-label-sm">Agreement Type</label>
                <div class="col-sm-8 drop-down">
                     <asp:DropDownList ID="cbagreetype" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnSelectedIndexChanged="cbagreetype_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
            <div class="clearfix form-group col-sm-offset-2 col-sm-5">
                <label class="col-sm-4 control-label-sm">First Party</label>
                <div class="col-sm-8">
                    <p class="text-red text-bold" style="padding-top:5px;">SALEEM BAWAZIR TRADING CORP.</p>
                </div>
            </div>
        </div>
    </div>

    <div class="divheader subheader subheader-bg">Gondola Section</div>

    <div class="container-fluid text-small">
        <div class="row">
           
            <div class=" col-sm-5 clearfix form-group">
                <label class="col-sm-4 control-label-sm">Payment Term</label>
                <div class="col-sm-8 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbcontractterm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbcontractterm_SelectedIndexChanged" CssClass="form-control input-sm" Enabled="false">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbagreetype" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="col-sm-offset-2 col-sm-5 clearfix form-group">
                <label class="col-sm-4 control-label-sm">Payment Type</label>
                <div class="col-sm-8 drop-down">
                    
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbpaymenttype" runat="server" AutoPostBack="True" CssClass="form-control input-sm" OnSelectedIndexChanged="cbpaymenttype_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbagreetype" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
          
        </div>
        <div class="row">
            <div class="col-sm-12">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grdschedule" runat="server" AutoGenerateColumns="False" CellPadding="0"  GridLines="None" CssClass=" table table-striped mygrid row-no-padding margin-bottom" >
                        <AlternatingRowStyle  />
                            <Columns>
                                <asp:TemplateField HeaderText="No." >
                                    <ItemTemplate ><%# Eval("sequenceno") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="dtpayment" runat="server" Text='<%# Eval("payment_dt","{0:d/M/yyyy}") %>' CssClass="form-control input-sm"></asp:TextBox>
                                        <asp:CalendarExtender CssClass="date"  ID="CalendarExtender1" runat="server" TargetControlID="dtpayment" Format="d/M/yyyy"></asp:CalendarExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txamt" runat="server" CssClass="form-control input-sm"></asp:TextBox></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txqty" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle  />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbcontractterm" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbpaymenttype" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="divheader subheader subheader-bg">Tactical Bonus Section</div>

    <div class="container-fluid text-small">
        <div class="row margin-bottom">
            <div class=" col-sm-5 clearfix form-group">
                <label class="col-sm-4 control-label-sm">Previous Sold</label>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txprevsold" runat="server"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
             <div class="col-sm-offset-2 col-sm-5 clearfix form-group">
                <label class="col-sm-4 control-label-sm">Bonus Pct</label>
                <div class="col-sm-8 ">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txpct" runat="server"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class=" col-sm-5 clearfix form-group">
                <label class="col-sm-4 control-label-sm">Increasing Target Sold Pct</label>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txincreasing" runat="server"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <div class="h-divider"></div>
        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="clearfix form-group col-sm-5">
                        <label class="col-sm-4 control-label-sm"> Second Party Type</label>
                        <div class="col-sm-8">
                    
                                     <asp:RadioButtonList ID="rdcust" CssClass="radio toolbar-group-radio no-margin" runat="server" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rdcust_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="C">Customer</asp:ListItem>
                                        <asp:ListItem Value="G">Group Customer</asp:ListItem>
                                    </asp:RadioButtonList>
                            
                  
                        </div>
                    </div>
                    <div class="clearfix form-group col-sm-offset-2 col-sm-5">
                        <label class="col-sm-4 control-label-sm">Cust/Group</label>
                        <div class="col-sm-8">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="input-group">
                                <ContentTemplate>

                                    <asp:DropDownList ID="cbgroupcust" runat="server" Visible="False"  CssClass="form-control input-sm">
                                    </asp:DropDownList>

                                    <asp:TextBox ID="txcust" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="txcust_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txcust" UseContextKey="True" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionSetCount="10" OnClientItemSelected="CustSelected">
                                    </asp:AutoCompleteExtender>
                                    <div class="input-group-btn">
                                        <asp:Button ID="btadd" runat="server" Text="Add" CssClass="btn-success btn-sm btn btn-add" OnClick="btadd_Click" />
                                    </div>
 
                                    <asp:HiddenField ID="hdcust" runat="server" />

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grdcust" runat="server" AutoGenerateColumns="False" CellPadding="0" OnRowDeleting="grdcust_RowDeleting" CssClass="table table-striped table-hover mygrid text-small">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lbcustcode" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer / Group">
                                            <ItemTemplate><%# Eval("cust_nm") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" />
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="clearfix form-group col-sm-5">
                        <label class="col-sm-4 control-label-sm"> Product / Item</label>
                        <div class="col-sm-8">

                            <asp:RadioButtonList ID="rditem" CssClass="radio no-margin toolbar-group-radio" runat="server" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rditem_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="I">Item</asp:ListItem>
                                <asp:ListItem Value="P">Product Group</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="clearfix form-group col-sm-offset-2 col-sm-5">
                        <label class="col-sm-4 control-label-sm"> Item/Prod Group</label>
                        <div class="col-sm-8">
                            <div class=" input-group">
                                <asp:DropDownList ID="cbprod" runat="server" Visible="False" CssClass="form-control input-sm">
                                    </asp:DropDownList>

                                    <asp:TextBox ID="txitem" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="txitem_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList2" TargetControlID="txitem" UseContextKey="True" EnableCaching="false" FirstRowSelected="false" CompletionSetCount="10" MinimumPrefixLength="1" OnClientItemSelected="ItemSelected">
                                    </asp:AutoCompleteExtender>

                                    <div class="input-group-btn">
                                        <asp:Button ID="btadditem" runat="server" Text="Add" CssClass="btn-success btn btn-sm btn-add" OnClick="btadditem_Click" />
                                    </div>
                                <asp:HiddenField ID="hditem" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                    <asp:GridView ID="grditem" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover mygrid" OnRowDeleting="grditem_RowDeleting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("itemcode") %>'></asp:Label></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item / Group Product">
                                            <ItemTemplate><%# Eval("itemname") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" />
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="clearfix form-group col-sm-5">
                        <label class="col-sm-4 control-label-sm">Paid Product / Item</label>
                        <div class="col-sm-8">

                            <asp:RadioButtonList ID="rdfreeitem" CssClass="radio toolbar-group-radio no-margin" runat="server" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rdfreeitem_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="I">Item</asp:ListItem>
                                <asp:ListItem Value="P">Product Group</asp:ListItem>
                            </asp:RadioButtonList>

                        </div>
                    </div>
                    <div class="clearfix form-group col-sm-offset-2 col-sm-5">
                        <label class="col-sm-4 control-label-sm">Paid Free/Product</label>
                        <div class="col-sm-8">
                            <asp:UpdatePanel ID="UpdatePanel12" runat="server" class="input-group">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbfreeprod" runat="server" Visible="False" CssClass="form-control input-sm">
                                    </asp:DropDownList>

                                    <asp:TextBox ID="txfreeitem" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="txfreeitem_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList3" TargetControlID="txfreeitem" UseContextKey="True" FirstRowSelected="false" EnableCaching="false" MinimumPrefixLength="1" CompletionSetCount="10" OnClientItemSelected="ItemFreeSelected">
                                    </asp:AutoCompleteExtender>
                                    <div class="input-group-btn">
                                        <asp:Button ID="btaddfree" runat="server" Text="Add" CssClass="btn-success btn btn-sm btn-add" OnClick="btaddfree_Click" />
                                    </div>
                            
                                    <asp:HiddenField ID="hditemfree" runat="server" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rdfreeitem" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cbpaymenttype" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <asp:GridView ID="grdfreeitem" CssClass="table table-stirped table-hover mygrid" runat="server" AutoGenerateColumns="False"  OnRowDeleting="grdfreeitem_RowDeleting" CellPadding="0" GridLines="None">
                            <AlternatingRowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("itemcode") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item / Group Product">
                                    <ItemTemplate><%# Eval("itemname") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="h-divider"></div>

        <div class="row">
            <div class="clearfix form-group col-sm-12">
                <label class="col-sm-2 control-label-sm">A. Agreement</label>
                <div class="col-sm-10">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                             <asp:GridView ID="grdagree" CssClass="table table-striped mygrid" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="None">
                                <AlternatingRowStyle  />
                                <Columns>
                                    <asp:TemplateField HeaderText="Choose">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkselect" runat="server" CssClass="no-margin checkbox checkbox-block-center" /></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lbagreecode" runat="server" Text='<%# Eval("agree_cd") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Agreement">
                                        <ItemTemplate><%# Eval("agree_desc") %></ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbagreetype" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <div class="h-divider"></div>

        <div class="row">
            <div class="clearfix form-group col-sm-12">
                <label class="col-sm-2 control-label-sm">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                              B. <asp:Label ID="lbB" runat="server" Text="Label"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbagreetype" EventName="SelectedIndexChanged" />
                        </Triggers>
                  </asp:UpdatePanel>
                </label>
                <div class="col-sm-10">
                     <asp:GridView ID="grddisplay" CssClass="table table-striped mygrid" runat="server" AutoGenerateColumns="False" CellPadding="0" Width="100%"  GridLines="None">
                        <AlternatingRowStyle/>
                        <Columns>
                            <asp:TemplateField HeaderText="Choose">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chselect" runat="server" CssClass="no-margin checkbox checkbox-block-center" /></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Display">
                                <ItemTemplate><%# Eval("fld_desc") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate>
                                    <asp:TextBox ID="txsize" runat="server" CssClass="form-control input-sm"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txqty" runat="server" CssClass="form-control input-sm"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle  />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </div>
            </div>

             <div class="clearfix form-group col-sm-12">
                <label class="col-sm-offset-2 col-sm-2 control-label-sm"  style="margin-bottom:10px;">Location Display</label>
                <div class="col-sm-3 " style="margin-bottom:5px;">
                    <div class="drop-down-sm">
                        <asp:DropDownList ID="cbloc" runat="server" CssClass="form-control input-sm">
                        </asp:DropDownList>
                    </div>
                </div>
                 <div class="col-sm-5">
                     <p>Location must in a prime area, clearly visible from all sides, and must be in food sections.</p>
                 </div>
            </div>
        </div>

        <div class="h-divider"></div>

        <div class="row">
            <div class="clearfix form-group col-sm-12">
                <label class="col-sm-2 control-label-sm">C. Period</label>
                <div class="col-sm-10 no-padding">
                    <div class="col-sm-3 drop-down-date">
                        <asp:TextBox ID="dtstart" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                        <asp:CalendarExtender CssClass="date" ID="dtstart_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtstart">
                        </asp:CalendarExtender>
                    </div>
                    <p class="col-sm-1 text-center" style="margin-top:7px;">To</p>
                    <div class="col-sm-3 drop-down-date">
                        <asp:TextBox ID="dtend" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                        <asp:CalendarExtender CssClass="date" ID="dtend_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtend">
                        </asp:CalendarExtender>
                    </div>
                    <p class="col-sm-5 text-center" style="margin-top:7px;">Valid period contract</p>
                </div>
            </div>
        </div>

        <div class="h-divider"></div>

        <div class="row">
            <label class="col-sm-2 control-label-sm">D. Term Of Display Rental</label>
            <div class="col-sm-10">
                <asp:GridView ID="grdterm" CssClass="table table-striped mygrid text-left" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="None">
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Choose">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkchoose" runat="server" CssClass="no-margin checkbox checkbox-block-center" /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Terms Of Display">
                            <ItemTemplate><%# Eval("agree_desc") %></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle CssClass="table-edit" />
                    <FooterStyle CssClass="table-footer" />
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page"/>
                    <RowStyle  />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>
        </div>

        <div class="h-divider"></div>

        <div class="row">
            <label class="col-sm-2 control-label-sm">E. Damage or Expired Products Policy</label>
            <div class="col-sm-10">
                <asp:GridView ID="grddamage"  CssClass="table table-striped mygrid text-left" runat="server" AutoGenerateColumns="False"  CellPadding="0"  GridLines="None">
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Choose">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkchoose" runat="server" CssClass="no-margin checkbox checkbox-block-center"/></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Damage or Expired Products Policy">
                            <ItemTemplate><%# Eval("agree_desc") %></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle CssClass="table-edit" />
                    <FooterStyle CssClass="table-footer" />
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page"/>
                    <RowStyle  />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>
        </div>
    </div>

    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row navi margin-bottom">
            <asp:Button ID="btnew" runat="server" CssClass="btn-success btn btn-add" OnClick="btnew_Click" Text="New" />
            <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn-warning btn btn-save" OnClick="btsave_Click" />
        </div>
    </div>
    
</asp:Content>

