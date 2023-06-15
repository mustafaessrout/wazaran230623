<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adm.master" AutoEventWireup="true" CodeFile="fm_appsetup.aspx.cs" Inherits="admin_fm_appsetup" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function ItemSelected(sender, e)
        {
            $get('<%=hdemp2.ClientID%>').value = e.get_value();
           $get('<%=txemp.ClientID%>').setAttribute('class', 'form-control ro');
          
        }

    </script>
    <link href="../css/anekabutton.css" rel="stylesheet" />
    <link href="../css/sweetalert.css" rel="stylesheet" />
    <script src="../js/sweetalert.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentbody" Runat="Server">
   
    <div class="container">
         <h3>Approval Pattern</h3>
    <img src="../div2.png" class="divid" />
        <div class="form-horizontal">
            <div class="form-group">
                
                 <label class="control-label col-md-1">Type</label>
                <div class="col-md-2">
                    <asp:DropDownList ID="cbtype" runat="server" CssClass="form-control-static" AutoPostBack="True" OnSelectedIndexChanged="cbtype_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <label class="control-label col-md-1">Emp</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txemp" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txemp_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txemp" UseContextKey="True" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" MinimumPrefixLength="1" OnClientItemSelected="ItemSelected" CompletionListCssClass="divemp" ShowOnlyCurrentWordInCompletionListItem="true">
                    </asp:AutoCompleteExtender>
                </div>
                <label class="control-label col-md-1">Min</label>
                <div class="col-md-1">
                    <asp:TextBox ID="txmin" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <label class="control-label col-md-1">Max</label>
                <div class="col-md-1">
                    <asp:TextBox ID="txmax" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:Button ID="btadd" runat="server" Text="Add" CssClass="btn btn-default" OnClick="btadd_Click" />
                </div>
            </div>
        </div>
        <div class="form-horizontal">
            <div class="col-md-8">
            <asp:GridView ID="grdapp" runat="server" AutoGenerateColumns="False" CellPadding="0" CssClass="mygrid" Width="100%" OnRowDeleting="grdapp_RowDeleting" OnSelectedIndexChanging="grdapp_SelectedIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Type">
                        <ItemTemplate>
                            <asp:Label ID="lbdoctype" runat="server" Text='<%# Eval("doc_typ") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdemp" runat="server" Value='<%# Eval("emp_cd") %>' />
                            <%# Eval("emp_desc") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Min Value">
                        <ItemTemplate><%# Eval("min_amt") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Max Value">
                        <ItemTemplate><%# Eval("max_amt") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowSelectButton="True" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
            </div>
        </div>
   </div>
    <div class="navi">
        <asp:Button ID="btnew" runat="server" Text="NEW" CssClass="btn btn-block" OnClick="btnew_Click" />
    </div>
    <asp:HiddenField ID="hdemp2" runat="server" />
    <div id="divemp" style="font-size:x-small;font-family:Calibri"></div>
    </asp:Content>

