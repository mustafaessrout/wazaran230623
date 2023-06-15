using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mst_employeeDependent : System.Web.UI.Page
{
    cbll bll = new cbll();
    Utitlity ut = new Utitlity();
    cdal cdl = new cdal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            bindControl();
            if (Request.QueryString["emp_cd"] != null) {
                hdEmp.Value = Convert.ToString(Request.QueryString["emp_cd"]);
                BindGrid();
            }
            dtDOB.Text = System.DateTime.Today.ToString("d/M/yyyy");
            dtInsuranceExipry.Text = System.DateTime.Today.ToString("d/M/yyyy");
            dtIqamaExipry.Text = System.DateTime.Today.ToString("d/M/yyyy");
            dtPassportExipry.Text = System.DateTime.Today.ToString("d/M/yyyy");
            Session["fileName"] = null;
            Session["fileSize"] = null;
            lbfileloc.Text = string.Empty;
            //btsave.Enabled = false;
            //btprint.Enabled = false;
            imgEmployeePhoto.Src = "statics/img/dummy_prof.jpg";
        }
    }

    private void bindControl()
    {
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();


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
        arr.Add(new cArrayList("@fld_nm", "depdent_relation_id"));
        dt = cdl.GetValueFromSP("sp_tfield_value_get", arr);
        ddlRelationShip.DataSource = dt;
        ddlRelationShip.DataValueField = "fld_valu";
        ddlRelationShip.DataTextField = "fld_desc";
        ddlRelationShip.DataBind();

        BindGrid();


    }

    private void BindGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", Convert.ToString(hdEmp.Value)));
        bll.vBindingGridToSp(ref grd, "Sp_tacc_mst_EmpDependent_get", arr);
    }
    protected void btlookup_Click(object sender, EventArgs e)
    {

        DataTable dt = new DataTable();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@Empno", Convert.ToString(txtEmpCode.Text)));
        dt = cdl.GetValueFromSP("Sp_tacc_mst_Emp_getByID", arr);

        if (dt.Rows.Count > 0)
        {
            bindControl();
            Session["fileName"] = Convert.ToString(dt.Rows[0]["fileName"]);
            btsave.Text = "Update";
            BindGrid();
        }
    }

    private void ClearControl()
    {
        bindControl();
        txtEmpCode.Text = string.Empty;
        txtNameArabic.Text = string.Empty;
        txtEmpCode.Text = string.Empty;
        txtIqama.Text = string.Empty;
        txtMobile.Text = string.Empty;
        txtName.Text = string.Empty;
        txtPassport.Text = string.Empty;
        txtSponsor.Text = string.Empty;
        txtSortName.Text = string.Empty;
        txtBirthPalce.Text = string.Empty;
        dtDOB.Text = System.DateTime.Today.ToString("d/M/yyyy");

        btsave.Text = "Save";
        Session["fileName"] = null;
        Session["fileSize"] = null;
        lbfileloc.Text = string.Empty;
        imgEmployeePhoto.Src = "statics/img/dummy_prof.jpg";
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_mst_employeeDependent.aspx");
    }
    protected void btnShowEmployeDependent_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex) { }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            string gender = "M";
            if (cbGenderFemale.Checked)
                gender = "F";
            int nEmpCode;

            List<cArrayList> arr = new List<cArrayList>();
            if (!int.TryParse(hdEmp.Value, out nEmpCode))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Emp code must numeric','Emp code','warning');", true);
                return;
            }
            else if (hdEmp.Value.Length > 4)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Emp code must 4 degit','Emp code','warning');", true);
                return;
            }
            else if (txtName.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Enter Employee Name','Emp name','warning');", true);
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Emp mobile must 10 degit','mobile must','warning');", true);
                return;
            }
            arr.Add(new cArrayList("@emp_cd", Convert.ToString(hdEmp.Value)));
            arr.Add(new cArrayList("@fullname", Convert.ToString(txtName.Text)));
            arr.Add(new cArrayList("@SortName", Convert.ToString(txtSortName.Text)));
            arr.Add(new cArrayList("@birth_dt", System.DateTime.ParseExact(dtDOB.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@gender", gender));
            arr.Add(new cArrayList("@nationality", Convert.ToString(ddlNationality.SelectedValue)));
            arr.Add(new cArrayList("@insurance_no", Convert.ToString(txtInsuranceNumber.Text)));
            arr.Add(new cArrayList("@insuranceExipiry", System.DateTime.ParseExact(dtInsuranceExipry.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@iqoma_no", Convert.ToString(txtIqama.Text)));
            arr.Add(new cArrayList("@IqamaExpiry", System.DateTime.ParseExact(dtIqamaExipry.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@passport_no", Convert.ToString(txtPassport.Text)));
            arr.Add(new cArrayList("@PassportExpiry", System.DateTime.ParseExact(dtPassportExipry.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@sponsor_cd", Convert.ToString(txtSponsor.Text)));
            arr.Add(new cArrayList("@religion_cd", Convert.ToString(ddlReligion.SelectedValue)));
            arr.Add(new cArrayList("@mobile_no", Convert.ToString(txtMobile.Text)));
            arr.Add(new cArrayList("@edu_cd", Convert.ToString(ddlEducation.SelectedValue)));
            arr.Add(new cArrayList("@fullnameArabic", Convert.ToString(txtNameArabic.Text)));
            arr.Add(new cArrayList("@blood_cd", Convert.ToString(ddlBloodGroup.SelectedValue)));
            arr.Add(new cArrayList("@birth_place", Convert.ToString(txtBirthPalce.Text)));
            arr.Add(new cArrayList("@OrderBy", Convert.ToString(ddlOrderBy.SelectedValue)));
            arr.Add(new cArrayList("@relationship", Convert.ToString(ddlRelationShip.SelectedValue)));
            string Empno = string.Empty;


            if (btsave.Text == "Save")
            {


                arr.Add(new cArrayList("@employeeDependentImage", Convert.ToString(Session["fileName"])));
                bll.vInsMastEmpDependent(arr, ref Empno);
                //btsave.Enabled = true;
                //btprint.Enabled = true;
                if (Empno == "-2")
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not insert duplicate ','Can not insert duplicate','warning');", true);

                }
                else
                {
                    Session["fileName"] = null;
                    txtEmpCode.Text = Empno;
                    UploadFiles(Convert.ToString(txtEmpCode.Text));
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Record Inserted successfully ','Insert Success','success');", true);
                    ClearControl();
                }
            }
            else if (btsave.Text == "Update")
            {
                //arr.Add(new cArrayList("@Empno", Convert.ToString(txtEmpCode.Text)));
                if (Session["fileSize"] == null || Session["fileSize"] == "")
                {
                    arr.Add(new cArrayList("@employeeDependentImage", Convert.ToString(Session["fileName"])));
                }
                else
                {
                    arr.Add(new cArrayList("@employeeDependentImage", Convert.ToString(txtEmpCode.Text) + "-" + Convert.ToString(Session["fileName"])));
                }
                bll.vUpdateMastEmpDependent(arr, ref Empno);
                //btsave.Enabled = true;
                //btprint.Enabled = true;
                UploadFiles(Convert.ToString(txtEmpCode.Text));
                Session["fileName"] = null;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Record Updated successfully ','Insert Success','success');", true);
                ClearControl();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('File upload failed!','File upload failed');", true);
            ut.Logs("", "HR", "Employee", "fr_mst_employee", "btnUpload_Click", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    private void UploadFiles(string extendendName)
    {
        if (Session["fileSize"] == null || Session["fileSize"] == "")
        { }
        else
        {

            byte[] fs = (byte[])Session["fileSize"];
            File.WriteAllBytes(bll.sGetControlParameter("image_path") + "/HR/employeeDependent/" + extendendName + "-" + Convert.ToString(ddlOrderBy.SelectedValue) +"-" + lbfileloc.Text, fs);


            Session["fileSize"] = null;
            Session["fileName"] = null;
            lbfileloc.Text = string.Empty;
        }
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lblEmpno = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblEmpno");
        Label lblOrderBy = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblOrderBy");
        DataTable dt = new DataTable();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", Convert.ToString(lblEmpno.Text)));
        arr.Add(new cArrayList("@OrderBy", Convert.ToString(lblOrderBy.Text)));
        dt = cdl.GetValueFromSP("Sp_tacc_mst_EmpDependent_getByID", arr);

        if (dt.Rows.Count > 0)
        {
            bindControl();

            DataTable dtJobTitle = new DataTable();
            DataTable dtDepartment = new DataTable();
            DataTable dtLevel = new DataTable();
            DataTable dtMarriedStatus = new DataTable();
            DataTable dtSalespointcd = new DataTable();
            DataTable dtBloodGroup = new DataTable();
            string gender = string.Empty;

            arr.Clear();
            arr.Add(new cArrayList("@fld_nm", "job_title_cd"));
            dtJobTitle = cdl.GetValueFromSP("sp_tfield_value_get", arr);
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
            gender = Convert.ToString(dt.Rows[0]["gender"]);
            if (gender == "M") { cbGenderMale.Checked = true; } else { cbGenderFemale.Checked = true; }
            hdEmp.Value = Convert.ToString(dt.Rows[0]["emp_cd"]);
            txtEmpCode.Text = Convert.ToString(dt.Rows[0]["emp_cd"]);
            btsave.Text = "Update";
            Session["fileName"] = Convert.ToString(dt.Rows[0]["employeeDependentImage"]);
            lbfileloc.Text = Convert.ToString(dt.Rows[0]["employeeDependentImage"]);
            lblEmpno.Text = Convert.ToString(dt.Rows[0]["emp_cd"]);
            txtEmpCode.Text = Convert.ToString(dt.Rows[0]["emp_cd"]);
            txtIqama.Text = Convert.ToString(dt.Rows[0]["iqoma_no"]);
            txtInsuranceNumber.Text = Convert.ToString(dt.Rows[0]["insurance_no"]);
            txtMobile.Text = Convert.ToString(dt.Rows[0]["mobile_no"]);
            txtName.Text = Convert.ToString(dt.Rows[0]["fullname"]);
            txtNameArabic.Text = Convert.ToString(dt.Rows[0]["fullnameArabic"]);
            txtPassport.Text = Convert.ToString(dt.Rows[0]["passport_no"]);
            txtSponsor.Text = Convert.ToString(dt.Rows[0]["sponsor_cd"]);
            ddlEducation.SelectedValue = Convert.ToString(dt.Rows[0]["edu_cd"]);
            dtDOB.Text = Convert.ToDateTime(dt.Rows[0]["birth_dt"]).ToString("d/M/yyyy");
            dtInsuranceExipry.Text = Convert.ToDateTime(dt.Rows[0]["insuranceExipiry"]).ToString("d/M/yyyy");
            dtIqamaExipry.Text = Convert.ToDateTime(dt.Rows[0]["iqomaExipry"]).ToString("d/M/yyyy");
            dtPassportExipry.Text = Convert.ToDateTime(dt.Rows[0]["passportExipry"]).ToString("d/M/yyyy");
            ddlOrderBy.SelectedValue = Convert.ToString(dt.Rows[0]["OrderBy"]);
            ddlRelationShip.SelectedValue = Convert.ToString(dt.Rows[0]["relationship"]);
            ddlReligion.SelectedValue = Convert.ToString(dt.Rows[0]["religion_cd"]);
            txtSortName.Text = Convert.ToString(dt.Rows[0]["SortName"]);

            if (Convert.ToString(dt.Rows[0]["passportExipry"]) == "")
            {
                dtPassportExipry.Text = System.DateTime.Today.ToString("d/M/yyyy");
            }
            else { dtPassportExipry.Text = Convert.ToDateTime(dt.Rows[0]["passportExipry"]).ToString("d/M/yyyy"); }

            if (Convert.ToString(dt.Rows[0]["iqomaExipry"]) == "")
            {
                dtIqamaExipry.Text = System.DateTime.Today.ToString("d/M/yyyy");
            }
            else { dtIqamaExipry.Text = Convert.ToDateTime(dt.Rows[0]["iqomaExipry"]).ToString("d/M/yyyy"); }

            if (Convert.ToString(dt.Rows[0]["iqomaExipry"]) == "")
            {
                dtInsuranceExipry.Text = System.DateTime.Today.ToString("d/M/yyyy");
            }
            else { dtInsuranceExipry.Text = Convert.ToDateTime(dt.Rows[0]["iqomaExipry"]).ToString("d/M/yyyy"); }


            imgEmployeePhoto.Src = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/images/HR/employeeDependent/" + dt.Rows[0]["employeeDependentImage"];// "statics/img/dummy_prof.jpg";


            var resultsLevel = from myRow in dtLevel.AsEnumerable()
                               where myRow.Field<string>("fld_valu") == Convert.ToString(dt.Rows[0]["joblevelUpdate_cd"])
                               select myRow;

            hdfOldFileName.Value = Convert.ToString(dt.Rows[0]["employeeDependentImage"]);
            //////BindGrid();

            txtSortName.Text = Convert.ToString(dt.Rows[0]["emp_sn"]);
            txtBirthPalce.Text = Convert.ToString(dt.Rows[0]["birth_place"]);


            var resultsBloodGroup = from myRow in dtBloodGroup.AsEnumerable()
                                    where myRow.Field<string>("fld_valu") == Convert.ToString(dt.Rows[0]["blood_cd"])
                                    select myRow;

            if (resultsBloodGroup.ToList().Count > 0)
            {
                ddlBloodGroup.SelectedValue = Convert.ToString(dt.Rows[0]["blood_cd"]);
            }


        }
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList2(string prefixText, int count, string contextKey)
    {
        HttpCookie cook;
        HttpCookie cookuser;
        cookuser = HttpContext.Current.Request.Cookies["usr_id"];
        cook = HttpContext.Current.Request.Cookies["sp"];
        cbll bll = new cbll();
        List<string> lCust = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sCust = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;

        arr.Add(new cArrayList("@emp_cd", prefixText));
        bll.vGetHRDMstEmployeeData(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_cd"].ToString() + "-" + rs["fullname"].ToString(), rs["emp_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }
    protected void btDelete_Click(object sender, EventArgs e)
    {
        if (txtEmpCode.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please select record first. ','select record','warning');", true);
        }
        else
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@Empno", Convert.ToString(txtEmpCode.Text)));
            bll.vDeleteMastEmpDependent(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Record Deleted successfully ','Deleted Success','success');", true);
            ClearControl();
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('/fm_report2.aspx?src=mst_Emp');", true);
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["fileName"] == null || Session["fileName"] == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Please upload files!','Check file');", true);
            }
            else
            {
                DirectoryInfo dir = new DirectoryInfo(bll.sGetControlParameter("image_path") + "/HR/employeeDependent/");

                string fileName = Convert.ToString(Session["fileName"]);
                string fileLocation = Convert.ToString(Session["fileLocation"]);
                lbfileloc.Text = fileName;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('File upload failed!','File upload failed');", true);
            ut.Logs("", "HR", "Employee", "fr_mst_employee", "btnUpload_Click", "Exception", ex.Message + ex.InnerException);
        }
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
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        //BindGrid();
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        //BindGrid();
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
}