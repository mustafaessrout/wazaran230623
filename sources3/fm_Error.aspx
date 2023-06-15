<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_Error.aspx.cs" Inherits="fm_Error" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />

    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/index.js"></script>
    <script src="js/jquery.floatThead.js"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />

    <style>
        .table-header {
            font-weight: normal;
        }
    </style>
    <script type="text/javascript">
        function closewin() {
            window.opener.updpnl();
            window.close();
        }

        function backPage() {
            //alert("sd1");
            //window.location.reload();
            //window.history.back();
            //window.history.go(-1);
            window.history.back();
            return false;
        }

        $(document).ready(function () {
            <%-- $('#' + '<%=Submit1.ClientID%>').click(function () {
                alert("sd");

                //window.location.reload();
                //window.history.back();
                //return false;
                //window.history.go(-1); 
            });--%>


            <%--$('#' + '<%=Submit1.ClientID%>').click(function () {
                //alert("sd");
                //window.location.reload();
                window.history.back();
                return false;
                //window.history.go(-1); 
            });--%>


            //$("Submit1")
        });

    </script>
    <script runat="server">
        protected void Submit_Click(object sender, EventArgs e)
        {
            // Exception handled by Page_Error
            //throw new Exception("MyException");

        }

        private void Page_Error(object sender, EventArgs e)
        {
            // Get last error from the server
            Exception exc = Server.GetLastError();

            Response.Write("<h2>Default Page Error</h2>\n");
            Response.Write("<p>Server error: " + exc.Message + "</p>\n");

            // Clear the error from the server
            Server.ClearError();

            //
        }
    
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form-horizontal">

        <%--OnClick="Submit1_Click" OnClientClick="return backPage();"--%>
        <asp:Label ID="lblError" Text="" Style="color: red;" runat="server"></asp:Label>
        <button type="button" onclick="backPage();" title="back">Back</button>
        <%--<asp:Button
            ID="Submit1"
            runat="server"
            OnClientClick="backPage();"
            Text="test" />--%>
    </div>
</asp:Content>

