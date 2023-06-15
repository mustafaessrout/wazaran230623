﻿<%@ Page Title="" Language="C#" MasterPageFile="~/master/homaster.master" AutoEventWireup="true" CodeFile="fm_mst_employee.aspx.cs" Inherits="fm_mst_employee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <link href="../css/anekabutton.css" rel="stylesheet" />
    <%-- <link href="Content/beatifullcontrol.css" rel="stylesheet" />--%>
    <script>
        function SelectData(sVal) {
            $get('<%=hdAssecode.ClientID%>').value = sVal;
            $get('<%=txtEmpCode.ClientID%>').value = sVal;
            $get('<%=btlookup.ClientID%>').click();
        }
        function uploadFiles() {
            window.open("fm_UploadFiles.aspx?fileType=Emp", "lookup", "height=700,width=900,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }
        function UploadFIlesData() {
            document.getElementById('<%=btnUpload.ClientID%>').click();
            <%--$('<%=lkl.ClientID%>').prop("href", "/images/account/Emp/" + '<%=Session["fileName"] %>');
            $('<%=lkl.ClientID%>').prop("data-lightbox", "example-" + '<%=Session["fileName"] %>');--%>
            <%--$("a").prop("href", "/images/account/Emp/" + '<%=Session["fileName"] %>');
            $("a").prop("data-lightbox", "example-" + '<%=Session["fileName"] %>');--%>
            return (false);
        }
    </script>
    <script>
        $(document).ready(function () {
            $("#<%=btsearch.ClientID%>").click(function () {
                alert('Under progress');
                //PopupCenter('lookup_mst_Emp.aspx', 'xtf', '900', '500');
                //$(this).removeClass('optional').addClass('selected');
            });

        });

        function openPopup(url) {
            //alert($(url).attr('href'));
            //alert('http://localhost:29002/' + url)
            console.log(url.length);
            $('#popUpimg').attr('src', 'http://' + window.location.host + '/' + url);
            $('#btnPopup').click();

        }

        $(document).ready(function () {
            $("#<%=txtEmpCode.ClientID%>").keydown(function (e) {
                // Allow: backspace, delete, tab, escape, enter and .
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                    // Allow: Ctrl+A, Command+A
                    (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                    // Allow: home, end, left, right, down, up
                    (e.keyCode >= 35 && e.keyCode <= 40)) {
                    // let it happen, don't do anything
                    return;
                }
                // Ensure that it is a number and stop the keypress
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });
            $("#<%=txtMobile.ClientID%>").keydown(function (e) {
                // Allow: backspace, delete, tab, escape, enter and .
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                    // Allow: Ctrl+A, Command+A
                    (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                    // Allow: home, end, left, right, down, up
                    (e.keyCode >= 35 && e.keyCode <= 40)) {
                    // let it happen, don't do anything
                    return;
                }
                // Ensure that it is a number and stop the keypress
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });
            $("#<%=txtIqama.ClientID%>").keydown(function (e) {
                // Allow: backspace, delete, tab, escape, enter and .
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                    // Allow: Ctrl+A, Command+A
                    (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                    // Allow: home, end, left, right, down, up
                    (e.keyCode >= 35 && e.keyCode <= 40)) {
                    // let it happen, don't do anything
                    return;
                }
                // Ensure that it is a number and stop the keypress
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });
        });
    </script>
    <style>
        .form-horizontal.required .form-control {
            content: "*";
            color: red;
        }

        .mygrid thead th, .mygrid tbody th {
            background-color: #5D7B9D !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" runat="Server">
    <asp:HiddenField ID="hdAssecode" runat="server"></asp:HiddenField>

    <div class="form-horizontal" style="font-family: Calibri; font-size: small">
        <h4 class="jajarangenjang">Master Employee</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <label class="control-label col-md-1">Emp Code</label>
            <div class="col-md-2">

                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <div class="input-group">

                            <asp:HiddenField ID="hdfOldFileName" runat="server"></asp:HiddenField>
                            <asp:TextBox ID="txtEmpCode" CssClass="form-control input-group-sm" MaxLength="4" runat="server"></asp:TextBox>
                            <div class="input-group-btn">
                                <asp:LinkButton ID="btsearch" CssClass="btn btn-primary" runat="server"><span class="glyphicon glyphicon-search"></span></asp:LinkButton>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>

            <label class="control-label col-md-1">Full Name</label>
            <div class="col-md-2 require">
                <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtName" runat="server" Text="" CssClass="form-control input-sm"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">Full Name Arabic</label>
            <div class="col-md-2">
                <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtNameArabic" Style="text-align: right;" runat="server" Text="" CssClass="form-control input-sm"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">Photo</label>
            <div class="col-md-2">
                <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                    <ContentTemplate>
                        <img src="" id="imgEmployeePhoto" runat="server" alt="photo" style="width: auto; height: 50px;" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="form-group">
            <label class="control-label col-md-1">Sort Name</label>
            <div class="col-md-2">
                <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtSortName" Style="text-align: right;" runat="server" Text="" CssClass="form-control input-sm"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">Join Date</label>
            <div class="col-md-2 require">
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="dtJoin" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" CssClass="date" runat="server" TargetControlID="dtJoin" Format="d/M/yyyy">
                        </asp:CalendarExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">Birthdate</label>
            <div class="col-md-2 require">
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="dtDOB" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                        <asp:CalendarExtender ID="dtPurchase_CalendarExtender" CssClass="date" runat="server" TargetControlID="dtDOB" Format="d/M/yyyy">
                        </asp:CalendarExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">Gender</label>
            <asp:UpdatePanel ID="UpdatePanel14" runat="server">

                <ContentTemplate>
                    <asp:RadioButton ID="cbGenderMale" runat="server" Checked="true" Text="Male" GroupName="gender" />
                    <asp:RadioButton ID="cbGenderFemale" runat="server" Text="Female" GroupName="gender" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">Nationality</label>
            <div class="col-md-2 drop-down require">
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlNationality" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlNationality_SelectedIndexChanged"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1 required">Religion</label>
            <div class="col-md-2 drop-down require">
                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlReligion" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlReligion_SelectedIndexChanged"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1 required">Blood Group</label>
            <div class="col-md-2  drop-down require">
                <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlBloodGroup" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1 required">Married Status</label>
            <div class="col-md-2  drop-down require">
                <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlMarriedStatus" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">Birth Place</label>
            <div class="col-md-2 require">
                <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtBirthPalce" runat="server" Text="" CssClass="form-control input-sm"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">Education </label>
            <div class="col-md-2  drop-down require">
                <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlEducation" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEducation_SelectedIndexChanged"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">Mobile Number </label>
            <div class="col-md-2 require">
                <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtMobile" runat="server" Text="" MaxLength="10" CssClass="form-control input-sm"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div>
            <h4 class="jajarangenjang">Employment Info</h4>
            <div class="h-divider"></div>
            <div class="form-group">

            
            <label class="control-label col-md-1">Iqama</label>
            <div class="col-md-2 require">
                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtIqama" runat="server" Text="" MaxLength="10" CssClass="form-control input-sm"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">Iqama Exipry</label>
            <div class="col-md-2 require">
                <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="dtIqamaExipry" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" CssClass="date" runat="server" TargetControlID="dtIqamaExipry" Format="d/M/yyyy">
                        </asp:CalendarExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">Passport</label>
            <div class="col-md-2 require">
                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtPassport" runat="server" Text="" CssClass="form-control input-sm"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">Passport Exipry</label>
            <div class="col-md-2 require">
                <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="dtPassportExipry" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender3" CssClass="date" runat="server" TargetControlID="dtPassportExipry" Format="d/M/yyyy">
                        </asp:CalendarExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
                </div>
            <div class="form-group">
            <label class="control-label col-md-1">Insurance Number</label>
            <div class="col-md-2 ">
                <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtInsuranceNumber" runat="server" Text="" CssClass="form-control input-sm"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">Insurance Expiry</label>
            <div class="col-md-2 ">

                <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="dtInsuranceExipry" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender4" CssClass="date" runat="server" TargetControlID="dtInsuranceExipry" Format="d/M/yyyy">
                        </asp:CalendarExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">Sponsor</label>
            <div class="col-md-2 require">
                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtSponsor" runat="server" Text="" CssClass="form-control input-sm"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">Branch Name</label>
            <div class="col-md-2  drop-down require">
                <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbsalespoint" Enabled="false" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
                </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">Old Department</label>
            <div class="col-md-2 ">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblOldDepartment" runat="server" Text='N/A'></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">Department</label>
            <div class="col-md-2  drop-down require">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlDepartment" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">Old Job Level</label>
            <div class="col-md-2 ">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblJobLevel" runat="server" Text='N/A'></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">Job Level</label>
            <div class="col-md-2  drop-down require ">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlJobLevel" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlJobLevel_SelectedIndexChanged"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>



        </div>
        <div class="form-group">
            <label class="control-label col-md-1">Old Job Title</label>
            <div class="col-md-2 ">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblOldJobTitle" runat="server" Text='N/A'></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <label class="control-label col-md-1">Job Title</label>
            <div class="col-md-2 drop-down require">
                <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlJobTitle" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlJobTitle_SelectedIndexChanged"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div style="display: none;">
                <label class="control-label col-md-1" style="display: none;">Job Title Visa</label>
                <div class="col-md-2 require">
                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlJobTitleVisa" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlJobTitleVisa_SelectedIndexChanged"></asp:DropDownList>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <label class="control-label col-md-1">Upload File</label>
            <div class="col-md-2">

                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                    <ContentTemplate>
                        <input type="submit" value="Upload Files" id="btnUploadFiles" onclick="uploadFiles()" title="UploadFiles" />
                        <asp:Label ID="lbfileloc" runat="server" Text=''></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

        </div>


        <div class="row navi padding-bottom padding-top margin-bottom" style="text-align: center;">
            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                <ContentTemplate>

                    <asp:LinkButton ID="btnew" CssClass="btn-primary btn" runat="server" OnClick="btnew_Click">New</asp:LinkButton>
                    <asp:LinkButton ID="btsave" CssClass="btn-warning btn btn-warning" runat="server" OnClick="btsave_Click">Save</asp:LinkButton>
                    <asp:LinkButton ID="btprint" CssClass="btn-danger btn btn-dangerdivhid" runat="server" OnClick="btDelete_Click">Delete</asp:LinkButton>
                    <asp:LinkButton ID="btprintall" runat="server" CssClass="btn-info btn" OnClick="btprint_Click">Print</asp:LinkButton>
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="button2 save" OnClick="btnUpload_Click" Style="display: none" />

                    <asp:Button ID="btlookup" runat="server" Text="Button" OnClick="btlookup_Click" Style="display: none" />
                    <button type="button" class="btn btn-info btn-lg" id="btnPopup" data-toggle="modal" data-target="#myModal" style="display: none">Open Modal</button>
                </ContentTemplate>
            </asp:UpdatePanel>
            <span style="padding-left: 378px;"><span style="color: red">*</span> Mandotry field</span>
        </div>
        <div class="form-group">
            <div class="col-md-12">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="table-page-fixer">
                            <div class="overflow-y relative" style="max-height: 250px;">
                                <asp:GridView ID="grd"
                                    runat="server" CssClass="table table-striped table-page-fix table-hover table-fix mygrid" data-table-page="#ss"
                                    AutoGenerateColumns="False" AllowPaging="True" OnSelectedIndexChanging="grd_SelectedIndexChanging"
                                    OnPageIndexChanging="grd_PageIndexChanging" CellPadding="0" GridLines="None" PageSize="5">
                                    <AlternatingRowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Emp Number">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmpno" runat="server" Text='<%#Eval("emp_cd") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Emp Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfullname" runat="server" Text='<%#Eval("fullname") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Passport No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpassport_no" runat="server" Text='<%#Eval("passport_no") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Iqama">
                                            <ItemTemplate>
                                                <asp:Label ID="lbliqoma_no" runat="server" Text='<%#Eval("iqoma_no") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Job Title">
                                            <ItemTemplate>
                                                <asp:Label ID="lblJobtitle_visa" runat="server" Text='<%#Eval("Jobtitle_visa") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BirthDate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmpTypes" runat="server" Text='<%# Eval("birth_dt", "{0:dd/MM/yyyy}") %>'> ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Dependent">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDependentCount" runat="server" Text='<%#Eval("countDependent") %>'> ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Image">
                                            <ItemTemplate>
                                                <a class="example-image-link" id="<%#Eval("emp_cd") %>" onclick="openPopup('<%# Eval("imagepath") %>')" href="#" style="color: blue;">
                                                    <asp:Label ID="lbfileloc" runat="server" Text='Photo'></asp:Label>
                                                </a>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowSelectButton="True" />
                                    </Columns>
                                    <EditRowStyle CssClass="table-edit" />
                                    <FooterStyle CssClass="table-footer" />
                                    <HeaderStyle CssClass="table-header" />
                                    <PagerStyle CssClass="table-page" />
                                    <RowStyle />
                                    <SelectedRowStyle CssClass="table-edit" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="table-copy-page-fix" id="ss"></div>
                    </ContentTemplate>
                    <Triggers>
                        <%--<asp:AsyncPostBackTrigger ControlID="cbsalespoint" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cbEmp_type" EventName="SelectedIndexChanged" />--%>
                    </Triggers>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>
    <div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Employee Image</h4>
                </div>
                <div class="modal-body">
                    <img id="popUpimg" src="" width="200" height="200" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>
</asp:Content>

