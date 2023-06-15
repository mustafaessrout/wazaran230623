using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
//using iTextSharp.text.pdf;
using System.IO;
using System.Net;
using System.Net.Http;
public partial class cashamountclosingadv : System.Web.UI.Page
{
    cbll bll = new cbll();
    cbll2 bll2 = new cbll2();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            string _salespoint = Request.Cookies["sp"].Value;
            string sCheck = bll.vLookUp("select dbo.fn_cashregisterpendingadv('"+_salespoint+"')");
            if (sCheck != "ok")
            {
                //lbalert.Text = "Pending:" + sCheck;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Pending:  " + sCheck + "','You can not do closing now','warning');", true);
                return;
                //bll.vBindingGridToSp(ref grd, "sp_tcashregister_getbycashstaid");
            }
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", _salespoint));
            bll.vBindingGridToSp(ref grdcsh, "sp_cashregister_closing_get", arr);
        }
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }


    protected void btsave_Click(object sender, EventArgs e)
    {
        string sCheck = bll.vLookUp("select dbo.fn_cashregisterpendingadv('" + Request.Cookies["sp"].Value +"')");
        if (sCheck != "ok")
        {
            //lbalert.Text = "Pending:" + sCheck;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Pending:  " + sCheck + "','You can not do closing now','warning');", true);
            return;
            //bll.vBindingGridToSp(ref grd, "sp_tcashregister_getbycashstaid");
        }
        //btsave.Visible = false;
        //btsave.Enabled = false;
        //cd.v_hiddencontrol(btsave);
        List<cArrayList> arr = new List<cArrayList>();
        try
        {
            if (grdcsh.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Petty Cashier Either Already Closing Or You do not have Petty Cash','You can not do closing now','warning');", true);
                return;
            }

            //
            //string sResponse = client.DownloadString(sAPI + "SendMessage?");
            //DateTime dtdate = Convert.ToDateTime(DateTime.ParseExact(lbchclosing_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            //DateTime sdtdate = new DateTime(dtdate.Year, dtdate.Month, dtdate.Day);
            //string sdate = sdtdate.ToString("M/d/yyyy");
            string ssp = Request.Cookies["sp"].Value.ToString();
            //string cek = bll.vLookUp("select count(*) from tcashregister_advance_closing where salespointcd='" + ssp + "' and chclosing_dt='" + sdate + "'");
            //if (cek != "0")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Cashier already closing','You can not make closing more than once','warning');", true);
            //    return;
            //}
            foreach (GridViewRow row in grdcsh.Rows)
            {
                FileUpload upl = (FileUpload)row.FindControl("fupl");
                if (upl.HasFile)
                {
                    FileInfo fi = new FileInfo(upl.FileName);
                    string ext = fi.Extension;
                    byte[] fs = upl.FileBytes;
                    if (fs.Length > 1000000)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Document Size must be less than 1 MB','Document size','warning');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Document mandatory must be uploaded','Customer Document','warning');", true);
                    return;
                }
            }
            if (lbchclosingno.Text != "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('Closing cashier has been save','You can not retry','warning');", true);
                return;
            }
            else
            {

                string schclosingno = "";
                //if (lbalert.Text != "No Pending Cashregister")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();sweetAlert('There are pending paid/received all !','Cash Register','warning');", true);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "vDisableShow();", true);
                //    return;
                //}              
                //arr.Add(new cArrayList("@chclosingno", sCashID));
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //arr.Add(new cArrayList("@chclosing_dt", DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                //arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
                //arr.Add(new cArrayList("@acknowledge", "N"));
                //arr.Add(new cArrayList("@cashier_id" ,))
                //string _no = string.Empty;
                ////bll.vInserttcashregister_closingAdv(arr, ref schclosingno);
                //bll2.vInsertCashregisterClosingAdvance(arr, ref _no);
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@closing_dt", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                bll.vBatchClosingCashierAdv(arr);
                //lbchclosingno.Text = _no;
                foreach (GridViewRow row in grdcsh.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        Label lbdoccode = (Label)row.FindControl("lbcashier_cd");
                        FileUpload upl = (FileUpload)row.FindControl("fupl");
                        FileInfo fi = new FileInfo(upl.FileName);
                        string ext = fi.Extension;
                        DateTime dt = DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        if ((upl.FileName != "") || (upl.FileName != null))
                        {
                            upl.SaveAs(bll.sGetControlParameter("image_path") + @"pettycash\" + Request.Cookies["sp"].Value.ToString() + dt.Year + "-" + dt.Month + "-" + dt.Day + "-" + schclosingno + " - " + lbdoccode.Text + ext);
                        }
                        arr.Clear();
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        arr.Add(new cArrayList("@chclosing_dt", DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
                        arr.Add(new cArrayList("@acknowledge", "Y")); //Bypassed 
                        arr.Add(new cArrayList("@cashier_id", lbdoccode.Text));
                       
                        string _no = string.Empty;
                        //bll.vInserttcashregister_closingAdv(arr, ref schclosingno);
                        bll2.vInsertCashregisterClosingAdvance(arr, ref _no);
                        lbchclosingno.Text = _no;
                        //arr.Add(new cArrayList("@chclosingno", _no));
                        //arr.Add(new cArrayList("@cashier_id", lbdoccode.Text));
                        //arr.Add(new cArrayList("@cashierclosing_file", Request.Cookies["sp"].Value.ToString() + dt.Year + "-" + dt.Month + "-" + dt.Day + "-" + schclosingno + " - " + lbdoccode.Text + ext));
                        ////bll.vInsertCashregisterClosingInfoAdvance(arr);
                    }
                }
                //arr.Clear();

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Closing PettyCashier has been saved successfully , and need acknowledge','Transaction No. " + schclosingno + "','info');", true);
                cd.v_hiddencontrol(btsave);
            }
        }
        catch (Exception ex)
        {
            arr.Clear();
            arr.Add(new cArrayList("@err_source", "Save Cashamount advance Closing"));
            arr.Add(new cArrayList("@err_description", ex.Message.ToString()));
            bll.vInsertErrorLog(arr);
        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        }
    }


    protected void btclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "window.opener.RefreshData();window.close();", true);
    }
    protected void bttmp_Click(object sender, EventArgs e)
    {
        SqlDataReader rs = null;
        lbchclosingno.Text = Convert.ToString(Session["loochclosingno"]);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@chclosingno", lbchclosingno.Text));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vGettcashregister_closing(arr, ref rs);
        while (rs.Read())
        {
            //string sdate = string.Format("{0:d/M/yyyy}", rs["chclosing_dt"]);
            //DateTime dtdate = Convert.ToDateTime(sdate);
            //string strDate = dtdate.ToString("d/M/yyyy");            
        }
        rs.Close();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
    }

}