<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_salesmanvisit.aspx.cs" Inherits="fm_salesmanvisit" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Salesman Visit Office Schedule</div>
    <img src="div2.png" class="divid" />
    <table><tr><td>
        Period</td><td>:</td><td style="margin-left: 40px">
            <asp:Label ID="lbperiod" runat="server" BorderStyle="Solid" BorderWidth="2px" ForeColor="Red"></asp:Label>
               </td><td>
            &nbsp;</td><td>
            &nbsp;</td><td>
            &nbsp;</td></tr><tr><td>
            Kind Of Visit</td><td>:</td><td style="margin-left: 40px">
            <asp:DropDownList ID="cbvisit" runat="server" Width="20em" AutoPostBack="True" OnSelectedIndexChanged="cbvisit_SelectedIndexChanged"></asp:DropDownList>
               </td><td>
                Salesman
               </td><td>
                :</td><td>
                <asp:DropDownList ID="cbsalesman" runat="server" Width="30em" AutoPostBack="True" OnSelectedIndexChanged="cbsalesman_SelectedIndexChanged">
                </asp:DropDownList>
               </td></tr><tr><td>
            Visit Date</td><td>:</td><td style="margin-left: 40px">
            <asp:TextBox ID="dtvisit" runat="server"></asp:TextBox>
            <asp:CalendarExtender ID="dtvisit_CalendarExtender" runat="server" TargetControlID="dtvisit" Format="d/M/yyyy">
            </asp:CalendarExtender>
               </td><td>
                <asp:Button ID="btadd" runat="server" CssClass="button2 add" Text="Save" OnClick="btadd_Click" />
            </td><td>
                &nbsp;</td><td>
                &nbsp;</td></tr></table>
            <img src="div2.png" class="divid" />
    <asp:Calendar ID="cld" runat="server"></asp:Calendar>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" CssClass="mygrid" Width="100%" OnRowDeleting="grd_RowDeleting">
        <Columns>
            <asp:TemplateField HeaderText="Period">
               <ItemTemplate>
                   <asp:HiddenField ID="hdids" runat="server"  Value='<%# Eval("ids") %>' />
                   <asp:Label ID="lbperiod" runat="server" Text='<%# Eval("period") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Kind Visit">
                <ItemTemplate>
                    <asp:Label ID="lbvisit" runat="server" Text='<%# Eval("visit_typ_nm") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Visit Date">
                <ItemTemplate><%# Eval("visit_dt","{0:d/M/yyyy}") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Salesman">
                <ItemTemplate><%# Eval("emp_desc") %></ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
            </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cbvisit" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cbsalesman" EventName="SelectedIndexChanged" />
        </Triggers>
        </asp:UpdatePanel>
    
    <img src="div2.png" class="divid" />
    <div class="navi">
    </div>
</asp:Content>

