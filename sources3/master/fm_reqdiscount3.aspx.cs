using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class fm_reqdiscount3 : System.Web.UI.Page
{
    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkCusGrCD(arr);
            bll.vDelWrkCustomer(arr);
            bll.vDelWrkCustType(arr);
            bll.vDelwrkFormulaDiscount(arr);
            bll.vDelWrkItem(arr);
            bll.vDelWrkProduct(arr);
            bll.vDelWrkSalespoint(arr);
            bll.vDelWrkFreeItem(arr);
            bll.vDelWrkFreeProduct(arr);
            bll.vDelWrkDiscountProduct(arr);

            lbsalespoint.Text = bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            bll.vBindingComboToSp(ref cbdisctyp, "sp_tpromotion_dtl_get", "promo_typ", "promotyp_nm");
            //bll.vBindingFieldValueToCombo(ref cbdisctyp, "disc_typ");
            bll.vBindingFieldValueToCombo(ref cbuom, "uom");
            bll.vBindingFieldValueToCombo(ref cbuomfree, "uom");
            bll.vBindingFieldValueToCombo(ref cbmethod, "disc_method");

            arr.Clear();
            arr.Add(new cArrayList("@level_no", 1));
            bll.vBindingComboToSp(ref cbbrandedfree, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
            cbbrandedfree_SelectedIndexChanged(sender, e);
            arr.Clear();

            txdisccode.Attributes.Add("readonly", "readonly");
            txdisccode.Text = "NEW";
            dtstart.Attributes.Add("readonly", "readonly");
            dtend.Attributes.Add("readonly", "readonly");
            //dtdelivery.Attributes.Add("readonly", "readonly");
            dtmaxclaim.Attributes.Add("readonly", "readonly");
            txvendorref.Attributes.Add("readonly", "readonly");
            txvendor.Attributes.Add("readonly", "readonly");

            cbdisctyp.Attributes.Add("readonly", "readonly");

            if (grdslspoint.DataSource == null)
            {
                grdslspoint.DataSource = new string[] { };
            }
            //if (grdviewcust.DataSource == null)
            //{
            //    grdviewcust.DataSource = new string[] { };
            //}
            if (grdviewgroup.DataSource == null)
            {
                grdviewgroup.DataSource = new string[] { };
            }
            grdslspoint.DataBind();
            //grdviewcust.DataBind();
            grdviewgroup.DataBind();

            btsave.Visible = false;

            Session["hdprop"] = "";
            rdcust_SelectedIndexChanged(sender, e);
            //rdicust.SelectedValue = "Y";
            rdicust_SelectedIndexChanged(sender, e);

            rdisalespoint_SelectedIndexChanged(sender, e);
            bll.vBindingComboToSp(ref rdsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");

        }
    }
    
    protected void btaddformula_Click(object sender, EventArgs e)
    {
        if (cbuom.SelectedValue.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('UOM can not empty','Formula Discount','warning');", true);
            return;
        }
        if (txordermin.Text.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Order Min can not empty','Formula Discount','warning');", true);
            return;
        }
        
        List<cArrayList> arr = new List<cArrayList>();
        if (rdmethod.SelectedValue.ToString() == "FG")
        {
            if (cbuomfree.SelectedValue.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('UOM Free can not empty','Formula Discount','warning');", true);
                return;
            }
            txpercent.Text = "0";
            txcash.Text = "0";
        }
        else if (rdmethod.SelectedValue.ToString() == "CH")
        {
            txqty.Text = "0";
            txpercent.Text = "0";
        }
        else if (rdmethod.SelectedValue.ToString() == "PC")
        {
            txcash.Text = "0";
            txqty.Text = "0";
        }
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@min_qty", txordermin.Text));
        arr.Add(new cArrayList("@qty", txqty.Text));
        arr.Add(new cArrayList("@amt", txcash.Text));
        arr.Add(new cArrayList("@percentage", txpercent.Text));
        arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
        arr.Add(new cArrayList("@uom_free", cbuomfree.SelectedValue.ToString()));
        arr.Add(new cArrayList("@disc_typ", cbmethod.SelectedValue.ToString()));
        bll.vInsertWrkFomulaDiscount(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdformula, "sp_twrk_formuladiscount_get", arr);
    }
    protected void rdmethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdmethod.SelectedValue.ToString() == "FG")
        {
            txcash.Attributes.Add("readonly", "readonly"); txcash.Text = "0";
            txpercent.Attributes.Add("readonly", "readonly"); txpercent.Text = "0";
            txqty.Attributes.Remove("readonly"); txqty.Text = "0";
            tbdiscount.Attributes.Remove("style");
        }
        else if (rdmethod.SelectedValue.ToString() == "CH")
        {
            txqty.Attributes.Add("readonly", "readonly"); txqty.Text = "0";
            txpercent.Attributes.Add("readonly", "readonly"); txpercent.Text = "0";
            txcash.Attributes.Remove("readonly"); txcash.Text = "0";
            tbdiscount.Attributes.Add("style", "display:none");
        }
        else if (rdmethod.SelectedValue.ToString() == "PC")
        {
            txqty.Attributes.Add("readonly", "readonly"); txqty.Text = "0";
            txcash.Attributes.Add("readonly", "readonly"); txcash.Text = "0";
            txpercent.Attributes.Remove("readonly"); txpercent.Text = "0";
            tbdiscount.Attributes.Add("style", "display:none");
        }
        string salespoint = rdisalespoint.SelectedValue.ToString() == "Y" ? "P" : "N";
        string customer = rdicust.SelectedValue.ToString() == "Y" ? "P" : "N";
        string cust = rdicust.SelectedValue.ToString() == "Y" ? "P" : rdcust.SelectedValue.ToString();
        checkDiscount(Session["hdprop"].ToString(), salespoint, customer, cust);
    }
    protected void rdfree_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vDelWrkFreeItem(arr);
        bll.vDelWrkFreeProduct(arr);
        arr.Clear();
        if (rdfree.SelectedValue.ToString() == "P")
        {
            showItemFree.Attributes.Remove("style");
            grdfreeproduct.Attributes.Add("style", "display:none");
            grdfreeitem.Attributes.Remove("style");
        }
        else if (rdfree.SelectedValue.ToString() == "G")
        {
            showItemFree.Attributes.Add("style", "display:none");
            grdfreeitem.Attributes.Add("style", "display:none");
            grdfreeproduct.Attributes.Remove("style");
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList3(string prefixText, int count, string contextKey)
    {
        HttpCookie cook;
        cook = HttpContext.Current.Request.Cookies["sp"];
        cbll bll = new cbll();
        List<string> lItem = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sItem = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@prop_no", prefixText));
        //arr.Add(new cArrayList("@item_cd", prefixText));
        bll.vSearchMstProposal(arr, ref rs);
        while (rs.Read())
        {
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["prop_no"].ToString(), rs["prop_no"].ToString());
            lItem.Add(sItem);

        }
        rs.Close();
        return (lItem.ToArray());
    }
    protected void btrefresh_Click(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
        Session["hdprop"] = hdprop.Value.ToString();
        //bll.vGetMstProposal(arr, ref rs);
        bll.vGetProposal(arr, ref rs);
        while (rs.Read())
        {
            //DateTime timeNow = DateTime.Now;
            //DateTime timeEnd = DateTime.ParseExact(rs["end_dt"].ToString(), "d/M/yyyy", null);
            //if (timeEnd >= timeNow)
            //{
            //arr.Clear();
            //arr.Add(new cArrayList("@prop_no", rs["prop_no"].ToString()));
                //hdprop.Value = rs["prop_no"].ToString();         
            if (rs["promo_typ"].ToString() == "LT" || rs["promo_typ"].ToString() == "IP" || rs["promo_typ"].ToString() == "SC")
            {
                dtstart_CalendarExtender.Enabled = true;
                dtend_CalendarExtender.Enabled = true;
                dtdelivery_CalendarExtender.Enabled = true;
            }
            else
            {
                dtstart_CalendarExtender.Enabled = false;
                dtend_CalendarExtender.Enabled = false;
                dtdelivery_CalendarExtender.Enabled = false;
            }
                dtstart.Text = rs["delivery_dt"].ToString();
                dtend.Text = rs["end_dt"].ToString();
                dtdelivery.Text = rs["delivery_dt"].ToString();
                dtmaxclaim.Text = rs["claim_dt"].ToString();
                txvendorref.Text = rs["prop_no_vendor"].ToString();
                txvendor.Text = rs["vendor_nm"].ToString();
                hdvendor.Value = rs["vendor_cd"].ToString();

                cbdisctyp.SelectedValue = rs["promo_typ"].ToString();

                bll.vBindingGridToSp(ref grdslspoint, "sp_tproposal_salespoint_get", arr);
                hdcust.Value = rs["rdcust"].ToString();
                if (rs["rdcust"].ToString() == "C")
                {
                    grdviewcust.Visible = false;
                    grdviewcusgrcd.Visible = false;
                    grdviewcusttype.Visible = false;
                    bll.vBindingGridToSp(ref grdviewcust, "sp_tproposal_customer_get", arr);
                }
                else if (rs["rdcust"].ToString() == "G")
                {
                    grdviewcusgrcd.Visible = false;
                    grdviewcust.Visible = false;
                    grdviewcusttype.Visible = false;
                    bll.vBindingGridToSp(ref grdviewcusgrcd, "sp_tproposal_cusgrcd_get", arr);
                }
                else if (rs["rdcust"].ToString() == "T")
                {
                    grdviewcusttype.Visible = false;
                    grdviewcusgrcd.Visible = false;
                    grdviewcust.Visible = false;
                    bll.vBindingGridToSp(ref grdviewcusttype, "sp_tproposal_custtype_get", arr);
                }
                
                if (rs["rditem"].ToString() == "I")
                {
                    hditem.Value = "P";
                    arr.Add(new cArrayList("@cust", rs["rdcust"].ToString()));
                    //arr.Clear();
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    //arr.Add(new cArrayList("@prop_no", txproposal.Text));
                    //bll.vBindingGridToSp(ref grdviewitem, "sp_discount_item_get", arr);
                    bll.vBindingGridToSp(ref grdviewitem, "sp_tproposal_item_get", arr);
                    grdviewgroup.Visible = false;
                    grdviewitem.Visible = true;
                    Label lbrbpbefore = (Label)grdviewitem.Rows[0].FindControl("lbrbpbefore");
                    Label lbrbpafter = (Label)grdviewitem.Rows[0].FindControl("lbrbpafter");
                    //txregularcost.Text = lbrbpbefore.Text.ToString();
                    //txnetcost.Text = lbrbpafter.Text.ToString();
                    txregularcost.Text = "0";
                    txnetcost.Text = "0";
                }
                if (rs["rditem"].ToString() == "G")
                {
                    hditem.Value = "G";
                    arr.Add(new cArrayList("@cust", rs["rdcust"].ToString()));
                    //arr.Clear();
                    arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                    //arr.Add(new cArrayList("@prop_no", txproposal.Text));
                    //bll.vBindingGridToSp(ref grdviewgroup, "sp_discount_product_get", arr);
                    bll.vBindingGridToSp(ref grdviewgroup, "sp_tproposal_productgroup_get", arr);
                    grdviewitem.Visible = false;
                    grdviewgroup.Visible = true;
                    Label lbrbpbefore = (Label)grdviewgroup.Rows[0].FindControl("lbrbpbefore");
                    Label lbrbpafter = (Label)grdviewgroup.Rows[0].FindControl("lbrbpafter");
                    txregularcost.Text = lbrbpbefore.Text.ToString();
                    txnetcost.Text = lbrbpafter.Text.ToString();
                }
                btsave.Visible = true;
            //}
            //else
            //{
            //    txproposal.Text = "";
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Sorry, this proposal expired.',' " + rs["prop_no"].ToString() + " ','warning');", true);
            //    return;
            //}      

            


        } rs.Close();
        System.Data.SqlClient.SqlDataReader rsProd = null;
        List<cArrayList> arrProd = new List<cArrayList>();
        arrProd.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
        arrProd.Add(new cArrayList("@product", hditem.Value.ToString()));
        bll.vGetProductBranded(arrProd, ref rsProd);
        while (rsProd.Read())
        {
            cbbrandedfree.SelectedValue = rsProd["prod_cd"].ToString();
        } rsProd.Close();
        cbbrandedfree_SelectedIndexChanged(sender, e);
        //cbbrandedfree.Attributes.Add("disabled", "true");

        // Cek Discount Available or Not
        arr.Clear();
        checkDiscount(Session["hdprop"].ToString(),"P","P","P");
    }

    protected void checkDiscount(String propNo,String salespoint,String customer, String custtype)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@prop_no", propNo));
        arr.Add(new cArrayList("@customer", customer));
        arr.Add(new cArrayList("@cust", custtype));
        arr.Add(new cArrayList("@item", null));
        arr.Add(new cArrayList("@salespoint", salespoint));
        arr.Add(new cArrayList("@sp", null));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@discount", (rdmethod.SelectedValue.ToString() == "" ? "FG" : rdmethod.SelectedValue.ToString())  ));
        bll.vBindingGridToSp(ref grddiscount, "sp_checkdiscount2", arr);
        if (grddiscount.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
        }        
    }


    protected void txminqty_TextChanged(object sender, EventArgs e)
    {
        txordermin.Text = txminqty.Text;
        txordermin.Attributes.Add("readonly", "readonly");
    }

    protected void grdformula_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbminqty = (Label)grdformula.Rows[e.RowIndex].FindControl("lbminqty");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@min_qty", lbminqty.Text));
        bll.vDelwrkFormulaDiscount(arr); arr.Clear(); arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdformula, "sp_twrk_formuladiscount_get", arr);
    }

    protected void cbbrandedfree_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@level_no", 2));
        arr.Add(new cArrayList("@prod_cd_parent", cbbrandedfree.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbprodgroupfree, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
        cbprodgroupfree_SelectedIndexChanged(sender, e);
    }

    protected void cbprodgroupfree_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@level_no", 3));
        arr.Add(new cArrayList("@prod_cd_parent", cbprodgroupfree.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbitemfree, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
        cbitemfree_SelectedIndexChanged(sender, e);
    }

    protected void cbitemfree_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("prod_cd", cbitemfree.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@prod_cd_parent", cbprodgroupfree.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbitemfrees, "sp_tmst_item_get", "item_cd", "item_desc", arr);
    }

    protected void btaddfree_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (rdfree.SelectedValue.ToString() == "P")
        {
            arr.Add(new cArrayList("@item_cd", cbitemfrees.SelectedValue.ToString()));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@uom", cbuomfree.SelectedValue.ToString()));
            bll.vInsertWrkFreeItem(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdfreeitem, "sp_twrk_freeitem_get", arr);
            arr.Clear();
            grdfreeproduct.Attributes.Add("style", "display:none");
            grdfreeitem.Attributes.Remove("style");
        }
        else
        {
            arr.Add(new cArrayList("@prod_cd", cbitemfree.SelectedValue.ToString()));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vInsertWrkFreeProduct(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdfreeproduct, "sp_twrk_freeproduct_get", arr);
            arr.Clear();
            grdfreeitem.Attributes.Add("style", "display:none");
            grdfreeproduct.Attributes.Remove("style");
        }        
    }

    protected void grdfreeitem_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grdfreeitem.PageIndex = e.NewPageIndex;
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdfreeitem, "sp_twrk_freeitem_get", arr);
    }

    protected void grdfreeitem_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (rdfree.SelectedValue.ToString() == "P")
        {
            Label lbitemcode = (Label)grdfreeitem.Rows[e.RowIndex].FindControl("lbitemcode");
            arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkFreeItem(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdfreeitem, "sp_twrk_freeitem_get", arr);
        }
        else 
        {
            Label lbprodcode = (Label)grdfreeproduct.Rows[e.RowIndex].FindControl("lbprodcode");
            arr.Add(new cArrayList("@prod_cd", lbprodcode.Text));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkFreeProduct(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdfreeproduct, "sp_twrk_freeproduct_get", arr);
        }

    }

    protected void btsave_Click(object sender, EventArgs e)
    {

        string docUpload = "";

        docUpload = bll.vLookUp("select fileloc from tproposal_doc where prop_no = '"+txproposal.Text.ToString()+"'");

        if (docUpload == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Upload Scan Document Proposal','Proposal Document','warning');", true);
            return;
        }

        //if (grddiscount.Rows.Count > 0)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ShowPopup();", true);
        //    return;
        //}


        if (txproposal.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Proposal No can not empty','Proposal No','warning');", true);
            return;
        }
        if (txremark.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Remark can not empty','Remark ','warning');", true);
            return;
        }
        if (txbenefit.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Benefit Promotion can not empty','Benefit Promotion','warning');", true);
            return;
        }
        if (rdused.SelectedValue.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Disc Used can not empty','Disc Used','warning');", true);
            return;
        }
        if (txregularcost.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Regular Cost can not empty','Regular Cost','warning');", true);
            return;
        }
        if (txnetcost.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Net Cost can not empty','Net Cost','warning');", true);
            return;
        }
        if (txminqty.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Minimum Order can not empty','Minimum Order','warning');", true);
            return;
        }
        if (txmaxorder.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Maximum Order can not empty','Maximum Order','warning');", true);
            return;
        }
        if (rdmethod.SelectedValue.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Discount Mechanism can not empty','Discount Mechanism','warning');", true);
            return;
        }

        if (rdmethod.SelectedValue.ToString() == "FG")
        {
            if (rdfree.SelectedValue.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please select free item or product','Free Item or Product','warning');", true);
                return;
            }
        }
        if (grdformula.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Formula Discount can not empty','Formula Discount','warning');", true);
            return;
        }

        string sDiscNo = "";
        string imgDiscount = "";
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@sys", "discount"));
        arr.Add(new cArrayList("@sysno", ""));
        bll.vGetDiscountNo(arr, ref rs);
        while (rs.Read())
        {
            sDiscNo = rs["generated"].ToString();
        }

        arr.Clear();

        if (uplcatalog.FileName == "")
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('No Image uploaded','Please scan the document and upload','warning');", true);
            //return;
            arr.Add(new cArrayList("@catalogimage", null));
        }
        else
        {
            FileInfo fi = new FileInfo(uplcatalog.FileName);
            string ext = fi.Extension;
            byte[] fs = uplcatalog.FileBytes;
            if (fs.Length <= 500000)
            {
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".bmp" || ext == ".gif" || ext == ".png" || ext == ".JPEG" || ext == ".JPG" || ext == ".BMP" || ext == ".GIF" || ext == ".PNG")
                {
                    if ((uplcatalog.FileName != "") || (uplcatalog.FileName != null))
                    {
                        imgDiscount = sDiscNo + ext;
                        arr.Add(new cArrayList("@catalogimage", sDiscNo + ext));
                        uplcatalog.SaveAs(bll.sGetControlParameter("image_path") + "/discount/" + sDiscNo + ext);
                        //string savedFileName = Server.MapPath("upload//discount//" + sDiscNo + ext);
                        //uplcatalog.SaveAs(savedFileName);
                        uplcatalog.Visible = false;
                        hpfile_nm.Visible = true;
                        lbfileloc.Text = sDiscNo + ext;
                        //hpfile_nm.NavigateUrl = "~\\upload\\discount\\" + sDiscNo + ext;
                        hpfile_nm.NavigateUrl = "/images/discount/" + sDiscNo + ext;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('You should upload image','jpg,bmp,gif and png upload document again');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 500KB');", true);
                return;
            }
        }

        arr.Clear();
        arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
        arr.Add(new cArrayList("@disc_no", sDiscNo));

        if (rdisalespoint.SelectedValue.ToString() == "Y")
        {
            bll.vInsertDiscSalespoint(arr);
        }
        else
        {
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@disc_no", sDiscNo));
            bll.vInsertDiscountSalespoint(arr);
        }

        arr.Clear();
        arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
        arr.Add(new cArrayList("@disc_no", sDiscNo));

        if (rdicust.SelectedValue.ToString() == "Y")
        {
            if (hdcust.Value.ToString() == "C")
            {
                bll.vInsertDiscCustomer(arr);
            }
            else if (hdcust.Value.ToString() == "G")
            {
                bll.vInsertDiscCusgrcd(arr);
            }
            else
            {
                bll.vInsertDiscCustype(arr);
            }
        }
        else
        {
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@disc_no", sDiscNo));
            if (rdcust.SelectedValue.ToString() == "C")
            {
                bll.vInsertDiscountCustomer(arr);
            }
            else if (rdcust.SelectedValue.ToString() == "G")
            {
                bll.vInsertDiscountCusGrCd(arr);
            }
            else
            {
                bll.vInsertDiscountCusttype(arr);
            }

        }

        arr.Clear();
        arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
        arr.Add(new cArrayList("@disc_no", sDiscNo));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        if (hditem.Value.ToString() == "P")
        {
            bll.vInsertDiscItem(arr);
        }
        else
        {
            bll.vInsertDiscProduct(arr);
        }
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@disc_cd", sDiscNo));
        bll.vInsertDiscountFormula(arr);
        if (rdfree.SelectedValue.ToString() == "P")
        {
            bll.vInsertDiscountFreeitem(arr);
        }
        else if (rdfree.SelectedValue.ToString() == "G")
        {
            bll.vInsertDiscountFreeProduct(arr);
        }
        arr.Clear();
        //Save main discount data
        arr.Add(new cArrayList("@disc_cd", sDiscNo));
        arr.Add(new cArrayList("@proposal_no", hdprop.Value.ToString()));
        arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(dtstart.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@end_dt", DateTime.ParseExact(dtend.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@delivery_dt", DateTime.ParseExact(dtdelivery.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@disc_dt", DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@rditem", hditem.Value.ToString()));
        arr.Add(new cArrayList("@disc_typ", cbdisctyp.SelectedValue.ToString()));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@disc_sta_id", "A"));
        arr.Add(new cArrayList("@remark", txremark.Text));
        if (rdicust.SelectedValue.ToString() == "Y")
        { arr.Add(new cArrayList("@rdcustomer", hdcust.Value.ToString())); }
        else { arr.Add(new cArrayList("@rdcustomer", rdcust.SelectedValue.ToString())); }
        
        arr.Add(new cArrayList("@propvendor_no", txvendorref.Text));
        arr.Add(new cArrayList("@discount_mec", rdmethod.SelectedValue.ToString()));
        arr.Add(new cArrayList("@discount_use", rdused.SelectedValue.ToString()));
        arr.Add(new cArrayList("@vendor_cd", hdvendor.Value.ToString()));
        arr.Add(new cArrayList("@benefitpromotion", txbenefit.Text));
        arr.Add(new cArrayList("@qty_min", txminqty.Text));
        arr.Add(new cArrayList("@qty_max", txmaxorder.Text));
        arr.Add(new cArrayList("@regularcost", txregularcost.Text));
        arr.Add(new cArrayList("@netcost", txnetcost.Text));
        if (rdfree.SelectedValue.ToString() == "P")
        {
            arr.Add(new cArrayList("@rdfreeitem", "I"));
        }
        else
        {
            arr.Add(new cArrayList("@rdfreeitem", rdfree.SelectedValue.ToString()));
        }
        arr.Add(new cArrayList("@catalogimage", imgDiscount));
        bll.vInsertMstDiscount(arr);

        txdisccode.Text = sDiscNo;
        arr.Clear();

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('New Discount has been created','" + sDiscNo + "','success');", true);

        btsave.Visible = false;
        uplcatalog.Attributes.Add("readonly", "readonly");
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_reqdiscount3.aspx");
    }

    protected void btaddcust_Click(object sender, EventArgs e)
    {
        if (rdcust.SelectedValue.ToString() == "C")
        {
            if (hdcust.Value.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer Not yet selcted','select customer','warning');", true);
                return;
            }
        }
        List<cArrayList> arr = new List<cArrayList>();
        if (rdcust.SelectedValue.ToString() == "C")
        {
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@cust_cd", hdcust_new.Value.ToString()));
            arr.Add(new cArrayList("@cust_nm", txsearchcust.Text.ToString()));
            bll.vInsertWrkCustomer(arr); arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdcust, "sp_twrk_customer_get", arr);
            grdcusttype.Attributes.Add("style", "display:none");
            grdcusgrcd.Attributes.Add("style", "display:none");
            grdcust.Attributes.Remove("style");
            txsearchcust.Text = "";
        }
        else if (rdcust.SelectedValue.ToString() == "G")
        {
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@cusgrcd", cbcusgrcd.SelectedValue.ToString()));
            bll.vInsertWrkCusGrCd(arr); arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdcusgrcd, "sp_twrk_cusgrcd_get", arr);
            grdcusttype.Attributes.Add("style", "display:none");
            grdcust.Attributes.Add("style", "display:none");
            grdcusgrcd.Attributes.Remove("style");
        }
        else if (rdcust.SelectedValue.ToString() == "T")
        {
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@cust_typ", cbcusgrcd.SelectedValue.ToString()));
            bll.vInsertTwrkCustType(arr); arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdcusttype, "sp_twrk_custtype_get", arr);
            grdcusgrcd.Attributes.Add("style", "display:none");
            grdcust.Attributes.Add("style", "display:none");
            grdcusttype.Attributes.Remove("style");
        }
        checkDiscount(Session["hdprop"].ToString(), "N", "N",rdcust.SelectedValue.ToString());
    }

    protected void rdicust_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdicust.SelectedValue.ToString() == "N")
        {
            rdcust.Attributes.Remove("style");
            rdcust_SelectedIndexChanged(sender, e);
            btaddcust.Attributes.Remove("style");
            grdviewcust.Visible = false;
            grdviewcusgrcd.Visible = false;
            grdviewcusttype.Visible = false;
        }
        else
        {
            rdcust.Attributes.Add("style", "display:none");
            txsearchcust.Attributes.Add("style", "display:none");
            cbcusgrcd.Attributes.Add("style", "display:none");
            btaddcust.Attributes.Add("style", "display:none");
            grdcust.Attributes.Add("style", "display:none");
            grdcusgrcd.Attributes.Add("style", "display:none");
            grdcusttype.Attributes.Add("style", "display:none");
            if (hdcust.Value.ToString() == "C")
            {
                grdviewcust.Visible = true;
                grdviewcusgrcd.Visible = false;
                grdviewcusttype.Visible = false;
            }
            else if (hdcust.Value.ToString() == "G")
            {
                grdviewcust.Visible = false;
                grdviewcusgrcd.Visible = true;
                grdviewcusttype.Visible = false;
            }
            else
            {
                grdviewcust.Visible = false;
                grdviewcusgrcd.Visible = false;
                grdviewcusttype.Visible = true;
            }
        }
    }

    protected void rdcust_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vDelWrkCusGrCD(arr);
        bll.vDelWrkCustomer(arr);
        bll.vDelWrkCustType(arr);
        grdcusttype.Attributes.Add("style", "display:none");
        grdcusgrcd.Attributes.Add("style", "display:none");
        grdcust.Attributes.Add("style", "display:none");
        if (rdcust.SelectedValue.ToString() == "C")
        {
            txsearchcust.Attributes.Remove("style");
            cbcusgrcd.Attributes.Add("style", "display:none");
        }
        else if (rdcust.SelectedValue.ToString() == "G")
        {
            txsearchcust.Attributes.Add("style", "display:none");
            cbcusgrcd.Attributes.Remove("style");
            bll.vBindingFieldValueToCombo(ref cbcusgrcd, "cusgrcd");
        }
        else if (rdcust.SelectedValue.ToString() == "T")
        {
            txsearchcust.Attributes.Add("style", "display:none");
            cbcusgrcd.Attributes.Remove("style");
            bll.vBindingFieldValueToCombo(ref cbcusgrcd, "otlcd");
        }
    }

    protected void grdcust_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblsalespointcd = (Label)grdcust.Rows[e.RowIndex].FindControl("lblsalespointcd");
        Label lblcustcode = (Label)grdcust.Rows[e.RowIndex].FindControl("lbcustcode");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespoint_cd", lblsalespointcd.Text));
        arr.Add(new cArrayList("@cust_cd", lblcustcode.Text));
        bll.vDelWrkCustomer(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdcust, "sp_twrk_customer_get", arr);
    }

    protected void grdcusgrcd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbcusgrcd = (Label)grdcusgrcd.Rows[e.RowIndex].FindControl("lbcusgrcd");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@cusgrcd", lbcusgrcd.Text));
        bll.vDelWrkCusGrCD(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdcusgrcd, "sp_twrk_cusgrcd_get", arr);
    }

    protected void grdcusttype_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbcusttype = (Label)grdcusttype.Rows[e.RowIndex].FindControl("lbcusttype");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@cust_typ", lbcusttype.Text));
        bll.vDelWrkCustType(arr);
        arr.Clear();
        arr.Add(new cArrayList("@prop_no", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdcusttype, "sp_twrk_custtype_get", arr);
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

        arr.Add(new cArrayList("@prop_no", HttpContext.Current.Session["hdprop"].ToString()));
        arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@usr_id", cookuser.Value.ToString()));
        bll.vSearchMstCustomerDisc(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }

    protected void grditem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbitemcode = (Label)grdviewitem.Rows[e.RowIndex].FindControl("lbitemcode");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        bll.vInsertWrkItem(arr); arr.Clear();
        arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
        arr.Add(new cArrayList("@cust", rdicust.SelectedValue.ToString() == "Y" ? hdcust.Value.ToString() : rdcust.SelectedValue.ToString() ));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdviewitem, "sp_tproposal_item_get", arr);
        //bll.vBindingGridToSp(ref grdviewitem, "sp_discount_item_get", arr);
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item Deleted','Deleted Item','warning');", true);
        //return;
    }

    protected void grdproduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbgroupcode = (Label)grdviewgroup.Rows[e.RowIndex].FindControl("lbgroupcode");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@prod_cd", lbgroupcode.Text));
        bll.vInsertWrkProduct(arr); arr.Clear();
        arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
        arr.Add(new cArrayList("@cust", rdicust.SelectedValue.ToString() == "Y" ? hdcust.Value.ToString() : rdcust.SelectedValue.ToString()));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdviewgroup, "sp_tproposal_productgroup_get", arr);
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Product Deleted','Deleted Product','warning');", true);
        //return;
    }

    protected void rdisalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdisalespoint.SelectedValue.ToString() == "N")
        {
            rdsalespoint.Attributes.Remove("style");
            btaddsalespoint.Attributes.Remove("style");
            grdslspoint.Visible = false;
            grdslspointnew.Visible = true;
            if (grdslspointnew.Rows.Count == 0)
            {
                grdslspointnew.Visible = false;
            }
        }
        else
        {
            grdslspoint.Visible = true;
            grdslspointnew.Visible = false;
            rdsalespoint.Attributes.Add("style", "display:none");
            btaddsalespoint.Attributes.Add("style", "display:none");
            if (grdslspoint.Rows.Count == 0)
            {
                grdslspoint.Visible = false;
            }
        }
    }

    protected void btaddsalespoint_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (rdsalespoint.SelectedValue.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Salespoint Not yet selcted','select Salespoint','warning');", true);
            return;
        }
        else
        {
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", rdsalespoint.SelectedValue.ToString()));
            bll.vInsertWrkSalespoint(arr); arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdslspointnew, "sp_twrk_salespoint_get", arr);
            grdslspoint.Visible = false;
            grdslspointnew.Visible = true;
            if (grdslspointnew.Rows.Count == 0)
            {
                grdslspointnew.Visible = false;
            }
            checkDiscount(Session["hdprop"].ToString(),"N","P","P");
        }
    }

}