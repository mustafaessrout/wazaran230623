<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_claimreport_ho.aspx.cs" Inherits="fm_claimreport_ho" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 

    <div class="container-fluid">
        <div class="page-header">
            <h3>Report Claim</h3>
        </div>

        <div id="search1" runat="server" class="row">
            <div class="col-sm-6">
                <div class="form-group">
                    <label for="type" class="col-xs-4 col-form-label col-form-label-sm">Claim Monthly </label>    
                    <%--<div class="col-xs-8">
                        <div class="col-xs-6">
                            <div class="form-group">
                            <label for="month" class="col-xs-2 col-form-label col-form-label-sm">Month </label>    
                            <div class="col-xs-10">
                                <asp:DropDownList ID="clmonth" runat="server" CssClass="form-control-static" Width="50%">
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
                            </div>
                        </div>
                        <div class="col-xs-6">
                            <div class="form-group">
                            <label for="month" class="col-xs-2 col-form-label col-form-label-sm">Year </label>    
                            <div class="col-xs-10">
                                <asp:DropDownList ID="clyear" runat="server" CssClass="form-control-static" Width="50%">
                                    <asp:ListItem Text="2011" Value="2011"></asp:ListItem>
                                    <asp:ListItem Text="2012" Value="2012"></asp:ListItem>
                                    <asp:ListItem Text="2013" Value="2013"></asp:ListItem>
                                    <asp:ListItem Text="2014" Value="2014"></asp:ListItem>
                                    <asp:ListItem Text="2015" Value="2015"></asp:ListItem>
                                    <asp:ListItem Text="2016" Value="2016"></asp:ListItem>
                                    <asp:ListItem Text="2017" Value="2017"></asp:ListItem>
                                    <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                                    <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            </div>
                        </div>
                    </div>--%>
                    <div class="col-xs-8"> 
                        <asp:LinkButton ID="btngenerate" 
                                        runat="server" 
                                        CssClass="btn btn-info btn-sm"    
                                        OnClick="btngenerate_Click" OnClientClick="vEnableShow();"> <span aria-hidden="true" class="glyphicon glyphicon-save-file"></span>Generate </asp:LinkButton>        
                    </div>
                </div>
            </div>
        </div>

        <div class="row"></div>

        <div id="search" runat="server" class="row">
            <div class="col-sm-6">
                <div class="form-group">
                    <label for="type" class="col-xs-4 col-form-label col-form-label-sm">Report Name </label>    
                    <div class="col-xs-8">
                        <asp:DropDownList ID="cbreport" runat="server" OnSelectedIndexChanged="cbreport_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-static" Width="100%">
                        </asp:DropDownList> 
                    </div>
                </div>
            </div>

            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
            <div id="logbook" runat="server" class="col-sm-6">
                <div class="row">
                    <div class="form-group">
                        <label for="status" class="col-xs-2 col-form-label col-form-label-sm">Status </label>    
                        <div class="col-xs-10">
                            <asp:DropDownList ID="cblogstatus" runat="server" CssClass="form-control-static" Width="50%">
                            </asp:DropDownList> 
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label for="status" class="col-xs-2 col-form-label col-form-label-sm">Principal </label>    
                        <div class="col-xs-10">
                            <asp:DropDownList ID="cblogvendor" runat="server" CssClass="form-control-static" Width="50%">
                            </asp:DropDownList> 
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label for="branch" class="col-xs-2 col-form-label col-form-label-sm">Branch </label>    
                        <div class="col-xs-10">
                            <asp:DropDownList ID="cblogbranch" runat="server" CssClass="form-control-static" Width="50%">
                            </asp:DropDownList> 
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label for="month" class="col-xs-2 col-form-label col-form-label-sm">Month </label>    
                        <div class="col-xs-10">
                            <asp:DropDownList ID="cblogMonth" runat="server" CssClass="form-control-static" Width="50%">
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
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label for="month" class="col-xs-2 col-form-label col-form-label-sm">Year </label>    
                        <div class="col-xs-10">
                            <asp:DropDownList ID="cblogYear" runat="server" CssClass="form-control-static" Width="50%">
                                
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-2"></div>
                    <div class="col-xs-10"> 
                        <asp:LinkButton ID="btnlogPrint" 
                                        runat="server" 
                                        CssClass="btn btn-info btn-sm"    
                                        OnClick="btnPrint_Click"> <span aria-hidden="true" class="glyphicon glyphicon-print"></span>Print </asp:LinkButton>        
                    </div>
                </div>
            </div>
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cbreport" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <div id="summarylog" runat="server" class="col-sm-6">
                <div class="row">
                    <div class="form-group">
                        <label for="month" class="col-xs-2 col-form-label col-form-label-sm">Year </label>    
                        <div class="col-xs-10">
                            <asp:DropDownList ID="cbsumyear" runat="server" CssClass="form-control-static" Width="50%">
                                
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-2"></div>
                    <div class="col-xs-10"> 
                        <asp:LinkButton ID="btnprintsum" 
                                        runat="server" 
                                        CssClass="btn btn-info btn-sm"    
                                        OnClick="btnPrint_Click"> <span aria-hidden="true" class="glyphicon glyphicon-print"></span>Print </asp:LinkButton>        
                    </div>
                </div>
            </div>
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cbreport" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
            <div id="propvsclaimdtl" runat="server" class="col-sm-6">
                <div class="row">
                    <div class="form-group">
                        <label for="type" class="col-xs-2 col-form-label col-form-label-sm">Type </label>    
                        <div class="col-xs-10">
                            <asp:DropDownList ID="cbpvscdtltype" runat="server" CssClass="form-control-static" Width="50%">
                                <asp:ListItem Text="ATL" Value="ATL"></asp:ListItem>
                                <asp:ListItem Text="BTL" Value="BTL"></asp:ListItem>
                            </asp:DropDownList> 
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label for="cost" class="col-xs-2 col-form-label col-form-label-sm">Cost </label>    
                        <div class="col-xs-10">
                            <asp:DropDownList ID="cbpvscdtlcost" runat="server" CssClass="form-control-static" Width="50%">
                                <asp:ListItem Text="principal" Value="principal"></asp:ListItem>
                                <asp:ListItem Text="sbtc" Value="sbtc"></asp:ListItem>
                                <asp:ListItem Text="Percentage" Value="Percentage"></asp:ListItem>
                            </asp:DropDownList> 
                        </div>
                    </div>
                </div>
                 <div class="row">
                    <div class="form-group">
                        <label for="status" class="col-xs-2 col-form-label col-form-label-sm">Principal </label>    
                        <div class="col-xs-10">
                            <asp:DropDownList ID="cbpvscdtlvendor" runat="server" CssClass="form-control-static" Width="50%">
                            </asp:DropDownList> 
                        </div>
                    </div>
                </div>
                <div class="row" runat="server" id="pvscdtltypepay">
                    <div class="form-group">
                        <label for="type" class="col-xs-2 col-form-label col-form-label-sm">Type Payment </label>    
                        <div class="col-xs-10">
                            <asp:DropDownList ID="cbpvscdtltypepay" runat="server" CssClass="form-control-static" Width="50%">
                            </asp:DropDownList> 
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label for="month" class="col-xs-2 col-form-label col-form-label-sm">Month</label>
                        <div class="col-xs-10">
                            <asp:DropDownList ID="cbpvscdtlmonth" runat="server" CssClass="form-control-static" Width="50%">
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
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label for="month" class="col-xs-2 col-form-label col-form-label-sm">Year </label>    
                        <div class="col-xs-10">
                            <asp:DropDownList ID="cbpvscdtlyear" runat="server" CssClass="form-control-static" Width="50%">                                
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-2"></div>
                    <div class="col-xs-10"> 
                        <asp:LinkButton ID="btnprintpvscdtl" 
                                        runat="server" 
                                        CssClass="btn btn-info btn-sm"    
                                        OnClick="btnPrint_Click"> <span aria-hidden="true" class="glyphicon glyphicon-print"></span>Print </asp:LinkButton>        
                    </div>
                </div>
            </div>
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cbreport" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
            <div id="claimstatement" runat="server" class="col-sm-6">
                <div class="row">
                    <div class="form-group">
                        <label for="branch" class="col-xs-2 col-form-label col-form-label-sm">Branch </label>    
                        <div class="col-xs-10">
                            <asp:DropDownList ID="cbstatementbr" runat="server" CssClass="form-control-static" Width="50%">
                            </asp:DropDownList> 
                        </div>
                    </div>
                </div>
                 <div class="row">
                    <div class="form-group">
                        <label for="month" class="col-xs-2 col-form-label col-form-label-sm">Month </label>    
                        <div class="col-xs-10">
                            <asp:DropDownList ID="cbstatementmonth" runat="server" CssClass="form-control-static" Width="50%">
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
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label for="year" class="col-xs-2 col-form-label col-form-label-sm">Year </label>    
                        <div class="col-xs-10">
                            <asp:DropDownList ID="cbstatementyear" runat="server" CssClass="form-control-static" Width="50%">
                                
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-2"></div>
                    <div class="col-xs-10"> 
                        <asp:LinkButton ID="btnprintstatement" 
                                        runat="server" 
                                        CssClass="btn btn-info btn-sm"    
                                        OnClick="btnPrint_Click"> <span aria-hidden="true" class="glyphicon glyphicon-print"></span>Print </asp:LinkButton>        
                    </div>
                </div>
            </div>
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cbreport" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
            <div id="salesvsclaimdetail" runat="server" class="col-sm-6">
                <div class="row">
                    <div class="form-group">
                        <label for="branch" class="col-xs-2 col-form-label col-form-label-sm">Branch </label>    
                        <div class="col-xs-10">
                            <asp:DropDownList ID="cbsalesdetailbranch" runat="server" CssClass="form-control-static" Width="50%">
                            </asp:DropDownList> 
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label for="dtStart" class="col-xs-2 col-form-label col-form-label-sm">Start Date </label>    
                        <div class="col-xs-10">
                            <asp:TextBox ID="dtsalesdetailbranchstart" runat="server" CssClass="form-control input-group-lg" Height="2em" Width="50%"></asp:TextBox>
                            <asp:CalendarExtender ID="dtsalesdetailbranchstart_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtsalesdetailbranchstart">
                            </asp:CalendarExtender>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label for="dtEnd" class="col-xs-2 col-form-label col-form-label-sm">End Date </label>    
                        <div class="col-xs-10">
                            <asp:TextBox ID="dtsalesdetailbranchend" runat="server" CssClass="form-control input-group-lg" Height="2em" Width="50%"></asp:TextBox>
                            <asp:CalendarExtender ID="dtsalesdetailbranchend_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtsalesdetailbranchend">
                            </asp:CalendarExtender>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group">
                        <label for="Group From" class="col-xs-2 col-form-label col-form-label-sm">Group From </label>    
                        <div class="col-xs-10">
                            <asp:DropDownList ID="cbsalesdetailproduct" runat="server" CssClass="form-control-static" Width="50%">
                            </asp:DropDownList> 
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-2"></div>
                    <div class="col-xs-10"> 
                        <asp:LinkButton ID="btnprintsalesdetail" 
                                        runat="server" 
                                        CssClass="btn btn-info btn-sm"    
                                        OnClick="btnPrint_Click"> <span aria-hidden="true" class="glyphicon glyphicon-print"></span>Print </asp:LinkButton>        
                    </div>
                </div>
            </div>
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cbreport" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>

    </div>

</asp:Content>

