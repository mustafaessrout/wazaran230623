using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mst_employeeBranchApp : System.Web.UI.Page
{
    //Can not delete
    //Can not new
    //Add status  A , I from which date
    //remarks with fixed reason
    //Resign
    //Transfer to other

    //National Address mandatory


    //Iqama manadatory
    //Driving lic

    //Vehicle info

    cbll bll = new cbll();
    Utitlity ut = new Utitlity();
    cdal cdl = new cdal();
    long unixTime = new long();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        DateTime foo = DateTime.UtcNow;
        unixTime = ((DateTimeOffset)foo).UtcTicks;

        //long unixTimestamp = Convert.ToString(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        //(Int32)(DateTime.UtcNow.Subtract(DateTime.Now)).TotalSeconds;

        if (!IsPostBack)
        {


            bindControl();
            dtDOB.Text = System.DateTime.Today.ToString("d/M/yyyy");
            dtJoin.Text = System.DateTime.Today.ToString("d/M/yyyy");
            dtInsuranceExipry.Text = System.DateTime.Today.ToString("d/M/yyyy");
            dtIqamaExipry.Text = System.DateTime.Today.ToString("d/M/yyyy");
            dtPassportExipry.Text = System.DateTime.Today.ToString("d/M/yyyy");
            emp_std_Dt.Text = System.DateTime.Today.ToString("d/M/yyyy");
            //txtNationalAddress.Text = "12345678901";
            //btsave.Enabled = false;
            //btprint.Enabled = false;
            imgEmployeePhoto.Src = "image/dummy_prof.jpg";
            ddlEmployee_SelectedIndexChanged(sender, e);
        }
    }

    private void bindControl()
    {
        try { 
        txtBranch.Text = bll.vLookUp("select salespointcd from tuser_profile where emp_Cd='" + HttpContext.Current.Request.Cookies["usr_id"].Value + "'");
        txtBranchName.Text = bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd in ( select salespointcd from tuser_profile where emp_Cd='" + HttpContext.Current.Request.Cookies["usr_id"].Value + "')");
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
        arr.Add(new cArrayList("@fld_nm", "job_title_cd"));
        dt = cdl.GetValueFromSP("sp_tfield_value_get", arr);
        ddlJobTitle.DataSource = dt;
        ddlJobTitle.DataValueField = "fld_valu";
        ddlJobTitle.DataTextField = "fld_desc";
        ddlJobTitle.DataBind();
        arr.Clear();
        arr.Add(new cArrayList("@fld_nm", "driving_licenses_types"));
        dt = cdl.GetValueFromSP("sp_tfield_value_get", arr);
        ddlLicenseType.DataSource = dt;
        ddlLicenseType.DataValueField = "fld_valu";
        ddlLicenseType.DataTextField = "fld_desc";
        ddlLicenseType.DataBind();
        //bll.vBindingComboToSp(ref ddlEmployee, "Sp_tmst_employeeByApp_get", "emp_Cd", "fullname");
        arr.Clear();
        arr.Add(new cArrayList("@salespointcd", txtBranch.Text));
        if (dt.Rows.Count > 0)
        {
            dt = new DataTable();
        }
        dt = cdl.GetValueFromSP("Sp_tmst_employeeByApp_get", arr);
        ddlEmployee.DataSource = dt;
        ddlEmployee.DataValueField = "emp_cd";
        ddlEmployee.DataTextField = "fullname";
        ddlEmployee.DataBind();
        bll.vBindingComboToSp(ref ddlDepartment, "sp_tmst_department_get", "dept_cd", "dept_nm");

        if (dt.Rows.Count > 0)
        {
            dt = new DataTable();
        }
        arr.Clear();
        arr.Add(new cArrayList("@fld_nm", "level_cd"));
        dt = cdl.GetValueFromSP("sp_tfield_value_get", arr);
        ddlJobLevel.DataSource = dt;
        ddlJobLevel.DataValueField = "fld_valu";
        ddlJobLevel.DataTextField = "fld_desc";
        ddlJobLevel.DataBind();
        //if (dt.Rows.Count > 0)
        //{
        //    ddlJobLevel.Items.Remove(ddlJobLevel.Items[1]);
        //}
        if (dt.Rows.Count > 0)
        {
            dt = new DataTable();
        }
        arr.Clear();
        arr.Add(new cArrayList("@reasn_typ", "emp_std"));
        dt = cdl.GetValueFromSP("sp_tmst_reason_get", arr);
        cbemp_std.DataSource = dt;
        cbemp_std.DataValueField = "reasn_cd";
        cbemp_std.DataTextField = "reasn_nm";
        cbemp_std.DataBind();

        if (dt.Rows.Count > 0)
        {
            dt = new DataTable();
        }
        arr.Clear();
        arr.Add(new cArrayList("@fld_nm", "Nationality"));
        dt = cdl.GetValueFromSP("sp_tfield_value_get", arr);
        ddlNationality.DataSource = dt;
        ddlNationality.DataValueField = "fld_valu";
        ddlNationality.DataTextField = "fld_desc";
        ddlNationality.DataBind();
        //if (dt.Rows.Count > 0)
        //{
        //    ddlNationality.Items.Remove(ddlNationality.Items[1]);
        //}

        if (dt.Rows.Count > 0)
        {
            dt = new DataTable();
        }
        arr.Clear();
        arr.Add(new cArrayList("@fld_nm", "jobtitle_visa"));
        dt = cdl.GetValueFromSP("sp_tfield_value_get", arr);
        ddlJobTitleVisa.DataSource = dt;
        ddlJobTitleVisa.DataValueField = "fld_valu";
        ddlJobTitleVisa.DataTextField = "fld_desc";
        ddlJobTitleVisa.DataBind();
        //if (dt.Rows.Count > 0)
        //{
        //    ddlJobTitleVisa.Items.Remove(ddlJobTitleVisa.Items[1]);
        //}

        if (dt.Rows.Count > 0)
        {
            dt = new DataTable();
        }
        arr.Clear();
        arr.Add(new cArrayList("@fld_nm", "religion_cd"));
        dt = cdl.GetValueFromSP("sp_tfield_value_get", arr);
        ddlReligion.DataSource = dt;
        ddlReligion.DataValueField = "fld_valu";
        ddlReligion.DataTextField = "fld_desc";
        ddlReligion.DataBind();
        //if (dt.Rows.Count > 0)
        //{
        //    ddlReligion.Items.Remove(ddlReligion.Items[1]);
        //}

        if (dt.Rows.Count > 0)
        {
            dt = new DataTable();
        }
        arr.Clear();
        arr.Add(new cArrayList("@fld_nm", "edu_cd"));
        dt = cdl.GetValueFromSP("sp_tfield_value_get", arr);
        ddlEducation.DataSource = dt;
        ddlEducation.DataValueField = "fld_valu";
        ddlEducation.DataTextField = "fld_desc";
        ddlEducation.DataBind();

        if (dt.Rows.Count > 0)
        {
            dt = new DataTable();
        }
        arr.Clear();
        arr.Add(new cArrayList("@fld_nm", "blood_cd"));
        dt = cdl.GetValueFromSP("sp_tfield_value_get", arr);
        ddlBloodGroup.DataSource = dt;
        ddlBloodGroup.DataValueField = "fld_valu";
        ddlBloodGroup.DataTextField = "fld_desc";
        ddlBloodGroup.DataBind();

        if (dt.Rows.Count > 0)
        {
            dt = new DataTable();
        }
        arr.Clear();
        arr.Add(new cArrayList("@fld_nm", "married_sta_id"));
        dt = cdl.GetValueFromSP("sp_tfield_value_get", arr);
        ddlMarriedStatus.DataSource = dt;
        ddlMarriedStatus.DataValueField = "fld_valu";
        ddlMarriedStatus.DataTextField = "fld_desc";
        ddlMarriedStatus.DataBind();
        arr.Clear();
        bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespointSN_get", "salespointcd", "salespoint_desc", arr);
        var wadiItem = cbsalespoint.Items[cbsalespoint.Items.Count - 2];
        cbsalespoint.Items.Remove(wadiItem);
        cbsalespoint.SelectedValue = txtBranch.Text;//  Request.Cookies["sp"].Value;
        //if (dt.Rows.Count > 0)
        //{
        //    ddlEducation.Items.Remove(ddlEducation.Items[1]);
        //}
        BindGrid();

        //if (Request.Cookies["sp"].Value == "0")
        if (txtBranch.Text == "0")
        { cbsalespoint.Enabled = true; }
        else { cbsalespoint.Enabled = false; }
        }
        catch (Exception ex)
        {
            ut.Logs("", "HR", "Employee", "fr_mst_employeeBranchApp", "BindControl()", "Exception", ex.Message + ex.InnerException);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('" + ex.Message + ex.InnerException + "','Error to save','warning');", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('File upload failed!','File upload failed');", true);

        }
    }

    private void BindGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();

        //bll.vBindingGridToSp(ref grd, "Sp_tacc_mst_Emp_get", arr);
    }


    private void ClearControl()
    {
        bindControl();
        //txtEmpCode.Text = string.Empty;
        lblJobLevel.Text = string.Empty;
        lblOldDepartment.Text = string.Empty;
        lblOldJobTitle.Text = string.Empty;
        txtNameArabic.Text = string.Empty;
        //txtEmpCode.Text = string.Empty;
        txtIqama.Text = string.Empty;
        txtMobile.Text = string.Empty;
        txtName.Text = string.Empty;
        txtPassport.Text = string.Empty;
        txtSponsor.Text = string.Empty;
        txtSortName.Text = string.Empty;
        txtBirthPalce.Text = string.Empty;
        txtInsuranceNumber.Text = string.Empty;
        dtDOB.Text = System.DateTime.Today.ToString("d/M/yyyy");
        dtJoin.Text = System.DateTime.Today.ToString("d/M/yyyy");
        dtInsuranceExipry.Text = System.DateTime.Today.ToString("d/M/yyyy");
        dtIqamaExipry.Text = System.DateTime.Today.ToString("d/M/yyyy");
        dtPassportExipry.Text = System.DateTime.Today.ToString("d/M/yyyy");




        imgEmployeePhoto.Src = "image/dummy_prof.jpg";
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_mst_employee.aspx");
    }


    private void UploadFiles(string extendendName)
    {

    }

    public void BindCotrole(string emp_cd)
    {
        try
        {
            DataTable dt = new DataTable();
            List<cArrayList> arr = new List<cArrayList>();

            arr.Add(new cArrayList("@salespointcd",txtBranch.Text ));//HttpContext.Current.Request.Cookies["sp"].Value
            arr.Add(new cArrayList("@emp_cd", Convert.ToString(ddlEmployee.SelectedValue)));


            arr.Clear();
            arr.Add(new cArrayList("@emp_cd", Convert.ToString(emp_cd)));
            //dt = cdl.GetValueFromSP("Sp_tacc_mst_Emp_getByID", arr);
            dt = cdl.GetValueFromSP("Sp_tacc_mst_EmpApp_getByID", arr);



            if (dt.Rows.Count > 0)
            {
                bindControl();

                DataTable dtJobTitle = new DataTable();
                DataTable dtDepartment = new DataTable();
                DataTable dtLevel = new DataTable();
                DataTable dtMarriedStatus = new DataTable();
                DataTable dtSalespointcd = new DataTable();
                DataTable dtBloodGroup = new DataTable();
                DataTable dtEducation = new DataTable();
                DataTable dtReligion = new DataTable();
                DataTable dtlicense_typ = new DataTable();
                DataTable dtSalespointTransfer = new DataTable();
                DataTable dtNationality = new DataTable();

                string gender = string.Empty;
                arr.Clear();
                bll.vBindingComboToSp(ref cbsalespointTransfer, "sp_tmst_salespointSN_get", "salespointcd", "salespoint_desc", arr);
                //var wadiTransferItem = cbsalespoint.Items[cbsalespointTransfer.Items.Count - 2];
                //cbsalespointTransfer.Items.Remove(wadiTransferItem);
                //cbsalespointTransfer.SelectedValue = txtBranch.Text;//Request.Cookies["sp"].Value;

                arr.Clear();
                arr.Add(new cArrayList("@fld_nm", "job_title_cd"));
                dtJobTitle = cdl.GetValueFromSP("sp_tfield_value_get", arr);
                arr.Clear();
                arr.Add(new cArrayList("@fld_nm", "Nationality"));
                dtNationality = cdl.GetValueFromSP("sp_tfield_value_get", arr);
                arr.Clear();
                arr.Add(new cArrayList("@fld_nm", "blood_cd"));
                dtBloodGroup = cdl.GetValueFromSP("sp_tfield_value_get", arr);

                arr.Clear();
                arr.Add(new cArrayList("@fld_nm", "married_sta_id"));
                dtMarriedStatus = cdl.GetValueFromSP("sp_tfield_value_get", arr);

                dtDepartment = cdl.GetValueFromSP("sp_tmst_department_get");
                dtSalespointcd = cdl.GetValueFromSP("sp_tmst_salespoint_get");

                arr.Clear();
                arr.Add(new cArrayList("@fld_nm", "level_cd"));
                dtLevel = cdl.GetValueFromSP("sp_tfield_value_get", arr);
                arr.Clear();
                arr.Add(new cArrayList("@fld_nm", "driving_licenses_types"));
                dtlicense_typ = cdl.GetValueFromSP("sp_tfield_value_get", arr);
                arr.Clear();
                arr.Add(new cArrayList("@fld_nm", "edu_cd"));
                dtEducation = cdl.GetValueFromSP("sp_tfield_value_get", arr);

                arr.Clear();
                arr.Add(new cArrayList("@fld_nm", "religion_cd"));
                dtReligion = cdl.GetValueFromSP("sp_tfield_value_get", arr);

                gender = Convert.ToString(dt.Rows[0]["gender"]); ;//Convert.ToString(dt.Rows[0]["gender"]);
                if (gender == "M") { cbGenderMale.Checked = true; } else { cbGenderFemale.Checked = true; }

                ddlEmployee.SelectedValue = Convert.ToString(dt.Rows[0]["emp_cd"]);

                //hdfEmp_std.Value = Convert.ToString(dt.Rows[0]["emp_std_doc"]);
                //hdfDrivingLicense.Value = Convert.ToString(dt.Rows[0]["license_doc"]);
                //hdfInsurance.Value = Convert.ToString(dt.Rows[0]["insurance_Doc"]);
                //hdfNationalAdd.Value = Convert.ToString(dt.Rows[0]["national_Doc"]); ;
                //hdfPassport.Value = Convert.ToString(dt.Rows[0]["passport_Doc"]);
                //hdfVehicle.Value = Convert.ToString(dt.Rows[0]["vehicleDoc"]);
                //hdfIqama.Value = Convert.ToString(dt.Rows[0]["iqamaDoc"]);
                //hdfEmployeePhoto.Value = Convert.ToString(dt.Rows[0]["employeeImage"]);



                ddlEmployee.SelectedValue = Convert.ToString(dt.Rows[0]["emp_cd"]);
                lblJobLevel.Text = Convert.ToString(dt.Rows[0]["old_level_cd_dt"]);
                lblOldDepartment.Text = Convert.ToString(dt.Rows[0]["old_dept_cd"]);
                lblOldJobTitle.Text = Convert.ToString(dt.Rows[0]["old_jobtitle_cd_dt"]);
                ddlEmployee.SelectedValue = Convert.ToString(dt.Rows[0]["emp_cd"]);
                txtIqama.Text = Convert.ToString(dt.Rows[0]["iqoma_no"]);
                txtInsuranceNumber.Text = Convert.ToString(dt.Rows[0]["insurance_no"]);
                txtMobile.Text = Convert.ToString(dt.Rows[0]["mobile_no"]);
                txtName.Text = Convert.ToString(dt.Rows[0]["fullname"]);
                txtNameArabic.Text = Convert.ToString(dt.Rows[0]["emp_nm_arabic"]);
                txtPassport.Text = Convert.ToString(dt.Rows[0]["passport_no"]);
                txtSponsor.Text = Convert.ToString(dt.Rows[0]["comp_id"]);
                txtSortName.Text = Convert.ToString(dt.Rows[0]["emp_sn"]);
                txtBirthPalce.Text = Convert.ToString(dt.Rows[0]["birth_place"]);

                txtDrivinglicenseNo.Text = Convert.ToString(dt.Rows[0]["driving_license"]);
                txtVehicleSponsor.Text = Convert.ToString(dt.Rows[0]["vehicle_owner_id"]);
                txtVIN.Text = Convert.ToString(dt.Rows[0]["vehicle_series_haikal"]);
                txtCarTypeEnglish.Text = Convert.ToString(dt.Rows[0]["car_type_english"]);
                txtCarTypeArabic.Text = Convert.ToString(dt.Rows[0]["car_type_arabic"]);
                txtModelYear.Text = Convert.ToString(dt.Rows[0]["vehicle_model_year"]);

                txtPlate1.Text = Convert.ToString(dt.Rows[0]["vehicle_no1"]);
                txtPlate2.Text = Convert.ToString(dt.Rows[0]["vehicle_no2"]);
                txtPlate3.Text = Convert.ToString(dt.Rows[0]["vehicle_no3"]);
                txtPlate4.Text = Convert.ToString(dt.Rows[0]["vehicle_no4"]);
                txtPlateArabic1.Text = Convert.ToString(dt.Rows[0]["vehicleArabic_no1"]);
                txtPlateArabic2.Text = Convert.ToString(dt.Rows[0]["vehicleArabic_no2"]);
                txtPlateArabic3.Text = Convert.ToString(dt.Rows[0]["vehicleArabic_no3"]);
                txtPlateArabic4.Text = Convert.ToString(dt.Rows[0]["vehicleArabic_no4"]);
                txtCarColor.Text = Convert.ToString(dt.Rows[0]["vehicle_color"]);
                txtNationalAddress.Text = Convert.ToString(dt.Rows[0]["nationalAddress"]);
                ddlEmployee.SelectedValue = Convert.ToString(dt.Rows[0]["emp_cd"]);
                //lblJobLevel.Text = Convert.ToString(dt.Rows[0]["old_level_cd_dt"]);
                //lblOldDepartment.Text = Convert.ToString(dt.Rows[0]["old_dept_cd_dt"]);
                //lblOldJobTitle.Text = Convert.ToString(dt.Rows[0]["old_jobtitle_cd_dt"]);

                string sUrl = "http://172.16.1.18/";
                //string sUrl = bll.vLookUp("select dbo.fn_getcontrolparameter('link_branch')");
                //lnkAddress.HRef = sUrl + @"/images/HR/Employee/" + Convert.ToString(dt.Rows[0]["national_Doc"]);
                //lnkInsur.HRef = sUrl + @"/images/HR/Employee/" + Convert.ToString(dt.Rows[0]["insurance_doc"]);
                //lnkIqama.HRef = sUrl + @"/images/HR/Employee/" + Convert.ToString(dt.Rows[0]["iqamaDoc"]);
                //lnkPassport.HRef = sUrl + @"/images/HR/Employee/" + Convert.ToString(dt.Rows[0]["passport_doc"]);
                //lnkupEmp_std.HRef = sUrl + @"/images/HR/Employee/" + Convert.ToString(dt.Rows[0]["emp_std_doc"]);
                //lnkDrivingLicense.HRef = sUrl + @"/images/HR/Employee/" + Convert.ToString(dt.Rows[0]["license_doc"]);
                //lnkVehicle.HRef = sUrl + @"/images/HR/Employee/" + Convert.ToString(dt.Rows[0]["vehicleDoc"]);

                lnkAddress.HRef = sUrl + @"/images/HR/Employee/" + Convert.ToString(dt.Rows[0]["national_Doc"]);
                lnkInsur.HRef = sUrl + @"/images/HR/Employee/" + Convert.ToString(dt.Rows[0]["insurance_doc"]);
                lnkIqama.HRef = sUrl + @"/images/HR/Employee/" + Convert.ToString(dt.Rows[0]["iqamaDoc"]);
                lnkPassport.HRef = sUrl + @"/images/HR/Employee/" + Convert.ToString(dt.Rows[0]["passport_doc"]);
                lnkupEmp_std.HRef = sUrl + @"/images/HR/Employee/" + Convert.ToString(dt.Rows[0]["emp_std_doc"]);
                lnkVehicle.HRef = sUrl + @"/images/HR/Employee/" + Convert.ToString(dt.Rows[0]["vehicleDoc"]);
                lnkDrivingLicense.HRef = sUrl + @"/images/HR/Employee/" + Convert.ToString(dt.Rows[0]["license_doc"]);

                hdfOldJobtTitle.Value = Convert.ToString(dt.Rows[0]["job_title_cd"]);
                hdfOldJobLevel.Value = Convert.ToString(dt.Rows[0]["level_cd"]);
                hdfOldDep.Value = Convert.ToString(dt.Rows[0]["dept_cd"]);

                //ddlEducation.SelectedValue = Convert.ToString(dt.Rows[0]["profession_iqoma"]);
                //ddlJobTitleVisa.SelectedValue = Convert.ToString(dt.Rows[0]["profession_iqoma"]);
                if (dt.Rows[0]["birth_dt"].ToString() == "")
                {
                    dtDOB.Text = System.DateTime.Today.ToString("d/M/yyyy");
                }
                else
                {
                    dtDOB.Text = Convert.ToDateTime(dt.Rows[0]["birth_dt"]).ToString("d/M/yyyy");
                }
                if (dt.Rows[0]["vehicle_exp_dt"].ToString() == "")
                {
                    txtCarExpiry.Text = System.DateTime.Today.ToString("d/M/yyyy");
                }
                else
                {
                    txtCarExpiry.Text = Convert.ToDateTime(dt.Rows[0]["vehicle_exp_dt"]).ToString("d/M/yyyy");
                }
                if (dt.Rows[0]["join_dt"].ToString() == "")
                {
                    dtJoin.Text = System.DateTime.Today.ToString("d/M/yyyy");
                }
                else
                {
                    dtJoin.Text = Convert.ToDateTime(dt.Rows[0]["join_dt"]).ToString("d/M/yyyy");
                }
                if (dt.Rows[0]["license_exp_dt"].ToString() == "")
                {
                    txtLicenseExipry.Text = System.DateTime.Today.ToString("d/M/yyyy");
                }
                else
                {
                    txtLicenseExipry.Text = Convert.ToDateTime(dt.Rows[0]["license_exp_dt"]).ToString("d/M/yyyy");
                }


                if (Convert.ToString(dt.Rows[0]["passport_exipry"]) == "")
                {
                    dtPassportExipry.Text = System.DateTime.Today.ToString("d/M/yyyy");
                }
                else { dtPassportExipry.Text = Convert.ToDateTime(dt.Rows[0]["passport_exipry"]).ToString("d/M/yyyy"); }


                if (Convert.ToString(dt.Rows[0]["IqamaExpiry"]) == "")
                {
                    dtIqamaExipry.Text = System.DateTime.Today.ToString("d/M/yyyy");
                }
                else { dtIqamaExipry.Text = Convert.ToDateTime(dt.Rows[0]["IqamaExpiry"]).ToString("d/M/yyyy"); }

                if (Convert.ToString(dt.Rows[0]["insuranceExipiry"]) == "")
                {
                    dtInsuranceExipry.Text = System.DateTime.Today.ToString("d/M/yyyy");
                }
                else { dtInsuranceExipry.Text = Convert.ToDateTime(dt.Rows[0]["insuranceExipiry"]).ToString("d/M/yyyy"); }


                //imgEmployeePhoto.Src = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/images/HR/employee/" + dt.Rows[0]["employeeImage"];// "statics/img/dummy_prof.jpg";
                imgEmployeePhoto.Src = "http://172.16.1.18/" + "/images/HR/employee/" + dt.Rows[0]["employeeImage"];// "statics/img/dummy_prof.jpg";
                                                                                                                                                                                        //if (resultsJobTitle.ToList().Count > 0)
                //hdfEmp_std.Value = Convert.ToString(dt.Rows[0]["employeeImage"]);
                //hdfDrivingLicense.Value = Convert.ToString(dt.Rows[0]["license_doc"]);

                if (dt.Rows[0]["nationality"].ToString() != "")
                {

                    var resultsNationality = from myRow in dtNationality.AsEnumerable()
                                     where myRow.Field<string>("fld_valu") == Convert.ToString(dt.Rows[0]["nationality"])
                                     select myRow;

                    if (resultsNationality.ToList().Count > 0)
                    {
                       ddlNationality.SelectedValue = Convert.ToString(dt.Rows[0]["nationality"]);
                    }
                }

                if (dt.Rows[0]["license_typ"].ToString() != "")
                {

                    var resultslicense_typ = from myRow in dtlicense_typ.AsEnumerable()
                                             where myRow.Field<string>("fld_valu") == Convert.ToString(dt.Rows[0]["license_typ"])
                                             select myRow;

                    if (resultslicense_typ.ToList().Count > 0)
                    {
                        ddlLicenseType.SelectedValue = Convert.ToString(dt.Rows[0]["license_typ"]);
                    }
                }
                if (dt.Rows[0]["religion"].ToString() != "")
                {

                    var resultsReligion = from myRow in dtReligion.AsEnumerable()
                                          where myRow.Field<string>("fld_valu") == Convert.ToString(dt.Rows[0]["religion"])
                                          select myRow;

                    if (resultsReligion.ToList().Count > 0)
                    {
                        ddlReligion.SelectedValue = Convert.ToString(dt.Rows[0]["religion"]);
                    }
                }

                if (dt.Rows[0]["education"].ToString() != "")
                {

                    var resultsEdu = from myRow in dtEducation.AsEnumerable()
                                     where myRow.Field<string>("fld_valu") == Convert.ToString(dt.Rows[0]["education"])
                                     select myRow;

                    if (resultsEdu.ToList().Count > 0)
                    {
                        ddlEducation.SelectedValue = Convert.ToString(dt.Rows[0]["education"]);
                    }
                }

                if (dt.Rows[0]["level_cd"].ToString() != "")
                {

                    var resultsLevel = from myRow in dtLevel.AsEnumerable()
                                       where myRow.Field<string>("fld_valu") == Convert.ToString(dt.Rows[0]["level_cd"])
                                       select myRow;

                    if (resultsLevel.ToList().Count > 0)
                    {
                        ddlJobLevel.SelectedValue = Convert.ToString(dt.Rows[0]["level_cd"]);
                    }
                }
                if (dt.Rows[0]["job_title_cd"].ToString() != "")
                {
                    var resultsJobTitle = from myRow in dtJobTitle.AsEnumerable()
                                          where myRow.Field<string>("fld_valu") == Convert.ToString(dt.Rows[0]["job_title_cd"])
                                          select myRow;
                    if (resultsJobTitle.ToList().Count > 0)
                    {
                        ddlJobTitle.SelectedValue = Convert.ToString(dt.Rows[0]["job_title_cd"]);
                    }
                }
                if (dt.Rows[0]["dept_cd"].ToString() != "")
                {

                    var resultsDepartment = from myRow in dtDepartment.AsEnumerable()
                                            where myRow.Field<string>("dept_cd") == Convert.ToString(dt.Rows[0]["dept_cd"])
                                            select myRow;
                    if (resultsDepartment.ToList().Count > 0)
                    {
                        ddlDepartment.SelectedValue = Convert.ToString(dt.Rows[0]["dept_cd"]);
                    }
                }

                var resultsBloodGroup = from myRow in dtBloodGroup.AsEnumerable()
                                        where myRow.Field<string>("fld_valu") == Convert.ToString(dt.Rows[0]["blood_cd"])
                                        select myRow;

                if (resultsBloodGroup.ToList().Count > 0)
                {
                    ddlBloodGroup.SelectedValue = Convert.ToString(dt.Rows[0]["blood_cd"]);
                }

                var resultsMarriedStatus = from myRow in dtMarriedStatus.AsEnumerable()
                                           where myRow.Field<string>("fld_valu") == Convert.ToString(dt.Rows[0]["married_sta_id"])
                                           select myRow;

                if (resultsMarriedStatus.ToList().Count > 0)
                {
                    ddlMarriedStatus.SelectedValue = Convert.ToString(dt.Rows[0]["married_sta_id"]);
                }

                var resultsSalespointcd = from myRow in dtSalespointcd.AsEnumerable()
                                          where myRow.Field<string>("salespointcd") == Convert.ToString(dt.Rows[0]["salespointcd"])
                                          select myRow;

                if (resultsSalespointcd.ToList().Count > 0)
                {
                    cbsalespoint.SelectedValue = Convert.ToString(dt.Rows[0]["salespointcd"]);
                }
                if (dt.Rows[0]["salespointcd_transafer"].ToString() != "")
                {
                    cbsalespointTransfer.SelectedValue = Convert.ToString(dt.Rows[0]["salespointcd_transafer"]);
                }
                txtBranch.Text = bll.vLookUp("select salespointcd from tuser_profile where emp_Cd='" + HttpContext.Current.Request.Cookies["usr_id"].Value + "'");
                txtBranchName.Text = bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd in ( select salespointcd from tuser_profile where emp_Cd='" + HttpContext.Current.Request.Cookies["usr_id"].Value + "')");
            }
        }
        catch (Exception ex)
        {
            ut.Logs("", "HR", "Employee", "fr_mst_employeeBranchApp", "BindControl(emp_cd)", "Exception", ex.Message + ex.InnerException);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('" + ex.Message + ex.InnerException + "','Error to save','warning');", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('File upload failed!','File upload failed');", true);

        }
    }



    protected void btDelete_Click(object sender, EventArgs e)
    {
        if (ddlEmployee.SelectedValue == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please select record first. ','select record','warning');", true);
        }
        else
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@Empno", Convert.ToString(ddlEmployee.SelectedValue)));
            bll.vDeleteMastEmp(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Record Deleted successfully ','Deleted Success','success');", true);
            ClearControl();
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('/fm_report2.aspx?src=mst_Emp');", true);
    }

    protected void ddlJobTitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
    }
    protected void ddlNationality_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
    }
    protected void ddlJobLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
    }


    protected void ddlJobTitleVisa_SelectedIndexChanged(object sender, EventArgs e)
    {
    }


    protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlReligion_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlEducation_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCotrole(ddlEmployee.SelectedValue);
    }


    protected void btnViewApp_Click(object sender, EventArgs e)
    {

    }
    protected void btprintEmp_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=employeeBranchApp&emp=" + ddlEmployee.SelectedValue + "');", true);
    }
}