<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="invoicedoc.aspx.cs" Inherits="invoicedoc" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container-fluid">
        <div class="page-header">
            <h3>Invoice Scan Upload<asp:Label ID="lbstatus" runat="server" BorderStyle="Solid" BorderWidth="1px" ForeColor="Red"></asp:Label></h3> 
        </div>
    </div>
    <div class="form-horizontal">
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <label for="type" class="col-xs-2 col-form-label col-form-label-sm" >Total</label>    
                    <div class="col-xs-2">
                        <%--<asp:FileUpload ID="upl" runat="server" />
                        <asp:Label ID="lbfileloc" runat="server" Text=''></asp:Label>--%>
                        <asp:Label ID="lbtotalfile" runat="server" Text=''></asp:Label>
                    </div>
                    <div class="col-xs-8">
                        <button type="submit" class="btn btn-search btn-sm" runat="server" id="btscan" onserverclick="btscan_ServerClick" >
                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span> Scan Folder</button> 
                        <button type="submit" class="btn btn-default btn-sm" runat="server" id="btupload" onserverclick="btupload_ServerClick" >
                        <span class="glyphicon glyphicon-upload" aria-hidden="true"></span> Upload</button> 
                    </div>
                </div> 
            </div>
            <div class="row">
                <div class="form-group">
                    <i><asp:Label ID="lblnote" runat="server" Text="note: invoice not uploaded check filename, extension file (.pdf/.jpeg/.jpg), or invoice claim."></asp:Label></i>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                    <ContentTemplate>
                        <div class="table-responsive">
                            <%--<asp:GridView ID="grdinvoice" runat="server" AutoGenerateColumns="false" EmptyDataText = "No files uploaded">
                                <Columns>
                                    <asp:BoundField DataField="Text" HeaderText="File Name" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDownload" Text = "Download" CommandArgument = '<%# Eval("Value") %>' runat="server" OnClick = "DownloadFile"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID = "lnkDelete" Text = "Delete" CommandArgument = '<%# Eval("Value") %>' runat = "server" OnClick = "DeleteFile" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>--%>
                            <asp:GridView ID="grdinvoice" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Files Found" OnRowDataBound="grdinvoice_RowDataBound" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Invoice No">
                                        <ItemTemplate>
                                            <asp:Label ID="lbinvoiceno" runat="server" Text='<%# Eval("Text") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>
            </div>
        </div>

    </div>

</asp:Content>

