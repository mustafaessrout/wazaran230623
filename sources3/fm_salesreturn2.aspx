<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_salesreturn2.aspx.cs" Inherits="fm_salesreturn2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h4 class="divheader">Sales Return from Customer</h4>
    <div class="h-divider"></div>

    <div class="container-fluid margin-bottom">
        <div class="form-horizontal" >
           
                <div class="form-group">
                <label class="control-label col-md-1">Source</label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cbsource" CssClass="form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
                    </div>
                     <label class="control-label col-md-1">Date</label>
                    <div class="col-md-2">
                        <asp:Label ID="lbreturdate" runat="server" CssClass="form-control ro"></asp:Label>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-md-1">Sys No</label>
                    <div class="col-md-2">
                        <div class="input-group">
                        <asp:Label ID="lbsysno" runat="server" CssClass="form-control"></asp:Label>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="btsearch" CssClass="btn btn-primary" runat="server"><i class="fa fa-search"></i></asp:LinkButton>
                        </div>
                        </div>
                    </div>
                    <label class="control-label col-md-1">Tablet No</label>
                    <div class="col-md-2">
                        <div class="input-group">
                        <asp:Label ID="lbtabno" runat="server" CssClass="form-control"></asp:Label>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="LinkButton1" CssClass="btn btn-primary" runat="server"><i class="fa fa-search"></i></asp:LinkButton>
                        </div>
                        </div>
                    </div>
                    <label class="control-label col-md-1">Type</label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cbtype" CssClass="form-control" runat="server">
                            <asp:ListItem Value="ALL">&lt;&lt;&lt;&lt; Select &gt;&gt;&gt;&gt;</asp:ListItem>
                            <asp:ListItem Value="D">Return To Depo</asp:ListItem>
                            <asp:ListItem Value="V">Return To Van</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <label class="control-label col-md-1">Manual No</label>
                    <div class="col-md-2">
                        <asp:TextBox ID="txmanualno" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    
                   </div>
                <div class="form-group">
                   
                    <label class="control-label col-md-1">Cust Man No</label>
                    <div class="col-md-2">
                        <asp:TextBox ID="txcustmanualno" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <label class="control-label col-md-1">Remark</label>
                    <div class="col-md-8">
                        <asp:TextBox ID="txremark" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
          
        </div>
    </div>
</asp:Content>

