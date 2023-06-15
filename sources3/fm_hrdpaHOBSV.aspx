<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_hrdpaHOBSV.aspx.cs" Inherits="fm_hrdpaHOBSV" %>

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
        function openreport(url) {
            window.open(url, "myrep");
        }
        //$(document).ready(function () {
        //    $('#pnlmsg').hide();
        //});

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" runat="Server">
    <div class="container">
        <div class="form-horizontal">
            <h4 class="jajarangenjang">BSV Performance Appraisal HO</h4>
            <div class="h-divider"></div>
            <div class="form-group">

                <div class="row">
                    <label class="control-label col-sm-1">Branch</label>
                    <div class="col-sm-4 drop-down">
                        <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <label class="control-label col-md-1">Period</label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cbperiod" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbperiod_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <label class="control-label col-md-1">Employee</label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cbemployee" AutoPostBack="true" OnSelectedIndexChanged="cbemployee_SelectedIndexChanged" CssClass="form-control" runat="server"></asp:DropDownList>
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
                                <asp:TemplateField HeaderText="KPI">
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
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Achievement">
                                    <ItemTemplate>
                                        <asp:Label ID="lbachievement" runat="server" Text='<%#Eval("achievement")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txachievement" Text='<%#Eval("achievement")%>' runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Result">
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
                                <asp:CommandField />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                
                <div class="row">
                    <label class="control-label col-md-1">Recommendation</label>
                    <div class="col-md-2 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlRecommendation" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRecommendation_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtRecommendation" ReadOnly="true" placeholder="Other Recommendation" runat="server" Text="" Visible="false"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Strengths</label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtStrengths" runat="server" Text="" ReadOnly="true" TextMode="MultiLine" Height="100"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Weakness</label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtWeakness" runat="server" Text="" ReadOnly="true" TextMode="MultiLine" Height="100"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Development Program</label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtDevelopmentProgram" runat="server" ReadOnly="true" Text="" TextMode="MultiLine" Height="100"></asp:TextBox>
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
                                <asp:TextBox ID="txtEvaluationPurpose" ReadOnly="true" placeholder="Other Evaluation Purpose" runat="server" Text="" Visible="false"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Remarks </label>
                    <div class="col-md-1">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtRemarks" runat="server" Text="" TextMode="MultiLine" Width="520" Height="100"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" style="text-align: center">
                        <%--<asp:LinkButton ID="btsave" runat="server" CssClass="btn btn-success" OnClick="btsave_Click"><i class="fa fa-save">&nbsp;Save</i></asp:LinkButton>--%>
                        <asp:LinkButton ID="bprint" CssClass="btn btn-danger" runat="server" OnClick="btprint_Click"><i class="fa fa-print">&nbsp;Print PA</i></asp:LinkButton>
                        <asp:LinkButton ID="btRaw" CssClass="btn btn-danger" runat="server" OnClick="btRaw_Click"><i class="fa fa-print">&nbsp;Raw Data</i></asp:LinkButton>
                        <asp:LinkButton ID="btPaprint" CssClass="btn btn-danger" runat="server" OnClick="btPaprint_Click"><i class="fa fa-print">&nbsp;Print PA Report </i></asp:LinkButton>
                        <asp:LinkButton ID="btInctvprint" CssClass="btn btn-danger" runat="server" OnClick="btInctvprint_Click"><i class="fa fa-print">&nbsp;Print Incentive Report </i></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

