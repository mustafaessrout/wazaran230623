<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_hrdpaMerchandiserOther.aspx.cs" Inherits="fm_hrdpaMerchandiserOther" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
        <link href="css/anekabutton.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-1.10.2.js"></script>
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
        //$(document).ready(function () {
        //    $('#pnlmsg').hide();
        //});

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="form-horizontal">
            <h4 class="jajarangenjang">Merchandiser Other Jobs</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-md-1">Period </label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cbperiod" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbperiod_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <label class="control-label col-md-1">Level</label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cblevel" CssClass="form-control" runat="server" onchange="javascript:ShowProgress();" AutoPostBack="True" OnSelectedIndexChanged="cblevel_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <label class="control-label col-md-1">Job Title</label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cbjobtitle" CssClass="form-control" runat="server" onchange="javascript:ShowProgress();" AutoPostBack="True" OnSelectedIndexChanged="cbjobtitle_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <label class="control-label col-md-1">Employee</label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cbemployee" AutoPostBack="true" OnSelectedIndexChanged="cbemployee_SelectedIndexChanged" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <label class="control-label col-md-1"></label>
                    <div class="col-md-2">
                    </div>
                </div>
                <div class="h-divider"></div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" CellPadding="0" ShowFooter="True"
                            OnRowCancelingEdit="grd_RowCancelingEdit" OnRowDeleting="grd_RowDeleting" OnRowEditing="grd_RowEditing"
                            OnRowUpdating="grd_RowUpdating" OnRowDataBound="grd_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Key Responsible">
                                    <ItemTemplate>
                                        <asp:Label ID="lblkeyresp_nm" runat="server" Text='<%#Eval("keyresp_nm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Objective">
                                    <ItemTemplate>
                                        <asp:Label ID="lblobjective" runat="server" Text='<%#Eval("objective") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="KPI" ControlStyle-Width="400">
                                    <ItemTemplate>
                                        <asp:Label ID="lblkpi" runat="server" Text='<%#Eval("kpi") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Weight %"  HeaderStyle-Width="64">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWeight" runat="server" Text='<%#Eval("weight_kpi") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbtotWeight" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Target">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdids" runat="server" Value='<%#Eval("ids")%>' />
                                        <asp:HiddenField ID="hdfweight_kpi" runat="server" Value='<%#Eval("weight_kpi")%>' />
                                        <asp:Label ID="lblids" runat="server" Text='<%#Eval("ids") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lbtarget" runat="server" Text='<%#Eval("target") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Achievement">
                                    <ItemTemplate>
                                        <asp:Label ID="lbachievement" runat="server" Text='<%#Eval("achievement")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txachievement" Text='<%#Eval("achievement")%>' runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Result %">
                                    <ItemTemplate>
                                        <asp:Label ID="lbresult" Width="5em" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbtotResult" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="table-footer" BackColor="Yellow" Font-Bold="True" Font-Size="Larger" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="h-divider"></div>
                <div class="row">
                    
                    
                </div>
                
                <div class="row">
                    <label class="control-label col-md-1"></label>
                    <div class="col-md-2 ">
                    </div>
                    <label class="control-label col-md-1"></label>
                    <div class="col-md-2 ">
                    </div>
                    <label class="control-label col-md-1"></label>
                    <div class="col-md-2 ">
                    </div>
                    <label class="control-label col-md-1" style="display: none;">Nationality</label>
                    <div class="col-md-2 ">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:Label class="control-label col-md-1" Text="" ID="lblNationalit" runat="server" Style="display: none;"></asp:Label>
                                <asp:HiddenField ID="hdfCustomerAchievement" runat="server" Value="" />
                                <asp:HiddenField ID="hdfCartoonAchievement" runat="server" Value="" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="row">

                   
                    <div class="col-md-12" style="text-align: left">
                        <span style="color: red; font-size: larger;">Result = (Achievement X Weight) / Target </span>
                    </div>
                    <div class="col-md-12" style="text-align: center">
                        <%--<asp:LinkButton ID="btsave" runat="server" CssClass="btn btn-success" OnClick="btsave_Click"><i class="fa fa-save">&nbsp;Save</i></asp:LinkButton>--%>
                        <asp:LinkButton ID="btnViewAttendance" runat="server" CssClass="btn btn-success" OnClick="btnViewAttendance_Click"><i class="fa fa-save">&nbsp;View Attendance</i></asp:LinkButton>
                        <asp:LinkButton ID="btnPrintDriverHis" Visible="false" runat="server" CssClass="btn btn-success" OnClick="btnPrintDriverHis_Click"><i class="fa fa-print">&nbsp;Print</i></asp:LinkButton>
                        <asp:LinkButton ID="btnPrintDriverHisAllDriver" Visible="false" runat="server" CssClass="btn btn-success" OnClick="btnPrintDriverHisAllDriver_Click"><i class="fa fa-print">&nbsp;Print All</i></asp:LinkButton>
                        <asp:LinkButton ID="btnMerchOther" CssClass="btn btn-finish" runat="server" OnClick="btnMerchOther_Click"><i class="fa fa-print">&nbsp;Merchandiser Other Jobs</i></asp:LinkButton>
                        <%--<asp:LinkButton ID="bprint" CssClass="btn btn-danger" runat="server" OnClick="btprint_Click"><i class="fa fa-print">&nbsp;Print PA</i></asp:LinkButton>
                        <asp:LinkButton ID="btnIncentive" CssClass="btn btn-danger" runat="server" OnClick="btnIncentive_Click"><i class="fa fa-print">&nbsp;Incentive Statement</i></asp:LinkButton>
                        <asp:LinkButton ID="btnValidationSheet" CssClass="btn btn-danger" runat="server" OnClick="btnValidationSheet_Click"><i class="fa fa-print">&nbsp;Validation Sheet</i></asp:LinkButton>--%>
                        <asp:LinkButton ID="btprintMerch_Visit" CssClass="btn btn-danger" runat="server" OnClick="btprintMerch_Visit_Click"><i class="fa fa-print">&nbsp;Print Merchandiser Visit</i></asp:LinkButton>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

