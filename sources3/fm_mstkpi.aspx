<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstkpi.aspx.cs" Inherits="fm_mstkpi" %>

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
    <style>
        body {
            overflow-y: auto !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="form-horizontal">
            <h4 class="jajarangenjang">Master Key Performance Indicator</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <label class="control-label col-md-1">Job Title</label>
                <div class="col-md-2 drop-down">
                    <asp:DropDownList ID="cbjobtitle" onchange="javascript:ShowProgress();" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbjobtitle_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <label class="control-label col-md-1">Level</label>
                <div class="col-md-2 drop-down">
                    <asp:DropDownList ID="cblevel" onchange="javascript:ShowProgress();" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cblevel_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <label class="control-label col-md-1">Key Responsible</label>
                <div class="col-md-2 drop-down">
                    <asp:DropDownList ID="cbkr" onchange="javascript:ShowProgress();" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbkr_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <label class="control-label col-md-1">Section</label>
                <div class="col-md-2 drop-down">
                    <asp:DropDownList ID="cbsection" onchange="javascript:ShowProgress();" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbsection_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-md-1">Weight KPI(%)</label>
                <div class="col-md-2 require">
                    <asp:TextBox ID="txweightkpi" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <label class="control-label col-md-1">Weight Objective(%)</label>
                <div class="col-md-2 require">
                    <asp:TextBox ID="txweightobj" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-md-1">Objective</label>
                <div class="col-md-2 require">
                    <asp:TextBox ID="txobjective" placeholder="Entry objective" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <label class="control-label col-md-1">Objective Arabic</label>
                <div class="col-md-2 require">
                    <asp:TextBox ID="txobjarabic" placeholder="موضوعي" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <label class="control-label col-md-1">KPI</label>
                <div class="col-md-2 require">
                    <asp:TextBox ID="txkpi" placeholder="Entry Key Performance Indicator" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <label class="control-label col-md-1">KPI Arabic</label>
                <div class="col-md-2 require">
                    <asp:TextBox ID="txkpiarabic" placeholder="مؤشر الأداء الرئيسي" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>


            <div class="form-group">
                <div class="col-md-12">
                    <table class="table table-page-fix">
                        <tr>
                            <th>Lang</th>
                            <th>POOR</th>
                            <th>FAIR</th>
                            <th>GOOD</th>
                            <th>VERY GOOD</th>
                        </tr>
                        <tr>
                            <td><strong>English</strong></td>
                            <td>
                                <asp:TextBox ID="txpoor" placeholder="Entry for poor definition" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txfair" placeholder="Entry for Fair definition" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txgood" placeholder="Entry for good definition" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txverygood" placeholder="Entry for Very Good definition" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td><strong>Arabia</strong></td>
                            <td>
                                <asp:TextBox ID="txpoorarabic" placeholder="دخول تعريف الفقراء" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txfairarabic" placeholder="تعريف معرض الدخول" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txgoodarabic" placeholder="دخول تعريف جيد" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txverygoodarabic" placeholder="دخول تعريف جيد جدا" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnRowDeleting="grd_RowDeleting" OnRowEditing="grd_RowEditing" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowUpdating="grd_RowUpdating" AllowPaging="True" CellPadding="0" ShowFooter="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="Key Responsibility">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdids" Value='<%#Eval("IDS") %>' runat="server" />
                                            <%#Eval("keyresp_nm") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Objective">
                                        <ItemTemplate>

                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("objective") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Section">
                                        <ItemTemplate><%#Eval("section_nm") %></ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="cbsection" runat="server"></asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="KPI">
                                        <ItemTemplate><%#Eval("kpi") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Weight KPI(%)">

                                        <ItemTemplate>
                                            <%#Eval("weight_kpi") %>
                                            <asp:HiddenField ID="hdfKPIWeight" Value='<%#Eval("weight_kpi") %>' runat="server" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txweightkpi" Text='<%#Eval("weight_kpi") %>' runat="server"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbweightkpi" runat="server" Text="" Font-Bold="True" Font-Size="Larger"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Poor">
                                        <ItemTemplate><%#Eval("poor") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fair">
                                        <ItemTemplate><%#Eval("fair") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Good">
                                        <ItemTemplate><%#Eval("good") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Very Good">
                                        <ItemTemplate><%#Eval("verygood") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                                </Columns>
                                <FooterStyle BackColor="Yellow" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="h-divider"></div>
            <div class="form-group">
                <div class="col-md-12" style="text-align: center; height: 20px;">
                    <asp:LinkButton ID="btnew" runat="server" CssClass="btn btn-primary" OnClick="btnew_Click"><i class="fa fa-plus">&nbsp;New</i></asp:LinkButton>
                    <asp:LinkButton ID="btsave" runat="server" OnClientClick="javascript:ShowProgress();" CssClass="btn btn-danger" OnClick="btsave_Click"><i class="fa fa-save">&nbsp;Save</i></asp:LinkButton>
                    <asp:LinkButton ID="btprint" runat="server" CssClass="btn btn-warning" OnClick="btprint_Click"><i class="fa fa-print">&nbsp;Print</i></asp:LinkButton>
                </div>
            </div>
            <!--Form group end -->
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>

</asp:Content>

