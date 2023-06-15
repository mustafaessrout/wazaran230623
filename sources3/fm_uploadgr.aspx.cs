using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class fm_uploadgr : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // bll.vBindingGridToSp(ref grd, "sp_tmst_goodreceipt_getbypending");
            dtupload.Text = Request.Cookies["waz_dt"].Value;
        }
    }

    protected void btupload_Click(object sender, EventArgs e)
    {
        LinkButton btnupload = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btnupload.NamingContainer;
        Label lbreceiptno = (Label)row.FindControl("lbreceiptno");
        FileUpload upl1 = (FileUpload)row.FindControl("upl1");
        FileUpload uplgdn = (FileUpload)row.FindControl("uplgdn");
        string sLoc = bll.sGetControlParameter("image_path");

        if (!upl1.HasFile)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('File receipt need upload','Upload file','info');", true);
            return;
        }
        string sExt = System.IO.Path.GetExtension(upl1.FileName);
    

        string sFile = lbreceiptno.Text + sExt;
      

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@receipt_no", lbreceiptno.Text));
        arr.Add(new cArrayList("@receiptfile", sFile));
         
        if (uplgdn.HasFile)
        {
            string sExtGdn = System.IO.Path.GetExtension(uplgdn.FileName);
            string sFileGDN = "GDN" + lbreceiptno.Text + sExtGdn;
            arr.Add(new cArrayList("@gdnfile", sFileGDN));
            uplgdn.SaveAs(sLoc +sFileGDN);
        }
        bll.vInsertgoodreceiptinfo(arr);
        
        upl1.SaveAs(sLoc + sFile);
        arr.Clear();
        arr.Add(new cArrayList("@status", cbstatus.SelectedValue));
        bll.vBindingGridToSp(ref grd, "sp_tmst_goodreceipt_getbypending",arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Upload good received no. "+lbreceiptno.Text+" done','Succeeded','info');", true);
    }

   
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btupload = (LinkButton)e.Row.FindControl("btupload");
            btupload.Attributes.Add("onclick", "ShowProgress();");
   
        }
    }

    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@status", cbstatus.SelectedValue));
        bll.vBindingGridToSp(ref grd, "sp_tmst_goodreceipt_getbypending", arr);
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@status", cbstatus.SelectedValue));
        bll.vBindingGridToSp(ref grd, "sp_tmst_goodreceipt_getbypending", arr);
    }
}