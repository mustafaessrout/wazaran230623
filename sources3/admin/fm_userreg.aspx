<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adm.master" AutoEventWireup="true" CodeFile="fm_userreg.aspx.cs" Inherits="admin_fm_userreg" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <link href="../css/sweetalert.css" rel="stylesheet" />
    <script src="../js/sweetalert-dev.js"></script>
    <script src="../js/sweetalert.min.js"></script>
     <link href="../css/anekabutton.css" rel="stylesheet" />    
    
    <script>
        function ItemSelectedemp_cd(sender, e) {

            $get('<%=hdemp_cd.ClientID%>').value = e.get_value();
            $get('<%=btrefresh.ClientID%>').click();
        }
    </script>
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentbody" Runat="Server">
    <h3 class="divheader">
        User Registration   
    </h3>
    <img src="../div2.png" class="divid" />
     <div class="container">
    <div class="row">
        <div class="col-md-5">        
            <label class="control-label">Employee</label>
           
                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                <asp:TextBox ID="txemp" runat="server" width="100%" CssClass="form-control"></asp:TextBox>
                <div id="divwidth" style="font-size: small; font-family: Calibri;position:absolute;"></div>
                <asp:HiddenField ID="hdemp_cd" runat="server" />
                <ajaxtoolkit:autocompleteextender id="txemp_AutoCompleteExtender" runat="server" servicemethod="GetCompletionList" targetcontrolid="txemp" usecontextkey="True"
                    completionlistelementid="divwidth" minimumprefixlength="1" enablecaching="false" firstrowselected="false" completioninterval="10" completionsetcount="1" onclientitemselected="ItemSelectedemp_cd">
                        </ajaxtoolkit:autocompleteextender>
                            </ContentTemplate></asp:UpdatePanel>
            
            </div>
            
        </div>
         <div class="row">
             <div class="col-md-3">
        <label>Short Name</label>
            <span class="form-control">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                <asp:TextBox ID="txshortname" runat="server" width="100%"></asp:TextBox>
                            </ContentTemplate></asp:UpdatePanel>
            </span>
        </div>
       <div class="col-md-3">
            <label>User ID</label>
            <span class="form-control">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                <asp:TextBox ID="txuid" runat="server" width="100%"></asp:TextBox>
                            </ContentTemplate></asp:UpdatePanel>
            </span>
                </div>
        <div class="col-md-3">
            <label>mobile No.</label>
            <span class="form-control">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                <asp:TextBox ID="txmobile" runat="server" width="100%"></asp:TextBox>
                            </ContentTemplate></asp:UpdatePanel>
            </span>
        </div>
         
             </div>
         <div class="row">
             <div class="col-md-3">
            <label>Email</label>
            <span class="form-control">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                <asp:TextBox ID="txemail" runat="server" width="100%"></asp:TextBox>
                            </ContentTemplate></asp:UpdatePanel>
            </span>
             </div>
        <div class="col-md-3">
            <label>branch</label>
            <span class="form-control">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                <asp:DropDownList ID="cbbranch" runat="server" width="100%">                   
                </asp:DropDownList> </ContentTemplate></asp:UpdatePanel>
            </span>
        </div>
                     <div class="col-md-3">
            <label>Password</label>
            <span class="form-control">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                <asp:TextBox ID="txpassword" runat="server" width="100%"></asp:TextBox>
                            </ContentTemplate></asp:UpdatePanel>
            </span>
            </div>
             </div>
         <div class="row">

        <div class="col-md-3">
            <label>Confirm Password</label>
            <span class="form-control">
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                <asp:TextBox ID="txconfirmpass" runat="server" width="100%"></asp:TextBox>
                            </ContentTemplate></asp:UpdatePanel>
            </span>
       </div>
        <div class="col-md-3">
            <label>Department</label>
            <span class="form-control">
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                <asp:DropDownList ID="cbdepartment" runat="server" width="100%">                    
                </asp:DropDownList>
                            </ContentTemplate></asp:UpdatePanel>
            </span>
            </div>
             </div>         
    
    <img src="../div2.png" class="divid" />
    <div class="navi">
        <asp:Button ID="btnew" runat="server" Text="New" CssClass="btn btn-primary" OnClick="btnew_Click"/>
        &nbsp;
        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btsave_Click" />
        &nbsp;
        <asp:Button ID="btupd" runat="server" Text="Update" CssClass="btn btn-primary" Visible='False' OnClick="btupd_Click"/>        
        <asp:Button ID="btrefresh" runat="server" OnClick="btrefresh_Click" Text="refresh" CssClass="divhid"/>        
    </div>
    </div>
</asp:Content>

