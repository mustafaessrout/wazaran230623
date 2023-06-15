<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstwareshouseentry.aspx.cs" Inherits="fm_mstwareshouseentry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="color:red;font-weight:bolder;padding:5px 5px 5px 5px">NEW WAREHOUSE/DEPO/SUBDEPO</div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                     <table>
                 <tr>
                    <td>
                      Salespoint
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="cbsalespoint" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Warehouse Code
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="txcode" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td>
                       Warehouse Name
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="txwhsname" runat="server" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td>
                        Stock Type
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="cbstocktyp" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                 <tr>
                    <td>
                        Level No
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="cblevelno" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                
                 <tr>
                    <td>
                       Address
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="txaddr" runat="server" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td>
                       City
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="cbloc" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                         <tr>
                             <td>Parent Warehouse</td>
                             <td>:</td>
                             <td>
                                 <asp:DropDownList ID="cbparent" runat="server">
                                 </asp:DropDownList>
                             </td>
                         </tr>
            </table>
                </ContentTemplate>
            </asp:UpdatePanel>
</asp:Content>

