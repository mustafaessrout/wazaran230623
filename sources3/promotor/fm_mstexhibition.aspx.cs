using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class fm_mstexhibition : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@qry_cd", "promotor"));
            bll.vBindingComboToSp(ref cbemployee, "sp_thrd_mstemployee_getbyqry", "emp_cd", "fullname", arr);
            bll.vBindingComboToSp(ref cbpicbooth, "sp_tmst_outsource_get", "idno", "fullname");
            arr.Clear();
            arr.Add(new cArrayList("@level_no", 2));
            bll.vBindingComboToSp(ref cbproduct, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
            txexhibitcode.CssClass = "form-control ro";
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDeleteWrkExhibitionBooth(arr);
        }
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        if (txlocation.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Location can not empty!','Check Location');", true);
            return;
        }
        if (txname.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Exhibition Name can not empty!','Check Exhibition Name');", true);
            return;
        }
        string sSysNo = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@exhibit_nm", txname.Text));
        arr.Add(new cArrayList("@start_dt", System.DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@end_dt", System.DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@deleted", 0));
        arr.Add(new cArrayList("@emp_cd", cbemployee.SelectedValue.ToString()));
        arr.Add(new cArrayList("@loc_nm", txlocation.Text));
        arr.Add(new cArrayList("@exh_sta_id", "A"));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vInsertMstExhibition(arr, ref sSysNo);
        txexhibitcode.Text = sSysNo;
     
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Data Saved Successfully!','Exhibition Code : "+sSysNo+". Please logoff to activated!','success');", true);
        Response.Redirect("fm_login.aspx");
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_mstexhibition.aspx");
    }

    protected void btrefresh_Click(object sender, EventArgs e)
    {
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@exhibit_cd", hdexhibition.Value.ToString()));
        bll.vGetMstExhibition(ref rs, arr);
        while (rs.Read())
        {
            txexhibitcode.Text = hdexhibition.Value.ToString();
            txname.Text = rs["exhibit_nm"].ToString();
            txlocation.Text = rs["loc_nm"].ToString();
            cbemployee.SelectedValue = rs["emp_cd"].ToString();
            dtstart.Text = Convert.ToDateTime(rs["start_dt"]).ToString("d/M/yyyy");
            dtend.Text = Convert.ToDateTime(rs["end_dt"]).ToString("d/M/yyyy");
            txexhibitcode.CssClass = "form-control ro";
            txname.CssClass = "form-control ro";
            dtstart.CssClass = "form-control ro";
            dtend.CssClass = "form-control ro";
            cbemployee.CssClass = "form-control ro";
            cbproduct.CssClass = "form-control ro";
            cbemployee.CssClass = "form-control ro";
            cbpicbooth.CssClass = "form-control ro";
            txlocation.CssClass = "form-control ro";
            btadd.CssClass = "btn btn-primary ro";
            btsave.CssClass = "btn btn-info ro";
            grd.CssClass = "mGrid ro";
        }
        rs.Close();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vInsertWrkExhibitionBoothFromCore(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_exhibitionbooth_get", arr);
        
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('../fm_report2.aspx?src=exhibition');", true);
    }

    protected void btadd_Click(object sender, EventArgs e)
    {
        string sCheck = bll.vLookUp("select dbo.fn_checkpromoterexist('"+cbpicbooth.SelectedValue.ToString()+"')");
        if (sCheck != "0")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Promoter already active in other exhibition!','Try with another promoter');", true); return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@emp_cd", cbpicbooth.SelectedValue.ToString()));
        arr.Add(new cArrayList("@prod_cd", cbproduct.SelectedValue.ToString()));
        bll.vInsertWrkExhibitioBooth(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_exhibitionbooth_get", arr);
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {

    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        HiddenField hdprodcode = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdprodcode");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@prod_cd", hdprodcode.Value.ToString()));
        bll.vDeleteWrkExhibitioBooth(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_exhibitionbooth_get", arr);
    }
}