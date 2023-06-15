<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_jobschedul.aspx.cs" Inherits="fm_jobschedul" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        Job Setting
    </div>
    <img src="div2.png" class="divid" />
    <div>
        <table>
            <tr>
                <td>Salespoint</td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="cbsalespoint" runat="server"></asp:DropDownList>
                </td>
                <td>
                    Job Name
                </td>
                <td>:</td>
                <td>
                     <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td>Job Name</td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="cbjob" runat="server"></asp:DropDownList>
                </td>
            </tr>

        </table>
    </div>
</asp:Content>

