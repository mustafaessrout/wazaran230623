<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_reqcustomerlocation_lookup.aspx.cs" Inherits="fm_reqcustomerlocation_lookup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script> 
    <link href="css/sweetalert.css" rel="stylesheet" />
    <script src="js/sweetalert.min.js"></script>
    <script src="js/sweetalert-dev.js"></script>

    <script>
        function openwindow(url) {
            mywindow = window.open(url, "mywindow", "location=1,status=1,scrollbars=1,  width=800,height=600");
            mywindow.moveTo(400, 200);
        }
        function vEnableShow() {
            $get('showmessagex').className = "showmessage";
        }

        function vDisableShow() {
            $get('showmessagex').className = "hidemessage";
        }
    </script>

</head>
<body>
    <div class="container-fluid">
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server"></asp:ToolkitScriptManager>
        
        <div class="form-horizontal">
            <div class="row">
                <div class="col-sm-12">
                    <ol class="breadcrumb">
                        <li><h3>Multiple Location Customer (<asp:Label ID="lbcustomer" runat="server"></asp:Label>)</h3></li>
                    </ol>
                </div>
            </div>
            
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <h4><label for="title" class="col-xs-12 col-form-label col-form-label-sm">List Location.</label></h4>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="table-responsive">
                    <asp:GridView ID="grdlocation" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="Location Type">
                                <ItemTemplate>
                                    <asp:Label ID="lbtype" runat="server" Text='<%# Eval("loc_type") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Latitude & Longitude">
                                <ItemTemplate>
                                    <a href="javascript:openwindow('https://www.google.com/maps/?q=<%# Eval("location") %>');">
                                        <asp:Label ID="lblocation" runat="server" Text='<%# Eval("location") %>'></asp:Label>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>         
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

        </div>

    </form>
    </div>
</body>
</html>
