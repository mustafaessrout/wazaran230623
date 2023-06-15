using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_rpsgroup : System.Web.UI.Page
{
    cbll bll = new cbll();
    cbll2 bll2 = new cbll2();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            bll.vBindingComboToSpWithEmptyChoosen(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_nm", arr   );
            bll.vBindingFieldValueToComboWithChoosen(ref cbgroup, "grouprps_cd");
            
            Session["trps_group"] = new List<trps_group>();
            cd.v_disablecontrol(cbday);
            cd.v_disablecontrol(cbcity);
            cd.v_disablecontrol(cbdistrict);
            cd.v_hiddencontrol(btsave);
            cd.v_hiddencontrol(btprint);
            //cd.v_disablecontrol(cbgroup);
        }
    }

    protected void cbgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        bll.vBindingFieldValueToComboWithChoosen(ref cbday, "day_cd");
        cd.v_enablecontrol(cbday);
        arr.Clear();
        arr.Add(new cArrayList("@grouprps_cd", cbgroup.SelectedValue));
        //arr.Add(new cArrayList("@day_cd", cbday.SelectedValue));
        arr.Add(new cArrayList("@emp_cd", cbsalesman.SelectedValue));
        List<trps_group> _trps_group = bll2.lGetRpsGroup(arr);
        grd.DataSource = _trps_group;
        grd.DataBind();
        Session["trps_group"] = (List<trps_group>)Session["trps_group"];
    }

    protected void cbday_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<trps_group> _trps_group = (List<trps_group>)Session["trps_group"];
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@loc_typ", "CIT"));
        bll.vBindingComboToSpWithEmptyChoosen(ref cbcity, "sp_tmst_location_getbytype", "loc_cd", "loc_nm", arr);
        
        //arr.Clear();
        //arr.Add(new cArrayList("@grouprps_cd", cbgroup.SelectedValue)); 
        //arr.Add(new cArrayList("@day_cd", cbday.SelectedValue));
        //arr.Add(new cArrayList("@emp_cd", cbsalesman.SelectedValue));   
        //List<trps_group> _trps_group = bll2.lGetRpsGroup(arr);
        //grd.DataSource = _trps_group;
        //grd.DataBind();
        cd.v_enablecontrol(cbcity);
        Session["trps_group"] = _trps_group;
    }

    protected void btadd_Click(object sender, EventArgs e)
    {
        if (cbgroup.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                "sweetAlert('Please select group !','Group','warning');", true);
            return;
        }
        if (cbday.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                "sweetAlert('Please select day to visited !','Day Visited','warning');", true);
            return;
        }
        if (cbcity.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                "sweetAlert('Please select city !','City','warning');", true);
            return;
        }
        if (cbdistrict.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
                "sweetAlert('Please select district !','District','warning');", true);
            return;
        }
        List<trps_group> _trps_group = (List<trps_group>)Session["trps_group"];
        foreach(trps_group _t in _trps_group)
        {
            if (_t.district_cd == cbdistrict.SelectedValue)
            {
                Session["trps_group"] = _trps_group;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                    "sweetAlert('Le district existe déjà',' veuillez sélectionner un autre district','warning');", true);
                return;
            }
        }
        _trps_group.Add(new trps_group
        {
            day_cd = Convert.ToInt32(cbday.SelectedValue),
            day_nm = bll.vLookUp("select fld_desc from tfield_value where fld_nm='day_cd' and fld_valu='" + cbday.SelectedValue + "'"),
            district_cd = cbdistrict.SelectedValue,
            district_nm = bll.vLookUp("select loc_nm from tmst_location where loc_cd='" + cbdistrict.SelectedValue + "'"),
            //emp_cd = cbsalesman.SelectedValue,
            //emp_nm = bll.vLookUp("select emp_nm from tmst_employee where emp_cd='" + cbsalesman.SelectedValue + "'"),
            grouprps_cd = cbgroup.SelectedValue,
            grouprps_nm = cbgroup.SelectedItem.Text,
            emp_cd = cbsalesman.SelectedValue,
            emp_nm = cbsalesman.SelectedItem.Text

        });
        grd.DataSource = _trps_group;
        grd.DataBind();
        cbdistrict.SelectedValue = string.Empty;
        Session["trps_group"] = _trps_group;
        cbgroup.SelectedValue = string.Empty;
        cbcity.SelectedValue = string.Empty;
        cbdistrict.SelectedValue = string.Empty;
        cbday.SelectedValue = string.Empty;
        cd.v_enablecontrol(cbgroup);
        cd.v_enablecontrol(cbcity);
        cd.v_enablecontrol(cbdistrict);
        cd.v_enablecontrol(cbday);
        cd.v_showcontrol(btsave);

    }

    protected void cbcity_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<trps_group> _trps_group = (List<trps_group>)Session["trps_group"];
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@loc_cd", cbcity.SelectedValue));
        arr.Add(new cArrayList("@emp_cd", cbsalesman.SelectedValue));   
        bll.vBindingComboToSpWithEmptyChoosen(ref cbdistrict, "sp_tmst_location_getbyparent", "loc_cd", "loc_nm", arr);
        cd.v_enablecontrol(cbdistrict);
        Session["trps_group"] = _trps_group;
    }

    protected void cbsalesman_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["trps_group"] = (List<trps_group>)Session["trps_group"];
        cd.v_enablecontrol(cbday);
        cd.v_enablecontrol(cbcity);
        cd.v_enablecontrol(cbdistrict);
        cd.v_enablecontrol(cbgroup);

    }

    protected void cbdistrict_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
            "openreport('fm_report2.aspx?src=rpsgroup');", true);
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (grd.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
              "sweetAlert('No route saved !','Check district for route','warning');", true);
                return;
            }
            string _sql = string.Empty;
            //string _sql = "delete trps_group where grouprps_cd='" + cbgroup.SelectedValue + "' and day_cd=" + cbday.SelectedValue;
            //bll.vExecuteSQL(_sql);
            List<cArrayList> arr = new List<cArrayList>();
            List<trps_group> _trps_group = (List<trps_group>)Session["trps_group"];
            foreach (trps_group _t in _trps_group)
            {

                arr.Clear();
                arr.Add(new cArrayList("@grouprps_cd", _t.grouprps_cd));
                arr.Add(new cArrayList("@district_cd", _t.district_cd));
                arr.Add(new cArrayList("@day_cd", _t.day_cd));
                arr.Add(new cArrayList("@emp_cd", _t.emp_cd));
                bll2.vInsertRpsGroup(arr);

            }
            cd.v_hiddencontrol(btsave);
            cd.v_disablecontrol(cbdistrict);
            cd.v_enablecontrol(btadd);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
              "sweetAlert('Rps group has been saved successfully !','RPS Group has been assigned','success');", true);
        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, "Save RPS");
        }
    }

    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        List<trps_group> _trps_group = (List<trps_group>)Session["trps_group"];
        _trps_group.RemoveAt(e.RowIndex);
        grd.DataSource = _trps_group;
        grd.DataBind();
        cd.v_showcontrol(btsave);
        Session["trps_group"] = _trps_group;
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_rpsgroup.aspx");
    }

    protected void btsalesmandistrict_Click(object sender, EventArgs e)
    {
        if (cbsalesman.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
                "sweetAlert('Please select salesman !','Salesman','warning');", true);
            return;
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "PopupCenter('fm_salesmandistrict.aspx?sal="+cbsalesman.SelectedValue+"','',800,800);", true);
    }

    protected void cbdistrict_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
}