<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_salestargetsalespoint.aspx.cs" Inherits="fm_salestargetsalespoint" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="css/jquery-1.9.1.js"></script>
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="css/jquery-ui.js"></script>
    <script src="../js/jquery.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
   
    <script src="admin/js/bootstrap.js"></script>

    <script src="admin/js/bootstrap.min.js"></script>
   
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery-3.2.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>

    <style>
        .alert.alert-dismissible{
            position:absolute !important;
            top:0;
            left:0;
            right:0;
            animation-duration: 1s;
            animation-fill-mode: both;
            animation-name: fadeInUp;
        }
        .alert.alert-warning i{
            padding-right:10px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
   
        .alert-top{
            margin-left: -15px ;
            margin-right: -15px ;
            margin-top: -5px ;
            margin-bottom: 15px ;
        }
        .alert-popup{
            position: relative;
            line-height:normal;
            padding: 15px 15px 10px 55px;
        }
        .alert-popup > i:first-child{
            position: absolute;
            left: 0;
            top: 0;
            height: 100%;
            font-size: 30px;
            padding: 8px 10px;
            background: #fff;
            border-radius: 5px 0 0 5px;
        }
        .alert-popup .close {
            color: #333;
            position: absolute;
            top: 5px;
            right: 10px;
        }
        .alert-popup .close:focus{
            outline: none;
        }
    </style>
    <script>
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
    </script>
    <div class="no-padding alert-top">
        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblMsg" runat="server"  Visible="false"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
    <div class="divheader">Salespoint Target to Salesman</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row">
            <div class="form-group clearfix">
                <div class="col-sm-3">
                    <label class="control-label col-sm-2">Period</label>
                    <div class="col-md-10 drop-down">
                        <asp:DropDownList ID="cbperiod" runat="server"  CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbperiod_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-6">
                    <label class="control-label col-sm-2">Salesman</label>
                    <div class="col-md-10 drop-down">
                        <asp:DropDownList ID="cbsalesman" runat="server"  CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbsalesman_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3">
                    <label class="control-label col-sm-2" >Sales Group</label>
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" class="col-sm-10">
                    <ContentTemplate>
                        <asp:Label ID="lblGroupName" runat="server" CssClass="control-label badge badge-lg danger"></asp:Label>
                    </ContentTemplate></asp:UpdatePanel>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-12 overflow-x">
                <table class="table table-striped mygrid row-no-padding">
                    <tr>
                        <th><b>Product Group </b></th>
                        <th><b>HO Target</b></th>
                        <th><b>Priority </b></th>
                        <th><b>Min. Loading</b></th>
                        <th style="display:none;"><b>UptoDate</b></th>
                        <th><b>Already Used</b></th>
                        <th><b>Sales Target</b></th>
                        <th><b>Remark</b></th>
                        <th><strong>Tablet Add</strong></th>
                        <th>ACTION</th>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbprod" CssClass="form-control input-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbprod_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cbperiod" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td >
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="txtargethoPnl">
                                        <asp:TextBox ID="txtargetho" runat="server" CssClass="form-control input-sm ro" Enabled="false"></asp:TextBox>
                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cbprod" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cbperiod" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cbsalesman" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width:60px;">
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtPriority" runat="server" CssClass="form-control input-sm ro" Enabled="false"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width:90px;">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtMinTarget" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control input-sm">0</asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>

                        <td style="display:none;" >
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtUptoDateTarget" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                    <asp:CalendarExtender CssClass="date" ID="txstart_dt_CalendarExtender" runat="server" OnClientShown="calendarShown"
                                        TargetControlID="txtUptoDateTarget" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                                    </asp:CalendarExtender>
                                </ContentTemplate>

                            </asp:UpdatePanel>

                        </td>

                        <td style="width:100px;">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txused" runat="server" CssClass="form-control input-sm ro" Enabled="false"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cbprod" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </td>
                        <td style="width:100px;">  
                            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="txqtyPnl">
                                        <asp:TextBox ID="txqty" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lbremark" runat="server" CssClass="text-red full block" Style="padding-top:5px;"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cbprod" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </td>
                        <td style="width:100px;">
                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="txtabqtyPnl">
                                        <asp:TextBox ID="txtabqty" runat="server" CssClass="form-control input-sm" >0</asp:TextBox>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width:70px;">
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btadd" runat="server" Text="Add" CssClass="btn-success btn btn-sm btn-block btn-add" OnClick="btadd_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr style="display:none;">
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>Date Format</td>
                        <td>dd/mm/yyyy</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
            </div> 
        </div>

        <div class="clearfix">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>

                    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="None" 
                        OnRowEditing="grd_RowEditing" OnRowDataBound="grd_RowDataBound" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowUpdating="grd_RowUpdating" CssClass="table table-striped mygrid">
                        <AlternatingRowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Product Group">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdprodcd" runat="server" Value='<%# Eval("prod_cd") %>' />
                                    <asp:HiddenField ID="hdfGroup" runat="server" Value='<%# Eval("SalesmanGroup") %>' />
                                    <asp:Label ID="lbprodname" runat="server" Text='<%# Eval("prod_nm") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HO Target">
                                <ItemTemplate>
                                    <asp:Label ID="lbhotarget" runat="server" Text='<%# Eval("target_ho") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Sales Man">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdSalesmanCd" runat="server" Value='<%# Eval("salesman_cd") %>' />
                                    <asp:Label ID="lblSalesMan" runat="server" Text='<%# Eval("salesMan") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                     <asp:HiddenField ID="hdfIsPriority" runat="server" Value='<%# Eval("IsPriority") %>' />
                                    <asp:CheckBox ID="chkIsPriority" runat="server" Checked='<%# Eval("isPriority") %>' Enabled="false" />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    Priority
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sales Target">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdfQty" runat="server" Value='<%# Eval("qty") %>' />
                                    <asp:Label ID="lbqty" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txqty" runat="server" Text='<%# Eval("qty") %>' CssClass="form-control input-sm"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Upto Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbUptoDateTarget" runat="server" Text='<%# Eval("UptoDateTarget", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="txUptoDateTarget" runat="server" Text='<%# Eval("UptoDateTarget", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                    <%--<asp:TextBox ID="txUptoDateTarget" runat="server" Text='<%# Eval("UptoDateTarget", "{0:dd/MM/yyyy}") %>'></asp:TextBox>--%>
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Min Loading">
                                <ItemTemplate>
                                    <asp:Label ID="lbMintarget" runat="server" Text='<%# Eval("MinTarget") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txMintarget" runat="server" Text='<%# Eval("MinTarget") %>' CssClass="form-control input-sm"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Additonal by branch">
                                 <ItemTemplate>
                                    <asp:Label ID="lbtabtarget" runat="server" Text='<%# Eval("tabtarget") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtabtarget" runat="server" Text='<%# Eval("tabtarget") %>' CssClass="form-control input-sm"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="Action" ShowEditButton="True" />
                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page"/>
                        <RowStyle/>
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cbperiod" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cbsalesman" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>

        <div class="navi row margin-bottom">
            <asp:LinkButton ID="btprintho" runat="server" CssClass="btn btn-info btn-print btn-print-ho" OnClick="btprintho_Click"> Print Target HO</asp:LinkButton>
            <asp:LinkButton ID="btprint" runat="server" CssClass="btn btn-info btn-print" OnClick="btprint_Click">Print</asp:LinkButton>
        </div>
    </div>

</asp:Content>

