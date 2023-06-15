<%@ Page Title="" Language="C#" MasterPageFile="site.master" AutoEventWireup="true" CodeFile="fm_mst_asset.aspx.cs" Inherits="fm_mst_asset" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
 <%--       function SelectData(sVal) {
            $get('<%=hdAssecode.ClientID%>').value = sVal;
        }--%>
        function SelectData(sVal) {
            $get('<%=hdAssecode.ClientID%>').value = sVal;
            $get('<%=txtAssetCode.ClientID%>').value = sVal;
        }
        function uploadFiles() {
            window.open("fm_UploadFiles.aspx?fileType=asset", "lookup", "height=700,width=900,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }
        function UploadFIlesData() {
            document.getElementById('<%=btnUpload.ClientID%>').click();
            <%--$('<%=lkl.ClientID%>').prop("href", "/images/account/asset/" + '<%=Session["fileName"] %>');
            $('<%=lkl.ClientID%>').prop("data-lightbox", "example-" + '<%=Session["fileName"] %>');--%>
            <%--$("a").prop("href", "/images/account/asset/" + '<%=Session["fileName"] %>');
            $("a").prop("data-lightbox", "example-" + '<%=Session["fileName"] %>');--%>
            return (false);
        }
    </script>
    <script>
        function openPopup(url) {
            //alert(url)
            //debugger

            //alert($(url).attr('href'));
            //alert('http://localhost:29002/' + url)
            console.log(url.length);
            $('#popUpimg').attr('src', 'http://' + window.location.host + '/' + url);
            $('#btnPopup').click();

        }
    </script>
    <style>
        .form-horizontal.required .form-control {
            content: "*";
            color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdAssecode" runat="server"></asp:HiddenField>

    <div class="form-horizontal" style="font-family: Calibri; font-size: small">
        <h4 class="jajarangenjang">Master Asset</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <label class="control-label col-md-1">Asset Code</label>
            <div class="col-md-2 require">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdfOldFileName" runat="server"></asp:HiddenField>
                        <asp:TextBox ID="txtAssetCode"  CssClass="form-control input-sm" Enabled="false" runat="server"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">Asset Group</label>
            <div class="col-md-2 drop-down require">
                <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlAssetGroup" CssClass="form-control input-sm" Enabled="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAssetGroup_SelectedIndexChanged"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">Asset Type</label>
            <div class="col-md-2 drop-down require">
                <asp:UpdatePanel ID="UpdatePane21" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbAsset_type" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbAsset_type_SelectedIndexChanged"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <label class="control-label col-md-1">Manufacture</label>
            <div class="col-md-2 drop-down require">
                <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlManufacture" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlManufacture_SelectedIndexChanged"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="form-group">
            <label class="control-label col-md-1">Model</label>
            <%--<div class="col-md-2">--%>
            <div class="col-md-2 drop-down require">
                <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlModel" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModel_SelectedIndexChanged"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">English Name</label>
            <div class="col-md-2 require">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtEnglishName" CssClass="form-control" runat="server"></asp:TextBox>                        
<%--                        <asp:RegularExpressionValidator runat="server" display="Dynamic"
                        controltovalidate="txtEnglishName" 
                        errormessage="Name should be numeric characters." 
                        validationexpression="^[0-9]+$" />--%>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">Arabic Name</label>
            <div class="col-md-2">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtArabicName" CssClass="form-control" runat="server"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>        
            <label class="control-label col-md-1">Upload File</label>
            <div class="col-md-2">
                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                    <ContentTemplate>
                        <input type="submit" value="Upload Files" id="btnUploadFiles" onclick="uploadFiles()" title="UploadFiles" />
                        <asp:Label ID="lbfileloc" runat="server" Text=''></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="form-group">
            <label class="control-label col-md-1">Remarks</label>
            <div class="col-md-2">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtRemarks" CssClass="form-control" runat="server"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="row navi padding-bottom padding-top margin-bottom" style="text-align: center;">
            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                <ContentTemplate>

                    <asp:LinkButton ID="btnew" CssClass="btn-primary btn" runat="server" OnClick="btnew_Click">New</asp:LinkButton>
                    <asp:LinkButton ID="btsave" CssClass="btn-warning btn btn-warning" runat="server" OnClick="btsave_Click">Save</asp:LinkButton>
                    <asp:LinkButton ID="btprint" CssClass="btn-danger btn btn-dangerdivhid" runat="server" OnClick="btDelete_Click">Delete</asp:LinkButton>
                    <asp:LinkButton ID="btprintall" runat="server" CssClass="btn-info btn" OnClick="btprint_Click">Print</asp:LinkButton>
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="button2 save" OnClick="btnUpload_Click" Style="display: none" />

                    <button type="button" class="btn btn-info btn-lg" id="btnPopup" data-toggle="modal" data-target="#myModal" style="display: none">Open Modal</button>
                </ContentTemplate>
            </asp:UpdatePanel>
            <span style="padding-left: 378px;"><span style="color: red">*</span> Mandotry field</span>
        </div>
        <div class="form-group">
            <div class="col-md-12">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" CellPadding="0"
                            OnSelectedIndexChanging="grd_SelectedIndexChanging"
                            OnRowCancelingEdit="grd_RowCancelingEdit" OnRowEditing="grd_RowEditing">
                            <Columns>
                                <asp:TemplateField HeaderText="Asset Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAssetno" runat="server" Text='<%#Eval("assetno") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Asset Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAssetNM" runat="server" Text='<%#Eval("asset_nm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Asset Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAssetTypes" runat="server" Text='<%#Eval("assetTypes") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File location">
                                    <ItemTemplate>
                                         <a class="example-image-link" id="<%#Eval("assetno") %>" onclick="openPopup('<%# Eval("imagepath") %>')" href="#" style="color: blue;">
                                            <asp:Label ID="lbfileloc2" runat="server" Text='Document'></asp:Label>
                                        </a>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inactive">
                                    <ItemTemplate>
                                        <%--<asp:Checkbox ID="lblAssetTypes" runat="server" Enabled="false" Text='<%#Eval("isDelete") %>'></asp:Checkbox>--%>
                                        <asp:Checkbox ID="Checkbox1" runat="server" Enabled="false" Checked='<%#Eval("isDelete")%>'></asp:Checkbox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowSelectButton="True" />
                            </Columns>
                            <HeaderStyle CssClass="table-header" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <%--<asp:AsyncPostBackTrigger ControlID="cbsalespoint" EventName="SelectedIndexChanged" />--%>
                        <asp:AsyncPostBackTrigger ControlID="cbAsset_type" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>
    <div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Attachment</h4>
                </div>
                <div class="modal-body">
                    <img id="popUpimg" src="" width="200" height="200" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>
</asp:Content>

