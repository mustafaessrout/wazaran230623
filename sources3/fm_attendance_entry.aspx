<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_attendance_entry.aspx.cs" Inherits="fm_attendance_entry" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
        .divmsg{
       /*position:static;*/
       top:30%;
       right:50%;
       left:50%;
       width:200px;
       height:200px;
       position:fixed;
       /*background-color:greenyellow;*/
       overflow-y:auto;
                }
        .divhid {
            display:none;
        }

        .divnormal {
            display:normal;
        }
        @media (min-width: 768px){
            .form-custom{
                padding-left:5% !important;
                width:91.65% !important;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h4 class="jajarangenjang">Daily Attendance</h4>
    <div class="h-divider"></div>
    <asp:HiddenField runat="server" ID="hdno" />

    <div class="row">
        <div class="col-sm-6 clearfix margin-bottom">
            <label class="col-sm-3 control-label">Salespoint</label>
            <div class="col-sm-9">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" class="input-group">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbSalesPointCD" runat="server" CssClass="form-control  " AutoPostBack="true" OnSelectedIndexChanged="cbSalesPointCD_SelectedIndexChanged">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="col-sm-6 clearfix margin-bottom">
            <label class="col-sm-3 control-label">Date</label>
            <div class="col-sm-9 drop-down">
                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="dtattendance" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:CalendarExtender CssClass="date" ID="dtattendance_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtattendance">
                        </asp:CalendarExtender>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbSalesPointCD" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                     
            </div>
        </div>
        <div class="col-sm-12 clearfix margin-bottom">
            
            <div class="col-sm-10 form-custom">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="text-center margin-top">
                            <asp:Button ID="btgenerate" runat="server" CssClass="btn-success btn btn-add" OnClick="btgenerate_Click" Text="Show Employee" Visible="true" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div id="listEmployee" runat="server">
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">    
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <ContentTemplate>
                    <asp:GridView ID="grdemployee" CssClass="mGrid" runat="server" AutoGenerateColumns="False" ShowFooter="False" CellPadding="0" OnRowDataBound="grdemployee_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Branch">
                                <ItemTemplate>
                                    <asp:Label ID="lbsalespointcd" runat="server" Text='<%#Eval("salespointcd") %>'></asp:Label> | <asp:Label ID="lbsalespointnm" runat="server" Text='<%#Eval("salespoint_nm") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Employee">
                                <ItemTemplate>
                                    <asp:Label ID="lbemp_cd" runat="server" Text='<%#Eval("emp_cd") %>'></asp:Label> | <asp:Label ID="lbemp_nm" runat="server" Text='<%#Eval("emp_nm") %>'></asp:Label> | <asp:Label ID="lbarabic_nm" runat="server" Text='<%#Eval("emp_nm_arabic") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Position"><ItemTemplate><%# Eval("job_title_nm") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Department"><ItemTemplate><%# Eval("dept_nm") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txattdate" runat="server" Text='<%# Eval("attendance_dt","{0:d/M/yyyy}") %>'  CssClass="form-control input-sm"></asp:TextBox>
                                    <asp:CalendarExtender ID="txattdate_CalendarExtender" PopupPosition="TopLeft" CssClass="date" runat="server" DaysModeTitleFormat="d/M/yyyy" Format="d/M/yyyy" TargetControlID="txattdate" TodaysDateFormat="d/M/yyyy">
                                    </asp:CalendarExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Check In">
                                <ItemTemplate>
                                    <asp:TextBox ID="txcheckin" Text='<%#Eval("check_in","{0:hh:mm}") %>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Check Out">
                                <ItemTemplate>
                                    <asp:TextBox ID="txcheckout" Text='<%#Eval("check_out","{0:hh:mm}") %>' runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btgenerate" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <div class="h-divider"></div>
    <div class="navi margin-bottom margin-top padding-bottom">
        <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
        <asp:Button ID="btnew" runat="server" Text="NEW" CssClass="btn-success btn btn-add" OnClick="btnew_Click" />        
        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn-warning btn btn-save" OnClick="btsave_Click"/>
        <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btgenerate" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

<%--   
    <div class="divmsg loading-cont" id="dvshow">
        <div>
            <i class="fa fa-spinner spiner fa-spin fa-3x fa-fw" aria-hidden="true"></i>
        </div>
    </div>--%>

</asp:Content>

