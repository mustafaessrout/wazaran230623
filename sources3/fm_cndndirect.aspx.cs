using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class fm_cndndirect : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dtcndn.Text = Request.Cookies["waz_dt"].Value;
            dtcndn.CssClass = cd.csstextro;
            bll.vBindingFieldValueToCombo(ref cbcndn, "cndn");
            bll.vBindingFieldValueToCombo(ref cbtype, "cndntyp");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value));
            bll.vDelWrkCnDnEmployee(arr);
            cbtype_SelectedIndexChanged(sender, e);
            List<string> lapproval = bll.lGetApproval("cndnemployee", 1);
            
            txcndnno.CssClass = "form-control ro";
            cbcndn_SelectedIndexChanged(sender, e);
            //InitGrid();
        }
    }


    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetEmployeeList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lEmployee = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sEmployee = string.Empty;
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        arr.Add(new cArrayList("@emp_nm", prefixText));
        bll.vSearchMstEmployee(arr, ref rs);
        while (rs.Read())
        {
            sEmployee = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_cd"].ToString() + "-" + rs["emp_nm"], rs["emp_cd"].ToString());
            lEmployee.Add(sEmployee);
        }
        return (lEmployee.ToArray());
    }

    protected void btlookup_Click(object sender, EventArgs e)
    {
        string sJobTitle = bll.vLookUp("select fld_desc from tfield_value where fld_nm='job_title_cd' and fld_valu=(select job_title_cd from tmst_employee where emp_cd='" + hdemp.Value + "')");
        string sLevelCode = bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd=(select salespointcd from tmst_employee where emp_cd='" + hdemp.Value + "')");
        lbjobtitle.Text = sJobTitle;
        lbsalespoint.Text = sLevelCode;
        //lblevel.Text = sLevelCode;
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@emp_cd", hdemp.Value));
        //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value));
        //bll.vInsertWrkCNDNEmployee(arr);
        //arr.Clear();
        //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value));
        //bll.vBindingGridToSp(ref grd, "sp_twrk_cndnemployee_get", arr);
        txcustomer_AutoCompleteExtender.ContextKey = hdemp.Value;
        txemployeesearch.CssClass = "form-control ro";
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_cndndirect.aspx");
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        if ((txhoref.Text == string.Empty))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Ref is mandatory !','ref mandatory','warning');", true);
            return;
        }
        if ((fupl.FileName == string.Empty) || (fupl.FileName == null))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Document supported must upload!','Request Form','warning');", true);
            return;
        }

        decimal dAmt = 0;
        if (!decimal.TryParse(txamt.Text, out dAmt))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amount CNDN can not zero or must numeric!','CNDN amount','warning');", true);
            return;
        }
        if (dAmt <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amount CNDN must greater than zero!','CNDN amount','warning');", true);
            return;
        }

        if (txmanualno.Text == string.Empty && cbcndn.SelectedValue == "CN")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Manual no can not empty!','Check manual number','warning');", true);
            return;
        }
        if (cbcndn.SelectedValue == "DN" && txhoref.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Ref no can not empty!','Check ref no','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value));
        arr.Add(new cArrayList("@cndn_sta_id", "N"));
        arr.Add(new cArrayList("@totamt", txamt.Text));
        arr.Add(new cArrayList("@approval_cd", hdapproval.Value));
        arr.Add(new cArrayList("@reasn_cd", cbreason.SelectedValue));
        arr.Add(new cArrayList("@cndntyp", cbtype.SelectedValue));
        arr.Add(new cArrayList("@dbcr", cbcndn.SelectedValue));
        arr.Add(new cArrayList("@emp_cd", hdemp.Value));
        arr.Add(new cArrayList("@cndn_dt", System.DateTime.ParseExact(dtcndn.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@filedoc", fupl.FileName));
        arr.Add(new cArrayList("@manualno", txmanualno.Text));
        arr.Add(new cArrayList("@cust_cd", hdcust.Value));
        arr.Add(new cArrayList("@ho_ref", txhoref.Text ));
        string sCnDn = string.Empty;
        bll.vInsertMstCnDnEmployee(arr, ref sCnDn);
        txcndnno.Text = sCnDn;
        txcndnno.CssClass = "form-control ro";
        string sImage = bll.sGetControlParameter("image_path");
        FileInfo fs = new FileInfo(fupl.FileName);
        string sExtension = fs.Extension;
        fupl.SaveAs(sImage + sCnDn + sExtension);
        arr.Clear();
        arr.Add(new cArrayList("@cndn_no", sCnDn));
        arr.Add(new cArrayList("@filedoc",  sCnDn + sExtension));
        bll.vUpdateMstCNDNEmployeeByFIle(arr);
        btsave.CssClass = "btn btn-default ro";
        cbreason.CssClass = "form-control ro";
        cbtype.CssClass = cd.csstextro;
        txmanualno.CssClass = cd.csstextro;
        btprint.CssClass = "btn btn-default ro";

        

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('New CNDN Employee has been created : "+sCnDn+"','Waiting for approval by "+lbapproval.Text+"','success');", true);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCustomerList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        // arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        arr.Add(new cArrayList("@salesman_cd", contextKey));
        bll.vSearchCustomerBySalesman2(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }
    protected void cbtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbtype.SelectedValue == "D")
        {
            grd.Enabled = false;
            txamt.CssClass = "form-control";
            txcustomer.CssClass = "form-control ro";
        }
        else if (cbtype.SelectedValue == "T")
        {
            if (cbcndn.SelectedValue == "CN")
            {
                grd.Enabled = true;
                txamt.CssClass = "form-control ro";
                txcustomer.CssClass = "form-control";
            }
        }
    }

    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        cbcndn_SelectedIndexChanged(sender, e);
        //InitGrid();

    }

    void InitGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value));
        bll.vBindingGridToSp(ref grd, "sp_twrk_cndnemployee_get", arr);
        if (grd.Rows.Count > 0)
        {
            Label lbtotcndn = (Label)grd.FooterRow.FindControl("lbtotcndn");
            lbtotcndn.Text = bll.vLookUp("select isnull(sum(amtcndn),0) from twrk_cndnemployee where usr_id='" + Request.Cookies["usr_id"].Value + "'");
            txamt.Text = lbtotcndn.Text;
        }
    }

    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lbinvoice = (Label)grd.Rows[e.RowIndex].FindControl("lbinvoice");
        TextBox txcndnamt = (TextBox)grd.Rows[e.RowIndex].FindControl("txcndnamt");
        decimal dCndn = 0;
        if (!decimal.TryParse(txcndnamt.Text, out dCndn))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amount cndn must numeric!','Check amount to be edited','warning');", true);
            return;
        }
        if (dCndn <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amount cndn must greater than zero!','Check amount to be edited','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@inv_no", lbinvoice.Text));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value));
        arr.Add(new cArrayList("@amtcndn", txcndnamt.Text));
        bll.vUpdateWrkCnDnEmployee(arr);
        grd.EditIndex = -1;
        //InitGrid();
        cbcndn_SelectedIndexChanged(sender, e);
    }

    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        cbcndn_SelectedIndexChanged(sender, e);
        //InitGrid();
    }

    protected void btlookupcust_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value));
        arr.Add(new cArrayList("@cust_cd", hdcust.Value));
        bll.vInsertWrkCNDNEmployee(arr);
        cbcndn_SelectedIndexChanged(sender, e);
        //InitGrid();
        txcustomer.CssClass = "form-control ro";
        cbtype.CssClass = "form-control ro";
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('xx','xx','success');", true);
    }

    protected void btsearchcndn_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "PopupCenter('lookupcndndirect.aspx', 'Lookup CNDN', 700, 600);", true);
    }

    protected void btcn_Click(object sender, EventArgs e)
    {
        
        System.Data.SqlClient.SqlDataReader rs = null;
        txcndnno.Text = hdcn.Value;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cndn_no", hdcn.Value));
        arr.Add(new cArrayList("@cndn_sta_id", "N"));
        bll.vGetMstCNDNEmployee(arr, ref rs);
        while (rs.Read())
        {
            txemployeesearch.CssClass = cd.csstextro;
            cbcndn.CssClass = cd.csstextro;
            cbtype.CssClass = cd.csstextro;
            txcustomer.CssClass = cd.csstextro;
            cbreason.CssClass = cd.csstextro;
            txmanualno.CssClass = cd.csstextro;
            txemployeesearch.Text = rs["emp_desc"].ToString();
            lbjobtitle.Text = rs["job_title"].ToString();
            lbsalespoint.Text = rs["salespoint_desc"].ToString();
            cbcndn.SelectedValue = rs["dbcr"].ToString();
            cbtype.SelectedValue = rs["cndntyp"].ToString();
            txcustomer.Text = rs["cust_desc"].ToString();
            cbcndn_SelectedIndexChanged(sender, e);
            cbreason.SelectedValue = rs["reasn_cd"].ToString();
            txmanualno.Text = rs["manualno"].ToString();
            lbstatus.Text = rs["status_nm"].ToString();
            if (rs["cndn_sta_id"].ToString() == "A")
            {
                btprint.CssClass = "btn btn-warning";
            } else if (rs["cndn_sta_id"].ToString() == "N")
            {
                btprint.CssClass = "btn btn-default ro";
            }
        }
        rs.Close();
        
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=cndnemp&id="+hdcn.Value+"');", true);
    }

    protected void cbcndn_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (cbcndn.SelectedValue == "CN")
        {
            hdapproval.Value = bll.vLookUp("select emp_cd from tapprovalpattern where doc_typ='cndnemployee' and level_no=1");
            lbapproval.Text = bll.vLookUp("select emp_cd+':'+emp_nm from tmst_employee where emp_cd=(select emp_cd from tapprovalpattern where doc_typ='cndnemployee' and level_no=1)");
            arr.Add(new cArrayList("@reasn_typ", "DBDIRECT"));
            bll.vBindingComboToSp(ref cbreason, "sp_tmst_reason_get", "reasn_cd", "reasn_nm", arr);
            grd.Visible = true;
            InitGrid();
        }
        else if (cbcndn.SelectedValue == "DN")
        {
            hdapproval.Value = bll.vLookUp("select emp_cd from tapprovalpattern where doc_typ='cndnDirectemployee' and level_no=1");
            lbapproval.Text = bll.vLookUp("select emp_cd+':'+emp_nm from tmst_employee where emp_cd=(select emp_cd from tapprovalpattern where doc_typ='cndnDirectemployee' and level_no=1)");
            arr.Add(new cArrayList("@reasn_typ", "DBEMPDIRECT"));
            bll.vBindingComboToSp(ref cbreason, "sp_tmst_reason_get", "reasn_cd", "reasn_nm", arr);
            grd.Enabled = false;
            txamt.CssClass = "form-control";
            txcustomer.CssClass = "form-control ro";
            grd.Visible = false;
            bll.vBindingFieldValueToCombo(ref cbtype, "cndntyp");
        }
       
       
    }

    protected void txhoref_TextChanged(object sender, EventArgs e)
    {
        //txmanualno.Text = Convert.ToString(txhoref.Text);
    }
}