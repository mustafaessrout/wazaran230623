<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_prodsupport.aspx.cs" Inherits="fm_prodsupport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%--<link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="css/jquery-1.9.1.js"></script>
    <script src="css/jquery-ui.js"></script>--%>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    
    <script>
        function openwindow() {
            var oNewWindow = window.open("fm_lookup_prodsupport.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }

        function updpnl() {
            document.getElementById('<%=bttmp.ClientID%>').click();
            return (false);
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 206px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        Production Support Entry -  <asp:Label ID="lbsp" runat="server" CssClass="text-bold text-red" />
    </div>
    <div class="h-divider"></div>

    <div class="container">
        <div class="row margin-bottom">
            <div class="margin-bottom">
                <label class="control-label col-md-1 titik-dua">Status </label>
                <div class="col-md-5 drop-down">
                    <asp:DropDownList ID="cbsup_sta_id" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <table class="table table-striped table-bordered table-condensed mygrid">
                <tr>
                    <td>Support No.</td>
                    <td>:</td>
                    <td class="col-md-2">
                        
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="input-group">
                                     <asp:TextBox ID="txsup_no" runat="server" CssClass="form-control input-sm ro" Enabled="false" ReadOnly="True"></asp:TextBox>
                                     <div class="input-group-btn">
                                         <asp:Button ID="btsearch" runat="server" CssClass="btn-primary btn btn-sm btn-search" Text="Search" OnClientClick="openwindow();return(false);" />
                                     </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:Button ID="bttmp" runat="server" Text="Button" OnClick="bttmp_Click" style="display:none" />
                    </td>
                    <td>Date  </td>   <td>:</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtsup_dt" runat="server" ReadOnly="True" Enabled="false" CssClass="form-control input-sm ro"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>Time  </td><td>:</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtsup_time" runat="server" ReadOnly="True" Enabled="false" CssClass="form-control input-sm ro"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>Menu     </td><td>:</td>
                    <td class="auto-style1">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbmenu" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnSelectedIndexChanged="cbmenu_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>Sub Menu</td>
                    <td>:</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbmenu_sub" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnSelectedIndexChanged="cbmenu_sub_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>Trans Menu</td>
                    <td>:</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbmenu_transaction" runat="server" AutoPostBack="True" CssClass="form-control input-sm">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>Trans Type</td>
                    <td>:</td>
                    <td class="auto-style1">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbsup_tran_type" runat="server" CssClass="form-control input-sm">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>Trans No.</td>
                    <td>:</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtran_no" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>Description</td>
                    <td>:</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txdescription" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>Creator</td>
                    <td>:</td>
                    <td class="auto-style1">
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txcreator" runat="server" ReadOnly="True" Enabled="false" CssClass="form-control input-sm ro"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>Approval</td><td>:</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbapproval" runat="server" CssClass="form-control input-sm">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>PIC</td>
                    <td>:</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbsup_pic" runat="server" CssClass="form-control input-sm">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>Supp Date</td>
                    <td>:</td>
                    <td class="auto-style1">
                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtsup_dt_finish" runat="server" ReadOnly="True" Enabled="false"  CssClass="form-control input-sm ro"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>Supp Time</td>     <td>:</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtsup_time_finish" runat="server" ReadOnly="True" Enabled="false" CssClass="form-control input-sm ro"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>Remark     </td><td>:</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txremark" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        Error By
                    </td>
                    <td>:</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cberrby" runat="server">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div class="row">
            <div class="col-sm-4">
                <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                <ContentTemplate>
                    <asp:FileUpload ID="upl" runat="server" ClientIDMode="Static" />
                </ContentTemplate>
                </asp:UpdatePanel>
             </div>
             <div class="col-sm-2">
                <%--<asp:Button ID="btadd" runat="server" CssClass="btn btn-primary btn-sm glyphicon glyphicon-plus" OnClick="btadd_Click" Text="Add" />--%>
                 <%--<asp:Button ID="Button1" runat="server" OnClick="btadd_Click" Text="Add" />--%>
                  <span class="input-group-btn"><button type="submit" class="btn btn-primary btn-sm" runat="server" id="btadd" onserverclick="btadd_Click">Add <span class="glyphicon glyphicon-plus" aria-hidden="true"></span></button> </span>
             </div>
          </div>
        <div class="row">
            <div class="text-bold text-lg">Document Related</div>
            <div>
                 <%--<asp:UpdatePanel ID="UpdatePanel16" runat="server">--%>
                    <%--<ContentTemplate>--%>
                        <asp:GridView ID="grddoc" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mygrid" CellPadding="0" GridLines="None" PageSize="5"  ShowFooter="True" OnRowCancelingEdit="grddoc_RowCancelingEdit" OnRowEditing="grddoc_RowEditing" OnRowUpdating="grddoc_RowUpdating" OnRowDeleting="grddoc_RowDeleting">
                            <AlternatingRowStyle  />
                            <Columns>
                                <asp:TemplateField HeaderText="Document Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbsup_no" runat="server" Text='<%# Eval("sup_no") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SalespointCD">
                                    <ItemTemplate>
                                        <asp:Label ID="lbsalespointcd" runat="server" Text='<%# Eval("salespointcd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UploadImage">
                                    <ItemTemplate>
                                        <asp:Image ImageUrl='<%# Eval("filename") %>' runat="server" ID="image" />
                                        <a class="example-image-link" data-lightbox='example-1<%# Eval("filename") %>' href='/images/prodsupport/<%# Eval("filename") %>'>
                                        <asp:Label ID="lbfilename" runat="server" Text='<%# Eval("filename") %>'></asp:Label>
                                        </a>
                                    </ItemTemplate>
                                    <ItemTemplate>
                                        <a class="example-image-link" href="/images/prodsupport/<%# Eval("filename") %>" data-lightbox="example-1<%# Eval("filename") %>">
                                        <asp:Label ID="lbfilename" runat="server" Text='<%# Eval("filename") %>'></asp:Label>
                                        </a>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:FileUpload ID="FileUpload1" runat="server" />

                                        <%--</asp:UpdatePanel>--%>
                                    
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField visible="false">
                                    <ItemTemplate>
                                    <asp:Label ID="hdnfilename" runat="server" Text='<%# Eval("filename") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                            </Columns>
                            <EditRowStyle CssClass="table-edit" />
                            <FooterStyle CssClass="table-footer" />
                            <HeaderStyle CssClass="table-header" />
                            <PagerStyle CssClass="table-page" />
                            <RowStyle />
                            <SelectedRowStyle CssClass="table-edit" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    <%--</ContentTemplate>--%>
                    <%--<Triggers>
                        <asp:PostBackTrigger ControlID="FileUpload1" />
                    </Triggers>
                </asp:UpdatePanel>--%>
            </div>
        </div>
        <div class="row navi margin-bottom">
    
            <%--<asp:UpdatePanel ID="UpdatePanel18" runat="server">
                <ContentTemplate>--%>
                    <%--<asp:Button ID="btnew" runat="server" CssClass="button2 add" OnClick="btnew_Click" Text="NEW" />--%>
                    <%--<asp:Button ID="btsave" runat="server" CssClass="button2 save" OnClick="btsave_Click" Text="Save" OnClientClick="dvshow.setAttribute('class','divmsg');"/>--%>

                    <%--<asp:Button ID="btDelete" runat="server" CssClass="button2 delete" OnClick="btDelete_Click" Text="Delete" Visible="False" />--%>
                    <button type="submit" class="btn btn-success " runat="server" id="btnew" onserverclick="btnew_Click">New <span class="fa  fa-file" aria-hidden="true"></span></button> 
                    <button type="submit" class="btn btn-warning " runat="server" id="btsave" onserverclick="btsave_Click">Save <span class="fa fa-save" aria-hidden="true"></span></button>                    
                <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
    
        </div>
    </div>

    <div class="container-fluid">

        <div class="divmsg loading-cont" id="dvshow">
            <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
        </div>
    </div>
</asp:Content>

