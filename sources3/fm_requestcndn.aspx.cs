using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_requestcndn : System.Web.UI.Page
{
    cbll bll = new cbll();
    cbll2 bll2 = new cbll2();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();  
            dtcndn.Text = Request.Cookies["waz_dt"].Value;
            bll.vBindingComboToSpWithEmptyChoosen(ref cbvatrate, "sp_tmst_tax_get", "tax_cd", "tax_desc");
            bll.vBindingFieldValueToComboWithChoosen(ref cbtype, "cndn");
            dtpost.Text = Request.Cookies["waz_dt"].Value;
            arr.Add(new cArrayList("@reasn_typ", "cndn"));
            bll.vBindingComboToSpWithEmptyChoosen(ref cbreason, "sp_tmst_reason_get", "reasn_cd", "reasn_nm", arr);
            cd.v_disablecontrol(txcndnno);
            cd.v_disablecontrol(dtcndn);
            cd.v_hiddencontrol(btsave);
            cd.v_hiddencontrol(btprint);
        }
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCustomerList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        cbll2 bll2 = new cbll2();
        System.Data.SqlClient.SqlDataReader rs = null;
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        // arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll2.vSearchMstCustomerHaveBalance(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(),rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }

    protected void btsearchcustomer_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", hdcustomer.Value));
        List<tacc_cndndtl> _tacc_cndndtl = bll2.lGetAccCndnDtlByCustomer(arr);
        grd.DataSource = _tacc_cndndtl;
        grd.DataBind();

        Session["tacc_cndndtl"] = _tacc_cndndtl;
        cd.v_disablecontrol(txcustomer);
    }

    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        List<tacc_cndndtl> _tacc_cndndtl = (List<tacc_cndndtl>)Session["tacc_cndndtl"];

        grd.EditIndex = e.NewEditIndex;
        grd.DataSource = _tacc_cndndtl;
        grd.DataBind();

        Session["tacc_cndndtl"] = _tacc_cndndtl;

    }

    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        List<tacc_cndndtl> _tacc_cndndtl = (List<tacc_cndndtl>)Session["tacc_cndndtl"];
        Label lbinvoiceno = (Label)grd.Rows[e.RowIndex].FindControl("lbinvoiceno");
        TextBox txamt = (TextBox)grd.Rows[e.RowIndex].FindControl("txamt");
        if (cbvatrate.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select VAT rate!','VAT Rate','warning');", true);
            Session["tacc_cndndtl"] = _tacc_cndndtl;
            return;
        }
        double _amount = 0;

        if (!double.TryParse(txamt.Text, out _amount))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please entry correct amount!','Amount CN DN','warning');", true);
            Session["tacc_cndndtl"] = _tacc_cndndtl;
            return;
        }
        if (cbvatrate.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('VAT Rate is mandatory !','Vat Rate','warning');", true);
            Session["tacc_cndndtl"] = _tacc_cndndtl;
            return;
        }
        if (txmanualno.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Manual number is mandatory !','Manual','warning');", true);
            Session["tacc_cndndtl"] = _tacc_cndndtl;
            return;
        }
        if (txremark.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Remark is mandatory !','Remark','warning');", true);
            Session["tacc_cndndtl"] = _tacc_cndndtl;
            return;
        }

        int _c = Convert.ToInt16( bll.vLookUp("select count(*) from tacc_cndn where refho_no='" + txmanualno.Text + "'"));
        if (_c > 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('HO Reference no can not duplicate !!','Ref No HO','warning');", true);
            Session["tacc_cndndtl"] = _tacc_cndndtl;
            return;
        }

        double _formulavat = Convert.ToDouble( bll.vLookUp("select tax_formula from tmst_tax where tax_cd='" + cbvatrate.SelectedValue + "'"));
        foreach (tacc_cndndtl _t in _tacc_cndndtl)
        {
            if (_t.inv_no == lbinvoiceno.Text)
            {
                _t.inv_cnamount = _amount;
                _t.refho_no = txmanualno.Text;
                _t.tax_cd = cbvatrate.SelectedValue;
                _t.tax_formula = _formulavat;
                _t.vat_rate = _formulavat;
                _t.based_amt = ((1 / (1 + _formulavat)) * _amount);
                _t.totalvat =  _amount - ((1 / (1 + _formulavat)) * _amount);
                _t.inv_balance = _t.inv_noamount;
                _t.cndntype = cbtype.SelectedValue;
            }
        }
        grd.EditIndex = -1;
        grd.DataSource = _tacc_cndndtl;
        grd.DataBind();
        Session["tacc_cndndtl"] = _tacc_cndndtl;
        cd.v_disablecontrol(txcustomer);
        cd.v_disablecontrol(cbtype);
        cd.v_showcontrol(btsave);
        cd.v_disablecontrol(cbvatrate);
        cd.v_disablecontrol(txremark);
        cd.v_disablecontrol(txmanualno);
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_requestcndn.aspx");
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        DateTime _dt;
        if (dtpost.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                "sweetAlert('Please post date !','Incorrect format date','warning');", true);
            return;
        }
        _dt = DateTime.ParseExact(dtpost.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        if (cbtype.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                "sweetAlert('Please select type !','CN or DN','warning');", true);
            return;
        }
        if (cbvatrate.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                "sweetAlert('Please select vat rate !','Vat Rate','warning');", true);
            return;
        }
        if (txmanualno.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                "sweetAlert('Please manual number !','Mandatory','warning');", true);
            return;
        }

        if (cbreason.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                "sweetAlert('Please select reason !','Reason','warning');", true);
            return;
        }
        if (txremark.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                "sweetAlert('Please fill remark !','Remark','warning');", true);
            return;
        }
        if (!fupl.HasFile)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
                "sweetAlert('Please file upload !','File mandatory to upload','warning');", true);
            return;
        }
        if (grd.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
              "sweetAlert('Invoice to be proceed was not found  !','Invoice','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cndn_dt", System.DateTime.ParseExact(dtcndn.Text ,"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@cndnType", cbtype.SelectedValue));
        arr.Add(new cArrayList("@remark", txremark.Text));
        arr.Add(new cArrayList("@totamtcn", 0));
        arr.Add(new cArrayList("@totamtdn", 0));
        arr.Add(new cArrayList("@cust_cd", hdcustomer.Value));
        arr.Add(new cArrayList("@vatamt", 0));
        arr.Add(new cArrayList("@vatincluded", 0));
        arr.Add(new cArrayList("@refho_no", txmanualno.Text));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        arr.Add(new cArrayList("@post_dt", _dt));  //System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value));
        arr.Add(new cArrayList("@HOFormType", "HO-DIFF"));
        arr.Add(new cArrayList("@ClearanceNo", "NA"));
        arr.Add(new cArrayList("@cndnAdj_sta_id", "N"));
        arr.Add(new cArrayList("@uploadByUser", Request.Cookies["usr_id"].Value));
        arr.Add(new cArrayList("@fileExtension", "NA"));
        arr.Add(new cArrayList("@reasn_cd", cbreason.SelectedValue));
        arr.Add(new cArrayList("@tax_cd", cbvatrate.SelectedValue));
        string _cndnumber = string.Empty;
        bll.vInsertACC_cndn(arr, ref _cndnumber);
        txcndnno.Text = _cndnumber;
        List<tacc_cndndtl> _tacc_cndndtl = (List<tacc_cndndtl>)Session["tacc_cndndtl"];
        double _totalamountcn = 0;
        double _totalamountdn = 0;
        foreach (tacc_cndndtl _t in _tacc_cndndtl)
        {
            if ((_t.inv_cnamount > 0) || (_t.inv_dnamount > 0))
            {
                arr.Clear();
                arr.Add(new cArrayList("@cndn_cd", _cndnumber));
                arr.Add(new cArrayList("@inv_cnamount", _t.inv_cnamount));
                arr.Add(new cArrayList("@inv_dnamount", _t.inv_dnamount));
                arr.Add(new cArrayList("@inv_no", _t.inv_no));
                arr.Add(new cArrayList("@cndntype", _t.cndntype));
                arr.Add(new cArrayList("@inv_Noamount", _t.inv_noamount));
                arr.Add(new cArrayList("@salespointcd", _t.salespointcd));  
                arr.Add(new cArrayList("@refho_no", _t.refho_no));
                arr.Add(new cArrayList("@inv_balance", _t.inv_balance));
                arr.Add(new cArrayList("@vat_amt", _t.totalvat));
                arr.Add(new cArrayList("@base_amt", _t.based_amt));
                bll2.vInsertAccCndnDtl(arr);
                _totalamountcn += _t.inv_cnamount;
                _totalamountdn += _t.inv_dnamount;
            }
        }
        string _sql = "update tacc_cndn set totamtCN=" + _totalamountdn.ToString("N2") + ", totamtDN=" + _totalamountdn.ToString("N2") + " where cndn_cd='"+_cndnumber+"'";
        bll.vExecuteSQL(_sql);
        string _message = "#New " + cbtype.SelectedItem.Text + " with system no : " + _cndnumber + " has been requested from branch " + Request.Cookies["sp"].Value +", "
            + "please doing approval for reject from wazaran , ";
        arr.Clear();
        arr.Add(new cArrayList("@doc_typ", "cndn"));
        arr.Add(new cArrayList("@level_no", 1));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        List<tapprovalpattern> _app = bll2.lApprovalPattern(arr);
        foreach(tapprovalpattern _approval in _app)
        {
            //bll.vSendWhatsapp(_approval.whatsapp_no, _message);
            //bll.vSendWhatsapp("966503743024", _message);
        }
        cd.v_hiddencontrol(btsave);
        cd.v_showcontrol(btprint);

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('New credit note customer has been created !','Need approval for cndn number : "+_cndnumber+"','success');", true);
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        string _sql = "update tacc_cndn set cndnAdj_sta_id='W' where cndn_cd='"+txcndnno.Text+"'";
        bll.vExecuteSQL(_sql);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=cndn&c="+txcndnno.Text+"');", true);
    }

    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        List<tacc_cndndtl> _tacc_cndndtl = (List<tacc_cndndtl>)Session["tacc_cndndtl"];
        grd.DataSource = _tacc_cndndtl;
        grd.DataBind();
        Session["tacc_cndndtl"] = _tacc_cndndtl;
    }

    protected void btlookup_Click(object sender, EventArgs e)
    {
        txcndnno.Text = hdcndn.Value;
        string _cndnadjstatus = bll.vLookUp("select cndnadj_sta_id from tacc_cndn where cndn_cd='"+hdcndn.Value+"'");
        if (_cndnadjstatus == "N")
        {
            cd.v_showcontrol(btprint);
        }



        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cndn_cd", hdcndn.Value));
        List<tacc_cndn> _tacc_cndn = bll2.lAccCndn(arr);
        List<tacc_cndndtl> _tacc_cndndtl = bll2.lAccCndnDtl(arr);
        grd.DataSource = _tacc_cndndtl;
        grd.DataBind();
        foreach(tacc_cndn _t in _tacc_cndn)
        {
            txremark.Text = _t.remark;
            txcustomer.Text = bll.vLookUp("select cust_nm from tmst_customer where cust_cd='"+_t.cust_cd+"'");
            dtcndn.Text = _t.cndn_dt.ToString("d/M/yyyy");
            dtpost.Text = _t.post_dt.ToString("d/M/yyyy");
            cbvatrate.SelectedValue = _t.tax_cd;
            cbtype.SelectedValue = _t.cndntype;
            cbreason.SelectedValue = _t.reasn_cd;

        }
        Session["tacc_cndndtl"] = _tacc_cndndtl;
        cd.v_disablecontrol(txcustomer);
        cd.v_disablecontrol(txremark);
        cd.v_disablecontrol(dtcndn);
        cd.v_disablecontrol(dtpost);
        cd.v_disablecontrol(cbtype);
        cd.v_disablecontrol(cbvatrate);
        cd.v_disablecontrol(cbreason);
    }
}