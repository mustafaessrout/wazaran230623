<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstloc.aspx.cs" Inherits="fm_mstloc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 302px;
        }
        .auto-style2 {
            margin-left:15px;
            width: 36px;
            height: 36px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Location <img alt="" class="auto-style2" src="loc.png" /></div>
    <div class="h-divider"></div>
    
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-offset-4 col-md-4 margin-bottom">
                <p class="divheader no-margin">Region - Area - City - District-Street</p>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:TreeView ID="trv" runat="server" OnSelectedNodeChanged="trv_SelectedNodeChanged" BackColor="#FFFFCC" ShowLines="True" BorderStyle="None" ExpandDepth="2" ImageSet="News" CssClass="grd_1">
                        <Nodes>
                            <asp:TreeNode Text="REGION" Value="REGION">
                            </asp:TreeNode>
                        </Nodes>
                            <RootNodeStyle ImageUrl="~/folderclose.png" />
                            <SelectedNodeStyle BackColor="#9999FF" />
                        </asp:TreeView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btsave" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="row">
            <div class="col-md-offset-4 col-md-4">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="clearfix form-group">
                            <label class="control-label">Location Code</label>
                            <div class="">
                                <asp:TextBox ID="txlocationcode" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="clearfix form-group">
                            <label class="control-label">location Name</label>
                            <div class="">
                                <asp:TextBox ID="txlocationname" runat="server" CssClass="makeitupper form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="clearfix form-group">
                            <label class="control-label">Arabic</label>
                            <div class="">
                                <asp:TextBox ID="txarabic" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="clearfix form-group">
                            <label class="control-label">Level No.</label>
                            <div class="drop-down">
                                <asp:DropDownList ID="cblevelno" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="clearfix form-group">
                            <label class="control-label">Parent.</label>
                            <div class="drop-down">
                                <asp:DropDownList ID="cbparent" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="navi margin-top margin-bottom">
                            <asp:Button ID="btnew" runat="server" Text="New" CssClass="btn-success btn btn-add" OnClick="btnew_Click" />
                            <asp:Button ID="btedit" runat="server" Text="Edit" CssClass="btn-primary btn btn-edit" OnClick="btedit_Click"/>
                            <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn-warning btn btn-save" OnClick="btsave_Click" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="trv" EventName="SelectedNodeChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        
    </div>
        
    
</asp:Content>

