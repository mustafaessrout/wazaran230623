using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstempentry2 : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //txempcd.Text = bll.vLookUp("select max(cast(emp_cd as int)) + 1 from tmst_employee where emp_cd not in ('BHA','KBR')");
            hdemp.Value = txempcd.Text;
            bll.vBindingFieldValueToCombo(ref cbblood, "blood_cd");
            bll.vBindingFieldValueToCombo(ref cbjobtitle, "job_title_cd");
            bll.vBindingFieldValueToCombo(ref cblevel, "level_cd");
            bll.vBindingComboToSp(ref cbdept, "sp_tmst_department_get", "dept_cd", "dept_nm");
            bll.vBindingFieldValueToCombo(ref cbonation, "nationality");
            bll.vBindingFieldValueToCombo(ref cbprofiqoma, "profession_iqoma");
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_getall", "salespointcd", "salespoint_nm");
            bll.vBindingFieldValueToCombo(ref cbmarried, "married_sta_id");
        }
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "popupwindow('lookup_emp.aspx');", true);
        return;
    }
    protected void btrefresh_Click(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        bll.vBindingFieldValueToCombo(ref cbblood, "blood_cd");
        bll.vBindingFieldValueToCombo(ref cbjobtitle, "job_title_cd");
        bll.vBindingFieldValueToCombo(ref cblevel, "level_cd");
        bll.vBindingComboToSp(ref cbdept, "sp_tmst_department_get", "dept_cd", "dept_nm");
        bll.vBindingFieldValueToCombo(ref cbonation, "nationality");
        bll.vBindingFieldValueToCombo(ref cbprofiqoma, "profession_iqoma");
        bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_getall", "salespointcd", "salespoint_nm");
        bll.vBindingFieldValueToCombo(ref cbmarried, "married_sta_id");
        arr.Add(new cArrayList("@emp_cd", hdemp.Value.ToString()));
        bll.vGetMstEmployee(ref rs, arr);
        while (rs.Read())
        {
            lbempcodeedu.Text = hdemp.Value.ToString();
            lbempnameedu.Text = rs["emp_nm"].ToString();
            txempname.Text = rs["emp_nm"].ToString();
            lbempcodedoc.Text = hdemp.Value.ToString();
            lbempnamedoc.Text = txempname.Text;
            
            dtbirth.Text =  rs["birth_dt"].ToString();
            txbirthplace.Text = rs["birth_place"].ToString();
            dtjoin.Text = rs["join_dt"].ToString();
            if (!string.IsNullOrEmpty(rs["blood_cd"].ToString()))
            {
                cbblood.SelectedValue = rs["blood_cd"].ToString();
                
            }
            else
            {
               
                cbblood.Items.Clear();
            }
            if (!string.IsNullOrEmpty(rs["nationality"].ToString()))
            {
                cbonation.SelectedValue = rs["nationality"].ToString();
                
            }
            else
            {
               
                cbonation.Items.Clear();
            }
            if (!string.IsNullOrEmpty(rs["job_title_cd"].ToString()))
            {
                cbjobtitle.SelectedValue = rs["job_title_cd"].ToString();
            }
            else { cbjobtitle.Items.Clear(); }
            if (!string.IsNullOrEmpty(rs["salespointcd"].ToString()))
            { cbsalespoint.SelectedValue = rs["salespointcd"].ToString(); }
            else { cbsalespoint.Items.Clear(); }

            if (!string.IsNullOrEmpty(rs["profession_iqoma"].ToString()))
            { cbprofiqoma.SelectedValue = rs["profession_iqoma"].ToString(); }
            else { cbprofiqoma.Items.Clear(); }
            if (!string.IsNullOrEmpty(rs["married_sta_id"].ToString()))
            {cbmarried.SelectedValue = rs["married_sta_id"].ToString();}else{cbmarried.Items.Clear();}
            if (!string.IsNullOrEmpty(rs["level_cd"].ToString()))
            { cblevel.SelectedValue = rs["level_cd"].ToString(); }
            else { cblevel.Items.Clear(); }

            if (!string.IsNullOrEmpty(rs["pict_nm"].ToString()))
            {
                img.ImageUrl = "~/images/EMP" + hdemp.Value.ToString() + ".jpg";
            }
            else
            { img.ImageUrl = "~/noimage.jpg"; }

            txemergency.Text = rs["emergency_contact"].ToString();
            txphoneno.Text = rs["phone_no"].ToString();
            txemerphone.Text = rs["emergency_phone"].ToString();
            txemergency.CssClass = "ro";
            txphoneno.CssClass = "ro";
            cbblood.CssClass = "ro";
            cbjobtitle.CssClass = "ro";
            txbirthplace.CssClass = "ro";
            dtbirth.CssClass = "ro";
            txempname.CssClass = "ro";
            cbonation.CssClass = "ro";
            cbsalespoint.CssClass = "ro";
            cbmarried.CssClass = "ro";
            txemerphone.CssClass = "ro";
            cbdept.CssClass = "ro";
            cbprofiqoma.CssClass = "ro";
            dtjoin.CssClass = "ro";
            cblevel.CssClass = "ro";
            
        } rs.Close();
        arr.Clear();
        arr.Add(new cArrayList("@emp_cd", hdemp.Value.ToString()));
        bll.vBindingGridToSp(ref grdedu, "sp_temployee_education_get", arr);
        btedit.Visible = true;
        btnew.Visible = true;
        btsave.Visible = false; upl.Visible = false;
    }

    void vMakeReadOnly()
    {

        txemergency.CssClass = "ro";
        txphoneno.CssClass = "ro";
        cbblood.CssClass = "ro";
        cbjobtitle.CssClass = "ro";
        txbirthplace.CssClass = "ro";
        dtbirth.CssClass = "ro";
        txempname.CssClass = "ro";
        cbonation.CssClass = "ro";
        cbsalespoint.CssClass = "ro";
        cbmarried.CssClass = "ro";
        txemerphone.CssClass = "ro";
        cbdept.CssClass = "ro";
        cbprofiqoma.CssClass = "ro";
        dtjoin.CssClass = "ro";
        cblevel.CssClass = "ro";
    }
    protected void btedit_Click(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        bll.vBindingFieldValueToCombo(ref cbblood, "blood_cd");
        bll.vBindingFieldValueToCombo(ref cbjobtitle, "job_title_cd");
        bll.vBindingFieldValueToCombo(ref cblevel, "level_cd");
        bll.vBindingComboToSp(ref cbdept, "sp_tmst_department_get", "dept_cd", "dept_nm");
        bll.vBindingFieldValueToCombo(ref cbonation, "nationality");
        bll.vBindingFieldValueToCombo(ref cbprofiqoma, "profession_iqoma");
        bll.vBindingFieldValueToCombo(ref cbmarried, "married_sta_id");
        bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_getall", "salespointcd", "salespoint_nm");
        arr.Add(new cArrayList("@emp_cd", hdemp.Value.ToString()));
        bll.vGetMstEmployee(ref rs, arr);
        while (rs.Read())
        {
            txempname.Text = rs["emp_nm"].ToString();
            dtbirth.Text = rs["birth_dt"].ToString();
            txbirthplace.Text = rs["birth_place"].ToString();
            if (!string.IsNullOrEmpty(rs["blood_cd"].ToString()))
            {
                cbblood.SelectedValue = rs["blood_cd"].ToString();

            }
           
            if (!string.IsNullOrEmpty(rs["nationality"].ToString()))
            {
                cbonation.SelectedValue = rs["nationality"].ToString();

            }
          
            if (!string.IsNullOrEmpty(rs["job_title_cd"].ToString()))
            {
                cbjobtitle.SelectedValue = rs["job_title_cd"].ToString();
            }
           
            if (!string.IsNullOrEmpty(rs["salespointcd"].ToString()))
            { cbsalespoint.SelectedValue = rs["salespointcd"].ToString(); }
            if (!string.IsNullOrEmpty(rs["profession_iqoma"].ToString()))
            { cbprofiqoma.SelectedValue = rs["profession_iqoma"].ToString(); }
            


            txemergency.Text = rs["emergency_contact"].ToString();
            txphoneno.Text = rs["phone_no"].ToString();
            txemergency.CssClass = "ro";
            txphoneno.CssClass = "ro";
            cbblood.CssClass = "ro";
            cbjobtitle.CssClass = "ro";
            txbirthplace.CssClass = "ro";
            dtbirth.CssClass = "ro";
            txempname.CssClass = "ro";
            cbonation.CssClass = "ro";
            cbsalespoint.CssClass = "ro";
            cbprofiqoma.CssClass = "ro";
        } rs.Close();
        txbirthplace.CssClass = "makeitreadwrite";
        dtbirth.CssClass = "makeitreadwrite";
        cbjobtitle.CssClass = "makeitreadwrite";
        cblevel.CssClass = "makeitreadwrite";
        cbblood.CssClass = "makeitreadwrite";
        cbdept.CssClass = "makeitreadwrite";
        cbsalespoint.CssClass = "maketireadwrite";
        cbonation.CssClass = "makeitreadwrite";
        txemergency.CssClass = "makeitreadwrite";
        txphoneno.CssClass = "makeitreadwrite";
        cbmarried.CssClass = "makeitreadwrite";
        cbprofiqoma.CssClass = "makeitreadwrite";
        txemerphone.CssClass = "makeitreadwrite";
        dtjoin.CssClass = "makeitreadwrite";
        btsave.Visible = true;
        upl.Visible = true;
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_mstempentry2.aspx");
    }
    protected void img_DataBinding(object sender, EventArgs e)
    {
       
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (txbirthplace.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Birth Place can not empty','Birth Place','warning');", true);
            return;
        }

        if (dtbirth.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Birth Date can not empty','Birth Date','warning');", true);
            return;
        }

        if (txempname.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('EmployeeName can not empty','Employee Name','warning');", true);
            return;
        }

        if (txemergency.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Emergency contact can not empty','Emergency Contact','warning');", true);
            return;
        }

        if (txphoneno.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Phone No can not empty','Phone No','warning');", true);
            return;
        }

        if (txemerphone.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Emergency Phone No can not empty','Emergency Phone No','warning');", true);
            return;
        }
        hdemp.Value = txempcd.Text;
        arr.Add(new cArrayList("@emp_cd", hdemp.Value));
        arr.Add(new cArrayList("@emp_nm", txempname.Text));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@blood_cd", cbblood.SelectedValue.ToString()));
        arr.Add(new cArrayList("@dept_cd", cbdept.SelectedValue.ToString()));
        arr.Add(new cArrayList("@level_cd", cblevel.SelectedValue.ToString()));
        arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
        arr.Add(new cArrayList("@nationality", cbonation.SelectedValue.ToString()));
        arr.Add(new cArrayList("@joint_dt",System.DateTime.ParseExact( dtjoin.Text,"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@birth_dt", System.DateTime.ParseExact( dtbirth.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@birth_place", txbirthplace.Text));
        arr.Add(new cArrayList("@married_sta_id", cbmarried.SelectedValue.ToString()));
        arr.Add(new cArrayList("@profession_iqoma", cbprofiqoma.SelectedValue.ToString()));
        arr.Add(new cArrayList("@phone_no", txphoneno.Text));
        arr.Add(new cArrayList("@emergency_contact", txemergency.Text));
        arr.Add(new cArrayList("@emergency_phone", txemerphone.Text));
        if (upl.FileName != "")
        {
            arr.Add(new cArrayList("@pict_nm", bll.sGetControlParameter("image_path") + "EMP" + hdemp.Value.ToString() + ".jpg"));
            upl.PostedFile.SaveAs(bll.sGetControlParameter("image_path") + "EMP" + hdemp.Value.ToString() + ".jpg");
            img.ImageUrl = "~/images/EMP" + hdemp.Value.ToString() + ".jpg";
        }
        bll.vUpdateTmstEmployeeAll(arr);
        btsave.Visible = false;
        btedit.Visible = true;
        btnew.Visible = true;
        upl.Visible = false;
        vMakeReadOnly();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data saved','Employee data','success');", true);
    }
    protected void btaddedu_Click(object sender, EventArgs e)
    {
        if (lbempcodeedu.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Employee not selected','Employee data','warning');", true);
            return;
        }


        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", lbempcodeedu.Text));
        arr.Add(new cArrayList("@sequenceno", txsequence.Text));
        arr.Add(new cArrayList("@school_nm", txschool.Text));
        arr.Add(new cArrayList("@degree", txdegree.Text));
        arr.Add(new cArrayList("@graduate_dt", dtgraduate.Text));
        arr.Add(new cArrayList("@major", txmajor.Text));
        arr.Add(new cArrayList("@gpa", txgpa.Text));
        bll.vInsertEmployeeEducation(arr);
        arr.Clear();
        arr.Add(new cArrayList("@emp_cd", lbempcodeedu.Text));
        bll.vBindingGridToSp(ref grdedu, "sp_temployee_education_get", arr);
        txgpa.Text = "";
        txmajor.Text = "";
        txschool.Text = "";
        txdegree.Text = "";
        dtgraduate.Text = "";
        txsequence.Text = "";
    }
    protected void grdedu_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbseq = (Label)grdedu.Rows[e.RowIndex].FindControl("lbseq");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", lbempcodeedu.Text));
        arr.Add(new cArrayList("@sequenceno", lbseq.Text));
        bll.vDelEmployeeEducation(arr);
        arr.Clear();
        arr.Add(new cArrayList("@emp_cd", lbempcodeedu.Text));
        bll.vBindingGridToSp(ref grdedu, "sp_temployee_education_get", arr);
    }
    protected void btadddoc_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
    }
}