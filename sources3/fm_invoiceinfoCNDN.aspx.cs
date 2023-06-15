using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_invoiceinfoCNDN : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string scndn = Request.QueryString["no"];
            System.Data.SqlClient.SqlDataReader rs = null;
            //  grditem.Visible = false;
            //  grdprod.Visible = false;
            List<cArrayList> arr = new List<cArrayList>();

            string x = bll.vLookUp("select count(inv_no) from twrk_claimconfirm where disc_cd='" + scndn + "' and inv_no='" + scndn + "'");
            if (x == "0")
            {
                arr.Add(new cArrayList("@disc_cd", scndn));
                arr.Add(new cArrayList("@inv_no", scndn));
                bll.vInsertTWRKClaimConfirm(arr);
            }
            arr.Clear();
            arr.Add(new cArrayList("@inv_no", scndn.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["SP"].Value.ToString()));
            bll.vGetMstinvoiceCNDN(arr, ref rs);
            while (rs.Read())
            {
                lbcndn.Text = scndn;
                lbproposal.Text = rs["prop_no"].ToString();
                lbcontract.Text = rs["contract_no"].ToString();
                lbcndndt.Text = Convert.ToDateTime(rs["cndn_dt"].ToString()).ToShortDateString();
                bll.sFormat2ddmmyyyy(ref lbcndndt);
            } rs.Close();
            arr.Clear();
            arr.Add(new cArrayList("@inv_no", scndn));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["SP"].Value.ToString()));
            bll.vBindingGridToSp(ref grdcndn, "sp_invoiceinfoCNDN_get", arr);
            arr.Clear();     
        }
    }



    protected void btscratch_Click(object sender, EventArgs e)
    {

        if (bll.vLookUp("select inv_no from tclaim_invoice where inv_no='" + Request.QueryString["no"] + "'") == "")
        {
            List<cArrayList> arr = new List<cArrayList>();
            if (uplo.HasFile)
            {
                FileInfo fio = new FileInfo(uplo.FileName);
                string exto = fio.Extension;
                byte[] fso = uplo.FileBytes;
                if (fso.Length <= 104857600)
                {
                    if ((uplo.FileName != "") || (uplo.FileName != null ))
                    {
                        arr.Add(new cArrayList("@fileinv", Request.QueryString["no"] + "_O_" + exto));
                        arr.Add(new cArrayList("@fileinv_f", null));
                        uplo.SaveAs(bll.sGetControlParameter("image_path") + "/cndn_doc/" + Request.QueryString["no"] + "_O_" + exto);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Please attach all the documents required','Invoice Scan!');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 100MB');", true);
                    return;
                }
                
                //2 file
            }
            arr.Add(new cArrayList("@inv_no", Request.QueryString["no"]));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vUploadInvoice(arr);
        }

        List<cArrayList> arr1 = new List<cArrayList>();
        arr1.Add(new cArrayList("@inv_no", Request.QueryString["no"]));
        arr1.Add(new cArrayList("@disc_cd", lbcndn.Text));
        arr1.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr1.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vUpdateWrkClaimExclude(arr1);
        arr1.Clear();
        arr1.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vDelWrkClaimExclude(arr1);
        Response.Write("<script>window.close();</" + "script>");
        Response.End();
    }

}

