<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_customerentry.aspx.cs" Inherits="fm_customerentry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }

        function LocationSelected(sender, e) {
            $get('<%=hdcity.ClientID%>').value = e.get_value();
            $get('<%=btcity.ClientID%>').click();

        }

        function SelectCustomer(custcode) {
            $get('<%=hdcust.ClientID%>').value = custcode;
            $get('<%=btrefresh.ClientID%>').click();
        }

        function DistrictSelected(sender, e) {
            $get('<%=hddistrict.ClientID%>').value = e.get_value();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdcust" runat="server" />
    <asp:HiddenField ID="hdcity" runat="server" />
    <asp:HiddenField ID="hddistrict" runat="server" />
    <div class="alert alert-info text-bold">Customer Entry</div>
    <div class="container">
        <%--<h5 class="jajarangenjang">Master Customer</h5>
        <div class="h-divider"></div>--%>
        <div class="form-group">
            <div class="row margin-bottom margin-top">
                <label class="control-label-sm col-sm-1 input-sm">Customer Code</label>
                <div class="col-sm-2">
                    <div class="input-group">
                        <asp:TextBox ID="txcustocode" CssClass="form-control input-sm ro" runat="server"></asp:TextBox>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="btsearch" OnClientClick="javascript:popupwindow('lookupcustomer.aspx','Lookup Customer',600,600);" CssClass="btn btn-primary btn-sm" runat="server"><span class="fa fa-search"></span></asp:LinkButton>
                        </div>
                    </div>
                </div>
                <label class="control-label-sm col-sm-1 input-sm">Customer Name</label>
                <div class="col-sm-2 require">
                    <div class="input-group">
                        <div class="input-group-sm">
                            <asp:TextBox ID="txcustname" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                        </div>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="btupdatecustomername" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" OnClick="btupdatecustomername_Click" runat="server">Update</asp:LinkButton>
                        </div>
                    </div>

                </div>
                <label class="control-label-sm col-sm-1 input-sm">Group</label>
                <div class="col-sm-2">
                    <div class="input-group">
                        <div class="drop-down input-group-sm">
                            <asp:DropDownList ID="cbgroup" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                        </div>
                        <div class="input-group-btn">
                            <asp:LinkButton OnClientClick="ShowProgress();" ID="btupdatecusgrcd" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btupdatecusgrcd_Click">Update</asp:LinkButton>
                        </div>
                    </div>

                </div>
                <label class="control-label-sm col-sm-1 input-sm">Channel</label>
                <div class="input-group">
                    <div class="drop-down input-group-sm">
                        <asp:DropDownList ID="cbchannel" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                    </div>
                    <div class="input-group-btn">
                        <asp:LinkButton OnClientClick="ShowProgress();" ID="btupdatechannel" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btupdatechannel_Click">Update</asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="row margin-bottom">
                <label class="control-label-sm col-sm-1 input-sm">Salespoint</label>
                <div class="col-sm-2 drop-down">
                    <asp:DropDownList ID="cbsp" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                </div>
                <label class="control-label-sm col-sm-1 input-sm">Category</label>
                <div class="col-sm-2">
                    <div class="input-group">
                        <div class="input-group-sm drop-down">
                            <asp:DropDownList ID="cbcustcate" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                        </div>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="btupdatecastegory" onchange="ShowProgress();" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btupdatecastegory_Click">Update</asp:LinkButton>
                        </div>
                    </div>

                </div>
                <label class="control-label-sm col-sm-1 input-sm">Term Of Payment</label>
                <div class="col-sm-2">
                    <div class="input-group">
                        <div class="input-group-sm drop-down">
                            <asp:DropDownList ID="cbpaymentterm" CssClass="form-control input-sm input-group-sm" runat="server"></asp:DropDownList>
                        </div>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="btupdatepaymentterm" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" OnClick="btupdatepaymentterm_Click" runat="server">Update</asp:LinkButton>
                        </div>
                    </div>

                </div>
                <label class="control-label-sm col-sm-1 input-sm">Salesman</label>
                <div class="col-sm-2 drop-down">
                    <asp:DropDownList ID="cbsalesman" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                </div>
            </div>

            <div class="row margin-bottom">
                <label class="control-label-sm col-sm-1 input-sm">Short Name</label>
                <div class="col-sm-2">
                    <asp:TextBox ID="txshortname" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                </div>
                <label class="control-label-sm col-sm-1 input-sm">Credit Limit</label>
                <div class="col-sm-2 require">
                    <div class="input-group">
                        <div class="input-group-sm">
                            <asp:TextBox ID="txcreditlimit" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                        </div>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="btupdatecreditlimit" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" OnClick="btupdatecreditlimit_Click" runat="server">Update</asp:LinkButton>
                        </div>
                    </div>

                </div>
                <label class="control-label-sm col-sm-1 input-sm">Cust Status</label>
                <div class="col-sm-2 drop-down">
                    <asp:DropDownList ID="cbstatus" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                </div>
                <label class="control-label-sm col-sm-1 input-sm">Virtual Account</label>
                <div class="col-sm-2">
                    <asp:TextBox ID="txvirtualaccount" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row margin-bottom">
                <label class="control-label-sm col-sm-1 input-sm">Identifiant Commun de l’Entreprise (ICE)</label>
                <div class="col-sm-2 require">
                    <div class="input-group">
                        <asp:TextBox ID="txice" CssClass="form-control input-sm input-group-sm" runat="server"></asp:TextBox>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="btupdateice" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btupdateice_Click">Update</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <label class="control-label-sm col-sm-1 input-sm">Registre du commerce (RC)</label>
                <div class="col-sm-2 ">
                    <div class="input-group">
                        <asp:TextBox ID="txrc" CssClass="form-control input-sm input-group-sm" runat="server"></asp:TextBox>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="btupdaterc" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btupdaterc_Click">Update</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <label class="control-label-sm col-sm-1 input-sm">Identifiant Fiscal (IF)</label>
                <div class="col-sm-2 ">
                    <div class="input-group">
                        <asp:TextBox ID="txif" CssClass="form-control input-sm input-group-sm" runat="server"></asp:TextBox>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="btupdateif" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btupdateif_Click">Update</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <label class="control-label-sm col-sm-1 input-sm">Tax Professional (TP)</label>
                <div class="col-sm-2 require">
                    <div class="input-group">
                        <asp:TextBox ID="txtp" CssClass="form-control input-sm input-group-sm" runat="server"></asp:TextBox>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="btupdatetp" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btupdatetp_Click">Update</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="h-divider"></div>
            <h5 class="jajarangenjang">Contact Information</h5>
            <div class="h-divider"></div>
            <div class="row margin-bottom">
                <label class="control-label-sm col-sm-1 input-sm">Address Type</label>
                <div class="col-sm-2 drop-down">
                    <asp:DropDownList ID="cbaddresstype" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                </div>
                <label class="control-label-sm col-sm-1 input-sm require">Address</label>
                <div class="col-sm-2">
                    <asp:TextBox ID="txaddress" CssClass="form-control input-sm" runat="server" TextMode="MultiLine"></asp:TextBox>
                </div>
                <label class="control-label-sm col-sm-1 input-sm">City</label>
                <div class="col-sm-2 require">
                    <asp:TextBox ID="txcity" CssClass="form-control input-sm" runat="server" ToolTip="Please call Admin Wazaran if your city not found"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txcity_AutoCompleteExtender" CompletionListCssClass="input-sm" ServiceMethod="GetLocationList" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" UseContextKey="true" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="LocationSelected" runat="server" TargetControlID="txcity">
                    </asp:AutoCompleteExtender>
                </div>
                <label class="control-label-sm col-sm-1 input-sm">Sector</label>
                <div class="col-sm-1 require">
                    <asp:TextBox ID="txdistrict" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txdistrict_AutoCompleteExtender" CompletionListCssClass="input-sm" runat="server" TargetControlID="txdistrict" MinimumPrefixLength="1" ShowOnlyCurrentWordInCompletionListItem="true" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" UseContextKey="true" FirstRowSelected="false" OnClientItemSelected="DistrictSelected" ServiceMethod="GetDistrictList">
                    </asp:AutoCompleteExtender>
                </div>
                <%--<label class="control-label-sm col-sm-1 input-sm">Post Code</label>
                <div class="col-sm-1 require">
                    <asp:TextBox ID="txpostcode" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                </div>--%>
                <div class="col-sm-1">
                    <asp:LinkButton ID="btadd" OnClientClick="ShowProgress();" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btadd_Click">Update</asp:LinkButton>
                </div>
            </div>
            <div class="row margin-bottom">
                <div class="col-sm-12">
                    <asp:GridView ID="grdaddress" CssClass="table table-bordered table-condensed input-sm" runat="server" AutoGenerateColumns="False" OnRowDeleting="grdaddress_RowDeleting" AllowPaging="True">
                        <Columns>
                            <asp:TemplateField HeaderText="Address Typ">
                                <ItemTemplate>
                                    <%#Eval("addr_typ_nm") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Address">
                                <ItemTemplate>
                                    <asp:Label ID="lbaddr" runat="server" Text='<%#Eval("address1")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="City">
                                <ItemTemplate>
                                    <asp:Label ID="lbloc" runat="server" Text='<%#Eval("city") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Post Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbpostcode" runat="server" Text='<%#Eval("zipcode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                        <RowStyle BackColor="#66CCFF" />
                    </asp:GridView>
                </div>
            </div>
            <div class="h-divider"></div>
            <div class="row margin-bottom">
                <label class="control-label-sm col-sm-1 input-sm">Phone</label>
                <div class="col-sm-2 require">
                    <asp:TextBox ID="txphone" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                </div>
                <label class="control-label-sm col-sm-1 input-sm">Map</label>
                <div class="col-sm-1">
                    <asp:LinkButton ID="btmap" OnClientClick="ShowProgress();" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btmap_Click">Map</asp:LinkButton>
                </div>
            </div>
            <div class="row margin-bottom">
                <label class="control-label-sm col-sm-1 input-sm">Fax</label>
                <div class="col-sm-2 require">
                    <asp:TextBox ID="txfax" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                </div>
                <label class="control-label-sm col-sm-1 input-sm">Contact 1</label>
                <div class="col-sm-2 require">
                    <asp:TextBox ID="txcontact1" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                </div>
                <label class="control-label-sm col-sm-1 input-sm">Contact 2</label>
                <div class="col-sm-2 require">
                    <asp:TextBox ID="txcontact2" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                </div>
                <label class="control-label-sm col-sm-1 input-sm">Email</label>
                <div class="col-sm-2 require">
                    <asp:TextBox ID="txemail" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                </div>
            </div>
            <h5 class="jajarangenjang">Additional Information</h5>
            <div class="h-divider"></div>
            <%--<div class="row margin-bottom">
                <label class="control-label-sm col-sm-1 input-sm">Hit By Discount DC</label>
                <div class="col-sm-1 require input-sm">
                    <asp:CheckBox ID="chdiscdc" runat="server" />
                </div>
                <label class="control-label-sm col-sm-1 input-sm">Facture Exchange</label>
                <div class="col-sm-2 require">
                    <asp:CheckBox ID="chfacture" runat="server" />
                </div>
                <label class="control-label-sm col-sm-1 input-sm">NPWP</label>
                <div class="col-sm-2 require">
                    <asp:TextBox ID="txnpwp" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                </div>
                <label class="control-label-sm col-sm-1 input-sm">Disc DC</label>
                <div class="col-sm-2 require drop-down">
                    <asp:DropDownList ID="cbdiscdc" CssClass="form-control input-sm" runat="server">
                        <asp:ListItem Value="0">No Discount</asp:ListItem>
                        <asp:ListItem Value="0.05">Discount 5%</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>--%>
            <div class="row margin-bottom">
                <label class="control-label-sm col-sm-1 input-sm">Latitude</label>
                <div class="col-sm-1">
                    <asp:TextBox ID="txlatitude" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                </div>
                <label class="control-label-sm col-sm-1 input-sm">Longitude</label>
                <div class="col-sm-1">
                    <asp:TextBox ID="txlongitude" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                </div>
            </div>
            <%--<h5 class="jajarangenjang">File Supported</h5>
            <div class="h-divider"></div>
            <div class="row margin-bottom">
                <label class="col-sm-1 control-label-sm input-sm">ID/KTP/SIM Card</label>
                <div class="col-sm-2">
                    <asp:FileUpload ID="fuplktp" CssClass="form-control input-sm" runat="server" />
                </div>
                <label class="col-sm-1 control-label input-sm">NPWP</label>
                <div class="col-sm-2">
                    <asp:FileUpload ID="fuplnpwp" CssClass="form-control input-sm" runat="server" />
                </div>

            </div>--%>
            <div class="row margin-bottom">
                <div class="col-sm-12 alert-info" style="text-align: center">
                    <asp:LinkButton ID="btnew" runat="server" OnClientClick="ShowProgress();" CssClass="btn btn-primary btn-sm" OnClick="btnew_Click">New</asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="btsave" OnClientClick="ShowProgress();" runat="server" CssClass="btn btn-info btn-sm" OnClick="btsave_Click">Save</asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="btedit" OnClientClick="ShowProgress();" runat="server" CssClass="btn btn-info btn-sm" OnClick="btedit_Click">Save Edit</asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="btprint" runat="server" CssClass="btn btn-success btn-sm">Print</asp:LinkButton>&nbsp;
                    <asp:Button ID="btrefresh" runat="server" OnClick="btrefresh_Click" OnClientClick="ShowProgress();" Text="Button" Style="display: none" />
                    <asp:LinkButton ID="btlistall" runat="server" OnClientClick="ShowProgress();" CssClass="btn btn-success btn-sm" OnClick="btlistall_Click">List Of Customer</asp:LinkButton>
                    <asp:Button ID="btcity" runat="server" Style="display: none" Text="Button" OnClick="btcity_Click" />
                </div>
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

