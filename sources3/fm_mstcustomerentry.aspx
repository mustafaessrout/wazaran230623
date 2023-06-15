<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstcustomerentry.aspx.cs" Inherits="fm_mstcustomerentry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="admin/css/bootstrap.min.css" rel="stylesheet" />
    <script src="admin/js/bootstrap.min.js"></script>--%>
    <style>
        span.checkbox label {
            padding-top: 2px !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divheader">
        Customer Entry -
        <asp:Label ID="lbsp" runat="server" CssClass="text-red text-bold"></asp:Label>
    </div>
    <div class=" h-divider"></div>

    <div class="container-fluid">
        <div class="row ">
            <div class="clearfix">
                <div class="clearfix col-md-4 col-sm-6 margin-bottom no-padding">
                    <label class="control-label titik-dua col-sm-4">Salespoint</label>
                    <div class="col-sm-8 drop-down">
                        <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="clearfix col-md-4 col-sm-6 margin-bottom no-padding">
                    <label class="control-label titik-dua col-sm-4">Sales Block</label>
                    <div class="col-sm-8">
                        <asp:CheckBox ID="chsales" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="clearfix col-md-4 col-sm-6 margin-bottom no-padding">
                    <label class="control-label titik-dua col-sm-4">RPS</label>
                    <div class="col-sm-8 drop-down">
                        <asp:DropDownList ID="cbrps" runat="server" CssClass="form-control"></asp:DropDownList>

                    </div>
                </div>
            </div>
            <div class="clearfix">
                <div class="clearfix col-md-4 col-sm-6 margin-bottom no-padding">
                    <label class="control-label titik-dua col-sm-4">Customer Code</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txcustcode" runat="server" CssClass=" form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="clearfix col-md-4 col-sm-6 margin-bottom no-padding">
                    <label class="control-label titik-dua col-sm-4">Category</label>
                    <div class="col-sm-8 drop-down">
                        <asp:DropDownList ID="cbcategory" runat="server" OnSelectedIndexChanged="cbcategory_SelectedIndexChanged" CssClass="form-control" AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="clearfix col-md-4 col-sm-6 margin-bottom no-padding">
                    <label class="control-label titik-dua col-sm-4">Category</label>
                    <div class="col-sm-8">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbcate" runat="server" CssClass="text-bold text-red block padding-top-4"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cbcategory" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="clearfix">
                <div class="clearfix col-md-4 col-sm-6 margin-bottom no-padding">
                    <label class="control-label titik-dua col-sm-4">Credit Limit</label>
                    <div class="col-sm-8">
                        <asp:UpdatePanel ID="updpnl2" runat="server" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:Panel runat="server" ID="txclPnl">
                                    <asp:TextBox ID="txcl" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                    Min<asp:Label ID="lblmincredit" runat="server"></asp:Label>
                                    Max<asp:Label ID="lblmaxcredit" runat="server"></asp:Label>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="clearfix col-md-4 col-sm-6 margin-bottom no-padding">
                    <label class="control-label titik-dua col-sm-4">TOP</label>
                    <div class="col-sm-8 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbtermpayment" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <div class="h-divider"></div>

            <div class="clearfix">
                <div class="clearfix col-sm-6 margin-bottom no-padding">
                    <label class="control-label titik-dua col-sm-2">Name</label>
                    <div class="col-md-10">
                        <asp:Panel runat="server" ID="txcusnamePnl">
                            <asp:TextBox ID="txcusname" runat="server" CssClass="form-control makeitupper" OnTextChanged="txcusname_TextChanged"></asp:TextBox>
                        </asp:Panel>
                    </div>
                </div>
                
                <div class="clearfix col-sm-6 margin-bottom no-padding">
                    <label class="control-label titik-dua col-sm-2">Short Name</label>
                    <div class="col-sm-10">
                        <asp:Panel runat="server" ID="txshortnamePnl">
                            <asp:TextBox ID="txshortname" runat="server" CssClass="form-control makeitupper" OnTextChanged="txshortname_TextChanged"></asp:TextBox>
                        </asp:Panel>
                    </div>
                </div>
                <div class="clearfix col-sm-6 margin-bottom no-padding">
                    <label class="control-label titik-dua col-sm-2">Arabic</label>
                    <div class="col-sm-10">
                        <asp:Panel runat="server" ID="txarabicPnl">
                            <asp:TextBox ID="txarabic" runat="server" CssClass="form-control"></asp:TextBox>
                        </asp:Panel>
                    </div>
                </div>
                <div class="clearfix col-sm-6 margin-bottom no-padding">
                    <label class="control-label titik-dua col-sm-2">Tax ID</label>
                    <div class="col-sm-10">
                        <asp:Panel runat="server" ID="Panel3">
                            <asp:TextBox ID="txvat" runat="server" CssClass="form-control"></asp:TextBox>
                        </asp:Panel>
                    </div>
                </div>
                <div class="clearfix col-sm-6 margin-bottom no-padding">
                    <label class="control-label titik-dua col-sm-2">CR No</label>
                    <div class="col-sm-10">
                        <asp:Panel runat="server" ID="Panel4">
                            <asp:TextBox ID="txcrno" runat="server" CssClass="form-control"></asp:TextBox>
                        </asp:Panel>
                    </div>
                </div>

                <div class="clearfix col-sm-6 margin-bottom no-padding">
                    <label class="control-label titik-dua col-sm-2">VAT Customer Name</label>
                    <div class="col-sm-10">
                        <asp:Panel runat="server" ID="Panel1">
                            <asp:TextBox ID="txvatname" runat="server" CssClass="form-control"></asp:TextBox>
                        </asp:Panel>
                    </div>
                </div>
                <div class="clearfix col-sm-6 margin-bottom no-padding">
                    <label class="control-label titik-dua col-sm-2">VAT Customer ARABIC</label>
                    <div class="col-sm-10">
                        <asp:Panel runat="server" ID="Panel2">
                            <asp:Panel runat="server" ID="txvatarabicPnl">
                                <asp:TextBox ID="txvatarabic" runat="server" CssClass="form-control"></asp:TextBox>
                            </asp:Panel>
                        </asp:Panel>
                    </div>
                </div>

            </div>

            <div class="h-divider"></div>

            <div class="clearfix">
                <div class="clearfix col-sm-6 margin-bottom no-padding">
                    <label class="control-label titik-dua col-sm-2">Street name</label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txaddress" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="clearfix col-sm-6 margin-bottom no-padding">
                    <label class="control-label titik-dua col-sm-2">Building Name/No., floor, Apt.</label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txaddress2" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="clearfix col-sm-6 margin-bottom no-padding">
                    <label class="control-label titik-dua col-sm-2">City</label>
                    <div class="col-sm-10 drop-down">
                        <asp:DropDownList ID="cblocation" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cblocation_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="clearfix col-sm-6 margin-bottom no-padding">
                    <label class="control-label titik-dua col-sm-2">District</label>
                    <div class="col-sm-10 drop-down">
                        <asp:DropDownList ID="cbdistrict" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="clearfix col-sm-6 margin-bottom no-padding">
                    <label class="control-label titik-dua col-sm-2">Zipcode</label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txzipcode" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="h-divider"></div>

            <div class="clearfix">
                <div class="clearfix col-sm-6 margin-bottom no-padding">
                    <label class="control-label titik-dua col-sm-2">Channel</label>
                    <div class="col-sm-10 drop-down">

                        <asp:Panel runat="server" ID="cbotlcdPnl">
                            <asp:DropDownList ID="cbotlcd" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </asp:Panel>
                    </div>
                </div>
                <div class="clearfix col-sm-6 margin-bottom no-padding">
                    <label class="control-label titik-dua col-sm-2">Group</label>
                    <div class="col-sm-10 drop-down">
                        <asp:Panel runat="server" ID="cbcusgroupPnl">
                            <asp:DropDownList ID="cbcusgroup" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </asp:Panel>
                    </div>
                </div>
                
            </div>


            <div class="h-divider"></div>

            <div class="clearfix">
                <div class="clearfix col-sm-6 margin-bottom no-padding">
                    <label class="control-label titik-dua col-sm-2">Maximum Inv</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txmaxinvoice" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="clearfix col-sm-6 margin-bottom no-padding">
                    <label class="control-label titik-dua col-sm-2">Checked</label>
                    <div class="col-sm-10">
                        <asp:CheckBox ID="chmerchand" runat="server" CssClass="form-control checkbox no-margin" Text="No Merchandizer" OnCheckedChanged="chmerchand_CheckedChanged" AutoPostBack="true" />
                    </div>
                </div>
                <div class="clearfix col-sm-6 margin-bottom no-padding">
                    <label class="control-label titik-dua col-sm-2">Merchandiser</label>
                    <div class="col-md-10">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbmerchandiser" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="chmerchand" EventName="CheckedChanged" />
                                <asp:AsyncPostBackTrigger ControlID="cbsalespoint" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="clearfix col-sm-6 margin-bottom no-padding">
                    <label class="control-label titik-dua col-sm-2">Salesman</label>
                    <div class="col-md-10 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbsalesman" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <div class="h-divider"></div>

            <div class="clearfix">
                <div class="col-sm-6 margin-bottom no-padding">
                    <div class="clearfix margin-bottom">
                        <label class="control-label titik-dua col-sm-2">Latitude</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txlatitude" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix margin-bottom">
                        <label class="control-label titik-dua col-sm-2">Longitude</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txlongitude" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix margin-bottom">
                        <label class="control-label titik-dua col-sm-2">Phone</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txphoneno" runat="server" type="tel" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix margin-bottom">
                        <label class="control-label titik-dua col-sm-2">Fax</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txfaxno" runat="server" type="tel" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix margin-bottom">
                        <label class="control-label titik-dua col-sm-2">Contact 1</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txcontactname" runat="server" type="tel" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix margin-bottom">
                        <label class="control-label titik-dua col-sm-2">Contact 2</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txcontactname2" runat="server" type="tel" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix margin-bottom">
                        <label class="control-label titik-dua col-sm-2">Bank Acct</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txbankacc" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                    </div>
                </div>
                <div class="col-sm-6 margin-bottom no-padding">
                    <div class="clearfix margin-bottom">
                        <label class="control-label titik-dua col-sm-2">Mobile 1</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txmobile" runat="server" type="tel" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix margin-bottom">
                        <label class="control-label titik-dua col-sm-2">Mobile 2</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txmobile2" runat="server" type="tel" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix margin-bottom">
                        <label class="control-label titik-dua col-sm-2">Email 1</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txcontactmail" runat="server" type="email" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix margin-bottom">
                        <label class="control-label titik-dua col-sm-2">Email 2</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txcontactmail2" runat="server" type="email" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix margin-bottom">
                        <label class="control-label titik-dua col-sm-2">National ID</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txidentity" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix margin-bottom">
                        <label class="control-label titik-dua col-sm-2">Passport No</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txidentity2" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                </div>

            </div>

            <div class="h-divider"></div>

            <div class="clearfix margin-bottom">
                <div class="col-md-12">

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <asp:GridView ID="grddoc" CssClass="table table-striped mygrid" runat="server" AutoGenerateColumns="False" CellPadding="4" GridLines="None">
                                <AlternatingRowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Document Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lbdoc_cd" runat="server" Text='<%# Eval("doc_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbdoc_nm" runat="server" Text='<%# Eval("doc_nm") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Expire Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbexp_dt" runat="server" Text='<%# Eval("exp_dt", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="File location">
                                        <ItemTemplate>
                                            <a class="example-image-link" href="/images/customer/<%# Eval("fileloc") %>" data-lightbox="example-1<%# Eval("fileloc") %>">
                                                <asp:Label ID="lbfileloc" runat="server" Text='Picture'></asp:Label>
                                            </a>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle CssClass="table-edit" />
                                <FooterStyle CssClass="table-footer" />
                                <HeaderStyle CssClass="table-header" />
                                <PagerStyle CssClass="table-page" />
                                <RowStyle />
                                <SelectedRowStyle CssClass="table-edit" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                            <br />

                            <asp:GridView ID="grdcate" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanging="grdcate_SelectedIndexChanging" OnSelectedIndexChanged="grdcate_SelectedIndexChanged" OnPageIndexChanging="grdcate_PageIndexChanging" GridLines="None" PageSize="5" OnRowDataBound="grdcate_RowDataBound" CellPadding="0" CssClass="table table-striped mygrid">
                                <AlternatingRowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Document Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lbdoccode" runat="server" Text='<%# Eval("doc_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbdocname" runat="server" Text='<%# Eval("doc_nm") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Upload">
                                        <ItemTemplate>
                                            <asp:FileUpload ID="upl" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Expiration">
                                        <ItemTemplate>
                                            <asp:CalendarExtender CssClass="date" ID="CalendarExtender1" runat="server" TargetControlID="dtexp" Format="d/M/yyyy"></asp:CalendarExtender>
                                            <asp:TextBox ID="dtexp" runat="server" TextMode="SingleLine"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="serial No.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txserialno" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <EditRowStyle CssClass="table-edit" />
                                <FooterStyle CssClass="table-footer" />
                                <HeaderStyle CssClass="table-header" />
                                <PagerStyle CssClass="table-page" />
                                <RowStyle />
                                <SelectedRowStyle CssClass="table-edit" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>

                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbcategory" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <!--END DIV -->
        </div>
    </div>


    <div class="navi margin-bottom">

        <asp:Button ID="btnew" runat="server" CssClass="btn btn-success btn-new" OnClick="btnew_Click" Text="New" Visible="False" />
        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn btn-warning btn-save" OnClick="btsave_Click" />

        <asp:Button ID="btupdate" runat="server" Text="Update" CssClass="btn btn-warning btn-edit" OnClick="btedit_Click" />

        <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn btn-info btn-print" OnClick="btprint_Click" />

    </div>
</asp:Content>

