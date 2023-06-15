<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="frmprnlistcustbysalesman.aspx.cs" Inherits="frmprnlistcustbysalesman" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style>
         #mydiv{
             height:430px;
         }
     </style>
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="mydiv" class="full center middle">
        <div class="clearfix  col-sm-8">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="clearfix">
                        <div class="form-group clearfix">
                            <label class="control-label col-sm-2 ">Salesman</label>
                            <div class="col-sm-10 drop-down">
                                <asp:DropDownList ID="cbsalesman" runat="server" CssClass="form-control" AutoPostBack="True" >
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-10 col-sm-offset-2 checkbox">
                                <asp:CheckBox ID="chall" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" Text="ALL" />
                            </div>
                        </div>

                        <div class="form-group col-sm-6 no-padding clearfix">
                            <label class="control-label col-sm-4">Period From</label>
                            <div class="col-sm-8 drop-down">
                                <asp:DropDownList ID="cbMonthCDFr" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbMonthCDFr_SelectedIndexChanged" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-offset-4 col-sm-8 drop-down margin-top">
                                <asp:TextBox ID="txfrom" runat="server" CssClass="makeitreadonly ro form-control text-primary" Enabled="False"></asp:TextBox>
                                <asp:CalendarExtender ID="txfrom_CalendarExtender" runat="server" TargetControlID="txfrom" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                                </asp:CalendarExtender>
                            </div>
                        </div>


                        <div class="form-group col-sm-6 no-padding clearfix">
                            <label class="control-label col-sm-4 " style="text-align:center;">To</label>
                            <div  class="col-sm-8 drop-down">
                                <asp:DropDownList ID="cbMonthCDTo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbMonthCDTo_SelectedIndexChanged" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-offset-4 col-sm-8 drop-down margin-top">
                                <asp:TextBox ID="txto" runat="server" CssClass="makeitreadonly ro form-control text-primary" Enabled="False"></asp:TextBox>
                                <asp:CalendarExtender ID="txto_CalendarExtender" runat="server" TargetControlID="txto" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                    </div>
      
                    <div class="col-sm-offset-2 col-sm-10 navi padding-top margin-bottom">
                        <asp:Button ID="btprint" runat="server" Text="Print" OnClick="btprint_Click" CssClass="btn-info btn print" />
                        <asp:Button ID="btclose" runat="server" Text="Close" OnClick="btclose_Click" CssClass="btn-danger btn btn-close" />
                    </div>    
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        
    </div>
</asp:Content>

