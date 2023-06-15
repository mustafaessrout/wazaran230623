<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstitementry.aspx.cs" Inherits="fm_mstitementry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
      <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="css/jquery-1.9.1.js"></script>
    <script src="css/jquery-ui.js"></script>
     <script type="text/javascript">
         $(function () {
             $("#<%=dteffective.ClientID%>").datepicker({ dateFormat: "mm/dd/yy" }).val();
             $("#<%=dtExpired.ClientID%>").datepicker({ dateFormat: "mm/dd/yy" }).val();
         });

</script>
      <style type="text/css">
          .auto-style1 {
              width: 357px;
          }
          .auto-style2 {
              height: 20px;
          }
          .auto-style3 {
              width: 310px;
          }
          .auto-style4 {
              height: 36px;
          }
          .auto-style5 {
              width: 310px;
              height: 36px;
          }
      </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>Entry Master Item</h3>
    <table style="width: 100%; font-size: small;">
        <tr>
            <td >
                Brand</td>
            <td>:</td>
            <td class="auto-style3">
                <asp:DropDownList ID="cbbrand" runat="server" Height="16px" Width="216px" AutoPostBack="true" OnSelectedIndexChanged="cbbrand_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td rowspan="8">
                <asp:Image ID="img" runat="server" Height="212px" style="margin-left: 0px" ImageUrl="~/noimage.jpg" Width="251px" />
            </td>
        </tr>
        <tr>
            <td >
                Group</td>
            <td>:</td>
            <td class="auto-style3">
                <asp:UpdatePanel ID="updpnlprod" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbprod" runat="server" Height="17px" Width="214px" AutoPostBack="true" OnSelectedIndexChanged="cbprod_SelectedIndexChanged">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers><asp:AsyncPostBackTrigger ControlID="cbbrand" EventName="SelectedIndexChanged" /></Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td >
                &nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style3">
                <asp:UpdatePanel ID="Updateprod" runat="server">
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="auto-style2" colspan="3"><hr style="color:blue>"</td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lbitemcode" runat="server" Text="Item Code"></asp:Label>
            </td>
            <td>:</td>
            <td class="auto-style3">
                <asp:TextBox ID="txitemcode" runat="server" Width="215px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lbitemname" runat="server" Text="Item Name"></asp:Label>
            </td>
            <td>:</td>
            <td class="auto-style3">
                <asp:TextBox ID="txitemname" runat="server" Width="212px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lbshortname" runat="server" Text="Short Name"></asp:Label>
            </td>
            <td>:</td>
            <td class="auto-style3">
                <asp:TextBox ID="txshortname" runat="server" Width="212px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lbalias" runat="server" Text="Item alias (arabic)"></asp:Label>
            </td>
            <td>:</td>
            <td class="auto-style3">
                <asp:TextBox ID="txarabic" runat="server" Width="211px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td >&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td rowspan="5">
                <table style="width:100%;">
                    <tr>
                        <td>Min Stock</td>
                        <td>:</td>
                        <td>
                <asp:TextBox ID="txminstock" runat="server" Width="58px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">Size</td>
                        <td class="auto-style2">:</td>
                        <td class="auto-style2">
                <asp:TextBox ID="txsize" runat="server" Width="56px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Unit Price</td>
                        <td>:</td>
                        <td>
                <asp:TextBox ID="txunitprice" runat="server" Width="93px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td >&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
        </tr>
        <tr>
            <td >Effective Date</td>
            <td>:</td>
            <td class="auto-style3">
                            <asp:TextBox ID="dteffective" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td >Expired Date</td>
            <td>:</td>
            <td class="auto-style3">
                            <asp:TextBox ID="dtExpired" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td >UOM</td>
            <td>:</td>
            <td class="auto-style3">
                <asp:DropDownList ID="cbuom" runat="server" Height="16px" Width="218px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td >Item Code Vendor</td>
            <td class="auto-style5">:</td>
            <td class="auto-style3">
                <asp:TextBox ID="txcodevendor" runat="server" Width="215px"></asp:TextBox>
            </td>
            <td class="auto-style5">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style4" >Vendor Code</td>
            <td class="auto-style4">:</td>
            <td class="auto-style5">
                <asp:DropDownList ID="cbvendor" runat="server" Height="16px" Width="222px">
                </asp:DropDownList>
            </td>
            <td class="auto-style4">
                </td>
        </tr>
        <tr>
            <td >Image</td>
            <td>:</td>
            <td class="auto-style3">
                <asp:FileUpload ID="uplitem" runat="server" Height="23px" Width="215px" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td >&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td ></td>
            <td class="auto-style5"></td>
            <td class="auto-style3">
                <strong>ITEM SELLING TO SALESPOINT</strong></td>
            <td class="auto-style5">
                </td>
        </tr>
        <tr>
            <td ></td>
            <td class="auto-style5"></td>
            <td class="auto-style22" colspan="2">
               <div style="border-bottom:1px solid blue;border-top:1px dashed blue"> Sales Point <asp:DropDownList ID="cbsalespoint" runat="server" Height="16px" Width="285px"></asp:DropDownList>    
                <asp:Button ID="btadd" runat="server"  Text="&gt;&gt;&gt;" OnClick="btadd_Click" /></div>
            </td>
        </tr>
        <tr>
            <td >&nbsp;</td>
            <td>&nbsp;</td>
            <td colspan="2">
                <asp:UpdatePanel ID="updpnlsp" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grdsp" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCommand="grdsp_RowCommand" OnSelectedIndexChanging="grdsp_SelectedIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="SalesPoint Code"><ItemTemplate>
                                    <asp:Label ID="lbsalespointcd" runat="server" Text='<%# Eval("salespointcd") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                <asp:TemplateField HeaderText="Name"><ItemTemplate>
                                    <asp:Label ID="lbsalespoint" runat="server" Text='<%# Eval("salespoint_nm") %>'></asp:Label>
                                                                     </ItemTemplate></asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" /></ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers><asp:AsyncPostBackTrigger ControlID="btadd" EventName="Click"/></Triggers>
                </asp:UpdatePanel>
               <div style="text-align:right">
                   <asp:Button ID="btdel" runat="server" Text="DELETE" OnClick="btdel_Click" /></div>
                <span class="auto-style20"><strong>ITEM SELLING TO CUSTOMER</strong></span><br />
                <asp:GridView ID="grdcust" runat="server" AutoGenerateColumns="False" Width="100%" BorderStyle="None" GridLines="Horizontal">
                    <Columns>
                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate><%# Eval("fld_valu") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer Type">
                            <ItemTemplate><%# Eval("fld_desc") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pricing">
                            <ItemTemplate>
                                <asp:UpdatePanel ID="updpricing" runat="server">
                                   <ContentTemplate>
                                       SAR <asp:TextBox ID="txpricing" Text="0" runat="server" Width="100px"></asp:TextBox></ContentTemplate>
                                   <Triggers><asp:AsyncPostBackTrigger  ControlID="chkcust"/></Triggers>     
                                </asp:UpdatePanel>   
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Selected">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkcust" runat="server" Checked="true" /></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="auto-style2" colspan="4"><hr style="width:100%;color:blue" /></td>
        </tr>
     
       
        <tr>
            <td >&nbsp;</td>
            <td class="auto-style5">&nbsp;</td>
            <td class="auto-style3">&nbsp;</td>
            <td class="auto-style5">&nbsp;</td>
        </tr>
        <tr>
            <td >&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style3">
                <div><asp:Button ID="btsave" runat="server" Text="Save" Width="90px" OnClick="btsave_Click" CssClass="auto-style21" Height="28px" /></div>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>

</asp:Content>

