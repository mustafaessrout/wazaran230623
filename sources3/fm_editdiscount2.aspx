<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_editdiscount2.aspx.cs" Inherits="fm_editdiscount2" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <asp:HiddenField ID="hddisc" runat="server" />
    <asp:HiddenField ID="hdfreeitem" runat="server" />
    <asp:HiddenField ID="hdprop" runat="server" />

    <div class="container">
        <div class="page-header">
            <h3>Edit Discount
                <br />
                <small>
                <asp:Label ID="lbdisc" runat="server" ForeColor="Red"></asp:Label>
                </small>
            </h3>
        </div>

        <div class="bd-example">

            <div class="row">
                <div class="col-md-4">
                    <label for="propCode" class="col-xs-4 col-form-label">Prop No</label>
                    <asp:Label ID="lbprop" runat="server" Text="Label" CssClass="col-xs-8 col-form-label font-weight-bold"></asp:Label>
                </div>
                <div class="col-md-8">
                    <label for="remark" class="col-xs-2 col-form-label">Remark</label>
                    <asp:Label ID="lbremark" runat="server" Text="Label" CssClass="col-xs-10 col-form-label font-weight-bold"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <label for="dtstart" class="col-xs-4 col-form-label text-lg-left">Start Date</label>                        
                    <asp:Label ID="dtstart" runat="server" Text="Label" CssClass="col-xs-8 col-form-label font-weight-bold text-lg-left"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label for="dtdelivery" class="col-xs-5 col-form-label">Delivery Date</label>
                    <asp:Label ID="lbdelidate" runat="server" Text="Label" CssClass="col-xs-7 col-form-label font-weight-bold"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label for="dtend" class="col-xs-4 col-form-label">End Date</label>
                    <asp:Label ID="dtend" runat="server" Text="Label" CssClass="col-xs-8 col-form-label font-weight-bold"></asp:Label>
                    
                </div>
            </div>

        </div>

        <div class="bd-example">
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label for="discStatus" class="col-xs-4 col-form-label">Delivery Date</label>
                        <div class="col-xs-8">
                        <asp:TextBox ID="dteditdelivery" runat="server" CssClass="form-control input-group-lg" Height="2em"></asp:TextBox>
                            <asp:CalendarExtender CssClass="date" ID="dteditdelivery_CalendarExtender" runat="server" TargetControlID="dteditdelivery" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label for="discStatus" class="col-xs-4 col-form-label">End Date</label>
                        <div class="col-xs-8">
                        <asp:TextBox ID="dteditend" runat="server" CssClass="form-control input-group-lg" Height="2em"></asp:TextBox>
                        <asp:CalendarExtender CssClass="date" ID="dteditend_CalendarExtender" runat="server" TargetControlID="dteditend" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label for="discType" class="col-xs-4 col-form-label">Discount Type</label>
                        <div class="col-xs-8">
                        <asp:DropDownList ID="cbdisctyp" runat="server" CssClass="form-control-static" Width="100%"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label for="disc" class="col-xs-4 col-form-label">Disc Used To</label>
                        <div class="col-xs-8">
                            <asp:RadioButtonList ID="rdused" runat="server" CssClass="form-control" style="background-color:skyblue" RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0" Width="100%">
                                <asp:ListItem Value="M">Manual</asp:ListItem>
                                <asp:ListItem Value="A">Automatic</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label for="regCost" class="col-xs-4 col-form-label">Regular Cost</label>
                        <div class="col-xs-8">
                        <asp:TextBox ID="txregularcost" runat="server" CssClass="form-control input-group-lg" Height="2em"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label for="netCost" class="col-xs-4 col-form-label">Net Cost</label>
                        <div class="col-xs-8">
                        <asp:TextBox ID="txnetcost" runat="server" CssClass="form-control input-group-lg" Height="2em"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label for="orderMin" class="col-xs-4 col-form-label">Minimum Order</label>
                        <div class="col-xs-8">
                        <asp:TextBox ID="txminqty" runat="server" CssClass="form-control input-group-lg" Height="2em" OnTextChanged="txminqty_TextChanged" AutoPostBack="True"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label for="orderMax" class="col-xs-4 col-form-label">Maximum Order</label>
                        <div class="col-xs-8">
                        <asp:TextBox ID="txmaxorder" runat="server" CssClass="form-control input-group-lg" Height="2em"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <div class="bd-example">

            <div class="row">
                <div class="col-md-12">
                    <label for="RemarkEdit" class="col-xs-4 col-form-label">Remark</label>
                    <asp:TextBox ID="txremark" runat="server" CssClass="form-control input-group-lg" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="bd-example">
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <label for="discMechanism" class="col-xs-3 col-form-label">Discount Mechanism</label>
                        <div class="col-xs-9">
                        <asp:RadioButtonList ID="rdmethod" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdmethod_SelectedIndexChanged" CssClass="form-control" style="background-color:skyblue" RepeatDirection="Horizontal"  Width="100%">
                            <asp:ListItem Value="FG">Free Good</asp:ListItem>
                            <asp:ListItem Value="CH">Free Cash</asp:ListItem>
                            <asp:ListItem Value="PC">Free Percentage</asp:ListItem>
                            <asp:ListItem Value="CG">Free Cash as Good</asp:ListItem>
                        </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <div class="col-md-1">
                            <label for="minQty">Min Qty</label>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txordermin" runat="server" CssClass="form-control input-group-lg" Height="2em" ></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txminqty" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>                        
                        </div>
                        <div class="col-md-2">
                            <label for="minQty">UOM</label>
                            <asp:DropDownList ID="cbuom" runat="server" CssClass="form-control-static" Width="100%"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label for="minQty">Free Qty</label>
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txqty" runat="server" CssClass="form-control input-group-lg" Height="2em"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rdmethod" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        
                        </div>
                        <div class="col-md-2">
                            <label for="minQty">UOM Free</label>
                            <asp:DropDownList ID="cbuomfree" runat="server" CssClass="form-control-static" Width="100%"></asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <label for="minQty">Cash</label>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txcash" runat="server" CssClass="form-control input-group-lg" Height="2em"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rdmethod" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>                        
                        </div>
                        <div class="col-md-1">
                            <label for="minQty">% Cash</label>
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txpercent" runat="server" CssClass="form-control input-group-lg" Height="2em"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rdmethod" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>                        
                        </div>
                        <div class="col-md-2">
                            <label for="minQty">Type</label>
                            <asp:DropDownList ID="cbmethod" runat="server" CssClass="form-control-static" Width="100%"></asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <label for="minQty"> </label>
                            <asp:Button ID="btaddformula" runat="server" Text="Add" CssClass="btn btn-default" OnClick="btaddformula_Click" />
                        </div>
                    </div>
                </div>
            </div>
                <div class="col-sm-12">
                    <div class="form-group">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                        <div class="table-responsive">
                            <asp:GridView ID="grdformula" OnRowDeleting="grdformula_RowDeleting" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">  
                                <Columns>
                                    <asp:TemplateField HeaderText="Min Qty">
                                         <ItemTemplate>
                                             <asp:Label ID="lbminqty" runat="server" Text='<%# Eval("min_qty") %>'></asp:Label>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="UOM">
                                         <ItemTemplate>
                                             <%# Eval("UOM") %>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Free Qty">
                                         <ItemTemplate><%# Eval("qty") %></ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="UOM Free">
                                         <ItemTemplate><%# Eval("uom_free") %></ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Free Cash">
                                         <ItemTemplate><%# Eval("amt") %></ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Free Pct">
                                         <ItemTemplate><%# Eval("percentage") %></ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Disc Type">
                                         <ItemTemplate><%# Eval("disc_typ") %></ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:CommandField ShowDeleteButton="True" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btaddformula" EventName="Click" />
                        </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
        </div>

        <div class="bd-example">
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <label for="freeItem" class="col-xs-3 col-form-label">Free Item</label>
                        <div class="col-xs-9">
                        <asp:RadioButtonList ID="rdfree" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdfree_SelectedIndexChanged" CssClass="form-control" style="background-color:skyblue" RepeatDirection="Horizontal"  Width="100%">
                            <asp:ListItem Value="P">Item</asp:ListItem>
                            <asp:ListItem Value="G">Product </asp:ListItem>
                        </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <div class="col-md-2">
                            <label for="branded" style="background-color:skyblue">Branded</label>
                            <asp:DropDownList ID="cbbrandedfree" runat="server" OnSelectedIndexChanged="cbbrandedfree_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-static" Width="100%"></asp:DropDownList>  
                        </div>
                        <div class="col-md-3">
                            <label for="branded" style="background-color:skyblue">Group Product</label>
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                     <asp:DropDownList ID="cbprodgroupfree" runat="server" OnSelectedIndexChanged="cbprodgroupfree_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-static" Width="100%"></asp:DropDownList> 
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cbbrandedfree" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>                        
                        </div>
                        <div class="col-md-3">
                            <label for="branded" style="background-color:skyblue">Product</label>
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                     <asp:DropDownList ID="cbitemfree" runat="server" OnSelectedIndexChanged="cbitemfree_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-static" Width="100%"></asp:DropDownList> 
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cbprodgroupfree" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>                         
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                        <div id="showItemFree" runat="server">
                            <div class="col-md-3">
                                <label for="branded" style="background-color:skyblue">Item</label>
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                         <asp:DropDownList ID="cbitemfrees" runat="server" CssClass="form-control-static" Width="100%"></asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="cbitemfree" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>                          
                            </div>
                        </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rdfree" EventName="SelectedIndexChanged" />
                        </Triggers>
                        </asp:UpdatePanel>  

                        <div class="col-md-1">
                            <label for="action"> </label>
                            <asp:Button ID="btaddfree" runat="server" Text="Add" CssClass="btn btn-default" OnClick="btaddfree_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                        <div class="table-responsive">
                            <asp:GridView ID="grdfreeitem" AllowPaging="True" OnPageIndexChanging="grdfreeitem_PageIndexChanging" OnRowDeleting="grdfreeitem_RowDeleting1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">  
                                <Columns>
                                    <asp:TemplateField HeaderText="Item Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name"><ItemTemplate><%# Eval("item_desc") %></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size">
                                       <ItemTemplate> <%# Eval("size") %></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Branded">
                                        <ItemTemplate>
                                            <asp:Label ID="lbbranded" runat="server" Text='<%# Eval("branded_nm") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM">
                                        <ItemTemplate><%# Eval("uom") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" />
                                </Columns>
                            </asp:GridView>
                            <asp:GridView ID="grdfreeproduct" AllowPaging="True" OnPageIndexChanging="grdfreeitem_PageIndexChanging" OnRowDeleting="grdfreeitem_RowDeleting1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">  
                                <Columns>
                                    <asp:TemplateField HeaderText="Prod Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lbprodcode" runat="server" Text='<%# Eval("prod_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <%# Eval("prod_nm") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        </ContentTemplate>
                         <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btaddfree" EventName="Click" />
                        </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <img src="div2.png" class="divid" />
    <div class="navi">
        <asp:Button ID="btcancel" runat="server" Text="Cancel" CssClass="button2 cancel" OnClick="btcancel_Click" />
        <asp:Button ID="btsave" runat="server" Text="Save Update" CssClass="button2 save" OnClick="btsave_Click" />
    </div>

     
</asp:Content>

