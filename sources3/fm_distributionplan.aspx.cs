using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_distributionplan : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dtplan.Text = DateTime.Now.ToString("d/M/yyyy");
            dtdelivery.Text = DateTime.Now.ToString("d/M/yyyy");
            List<cArrayList> arr = new List<cArrayList>();
            bll.vBindingSalespointToCombo(ref cbsalespoint);
            arr.Clear();
            bll.vBindingGridToSp(ref grdpo, "sp_summary_po_get", arr);
            btsave.Visible = false;
            //if (grdori.Rows.Count > 0)
            //{
                //bll.vBindingGridToSpPivot(ref grdpivot, "sp_summary_po_get", arr);
            //}
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetListItem(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        List<string> lItem = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sItem = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@item_nm", prefixText));
        bll.vSearchMstItem2(arr, ref rs);
        while (rs.Read())
        {
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "-" + rs["item_nm"].ToString() + "-" + rs["size"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);

        }
        rs.Close();
        return (lItem.ToArray());
    }

    private DataTable PivotTable(DataTable origTable)
    {

        DataTable newTable = new DataTable();
        DataRow dr = null;
        //Add Columns to new Table
        for (int i = 0; i <= origTable.Rows.Count; i++)
        {
            newTable.Columns.Add(new DataColumn(origTable.Columns[i].ColumnName, typeof(String)));
        }

        //Execute the Pivot Method
        for (int cols = 0; cols < origTable.Columns.Count; cols++)
        {
            dr = newTable.NewRow();
            for (int rows = 0; rows < origTable.Rows.Count; rows++)
            {
                if (rows < origTable.Columns.Count)
                {
                    dr[0] = origTable.Columns[cols].ColumnName; // Add the Column Name in the first Column
                    dr[rows + 1] = origTable.Rows[rows][cols];
                }
            }
            newTable.Rows.Add(dr); //add the DataRow to the new Table rows collection
        }
        return newTable;
    }



    protected void btsearch_Click(object sender, EventArgs e)
    {

    }

    protected void grdori_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }

    protected void btadd_Click(object sender, EventArgs e)
    {
        if (hditem.Value == "" || hditem.Value == null)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item can not empty!','Item,'warning');", true);
            return;
        }
        if (txqty.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty can not empty!','Qty,'warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@pp_cd", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespoint_cd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
        arr.Add(new cArrayList("@qty", txqty.Text));
        arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
        bll.vInsertDistributionPlanDtl(arr); arr.Clear();
        txitem.Text = "";
        txqty.Text = "";
        btsave.Visible = true;
    }

    protected void grdplan_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        Label lbsalespoint = (Label)grdplan.Rows[e.RowIndex].FindControl("lbsalespoint");
        Label lbitemcode = (Label)grdplan.Rows[e.RowIndex].FindControl("lbitemcode");
        arr.Add(new cArrayList("@pp_cd", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespoint_cd", lbsalespoint.Text));
        arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        bll.vDelDistributionPlanDtl(arr);
        arr.Clear();
        arr.Add(new cArrayList("@pp_cd", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdplan, "sp_tpch_distplan_dtl_get", arr);
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_distributionplan.aspx");
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        if (dtdelivery.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Delivery Date can not empty!','Delivery Date,'warning');", true);
            return;
        }
        if (grdplan.Rows.Count <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, 
                Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item Distribution can not empty!','Item Distribution,'warning');", true);
            return;
        }

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@delivery_dt", DateTime.ParseExact(dtdelivery.Text, "d/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@salespoint_cd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@pp_status_id", "N"));
        string sDistPlan = "";
        bll.vInsertDistributionPlan(arr, ref sDistPlan);
        lbppno.Text = sDistPlan;
        btsave.Visible = false;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Distribution Plan for Delivery: "+dtdelivery.Text+"', has been saved.,'Distribution Plan','success');", true);
    }
}