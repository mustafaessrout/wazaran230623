<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupgraph.aspx.cs" Inherits="eis_lookupgraph" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="width:100%;height:100%">
    <div style="width:100%;height:100%">
        <asp:Chart ID="charteis" runat="server" Width="954px" Height="800px">
            <Series>
                <asp:Series Name="Series1"></asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="charteis">
                    <AxisX TextOrientation="Rotated90">
                    </AxisX>
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </div>
    </form>
</body>
</html>
