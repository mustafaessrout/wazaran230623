<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstcompetency.aspx.cs" Inherits="fm_mstcompetency" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="js/jquery-1.9.1.min.js"></script>
    <script src="admin/js/bootstrap.min.js"></script>
    <%--<link href="css/anekabutton.css" rel="stylesheet" />--%>
    <script>
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
    <style>
        .f-item{
            margin-left:10px;
            margin-right:10px;
        }
        .f-item:first-child{
            margin-left:0;
        }
        .f-item:last-child{
            margin-right:0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblMsg" runat="server"  Visible="false" CssClass="block" style="position:absolute; z-index:99;" Width="90%"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="row">
            <div class="divheader">Master Key Appraisal</div>
            <div class="h-divider"></div>
        </div>
        <div class="">
            <div class="clearfix margin-bottom">
                <div class="col-sm-6 no-padding margin-bottom">
                     <label class="control-label col-md-2 col-sm-4">Salespoint</label>
                    <div class="col-md-10 col-sm-8 drop-down">
                        <asp:DropDownList ID="cbsp" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbsp_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-6 no-padding margin-bottom">
                    <label class="control-label col-md-2 col-sm-4">Period</label>
                    <div class="col-md-10 col-sm-8 drop-down">
                        <asp:DropDownList ID="cbperiod" runat="server"  CssClass="form-control" OnSelectedIndexChanged="cbperiod_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="clearfix margin-bottom">
                <div class="col-md-3 col-sm-6 no-padding ">
                    <label class="control-label col-sm-4">Category</label>
                    <div class="col-sm-8 drop-down">
                        <asp:DropDownList ID="cbcat" runat="server" CssClass="form-control"  AutoPostBack="True" OnSelectedIndexChanged="cbcat_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 no-padding ">
                    <label class="control-label col-sm-4">Job Title</label>
                    <div class="col-sm-8 drop-down">
                        <asp:DropDownList ID="cbjobtitle" CssClass="form-control"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbjobtitle_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>               
                <div class="col-md-3 col-sm-6 no-padding ">
                    <label class="control-label col-sm-4">For</label>
                    <div class="col-sm-8 drop-down">
                        <asp:DropDownList ID="cbfor" CssClass="form-control"  runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 no-padding ">
                    <label class="control-label col-sm-4">Appraisal For</label>
                    <div class="col-sm-8 drop-down">
                        <asp:DropDownList ID="ddlAppraisalFor" CssClass="form-control"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAppraisalFor_SelectedIndexChanged">
                            <asp:ListItem Text="Saudi" Value="saudi" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Non Saudi" Value="nonsaudi"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>


            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                <ContentTemplate>
                    <div id="dvKPI" runat="server">
                        <div class="well well-sm">
                            <div class="clearfix">
                                <div class="col-sm-6 no-padding">
                                    <label class="col-sm-12 no-padding-left">KPI QTY Weight</label>
                                    <div class="col-sm-12 no-padding-left">
                                        <asp:TextBox ID="txtInvoiceWeight" CssClass="form-control" runat="server" onkeypress="return isNumberKey(event)" MaxLength="2"  ></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6 no-padding">
                                    <label class="col-sm-12 no-padding-left" >KPI Invoice Weight</label>
                                    <div class="col-sm-12 no-padding-left">
                                        <asp:TextBox ID="txtQTYWeight" CssClass="form-control" runat="server" onkeypress="return isNumberKey(event)"  MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="dvDescription" runat="server">
                        <div class="clearfix margin-bottom">
                            <label class="control-label col-md-1 col-sm-4">Description</label>
                            <div class="col-md-11 col-sm-8">
                                <asp:TextBox ID="txdesc" CssClass="form-control"  runat="server" TextMode="MultiLine" Rows="2"></asp:TextBox>
                            </div>
                        </div>
                        <div class="clearfix">
                            <div class="col-sm-12 no-padding">
                                <div class="flex space-between margin-bottom">
                                    <div class="f-item">
                                        <label class="control-label">Poor</label>
                                        <div class="">
                                            <asp:TextBox ID="txpoor" CssClass="form-control"  TextMode="MultiLine"  runat="server" Rows="2"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="f-item">
                                        <label class="control-label ">Fair</label>
                                         <div class="">
                                            <asp:TextBox ID="txfair" CssClass="form-control"  TextMode="MultiLine"  runat="server" Rows="2"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="f-item">
                                        <label class="control-label ">Good</label>
                                        <div class="">
                                            <asp:TextBox ID="txgood" CssClass="form-control"  TextMode="MultiLine"  runat="server" Rows="2"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="f-item">
                                        <label class="control-label ">Very Good</label>
                                        <div class="">
                                            <asp:TextBox ID="txverygood" CssClass="form-control"  TextMode="MultiLine"  runat="server" Rows="2"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="f-item">
                                        <label class="control-label ">Excellent</label>
                                        <div class="">
                                            <asp:TextBox ID="txexcellent" CssClass="form-control"  TextMode="MultiLine"  runat="server" Rows="2"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="f-item">
                                        <label class="control-label ">Order By</label>
                                        <div class="">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtOrderBy" onkeypress="return isNumberKey(event)" MaxLength="2" ReadOnly="true" Enabled="false" CssClass="form-control"   runat="server"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="f-item">
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <label class="control-label " runat="server" id="lblWeightMaster">Weight</label>
                                                <label class="control-label " runat="server" id="lblTargetMaster">Target</label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                            <ContentTemplate>
                                                <div class="">
                                                    <asp:TextBox ID="txtMasterWeight" CssClass="form-control" onkeypress="return isNumberKey(event)" MaxLength="4"   runat="server" Rows="2"></asp:TextBox>
                                                </div>
                                                <div class="">
                                                    <asp:DropDownList ID="cbTargetMaster" runat="server">
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
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                        
                </ContentTemplate>
            </asp:UpdatePanel>

            <div class="clearfix">
                <div class="col-sm-12 no-padding">
                    <div class="flex space-between margin-bottom well well-sm">
                        <div>
                            <label class="control-label">Poor Incentive</label>
                            <div class="">
                                <asp:TextBox ID="txtPoorIncentive" CssClass="form-control" onkeypress="return isNumberKey(event)" MaxLength="4"   runat="server" Rows="2"></asp:TextBox>
                            </div>
                        </div>
                        <div>
                            <label class="control-label">Fair Incentive</label>
                             <div class="">
                                <asp:TextBox ID="txtFairIncentive" CssClass="form-control" onkeypress="return isNumberKey(event)" MaxLength="4"   runat="server" Rows="2"></asp:TextBox>
                            </div>
                        </div>
                        <div>
                            <label class="control-label ">Good Incentive</label>
                            <div class="">
                                <asp:TextBox ID="txtGoodIncentive" CssClass="form-control" onkeypress="return isNumberKey(event)" MaxLength="4"   runat="server" Rows="2"></asp:TextBox>
                            </div>
                        </div>
                        <div>
                            <label class="control-label ">Very Good Incentive</label>
                             <div class="">
                                <asp:TextBox ID="txtVeryGoodIncentive" CssClass="form-control" onkeypress="return isNumberKey(event)" MaxLength="4"   runat="server" Rows="2"></asp:TextBox>
                            </div>
                        </div>
                        <div>
                            <label class="control-label ">Excellent Incentive</label>
                            <div class="">
                                <asp:TextBox ID="txtExcellentIncentive" CssClass="form-control" onkeypress="return isNumberKey(event)" MaxLength="4"   runat="server" Rows="2"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="clearfix">
                <div class="col-sm-12 no-padding">
                    <div class="flex space-between margin-bottom">
                        <div class="f-item  well well-sm">
                            <label class="control-label">Poor Percentage %</label>
                            <div class="clearfix ">
                                <div class="col-sm-6 no-padding-left">
                                    <asp:TextBox ID="txtPoorIncentiveMin" CssClass="form-control" onkeypress="return isNumberKey(event)" MaxLength="3"  runat="server" Rows="2"></asp:TextBox>
                                </div>
                                <div class="col-sm-6 no-padding-right">
                                    <asp:TextBox ID="txtPoorIncentiveMax" CssClass="form-control" onkeypress="return isNumberKey(event)" MaxLength="3"  runat="server" Rows="2"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="f-item  well well-sm">
                            <label class="control-label">Fair Percentage %</label>
                            <div class="clearfix">
                                <div class="col-md-6 no-padding-left">
                                    <asp:TextBox ID="txtFairIncentiveMin" CssClass="form-control" onkeypress="return isNumberKey(event)" MaxLength="3"  runat="server" Rows="2"></asp:TextBox>
                                </div>

                                <div class="col-md-6 no-padding-right">
                                    <asp:TextBox ID="txtFairIncentiveMax" CssClass="form-control" onkeypress="return isNumberKey(event)" MaxLength="3"  runat="server" Rows="2"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="f-item  well well-sm">
                            <label class="control-label">Good Percentage %</label>
                            <div class=" clearfix">
                                <div class="col-md-6 no-padding-left">
                                    <asp:TextBox ID="txtGoodIncentiveMin" CssClass="form-control" onkeypress="return isNumberKey(event)" MaxLength="3"  runat="server" Rows="2"></asp:TextBox>
                                </div>

                                <div class="col-md-6 no-padding-right">
                                    <asp:TextBox ID="txtGoodIncentiveMax" CssClass="form-control" onkeypress="return isNumberKey(event)" MaxLength="3"  runat="server" Rows="2"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="f-item  well well-sm">
                            <label class="control-label">Very Good Percentage %</label>
                            <div class=" clearfix">
                                <div class="col-md-6 no-padding-left">
                                    <asp:TextBox ID="txtVeryGoodIncentiveMin" CssClass="form-control" onkeypress="return isNumberKey(event)" MaxLength="3"  runat="server" Rows="2"></asp:TextBox>
                                </div>

                                <div class="col-md-6 no-padding-right">
                                    <asp:TextBox ID="txtVeryGoodIncentiveMax" CssClass="form-control" onkeypress="return isNumberKey(event)" MaxLength="3"  runat="server" Rows="2"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="f-item  well well-sm">
                            <label class="control-label">Excellent Percentage %</label>
                            <div class=" clearfix">
                                <div class="col-md-6 no-padding-left">
                                    <asp:TextBox ID="txtExcellentIncentiveMin" CssClass="form-control" onkeypress="return isNumberKey(event)" MaxLength="3"  runat="server" Rows="2"></asp:TextBox>
                                </div>

                                <div class="col-md-6 no-padding-right">
                                    <asp:TextBox ID="txtExcellentIncentiveMax" CssClass="form-control" onkeypress="return isNumberKey(event)" MaxLength="3"  runat="server" Rows="2"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="h-divider"></div>
                <h6 class="divheader subheader subheader-bg" runat="server">Key Responsibility </h6>
            </div>

            <div class="clearfix margin-bottom">
                <div class="col-md-12">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdKR" runat="server" OnRowEditing="grdKR_RowEditing" OnRowCancelingEdit="grdKR_RowCancelingEdit" OnRowUpdating="grdKR_RowUpdating"
                                AutoGenerateColumns="False" CssClass="mygrid table table-hover table-striped"  OnRowDeleting="grdKR_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdappraisal" runat="server" Value='<%# Eval("appraisal_id") %>' />
                                            <asp:HiddenField ID="hdfTargetValue" runat="server" Value='<%# Eval("TargetValue") %>' />
                                            <asp:HiddenField ID="hdfWeightValue" runat="server" Value='<%# Eval("WeightValue") %>' />
                                            <asp:HiddenField ID="hdfAchiveTarget" runat="server" Value='<%# Eval("AchiveTarget") %>' />
                                            <asp:HiddenField ID="hdfappraisal_desc" runat="server" Value='<%# Eval("appraisal_desc") %>' />
                                            <strong style="color: red"><%# Eval("appraisal_desc") %></strong>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
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



                                    <asp:TemplateField HeaderText="Order By">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrderBy" runat="server" Text='<%# Eval("OrderBy") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtOrderBy" Text='<%# Eval("OrderBy") %>' runat="server" onkeypress="return isNumberKey(event)" MaxLength="2"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="true" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbcat" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cbjobtitle" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btsave" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>

   
            <div class="row">
                <div class="h-divider"></div>
                <h6 class="divheader subheader subheader-bg"  runat="server">Key Performance Indicator </h6>
            </div>
            <div class="clearfix margin-bottom">
                <div class="col-md-12">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdKPI" runat="server" OnRowEditing="grdKPI_RowEditing" OnRowCancelingEdit="grdKPI_RowCancelingEdit" OnRowUpdating="grdKPI_RowUpdating"
                                AutoGenerateColumns="False" CssClass="mygrid table table-striped table-hover"  OnRowDeleting="grdKPI_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdappraisal" runat="server" Value='<%# Eval("appraisal_id") %>' />
                                            <asp:HiddenField ID="hdfTargetValue" runat="server" Value='<%# Eval("TargetValue") %>' />
                                            <asp:HiddenField ID="hdfWeightValue" runat="server" Value='<%# Eval("WeightValue") %>' />
                                            <asp:HiddenField ID="hdfAchiveTarget" runat="server" Value='<%# Eval("AchiveTarget") %>' />
                                            <asp:HiddenField ID="hdfappraisal_desc" runat="server" Value='<%# Eval("appraisal_desc") %>' />
                                            <strong style="color: red"><%# Eval("appraisal_desc") %></strong>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Weight%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWeightValue" runat="server" Text='<%# Eval("WeightValue") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtWeightValue" Text='<%# Eval("WeightValue") %>' runat="server" onkeypress="return isNumberKey(event)" MaxLength="2"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Order By">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrderBy" runat="server" Text='<%# Eval("OrderBy") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtOrderBy" Text='<%# Eval("OrderBy") %>' runat="server" onkeypress="return isNumberKey(event)" MaxLength="2"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowEditButton="true" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbcat" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cbjobtitle" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btsave" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>
            <div  class="row">
                <div class="h-divider"></div>
                <h6 class="divheader subheader subheader-bg" runat="server">Competencies </h6>
            </div>
            <div class="clearfix margin-bottom">
                <div class="col-md-12">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdCMP" runat="server" OnRowEditing="grdCMP_RowEditing" OnRowCancelingEdit="grdCMP_RowCancelingEdit" OnRowUpdating="grdCMP_RowUpdating"
                                AutoGenerateColumns="False" CssClass="mygrid table table-striped table-hover"  OnRowDeleting="grdCMP_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdappraisal" runat="server" Value='<%# Eval("appraisal_id") %>' />
                                            <asp:HiddenField ID="hdfappraisal_desc" runat="server" Value='<%# Eval("appraisal_desc") %>' />
                                            <strong style="color: red"><%# Eval("appraisal_desc") %></strong>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
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

                                    <asp:TemplateField HeaderText="Target">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTarget" runat="server" Font-Bold="True" Text='<%# Eval("TargetValue") %>' ForeColor="Red"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="cbTarget" runat="server">
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
                                    <asp:TemplateField HeaderText="Weight%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCMPWeight" runat="server" Text='<%# Eval("WeightValue") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtCMPWeight" Text='<%# Eval("WeightValue") %>' runat="server" onkeypress="return isNumberKey(event)" MaxLength="2"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Order By">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrderBy" runat="server" Text='<%# Eval("OrderBy") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtOrderBy" Text='<%# Eval("OrderBy") %>' runat="server" onkeypress="return isNumberKey(event)" MaxLength="2"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="true" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbcat" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cbjobtitle" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btsave" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>
            <div class="clearfix margin-bottom">
                <div class="col-md-12">
                    <div class="navi">
                        <asp:LinkButton ID="btnew" runat="server" CssClass="btn btn-primary" OnClick="btsave_Click">New</asp:LinkButton>
                        <asp:LinkButton ID="btsave" runat="server" CssClass="btn btn-warning" OnClick="btsave_Click">Save</asp:LinkButton>
                        <asp:LinkButton ID="btprint" runat="server" CssClass="btn btn-info" OnClick="btsave_Click">Print</asp:LinkButton>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>

