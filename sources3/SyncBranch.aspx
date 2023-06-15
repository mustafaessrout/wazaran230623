<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="SyncBranch.aspx.cs" Inherits="SyncBranch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="css/jquery-1.9.1.js"></script>
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="../js/jquery.min.js"></script>

    <style>
        input[type=checkbox][disabled] {
            outline: 1px solid red;
        }

        .circleBase {
            border-radius: 50%;
            behavior: url(PIE.htc); /* remove if you don't care about IE8 */
        }

        .type1 {
            width: 20px;
            height: 20px;
            display: inline-block;
            background: yellow;
        }
        .type1.green{

        }
       .type1.red{

        }
       .lblHOStat{
            vertical-align: top;
            line-height: 20px;
            padding-left: 10px;
            padding-right: 10px;
       }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {
            $('#pnlmsg').hide();
           <%-- $('#' + '<%=btSearch.ClientID%>').click(function () {
                BindData();
            });--%>
        });


        function BindData() {
            var sp = $('#' + '<%=hdfBaranchID.ClientID%>').val();
            var syncID = $('#' + '<%= ddlSync.ClientID%>').val();
            var userID = $('#' + '<%=hdfUserID.ClientID%>').val();
            


            var custUrl = '';
            if (syncID == 3 || syncID == 301 || syncID == 9) {
                var period = $('#' + '<%=ddlPeriod.ClientID%>').val();
                //custUrl = 'http://172.16.3.5:8081/api/values/' + sp + '/' + syncID + '/' + userID + '/' + period;
                custUrl = 'http://localhost:3796/api/values/' + sp + '/' + syncID + '/' + userID + '/' + period;
            }
            else {
                //custUrl = 'http://172.16.3.5:8081/api/values/' + sp + '/' + syncID + '/' + userID;
                custUrl = 'http://localhost:3796/api/values/' + sp + '/' + syncID + '/' + userID;
            }

            if ($('#' + '<%=hdfHOConnected.ClientID%>').val() == 'true') {
                $('#dvsync').html('');
                $('#pnlmsg').show();
                status = '';
                
                $.ajax({

                    url: custUrl,

                    type: 'GET',

                    dataType: 'json',

                    success: function (data, textStatus, xhr) {
                        status = "<table class='table table-striped'> <tr> ";
                        status += "<th> Branch Name</th>";
                        status += "<th> Sync Name </th>";
                        status += "<th> is SyncSuccess </th>";
                        status += "<th> error </th>";
                        status += "</tr>";
                        for (i = 0; i < data.length; i++) {
                            var statusValue = '';
                            if (data[i].isSyncSuccess == true) { statusValue = 'Yes'; }
                            else { statusValue = 'No'; }
                            status += "<tr> ";
                            status += "<td>" + data[i].BranchName + "</td>";
                            status += "<td>" + data[i].SyncName + "</td>";
                            status += "<td>" + statusValue + "</td>";
                            status += "<td>" + data[i].error + "</td>";
                            status += "</tr> ";
                        }
                        status += "</table>";
                        
                        $("#pnlmsg").hide();
                        sweetAlert('Successfully', 'Sync Successfully', 'success');
                        $('#dvsync').html(status);
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        $("#pnlmsg").hide();
                        alert("Sync Failed, Check Error");
                        console.log('Error in Operation');

                    }
                });

            }
            else {
                alert('HO is not connected');
            }
        }


        function BranchToHO() {
            var sp = $('#' + '<%=hdfBaranchID.ClientID%>').val();
            var syncID = $('#' + '<%= ddlSync.ClientID%>').val();
            var userID = $('#' + '<%=hdfUserID.ClientID%>').val();



            var custUrl = '';
           <%-- if (syncID == 3) {
                var period = $('#' + '<%=ddlPeriod.ClientID%>').val();
                custUrl = 'http://localhost:8081/api/values/' + sp + '/' + syncID + '/' + userID + '/' + period;
            }
            else {--%>
                custUrl = 'http://localhost:8081/api/values/' + sp + '/201/' + userID;
            //}

            if ($('#' + '<%=hdfHOConnected.ClientID%>').val() == 'true') {
                $('#dvsync').html('');
                $('#pnlmsg').show();
                status = '';

                $.ajax({

                    url: custUrl,

                    type: 'GET',

                    dataType: 'jsonp',

                    success: function (data, textStatus, xhr) {
                        status = "<table class='table table-striped'> <tr> ";
                        status += "<th> Branch Name</th>";
                        status += "<th> Sync Name </th>";
                        status += "<th> is SyncSuccess </th>";
                        status += "<th> error </th>";
                        status += "</tr>";
                        for (i = 0; i < data.length; i++) {
                            var statusValue = '';
                            if (data[i].isSyncSuccess == true) { statusValue = 'Yes'; }
                            else { statusValue = 'No'; }
                            status += "<tr> ";
                            status += "<td>" + data[i].BranchName + "</td>";
                            status += "<td>" + data[i].SyncName + "</td>";
                            status += "<td>" + statusValue + "</td>";
                            status += "<td>" + data[i].error + "</td>";
                            status += "</tr> ";
                        }
                        status += "</table>";

                        $("#pnlmsg").hide();
                        alert("Sync Successfully");
                        $('#dvsync').html(status);
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        $("#pnlmsg").hide();
                        alert("Sync Failed, Check Error");
                        console.log('Error in Operation');

                    }
                });

            }
            else {
                alert('HO is not connected');
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div >
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblMsg" runat="server" Width="100%" Visible="false"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="divheader">
            Branch Sync
        </div>
        <div class="h-divider"></div>
        <div class="container">
            <div class="row">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                    <ContentTemplate>
                        <asp:HiddenField ID="hdfHOConnected" runat="server" />
                        <asp:HiddenField ID="hdfBaranchID" runat="server" />
                        <asp:HiddenField ID="hdfUserID" runat="server" />
                        <asp:HiddenField ID="hdfSyncID" runat="server" />

                        <div class="clearfix margin-bottom">
                            <div class="col-md-6 col-sm-12">
                                <asp:Label ID="lblBranch" runat="server" Text="Branch Name" CssClass="titik-dua col-md-3 col-sm-4 text-bold"></asp:Label>
                                <asp:Label ID="lblBranchName" runat="server" Text="" CssClass="padding-top-4 col-md-9 col-sm-8 text-bold block"></asp:Label>
                            </div>
                        </div>
                        <div class="clearfix margin-bottom">
                            <div class="col-md-6 col-sm-12">
                                <asp:Label ID="blbHOStatus" runat="server" Text="HO Status"  CssClass="titik-dua col-md-3 col-sm-4 text-bold"></asp:Label>
                                <div class="col-md-9 col-sm-8">
                                    <div id="dvHOStatusValue" class="circleBase type1" runat="server"></div>
                                    <asp:Label ID="lblHOStat" Text="Connected" CssClass="lblHOStat text-bold" runat="server" />
                                    <asp:Button ID="btnRefesh" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btSearchHO_Click"  Text="Refresh" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="h-divider"></div>

                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="clearfix margin-bottom">
                            <div class="col-md-6 col-md-offset-3 col-md-12">
                                <asp:Label ID="lblSync" runat="server" Text="Sync Name" CssClass="text-bold titik-dua col-md-3 col-sm-4"></asp:Label>
                                <div class="drop-down  col-md-9 col-sm-8">
                                    <asp:DropDownList ID="ddlSync" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSync_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6  col-md-offset-3 col-md-12">
                                 <div id="dvPeriod" runat="server" visible="false" >
                                    <asp:Label ID="Label1" runat="server" Text="Period" CssClass="text-bold titik-dua col-md-3 col-sm-4"></asp:Label>
                                    <div class="drop-down  col-md-9 col-sm-8">
                                        <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="navi margin-top margin-bottom">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btSearch" runat="server" CssClass="btn btn-success save" OnClientClick="BindData();" Text="Sync" />
                            <asp:Button ID="btSearchHDF" runat="server" Text="Button" OnClick="btSearch_Click" Style="display: none" />
                            <asp:Button ID="btShowAll" runat="server" CssClass="button2 save"  OnClientClick="BranchToHO();" Style="display: none"  Text="Sync Branch TO Ho" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="h-divider"></div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div id="dvsync" >
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="div-table-row">
                    <div class="div-table-colFull">
                        <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grdSync" CssClass="footable" AutoGenerateSelectButton="true" Width="1300" runat="server"
                                    DataKeyNames="EquipmentDetailsID" AllowPaging="true" PageSize="10" AutoGenerateColumns="false"
                                    OnRowDataBound="OnRowDataBound" OnSelectedIndexChanged="OnSelectedIndexChanged" OnPageIndexChanging="grdSync_PageIndexChanging">
                                    <%--<AlternatingRowStyle BackColor="White" ForeColor="#284775" />--%>
                                    <Columns>
                                        <asp:BoundField HeaderText="Branch Name" DataField="brnName" />
                                        <asp:TemplateField>

                                            <ItemTemplate>
                                                <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("~/Admin/fr_HOEquipmentDownload.aspx?Id={0}", HttpUtility.UrlEncode(Eval("InvoiceFileNames").ToString())) %>'
                                                    Text="View Files" Target="_blank" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Left" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

               
                <div class="divmsg loading-cont" id="pnlmsg">
                    <div>
                        <i class="fa fa-spinner spiner fa-spin fa-3x fa-fw" aria-hidden="true"></i>
                    </div>
                </div>
            </div>
        </div>

    </div>

</asp:Content>
