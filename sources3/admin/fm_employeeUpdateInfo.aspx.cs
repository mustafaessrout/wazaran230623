﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_employeeUpdateInfo : System.Web.UI.Page
{
    cbll bll = new cbll();
    Utitlity ut = new Utitlity();
    cdal cdl = new cdal();
    long unixTime = new long();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        DateTime foo = DateTime.UtcNow;
        unixTime = ((DateTimeOffset)foo).UtcTicks;


        if (!IsPostBack)
        {

            bll.vBindingComboToSp(ref cbAllBranch, "sp_tmst_salespointSN_get", "salespointcd", "salespoint_desc");
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
        //txtBranch.Text = bll.vLookUp("select salespointcd from tuser_profile where emp_Cd='" + HttpContext.Current.Request.Cookies["usr_id"].Value + "'");
        //txtBranchName.Text = bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd in ( select salespointcd from tuser_profile where emp_Cd='" + HttpContext.Current.Request.Cookies["usr_id"].Value + "')");
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
        //arr.Add(new cArrayList("@salespointcd", txtBranch.Text));
 

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
        //var wadiItem = cbsalespoint.Items[cbsalespoint.Items.Count - 2];
        //cbsalespoint.Items.Remove(wadiItem);
        //cbsalespoint.SelectedValue = txtBranch.Text;//  Request.Cookies["sp"].Value;
        //if (dt.Rows.Count > 0)
        //{
        //    ddlEducation.Items.Remove(ddlEducation.Items[1]);
        //}
        

        //if (Request.Cookies["sp"].Value == "0")
        //if (txtBranch.Text == "0")
        //{ cbsalespoint.Enabled = true; }
        //else { cbsalespoint.Enabled = false; }
    }
    private void bindControlWithOutEmployee()
    {
        //txtBranch.Text = bll.vLookUp("select salespointcd from tuser_profile where emp_Cd='" + HttpContext.Current.Request.Cookies["usr_id"].Value + "'");
        //txtBranchName.Text = bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd in ( select salespointcd from tuser_profile where emp_Cd='" + HttpContext.Current.Request.Cookies["usr_id"].Value + "')");
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
        //arr.Add(new cArrayList("@salespointcd", txtBranch.Text));
        //if (dt.Rows.Count > 0)
        //{
        //    dt = new DataTable();
        //}
        //dt = cdl.GetValueFromSP("Sp_tmst_employeeByAll_get", arr);
        //ddlEmployee.DataSource = dt;
        //ddlEmployee.DataValueField = "emp_cd";
        //ddlEmployee.DataTextField = "fullname";
        //ddlEmployee.DataBind();
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
        //var wadiItem = cbsalespoint.Items[cbsalespoint.Items.Count - 2];
        //cbsalespoint.Items.Remove(wadiItem);
        //cbsalespoint.SelectedValue = txtBranch.Text;//  Request.Cookies["sp"].Value;
        //if (dt.Rows.Count > 0)
        //{
        //    ddlEducation.Items.Remove(ddlEducation.Items[1]);
        //}


        //if (Request.Cookies["sp"].Value == "0")
        //if (txtBranch.Text == "0")
        //{ cbsalespoint.Enabled = true; }
        //else { cbsalespoint.Enabled = false; }
    }
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCotrole(ddlEmployee.SelectedValue);
    }
    public void BindCotrole(string emp_cd)
    {
        try
        {
            DataTable dt = new DataTable();
            List<cArrayList> arr = new List<cArrayList>();

            //arr.Add(new cArrayList("@salespointcd", txtBranch.Text));//HttpContext.Current.Request.Cookies["sp"].Value
            //arr.Add(new cArrayList("@emp_cd", Convert.ToString(ddlEmployee.SelectedValue)));


            arr.Clear();
            arr.Add(new cArrayList("@emp_cd", Convert.ToString(emp_cd)));
            //dt = cdl.GetValueFromSP("Sp_tacc_mst_Emp_getByID", arr);
            dt = cdl.GetValueFromSP("Sp_tacc_mst_EmpApp_getByID", arr);



            if (dt.Rows.Count > 0)
            {
                bindControlWithOutEmployee();
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

                hdfEmp_std.Value = Convert.ToString(dt.Rows[0]["emp_std_doc"]);
                hdfDrivingLicense.Value = Convert.ToString(dt.Rows[0]["license_doc"]);
                hdfInsurance.Value = Convert.ToString(dt.Rows[0]["insurance_Doc"]);
                hdfNationalAdd.Value = Convert.ToString(dt.Rows[0]["national_Doc"]); ;
                hdfPassport.Value = Convert.ToString(dt.Rows[0]["passport_Doc"]);
                hdfVehicle.Value = Convert.ToString(dt.Rows[0]["vehicleDoc"]);
                hdfIqama.Value = Convert.ToString(dt.Rows[0]["iqamaDoc"]);
                hdfEmployeePhoto.Value = Convert.ToString(dt.Rows[0]["employeeImage"]);



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
                hdfEmp_std.Value = Convert.ToString(dt.Rows[0]["employeeImage"]);
                hdfDrivingLicense.Value = Convert.ToString(dt.Rows[0]["license_doc"]);

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
                //txtBranch.Text = bll.vLookUp("select salespointcd from tuser_profile where emp_Cd='" + HttpContext.Current.Request.Cookies["usr_id"].Value + "'");
                //txtBranchName.Text = bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd in ( select salespointcd from tuser_profile where emp_Cd='" + HttpContext.Current.Request.Cookies["usr_id"].Value + "')");
            }
        }
        catch (Exception ex)
        {
            ut.Logs("", "HR", "Employee", "fr_mst_employee", "btnUpload_Click", "Exception", ex.Message + ex.InnerException);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('" + ex.Message + ex.InnerException + "','Error to save','warning');", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('File upload failed!','File upload failed');", true);

        }
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        // arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@emp_nm", prefixText));
        bll.vSearchMstEmployeeApp(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_cd"].ToString() + "-" + rs["emp_nm"].ToString(), rs["emp_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        //ddlEmployee.SelectedValue = hdEmployee.Value.ToString();
        BindCotrole(ddlEmployee.SelectedValue);
        
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            string gender = "M";
            if (cbGenderFemale.Checked)
                gender = "F";
            int nEmpCode;
            string employee_app_cd = string.Empty;
            
            employee_app_cd = bll.vLookUp("select employee_app_cd from tmst_employee_app where emp_cd='" + ddlEmployee.SelectedValue + "' and employee_app_sta='N'");
           
             if (txtIqama.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please enter mobile number','mobile number','warning');", true);
                return;
            }
            
           
            else if (txtName.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Enter Employee Name','Emp name','warning');", true);
                return;
            }
            else if (dtJoin.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Enter join date','Join date','warning');", true);
                return;
            }
            else if (dtDOB.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Enter birthdate','Birthdate','warning');", true);
                return;
            }
            else if (txtBirthPalce.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Enter birth place','birth place','warning');", true);
                return;
            }
            else if (txtIqama.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Enter Iqama','Iqama','warning');", true);
                return;
            }
            else if (dtIqamaExipry.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Enter Iqama expiry','Iqama expiry','warning');", true);
                return;
            }
            else if (txtPassport.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Enter passport number','passport number','warning');", true);
                return;
            }
            else if (dtPassportExipry.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Enter passport expiry','passport expiry','warning');", true);
                return;
            }
            else if (txtSponsor.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Enter sponsor info','sponsor info','warning');", true);
                return;
            }
            else if (txtMobile.Text.Length < 10)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Mobile must 10 degit','mobile must','warning');", true);
                return;
            }

            // Here we upload all files
            //1:-Photo
            //2:-Upload Iqama
            //3:-Upload Passport
            //4:-Upload Insur. Info
            //5:-Upload Add. Info
            //6:-Upload Vehicle Info
            //7:-Employee status

            string emp_photo = string.Empty;
            string iqama_photo = string.Empty;
            string passport_photo = string.Empty;
            string insurance_photo = string.Empty;
            string nationalAdd_photo = string.Empty;
            string vehicle_photo = string.Empty;
            string emp_status_photo = string.Empty;
            string license_doc = string.Empty;


            List<cArrayList> arr = new List<cArrayList>();


            arr.Add(new cArrayList("@emp_cd", Convert.ToString(ddlEmployee.SelectedValue)));
            arr.Add(new cArrayList("@emp_nm", Convert.ToString(txtName.Text)));
            arr.Add(new cArrayList("@emp_nm_arabic", Convert.ToString(txtNameArabic.Text)));
            arr.Add(new cArrayList("@emp_photo", Convert.ToString(emp_photo)));
            arr.Add(new cArrayList("@emp_sn", Convert.ToString(txtSortName.Text)));
            arr.Add(new cArrayList("@join_dt", System.DateTime.ParseExact(dtJoin.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@birth_dt", System.DateTime.ParseExact(dtDOB.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));

            if (cb_emp_sta_decative.Checked == true)
            {
                arr.Add(new cArrayList("@salespointcd_transafer", Convert.ToString(cbsalespointTransfer.SelectedValue)));
            }
            else
            {
                arr.Add(new cArrayList("@salespointcd_transafer", null));
            }
            arr.Add(new cArrayList("@salespointcd", Convert.ToString(cbsalespoint.SelectedValue)));
            arr.Add(new cArrayList("@gender", Convert.ToString(gender)));
            arr.Add(new cArrayList("@nationality", Convert.ToString(ddlNationality.SelectedValue)));
            arr.Add(new cArrayList("@religion", Convert.ToString(ddlReligion.SelectedValue)));
            arr.Add(new cArrayList("@blood_cd", Convert.ToString(ddlBloodGroup.SelectedValue)));
            arr.Add(new cArrayList("@married_sta_id", Convert.ToString(ddlMarriedStatus.SelectedValue)));
            arr.Add(new cArrayList("@education", Convert.ToString(ddlEducation.SelectedValue)));
            arr.Add(new cArrayList("@mobile_no", Convert.ToString(txtMobile.Text)));

            if (cb_emp_sta_active.Checked == true)
            {
                arr.Add(new cArrayList("@emp_std", true));
                arr.Add(new cArrayList("@sub_emp_std", null));
            }
            else if (cb_emp_sta_decative.Checked == true)
            {
                arr.Add(new cArrayList("@emp_std", false));
                arr.Add(new cArrayList("@sub_emp_std", Convert.ToString(cbemp_std.SelectedValue)));
            }

            arr.Add(new cArrayList("@emp_std_Dt", System.DateTime.ParseExact(emp_std_Dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@emp_sta_photo", Convert.ToString(emp_status_photo)));
            arr.Add(new cArrayList("@iqama", Convert.ToString(txtIqama.Text)));
            arr.Add(new cArrayList("@iqama_exipry_dt", System.DateTime.ParseExact(dtIqamaExipry.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@iqama_photo", Convert.ToString(iqama_photo)));
            arr.Add(new cArrayList("@passport", Convert.ToString(txtPassport.Text)));
            arr.Add(new cArrayList("@passport_exipry_dt", System.DateTime.ParseExact(dtPassportExipry.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@passport_photo", Convert.ToString(passport_photo)));
            arr.Add(new cArrayList("@insurance", Convert.ToString(txtInsuranceNumber.Text)));
            arr.Add(new cArrayList("@insurance_exipry", System.DateTime.ParseExact(dtInsuranceExipry.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@insurance_photo", Convert.ToString(insurance_photo)));
            arr.Add(new cArrayList("@national_address", Convert.ToDecimal(txtNationalAddress.Text)));
            arr.Add(new cArrayList("@national_address_photo", Convert.ToString(nationalAdd_photo)));
            arr.Add(new cArrayList("@comp_id", Convert.ToString("SBTC")));
            arr.Add(new cArrayList("@birth_place", Convert.ToString(txtBirthPalce.Text)));
            arr.Add(new cArrayList("@old_job_title_cd", Convert.ToString(hdfOldJobtTitle.Value)));
            arr.Add(new cArrayList("@job_title_cd", Convert.ToString(ddlJobTitle.SelectedValue)));
            arr.Add(new cArrayList("@old_dept_cd", Convert.ToString(hdfOldDep.Value)));
            arr.Add(new cArrayList("@dept_cd", Convert.ToString(ddlDepartment.SelectedValue)));
            arr.Add(new cArrayList("@old_level_cd", Convert.ToString(hdfOldJobLevel.Value)));
            arr.Add(new cArrayList("@level_cd", Convert.ToString(ddlJobLevel.SelectedValue)));


            arr.Add(new cArrayList("@driving_license", Convert.ToString(txtDrivinglicenseNo.Text)));

            arr.Add(new cArrayList("@license_typ", Convert.ToString(ddlLicenseType.SelectedValue)));
            if (txtLicenseExipry.Text == "") { arr.Add(new cArrayList("@license_exp_dt", null)); }
            else { arr.Add(new cArrayList("@license_exp_dt", System.DateTime.ParseExact(txtLicenseExipry.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture))); }

            arr.Add(new cArrayList("@license_doc", license_doc));

            arr.Add(new cArrayList("@car_type_arabic", Convert.ToString(txtCarTypeEnglish.Text)));
            arr.Add(new cArrayList("@car_type_english", Convert.ToString(txtCarTypeEnglish.Text)));
            if (txtVehicleSponsor.Text == "")
            {
                arr.Add(new cArrayList("@vehicle_owner_id", null));
                arr.Add(new cArrayList("@vehicle_series_haikal", null));
                arr.Add(new cArrayList("@vehicle_model_year", null));
                arr.Add(new cArrayList("@vehicle_exp_dt", null));
                arr.Add(new cArrayList("@vehicle_color", null));
                arr.Add(new cArrayList("@vehicleArabic_no1", null));
                arr.Add(new cArrayList("@vehicleArabic_no2", null));
                arr.Add(new cArrayList("@vehicleArabic_no3", null));
                arr.Add(new cArrayList("@vehicleArabic_no4", null));
                arr.Add(new cArrayList("@vehicle_no1", null));
                arr.Add(new cArrayList("@vehicle_no2", null));
                arr.Add(new cArrayList("@vehicle_no3", null));
                arr.Add(new cArrayList("@vehicle_no4", null));
                arr.Add(new cArrayList("@vehicle_photo", null));
            }
            else
            {
                arr.Add(new cArrayList("@vehicle_owner_id", Convert.ToString(txtVehicleSponsor.Text)));
                arr.Add(new cArrayList("@vehicle_series_haikal", Convert.ToString(txtVIN.Text)));
                arr.Add(new cArrayList("@vehicle_model_year", Convert.ToString(txtModelYear.Text)));
                if (txtCarExpiry.Text == "") { arr.Add(new cArrayList("@vehicle_exp_dt", null)); }
                else { arr.Add(new cArrayList("@vehicle_exp_dt", System.DateTime.ParseExact(txtCarExpiry.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture))); }
                arr.Add(new cArrayList("@vehicle_color", Convert.ToString(txtCarColor.Text)));
                arr.Add(new cArrayList("@vehicleArabic_no1", Convert.ToString(txtPlateArabic1.Text)));
                arr.Add(new cArrayList("@vehicleArabic_no2", Convert.ToString(txtPlateArabic2.Text)));
                arr.Add(new cArrayList("@vehicleArabic_no3", Convert.ToString(txtPlateArabic3.Text)));
                arr.Add(new cArrayList("@vehicleArabic_no4", Convert.ToString(txtPlateArabic4.Text)));
                arr.Add(new cArrayList("@vehicle_no1", Convert.ToString(txtPlate1.Text)));
                arr.Add(new cArrayList("@vehicle_no2", Convert.ToString(txtPlate2.Text)));
                arr.Add(new cArrayList("@vehicle_no3", Convert.ToString(txtPlate3.Text)));
                arr.Add(new cArrayList("@vehicle_no4", Convert.ToString(txtPlate4.Text)));
                arr.Add(new cArrayList("@vehicle_photo", Convert.ToString(vehicle_photo)));
            }






            arr.Add(new cArrayList("@employee_app_sta", Convert.ToString("N")));
            arr.Add(new cArrayList("@createdBy", Convert.ToString(Request.Cookies["usr_id"].Value)));
            arr.Add(new cArrayList("@employee_app_cd", employee_app_cd));

            //arr.Add(new cArrayList("@employee_app_cd", Convert.ToString(ddlEmployee.SelectedValue)));

            bll.vUpdate_tmst_employee_appByHO(arr);

            int nrnd;
            Random rnd = new Random();
            nrnd = rnd.Next(1000, 9999);
            int nRnd = rnd.Next(1000, 9999);
            string sSubject = "Update Employee Information Request"; string sMessage = " employee :" + ddlEmployee.SelectedItem.Text + " has been created ";
            string stitle = bll.vLookUp("select fld_desc from tfield_value where fld_nm='job_title_cd' and fld_valu='" + ddlJobTitle.SelectedValue + "'");
            string spic_nm = bll.vLookUp("select emp_cd+'-'+emp_nm from tmst_employee where  emp_cd='0281'");
            string sto = bll.vLookUp("select email from tuser_profile where emp_cd='0281'");
            string mobile = bll.vLookUp("select mobile_no from tuser_profile where emp_cd='0281'");
            string screator_nm = bll.vLookUp("select emp_cd+'-'+emp_nm from tmst_employee where  emp_cd='" + Request.Cookies["usr_id"].Value.ToString() + "'");
            string ssalespoint = bll.vLookUp("select salespointcd +'-'+salespoint_nm from tmst_salespoint where salespointcd=" + cbsalespoint.SelectedValue);
            sSubject = "Updated Employee infromation for Branch " + cbsalespoint.SelectedItem.Text + " , employee :" + ddlEmployee.SelectedItem.Text + " has been created ";
            //sMessage = "Request No. " + sCasregNo + " created by " + lbemp_nm.Text + ", with the item " + lbitemcode.Text + 
            sMessage = "<table><tr><td colspan=3>#Employee infromation update Approval</td></tr><tr><td></td></tr><tr><td>Salespoint</td><td>:</td><td>" + ssalespoint + "</td></tr><tr><td>Request No</td><td>:</td><td>" + employee_app_cd + "</td><td>Employee</td><td>:</td><td>" + ddlEmployee.SelectedItem.Text + "</td></tr><tr><td>Created</td><td>:</td><td>" + screator_nm + "</td><td>Position </td><td>:</td><td>" + stitle + "</td></tr><tr><td>Trans Type</td><td>:</td><td>" + "Update employee info" + "</td><td></td><td></td><td>" + "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr></table>" +
            //"<p> Please Click this  for approved : <a href='" + Request.Url.GetLeftPart(UriPartial.Authority) + "/landingpageCasregout.aspx?appcode=" + nRnd.ToString() + "&sta=A'>Approve</a>, or for rejected please click <a href='" + Request.Url.GetLeftPart(UriPartial.Authority) + "/landingpageCasregout.aspx?appcode=" + nRnd.ToString() + "&sta=R'>Reject</a></p>" + 
            "<p> Please Click this  for approved : " + "http://172.16.1.26:8089/" + "</p>" +
            " \n\r\n\r\n\r\n Wazaran Admin";
            //bll.vSendMail(sto, sSubject, sMessage, sfile_attachment);
            arr.Clear();
            arr.Add(new cArrayList("@trxcd", "Employee Information Update"));
            arr.Add(new cArrayList("@token", nRnd.ToString()));
            arr.Add(new cArrayList("@doc_no", employee_app_cd));
            bll.vInsertEmailSent(arr);
            arr.Clear();
            arr.Add(new cArrayList("@token", nRnd.ToString()));
            arr.Add(new cArrayList("@doc_typ", "Employee Information Update"));
            arr.Add(new cArrayList("@to", sto));
            arr.Add(new cArrayList("@doc_no", employee_app_cd));
            arr.Add(new cArrayList("@emailsubject", sSubject));
            arr.Add(new cArrayList("@msg", sMessage));
            arr.Add(new cArrayList("@file_attachment", null));
            bll.vInsertEmailOutbox(arr);
            arr.Clear();
            arr.Add(new cArrayList("@salespoint", cbAllBranch.SelectedItem.Text));
            arr.Add(new cArrayList("@smsto", mobile));
            arr.Add(new cArrayList("@smsmsg", sSubject));
            arr.Add(new cArrayList("@doc_no", employee_app_cd));
            arr.Add(new cArrayList("@token", nRnd.ToString()));
            arr.Add(new cArrayList("@newtoken", "0"));

            arr.Add(new cArrayList("@doc_typ", "employee info update"));


            bll.vInsertSmsOutbox(arr);
            bindControl();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Data Updated Successfully. " + employee_app_cd + "','Data Updated Successfully.','success');", true);

        }
        catch (Exception ex)
        {
            ut.Logs("", "HR", "Employee", "fr_mst_employee_update", "btnSave_Click", "Exception", ex.Message + ex.InnerException);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('" + ex.Message + ex.InnerException + "','Error to save','warning');", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('File upload failed!','File upload failed');", true);

        }
    }

    protected void cbAllBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbAllBranch.SelectedValue));
        dt = cdl.GetValueFromSP("Sp_tmst_employeeByAll_get", arr);
        ddlEmployee.DataSource = dt;
        ddlEmployee.DataValueField = "emp_cd";
        ddlEmployee.DataTextField = "fullname";
        ddlEmployee.DataBind();

        ddlEmployee_SelectedIndexChanged(sender, e);
    }
    protected void btprintEmp_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('../fm_report2.aspx?src=employeeBranchApp&emp=" + ddlEmployee.SelectedValue + "');", true);
    }

    protected void btReject_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@employee_app_cd", ddlEmployee.SelectedValue));
        arr.Add(new cArrayList("@employee_app_sta", "R"));
        arr.Add(new cArrayList("@updatedby", HttpContext.Current.Request.Cookies["usr_id"].Value));
        arr.Add(new cArrayList("@rejectInfo", ""));
        arr.Add(new cArrayList("@updatMethod", "App"));
        bll.vUpdateMstEmployeeApp(arr);
        //dal.vExecuteSP("sp_tmst_employee_app_updt", arr, sSP, susersql);

        //arr.Add(new cArrayList("@cust_nm", hdEmployee.Value));
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Employee has been updated successfully!','" + ddlEmployee.SelectedItem.Text + "','success');", true);
    }
}