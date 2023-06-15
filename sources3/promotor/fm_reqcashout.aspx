<%@ Page Title="" Language="C#" MasterPageFile="~/promotor/promotor2.master" AutoEventWireup="true" CodeFile="fm_reqcashout.aspx.cs" Inherits="promotor_fm_reqcashout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
        function SelectData(sVal)
        {
            $get('<%=hdcashout.ClientID%>').value = sVal;
            $get('<%=lbcashoutno.ClientID%>').value = sVal;
            $get('<%=btlookup.ClientID%>').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <asp:HiddenField ID="hdcashout" runat="server" />
    <div class="form-horizontal">
        <h4 class="jajarangenjang">Cash Out Or Cash In Exhibition</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <label class="control-label col-md-1">Req No</label>
            <div class="col-md-4">
                <div class="input-group">
                     <asp:Label ID="lbcashoutno" CssClass="form-control input-group-sm" runat="server"></asp:Label>
                    <div class="input-group-btn">
                        <asp:LinkButton ID="btsearch" CssClass="btn btn-primary" runat="server"><span class="glyphicon glyphicon-search"></span></asp:LinkButton>
                    </div>
                </div>
               
            </div>
            <label class="control-label col-md-1">Exhibition</label>
            <div class="col-md-4">
                <asp:DropDownList ID="cbexhibition" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>

        </div>
        <div class="form-group">
            <div class="col-md-12">
                <table class="mydatagrid">
                    <tr><th>In Out</th><th>Cashout Type</th><th>Item Cash</th><th>Amount</th><th>Remark</th><th>Action</th></tr>
                    <tr>
                        <td>
                             <asp:DropDownList ID="cbinout" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbinout_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td>
                              <asp:DropDownList ID="cbcashouttype" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbcashouttype_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td>
                             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbitemcashout" CssClass="form-control" runat="server"></asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbcashouttype" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cbinout" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:TextBox ID="txamt" CssClass="form-control" runat="server" Columns="10"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txremark" CssClass="form-control"  Columns="10" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:LinkButton ID="btsave" runat="server" CssClass="btn btn-primary" OnClick="btsave_Click">Add</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-12">
                <asp:GridView ID="grd" runat="server" CssClass="mydatagrid" RowStyle-CssClass="rows" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" AutoGenerateColumns="False" OnRowDeleting="grd_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="Item Cash">
                            <ItemTemplate>
                                <asp:Label ID="lbitemcode" runat="server" Text='<%#Eval("itemco_cd") %>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Name">
                            <ItemTemplate><%#Eval("itemco_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="In Out"><ItemTemplate><%#Eval("inout")%></ItemTemplate></asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate><%#Eval("amt") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" HeaderText="Delete" />
                    </Columns>

<HeaderStyle CssClass="header"></HeaderStyle>

<PagerStyle CssClass="pager"></PagerStyle>

<RowStyle CssClass="rows"></RowStyle>
                </asp:GridView>
            </div>
        </div>
        <div class="form-group">
           <div class="col-md-12" style="text-align:center">
                   <asp:LinkButton ID="btnew" CssClass="btn btn-info" runat="server" OnClick="btnew_Click">New</asp:LinkButton>
               <asp:LinkButton ID="btsaved" CssClass="btn btn-primary" runat="server" OnClick="btsaved_Click">Save</asp:LinkButton>
                   <asp:LinkButton ID="btprint" CssClass="btn btn-danger" runat="server" OnClick="btprint_Click">Print</asp:LinkButton>
                   <asp:Button ID="btlookup" runat="server" OnClick="btlookup_Click" Text="Button" style="display:none" />
           </div>
        </div>
    </div>
      <script>
          $(document).ready(function () {
              $("#<%=btsearch.ClientID%>").click(function () {
            PopupCenter('lookupcashout.aspx', 'xtf', '900', '500');
        });
    });


    </script>  
</asp:Content>

