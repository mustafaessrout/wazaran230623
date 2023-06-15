using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class master_fm_employee : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbgender, "gender");
            bll.vBindingFieldValueToCombo(ref cbjobtitle, "jobtitle_cd");
            bll.vBindingFieldValueToCombo(ref cbjoblevel, "joblevel_cd");
            bll.vBindingFieldValueToCombo(ref cbnationality, "nationality");
            bll.vBindingFieldValueToCombo(ref cbreligion, "religion_cd");
            bll.vBindingFieldValueToCombo(ref cbsponsor, "sponsor_cd", 1);
            bll.vBindingFieldValueToCombo(ref cbeducation, "edu_cd");
            bll.vBindingFieldValueToCombo(ref cbjobvisa, "jobtitle_visa");
            bll.vBindingFieldValueToCombo(ref cbempstatus, "emp_sta_id");
            btprint.CssClass = "btn btn-default ro";
            btedit.CssClass = "btn btn-default ro";
            btsave.CssClass = "btn btn-default ro";
        }
    }
    protected void btlookup_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@emp_cd", hdemp.Value.ToString()));
        bll.vGetHRDMstEmployee(arr, ref rs);
        while (rs.Read())
        {
            txempcode.Text = hdemp.Value.ToString(); txempcode.CssClass = "form-control ro";
            txfullname.Text = rs["fullname"].ToString();
            txfullname.CssClass = "form-control ro";
            dtbirth.Text = Convert.ToDateTime(rs["birth_dt"]).ToString("d/M/yyyy"); dtbirth.CssClass = "form-control ro";
            dtjoin.Text = Convert.ToDateTime(rs["join_dt"]).ToString("d/M/yyyy"); dtjoin.CssClass = "form-control ro";
            cbgender.SelectedValue = rs["gender"].ToString(); cbgender.CssClass = "form-control ro";
            cbjobtitle.SelectedValue = rs["jobtitle_cd"].ToString(); cbjobtitle.CssClass = "form-control ro";
            cbjoblevel.SelectedValue = rs["joblevel_cd"].ToString();cbjoblevel.CssClass = "form-control ro";
            if (rs["nationality"] != null)
            {
                cbnationality.SelectedValue = rs["nationality"].ToString(); cbnationality.CssClass = "form-control ro";
            }
            txiqoma.Text = rs["iqoma_no"].ToString(); txiqoma.CssClass = "form-control ro";
            txpassport.Text = rs["passport_no"].ToString(); txpassport.CssClass = "form-control ro";
            txmobileno.Text = rs["mobile_no"].ToString(); txmobileno.CssClass = "form-control ro";
            cbreligion.SelectedValue = rs["religion_cd"].ToString(); cbreligion.CssClass = "form-control ro";
            txemail.CssClass = "form-control ro";
            cbsponsor.SelectedValue = rs["sponsor_cd"].ToString(); cbsponsor.CssClass = "form-control ro";
            cbeducation.SelectedValue = rs["edu_cd"].ToString(); cbeducation.CssClass = "form-control ro";
            cbjobvisa.SelectedValue = rs["jobtitle_visa"].ToString();
            cbempstatus.SelectedValue = rs["emp_sta_id"].ToString(); cbempstatus.CssClass = "form-control ro";
            cbjobvisa_SelectedIndexChanged(sender, e);
            cbjobvisa.CssClass = "form-control ro";
            btsave.CssClass = "btn btn-default ro";
            btedit.CssClass = "btn btn-info";
            btprint.CssClass = "btn btn-danger";

        }
        rs.Close();
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_employee.aspx");
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        if (txempcode.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Emp Code can not be empty!','Check Emp Code');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();

        btsave.CssClass = "btn btn-default ro";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Data has been saved!','"+txfullname.Text+"');", true);
    }
    protected void btedit_Click(object sender, EventArgs e)
    {
        if (btedit.Text == "Edit")
        {
            cbeducation.CssClass = "form-control";
            cbjoblevel.CssClass = "form-control";
            cbjobtitle.CssClass = "form-control";
            cbnationality.CssClass = "form-control";
            cbsponsor.CssClass = "form-control";
            btsave.CssClass = "btn btn-info";
            btedit.Text = "Cancel";

        }
        else if ((btedit.Text == "Cancel"))
        {
            cbeducation.CssClass = "form-control ro";
            cbjoblevel.CssClass = "form-control ro";
            cbjobtitle.CssClass = "form-control ro";
            cbnationality.CssClass = "form-control ro";
            cbsponsor.CssClass = "form-control ro";
            btsave.CssClass = "btn btn-default ro";
            btedit.Text = "Edit";
        }
    }
    protected void cbjobvisa_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbvisaarabic.Text = bll.vLookUp("select fld_arabic from tfield_value where fld_nm='jobtitle_visa' and fld_valu='"+cbjobvisa.SelectedValue.ToString()+"'");
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('/fm_report2.aspx?src=employee&emp="+hdemp.Value.ToString()+"');", true);
    }
    protected void btprintall_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('/fm_report2.aspx?src=empall');", true);
    }
}