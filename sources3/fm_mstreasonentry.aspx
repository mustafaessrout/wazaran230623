<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstreasonentry.aspx.cs" Inherits="fm_mstreasonentry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Entry Reason</div>
    <div class="h-divider"></div>
    <div class="container-fluid">
        <div class="row">
            <div class="clearfix">
                <div class="col-sm-12 no-padding clearfix">
                    <div class="col-sm-6 clearfix margin-bottom">
                        <label class="titik-dua control-label col-sm-4">Reason Type</label>
                        <div class="col-sm-8">
                            <div class="drop-down">
                                <asp:DropDownList ID="cbreasontyp" runat="server" CssClass="form-control "></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 clearfix margin-bottom">
                        <label class="titik-dua control-label col-sm-4">Reason Code</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txreasoncode" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-12 clearfix margin-bottom">
                    <label class="titik-dua control-label col-sm-2">Description</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txreasonname" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-12 clearfix margin-bottom">
                    <label class="titik-dua control-label col-sm-2">Reason Arabic</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txreasonarabic" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="navi margin-bottom">
        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn btn-warning btn-save" OnClick="btsave_Click" />
    </div>
</asp:Content>

