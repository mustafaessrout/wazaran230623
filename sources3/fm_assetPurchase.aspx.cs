using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_assetPurchase : System.Web.UI.Page
{
    cbll bll = new cbll();
    Utitlity ut = new Utitlity();
    cdal cdl = new cdal();

    decimal zeroAmount = 0;

    Boolean isAdjustment = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            bindControl();

            dtPurchase.Text = System.DateTime.Today.ToString("d/M/yyyy");
            txtQuantity.Text = "1";
            txtPurchaseAmount.Text = zeroAmount.ToString("0.00");
            txtTaxAmount.Text = zeroAmount.ToString("0.00");

            Session["fileName"] = null;
            Session["fileSize"] = null;
            lbfileloc.Text = string.Empty;
            //btsave.Enabled = false;
            //btprint.Enabled = false;
        }
        mstAsset1.Visible = false;
        mstAsset2.Visible = false;
        reginfo.Visible = false;
        //btcancel.Visible = false;
        btsave.Visible = false;
        btprint.Visible = false;
        btprintall.Visible = false;
        btadjust.Visible = false;
        grid.Visible = true;
    }

    private void bindControl()
    {
        List<cArrayList> arr = new List<cArrayList>();
        bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespointSN_get", "salespointcd", "salespoint_desc", arr);
        var wadiItem = cbsalespoint.Items[cbsalespoint.Items.Count - 2];
        cbsalespoint.Items.Remove(wadiItem);

        //bll.vBindingFieldValueToComboWithALL(ref cbAsset_type, "acc_asset_type");
        List<cArrayList> arrSup = new List<cArrayList>();
        bll.vBindingComboToSp(ref cbSupplierNo, "sp_tmst_supplierSN_get", "supplier_cd", "supplier_desc", arrSup);
        var wadiItem2 = cbSupplierNo.Items[cbSupplierNo.Items.Count - 2];
        cbSupplierNo.Items.Remove(wadiItem2);

        DataTable dt = new DataTable();


        arr.Clear();
        if (dt.Rows.Count > 0)
        {
            dt = new DataTable();
        }
        arr.Add(new cArrayList("@fld_nm", "acc_asset_group"));
        dt = cdl.GetValueFromSP("sp_tfield_value_get", arr);
        ddlAssetGroup.DataSource = dt;
        ddlAssetGroup.DataValueField = "fld_valu";
        ddlAssetGroup.DataTextField = "fld_desc";
        ddlAssetGroup.DataBind();
        //if (dt.Rows.Count > 0)
        //{
        //    ddlAssetGroup.Items.Remove(ddlAssetGroup.Items[1]);
        //    //ddlAssetGroup.Items.Remove(ddlAssetGroup.Items[0]);
        //}
        ddlAssetGroup_SelectedIndexChanged(null, null);
        var firstitem = cbAsset_type.Items[0];

        //cbAsset_type.Items.Clear();
        //cbAsset_type.Items.Remove(firstitem);

        //---------------------
        //
        //arr.Clear();
        //if (dt.Rows.Count > 0)
        //{
        //    dt = new DataTable();
        //}
        //arr.Add(new cArrayList("@fld_nm", "acc_asset_group"));
        //dt = cdl.GetValueFromSP("sp_tfield_value_get", arr);
        //ddlAssetGroup.DataSource = dt;
        //ddlAssetGroup.DataValueField = "fld_valu";
        //ddlAssetGroup.DataTextField = "fld_desc";
        //ddlAssetGroup.DataBind();
        ////if (dt.Rows.Count > 0)
        ////{
        ////    ddlAssetGroup.Items.Remove(ddlAssetGroup.Items[1]);
        ////    //ddlAssetGroup.Items.Remove(ddlAssetGroup.Items[0]);
        ////}
        //ddlAssetGroup_SelectedIndexChanged(null, null);
        ////var firstitem = cbAsset_type.Items[0];
        //
        //---------------------


        arr.Clear();
        if (dt.Rows.Count > 0)
        {
            dt = new DataTable();
        }
        arr.Add(new cArrayList("@fld_nm", "acc_asset_taxType"));
        arr.Sort();
        dt = cdl.GetValueFromSP("sp_tfield_value_get", arr);
        ddlTaxType.DataSource = dt;
        ddlTaxType.DataValueField = "fld_valu";
        ddlTaxType.DataTextField = "fld_desc";
        ddlTaxType.DataBind();
        ddlTaxType.SelectedValue = "VAT";
        arr.Clear();

        arr.Clear();
        if (dt.Rows.Count > 0)
        {
            dt = new DataTable();
        }
        arr.Add(new cArrayList("@fld_nm", "acc_asset_ownershipSource"));
        arr.Sort();
        dt = cdl.GetValueFromSP("sp_tfield_value_get", arr);
        ddlOwnershipSrc.DataSource = dt;
        ddlOwnershipSrc.DataValueField = "fld_valu";
        ddlOwnershipSrc.DataTextField = "fld_desc";
        ddlOwnershipSrc.DataBind();
        ddlOwnershipSrc.SelectedValue = "0";
        arr.Clear();


        BindGrid();
        cbsalespoint.SelectedValue = Convert.ToString(Request.Cookies["sp"].Value);
    }

    private void BindGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        ////arr.Add(new cArrayList("@salespointcd", Convert.ToString(cbsalespoint.SelectedValue)));
        ////arr.Add(new cArrayList("@asset_typ", Convert.ToString(cbAsset_type.SelectedValue)));
        //arr.Add(new cArrayList("@assetPurchaseRegNo", Convert.ToString(txtAssetPurchaseRegNo.Text)));
        ////bll.vBindingGridToSp(ref grd, "Sp_tacc_mst_asset_get", arr);
        //bll.vBindingGridToSp(ref grd, "Sp_tacc_tpurchase_getByID", arr);

        arr.Add(new cArrayList("@search_thing", Convert.ToString(searchBox.Text)));
        arr.Add(new cArrayList("@salespointcd", Convert.ToString(cbsalespoint.Text)));
        bll.vBindingGridToSp(ref grd, "Sp_tacc_tpurchase_get", arr);

        //mstAsset1.Visible = true;
        //mstAsset2.Visible = true;
        //reginfo.Visible = true;
        ////btcancel.Visible = true;
        //btsave.Visible = true;
        //btprint.Visible = true;
        //btprintall.Visible = true;
        //btadjust.Visible = true;
        //grid.Visible = false;

    }
    protected void btlookup_Click(object sender, EventArgs e)
    {

        DataTable dt = new DataTable();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@assetno", Convert.ToString(txtAssetCode.Text)));
        dt = cdl.GetValueFromSP("Sp_tacc_mst_asset_getByID", arr);

        if (dt.Rows.Count > 0)
        {
            bindControl();
            //        @assetno,@asset_nm, @asset_arabic ,@salespointcd ,@asset_typ, @remarks ,@condition ,@purchased_dt,@acquired_dt 
            //,@serialno ,@purchase_place ,@insured ,@insured_company ,@manufacturer ,@model ,@fileimage ,@ecolifemonth ,@asset_sta_id ,getdate() ,@createdby

            txtArabicName.Text = Convert.ToString(dt.Rows[0]["asset_arabic"]);
            txtAssetCode.Text = Convert.ToString(dt.Rows[0]["assetno"]);
            txtEnglishName.Text = Convert.ToString(dt.Rows[0]["asset_nm"]);
            txtRemarks.Text = Convert.ToString(dt.Rows[0]["remarks"]);

            ddlAssetGroup.SelectedValue = Convert.ToString(dt.Rows[0]["assetGroup"]);

            ddlAssetGroup_SelectedIndexChanged(sender, e);
            cbAsset_type.SelectedValue = Convert.ToString(dt.Rows[0]["asset_typ"]);
            cbAsset_type_SelectedIndexChanged(sender, e);
            ddlManufacture.SelectedValue = Convert.ToString(dt.Rows[0]["manufacturer"]);
            ddlManufacture_SelectedIndexChanged(sender, e);

            ddlModel.SelectedValue = Convert.ToString(dt.Rows[0]["model"]);

            btsave.Text = "Save";
            BindGrid();

            txtModelspecification.Focus();
        }
        mstAsset1.Visible = true;
        mstAsset2.Visible = true;
        reginfo.Visible = true;
        //btcancel.Visible = true;
        btsave.Visible = true;
        //btprint.Visible = true;
        //btprintall.Visible = true;
        //btadjust.Visible = true;
        btprint.Visible = false;
        btprintall.Visible = false;
        btadjust.Visible = false;
        grid.Visible = false;
    }

    private void ClearControl()
    {
        bindControl();
        //cbSupplierNo.Text = string.Empty;
        //cbsalespoint.Text = string.Empty;
        txtAssetCondition.Text = string.Empty;
        txtAssetCode.Text = string.Empty;
        txtInvoiceNo.Text = string.Empty;
        dtPurchase.Text = string.Empty;
        txtModelspecification.Text = string.Empty;
        txtSerialno.Text = string.Empty;
        txtPurchasePlace.Text = string.Empty;
        chkInsured.Text = string.Empty;
        txtInsuranceCompany.Text = string.Empty;
        txtECOLifeMonth.Text = string.Empty;
        chkIsSecondHand.Text = string.Empty;
        dtFirstPurchase.Text = string.Empty;
        txtQuantity.Text = "1";
        txtFirstPurchaseAmount.Text = string.Empty;
        txtPurchaseAmount.Text = "0";
        //ddlTaxType.Text = string.Empty;
        txtTaxAmount.Text = "0";
        chkIsDepreciated.Text = string.Empty;
        txtSalvageAmount.Text = string.Empty;
        lbfileloc.Text = string.Empty;
        txtAssetPurchaseRegNo.Text = string.Empty;


        btsave.Text = "Save";
        Session["fileName"] = null;
        Session["fileSize"] = null;
        lbfileloc.Text = string.Empty;
        //btsave.Enabled = false;
        //btprint.Enabled = false;
        txtTotAmountBeforeTax.Text = string.Empty;
        txtTotAmountAfterTax.Text = string.Empty;
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_assetPurchase.aspx");
        //btsave.Enabled = true;
        //btprint.Enabled = true;

    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtEnglishName.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Name can not empty!','Check Name');", true);

                return;
            }
            else if (dtPurchase.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Purchase date can not empty!','Check Name');", true);
                return;
            }
            else if (cbAsset_type.SelectedValue == "ALL" | cbAsset_type.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Please select Asset Type!','Check Asset Type');", true);
                return;
            }
            else if (ddlManufacture.SelectedValue == "ALL" | ddlManufacture.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Please select Manufacture !','Check Manufacture');", true);
                return;
            }
            else if (ddlAssetGroup.SelectedValue == "ALL" | ddlAssetGroup.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Please select Asset Group!','Check Asset Group');", true);
                return;
            }


            List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@supplier_cd", Convert.ToString(cbSupplierNo.Text)));
            arr.Add(new cArrayList("@supplier_cd", Convert.ToString(cbSupplierNo.SelectedItem.Text.Split('|')[0].Trim())));
            arr.Add(new cArrayList("@salespointcd", Convert.ToString(cbsalespoint.Text)));
            arr.Add(new cArrayList("@salespointcd_SN", Convert.ToString(cbsalespoint.SelectedItem.Text.Split('|')[1].Trim())));
            arr.Add(new cArrayList("@asset_typ", Convert.ToString(cbAsset_type.Text)));
            arr.Add(new cArrayList("@condition", Convert.ToString(txtAssetCondition.Text)));
            arr.Add(new cArrayList("@assetno", Convert.ToString(txtAssetCode.Text)));
            arr.Add(new cArrayList("@invoiceNumber", Convert.ToString(txtInvoiceNo.Text)));
            arr.Add(new cArrayList("@purchased_dt", System.DateTime.ParseExact(dtPurchase.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@modelSpecification", Convert.ToString(txtModelspecification.Text)));
            arr.Add(new cArrayList("@serialno", Convert.ToString(txtSerialno.Text)));
            arr.Add(new cArrayList("@purchase_place", Convert.ToString(txtPurchasePlace.Text)));
            arr.Add(new cArrayList("@insured", Convert.ToBoolean(Convert.ToInt32(chkInsured.Checked ? true : false))));
            arr.Add(new cArrayList("@insurance_company", Convert.ToString(txtInsuranceCompany.Text)));
            arr.Add(new cArrayList("@ecolifemonth", Convert.ToString(txtECOLifeMonth.Text)));
            arr.Add(new cArrayList("@cashOut", ""));
            arr.Add(new cArrayList("@isOldAsset", Convert.ToBoolean(Convert.ToInt32(chkIsSecondHand.Checked ? true : false))));
            arr.Add(new cArrayList("@oldPurchaseDate", System.DateTime.ParseExact(dtFirstPurchase.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@assetQty", Convert.ToString(txtQuantity.Text)));
            arr.Add(new cArrayList("@oldPurchaseAmount", Convert.ToString(txtFirstPurchaseAmount.Text).Replace(",", "")));
            arr.Add(new cArrayList("@currentPurchaseAmount", Convert.ToString(txtPurchaseAmount.Text).Replace(",", "")));
            arr.Add(new cArrayList("@taxType", Convert.ToString(ddlTaxType.Text)));
            arr.Add(new cArrayList("@taxAmount", Convert.ToString(txtTaxAmount.Text).Replace(",", "")));
            arr.Add(new cArrayList("@isDepreciation", Convert.ToBoolean(Convert.ToInt32(chkIsDepreciated.Checked ? true : false))));
            arr.Add(new cArrayList("@salvageAmount", Convert.ToString(txtSalvageAmount.Text).Replace(",", "")));
            //arr.Add(new cArrayList("@fileimage", Convert.ToString(Convert.ToString());
            arr.Add(new cArrayList("@fileimage", Convert.ToString(Convert.ToString(txtAssetPurchaseRegNo.Text) + "-" + lbfileloc.Text) + 1));
            //arr.Add(new cArrayList("@fileimage", Convert.ToString(lbfileloc.Text)));
            //arr.Add(new cArrayList("@fileImagePathPart", Convert.ToString("account/assetpurchase/")));
            arr.Add(new cArrayList("@ownershipSrc", Convert.ToString(ddlOwnershipSrc.SelectedValue)));
            arr.Add(new cArrayList("@remarks2", Convert.ToString(txtRemarks2.Text)));
            arr.Add(new cArrayList("@totPurchasePriceBefTax", Convert.ToString(txtTotAmountBeforeTax.Text).Replace(",", "")));
            arr.Add(new cArrayList("@totPurchasePriceAfTax", Convert.ToString(txtTotAmountAfterTax.Text).Replace(",", "")));
            arr.Add(new cArrayList("@lastEconomicLifeDate", System.DateTime.ParseExact(dtPurchase.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).AddMonths(Convert.ToInt32(txtECOLifeMonth.Text))));
            arr.Add(new cArrayList("@user", Request.Cookies["usr_id"].Value.ToString()));



            //string assetno = string.Empty;


            if (btsave.Text == "Save")
            {
                string assetPurchaseRegNo = string.Empty;
                //string assetPurchaseRegNo = null;

                //arr.Add(new cArrayList("@fileimage", Convert.ToString(Session["fileName"])));
                arr.Add(new cArrayList("@fileimage2", Convert.ToString(lbfileloc.Text)));
                bll.vInsAssetPurchase(arr, ref assetPurchaseRegNo);
                //bll.vInsAssetPurchase(arr);
                //btsave.Enabled = true;
                //btprint.Enabled = true;
                if (assetPurchaseRegNo == "-2")
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not insert duplicate ','Can not insert duplicate','warning');", true);
                    cbsalespoint_SelectedIndexChanged(sender, e);
                    cbsupplier_SelectedIndexChanged(sender, e);

                }
                else
                {
                    Session["fileName"] = null;
                    txtAssetPurchaseRegNo.Text = assetPurchaseRegNo;
                    //txtAssetPurchaseRegNo.Text = bll.vLookUp("select assetPurchaseRegNo from tacc_tpurchase where ID in (select max(ID) from tacc_tpurchase)");
                    UploadFiles(Convert.ToString(txtAssetPurchaseRegNo.Text) + Convert.ToString(cbsalespoint.Text));
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Record Inserted successfully  " + assetPurchaseRegNo + "','Insert Success','success');", true);
                    cbsalespoint_SelectedIndexChanged(sender, e);
                    cbsupplier_SelectedIndexChanged(sender, e);
                    ClearControl();
                }

                //List<cArrayList> arrDepre = new List<cArrayList>();
                //arrDepre.Add(new cArrayList("@assetPurchaseRegNo", Convert.ToString(txtAssetPurchaseRegNo.Text)));
                ////doGenerateDepreciationSchedule(arrDepre);
                //bll.doGenerateDepreciationSchedule(arrDepre);

            }
            else if (btsave.Text == "Update")
            {
                //btsave.Enabled = true;
                //btprint.Enabled = true;
                UploadFiles(Convert.ToString(txtAssetPurchaseRegNo.Text) + Convert.ToString(cbsalespoint.Text));
                Session["fileName"] = null;
                if (txtPurchaseAmount.ReadOnly == false)
                //if (isAdjustment == true)
                {
                    List<cArrayList> arr2 = new List<cArrayList>();
                    arr2.Add(new cArrayList("@assetPurchaseRegNo", Convert.ToString(txtAssetPurchaseRegNo.Text)));
                    //arr2.Add(new cArrayList("@assetAdjustmentDate", System.DateTime.ParseExact(dtPurchase.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                    arr2.Add(new cArrayList("@newCurrentPurchaseAmount", Convert.ToString(txtPurchaseAmount.Text).Replace(",", "")));
                    arr2.Add(new cArrayList("@newAssetQty", Convert.ToString(txtQuantity.Text)));
                    arr2.Add(new cArrayList("@newTaxAmount", Convert.ToString(txtTaxAmount.Text).Replace(",", "")));
                    arr2.Add(new cArrayList("@newEcoLifeMonth", Convert.ToString(txtECOLifeMonth.Text)));
                    arr2.Add(new cArrayList("@newSalvageAmount", Convert.ToString(txtSalvageAmount.Text)));

                    //arr2.Add(new cArrayList("@purchased_dt", System.DateTime.ParseExact(dtPurchase.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                    //arr2.Add(new cArrayList("@ecolifemonth", Convert.ToString(txtECOLifeMonth.Text)));
                    //arr2.Add(new cArrayList("@assetQty", Convert.ToString(txtQuantity.Text)));
                    //arr2.Add(new cArrayList("@currentPurchaseAmount", Convert.ToString(txtPurchaseAmount.Text).Replace(",", "")));
                    //arr2.Add(new cArrayList("@taxAmount", Convert.ToString(txtTaxAmount.Text).Replace(",", "")));
                    //arr2.Add(new cArrayList("@salvageAmount", Convert.ToString(txtSalvageAmount.Text).Replace(",", "")));
                    arr2.Add(new cArrayList("@newTotPurchasePriceAfTax", Convert.ToString(txtTotAmountAfterTax.Text).Replace(",", "")));
                    arr2.Add(new cArrayList("@user", Request.Cookies["usr_id"].Value.ToString()));


                    bll.vUpdateAssetPurchaseAdjustment(arr2);
                }
                arr.Add(new cArrayList("@assetPurchaseRegNo", Convert.ToString(txtAssetPurchaseRegNo.Text)));
                arr.Add(new cArrayList("@fileimage2", Convert.ToString(lbfileloc.Text)));

                //arr.Add(new cArrayList("@isAdjustment", Convert.ToString(isAdjustment)));
                //arr.Add(new cArrayList("@assetAdjustmentDate", System.DateTime.ParseExact(dtPurchase.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                //arr.Add(new cArrayList("@newCurrentPurchaseAmount", Convert.ToString(txtPurchaseAmount.Text).Replace(",", "")));
                //arr.Add(new cArrayList("@newAssetQty", Convert.ToString(txtQuantity.Text)));
                //arr.Add(new cArrayList("@newTaxAmount", Convert.ToString(txtTaxAmount.Text).Replace(",", "")));
                //arr.Add(new cArrayList("@newEcoLifeMonth", Convert.ToString(txtECOLifeMonth.Text)));

                bll.vUpdateAssetPurchase(arr);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Record Updated successfully   " + Convert.ToString(txtAssetPurchaseRegNo.Text) + "','Update Success','success');", true);
                cbsalespoint_SelectedIndexChanged(sender, e);
                cbsupplier_SelectedIndexChanged(sender, e);
                ClearControl();

                txtQuantity.BackColor = System.Drawing.Color.LightGray;
                txtPurchaseAmount.BackColor = System.Drawing.Color.LightGray;
                txtTaxAmount.BackColor = System.Drawing.Color.LightGray;
                txtECOLifeMonth.BackColor = System.Drawing.Color.LightGray;
                txtSalvageAmount.BackColor = System.Drawing.Color.LightGray;
            }
            btsearch.Visible = true;
            mstAsset1.Visible = false;
            mstAsset2.Visible = false;
            reginfo.Visible = false;
            //btcancel.Visible = false;
            btsave.Visible = false;
            btprint.Visible = false;
            btprintall.Visible = false;
            btadjust.Visible = false;
            grid.Visible = true;

            Response.Redirect("fm_assetPurchase.aspx");
        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','" + ex.Message + " " + ex.InnerException + "','error');", true);
            //app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Save is not success!','Save is not success');", true);
            ut.Logs("", "Admin", "Equipment Maintenance", "fr_HOEquipmentMaintenance", "btnUpload_Click", "Exception", ex.Message + ex.InnerException);
        }
    }

    private void UploadFiles(string extendendName)
    {
        if (Session["fileSize"] == null | Session["fileSize"] == "")
        { }
        else
        {
            string imagePath = null;
            if (Directory.Exists(bll.sGetControlParameter("image_path")))
            {
                imagePath = bll.sGetControlParameter("image_path");
            }
            else
            {
                imagePath = "D:\\svn2\\sources3\\images";
            }

            //if (File.Exists(bll.sGetControlParameter("image_path") + "\\account\\assetpurchase\\" + Convert.ToString(hdfOldFileName.Value)))
            //{
            //    File.Delete(bll.sGetControlParameter("image_path") + "\\account\\assetpurchase\\" + Convert.ToString(hdfOldFileName.Value));
            //    hdfOldFileName.Value = string.Empty;
            //}

            if (File.Exists(imagePath + "\\account\\assetpurchase\\" + Convert.ToString(hdfOldFileName.Value)))
            {
                File.Delete(imagePath + "\\account\\assetpurchase\\" + Convert.ToString(hdfOldFileName.Value));
                hdfOldFileName.Value = string.Empty;
            }

            byte[] fs = (byte[])Session["fileSize"];
            //File.WriteAllBytes(bll.sGetControlParameter("image_path") + "/account/assetpurchase/" + extendendName + "-" + lbfileloc.Text, fs);
            //File.WriteAllBytes(imagePath + "\\account\\assetpurchase\\" + extendendName.Substring(0, extendendName.Length - 4) + "___" + lbfileloc.Text, fs);
            File.WriteAllBytes(imagePath + "\\account\\assetpurchase\\" + extendendName + "___" + lbfileloc.Text, fs);


            Session["fileSize"] = null;
            Session["fileName"] = null;
            //lbfileloc.Text = string.Empty;
        }

        mstAsset1.Visible = true;
        mstAsset2.Visible = true;
        reginfo.Visible = true;
        //btcancel.Visible = true;
        btsave.Visible = true;
        //btprint.Visible = true;
        //btprintall.Visible = true;
        //btadjust.Visible = true;
        btprint.Visible = false;
        btprintall.Visible = false;
        btadjust.Visible = false;
        grid.Visible = false;
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //cbAsset_type.Enabled = false;

        mstAsset1.Visible = true;
        mstAsset2.Visible = true;
        reginfo.Visible = true;
        //btcancel.Visible = true;
        btsave.Visible = true;
        btprint.Visible = true;
        btprintall.Visible = true;
        btadjust.Visible = true;
        grid.Visible = false;

        Label lblAssetPurchaseRegisterno = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblAssetPurchaseRegisterno");
        DataTable dt = new DataTable();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@assetPurchaseRegNo", Convert.ToString(lblAssetPurchaseRegisterno.Text)));
        dt = cdl.GetValueFromSP("Sp_tacc_tpurchase_getByID", arr);

        if (dt.Rows.Count > 0)
        {
            //bindControl();
            //        @assetno,@asset_nm, @asset_arabic ,@salespointcd ,@asset_typ, @remarks ,@condition ,@purchased_dt,@acquired_dt 
            //,@serialno ,@purchase_place ,@insured ,@insured_company ,@manufacturer ,@model ,@fileimage ,@ecolifemonth ,@asset_sta_id ,getdate() ,@createdby

            bindControl();
            txtAssetPurchaseRegNo.Text = Convert.ToString(dt.Rows[0]["assetPurchaseRegNo"]);
            txtInvoiceNo.Text = Convert.ToString(dt.Rows[0]["invoiceNumber"]);
            if (Convert.ToString(dt.Rows[0]["supplier_cd"]) != string.Empty)
            {
                cbSupplierNo.Enabled = false;

                cbSupplierNo.SelectedValue = Convert.ToString(dt.Rows[0]["supplier_cd"]);
            }
            else
            {
                cbSupplierNo.Enabled = true;
                List<cArrayList> arrSup = new List<cArrayList>();
                bll.vBindingComboToSp(ref cbSupplierNo, "sp_tmst_supplierSN_get", "supplier_cd", "supplier_desc", arrSup);

            }
            txtArabicName.Text = Convert.ToString(dt.Rows[0]["asset_arabic"]);
            txtEnglishName.Text = Convert.ToString(dt.Rows[0]["asset_nm"]);
            txtAssetCode.Text = Convert.ToString(dt.Rows[0]["assetno"]);
            ddlAssetGroup.SelectedValue = Convert.ToString(dt.Rows[0]["assetGroup"]);
            ddlAssetGroup_SelectedIndexChanged(sender, e);
            cbAsset_type.SelectedValue = Convert.ToString(dt.Rows[0]["asset_typ"]);
            cbAsset_type_SelectedIndexChanged(sender, e);
            ddlManufacture.SelectedValue = Convert.ToString(dt.Rows[0]["manufacturer"]);
            ddlManufacture_SelectedIndexChanged(sender, e);
            ddlModel.SelectedValue = Convert.ToString(dt.Rows[0]["model"]);
            txtRemarks.Text = Convert.ToString(dt.Rows[0]["remarks1"]);

            //cbsalespoint.SelectedValue = Convert.ToString(dt.Rows[0]["salespointcd"]);
            txtModelspecification.Text = Convert.ToString(dt.Rows[0]["modelspecification"]);
            txtSerialno.Text = Convert.ToString(dt.Rows[0]["serialno"]);
            txtAssetCondition.Text = Convert.ToString(dt.Rows[0]["condition"]);
            if (Convert.ToBoolean(dt.Rows[0]["insured"]) == true)
            {
                chkInsured.Checked = true;
            }
            else
            {
                chkInsured.Checked = false;
            }
            txtInsuranceCompany.Text = Convert.ToString(dt.Rows[0]["insured_company"]);
            //dtPurchase.Text = string.Format("{0:yyyyMMdd}", dt.Rows[0]["purchased_dt"]);
            dtPurchase.Text = string.Format("{0:dd/MM/yyyy}", dt.Rows[0]["purchased_dt"]);
            txtPurchasePlace.Text = Convert.ToString(dt.Rows[0]["purchase_place"]);
            txtQuantity.Text = Convert.ToString(dt.Rows[0]["assetQty"]);
            txtPurchaseAmount.Text = Convert.ToDecimal(dt.Rows[0]["currentPurchaseAmount"]).ToString(("###,###.00"));
            if (Convert.ToBoolean(dt.Rows[0]["isOldAsset"]) == true)
            {
                chkIsSecondHand.Checked = true;
            }
            else
            {
                chkIsSecondHand.Checked = false;
            }
            //dtFirstPurchase.Text = string.Format("{0:yyyyMMdd}", dt.Rows[0]["oldPurchaseDate"]);
            dtFirstPurchase.Text = string.Format("{0:dd/MM/yyyy}", dt.Rows[0]["oldPurchaseDate"]);
            txtFirstPurchaseAmount.Text = Convert.ToDecimal(dt.Rows[0]["oldPurchaseAmount"]).ToString(("###,###.00")); ;
            ddlTaxType.SelectedValue = Convert.ToString(dt.Rows[0]["taxType"]);
            ddlTaxType_SelectedIndexChanged(sender, e);
            txtTaxAmount.Text = Convert.ToDecimal(dt.Rows[0]["taxAmount"]).ToString(("###,###.00")); ;
            if (Convert.ToBoolean(dt.Rows[0]["isDepreciation"]) == true)
            {
                chkIsDepreciated.Checked = true;
            }
            else
            {
                chkIsDepreciated.Checked = false;
            }
            txtECOLifeMonth.Text = Convert.ToString(dt.Rows[0]["ecolifemonth"]);
            txtSalvageAmount.Text = Convert.ToDecimal(dt.Rows[0]["salvageAmount"]).ToString(("###,###.00"));
            //lbfileloc.Text = Convert.ToString(dt.Rows[0]["fileimage2"]);
            lbfileloc.Text = Convert.ToString(dt.Rows[0]["fileimage2"]).Substring(Convert.ToString(dt.Rows[0]["fileimage2"]).LastIndexOf("___") + 3);
            txtRemarks2.Text = Convert.ToString(dt.Rows[0]["remarks2"]);

            //hdfOldFileName.Value = Convert.ToString(dt.Rows[0]["fileimage2"]);
            hdfOldFileName.Value = Convert.ToString(dt.Rows[0]["fileimage2"]).Substring(Convert.ToString(dt.Rows[0]["fileimage2"]).LastIndexOf("___") + 3);


            Session["fileName"] = Convert.ToString(dt.Rows[0]["fileimage"]);
            btsave.Text = "Update";

            BindGrid();

            btsearch.Visible = false;
            txtQuantity.ReadOnly = true;
            txtPurchaseAmount.ReadOnly = true;
            txtTaxAmount.ReadOnly = true;
            txtECOLifeMonth.ReadOnly = true;
            txtSalvageAmount.ReadOnly = true;
            dtPurchase.Enabled = false;
            txtTotAmountBeforeTax.Text = Convert.ToDecimal(dt.Rows[0]["totPurchasePriceBefTax"]).ToString(("###,###.00"));
            txtTotAmountAfterTax.Text = Convert.ToDecimal(dt.Rows[0]["totPurchasePriceAfTax"]).ToString(("###,###.00"));

        }
    }

    protected void btDelete_Click(object sender, EventArgs e)
    {
        string AssetPurchaseRegNo = txtAssetPurchaseRegNo.Text;
        if (txtAssetPurchaseRegNo.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please select record first. ','select record','warning');", true);
        }
        else
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@assetPurchaseRegNo", Convert.ToString(txtAssetPurchaseRegNo.Text)));
            bll.vDeleteAssetPurchase(arr);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Record Deleted successfully ','Deleted Success " + AssetPurchaseRegNo + "','success');", true);
            ClearControl();
            cbsalespoint_SelectedIndexChanged(sender, e);
            cbsupplier_SelectedIndexChanged(sender, e);
            //btsave.Enabled = false;
            //btprint.Enabled = false;
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@assetPurchaseRegNo", Convert.ToString(txtAssetPurchaseRegNo.Text)));
        Session["lParampurchase"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('/fm_report2.aspx?src=tpurchase');", true);
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            mstAsset1.Visible = true;
            mstAsset2.Visible = true;
            reginfo.Visible = true;
            //btcancel.Visible = true;
            btsave.Visible = true;
            //btprint.Visible = true;
            //btprintall.Visible = true;
            //btadjust.Visible = true;            
            btprint.Visible = false;
            btprintall.Visible = false;
            btadjust.Visible = false;
            grid.Visible = false;

            if (Session["fileName"] == null | Session["fileName"] == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Please upload files!','Check file');", true);
            }
            else
            {
                string imagePath = null;
                if (Directory.Exists(bll.sGetControlParameter("image_path")))
                {
                    imagePath = bll.sGetControlParameter("image_path") + "account\\assetpurchase\\";
                }
                else
                {
                    imagePath = "D:\\svn2\\sources3\\images\\account\assetpurchase\\";
                }

                //DirectoryInfo dir = new DirectoryInfo(bll.sGetControlParameter("image_path") + "/account/asset/");
                DirectoryInfo dir = new DirectoryInfo(imagePath);

                string fileName = Convert.ToString(Session["fileName"]);
                string fileLocation = Convert.ToString(Session["fileLocation"]);
                //lbfileloc.Text = fileName;
                lbfileloc.Text = fileName.Substring(4);


                //string baseName =VirtualPathUtility.GetDirectory( "~/admin/ImageFolder/" + fileName);
                //lkl.HRef = "/images/account/asset/" + fileName;
                //Avatar.ImageUrl = baseName;
                //hdfFileName.Value = fileName;
                //hdfFilePath.Value = fileLocation;
            }
        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','" + ex.Message + " " + ex.InnerException + "','error');", true);
            //app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('File upload failed!','File upload failed');", true);
            ut.Logs("", "Admin", "Equipment Maintenance", "fr_HOEquipmentMaintenance", "btnUpload_Click", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        BindGrid();
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        BindGrid();
    }
    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void cbsupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void ddlAssetGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();

        arr.Add(new cArrayList("@fld_nm", ddlAssetGroup.SelectedValue));
        //dt = cdl.GetValueFromSP("sp_tfield_valueWithfld_get", arr);
        dt = cdl.GetValueFromSP("sp_tfield_value_get", arr);
        cbAsset_type.DataSource = dt;
        cbAsset_type.DataValueField = "fld_valu";
        cbAsset_type.DataTextField = "fld_desc";
        cbAsset_type.DataBind();
        cbAsset_type_SelectedIndexChanged(sender, e);

    }
    protected void cbAsset_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
        //if (dt.Rows.Count > 0)
        //{
        //    dt = new DataTable();
        //}
        arr.Add(new cArrayList("@fld_nm", "acc_" + cbAsset_type.SelectedValue));
        dt = cdl.GetValueFromSP("sp_tfield_value_get", arr);
        ddlManufacture.DataSource = dt;
        ddlManufacture.DataValueField = "fld_valu";
        ddlManufacture.DataTextField = "fld_desc";
        ddlManufacture.DataBind();
        ddlManufacture_SelectedIndexChanged(sender, e);

        BindGrid();
    }


    protected void ddlTaxType_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
        //if (dt.Rows.Count > 0)
        //{
        //    dt = new DataTable();
        //}
        arr.Add(new cArrayList("@fld_nm", ddlTaxType.SelectedValue));
        dt = cdl.GetValueFromSP("sp_tfield_valueWithfld_get", arr);
        ddlTaxType.DataSource = dt;
        if (dt.Rows.Count > 1)
        {
            ddlTaxType.DataValueField = "fld_valu";
            ddlTaxType.DataTextField = "fld_desc";
            ddlTaxType.DataBind();
            ddlTaxType_SelectedIndexChanged(sender, e);
        }
    }

    protected void ddlManufacture_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
        if (dt.Rows.Count > 0)
        {
            dt = new DataTable();
        }
        arr.Add(new cArrayList("@fld_nm", ddlManufacture.SelectedValue));
        dt = cdl.GetValueFromSP("sp_tfield_value_get", arr);
        ddlModel.DataSource = dt;
        ddlModel.DataValueField = "fld_valu";
        ddlModel.DataTextField = "fld_desc";
        ddlModel.DataBind();
    }
    protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //protected void ddlOwnershipSrc_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    List<cArrayList> arr = new List<cArrayList>();
    //    DataTable dt = new DataTable();
    //    //if (dt.Rows.Count > 0)
    //    //{
    //    //    dt = new DataTable();
    //    //}
    //    arr.Add(new cArrayList("@fld_nm", "acc_asset_ownershipSource" + ddlOwnershipSrc.SelectedValue));
    //    dt = cdl.GetValueFromSP("sp_tfield_value_get", arr);
    //    ddlOwnershipSrc.DataSource = dt;
    //    ddlOwnershipSrc.DataValueField = "fld_valu";
    //    ddlOwnershipSrc.DataTextField = "fld_desc";
    //    ddlOwnershipSrc.DataBind();
    //    ddlOwnershipSrc_SelectedIndexChanged(sender, e);

    //    BindGrid();
    //}

    protected void AmountOrQuantityChanged(object sender, EventArgs e)
    {
        decimal purchaseAmount = 0;
        int qty = 1;
        decimal taxAmount = 0;
        decimal totAmountBefTax = 0;
        decimal totAmountAftTax = 0;

        if (!Decimal.TryParse(txtPurchaseAmount.Text, out purchaseAmount))
        {
            txtValidation.Text = "Not a valid purchase amount format";
            return;
        }
        else if (!Int32.TryParse(txtQuantity.Text, out qty))
        {
            txtValidation.Text = "Not a valid quantity format";
            return;
        }

        totAmountBefTax = purchaseAmount * qty;
        txtTotAmountBeforeTax.Text = totAmountBefTax.ToString("###,###.00"); ;


        if (!Decimal.TryParse(txtTaxAmount.Text, out taxAmount))
        {
            txtValidation.Text = "Not a valid tax amount format";
            return;
        }
        totAmountAftTax = totAmountBefTax + taxAmount;
        txtTotAmountAfterTax.Text = totAmountAftTax.ToString("###,###.00");

        mstAsset1.Visible = true;
        mstAsset2.Visible = true;
        reginfo.Visible = true;
        //btcancel.Visible = true;
        btsave.Visible = true;
        btprint.Visible = true;
        btprintall.Visible = true;
        btadjust.Visible = true;
        grid.Visible = false;
    }

    //protected void doGenerateDepreciationSchedule(List<cArrayList> arrDepre)
    //{
    //    decimal purchaseAmount = 0;
    //}
    protected void depreciationSchedule_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@assetPurchaseRegNo", Convert.ToString(txtAssetPurchaseRegNo.Text)));
        arr.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
        Session["lParamdepreciationschedule"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('/fm_report2.aspx?src=depreciation_schedule');", true);

        mstAsset1.Visible = true;
        mstAsset2.Visible = true;
        reginfo.Visible = true;
        //btcancel.Visible = true;
        btsave.Visible = true;
        btprint.Visible = true;
        btprintall.Visible = true;
        btadjust.Visible = true;
        grid.Visible = false;
    }

    protected void btcancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_assetPurchase.aspx");
    }

    //protected void OnPaging(object sender, GridViewPageEventArgs e)
    //{
    //    grd.PageIndex = e.NewPageIndex;
    //    grd.DataBind();
    //}

    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        BindGrid();
        //grd.DataBind();
    }
    protected void searchButton_Click(object sender, EventArgs e)
    {
        BindGrid();
        //grid.DataBind();

    }
    protected void resetSearchButton_Click(object sender, EventArgs e)
    {
        searchBox.Text = string.Empty;
        BindGrid();
        //grd.DataBind();

    }

    protected void btadjust_Click(object sender, EventArgs e)
    {
        mstAsset1.Visible = true;
        mstAsset2.Visible = true;
        reginfo.Visible = true;
        //btcancel.Visible = true;
        btsave.Visible = true;
        btprint.Visible = true;
        btprintall.Visible = true;
        btadjust.Visible = true;
        grid.Visible = false;

        txtQuantity.BackColor = System.Drawing.Color.LightYellow;
        txtPurchaseAmount.BackColor = System.Drawing.Color.LightYellow;
        txtTaxAmount.BackColor = System.Drawing.Color.LightYellow;
        txtECOLifeMonth.BackColor = System.Drawing.Color.LightYellow;
        txtSalvageAmount.BackColor = System.Drawing.Color.LightYellow;

        txtQuantity.ReadOnly = false;
        txtPurchaseAmount.ReadOnly = false;
        txtTaxAmount.ReadOnly = false;
        txtECOLifeMonth.ReadOnly = false;
        txtSalvageAmount.ReadOnly = false;

        isAdjustment = true;
    }

}
