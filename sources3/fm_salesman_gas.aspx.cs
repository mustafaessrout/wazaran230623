using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_salesman_gas : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            dtrefill.Text = Request.Cookies["waz_dt"].Value;
            bll.vBindingComboToSpWithEmptyChoosen(ref cbsalesman, "sp_tmst_employee_getbyjobtitleqry", "emp_cd", "emp_nm", arr);
            cd.v_disablecontrol(txgasolinecode);
            cd.v_disablecontrol(dtrefill);
            cd.v_disablecontrol(txtabno);
            cd.v_hiddencontrol(btsave);
            cd.v_hiddencontrol(btprint);
            cd.v_disablecontrol(txkilometer);
            cd.v_disablecontrol(txliter);
        }
    }

    protected void btlookup_Click(object sender, EventArgs e)
    {
        txgasolinecode.Text = hdgasoline.Value;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        arr.Add(new cArrayList("@period_cd", bll.vLookUp("select dbo.fn_getcontrolparameter('period')")));
        List<tsalesman_gasoline_consume> _tsalesman_gasoline_consume = bll.lGetSalesmanGasoline(arr);
        foreach(tsalesman_gasoline_consume _t in _tsalesman_gasoline_consume)
        {
            dtrefill.Text = _t.fill_dt.ToString("d/M/yyyy");
            cbsalesman.SelectedValue = _t.salesman_cd;
            txkilometer.Text = _t.kilometer.ToString();
            txliter.Text = _t.liter.ToString();
            txtabno.Text = _t.tab_gas_cd;
        }
        cd.v_disablecontrol(dtrefill);
        cd.v_disablecontrol(cbsalesman);
        cd.v_disablecontrol(txkilometer);
        cd.v_disablecontrol(txliter);
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_salesman_gas.aspx");
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        decimal _liter = 0;decimal _kilometer= 0;   
        if (!decimal.TryParse(txliter.Text, out _liter))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
                "sweetAlert('Please enter correct amount liter','Liter','success');", true);
            return;
        }

        if (!decimal.TryParse(txkilometer.Text, out _kilometer))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
              "sweetAlert('Please enter correct amount kilometer','Kilometer','success');", true);
            return;
        }
        string _gascode = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        arr.Add(new cArrayList("@kilometer", _kilometer));
        arr.Add(new cArrayList("@liter", _liter));
        arr.Add(new cArrayList("@fill_dt", System.DateTime.ParseExact(dtrefill.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue));
        bll.vInsertSalesmanGasolineConsume(arr, ref _gascode);
        cd.v_disablecontrol(txkilometer);
        cd.v_disablecontrol(txliter);
        cd.v_hiddencontrol(btsave);
        cd.v_showcontrol(btprint);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
              "sweetAlert('New gasoline consumed has been created!','"+_gascode+"','success');", true);

    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
            "openreport('fm_report2.aspx?src=salesgas&n="+txgasolinecode.Text+"');", true);
    }

    protected void cbsalesman_SelectedIndexChanged(object sender, EventArgs e)
    {
        cd.v_showcontrol(btsave);
        cd.v_enablecontrol(txkilometer);
        cd.v_enablecontrol(txliter);
    }
}