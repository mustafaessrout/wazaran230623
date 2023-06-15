<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstsalespointentry.aspx.cs" Inherits="fm_mstsalespointentry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Salespoint Entry</div>
    <div class="h-divider"></div>

    <div class="containers">
        <div class="row">
            <div class="clearfix">
                <div class="col-sm-6 clearfix margin-bottom">
                    <label class="col-sm-4 control-label titik-dua">Salespoint Code</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txsalespointcode" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                 <div class="col-sm-6 clearfix margin-bottom">
                    <label class="col-sm-4 control-label titik-dua">Salespoint Name</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txsalespointname" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                 <div class="col-sm-6 clearfix margin-bottom">
                    <label class="col-sm-4 control-label titik-dua">Short Name</label>
                    <div class="col-sm-8">
                         <asp:TextBox ID="txsalespointcode0" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                 <div class="col-sm-6 clearfix margin-bottom">
                    <label class="col-sm-4 control-label titik-dua">Type</label>
                    <div class="col-sm-8">
                        <div class="drop-down">
                            <asp:DropDownList ID="cbsptype" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="clearfix">
                    <div class="col-sm-6 margin-bottom">
                        <label class="col-sm-4 control-label titik-dua">Address</label>
                        <div class="col-sm-8">
                            <div class="drop-down">
                                <asp:DropDownList ID="cblocation" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clearfix">
                    <div class="col-sm-6 margin-bottom">
                        <label class="col-sm-4 control-label titik-dua">Area</label>
                        <div class="col-sm-8">
                            <div class="drop-down">
                                 <asp:DropDownList ID="cbarea" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6 clearfix margin-bottom">
                    <label class="col-sm-4 control-label titik-dua">Phone No.</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txphoneno" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-6 clearfix margin-bottom">
                    <label class="col-sm-4 control-label titik-dua">Mobile No.</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txmobileno" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-6 clearfix margin-bottom">
                    <label class="col-sm-4 control-label titik-dua">Email</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txemail" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-6 clearfix margin-bottom">
                    <label class="col-sm-4 control-label titik-dua"> Fax. No.</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txfaxno" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            
            </div>
        </div>
    </div>
    <div class="navi margin-bottom">
        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn btn-warning save" />
    </div>
</asp:Content>

