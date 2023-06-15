using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_destryoapp : System.Web.UI.Page
{
    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindingGrd();
            BindingGrdAppList();
            pnls1.Visible = false;
            pnls2.Visible = false;
            pnls3.Visible = false;
        }
    }

    protected void btapp_Click(object sender, EventArgs e)
    {
        if (!upl.HasFiles)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dasl", "sweetAlert('File is Empty','You must Upload Image of Approval','error');", true);
            return;
        }

       
        string doccode= "DST5";
        string imgpatch = bll.sGetControlParameter("image_path") + Request.Cookies["sp"].Value.ToString() + trsNo.Text + doccode + System.IO.Path.GetExtension(upl.FileName);
        string imgname = Request.Cookies["sp"].Value.ToString() + trsNo.Text + doccode + System.IO.Path.GetExtension(upl.FileName);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@trnstkNo", trsNo.Text));
        arr.Add(new cArrayList("@salespointCD", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@doc_cd", doccode));
        arr.Add(new cArrayList("@filename", imgname));
        bll.vUpdatetblTrnStock_document(arr);
        upl.SaveAs(imgpatch);

        arr.Clear();
        arr.Add(new cArrayList("@trnstkno", trsNo.Text));
        arr.Add(new cArrayList("@sta_id", "A"));
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "asl", "sweetAlert('Approve Success','You Approve " + trsNo.Text + "','success');", true);
        bll.vAppDestroy(arr);
        BindingGrd();
        BindingGrdAppList();
        pnls1.Visible = false;
        pnls2.Visible = false;
        grdDtl.Visible = false;
        pnls3.Visible = false;
    }

    protected void grddoc_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;

        pnls1.Visible = true;
        pnls2.Visible = true;
        pnls3.Visible = true;

        grdDtl.Visible = true;
        Label trnstkno = (Label)grddoc.Rows[e.NewSelectedIndex].FindControl("trnstkno");
        Label prSpv = (Label)grddoc.Rows[e.NewSelectedIndex].FindControl("emp_nm");
        trsNo.Text = trnstkno.Text;
        prodSpv.Text = prSpv.Text;
        BindingGrdDtl(trnstkno.Text);

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@doc_cd", "DST1"));
        arr.Add(new cArrayList("@trnstkno", trsNo.Text));
        bll.vGetTranstckApp(arr, ref rs);
        while (rs.Read())
        {
            dst1.Text = rs["filename"].ToString();
            dst1.NavigateUrl = "/images/"+ rs["filename"].ToString();
        }
        rs.Close();

        arr.Clear();
        arr.Add(new cArrayList("@doc_cd", "DST2"));
        arr.Add(new cArrayList("@trnstkno", trsNo.Text));
        bll.vGetTranstckApp(arr, ref rs);
        while (rs.Read())
        {
            dst2.Text = rs["filename"].ToString();
            dst2.NavigateUrl = "/images/" + rs["filename"].ToString();
        }
        rs.Close();
    }


    void BindingGrd()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@invtype", "12"));
        bll.vBindingGridToSp(ref grddoc, "sp_get_trnstock_will_app", arr);
    }
    void BindingGrdAppList()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@doc_cd", "DST5"));
        bll.vBindingGridToSp(ref grdls, "sp_get_dstry_app", arr);
    }
    void BindingGrdDtl(string trnsno)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@trnstkno", trnsno));
        bll.vBindingGridToSp(ref grdDtl, "sp_get_trnstockDet_will_app", arr);
    }
}