<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstrpsForBranch.aspx.cs" Inherits="fm_mstrpsForBranch" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function CustSelected(sender, e)
        {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
        }

        function openwindow() {
            var oNewWindow;
            var cusID = $get('<%=hdcust.ClientID%>').value;
            var salesID = $get('<%=cbsalesman.ClientID%>').value;
            var rpstypeID = $get('<%=cbrpstype.ClientID%>').value;
            if (salesID == '' || cusID == '' || rpstypeID == '') {
                alert('Please select Customer.');
            }
            else {
                oNewWindow = window.open("fm_mstRPSForecastPopup.aspx?cusID=" + cusID + "&salesID=" + salesID + "&rpstypeID=" + rpstypeID, "lookup", "height=700,width=1100,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
            }

        }
    </script>
    
<script type = "text/javascript">
    function SetContextKey() {
      //  alert('this yes');
        $find('<%=txcustsearch_AutoCompleteExtender.ClientID%>').set_contextKey($get('<%=cbsalesman.ClientID%>').value);
     //   alert('this no');
    }
</script>
    <style>
        .main-content #mCSB_2_container{
                min-height: 520px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">RPS Entry</div>
    <div class="h-divider"></div>
    <div class="container-fluid">
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <div class="">
                        <div class="clearfix form-group">
                            <div class="col-sm-6">
                                <label class="control-label col-sm-2">RPS Type</label>
                                <div class="col-sm-10 drop-down">
                                    <asp:DropDownList ID="cbrpstype" runat="server" OnSelectedIndexChanged="cbrpstype_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="clearfix">
                            <div class="col-sm-6 form-group">
                                <label class="col-sm-2 control-label">Salesman Code</label>
                                <div class="col-sm-10 drop-down">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>

                                            <asp:DropDownList ID="cbsalesman" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbsalesman_SelectedIndexChanged" CssClass="scs form-control"  >
                                            </asp:DropDownList>

                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cbrpstype" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="col-sm-6 form-group no-padding-right">
                                <label class="col-sm-2 control-label">Day Visit</label>
                                <div class="col-sm-10 drop-down">
                                    <asp:DropDownList ID="cbdaycode" runat="server" OnSelectedIndexChanged="cbdaycode_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control"></asp:DropDownList></td>
                                </div>
                                
                            </div>
                        </div>
                        <div class="clearfix margin-bottom">
                            <div class="col-sm-6">
                                <label class="col-sm-2 control-label">Customer</label>
                                <div class="col-sm-10 ">
                                    <div class="input-group">
                                         <asp:TextBox ID="txcustsearch" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txcustsearch_AutoCompleteExtender" UseContextKey="true" ContextKey="" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txcustsearch"  CompletionListElementID="divwidthx" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="CustSelected" MinimumPrefixLength="1" ShowOnlyCurrentWordInCompletionListItem="true"> 
                                        </asp:AutoCompleteExtender>
                
                                        <asp:HiddenField ID="hdcust" runat="server" />
                                        <div class="input-group-btn">
                                             <asp:Button ID="btadd" runat="server" Text="Add" CssClass="btn-success btn btn-add" OnClick="btadd_Click"/>
                                        </div>
                                    </div>
                                </div>
                               
                            </div>
                            
                        </div>
                        <div class="clearfix ">
                                <div class="well well-sm clearfix">
                                     <div class="col-sm-offset-3 col-sm-6 margin-bottom">
                                        <asp:RadioButtonList ID="rdby" runat="server" CssClass="radio no-margin radio-space-around" RepeatDirection="Horizontal" DataValueField="0" AutoPostBack="True" OnSelectedIndexChanged="rdby_SelectedIndexChanged">
                                            <asp:ListItem Value="ByDay">By Day</asp:ListItem>
                                            <asp:ListItem Value="ByDate">By Date</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-12 ">
                                
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" class="">
                                            <ContentTemplate>
                                                <asp:Label ID="lbdate" runat="server" Text="Date" CssClass="col-sm-2 control-label margin-bottom"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" class="drop-down-date col-sm-10">
                                            <ContentTemplate>
                                                <asp:TextBox ID="dtrps_dt" runat="server" CssClass="makeitreadonly  margin-bottom form-control" ></asp:TextBox>
                                                <asp:CalendarExtender CssClass="date" ID="dtrps_dt_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtrps_dt">
                                                </asp:CalendarExtender>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    
                                        <label class="col-sm-2 control-label">Period</label>
                                        <div class="col-sm-10 drop-down">
                                            <asp:DropDownList ID="cbMonthCD" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbMonthCD_SelectedIndexChanged" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        
                                </div>
                                </div>
                            </div>
                    </div>
                  
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="divgrid">
        <asp:UpdatePanel ID="updcust" runat="server">
            <ContentTemplate>
                <div class="overflow-x" style="max-height:150px">

                    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False"  GridLines="None" OnRowDeleting="grd_RowDeleting" CssClass="table table-fix table-striped mygrid">
                        <AlternatingRowStyle  />
                        <Columns>
                            <asp:TemplateField HeaderText="Sequence">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("sequenceno") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Code">
                                <ItemTemplate>
                                   <asp:Label ID="lbcustcode" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Name">
                                <ItemTemplate>
                                    <%# Eval("cust_nm") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Address">
                                <ItemTemplate><%# Eval("addr") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="City">
                                <ItemTemplate><%# Eval("phone_no") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Phone No"></asp:TemplateField>
                            <asp:TemplateField HeaderText="Price"></asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer"/>
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page" />
                        <RowStyle  />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                    
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btadd" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="cbdaycode" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="cbsalesman" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    
    <div class="h-divider"></div>

    <div>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grdbydate" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mygrid" GridLines="None" OnRowDeleting="grd_RowDeleting">
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="RPS Date">
                            <ItemTemplate>
                                <%# Eval("rps_dt","{0:d/M/yyyy}") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer Code">
                            <ItemTemplate>
                                <asp:Label ID="lbcustcode0" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer Name">
                            <ItemTemplate>
                                <%# Eval("cust_nm") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Address">
                            <ItemTemplate>
                                <%# Eval("addr") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="City">
                            <ItemTemplate>
                                <%# Eval("phone_no") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Phone No"></asp:TemplateField>
                        <asp:TemplateField HeaderText="Price"></asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                    <EditRowStyle CssClass="table-edit" />
                    <FooterStyle CssClass="table-footer" />
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page" />
                    <RowStyle  />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
    <div class="navi margin-bottom">
        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn-warning btn btn-save" OnClick="btsave_Click" />
        <asp:Button ID="brprint" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="brprint_Click" />
    </div>
    <table>
        <tr>
            <td style="position:relative">
                <div id="divwidthx"></div>
            </td>
        </tr>
    </table>
</asp:Content>

