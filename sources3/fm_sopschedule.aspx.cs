using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_sopschedule : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbschtype, "schedule_typ");
            bll.vBindingComboToSp(ref cbjob, "sp_tmst_job_get", "job_cd", "job_nm");
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get","salespointcd","salespoint_nm");
            arr.Add(new cArrayList("@level_no", 3));
            bll.vBindingComboToSp(ref cbproduct, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
        }
    }
    protected void rdwhs_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (rdwhs.SelectedValue.ToString() == "D")
        {
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            bll.vBindingComboToSp(ref cbwhs, "sp_tmst_warehouse_get", "whs_cd","whs_nm",arr);
            cbwhs_SelectedIndexChanged(sender, e);
        }
    }
    protected void cbwhs_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbbin, "sp_twarehouse_bin_get", "bin_cd", "bin_nm", arr);
    }
    protected void cbschtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        trv.Nodes.Clear();
        if (cbschtype.SelectedValue.ToString() == "W")
        {
            arr.Add(new cArrayList("@fld_nm", "dayofweek"));
            bll.vGetFieldValue(arr, ref rs);
            while (rs.Read())
            {
                TreeNode tn = new TreeNode();
                tn.Text = rs["fld_desc"].ToString();
                tn.Value = rs["fld_valu"].ToString();
                trv.Nodes.Add(tn);
            }
            rs.Close();
        }
        if (cbschtype.SelectedValue.ToString() == "Y")
        {
            arr.Add(new cArrayList("@fld_nm", "monthofyear"));
            bll.vGetFieldValue(arr, ref rs);
            while (rs.Read())
            {
                TreeNode tn = new TreeNode();
                tn.Text = rs["fld_desc"].ToString();
                tn.Value = rs["fld_valu"].ToString();
                trv.Nodes.Add(tn);
            }
            rs.Close();
        }
    }
    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        rdwhs_SelectedIndexChanged(sender, e);
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        if (trv.SelectedNode == null)
        { return; }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@job_cd", cbjob.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@dayofweek", trv.SelectedNode.Value.ToString()));
        arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
        arr.Add(new cArrayList("@bin_cd", cbbin.SelectedValue.ToString()));
        arr.Add(new cArrayList("@prod_cd", cbproduct.SelectedValue.ToString()));
        bll.vInsertScheduleWeekly(arr);
        arr.Clear();
        arr.Add(new cArrayList("@job_cd", cbjob.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@dayofweek", trv.SelectedNode.Value.ToString()));
        arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
        arr.Add(new cArrayList("@bin_cd", cbbin.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tschedule_weekly_get", arr);

    }
    protected void trv_SelectedNodeChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@job_cd", cbjob.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@dayofweek", trv.SelectedNode.Value.ToString()));
        arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
        arr.Add(new cArrayList("@bin_cd", cbbin.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tschedule_weekly_get", arr);
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbprodcode = (Label)grd.Rows[e.RowIndex].FindControl("lbprodcode");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@job_cd", cbjob.SelectedValue.ToString()));
        arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
        arr.Add(new cArrayList("@bin_cd", cbbin.SelectedValue.ToString()));
        arr.Add(new cArrayList("@prod_cd", lbprodcode.Text));
        arr.Add(new cArrayList("@dayofweek", trv.SelectedNode.Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        bll.vDelScheduleWeekly(arr);
        arr.Clear();
        arr.Add(new cArrayList("@job_cd", cbjob.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@dayofweek", trv.SelectedNode.Value.ToString()));
        arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
        arr.Add(new cArrayList("@bin_cd", cbbin.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tschedule_weekly_get", arr);


    }
}