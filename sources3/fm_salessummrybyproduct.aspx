<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_salessummrybyproduct.aspx.cs" Inherits="fm_salessummrybyproduct" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 482px;
        }
        .drop-down-date.drop-down-custom::before{
            right:10px;
        }
        .main-content #mCSB_2_container{
            min-height: 540px;
        }
        </style>
        <script>
            function CustSelected(sender, e) {
                $get('<%=hdemp.ClientID%>').value = e.get_value();

        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Reporting</div>
    <div class="h-divider"></div>

    <div class="container-fluid clearfix">
        <div class="row clearfix col-md-offset-3 col-sm-offset-2 col-md-6 col-sm-8 ">
            <div class="form-group  clearfix  col-sm-6">
                <asp:Label ID="lbldt1" runat="server" Text="Start Date" CssClass="control-label"></asp:Label>
                <div class="drop-down-date drop-down-custom">
                    <asp:TextBox ID="txdt1" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:CalendarExtender CssClass="date" ID="txdt1_CalendarExtender" runat="server" BehaviorID="txdt1_CalendarExtender" Format="d/M/yyyy" TargetControlID="txdt1">
                    </asp:CalendarExtender>
                </div>
            </div>
            <div class="form-group clearfix  col-sm-6">
                <asp:Label ID="lbldt2" runat="server" Text="End Date" CssClass="control-label"></asp:Label>
                <div  class="drop-down-date drop-down-custom">
                    <asp:TextBox ID="txdt2" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:CalendarExtender CssClass="date" ID="txdt2_CalendarExtender" runat="server" BehaviorID="txdt2_CalendarExtender" TargetControlID="txdt2" Format="d/M/yyyy">
                    </asp:CalendarExtender>
                </div>
            </div>
            <div class="form-group clearfix">
                <div class="col-sm-12">
                    <asp:Label ID="Label2" runat="server" Text="Salesman Name"  CssClass="control-label"></asp:Label>
                    <div >
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
                    <asp:HiddenField ID="hdemp" runat="server" />
                    <asp:TextBox ID="txCust" runat="server" CssClass="form-control" AutoPostBack="True"></asp:TextBox>  
                      
                    <div class="checkbox no-margin-top no-margin-bottom">
                        <asp:CheckBox ID="chCust" runat="server" AutoPostBack="True" OnCheckedChanged="chCust_CheckedChanged" Text="ALL" />
                    </div>    
                   
                     <div id="divwidths"></div>
                    <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListItemCssClass="auto-complate-item" CompletionListHighlightedItemCssClass="auto-complate-hover" ID="txCust_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txCust" UseContextKey="True" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" CompletionListElementID="divwidths" OnClientItemSelected="CustSelected">
                    </asp:AutoCompleteExtender>
                     </ContentTemplate>
                    </asp:UpdatePanel>
                    </div>
                </div>
                
            </div>
        </div>
        <div class="navi row margin-bottom col-xs-12">
             <asp:Button ID="btreport" runat="server" Text="Print" OnClick="btreport_Click" CssClass="btn-info btn btn-print" />
         </div>
    </div>

     
    
</asp:Content>


