<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_sopschedule.aspx.cs" Inherits="fm_sopschedule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
   <style>
       .treeview{
           border:none !important;
       }
       .treeview td:last-child{
           width:100% !important;
       }
       .treeview td a{
           padding: 5px;
           display:block;
       }
   </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Stock Opname Scheduling</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row clearfix">
            <div class="col-sm-11 no-padding clearfix">
                <div class="col-sm-3 no-padding margin-bottom clearfix">
                    <label class="control-label col-sm-4 titik-dua">Job</label>
                    <div class="col-sm-8  drop-down">
                        <asp:DropDownList ID="cbjob" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3 no-padding margin-bottom clearfix">
                    <label class="control-label col-sm-4 titik-dua">Scheduling Period</label>
                    <div class="col-sm-8 drop-down">
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3 no-padding margin-bottom clearfix">
                    <label class="control-label col-sm-4 titik-dua">Job</label>
                    <div class="col-sm-8 drop-down">
                        <asp:DropDownList ID="cbschtype" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbschtype_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3 no-padding margin-bottom clearfix">
                    <label class="control-label col-sm-4 titik-dua">salespoint</label>
                    <div class="col-sm-8 drop-down">
                        <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-sm-1 no-padding margin-bottom checkbox">
                <asp:CheckBox ID="chk" runat="server" Text="All" />
            </div>
        </div>
        
        <div class="row clearfix margin-bottom">
            <div class="col-sm-offset-3 col-sm-6 no-padding">
                <asp:RadioButtonList ID="rdwhs" CssClass="well no-margin well-sm radio radio-space-around" runat="server" RepeatDirection="Horizontal" BackColor="Silver"  OnSelectedIndexChanged="rdwhs_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem Value="D">Depo</asp:ListItem>
                    <asp:ListItem Value="V">Van Salesman</asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
        
        <div class="row clearfix">
            <div class="clearfix col-sm-11 no-padding">
                <div class="col-md-4 col-sm-6 no-padding margin-bottom">
                    <label class="col-sm-4 control-label titik-dua">Depo / Sub Depo</label>
                    <div class="col-sm-8 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbwhs" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbwhs_SelectedIndexChanged"></asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rdwhs" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col-md-4 col-sm-6 no-padding margin-bottom">
                    <label class="col-sm-4 control-label titik-dua">BIN</label>
                    <div class="col-sm-8 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                    <asp:DropDownList ID="cbbin" runat="server" CssClass="form-control"></asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cbwhs"  EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col-md-4 col-sm-6 no-padding margin-bottom">
                    <label class="col-sm-4 control-label titik-dua">Product</label>
                    <div class="col-sm-8 drop-down">
                        <asp:DropDownList ID="cbproduct" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-sm-1 no-padding margin-bottom">
                 <asp:Button ID="btadd" runat="server" Text="Add" CssClass="btn btn-success add" OnClick="btadd_Click" />
            </div>
        </div>
        
        <div class="row">
            <div class="h-divider"></div>
        </div>

        <div class="full padding-top padding-bottom clearfix">
            <div style="width:35%;float:left">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                         <asp:TreeView ID="trv" runat="server" OnSelectedNodeChanged="trv_SelectedNodeChanged" Width="90%" BorderStyle="Outset" CssClass="treeview">
                             <SelectedNodeStyle BackColor="#0090d9" BorderColor="transparent" CssClass="white" BorderStyle="Outset" />
                         </asp:TreeView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbschtype" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
        
            </div>
            <div style="width:60%;float:left">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                         <asp:GridView ID="grd" CssClass="table table-striped mygrid" runat="server" AutoGenerateColumns="False" CellPadding="4" GridLines="None" OnRowDeleting="grd_RowDeleting">
                             <AlternatingRowStyle  />
                    <Columns>
                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate>
                                <asp:Label ID="lbprodcode" runat="server" Text='<%# Eval("prod_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Product Name">
                            <ItemTemplate>
                                <%# Eval("prod_nm") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField HeaderText="ACT" ShowDeleteButton="True" />
                    </Columns>
                    <EditRowStyle CssClass="table-edit" />
                    <FooterStyle CssClass="table-footer" />
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page"/>
                    <RowStyle  />
                    <SelectedRowStyle CssClass="table-edit" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
       
            </div>
        </div>
        
        <div class="row">
            <div class="h-divider"></div>
        </div>

        <div class="navi margin-bottom">
            <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn btn-info btn-print" />
        </div>
    </div>
</asp:Content>

