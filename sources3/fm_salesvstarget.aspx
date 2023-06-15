<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_salesvstarget.aspx.cs" Inherits="fm_salesvstarget" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divheader">Sales VS TARGET Report</div>
    <div class="h-divider"></div>
    <div class="container-fluid">
        <div class="row clearfix">
            <div class="col-md-offset-2 col-md-8 col-sm-offset-1 col-sm-10">
                <div class="clearfix form-group">
                    <label class="control-label col-sm-2">Salespoint</label>
                    <div class="col-sm-10">
                        <div class="drop-down">
                            <asp:dropdownlist id="cbsalespoint" runat="server" cssclass="form-control">
                                    </asp:dropdownlist>
                        </div>
                    </div>
                </div>
                <div class="clearfix form-group">
                    <asp:label id="Label1" runat="server" text="Report Type" cssclass="control-label col-sm-2"></asp:label>
                    <div class="col-sm-10">
                        <div class="drop-down">
                            <asp:updatepanel id="UpdatePanel5" runat="server">
        <ContentTemplate>
                                    <asp:DropDownList ID="cbreporttype" runat="server" CssClass="form-control"  OnSelectedIndexChanged="cbreporttype_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                    </ContentTemplate>
    </asp:updatepanel>
                        </div>

                    </div>

                </div>

                <div class="clearfix form-group">
                    <div class="col-md-6 no-padding clearfix">
                        <asp:label id="lbldt1" runat="server" text="Start Date" cssclass="control-label col-md-4 col-sm-2"></asp:label>
                        <div class="col-md-8 col-sm-10">
                            <div class="drop-down-date">
                                <asp:textbox id="txdt1" runat="server" cssclass="form-control"></asp:textbox>
                                <asp:calendarextender cssclass="date" id="txdt1_CalendarExtender" runat="server" behaviorid="txdt1_CalendarExtender" format="d/M/yyyy" targetcontrolid="txdt1">
                                    </asp:calendarextender>
                            </div>

                        </div>
                    </div>
                    <div class="col-md-6 no-padding clearfix">
                        <asp:label id="lbldt2" runat="server" text="End Date" cssclass="control-label col-md-4 col-sm-2"></asp:label>
                        <div class="col-md-8 col-sm-10">
                            <div class="drop-down-date">
                                <asp:textbox id="txdt2" runat="server" cssclass="form-control"></asp:textbox>
                                <asp:calendarextender cssclass="date" id="txdt2_CalendarExtender" runat="server" behaviorid="txdt2_CalendarExtender" targetcontrolid="txdt2" format="d/M/yyyy">
                                    </asp:calendarextender>
                            </div>

                        </div>
                    </div>
                </div>



                <div class="clearfix form-group">
                    <div class="col-md-6 no-padding clearfix">
                        <asp:label id="lbsalesman" runat="server" text="Salesman" cssclass="control-label  col-md-4 col-sm-2"></asp:label>
                        <div class="col-md-8 col-sm-10">
                            <div class="drop-down">
                                <asp:dropdownlist id="cbsalesman" runat="server" cssclass=" form-control">
                                    </asp:dropdownlist>
                                <asp:checkbox id="chall" runat="server" oncheckedchanged="chall_CheckedChanged" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clearfix form-group">
                    <div class="clearfix col-sm-6 no-padding form-group">
                        <asp:label id="Label6" runat="server" text="Group" cssclass="control-label col-md-4 col-sm-2"></asp:label>
                        <div class="col-md-8 col-sm-10">
                            <div class="drop-down">
                                <asp:dropdownlist id="cbProd_cdFr" runat="server" cssclass="form-control" enabled="true">
                                            </asp:dropdownlist>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix col-sm-6 no-padding form-group">
                        <asp:label id="Label7" runat="server" text="to Group" cssclass="control-label col-sm-2"></asp:label>
                        <div class="col-md-8 col-sm-10">
                            <div class="drop-down">
                                <asp:dropdownlist id="cbProd_cdTo" runat="server" cssclass="form-control" enabled="true">
                                            </asp:dropdownlist>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    <div class="navi row margin-bottom">
        <asp:button id="btreport" runat="server" text="Print" onclick="btreport_Click" cssclass="btn-info btn btn-print" />
    </div>


</asp:Content>


