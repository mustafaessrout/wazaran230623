<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_supporting_detail.aspx.cs" Inherits="reporting_fm_supporting_detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <script src="../js/jquery.min.js"></script>
    <script src="../js/bootstrap.min.js"></script> 
    <link href="../css/sweetalert.css" rel="stylesheet" />
    <script src="../js/sweetalert.min.js"></script>
    <script src="../js/sweetalert-dev.js"></script>
    <style>
        .message-bubble 
        {
            padding: 10px 0px 10px 0px;
        }
        .message-bubble:nth-child(even) { background-color: #F5F5F5; }
        .message-bubble > *
        {
            padding-left: 10px;    
        }
        .panel-body { padding: 0px; }
        .panel-heading { background-color: #3d6da7 !important; color: white !important; }
    </style>
</head>
<body>

    <div class="container">
        <form id="form1" runat="server">
            <asp:ToolkitScriptManager ID="ScriptManager1" runat="server"></asp:ToolkitScriptManager>

            <div class="row">
                <div class="panel panel-default">
                  <div class="panel-heading"><h4>Title : <asp:Label ID="lbtitle" runat="server"></asp:Label></h4>
                      <p>Status: <asp:Label ID="lbstatus" runat="server"></asp:Label></p>
                      <p>By: <asp:Label ID="lbcreated" runat="server"></asp:Label></p>
                  </div>
                  <div class="panel-body">
                    <div class="container">
                            <asp:GridView ID="grdchat" runat="server" Width="100%" CssClass="table table-striped mygrid" AutoGenerateColumns="false" GridLines="None" ShowHeader="false" ShowFooter="false" >
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="lbmessage" runat="server" Text='<%# Eval("message") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>           
                                </Columns>
                            </asp:GridView>
                    </div>
                    <div class="panel-footer">
                         <div class="input-group">
                            <asp:TextBox ID="txMessage" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:Button ID="btreply" runat="server" Text="Reply" CssClass="btn btn-default" OnClick="btreply_Click"/>
                            </span>
                        </div>
                    </div>
                    <div class ="panel-footer text-center">
                        <div class="input-group">
                            <span class="input-group-btn">
                                <asp:Button ID="btclose" runat="server" Text="Close" CssClass="btn btn-danger" OnClick="btclose_Click"/>
                                <asp:Button ID="btopen" runat="server" Text="Re-Open" CssClass="btn btn-default" OnClick="btopen_Click"/>
                            </span>
                        </div>
                    </div>
                  </div>
                </div>
            </div>

        </form>
    </div>
</body>
</html>
