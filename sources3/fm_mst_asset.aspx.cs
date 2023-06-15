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

            Session["fileName"] = null;
            Session["fileSize"] = null;
            //btsave.Enabled = false;
            //btprint.Enabled = false;
        }
    }

    private void bindControl()
    {
        List<cArrayList> arr = new List<cArrayList>();

        //bll.vBindingFieldValueToComboWithALL(ref cbAsset_type, "acc_asset_type");

        DataTable dt = new DataTable();


        arr.Clear();
        if (dt.Rows.Count > 0)
        {
            dt = new DataTable();
        }
        arr.Add(new cArrayList("@fld_nm", "acc_asset_group"));
        arr.Sort();
        dt = cdl.GetValueFromSP("sp_tfield_value_get", arr);
        ddlAssetGroup.DataSource = dt;
        ddlAssetGroup.DataValueField = "fld_valu";
        ddlAssetGroup.DataTextField = "fld_desc";
        ddlAssetGroup.DataBind();
        //ddlAssetGroup.SelectedValue = "acc_Tangible_Asset";
        ddlAssetGroup.SelectedValue = "acc_Tangible_Asset";
        //if (dt.Rows.Count > 0)
        //{

        //    //ddlAssetGroup.Items.Remove(ddlAssetGroup.Items[1]);
        //    ////ddlAssetGroup.Items.Remove(ddlAssetGroup.Items[0]);
        //    for (int n = 1; n <= dt.Rows.Count-1; n++)
        //    {
        //        ddlAssetGroup.Items.Remove(ddlAssetGroup.Items[n]);
        //    }
        //}
        ddlAssetGroup_SelectedIndexChanged(null, null);
        if (ddlAssetGroup.SelectedValue != "")
        {
            var firstitem = cbAsset_type.Items[1];
        }
        //var firstitem = cbAsset_type.Items[0];

        //cbAsset_type.Items.Clear();
        //cbAsset_type.Items.Remove(firstitem);



        BindGrid();
    }

    private void BindGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@asset_typ", ""));
        bll.vBindingGridToSp(ref grd, "Sp_tacc_mst_asset_get", arr);
    }

    private void ClearControl()
    {
        //bindControl();
        //txtArabicName.Text = string.Empty;
        //txtEnglishName.Text = string.Empty;

        //txtRemarks.Text = string.Empty;

        txtEnglishName.Text= string.Empty;
        txtArabicName.Text= string.Empty;
        //cbAsset_type.SelectedValue= string.Empty;
        txtRemarks.Text= string.Empty;
        //ddlManufacture.SelectedValue= string.Empty;
        //ddlModel.SelectedValue= string.Empty;     
        //ddlAssetGroup.SelectedValue= string.Empty;
        lbfileloc.Text = string.Empty;



        btsave.Text = "Save";
        Session["fileName"] = null;
        Session["fileSize"] = null;
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
            //arr.Add(new cArrayList("@assetno", Convert.ToString(txtAssetCode.Text)));
            arr.Add(new cArrayList("@asset_nm", Convert.ToString(txtEnglishName.Text)));
            arr.Add(new cArrayList("@asset_arabic", Convert.ToString(txtArabicName.Text)));
            arr.Add(new cArrayList("@asset_typ", Convert.ToString(cbAsset_type.SelectedValue)));
            arr.Add(new cArrayList("@remarks", Convert.ToString(txtRemarks.Text)));
            arr.Add(new cArrayList("@manufacturer", Convert.ToString(ddlManufacture.SelectedValue)));
            arr.Add(new cArrayList("@model", Convert.ToString(ddlModel.SelectedValue)));
            arr.Add(new cArrayList("@assetGroup", Convert.ToString(ddlAssetGroup.SelectedValue)));
            arr.Add(new cArrayList("@fileimage", Convert.ToString(lbfileloc.Text)));
            arr.Add(new cArrayList("@asset_sta_id", "A"));
            arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));


            
            //arr.Add(new cArrayList("@assetno", Convert.ToString(txtAssetCode.Text)));

            if (btsave.Text == "Save")
            {
                string assetno = string.Empty;

                //arr.Add(new cArrayList("@fileimage", Convert.ToString(Session["fileName"])));
                //arr.Add(new cArrayList("@fileimage2", Convert.ToString(lbfileloc.Text)));
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
                arr.Add(new cArrayList("@fileimage2", Convert.ToString(lbfileloc.Text)));
                
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
            ut.Logs("", "Admin", "Equipment Maintenance", "fr_HOEquipmentMaintenance", "btnUpload_Click", "Exception", ex.Message + ex.InnerException);
        }
    }

    private void UploadFiles(string extendendName)
    {
        if (Session["fileSize"] == null || Session["fileSize"] == "")
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
                imagePath = "D:\\svn2\\sources3\\images\\";
            }

            //if (File.Exists(bll.sGetControlParameter("image_path") + "\\account\\asset\\" + Convert.ToString(hdfOldFileName.Value) ))
            //{
            //    File.Delete(bll.sGetControlParameter("image_path") + "\\account\\asset\\" + Convert.ToString(hdfOldFileName.Value));
            //    hdfOldFileName.Value = string.Empty;
            //}

            if (File.Exists(imagePath + "\\account\\assetmaster\\" + Convert.ToString(hdfOldFileName.Value)))
            {
                File.Delete(imagePath + "\\account\\assetmaster\\" + Convert.ToString(hdfOldFileName.Value));
                hdfOldFileName.Value = string.Empty;
            }

            byte[] fs = (byte[])Session["fileSize"];
            File.WriteAllBytes(imagePath + "\\account\\assetmaster\\" + extendendName + "___" + lbfileloc.Text.Substring(4), fs);


            Session["fileSize"] = null;
            Session["fileName"] = null;
        }
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        cbAsset_type.Enabled = false;

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
            txtAssetCode.Text = Convert.ToString(dt.Rows[0]["assetno"]);
            txtArabicName.Text = Convert.ToString(dt.Rows[0]["asset_arabic"]);
            txtEnglishName.Text = Convert.ToString(dt.Rows[0]["asset_nm"]);
            ddlAssetGroup.SelectedValue = Convert.ToString(dt.Rows[0]["assetGroup"]);
            ddlAssetGroup_SelectedIndexChanged(sender, e);
            cbAsset_type.SelectedValue = Convert.ToString(dt.Rows[0]["asset_typ"]);
            cbAsset_type_SelectedIndexChanged(sender, e);
            ddlManufacture.SelectedValue = Convert.ToString(dt.Rows[0]["manufacturer"]);
            ddlManufacture_SelectedIndexChanged(sender, e);
            ddlModel.SelectedValue = Convert.ToString(dt.Rows[0]["model"]);
            lbfileloc.Text = Convert.ToString(dt.Rows[0]["fileimage"]);
            txtRemarks.Text = Convert.ToString(dt.Rows[0]["remarks"]);
            //arr.Add(new cArrayList("@purchased_dt", System.DateTime.ParseExact(dtPurchase.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            //arr.Add(new cArrayList("@acquired_dt", System.DateTime.ParseExact(dtAcquired.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            btsave.Text = "Update";
            //lkl.HRef = "/images/account/asset/" + Convert.ToString(dt.Rows[0]["fileimage"]);
            Session["fileName"] = Convert.ToString(dt.Rows[0]["fileimage"]);
            BindGrid();
        }
    }

    protected void btDelete_Click(object sender, EventArgs e)
    {
        if (txtEnglishName.Text == "")
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
                string imagePath = null;
                if (Directory.Exists(bll.sGetControlParameter("image_path")))
                {
                    imagePath = bll.sGetControlParameter("image_path");
                }
                else
                {
                    imagePath = "D:\\svn2\\sources3\\images\\";
                }

                //DirectoryInfo dir = new DirectoryInfo(bll.sGetControlParameter("image_path") + "/account/asset/");
                DirectoryInfo dir = new DirectoryInfo(imagePath);

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
    protected void cbAsset_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
        if (dt.Rows.Count > 0)
        {
            dt = new DataTable();
        }        
        //arr.Add(new cArrayList("@fld_nm", "acc_" + cbAsset_type.SelectedValue));
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
        arr.Add(new cArrayList("@fld_nm", ddlManufacture.SelectedValue));//actually fld_desc not fld_nm
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