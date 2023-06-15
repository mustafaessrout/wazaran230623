using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class fm_cndnsalesman : System.Web.UI.Page
{
    cbll2 bll2 = new cbll2();
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            bll.vBindingComboToSpWithEmptyChoosen(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
            bll.vBindingFieldValueToComboWithChoosen(ref cbcndntype, "cndn_typ");
            bll.vBindingComboToSpWithEmptyChoosen(ref cbvat, "sp_tmst_tax_get", "tax_cd", "tax_desc");
            dtcndn.Text = Request.Cookies["waz_dt"].Value;
            dtpost.Text = Request.Cookies["waz_dt"].Value;
            dtpost_CalendarExtender.EndDate = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            cd.v_disablecontrol(dtcndn);
            cd.v_disablecontrol(txcndnno);
            cd.v_disablecontrol(txcurrentbalance);
            cd.v_hiddencontrol(btsave);
            cd.v_hiddencontrol(btprint);
        }
    }

    protected void cbsalesman_SelectedIndexChanged(object sender, EventArgs e)
    {
        string _amount =  bll.vLookUp("select sum(balance) from tsalesman_balance where emp_cd='"+cbsalesman.SelectedValue+"'");
        if (_amount==string.Empty)
        { _amount = "0"; }
        txcurrentbalance.Text = Convert.ToDouble( _amount).ToString("N2");
        cd.v_showcontrol(btsave);
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        string _cndncode = string.Empty;
        double _vat = 0; double _afterbalance = 0;double _amount = 0;double _currentbalance = 0; double _vat_rate = 0;double _base=0 ; 
        if (!double.TryParse(txamt.Text, out _amount))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
                "sweetAlert('Please put correct amount !','CNDN Amount','warning');", true);
            return;
        }

        if (cbvat.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
               "sweetAlert('Please select vat rate !','VAT Rate','warning');", true);
            return;
        }
        _currentbalance = Convert.ToDouble(txcurrentbalance.Text);
        _afterbalance = _currentbalance - _amount;
        _vat_rate = Convert.ToDouble( bll.vLookUp("select tax_formula from tmst_tax where tax_cd='" + cbvat.SelectedValue + "'"));
        _base = (1/(1-_vat_rate)) * _amount;
        _vat = _amount - _base;
        //if (_afterbalance < 0)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
        //      "sweetAlert('Amount cndn can not bigger than current balance !','CNDN Amount after become minus','warning');", true);
        //    return;
        //}

        if (dtpost.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
            "sweetAlert('Please correct post date !','Transaction date will be as post date','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cndn_nm", "NA"));
        arr.Add(new cArrayList("@reason", txreason.Text));
        arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue));
        arr.Add(new cArrayList("@cndn_typ", cbcndntype.SelectedValue));
        arr.Add(new cArrayList("@amt", _amount));
        arr.Add(new cArrayList("@cndn_sta_id", "N"));
        arr.Add(new cArrayList("@vat_rate", _vat_rate));
        arr.Add(new cArrayList("@vat", _vat));
        arr.Add(new cArrayList("@current_balance", _currentbalance));
        arr.Add(new cArrayList("@after_balance", _afterbalance));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        arr.Add(new cArrayList("@cndn_dt", System.DateTime.ParseExact(dtcndn.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@post_dt", System.DateTime.ParseExact(dtpost.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@tax_cd", cbvat.SelectedValue));    
        bll2.vInsertCndnSalesman(arr, ref _cndncode);
        txcndnno.Text = _cndncode;
       
        cd.v_hiddencontrol(btsave);
        cd.v_showcontrol(btprint);
        cd.v_disablecontrol(txreason);
        cd.v_disablecontrol(txamt);
        cd.v_disablecontrol(cbcndntype);
        cd.v_disablecontrol(cbsalesman);
        cd.v_disablecontrol(cbvat);

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
             "sweetAlert('New cndn has been created !','CNDN No." + _cndncode + " need approval Head Office','warning');", true);

    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        string _sql = "update tcndn_salesman set cndn_sta_id='W' where cndn_cd='"+txcndnno.Text+"'";
        bll.vExecuteSQL(_sql);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=salesmancndn&n="+txcndnno.Text+"');", true);
    }

    protected void btlookup_Click(object sender, EventArgs e)
    {
        txcndnno.Text = hdcndncode.Value;
        string _status = bll.vLookUp("select cndn_sta_id from tcndn_salesman where cndn_cd='"+hdcndncode.Value+"'");
        if (_status == "N")
        {
            cd.v_showcontrol(btprint);
        }
       
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_cndnsalesman.aspx");
    }
}