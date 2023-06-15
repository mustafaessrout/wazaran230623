<%@ Page Language="C#" AutoEventWireup="true" CodeFile="trn_fm_ItemCashOut.aspx.cs" Inherits="trn_fm_ItemCashOut" MasterPageFile="~/master/homaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <link href="../css/anekabutton.css" rel="stylesheet" />
    <%-- <link href="Content/beatifullcontrol.css" rel="stylesheet" />--%>
    <script>
        
    </script>
    <style>
        .form-horizontal.required .form-control {
            content: "*";
            color: red;
        }

        .mygrid thead th, .mygrid tbody th {
            background-color: #5D7B9D !important;
        }

        .AutoExtender {
            font-family: Verdana, Helvetica, sans-serif;
            font-size: .8em;
            font-weight: normal;
            border: solid 1px #006699;
            line-height: 20px;
            padding: 10px;
            background-color: White;
            margin-left: 10px;
        }

        .AutoExtenderList {
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: Maroon;
            width: 250px;
            padding: 0px;
        }

        .AutoExtenderHighlight {
            color: White;
            background-color: #006699;
            cursor: pointer;
            width: 250px;
        }

        #divwidth {
            width: 250px !important;
        }

            #divwidth div {
                width: 250px !important;
            }

        .divmsg {
            /*position:static;*/
            top: 30%;
            right: 50%;
            left: 50%;
            width: 200px;
            height: 200px;
            position: fixed;
            /*background-color:greenyellow;*/
            overflow-y: auto;
        }

        .divhid {
            display: none;
        }

        .divnormal {
            display: normal;
        }

        .circleBase {
            border-radius: 50%;
            behavior: url(PIE.htc); /* remove if you don't care about IE8 */
        }

        .type1 {
            width: 20px;
            height: 20px;
            display: inline-block;
            /*background: yellow;*/
        }

            .type1.green {
            }

            .type1.red {
            }

        .lblHOStat {
            vertical-align: top;
            line-height: 20px;
            padding-left: 10px;
            padding-right: 10px;
        }
    </style>
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        $(document).ready(function () {
            //HideProgress();
            <%--$("#<%=btsearch.ClientID%>").click(function () {
                PopupCenter('lookup_mst_ItemCashOut.aspx', 'xtf', '900', '500');
                $(this).removeClass('optional').addClass('selected');
            });--%>

        });
        function SelectData(sVal) {
            $get('<%=hdfItemCashOut_cdValue.ClientID%>').value = sVal;
            $get('<%=txtCashOut.ClientID%>').value = sVal;
            $get('<%=btlookup.ClientID%>').click();
        }
        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" runat="Server">
    <div class="form-horizontal" style="font-family: Calibri; font-size: small">
        <h4 class="jajarangenjang">Master Item Cash OUT/IN</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <label class="control-label col-md-1">CashOut Code</label>
            <div class="col-md-2">
                <div class="input-group">
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <ContentTemplate>
                            
                            <asp:HiddenField ID="hdfOldFileName" runat="server"></asp:HiddenField>
                            <asp:TextBox ID="txtCashOut" CssClass="form-control input-group-sm" Enabled="false" runat="server"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

            </div>


            <label class="control-label col-md-1">Item Type</label>
            <div class="col-md-2  ">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdfItemCashOut_cdValue" runat="server"></asp:HiddenField>
                        <asp:DropDownList ID="cbitemtype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbitemtype_SelectedIndexChanged" CssClass="form-control">
                    </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">Routine</label>
            <div class="col-md-2  ">
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbroutine" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbroutine_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">In/Out</label>
            <div class="col-md-2  ">
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbinout" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbinout_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>



        <div class="form-group">
            <label class="control-label col-md-1">Item Name</label>
            <div class="col-md-8  ">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txitemname" runat="server" CssClass="form-control"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            
        </div>
        <div class="form-group">
            <div class="col-md-12">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="table-page-fixer">
                            <div class="overflow-y relative" style="max-height: 250px;">
                                <asp:GridView ID="grd"
                                    runat="server" CssClass="table table-striped table-page-fix table-hover table-fix mygrid" data-table-page="#ss"
                                    AutoGenerateColumns="False" AllowPaging="True"
                                    OnPageIndexChanging="grd_PageIndexChanging" OnRowDeleting="grd_RowDeleting" OnSelectedIndexChanging="grd_SelectedIndexChanging" CellPadding="0" GridLines="None" PageSize="5">
                                    <AlternatingRowStyle />
                                    <Columns>

                                        <asp:TemplateField HeaderText="ItemCashOut S#">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdfItemCashOut_cd" runat="server" Value='<%#Eval("itemco_cd") %>' />
                                                <asp:Label ID="lblItemCashOut_cd" runat="server" Text='<%#Eval("itemco_cd")  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemCashOutName" runat="server" Text='<%#Eval("Itemco_nm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CashOut Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCashoutType" runat="server" Text='<%#Eval("cashoutType") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="InOut">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInOutValue" runat="server" Text='<%#Eval("inOutValue") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Routine">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRoutineValue" runat="server" Text='<%#Eval("routineValue") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                        <asp:CommandField  ShowSelectButton="true" ShowDeleteButton="true" />
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
                            </div>
                        </div>
                        <div class="table-copy-page-fix" id="ss"></div>
                    </ContentTemplate>
                    <Triggers>
                     </Triggers>

                </asp:UpdatePanel>

            </div>
        </div>
        
        <div class="row">
            <div class="form-group">
                <div class="navi margin-bottom padding-bottom margin-top">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate><%--<i class="fa fa-save">&nbsp;Save</i>--%>
                            <asp:LinkButton ID="btnew" runat="server" CssClass="btn btn-primary" OnClick="btnew_Click"><i class="fa fa-plus">&nbsp;New</i></asp:LinkButton>
                            <asp:LinkButton ID="btsave" runat="server" style="font-size: 18px;" CssClass="btn btn-danger fa fa-save" Text="Save" OnClick="btsave_Click"></asp:LinkButton>
                            <asp:LinkButton ID="btprint" runat="server" CssClass="btn btn-warning" OnClick="btprint_Click"><i class="fa fa-print">&nbsp;Print</i></asp:LinkButton>
                            <asp:Button ID="btlookup" runat="server" Text="Button" OnClick="btlookup_Click" Style="display: none" />
                            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
       <%--<div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>--%>
</asp:Content>
