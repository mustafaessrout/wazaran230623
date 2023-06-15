<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_sumoutletperformance.aspx.cs" Inherits="fm_sumoutletperformance" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
     <script>
         function ItemSelectedsalesman_cd(sender, e) {

             $get('<%=hdsalesman_cd.ClientID%>').value = e.get_value();
        }
        
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 79px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Aging Performance by Branch</div>
    <div class="h-divider"></div>
    
    <div class="container-fluid">
        <div class="clearfix margin-bottom">
            <label class="col-md-1 col-sm-2 titik-dua control-label">Type</label>
            <div class="col-sm-4 drop-down">
                <asp:DropDownList ID="cbtype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbtype_SelectedIndexChanged" CssClass="form-control">
                    <asp:ListItem Value="0">Aging Performance by Salesman</asp:ListItem>
                    <asp:ListItem Value="2">Aging Performance by Customer</asp:ListItem>
                    <asp:ListItem Value="1">Over Due History</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-sm-2 ">
                <asp:CheckBox ID="cbdt" runat="server" CssClass="checkbox" Text="Detail (ACC)" />
            </div>
            <div class="col-sm-2 ">
                <asp:CheckBox ID="cb120" runat="server" CssClass="checkbox" Text="120 (INV)" />
            </div>
        </div>
        <div class="clearfix margin-bottom">
            <label class="col-md-1 col-sm-2 titik-dua control-label">Salesman</label>
            <div class="col-sm-4">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                    <asp:TextBox ID="txsalesman" runat="server"  CssClass="form-control ro"></asp:TextBox>                                                
                        <asp:CheckBox ID="chall" runat="server" OnCheckedChanged="chall_CheckedChanged" Text="ALL" AutoPostBack="True" />
                        <div id="divwidth" style="font-size:small;font-family:Calibri;"></div>                                     
                        <asp:HiddenField ID="hdsalesman_cd" runat="server" />               
                        <asp:AutoCompleteExtender ID="txsalesman_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txsalesman" UseContextKey="True" 
                                CompletionListElementID="divwidth" MinimumPrefixLength="1" EnableCaching="false"  FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="ItemSelectedsalesman_cd">
                        </asp:AutoCompleteExtender>
                          </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="clearfix margin-bottom">
            <label class="col-md-1 col-sm-2 titik-dua control-label">salespoint</label>
            <div class="col-sm-4 drop-down">
                 <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
        </div>
        <div class="clearfix margin-bottom">
            <asp:Label ID="lblper" runat="server" Text="Period:" Visible="False" CssClass="col-md-1 col-sm-2 titik-dua control-label"></asp:Label>
            <asp:Panel runat="server" ID="cbperPnl" CssClass="col-sm-4 drop-down" Visible="False">
                <asp:DropDownList ID="cbper" runat="server"  CssClass="form-control">
                </asp:DropDownList>
            </asp:Panel>
        </div>
    </div>

    <div class="navi">
         <asp:Button ID="btreport" runat="server" Text="Print" OnClick="btreport_Click" CssClass="btn btn-info print" />
     </div>
</asp:Content>


