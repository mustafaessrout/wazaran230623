using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstcontract : System.Web.UI.Page
{

    cbll bll = new cbll();
    decimal _totalSales = 0m;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["hdprop"] = "";
            Session["rditem"] = "";
            Session["edit"] = "";
            List<cArrayList> arr = new List<cArrayList>();
            lbSp.Text = Request.Cookies["spn"].Value.ToString();
            //txContractNo.Text = "NEW";
            lbconractno.Text = "NEW";
            dtContract.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            txFirst.Text = "SAID SALEM BAWAZIR TRD. EST.";
            bll.vBindingFieldValueToCombo(ref cbCusGrp, "cusgrcd");
            cbCustomer_SelectedIndexChanged(sender, e);
            //rditem.SelectedValue = "I";
            //rditem_SelectedIndexChanged(sender, e);
            //rdfreeitem.SelectedValue = "I";
            //rdfreeitem_SelectedIndexChanged(sender, e);
            txitem.Attributes.Add("style", "display:none");
            txfreeitem.Attributes.Add("style", "display:none");
            cbprod.Attributes.Add("style", "display:none");
            cbfreeprod.Attributes.Add("style", "display:none");
            btaddprod.Attributes.Add("style", "display:none");
            btaddfreeprod.Attributes.Add("style", "display:none");
            cbType_SelectedIndexChanged(sender, e);
            bll.vBindingFieldValueToCombo(ref cbcontractterm, "contract_term");
            bll.vBindingFieldValueToCombo(ref cbpaymenttype, "contract_payment");
            cbcontractterm_SelectedIndexChanged(sender, e);
            checkAllAgreement();
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));

            bll.vDelContractItem(arr);
            bll.vDelContractProduct(arr);
            bll.vDelContractFreeProduct(arr);
            bll.vDelContractFreeItem(arr);

            btprint.Attributes.Add("style", "display:none");

            arr.Clear();
            arr.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbContactFirst, "sp_tcontract_contact", "emp_cd", "emp_nm", arr);
            cbContactFirst_SelectedIndexChanged(sender, e);
            //rdfreeitem.SelectedValue = "I";
            //rdfreeitem.Attributes.Add("readonly", "readonly");
            //rdfreeitem_SelectedIndexChanged(sender, e);

            txcalcpct.Attributes.Add("readonly", "readonly");

            btedit.Attributes.Add("style", "display:none");
            btdelete.Attributes.Add("style", "display:none");
            btupdate.Attributes.Add("style", "display:none");
            btsave.Enabled = true;

            cbpaytype.Attributes.Add("style", "display:none");

        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetListProposal(string prefixText, int count, string contextKey)
    {
        HttpCookie cook;
        cook = HttpContext.Current.Request.Cookies["sp"];
        cbll bll = new cbll();
        List<string> lItem = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sItem = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@prop_no", prefixText));
        bll.vSearchMstProposal2(arr, ref rs);
        while (rs.Read())
        {
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["prop_no"].ToString(), rs["prop_no"].ToString());
            lItem.Add(sItem);

        }
        rs.Close();
        return (lItem.ToArray());
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetListCustomer(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@prop_no", HttpContext.Current.Session["hdprop"].ToString()));
        arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchMstCustomerContract(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetListItem(string prefixText, int count, string contextKey)
    {
        List<cArrayList> arr = new List<cArrayList>();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> litem = new List<string>();
        string sitem = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        cbll bll = new cbll();
        arr.Add(new cArrayList("@prop_no", HttpContext.Current.Session["hdprop"].ToString()));
        //arr.Add(new cArrayList("@rditem", HttpContext.Current.Session["rditem"].ToString()));
        arr.Add(new cArrayList("@item_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchMstItemBySalespoint(arr, ref rs);
        while (rs.Read())
        {
            sitem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "-" + rs["item_nm"].ToString() + "-" + rs["size"].ToString() + "-" + rs["branded_nm"].ToString(), rs["item_cd"].ToString());
            litem.Add(sitem);
        }
        rs.Close();
        return (litem.ToArray());
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_mstcontract.aspx");
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        string sContract = string.Empty;
        string totalSalesPrev = ""; string totalSalesAchive = "";
        btsave.Enabled = false;
        btsave.Visible = false;

        DateTime end_dt = DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime start_dt = DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);


        if (rdTypePayment.SelectedValue.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Select Payment Type !!!','Business Agreement','warning');", true);
            return;
        }

        if (rdTypePayment.SelectedValue.ToString() == "FG")
        {
            if (grditem.Rows.Count <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item / Product can not empty !!!','Business Agreement','warning');", true);
                return;
            }

            if (grdfreeitem.Rows.Count <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Free Item / Product can not empty !!!','Business Agreement','warning');", true);
                return;
            }
        }



        List<cArrayList> arr = new List<cArrayList>();

        if (cbType.SelectedValue.ToString() == "TB")
        {
            if (grdPrevSold.Rows.Count > 0)
            {
                GridViewRow row = grdPrevSold.FooterRow;
                totalSalesPrev = row.Cells[1].Text;
                totalSalesPrev = txprevsold.Text.ToString();
            }
            else
            {
                totalSalesPrev = txprevsold.Text.ToString();
            }
            totalSalesAchive = txachievement.Text.ToString();

            double dpct = 0;
            double dincreasing = 0;
            double dpctincreasing = 0;
            double dcalc = 0;
            double totSales = Convert.ToDouble(totalSalesPrev.Replace(" ", "").Replace("CTN", ""));
            double totSalesAchive = Convert.ToDouble(totalSalesAchive.Replace(" ", "").Replace("CTN", ""));
            double totIncreasing = Convert.ToDouble(txincreasing.Text.Replace(" ", ""));
            //double totPctIncreasing = Convert.ToDouble(txincreasingpct.Text.Replace(" ",""));

            if (!double.TryParse(txpct.Text, out dpct))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Bonus Percentage must numeric or can not empty','Tactical bonus percentage','warning');", true);
                return;
            }

            if (!double.TryParse(txincreasing.Text, out dincreasing))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Increasing percentage must numeric or can not empty','Increment percentage sold','warning');", true);
                return;
            }

            if (!double.TryParse(txincreasingpct.Text, out dpctincreasing))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Increasing percentage must numeric or can not empty','Increment percentage sold','warning');", true);
                return;
            }

            if (!double.TryParse(txcalcpct.Text, out dcalc))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Calculation can not be empty, Please click! Calculation Button','Calculation','warning');", true);
                return;
            }

            if (totSalesAchive <= totIncreasing)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Sales Achievement must be greater than Sales Target.','Sales Increasing','warning');", true);
                return;
            }
        }

        if (cbType.SelectedValue.ToString() == "SB")
        {
            if (txelec.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Electricity % can not be empty.','Shop Sign','warning');", true);
                return;
            }
            if (txmunicipal.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Municipality % can not be empty.','Shop Sign','warning');", true);
                return;
            }
            if (rdTypePayment.SelectedValue.ToString() == "FG")
            {
                if (grdfreeitem.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item Or Product for paid must be selected !','Which Item/Product Group to be Paid','warning');", true);
                    return;
                }
            }
        }

        if (grditem.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item Or Product Group must be selected !','Which Item/Product Group to be displayed','warning');", true);
            return;
        }

        if (cbType.SelectedValue.ToString() == "DR" || cbType.SelectedValue.ToString() == "DS" || cbType.SelectedValue.ToString() == "OT")
        {
            if (rdTypePayment.SelectedValue.ToString() == "FG")
            {
                if (grdfreeitem.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item Or Product for paid must be selected !','Which Item/Product Group to be Paid','warning');", true);
                    return;
                }
            }
        }

        if ((dtstart.Text == "") || (dtend.Text == ""))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Period contract must be completed !','Start and End Date','warning');", true);
            return;
        }

        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Clear();
        arr.Add(new cArrayList("@sys", "contract"));
        arr.Add(new cArrayList("@sysno", ""));
        bll.vGetContractNo(arr, ref rs);
        while (rs.Read())
        {
            sContract = rs["generated"].ToString();
        }
        arr.Clear();
        arr.Add(new cArrayList("@contract_no", sContract));
        arr.Add(new cArrayList("@prop_no", txPropNo.Text));
        arr.Add(new cArrayList("@manual_no", txManualNo.Text));
        arr.Add(new cArrayList("@start_dt", start_dt.Year + "-" + start_dt.Month + "-" + start_dt.Day));
        arr.Add(new cArrayList("@contract_dt", DateTime.ParseExact(dtContract.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@end_dt", end_dt.Year + "-" + end_dt.Month + "-" + end_dt.Day));
        arr.Add(new cArrayList("@type", cbType.SelectedValue.ToString()));
        arr.Add(new cArrayList("@rdcust", cbCustomer.SelectedValue.ToString()));
        arr.Add(new cArrayList("@rditem", rditem.SelectedValue.ToString()));
        arr.Add(new cArrayList("@rdfreeitem", "I"));
        arr.Add(new cArrayList("@contract_term", cbcontractterm.SelectedValue.ToString()));
        arr.Add(new cArrayList("@contract_payment", rdTypePayment.SelectedValue.ToString()));
        arr.Add(new cArrayList("@totamt", "0"));
        arr.Add(new cArrayList("@deleted", "0"));
        arr.Add(new cArrayList("@createby", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@createdt", DateTime.ParseExact(String.Format("{0:dd/MM/yyyy}", DateTime.Now), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        //arr.Add(new cArrayList("@pctbonus", txpct.Text));
        //arr.Add(new cArrayList("@pctincreasing", txincreasing.Text));
        //arr.Add(new cArrayList("@prevsold", txprevsold.Text));
        bll.vInsertMstContract(arr);
        //txContractNo.Text = sContract;
        lbconractno.Text = sContract;
        if (cbCustomer.SelectedValue.ToString() == "C")
        {
            if (txCustomer.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer can not be empty.','Customer','warning');", true);
                return;
            }
            string[] cust = txCustomer.Text.ToString().Split('-');
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", sContract));
            arr.Add(new cArrayList("@cust_cd", cust[0]));
            arr.Add(new cArrayList("@sp", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertContractCustomer(arr);
        }
        else
        {
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", sContract));
            arr.Add(new cArrayList("@cusgrcd", cbCusGrp.SelectedValue.ToString()));
            arr.Add(new cArrayList("@sp", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertContractCusgrcd(arr);
        }

        if (rditem.SelectedValue.ToString() == "I")
        {
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", sContract));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vUpdateContractItem(arr);
        }
        else
        {
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", sContract));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vUpdateContractProduct(arr);
        }

        if (cbType.SelectedValue.ToString() != "SB")
        {
            if (rdfreeitem.SelectedValue.ToString() == "I")
            {
                arr.Clear();
                arr.Add(new cArrayList("@contract_no", sContract));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vUpdateContractFreeitem(arr);
            }
            else
            {
                arr.Clear();
                arr.Add(new cArrayList("@contract_no", sContract));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vUpdateContractFreeproduct(arr);
            }

            if (cbType.SelectedValue.ToString() == "DR" || cbType.SelectedValue.ToString() == "DS" || cbType.SelectedValue.ToString() == "OT")
            {
                foreach (GridViewRow row in grdschedule.Rows)
                {
                    Label lbseqno = (Label)row.FindControl("lbseqno");
                    TextBox dtpayment = (TextBox)row.FindControl("dtpayment");
                    TextBox txamt = (TextBox)row.FindControl("txamt");
                    TextBox txqty = (TextBox)row.FindControl("txqty");
                    DropDownList cbuom = (DropDownList)row.FindControl("cbuom");
                    arr.Clear();
                    arr.Add(new cArrayList("@contract_no", sContract));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@seqno", lbseqno.Text));
                    arr.Add(new cArrayList("@dtpayment", DateTime.ParseExact(dtpayment.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                    arr.Add(new cArrayList("@txamt", txamt.Text));
                    arr.Add(new cArrayList("@txqty", txqty.Text));
                    arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
                    bll.vUpdateContractPaySchedule(arr);
                }
                if (cbType.SelectedValue.ToString() != "OT")
                {
                arr.Clear();
                arr.Add(new cArrayList("@contract_no", sContract));
                arr.Add(new cArrayList("@sp", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@display", cbtypedisplay.SelectedValue.ToString()));
                arr.Add(new cArrayList("@sizeP", txsizeP.Text));
                arr.Add(new cArrayList("@sizeL", txsizeL.Text));
                arr.Add(new cArrayList("@qty", txqtyGD.Text));
                arr.Add(new cArrayList("@loc", cbloc.SelectedValue.ToString()));
                bll.vInsertContractDR(arr);
                }
            }
            else
            {

                arr.Clear();
                arr.Add(new cArrayList("@contract_no", sContract));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@seqno", "1"));
                arr.Add(new cArrayList("@dtpayment", end_dt.Year + "-" + end_dt.Month + "-" + end_dt.Day));
                arr.Add(new cArrayList("@txamt", "0"));
                arr.Add(new cArrayList("@txqty", txcalcpct.Text));
                arr.Add(new cArrayList("@uom", "CTN"));
                bll.vUpdateContractPaySchedule(arr);
                arr.Clear();
                arr.Add(new cArrayList("@contract_no", sContract));
                arr.Add(new cArrayList("@sp", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@prevsold", totalSalesPrev.Replace(" ", "").Replace("CTN", "")));
                arr.Add(new cArrayList("@pctincreasing", txincreasing.Text.Replace(" ", "")));
                arr.Add(new cArrayList("@increasing", txincreasingpct.Text.Replace(" ", "")));
                arr.Add(new cArrayList("@pctbonus", txpct.Text.Replace(" ", "")));
                arr.Add(new cArrayList("@start_dt", start_dt.Year + "-" + start_dt.Month + "-" + start_dt.Day));
                arr.Add(new cArrayList("@end_dt", end_dt.Year + "-" + end_dt.Month + "-" + end_dt.Day));
                arr.Add(new cArrayList("@periode", cbperiodtype.SelectedValue.ToString()));
                bll.vInsertContractTB(arr);
            }
        }
        else
        {
            double total = 0.00;
            int periode = 0;
            double sizex = 0, sizey = 0;
            if (txsizex.Text.ToString().Replace(" ", "") == "") { sizex = 0; } else { sizex = Convert.ToDouble(txsizex.Text); }
            if (txsizey.Text.ToString().Replace(" ", "") == "") { sizey = 0; } else { sizey = Convert.ToDouble(txsizey.Text); }
            periode = (start_dt.Year * 12 + start_dt.Month) - (end_dt.Year * 12 + end_dt.Month);
            total = sizex * sizey * 200 * periode;
            if (rdTypePayment.SelectedValue.ToString() == "CH")
            {
                arr.Clear();
                arr.Add(new cArrayList("@contract_no", sContract));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@seqno", "1"));
                arr.Add(new cArrayList("@dtpayment", end_dt.Year + "-" + end_dt.Month + "-" + end_dt.Day));
                arr.Add(new cArrayList("@txamt", total.ToString()));
                arr.Add(new cArrayList("@txqty", "0"));
                arr.Add(new cArrayList("@uom", null));
                bll.vUpdateContractPaySchedule(arr);
            }
            else
            {
                foreach (GridViewRow row in grdschedule.Rows)
                {
                    Label lbseqno = (Label)row.FindControl("lbseqno");
                    TextBox dtpayment = (TextBox)row.FindControl("dtpayment");
                    TextBox txamt = (TextBox)row.FindControl("txamt");
                    TextBox txqty = (TextBox)row.FindControl("txqty");
                    DropDownList cbuom = (DropDownList)row.FindControl("cbuom");
                    arr.Clear();
                    arr.Add(new cArrayList("@contract_no", sContract));
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@seqno", lbseqno.Text));
                    arr.Add(new cArrayList("@dtpayment", DateTime.ParseExact(dtpayment.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                    arr.Add(new cArrayList("@txamt", txamt.Text));
                    arr.Add(new cArrayList("@txqty", txqty.Text));
                    arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
                    bll.vUpdateContractPaySchedule(arr);
                }
            }


            arr.Clear();
            arr.Add(new cArrayList("@contract_no", sContract));
            arr.Add(new cArrayList("@sp", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@electricity", txelec.Text));
            arr.Add(new cArrayList("@municipality", txmunicipal.Text));
            arr.Add(new cArrayList("@sizeP", txsizex.Text));
            arr.Add(new cArrayList("@sizeL", txsizey.Text));
            arr.Add(new cArrayList("@total", total.ToString()));
            bll.vInsertContractSB(arr);
        }

        arr.Clear();
        arr.Add(new cArrayList("@contract_no", sContract));
        arr.Add(new cArrayList("@emp_cd", cbContactFirst.SelectedValue.ToString()));
        arr.Add(new cArrayList("@vendor_nm", txNameSecond.Text));
        arr.Add(new cArrayList("@vendor_position", txPostionSecond.Text));
        arr.Add(new cArrayList("@vendor_mobile", txMobileSecond.Text));
        bll.vInsertContractContact(arr);

        //Random rnd = new Random();
        //int token = rnd.Next(1000, 9999);
        ////Send Approval SMS
        //string sSPN = bll.vLookUp("select salespoint_sn from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
        //List<string> lapproval = bll.lGetApproval("contractapp", 1);
        //string sSMS = "#Business Agreement ("+sContract+"), with Prop no:" + hdprop.Value.ToString() + ", BRN:" + sSPN + ",pls reply with (Y/N)" + token.ToString();        // cd.vSendSms(sSMS, lapproval[0]);
        //arr.Clear();
        ////arr.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
        //arr.Add(new cArrayList("@token", token.ToString()));
        //arr.Add(new cArrayList("@doc_typ", "contractapp"));
        //arr.Add(new cArrayList("@to", lapproval[0]));
        //arr.Add(new cArrayList("@doc_no", sContract));
        //arr.Add(new cArrayList("@msg", sSMS));

        //arr.Add(new cArrayList("@token", token.ToString()));
        //arr.Add(new cArrayList("@doc_typ", "contractapp"));
        //arr.Add(new cArrayList("@to", lapproval[0]));
        //arr.Add(new cArrayList("@doc_no", sContract));
        //arr.Add(new cArrayList("@msg", sSMS));
        //bll.vInsertSmsOutbox(arr);



        //foreach (GridViewRow row in grdagree.Rows)
        //{
        //    Label lbagreecode = (Label)row.FindControl("lbagreecode");
        //    arr.Clear();
        //    arr.Add(new cArrayList("@agree_cd", lbagreecode.Text));
        //    arr.Add(new cArrayList("@contract_no", sContract));
        //    bll.vInsertContractAgreement(arr);
        //}
        btsave.Attributes.Add("style", "display:none");
        btnew.Attributes.Remove("style");
        if (cbType.SelectedValue.ToString() == "OT")
        {
            btprint.Attributes.Add("style","display:none");
        }
        else
        {
            btprint.Attributes.Remove("style");
        }
        
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('New contract has been saved','" + sContract + "','success');", true);
    }

    protected void cbCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbCustomer.SelectedValue.ToString() == "C")
        {
            txCustomer.Attributes.Remove("style");
            cbCusGrp.Attributes.Add("style", "display:none");
        }
        else
        {
            txCustomer.Attributes.Add("style", "display:none");
            cbCusGrp.Attributes.Remove("style");
        }
    }
    protected void rditem_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (rditem.SelectedValue.ToString() == "I")
        {
            txitem.Attributes.Remove("style");
            cbprod.Attributes.Add("style", "display:none");
            btaddprod.Attributes.Remove("style");
            Session["rditem"] = "I";
        }
        else
        {
            arr.Clear();
            arr.Add(new cArrayList("@level_no", 3));
            arr.Add(new cArrayList("@proposal", txPropNo.Text));
            bll.vBindingComboToSp(ref cbprod, "sp_tmst_product_getbyprop", "prod_cd", "prod_nm", arr);
            txitem.Attributes.Add("style", "display:none");
            cbprod.Attributes.Remove("style");
            btaddprod.Attributes.Remove("style");
            Session["rditem"] = "P";
        }

    }
    protected void btaddprod_ServerClick(object sender, EventArgs e)
    {
        string[] cust = txCustomer.Text.Split('-');
        List<cArrayList> arr = new List<cArrayList>();
        if (rditem.SelectedValue.ToString() == "I")
        {
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
            bll.vInsertContractItem(arr);
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grditem, "sp_tcontract_item_get", arr);
            if (cbType.SelectedValue.ToString() == "TB")
            {
                arr.Clear();
                arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@item", rditem.SelectedValue.ToString()));
                arr.Add(new cArrayList("@cust_cd", cust[0].ToString()));
                //bll.vBindingGridToSp(ref grdPrevSold, "sp_sum_prevsold", arr);
                cbperiodtype_SelectedIndexChanged(sender, e);
            }
        }
        else
        {
            if (Session["Edit"] == "true")
            {
                arr.Add(new cArrayList("@contract_no", txPropNo.Text));
            }
            else
            {
                arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            
            arr.Add(new cArrayList("@prod_cd", cbprod.SelectedValue.ToString()));
            bll.vInsertContractProduct(arr);
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grditem, "sp_tcontract_product_get", arr);
            if (cbType.SelectedValue.ToString() == "TB")
            {
                arr.Clear();
                if (Session["Edit"] == "true")
                {
                    arr.Add(new cArrayList("@contract_no", txPropNo.Text));
                }
                else
                {
                    arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
                }
                arr.Add(new cArrayList("@item", rditem.SelectedValue.ToString()));
                arr.Add(new cArrayList("@cust_cd", cust[0].ToString()));
                //bll.vBindingGridToSp(ref grdPrevSold, "sp_sum_prevsold", arr);
                cbperiodtype_SelectedIndexChanged(sender, e);
            }
        }
        if (grdPrevSold.Rows.Count == 0)
        {
            txprevsold.Attributes.Remove("style");
            grdPrevSold.Visible = false;
        }
    }
    protected void cbType_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (cbType.SelectedValue.ToString() == "DR" || cbType.SelectedValue.ToString() == "DS")
        {
            secTB.Attributes.Add("style", "display:none");
            secSS.Attributes.Add("style", "display:none");
            aggTB.Attributes.Add("style", "display:none");
            aggSS.Attributes.Add("style", "display:none");
            secGD.Attributes.Remove("style");
            aggGD.Attributes.Remove("style");
            arr.Clear();
            arr.Add(new cArrayList("@agree_typ", cbType.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grdagreeGD, "tmst_agreement_get", arr);
            lbBGD.Text = "Type of Display";
            bll.vBindingComboToSp(ref cbtypedisplay, "sp_display_typ", "promo_typ", "promotyp_nm", arr);
            //bll.vBindingFieldValueToCombo(ref cbtypedisplay, "display_typ");
            bll.vBindingFieldValueToCombo(ref cbloc, "display_loc");
            arr.Clear();
            arr.Add(new cArrayList("@agree_typ", "TRM"));
            bll.vBindingGridToSp(ref grdtermGD, "tmst_agreement_get", arr);
            arr.Clear();
            arr.Add(new cArrayList("@agree_typ", "DMG"));
            bll.vBindingGridToSp(ref grddamageGD, "tmst_agreement_get", arr);
            checkAllAgreement();
            rdTypePayment.Attributes.Remove("style");
        }
        else if (cbType.SelectedValue.ToString() == "TB")
        {
            secGD.Attributes.Add("style", "display:none");
            secSS.Attributes.Add("style", "display:none");
            aggGD.Attributes.Add("style", "display:none");
            aggSS.Attributes.Add("style", "display:none");
            secTB.Attributes.Remove("style");
            aggTB.Attributes.Remove("style");
            arr.Clear();
            arr.Add(new cArrayList("@agree_typ", cbType.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grdagreeTB, "tmst_agreement_get", arr);
            arr.Clear();
            arr.Add(new cArrayList("@agree_typ", "DMG"));
            bll.vBindingGridToSp(ref grddamageTB, "tmst_agreement_get", arr);
            checkAllAgreement();
            rdTypePayment.Attributes.Remove("style");
        }
        else if (cbType.SelectedValue.ToString() == "OT")
        {
            secTB.Attributes.Add("style", "display:none");
            secSS.Attributes.Add("style", "display:none");
            aggTB.Attributes.Add("style", "display:none");
            aggSS.Attributes.Add("style", "display:none");
            aggGD.Attributes.Add("style", "display:none");
            secGD.Attributes.Remove("style");
            rdTypePayment.Attributes.Remove("style");
        }
        else
        {
            secTB.Attributes.Add("style", "display:none");
            secGD.Attributes.Add("style", "display:none");
            aggTB.Attributes.Add("style", "display:none");
            aggGD.Attributes.Add("style", "display:none");
            secSS.Attributes.Remove("style");
            //aggSS.Attributes.Remove("style");
            rdTypePayment.Attributes.Remove("style");
        }
    }
    protected void grditem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbitemcode;
        List<cArrayList> arr = new List<cArrayList>();
        lbitemcode = (Label)grditem.Rows[e.RowIndex].FindControl("lbitemcode");
        if (rditem.SelectedValue.ToString() == "I")
        {
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
            bll.vDelContractItem(arr);
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grditem, "sp_tcontract_item_get", arr);
        }
        else
        {
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@prod_cd", lbitemcode.Text));
            bll.vDelContractProduct(arr);
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grditem, "sp_tcontract_product_get", arr);
        }
    }


    protected void grdfreeitem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbitemcode;
        List<cArrayList> arr = new List<cArrayList>();
        lbitemcode = (Label)grdfreeitem.Rows[e.RowIndex].FindControl("lbitemcode");
        if (rdfreeitem.SelectedValue.ToString() == "I")
        {
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
            bll.vDelContractFreeItem(arr);
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdfreeitem, "sp_tcontract_freeitem_get", arr);
        }
        else
        {
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@prod_cd", lbitemcode.Text));
            bll.vDelContractFreeProduct(arr);
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdfreeitem, "sp_tcontract_freeproduct_get", arr);
        }

    }

    protected void cbcontractterm_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
        bll.vDelContractPaySchedule(arr);
        int nCount = Convert.ToInt16(cbcontractterm.SelectedValue);
        if (nCount != 1)
        {
            nCount /= 30;
        }
        for (int i = 1; i <= nCount; i++)
        {
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@sequenceno", i));
            arr.Add(new cArrayList("@payment_dt", System.DateTime.Today));
            arr.Add(new cArrayList("@paycont_sta_id", "N"));
            bll.vInsertContractPaySchedule(arr);
        }
        arr.Clear();
        arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdschedule, "sp_tcontract_payschedule_get", arr);
        //cbpaymenttype_SelectedIndexChanged(sender, e);
        rdTypePayment_SelectedIndexChanged(sender, e);
    }
    protected void cbpaymenttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (cbpaymenttype.SelectedValue.ToString() == "FG")
        //{
        //    btaddfreeprod.Attributes.Add("style", "display:none");
        //    foreach (GridViewRow row in grdschedule.Rows)
        //    {
        //        TextBox txamt = (TextBox)row.FindControl("txamt");
        //        TextBox txqty = (TextBox)row.FindControl("txqty");
        //        txqty.Attributes.Remove("readonly");
        //        txamt.Attributes.Add("readonly", "readonly");
        //        cbfreeprod.Attributes.Remove("readonly");
        //        txfreeitem.Attributes.Remove("readonly");
        //        txamt.BackColor = System.Drawing.ColorTranslator.FromHtml("#939391");
        //        txqty.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        //    }
        //}
        //else
        //{
        //    btaddfreeprod.Attributes.Remove("style");
        //    foreach (GridViewRow row in grdschedule.Rows)
        //    {
        //        TextBox txamt = (TextBox)row.FindControl("txamt");
        //        TextBox txqty = (TextBox)row.FindControl("txqty");
        //        txamt.Attributes.Remove("readonly");
        //        txqty.Attributes.Add("readonly", "readonly");
        //        txfreeitem.Attributes.Add("readonly", "readonly");
        //        cbfreeprod.Attributes.Add("readonly", "readonly");
        //        txqty.BackColor = System.Drawing.ColorTranslator.FromHtml("#939391");
        //        txamt.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        //    }
        //}
    }
    protected void rdfreeitem_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (rdfreeitem.SelectedValue.ToString() == "I")
        {
            txfreeitem.Attributes.Remove("style");
            cbfreeprod.Attributes.Add("style", "display:none");
            btaddfreeprod.Attributes.Remove("style");
        }
        else
        {
            arr.Clear();
            arr.Add(new cArrayList("@level_no", 3));
            arr.Add(new cArrayList("@proposal", txPropNo.Text));
            bll.vBindingComboToSp(ref cbfreeprod, "sp_tmst_product_getbyprop", "prod_cd", "prod_nm", arr);
            txfreeitem.Attributes.Add("style", "display:none");
            cbfreeprod.Attributes.Remove("style");
            btaddfreeprod.Attributes.Remove("style");
        }
    }
    protected void btaddfreeprod_ServerClick(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (rdfreeitem.SelectedValue.ToString() == "I")
        {
            if (Session["Edit"] == "true")
            {
                arr.Add(new cArrayList("@contract_no", txPropNo.Text));
            }
            else
            {
                arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            arr.Add(new cArrayList("@item_cd", hdfreeitem.Value.ToString()));
            bll.vInsertContractFreeItem(arr); arr.Clear();
            if (Session["Edit"] == "true")
            {
                arr.Add(new cArrayList("@contract_no", txPropNo.Text));
            }
            else
            {
                arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            bll.vBindingGridToSp(ref grdfreeitem, "sp_tcontract_freeitem_get", arr);
        }
        else
        {
            if (Session["Edit"] == "true")
            {
                arr.Add(new cArrayList("@contract_no", txPropNo.Text));
            }
            else
            {
                arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            arr.Add(new cArrayList("@prod_cd", cbfreeprod.SelectedValue.ToString()));
            bll.vInsertContractFreeProduct(arr); arr.Clear();
            if (Session["Edit"] == "true")
            {
                arr.Add(new cArrayList("@contract_no", txPropNo.Text));
            }
            else
            {
                arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
            }
            bll.vBindingGridToSp(ref grdfreeitem, "sp_tcontract_freeproduct_get", arr);
        }
    }


    protected void grdschedule_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList cbuom = (DropDownList)e.Row.FindControl("cbuom");
            bll.vBindingFieldValueToCombo(ref cbuom, "uom");
        }
    }

    private void checkAllAgreement()
    {
        foreach (GridViewRow row in grdagreeGD.Rows)
        {
            CheckBox chckrw = (CheckBox)row.FindControl("chkselect");
            chckrw.Checked = true;
        }
        foreach (GridViewRow row in grdtermGD.Rows)
        {
            CheckBox chckrw = (CheckBox)row.FindControl("chkselect");
            chckrw.Checked = true;
        }
        foreach (GridViewRow row in grddamageGD.Rows)
        {
            CheckBox chckrw = (CheckBox)row.FindControl("chkselect");
            chckrw.Checked = true;
        }
        foreach (GridViewRow row in grdagreeTB.Rows)
        {
            CheckBox chckrw = (CheckBox)row.FindControl("chkselect");
            chckrw.Checked = true;
        }
        foreach (GridViewRow row in grddamageTB.Rows)
        {
            CheckBox chckrw = (CheckBox)row.FindControl("chkselect");
            chckrw.Checked = true;
        }
    }

    protected void btlookup_Click(object sender, EventArgs e)
    {
        string typeProposal = "";
        Session["hdprop"] = txPropNo.Text;
        string[] proposal = txPropNo.Text.Split('/');
        txPropNo.Attributes.Add("readonly", "readonly");
        txPropNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#939391");
        if ((proposal[1] == "GD") || (proposal[1] == "FD") || (proposal[1] == "SD")) { typeProposal = "DR"; }
        else if (proposal[1] == "FS") { typeProposal = "DS"; }
        else if (proposal[1] == "TB") { typeProposal = "TB"; }
        else if (proposal[1] == "SB") { typeProposal = "SB"; }
        else { 
            typeProposal = "OT"; 
            btprint.Attributes.Add("style","display:none");
            lbtitlesecGD.Text = "Agreement Section";
            cbcontractterm.Attributes.Add("disabled", "disabled");
            //cbpaymenttype.Attributes.Add("disabled", "disabled");
        }
        cbType.SelectedValue = typeProposal;
        cbType.Attributes.Add("disabled", "disabled");
        cbType_SelectedIndexChanged(sender, e);
        if (typeProposal == "SB") { lblFree.Attributes.Add("style", "display:none"); dtFree.Attributes.Add("style", "display:none"); }
        else { lblFree.Attributes.Remove("style"); dtFree.Attributes.Remove("style"); }

        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@prop_no", txPropNo.Text));
        bll.vGetProposal(arr, ref rs);
        string rditemProp = "";
        while (rs.Read())
        {
            dtstart.Text = rs["start_dt"].ToString();
            dtend.Text = rs["end_dt"].ToString();
            rditemProp = rs["rditem"].ToString();
        }
        if (rditemProp == "I")
        {
            rditem.SelectedValue = "I";
            rdfreeitem.SelectedValue = "I";
            rditem.Enabled = false;
            rdfreeitem.Enabled = false;
            rditem_SelectedIndexChanged(sender, e);
            rdfreeitem_SelectedIndexChanged(sender, e);
        }

    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=contract&type=" + cbType.SelectedValue.ToString() + "&cno=" + lbconractno.Text + "');", true);
    }

    protected void btsearchcontract_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ops", "popupwindow('fm_lookupcontract_all.aspx');", true);
    }
    protected void btlookcontract_Click(object sender, EventArgs e)
    {
        string salesAchieve = "";
        lbconractno.Text = hdcontract.Value.ToString();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@contract_no", hdcontract.Value.ToString()));
        bll.vGetContract(arr, ref rs);
        while (rs.Read())
        {
            cbType.SelectedValue = rs["type"].ToString();
            txPropNo.Text = rs["prop_no"].ToString();
            txManualNo.Text = rs["manual_no"].ToString();
            dtContract.Text = rs["contract_dt2"].ToString();
            dtstart.Text = rs["start_dt2"].ToString();
            dtend.Text = rs["end_dt2"].ToString();
            cbCustomer.SelectedValue = rs["rdcust"].ToString();
            txCustomer.Text = rs["customer"].ToString();
            rditem.SelectedValue = rs["rditem"].ToString();
            rdfreeitem.SelectedValue = rs["rdfreeitem"].ToString();
            cbContactFirst.SelectedValue = rs["emp_cd"].ToString();
            txNameSecond.Text = rs["vendor_nm"].ToString();
            txPostionSecond.Text = rs["vendor_position"].ToString();
            txMobileSecond.Text = rs["vendor_mobile"].ToString();
            
        }
        rs.Close();
        arr.Clear();
        if (cbprod.SelectedValue.ToString() == "I")
        {
            arr.Add(new cArrayList("@contract_no", hdcontract.Value.ToString()));
            bll.vBindingGridToSp(ref grditem, "sp_tcontract_item_get", arr);
        }
        else
        {
            arr.Add(new cArrayList("@contract_no", hdcontract.Value.ToString()));
            bll.vBindingGridToSp(ref grditem, "sp_tcontract_product_get", arr);
        }
        arr.Clear();
        if (cbfreeprod.SelectedValue.ToString() == "I")
        {
            arr.Add(new cArrayList("@contract_no", hdcontract.Value.ToString()));
            bll.vBindingGridToSp(ref grdfreeitem, "sp_tcontract_freeitem_get", arr);
        }
        else
        {
            arr.Add(new cArrayList("@contract_no", hdcontract.Value.ToString()));
            //bll.vBindingGridToSp(ref grdfreeitem, "sp_tcontract_freeproduct_get", arr);
            bll.vBindingGridToSp(ref grdfreeitem, "sp_tcontract_freeitem_get", arr);
        }
        cbType_SelectedIndexChanged(sender, e);
        if (cbType.SelectedValue.ToString() == "DR")
        {
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", hdcontract.Value.ToString()));
            arr.Add(new cArrayList("@type", cbType.SelectedValue.ToString()));
            //bll.
        }
        else if (cbType.SelectedValue.ToString() == "TB")
        {
            arr.Clear();
            arr.Add(new cArrayList("@contract_no", hdcontract.Value.ToString()));
            arr.Add(new cArrayList("@type", cbType.SelectedValue.ToString()));
            bll.vGetContract(arr, ref rs);
            while (rs.Read())
            {
                txprevsold.Text = rs["prevsold"].ToString() == "" ? "0" : rs["prevsold"].ToString();
                txpct.Text = rs["pctbonus"].ToString() == "" ? "0" : rs["pctbonus"].ToString();
                txincreasing.Text = rs["pctincreasing"].ToString() == "" ? "0" : rs["pctincreasing"].ToString();
                txincreasingpct.Text = rs["increasing"].ToString() == "" ? "0" : rs["increasing"].ToString();
                txachievement.Text = rs["prevsold"].ToString() == "" ? "0" : rs["prevsold"].ToString();
            }
            rs.Close();
            
            
            btncalc_Click(sender, e);
        }
        else if (cbType.SelectedValue.ToString() == "SB")
        {

        }
        string status = bll.vLookUp("select approval from tmst_contract where contract_no = '" + lbconractno.Text + "'");
        if (status != "N" || status != "U" || status != "A")
        {
            btupdate.Attributes.Remove("style");
            btdelete.Attributes.Remove("style");
            Session["edit"] = "true";
        }
        else
        {
            btupdate.Attributes.Add("style", "display:none");
            btdelete.Attributes.Add("style", "display:none");
        }
        btsave.Attributes.Add("style", "display:none");
        btprint.Attributes.Remove("style");
        
    }
    protected void grdPrevSold_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label total = (Label)e.Row.FindControl("lbtotal");
            _totalSales += Convert.ToDecimal(total.Text);
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Total : ";
            e.Row.Cells[1].Text = _totalSales.ToString() + " CTN";
            txprevsold.Text = _totalSales.ToString();
        }
    }
    protected void cbContactFirst_SelectedIndexChanged(object sender, EventArgs e)
    {
        txPostionFirst.Text = bll.vLookUp("select fld_desc from tmst_employee left join tfield_value on tmst_employee.job_title_cd = tfield_value.fld_valu and fld_nm = 'job_title_cd' where tmst_employee.emp_cd = '" + cbContactFirst.SelectedValue.ToString() + "' ");

        txMobileFirst.Text = bll.vLookUp("select phone_no from tmst_employee left join tfield_value on tmst_employee.job_title_cd = tfield_value.fld_valu and fld_nm = 'job_title_cd' where tmst_employee.emp_cd = '" + cbContactFirst.SelectedValue.ToString() + "' ");

    }
    protected void txpct_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btncalc_Click(object sender, EventArgs e)
    {
        double dtotal = 0;
        string stotal = "";
        string sright = "";

        string sContract = string.Empty;
        string totalSalesPrev = "";
        string totalSalesAchive = "";
        if (cbType.SelectedValue.ToString() == "TB")
        {
            if (grdPrevSold.Rows.Count > 0)
            {
                GridViewRow row = grdPrevSold.FooterRow;
                totalSalesPrev = row.Cells[1].Text;
                totalSalesPrev = txprevsold.Text.ToString();
            }
            else
            {
                totalSalesPrev = txprevsold.Text.ToString();
            }
            totalSalesAchive = txachievement.Text.ToString();

            double dpct = 0;
            double dincreasing = 0;
            double dpctincreasing = 0;
            double dcalc = 0;
            double totSales = Convert.ToDouble(totalSalesPrev.Replace(" ", "").Replace("CTN", ""));
            double totSalesAchive = Convert.ToDouble(totalSalesAchive.Replace(" ", "").Replace("CTN", ""));
            double totIncreasing = Convert.ToDouble(txincreasing.Text.Replace(" ", ""));
            //double totPctIncreasing = Convert.ToDouble(txincreasingpct.Text.Replace(" ",""));


            if (totSalesAchive <= totIncreasing)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Sales Achievement must be greater than Sales Target.','Sales Increasing','warning');", true);
                return;
            }
        }


        if (grdPrevSold.Rows.Count > 0)
        {
            GridViewRow row = grdPrevSold.FooterRow;
            string total = row.Cells[1].Text;
            total = txprevsold.Text;
            dtotal = Math.Round((Convert.ToDouble(txpct.Text.Replace(" ", "").Replace("%", "")) * Convert.ToDouble(total.Replace(" ", "").Replace("CTN", "")) / 100), 1);
            stotal = dtotal.ToString();
            if (stotal.Length > 1)
            {
                sright = stotal.Substring(stotal.Length - 2, 2);
                if (sright == ".5")
                {
                    dtotal = dtotal + (double)0.1;
                }
            }
            dtotal = Math.Round(dtotal, 0);
            txcalcpct.Text = dtotal.ToString();
        }
        else
        {
            dtotal = Math.Round((Convert.ToDouble(txpct.Text.Replace(" ", "").Replace("%", "")) * Convert.ToDouble(txprevsold.Text) / 100), 1);
            stotal = dtotal.ToString();
            if (stotal.Length > 1)
            {
                sright = stotal.Substring(stotal.Length - 2, 2);
                if (sright == ".5")
                {
                    dtotal = dtotal + (double)0.1;
                }
            }
            dtotal = Math.Round(dtotal, 0);
            txcalcpct.Text = dtotal.ToString();
            //txcalcpct.Text = Convert.ToString(Convert.ToDouble(txpct.Text.Replace(" ", "").Replace("%", "")) * Convert.ToDouble(txprevsold.Text) / 100);
        }

    }
    protected void btedit_ServerClick(object sender, EventArgs e)
    {
        string status = bll.vLookUp("select approval from tmst_contract where contract_no = '" + lbconractno.Text + "'");
        if (status != "N" || status != "U")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dd", "alert('Bussiness Agreement can't edit.')", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Bussiness Agreement can't edit.','Bussiness Agreement','warning');", true);
            
        }
        else
        {
            btupdate.Attributes.Remove("style");
            btedit.Attributes.Add("style", "display:none");
            Session["edit"] = "true";
        }
    }

    protected void btdelete_ServerClick(object sender, EventArgs e)
    {
        string status = bll.vLookUp("select approval from tmst_contract where contract_no = '" + lbconractno.Text + "'");
        if (status != "N")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Bussiness Agreement can't delete.','Bussiness Agreement','warning');", true);
            return;
        }
        else
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@contract_no", lbconractno.Text));
            bll.vDelContract(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Contract has been deleted','" + lbconractno.Text + "','success');", true);
            //btnew_Click(sender, e);
        }
    }

    protected void btupdate_ServerClick(object sender, EventArgs e)
    {
        string status = bll.vLookUp("select approval from tmst_contract where contract_no = '" + lbconractno.Text + "'");
        if (status != "N" || status != "U" || status != "A")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Bussiness Agreement can't Update.','Bussiness Agreement','warning');", true);
            return;
        }
        else
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@contract_no", lbconractno.Text));
            arr.Add(new cArrayList("@prop_no", txPropNo.Text));
            bll.vUpdContract(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Bussiness Agreement','" + lbconractno.Text + " has been update.','success');", true);
            btnew_Click(sender, e);
        }
    }

    protected void rdTypePayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdTypePayment.SelectedValue.ToString() == "CH" || rdTypePayment.SelectedValue.ToString() == "CN")
        {
            if (cbType.SelectedValue.ToString() == "SB")
            {
                //titleSection.Text = "Gondola Section";
                secGD.Attributes.Add("style", "display:none");
            }
            lblFree.Attributes.Add("style", "display:none"); dtFree.Attributes.Add("style", "display:none");

            btaddfreeprod.Attributes.Remove("style");
            foreach (GridViewRow row in grdschedule.Rows)
            {
                TextBox txamt = (TextBox)row.FindControl("txamt");
                TextBox txqty = (TextBox)row.FindControl("txqty");
                txamt.Attributes.Remove("readonly");
                txqty.Attributes.Add("readonly", "readonly");
                txfreeitem.Attributes.Add("readonly", "readonly");
                cbfreeprod.Attributes.Add("readonly", "readonly");
                txqty.BackColor = System.Drawing.ColorTranslator.FromHtml("#939391");
                txamt.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            }

        }
        else
        {
            if (cbType.SelectedValue.ToString() == "SB")
            {
                //titleSection.Text = "Signboard Section";
                secGD.Attributes.Remove("style");
            }
            lblFree.Attributes.Remove("style"); dtFree.Attributes.Remove("style");

            btaddfreeprod.Attributes.Add("style", "display:none");
            btaddfreeprod.Attributes.Remove("style");

            foreach (GridViewRow row in grdschedule.Rows)
            {
                TextBox txamt = (TextBox)row.FindControl("txamt");
                TextBox txqty = (TextBox)row.FindControl("txqty");
                txqty.Attributes.Remove("readonly");
                txamt.Attributes.Add("readonly", "readonly");
                cbfreeprod.Attributes.Remove("readonly");
                txfreeitem.Attributes.Remove("readonly");
                txamt.BackColor = System.Drawing.ColorTranslator.FromHtml("#939391");
                txqty.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            }


        }
    }
    protected void cbperiodtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        string contract_no , customer, item, periode, prop_no;
        if (grditem.Rows.Count <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item / Product can not empty !!!','Business Agreement','warning');", true);
            return;
        }            
        if (Session["Edit"] == "true")
        {
            contract_no = txPropNo.Text;
            //arr.Add(new cArrayList("@contract_no", txPropNo.Text));
        }
        else
        {
            contract_no = Request.Cookies["usr_id"].Value.ToString();
            //arr.Add(new cArrayList("@contract_no", Request.Cookies["usr_id"].Value.ToString()));
        }
        if (cbCustomer.SelectedValue.ToString() == "C")
        {
            if (txCustomer.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer can not be empty.','Customer','warning');", true);
                return;
            }
            string[] cust = txCustomer.Text.ToString().Split('-');
            //arr.Add(new cArrayList("@customer", cust[0]));
            customer = cust[0];
        }
        else
        {
            //arr.Add(new cArrayList("@customer", cbCusGrp.SelectedValue.ToString()));
            customer = cbCusGrp.SelectedValue.ToString();
        }
        //arr.Add(new cArrayList("@item", rditem.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@periode", cbperiodtype.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
        string actualSales = bll.vLookUp("exec sp_sum_actualsales '" + contract_no + "','" + customer + "','" + rditem.SelectedValue.ToString() + "','" + cbperiodtype.SelectedValue.ToString() + "','" + hdprop.Value.ToString() + "'");
        string salesAchieve = bll.vLookUp("exec sp_sum_salesachievement '" + contract_no + "','" + customer + "','" + rditem.SelectedValue.ToString() + "','" + cbperiodtype.SelectedValue.ToString() + "','" + hdprop.Value.ToString() + "'");
        txprevsold.Text = actualSales;
        txachievement.Text = salesAchieve;
        txprevsold.Attributes.Add("readonly", "readonly");
        txachievement.Attributes.Add("readonly", "readonly");
    }
}