<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstproduct.aspx.cs" Inherits="fm_mstproduct" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="divheader">Product Leveling</div>
    <div class="h-divider"></div>

    <div class="container-fluid">

        <div class="row">
            <div class="sd-bar col-sm-4 col-md-2 margin-bottom padding-bottom no-padding-left">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" class="sd-bar-container">
                <ContentTemplate>
                     <asp:TreeView ID="trv" runat="server"  ShowExpandCollapse="False" OnSelectedNodeChanged="trv_SelectedNodeChanged">
                       <%-- <HoverNodeStyle Font-Underline="True" ForeColor="#FFF" BackColor="#5D7B9D" />--%>
                        <NodeStyle  />
                        <ParentNodeStyle />
                        <SelectedNodeStyle CssClass="active"/>
                    </asp:TreeView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btsave" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>

            <div class="col-sm-8 col-md-10">
                <div style="color:red;font-weight:bolder;padding:5px 5px 5px 5px">Product</div>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>Parent</td>
                                    </tr>
                                             <tr>
                                                 <td>
                                                     <asp:DropDownList ID="cbparent" runat="server">
                                                     </asp:DropDownList>
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>Level</td>
                                             </tr>
                                             <tr>
                                                 <td>
                                                     <asp:DropDownList ID="cblevel" runat="server">
                                                     </asp:DropDownList>
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>Product Code</td>
                                             </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txprodcode" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Product Name</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txprodname" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Arabic</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txarabic" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                             <tr>
                                        <td>
                                            Product Supervisor</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="cbsupervisor" runat="server" Width="400px"></asp:DropDownList>
                                        </td>
                                    </tr>
                                             <tr>
                                                 <td><img src="div1.gif" class="divid" /></td>
                                             </tr>
                                             <tr>
                                                 <td></td>
                                             </tr>
                                             <tr>
                                                 <td class="navi">
                                                     <asp:Button ID="btadd" runat="server" Text="Add" CssClass="button2 add" OnClick="btadd_Click" /><asp:Button ID="btedit" runat="server" Text="Edit" CssClass="button2 edit" OnClick="btedit_Click" /><asp:Button ID="btsave" runat="server" Text="Save" CssClass="button2 save" OnClick="btsave_Click" />
                                                     <asp:Button ID="btdelete" runat="server" Text="Delete" CssClass="button2 save" OnClick="btdelete_Click" />
                           
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td>&nbsp;</td>
                                             </tr>
                                </table> 
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="trv" EventName="SelectedNodeChanged" />
                        </Triggers>
                    </asp:UpdatePanel>                
            </div>
            
        </div>

    </div>



</asp:Content>

