<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookup_mst_asset.aspx.cs" Inherits="lookup_mst_asset" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../js/jquery-1.9.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="container">
            <div class="form-horizontal">
                <div class="form-group">
                     <table>
                        <tr>
                            <td>
                                Branch 
                            </td>
                            <td>
                                 <asp:DropDownList ID="cbsalespoint" CssClass="form-control" runat="server" style="width:76%;">
                </asp:DropDownList>
                            </td>
                            </tr>  <tr>
                            <td>Asset Type</td>
                            <td>
                                <asp:DropDownList ID="cbAsset_type" CssClass="form-control" runat="server" style="width:76%;"></asp:DropDownList>
                                <asp:LinkButton ID="btsearch" runat="server" CssClass="btn btn-primary" style="float:right;margin-top:-33px;" OnClick="btsearch_Click">Search</asp:LinkButton>
                            </td>
                           </tr>
                          <tr>
                              <td colspan="2">
                                  </td>
                          </tr>   
                         <tr style="display:none;">
                             <td>
                                 Serach By ID Or Name
                             </td>
                             <td> <asp:TextBox ID="txsearch" runat="server" CssClass="form-control" style="width:76%;"></asp:TextBox>
                                  
                             </td>
                         </tr>
                    </table>
                </div>
                <div class="form-group">
                    <div class="col-md-12">
                           


                        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="mydatagrid" CellPadding="0" 
                            OnPageIndexChanging="grd_PageIndexChanging" OnSelectedIndexChanging="grd_SelectedIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Asset Number">
                            <ItemTemplate>
                                <asp:Label ID="lblAssetno" runat="server" Text='<%#Eval("assetno") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Asset Name">
                            <ItemTemplate>
                                <asp:Label ID="lblAssetNM" runat="server" Text='<%#Eval("asset_nm") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Asset Type">
                            <ItemTemplate>
                                <asp:Label ID="lblAssetTypes" runat="server" Text='<%#Eval("assetTypes") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:CommandField ShowSelectButton="True" HeaderText="Select"  />
                    </Columns>
                     <HeaderStyle CssClass="header"></HeaderStyle>

                    <PagerStyle CssClass="pager"></PagerStyle>

                    <RowStyle CssClass="rows"></RowStyle>
                </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
       
        
    </div>
        <div>
         
        </div>
    </form>
</body>
</html>
