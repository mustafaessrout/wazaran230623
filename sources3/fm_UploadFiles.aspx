<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="fm_UploadFiles.aspx.cs" Inherits="fm_UploadFiles" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<body style="font-family: Tahoma,Verdana; font-size: small">
    <link href="../css/anekabutton.css" rel="stylesheet" />
    <form id="form1" runat="server">

        <style type="text/css">
 
    </style>

        <asp:ToolkitScriptManager ID="tsmanager" runat="server">
        </asp:ToolkitScriptManager>
        <div class="div-table">
            <strong>Upload Files </strong>
            <link href="css/anekabutton.css" rel="stylesheet" />
            <script>
                function closewin() {
                    window.opener.UploadFIlesData();
                    window.close();
                }

                

            </script>
            

                <div class="div-table-row">
                    <div class="div-table-col1" align="center"></div>
                    <div class="div-table-col2"></div>
                    <div class="div-table-col3"></div>
                    <div class="div-table-col4">
                                     <asp:FileUpload ID="upl" runat="server" Visible="true" />
            <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="button2 save"   OnClick="btnUpload_Click" />
                        <asp:Label ID="lblUploadMessage" runat="server" Text=""></asp:Label>
                    </div>
                </div>
        </div>
    </form>
</body>

