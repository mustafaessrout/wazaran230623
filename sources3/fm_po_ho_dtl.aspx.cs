using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_po_ho_dtl : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            string sPO = Request.QueryString["dc"];
            string sSP = Request.QueryString["sp"];
            System.Data.SqlClient.SqlDataReader rs = null;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@po_no", sPO));
            arr.Add(new cArrayList("@salespointcd", sSP));
            bll.vGetMstPOHO(arr, ref rs);
            while (rs.Read())
            {
                lbsalespoint.Text = rs["salespoint"].ToString();
                lbpo.Text = rs["po_no"].ToString();
                lbpo_dt.Text = Convert.ToDateTime(rs["po_dt"]).ToShortDateString();
                lbdelivery_dt.Text = Convert.ToDateTime(rs["po_delivery_dt"]).ToShortDateString();
                lbdestination.Text = rs["po_type"].ToString();
                lbto.Text = rs["to_nm"].ToString();
                lbremark.Text = rs["remark"].ToString();
                if (rs["po_sta_id"].ToString() == "N")
                {
                    btprocess.Visible = true;
                }
                else
                {
                    btprocess.Visible = false;
                }
            }
            bll.vBindingGridToSp(ref grdpo, "sp_tmst_po_ho_detail ", arr);
        }
    }

    protected void btprint_Click(object sender, EventArgs e)
    {

    }

    protected void btprocess_Click(object sender, EventArgs e)
    {
        string sPO = Request.QueryString["dc"];
        string sSP = Request.QueryString["sp"];
        List<cArrayList> arr = new List<cArrayList>();
        double dRow = 0;
        string statusPO = "Full";
        foreach (GridViewRow row in grdpo.Rows)
        {
            Label lbitemcode = (Label)row.FindControl("lbitemcode");
            DropDownList cbsource = (DropDownList)row.FindControl("cbsource");
            if(cbsource.SelectedValue.ToString() != "NA")
            {
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", sSP));
                arr.Add(new cArrayList("@po_no", sPO));
                arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
                arr.Add(new cArrayList("@source", cbsource.SelectedValue.ToString()));
                bll.vUpdatePOHOdetail(arr);
            }
            else
            {
                statusPO = "Partial";
            }
        }
        arr.Clear();
        arr.Add(new cArrayList("@salespointcd", sSP));
        arr.Add(new cArrayList("@po_no", sPO));
        arr.Add(new cArrayList("@status", statusPO));
        bll.vUpdMstPOHO(arr);

        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('" + lbitemcode.Text + ":" + cbsource.SelectedValue.ToString() + "','','warning');", true);
        //return;
    }

    protected void grdpo_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdpo.EditIndex = e.NewEditIndex;
    }

    protected void grdpo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdpo.EditIndex = -1;
    }

    protected void grdpo_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
}