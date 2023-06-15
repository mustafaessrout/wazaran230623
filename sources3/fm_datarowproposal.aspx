<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_datarowproposal.aspx.cs" Inherits="fm_datarowproposal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container-fluid">

        <div class="page-header">
            <h3>Proposal Data Row Report</h3>
        </div>

        <div class="form-horizontal">

            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <label for="principal" class="col-xs-2 col-form-label col-form-label-sm">By Principal</label>    
                        <div class="col-xs-4">
                            <asp:DropDownList ID="cbvendor" runat="server" styleCssClass="form-control-static" Width="100%"></asp:DropDownList> 
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <label for="marketing" class="col-xs-2 col-form-label col-form-label-sm">By Marketing Cost</label>    
                        <div class="col-xs-4">
                            <asp:DropDownList ID="cbmarketingcost" runat="server" styleCssClass="form-control-static" Width="100%"></asp:DropDownList> 
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <label for="Promotion Type" class="col-xs-2 col-form-label col-form-label-sm">By Promotion Type</label>    
                        <div class="col-xs-4">
                            <asp:DropDownList ID="cbpromotype" runat="server" styleCssClass="form-control-static" Width="100%"></asp:DropDownList> 
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <label for="year" class="col-xs-2 col-form-label col-form-label-sm">By Month</label>    
                        <div class="col-xs-2">
                            <asp:DropDownList ID="cbMonth" runat="server" styleCssClass="form-control-static" Width="100%">
                                <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                <asp:ListItem Text="December" Value="12"></asp:ListItem>
                             </asp:DropDownList>
                        </div>
                        <label for="year" class="col-xs-2 col-form-label col-form-label-sm">By Year</label>    
                        <div class="col-xs-2">
                            <asp:DropDownList ID="cbYear" runat="server" styleCssClass="form-control-static" Width="100%">
                                <asp:ListItem Text="2016" Value="2016"></asp:ListItem>
                                <asp:ListItem Text="2017" Value="2017"></asp:ListItem>
                            </asp:DropDownList> 
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <div class="row">
          <div class="col-sm-12">
            <div class="text-center">
                <button type="button" class="btn btn-default btn-sm" runat="server" id="btprint" onserverclick="btprint_Click" >
                  <span class="glyphicon glyphicon-print" aria-hidden="true"></span> Print
                </button>
            </div>
          </div>
        </div>

    </div>

</asp:Content>

