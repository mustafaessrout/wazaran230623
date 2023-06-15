using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mst_asset : System.Web.UI.Page
{
    cbll bll = new cbll();
    Utitlity ut = new Utitlity();
    cdal cdl = new cdal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            bindControl();

            dtAcquired.Text = System.DateTime.Today.ToString("d/M/yyyy");
            dtPurchase.Text = System.DateTime.Today.ToString("d/M/yyyy");

            Session["fileName"] = null;
            Session["fileSize"] = null;
            lbfileloc.Text = string.Empty;
            //btsave.Enabled = false;
            //btprint.Enabled = false;
        }
    }

    private void bindControl()
    {
        List<cArrayList> arr = new List<cArrayList>();
        bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespointSN_get", "salespointcd", "salespoint_desc", arr);
        var wadiItem = cbsalespoint.Items[cbsalespoint.Items.Count - 2];
        cbsalespoint.Items.Remove(wadiItem);

        //bll.vBindingFieldValueToComboWithALL(ref cbAsset_type, "acc_asset_type");

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
        if (dt.Rows.Count > 0)
        {
            ddlAssetGroup.Items.Remove(ddlAssetGroup.Items[1]);
            //ddlAssetGroup.Items.Remove(ddlAssetGroup.Items[0]);
        }
        ddlAssetGroup_SelectedIndexChanged(null, null);
        var firstitem = cbAsset_type.Items[0];

        //cbAsset_type.Items.Clear();
        //cbAsset_type.Items.Remove(firstitem);
        BindGrid();
        cbsalespoint.SelectedValue = Convert.ToString(Request.Cookies["sp"].Value);
    }

    private void BindGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@salespointcd", Convert.ToString(cbsalespoint.SelectedValue)));
        arr.Add(new cArrayList("@asset_typ", Convert.ToString(cbAsset_type.SelectedValue)));
        bll.vBindingGridToSp(ref grd, "Sp_tacc_mst_asset_get", arr);
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
            txtAssetCondition.Text = Convert.ToString(dt.Rows[0]["condition"]);
            txtECOLifeMonth.Text = Convert.ToString(dt.Rows[0]["ecolifemonth"]);
            txtEnglishName.Text = Convert.ToString(dt.Rows[0]["asset_nm"]);
            txtInsured.Text = Convert.ToString(dt.Rows[0]["insured"]);
            txtInsuredCompany.Text = Convert.ToString(dt.Rows[0]["insured_company"]);
            txtPurchasePlace.Text = Convert.ToString(dt.Rows[0]["purchase_place"]);
            txtRemarks.Text = Convert.ToString(dt.Rows[0]["remarks"]);
            txtSerialno.Text = Convert.ToString(dt.Rows[0]["serialno"]);
            txtModelNumber.Text = Convert.ToString(dt.Rows[0]["modelNumber"]);
            cbAsset_type.SelectedValue = Convert.ToString(dt.Rows[0]["asset_typ"]);
            cbsalespoint.SelectedValue = Convert.ToString(dt.Rows[0]["salespointcd"]);
            ddlAssetGroup.SelectedValue = Convert.ToString(dt.Rows[0]["assetGroup"]);
            ddlManufacture.SelectedValue = Convert.ToString(dt.Rows[0]["manufacturer"]);
            ddlModel.SelectedValue = Convert.ToString(dt.Rows[0]["model"]);
            Session["fileName"] = Convert.ToString(dt.Rows[0]["fileimage"]);
            btsave.Text = "Update";
            BindGrid();
        }
    }

    private void ClearControl()
    {
        bindControl();
        txtArabicName.Text = string.Empty;
        txtAssetCode.Text = string.Empty;
        txtAssetCondition.Text = string.Empty;
        txtECOLifeMonth.Text = string.Empty;
        txtEnglishName.Text = string.Empty;
        txtInsured.Text = string.Empty;
        txtInsuredCompany.Text = string.Empty;
        txtModelNumber.Text = string.Empty;

        txtPurchasePlace.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        txtSerialno.Text = string.Empty;
        txtModelNumber.Text = string.Empty;
        dtAcquired.Text = System.DateTime.Today.ToString("d/M/yyyy");
        dtPurchase.Text = System.DateTime.Today.ToString("d/M/yyyy");

        btsave.Text = "Save";
        Session["fileName"] = null;
        Session["fileSize"] = null;
        lbfileloc.Text = string.Empty;
        //btsave.Enabled = false;
        //btprint.Enabled = false;
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_mst_asset.aspx");
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
            else if (cbAsset_type.SelectedValue == "ALL" || cbAsset_type.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Please select Asset Type!','Check Asset Type');", true);
                return;
            }
            else if (ddlManufacture.SelectedValue == "ALL" || ddlManufacture.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Please select Manufacture !','Check Manufacture');", true);
                return;
            }
            else if (ddlAssetGroup.SelectedValue == "ALL" || ddlAssetGroup.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Please select Asset Group!','Check Asset Group');", true);
                return;
            }


            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@asset_nm", Convert.ToString(txtEnglishName.Text)));
            arr.Add(new cArrayList("@asset_arabic", Convert.ToString(txtArabicName.Text)));
            arr.Add(new cArrayList("@salespointcd", Convert.ToString(cbsalespoint.Text)));
            arr.Add(new cArrayList("@salespointcd_SN", Convert.ToString(cbsalespoint.SelectedItem.Text.Split('|')[1].Trim())));
            arr.Add(new cArrayList("@asset_typ", Convert.ToString(cbAsset_type.SelectedValue)));
            arr.Add(new cArrayList("@remarks", Convert.ToString(txtRemarks.Text)));
            arr.Add(new cArrayList("@condition", Convert.ToString(txtAssetCondition.Text)));
            arr.Add(new cArrayList("@purchased_dt", System.DateTime.ParseExact(dtPurchase.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@acquired_dt", System.DateTime.ParseExact(dtAcquired.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@serialno", Convert.ToString(txtSerialno.Text)));
            arr.Add(new cArrayList("@purchase_place", Convert.ToString(txtPurchasePlace.Text)));
            arr.Add(new cArrayList("@insured", Convert.ToString(txtInsured.Text)));
            arr.Add(new cArrayList("@insured_company", Convert.ToString(txtInsuredCompany.Text)));
            arr.Add(new cArrayList("@manufacturer", Convert.ToString(ddlManufacture.SelectedValue)));
            arr.Add(new cArrayList("@model", Convert.ToString(ddlModel.SelectedValue)));
            //arr.Add(new cArrayList("@modelNumber", Convert.ToString(txtModelNumber.Text)));
            arr.Add(new cArrayList("@assetGroup", Convert.ToString(ddlAssetGroup.SelectedValue)));
            arr.Add(new cArrayList("@ecolifemonth", Convert.ToString(txtECOLifeMonth.Text)));
            //arr.Add(new cArrayList("@fileimage", Convert.ToString(lbfileloc.Text)));
            arr.Add(new cArrayList("@fileimage", Convert.ToString(Session["fileName"])));
            arr.Add(new cArrayList("@asset_sta_id", "A"));            
            arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));


            string assetno = string.Empty;


            if (btsave.Text == "Save")
            {
                arr.Add(new cArrayList("@fileimage", Convert.ToString(Session["fileName"])));
                //arr.Add(new cArrayList("@fileimage", Convert.ToString(lbfileloc.Text)));
                bll.vInsMastAsset(arr, ref assetno);
                //btsave.Enabled = true;
                //btprint.Enabled = true;
                if (assetno == "-2")
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Can not insert duplicate ','Can not insert duplicate','warning');", true);
                    cbsalespoint_SelectedIndexChanged(sender, e);

                }
                else
                {
                    Session["fileName"] = null;
                    txtAssetCode.Text = assetno;
                    UploadFiles(Convert.ToString(txtAssetCode.Text));
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Record Inserted successfully ','Insert Success','success');", true);
                    cbsalespoint_SelectedIndexChanged(sender, e);
                    ClearControl();
                }
            }
            else if (btsave.Text == "Update")
            {
                arr.Add(new cArrayList("@assetno", Convert.ToString(txtAssetCode.Text)));
                if (Session["fileSize"] == null || Session["fileSize"] == "")
                {
                    arr.Add(new cArrayList("@fileimage", Convert.ToString(Session["fileName"])));
                }
                else
                {
                    arr.Add(new cArrayList("@fileimage", Convert.ToString(txtAssetCode.Text) + "_" + Convert.ToString(Session["fileName"])));
                }
                bll.vUpdateMastAsset(arr);
                //btsave.Enabled = true;
                //btprint.Enabled = true;
                UploadFiles(Convert.ToString(txtAssetCode.Text));
                Session["fileName"] = null;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Record Updated successfully ','Insert Success','success');", true);
                cbsalespoint_SelectedIndexChanged(sender, e);
                ClearControl();
            }
        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','" + ex.Message + " " + ex.InnerException + "','error');", true);
            //app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('File upload failed!','File upload failed');", true);
            ut.Logs("", "Account", "Asset", "fr_mst_asset", "btnUpload_Click", "Exception", ex.Message + ex.InnerException);
        }
    }

    private void UploadFiles(string extendendName)
    {
        if (Session["fileSize"] == null || Session["fileSize"] == "")
        { }
        else
        {
            if (File.Exists(bll.sGetControlParameter("image_path") + "\\account\\asset\\" + Convert.ToString(hdfOldFileName.Value)))
            {
                File.Delete(bll.sGetControlParameter("image_path") + "\\account\\asset\\" + Convert.ToString(hdfOldFileName.Value));
                hdfOldFileName.Value = string.Empty;
            }

            byte[] fs = (byte[])Session["fileSize"];
            File.WriteAllBytes(bll.sGetControlParameter("image_path") + "/account/asset/" + extendendName + "-" + lbfileloc.Text, fs);


            Session["fileSize"] = null;
            Session["fileName"] = null;
            //lbfileloc.Text = string.Empty;
        }
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lblAssetno = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblAssetno");
        DataTable dt = new DataTable();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@assetno", Convert.ToString(lblAssetno.Text)));
        dt = cdl.GetValueFromSP("Sp_tacc_mst_asset_getByID", arr);

        if (dt.Rows.Count > 0)
        {
            bindControl();
            //        @assetno,@asset_nm, @asset_arabic ,@salespointcd ,@asset_typ, @remarks ,@condition ,@purchased_dt,@acquired_dt 
            //,@serialno ,@purchase_place ,@insured ,@insured_company ,@manufacturer ,@model ,@fileimage ,@ecolifemonth ,@asset_sta_id ,getdate() ,@createdby
            bindControl();
            txtArabicName.Text = Convert.ToString(dt.Rows[0]["asset_arabic"]);
            txtAssetCode.Text = Convert.ToString(dt.Rows[0]["assetno"]);
           // txtAssetCondition.Text = Convert.ToString(dt.Rows[0]["condition"]);
            txtECOLifeMonth.Text = Convert.ToString(dt.Rows[0]["ecolifemonth"]);
            txtEnglishName.Text = Convert.ToString(dt.Rows[0]["asset_nm"]);
            txtInsured.Text = Convert.ToString(dt.Rows[0]["insured"]);
            ddlAssetGroup.SelectedValue = Convert.ToString(dt.Rows[0]["assetGroup"]);
            ddlAssetGroup_SelectedIndexChanged(sender, e);
            cbAsset_type.SelectedValue = Convert.ToString(dt.Rows[0]["asset_typ"]);
            cbAsset_type_SelectedIndexChanged(sender, e);
            ddlManufacture.SelectedValue = Convert.ToString(dt.Rows[0]["manufacturer"]);
            ddlManufacture_SelectedIndexChanged(sender, e);
            ddlModel.SelectedValue = Convert.ToString(dt.Rows[0]["model"]);
            txtInsuredCompany.Text = Convert.ToString(dt.Rows[0]["insured_company"]);
            txtPurchasePlace.Text = Convert.ToString(dt.Rows[0]["purchase_place"]);
            txtRemarks.Text = Convert.ToString(dt.Rows[0]["remarks"]);
            txtSerialno.Text = Convert.ToString(dt.Rows[0]["serialno"]);
            txtModelNumber.Text = Convert.ToString(dt.Rows[0]["modelNumber"]);
            cbsalespoint.SelectedValue = Convert.ToString(dt.Rows[0]["salespointcd"]);
            dtPurchase.Text = Convert.ToDateTime(dt.Rows[0]["purchased_dt"]).ToString("d/M/yyyy"); //Convert.ToString(dt.Rows[0]["purchased_dt"]); Convert.ToDateTime(rs["end_dt"]).ToString("d/M/yyyy");
            dtAcquired.Text = Convert.ToDateTime(dt.Rows[0]["acquired_dt"]).ToString("d/M/yyyy"); //Convert.ToString(dt.Rows[0]["acquired_dt"]);
            //arr.Add(new cArrayList("@purchased_dt", System.DateTime.ParseExact(dtPurchase.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            //arr.Add(new cArrayList("@acquired_dt", System.DateTime.ParseExact(dtAcquired.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            btsave.Text = "Update";
            //lkl.HRef = "/images/account/asset/" + Convert.ToString(dt.Rows[0]["fileimage"]);
            Session["fileName"] = Convert.ToString(dt.Rows[0]["fileimage"]);
            lbfileloc.Text = Convert.ToString(dt.Rows[0]["fileimage"]);
            hdfOldFileName.Value = Convert.ToString(dt.Rows[0]["fileimage"]);
            BindGrid();
        }
    }

    protected void btDelete_Click(object sender, EventArgs e)
    {
        if (txtAssetCode.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please select record first. ','select record','warning');", true);
        }
        else
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@assetno", Convert.ToString(txtAssetCode.Text)));
            bll.vDeleteMastAsset(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Record Deleted successfully ','Deleted Success','success');", true);
            ClearControl();
            cbsalespoint_SelectedIndexChanged(sender, e);
            //btsave.Enabled = false;
            //btprint.Enabled = false;
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('/fm_report2.aspx?src=mst_asset');", true);
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["fileName"] == null || Session["fileName"] == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Please upload files!','Check file');", true);
            }
            else
            {
                DirectoryInfo dir = new DirectoryInfo(bll.sGetControlParameter("image_path") + "/account/asset/");

                string fileName = Convert.ToString(Session["fileName"]);
                string fileLocation = Convert.ToString(Session["fileLocation"]);
                lbfileloc.Text = fileName;


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
            ut.Logs("", "Account", "Asset", "fr_mst_asset", "btnUpload_Click", "Exception", ex.Message + ex.InnerException);
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
    protected void cbAsset_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
        if (dt.Rows.Count > 0)
        {
            dt = new DataTable();
        }
        arr.Add(new cArrayList("@fld_nm", "acc_" + cbAsset_type.SelectedValue));
        dt = cdl.GetValueFromSP("sp_tfield_value_get", arr);
        ddlManufacture.DataSource = dt;
        ddlManufacture.DataValueField = "fld_valu";
        ddlManufacture.DataTextField = "fld_desc";
        ddlManufacture.DataBind();
        ddlManufacture_SelectedIndexChanged(sender, e);

        BindGrid();
    }
    protected void ddlAssetGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();

        arr.Add(new cArrayList("@fld_nm", ddlAssetGroup.SelectedValue));
        dt = cdl.GetValueFromSP("sp_tfield_valueWithfld_get", arr);
        cbAsset_type.DataSource = dt;
        cbAsset_type.DataValueField = "fld_valu";
        cbAsset_type.DataTextField = "fld_desc";
        cbAsset_type.DataBind();
        cbAsset_type_SelectedIndexChanged(sender, e);

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
}