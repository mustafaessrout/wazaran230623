<%@ Page Title="" Language="C#" MasterPageFile="~/promotor/promotor2.master" AutoEventWireup="true" CodeFile="fm_mstexhibition.aspx.cs" Inherits="fm_mstexhibition" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
         function RefreshData(sExhibit)
         {
             $get('<%=hdexhibition.ClientID%>').value = sExhibit;
             $get('<%=btrefresh.ClientID%>').click();
         }
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">

    <asp:HiddenField ID="hdexhibition" runat="server" />
    <%--<div class="container">--%>
        <div class="form-horizontal">
            <h4 class="jajarangenjang">Exhibition Entry</h4>
            <div class="h-divider"></div>
           <div class="form-group">
               <label class="control-label col-md-1">Exh Code</label>
               <div class="col-md-2">
                   <asp:TextBox ID="txexhibitcode" runat="server" CssClass="form-control"></asp:TextBox>
               </div>
               <label class="control-label col-md-1">Exh Name</label>
               <div class="col-md-3">
                   <div class="input-group">
                         <asp:TextBox ID="txname" runat="server" CssClass="form-control input-group-sm" placeholder="Enter Exhibition Name"></asp:TextBox>
                       <div class="input-group-btn">
                           <asp:LinkButton ID="btsearch" runat="server" CssClass="btn btn-primary" OnClick="btsearch_Click"><span class="glyphicon glyphicon-search"></span></asp:LinkButton>
                       </div>
                   </div>
                 
               </div>
               <label class="control-label col-md-1">Promoter Duty</label>
               <div class="col-md-4">
                   <asp:DropDownList ID="cbemployee" runat="server" CssClass="form-control"></asp:DropDownList>
               </div>
           </div>
            <div class="form-group">
                 <label class="control-label col-md-1">Start Date</label>
                <div class="col-md-2">
                    <asp:TextBox ID="dtstart" runat="server" CssClass="form-control"></asp:TextBox>
                  
                    <ajaxP:CalendarExtender ID="dtstart_CalendarExtender" runat="server" TargetControlID="dtstart" Format="d/M/yyyy">
                    </ajaxP:CalendarExtender>
                  
                </div>
                 <label class="control-label col-md-1">End Date</label>
                 <div class="col-md-2">
                     <asp:TextBox ID="dtend" runat="server" CssClass="form-control"></asp:TextBox>
                    
                 
                     <ajaxP:CalendarExtender ID="dtend_CalendarExtender" runat="server" TargetControlID="dtend" Format="d/M/yyyy">
                     </ajaxP:CalendarExtender>
                    
                 
                 </div>
                <label class="control-label col-md-1">Location</label>
                <div class="col-md-5">
                    <asp:TextBox ID="txlocation" runat="server" CssClass="form-control" placeholder="Enter exhibition location"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-md-1">Booth</label>  
                         
                <div class="col-md-11">
                    
                    <table>
                        <tr><th class="header" style="text-align:left">Section</th><th class="header" style="text-align:left">Promoter</th><th style="left" class="header">Add</th></tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="cbproduct" runat="server" CssClass="form-control"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="cbpicbooth" runat="server" CssClass="form-control"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:LinkButton ID="btadd" runat="server" CssClass="btn btn-primary" OnClick="btadd_Click">Add</asp:LinkButton>
                            </td>
                        </tr>    

                     </table>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12">
                    <asp:GridView ID="grd" CssClass="mydatagrid" RowStyle-CssClass="rows" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager" runat="server" AutoGenerateColumns="False" OnRowDeleting="grd_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="Booth">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdprodcode" Value='<%#Eval("prod_cd") %>' runat="server" />
                                    <asp:Label ID="lbprodname" runat="server" Text='<%#Eval("prod_desc") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PIC (Promotor team)">
                                
                                <ItemTemplate><%#Eval("emp_desc") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
<HeaderStyle CssClass="header"></HeaderStyle>

<PagerStyle CssClass="pager"></PagerStyle>

<RowStyle CssClass="rows"></RowStyle>
                    </asp:GridView>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12 text-center">
                   
                    <asp:LinkButton ID="btnew" runat="server" CssClass="btn btn-primary" OnClick="btnew_Click">New</asp:LinkButton>
                    <asp:LinkButton ID="btsave" runat="server" CssClass="btn btn-danger" OnClick="btsave_Click">Save</asp:LinkButton>
                     <asp:LinkButton ID="btprint" runat="server" CssClass="btn btn-info" OnClick="btprint_Click">Print</asp:LinkButton>
                    <asp:Button ID="btrefresh" runat="server" OnClick="btrefresh_Click" style="display:none" Text="Button" />
                </div>
            </div>
           

        </div>
    <%--</div>--%>
    <script>  
    $(document).ready(function () {
            $("#<%=btsearch.ClientID%>").click(function () {
               PopupCenter('lookupexhibition.aspx', 'xtf', '900', '500');  
            });
        });

       
    </script>  
</asp:Content>

