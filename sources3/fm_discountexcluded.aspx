<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_discountexcluded.aspx.cs" Inherits="fm_discountexcluded" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="admin/css/bootstrap.min.css" rel="stylesheet" />
    <script src="admin/js/bootstrap.min.js"></script>

   <script>
       function CustSelected(sender, e)
       {
           $get('<%=hdcust.ClientID%>').value = e.get_value();
       }
   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <h3>Discount Excluded </h3>
    <img src="div2.png" class="divid" />
    <div class="form-horizontal">
        <div class="form-group">
            <label class="control-label col-md-2">Valid in Salespoint</label>
            <div class="col-md-4">
            <asp:RadioButtonList ID="rdsalespoint" runat="server" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rdsalespoint_SelectedIndexChanged" BorderStyle="Inset" Width="100%" CssClass="form-control">
            <asp:ListItem Value="A">All Salespoint</asp:ListItem>
            <asp:ListItem Value="S">Salespoint</asp:ListItem>
            </asp:RadioButtonList>
            </div>
             <div class="col-md-5">
                <asp:DropDownList ID="cbsalespoint" runat="server" Width="20em" AutoPostBack="True" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged" CssClass="form-control-static">
                </asp:DropDownList>
        </div>
        </div>
      
       </div>
        <div class="form-horizontal">
           <div class="form-group">
            <label class="control-label col-md-2">Option Excluded</label>
             <div class="col-md-4"> 
              <asp:RadioButtonList ID="rdcust" runat="server" AutoPostBack="True" CellPadding="0" CellSpacing="0" OnSelectedIndexChanged="rdcust_SelectedIndexChanged" RepeatDirection="Horizontal" BorderStyle="Inset" Width="100%" CssClass="form-control">
                    <asp:ListItem Value="C">Cust</asp:ListItem>
                    <asp:ListItem Value="G">Group Customer</asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div class="col-md-6">
                  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbgroup" runat="server" Width="30em">
                                </asp:DropDownList>
                                <asp:TextBox ID="txcust" runat="server" Width="20em"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="txcust_AutoCompleteExtender" runat="server" TargetControlID="txcust" ServiceMethod="GetCompletionCust" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" UseContextKey="true" CompletionInterval="10" CompletionSetCount="1" CompletionListElementID="divw" OnClientItemSelected="CustSelected">
                                </asp:AutoCompleteExtender>
                                <asp:HiddenField ID="hdcust" runat="server" />
                                <div id="divw" style="font-size:small;font-family:Calibri"></div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rdcust" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-horizontal">
                <div class="form-group">
                        <label class="control-label col-md-2">Discount</label>
                        <div class="col-md-4">
                        <asp:RadioButtonList ID="rddiscount" runat="server" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="rddiscount_SelectedIndexChanged" BorderStyle="Inset" CssClass="form-control">
                        <asp:ListItem Value="A">All Discount</asp:ListItem>
                        <asp:ListItem Value="D">Only Some Of Discount</asp:ListItem>
                       </asp:RadioButtonList>
                       </div>
                        <div class="col-md-4">
                         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                            <asp:DropDownList ID="cbdiscount" runat="server" OnSelectedIndexChanged="cbdiscount_SelectedIndexChanged" CssClass="form-control-static" Width="100%">
                            </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rddiscount" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        <div class="form-horizontal">
            <div class="form-group">
            <label class="control-label col-md-2">Start Date</label>
                <div class="col-md-3">
            <asp:TextBox ID="dtstart" runat="server" CssClass="form-control-static"></asp:TextBox>
             <asp:CalendarExtender ID="dtstart_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtstart">
                </asp:CalendarExtender>
              </div>
            <label class="control-label col-md-2">End Date</label>
                <div class="col-md-3">
           <asp:TextBox ID="dtend" runat="server" CssClass="form-control-static"></asp:TextBox>
           <asp:CalendarExtender ID="dtend_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtend">
           </asp:CalendarExtender>
           </div>
          </div>
        </div>
        </div>
             
                 
               
         <div class="navi">
         <asp:Button ID="btaddcust" runat="server" Text="Save" CssClass="btn btn-default" OnClick="btaddcust_Click" />
         </div>
    <div class="divgrid" style="padding-top:1em;padding-bottom:1em">
        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" OnRowDeleting="grd_RowDeleting" OnRowEditing="grd_RowEditing" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowUpdating="grd_RowUpdating" CssClass="mygrid">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="Valid By">
             
                    <ItemTemplate> <asp:HiddenField ID="hdids" Value='<%# Eval("IDS") %>' runat="server" /><%# Eval("rdcust_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Salespoint">
                    <ItemTemplate><%# Eval("salespoint_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Customer">
                    <ItemTemplate><%# Eval("cust_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Group Customer">
                    <ItemTemplate><%# Eval("cusgrcd_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Discount Excluded"><ItemTemplate><%# Eval("rddiscount_nm") %></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Discount">
                    <ItemTemplate>
                        <%# Eval("disc_cd") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Start Date"><ItemTemplate><%# Eval("start_dt","{0:d/M/yyyy}") %></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="End Date">
                    <ItemTemplate><%# Eval("end_dt","{0:d/M/yyyy}") %></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="dtend" runat="server" Text='<%# Eval("start_dt","{0:d/M/yyyy}") %>'></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="dtend" Format="d/M/yyyy"></asp:CalendarExtender>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
    </div>
    <img src="div2.png" class="divid" />
    <div class="navi">
        <asp:Button ID="btprint" runat="server" Text="Print" CssClass="button2 print" OnClick="btprint_Click" />
    </div>
    </div>
</asp:Content>

