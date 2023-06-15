<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_salesplan_ho.aspx.cs" Inherits="fm_salesplan_ho" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="css/anekabutton.css" />
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }

<%--        $(document).ready(function () {
            $("#<%=btsearch.ClientID%>").click(function () {
                PopupCenter('fm_lookup_cmo_ho.aspx', 'xtf', '900', '500');

            });
        });--%>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="hdspno" runat="server" />
    </ContentTemplate>
    </asp:UpdatePanel>

    <div class="container">
        <h4 class="jajarangenjang">Monthly Sales Plan</h4>
        <div class="h-divider"></div>

        <div class="row">
            <div class="col-sm-6 ">
                <div class="form-group">
                <label class="control-label col-sm-2">For Period</label>
                <div class="col-sm-10">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="drop-down">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbMonthCD" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="cbMonthCD_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label ID="lbspno" CssClass="form-control input-group-sm ro" runat="server" Text="" Visible="false"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>                         
                </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="form-group">
                    <label class="control-label col-sm-2">Created Date#</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="dtcreated" CssClass="form-control input-sm" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                        <asp:CalendarExtender ID="dtcreated_CalendarExtender" Format="d/M/yyyy" runat="server" TargetControlID="dtcreated">
                        </asp:CalendarExtender>
                    </div>                    
                </div>
            </div>
        </div>

        <h5 class="jajarangenjang">Item Details</h5>
        <div class="h-divider"></div>

        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <asp:RadioButtonList ID="rdInsert" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdInsert_SelectedIndexChanged" CssClass="form-control" RepeatDirection="Horizontal"  Width="100%" >
                        <asp:ListItem Value="M">Created Manually</asp:ListItem>
                        <asp:ListItem Value="I">Imported from Excel</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
        <div class="row" runat="server" id="showImport">
            <div class="col-sm-12">
                <div class="form-group">
                    <label class="col-md-2 col-sm-4 control-label">Upload Excel File..</label>
                    <div class="col-md-6 col-sm-6">
                        <asp:FileUpload id="FileUploadControl" runat="server" />
                        <asp:HyperLink id="hpfile_nm" runat="server" Visible="False" ForeColor="blue" class="example-image-link" data-lightbox="example-1">
                            <asp:Label id="lblocfile" runat="server" Text='Excel Document'></asp:Label>
                        </asp:HyperLink>
                    </div>
                    <div class="col-md-2 col-sm-4">
                        <asp:Button ID="btupload" runat="server" Text="Upload" CssClass="btn btn-default" OnClick="btupload_Click" />
                    </div>
                </div>
            </div>
        </div>
        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rdInsert" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
        <div class="row" runat="server" id="showManual">
            <div class="form-group">
                <div class="col-sm-12">
                    <table class="mGrid">
                        <tr>
                            <th>Product</th>
                            <th>Item Code</th>
                            <th>Branch</th>
                            <th>Qty</th>
                            <th></th>
                        </tr>
                        <tr>
                            <td class="drop-down">
                                <asp:UpdatePanel ID="UpdatePanel18" runat="server" class="drop-down">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cbProduct" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cbProduct_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="drop-down">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cbItem" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="cbProduct" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td class="drop-down">
                                <asp:DropDownList ID="cbbranch" CssClass="form-control" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txqty" runat="server" CssClass="form-control"></asp:TextBox>                         
                            </td>
                            <td>
                                <asp:Button ID="btadd" CssClass="btn btn-primary" runat="server" OnClick="btadd_Click" Text="Add" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="row" runat="server" id="showManualList">
            <div class="form-group">
                <div class="col-sm-12">
                    <asp:GridView ID="grditem" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnRowCreated="grditem_RowCreated" OnRowDeleting="grditem_RowDeleting" OnRowCancelingEdit="grditem_RowCancelingEdit" OnRowEditing="grditem_RowEditing" OnRowUpdating="grditem_RowUpdating" ShowFooter="True" CellPadding="0">
                        <Columns>
                            <asp:TemplateField HeaderText="Branch">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdsalespoint" runat="server" Value='<%#Eval("salespoint_cd") %>' />
                                    <asp:Label ID="lbsalespoint" runat="server" Text='<%#Eval("salespoint_nm") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Region">
                                <ItemTemplate>
                                    <%#Eval("zone_cd") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hditem" runat="server" Value='<%#Eval("item_cd") %>' />
                                    <asp:Label ID="lbitem" runat="server" Text='<%#Eval("item") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty By CTN">
                                <ItemTemplate>
                                    <asp:Label ID="lbqty" runat="server" Text='<%#Eval("qty") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txqty" Text='<%#Eval("qty") %>' runat="server"></asp:TextBox>
                                </EditItemTemplate>                            
                                <FooterTemplate>
                                    <asp:Label ID="lbtotqty" runat="server" Text=""></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="Change Qty" ShowEditButton="True" />
                            <asp:CommandField ShowDeleteButton="True" />                        
                        </Columns>
                        <FooterStyle BackColor="Yellow" />
                    </asp:GridView>
                </div>
            </div>
        </div>
        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rdInsert" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>

    </div>

    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
    <ContentTemplate>
    <div class="row margin-bottom">
        <div class="col-sm-12">
            <div class="text-center navi">
                <asp:Button id="btnew" runat="server" Text="New" class="btn btn-warning btn-sm" OnClick="btnew_Click" />
                <asp:Button id="btsave" runat="server" Text="Save" class="btn btn-success btn-sm" OnClick="btsave_Click" />
                <asp:Button id="btedit" runat="server" Text="Edit" class="btn btn-primary btn-sm" OnClick="btedit_Click" />
                <asp:Button id="btprint" runat="server" Text="Print" class="btn btn-info btn-sm" OnClick="btprint_Click" />
            </div>
        </div>
    </div>  
    </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cbMonthCD" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>

    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>

</asp:Content>

