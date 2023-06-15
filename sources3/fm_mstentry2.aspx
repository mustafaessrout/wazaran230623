<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstentry2.aspx.cs" Inherits="fm_mstentry2" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    
 <link href="css/anekabutton.css" rel="stylesheet" />
  <script>
      function ItemSelected(sender, e)
      {
          $get('<%=hdsize.ClientID%>').value = e.get_value();
      }
  </script>

    <style>
        .custom-table1>tbody> tr> td:first-child{
            width:250px;
        }
        .custom-table1>tbody> tr> td:last-child{
            width:250px;
        }
    </style>
 </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <div class="divheader">
               Entry Item <asp:Label ID="lbstatus" runat="server" style="color: #FF0000"></asp:Label>
           </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="h-divider"></div>

    <div class="container-fluid margin-bottom">
        <div class="row">
            <div class="clearfix col-sm-6 ">
                <div class="clearfix form-group">
                    <label class="col-sm-2 control-label">Category</label>
                    <div class="col-sm-10 drop-down">
                        <asp:DropDownList ID="cbbranded" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbbranded_SelectedIndexChanged" >
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="clearfix form-group">
                    <label class="col-sm-2 control-label">Shape</label>
                    <div class="col-sm-10">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" Class="drop-down">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbsubbranded" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbsubbranded_SelectedIndexChanged" CssClass="form-control">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cbbranded" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="clearfix form-group">
                    <label class="col-sm-2 control-label">Size</label>
                    <div class="col-sm-10">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" Class="drop-down">
                            <ContentTemplate>
                                <div>
                                    <asp:DropDownList ID="cbproduct" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cbsubbranded" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="clearfix form-group">
                    <label class="col-sm-2 control-label">Product Type</label>
                    <div class="col-sm-10 drop-down">
                        <asp:DropDownList ID="cbprodtype" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <div class="divheader subheader subheader-bg">Item Attribute</div>

    <div class="container-fluid margin-bottom">
        <div class="row">
            <div class="clearfix col-sm-6 ">
                <div class="clearfix ">
                    <label class="col-sm-2 control-label">Item Code</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txitemcode" runat="server" CssClass="makeitreadonly form-control"></asp:TextBox> 
                        <asp:RequiredFieldValidator ID="reqitemcode" runat="server" ErrorMessage="/**" ControlToValidate="txitemcode" CssClass="validator-alert"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="clearfix ">
                    <label class="col-sm-2 control-label">Item Name</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txitemname" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txitemname" ErrorMessage="/**" CssClass="validator-alert"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="clearfix form-group">
                    <label class="col-sm-2 control-label">Shortname</label>
                    <div class="col-sm-10">
                       <asp:TextBox ID="txitemshortname" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="clearfix form-group">
                    <label class="col-sm-2 control-label">Arabic</label>
                    <div class="col-sm-10">
                       <asp:TextBox ID="txarabic" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="clearfix form-group">
                    <label class="col-sm-2 control-label">Branded</label>
                    <div class="col-sm-10 drop-down">
                       <asp:DropDownList ID="cbbrand" runat="server" CssClass="form-control">
                       </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="clear-float col-sm-6  avatar text-center">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/noimage.jpg" Height="100px" />
            </div>
        </div>
    </div>

    <div class="divheader subheader subheader-bg">Item Property</div>

    <div class="container-fluid margin-bottom">
        <div class="row">
            <div class="clearfix form-group col-sm-6">
                <label class="col-sm-2 control-label">UOM Base</label>
                <div class="drop-down col-sm-10">
                    <asp:DropDownList ID="cbuom" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="clearfix form-group col-sm-6">
                <label class=" col-sm-2 col-xs-12 control-label">Vendor Price</label>
                <div class="col-md-7 col-sm-8 col-xs-9 ">
                    <asp:TextBox ID="txvendorprice" runat="server" CssClass="form-control"></asp:TextBox> 
                </div>
                <p class="col-sm-2 col-xs-3 text-center no-margin no-padding" style="line-height:30px;">(EGP)</p>
            </div>


            <div class="clearfix form-group col-sm-6">
                <label class="col-sm-2 control-label"> Size<asp:HiddenField ID="hdsize" runat="server" /></label>
                <div class="col-sm-10">
                    <asp:TextBox ID="txsizesearch" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txsizesearch_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txsizesearch" UseContextKey="True" MinimumPrefixLength="1" CompletionInterval="10" FirstRowSelected="false" EnableCaching="false" OnClientItemSelected="ItemSelected">
                    </asp:AutoCompleteExtender>
                </div>
            </div>
            <div class="clearfix form-group col-sm-6">
                <label class="col-sm-2 col-xs-12 control-label">Branches Price</label>
                <div class="col-md-7 col-sm-8 col-xs-9 ">
                    <asp:TextBox ID="txunitprice" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <p class="col-sm-2 col-xs-3 text-center no-margin no-padding" style="line-height:30px;">(EGP)</p>
            </div>

            <div class="clearfix form-group col-sm-6">
                <label class="col-sm-2 control-label">Length</label>
                <div class="col-sm-4">
                    <asp:TextBox ID="txlength" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <label class="col-sm-2 control-label" style="text-align:right;">Width</label>
                <div class="col-sm-4">
                    <asp:TextBox ID="txwidth" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="clearfix form-group col-sm-6">
                <label class="col-sm-2 control-label" >Height</label>
                <div class="col-sm-4">
                    <asp:TextBox ID="txheigth" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <label class="col-sm-2 control-label" style="text-align:right;">Volume</label>
                <div class="col-sm-4">
                    <asp:TextBox ID="txvol" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="clearfix form-group col-sm-6" style="margin-bottom: 10px;">
                <label class="col-sm-2 control-label"> Barcode Carton</label>
                <div class="col-sm-10">
                    <asp:TextBox ID="txbarcodectn" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="clearfix form-group col-sm-6">
                <label class="col-sm-2 control-label"> Barcode Box</label>
                <div class="col-sm-10">
                    <asp:TextBox ID="txbarcodebox" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="clearfix form-group col-sm-6">
                <label class="col-sm-2 control-label">Barcode Product</label>
                <div class="col-sm-10">
                    <asp:TextBox ID="txbarcodeprod" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

        </div>
        <div class="row padding-top">
            <div class="clearfix alert alert-danger no-padding-bottom">
                <label class="col-sm-2 control-label">UOM Converter</label>
                <div class="col-sm-3 drop-down no-padding-left margin-bottom">
                    <asp:DropDownList ID="cbuomfrom" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-3 drop-down no-padding-left margin-bottom">
                    <asp:DropDownList ID="cbuomto" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-3 no-padding-left margin-bottom">
                    <asp:TextBox ID="txconvertqty" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-1 col-sm-2 no-padding-left margin-bottom">
                     <asp:Button ID="btaddconvert" runat="server" Text="Add" CssClass="btn-block btn-success btn btn-add" OnClick="btaddconvert_Click"  />   
                </div>
            </div>
        </div>
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grduomconvert" runat="server" AutoGenerateColumns="False" OnRowDeleting="grduomconvert_RowDeleting" GridLines="None" CssClass="table table-striped mygrid">
                        <AlternatingRowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="UOM FROM">
                                <ItemTemplate>
                                    <asp:Label ID="lbuomfrom" runat="server" Text='<%# Eval("uom_from") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM TO">
                                <ItemTemplate><%# Eval("uom_to") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CONVERSION (QTY)">
                                <ItemTemplate><%# Eval("qty") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle/>
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btaddconvert" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
  
    <div class="divheader subheader subheader-bg">Item Information</div>

    <div class="container-fluid margin-bottom">
        <div class="row">
            <div class="clearfix form-group col-sm-6">
                <label class="col-sm-2 control-label">Image</label>
                <div class="col-sm-10">
                    <asp:FileUpload ID="uplfile" runat="server" />
                </div>
            </div>
            <div class="clearfix form-group col-sm-6">
                <label class="col-sm-2 control-label">Packing</label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList ID="cbpacking" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            
        </div>
        <div class="row">
            <div class="clearfix form-group col-sm-6">
                <label class="col-sm-2 control-label">Remark</label>
                <div class="col-sm-10">
                    <asp:TextBox ID="txremark" runat="server" TextMode="MultiLine" CssClass="form-control" Style="resize: none;" Height="80"></asp:TextBox>
                </div>
            </div>
            <div class="clearfix form-group col-sm-6">
                <label class="col-sm-2 control-label">Vendor</label>
                <div class="col-sm-10">
                    <asp:DropDownList ID="cbvendor" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="clearfix form-group col-sm-6">
                <label class="col-sm-2 control-label">Start Date</label>
                <div class="col-sm-10">
                    <asp:TextBox ID="dtstart" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:CalendarExtender CssClass="date" ID="dtstart_CalendarExtender" runat="server" TargetControlID="dtstart" Format="d/M/yyyy">
                    </asp:CalendarExtender>
                </div>
            </div>
            <div class="clearfix form-group col-sm-6">
                <label class="col-sm-2 control-label">End Date </label>
                <div class="col-sm-10">
                    <asp:TextBox ID="dtend" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:CalendarExtender CssClass="date" ID="dtend_CalendarExtender" runat="server" TargetControlID="dtend" Format="d/M/yyyy">
                    </asp:CalendarExtender>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="clearfix alert alert-danger no-padding-bottom">
                <label class="col-sm-2 control-label">Sell Salespoint</label>
                <div class="drop-down col-sm-6">
                    <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="checkbox col-sm-2">
                    <asp:CheckBox ID="chk" runat="server" Text="All" CssClass=""/>
                </div>
                <div class="col-md-1 col-sm-2 margin-bottom">
                    <asp:Button ID="btadd" runat="server" Text="Add" CssClass="btn-success btn-block btn btn-add" OnClick="btadd_Click" />
                </div>
            </div>
        </div>
        <div class="row">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grdsp" runat="server" AutoGenerateColumns="False" OnRowDeleting="grdsp_RowDeleting" AllowPaging="True" OnPageIndexChanging="grdsp_PageIndexChanging" GridLines="None" PageSize="5" CellPadding="0" CssClass="table table-striped  table-hover mygrid custom-table1">
                            <AlternatingRowStyle/>
                            <Columns>
                                <asp:TemplateField HeaderText="Salespoint Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbsalespointcode" runat="server" Text='<%# Eval("salespointcd") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Salespoint Name">
                                    <ItemTemplate><%# Eval("salespoint_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="table-page" />
                            <RowStyle/>
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btadd" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
         </div>
        
        
    </div>

    <div class="h-divider no-margin-bottom margin-top"></div>

    <div class="container-fluid">
        <div class="row ">
            <p class="text-red text-right" style="padding-top:10px;">* = Mandatory Field , please check before save/add data !</p>
        </div>
        <div class="row navi margin-bottom">
           <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn-warning btn btn-save" OnClick="btsave_Click"/>
           <asp:Button ID="btedit" runat="server" Text="Edit" CssClass="btn-primary btn btn-edit" OnClick="btedit_Click" />
           <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info btn btn-print"/>
        </div>
    </div>
</asp:Content>

