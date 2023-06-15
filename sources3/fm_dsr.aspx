<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_dsr.aspx.cs" Inherits="fm_dsr" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
<script>
    function ItemSelectedsalesman_cd(sender, e) {

        $get('<%=hdsalesman_cd.ClientID%>').value = e.get_value();
        }
</script>
    <style type="text/css">
        .auto-style1 {
            width: 334px;
        }
        .main-content #mCSB_2_container{
            height:100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid margin-top padding-top">
        <div class="row">
            <div class="col-sm-offset-2 col-sm-8">
                <div class="clearfix form-group">
                    <label class="control-label col-sm-2">Driver</label>
                    <div class="col-sm-8">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                            <asp:TextBox ID="txsalesman" runat="server" CssClass="form-control"></asp:TextBox>                                                
                                <div id="divwidth" style="position:absolute;"></div>                                     
                                <asp:HiddenField ID="hdsalesman_cd" runat="server" />               
                                <ajaxToolkit:AutoCompleteExtender ID="txsalesman_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txsalesman" UseContextKey="True" 
                                        CompletionListElementID="divwidth" MinimumPrefixLength="1" EnableCaching="false"  FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="ItemSelectedsalesman_cd">
                                </ajaxToolkit:AutoCompleteExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="clearfix form-group margin-bottom">
                    <label class="control-label col-sm-2">Date</label>
                    <div  class="col-sm-8 drop-down-date">
                         <asp:TextBox ID="dtstart" runat="server" CssClass="form-control"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="dtstart_CalendarExtender" CssClass="date" runat="server" BehaviorID="dtstart_CalendarExtender" Format="d/M/yyyy" TargetControlID="dtstart">
                        </ajaxToolkit:CalendarExtender>
                         
                    </div>
                </div>
            </div>
        </div>
    </div>
    
        
    <div class="navi margin-bottom padding-bottom padding-top margin-top">
        <asp:Button ID="brnew" runat="server" CssClass="btn-success btn btn-add" OnClick="brnew_Click" Text="New" />
        <asp:Button ID="btreport" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btreport_Click" />
        <asp:Button ID="btgenerate" runat="server" CssClass="btn-warning btn btn-save" OnClick="btgenerate_Click" Text="Generate" Visible="False" />    
        </div>
</asp:Content>

