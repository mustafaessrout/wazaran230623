using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Data;

public partial class fm_carEntry : System.Web.UI.Page
{
    cbll bll = new cbll();
    Utitlity ut = new Utitlity();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            bll.vBindingComboToSp(ref ddlCarBrand, "sp_tmst_carbrand_get", "carbrand_cd", "carbrand_nm");
            ddlCarBrand.Items.Insert(0, new ListItem("Please Select", "0"));
            header.Visible = false;
            divider.Visible = false;
            grd.Visible = false;
            lblCarbrandPhoto.Visible = false;
            btUpdateBrand.Visible = false;
            btUpdateModel.Visible = false;
        }
    }


    protected void btShowData_Click(object sender, EventArgs e)
    {
        bll.vBindingGridToSp(ref grd, "sp_carBrandModel_get");
        if (!header.Visible)
        {
            header.Visible = true;
            divider.Visible = true;
        }
        grd.Visible = true;
        grd.DataBind();
        grdv.Update();
        if (!grd.PageIndex.Equals(0))
        {
            grd.PageIndex = 0;
        }

    }

    protected void btrefresh_Click(object sender, EventArgs e)
    {
        bll.vBindingGridToSp(ref grd, "sp_carBrandModel_get");
        if (!header.Visible)
        {
            header.Visible = true;
            divider.Visible = true;
        }
        grd.Visible = true;
        grd.DataBind();
        grdv.Update();
   

    }
    protected void btSaveBrand_Click(object sender, EventArgs e)
    {
        string carbrand_photo = string.Empty;
        if (txtcarBrand.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please Enter Car Brand!','Car Brand is not Entered','warning');", true);
            return;
        }
        if (txtcarBrandAr.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please Enter Car Brand Arabic!','Car Brand Arabic is not Entered','warning');", true);
            return;
        }
        carbrand_photo = null;
        if (upCarBrand.HasFile)
        {
            FileInfo fi = new FileInfo(upCarBrand.FileName);
            string ext = fi.Extension;
            byte[] fs = upCarBrand.FileBytes;
            if ((upCarBrand.FileName != "") || (upCarBrand.FileName != null))
            {
                carbrand_photo = txtcarBrand.Text + ext;
                upCarBrand.SaveAs(bll.sGetControlParameter("image_path") + "/Cars/" + carbrand_photo);
                //bll.vLookUp("update tmst_carbrand set carbrand_pics='" + carbrand_photo + "' where carbrand_nm='" + txcarBrand.Text + "'");
            }
        }
        string x = string.Empty;
        string carbrand_cd = string.Empty;
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@car_brand", Convert.ToString(txtcarBrand.Text)));
        arr.Add(new cArrayList("@car_brandAr", Convert.ToString(txtcarBrandAr.Text)));
        arr.Add(new cArrayList("@carbrand_pics", carbrand_photo));
        bll.vInsertCarBrand(arr, ref rs);
        while (rs.Read())
        {
            x = rs["x"].ToString();
            if (x == "Ok")
            {
                carbrand_cd = rs["carbrand_cd"].ToString();

            }

        }
        if (x == "Duplicate")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('The Entered Car Brand is Already Available!','Please Enter New Car Brand','warning');", true);
            return;
        }
        else
        {

            //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "sweetAlert('The Entered Car Brand Has Been Saved Successfully! ', '" + carbrand_cd + "','success');setTimeout(function(){window.location='"+ Request.ApplicationPath +"fm_carEntry.aspx';},5000)", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "sweetAlert('The Entered Car Brand Has Been Saved Successfully! ', '" + txtcarBrand.Text + "','success') ;  onclick=(function(){window.location='/admin/fm_carEntry.aspx';})", true);
            //Response.Redirect("fm_carEntry.aspx");
        }
    }
    protected void SaveModel_Click(object sender, EventArgs e)
    {
        string carModel_photo = string.Empty;
        if (ddlCarBrand.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please Select Car Brand!','Car Brand is not Selected','warning');", true);
            return;
        }
        if (txtCarModel.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please Enter Car Model!','Car Model is not Entered','warning');", true);
            return;
        }
        if (txtCarModelAr.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please Enter Car Model Arabic!','Car Model Arabic is not Entered','warning');", true);
            return;
        }
        carModel_photo = null;
        if (upCarModel.HasFile)
        {
            FileInfo fi = new FileInfo(upCarModel.FileName);
            string ext = fi.Extension;
            byte[] fs = upCarModel.FileBytes;
            if ((upCarModel.FileName != "") || (upCarModel.FileName != null))
            {
                carModel_photo = txtCarModel.Text + ext;
                upCarModel.SaveAs(bll.sGetControlParameter("image_path") + "/Cars/" + carModel_photo);
                //bll.vLookUp("update tmst_carbrand set carbrand_pics='" + carbrand_photo + "' where carbrand_nm='" + txcarBrand.Text + "'");
            }
        }
        string x = string.Empty;
        string carmodel_cd = string.Empty;
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@car_brand", (ddlCarBrand.SelectedValue.ToString())));
        arr.Add(new cArrayList("@car_model", Convert.ToString(txtCarModel.Text)));
        arr.Add(new cArrayList("@car_modelAr", Convert.ToString(txtCarModelAr.Text)));
        arr.Add(new cArrayList("@carmodel_pics", carModel_photo));
        bll.vInsertCarModel(arr, ref rs);
        while (rs.Read())
        {
            x = rs["x"].ToString();
            if (x == "Ok")
            {
                carmodel_cd = rs["carmodel_cd"].ToString();

            }

        }
        if (x == "Duplicate")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('The Entered Car Model is Already Available!','Please Enter New Car Model','warning');", true);
            return;
        }
        else
        {

            //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "sweetAlert('The Entered Car Brand Has Been Saved Successfully! ', '" + carbrand_cd + "','success');setTimeout(function(){window.location='"+ Request.ApplicationPath +"fm_carEntry.aspx';},5000)", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "sweetAlert('The Entered Car Model Has Been Saved Successfully! ', '" + txtCarModel.Text + "','success') ;  onclick=(function(){window.location='/admin/fm_carEntry.aspx';})", true);
            //Response.Redirect("fm_carEntry.aspx");
        }

    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        btrefresh_Click(sender, e);
    }



    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
      
            grd.EditIndex = e.NewEditIndex;
            grd.DataBind();
            bll.vBindingGridToSp(ref grd, "sp_carBrandModel_get");
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        grd.DataBind();
        bll.vBindingGridToSp(ref grd, "sp_carBrandModel_get");
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        int index = grd.EditIndex;
        GridViewRow row = grd.Rows[index];
        TextBox t1 = row.FindControl("CarBrand") as TextBox;
        TextBox t2 = row.FindControl("CarBrandAr") as TextBox;

      
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkBtnDeleteBrand = e.Row.FindControl("lnkBtnDeleteBranch") as LinkButton;
            LinkButton lnkBtnDeleteModel = e.Row.FindControl("lnkBtnDeleteModel") as LinkButton;
            // Use whatever control you want to show in the confirmation message
            Label CarBrand = e.Row.FindControl("CarBrand") as Label;
            Label CarModel = e.Row.FindControl("CarModel") as Label;
            if (CarModel.Text != "N/A")
            {
                lnkBtnDeleteBrand.Text = "--";
                lnkBtnDeleteBrand.Enabled = false;
                lnkBtnDeleteBrand.ForeColor = System.Drawing.Color.Red;
                lnkBtnDeleteBrand.Style.Add("text-decoration", "none");
            }
            else
            {
                lnkBtnDeleteBrand.Attributes.Add("onclick", string.Format("return confirm('Do you want to delete the selected Brand   {0}   With its Models if available?');", CarBrand.Text));
            }
            if (CarModel.Text == "N/A")
            {
                lnkBtnDeleteModel.Text = "--";
                lnkBtnDeleteModel.Enabled = false;
                lnkBtnDeleteModel.ForeColor = System.Drawing.Color.Red;
                lnkBtnDeleteModel.Style.Add("text-decoration", "none");
            }
            else
            {
                lnkBtnDeleteModel.Attributes.Add("onclick", string.Format("return confirm('Do you want to delete the selected Model   {0} ?');", CarModel.Text)); 
            }
            
        } 
    }

    protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "DeleteBrand")
            {
                List<cArrayList> arr = new List<cArrayList>();
                string carbrand_cd = (e.CommandArgument).ToString();
                if (carbrand_cd == "" || carbrand_cd == null) 
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('The are no Car Brand to be Deleted!','Error!','warning');", true);
                    btrefresh_Click(sender, e);
                    return;
                }
                arr.Add(new cArrayList("@carbrand_cd", carbrand_cd));
                bll.vDeleteCarModelorBrand(arr);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('The Car Brand Has Been Deleted Successfully!,'" + carbrand_cd + "','success');", true);
                btrefresh_Click(sender, e);
                bll.vBindingComboToSp(ref ddlCarBrand, "sp_tmst_carbrand_get", "carbrand_cd", "carbrand_nm");
                ddlCarBrand.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            if (e.CommandName == "DeleteModel")
            {
                List<cArrayList> arr = new List<cArrayList>();
                string carmodel_cd = (e.CommandArgument).ToString();
                if (carmodel_cd == "" || carmodel_cd == null) 
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('The are no Car Model to be Deleted!','Please Delete Car Brand','warning');", true);
                    btrefresh_Click(sender, e);
                    return;
                }
                arr.Add(new cArrayList("@carmodel_cd",carmodel_cd));
                
                bll.vDeleteCarModelorBrand(arr);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('The Car Model Has Been Deleted Successfully!,'" + carmodel_cd + "','success');", true);
                btrefresh_Click(sender, e);
            }

        }
        catch (Exception ex)
        {
            ut.Logs("", "HR", "Car Delete", "fm_carEntry", "grd_RowCommand", "Exception", ex.Message + ex.InnerException);
        }
    }

    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        hddcarbrand_cd = (HiddenField)grd.Rows[e.NewSelectedIndex].FindControl("hdcarbrand_cd");
        Hddcarmodel_cd = (HiddenField)grd.Rows[e.NewSelectedIndex].FindControl("hdcarmodel_cd");
        if (Hddcarmodel_cd.Value == "")
        {
            Label CarBrandUp = (Label)grd.Rows[e.NewSelectedIndex].FindControl("CarBrand");
            Label CarBrandArUp = (Label)grd.Rows[e.NewSelectedIndex].FindControl("CarBrandAr");
            HiddenField UploadCarBrandPhoto = (HiddenField)grd.Rows[e.NewSelectedIndex].FindControl("hdcarbrand_pics");

            txtcarBrand.Text = CarBrandUp.Text;
            txtcarBrandAr.Text = CarBrandArUp.Text;
            lblCarbrandPhoto.Visible = true;
            if (UploadCarBrandPhoto.Value=="")
            {
                UploadCarBrandPhoto.Value = "N/A";
            }
            lblCarbrandPhoto.InnerText = UploadCarBrandPhoto.Value;
            btUpdateBrand.Visible = true;
            btSaveBrand.Visible = false;
            txtcarBrand.Focus();
            ddlCarBrand.SelectedValue = "0";
            txtCarModel.Text = "";
            txtCarModelAr.Text = "";
            lblCarModelPhoto.Visible = false;
            btSaveModel.Visible = true;
            btUpdateModel.Visible = false;
            ddlCarBrand.Enabled = true;
        }
        else if (Hddcarmodel_cd.Value !="")
        {
            Label CarModelUp = (Label)grd.Rows[e.NewSelectedIndex].FindControl("CarModel");
            Label CarModelArUp = (Label)grd.Rows[e.NewSelectedIndex].FindControl("CarModelAr");
            HiddenField UploadCarModelPhoto = (HiddenField)grd.Rows[e.NewSelectedIndex].FindControl("hdcarmodel_pics");

            ddlCarBrand.SelectedValue = hddcarbrand_cd.Value;
            txtCarModel.Text = CarModelUp.Text;
            txtCarModelAr.Text = CarModelArUp.Text;
            lblCarModelPhoto.Visible = true;
            if (UploadCarModelPhoto.Value == "")
            {
                UploadCarModelPhoto.Value = "N/A";
            }
                lblCarModelPhoto.InnerText = UploadCarModelPhoto.Value;
                btUpdateModel.Visible = true;
                btSaveModel.Visible = false;
                txtCarModel.Focus();
                ddlCarBrand.Enabled = false;
                txtcarBrand.Text = "";
                txtcarBrandAr.Text = "";
                lblCarbrandPhoto.Visible = false;
                btUpdateBrand.Visible = false;
                btSaveBrand.Visible = true;     
        }
        
        
    }
    protected void btUpdateBrand_Click(object sender, EventArgs  e)
    {
        string carbrand_photo = string.Empty;
        if (txtcarBrand.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please Enter Car Brand!','Car Brand is not Entered','warning');", true);
            return;
        }
        if (txtcarBrandAr.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please Enter Car Brand Arabic!','Car Brand Arabic is not Entered','warning');", true);
            return;
        }
        carbrand_photo = null;
        if (upCarBrand.HasFile)
        {
            FileInfo fi = new FileInfo(upCarBrand.FileName);
            string ext = fi.Extension;
            byte[] fs = upCarBrand.FileBytes;
            if ((upCarBrand.FileName != "") || (upCarBrand.FileName != null))
            {
                carbrand_photo = txtcarBrand.Text + ext;
                upCarBrand.SaveAs(bll.sGetControlParameter("image_path") + "/Cars/" + carbrand_photo);
                //bll.vLookUp("update tmst_carbrand set carbrand_pics='" + carbrand_photo + "' where carbrand_nm='" + txcarBrand.Text + "'");
            }
        }
        int columnIndex = grd.SelectedIndex;
        hddcarbrand_cd = (HiddenField)grd.Rows[columnIndex].FindControl("hdcarbrand_cd");
        Hddcarbrand_pics = (HiddenField)grd.Rows[columnIndex].FindControl("hdcarbrand_pics");
        if (carbrand_photo == null)
        {
            carbrand_photo = Hddcarbrand_pics.Value;
        }
        string x = string.Empty;
        string carbrand_cd = string.Empty;
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@carbrand_cd", Convert.ToString(hddcarbrand_cd.Value)));
        arr.Add(new cArrayList("@car_brand", Convert.ToString(txtcarBrand.Text)));
        arr.Add(new cArrayList("@car_brandAr", Convert.ToString(txtcarBrandAr.Text)));
        arr.Add(new cArrayList("@carbrand_pics", carbrand_photo));
        bll.vUpdateCarBrand(arr, ref rs);
        while (rs.Read())
        {
            x = rs["x"].ToString();
        }
        if (x == "Duplicate")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('The Entered Car Brand is Already Available!','Please Enter Correct Car Brand','warning');", true);
            return;
        }
        else
        {

            //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "sweetAlert('The Entered Car Brand Has Been Saved Successfully! ', '" + carbrand_cd + "','success');setTimeout(function(){window.location='"+ Request.ApplicationPath +"fm_carEntry.aspx';},5000)", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "sweetAlert('The Car Brand Has Been Updated Successfully! ', '" + txtcarBrand.Text + "','success') ;  onclick=(function(){window.location='/admin/fm_carEntry.aspx';})", true);
            //Response.Redirect("fm_carEntry.aspx");
        }
    }
    protected void btUpdateModel_Click(object sender, EventArgs e)
    {
        string carModel_photo = string.Empty;
        if (ddlCarBrand.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please Select Car Brand!','Car Brand is not Selected','warning');", true);
            return;
        }
        if (txtCarModel.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please Enter Car Model!','Car Model is not Entered','warning');", true);
            return;
        }
        if (txtCarModelAr.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please Enter Car Model Arabic!','Car Model Arabic is not Entered','warning');", true);
            return;
        }
        carModel_photo = null;
        if (upCarModel.HasFile)
        {
            FileInfo fi = new FileInfo(upCarModel.FileName);
            string ext = fi.Extension;
            byte[] fs = upCarModel.FileBytes;
            if ((upCarModel.FileName != "") || (upCarModel.FileName != null))
            {
                carModel_photo = txtCarModel.Text + ext;
                upCarModel.SaveAs(bll.sGetControlParameter("image_path") + "/Cars/" + carModel_photo);
                //bll.vLookUp("update tmst_carbrand set carbrand_pics='" + carbrand_photo + "' where carbrand_nm='" + txcarBrand.Text + "'");
            }
        }
        int columnIndex = grd.SelectedIndex;
        Hddcarmodel_cd = (HiddenField)grd.Rows[columnIndex].FindControl("hdcarmodel_cd");
        Hddcarmodel_pics = (HiddenField)grd.Rows[columnIndex].FindControl("hdcarmodel_pics");
        if (carModel_photo == null)
        {
            carModel_photo = Hddcarmodel_pics.Value;
        }
        string x = string.Empty;
        string carmodel_cd = string.Empty;
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@carmodel_cd", Convert.ToString(Hddcarmodel_cd.Value)));
        arr.Add(new cArrayList("@car_model", Convert.ToString(txtCarModel.Text)));
        arr.Add(new cArrayList("@car_modelAr", Convert.ToString(txtCarModelAr.Text)));
        arr.Add(new cArrayList("@carmodel_pics", carModel_photo));
        bll.vUpdateCarModel(arr, ref rs);
        while (rs.Read())
        {
            x = rs["x"].ToString();
        }
        if (x == "Duplicate")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('The Entered Car Model is Already Available!','Please Enter New Car Model','warning');", true);
            return;
        }
        else
        {

            //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "sweetAlert('The Entered Car Brand Has Been Saved Successfully! ', '" + carbrand_cd + "','success');setTimeout(function(){window.location='"+ Request.ApplicationPath +"fm_carEntry.aspx';},5000)", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "sweetAlert('The Car Model Has Been Updated Successfully! ', '" + txtCarModel.Text + "','success') ;  onclick=(function(){window.location='/admin/fm_carEntry.aspx';})", true);
            //Response.Redirect("fm_carEntry.aspx");
        }
    }
    protected void btNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_carEntry.aspx");
    }
}