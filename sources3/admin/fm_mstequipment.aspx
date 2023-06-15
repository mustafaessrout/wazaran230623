<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adm.master" AutoEventWireup="true" CodeFile="fm_mstequipment.aspx.cs" Inherits="admin_fm_mstequipment" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
    <link href="../css/newbootstrap.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentbody" Runat="Server">
    <div class="container">
        <div class="form-horizontal">
            <h4 class="jajarangenjang">Equipment Master</h4>
            <div class="h-divider"></div>
            <div class="form-group">
               <label class="control-label col-md-1">Name</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txequipname" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <label class="control-label col-md-1">Type</label>
                <div class="col-md-2">
                    <asp:DropDownList ID="cbtype" runat="server" CssClass="form-control-static"></asp:DropDownList>
                </div>
                <label class="control-label col-md-1">Status</label>
                <div class="col-md-2">
                    <asp:DropDownList ID="cbstatus" runat="server" CssClass="form-control-static"></asp:DropDownList>
                </div>
                <label class="control-label col-md-1">Own</label>
                <div class="col-md-2">
                    <asp:DropDownList ID="cbowner" runat="server" CssClass="form-control-static"></asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-md-1">Budget</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txbudget" runat="server" CssClass="form-control-static"></asp:TextBox>
                    </div>
                <label class="control-label col-md-1">Branded</label>
                <div class="col-md-2">
                    <asp:DropDownList ID="cbbrand" runat="server" CssClass="form-control-static"></asp:DropDownList>
                </div>
                 <label class="control-label col-md-1">imei</label>
                <div class="col-md-2">
                    <asp:TextBox ID="tximei" runat="server" CssClass="form-control-static"></asp:TextBox>
                </div>
                 <label class="control-label col-md-1">model</label>
                  <div class="col-md-2">
                      <asp:TextBox ID="txmodel" CssClass="form-control-static" runat="server"></asp:TextBox>
                      </div>
            </div>
            <div class="form-group">
                <label class="control-label col-md-1">Pin1</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txpin1" runat="server" CssClass="form-control-static"></asp:TextBox>
                </div>
                <label class="control-label col-md-1">Pin2</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txpint2" runat="server" CssClass="form-control-static"></asp:TextBox>
                </div>
                <label class="control-label col-md-1">PUK1</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txpuk1" runat="server" CssClass="form-control-static"></asp:TextBox>
                </div>
                <label class="control-label col-md-1">PUK2</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txpuk2" runat="server" CssClass="form-control-static"></asp:TextBox>
                </div>
            </div>
             <div class="form-group">
                <label class="control-label col-md-1">Emp</label>
                <div class="col-md-2">
                     <asp:TextBox ID="txemp" runat="server" CssClass="form-control-static"></asp:TextBox>
                     <asp:AutoCompleteExtender ID="txemp_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txemp" UseContextKey="True" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" MinimumPrefixLength="1"> 
                     </asp:AutoCompleteExtender>
                </div>
                <label class="control-label col-md-1">Phone No</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txphoneno" runat="server" CssClass="form-control-static"></asp:TextBox>
                </div>
                <label class="control-label col-md-1">SeriesNo</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txseriesno" runat="server" CssClass="form-control-static"></asp:TextBox>
                </div>
                <label class="control-label col-md-1">Salespoint</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txsalespoint" runat="server" CssClass="form-control-static"></asp:TextBox>
                </div>
            </div>
             <div class="form-group">
                <label class="control-label col-md-1">Package</label>
                <div class="col-md-2">
                    <asp:DropDownList ID="cbpackage" runat="server" CssClass="form-control-static"></asp:DropDownList>
                </div>
                <label class="control-label col-md-1">Phone No</label>
                <div class="col-md-2">
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control-static"></asp:TextBox>
                </div>
                <label class="control-label col-md-1">SeriesNo</label>
                <div class="col-md-2">
                    <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control-static"></asp:TextBox>
                </div>
                <label class="control-label col-md-1">Salespoint</label>
                <div class="col-md-2">
                    <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control-static"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12">
                    <asp:GridView ID="grd" runat="server" CssClass="mygrid" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Equip Name"></asp:TemplateField>
                            <asp:TemplateField HeaderText="Brand"></asp:TemplateField>
                            <asp:TemplateField HeaderText="Ownership"></asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="h-divider"></div>
            <div class="form-group">
                <div class="col-md-12">
                    <div class="navi">
                        <asp:LinkButton ID="btsave" runat="server" CssClass="btn btn-primary btn-lg">Save</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

