using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_printall : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            string sPrintType = Request.QueryString["printtype"];
            arr.Add(new cArrayList("@print_typ", sPrintType));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_twrk_printall_get", arr);
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        string sPrintType = Request.QueryString["printtype"];
        int count = 0; List<cArrayList> arr = new List<cArrayList>();
        foreach (GridViewRow row in grd.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                Label lbdocno = (Label)row.FindControl("lbdocno");
                CheckBox chk = (CheckBox)row.FindControl("chk");
                if (sPrintType == "payment")
                {
                    if (chk.Checked)
                    {
                        string sSta = bll.vLookUp("select payment_sta_id from tmst_payment where payment_no='" + lbdocno.Text + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                        string sPaymentType = bll.vLookUp("select payment_typ from tmst_payment where payment_no='" + lbdocno.Text + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                        if (((sPaymentType == "CQ") || (sPaymentType == "BT") || (sPaymentType == "IT")) && (sSta == "N"))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "pr" + count.ToString(), "window.open('fm_report2.aspx?src=pycheque&py=" + lbdocno.Text + "');", true);

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "pr" + count.ToString(), "window.open('fm_report3.aspx?src=py&noy=" + lbdocno.Text + "');", true);


                        }
                        arr.Clear();
                        arr.Add(new cArrayList("@doc_no", lbdocno.Text));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        bll.vDelWrkPrintAll(arr);
                       
                    }
                }
                else if (sPrintType == "canvas")
                {
                    if (chk.Checked)
                    {
                        
                        string sDiscAmt = bll.vLookUp("select sum(amt) from tcanvasorder_disccash where so_cd='" + lbdocno.Text + "' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                        string sInvNo = bll.vLookUp("select inv_no from tmst_dosales where so_cd='"+lbdocno.Text+"' and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "'");
                        arr.Clear();
                        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                        arr.Add(new cArrayList("@inv_no", sInvNo));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        bll.vInsertRptDosalesInvoice(arr);
                        arr.Clear();
                        arr.Add(new cArrayList("@doc_no", sInvNo));
                        arr.Add(new cArrayList("@print_cd", "CANVINV")); //Canvas INvoice
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        //arr.Add(new cArrayList("@qty"))
                        bll.vInsertPrintControl(arr);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op" + count.ToString(), "window.open('fm_report3.aspx?src=inv&no=" + sInvNo + "&amt=" + sDiscAmt + "');", true);
                        arr.Clear();
                        arr.Add(new cArrayList("@doc_no", lbdocno.Text));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        bll.vDelWrkPrintAll(arr);
                    }
                }
                count++;
            }
        }
        arr.Clear();
        arr.Add(new cArrayList("@print_typ", Request.QueryString["printtype"]));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_printall_get", arr);
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grd.PageIndex = e.NewPageIndex;
        arr.Add(new cArrayList("@print_typ", Request.QueryString["printtype"]));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_printall_get", arr);
    }
}