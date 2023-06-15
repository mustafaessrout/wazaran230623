<%@ Page Title="" Language="C#" MasterPageFile="~/promotor/promotor2.master" AutoEventWireup="true" CodeFile="fm_mstoutsource.aspx.cs" Inherits="promotor_fm_mstoutsource" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script src="Content/jquery.min.js"></script>
    <script>
        function SelectData(sVal)
        {
            $get('<%=hdiqomah.ClientID%>').value = sVal;
            $get('<%=txidno.ClientID%>').value = sVal;
            $get('<%=btlookup.ClientID%>').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <asp:HiddenField ID="hdiqomah" runat="server" />
    <%--<div class="container">--%>
        <asp:HiddenField ID="hdcity" runat="server" />
        <div class="form-horizontal" style="font-family:Calibri;font-size:small">
            <h4 class="jajarangenjang">Outsourcing Data</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <label class="control-label col-md-1 input-group-sm">ID No</label>
                <div class="col-md-2">
                <div class="input-group">
                   
                            <asp:TextBox ID="txidno" CssClass="form-control" runat="server"></asp:TextBox>
                    <div class="input-group-btn">
                        <asp:LinkButton ID="btsearch" runat="server" CssClass="btn btn-primary"><span class="glyphicon glyphicon-search"></span></asp:LinkButton>
                    </div>
                   
                    
                </div>
                </div>
                <label class="control-label col-md-1">Type</label>
                <div class="col-md-2">
                    <asp:DropDownList ID="cbidtype" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                 <label class="control-label col-md-1">Name</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txname" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                 <label class="control-label col-md-1">Gender</label>
                <div class="col-md-2">
                    <asp:DropDownList ID="cbgender" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                 <label class="control-label col-md-1">Mobile</label>
            <div class="col-md-2">
                <asp:TextBox ID="txmobile" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
                <label class="control-label col-md-1">Marital</label>
                <div class="col-md-2">
                    <asp:DropDownList ID="cbmarried" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-md-1">Nationality</label>
                <div class="col-md-2">
                    <asp:DropDownList ID="cbnationality" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
                 <label class="control-label col-md-1">Address</label>
                <div class="col-md-5">
                    <asp:TextBox ID="txaddress" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                 <label class="control-label col-md-1">City</label>
                <div class="col-md-2">
                    <asp:DropDownList ID="cbcity" CssClass="form-control" runat="server"></asp:DropDownList>
                 </div>
            </div>
            <div class="form-group">
                <div class="col-md-12" style="text-align:center">
                    <asp:LinkButton ID="btnew" runat="server" CssClass="btn btn-primary" OnClick="btnew_Click">New</asp:LinkButton>
                     <asp:LinkButton ID="btedit" runat="server" CssClass="btn btn-warning" OnClick="btedit_Click">Edit</asp:LinkButton>
                    <asp:LinkButton ID="btsave" runat="server" CssClass="btn btn-info" OnClick="btsave_Click">Save</asp:LinkButton>
                    <asp:LinkButton ID="btprint" runat="server" CssClass="btn btn-danger" OnClick="btprint_Click">Print</asp:LinkButton>
                    <asp:Button ID="btlookup" runat="server" OnClick="btlookup_Click" Text="Button" style="display:none"/>
                </div>
            </div>
        </div>
 <%--   </div>--%>
       <script>
           $(document).ready(function () {
               $("#<%=btsearch.ClientID%>").click(function () {
            PopupCenter('lookupoutsource.aspx', 'xtf', '900', '500');
        });
    });


    </script>  
</asp:Content>

