<%@ Page Title="" Language="C#" MasterPageFile="site.master" AutoEventWireup="true" CodeFile="fm_assetPurchase.aspx.cs" Inherits="fm_assetPurchase" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
        function SelectData(sVal) {
            $get('<%=hdAssecode.ClientID%>').value = sVal;
            $get('<%=txtAssetCode.ClientID%>').value = sVal;
            $get('<%=btlookup.ClientID%>').click();
        }
        function uploadFiles() {
            window.open("fm_UploadFiles.aspx?fileType=asset", "lookup", "height=700,width=900,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
            //document.getElementById("mstAsset1").style.visibility = "visible";
            //document.getElementById("mstAsset2").style.visibility = "visible";
            //document.getElementById("reginfo").style.visibility = "visible";
            //document.getElementById("btcancel").style.visibility = "visible";
            //document.getElementById("btsave").style.visibility = "visible";
            //document.getElementById("btprint").style.visibility = "visible";
            //document.getElementById("btprintall").style.visibility = "visible";
            //document.getElementById("btadjust").style.visibility = "visible";
            //document.getElementById("grid").style.visibility = "hidden";
        }
        function UploadFIlesData() {
            document.getElementById('<%=btnUpload.ClientID%>').click();
            <%--$('<%=lkl.ClientID%>').prop("href", "/images/account/asset/" + '<%=Session["fileName"] %>');
            $('<%=lkl.ClientID%>').prop("data-lightbox", "example-" + '<%=Session["fileName"] %>');--%>
            <%--$("a").prop("href", "/images/account/asset/" + '<%=Session["fileName"] %>');
            $("a").prop("data-lightbox", "example-" + '<%=Session["fileName"] %>');--%>
            return (false);
        }
        function addSupplier() {
            //window.open("/master/lookup_mst_Supplier.aspx?fileType=asset", "lookup", "height=700,width=900,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
            window.open("/master/trn_fm_Supplier.aspx?fileType=asset", "lookup", "height=700,width=900,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
            //document.getElementById("mstAsset1").style.visibility = "visible";
            //document.getElementById("mstAsset2").style.visibility = "visible";
            //document.getElementById("reginfo").style.visibility = "visible";
            //document.getElementById("btcancel").style.visibility = "visible";
            //document.getElementById("btsave").style.visibility = "visible";
            //document.getElementById("btprint").style.visibility = "visible";
            //document.getElementById("btprintall").style.visibility = "visible";
            //document.getElementById("btadjust").style.visibility = "visible";
            //document.getElementById("grid").style.visibility = "hidden";
        }

        $(document).ready(function () {

            $("#<%=btsearch.ClientID%>").click(function () {
                PopupCenter('lookup_mst_asset.aspx', 'xtf', '900', '500');
                $(this).removeClass('optional').addClass('selected');
            });

        });
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
  <%--  <script>
        $(document).ready(function () {
            alert("ssdsdsd")
            $("#<%=btsearch.ClientID%>").click(function () {
                PopupCenter('lookup_mst_asset.aspx', 'xtf', '900', '500');
                $(this).removeClass('optional').addClass('selected');
            });

        });
        $(document).ready(function () {

            $("#<%=btadd.ClientID%>").click(function () {
                PopupCenter('master/lookup_mst_supplier.aspx', 'xtf', '900', '500');
                $(this).removeClass('optional').addClass('selected');
            });

        });

        function openPopup(url) {
            //alert(url)
            //debugger

            //alert($(url).attr('href'));
            //alert('http://localhost:29002/' + url)
            console.log(url.length);
            $('#popUpimg').attr('src', 'http://' + window.location.host + '/' + url);
            $('#btnPopup').click();

        }
    </script>--%>
    <asp:HiddenField ID="hdAssecode" runat="server"></asp:HiddenField>

    <div class="form-horizontal" style="font-family: Calibri; font-size: small">
        <h4 class="jajarangenjang">Master Asset</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <label class="control-label col-md-1">Asset Code</label>
            <div class="col-md-2">
                <div class="input-group">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdfOldFileName" runat="server"></asp:HiddenField>
                            <asp:TextBox ID="txtAssetCode" CssClass="form-control input-group-sm" Enabled="false" runat="server"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="input-group-btn">
                        <asp:UpdatePanel ID="UpdatePanel40" runat="server">
                            <ContentTemplate>
                                <asp:LinkButton ID="btsearch" CssClass="btn btn-primary" runat="server"><span class="glyphicon glyphicon-search"></span></asp:LinkButton>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel38" runat="server">
                <ContentTemplate>
                    <div id="mstAsset1" runat="server">                        
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
                                    <asp:DropDownList ID="cbAsset_type" CssClass="form-control input-sm" Enabled="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbAsset_type_SelectedIndexChanged"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <label class="control-label col-md-1">Manufacture</label>
                        <div class="col-md-2 drop-down require">
                            <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlManufacture" CssClass="form-control input-sm" Enabled="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlManufacture_SelectedIndexChanged"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>         
        </div>
        <asp:UpdatePanel ID="UpdatePanel39" runat="server">
            <ContentTemplate>
                <div id="mstAsset2" runat="server"> 
                    <div class="form-group">
                        <label class="control-label col-md-1">Model</label>
                        <%--<div class="col-md-2">--%>
                        <div class="col-md-2 drop-down require">
                            <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlModel" CssClass="form-control input-sm" Enabled="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModel_SelectedIndexChanged"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <label class="control-label col-md-1">English Name</label>
                        <div class="col-md-2 require">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtEnglishName" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <label class="control-label col-md-1">Arabic Name</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtArabicName" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <label class="control-label col-md-1">Remarks</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtRemarks" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>  
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>       
        
        <h4 class="jajarangenjang">Asset Purchase Register Info</h4>
        <div class="h-divider"></div>
        <asp:UpdatePanel ID="UpdatePanel37" runat="server">
            <ContentTemplate>
                <div id="reginfo" runat="server">
                    <div class="form-group">
                        <label class="control-label col-md-1">Asset Purchase Register No</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtAssetPurchaseRegNo" Readonly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <label class="control-label col-md-1">Invoice No</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtInvoiceNo" Readonly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <label class="control-label col-md-1">Supplier No</label>
                        <%-- %>div class="col-md-2 drop-down"--%>
                        <div class="col-md-2">
                            <div class="input-group">
                                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                    <ContentTemplate>
                                        <%--<asp:TextBox ID="txtSupplierNo" Readonly="true" CssClass="form-control" runat="server"></asp:TextBox>--%>
                                        <asp:DropDownList ID="cbSupplierNo" Enabled="true" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbsupplier_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
<%--                                <div class="add-group-btn">
                                    <asp:UpdatePanel ID="UpdatePanel41" runat="server" class="glyphicon glyphicon-plus-sign">
                                        <ContentTemplate>
                                            <%--<asp:LinkButton ID="btadd" CssClass="btn btn-primary" runat="server"><span class="glyphicon glyphicon-plus-sign"></span></asp:LinkButton>
                                            <input type="submit" value="Add" id="btadd" onclick="addSupplier()" title="Add Supplier"/>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>--%>
                            </div>
                        </div>
                        <label class="control-label col-md-1">Salespoint</label>
                        <div class="col-md-2 drop-down">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbsalespoint" Enabled="false" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-1">Model Specification</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtModelspecification" CssClass="form-control" runat="server"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <label class="control-label col-md-1">Serial No.</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtSerialno" CssClass="form-control" runat="server"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <label class="control-label col-md-1">Asset Condition</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtAssetCondition" CssClass="form-control" runat="server"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <label class="control-label col-md-1">Insured</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                                <ContentTemplate>
                                    <asp:CheckBox ID="chkInsured" CssClass="form-control" runat="server"></asp:CheckBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>


                    <div class="form-group">
                        <label class="control-label col-md-1">Insurance Company</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtInsuranceCompany" CssClass="form-control" runat="server"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <label class="control-label col-md-1">Purchase/Acquire Date</label>
                        <div class="col-md-2 require">
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="dtPurchase" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                    <asp:CalendarExtender ID="dtPurchase_CalendarExtender" CssClass="date" runat="server" TargetControlID="dtPurchase" Format="d/M/yyyy">
                                    </asp:CalendarExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <label class="control-label col-md-1">Purchase Place</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtPurchasePlace" CssClass="form-control" runat="server"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <label class="control-label col-md-1">Purchase Amount</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtPurchaseAmount" CssClass="form-control" runat="server" OnTextChanged="AmountOrQuantityChanged" AutoPostBack="true"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>


                    <div class="form-group">
                        <label class="control-label col-md-1">Quantity</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtQuantity" CssClass="form-control" runat="server" OnTextChanged="AmountOrQuantityChanged" AutoPostBack="true"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <label class="control-label col-md-1">Total Purchase Amount Before Tax</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtTotAmountBeforeTax" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <label class="control-label col-md-1">Is Second Hand Buying (Not New)</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                <ContentTemplate>
                                    <asp:CheckBox ID="chkIsSecondHand" CssClass="form-control" runat="server"></asp:CheckBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <label class="control-label col-md-1">First Purchase Date (when it was Bought at New)</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="dtFirstPurchase" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" CssClass="date" runat="server" TargetControlID="dtFirstPurchase" Format="d/M/yyyy">
                                    </asp:CalendarExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="control-label col-md-1">First Purchase Amount (when it was Bought at New)</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtFirstPurchaseAmount" CssClass="form-control" runat="server"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <label class="control-label col-md-1">Tax Type</label>
                        <div class="col-md-2 drop-down require">
                            <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlTaxType" CssClass="form-control input-sm" Enabled="false" runat="server" AutoPostBack="true"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <label class="control-label col-md-1">Tax Amount</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtTaxAmount" CssClass="form-control" runat="server" OnTextChanged="AmountOrQuantityChanged" AutoPostBack="true"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <label class="control-label col-md-1">Total Purchase Amount After Tax</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel35" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtTotAmountAfterTax" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="control-label col-md-1">Is Depreciated</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                                <ContentTemplate>
                                    <asp:CheckBox ID="chkIsDepreciated" CssClass="form-control" runat="server"></asp:CheckBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <label class="control-label col-md-1">ECO Life Month</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtECOLifeMonth" CssClass="form-control" runat="server"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <label class="control-label col-md-1">Salvage Amount</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtSalvageAmount" CssClass="form-control" runat="server" data-validation="number"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <label class="control-label col-md-1">Upload File</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                <ContentTemplate>
                                    <input type="submit" value="Upload Files" id="btnUploadFiles" onclick="uploadFiles()" title="UploadFiles" />
                                    <asp:Label ID="lbfileloc" runat="server" Text=''></asp:Label>                                            
<%--                                <a class="example-image-link" id="<%#Eval("assetpurchaseregno") %>" onclick="openPopup('<%# Eval("imagepath") %>')" href="#" style="color: blue;">
                                        <asp:Label ID="lbfileloc" runat="server" Text=''></asp:Label>
                                    </a>--%>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="form-group">
<%--                        <label class="control-label col-md-1">Ownership Source</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel36" runat="server">
                                <ContentTemplate>                                            
                                    <asp:DropDownList ID="ddOwnershipSrc_" runat="server" CssClass="auto-style3 form-control input-sm" AutoPostBack="True">
                                        <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                                        <asp:ListItem Value="is">SBTC</asp:ListItem>
                                        <asp:ListItem Value="bs">Food Island</asp:ListItem>      
                                        <asp:ListItem Value="bs">Aloha</asp:ListItem>                    
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>--%>
                        <label class="control-label col-md-1">External Ownership Source</label>
                        <div class="col-md-2 drop-down">
                            <asp:UpdatePanel ID="UpdatePanel41" runat="server">
                                <ContentTemplate>
                                    <%--<asp:DropDownList ID="ddlOwnershipSrc" CssClass="form-control input-sm" Enabled="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOwnershipSrc_SelectedIndexChanged"></asp:DropDownList>--%>
                                    <asp:DropDownList ID="ddlOwnershipSrc" CssClass="form-control input-sm" runat="server" AutoPostBack="true"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <label class="control-label col-md-1">Remarks</label>
                        <div class="col-md-5">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtRemarks2" CssClass="form-control" runat="server"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <label class="control-label col-md-1">Depreciation Schedule</label>
                        <div class="col-md-2">
<%--                            <asp:UpdatePanel ID="UpdatePanel36" runat="server">
                                <ContentTemplate>--%>
                                    <asp:LinkButton runat="server" ID="btnDepreciationSchedule" OnClick="depreciationSchedule_Click" title="DepreciationSchedule">Print Schedule</asp:LinkButton>
<%--                                </ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:UpdatePanel ID="UpdatePanel34" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="txtValidation" CssClass="form-control" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="row navi padding-bottom padding-top margin-bottom" style="text-align: center;">
            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                <ContentTemplate>

                    <%--<asp:LinkButton ID="btnew" CssClass="btn-primary btn" runat="server" OnClick="btnew_Click">New</asp:LinkButton>--%>
                    <asp:LinkButton ID="btcancel" CssClass="btn-primary btn" runat="server" OnClick="btcancel_Click">Reset / Back to List</asp:LinkButton>
                    <asp:LinkButton ID="btsave" CssClass="btn-warning btn btn-warning" runat="server" OnClick="btsave_Click">Save</asp:LinkButton>
                    <asp:LinkButton ID="btprint" CssClass="btn-danger btn btn-dangerdivhid" runat="server" OnClick="btDelete_Click">Delete</asp:LinkButton>
                    <asp:LinkButton ID="btprintall" runat="server" CssClass="btn-info btn" OnClick="btprint_Click">Print</asp:LinkButton>
                    <asp:LinkButton ID="btadjust" runat="server" CssClass="btn-info btn" OnClick="btadjust_Click">Adjust</asp:LinkButton>
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="button2 save" OnClick="btnUpload_Click" Style="display: none" />

                    <asp:Button ID="btlookup" runat="server" Text="Button" OnClick="btlookup_Click" Style="display: none" />
                    <button type="button" class="btn btn-info btn-lg" id="btnPopup" data-toggle="modal" data-target="#myModal" style="display: none">Open Modal</button>
                </ContentTemplate>
            </asp:UpdatePanel>
            <span style="padding-left: 378px;"><span style="color: red">*</span> Mandatory field</span>
        </div>
        <div class="form-group">
            <div class="col-md-12">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div runat="server" id="grid">
                            <asp:TextBox ID="searchBox" runat="server"></asp:TextBox>
                            <asp:Button ID="searchButton" runat="server" Text="search" OnClick="searchButton_Click" />
                            <asp:Button ID="reset" runat="server" Text="reset" OnClick="resetSearchButton_Click" />
                            <br />
                            <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" CellPadding="0"
                                OnSelectedIndexChanging="grd_SelectedIndexChanging" OnPageIndexChanging="OnPageIndexChanging"
                                OnRowCancelingEdit="grd_RowCancelingEdit" OnRowEditing="grd_RowEditing" AllowPaging="True" PageSize="5">
                                <Columns>
                                    <asp:TemplateField HeaderText="Asset Purchase Register Number">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAssetPurchaseRegisterno" runat="server" Text='<%#Eval("assetPurchaseRegNo") %>'></asp:Label>
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
                                            <a class="example-image-link" id="<%#Eval("assetpurchaseregno") %>" onclick="openPopup('<%# Eval("imagepath") %>')" href="#" style="color: blue;">
                                                <asp:Label ID="lbfileloc2" runat="server" Text='Document'></asp:Label>
                                            </a>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inactive">
                                        <ItemTemplate>
                                            <%--<asp:Checkbox ID="lblAssetTypes" runat="server" Enabled="false" Text='<%#Eval("isDelete") %>'></asp:Checkbox>--%>
                                            <asp:CheckBox ID="Checkbox1" runat="server" Enabled="false" Checked='<%#Eval("isDelete2")%>'></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowSelectButton="True" />
                                </Columns>
                                <HeaderStyle CssClass="table-header" />
                                <PagerSettings PageButtonCount="5" />
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbsalespoint" EventName="SelectedIndexChanged" />
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

