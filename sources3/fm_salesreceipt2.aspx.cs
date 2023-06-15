using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_salesreceipt2 : System.Web.UI.Page
{
    cbll bll = new cbll();
    public string statusInvoice = "N";
    public double totDelivered = 0;
    public double totReceived = 0;
    public int monthInv = 0;
    public int monthNow = 0;
    public bool statusPayment = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@qry_cd", "SalesJob"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSp(ref cbdriver, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
                arr.Clear();
                arr.Add(new cArrayList("@qry_cd", "invedit"));
                bll.vBindingComboToSp(ref cbreason, "sp_tmst_reason_getbyqry", "reasn_cd", "reasn_nm", arr);
                cbreason.Items.Insert(0, new ListItem("<< Select Reason >>", "0"));
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vDelWrkDriver(arr);
                bll.vDelWrkDoSalesDtl(arr);
                bll.vDelWrkDoSalesFree(arr);
                bll.vDelWrkDoSalesCash(arr);
                arr.Clear();
                //arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
                //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                //bll.vInsertWrkDosalesDtl(arr);
                btprint.CssClass = "btn btn-default ro";
                rddisc.SelectedValue = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm = 'checkDO'");
                rddisc.CssClass = "form-control input-group-sm ro";
                lbtitle.Text = "Goods Received";
                freeCash.Attributes.Add("style", "display:none");
                freeGoods.Attributes.Add("style", "display:none");
                btdriver.Visible = false;
                btreceived.Visible = false;
                btprint.Visible = false;
                btconfirm.Visible = false;
                btprintedit.Visible = false;
                btcancel.Visible = false;
                btpostpone.Visible = false;

            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salesreceipt2");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {

    }

    void vInitGrid()
    {
        try
        {
            double dOrder = 0; double dShip = 0; double dConv = 0; double dConvShip = 0;
            List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
            //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            //bll.vInsertWrkDosalesDtl(arr);

            //arr.Clear();        
            if (statusInvoice == "W")
            {
                arr.Add(new cArrayList("@usr_id", hdinv.Value.ToString()));
                arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
            }
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_twrk_dosalesdtl_get", arr);
            foreach (GridViewRow row in grd.Rows)
            {
                if (((row.RowState & DataControlRowState.Edit) > 0) && (row.RowType == DataControlRowType.DataRow))
                {
                    dConv = 0; dConvShip = 0;
                    Label lbitemcode = (Label)row.FindControl("lbitemcode");
                    Label lbuom = (Label)row.FindControl("lbuom");
                    TextBox txqty = (TextBox)row.FindControl("txqty");
                    //if (lbuom.Text != "CTN")
                    //{
                    dConv += Math.Round(Double.Parse(bll.vLookUp("select dbo.sfnUomQtyConv('" + lbitemcode.Text + "','" + lbuom.Text + "','CTN','" + txqty.Text + "')")), 5);
                    //}
                    //dOrder += Double.Parse(txqty.Text);
                    //dShip += Double.Parse(txqty.Text);
                    dOrder += dConv;
                    dShip += dConv;
                }
                else
                {
                    dConv = 0; dConvShip = 0;
                    Label lbitemcode = (Label)row.FindControl("lbitemcode");
                    Label lbuom = (Label)row.FindControl("lbuom");
                    Label lbqty = (Label)row.FindControl("lbqty");
                    Label lbqtyshipment = (Label)row.FindControl("lbqtyshipment");
                    dConv += Math.Round(Double.Parse(bll.vLookUp("select isnull(dbo.sfnUomQtyConv('" + lbitemcode.Text + "','" + lbuom.Text + "','CTN','" + lbqty.Text + "'),0)")), 5);
                    dConvShip += Math.Round(Double.Parse(bll.vLookUp("select isnull(dbo.sfnUomQtyConv('" + lbitemcode.Text + "','" + lbuom.Text + "','CTN','" + lbqtyshipment.Text + "'),0)")), 5);
                    //dShip += Double.Parse(lbqtyshipment.Text);
                    //dOrder += Double.Parse(lbqty.Text);
                    dShip += dConvShip;
                    dOrder += dConv;
                }
            }
            Label lbqtotqtyorder = (Label)grd.FooterRow.FindControl("lbtotqtyorder");
            Label lbtotqtyshipment = (Label)grd.FooterRow.FindControl("lbtotqtyshipment");
            lbqtotqtyorder.Text = dOrder.ToString();
            lbtotqtyshipment.Text = dShip.ToString();

            //Detail Free
            arr.Clear();
            if (statusInvoice == "W")
            {
                arr.Add(new cArrayList("@usr_id", hdinv.Value.ToString()));
                arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
            }
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdfree, "sp_twrk_dosalesfree_get", arr);
            //bll.vBindingGridToSp(ref grdfree, "sp_tdosalesinvoice_getfree", arr);
            double dQtyFree = 0, dQtyFreeShip = 0;
            if (grdfree.Rows.Count > 0)
            {
                Label lbtotqtyfree = (Label)grdfree.FooterRow.FindControl("lbtotqtyfree");
                Label lbtotreceived = (Label)grdfree.FooterRow.FindControl("lbtotreceived");
                foreach (GridViewRow row in grdfree.Rows)
                {
                    if (((row.RowState & DataControlRowState.Edit) > 0) && (row.RowType == DataControlRowType.DataRow))
                    {
                        dConv = 0; dConvShip = 0;
                        Label lbitemcode = (Label)row.FindControl("lbitemcode");
                        Label lbuom = (Label)row.FindControl("lbuom");
                        TextBox txqty = (TextBox)row.FindControl("txqty");
                        dConv += Math.Round(Double.Parse(bll.vLookUp("select dbo.sfnUomQtyConv('" + lbitemcode.Text + "','" + lbuom.Text + "','CTN','" + txqty.Text + "')")), 5);
                        dQtyFree += dConv;
                        dQtyFreeShip += dConv;
                    }
                    else
                    {
                        dConv = 0; dConvShip = 0;
                        Label lbitemcode = (Label)row.FindControl("lbitemcode");
                        Label lbuom = (Label)row.FindControl("lbuom");
                        Label lbqtyfree = (Label)row.FindControl("lbqtyfree");
                        Label lbqtyreceived = (Label)row.FindControl("lbqtyreceived");
                        dConv += Math.Round(Double.Parse(bll.vLookUp("select dbo.sfnUomQtyConv('" + lbitemcode.Text + "','" + lbuom.Text + "','CTN','" + lbqtyfree.Text + "')")), 5);
                        dConvShip += Math.Round(Double.Parse(bll.vLookUp("select dbo.sfnUomQtyConv('" + lbitemcode.Text + "','" + lbuom.Text + "','CTN','" + lbqtyreceived.Text + "')")), 5);
                        dQtyFree += dConv;
                        dQtyFreeShip += dConvShip;
                    }
                }
                lbtotqtyfree.Text = dQtyFree.ToString();
                lbtotreceived.Text = dQtyFreeShip.ToString();
            }

            // Detail Cash 
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdcash, "sp_twrk_dosalescash_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salesreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }

    void initGridDriver()
    {
        List<cArrayList> arr = new List<cArrayList>();
    }

    protected void btinv_Click(object sender, EventArgs e)
    {
        try
        {
            string postpone = ""; int postpone_tot = 0; int postpone_num = 0;
            string sSystemDate = bll.vLookUp("select [dbo].[fn_getsystemdate]('" + Request.Cookies["sp"].Value.ToString() + "')");
            postpone_tot = int.Parse(bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='postponereceived'"));
            postpone = bll.vLookUp("select DATEADD(DAY," + postpone_tot + ",postpone_dt) from tdosalesinvoice_received where inv_no='" + hdinv.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            //postpone_num = int.Parse(bll.vLookUp("select isnull(postpone,0) from tdosalesinvoice_received where inv_no='" + hdinv.Value.ToString() + "'"));
            DateTime dtSystemDate = DateTime.Parse(sSystemDate);

            DateTime last_date = new DateTime(dtSystemDate.Year, dtSystemDate.Month, DateTime.DaysInMonth(dtSystemDate.Year, dtSystemDate.Month));

            if (dtSystemDate == last_date)
            {
                btpostpone.Visible = true;
            }
            else
            {
                if (postpone == "")
                {
                    btpostpone.Visible = false;
                }
                else
                {
                    DateTime dtPostponeDate = DateTime.Parse(postpone);
                    if ((dtPostponeDate - dtSystemDate).Days <= postpone_tot)
                    {
                        btpostpone.Visible = true;
                    }
                    else
                    {
                        btpostpone.Visible = false;
                    }
                }
            }

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vDelWrkDriver(arr);
            bll.vDelWrkDoSalesDtl(arr);
            bll.vDelWrkDoSalesFree(arr);
            bll.vDelWrkDoSalesCash(arr);
            arr.Clear();

            // Check Status Invoice Driver Receive / Customer Receive 
            statusInvoice = bll.vLookUp("select (case a.dosales_sta_id when 'D' then 'D' when 'W' then 'W' else isnull(b.so_sta_id,c.so_sta_id) end) as so_sta_id from tmst_dosales a left join tmst_salesorder b on a.so_cd = b.so_cd and a.salespointcd=b.salespointcd left join tmst_canvasorder c on a.so_cd = c.so_cd and a.salespointcd=b.salespointcd where a.inv_no='" + hdinv.Value.ToString() + "' and a.salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");

            // Check Payment Status this invoice 
            statusPayment = Convert.ToBoolean(bll.vLookUp("select case when exists(select a.inv_no from tmst_dosales a inner join tpayment_dtl b on a.inv_no=b.inv_no and a.salespointcd=b.salespointcd where a.inv_no = '" + hdinv.Value.ToString() + "' and a.salespointcd='" + Request.Cookies["sp"].Value.ToString() + "') then 'true' else 'false' end "));

            // Check Periode Wazaran 
            monthNow = Convert.ToDateTime(bll.vLookUp("select dbo.fn_getsystemdate('" + Request.Cookies["sp"].Value.ToString() + "')")).Month;

            System.Data.SqlClient.SqlDataReader rs = null;
            arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vGetdosalesdtlbyinvoice(ref rs, arr);
            while (rs.Read())
            {
                lbinvno.Text = rs["inv_no"].ToString();
                lbcust.Text = rs["cust_desc"].ToString();
                lbsalesman.Text = rs["emp_desc"].ToString();
                lbinvdate.Text = Convert.ToDateTime(rs["inv_dt"]).ToString("d/M/yyyy");
                lbdeliverydate.Text = Convert.ToDateTime(rs["do_dt"]).ToString("d/M/yyyy");
                monthInv = Convert.ToDateTime(rs["inv_dt"]).Month;
            }
            rs.Close();

            arr.Clear();
            arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            if (statusInvoice != "W")
            {
                bll.vInsertWrkDosalesDtl(arr);
                bll.vInsertWrkDosalesFree(arr);
                bll.vInsertWrkDosalesCash(arr);

            }
            vInitGrid();

            double dTotReceived = 0;
            Label lbtotqtyshipment = (Label)grd.FooterRow.FindControl("lbtotqtyshipment");
            if (grdfree.Rows.Count > 0)
            {
                Label lbtotreceived = (Label)grdfree.FooterRow.FindControl("lbtotreceived");
                dTotReceived = Double.Parse(lbtotreceived.Text);
                freeGoods.Attributes.Remove("style");
            }
            else
            {
                dTotReceived = 0;
                freeGoods.Attributes.Add("style", "display:none");
            }

            if (grdcash.Rows.Count > 0)
            {
                freeCash.Attributes.Remove("style");
            }
            else
            {
                freeCash.Attributes.Add("style", "display:none");
            }

            double dReceived = Double.Parse(lbtotqtyshipment.Text) + dTotReceived;
            totDelivered = Double.Parse(lbtotqtyshipment.Text) + dTotReceived;
            totReceived = Double.Parse(lbtotqtyshipment.Text) + dTotReceived;
            lbdelivery.Text = dReceived.ToString();

            // Add Driver 
            arr.Clear();
            arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@qty", lbdelivery.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertWrkDoDriver(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grddriver, "sp_twrk_driver_get", arr);
            double dBring = 0;
            foreach (GridViewRow row in grddriver.Rows)
            {
                Label lbqtydriver = (Label)row.FindControl("lbqtydriver");
                dBring += Double.Parse(lbqtydriver.Text);
            }
            if (grddriver.Rows.Count > 0)
            {
                Label lbtotqty2 = (Label)grddriver.FooterRow.FindControl("lbtotqty");
                lbtotqty2.Text = dBring.ToString();
            }

            if (statusInvoice == "D")
            {
                lbtitle.Text = "Customer Goods Received";
                grd.Columns[7].Visible = true;
                grdfree.Columns[7].Visible = true;
                grdcash.Columns[6].Visible = true;
                grdcash.Columns[5].Visible = true;
                btreceived.Visible = true;
                btdriver.Visible = false;
                btprint.Visible = true;
                btconfirm.Visible = false;
                confirmReason.Attributes.Add("style", "display:none");
            }
            else if (statusInvoice == "C")
            {
                lbtitle.Text = "Driver Goods Received";
                grd.Columns[7].Visible = false;
                grdfree.Columns[7].Visible = false;
                grdcash.Columns[6].Visible = false;
                grdcash.Columns[5].Visible = false;
                btreceived.Visible = false;
                btdriver.Visible = true;
                btprint.Visible = false;
                btconfirm.Visible = false;
                btpostpone.Visible = false;
                btdriver.Text = "RECEIVED DRIVER";
                confirmReason.Attributes.Add("style", "display:none");
            }
            else
            {
                lbtitle.Text = "Invoice Received Confirmation";
                grd.Columns[7].Visible = false;
                grdfree.Columns[7].Visible = false;
                grdcash.Columns[6].Visible = false;
                grdcash.Columns[5].Visible = false;
                grddriver.Columns[3].Visible = false;
                grddriver.Columns[4].Visible = false;
                btreceived.Visible = false;
                btdriver.Visible = false;
                btprint.Visible = false;
                btconfirm.Visible = true;
                btpostpone.Visible = false;
                confirmReason.Attributes.Remove("style");
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salesreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            // Postpone / Edit Invoice 
            string postpone = ""; int postpone_tot = 0; string inv_dt = "";
            DateTime dtSystemDate; DateTime dtInvDate; DateTime dtPostponeDate;
            postpone = bll.vLookUp("select DATEADD(DAY," + postpone_tot + ",postpone_dt) from tdosalesinvoice_received where inv_no='" + hdinv.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            postpone_tot = int.Parse(bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='postponereceived'"));
            inv_dt = bll.vLookUp("select inv_dt from tdosales_invoice where inv_no='" + hdinv.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            string sSystemDate = bll.vLookUp("select [dbo].[fn_getsystemdate]('" + Request.Cookies["sp"].Value.ToString() + "')");
            dtSystemDate = DateTime.Parse(sSystemDate);
            dtInvDate = DateTime.Parse(inv_dt);

            // Postpone / Edit Invoice

            if (bll.nCheckAccess("invedit", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To edit this invoice, contact Administrator !!','warning');", true);
                return;
            }

            if (statusPayment == true)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not edit because this invoice have payment.','To edit this invoice, contact Administrator !!','warning');", true);
                return;
            }

            if (dtSystemDate.Month != dtInvDate.Month)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not edit because different Period.','To edit this invoice, contact Administrator !!','warning');", true);
                return;
            }

            grdfree.EditIndex = -1;
            grd.EditIndex = e.NewEditIndex;
            txqty.CssClass = cd.csstextro;
            vInitGrid();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salesreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            if (bll.nCheckAccess("invedit", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To edit this invoice, contact Administrator !!','warning');", true);
                return;
            }

            if (monthInv != monthNow)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not edit because different Period.','To edit this invoice, contact Administrator !!','warning');", true);
                return;
            }

            if (statusPayment == true)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not edit because this invoice have payment.','To edit this invoice, contact Administrator !!','warning');", true);
                return;
            }

            double dRow = 0;
            foreach (GridViewRow row in grd.Rows)
            {
                if (((row.RowState & DataControlRowState.Edit) > 0) && (row.RowType == DataControlRowType.DataRow))
                {
                    TextBox txqtyupd = (TextBox)row.FindControl("txqty");
                    Label lbqty = (Label)row.FindControl("lbqty");
                    Label lbitemcode = (Label)row.FindControl("lbitemcode");
                    if (!double.TryParse(txqtyupd.Text, out dRow))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty Received!','Please put numeric in quantity received','warning');", true);
                        return;
                    }
                    if (Double.Parse(lbqty.Text) < Double.Parse(txqtyupd.Text))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty Received problem!','Qty Received can not bigger than Qty order','warning');", true);
                        return;
                    }
                    List<cArrayList> arr = new List<cArrayList>();
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@qty", txqtyupd.Text));
                    arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vUpdateWrkDosalesDtl(arr);
                    txqtyupd.CssClass = cd.csstext;
                }
            }


            grd.EditIndex = -1;
            vInitGrid();
            Label lbtotqtyshipment = (Label)grd.FooterRow.FindControl("lbtotqtyshipment");
            if (grdfree.Rows.Count > 0)
            {
                Label lbtotreceived = (Label)grdfree.FooterRow.FindControl("lbtotreceived");
                lbdelivery.Text = (Double.Parse(lbtotqtyshipment.Text) + Double.Parse(lbtotreceived.Text)).ToString();
            }
            else
            {
                lbdelivery.Text = lbtotqtyshipment.Text;
            }

            totReceived = (Double.Parse(lbdelivery.Text));
            txqty.CssClass = cd.csstext;

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salesreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        vInitGrid();
        txqty.CssClass = cd.csstext;

    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        try
        {
            string driver = "";

            driver = bll.vLookUp("select * from twrk_driver where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and emp_cd='" + cbdriver.SelectedValue.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");

            if (driver != "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Driver already exist!','Driver exist.','warning');", true);
                return;
            }

            if (grd.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Pls select invoice!','Invoice is not selected','warning');", true);
                return;
            }
            double dCheck = 0; double dTotQty = 0; double dQtyShipment = 0;
            Label lbtotqtyshipment = (Label)grd.FooterRow.FindControl("lbtotqtyshipment");
            dQtyShipment = Double.Parse(lbdelivery.Text);
            if (grddriver.Rows.Count > 0)
            {
                Label lbtotqty = (Label)grddriver.FooterRow.FindControl("lbtotqty");
                dTotQty = Double.Parse(lbtotqty.Text);
            }
            if (!double.TryParse(txqty.Text, out dCheck))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty must numeric!','Pls input numeric in qty field','warning');", true);
                return;
            }
            if ((dCheck + dTotQty) > dQtyShipment)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty can not bigger than total shipment!','Pls qty correctly','warning');", true);
                return;
            }



            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@emp_cd", cbdriver.SelectedValue.ToString()));
            arr.Add(new cArrayList("@qty", txqty.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertWrkDriver(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grddriver, "sp_twrk_driver_get", arr);
            double dBring = 0;
            foreach (GridViewRow row in grddriver.Rows)
            {
                Label lbqtydriver = (Label)row.FindControl("lbqtydriver");
                dBring += Double.Parse(lbqtydriver.Text);
            }
            Label lbtotqty2 = (Label)grddriver.FooterRow.FindControl("lbtotqty");
            lbtotqty2.Text = dBring.ToString();
            //grd.CssClass = "mGrid ro";
            //grdfree.CssClass = "mGrid ro";

            txqty.Text = "";

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salesreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void grddriver_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            string stBin = "";
            stBin = bll.vLookUp("select bin_cd from tmst_dosales where inv_no='" + hdinv.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            if (stBin == "FA")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('From BIN Factory, Can not Deleted!!! (Edit only Qty)','Driver Can not Deleted','warning');", true);
                return;
            }
            else
            {
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vDelWrkDriver(arr);
                bll.vBindingGridToSp(ref grddriver, "sp_twrk_driver_get", arr);
                double dBring = 0;
                foreach (GridViewRow row in grddriver.Rows)
                {
                    Label lbqtydriver = (Label)row.FindControl("lbqtydriver");
                    dBring += Double.Parse(lbqtydriver.Text);
                }
                if (grddriver.Rows.Count > 0)
                {
                    Label lbtotqty2 = (Label)grddriver.FooterRow.FindControl("lbtotqty");
                    lbtotqty2.Text = dBring.ToString();
                }
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salesreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btreceived_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            double dCheck = 0; double dTotQty = 0; double dQtyShipment = 0;
            string sCheck = "";
            if (grddriver.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Driver must be specify!','Add driver to received','warning');", true);
                return;
            }
            Label lbtotqty = (Label)grddriver.FooterRow.FindControl("lbtotqty");
            dQtyShipment = Double.Parse(lbdelivery.Text);
            if (Double.Parse(lbtotqty.Text) != dQtyShipment)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty received must be same with total received!','Pls qty correctly driver bring out','warning');", true);
                return;
            }

            double dTemp = Double.Parse(bll.vLookUp("select sum(qty) from twrk_dosalesdtl where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'"));
            double dInv = Double.Parse(bll.vLookUp("select sum(qty) from tdosalesinvoice_dtl where inv_no='" + hdinv.Value.ToString() + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'"));

            if (rddisc.SelectedValue.ToString() == "A")
            {
                sCheck = bll.vLookUp("select dbo.fn_checkdochanged('" + hdinv.Value.ToString() + "','" + Request.Cookies["usr_id"].Value.ToString() + "','"+ Request.Cookies["sp"].Value.ToString() + "')");
                if (sCheck != "ok")
                {
                    if (monthInv != monthNow)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not edit because different Period.','To edit this invoice, contact Administrator !!','warning');", true);
                        return;
                    }
                    string sDiscount = bll.vLookUp("select dbo.fn_checkdodiscount('" + hdinv.Value.ToString() + "','" + Request.Cookies["usr_id"].Value.ToString() + "','"+ Request.Cookies["sp"].Value.ToString() + "')");
                    if (sDiscount == "ok")
                    {
                        arr.Clear();
                        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        bll.vUpdateDOSalesDtlFromWRK2(arr);

                        btreceived.CssClass = "btn btn-default ro";
                        btsearch.CssClass = "btn btn-default ro";
                        btprint.CssClass = "btn btn-default ro";
                        btconfirm.CssClass = "btn btn-success";

                        lbtitle.Text = "Invoice Received";
                        grd.Columns[7].Visible = true;
                        grdfree.Columns[7].Visible = true;
                        grdcash.Columns[6].Visible = true;
                        btreceived.Visible = false;
                        btdriver.Visible = false;
                        btprint.Visible = false;
                        btconfirm.Visible = true;
                        btcancel.Visible = true;
                        btprintedit.Visible = false;

                        // Sent Email/SMS into BranchSupervisor 
                        //int nrnd;
                        //Random rnd = new Random();
                        //nrnd = rnd.Next(1000, 9999);

                        //List<string> lapproval = bll.lGetApproval("invapp", 1);
                        //string sMsg = "#Invoice No. (" + hdinv.Value.ToString() + ") from " + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + ", has been edited, do you want to approved : (Y/N)" + nrnd.ToString();
                        //arr.Clear();
                        //arr.Add(new cArrayList("@token", nrnd.ToString()));
                        //arr.Add(new cArrayList("@doc_typ", "invapp"));
                        //arr.Add(new cArrayList("@to", lapproval[0]));
                        //arr.Add(new cArrayList("@doc_no", hdinv.Value.ToString()));
                        //arr.Add(new cArrayList("@msg", sMsg));
                        //bll.vInsertSmsOutbox(arr);

                        //string sSubject = ""; string sMessage = "";
                        //sSubject = "Invoice Confirmation Branch " + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                        //sMessage = "Invoice No. " + hdinv.Value.ToString() + " edited by " + Request.Cookies["usr_id"].Value.ToString() + ", the following invoice was edited and need approval to received this invoice. \n\r";

                        //arr.Clear();
                        //System.Data.SqlClient.SqlDataReader rs = null;
                        //arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
                        //arr.Add(new cArrayList("@usr_id", hdinv.Value.ToString()));

                        //// Detail Order 
                        //if (grd.Rows.Count > 0)
                        //{

                        //    bll.vGetInvoiceConfirmDtl(arr, ref rs);

                        //    sMessage += "Detail Orders \n\r";

                        //    sMessage += "<table><tr style='background-color:silver'><td>Item Code</td><td>Item Name</td><td>Size</td><td>Branded</td><td>Quantity Order</td><td>Quantity Received</td><td>UOM</td></tr>";

                        //    while (rs.Read())
                        //    {
                        //        sMessage += "<tr><td></td>" + rs["item_cd"].ToString() + "</td><td>" + rs["item_nm"] + "</td><td>" + rs["size"] + "</td><td>" + rs["branded_nm"] + "</td><td>" + rs["qtyorder"].ToString() + "</td><td>" + rs["qty"] + "</td><td>" + rs["uom"] + "</td></tr>";
                        //    }
                        //    rs.Close();

                        //    sMessage += "</table>";
                        //}

                        //// Free Goods 

                        //if (grdfree.Rows.Count > 0)
                        //{
                        //    bll.vGetInvoiceConfirmFree(arr, ref rs);

                        //    sMessage += "\n\r Free Goods \n\r";

                        //    sMessage += "<table><tr style='background-color:silver'><td>Item Code</td><td>Item Name</td><td>Size</td><td>Branded</td><td>Quantity Free</td><td>Quantity Received</td><td>UOM</td></tr>";

                        //    while (rs.Read())
                        //    {
                        //        sMessage += "<tr><td></td>" + rs["item_cd"].ToString() + "</td><td>" + rs["item_nm"] + "</td><td>" + rs["size"] + "</td><td>" + rs["branded_nm"] + "</td><td>" + rs["qtyfree"].ToString() + "</td><td>" + rs["qty"] + "</td><td>" + rs["uom"] + "</td></tr>";
                        //    }
                        //    rs.Close();

                        //    sMessage += "</table>";
                        //}

                        //// Free Value / Discount 

                        //if (grdcash.Rows.Count > 0)
                        //{
                        //    bll.vGetInvoiceConfirmCash(arr, ref rs);

                        //    sMessage += "\n\r Discount \n\r";

                        //    sMessage += "<table><tr style='background-color:silver'><td>Item Code</td><td>Item Name</td><td>Size</td><td>Branded</td><td>Discount</td><td>Discount Received</td></tr>";

                        //    while (rs.Read())
                        //    {
                        //        sMessage += "<tr><td></td>" + rs["item_cd"].ToString() + "</td><td>" + rs["item_nm"] + "</td><td>" + rs["size"] + "</td><td>" + rs["branded_nm"] + "</td><td>" + rs["discount"].ToString() + "</td><td>" + rs["amt"] + "</td></tr>";
                        //    }
                        //    rs.Close();

                        //    sMessage += "</table>";
                        //}

                        ////sMessage +=
                        ////"<p> Please Click this  for approved : <a href='" + bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='link_branch'") + "/landingpage.aspx?trnname=invconfirm&salespointcd=" + Request.Cookies["sp"].Value.ToString() + "&RefNo=" + hdinv.Value.ToString() + "&appcode=" + nrnd.ToString() + "&sta=A'>Approve</a>, or for rejected please click <a href='" + bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='link_branch'") + "/landingpage.aspx?trnname=invconfirm&salespointcd=" + Request.Cookies["sp"].Value.ToString() + "&RefNo=" + hdinv.Value.ToString() + "&appcode=" + nrnd.ToString() + "&sta=R'>Reject</a></p>" +
                        ////        " \n\r\n\r\n\r\n Wazaran Admin";

                        //sMessage +=
                        //"<p> Please Click this  for approved : <a href='" + bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='link_branch'") + "/landingpage2.aspx?src=invconfirm&ids=" + hdinv.Value.ToString() + "&sta=A'>Approve</a>, or for rejected please click <a href='" + bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='link_branch'") + "/landingpage2.aspx?src=invconfirm&ids=" + hdinv.Value.ToString() + "&sta=R'>Reject</a></p>" +
                        //        " \n\r\n\r\n\r\n Wazaran Admin";

                        //arr.Clear();
                        //arr.Add(new cArrayList("@token", nrnd.ToString()));
                        //arr.Add(new cArrayList("@doc_typ", "InvoiceConfirm"));
                        //arr.Add(new cArrayList("@to", lapproval[1]));
                        //arr.Add(new cArrayList("@doc_no", hdinv.Value.ToString()));
                        //arr.Add(new cArrayList("@emailsubject", sSubject));
                        //arr.Add(new cArrayList("@msg", sMessage));
                        //arr.Add(new cArrayList("@file_attachment", null));
                        //bll.vInsertEmailOutbox(arr);     

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Invoice need confirmation!','" + hdinv.Value.ToString() + "','success');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Free Qty / Free Discount Must be edit, because this order qty not suitable with this scheme.','Pls edit in qty field','warning');", true);
                        return;
                    }
                }
                else
                {
                    string sDiscount = bll.vLookUp("select dbo.fn_checkdoorder('" + hdinv.Value.ToString() + "','" + Request.Cookies["usr_id"].Value.ToString() + "','"+ Request.Cookies["sp"].Value.ToString() +"')");
                    if (sDiscount == "ok")
                    {
                        arr.Clear();
                        arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
                        arr.Add(new cArrayList("@received_dt", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                        arr.Add(new cArrayList("@received_by", Request.Cookies["usr_id"].Value.ToString()));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        bll.vInsertDosalesInvoiceReceived(arr);

                        arr.Clear();
                        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        bll.vUpdateDOSalesDtlFromWRK(arr);

                        // IAG : Project Accounting : 4 Nov 2017 : Purposed : Insert Stock Card table 
                        arr.Clear();
                        arr.Add(new cArrayList("@refno", hdinv.Value.ToString()));
                        arr.Add(new cArrayList("@stockcard_typ", "SALES"));
                        //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        bll.vBatchStockCard(arr);

                        if (grdfree.Rows.Count > 0)
                        {
                            arr.Clear();
                            arr.Add(new cArrayList("@refno", hdinv.Value.ToString()));
                            arr.Add(new cArrayList("@stockcard_typ", "SALESFREE"));
                            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                            bll.vBatchStockCard(arr);
                        }

                        //autoPayment(Request.Cookies["usr_id"].Value.ToString(), hdinv.Value.ToString(), Request.Cookies["sp"].Value.ToString());

                        btreceived.CssClass = "btn btn-default ro";
                        btsearch.CssClass = "btn btn-default ro";
                        btprint.CssClass = "btn btn-success";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Invoice has been received!','" + hdinv.Value.ToString() + "','success');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Total Qty Order and Shipment Different, Please Contact Wazaran Administrator.','Pls contact wazaran Admin','warning');", true);
                        return;
                    }
                }
            }
            else
            {
                sCheck = bll.vLookUp("select dbo.fn_checkdochanged('" + hdinv.Value.ToString() + "','" + Request.Cookies["usr_id"].Value.ToString() + "','"+ Request.Cookies["sp"].Value.ToString() + "')");
                if (sCheck != "ok")
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vUpdateDOSalesDtlFromWRK2(arr);

                    btreceived.CssClass = "btn btn-default ro";
                    btsearch.CssClass = "btn btn-default ro";
                    btprint.CssClass = "btn btn-default ro";
                    btconfirm.CssClass = "btn btn-success";

                    lbtitle.Text = "Invoice Received";
                    grd.Columns[7].Visible = true;
                    grdfree.Columns[7].Visible = true;
                    grdcash.Columns[6].Visible = true;
                    btreceived.Visible = false;
                    btdriver.Visible = false;
                    btprint.Visible = false;
                    btconfirm.Visible = true;
                    btcancel.Visible = true;
                    btprintedit.Visible = false;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Invoice need confirmation!','" + hdinv.Value.ToString() + "','success');", true);
                }
                else
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
                    arr.Add(new cArrayList("@received_dt", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                    arr.Add(new cArrayList("@received_by", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vInsertDosalesInvoiceReceived(arr);

                    arr.Clear();
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vUpdateDOSalesDtlFromWRK3(arr);

                    // IAG : Project Accounting : 4 Nov 2017 : Purposed : Insert Stock Card table 
                    arr.Clear();
                    arr.Add(new cArrayList("@refno", hdinv.Value.ToString()));
                    arr.Add(new cArrayList("@stockcard_typ", "SALES"));
                    bll.vBatchStockCard(arr);

                    if (grdfree.Rows.Count > 0)
                    {
                        arr.Clear();
                        arr.Add(new cArrayList("@refno", hdinv.Value.ToString()));
                        arr.Add(new cArrayList("@stockcard_typ", "SALESFREE"));
                        bll.vBatchStockCard(arr);
                    }

                    btreceived.CssClass = "btn btn-default ro";
                    btsearch.CssClass = "btn btn-default ro";
                    btprint.CssClass = "btn btn-success";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Invoice has been received!','" + hdinv.Value.ToString() + "','success');", true);
                }
            }

            //if (dTemp != dInv)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Change','chnge','warning');", true);
            //    return;
            //}
            //if (!double.TryParse(txqty.Text, out dCheck))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty must numeric!','Pls input numeric in qty field','warning');", true);
            //    return;
            //}
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salesreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_salesreceipt2.aspx");
    }

    protected void grdfree_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            // Postpone / Edit Invoice 
            string postpone = ""; int postpone_tot = 0; string inv_dt = "";
            DateTime dtSystemDate; DateTime dtInvDate; DateTime dtPostponeDate;
            postpone = bll.vLookUp("select DATEADD(DAY," + postpone_tot + ",postpone_dt) from tdosalesinvoice_received where inv_no='" + hdinv.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            postpone_tot = int.Parse(bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='postponereceived'"));
            inv_dt = bll.vLookUp("select inv_dt from tdosales_invoice where inv_no='" + hdinv.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            string sSystemDate = bll.vLookUp("select [dbo].[fn_getsystemdate]('" + Request.Cookies["sp"].Value.ToString() + "')");
            dtSystemDate = DateTime.Parse(sSystemDate);
            dtInvDate = DateTime.Parse(inv_dt);

            // Postpone / Edit Invoice

            if (bll.nCheckAccess("invedit", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To edit this invoice, contact Administrator !!','warning');", true);
                return;
            }

            if (dtSystemDate.Month != dtInvDate.Month)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not edit because different Period.','To edit this invoice, contact Administrator !!','warning');", true);
                return;
            }

            if (statusPayment == true)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not edit because this invoice have payment.','To edit this invoice, contact Administrator !!','warning');", true);
                return;
            }

            grdfree.EditIndex = -1;
            grdfree.EditIndex = e.NewEditIndex;
            vInitGrid();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salesreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdfree_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        grdfree.EditIndex = -1;
        vInitGrid();
    }
    protected void grdfree_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            if (bll.nCheckAccess("invedit", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To edit this invoice, contact Administrator !!','warning');", true);
                return;
            }

            if (monthInv != monthNow)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not edit because different Period.','To edit this invoice, contact Administrator !!','warning');", true);
                return;
            }

            if (statusPayment == true)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not edit because this invoice have payment.','To edit this invoice, contact Administrator !!','warning');", true);
                return;
            }

            double dRow = 0;
            foreach (GridViewRow row in grdfree.Rows)
            {
                if (((row.RowState & DataControlRowState.Edit) > 0) && (row.RowType == DataControlRowType.DataRow))
                {
                    TextBox txqtyupd = (TextBox)row.FindControl("txqty");
                    Label lbqtyfree = (Label)row.FindControl("lbqtyfree");
                    Label lbitemcode = (Label)row.FindControl("lbitemcode");
                    HiddenField disc_cd = (HiddenField)row.FindControl("hddiscount");
                    if (!double.TryParse(txqtyupd.Text, out dRow))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty Received!','Please put numeric in quantity received','warning');", true);
                        return;
                    }
                    if (Double.Parse(lbqtyfree.Text) < Double.Parse(txqtyupd.Text))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty Received problem!','Qty Received can not bigger than Qty free','warning');", true);
                        return;
                    }
                    List<cArrayList> arr = new List<cArrayList>();
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@qty", txqtyupd.Text));
                    arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
                    arr.Add(new cArrayList("@disc_cd", disc_cd.Value.ToString()));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vUpdateWrkDosalesFree(arr);
                    txqtyupd.CssClass = cd.csstext;
                }
            }

            grdfree.EditIndex = -1;
            vInitGrid();
            Label lbtotqtyshipment = (Label)grd.FooterRow.FindControl("lbtotqtyshipment");
            if (grdfree.Rows.Count > 0)
            {
                Label lbtotreceived = (Label)grdfree.FooterRow.FindControl("lbtotreceived");
                lbdelivery.Text = (Double.Parse(lbtotqtyshipment.Text) + Double.Parse(lbtotreceived.Text)).ToString();
            }
            else
            {
                lbdelivery.Text = lbtotqtyshipment.Text;
            }
            totReceived = (Double.Parse(lbdelivery.Text));
            txqty.CssClass = cd.csstext;

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salesreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        try
        {
            if (bll.nCheckAccess("invtoc", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('You dont have access','Print invoice Take Order !!','warning');", true);
                return;
            }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertRptDosalesInvoice(arr);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport1('fm_report2.aspx?src=invto&no=" + hdinv.Value.ToString() + "','fm_report2.aspx?src=invto1&no=" + hdinv.Value.ToString() + "');", true); return;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=invto&no=" + hdinv.Value.ToString() + "')", true);
            btprint.CssClass = "btn btn-default ro";

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salesreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btdriver_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            if (btdriver.Text == "RECEIVED DRIVER")
            {
                if (grddriver.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Driver must be specify!','Add driver to received','warning');", true);
                    return;
                }
                arr.Clear();
                arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@received_dt", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vInsertDoDriverInvoiceReceived(arr);

                //arr.Clear();
                //arr.Add(new cArrayList("@refno", hdinv.Value.ToString()));
                //arr.Add(new cArrayList("@stockcard_typ", "ONDELIVERY"));
                //bll.vBatchStockCard(arr);

                //if (grdfree.Rows.Count > 0)
                //{
                //    arr.Clear();
                //    arr.Add(new cArrayList("@refno", hdinv.Value.ToString()));
                //    arr.Add(new cArrayList("@stockcard_typ", "ONDELIVERYFREE"));
                //    bll.vBatchStockCard(arr);
                //}

                btdriver.Text = "RECEIVED CUSTOMER";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Invoice has been driver received!','" + hdinv.Value.ToString() + "','success');", true);
            }
            else
            {
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vDelWrkDriver(arr);
                bll.vDelWrkDoSalesDtl(arr);
                bll.vDelWrkDoSalesFree(arr);

                btdriver.Visible = false;
                btinv_Click(sender, e);
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salesreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdcash_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            grdcash.EditIndex = -1;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdcash, "sp_twrk_dosalescash_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salesreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdcash_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            // Postpone / Edit Invoice 
            string postpone = ""; int postpone_tot = 0; string inv_dt = "";
            DateTime dtSystemDate; DateTime dtInvDate; DateTime dtPostponeDate;
            postpone = bll.vLookUp("select DATEADD(DAY," + postpone_tot + ",postpone_dt) from tdosalesinvoice_received where inv_no='" + hdinv.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            postpone_tot = int.Parse(bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='postponereceived'"));
            inv_dt = bll.vLookUp("select inv_dt from tdosales_invoice where inv_no='" + hdinv.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            string sSystemDate = bll.vLookUp("select [dbo].[fn_getsystemdate]('" + Request.Cookies["sp"].Value.ToString() + "')");
            dtSystemDate = DateTime.Parse(sSystemDate);
            dtInvDate = DateTime.Parse(inv_dt);

            // Postpone / Edit Invoice


            if (bll.nCheckAccess("invedit", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To edit this invoice, contact Administrator !!','warning');", true);
                return;
            }
            if (dtSystemDate.Month != dtInvDate.Month)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not edit because different Period.','To edit this invoice, contact Administrator !!','warning');", true);
                return;
            }
            if (statusPayment == true)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not edit because this invoice have payment.','To edit this invoice, contact Administrator !!','warning');", true);
                return;
            }

            grdcash.EditIndex = -1;
            grdcash.EditIndex = e.NewEditIndex;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdcash, "sp_twrk_dosalescash_get", arr);
            //txdiscount.CssClass = cd.csstextro;

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salesreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdcash_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            if (bll.nCheckAccess("invedit", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To edit this invoice, contact Administrator !!','warning');", true);
                return;
            }
            if (monthInv != monthNow)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not edit because different Period.','To edit this invoice, contact Administrator !!','warning');", true);
                return;
            }
            if (statusPayment == true)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not edit because this invoice have payment.','To edit this invoice, contact Administrator !!','warning');", true);
                return;
            }

            double dRow = 0;
            List<cArrayList> arr = new List<cArrayList>();
            foreach (GridViewRow row in grdcash.Rows)
            {
                if (((row.RowState & DataControlRowState.Edit) > 0) && (row.RowType == DataControlRowType.DataRow))
                {
                    TextBox txdiscupd = (TextBox)row.FindControl("txdiscount");
                    HiddenField disc_cd = (HiddenField)row.FindControl("hddiscount");
                    Label lbdiscount = (Label)row.FindControl("lbdiscount");
                    Label lbitemcode = (Label)row.FindControl("lbitemcode");
                    if (!double.TryParse(txdiscupd.Text, out dRow))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Discount Received!','Please put numeric in quantity received','warning');", true);
                        return;
                    }
                    if (Double.Parse(lbdiscount.Text) < Double.Parse(txdiscupd.Text))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Discount Received problem!','Discount Received can not bigger than free Discount','warning');", true);
                        return;
                    }
                    arr.Clear();
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@amt", txdiscupd.Text));
                    arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
                    arr.Add(new cArrayList("@disc_cd", disc_cd.Value.ToString()));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vUpdateWrkDosalesCash(arr);
                    txdiscupd.CssClass = cd.csstext;
                }
            }
            grdcash.EditIndex = -1;
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdcash, "sp_twrk_dosalescash_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salesreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btconfirm_Click(object sender, EventArgs e)
    {
        try { 
            if (bll.nCheckAccess("invapp", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To approve this invoice, contact Administrator !!','warning');", true);
                return;
            }

            if (cbreason.SelectedValue.ToString() == "select")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Select the reason for confirm this invoice','Select Reason for Confirm !!','warning');", true);
                return;
            }

            if (Double.Parse(lbdelivery.Text) == 0.0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Qty can not be 0 CTN.','Cancel This Invoice To Delete Edit Qty!!','warning');", true);
                return;
            }

            List<cArrayList> arr = new List<cArrayList>();

            hdconfirm.Value = "false";

            if (hdconfirm.Value.ToString() == "true")
            {
                // IAG : Project Accounting : 4 Nov 2017 : Purposed : Insert Stock Card table 
                // FR : Project Accounting : 6 Mei 2018 : Purposed : Double in Stock Card table
                //arr.Clear();
                //arr.Add(new cArrayList("@refno", hdinv.Value.ToString()));
                //arr.Add(new cArrayList("@stockcard_typ", "SALES"));
                //bll.vBatchStockCard(arr);

                arr.Clear();
                arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
                arr.Add(new cArrayList("@received_dt", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@received_by", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@reasn_cd", cbreason.SelectedValue.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vInsertDosalesInvoiceReceived(arr);

                arr.Clear();
                arr.Add(new cArrayList("@refno", hdinv.Value.ToString()));
                arr.Add(new cArrayList("@stockcard_typ", "SALESCANCEL"));
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBatchStockCard(arr);
            
                if (grdfree.Rows.Count > 0)
                {
                    // FR : Project Accounting : 6 Mei 2018 : Purposed : Double in Stock Card table
                    //arr.Clear();
                    //arr.Add(new cArrayList("@refno", hdinv.Value.ToString()));
                    //arr.Add(new cArrayList("@stockcard_typ", "SALESFREE"));
                    //bll.vBatchStockCard(arr);

                    arr.Clear();
                    arr.Add(new cArrayList("@refno", hdinv.Value.ToString()));
                    arr.Add(new cArrayList("@stockcard_typ", "SALESFREECANCEL"));
                    //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vBatchStockCard(arr);

                }   

                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vUpdateDOSalesDtlFromWRK3(arr);

                if (grdfree.Rows.Count > 0)
                {

                    arr.Clear();
                    arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vInsertsalesorderfreebyitemcancel(arr);

                }


                btreceived.CssClass = "btn btn-default ro";
                btsearch.CssClass = "btn btn-default ro";
                btcancel.CssClass = "btn btn-default ro";
                //btprint.CssClass = "btn btn-success";
                btprintedit.CssClass = "btn btn-success";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Invoice has been received!','" + hdinv.Value.ToString() + "','success');", true);
            }
            else
            {
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vUpdateDOSalesDtlFromWRK4(arr);

                btconfirm.CssClass = "btn btn-default ro";
                btsearch.CssClass = "btn btn-default ro";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Invoice has not been received!','" + hdinv.Value.ToString() + "','warning');", true);
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salesreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grddriver_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            grddriver.EditIndex = -1;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grddriver, "sp_twrk_driver_get", arr);
            double dBring = 0;
            foreach (GridViewRow row in grddriver.Rows)
            {
                Label lbqtydriver = (Label)row.FindControl("lbqtydriver");
                dBring += Double.Parse(lbqtydriver.Text);
            }
            if (grddriver.Rows.Count > 0)
            {
                Label lbtotqty2 = (Label)grddriver.FooterRow.FindControl("lbtotqty");
                lbtotqty2.Text = dBring.ToString();
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salesreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grddriver_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            //grddriver.EditIndex = -1;
            grddriver.EditIndex = e.NewEditIndex;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grddriver, "sp_twrk_driver_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salesreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grddriver_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            double dRow = 0;
            foreach (GridViewRow row in grddriver.Rows)
            {
                if (((row.RowState & DataControlRowState.Edit) > 0) && (row.RowType == DataControlRowType.DataRow))
                {
                    TextBox txqtydriver = (TextBox)row.FindControl("txqtydriver");
                    Label lbqtydriver = (Label)row.FindControl("lbqtydriver");
                    Label lbempcode = (Label)row.FindControl("lbempcode");
                    if (!double.TryParse(txqtydriver.Text, out dRow))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty Bring by Driver!','Please put numeric in quantity Bring by Driver','warning');", true);
                        return;
                    }
                    if (Double.Parse(lbdelivery.Text) < Double.Parse(txqtydriver.Text))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty Bring problem!','Qty bring by driver can not bigger than Qty delivery','warning');", true);
                        return;
                    }
                    if (double.Parse(txqtydriver.Text) <= 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Minimum Qty Can not less than 1 (one) !','Please put numeric in quantity Bring by Driver','warning');", true);
                        return;
                    }
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@qty", txqtydriver.Text));
                    arr.Add(new cArrayList("@emp_cd", lbempcode.Text));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vUpdateWrkDriver(arr);
                    txqtydriver.CssClass = cd.csstext;
                }
            }


            grddriver.EditIndex = -1;
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grddriver, "sp_twrk_driver_get", arr);
            double dBring = 0;
            foreach (GridViewRow row in grddriver.Rows)
            {
                Label lbqtydriver = (Label)row.FindControl("lbqtydriver");
                dBring += Double.Parse(lbqtydriver.Text);
            }
            if (grddriver.Rows.Count > 0)
            {
                Label lbtotqty2 = (Label)grddriver.FooterRow.FindControl("lbtotqty");
                lbtotqty2.Text = dBring.ToString();
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salesreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btprintedit_Click(object sender, EventArgs e)
    {

        if (bll.nCheckAccess("invapp", Request.Cookies["usr_id"].Value.ToString()) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('You dont have access','Print this invoice !!','warning');", true);
            return;
        }
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport1('fm_report2.aspx?src=invto&no=" + hdinv.Value.ToString() + "','fm_report2.aspx?src=invto1&no=" + hdinv.Value.ToString() + "');", true); return;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=invedit&no=" + hdinv.Value.ToString() + "')", true);
        btprintedit.CssClass = "btn btn-default ro";
    }
    protected void btcancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (bll.nCheckAccess("invapp", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To cancel edit this invoice, contact Administrator !!','warning');", true);
                return;
            }

            List<cArrayList> arr = new List<cArrayList>();

            if (hdconfirm.Value.ToString() == "true")
            {
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vUpdateDOSalesDtlFromWRK4(arr);

                btconfirm.CssClass = "btn btn-default ro";
                btcancel.CssClass = "btn btn-default ro";
                btsearch.CssClass = "btn btn-default ro";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Invoice has not been received!','" + hdinv.Value.ToString() + "','warning');", true);
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salesreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btpostpone_Click(object sender, EventArgs e)
    {
        try
        {
            string postpone = ""; int postpone_tot = 0; int postpone_num = 0;
            string sSystemDate = bll.vLookUp("select [dbo].[fn_getsystemdate]('" + Request.Cookies["sp"].Value.ToString() + "')");
            postpone_tot = int.Parse(bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='postponereceived'"));
            postpone = bll.vLookUp("select DATEADD(DAY," + postpone_tot + ",postpone_dt) from tdosalesinvoice_received where inv_no='" + hdinv.Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            //postpone_num = int.Parse(bll.vLookUp("select isnull(postpone,0) from tdosalesinvoice_received where inv_no='" + hdinv.Value.ToString() + "'"));
            DateTime dtSystemDate = DateTime.Parse(sSystemDate);
            DateTime last_date = new DateTime(dtSystemDate.Year, dtSystemDate.Month, DateTime.DaysInMonth(dtSystemDate.Year, dtSystemDate.Month));

            if (bll.nCheckAccess("invpostpone", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To postpone this invoice, contact Administrator !!','warning');", true);
                return;
            }

            List<cArrayList> arr = new List<cArrayList>();

            if (hdconfirm.Value.ToString() == "true")
            {
                if (postpone == "")
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vUpdatePostponeInv(arr);

                    btpostpone.CssClass = "btn btn-default ro";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Invoice has been postponed!','" + hdinv.Value.ToString() + "','success');", true);
                }
                else
                {
                    DateTime dtPostponeDate = DateTime.Parse(postpone);
                    if ((dtPostponeDate - dtSystemDate).Days <= postpone_tot)
                    {
                        arr.Clear();
                        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        bll.vUpdatePostponeInv(arr);

                        btpostpone.CssClass = "btn btn-default ro";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Invoice has been postponed!','" + hdinv.Value.ToString() + "','success');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This invoice already postpone!!!','postpone this invoice, contact Administrator !!','warning');", true);
                        return;
                    }
                }

            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salesreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void autoPayment(string usrid, string inv_no, string salespointcd)
    {
        try
        {
            string stAuto = bll.sGetControlParameterSalespoint("auto_payment", Request.Cookies["sp"].Value.ToString());
            //int status_inv = int.Parse(bll.vLookUp("select isnull(paidbycash,0) from tsalesorder_info a inner join tmst_dosales b on a.so_cd = b.so_cd and a.salespointcd = b.salespointcd where b.inv_no='"+inv_no+"' and b.salespointcd='"+salespointcd+"'"));

            //if (status_inv == 1)
            //{
            //    if (stAuto == "ok")
            //    { 

            //    }
            //}
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_salesreceipt2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

}