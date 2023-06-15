<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_AccSettingFinanceReport.aspx.cs" Inherits="fm_AccSettingFinanceReport" %>
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

<%--        function updpnl() {
            document.getElementById('<%=bttmp.ClientID%>').click();
            return (false);
        }--%>
    </script>
<%--    <script>
        function Selecteditem(sender, e) {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            dv.attributes["class"].value = "showdiv";
        }
    </script>--%>
    
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
    <div class="divheader">Customizable Financial Report Setting</div>

    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row">
            <div class="clearfix">
                <label class="control-label col-md-1">Report ID</label>
                <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                        <ContentTemplate>
                        <asp:TextBox ID="txReportID" runat="server" CssClass="form-control" Height="100%" ></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">Report Name</label>
                <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                        <ContentTemplate>
                        <asp:TextBox ID="txReportName" runat="server" CssClass="form-control" Height="100%" AutoPostBack="True" OnTextChanged="txReportName_SelectedIndexChanged" ></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">Financial Report Type</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>                                            
                            <asp:DropDownList ID="ddFrType" runat="server" CssClass="auto-style3 form-control input-sm" AutoPostBack="True">
                                <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                                <asp:ListItem Value="is">Profit and Loss</asp:ListItem>
                                <asp:ListItem Value="bs">Financial Position</asp:ListItem>
                                <asp:ListItem Value="dc">Direct Cash FLow</asp:ListItem>
                                <asp:ListItem Value="ic">Indirect Cash Flow</asp:ListItem>                       
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">Remarks</label>            
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                        <asp:TextBox ID="txRemark" runat="server" CssClass="form-control" Height="100%"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>  
            </div> 
            <br/>
            <div class="clearfix">  
                <label class="control-label col-md-1">MTD / YTD</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>                                            
                            <asp:DropDownList ID="ddMtdYtd" runat="server" CssClass="auto-style3 form-control input-sm" AutoPostBack="True">
                                <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                                <asp:ListItem Value="M">MTD</asp:ListItem>
                                <asp:ListItem Value="Y">YTD</asp:ListItem>                     
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">Status</label>          
                <div class="col-md-2">
<%--                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                        <asp:TextBox ID="txStatus" runat="server" CssClass="form-control" Height="100%"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>--%>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>                                            
                            <asp:DropDownList ID="ddStatus" runat="server" CssClass="auto-style3 form-control input-sm" AutoPostBack="True">
                                <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                                <asp:ListItem Value="A">Active</asp:ListItem>
                                <asp:ListItem Value="I">Inactive</asp:ListItem>                     
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

<%--    <div class="container-fluid top-devider">
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel1" runat="server">
                        <div class="h-divider"></div>
                        <div style="padding-bottom:10px;padding-top:10px">
                            <strong>Document Related</strong>
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
                                            <%# Eval("doc_nm") %>
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
    </div>--%>

    </br>

    <!--end of jd row-->
    <asp:UpdatePanel ID="UpdatePanel26" runat="server">
        <ContentTemplate>                                 
            <asp:Button ID="btNewDetailRow" runat="server" CssClass="btn-success btn btn-sm btn-add" OnClick="btaddNewDetailRow_Click" Text="Add New Report Detail Row" />                                     
        </ContentTemplate>
    </asp:UpdatePanel>

    <%--<div class="container-fluid top-devider"style="background-color:lightblue">--%>
    <div class="container-fluid top-devider">
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel20" runat="server" class="top-devider">
                <ContentTemplate>
                    <asp:Panel ID="newDetailRow" runat="server">
                      <%--<table class="table table-striped mygrid table-title-left row-no-padding" style="background-color:lightblue">--%>
<%--                      <table class="table table-striped mygrid table-title-left row-no-padding" style="background-color:white">
                            <tr >
                                <th>Line No</th> 
                                <th>Amount Real or Nominal</th> 
                                <th>Amount Debit or Credit</th>
                                <th>Description Detail</th>
                                <th>Is Indirect Cash Flow</th>
                                <th>Row Attribute Type</th>
                                <th>Account Source</th>
                                <th>Operator</th>
                                <th>Amount Source</th>
                                <th></th>
                            </tr>--%>
                            <%--<tr>--%>

                        <div class="col-lg-5">
                            <div class="row">
                                <label class="control-label col-md-3">Sequence No</label>
                                <div class="col-md-9">
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                        <ContentTemplate>
                                        <asp:TextBox ID="txseqno" runat="server" CssClass="form-control" Height="100%"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <label class="control-label col-md-3">Description</label>
                                <div class="col-md-9">
                                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txdescription" runat="server" AutoPostBack="True" CssClass="form-control input-sm"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <label class="control-label col-md-3">Row Attribute Type</label>
                                <div class="col-md-9">
                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                            <ContentTemplate>
                                                <asp:RadioButtonList ID="rdrowattrtype" runat="server" CssClass="form-control input-sm radio radio-space-around no-margin" AutoPostBack="True" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdrowattrtype_SelectedIndexChanged">
                                                    <asp:ListItem Value="H">Header</asp:ListItem>
                                                    <asp:ListItem Value="L">Line</asp:ListItem>
                                                    <asp:ListItem Value="D">Detail</asp:ListItem>
                                                    <asp:ListItem Value="S">Summary</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

<%--                                <label class="control-label col-md-3">Real or Nominal Sourc</label>
                                <div class="col-md-9">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <asp:RadioButtonList ID="rdrealnominal" runat="server" CssClass="form-control input-sm radio radio-space-around no-margin" AutoPostBack="True" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdrealnominal_SelectedIndexChanged">
                                                <asp:ListItem Value="R">Transaction Only</asp:ListItem>
                                                <asp:ListItem Value="N">Transaction with Begin Balance</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>--%>
                            </div>
<%--                            <div class="row">                                    
                                <label class="control-label col-md-3">Debit, Credit or Balanc</label>
                                <div class="col-lg-9">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:RadioButtonList ID="rddebitcreditbal" runat="server" CssClass="form-control input-sm radio radio-space-around no-margin" AutoPostBack="True" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdrealnominal_SelectedIndexChanged">
                                                <asp:ListItem Value="D">Debit</asp:ListItem>
                                                <asp:ListItem Value="C">Credit</asp:ListItem>
                                                <asp:ListItem Value="B">Balance</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>--%>
                            <div class="row">
<%--                                <label class="control-label col-md-3">Is Indirect Cash Flow</label>
                                <div class="col-md-9">
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <ContentTemplate>
                                            <asp:RadioButtonList ID="rdisindirectcf" runat="server" CssClass="form-control input-sm radio radio-space-around no-margin" AutoPostBack="True" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdrealnominal_SelectedIndexChanged">
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>--%>
                                <label class="control-label col-md-3">Amount Reference</label>
                                <div class="col-md-9">
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddAmountRef" runat="server" CssClass="auto-style3 form-control input-sm" AutoPostBack="True" OnSelectedIndexChanged="ddAmountRef_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txAmountRef" runat="server" CssClass="form-control" Height="100%"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-5">
                                
                            <div class="row">
      
                                <label class="control-label col-md-3">Account Source</label>
                                <div class="col-md-9">
                                    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddAccountSrc" runat="server" CssClass="auto-style3 form-control input-sm" AutoPostBack="True" OnSelectedIndexChanged="ddAccountSrc_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txAccountSrc" runat="server" CssClass="form-control" Height="100%"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                
                                <label class="control-label col-md-3">Calculation Value</label>
                                <div class="col-md-9">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:RadioButtonList ID="rdcalcval" runat="server" CssClass="form-control input-sm radio radio-space-around no-margin" AutoPostBack="True" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdrealnominal_SelectedIndexChanged">
                                                    <asp:ListItem Value="+">+</asp:ListItem>
                                                    <asp:ListItem Value="-">-</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <label class="control-label col-md-3">Displayed Value</label>
                                <div class="col-md-9">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:RadioButtonList ID="rddispval" runat="server" CssClass="form-control input-sm radio radio-space-around no-margin" AutoPostBack="True" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdrealnominal_SelectedIndexChanged">
                                                    <asp:ListItem Value="+">Genuine ([+ as +] or [- as -])</asp:ListItem>
                                                    <asp:ListItem Value="-">Conversed ([+ as -] or [- as +])</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <label class="control-label col-md-3">Is Hidden</label>
                                <div class="col-md-9">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <asp:RadioButtonList ID="rdishidden" runat="server" CssClass="form-control input-sm radio radio-space-around no-margin" AutoPostBack="True" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdrealnominal_SelectedIndexChanged">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div> 
                        <div class="col-lg-2">
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                 
                                    <asp:Button ID="btadd" runat="server" CssClass="btn-success btn btn-sm btn-add" OnClick="btadd_Click" Text="Save Add Row" /></br></br>
                                    <asp:Button ID="btCancelAddRow" runat="server" CssClass="btn-success btn btn-sm btn-add" OnClick="btCancelAddRow_Click" Text="Cancel" />
                                     
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    </br>

    

    <!--jd row-->

    <div class="container-fluid top-devider">
        <div class="row "  >
            <asp:UpdatePanel ID="UpdatePanel13" runat="server" >
                <ContentTemplate>
                    <%--<div class="overflow-y" style="max-height:350px; width:100%;">--%>
                    <div class="overflow-y" style="width:100%;">
                        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="4"  GridLines="None" OnPageIndexChanging="grd_PageIndexChanging" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowDeleting="grd_RowDeleting" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating"  CssClass="mygrid table table-striped table-page-fix table-hover table-fix"  data-table-page="#copy-fst" OnRowDataBound="grd_RowDataBound" ShowFooter="True">
                            <AlternatingRowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Report Detail ID">
                                    <ItemTemplate>
                                        <div style="text-align: left;">
                                        <asp:Label ID="lbfinreportdetid" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Sequence No.">
                                    <ItemTemplate>
                                        <div style="text-align: left;">
                                        <asp:Label ID="lbseqno" runat="server" Text='<%# Eval("seq_no") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <div style="text-align: left;">
                                        <asp:Label ID="lbdesc" runat="server" Text='<%# Eval("description") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Row Attribute Type">
                                    <ItemTemplate>
                                        <div style="text-align: left;">
                                        </span><asp:Label ID="lbRowAttrTyp" runat="server" Text='<%# Eval("row_attrib_type") %>' ></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
<%--                                <asp:TemplateField HeaderText="Amount Real or Nominal">
                                    <ItemTemplate>
                                        <div style="text-align: left;">
                                        <asp:Label ID="lbRealNominal" runat="server" Text='<%# Eval("real_nominal") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Debit, Credit or Balance">
                                    <ItemTemplate>
                                        <div style="text-align: left;">
                                        <asp:Label ID="lbDrCrBal" runat="server" Text='<%# Eval("db_cr_bal") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                              <%--  <asp:TemplateField HeaderText="Is Indirect Cash Flow">
                                <ItemTemplate>
                                        <div style="text-align: left;">
                                        <asp:Label ID="lbIsIndCF" runat="server" Text='<%# Eval("is_indirect_cf") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Amount Reference">
                                    <ItemTemplate>
                                        <div style="text-align: left;">
                                        <asp:Label ID="lbAmtRef" runat="server" Text='<%# Eval("amt_ref") %>' ></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Account Source">
                                    <ItemTemplate>
                                        <div style="text-align: left;">
                                        <asp:Label ID="lbAcctSrc" runat="server" Text='<%# Eval("acct_src") %>' ></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Calculated Value">
                                    <ItemTemplate>
                                        <div style="text-align: left;">
                                        <asp:Label ID="lbCalcVal" runat="server" Text='<%# Eval("calc_val") %>' ></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Displayed Value">
                                    <ItemTemplate>
                                        <div style="text-align: left;">
                                        <asp:Label ID="lbDispVal" runat="server" Text='<%# Eval("disp_val") %>' ></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Is Hidden">
                                    <ItemTemplate>
                                        <div style="text-align: left;">
                                        <asp:Label ID="lbIsHidden" runat="server" Text='<%# Eval("is_hidden") %>' ></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True"/>
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
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="table-copy-page-fix" id="copy-fst"></div>
        </div>

        <div class="h-divider"></div>

    </div>

    
    <%--<div class="container-fluid top-devider">--%>
    <div>
        <div class="navi row margin-bottom margin-top">
            <asp:UpdatePanel ID="UpdatePanel18" runat="server" class="top-devider">
                <ContentTemplate>
                    <%--<asp:Button ID="btnew" runat="server" CssClass="btn-success btn btn-add" OnClick="btnew_Click" Text="NEW" />--%>
                    <asp:Button ID="btsave" runat="server" CssClass="btn-warning btn btn-save" OnClick="btsave_Click" Text="Save"/>
                    <%--<asp:Button ID="btDelete" runat="server" CssClass="btn-danger btn btn-delete" OnClick="btDelete_Click" Text="Delete" Visible="False" />--%>
                    <%--<asp:Button ID="btprint" runat="server" CssClass="btn-info btn btn-print" OnClick="btprint_Click" Text="Print" />--%>
                     <%--<asp:Button ID="bttmp" runat="server" Text="Button" OnClick="bttmp_Click" style="display:none" />--%>
                    <asp:Button ID="btcancel" runat="server" CssClass="btn-warning btn btn-save" OnClick="btcancel_Click" Text="Back to List" OnClientClick="javascript:ShowProgress();"/>
                    <asp:Button ID="btdelete" runat="server" CssClass="btn-warning btn btn-save" OnClick="btdelete_Click" Text="Delete"/>
                 
                </ContentTemplate>
             </asp:UpdatePanel>
        </div>
    </div>
  <div class="divmsg loading-cont" id="pnlmsg" >
            <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
        </div>
</asp:Content>

