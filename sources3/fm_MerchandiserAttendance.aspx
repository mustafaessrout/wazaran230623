<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_MerchandiserAttendance.aspx.cs" Inherits="fm_MerchandiserAttendance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Attendance</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
    <link href="css/custom/metro.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div class="containers bg-white">
            <div class="h-divider"></div>
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-md-1">Employee </label>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtEmployee" CssClass="form-control ro" runat="server"></asp:TextBox>
                    </div>
                    <label class="control-label col-md-1">Period </label>
                    <div class="col-md-2">
                       <asp:TextBox ID="txtPeriod" CssClass="form-control ro" runat="server"></asp:TextBox>
                    </div>
                    <label class="control-label col-md-1">Total Attendance </label>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtTotAttendance" CssClass="form-control ro" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div>
                <asp:GridView ID="grd" runat="server" CssClass="table table-striped mygrid" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="grd_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="dateAtt" HeaderText="Attendance Date" DataFormatString="{0:d/M/yyyy}" />
                    </Columns>
                    <EditRowStyle CssClass="table-edit" />
                    <FooterStyle CssClass="table-footer" />
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page" />
                    <RowStyle />
                    <SelectedRowStyle CssClass="table-edit" />
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
