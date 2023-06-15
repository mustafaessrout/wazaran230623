<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="frmTranStock.aspx.cs" Inherits="frmTranStock" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />

    <script src="admin/js/bootstrap.min.js"></script>
    <script type = "text/javascript" >

        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>

    <style>
        .hidobject{
            display:none;
        }
        </style>
    <script>
        function openwindow() {
            var oNewWindow = window.open("fm_lookup_tranStock.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }

        function updpnl() {
            document.getElementById('<%=bttmp.ClientID%>').click();
            return (false);
        }
    </script>
    <script>
        function Selecteditem(sender, e) {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            dv.attributes["class"].value = "showdiv";
        }
    </script>
    
 <script>
     function ShowProgress() {
         $('#pnlmsg').show();
     }

     function HideProgress() {
         $("#pnlmsg").hide();
         return false;
     }
     $(document).ready(function () {
         $('#pnlmsg').hide();
     });

    </script>
    <style type="text/css">
        .divmsg{
       /*position:static;*/
       top:30%;
       right:50%;
       left:50%;
       width: 50px;
        height: 45px;
       position:fixed;
       /*background-color:greenyellow;*/
       overflow-y:auto;
  }
        .divhid {
            display:none;
        }

        .divnormal {
            display:normal;
        }
    </style> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Destroy, Add and Lose Request</div>

    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row">
            <div class="clearfix">

                <div class="clearfix form-group col-sm-3 no-padding-right">
                    <label class="control-label col-sm-3">Status</label>
                    <div class="col-sm-9" >
                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                            <ContentTemplate>
                                  <asp:Label ID="lbstatus" runat="server" CssClass="label label-primary"></asp:Label>
                            </ContentTemplate>
                         </asp:UpdatePanel>
                    </div>
                </div>

                <div class="clearfix form-group col-sm-3 no-padding-right">
                    <label class="control-label col-sm-3">Ref</label>
                    <div class="col-sm-9 ">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" Class="input-group">
                            <ContentTemplate>
                                <asp:TextBox ID="txtrnstkNo" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                <div class="input-group-btn">
                                    <asp:LinkButton ID="btsearch" runat="server" CssClass="btn btn-sm btn-primary" OnClientClick="openwindow();return(false);"><span class="fa fa-search"></span></asp:LinkButton>
                                </div>
                             </ContentTemplate>
                             <Triggers><asp:AsyncPostBackTrigger ControlID="bttmp" EventName="Click" /></Triggers>
                        </asp:UpdatePanel>
                    
                    </div>
                </div>

                <div class="clearfix form-group col-sm-3 no-padding-right">
                    <label class="control-label col-sm-3">Salespoint</label>
                    <div class="col-sm-9">
                         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbSalesPointCD" runat="server" AutoPostBack="True" CssClass="form-control" Enabled="False">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="clearfix form-group col-sm-3 no-padding-right">
                    <label class="control-label col-sm-3">Manual No</label>
                    <div class="col-sm-9">
                        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtrnstkmanno" runat="server" CssClass="form-control" Height="100%"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="clearfix">
                <div class="clearfix form-group col-sm-3 no-padding-right">
                    <label class="control-label col-sm-3">Date</label>
                    <div class="col-sm-9 drop-down-date">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dttrnstkDate" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender CssClass="date" ID="dttrnstkDate_CalendarExtender" runat="server" StartDate="09/4/2018" TargetControlID="dttrnstkDate" DaysModeTitleFormat="dd/M/yyyy" Format="dd/M/yyyy" TodaysDateFormat="dd/M/yyyy">
                                </asp:CalendarExtender>
                           </ContentTemplate>
                        </asp:UpdatePanel>
                     
                    </div>
                </div>
                
            
                <div class="clearfix form-group col-sm-3 no-padding-right">
                    <label class="control-label col-sm-3">Trans type</label>
                    <div class="col-sm-9 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbinvtype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbinvtype_SelectedIndexChanged" CssClass="form-control">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                     
                    </div>
                </div>

                <div class="clearfix form-group col-sm-3 no-padding-right">
                    <label class="control-label col-sm-3">Warehouse</label>
                    <div class="col-sm-9 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbwhs_cd" runat="server" CssClass="form-control" AutoPostBack="True">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                     
                    </div>
                </div>

                <div class="clearfix form-group col-sm-3 no-padding-right">
                    <label class="control-label col-sm-3">Bin</label>
                    <div class="col-sm-9 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbbin_cd" runat="server" AutoPostBack="True" CssClass="form-control" >
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <asp:UpdatePanel ID="ddpspvpnl" runat="server" class="clearfix form-group col-sm-12 no-padding-right">
                <ContentTemplate>
                    <div id="dv" runat="server">
                        <asp:Panel runat="server" ID="pspvlabel" CssClass="control-label no-padding-left text-bold col-lg-1 col-sm-3">Prod Spv.</asp:Panel>
                        <div class="col-lg-11 col-sm-9 drop-down">
                            <asp:DropDownList ID="ddpspv" runat="server" AutoPostBack="True" CssClass="form-control" >
                            </asp:DropDownList>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>


            <div class="clearfix form-group col-sm-12 no-padding-right">
                <label class="control-label col-sm-1">Remark</label>
                <div class="col-sm-11">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                        <asp:TextBox ID="txtrnstkRemark" runat="server" CssClass="form-control" Height="100%"></asp:TextBox>
                      </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="clearfix form-group col-sm-6">
                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="clmLbl" runat="server" Text="Claimed" CssClass="control-label col-sm-2"></asp:Label>
                        <div class="col-sm-10">
                            <asp:CheckBox CssClass="checkbox no-margin-top" ID="chclaim" runat="server" Text="Claim" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="clearfix text-right col-sm-6 no-padding-right" runat="server" id="vClaim">
                <div class="col-sm-12">
                     <asp:UpdatePanel runat="server"><ContentTemplate>
                    <asp:LinkButton ID="btdestroy" runat="server" CssClass="btn btn-primary" OnClick="btdestroy_Click">Destroy Now</asp:LinkButton>
                    <%--<asp:Button ID="btdestroy" runat="server" CssClass="button2 btn" OnClick="btdestroy_Click" style="left: 0px; top: 0px" Text="View Item Destroy" />--%>
                    </ContentTemplate></asp:UpdatePanel>
                </div>
            </div>

        </div>
    </div>


    <div class="container-fluid top-devider">
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel1" runat="server">
                        <div class="h-divider"></div>
                        <div style="padding-bottom:10px;padding-top:10px; display:flex; justify-content:space-between">
                            <strong>Document Related</strong>
                            <div id="lbdoc5" runat="server">
                                <asp:Label Text="Document Approval" CssClass="titik-dua" style="padding-right:15px;" runat="server" />
                                <asp:HyperLink id="lbldoc5" CssClass="btn btn-link" runat="server" />
                            </div>
                        </div>
                        <div class="overflow-x">
                            <asp:GridView ID="grddoc" CssClass="table table-hover table-striped mygrid" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="None" PageSize="5" Width="100%" ForeColor="#333333" ShowFooter="True" OnRowCancelingEdit="grddoc_RowCancelingEdit" OnRowEditing="grddoc_RowEditing" OnRowUpdating="grddoc_RowUpdating">
                                <AlternatingRowStyle  />
                                <Columns>
                                    <asp:TemplateField HeaderText="Document Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lbdoccode" runat="server" Text='<%# Eval("doc_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document Name">
                                        <ItemTemplate>
                                            <asp:Label ID="docnm" runat="server" Text='<%# Eval("doc_nm") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UploadImage">
                                    <ItemTemplate>
                                        <asp:Image ImageUrl='<%# Eval("filename") %>' runat="server" ID="image" /> 
                                        <a class="example-image-link" data-lightbox='example-1<%# Eval("filename") %>' href='/images/<%# Eval("filename") %>'>
                                        <asp:Label ID="lbfilename" runat="server" Text='<%# Eval("filename") %>'></asp:Label>
                                        </a> 
                                    </ItemTemplate>
                                    <ItemTemplate>
                                        <a class="example-image-link" href="/images/<%# Eval("filename") %>" data-lightbox="example-1<%# Eval("filename") %>">
                                        <asp:Label ID="lbfilename" runat="server" Text='<%# Eval("filename") %>'></asp:Label>
                                        </a>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:FileUpload ID="FileUpload1" runat="server" /> 
                                    </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowEditButton="True" />
                                </Columns>
                                <EditRowStyle CssClass="table-edit" />
                                <FooterStyle CssClass="table-footer" />
                                <HeaderStyle CssClass="table-header" />
                                <PagerStyle CssClass="table-page" />
                                <RowStyle  />
                                <SelectedRowStyle CssClass="table-edit" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                <asp:PostBackTrigger ControlID="grddoc" /> 
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="container-fluid top-devider">
        <div class="row "  >
            <asp:UpdatePanel ID="UpdatePanel13" runat="server" >
                <ContentTemplate>
                    <div class="overflow-y" style="max-height:300px; width:100%;">
                        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="4"  GridLines="None" OnPageIndexChanging="grd_PageIndexChanging" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowDeleting="grd_RowDeleting" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating"  CssClass="mygrid table table-striped table-page-fix table-hover table-fix"  data-table-page="#copy-fst" OnRowDataBound="grd_RowDataBound" ShowFooter="True">
                            <AlternatingRowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblseqno" runat="server" Text='<%# Eval("seqno") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <%# Eval("item_nm") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Brand">
                                    <ItemTemplate>
                                        <%# Eval("branded_nm") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size">
                                    <ItemTemplate>
                                        <%# Eval("size") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <div style="text-align: right;">
                                        <asp:Label ID="lblqty" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
				                <div style="text-align: right;">
				                <asp:Label ID="lblTotalqty" runat="server" />
				                </div>
			                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtqty" runat="server" Text='<%# Eval("qty") %>' CssClass="form-control input-sm" Width="70px"></asp:TextBox>
                                    </EditItemTemplate>
                                     
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="OUM">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="cboUOM" runat="server" CssClass="form-control input-sm">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Price">
                                    <ItemTemplate>
                                        <%# Eval("unitprice") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <div style="text-align: right;">
                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount","{0:n2}") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
				                <div style="text-align: right;">
				                <asp:Label ID="lblTotalAmount" runat="server" />
				                </div>
			                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltrnstkNo" runat="server" Text='<%# Eval("trnstkNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsalespointCD" runat="server" Text='<%# Eval("salespointCD") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                            </Columns>
                            <EditRowStyle CssClass="table-edit" />
                            <FooterStyle CssClass="table-footer" />
                            <HeaderStyle CssClass="table-header" />
                            <PagerStyle CssClass="table-page" />
                            <RowStyle  />
                            <SelectedRowStyle CssClass="table-edit" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </div>
                    <asp:HiddenField ID="maxQty" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="table-copy-page-fix" id="copy-fst"></div>
        </div>
    </div>

    <div class="container-fluid top-devider">
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel2" runat="server">
                      <table class="table table-striped mygrid table-title-left row-no-padding">
                            <tr >
                                <th>Item Name</th>
                                <th>UOM</th>
                                <th>Quantity</th>
                                <th>Price</th>
                                <th>Action</th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txsearchitem" runat="server" AutoPostBack="True" OnTextChanged="txsearchitem_TextChanged" CssClass="form-control input-sm"></asp:TextBox>
                                             <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" ID="txsearchitem_AutoCompleteExtender" runat="server" TargetControlID="txsearchitem" ServiceMethod="GetCompletionList"  MinimumPrefixLength="1" 
                                            EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="Selecteditem" UseContextKey="True">
                                            </asp:AutoCompleteExtender>
                                             <asp:HiddenField ID="hditem" runat="server" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cbUOM" runat="server" CssClass="auto-style3 form-control input-sm" AutoPostBack="True" OnSelectedIndexChanged="cbUOM_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txqty" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txunitprice" runat="server" CssClass="makeitreadonly ro form-control input-sm" Enabled="False" ></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width:50px;">
                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                        <ContentTemplate>
                                 
                                            <asp:Button ID="btadd" runat="server" CssClass="btn-success btn btn-sm btn-add" OnClick="btadd_Click" Text="Add" />
                                     
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td> 
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="container-fluid top-devider">
        <div class="navi row margin-bottom margin-top">
            <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                <ContentTemplate>
                    <asp:Button ID="btnew" runat="server" CssClass="btn-success btn btn-add" OnClick="btnew_Click" Text="NEW" />
                    <asp:Button ID="btsave" runat="server" CssClass="btn-warning btn btn-save" OnClick="btsave_Click" Text="Save" OnClientClick="javascript:ShowProgress();"/>
                    <asp:Button ID="btDelete" runat="server" CssClass="btn-danger btn btn-delete" OnClick="btDelete_Click" Text="Delete" Visible="False" />
                    <asp:Button ID="btprint" runat="server" CssClass="btn-info btn btn-print" OnClick="btprint_Click" Text="Print" />
                     <asp:Button ID="bttmp" runat="server" Text="Button" OnClick="bttmp_Click" style="display:none" />
                 
                </ContentTemplate>
             </asp:UpdatePanel>
        </div>
    </div>
  <div class="divmsg loading-cont" id="pnlmsg" >
            <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
        </div>
</asp:Content>

