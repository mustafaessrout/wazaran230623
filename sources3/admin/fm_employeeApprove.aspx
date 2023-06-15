<%@ Page Title="" Language="C#" MasterPageFile="~/admin/promotor2.master" AutoEventWireup="true" CodeFile="fm_employeeApprove.aspx.cs" Inherits="fm_employeeApprove" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
        .h-divider {
            margin-top: 5px;
            margin-bottom: 5px;
            height: 1px;
            width: 100%;
            border-top: 1px solid gray;
        }
    </style>
    <script>
        function EmpSelected(sender, e) {
            <%--$get('<%=hdEmployee.ClientID%>').value = e.get_value();--%>
            $get('<%=btsearch.ClientID%>').click();
        }

        function allowOnlyDigit(evt, obj) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode == 48) {
                var val = obj.value;
                if (val.length < 1) {
                    return false;
                }
            }
            var val = obj.value;
            if (val.length < 2)
                return true;
            else
                return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" runat="Server">
    <div class="container">
        <h3>Employee Approved</h3>
        <div class="form-horizontal">
            <%--<div class="form-group">

                <label class="control-label col-md-1">Employee:</label>
                <div class="col-md-4">
                    <asp:TextBox ID="txtEmployee" runat="server" CssClass="form-control-static" Width="100%"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txEmployee_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txtEmployee" UseContextKey="True" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" OnClientItemSelected="EmpSelected">
                    </asp:AutoCompleteExtender>
                    <asp:HiddenField ID="hdEmployee" runat="server" />
                </div>
            </div>--%>
            <div class="h-divider"></div>
            <div class="form-group">
                <label class="control-label col-md-1">Branch For Approved</label>
                <div class="col-md-2  drop-down require">
                    <asp:UpdatePanel ID="UpdatePanel54" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbAllBranch" CssClass="form-control input-sm" OnSelectedIndexChanged="cbAllBranch_SelectedIndexChanged"
                                AutoPostBack="true" runat="server">
                                <%--AutoPostBack="true" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged"--%>
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-md-1">Emp Code</label>
                <div class="col-md-2  drop-down">

                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlEmployee" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                            <%--<div class="input-group">--%>

                            <asp:HiddenField ID="hdfEmpCode" runat="server"></asp:HiddenField>
                            <%--<asp:TextBox ID="txtEmpCode" CssClass="form-control input-group-sm" MaxLength="4" runat="server"></asp:TextBox>--%>
                            <div class="input-group-btn">
                                <%--<asp:LinkButton ID="btsearch" CssClass="btn btn-primary" runat="server"><span class="glyphicon glyphicon-search"></span></asp:LinkButton>--%>
                            </div>
                            <%--</div>--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>

                <label class="control-label col-md-1">Full Name</label>
                <div class="col-md-2 require">
                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtName" runat="server" Text="" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">Full Name Arabic</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtNameArabic" Style="text-align: right;" runat="server" Text="" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">Photo</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                        <ContentTemplate>
                            <img src="www.vom" id="imgEmployeePhoto" runat="server" alt="photo" style="width: initial; height: 150px;" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-md-1">Sort Name</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtSortName" Style="text-align: right;" runat="server" Text="" CssClass="form-control input-sm ro"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">Join Date</label>
                <div class="col-md-2 require">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="dtJoin" runat="server" CssClass="form-control input-sm ro"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" CssClass="date" runat="server" TargetControlID="dtJoin" Format="d/M/yyyy">
                            </asp:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">Birthdate</label>
                <div class="col-md-2 require">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="dtDOB" runat="server" CssClass="form-control input-sm ro"></asp:TextBox>
                            <asp:CalendarExtender ID="dtPurchase_CalendarExtender" CssClass="date" runat="server" TargetControlID="dtDOB" Format="d/M/yyyy">
                            </asp:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">Gender</label>
                <asp:UpdatePanel ID="UpdatePanel14" runat="server">

                    <ContentTemplate>
                        <asp:RadioButton ID="cbGenderMale" runat="server" Checked="true" CssClass="ro" Text="Male" GroupName="gender" />
                        <asp:RadioButton ID="cbGenderFemale" runat="server" Text="Female" CssClass="ro" GroupName="gender" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="form-group">
                <label class="control-label col-md-1">Nationality</label>
                <div class="col-md-2 drop-down require">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlNationality" CssClass="form-control input-sm ro" runat="server"></asp:DropDownList>
                            <%--AutoPostBack="true" OnSelectedIndexChanged="ddlNationality_SelectedIndexChanged"--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1 required">Religion</label>
                <div class="col-md-2 drop-down require">
                    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlReligion" CssClass="form-control input-sm ro" runat="server"></asp:DropDownList>
                            <%--AutoPostBack="true" OnSelectedIndexChanged="ddlReligion_SelectedIndexChanged"--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1 required">Blood Group</label>
                <div class="col-md-2  drop-down require">
                    <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlBloodGroup" CssClass="form-control input-sm ro" runat="server"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1 required">Married Status</label>
                <div class="col-md-2  drop-down require">
                    <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlMarriedStatus" CssClass="form-control input-sm ro" runat="server"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-md-1">Branch Name</label>
                <div class="col-md-2  drop-down require">
                    <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbsalespoint" Enabled="false" CssClass="form-control input-sm ro" runat="server">
                                <%--AutoPostBack="true" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged"--%>
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">Education </label>
                <div class="col-md-2  drop-down require">
                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlEducation" CssClass="form-control input-sm ro" runat="server"></asp:DropDownList>
                            <%--AutoPostBack="true" OnSelectedIndexChanged="ddlEducation_SelectedIndexChanged"--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">Mobile Number </label>
                <div class="col-md-2 require">
                    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtMobile" runat="server" Text="" MaxLength="10" CssClass="form-control input-sm ro"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">Approve ID </label>
                <div class="col-md-2 require">
                    <asp:UpdatePanel ID="UpdatePanel37" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtemployee_app_cd" runat="server" Text="" MaxLength="10" CssClass="form-control input-sm ro"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <%--<label class="control-label col-md-1">Branch </label>
            <div class="col-md-2" style="width: 80px;">
                <asp:UpdatePanel ID="UpdatePanel66" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtBranch" runat="server" Text="" Width="60" CssClass="form-control input-sm ro"></asp:TextBox>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <asp:UpdatePanel ID="UpdatePanel67" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="txtBranchName" runat="server" Text="" Width="120" CssClass="form-control input-sm ro"></asp:TextBox>
                </ContentTemplate>
            </asp:UpdatePanel>--%>
            </div>
            <div class="form-group">
                <%--<label class="control-label col-md-1">Upload Emp Photo</label>--%>
                <div class="col-md-2 require ">




                    <asp:UpdatePanel ID="UpdatePanel46" runat="server">
                        <ContentTemplate>
                            <%--<asp:FileUpload ID="upEmployeePhoto" runat="server" />--%>
                            <asp:HiddenField ID="hdfEmployeePhoto" runat="server"></asp:HiddenField>


                            <%--<input type="submit" value="Upload Files" id="btnUploadFiles" onclick="uploadFiles()" title="UploadFiles" />
                        <asp:Label ID="lbfileloc" runat="server" Text=''></asp:Label>--%>
                        </ContentTemplate>
                        <%-- <Triggers>
                        <asp:PostBackTrigger ControlID="upEmployeePhoto" />
                    </Triggers>--%>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-md-1">Have Family Here</label>
                <div class="col-md-2 require">
                    <asp:UpdatePanel ID="UpdatePanel66" runat="server">
                        <ContentTemplate>
                            <asp:CheckBox ID="chfam" runat="server" AutoPostBack="true" OnCheckedChanged="chfam_CheckedChanged" Enabled="false" Checked="false" CssClass="control-label col-md-5 center"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <asp:UpdatePanel ID="UpdatePanel67" runat="server">
                    <ContentTemplate>
                        <div id="Depend" runat="server">

                            <label class="control-label col-md-1">Dependants No.</label>
                            <div class="col-md-2 require">
                                <asp:UpdatePanel ID="UpdatePanel68" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtDependant" runat="server" Text="" onkeypress="return allowOnlyDigit(event, this)" CssClass="form-control input-sm ro" AutoCompleteType="Disabled"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDependant"
                                            ValidationExpression="^\d\d{0,2}$" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtDependant_FilteredTextBoxExtender1" runat="server"
                                            Enabled="True" TargetControlID="txtDependant" FilterType="Numbers">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
           
            <label class="control-label col-md-1">Status</label>
            <div class="col-md-2 center">
                <asp:UpdatePanel ID="UpdatePanel69" runat="server">
                    <ContentTemplate>
                        <asp:label ID="lblStatus" runat="server" Style="text-align:  center; font-size: larger" Font-Bold="true" Text="" CssClass="control-label col-md-5 center"></asp:label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
                 </div>
            <div>
                <h4 class="jajarangenjang">Employment Info</h4>
                <div class="h-divider"></div>


                <div class="form-group">
                </div>
                <div class="form-group">
                    <label class="control-label col-md-2">Iqama Number</label>
                    <div class="col-md-2 require">
                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtIqama" runat="server" Text="" MaxLength="10" CssClass="form-control input-sm ro"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-2">Iqama Exipry</label>
                    <div class="col-md-2 require">
                        <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtIqamaExipry" runat="server" CssClass="form-control input-sm ro"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" CssClass="date" runat="server" TargetControlID="dtIqamaExipry" Format="d/M/yyyy">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <%--<label class="control-label col-md-1">Upload Iqama</label>--%>
                    <div class="col-md-2">


                        <%--<asp:FileUpload ID="uplIqama" runat="server" />--%>
                        <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdfIqama" runat="server"></asp:HiddenField>
                                <%--<input type="submit" value="Upload Files" id="btnUploadFiles" onclick="uploadFiles()" title="UploadFiles" />
                        <asp:Label ID="lbfileloc" runat="server" Text=''></asp:Label>--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <a id="lnkIqama" href="www.com" target="_blank" runat="server" title="Download">Download</a>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-md-2">Passport Number</label>
                    <div class="col-md-2 require">
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtPassport" runat="server" Text="" CssClass="form-control input-sm ro"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-2">Passport Exipry</label>
                    <div class="col-md-2 require">
                        <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtPassportExipry" runat="server" CssClass="form-control input-sm ro"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender3" CssClass="date" runat="server" TargetControlID="dtPassportExipry" Format="d/M/yyyy">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <%--<label class="control-label col-md-1">Upload Passport</label>--%>
                    <div class="col-md-2">


                        <%--<asp:FileUpload ID="upPassport" runat="server" />--%>
                        <asp:UpdatePanel ID="UpdatePanel34" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdfPassport" runat="server"></asp:HiddenField>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel45" runat="server">
                            <ContentTemplate>
                                <a id="lnkPassport" href="www.com" target="_blank" runat="server" title="Download">Download</a>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-md-2">Insurance Number</label>
                    <div class="col-md-2 ">
                        <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtInsuranceNumber" runat="server" Text="" CssClass="form-control input-sm ro"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-2">Insurance Expiry</label>
                    <div class="col-md-2 ">

                        <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtInsuranceExipry" runat="server" CssClass="form-control input-sm ro"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender4" CssClass="date" runat="server" TargetControlID="dtInsuranceExipry" Format="d/M/yyyy">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <%--<label class="control-label col-md-1">Upload Insur. Info</label>--%>
                    <div class="col-md-2">

                        <%--<asp:FileUpload ID="upInsurance" runat="server" />--%>
                        <asp:UpdatePanel ID="UpdatePanel35" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdfInsurance" runat="server"></asp:HiddenField>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel62" runat="server">
                            <ContentTemplate>
                                <a id="lnkInsur" href="www.com" target="_blank" runat="server" title="Download">Download</a>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-md-2">National Address</label>
                    <div class="col-md-2  require">
                        <asp:UpdatePanel ID="UpdatePanel36" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtNationalAddress" runat="server" Text="" CssClass="form-control input-sm ro"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <%--<label class="control-label col-md-1">Upload Add. Info</label>--%>
                    <div class="col-md-2">

                        <%--<asp:FileUpload ID="upNationalAddress" runat="server" />--%>
                        <asp:UpdatePanel ID="UpdatePanel38" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdfNationalAdd" runat="server"></asp:HiddenField>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <a id="lnkAddress" href="www.com" target="_blank" runat="server" title="Download">Download</a>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-md-2">Sponsor</label>
                    <div class="col-md-2 require">
                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtSponsor" runat="server" Text="" CssClass="form-control input-sm ro"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-2">Birth Place</label>
                    <div class="col-md-2 require">
                        <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtBirthPalce" runat="server" Text="" CssClass="form-control input-sm ro"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-md-2">Old Job Title</label>
                    <div class="col-md-2 ">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblOldJobTitle" runat="server" Text='N/A'></asp:Label>
                                <asp:HiddenField ID="hdfOldJobtTitle" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <label class="control-label col-md-2">Job Title</label>
                    <div class="col-md-2 drop-down require">
                        <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlJobTitle" CssClass="form-control input-sm ro" runat="server"></asp:DropDownList>
                                <%--AutoPostBack="true" OnSelectedIndexChanged="ddlJobTitle_SelectedIndexChanged"--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-md-2">Old Department</label>
                    <div class="col-md-2 ">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblOldDepartment" runat="server" Text='N/A'></asp:Label>
                                <asp:HiddenField ID="hdfOldDep" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-2">Department</label>
                    <div class="col-md-2  drop-down require">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlDepartment" CssClass="form-control input-sm ro" runat="server"></asp:DropDownList>
                                <%--AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-md-2">Old Job Level</label>
                    <div class="col-md-2 ">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblJobLevel" runat="server" Text='N/A'></asp:Label>
                                <asp:HiddenField ID="hdfOldJobLevel" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-2">Job Level</label>
                    <div class="col-md-2  drop-down require ">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlJobLevel" CssClass="form-control input-sm ro" runat="server"></asp:DropDownList>
                                <%--AutoPostBack="true" OnSelectedIndexChanged="ddlJobLevel_SelectedIndexChanged"--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-md-1">Employee Status</label>
                    <asp:UpdatePanel ID="UpdatePanel48" runat="server">

                        <ContentTemplate>
                            <asp:RadioButton ID="cb_emp_sta_active" runat="server" Checked="true" CssClass="ro" Text="Active" GroupName="active" />
                            <asp:RadioButton ID="cb_emp_sta_decative" runat="server" CssClass="ro" Text="Deactive" GroupName="active" />
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <div class="form-group">
                    <label class="control-label col-md-1">Sub Status</label>
                    <div class="col-md-2 drop-down require">
                        <asp:UpdatePanel ID="UpdatePanel42" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbemp_std" runat="server" CssClass="form-control ro"></asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Active / Deactive </label>
                    <div class="col-md-2 require">
                        <asp:UpdatePanel ID="UpdatePanel43" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="emp_std_Dt" runat="server" CssClass="form-control input-sm ro"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender5" CssClass="date" runat="server" TargetControlID="emp_std_Dt" Format="d/M/yyyy">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <%--<label class="control-label col-md-1">Upload Emp Status</label>--%>
                    <div class="col-md-2 ">


                        <%--<asp:FileUpload ID="upEmp_std" runat="server" />--%>
                        <asp:UpdatePanel ID="UpdatePanel44" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdfEmp_std" runat="server"></asp:HiddenField>
                                <%--<input type="submit" value="Upload Files" id="btnUploadFiles" onclick="uploadFiles()" title="UploadFiles" />
                        <asp:Label ID="lbfileloc" runat="server" Text=''></asp:Label>--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel63" runat="server">
                            <ContentTemplate>
                                <a id="lnkupEmp_std" href="www.com" target="_blank" runat="server" title="Download">Download</a>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-md-1">Transfer Branch</label>
                    <div class="col-md-2  drop-down">
                        <asp:UpdatePanel ID="UpdatePanel47" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbsalespointTransfer" CssClass="form-control input-sm ro" runat="server">
                                    <%--AutoPostBack="true" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged"--%>
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Driving license No</label>
                    <div class="col-md-2 require">
                        <asp:UpdatePanel ID="UpdatePanel49" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtDrivinglicenseNo" runat="server" Text="" CssClass="form-control input-sm ro"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <%--<label class="control-label col-md-1">Upload Driving License</label>--%>
                    <div class="col-md-2 ">


                        <%--<asp:FileUpload ID="upDrivingLicense" runat="server" />--%>
                        <asp:UpdatePanel ID="UpdatePanel50" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdfDrivingLicense" runat="server"></asp:HiddenField>
                                <%--<input type="submit" value="Upload Files" id="btnUploadFiles" onclick="uploadFiles()" title="UploadFiles" />
                        <asp:Label ID="lbfileloc" runat="server" Text=''></asp:Label>--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel64" runat="server">
                            <ContentTemplate>
                                <a id="lnkDrivingLicense" href="www.com" target="_blank" runat="server" title="Download">Download</a>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-md-1">Driving license type </label>
                    <div class="col-md-2  drop-down">
                        <asp:UpdatePanel ID="UpdatePanel51" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlLicenseType" CssClass="form-control input-sm ro" runat="server"></asp:DropDownList>
                                <%--AutoPostBack="true" OnSelectedIndexChanged="ddlEducation_SelectedIndexChanged"--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">License Exipry</label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel52" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtLicenseExipry" runat="server" CssClass="form-control input-sm ro"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender6" CssClass="date" runat="server" TargetControlID="txtLicenseExipry" Format="d/M/yyyy">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <h4 class="jajarangenjang">Vehicle Info</h4>
                <div class="h-divider"></div>
                <div class="form-group">
                    <label class="control-label col-md-1">Vehicle Sponsor No</label>
                    <div class="col-md-2 ">
                        <asp:UpdatePanel ID="UpdatePanel53" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtVehicleSponsor" runat="server" Text="" CssClass="form-control input-sm ro"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <label class="control-label col-md-1">VIN No</label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel55" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtVIN" runat="server" Text="" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Sample</label>
                    <div class="col-md-2 ">

                        <asp:UpdatePanel ID="UpdatePanel56" runat="server">
                            <ContentTemplate>
                                <label class="">KMAAM81AAFU106088</label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-md-1">Car Type English</label>
                    <div class="col-md-2 ">
                        <asp:UpdatePanel ID="UpdatePanel57" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtCarTypeEnglish" runat="server" Text="" CssClass="form-control input-sm ro"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Car Type Arabic</label>
                    <div class="col-md-2 ">
                        <asp:UpdatePanel ID="UpdatePanel58" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtCarTypeArabic" runat="server" Text="" CssClass="form-control input-sm ro"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Model Year</label>
                    <div class="col-md-2 ">
                        <asp:UpdatePanel ID="UpdatePanel59" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtModelYear" runat="server" Text="" CssClass="form-control input-sm ro"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Car Expiry</label>
                    <div class="col-md-2 ">
                        <asp:UpdatePanel ID="UpdatePanel60" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtCarExpiry" runat="server" CssClass="form-control input-sm ro"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender7" CssClass="date" runat="server" TargetControlID="txtCarExpiry" Format="d/M/yyyy">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="form-group">
                    <label class="control-label col-md-3">Vehicle Plate No English</label>
                    <div class="col-md-3 ">
                        <asp:UpdatePanel ID="UpdatePanel39" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtPlate1" runat="server" Text="" CssClass="ro" Width="80" placeholder="1234"></asp:TextBox>
                                <asp:TextBox ID="txtPlate2" runat="server" Text="" CssClass="ro" Width="30" placeholder="A"></asp:TextBox>
                                <asp:TextBox ID="txtPlate3" runat="server" Text="" CssClass="ro" Width="30" placeholder="B"></asp:TextBox>
                                <asp:TextBox ID="txtPlate4" runat="server" Text="" CssClass="ro" Width="30" placeholder="C"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-3">Vehicle Plate No Arabic</label>
                    <div class="col-md-3 ">
                        <asp:UpdatePanel ID="UpdatePanel40" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtPlateArabic1" runat="server" Text="" CssClass="ro" Width="80" placeholder="1234"></asp:TextBox>
                                <asp:TextBox ID="txtPlateArabic2" runat="server" Text="" CssClass="ro" Width="30" placeholder="ج"></asp:TextBox>
                                <asp:TextBox ID="txtPlateArabic3" runat="server" Text="" CssClass="ro" Width="30" placeholder="ب"></asp:TextBox>
                                <asp:TextBox ID="txtPlateArabic4" runat="server" Text="" CssClass="ro" Width="30" placeholder="أ"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <%-- <label class="control-label col-md-1">Upload Vehicle Info</label>--%>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel41" runat="server">
                            <ContentTemplate>
                                <%--<asp:FileUpload ID="upVehicle" runat="server" />--%>
                                <asp:HiddenField ID="hdfVehicle" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel65" runat="server">
                            <ContentTemplate>
                                <a id="lnkVehicle" href="www.com" target="_blank" runat="server" title="Download">Download</a>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-md-1">Car Color</label>
                    <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel61" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtCarColor" runat="server" Text="" CssClass="form-control input-sm ro"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>



                <div class="form-group">


                    <div style="display: none;">
                        <label class="control-label col-md-1" style="display: none;">Job Title Visa</label>
                        <div class="col-md-2 ">
                            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlJobTitleVisa" CssClass="form-control input-sm ro" runat="server"></asp:DropDownList>
                                    <%--AutoPostBack="true" OnSelectedIndexChanged="ddlJobTitleVisa_SelectedIndexChanged"--%>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>


                </div>




            </div>
        </div>
    </div>

    <div class="navi">
        <asp:Button ID="btsearch" runat="server" Text="Button" OnClick="btsearch_Click" CssClass="divhid" Visible="false" />
        <asp:Button ID="btsave" runat="server" Text="Partially Approve" CssClass="btn btn-default" OnClick="btsave_Click" Visible="false" />
        <asp:Button ID="btReject" runat="server" Text="Reject" CssClass="btn btn-default" OnClick="btReject_Click" Visible="false" />
        <asp:Button ID="btprintEmp" runat="server" Text="Print" CssClass="btn btn-default" OnClick="btprintEmp_Click" Visible="false" />
        <asp:Button ID="btprintEmpAll" runat="server" Text="Print All" CssClass="btn btn-default" OnClick="btprintEmpAll_Click" />
        <asp:Button ID="btprintEmpAllbyBrn" runat="server" Text="Print All By Branch" CssClass="btn btn-default" OnClick="btprintEmpAllbyBrn_Click" />
    </div>
</asp:Content>

