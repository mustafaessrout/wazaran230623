using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
public partial class master_fm_mstcoa : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            vInitGrid();
          //  bll.vBindingComboToSp(ref cbcoacode, "sp_tacc_mst_coa_get", "coa_cd", "coa_desc");
            bll.vBindingComboToSp(ref cbparent, "sp_tacc_mst_coa_get", "coa_cd","coa_desc");
            bll.vBindingFieldValueToCombo(ref cbtype, "coa_typ");
            txcoacode.CssClass = cd.csstextro;
            txdesc.CssClass = cd.csstextro;
            txarabic.CssClass = cd.csstextro;
            cbtype.CssClass = cd.csstextro;
            cbparent.CssClass = cd.csstextro;
            btedit.CssClass = "btn btn-default ro";
            
            
        }
    }

    void vInitGrid()
    {
        tvwcoa.Nodes.Clear();
        TreeNode troot = new TreeNode();
        troot.Value = "0";// bll.sGetControlParameter("salespoint");
        troot.Text = "Chart Of Account"; //bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + troot.Value + "'");
        AddNode(ref troot, "0");
        tvwcoa.Nodes.Add(troot);
        tvwcoa.CollapseAll();
    }

    void  AddNode(ref TreeNode tn, string sParent)
    {
       
        SqlDataReader rs = null; List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@coa_cd_parent", sParent));
        bll.vGetAccCoaByParent(arr, ref rs);
    
        while (rs.Read())
        {
            TreeNode tchild = new TreeNode();
            tchild.Value = rs["coa_cd"].ToString();
            tchild.Text = rs["coa_desc"].ToString(); //rs["coa_cd"].ToString() + " : " + rs["coa_nm"].ToString() + "(" + rs["coa_arabic"].ToString() + ")";
            AddNode(ref tchild, rs["coa_cd"].ToString());
            tn.ChildNodes.Add(tchild);
        }
    }

    protected void tvwcoa_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
          //  cbcoacode.SelectedValue = tvwcoa.SelectedValue.ToString();
            txcoacode.Text = tvwcoa.SelectedNode.Value.ToString();
           
            txdesc.Text = bll.vLookUp("select coa_nm from tacc_mst_coa where coa_cd='"+tvwcoa.SelectedNode.Value.ToString()+"'");
            string s = bll.vLookUp("select coa_typ from tacc_mst_coa where coa_cd='"+tvwcoa.SelectedNode.Value.ToString()+"'");
            btedit.CssClass = "btn btn-warning";
            if (s != "")
            {
                cbtype.SelectedValue = s;
            }
            cbparent.SelectedValue = tvwcoa.SelectedNode.Parent.Value.ToString();
        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : twcoa_SelectedNodeChanged");
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@coa_cd", txcoacode.Text));
        arr.Add(new cArrayList("@coa_nm", txdesc.Text));
        arr.Add(new cArrayList("@coa_cd_parent", cbparent.SelectedValue.ToString()));
        arr.Add(new cArrayList("@coa_arabic",txarabic.Text));
        arr.Add(new cArrayList("@coa_typ", cbtype.SelectedValue.ToString()));
        bll.vInsertAccMstCOA(arr);
        vInitGrid();
        tvwcoa.ExpandAll();
        bll.vBindingComboToSp(ref cbparent, "sp_tacc_mst_coa_get", "coa_cd", "coa_desc");
        txdesc.CssClass = cd.csstextro;
        txarabic.CssClass = cd.csstextro;
        cbtype.CssClass = cd.csstextro;
        cbparent.CssClass = cd.csstextro;
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        txcoacode.CssClass = cd.csstext;
        txdesc.CssClass = cd.csstext;
        txarabic.CssClass = cd.csstext;
        cbparent.CssClass = cd.csstext;
        cbtype.CssClass = cd.csstext;
    }
    protected void btedit_Click(object sender, EventArgs e)
    {
        txdesc.CssClass = cd.csstext;
        txarabic.CssClass = cd.csstext;
        cbtype.CssClass = cd.csstext;
        cbparent.CssClass = cd.csstext;
    }
    protected void btdelete_Click(object sender, EventArgs e)
    {
        if (txcoacode.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Pls select COA Code to be deleted!','Coa Code','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@coa_cd", txcoacode.Text));
        bll.vDeleteAccMstCOA(arr);
        vInitGrid();
        tvwcoa.ExpandAll();
        txcoacode.Text = "";
        txdesc.Text = "";
        txarabic.Text = "";
        

    }
}