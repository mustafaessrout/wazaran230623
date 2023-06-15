<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_claiminfoh.aspx.cs" Inherits="fm_claiminfoh" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/sweetalert.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script> 
    <script src="js/sweetalert.min.js"></script>
    <script src="js/sweetalert-dev.js"></script>
    <script>
       function openreport(url)
       {
           window.open(url, "myrep");
       }

   </script> 
</head>
<body>
    
    <div class="container-fluid">
        <form id="form1" runat="server">

        <asp:ScriptManager runat="server" ID="ScriptManager1" />

        <div class="form-horizontal">
            <div class="row">
                <div class="col-sm-12">
                    <ol class="breadcrumb">
                        <li><h3>Claim Header Info (<asp:Label ID="lbclaim" runat="server"></asp:Label>)</h3></li>
                    </ol>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-2"><h6><strong>Claim Date</strong></h6></div>
                <div class="col-sm-1"><h6>:</h6></div>
                <div class="col-sm-3"><h6><strong><asp:Label ID="lbclaimdt" runat="server"></asp:Label></strong></h6></div>
                <div class="col-sm-2"><h6><strong>Branch</strong></h6></div>
                <div class="col-sm-1"><h6>:</h6></div>
                <div class="col-sm-3"><h6><strong><asp:Label ID="lbsalespoint" runat="server"></asp:Label></strong></h6></div>
            </div>
            <div class="row">
                <div class="col-sm-2"><h6><strong>Proposal No</strong></h6></div>
                <div class="col-sm-1"><h6>:</h6></div>
                <div class="col-sm-3"><h6><strong><asp:Label ID="lbproposal" runat="server"></asp:Label></strong></h6></div>
                <div class="col-sm-2"><h6><strong>Status</strong></h6></div>
                <div class="col-sm-1"><h6>:</h6></div>
                <div class="col-sm-3"><h6><strong><asp:Label ID="lbstatus" runat="server"></asp:Label></strong></h6></div>
            </div>
            <div class="row">
                <div class="col-sm-2"><h6><strong>Remark</strong></h6></div>
                <div class="col-sm-1"><h6>:</h6></div>
                <div class="col-sm-9"><h6><strong><asp:Label ID="lbremark" runat="server"></asp:Label></strong></h6></div>
            </div>
            <div class="row">
                <div class="col-sm-2"><h5><strong>Claim Details</strong></h5></div>
                <div class="col-sm-1"><h6></h6></div>
                <div class="col-sm-9"></div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                    <asp:GridView ID="grdclaim" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="None" AllowPaging="false" EmptyDataText="NO DATA" CssClass="table table-striped" OnRowDataBound="grdclaim_RowDataBound" OnRowCommand="grdclaim_RowCommand" >
                        <Columns>
                            <asp:TemplateField HeaderText="Claim No">
                                <ItemTemplate>
                                    <asp:Label ID="lbclaimno" runat="server" Text='<%# Eval("claim_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CCNR No">
                                <ItemTemplate>
                                    <asp:Label ID="lbccnr" runat="server" Text='<%# Eval("ccnr_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate><%# Eval("discount_mec") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Order">
                                <ItemTemplate>
                                    <%# Eval("discount_mec").ToString() == "FG" ? String.Format("{0:n} {1}", Eval("tot_order"), " Qty") : String.Format("{0:n} {1}", Eval("tot_order"), " Qty") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Free (SAR/Qty)">
                                <ItemTemplate>
                                    <%# Eval("discount_mec").ToString() == "FG" ? String.Format("{0:n} {1}", Eval("tot_free"), " Qty") : String.Format("{0:n} {1}", Eval("tot_free"), " SAR") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Amount (SAR)">
                                <ItemTemplate>
                                    <%# Eval("discount_mec").ToString() == "FG" ? String.Format("{0:n} {1}", Eval("tot_amount"), " SAR") : String.Format("{0:n} {1}", Eval("tot_amount"), " SAR") %>
                                    <asp:HiddenField runat="server" ID="lbtotamount" Value='<%#Eval("tot_amount") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Approved (SAR)">
                                <ItemTemplate>
                                    <%# String.Format("{0:n} {1}", Eval("tot_payment"), " SAR")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product">
                                <ItemTemplate><%# Eval("product") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate><asp:Label ID="lbstatus" runat="server" Text='<%# Eval("status_nm").ToString() %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>                
                                    <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-default" CommandName="approve" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField>
                                <ItemTemplate>          
                                    <asp:Button ID="btnReturn" runat="server" Text="Return" CssClass="btn btn-warning" CommandName="return" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>          
                                    <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn btn-danger" CommandName="reject" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>          
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger" CommandName="delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>                
                                    <asp:Button ID="btnUpdate" runat="server" Text="Edit" CssClass="btn btn-danger" CommandName="editData" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div> 
            <div class="row">
                <div class="col-sm-2"><h5><strong>Documents Support</strong></h5></div>
                <div class="col-sm-1"><h6></h6></div>
                <div class="col-sm-9"></div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                    <asp:GridView ID="grddocument" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="None" AllowPaging="False" EmptyDataText="NO DATA" CssClass="table table-striped" OnRowDataBound="grddocument_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Claim No">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hddoc_cd" Value='<%#Eval("doc_cd") %>' runat="server" />
                                    <asp:Label ID="lbclaimno" runat="server" Text='<%# Eval("claim_no") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Document">
                                <ItemTemplate><%# String.Format("{0}_{1}", Eval("doc_cd"), Eval("doc_nm")) %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="File Location">
                                <ItemTemplate>
                                    <a class="example-image-link" href="<%# Eval("fileloc") %>" download data-lightbox="example-1<%# Eval("fileloc") %>">
                                    <asp:Label ID="lbfileloc" runat="server" Text='Download'></asp:Label>
                                    </a>
                                    <a class="example-image-link" href="<%# Eval("fileloc") %>" download data-lightbox="example-1<%# Eval("fileloc") %>">
                                    <asp:Label ID="lbfilelocproposal" runat="server" Text='Download'></asp:Label>
                                    </a>
                                </ItemTemplate>
                                <%--<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />--%>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:GridView ID="grdinvoice" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="None" AllowPaging="True" EmptyDataText="NO DATA" CssClass="table table-striped" OnPageIndexChanging="grdinvoice_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Invoice Document">
                                <ItemTemplate>
                                    <asp:Label ID="lbproposal" runat="server" Text='<%# Eval("inv_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Files">
                                <ItemTemplate>
                                    <a class="btn default btn-xs purple" href="<%# Eval("fileloc_o") %>" role="button" target="_blank" id="lnkPreview_o">
                                        <span class='glyphicon glyphicon-download' runat="server" visible='<%# Eval("fileloc_o").ToString() == "" ? false : true %>'></span>
                                        <asp:Label ID="lbfileloc" runat="server" Text='Order' Visible='<%# Eval("fileloc_o").ToString() == "" ? false : true %>'></asp:Label>
                                    </a>
                                        / 
                                    <a class="btn default btn-xs purple" href="<%# Eval("fileloc_f") %>" role="button" target="_blank" id="lnkPreview_f">
                                        <span class='glyphicon glyphicon-download' runat="server" visible='<%# Eval("fileloc_f").ToString() == "" ? false : true %>'></span>
                                        <asp:Label ID="Label1" runat="server" Text='Free' Visible='<%# Eval("fileloc_f").ToString() == "" ? false : true %>'></asp:Label>
                                    </a>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:TemplateField>                      
                        </Columns>
                    </asp:GridView>
                    <asp:GridView ID="grdcontract" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="None" AllowPaging="True" EmptyDataText="NO DATA" CssClass="table table-striped" OnPageIndexChanging="grdcontract_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Agreement Document">
                                <ItemTemplate>
                                    <asp:Label ID="lbproposal" runat="server" Text='<%# Eval("so_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Files">
                                <ItemTemplate>
                                    <a class="btn default btn-xs purple" href="<%# Eval("fileloc") %>" role="button" target="_blank" id="lnkPreview_o">
                                        <span class='glyphicon glyphicon-download' runat="server" visible='<%# Eval("fileloc").ToString() == "" ? false : true %>'></span>
                                        <asp:Label ID="lbfileloc" runat="server" Text='Download' Visible='<%# Eval("fileloc").ToString() == "" ? false : true %>'></asp:Label>
                                    </a>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:TemplateField>                      
                        </Columns>
                    </asp:GridView>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-2"><h5><strong>List VAT Invoice</strong></h5></div>
                <div class="col-sm-1"><h6></h6></div>
                <div class="col-sm-9"></div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <asp:GridView ID="grdvat" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" OnRowCommand="grdvat_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Inv No">
                                <ItemTemplate>
                                    <asp:Label ID="lbinvno" runat="server" Text='<%# Eval("inv_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Proposal No">
                                <ItemTemplate>
                                    <asp:Label ID="lbpropno" runat="server" Text='<%# Eval("prop_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Created Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbdate" runat="server" Text='<%# Eval("created_dt") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Amount + VAT">
                                <ItemTemplate>
                                    <%# Eval("total") %>
                                    <asp:HiddenField runat="server" ID="lbtotalamountinv" Value='<%#Eval("total") %>' />
                                    <asp:HiddenField runat="server" ID="lbamountinv" Value='<%#Eval("amount") %>' />
                                    <asp:HiddenField runat="server" ID="lbvatinv" Value='<%#Eval("vat") %>' />
                                </ItemTemplate>
                                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="File">
                                <ItemTemplate>                
                                    <asp:Button ID="btnPrint" runat="server" Text="Download" CssClass="btn btn-default" CommandName="print" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                </ItemTemplate>
                                <%--<ItemTemplate>
                                    <a class="example-image-link" href="/images/claim_doc/vat_inv/<%# Eval("fileinv") %>" download data-lightbox="example-1<%# Eval("doc_file") %>">
                                    <asp:Label ID="lbfileinv" runat="server" Text='Preview'></asp:Label>
                                    </a>
                                </ItemTemplate>--%>
                            </asp:TemplateField>                              
                            <asp:TemplateField>
                                <ItemTemplate>                
                                    <asp:Button ID="btnUpdate" runat="server" Text="Edit" CssClass="btn btn-danger" CommandName="editData" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>           
                        </Columns>
                    </asp:GridView>
                </div>
            </div>


        </div>

        <!-- Bootstrap Modal Dialog -->
        <div class="modal fade" id="myReturn" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title"><asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                         <div class="form-group">
                                            <label class="col-md-3 control-label">Claim No</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txClaim" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="issueGroup">
                                            <label class="col-md-3 control-label">Group Issue</label>
                                            <div class="col-md-9">
                                                <asp:DropDownList ID="cbgroup" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbgroup_SelectedIndexChanged" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="issueList">
                                            <label class="col-md-3 control-label">Issue List</label>
                                            <div class="col-md-9">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                <asp:DropDownList ID="cbissue" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                                </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="cbgroup" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="payment">
                                            <label class="col-md-3 control-label">Payment Type</label>
                                            <div class="col-md-9">
                                                <asp:DropDownList ID="cbPayment" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="FG">Freegood</asp:ListItem>
                                                    <asp:ListItem Value="CH">Cash</asp:ListItem>
                                                    <asp:ListItem Value="DN">Debit Note</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="totamount">
                                            <label class="col-md-3 control-label">Total Amount</label>
                                            <div class="col-md-9">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtotAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <span class="input-group-addon">
                                                        <i class="fa">SAR</i>
                                                    </span>
                                                </div> 
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="totamountapproved">
                                            <label class="col-md-3 control-label">Total Approved</label>
                                            <div class="col-md-9">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtotAppAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <span class="input-group-addon">
                                                        <i class="fa">SAR</i>
                                                    </span>
                                                </div> 
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="percentage">
                                            <label class="col-md-3 control-label">Amount</label>
                                            <div class="col-md-9">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <span class="input-group-addon">
                                                        <i class="fa">SAR</i>
                                                    </span>
                                                </div> 
                                            </div>
                                        </div>                                        
                                        <div class="form-group" runat="server" id="remarks">
                                            <label class="col-md-3 control-label">Remarks</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txremarks" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="uploadfile">
                                            <label class="col-md-3 control-label">Upload File</label>
                                            <div class="col-md-9">
                                                <asp:FileUpload ID="upl" runat="server"></asp:FileUpload>
                                                <asp:HyperLink ID="hpfile_nm" runat="server" Visible="False" ForeColor="blue" class="example-image-link" data-lightbox="example-1">
                                                <asp:Label ID="lblocfile" runat="server" CssClass="form-control" Text='Sales Document'></asp:Label></asp:HyperLink>
                                            </div>
                                        </div>   
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btSave" runat="server" Text="Return" CssClass="btn blue" OnClick="btsave_Click" />
                                <asp:HiddenField runat="server" ID="lbstatusCl" />
                                <%--<button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>--%>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        
        <!-- Modal Claim Edit Cover Letter-->
        <div class="modal fade" id="sendingClaim" role="dialog">
        <div class="modal-dialog">    
            <!-- Modal content-->
            <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">&times;</button>
                <h4 class="modal-title">Edit Cover Letter</h4>
            </div>
            <div class="modal-body">
                <div class="form-horizontal">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">   
                                <label for="dtclaim" class="col-xs-2 col-form-label col-form-label-sm">Claim Reference No</label>    
                                <div class="col-xs-8">
                                    <asp:TextBox ID="txclhno" runat="server" CssClass="form-control form-control-sm" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-xs-2">                           
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">   
                                <label for="attn" class="col-xs-2 col-form-label col-form-label-sm">To</label>    
                                <div class="col-xs-8">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                    <asp:TextBox ID="txto" runat="server" CssClass="form-control form-control-sm" ReadOnly="true"></asp:TextBox>
                                    </ContentTemplate>
                                    </asp:UpdatePanel> 
                                </div>
                                <div class="col-xs-2">                           
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">   
                                <label for="attn" class="col-xs-2 col-form-label col-form-label-sm">Attn</label>    
                                <div class="col-xs-8">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                    <asp:ListBox ID="lstAttn" runat="server" SelectionMode="Multiple">
                                    </asp:ListBox>
                                    </ContentTemplate>
                                    </asp:UpdatePanel> 
                                    <%--<asp:TextBox ID="txAttn" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>--%>
                                </div>
                                <div class="col-xs-2">                           
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">   
                                <label for="cc" class="col-xs-2 col-form-label col-form-label-sm">CC</label>    
                                <div class="col-xs-8">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                    <asp:ListBox ID="lstCC" runat="server" SelectionMode="Multiple" CssClass="form-control form-control-static">
                                    </asp:ListBox>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <%--<asp:TextBox ID="txCC" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>--%>
                                </div>
                                <div class="col-xs-2">                           
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">   
                                <label for="remarks" class="col-xs-2 col-form-label col-form-label-sm">Remarks</label>   
                                <div class="col-xs-8">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                    <asp:TextBox ID="txremarkupd" runat="server" CssClass="form-control form-control-sm" TextMode="MultiLine"></asp:TextBox>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-xs-2">                           
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnSending" runat="server" CssClass="btn btn-primary" OnClick="btSendClaim_Click" Text="Update" />
                <button type="button" id="btnClose" runat="server" class="btn btn-default" data-dismiss="modal" >Close</button>
            </div>
            </div>      
        </div>
        </div>


        <!-- Modal Claim Edit Vat Invoice -->
        <div class="modal fade" id="myEdit" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="editModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title"><asp:Label ID="lbEditTitle" runat="server" Text=""></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Invoice No</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txvatinvoice" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Proposal No</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txvatproposal" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="totamountinv">
                                            <label class="col-md-3 control-label">Total Amount</label>
                                            <div class="col-md-9">
                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                <ContentTemplate>
                                                <div class="input-group">                                                    
                                                    <asp:TextBox ID="txtotalamountinv" runat="server" CssClass="form-control" OnTextChanged="txtotalamountinv_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    <span class="input-group-addon">
                                                        <i class="fa">SAR</i>
                                                    </span>
                                                </div> 
                                                </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="totvatinv">
                                            <label class="col-md-3 control-label">Total Vat</label>
                                            <div class="col-md-9">
                                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                <ContentTemplate>
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtotalvatinv" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <span class="input-group-addon">
                                                        <i class="fa">SAR</i>
                                                    </span>
                                                </div> 
                                                </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" id="totamountvatinv">
                                            <label class="col-md-3 control-label">Total Amount+Vat</label>
                                            <div class="col-md-9">
                                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                <ContentTemplate>
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtotalamountvatinv" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <span class="input-group-addon">
                                                        <i class="fa">SAR</i>
                                                    </span>
                                                </div> 
                                                </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>  
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btsavevat" runat="server" Text="Return" CssClass="btn blue" OnClick="btsavevat_Click" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>


        <div class="row">
          <div class="col-sm-12">
            <div class="text-center">
                <button type="submit" class="btn btn-info btn-sm" runat="server" id="print" data-toggle="modal" onserverclick="print_Click" >
                  <span class="glyphicon glyphicon-briefcase" aria-hidden="true"></span> Print</button>
                <button type="submit" class="btn btn-info btn-sm" runat="server" id="claimSending" data-toggle="modal" onserverclick="update_Click" >
                  <span class="glyphicon glyphicon-briefcase" aria-hidden="true"></span> Update Attn & CC
                </button>
                <%--<button type="submit" class="btn btn-info btn-sm" runat="server" id="salesRcv" data-toggle="modal" >
                  <span class="glyphicon glyphicon-briefcase" aria-hidden="true"></span> Sales Rcv/Snd
                </button>--%>
                <%--<button type="submit" class="btn btn-info btn-sm" runat="server" id="vendorApp" onserverclick="btprint_Click" >
                  <span class="glyphicon glyphicon-ok" aria-hidden="true"></span> Vendor Approval
                </button>
                <button type="submit" class="btn btn-info btn-sm" runat="server" id="Button1" onserverclick="btprint_Click" >
                  <span class="glyphicon glyphicon-book" aria-hidden="true"></span> Claim To Vendor
                </button>--%>
            </div>
          </div>
        </div>

        </form>

        
    </div>

    

</body>
</html>
