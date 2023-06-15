<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_hrdpaHOUpdate.aspx.cs" Inherits="fm_hrdpaHOUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
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
            <h4 class="jajarangenjang">Performance Appraisal For HO </h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-md-1">Period </label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cbperiod" CssClass="form-control" runat="server"></asp:DropDownList>
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
                <div class="h-divider"></div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" CellPadding="0" ShowFooter="True"
                             OnRowDataBound="grd_RowDataBound">
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
                                <asp:TemplateField HeaderText="Weight %">
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
                                        <asp:Label ID="lbtarget" runat="server" Text='<%#Eval("TargetValue") %>'></asp:Label>
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
                                <asp:CommandField/>
                            </Columns>
                            <FooterStyle CssClass="table-footer" BackColor="Yellow" Font-Bold="True" Font-Size="Larger" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" style="text-align: center">
                        <asp:LinkButton ID="btcalculation" OnClientClick="javascript:ShowProgress();" CssClass="btn btn-primary" runat="server" OnClick="btcalculation_Click">CALCULATE PA</asp:LinkButton>

                    </div>
                </div>
                <div class="row">
                    <label class="control-label col-md-1">Recommendation</label>
                    <div class="col-md-2 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlRecommendation" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRecommendation_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtRecommendation" placeholder="Other Recommendation" runat="server" Text="" Visible="false"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Strengths<span style="color: red;">*</span></label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtStrengths" runat="server" Text="" TextMode="MultiLine" Height="100" ReadOnly="true"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Weakness<span style="color: red;">*</span></label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtWeakness" runat="server" Text="" TextMode="MultiLine" Height="100" ReadOnly="true"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Development Program<span style="color: red;">*</span></label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtDevelopmentProgram" runat="server" Text="" TextMode="MultiLine" Height="100" ReadOnly="true"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="row">
                    <label class="control-label col-md-1">Evaluation Purpose</label>
                    <div class="col-md-2 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlEvaluationPurpose" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEvaluationPurpose_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtEvaluationPurpose" placeholder="Other Evaluation Purpose" runat="server" Text="" Visible="false"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Remarks / Comments</label>
                    <div class="col-md-1">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtRemarks" runat="server" Text="" TextMode="MultiLine" Width="520" Height="100" ReadOnly="true"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" style="text-align: left ">
                        <span style="color:red; font-size:larger;"> Result = (Achievement X Weight) / Target </span>
                    </div>
                    <div class="col-md-12" style="text-align: center">
                        <asp:LinkButton ID="btsave" runat="server" CssClass="btn btn-success" OnClick="btsave_Click"><i class="fa fa-save">&nbsp;Save</i></asp:LinkButton>
                        <asp:LinkButton ID="btnPrintDriverHis" runat="server" CssClass="btn btn-success" OnClick="btnPrintDriverHis_Click"><i class="fa fa-print">&nbsp;Print</i></asp:LinkButton>
                        <%--<asp:LinkButton ID="btnPrintDriverHisAllDriver" runat="server" CssClass="btn btn-success" OnClick="btnPrintDriverHisAllDriver_Click"><i class="fa fa-print">&nbsp;Print All</i></asp:LinkButton>--%>
                        <asp:LinkButton ID="btnPushData" runat="server" CssClass="btn btn-success" OnClick="btnPushData_Click" onchange="javascript:ShowProgress();"><i class="fa fa-print">&nbsp;Branch To HO</i></asp:LinkButton>
                        <asp:LinkButton ID="btnPullData" runat="server" CssClass="btn btn-success" OnClick="btnPullData_Click" onchange="javascript:ShowProgress();"><i class="fa fa-print">&nbsp;HO To Branch</i></asp:LinkButton>
                        <asp:LinkButton ID="btnResetData" runat="server" CssClass="btn btn-success" OnClick="btnResetData_Click" onchange="javascript:ShowProgress();"><i class="fa fa-print">&nbsp;Reset Data</i></asp:LinkButton>
                        <%--<asp:LinkButton ID="bprint" CssClass="btn btn-danger" runat="server" OnClick="btprint_Click"><i class="fa fa-print">&nbsp;Print PA</i></asp:LinkButton>
                        <asp:LinkButton ID="btnIncentive" CssClass="btn btn-danger" runat="server" OnClick="btnIncentive_Click"><i class="fa fa-print">&nbsp;Incentive Statement</i></asp:LinkButton>
                        <asp:LinkButton ID="btnValidationSheet" CssClass="btn btn-danger" runat="server" OnClick="btnValidationSheet_Click"><i class="fa fa-print">&nbsp;Validation Sheet</i></asp:LinkButton>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

