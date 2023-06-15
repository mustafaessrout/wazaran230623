<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_uploadgr.aspx.cs" Inherits="fm_uploadgr" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <h4 class="jajarangenjang">Upload File for Good Received</h4>
        <div class="h-divider"></div>
    <div class="form-group">
        <div class="row margin-bottom">
            <label class="control-label col-sm-1 input-sm">Upload date</label>
            <div class="col-sm-2">
                <asp:TextBox ID="dtupload" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="dtupload_CalendarExtender" Format="d/M/yyyy" runat="server" TargetControlID="dtupload">
                </asp:CalendarExtender>
            </div>
            <label class="control-label col-sm-1 input-sm">Status</label>
            <div class="col-sm-2 drop-down">
                <asp:DropDownList ID="cbstatus" CssClass="form-control input-sm" runat="server">
                    <asp:ListItem Value="N">Not yet uploaded</asp:ListItem>
                    <asp:ListItem Value="U">Already uploaded</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-sm-1">
                <asp:LinkButton ID="btsearch" runat="server" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" OnClick="btsearch_Click"><span class="fa fa-search"></span></asp:LinkButton>
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="col-sm-12">
                <asp:GridView ID="grd" CssClass="table table-condensed table-bordered input-sm" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="grd_PageIndexChanging" OnRowDataBound="grd_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Receipt No">
                            <ItemTemplate>
                                <asp:Label ID="lbreceiptno" runat="server" CssClass="input-sm" Text='<%#Eval("receipt_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate><i class="input-sm"><%#Eval("receipt_dt","{0:d/M/yyyy}") %></i></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DO No">
                            <ItemTemplate><%#Eval("do_no") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="File 1">
                            <ItemTemplate>
                                <table class="table-condensed">
                                    <tr>
                                        <td>
                                             <asp:FileUpload ID="upl1" style="background-color:lightgreen" CssClass="form-control input-sm" runat="server" />
                                        </td>
                                        <td>
                                            <a href="javascript:popupwindow('lookupimagegr.aspx?src=<%#Eval("receipt_no")%>&f=n');">Check</a>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GDN File">
                            <ItemTemplate>
                                <table class="table-condensed">
                                    <tr>
                                        <td>
                                              <asp:FileUpload ID="uplgdn" style="background-color:lightyellow" CssClass="form-control input-sm" runat="server" />
                                        </td>
                                        <td>
                                            <a href="javascript:popupwindow('lookupimagegr.aspx?src=<%#Eval("receipt_no")%>&f=g');">Check</a>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Detail">
                            <ItemTemplate>
                                <a href="javascript:popupwindow('lookupreceiptgr.aspx?gr=<%#Eval("receipt_no")%>');">Detail</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Upload">
                            <ItemTemplate>
                                <asp:LinkButton ID="btupload" OnClick="btupload_Click" CssClass="btn btn-primary btn-sm" runat="server">Save</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    </div>

    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

