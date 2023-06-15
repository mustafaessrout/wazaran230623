<%@ Page Title="" Language="C#" MasterPageFile="~/eis/eis.master" AutoEventWireup="true" CodeFile="fm_contribute2.aspx.cs" Inherits="eis_fm_contribute2" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
        $(document).ready(function () {
            $('#pnlmsg').hide();
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <div class="form-horizontal" style="font-family:Calibri;font-size:small">
        <h4 class="jajarangenjang">Contribute 2</h4>
        <div class="h-divider"></div>
       
        <div class="form-group">
            <label class="control-label col-md-1">Area</label>
            <div class="col-md-2">
                <asp:DropDownList ID="cbarea" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbarea_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <label class="control-label col-md-1">Salespoint</label>
            <div class="col-md-2">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                         <asp:DropDownList ID="cbsalespoint" CssClass="form-control" runat="server"></asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbarea" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
               
            </div>
            <label class="control-label col-md-1">Start</label>
            <div class="col-md-2">
                <asp:TextBox ID="dtstart" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:CalendarExtender ID="dtstart_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtstart">
                </asp:CalendarExtender>
            </div>
            <label class="control-label col-md-1">End</label>
            <div class="col-md-2">
                <asp:TextBox ID="dtend" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:CalendarExtender ID="dtend_CalendarExtender" runat="server" TargetControlID="dtend" Format="d/M/yyyy">
                </asp:CalendarExtender>
            </div>
        </div>
        <div class="form-group">
             <label class="control-label col-md-1">Product Group</label>
            <div class="col-md-3">
                <asp:DropDownList ID="cbprod" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbprod_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <label class="control-label col-md-1">Item</label>
            <div class="col-md-3">
                <asp:DropDownList ID="cbitem" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
            <div class="col-md-1">
                <asp:LinkButton ID="btview" OnClientClick="javascript:ShowProgress();" runat="server" CssClass="btn btn-primary" OnClick="btview_Click">View</asp:LinkButton>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-12">
                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="mydatagrid" RowStyle-CssClass="rows" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager">
                    <Columns>
                        <asp:TemplateField HeaderText="WS">
                            <ItemTemplate><%#Eval("ws") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="WS %">
                            <ItemTemplate><b style="color:red"><%#Eval("pctws") %></b></ItemTemplate></asp:TemplateField>
                        <asp:TemplateField HeaderText="KA">
                            <ItemTemplate><%#Eval("ka") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="KA %">
                            <ItemTemplate><b style="color:red"><%#Eval("pctka") %></b></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RTA">
                            <ItemTemplate><%#Eval("rta") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RTA %">
                            <ItemTemplate><b style="color:red"><%#Eval("pctrta") %></b></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RTI">
                            <ItemTemplate><%#Eval("rti") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RTI %">
                            <ItemTemplate><b style="color:red"><%#Eval("pctrti") %></b></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HRC">
                            <ItemTemplate><%#Eval("hrc") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HRC %">
                            <ItemTemplate><b style="color:red"><%#Eval("pcthrc") %></b></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MM">
                            <ItemTemplate><%#Eval("mm") %>
                                                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MM %">
                            <ItemTemplate><b style="color:red"><%#Eval("pctmm") %></b></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PHA">
                            <ItemTemplate><%#Eval("pha") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PHA %">
                            <ItemTemplate><b style="color:red"><%#Eval("pctpha") %></b></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

<HeaderStyle CssClass="header"></HeaderStyle>

<PagerStyle CssClass="pager"></PagerStyle>

<RowStyle CssClass="rows"></RowStyle>
                </asp:GridView>
            </div>
        </div>
    </div>
     <p id="pnlmsg" style="position:absolute; top: 50%;left: 50%;margin-top: -50px; margin-left: -50px;width: 100px;height: 100px;opacity:1 !important;z-index:100">
                <img src="/image/loading2.gif" />
     </p>
</asp:Content>

