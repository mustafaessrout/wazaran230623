<%@ Page Title="" Language="C#" MasterPageFile="~/eis/eis.master" AutoEventWireup="true" CodeFile="fm_salescoll.aspx.cs" Inherits="eis_fm_salescoll" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
        function CustSelected(sender, e)
        {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
        }
    </script>
  <%--  <script>
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

    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <asp:HiddenField ID="hdcust" runat="server" />
    <div class="form-horizontal" style="font-family:Calibri;font-size:small">
        <h4 class="jajarangenjang">Sales VS Collection</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <label class="control-label col-md-1">Year</label>
            <div class="col-md-3">
                <asp:DropDownList ID="cbyear" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <label class="control-label col-md-1">Salespoint</label>
            <div class="col-md-3">
                <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <label class="control-label col-md-1">Channel</label>
            <div class="col-md-3">
                <asp:DropDownList ID="cbchannel" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
           
        </div>
        <div class="form-group">
             <label class="control-label col-md-1">Cust</label>
            <div class="col-md-7">
                <div class="input-group">
                     <asp:TextBox ID="txcustomer" CssClass="form-control input-group-sm" runat="server"></asp:TextBox>
                     <asp:AutoCompleteExtender ID="txcustomer_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txcustomer" UseContextKey="True" CompletionInterval="1" CompletionSetCount="10" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false">
                     </asp:AutoCompleteExtender>
                     <div class="input-group-btn">
                         <asp:CheckBox ID="chall" runat="server" AutoPostBack="true" CssClass="form-control" Text=" ALL" />
                     </div>
                     <div class="col-md-1">
                         <asp:LinkButton ID="btnsearch" runat="server"  OnClientClick="javascript:ShowProgress();" CssClass="btn btn-primary" OnClick="btnsearch_Click">Search</asp:LinkButton>
                     </div>
                </div>
               
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-12">
            <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="mydatagrid" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager">
                <Columns>
                    <asp:TemplateField HeaderText="Year">
                        <ItemTemplate><%#Eval("MonthYear") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Month">
                        <ItemTemplate><%#Eval("MonthName") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sales">
                        <ItemTemplate><%#Eval("Sales","{0:n3}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Collection">
                        <ItemTemplate><%#Eval("Collection","{0:n3}") %></ItemTemplate>
                    </asp:TemplateField>

                    <%--<asp:TemplateField HeaderText="JAN Sls">
                        <ItemTemplate><%#Eval("jansls","{0:n3}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Col">
                        <ItemTemplate><%#Eval("jancoll","{0:n3}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FEB Sls">
                        <ItemTemplate><%#Eval("febsls","{0:n3}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Col">
                        <ItemTemplate><%#Eval("febcoll") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MAR Sls">
                        <ItemTemplate><%#Eval("marsls","{0:n3}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Col">
                        <ItemTemplate><%#Eval("marcoll","{0:n3}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="APR Sls">
                        <ItemTemplate><%#Eval("aprsls") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Col">
                        <ItemTemplate><%#Eval("aprcoll","{0:n3}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MAY Sls">
                        <ItemTemplate><%#Eval("maysls","{0:n3}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Col">
                        <ItemTemplate><%#Eval("maycoll") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="JUN Sls">
                        <ItemTemplate>
                            <%#Eval("junsls","{0:n3}") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Col">
                        <ItemTemplate>
                            <%#Eval("juncoll","{0:n3}") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="JUL Sls">
                        <ItemTemplate>
                            <%#Eval("julsls","{0:n3}") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Col">
                        <ItemTemplate>
                            <%#Eval("julcoll","{0:n3}") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AUG Sls">
                        <ItemTemplate><%#Eval("augsls","{0:n3}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Col">
                        <ItemTemplate><%#Eval("augcoll","{0:n3}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SEP Sls">
                        <ItemTemplate><%#Eval("sepsls","{0:n3}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Col">
                        <ItemTemplate><%#Eval("sepcoll","{0:n3}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OCT Sls">
                        <ItemTemplate><%#Eval("octsls","{0:n3}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Col">
                        <ItemTemplate><%#Eval("octcoll","{0:n3}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NOV Sls">
                        <ItemTemplate><%#Eval("novsls","{0:n3}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Col">
                        <ItemTemplate><%#Eval("novcoll","{0:n3}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DES Sls">
                        <ItemTemplate><%#Eval("decsls","{0:n3}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Col">
                        <ItemTemplate><%#Eval("deccoll") %></ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
        </div>
       </div>
        <div class="form-group">
            <div class="col-md-12">
                <asp:GridView ID="grddtl" runat="server" CssClass="mydatagrid" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="grddtl_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Cust Code">
                            <ItemTemplate><%#Eval("cust_cd") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cust Name">
                            <ItemTemplate><%#Eval("cust_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Salespoint">
                            <ItemTemplate><%#Eval("salespointcd") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sales">
                            <ItemTemplate><%#Eval("sales") %></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
  <%--  <div class="divmsg loading-cont" id="pnlmsg" >
    <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>--%>

</asp:Content>

