<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_claim.aspx.cs" Inherits="fm_claim" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <link href="css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
       
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Claim Reports </div>
     <div class="h-divider"></div>

    <div class="container clearfix">
        <div class="row col-md-offset-2 col-md-8 col-sm-offset-1 col-sm-10">
            <div class="col-sm-12">
                <div class="margin-bottom clearfix">
                    <label for="cbreporttype" class="col-sm-2 control-label">Report Type</label>
                    <div class="col-sm-10 drop-down" style="padding-left:10px;">
                         <asp:DropDownList ID="cbreporttype" runat="server" CssClass="form-control">
                             <asp:ListItem Value="0">Claim List</asp:ListItem>
                         </asp:DropDownList>
                         
                    </div>
                </div>
                
                
            </div>
           
            <div class="col-sm-6">
                <div class="margin-bottom clearfix">
                    <label for="dtfrom" class="col-sm-4 control-label">From Date</label>
                    <div class="col-sm-8 drop-down-date">
                            <asp:TextBox ID="dtfrom" runat="server" CssClass="form-control"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender CssClass="date" ID="dtfrom_CalendarExtender" runat="server" BehaviorID="dtfrom_CalendarExtender" TargetControlID="dtfrom" Format="d/M/yyyy">
                            </ajaxToolkit:CalendarExtender>
                      
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="margin-bottom clearfix">
                    <label for="dtend" class="col-sm-4 control-label">To date</label>
                    <div class="col-sm-8 drop-down-date">
                        <asp:TextBox ID="dtend" runat="server" CssClass="form-control"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender CssClass="date" ID="dtend_CalendarExtender" runat="server" BehaviorID="dtend_CalendarExtender" TargetControlID="dtend" Format="d/M/yyyy">
                            </ajaxToolkit:CalendarExtender>
                     
                    </div>
                </div>
                </div>
        </div>

      
        <div class="navi row col-xs-12 clearfix margin-bottom">
            <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn btn-info btn-print" OnClick="btprint_Click"  />                      
        </div>
        
    </div>
    
</asp:Content>

