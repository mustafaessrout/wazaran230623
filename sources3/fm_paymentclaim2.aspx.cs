using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_paymentclaim2 : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            string sClaimCode = Request.QueryString["dc"];
            lbclaim.Text = sClaimCode;
            //bll.vBindingFieldValueToCombo(ref cbpaymnttype, "payment_typ");
            //bll.vBindingFieldValueToCombo(ref cbbankcq, "bank_cd");
            arr.Add(new cArrayList("@salespointcd", 0));
            //bll.vBindingComboToSp(ref cbbankho, "sp_tmst_bankaccount_getbysp", "acc_no", "bank_nm", arr);
            bll.vBindingComboToSp(ref cbdoctype, "sp_get_docpayment_typ", "fld_valu", "fld_desc");
            arr.Clear();
            arr.Add(new cArrayList("@clh_no", sClaimCode));
            arr.Add(new cArrayList("@payment", "A"));
            bll.vBindingGridToSp(ref grdclaim, "sp_tclaim_header_detail", arr);
            arr.Clear();
            arr.Add(new cArrayList("@clh_no", sClaimCode));
            bll.vBindingGridToSp(ref grddocument, "sp_tclaim_header_document", arr);
            grdclaim.UseAccessibleHeader = true;
            grdclaim.HeaderRow.TableSection = TableRowSection.TableHeader;
            cbdoctype_SelectedIndexChanged(sender, e);
            claimDetailPay.Attributes.Add("style","display:none");
            arr.Clear();
            arr.Add(new cArrayList("@clh_no", sClaimCode));
            arr.Add(new cArrayList("@claim_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDeletePaymentClaimDtl(arr);
            txOrderQty.Text = "0";
            txFreeQty.Text = "0";
            txFreeValue.Text = "0";
            txDiscount.Text = "0";
            //txPrice.Text = "0";
            cbPrice.SelectedValue = "0.00";
            txAmount.Text = "0";
            bll.vBindingFieldValueToCombo(ref cbbankcq, "bank_cd");
            cbbankcq.Items.Insert(0, new ListItem("None", "none"));
            arr.Clear();
            arr.Add(new cArrayList("@clh_no", sClaimCode));
            bll.vBindingGridToSp(ref grdvat, "sp_tclaim_header_vat", arr);
            this.RegisterPostBackControl();
        }
    }

    private void RegisterPostBackControl()
    {
        foreach (GridViewRow row in grdpayment.Rows)
        {
            Button btnEdit = row.FindControl("btnEdit") as Button;
            ScriptManager.GetCurrent(this).RegisterPostBackControl(btnEdit);
            Button btnDelete = row.FindControl("btnDelete") as Button;
            ScriptManager.GetCurrent(this).RegisterPostBackControl(btnDelete);
        }
    }

    protected void cbdoctype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (cbdoctype.SelectedValue.ToString() == "CN")
        //{
        //    upl.Visible = false;
        //    screenDN.Visible = true;
        //    detailDN.Visible = true;
        //    // Config DNScreen
        //    System.Data.SqlClient.SqlDataReader rs = null;
        //    List<cArrayList> arr = new List<cArrayList>();

        //    arr.Add(new cArrayList("@clh_no", Request.QueryString["dc"]));
        //    bll.vGetPaymentDN(arr, ref rs);
        //    while (rs.Read())
        //    {
        //        txdescriptionDN.Text = "Paid - " + rs["promotyp_nm"].ToString();
        //        txqtyDN.Text = rs["tot_order"].ToString();
        //        //if(rs["discount_mec"].ToString() == "FG")
        //        txfgDN.Text = rs["tot_free"].ToString();
        //        txvalueDN.Text = rs["tot_amount"].ToString();
        //        txamountDN.Text = rs["tot_payment"].ToString();
        //    }

        //    txaccountDN.Text = "101001613";
        //}
        //else
        //{
            upl.Visible = true;
            screenDN.Visible = false;
            detailDN.Visible = false;
        //}
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        string vat_inv = "";
        //if ((dtpayment.Text == "") || (dtcheque.Text == "") || (dtdue.Text == "") )
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "swal('Date must be filled.','Payment Date','warning');", true);
        //    return;
        //}
        //if (txremarks.Text == "")
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "swal('Remark must be filled.','Remark','warning');", true);
        //    return;
        //}

        string countPayment = bll.vLookUp("select count(*)+1 from tclaim_payment where clh_no = '"+ Request.QueryString["dc"] + "'");

        List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@pay_dt", DateTime.ParseExact(dtpayment.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        //arr.Add(new cArrayList("@due_dt", DateTime.ParseExact(dtdue.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        //arr.Add(new cArrayList("@created_dt", DateTime.ParseExact(dtcheque.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        //arr.Add(new cArrayList("@clh_no", Request.QueryString["dc"]));
        //arr.Add(new cArrayList("@pay_typ", cbpaymnttype.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@bankho", cbbankho.SelectedValue.ToString()));
        arr.Add(new cArrayList("@bankcq", cbbankcq.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@doc_no", txdocno.Text));
        arr.Add(new cArrayList("@remark", txremarks.Text));
        //arr.Add(new cArrayList("@doc_typ", cbdoctype.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@paymentby", Request.Cookies["usr_id"].Value.ToString()));
        //if (cbdoctype.SelectedValue.ToString() != "CN")
        //{
            if (fup.FileName == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "swal('No File Uploaded.','Document Upload','warning');", true);
                return;
            }
            else
            {
                FileInfo fi = new FileInfo(fup.FileName);
                string ext = fi.Extension;
                byte[] fs = fup.FileBytes;
                if (fs.Length <= 1073741824)
                {
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".bmp" || ext == ".gif" || ext == ".png" || ext == ".pdf" || ext == ".JPEG" || ext == ".JPG" || ext == ".BMP" || ext == ".GIF" || ext == ".PNG" || ext == ".PDF")
                    {
                        if ((fup.FileName != "") || (fup.FileName != null))
                        {
                            arr.Add(new cArrayList("@clh_no", lbclaim.Text));
                            arr.Add(new cArrayList("@pay_typ", "CH"));
                            arr.Add(new cArrayList("@doc_typ", cbdoctype.SelectedValue.ToString()));
                            arr.Add(new cArrayList("@paymentby", Request.Cookies["usr_id"].Value.ToString()));
                            arr.Add(new cArrayList("@doc_no", txRefNoPay.Text));
                            arr.Add(new cArrayList("@doc_file", "PY_" + Request.QueryString["dc"] + "_" + countPayment + ext));
                            bll.vInsClaimPayment(arr);
                            arr.Clear();
                            fup.SaveAs(bll.sGetControlParameter("image_path") + "/claim_doc/payment/" + "PY_" + Request.QueryString["dc"] + "_" + countPayment + ext);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "swal('You should upload image','jpg,bmp,gif and png upload document again','warning');", true);
                        return;
                    }
                }
            }
        //}

        //System.Data.SqlClient.SqlDataReader rs = null;
        //arr.Clear();
        //arr.Add(new cArrayList("@sys", "clheaderinv"));
        //arr.Add(new cArrayList("@sysno", ""));
        //bll.vGetDiscountNo(arr, ref rs);
        //while (rs.Read())
        //{
        //    vat_inv = rs["generated"].ToString();
        //}

        arr.Clear();
        arr.Add(new cArrayList("@clh_no", lbclaim.Text));
        //arr.Add(new cArrayList("@inv_no", vat_inv));
        bll.vUpdatePaymentClaimDtl(arr);


        //foreach (GridViewRow row in grdclaim.Rows)
        //{
        //    arr.Clear();
        //    Label lbclaimno = (Label)row.FindControl("lbclaimno");
        //    DropDownList lbstatus = (DropDownList)row.FindControl("status");
        //    arr.Add(new cArrayList("@clh_no", Request.QueryString["dc"]));
        //    arr.Add(new cArrayList("@claim_no", lbclaimno.Text));
        //    arr.Add(new cArrayList("@status", lbstatus.Text));
        //    bll.vUpdClaimPayment(arr);
        //}
        arr.Clear();
        arr.Add(new cArrayList("@clh_no", lbclaim.Text));
        arr.Add(new cArrayList("@payment", "A"));
        bll.vBindingGridToSp(ref grdclaim, "sp_tclaim_header_detail", arr);
        btsave.Visible = false;
    }

    protected void chkSelect(object sender, EventArgs e)
    {
        int countSel = 0;
        string typeClaim = "";
        foreach (GridViewRow row in grdclaim.Rows)
        {
            CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
            if (chckrw.Checked == true)
            {
                countSel++;
            }
        }
        if (countSel > 1)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Select only 1 (one) Claim','Claim No','warning');", true);
            return;
        }
        else 
        {
            foreach (GridViewRow row in grdclaim.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                Label grlbclaim = (Label)row.FindControl("lbclaimno");
                if (chckrw.Checked == true)
                {
                    typeClaim = bll.vLookUp("select top 1 b.discount_mec from tclaim_header a inner join tmst_claim b on a.claim_no = b.claim_no and a.salespointcd = b.salespointcd where a.clh_no = '"+lbclaim.Text+"'");
                    if (typeClaim == "CNDN" || typeClaim == "CSH")
                    {
                        claimDetailPay.Attributes.Remove("style");
                    }
                    else
                    {
                        List<cArrayList> arr = new List<cArrayList>();
                        arr.Add(new cArrayList("@clh_no", lbclaim.Text));
                        arr.Add(new cArrayList("@claim_no", grlbclaim.Text));
                        bll.vBindingGridToSp(ref grdclaimdtl, "sp_tclaim_payment_detail", arr);
                    }
                }
            }
        }
        
    }

    protected void chkSelectdtl(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        string[] itemcode;        
        int countSel = 0;
        foreach (GridViewRow row in grdclaimdtl.Rows)
        {
            CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
            if (chckrw.Checked == true)
            {
                countSel++;
            }
        }
        if (countSel > 1)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Select only 1 (one) Claim','Claim No','warning');", true);
            return;
        }
        else
        {
            foreach (GridViewRow row in grdclaimdtl.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                Label grlbclaim = (Label)row.FindControl("lbclaimno");
                Label product = (Label)row.FindControl("product");
                if (chckrw.Checked == true)
                {
                    itemcode = product.Text.Split('_');
                    arr.Clear();
                    arr.Add(new cArrayList("@clh_no", lbclaim.Text));
                    arr.Add(new cArrayList("@claim_no", grlbclaim.Text));
                    bll.vBindingGridToSp(ref grdpayment, "sp_tclaim_payment_detail_get", arr);
                    // Get Price By Product 
                    arr.Clear();
                    arr.Add(new cArrayList("@item_cd", itemcode[0]));
                    bll.vBindingComboToSp(ref cbPrice, "sp_item_price", "unitprice", "type", arr);
                    
                }
            }
            claimDetailPay.Attributes.Remove("style");
            
        }
        cbPrice.Items.Insert(0, "0.00");
    }


    protected void btnadd_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        int countDetail = 0;
        string claim_no = "", product_claim = "";
        string[] item;
        string typeClaim = "";

        typeClaim = bll.vLookUp("select top 1 b.discount_mec from tclaim_header a inner join tmst_claim b on a.claim_no = b.claim_no and a.salespointcd = b.salespointcd where a.clh_no = '" + lbclaim.Text + "'");

        if (typeClaim != "CNDN" && typeClaim != "CSH")
        {
            foreach (GridViewRow row in grdclaimdtl.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                Label lbclaimno = (Label)row.FindControl("lbclaimno");
                Label product = (Label)row.FindControl("product");
                if (chckrw.Checked == true)
                {
                    countDetail++;
                    claim_no = lbclaimno.Text;
                    product_claim = product.Text;
                }
            }
        }
        else
        {
            foreach (GridViewRow row in grdclaim.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                Label grlbclaim = (Label)row.FindControl("lbclaimno");
                if (chckrw.Checked == true)
                {
                    countDetail++;
                    claim_no = grlbclaim.Text;
                }
            }
        }



        if (countDetail > 1)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Select only 1 (one) Claim.','Payment','warning');", true);
            return;
        }
        if (dtPay.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Payment Date can not be empty.','Payment','warning');", true);
            return;
        }

        if (typeClaim != "CNDN" && typeClaim != "CSH")
        {
            item = product_claim.Split('_');

            arr.Add(new cArrayList("@clh_no", lbclaim.Text));
            arr.Add(new cArrayList("@manual_no", claim_no));
            arr.Add(new cArrayList("@product", item[0]));
            arr.Add(new cArrayList("@ref_no", txRefNoPay.Text));
            arr.Add(new cArrayList("@vref_no", txVendorRefNoPay.Text));
            arr.Add(new cArrayList("@pay_dt", DateTime.ParseExact(dtPay.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@orderqty", txOrderQty.Text));
            arr.Add(new cArrayList("@freeqty", txFreeQty.Text));
            arr.Add(new cArrayList("@freevalue", txFreeValue.Text));
            arr.Add(new cArrayList("@discount", txDiscount.Text));
            arr.Add(new cArrayList("@unitprice", cbPrice.SelectedValue.ToString()));
            arr.Add(new cArrayList("@amount", txAmount.Text));
        }
        else
        {
            arr.Add(new cArrayList("@clh_no", lbclaim.Text));
            arr.Add(new cArrayList("@manual_no", claim_no));
            arr.Add(new cArrayList("@product", ""));
            arr.Add(new cArrayList("@ref_no", txRefNoPay.Text));
            arr.Add(new cArrayList("@vref_no", txVendorRefNoPay.Text));
            arr.Add(new cArrayList("@pay_dt", DateTime.ParseExact(dtPay.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@orderqty", 0));
            arr.Add(new cArrayList("@freeqty", 0));
            arr.Add(new cArrayList("@freevalue", txFreeValue.Text));
            arr.Add(new cArrayList("@discount", 0));
            arr.Add(new cArrayList("@unitprice", 0));
            arr.Add(new cArrayList("@amount", txAmount.Text));
        }

        if (btnadd.Text == "Add")
        {
            arr.Add(new cArrayList("@claim_no", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@status", 'N'));
            bll.vInsertPaymentClaimDtl(arr);
            arr.Clear();
            //chkSelectdtl(sender , e);
            arr.Add(new cArrayList("@clh_no", lbclaim.Text));
            arr.Add(new cArrayList("@claim_no", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdpayment, "sp_tclaim_payment_detail_get", arr);
        }
        else
        {
            arr.Add(new cArrayList("@claim_no", claim_no));
            arr.Add(new cArrayList("@status", 'A'));
            bll.vEditPaymentClaimDtl(arr);
            arr.Clear();
            //chkSelectdtl(sender , e);
            arr.Add(new cArrayList("@clh_no", lbclaim.Text));
            arr.Add(new cArrayList("@claim_no", claim_no));
            bll.vBindingGridToSp(ref grdpayment, "sp_tclaim_payment_detail_get", arr);
        }

        btnadd.Text = "Add";
    }

    protected void txAll_TextChanged(object sender, EventArgs e)
    {
        //double dOrder, dFreeQty, dFreeValue, dUnitprice, dDiscount ;y
        //if (!double.TryParse(txOrderQty.Text, out dOrder))
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('can not empty or non numeric','Payment','warning');", true);
        //    return;
        //}
        //if (!double.TryParse(txFreeQty.Text, out dFreeQty))
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('can not empty or non numeric','Payment','warning');", true);
        //    return;
        //}
        //if (!double.TryParse(txFreeValue.Text, out dFreeValue))
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('can not empty or non numeric','Payment','warning');", true);
        //    return;
        //}
        //if (!double.TryParse(txPrice.Text, out dUnitprice))
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('can not empty or non numeric','Payment','warning');", true);
        //    return;
        //}
        //if (!double.TryParse(txDiscount.Text, out dDiscount))
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('can not empty or non numeric','Payment','warning');", true);
        //    return;
        //}

        if (txOrderQty.Text != "" && txOrderQty.Text != "0" && txOrderQty.Text != "0.00") 
        {
            //if ((txFreeQty.Text != "" && txFreeQty.Text != "0") && (txPrice.Text != "" && txPrice.Text != "0" ))
            if ((txFreeQty.Text != "" && txFreeQty.Text != "0" && txFreeQty.Text != "0.00"))
            {
                txAmount.Text = (Convert.ToDecimal(txFreeQty.Text) * Convert.ToDecimal(cbPrice.SelectedValue.ToString())).ToString();
            }
            else if (txDiscount.Text != "" && txDiscount.Text != "0" && txDiscount.Text != "0.00")
            {
                txAmount.Text = (Convert.ToDecimal(txOrderQty.Text) * Convert.ToDecimal(txDiscount.Text)).ToString();
            }
        }
        else if (txFreeValue.Text != "" && txFreeValue.Text != "0" && txFreeValue.Text != "0.00")
        {
            txAmount.Text = txFreeValue.Text;
        }

    }

    
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        //string lbref_no = ((sender as Button).NamingContainer as GridViewRow).RowIndex;
        string price = "0.00";
        int index = Convert.ToInt32(((sender as Button).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = grdpayment.Rows[index];
        Label lbref_no = (Label)grdpayment.Rows[index].FindControl("lbref_no");
        Label lbvendor_ref_no = (Label)grdpayment.Rows[index].FindControl("lbvendor_ref_no");
        Label lbqtyorder = (Label)grdpayment.Rows[index].FindControl("lbqtyorder");
        Label lbqtyfree = (Label)grdpayment.Rows[index].FindControl("lbqtyfree");
        Label lbdiscount = (Label)grdpayment.Rows[index].FindControl("lbdiscount");
        Label lbunitprice = (Label)grdpayment.Rows[index].FindControl("lbunitprice");
        Label lbfreevalue = (Label)grdpayment.Rows[index].FindControl("lbfreevalue");
        btnadd.Text = "Update";
        txRefNoPay.Text = lbref_no.Text;
        txVendorRefNoPay.Text = lbvendor_ref_no.Text;
        txOrderQty.Text = lbqtyorder.Text.Replace(" ", "").Replace("SAR", "");
        txFreeQty.Text = lbqtyfree.Text.Replace(" ", "").Replace("SAR", "");
        txDiscount.Text = lbdiscount.Text.Replace(" ", "").Replace("SAR", "");
        price = lbunitprice.Text.Replace(" ", "").Replace("SAR","");
        cbPrice.SelectedValue = price.ToString();
        txFreeValue.Text = lbfreevalue.Text.Replace(" ", "").Replace("SAR", "");
        txAll_TextChanged(sender, e);
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        int countDetail = 0;
        string claim_no = "", product_claim = "";
        string[] item;
        foreach (GridViewRow row in grdclaimdtl.Rows)
        {
            CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
            Label lbclaimno = (Label)row.FindControl("lbclaimno");
            Label product = (Label)row.FindControl("product");
            if (chckrw.Checked == true)
            {
                countDetail++;
                claim_no = lbclaimno.Text;
                product_claim = product.Text;
            }
        }

        int index = Convert.ToInt32(((sender as Button).NamingContainer as GridViewRow).RowIndex);
        GridViewRow rowPayment = grdpayment.Rows[index];
        Label lbref_no = (Label)grdpayment.Rows[index].FindControl("lbref_no");
        item = product_claim.Split('_');
        arr.Add(new cArrayList("@clh_no", lbclaim.Text));
        arr.Add(new cArrayList("@claim_no", claim_no));
        arr.Add(new cArrayList("@product", item[0]));
        arr.Add(new cArrayList("@ref_no", lbref_no.Text));
        bll.vDelPaymentClaimDtl(arr);
    }
    protected void grdvat_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "print")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdvat.Rows[index];
            Label lbinvno = (Label)grdvat.Rows[index].FindControl("lbinvno");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=clhvatinv&inv=" + lbinvno.Text + "');", true);
        }
    }
}