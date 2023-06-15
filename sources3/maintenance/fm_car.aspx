<%@ Page Title="" Language="C#" MasterPageFile="~/maintenance/mtn.master" AutoEventWireup="true" CodeFile="fm_car.aspx.cs" Inherits="maintenance_fm_car" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
        function SelectData(sValue)
        {
            $get('<%=hdcar.ClientID%>').value = sValue;
            $get('<%=btlookup.ClientID%>').click();
        }

        function EmpSelected(sender, e)
        {
            $get('<%=hdemp.ClientID%>').value = e.get_value();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <asp:HiddenField ID="hdcar" runat="server" />
    <asp:HiddenField ID="hdemp" runat="server" />
    <div class="form-horizontal" style="font-size:small;font-family:Calibri">
        <h4 class="jajarangenjang">Master Vehicle / Car</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <label class="control-label col-md-1">Vhc code</label>
            <div class="col-md-3">
                <div class="input-group">
                    <asp:Label ID="lbvhcode" runat="server" CssClass="form-control input-group-sm"></asp:Label>
                    <div class="input-group-btn">
                        <asp:LinkButton ID="btsearch" CssClass="btn btn-primary" runat="server"><i class="fa fa-search"></i></asp:LinkButton>
                    </div>
                </div>
                
            </div>
             <label class="control-label col-md-1">Vhc code</label>
            <div class="col-md-3">
                <asp:TextBox ID="txplateno" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
             <label class="control-label col-md-1">Engine No</label>
            <div class="col-md-3">
                <asp:TextBox ID="txengineno" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">Plate Type</label>
            <div class="col-md-3">
                <asp:DropDownList ID="cbplatetype" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
             <label class="control-label col-md-1">Color</label>
            <div class="col-md-3">
                <asp:DropDownList ID="cbcolor" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
            <label class="control-label col-md-1">Branded</label>
            <div class="col-md-3">
                <asp:DropDownList ID="cbbranded" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">Model</label>
            <div class="col-md-3">
                <asp:DropDownList ID="cbmodel" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
            <label class="control-label col-md-1">Type</label>
            <div class="col-md-3">
                <asp:DropDownList ID="cbvhctype" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
             <label class="control-label col-md-1">Year</label>
             <div class="col-md-3">
                 <asp:TextBox ID="txyear" CssClass="form-control" runat="server"></asp:TextBox>
             </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">Body No</label>
            <div class="col-md-3">
                 <asp:TextBox ID="txbodyno" CssClass="form-control" runat="server"></asp:TextBox>
             </div>
            <label class="control-label col-md-1">Purchase Date</label>
            <div class="col-md-3">
                 <asp:TextBox ID="dtpurchase" CssClass="form-control" runat="server"></asp:TextBox>
                 <asp:CalendarExtender ID="dtpurchase_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtpurchase">
                 </asp:CalendarExtender>
             </div>
            <label class="control-label col-md-1">Expired Date</label>
            <div class="col-md-3">
                 <asp:TextBox ID="dtexpired" CssClass="form-control" runat="server"></asp:TextBox>
                 <asp:CalendarExtender ID="dtexpired_CalendarExtender" Format="d/M/yyyy" runat="server" TargetControlID="dtexpired">
                 </asp:CalendarExtender>
             </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">Business Unit</label>
            <div class="col-md-3">
                <asp:DropDownList ID="cbbusinessunit" CssClass="form-control" runat="server"></asp:DropDownList>
             </div>
            <label class="control-label col-md-1">Salespoint</label>
            <div class="col-md-3">
                <asp:DropDownList ID="cbsalespoint" CssClass="form-control" runat="server"></asp:DropDownList>
             </div>
             <label class="control-label col-md-1">Insurance No</label>
            <div class="col-md-3">
                <asp:TextBox ID="txinsuranceno" CssClass="form-control" runat="server"></asp:TextBox>
             </div>
        </div>
         <div class="form-group">
            <label class="control-label col-md-1">Purchase Price</label>
            <div class="col-md-3">
                <asp:TextBox ID="txpurchaseprice" CssClass="form-control" runat="server"></asp:TextBox>
             </div>
            <label class="control-label col-md-1">Box Price</label>
            <div class="col-md-3">
                <asp:TextBox ID="txboxprice" CssClass="form-control" runat="server"></asp:TextBox>
             </div>
             <label class="control-label col-md-1">Box Date</label>
            <div class="col-md-3">
                <asp:TextBox ID="dtbox" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="dtbox_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtbox">
                </asp:CalendarExtender>
             </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">Remark</label>
            <div class="col-md-11">
                 <asp:TextBox ID="txremark" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">Ownership</label>
            <div class="col-md-3">
                  <asp:DropDownList ID="cbownership" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
             <label class="control-label col-md-1">Status</label>
            <div class="col-md-3">
                  <asp:DropDownList ID="cbvhcstatus" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
            <label class="control-label col-md-1">Employee</label>
            <div class="col-md-3">
                <asp:TextBox ID="txemp" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:AutoCompleteExtender ID="txemp_AutoCompleteExtender" EnableCaching="false" FirstRowSelected="false" CompletionSetCount="1" CompletionInterval="10" MinimumPrefixLength="1" OnClientItemSelected="EmpSelected" runat="server" TargetControlID="txemp" ServiceMethod="GetCompletionList" UseContextKey="True">
                </asp:AutoCompleteExtender>
            </div>
        </div>
        <div class="h-divider"></div>
    </div>
    <script>
        $(document).ready(function () {
            $("#<%=btsearch.ClientID%>").click(function () {
            PopupCenter('lookupvhc.aspx', 'xtf', '900', '500');
        });
    });


    </script>  

    <div style="text-align:center">
        <asp:LinkButton ID="btnew" runat="server" CssClass="btn btn-primary" OnClick="btnew_Click">New</asp:LinkButton>
        <asp:LinkButton ID="btedit" runat="server" CssClass="btn btn-info" OnClick="btedit_Click">Edit</asp:LinkButton>
          <asp:LinkButton ID="btsave" runat="server" CssClass="btn btn-success" OnClick="btsave_Click">Save</asp:LinkButton>
        <asp:LinkButton ID="btprint" runat="server" CssClass="btn btn-danger" OnClick="btprint_Click">Print All</asp:LinkButton>
        <asp:Button ID="btlookup" runat="server" Text="Button" OnClick="btlookup_Click" style="display:none"/>
        <asp:LinkButton ID="btprintvhc" runat="server" CssClass="btn btn-danger" OnClick="btprintvhc_Click">Print</asp:LinkButton>
    </div>
</asp:Content>

