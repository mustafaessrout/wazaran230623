<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_cmo_ho.aspx.cs" Inherits="fm_cmo_ho" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="css/anekabutton.css" />
    <script>    
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }

        function SelectCMO(sVal) {
            ShowProgress();
            $get('<%=hdcmo.ClientID%>').value = sVal;
            $get('<%=btselect.ClientID%>').click();
        }

        $(document).ready(function () {
            $("#<%=btsearch.ClientID%>").click(function () {
                PopupCenter('fm_lookup_cmo_ho.aspx', 'xtf', '900', '500');

            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:HiddenField ID="hdcmo" runat="server" />
     
     <div class="container">
        <h4 class="jajarangenjang">CMO / MOP Monthly</h4>
        <div class="h-divider"></div>

        <div class="row">
            <div class="col-sm-6">
                <div class="form-group">
                    <label class="control-label col-sm-2">Batch Seq#</label>
                    <div class="input-group">
                    <asp:Label ID="lbcmono" CssClass="form-control input-group-sm ro" runat="server" Text=""></asp:Label>
                    <div class="input-group-btn">
                        <asp:LinkButton ID="btsearch" CssClass="btn btn-primary" runat="server" OnClick="btsearch_Click"><i class="fa fa-search"></i></asp:LinkButton>
                    </div>
                </div>
                </div>
            </div>
            <div class="col-sm-6 ">
                <div class="form-group">
                <label class="control-label col-sm-2">For Period</label>
                <div class="col-sm-10">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="drop-down">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbMonthCD" runat="server" AutoPostBack="True" CssClass="form-control"  OnSelectedIndexChanged="cbMonthCD_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>                         
                </div>
                </div>
            </div>
        </div>

        <h5 class="jajarangenjang">Data Details</h5>
        <div class="h-divider"></div>

        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <label class="control-label col-sm-2">Data# </label>
                    <div class="col-sm-10">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                        <asp:CheckBox runat="server" ID="ch1" Checked="true" Enabled="false" Text="Stock" />
                        <asp:CheckBox runat="server" ID="ch2" Checked="true" Enabled="false" Text="Stock" />
                        <asp:CheckBox runat="server" ID="ch3" Checked="true" Enabled="false" Text="Stock" />
                        <asp:CheckBox runat="server" ID="ch4" Checked="true" Enabled="false" Text="Stock" />
                        <asp:CheckBox runat="server" ID="ch5" Checked="true" Enabled="false" Text="Stock" />
                        </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cbMonthCD" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                <label class="control-label col-sm-2">From</label>
                <div class="col-sm-2">
                    <asp:TextBox ID="txfrom" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                    <asp:CalendarExtender ID="txfrom_CalendarExtender" Format="d/M/yyyy" runat="server" TargetControlID="txfrom">
                    </asp:CalendarExtender>
                </div>
                <label class="control-label col-sm-2">To</label>
                <div class="col-sm-2">
                    <asp:TextBox ID="txto" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                    <asp:CalendarExtender ID="txto_CalendarExtender" Format="d/M/yyyy" runat="server" TargetControlID="txto">
                    </asp:CalendarExtender>
                </div>
                </div>
            </div>
        </div>
        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cbMonthCD" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
         
        <h5 class="jajarangenjang">Parameters</h5>
        <div class="h-divider"></div>

        <div class="row">
            <div class="col-sm-6">
                <label class="control-label col-sm-3">Buffer# Days</label>
                <div class="col-sm-3">
                    <asp:TextBox ID="txdays" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-6">
                <label class="control-label col-sm-3">Factory# Percentage</label>
                <div class="col-sm-9">
                    <label class="control-label col-sm-3">East %</label>
                    <asp:TextBox ID="txeastfactory" CssClass="form-control input-sm" Width="50px" runat="server">50</asp:TextBox>
                    <label class="control-label col-sm-3">West %</label>
                    <asp:TextBox ID="txwestfactory" CssClass="form-control input-sm" Width="50px" runat="server">50</asp:TextBox>
                </div>  
            </div>
        </div>

         <div class="row margin-bottom">
            <div class="col-sm-12">
                <div class="text-center navi">
                    <asp:Button id="btnew" runat="server" Text="New" class="btn btn-warning btn-sm" OnClick="btnew_Click" />
                    <asp:Button id="btgenerate" runat="server" Text="Generate" class="btn btn-success btn-sm" OnClick="btgenerate_Click" />
                    <asp:Button ID="btselect" runat="server" Text="Button" Style="display: none" OnClick="btselect_Click" />
                </div>
            </div>
        </div>  


        <div runat="server" id="showResult">
            <h5 class="jajarangenjang">Data Results#</h5>
            <div class="h-divider"></div>
            <div class="row">
                <div class="form-group">
                    <div class="col-sm-12">
                        <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" ShowFooter="True" CellPadding="0" >
                            <Columns>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <%#Eval("item_cd") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Zone">
                                    <ItemTemplate>
                                        <%#Eval("zone_cd") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="OPB">
                                    <ItemTemplate>
                                        <%#Eval("opb") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="STD">
                                    <ItemTemplate>
                                        <%#Eval("std") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="STT">
                                    <ItemTemplate>
                                        <%#Eval("stt") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="END">
                                    <ItemTemplate>
                                        <%#Eval("end_prevmonth") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sales Plan">
                                    <ItemTemplate>
                                        <%#Eval("stt_thismonth") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="END">
                                    <ItemTemplate>
                                        <%#Eval("end_thismonth") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="cmo">
                                    <ItemTemplate>
                                        <%#Eval("cmo") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

        </div>
        

    </div>

</asp:Content>

