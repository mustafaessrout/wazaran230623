<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_empcompetency.aspx.cs" Inherits="fm_empcompetency" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="js/bootstrap.min.js"></script>
    <script src="js/jquery-3.2.1.min.js"></script>
    <style>
        #lblMsg{
            z-index: 99;
            position: absolute;
            top: 10px;
            width: calc(100% - 40px);
            left: 20px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class="divheader">APPRAISAL FORM</div>
    <div class="h-divider"></div>
    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblMsg" runat="server"  Visible="false"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="container">
        <div class="clearfix" >
            <div class="clearfix margin-bottom">
                <div class="col-md-6 no-padding">
                    <label class="control-label col-sm-4 col-md-2">Period KPI</label>
                    <div class="col-sm-8 col-md-10 drop-down">
                        <asp:DropDownList ID="cbperiod" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="clearfix margin-bottom">
                <div class="col-md-3 col-sm-6 no-padding">
                    <label class="control-label col-sm-4">Salespoint</label>
                    <div class="col-sm-8 drop-down">
                        <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                 <div class="col-md-3 col-sm-6 no-padding">
                    <label class="control-label col-sm-4">Job Title</label>
                    <div class="col-sm-8  drop-down">
                        <asp:DropDownList ID="cbjobtitle" runat="server" CssClass="form-control"   AutoPostBack="True" OnSelectedIndexChanged="cbjobtitle_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                 <div class="col-md-3 col-sm-6 no-padding">
                     <label class="control-label col-sm-4">Employee</label>
                    <div class="col-sm-8  drop-down" style="">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbemployee" runat="server" CssClass="form-control"   AutoPostBack="True" OnSelectedIndexChanged="cbemployee_SelectedIndexChanged"></asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cbjobtitle" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                 <div class="col-md-3 col-sm-6 no-padding">
                    <label class="control-label col-sm-4">Nationality</label>
                    <div class="col-sm-8  drop-down" >
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtNationality" CssClass="form-control"  runat="server"  Enabled="false"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>


            <div class="row">
                <h5 class="divheader subheader subheader-bg">Key Responsibility (KR) - What to do ?</h5>
            </div>

            <div class="clear-float margin-bottom">
                <div class="col-md-12">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdkr" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mygrid"  OnRowEditing="grdkr_RowEditing" OnRowCancelingEdit="grdkr_RowCancelingEdit" OnRowDataBound="grdkr_RowDataBound" OnRowUpdating="grdkr_RowUpdating">
                                <Columns>
                                    <asp:TemplateField HeaderText="Key Responsibility">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdappraisal" runat="server" Value='<%# Eval("appraisal_id") %>' />
                                            <asp:HiddenField ID="hdfPoint" Value='<%# Eval("point") %>' runat="server" />
                                            <asp:Label ID="lbresponsibility" runat="server" Text='<%# Eval("appraisal_desc") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Poor">
                                        <ItemTemplate><%# Eval("poor") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fair">
                                        <ItemTemplate><%# Eval("fair") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Good">
                                        <ItemTemplate><%# Eval("good") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Very Good">
                                        <ItemTemplate><%# Eval("verygood") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Excellent">
                                        <ItemTemplate><%# Eval("excellent") %></ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Point">
                                        <ItemTemplate>
                                            <asp:Label ID="lbpoint" runat="server" Font-Bold="True" Text="point" ForeColor="Red"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <div class="drop-down">
                                                <asp:DropDownList ID="cbpoint" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                                            </div>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowEditButton="True" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbjobtitle" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>

            <div class="row">
                <div class="divheader subheader subheader-bg">Key Performance Indicator(KPI) - What Result?</div>
            </div>
            <div class="clear-float margin-bottom">
                <div class="col-md-12">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdkpi" runat="server" CssClass="table table-striped mygrid" AutoGenerateColumns="False"  OnRowEditing="grdkpi_RowEditing" OnRowCancelingEdit="grdkpi_RowCancelingEdit" OnRowDataBound="grdkpi_RowDataBound" OnRowUpdating="grdkpi_RowUpdating">
                                <Columns>
                                    <asp:TemplateField HeaderText="Key Performance Indicator">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdappraisal" Value='<%# Eval("appraisal_id") %>' runat="server" />
                                            <asp:HiddenField ID="hdfTotalInvoice" Value='<%# Eval("totalInvoice") %>' runat="server" />
                                            <asp:HiddenField ID="hdfTotalQty" Value='<%# Eval("totalQty") %>' runat="server" />
                                            <asp:HiddenField ID="hdfTragetInvoice" Value='<%# Eval("tragetInvoice") %>' runat="server" />
                                            <asp:HiddenField ID="hdfTragetQty" Value='<%# Eval("tragetQty") %>' runat="server" />
                                            <asp:HiddenField ID="hdfPoint" Value='<%# Eval("point") %>' runat="server" />
                                            <asp:Label ID="lbappraisal" runat="server" Text='<%# Eval("appraisal_desc") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Target">
                                        <ItemTemplate>
                                            <asp:Label ID="lbtarget" runat="server" Text='<%# Eval("totalQty") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actual">
                                        <ItemTemplate>
                                            <asp:Label ID="lbactual" runat="server" Text='<%# Eval("totalInvoice") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="%">
                                        <ItemTemplate>
                                            <asp:Label ID="lbpct" runat="server" Text="0"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Point">
                                        <ItemTemplate>
                                            <asp:Label ID="lbpoint" runat="server" Font-Bold="True" Text="point" ForeColor="Red"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <div class="drop-down">
                                                <asp:DropDownList ID="cbpoint" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                                            </div>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowEditButton="True" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>


            <div class="row">
                <h5 class="divheader subheader subheader-bg">Competencies(Skill, Knowledge, Attribute) - How to do ?</h5>
            </div>
            <div class="form-group">
                <div class="col-md-12">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdcompetency" runat="server" AutoGenerateColumns="False" CssClass="mygrid"  OnRowEditing="grdcompetency_RowEditing" OnRowDataBound="grdcompetency_RowDataBound" OnRowCancelingEdit="grdcompetency_RowCancelingEdit" OnRowUpdating="grdcompetency_RowUpdating">
                                <Columns>
                                    <asp:TemplateField HeaderText="Competencies">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdappraisal" runat="server" Value='<%# Eval("appraisal_id") %>' />
                                            <asp:HiddenField ID="hdfPoint" Value='<%# Eval("point") %>' runat="server" />
                                            <asp:HiddenField ID="hdfAchiveTarget" Value='<%# Eval("AchiveTarget") %>' runat="server" />
                                            <asp:HiddenField ID="hdfTargetValue" Value='<%# Eval("TargetValue") %>' runat="server" />
                                            <asp:HiddenField ID="hdfTargetWeight" Value='<%# Eval("WeightValue") %>' runat="server" />
                                            <asp:Label ID="lbappraisal" runat="server" Text='<%# Eval("appraisal_desc") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="poor">
                                        <ItemTemplate><%# Eval("poor") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fair">
                                        <ItemTemplate><%# Eval("fair") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Good">
                                        <ItemTemplate><%# Eval("good") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Very Good">
                                        <ItemTemplate><%# Eval("verygood") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Excellent">
                                        <ItemTemplate><%# Eval("excellent") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Excellent">
                                        <ItemTemplate><%# Eval("excellent") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Target" HeaderStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTarget" runat="server" Font-Bold="True" Text='<%# Eval("TargetValue") %>' ForeColor="Red"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Target Weight %" HeaderStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTargetWeight" runat="server" Font-Bold="True" Text='<%# Eval("WeightValue") %>' ForeColor="Red"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Achive" HeaderStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAchive" runat="server" Font-Bold="True" Text='<%# Eval("AchiveTarget") %>' ForeColor="Green"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="cbAchive" runat="server">
                                                <asp:ListItem Selected="True" Text="1" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                                                         <asp:TemplateField HeaderText="Achive Weight %" HeaderStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAchiveWeight" runat="server" Font-Bold="True" Text='<%# Eval("AchiveWeight") %>' ForeColor="Green"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Point">
                                        <ItemTemplate>
                                            <asp:Label ID="lbpoint" runat="server" Font-Bold="True" Text="point" ForeColor="Red"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="cbpoint" runat="server"></asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowEditButton="True" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbjobtitle" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <%-- <div class="form-group">
                <label class="control-label col-md-1">Target QTY</label>
                <div class="col-md-2">
                    <asp:Label ID="lbqty" runat="server" Text="0" CssClass="form-control"></asp:Label>
                </div>
                <label class="control-label col-md-1">Target Invoice</label>
                <div class="col-md-2">
                    <asp:Label ID="lbinv" runat="server" Text="0" CssClass="form-control"></asp:Label>
                </div>
                 <label class="control-label col-md-1">Achievement QTY</label>
                <div class="col-md-2">
                    <asp:Label ID="lbachieveqty" runat="server" Text="0" CssClass="form-control"></asp:Label>
                </div>
                 <label class="control-label col-md-1">Achievement Invoice</label>
                <div class="col-md-2">
                    <asp:Label ID="lbachieveinv" runat="server" Text="0" CssClass="form-control"></asp:Label>
                </div>
            </div>--%>

            <div class="margin-bottom padding-bottom">
                <div class="navi">
                    <asp:LinkButton ID="btsave" CssClass="btn btn-warning " runat="server" OnClick="btsave_Click">Save</asp:LinkButton>
                    <asp:LinkButton ID="btprint" CssClass="btn btn-info " runat="server" OnClick="btprint_Click">Print</asp:LinkButton>
                    <asp:LinkButton ID="btnOldReport" CssClass="btn btn-primary" runat="server" OnClick="btnOldReport_Click">Old Format Report</asp:LinkButton>
                    <asp:LinkButton ID="btnIncentive" CssClass="btn btn-primary " runat="server" OnClick="btnIncentive_Click">Incentive</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

