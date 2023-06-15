using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_claiminfoho : System.Web.UI.Page
{
    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sClaimCode = Request.QueryString["dc"];
            string sBranch = Request.QueryString["sp"];
            string sType = "";
            lbclaim.Text = sClaimCode;
            lbbranch.Text = sBranch;
            System.Data.SqlClient.SqlDataReader rs = null;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@claim_no", sClaimCode));
            arr.Add(new cArrayList("@branch", sBranch));
            bll.vGetMstClaimViewHO(arr, ref rs);
            while (rs.Read())
            {
                lbdate.Text = Convert.ToDateTime(rs["claim_dt"]).ToShortDateString();
                lbstatus.Text = rs["status"].ToString();
                lbpropno.Text = rs["prop_no"].ToString();
                lbccnr.Text = rs["ccnr_no"].ToString();
                lbremark.Text = rs["remarka"].ToString();
                sType = rs["discount_mec"].ToString();
                if (rs["discount_mec"].ToString() != "CSH")
                {
                    if (rs["ordervalue"].ToString() == "0.00")
                    {
                        lborder.Text = "Qty";
                        lbtotalorder.Text = rs["total_order"].ToString();
                        lbfree.Text = "Qty";
                        lbtotalfree.Text = rs["freeqty"].ToString();
                    }
                    else
                    {
                        lborder.Text = "Qty";
                        lbtotalorder.Text = rs["ordervalue"].ToString();
                        lbfree.Text = "SAR";
                        lbtotalfree.Text = rs["freevalue"].ToString();
                    }
                }
                else
                {
                    lborder.Text = "Qty";
                    lbtotalorder.Text = "0";
                    lbfree.Text = "Qty";
                    lbtotalfree.Text = "0";
                }
                
                lbamount.Text = "SAR";
                lbtotalamount.Text = rs["amount"].ToString();
                txtotalamount.Text = rs["total"].ToString();

                if (rs["rdcost"].ToString() == "Percentage")
                {
                    lbcost.Text = "Sbtc:" + rs["sbtccost"].ToString() + "%, Principal:" + rs["principalcost"].ToString() + "%";
                }
                else if (rs["rdcost"].ToString() == "sbtc")
                {
                    lbcost.Text = "Sbtc:" + rs["sbtccost"].ToString() + "%";
                }
                else
                {
                    lbcost.Text = "Principal:" + rs["principalcost"].ToString() + "%";
                }
            }
            arr.Clear();
            arr.Add(new cArrayList("@claim_no", sClaimCode));
            arr.Add(new cArrayList("@branch", sBranch));
            if (sType == "CSH") { bll.vBindingGridToSp(ref grdcash, "sp_tclaim_ho_info", arr); }
            else { bll.vBindingGridToSp(ref grddetail, "sp_tclaim_ho_info", arr); }
            arr.Clear();
            arr.Add(new cArrayList("@clh_no", null));
            arr.Add(new cArrayList("@claim_no", sClaimCode));
            arr.Add(new cArrayList("@branch", sBranch));
            bll.vBindingGridToSp(ref grddocument, "sp_tclaim_document", arr);
            //bll.vBindingGridToSp(ref grdsales, "sp_tclaim_ho_info", arr); 

            // Adjustment Properties 
            lbtotalamount.Attributes.Add("style","display:none");
            txtotalamount.ReadOnly = true;
            txtotalamount.BackColor = System.Drawing.ColorTranslator.FromHtml("#939391");
            string typeCL = lbclaim.Text.Substring(0, 3).ToString();
            if (typeCL == "CLO") { btadjustment.Attributes.Add("style", "display:none"); }
            else { btadjustment.Attributes.Remove("style"); }
        }
    }

    protected void btapprove_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_claimentry_ho.aspx");
    }

    protected void btreject_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_claimentry_ho.aspx");
    }

    protected void print_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=sendclaim&clh=" + Request.QueryString["dc"] + "');", true);
    }

    protected void btadjustment_Click(object sender, EventArgs e)
    {
        double amount = 0; 
        if (btadjustment.Text == "Edit")
        {
            txtotalamount.ReadOnly = false;
            btadjustment.Text = "Update";
            txtotalamount.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        }
        else
        {
            if (!double.TryParse(txtotalamount.Text, out amount))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Total Amount must numeric or can not empty','Claim Detail Adjustment','warning');", true);
                return;
            }
            txtotalamount.ReadOnly = true;
            btadjustment.Text = "Edit";
            txtotalamount.BackColor = System.Drawing.ColorTranslator.FromHtml("#939391");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@claim_no", lbclaim.Text));
            arr.Add(new cArrayList("@amount", txtotalamount.Text));
            arr.Add(new cArrayList("@diff", (Convert.ToDouble((txtotalamount.Text.Replace(" ", "") == "" ? "0" : txtotalamount.Text.Replace(" ", ""))) - Convert.ToDouble((lbtotalamount.Text.Replace(" ", "") == "" ? "0" : lbtotalamount.Text.Replace(" ", ""))))));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vUpdateClaimData(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('This Claim has been Adjust.','" + lbclaim.Text + "','success');", true);
        }
    }
    protected void grddetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grddetail.EditIndex = e.NewEditIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@claim_no", lbclaim.Text));
        arr.Add(new cArrayList("@branch", lbbranch.Text));
        //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grddetail, "sp_tclaim_ho_info", arr);
    }
    protected void grddetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string invNo = "";
        TextBox qtyorder = (TextBox)grddetail.Rows[e.RowIndex].FindControl("txqtyorder");
        TextBox qtyfree = (TextBox)grddetail.Rows[e.RowIndex].FindControl("txqtyfree");
        TextBox unitprice = (TextBox)grddetail.Rows[e.RowIndex].FindControl("txunitprice");
        TextBox disc_amt = (TextBox)grddetail.Rows[e.RowIndex].FindControl("txdisc_amt");
        Label lbinvoiceno = (Label)grddetail.Rows[e.RowIndex].FindControl("lbinvoiceno");
        //invNo = (lbinvoiceno.Text == "") : "0" ? lbinvoiceno.Text;
        List<cArrayList> arr = new List<cArrayList>();
       // arr.Add(new cArrayList("@inv_no", lbinvoiceno.Text));
        arr.Add(new cArrayList("@claim_no", lbclaim.Text));
        arr.Add(new cArrayList("@salespoint", lbbranch.Text));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@qtyorder", qtyorder.Text));
        arr.Add(new cArrayList("@qtyfree", qtyfree.Text));
        arr.Add(new cArrayList("@unitprice", unitprice.Text));
        arr.Add(new cArrayList("@disc_amt", disc_amt.Text));
        bll.vUpdateClaimDetail(arr);
        grddetail.EditIndex = -1;
        arr.Clear();
        Response.Redirect(Request.RawUrl);
        //arr.Add(new cArrayList("@claim_no", lbclaim.Text));
        //arr.Add(new cArrayList("@branch", lbbranch.Text));
        //bll.vBindingGridToSp(ref grddetail, "sp_tclaim_ho_info", arr);
    }
    protected void grddocument_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;
        HiddenField hddoc_cd = (HiddenField)e.Row.FindControl("hddoc_cd");

        if (hddoc_cd.Value == "P02")
        {
            Label lbfilelocproposal = (Label)e.Row.FindControl("lbfilelocproposal");
            Label lbfileloc = (Label)e.Row.FindControl("lbfileloc");
            lbfileloc.Visible = false;
            lbfilelocproposal.Visible = true;
        }
        else
        {
            Label lbfilelocproposal = (Label)e.Row.FindControl("lbfilelocproposal");
            Label lbfileloc = (Label)e.Row.FindControl("lbfileloc");
            lbfileloc.Visible = true;
            lbfilelocproposal.Visible = false;
        }
    }
}