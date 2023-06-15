<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_employeeadvanced.aspx.cs" Inherits="fm_employeeadvanced" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function EmpSelected(sender, e) {
            $get('<%=hdemployee.ClientID%>').value = e.get_value();
           <%-- $('#<%=txsearchemployee.ClientID%>').addClass("form-control ro");--%>

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdemployee" runat="server" />
    <div class="container">
        <h4 class="jajarangenjang">Employee Advanced Report</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <div class="row margin-bottom">
                <label class="control-label col-md-1">Employee</label>
                <div class="col-md-3">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txsearchemployee" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txsearchemployee_AutoCompleteExtender" runat="server" TargetControlID="txsearchemployee" FirstRowSelected="false" EnableCaching="false" MinimumPrefixLength="1" CompletionInterval="10" CompletionSetCount="1" UseContextKey="true" ServiceMethod="EmployeeList" OnClientItemSelected="EmpSelected">
                            </asp:AutoCompleteExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">All Employees</label>
                <div class="col-md-1">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:CheckBox ID="chAll" runat="server" AutoPostBack="true" OnCheckedChanged="chAll_CheckedChanged" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">Start Date</label>
                <div class="col-md-2">
                    <asp:TextBox ID="dtstart" CssClass="form-control" runat="server" onkeydown="return false"></asp:TextBox>
                    <asp:CalendarExtender CssClass="date" ID="dtstart_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtstart">
                    </asp:CalendarExtender>
                </div>
                <label class="control-label col-md-1">End Date</label>
                <div class="col-md-2">
                    <asp:TextBox ID="dtend" CssClass="form-control" runat="server" onkeydown="return false"></asp:TextBox>
                    <asp:CalendarExtender CssClass="date" ID="dtend_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtend">
                    </asp:CalendarExtender>
                </div>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div class="form-group">
                    <div class="center">
                        <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn btn-sm btn-primary" OnClick="btprint_Click" />
                         &nbsp;&nbsp;
                        <asp:Button ID="btprintAll" runat="server" Text="Print All" CssClass="btn btn-sm btn-primary" OnClick="btprintAll_Click" />
                         &nbsp;&nbsp;
                        <asp:Button ID="btprintAllSummary" runat="server" Text="Print All Summary" CssClass="btn btn-sm btn-primary " OnClick="btprintAllSummary_Click" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

