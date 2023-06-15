<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="SMSSupervisoor.aspx.cs" Inherits="SMSSupervisoor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="css/jquery-1.9.1.js"></script>
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="css/jquery-ui.js"></script>
    <script src="../js/jquery.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="admin/css/bootstrap.css" rel="stylesheet" />
    <script src="admin/js/bootstrap.js"></script>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        input[type=checkbox][disabled] {
            outline: 1px solid red;
        }
    </style>
    <div class="divheader">
        Branch Supervisor
    </div>
    <img src="div2.png" class="divid" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:Label ID="lblSupervisor" runat="server" Width="200" Text="Supervisor Name :"></asp:Label>
            <asp:Label ID="lblSupervisorName" runat="server" Text="Supervisor Name :"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:HiddenField id="hdfMobileNumber" runat="server" />
            <asp:Label ID="lblSMS" runat="server" Width="200" Text="SMS :"></asp:Label>
            <asp:TextBox Height="100" runat="server" Text="" Width="200" TextMode="MultiLine" ID="txtsms"></asp:TextBox>
        </ContentTemplate>
    </asp:UpdatePanel>
    <img src="div2.png" />
    <div class="navi">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <asp:Button ID="btSend" runat="server" Text="Send" CssClass="button2 save" OnClick="btSend_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
