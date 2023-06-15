<%@ Page Title="" Language="C#" MasterPageFile="~/admin/promotor2.master" AutoEventWireup="true" CodeFile="fm_carEntry.aspx.cs" Inherits="fm_carEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" runat="Server">
    <div class="container">
        <h4 class="">Master Car Entry</h4>
        <div class="h-divider"></div>
        <div class="row">
            <h5 class="jajarangenjang" style="font-size:large;">Car Brand</h5>
        </div>
        <div class="h-divider"></div>
        <div class="row">
            <label class="control-label col-md-1">Car Brand</label>
            <div class="col-md-2">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtcarBrand" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                        <asp:HiddenField ID="hddcarbrand_cd" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="Hddcarbrand_pics" runat="server"></asp:HiddenField>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">Car Brand Arabic</label>
            <div class="col-md-2">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtcarBrandAr" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <label class="control-label col-md-2">Upload Car Brand Photo</label>
            <div class="col-md-2">
                <asp:UpdatePanel ID="UpPnCarBrand" runat="server">
                    <ContentTemplate>
                        <asp:FileUpload ID="upCarBrand" runat="server" />

                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btSaveBrand" />
                        <asp:PostBackTrigger ControlID="btUpdateBrand" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-4">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <label id="lblCarbrandPhoto" class="control-label col-md-3 align-right" runat="server" title=""></label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="button-group">
            <div class="align-center">
                <asp:UpdatePanel ID="UpdatePanelbtn" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btSaveBrand" runat="server" Text="Save Car Brand" CssClass="btn btn-sm btn-success" OnClick="btSaveBrand_Click" style="width:  inherit; height: 30px;  font-size:small;" />
                        <asp:Button ID="btUpdateBrand" runat="server" Text="Update Car Brand" CssClass="btn btn-sm btn-success" OnClick="btUpdateBrand_Click" style="width:  inherit; height: 30px;  font-size:small;" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="row">
            <h5 class="jajarangenjang" style="font-size:large;">Car Model</h5>
        </div>
        <div class="h-divider"></div>
        <div class="row">
            <label class="control-label col-md-1">Car Brand</label>
            <div class="col-md-2 drop-down">
                <asp:UpdatePanel ID="UpdatePanel57" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlCarBrand" AutoPostBack="true" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">Car Model</label>
            <div class="col-md-2 ">
                <asp:UpdatePanel ID="UpdatePanel72" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtCarModel" runat="server" Text="" CssClass="form-control input-sm"></asp:TextBox>
                        <asp:HiddenField ID="Hddcarmodel_cd" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="Hddcarmodel_pics" runat="server"></asp:HiddenField>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">Car Model Arabic</label>
            <div class="col-md-2">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtCarModelAr" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <label class="control-label col-md-2">Upload Car Modal Photo</label>
            <div class="col-md-2">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <asp:FileUpload ID="upCarModel" runat="server" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btSaveModel" />
                        <asp:PostBackTrigger ControlID="btUpdateModel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>


            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <label id="lblCarModelPhoto" class="control-label col-md-3 align-center" runat="server" title=""></label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>


        <div class=" form-group">
            <div class="align-center">
                <asp:UpdatePanel ID="UpdatePanebtn2" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btNew" runat="server" Text="New" CssClass="btn btn-sm btn-primary" OnClick="btNew_Click"  style="width:  inherit; height: 30px;  font-size:small;"/>
                        <asp:Button ID="btSaveModel" runat="server" Text="Save Car Model" CssClass="btn btn-sm btn-success" OnClick="SaveModel_Click" style="width:  inherit; height: 30px;  font-size:small;" />
                        <asp:Button ID="btUpdateModel" runat="server" Text="Update Car Model" CssClass="btn btn-sm btn-success" OnClick="btUpdateModel_Click" style="width:  inherit; height: 30px;  font-size:small;" />
                        <asp:Button ID="btShowData" runat="server" Text="Show Data" CssClass="btn btn-sm btn-primary" UseSubmitBehavior="true" OnClick="btShowData_Click"  style="width:  inherit; height: 30px;  font-size:small;" />
                        <asp:Button ID="btrefresh" runat="server" Text="Show Data" CssClass="btn btn-sm btn-primary" UseSubmitBehavior="true" OnClick="btrefresh_Click" Visible="false" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>



        <div class="row">
            <div class="col-md-13">
                <asp:UpdatePanel ID="grdv" runat="server" class="table" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="table-page-fixer">
                            <h5 id="header" runat="server" class="jajarangenjang" style="font-size:large;">Car Brands and Models</h5>
                            <div id="divider" runat="server" class="h-divider"></div>
                            <div class="overflow-y relative" style="max-height: 550px;">
                                <asp:GridView ID="grd"
                                    runat="server" CssClass="mygrid"
                                    AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="grd_PageIndexChanging" BorderColor="SlateBlue" CellPadding="2" Width="100%" PageSize="10"
                                    OnRowDataBound="grd_RowDataBound" OnRowCommand="grd_RowCommand" OnSelectedIndexChanging="grd_SelectedIndexChanging"
                                    OnRowUpdating="grd_RowUpdating">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Ser#">
                                            <ItemTemplate>
                                                <asp:Label ID="SerNo" runat="server" Text='<%#Container.DataItemIndex+1%>'>'</asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Car Brand">
                                            <ItemTemplate>
                                                <asp:Label ID="CarBrand" runat="server" Text='<%# Convert.ToString(Eval("carbrand_nm").ToString() == "" ? "N/A" : Eval("carbrand_nm")) %>'></asp:Label>
                                                <asp:HiddenField ID="hdcarbrand_cd" runat="server" Value='<%#Eval("carbrand_cd") %>'></asp:HiddenField>
                                                <%--ForeColor='<%# (Eval("carbrand_nm").ToString() == "" ? System.Drawing.Color.Red : System.Drawing.Color.Blue) %>'
                                                    Style="text-decoration: none; font: bold" ></asp:Label>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Car Brand Arabic">
                                            <ItemTemplate>
                                                <asp:Label ID="CarBrandAr" runat="server" Text='<%# Convert.ToString(Eval("carbrand_arabic").ToString() == "" ? "N/A" : Eval("carbrand_arabic")) %>'></asp:Label>
                                                <%--    ForeColor='<%# (Eval("carbrand_arabic").ToString() == "" ? System.Drawing.Color.Red : System.Drawing.Color.Blue) %>'
                                                    Style="text-decoration: none; font: bold" ></asp:Label>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Car Brand Photo">
                                            <ItemTemplate>
                                                <%--<a class="example-image-link" id="<%#Eval("carbrand_pics") %>" onclick="openPopup('<%# Eval("carbrand_pics") %>')" href="#" style="color: blue;">--%>
                                                <asp:HyperLink runat="server"
                                                    NavigateUrl='<%# string.Format("~/images/Cars/{0}",   
                                                    HttpUtility.UrlEncode(Eval("carbrand_pics").ToString())) %>'
                                                    Target="_blank"
                                                    Enabled='<%# Convert.ToBoolean(Eval("carbrand_pics").ToString() != "" ? "True" : "False") %>'
                                                    Text='<%# Convert.ToString(Eval("carbrand_pics").ToString() == "" ? "N/A" : Eval("carbrand_pics")) %>'
                                                    ForeColor='<%# (Eval("carbrand_pics").ToString() == "" ? System.Drawing.Color.Red : System.Drawing.Color.Blue) %>'
                                                    Style="text-decoration: none; font: bold" />
                                                <asp:HiddenField ID="hdcarbrand_pics" runat="server" Value='<%#Eval("carbrand_pics") %>'></asp:HiddenField>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Car Model">
                                            <ItemTemplate>
                                                <asp:Label ID="CarModel" runat="server" Text='<%# Convert.ToString(Eval("carmodel_nm").ToString() == "" ? "N/A" : Eval("carmodel_nm")) %>'></asp:Label>
                                                <asp:HiddenField ID="hdcarmodel_cd" runat="server" Value='<%#Eval("carmodel_cd") %>'></asp:HiddenField>
                                                <%--ForeColor='<%# (Eval("carmodel_nm").ToString() == "" ? System.Drawing.Color.Red : System.Drawing.Color.Blue) %>'
                                                    Style="text-decoration: none; font: bold" ></asp:Label>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Car Model Arabic">
                                            <ItemTemplate>
                                                <asp:Label ID="CarModelAr" runat="server" Text='<%# Convert.ToString(Eval("carmodel_arabic").ToString() == "" ? "N/A" : Eval("carmodel_arabic")) %>'></asp:Label>
                                                <%--ForeColor='<%# (Eval("carmodel_arabic").ToString() == "" ? System.Drawing.Color.Red : System.Drawing.Color.Blue) %>'
                                                    Style="text-decoration: none; font: bold" ></asp:Label>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Car Model Photo">
                                            <ItemTemplate>
                                                <%-- <a class="example-image-link" id="<%#Eval("carmodel_pics") %>" onclick="openPopup('<%# Eval("carmodel_pics") %>')" href="#" style="color: blue;">
                                                    <asp:Label ID="lbfileloc" runat="server" Text='Photo'></asp:Label>
                                                </a>--%>
                                                <asp:HyperLink runat="server"
                                                    NavigateUrl='<%# string.Format("~/images/Cars/{0}",   
                                                    HttpUtility.UrlEncode(Eval("carmodel_pics").ToString())) %>'
                                                    Target="_blank"
                                                    Enabled='<%# Convert.ToBoolean(Eval("carmodel_pics").ToString() != "" ? "True" : "False") %>'
                                                    Text='<%# Convert.ToString(Eval("carmodel_pics").ToString() == "" ? "N/A" : Eval("carmodel_pics")) %>'
                                                    ForeColor='<%# (Eval("carmodel_pics").ToString() == "" ? System.Drawing.Color.Red : System.Drawing.Color.Blue) %>'
                                                    Style="text-decoration: none; font: bold" />
                                                <asp:HiddenField ID="hdcarmodel_pics" runat="server" Value='<%#Eval("carmodel_pics") %>'></asp:HiddenField>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete Brand">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkBtnDeleteBranch" runat="server" CommandName="DeleteBrand" CommandArgument='<%#Eval("carbrand_cd")%>'>Delete </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete Model">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkBtnDeleteModel" runat="server" CommandName="DeleteModel" CommandArgument='<%#Eval("carmodel_cd")%>'>Delete </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowSelectButton="true" HeaderText="Edit">
                                            <ControlStyle CssClass="modMarketAdjust" />
                                        </asp:CommandField>
                                    </Columns>
                                    <PagerStyle CssClass="table-page" />
                                    <EditRowStyle CssClass="table-edit" />
                                    <HeaderStyle CssClass="table-header" />
                                    <RowStyle />
                                    <SelectedRowStyle CssClass="table-edit" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
