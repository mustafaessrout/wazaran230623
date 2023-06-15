using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_claimentry_ho : System.Web.UI.Page
{

    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            txClaimNo.Text = "NEW";
            txClaimNo.Attributes.Add("readonly", "readonly");
            dtClaim.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
            bll.vBindingComboToSp(ref cbbranch, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            bll.vBindingComboToSp(ref cbPromotion, "sp_tpromotion_dtl_get", "promo_typ", "promotyp_nm");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@claim_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelMstClaimDetail(arr);
            bll.vBindingComboToSp(ref cbvendor, "sp_tmst_vendor_get", "vendor_cd", "vendor_nm");
            //txVendor.Attributes.Add("readonly", "readonly");
            //txPromotion.Attributes.Add("readonly", "readonly");
            txProduct.Attributes.Add("readonly", "readonly");
            txBudget.Attributes.Add("readonly", "readonly");
            //btedit.Attributes.Add("style","display:none");
            btupdate.Attributes.Add("style", "display:none");
            btdelete.Attributes.Add("style", "display:none");
            arr.Clear();
            hdedit.Value = "new";
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
    public static string[] GetListProduct(string prefixText, int count, string contextKey)
    {
        HttpCookie cook;
        cook = HttpContext.Current.Request.Cookies["sp"];
        cbll bll = new cbll();
        List<string> lItem = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sItem = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@prop_no", HttpContext.Current.Session["hdprop"].ToString()));
        arr.Add(new cArrayList("@item_cd", prefixText));
        bll.vSearchItembyProposal(arr, ref rs);
        while (rs.Read())
        {
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "-" + rs["item_nm"].ToString() + "-" + rs["size"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);
        }
        rs.Close();
        return (lItem.ToArray());
    }

    protected void btsearchprop_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "popupwindow('lookproposal.aspx');", true);
    }


    protected void btprop_Click(object sender, EventArgs e)
    {
        string promotyp = "";
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
        Session["hdprop"] = hdprop.Value.ToString();
        bll.vGetProposal(arr, ref rs);
        while (rs.Read())
        {
            hdvendor.Value = rs["vendor_cd"].ToString();
            cbvendor.SelectedValue = rs["vendor_cd"].ToString();
            cbPromotion.SelectedValue = rs["promo_typ"].ToString();
            //txVendor.Text = rs["vendor_cd"].ToString() + " - " + rs["vendor_nm"].ToString();
            //txPromotion.Text = rs["promo_typ"].ToString() + " - " + rs["promotyp_nm"].ToString();
            promotyp = rs["promo_typ"].ToString();
            txProduct.Text = rs["product"].ToString();
            hditem.Value = rs["rditem"].ToString();
            txBudget.Text = (rs["rdcost"] == "percentage") ? "Sbtc " + rs["sbtccost"].ToString()+"%, Principal "+rs["principalcost"].ToString() : (rs["rdcost"].ToString() == "sbtc") ? "Sbtc "+rs["sbtccost"].ToString()+"%" : "Principal "+rs["principalcost"].ToString()+"%" ; 
        } rs.Close();
        cbvendor_SelectedIndexChanged(sender, e);
        //arr.Clear();
        //cbproduct_SelectedIndexChanged(sender, e);
        // Document Proposal 
        arr.Clear();
        arr.Add(new cArrayList("@promo_cd", promotyp));
        arr.Add(new cArrayList("@dic", "BR"));
        bll.vBindingGridToSp(ref grdcate, "sp_tpromotion_doc_get", arr);
    }

    protected void rdmethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdmethod.SelectedValue.ToString() == "FG")
        {
            txOrderQty.Attributes.Remove("readonly"); txOrderQty.Text = "0";
            //txOrderValue.Attributes.Add("readonly", "readonly"); txOrderValue.Text = "0";
            //txFreeValue.Attributes.Add("readonly", "readonly"); txFreeValue.Text = "0";
            txFreeQty.Attributes.Remove("readonly"); txFreeQty.Text = "0";
            cbPrice.Attributes.Remove("readonly");
            //txPrice.Attributes.Remove("readonly"); txPrice.Text = "0";
            txDiscount.Attributes.Add("readonly", "readonly"); txDiscount.Text = "0";
            txAmount.Text = "0"; //txAmount.Attributes.Add("readonly", "readonly");
        }
        else if (rdmethod.SelectedValue.ToString() == "CH")
        {
            txOrderQty.Attributes.Remove("readonly"); txOrderQty.Text = "0";
            //txOrderValue.Attributes.Remove("readonly"); txOrderValue.Text = "0";
            //txFreeValue.Attributes.Remove("readonly"); txFreeValue.Text = "0";
            txFreeQty.Attributes.Add("readonly", "readonly"); txFreeQty.Text = "0";
            cbPrice.Attributes.Add("readonly", "readonly"); 
            //txPrice.Attributes.Add("readonly", "readonly"); txPrice.Text = "0";
            txDiscount.Attributes.Remove("readonly"); txDiscount.Text = "0";
            txAmount.Text = "0"; //txAmount.Attributes.Add("readonly", "readonly");
            cbPrice.SelectedValue = "0.00";
            if (grdproduct.Rows.Count > 0)
            {
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@claim_no", Request.Cookies["usr_id"].Value.ToString()));
                bll.vDelMstClaimDetail(arr);
            }
        }
        else if (rdmethod.SelectedValue.ToString() == "PC")
        {
            txOrderQty.Attributes.Remove("readonly"); txOrderQty.Text = "0";
            //txOrderValue.Attributes.Remove("readonly"); txOrderValue.Text = "0";
            //txFreeValue.Attributes.Remove("readonly"); txFreeValue.Text = "0";
            txFreeQty.Attributes.Add("readonly", "readonly"); txFreeQty.Text = "0";
            cbPrice.Attributes.Add("readonly", "readonly");
            //txPrice.Attributes.Add("readonly", "readonly"); txPrice.Text = "0";
            txDiscount.Attributes.Remove("readonly"); txDiscount.Text = "0";
            txAmount.Text = "0"; //txAmount.Attributes.Add("readonly", "readonly");
            cbPrice.SelectedValue = "0.00";
            if (grdproduct.Rows.Count > 0)
            {
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@claim_no", Request.Cookies["usr_id"].Value.ToString()));
                bll.vDelMstClaimDetail(arr);
            }
        }
        else if (rdmethod.SelectedValue.ToString() == "CMHO")
        {
            List<cArrayList> arr = new List<cArrayList>();
            btnadd.Enabled = false;
            grdproduct.Visible = false;
            grdBA.Visible = true;
            arr.Clear();
            arr.Add(new cArrayList("@claim_no", hdclaim.Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdBA, "sp_tclaimdtl_getbycontractka", arr);
        }
    }

    protected void btadd_Click(object sender, EventArgs e)
    {
        if (rdmethod.SelectedValue.ToString() == "FG")
        {
            if (txOrderQty.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Order Qty can not empty','Order Qty','warning');", true);
                return;
            }
            if (txFreeQty.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Free Qty can not empty','Free Qty','warning');", true);
                return;
            }
            //if (txPrice.Text == "")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Unit Price can not empty','Unit Price','warning');", true);
            //    return;
            //}
        }
        else
        {
            if (txOrderQty.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Order Qty can not empty','Order Qty','warning');", true);
                return;
            }
        }

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@claim_no", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@product_code", cbproduct.SelectedValue.ToString()));
        arr.Add(new cArrayList("@claim_mec", rdmethod.SelectedValue.ToString()));
        if (rdmethod.SelectedValue.ToString() == "FG")
        {
            arr.Add(new cArrayList("@order_qty", txOrderQty.Text));
            arr.Add(new cArrayList("@free_qty", txFreeQty.Text));
            arr.Add(new cArrayList("@disc_amt", "0.00"));
            if (txPrice.Text == "")
            {
                arr.Add(new cArrayList("@unitprice", cbPrice.SelectedValue.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@unitprice", txPrice.Text));
            }            
        }
        else if(rdmethod.SelectedValue.ToString() == "CH" || rdmethod.SelectedValue.ToString() == "PC" )
        {
            if((txOrderQty.Text == "0") || (txDiscount.Text == "0"))
            {
                arr.Add(new cArrayList("@order_qty", "0.00"));
                arr.Add(new cArrayList("@free_qty", "0.00"));
                arr.Add(new cArrayList("@disc_amt", txAmount.Text));
                arr.Add(new cArrayList("@unitprice", "0.00"));
            }else
            {
                arr.Add(new cArrayList("@order_qty", txOrderQty.Text));
                arr.Add(new cArrayList("@free_qty", "0.00"));
                arr.Add(new cArrayList("@disc_amt", txDiscount.Text));
                arr.Add(new cArrayList("@unitprice", "0.00"));
            }            
        }
        arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
        //string[] promo = txPromotion.Text.Split('-');
        //arr.Add(new cArrayList("@disc_typ", promo[0].Replace(" ","").ToString()));
        arr.Add(new cArrayList("@disc_typ", cbPromotion.SelectedValue.ToString()));
        bll.vInsertMstClaimDetail(arr);
        arr.Clear();
        arr.Add(new cArrayList("@claim_no", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdproduct, "sp_tmst_claim_ho_dtl_get", arr);
        grdproduct.Visible = true;
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_claimentry_ho.aspx");
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        string sbrn_cek = bll.vLookUp("select salespoint_sn from tmst_salespoint where salespointcd='" + cbbranch.SelectedValue.ToString() + "'") + txBranchNo.Text;

        string ccnr_exist = bll.vLookUp("select count(manual_no) from tmst_ccnr where manual_no='" + sbrn_cek + "'");

        if (txAgHo.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('AG HO No can not empty','AG HO No','warning');", true);
            return;
        }

        if ((Convert.ToInt16(ccnr_exist)) > 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('CCNR No Already Exist.','Claim Details','warning');", true);
            return;
        }

        if (grdproduct.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Claim Details can not empty','Claim Details','warning');", true);
            return;
        }
        if (txPropNo.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Proposal No can not empty','Proposal No','warning');", true);
            return;
        }
        if (txBranchNo.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('CCNR can not empty','Branch Claim No','warning');", true);
            return;
        }
        if (txremark.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Remark can not empty','Remark','warning');", true);
            return;
        }
        string claimNo = "";
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@sys", "claimho"));
        arr.Add(new cArrayList("@sysno", ""));
        bll.vGetDiscountNo(arr, ref rs);
        while (rs.Read())
        {
            claimNo = rs["generated"].ToString();
        }

        arr.Clear();
        string sbrn = bll.vLookUp("select salespoint_sn from tmst_salespoint where salespointcd='" + cbbranch.SelectedValue.ToString() + "'") + txBranchNo.Text;
        arr.Add(new cArrayList("@claim_no", claimNo));
        arr.Add(new cArrayList("@prop_no", txPropNo.Text));
        arr.Add(new cArrayList("@ccnr", sbrn));
        arr.Add(new cArrayList("@salespoint", cbbranch.SelectedValue.ToString()));
        arr.Add(new cArrayList("@claim_dt", DateTime.ParseExact(dtClaim.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@new_dt", DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@month", cbMonth.SelectedValue.ToString()));
        arr.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
        arr.Add(new cArrayList("@discount", rdmethod.SelectedValue.ToString()));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@remark", txremark.Text));
        //string[] promo = txPromotion.Text.Split('-');
        //arr.Add(new cArrayList("@disc_typ", promo[0].Replace(" ", "").ToString()));
        arr.Add(new cArrayList("@disc_typ", cbPromotion.SelectedValue.ToString()));
        string[] propcode = txPropNo.Text.Split('/');
        arr.Add(new cArrayList("@propcode", propcode[2].Replace(" ", "").ToString()));
        //string[] vendor = txVendor.Text.Split('-');
        //arr.Add(new cArrayList("@vendor_cd", vendor[0].Replace(" ", "").ToString()));
        arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue.ToString()));
        double orderqty=0, ordervalue=0, freeqty=0, freevalue=0, amount=0;

        foreach (GridViewRow row in grdproduct.Rows)
        {
            Label lborderqty = (Label)row.FindControl("lborderqty");
            Label lbordervalue = (Label)row.FindControl("lbordervalue");
            Label lbfreeqty = (Label)row.FindControl("lbfreeqty");
            Label lbfreevalue = (Label)row.FindControl("lbfreevalue");
            Label lbamount = (Label)row.FindControl("lbamount");
            orderqty += Convert.ToDouble(lborderqty.Text.Replace(" ", "") == "" ? "0" : lborderqty.Text.Replace(" ", ""));
            ordervalue += Convert.ToDouble(lbordervalue.Text.Replace(" ", "") == "" ? "0" : lbordervalue.Text.Replace(" ", ""));
            freeqty += Convert.ToDouble(lbfreeqty.Text.Replace(" ", "") == "" ? "0" : lbfreeqty.Text.Replace(" ", ""));
            freevalue += Convert.ToDouble(lbfreevalue.Text.Replace(" ", "") == "" ? "0" : lbfreevalue.Text.Replace(" ", ""));
            amount += Convert.ToDouble(lbamount.Text.Replace(" ", "") == "" ? "0" : lbamount.Text.Replace(" ", ""));
        }
        arr.Add(new cArrayList("@orderqty", orderqty.ToString()));
        arr.Add(new cArrayList("@ordervalue", ordervalue.ToString()));
        arr.Add(new cArrayList("@freeqty", freeqty.ToString()));
        arr.Add(new cArrayList("@freevalue", freevalue.ToString()));
        arr.Add(new cArrayList("@amount", amount.ToString()));

        string srefbrn = bll.vLookUp("select salespoint_sn from tmst_salespoint where salespointcd='" + cbbranch.SelectedValue.ToString() + "'") + txRefBranchNo.Text;

        arr.Add(new cArrayList("@refccnr", srefbrn));

        bll.vInsertMstClaimHO(arr);

        txClaimNo.Text = claimNo;
        arr.Clear();
        // Insert CCNR
        string sCCNRNO = claimNo.Substring(3);
        arr.Clear();
        
        arr.Add(new cArrayList("@ccnr_no", "CCNR"+sCCNRNO));
        arr.Add(new cArrayList("@ccnr_dt", DateTime.ParseExact(dtClaim.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@manual_no", sbrn));
        arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
        arr.Add(new cArrayList("@claim_no", claimNo));
        arr.Add(new cArrayList("@app_by", null));
        bll.vInsertCcnr(arr);
        txBranchNo.Text = sbrn;
        arr.Clear();

        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@claim_no", claimNo));
        bll.vUpdatetMstClaimNo(arr);

        // Upload Dokumen Support
        arr.Clear();
        foreach (GridViewRow row in grdcate.Rows)
        {
            Label lbdoccode = (Label)row.FindControl("lbdoccode");
            Label lbdocname = (Label)row.FindControl("lbdocname");
            FileUpload upl = (FileUpload)row.FindControl("upl");
            if (upl.HasFile)
            {
                FileInfo fi = new FileInfo(upl.FileName);
                string ext = fi.Extension;
                byte[] fs = upl.FileBytes;
                if (fs.Length <= 104857600)
                {

                    if ((upl.FileName != "") || (upl.FileName != null))
                    {
                        arr.Clear();
                        arr.Add(new cArrayList("@claim_no", claimNo));
                        arr.Add(new cArrayList("@doc_cd", lbdoccode.Text));
                        arr.Add(new cArrayList("@salespoint", cbbranch.SelectedValue.ToString()));
                        arr.Add(new cArrayList("@fileloc", cbbranch.SelectedValue.ToString() + "-"+ claimNo + "-" + lbdoccode.Text + ext));
                        arr.Add(new cArrayList("@doc_nm", lbdocname.Text));
                        upl.SaveAs(bll.sGetControlParameter("image_path") + "/claim_doc/" + cbbranch.SelectedValue.ToString() + "-" + claimNo + "-" + lbdoccode.Text + ext);
                        bll.vInsClaimDoc(arr);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Please attach all the documents required','Claim HO');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 100MB');", true);
                    return;
                }
            }
        }

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('New Claim HO has been created','" + claimNo + "','success');", true);

        btsave.Visible = false;
    }

    protected void grdproduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (hdedit.Value.ToString() == "new")
        {
            Label lbproductcode;
            List<cArrayList> arr = new List<cArrayList>();
            lbproductcode = (Label)grdproduct.Rows[e.RowIndex].FindControl("lbproduct");
            string[] code = lbproductcode.Text.Split('_');
            arr.Add(new cArrayList("@claim_no", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@item_cd", code[0]));
            bll.vDelMstClaimDetail(arr);
            arr.Clear();
            arr.Add(new cArrayList("@claim_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdproduct, "sp_tmst_claim_ho_dtl_get", arr);
            if (grdproduct.Rows.Count == 0)
            {
                grdproduct.Visible = false;
            }
        }
        else if(hdedit.Value.ToString() == "true" )
        {
            Label lbproductcode;
            List<cArrayList> arr = new List<cArrayList>();
            lbproductcode = (Label)grdproduct.Rows[e.RowIndex].FindControl("lbproduct");
            string[] code = lbproductcode.Text.Split('_');
            arr.Add(new cArrayList("@claim_no", hdclaim.Value.ToString()));
            arr.Add(new cArrayList("@salespoint", hdsalespoint.Value.ToString()));
            arr.Add(new cArrayList("@item_cd", code[0]));
            bll.vDelMstClaimDetail(arr);
            arr.Clear();
            //arr.Add(new cArrayList("@claim_no", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@claim_no", hdclaim.Value.ToString()));
            arr.Add(new cArrayList("@salespoint", hdsalespoint.Value.ToString()));
            bll.vBindingGridToSp(ref grdproduct, "sp_tmst_claim_ho_dtl_get", arr);
            if (grdproduct.Rows.Count == 0)
            {
                grdproduct.Visible = false;
            }
        }        
    }

    protected void txAll_TextChanged(object sender, EventArgs e)
    {
        if (rdmethod.SelectedValue.ToString() == "FG")
        {
            if (txPrice.Text == "")
            {
                txAmount.Text = (Convert.ToDecimal(txFreeQty.Text) * Convert.ToDecimal(cbPrice.SelectedValue.ToString())).ToString();
            }
            else
            {
                txAmount.Text = (Convert.ToDecimal(txFreeQty.Text) * Convert.ToDecimal(txPrice.Text)).ToString();
            }
            
        }
        else if (rdmethod.SelectedValue.ToString() == "CH")
        {
            if ((Convert.ToDecimal(txOrderQty.Text) > 0) || (Convert.ToDecimal(txDiscount.Text) > 0))
            {
                txAmount.Text = (Convert.ToDecimal(txOrderQty.Text) * Convert.ToDecimal(txDiscount.Text)).ToString();
            }            
        }
    }
    protected void cbproduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Get Price By Product 
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", cbproduct.SelectedValue.ToString()));
        arr.Add(new cArrayList("@month", cbMonth.SelectedValue.ToString()));
        arr.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbPrice, "sp_item_price", "unitprice", "type", arr);
        cbPrice.Items.Insert(0, "0.00");
    }

    protected void grdcate_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grdcate.Rows.Count > 0)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string docStatus = "";
                string docSystem = "";
                string[] promotype = txPropNo.Text.Split('/');


                Label lbdocname1 = (Label)e.Row.FindControl("lbdocname");
                Label lbdoccode = (Label)e.Row.FindControl("lbdoccode");
                FileUpload upl1 = (FileUpload)e.Row.FindControl("upl");
                docStatus = bll.vLookUp("select doc_status from tpromotion_doc where doc_cd='" + lbdoccode.Text + "' and promo_typ='" + promotype[1].ToString() + "'");
                docSystem = bll.vLookUp("select doc_system from tpromotion_doc where doc_cd='" + lbdoccode.Text + "' and promo_typ='" + promotype[1].ToString() + "'");
                if (docStatus == "Y")
                {
                    if (docSystem == "N")
                    {
                        //upl1.CssClass = "makeitreadwrite";
                    }
                    else
                    {
                        e.Row.BackColor = System.Drawing.Color.Gray; 
                        //upl1.BackColor = System.Drawing.Color.Gray;
                    }
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.Gray;
                    //upl1.BackColor = System.Drawing.Color.Gray;
                }
            }

        }
        
    }
    protected void btsearchclaim_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "popupwindow('lookclaimho.aspx');", true);
    }
    protected void btlookup_Click(object sender, EventArgs e)
    {
        txClaimNo.Text = hdclaim.Value.ToString();
        cbbranch.SelectedValue = hdsalespoint.Value.ToString();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@claim_no", hdclaim.Value.ToString()));
        arr.Add(new cArrayList("@salespoint", hdsalespoint.Value.ToString()));
        bll.vGetClaimBy(arr, ref rs);
        while (rs.Read())
        {
            txAgHo.Text = rs["claim_no_ho"].ToString();
            txBranchNo.Text = rs["ccnr_no"].ToString().Substring(3);
            hdccnr.Value = rs["ccnr_no"].ToString();
            txPropNo.Text = rs["prop_no"].ToString();
            hdprop.Value = rs["prop_no"].ToString();
            cbMonth.SelectedValue = rs["tx_month"].ToString();
            cbYear.SelectedValue = rs["tx_year"].ToString();
            txPropNo.Text = rs["prop_no"].ToString();
            dtClaim.Text = rs["dt_claim"].ToString();
            rdmethod.SelectedValue = rs["discount_mec"].ToString();
            cbvendor.SelectedValue = rs["vendor_cd"].ToString();
            if (rs["cstatus"].ToString() == "H-01") { btdelete.Attributes.Remove("style"); }
            else { btdelete.Attributes.Add("style", "display:none"); }
            if (rs["discount_mec"].ToString() == "CMHO") { txBranchNo.Text = rs["ccnr_no"].ToString(); }
        }
        btprop_Click(sender, e);
        //cbvendor_SelectedIndexChanged(sender, e);
        arr.Clear();
        arr.Add(new cArrayList("@promo_cd", cbPromotion.SelectedValue.ToString()));
        arr.Add(new cArrayList("@dic", "BR"));
        bll.vBindingGridToSp(ref grdcate, "sp_tpromotion_doc_get", arr);
        arr.Clear();
        rdmethod_SelectedIndexChanged(sender, e);
        arr.Add(new cArrayList("@claim_no", hdclaim.Value.ToString()));
        arr.Add(new cArrayList("@salespoint", hdsalespoint.Value.ToString()));
        bll.vBindingGridToSp(ref grdproduct, "sp_tmst_claim_ho_dtl_get", arr);
        bll.vBindingGridToSp(ref grddoc, "sp_tclaim_doc_get", arr);
        btedit.Attributes.Remove("style");
        //btedit.Attributes.Add("style","display:none");
        btsave.Attributes.Add("style", "display:none");
        hdedit.Value = "false";
    }
    protected void cbvendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        //if (hdvendor.Value.ToString() == "" || hdvendor.Value.ToString() == null)
        //{
            arr.Clear();
            arr.Add(new cArrayList("@vendor", cbvendor.SelectedValue.ToString()));
            arr.Add(new cArrayList("@prop_no", txPropNo.Text));
            bll.vBindingComboToSp(ref cbproduct, "sp_item_search2", "item_cd", "item_desc", arr);
            cbproduct_SelectedIndexChanged(sender, e);
        //}
    }
    protected void btedit_ServerClick(object sender, EventArgs e)
    {
        hdedit.Value = "true";
        btupdate.Attributes.Remove("style");
        btsave.Attributes.Add("style","display:none");
        btedit.Attributes.Add("style", "display:none");
    }
    protected void btupdate_ServerClick(object sender, EventArgs e)
    {
        if (hdedit.Value.ToString() == "true")
        {

            Int32 dccnr;
            if (!Int32.TryParse(txBranchNo.Text, out dccnr))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('CCNR Must be numeric','Budget Limit','warning');", true);
                return;
            }

            string sbrn_cek = bll.vLookUp("select salespoint_sn from tmst_salespoint where salespointcd='" + cbbranch.SelectedValue.ToString() + "'") + txBranchNo.Text;

            string ccnr_exist = bll.vLookUp("select count(manual_no) from tmst_ccnr where manual_no='" + sbrn_cek + "'");

            if ((Convert.ToInt16(ccnr_exist)) > 0)
            {
                if (hdccnr.Value.ToString() != sbrn_cek)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('CCNR No Already Exist.','Claim Details','warning');", true);
                    return;
                }                
            }

            if (grdproduct.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Claim Details can not empty','Claim Details','warning');", true);
                return;
            }
            if (txPropNo.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Proposal No can not empty','Proposal No','warning');", true);
                return;
            }
            if (txBranchNo.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('CCNR can not empty','Branch Claim No','warning');", true);
                return;
            }
            if (txremark.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Remark can not empty','Remark','warning');", true);
                return;
            }
            string claimNo = hdclaim.Value.ToString();
            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            string sbrn = bll.vLookUp("select salespoint_sn from tmst_salespoint where salespointcd='" + cbbranch.SelectedValue.ToString() + "'") + txBranchNo.Text;
            arr.Add(new cArrayList("@claim_no", claimNo));
            arr.Add(new cArrayList("@oldSP", hdsalespoint.Value.ToString()));
            arr.Add(new cArrayList("@prop_no", txPropNo.Text));
            arr.Add(new cArrayList("@ccnr", sbrn));
            arr.Add(new cArrayList("@salespoint", cbbranch.SelectedValue.ToString()));
            //arr.Add(new cArrayList("@edit_dt", DateTime.ParseExact(DateTime.Now.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@month", cbMonth.SelectedValue.ToString()));
            arr.Add(new cArrayList("@year", cbYear.SelectedValue.ToString()));
            arr.Add(new cArrayList("@discount", rdmethod.SelectedValue.ToString()));
            arr.Add(new cArrayList("@editby", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@remark", txremark.Text));
            //string[] promo = txPromotion.Text.Split('-');
            //arr.Add(new cArrayList("@disc_typ", promo[0].Replace(" ", "").ToString()));
            arr.Add(new cArrayList("@disc_typ", cbPromotion.SelectedValue.ToString()));
            string[] propcode = txPropNo.Text.Split('/');
            arr.Add(new cArrayList("@propcode", propcode[2].Replace(" ", "").ToString()));
            //string[] vendor = txVendor.Text.Split('-');
            //arr.Add(new cArrayList("@vendor_cd", vendor[0].Replace(" ", "").ToString()));
            arr.Add(new cArrayList("@vendor_cd", cbvendor.SelectedValue.ToString()));
            double orderqty = 0, ordervalue = 0, freeqty = 0, freevalue = 0, amount = 0;

            foreach (GridViewRow row in grdproduct.Rows)
            {
                Label lborderqty = (Label)row.FindControl("lborderqty");
                Label lbordervalue = (Label)row.FindControl("lbordervalue");
                Label lbfreeqty = (Label)row.FindControl("lbfreeqty");
                Label lbfreevalue = (Label)row.FindControl("lbfreevalue");
                Label lbamount = (Label)row.FindControl("lbamount");
                orderqty += Convert.ToDouble(lborderqty.Text.Replace(" ", "") == "" ? "0" : lborderqty.Text.Replace(" ", ""));
                ordervalue += Convert.ToDouble(lbordervalue.Text.Replace(" ", "") == "" ? "0" : lbordervalue.Text.Replace(" ", ""));
                freeqty += Convert.ToDouble(lbfreeqty.Text.Replace(" ", "") == "" ? "0" : lbfreeqty.Text.Replace(" ",""));
                freevalue += Convert.ToDouble(lbfreevalue.Text.Replace( " ", "") == "" ? "0" : lbfreevalue.Text.Replace(" ",""));
                amount += Convert.ToDouble(lbamount.Text.Replace(" ", "") == "" ? "0" : lbamount.Text.Replace(" ", ""));
            }
            arr.Add(new cArrayList("@orderqty", orderqty.ToString()));
            arr.Add(new cArrayList("@ordervalue", ordervalue.ToString()));
            arr.Add(new cArrayList("@freeqty", freeqty.ToString()));
            arr.Add(new cArrayList("@freevalue", freevalue.ToString()));
            arr.Add(new cArrayList("@amount", amount.ToString()));

            bll.vUpdateMstClaimHO(arr);

            txClaimNo.Text = claimNo;
            arr.Clear();
            // Insert CCNR
            if (hdccnr.Value.ToString() != sbrn_cek)
            {
                string sCCNRNO = txBranchNo.Text.Substring(3);
                arr.Clear();

                arr.Add(new cArrayList("@ccnr_no", "CCNR" + sCCNRNO));
                arr.Add(new cArrayList("@ccnr_dt", DateTime.ParseExact(dtClaim.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@manual_no", sbrn));
                arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
                arr.Add(new cArrayList("@claim_no", claimNo));
                arr.Add(new cArrayList("@app_by", null));
                bll.vInsertCcnr(arr);
                //txBranchNo.Text = sbrn;
                arr.Clear();
            }            

            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@claim_no", claimNo));
            bll.vUpdatetMstClaimNo(arr);

            // Upload Dokumen Support
            arr.Clear();
            foreach (GridViewRow row in grdcate.Rows)
            {
                Label lbdoccode = (Label)row.FindControl("lbdoccode");
                Label lbdocname = (Label)row.FindControl("lbdocname");
                FileUpload upl = (FileUpload)row.FindControl("upl");
                if (upl.HasFile)
                {
                    FileInfo fi = new FileInfo(upl.FileName);
                    string ext = fi.Extension;
                    byte[] fs = upl.FileBytes;
                    if (fs.Length <= 104857600)
                    {

                        if ((upl.FileName != "") || (upl.FileName != null))
                        {
                            arr.Clear();
                            arr.Add(new cArrayList("@claim_no", claimNo));
                            arr.Add(new cArrayList("@doc_cd", lbdoccode.Text));
                            arr.Add(new cArrayList("@salespoint", cbbranch.SelectedValue.ToString()));
                            arr.Add(new cArrayList("@fileloc", cbbranch.SelectedValue.ToString() + "-" + claimNo + "-" + lbdoccode.Text + ext));
                            arr.Add(new cArrayList("@doc_nm", lbdocname.Text));
                            upl.SaveAs(bll.sGetControlParameter("image_path") + "/claim_doc/" + cbbranch.SelectedValue.ToString() + "-" + claimNo + "-" + lbdoccode.Text + ext);
                            string cekClaim = bll.vLookUp("select count(*) from tclaim_doc where claim_no='"+claimNo+"' and salespointcd='"+cbbranch.SelectedValue.ToString()+"' and doc_cd ='"+lbdoccode.Text+"' ");

                            if (Convert.ToInt16(cekClaim) > 0)
                            {
                                //bll.vUpdateClaimDoc(arr);
                            }
                            else
                            {
                                bll.vInsClaimDoc(arr);
                            }                            
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Please attach all the documents required','Claim HO');", true);
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 100MB');", true);
                        return;
                    }
                }
            }

            btsave.Attributes.Remove("style");
            btedit.Attributes.Add("style", "display:none");
            btupdate.Attributes.Add("style", "display:none");
            hdedit.Value = "new";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Claim HO has been update','" + claimNo + "','success');", true);

            btsave.Visible = false;

        }

    }
    protected void btprint_ServerClick(object sender, EventArgs e)
    {
        string claim_no = txClaimNo.Text;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=claimho&cl=" + claim_no + "&cr="+txBranchNo.Text+"');", true);
    }
    protected void btdelete_ServerClick(object sender, EventArgs e)
    {
        string claim_no = txClaimNo.Text;
        string status = bll.vLookUp("select cstatus from tmst_claim where claim_no='"+claim_no+"' and salespointcd='"+cbbranch.SelectedValue.ToString()+"'");
        if (claim_no.Substring(0, 2) == "CL")
        {
            if (status == "H-01")
            {
                List<cArrayList> arr = new List<cArrayList>();
                arr.Clear();
                arr.Add(new cArrayList("@claim_no", claim_no));
                arr.Add(new cArrayList("@salespoint", cbbranch.SelectedValue.ToString()));
                bll.vDelMstClaim(arr);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This Claim Deleted.','" + claim_no + "','success');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This Claim Can not Delete','" + claim_no + "','warning');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This Claim Can not Delete','" + claim_no + "','warning');", true);
        }
    }
    protected void grdBA_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdBA.EditIndex = e.NewEditIndex;
    }
    protected void grdBA_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdBA.EditIndex = -1;
    }
    protected void grdBA_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdBA.EditIndex = -1;
    }
}