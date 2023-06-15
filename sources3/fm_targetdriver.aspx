<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_targetdriver.aspx.cs" Inherits="fm_targetdriver" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="admin/js/bootstrap.min.js"></script>
    <script src="css/jquery-1.9.1.js"></script>
    <script>
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <h4 class="divheader">Target Driver</h4>
    <div class="h-divider"></div>

    <div class="container">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="">
           
            <div class="clearfix margin-bottom">
                <div class="col-md-6 col-sm-12 no-padding">
                     <label class="control-label col-md-2 col-sm-4">Salespoint</label>
                    <div class="col-md-10 col-sm-8 drop-down">
                        <asp:DropDownList ID="cbsp" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbsp_SelectedIndexChanged" Width="100%"></asp:DropDownList>
                    </div>
                </div> 
                <div class="col-md-6 col-sm-12 no-padding">
                    <label class="control-label col-md-2 col-sm-4">Period</label>
                    <div class="col-md-10 col-sm-8 drop-down">
                        <asp:DropDownList ID="cbperiod" runat="server" CssClass="form-control" OnSelectedIndexChanged="cbperiod_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="clearfix margin-bottom">
                <table class="table table-stipeed mygrid">
                    <tr class="table-header">
                        <th  >Driver</th>
                        <th >Nationality</th>
                        <th >Qty</th>
                        <th  >Qty Weight Rat.</th>
                        <th  >Invoice</th>
                        <th  >Invoice Weight Rat.</th>
                        <th >Add</th>
                    </tr>
                    <tr>
                        <td>
                             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbdriver" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnSelectedIndexChanged="cbdriver_SelectedIndexChanged"></asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cbsp" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtNationality" CssClass="form-control input-sm"  runat="server" Enabled="false"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:TextBox ID="txqty" CssClass="form-control input-sm"  runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtQtyWeight" CssClass="form-control input-sm"  runat="server" Enabled="false"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:TextBox ID="txinvamt" CssClass="form-control input-sm"  runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtInvoiceWeight" CssClass="form-control input-sm"  runat="server" Enabled="false"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:LinkButton ID="btadd" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btadd_Click">Add</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>

         
            <div class="clearfix margin-bottom">
                <div class="col-md-12 no-padding">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grd" runat="server" OnRowDeleting="grd_RowDeleting" AutoGenerateColumns="False" CellPadding="0" CssClass="table table-striped mygrid">
                                <Columns>
                                    <asp:TemplateField HeaderText="Salespoint">
                                        <ItemTemplate><%# Eval("salespointcd") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Period">
                                        <ItemTemplate><%# Eval("period") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdempcode" runat="server" Value='<%# Eval("emp_cd") %>' />
                                            <asp:HiddenField ID="hdfsalespointcd" runat="server" Value='<%# Eval("salespointcd") %>' />
                                            <asp:HiddenField ID="hdfperiod" runat="server" Value='<%# Eval("period") %>' />
                                            <asp:Label ID="lbemp" runat="server" Text='<%# Eval("emp_desc") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate><%# Eval("totqty") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Qty Weight %">
                                        <ItemTemplate><%# Eval("TotalQtyWeight") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tot Invoice">
                                        <ItemTemplate><%# Eval("totinvoice") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Invoice Weight %">
                                        <ItemTemplate><%# Eval("TotalInvoiceWeight") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="true" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbsp" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cbperiod" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>


            <div class="clearfix margin-bottom">
                
                <div class="col-md-offset-6 col-md-2">
                    Total Weight
                </div>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblWeight" runat="server" Text="0"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>


            <div class="clearfix margin-bottom margin-top">
                <div class="text-center">
                    <asp:LinkButton ID="btprint" runat="server" CssClass="btn btn-info" OnClick="btprint_Click">Print</asp:LinkButton>
                    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" CssClass="divhid" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

