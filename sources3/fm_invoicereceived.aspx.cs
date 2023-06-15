using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class fm_invoicereceived : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            vBinding();
        }
    }
    void vBinding()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@rcp_sta_id", "N"));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        bll.vBindingGridToSp(ref grd, "sp_tdosalesinvoice_receved_getbystatus", arr);
    }

    protected void btreceived_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        FileUpload fupl = (FileUpload)row.FindControl("fupl");
        Label lbinvoiceno = (Label)row.FindControl("lbinvoiceno");
        Label dtreceived = (Label)row.FindControl("dtreceived");
        string _inv_no = lbinvoiceno.Text;

        if (!fupl.HasFile)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
                "sweetAlert('File invoice back must be upload !','Choose file from invoice scanning','warning');", true);
            return;
        }
        //string _checkpaymentcash = bll.vLookUp("select dbo.fn_checktabcashpayment('"+_inv_no+"')");

        //if (_checkpaymentcash == "not ok")
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('This invoice has payment cash from device, please import payment first before received!','"+_inv_no+"','warning');", true);
        //    return;
        //}
        FileInfo _fileinfo = new FileInfo(fupl.FileName);
        string _ext = _fileinfo.Extension;
        string _filename = bll.sGetControlParameter("image_path") + @"InvoiceReceived\" + _inv_no + _ext;
        fupl.SaveAs(_filename);
        DateTime _dtreceived = System.DateTime.ParseExact(dtreceived.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@inv_no", lbinvoiceno.Text));
        arr.Add(new cArrayList("@rcp_sta_id", "A"));
        arr.Add(new cArrayList("@received_dt", _dtreceived));
        arr.Add(new cArrayList("@received_by", Request.Cookies["usr_id"].Value));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        bll.vInsertDosalesInvoiceReceived(arr);
        string _sql = "update tdosalesinvoice_info set driver_sta_id='Y' where inv_no='"+lbinvoiceno.Text+"'";
        bll.vExecuteSQL(_sql);
        vBinding();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Received invoice has been succeeded!','" + lbinvoiceno.Text + "','success');", true);

    }

    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Label lbgdndate = (Label)e.Row.FindControl("lbgdndate");
            //DateTime _dtgdn = System.DateTime.ParseExact(lbgdndate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            Label dtreceived = (Label)e.Row.FindControl("dtreceived");
            //AjaxControlToolkit.CalendarExtender ext = (AjaxControlToolkit.CalendarExtender)e.Row.FindControl("dtreceived_Extender");
            //ext.StartDate = _dtgdn;
            dtreceived.Text = Request.Cookies["waz_dt"].Value;

            if ((e.Row.RowIndex % 2) == 0)
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
            }

            cd.v_disablecontrol(dtreceived);
        }
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@inv_no", txinvoiceno.Text));
        //bll.vBindingGridToSp(ref grd, "sp_tdosalesinvoice_receved_getbygdn", arr);
    }

    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        vBinding();
    }
}