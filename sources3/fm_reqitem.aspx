<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_reqitem.aspx.cs" Inherits="fm_reqitem" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
        #newPreview
        {
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale);
        }
    </style>
    <script type="text/javascript">
        function previewFile() {
            var preview = document.querySelector('#<%=Avatar.ClientID %>');
            var file = document.querySelector('#<%=avatarUpload.ClientID %>').files[0];
            var reader = new FileReader();
            $get('<%=upl.ClientID%>').filename = preview.value;
            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }
    </script>
  </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Request New Item</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row ">
            
            <div class="clearfix form-group col-sm-6 col-xs-12 pull-left">
                <label class="control-label col-sm-2">Branded</label>
                <div class="col-sm-10 drop-down margin-bottom">
                    <asp:DropDownList ID="cbbranded" runat="server" OnSelectedIndexChanged="cbbranded_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                </div>

                <label class="control-label col-sm-2">Product Group</label>
                <div class="col-sm-10 drop-down margin-bottom">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                            <asp:DropDownList ID="cbprodgroup" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbprodgroup_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                    </ContentTemplate>  
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbbranded" EventName="SelectedIndexChanged" />
                        </Triggers>   
                    </asp:UpdatePanel>
                </div>

                <label class="control-label col-sm-2">Type</label>
                <div class="col-sm-10 drop-down margin-bottom">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                                <asp:DropDownList ID="cbproduct" runat="server" CssClass="form-control"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbprodgroup" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

                <label class="control-label col-sm-2">Product Type</label>
                <div class="col-sm-10 drop-down margin-bottom">
                    <asp:DropDownList ID="cbprodtype" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            


            <div class="col-sm-6 col-xs-12 margin-bottom pull-right text-center padding-top">
                <div class="margin-bottom">
                    <asp:Image ID="Avatar" runat="server" Height="100" ImageUrl="~/noimage.jpg" Width="100" CssClass="img-circle" style="border: 1px solid #555;" AlternateText="avatar-photo" />
                </div>
                <div class="clearfix form-group">
                    <div class="col-sm-offset-2 col-sm-10" style="height:34px;">
                        <input id="avatarUpload" type="file" name="file" onchange="previewFile()"  runat="server" />
                        <asp:FileUpload ID="upl" runat="server" style="display:none" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-12 clearfix form-group">
                <label class="control-label col-sm-1" >No. Request</label>
                <div class="col-sm-11">
                    <asp:TextBox ID="txrequestno" runat="server" CssClass="makeitreadonly ro form-control" Enabled="false"></asp:TextBox>
                </div>
            </div>

            <div class="col-sm-6 clearfix form-group">
                <label class="control-label col-sm-2" >Item Name</label>
                <div class="col-sm-10">
                    <asp:TextBox CssClass="form-control"  ID="txitemname" runat="server" ></asp:TextBox>
                </div>
            </div>

            <div class="col-sm-6 clearfix form-group">
                <label class="control-label col-sm-2" >Size</label>
                <div class="col-sm-10">
                    <asp:TextBox CssClass="form-control"  ID="txsize" runat="server"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txsize_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txsize" UseContextKey="True" CompletionListElementID="divwidth" CompletionListItemCssClass="AutoExtender" CompletionListCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" EnableCaching="false" CompletionInterval="10" CompletionSetCount="1" FirstRowSelected="false">
                    </asp:AutoCompleteExtender>
                    <div id="divwidth"></div>  
                </div>
            </div>

            <div class="col-sm-6 clearfix form-group">
                <label class="control-label col-sm-2" >Arabic</label>
                <div class="col-sm-10">
                    <asp:TextBox CssClass="form-control"  ID="txarabic" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="col-sm-6 clearfix form-group">
                <label class="control-label col-sm-2" >UOM</label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList CssClass="form-control" ID="cbuom" runat="server"></asp:DropDownList>
                </div>
            </div>

            <div class="col-sm-6 clearfix form-group">
                <label class="control-label col-sm-2" >Packing</label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList CssClass="form-control" ID="cbpacking" runat="server"></asp:DropDownList>
                </div>
            </div>

            

            <div class="col-sm-6 clearfix form-group">
                <label class="control-label col-sm-2" >Vendor Price</label>
                <div class="col-sm-10">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                              <asp:TextBox CssClass="form-control"  ID="txbuyprice" runat="server"></asp:TextBox>
                             <asp:AutoCompleteExtender ID="txbuyprice_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txbuyprice" UseContextKey="True" CompletionListElementID="divwidth" CompletionListCssClass="AutoExtenderList" CompletionListItemCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" EnableCaching="false" CompletionInterval="10" CompletionSetCount="1" FirstRowSelected="false">
                    </asp:AutoCompleteExtender>
                    <asp:RequiredFieldValidator  ID="RequiredFieldValidator1" runat="server" ControlToValidate="txbuyprice" ErrorMessage="/**" Font-Bold="True" ForeColor="#FF3300" style="height: 0;display: block;"></asp:RequiredFieldValidator>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="col-sm-6 clearfix form-group">
                <label class="control-label col-sm-2" >Remark</label>
                <div class="col-sm-10">
                    <asp:TextBox CssClass="form-control"  ID="txremark" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="col-sm-6 clearfix form-group">
                <label class="control-label col-sm-2" >Vendor</label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList ID="cbvendor" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>

            <div class="col-sm-6 clearfix form-group" style="margin-bottom:10px;">
                <label class="control-label col-sm-2" >Barcode Cartoon</label>
                <div class="col-sm-10">
                    <asp:TextBox CssClass="form-control"  ID="txbarcodectn" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="col-sm-6 clearfix form-group">
                <label class="control-label col-sm-2" >Barcode Box</label>
                <div class="col-sm-10">
                    <asp:TextBox CssClass="form-control"  ID="txbarcodebox" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="col-sm-6 clearfix form-group">
                <label class="control-label col-sm-2" >Barcode Product</label>
                <div class="col-sm-10">
                    <asp:TextBox CssClass="form-control"  ID="txbarcodeprod" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="col-sm-12 clearfix form-group margin-top well " style="padding:15px;">
                <p class="text-bold col-sm-12" >Dimension</p>
                
                <div class="col-sm-4">
                    <label class="control-label ">Length</label>
                    <div class="">
                        <asp:TextBox CssClass="form-control"  ID="txlength" runat="server" ></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-4">
                    <label class="control-label ">Width</label>
                    <div class="">
                        <asp:TextBox CssClass="form-control"  ID="txwidth" runat="server" ></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-4">
                    <label class="control-label ">Depth</label>
                    <div class="">
                        <asp:TextBox CssClass="form-control"  ID="txdepth" runat="server" ></asp:TextBox>
                    </div>
                </div>
               
            </div>
        </div>

        
    </div>

    <div class="h-divider"></div>

     <div class="container-fluid">
        <div class="row">
            <table class="table table-striped mygrid row-no-padding">
                <tr >
                    <th>Customer Type</th>
                    <th>All</th>
                    <th>Price</th>
                    <th>Start Date</th>
                    <th>Add</th>
                </tr>

                <tr>
                    <td class="drop-down-sm ">
                        <asp:DropDownList ID="cbcusttype" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                    </td>
                    <td >
                        <div class="checkbox no-margin">
                            <asp:CheckBox ID="chkallcusttype" runat="server" Text="All" CssClass=" no-margin"/>
                        </div>
                    </td>
                    <td>
                          <asp:TextBox CssClass="form-control  input-sm"  ID="txprice" runat="server" ></asp:TextBox>
                    </td>
                    <td >
                       
                            <asp:TextBox CssClass="form-control input-sm"  ID="dtstart" runat="server"></asp:TextBox>
                            <asp:CalendarExtender CssClass="date" ID="dtstart_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtstart">
                            </asp:CalendarExtender>
                       
                    </td>
                    <td style="width:90px;">
                         <asp:Button ID="btaddcusttype" runat="server" Text="Add" CssClass="btn-success btn-sm btn-block btn btn-add" OnClick="btaddcusttype_Click" />
                    </td>
                </tr>
            </table>
        </div>
        
     
    </div>


    <div class="container-fluid">
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                     <asp:GridView ID="grdcusttyp" runat="server" AutoGenerateColumns="False" GridLines="None" AllowPaging="True" PageSize="5" CssClass="table table-striped mygrid top-devider">
                <AlternatingRowStyle  />
                <Columns>
                    <asp:TemplateField HeaderText="Code">
                        <ItemTemplate>
                            <asp:Label ID="lbcode" runat="server" Text='<%# Eval("otlcd") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate><%# Eval("otl_nm") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Price">
                        <ItemTemplate><%# Eval("sell_price") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start Date">
                        <ItemTemplate>
                            <%# Eval("start_dt","{0:d/M/yyyy}") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="True" />
                </Columns>
                <EditRowStyle CssClass="table-edit" />
                <FooterStyle CssClass="table-footer" />
                <HeaderStyle CssClass="table-header" />
                <PagerStyle CssClass="table-page" />
                <RowStyle  />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btaddcusttype" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div> 
    </div>

    <div class="h-divider"></div>
    <div class="container-fluid margin-bottom">
        <div class="row">
            <div class="clearfix form-group">
                <label class="control-label col-sm-2">Target Market</label>
                <div class="col-md-4 col-sm-6 drop-down">
                    <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-sm-1 checkbox no-margin-top">
                    <asp:CheckBox ID="chk" runat="server" CssClass=" no-margin" Text="All" />
                </div>
                <div class="col-md-1 col-sm-2">
                    <asp:Button ID="btspadd" runat="server" Text="Add" CssClass="btn-success btn btn-add" OnClick="btspadd_Click" />
                </div>
                
            </div>
        </div>
        
    </div>
    
    
    <div class="container-fluid">
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grdsp" CssClass="table table-striped mygrid top-devider" runat="server" AutoGenerateColumns="False"  AllowPaging="True" CellPadding="2" GridLines="None" OnRowDeleting="grdsp_RowDeleting" PageSize="5">
                        <AlternatingRowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Salespoint Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbsalespointcd" runat="server" Text='<%# Eval("salespointcd") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salespoint Name">
                                <ItemTemplate><%# Eval("salespoint_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Addr"></asp:TemplateField>
                            <asp:TemplateField HeaderText="City"></asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page" />
                        <RowStyle  />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btspadd" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        
    </div>

    <div class="h-divider"></div>
    <div class="container-fluid">
        <div class="navi margin-top margin-bottom row">
            <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn-warning btn btn-save" OnClick="btsave_Click" />
        </div>
    </div>
    
</asp:Content>

