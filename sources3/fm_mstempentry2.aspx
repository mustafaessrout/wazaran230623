<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstempentry2.aspx.cs" Inherits="fm_mstempentry2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="admin/css/bootstrap.min.css" rel="stylesheet" />
    <script src="admin/js/bootstrap.min.js"></script>
    <style>
        .ajax__tab_tab {
            -webkit-box-sizing: content-box !important;
            -moz-box-sizing: content-box !important;
            box-sizing: content-box !important;
        }
    </style>
    <script>
        function refreshdata(val)
        {
            $get('<%=hdemp.ClientID%>').value = val;
            $get('<%=txempcd.ClientID%>').value = val;
            $get('<%=btrefresh.ClientID%>').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <div class="form-horizontal">
            <h3>Employee Data</h3>
            <div class="h-divider"></div>
            <div class="form-group">
                <div class="col-md-12">
                     <asp:TabContainer ID="tabemp" runat="server" ActiveTabIndex="0" Width="100%" VerticalStripWidth="" BorderStyle="None" Font-Size="Larger">
        <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1">
            <HeaderTemplate>Data</HeaderTemplate>
            <ContentTemplate>
               <div class="container">
                    <div class="form-horizontal">
                    <label class="control-label">Emp Code</label>
                    <div class="col-md-4 col-lg-offset-0">
                    <asp:TextBox ID="txempcode" runat="server" CssClass="form-control-static" Width="100%"></asp:TextBox>
                    <label class="control-label col-md-2 col-lg-offset-0">Emp Code</label>
                    <div class="col-md-4 col-lg-offset-0">
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control-static" Width="100%"></asp:TextBox>
                    </div>
                    </div>
               
                   <%-- <div class="col-md-1">
                        <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-default" />
                    </div>
                    
                        <label class="control-label col-md-2">Emp Name</label>
                      <div class="col-md-4">
                        <asp:TextBox ID="txempname1" runat="server" CssClass="form-control-static" Width="100%"></asp:TextBox>
                    </div>--%>
                </div>
                <table><tr><td>Employee ID</td><td>:</td><td><asp:TextBox ID="txempcd" runat="server" CssClass="makeitreadonly"></asp:TextBox><asp:Button ID="btsearch" runat="server" CssClass="button2 search" OnClick="btsearch_Click" /><asp:HiddenField ID="hdemp" runat="server" /></td><td>Employee Name</td><td>:</td><td><asp:TextBox ID="txempname" runat="server" Width="20em"></asp:TextBox></td><td rowspan="4"><asp:Image ID="img" runat="server" Height="100px" ImageUrl="~/noimage.jpg" /></td></tr><tr><td>Birth Place</td><td>:</td><td><asp:TextBox ID="txbirthplace" runat="server" Width="14em"></asp:TextBox></td><td>Birth Date</td><td>:</td><td style="margin-left: 40px"><asp:TextBox ID="dtbirth" runat="server" TextMode="Date"></asp:TextBox>
                <asp:CalendarExtender ID="dtbirth_CalendarExtender" runat="server" TargetControlID="dtbirth" Enabled="True" Format="d/M/yyyy">
                </asp:CalendarExtender>
                </td></tr><tr><td>Blood</td><td>:</td><td><asp:DropDownList ID="cbblood" runat="server" Width="14em"></asp:DropDownList></td><td>Nationality</td><td>:</td><td style="margin-left: 40px"><asp:DropDownList ID="cbonation" runat="server" Width="10em"></asp:DropDownList></td></tr><tr><td>Job Title</td><td>:</td><td><asp:DropDownList ID="cbjobtitle" runat="server" Width="14em"></asp:DropDownList></td><td>Salespoint</td><td>:</td><td style="margin-left: 40px"><asp:DropDownList ID="cbsalespoint" runat="server" Width="10em"></asp:DropDownList></td></tr><tr><td>Profession Iqoma</td><td>:</td><td><asp:DropDownList ID="cbprofiqoma" runat="server" Width="14em"></asp:DropDownList></td><td>Joint Date</td><td>:</td><td style="margin-left: 40px"><asp:TextBox ID="dtjoin" runat="server" TextMode="Date"></asp:TextBox>
                <asp:CalendarExtender ID="dtjoin_CalendarExtender" runat="server" TargetControlID="dtjoin" Enabled="True" Format="d/M/yyyy">
                </asp:CalendarExtender>
                </td><td style="margin-left: 40px">
                    <asp:FileUpload ID="upl" runat="server" />
                </td></tr><tr><td>Department</td><td>:</td><td><asp:DropDownList ID="cbdept" runat="server" Width="14em"></asp:DropDownList></td><td>Level</td><td>:</td><td style="margin-left: 40px"><asp:DropDownList ID="cblevel" runat="server" Width="10em"></asp:DropDownList></td><td style="margin-left: 40px">&nbsp;</td></tr><tr><td>Phone No.</td><td>:</td><td><asp:TextBox ID="txphoneno" runat="server" Width="14em"></asp:TextBox></td><td>Emergency Contact</td><td>:</td><td style="margin-left: 40px"><asp:TextBox ID="txemergency" runat="server"></asp:TextBox></td><td style="margin-left: 40px">&nbsp;</td></tr><tr><td>Married Status</td><td>:</td><td>
                <asp:DropDownList ID="cbmarried" runat="server" Width="14em">
                </asp:DropDownList>
                </td><td>Emergency Phone</td><td>:</td><td style="margin-left: 40px">
                <asp:TextBox ID="txemerphone" runat="server" Width="10em"></asp:TextBox>
                </td><td style="margin-left: 40px">&nbsp;</td></tr></table><img src="div2.png" class="divid" /><div class="navi"><asp:Button ID="btrefresh" runat="server" OnClick="btrefresh_Click" Text="Button" style="display:none" /><asp:Button ID="btnew" runat="server" CssClass="button2 add" Text="New Employee" OnClick="btnew_Click" />
                <asp:Button ID="btedit" runat="server" CssClass="button2 edit" OnClick="btedit_Click" Text="Edit" />
                <asp:Button ID="btsave" runat="server" Text="Save" CssClass="button2 save" OnClick="btsave_Click" /></div></ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
            <HeaderTemplate>Education</HeaderTemplate>
            <ContentTemplate>
                <div class="divheader">
                    <asp:Label ID="lbempcodeedu" runat="server" Text="-"></asp:Label><asp:Label ID="lbempnameedu" runat="server" Text="-" Font-Bold="True"></asp:Label>
                </div>
                <table>
                    <tr style="background-color:silver;font-weight:bold">
                        <td>
                            No.</td>
                        <td>School Name </td>
                        <td>Major</td>
                        <td>Graduate Date</td>
                        <td>Degree</td>
                        <td>GPA</td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txsequence" runat="server" Width="2em"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txschool" runat="server" Width="20em"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txmajor" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="dtgraduate" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="dtgraduate_CalendarExtender" runat="server" Enabled="True" TargetControlID="dtgraduate">
                            </asp:CalendarExtender>
                        </td>
                        <td>
                            <asp:TextBox ID="txdegree" runat="server"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txgpa" runat="server"></asp:TextBox></td>
                        <td>
                            <asp:Button ID="btaddedu" runat="server" Text="Add" CssClass="button2 add" OnClick="btaddedu_Click" /></td>
                    </tr>
                    
                </table>
                <div class="divgrid">
                    <asp:GridView ID="grdedu" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" OnRowDeleting="grdedu_RowDeleting">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <asp:Label ID="lbseq" runat="server" Text='<%# Eval("sequenceno") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="School Name">
                                <ItemTemplate><%# Eval("school_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Major">
                                <ItemTemplate><%# Eval("major") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Graduate Date">
                                <ItemTemplate><%# Eval("graduate_dt","{0:d/M/yyyy}") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Degree">
                                <ItemTemplate><%# Eval("degree") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GPA">
                                <ItemTemplate><%# Eval("gpa") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3">
            <HeaderTemplate>Language</HeaderTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="TabPanel4">
            <HeaderTemplate>Document</HeaderTemplate>
            <ContentTemplate>
                <div class="divheader">
                    <asp:Label ID="lbempcodedoc" runat="server" Text="-"></asp:Label>-
                    <asp:Label ID="lbempnamedoc" runat="server" Text="-"></asp:Label>
                </div>
                <table>
                    <tr style="background-color:silver"><td>Doc No.</td><td>Doc Name</td><td>Date</td><td>Expired Date</td><td>Image File</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="margin-left: 80px">
                            <asp:TextBox ID="txdocno" runat="server" Width="2em"></asp:TextBox>
                        </td>
                        <td style="margin-left: 40px">
                            <asp:DropDownList ID="cbdoc" runat="server" Width="10em">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox3" runat="server" Width="6em"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox4" runat="server" Width="6em"></asp:TextBox>
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpload1" runat="server" Width="15em" />
                        </td>
                        <td>
                            <asp:Button ID="btadddoc" runat="server" Text="Add" CssClass="button2 add" OnClick="btadddoc_Click"/>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel5" runat="server" HeaderText="TabPanel5">
            <HeaderTemplate>Dependant</HeaderTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
                </div>
            </div>
        </div>
    </div>
    
   
</asp:Content>

