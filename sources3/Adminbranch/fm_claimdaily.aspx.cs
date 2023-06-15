using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Adminbranch_fm_claimdaily : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            lbbranch.Text = Request.Cookies["spn"].Value.ToString();
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> linv = new List<string>();
        string sInv = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@inv_no", prefixText));
        bll.vGetClaimInvoice(arr, ref rs);
        while (rs.Read())
        {
            sInv = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["inv_no"].ToString(), rs["inv_no"].ToString());
            linv.Add(sInv);
        }
        rs.Close();
        return (linv.ToArray());
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (uplo.HasFile && uplf.HasFile)
        {
            FileInfo fio = new FileInfo(uplo.FileName);
            string exto = fio.Extension;
            byte[] fso = uplo.FileBytes;
            FileInfo fif = new FileInfo(uplf.FileName);
            string extf = fif.Extension;
            byte[] fsf = uplf.FileBytes;
            if (fso.Length <= 104857600 && fsf.Length <= 104857600)
            {
                if ((uplo.FileName != "" && uplf.FileName != "") || (uplo.FileName != null && uplf.FileName != null))
                {
                    arr.Add(new cArrayList("@fileinv", hdinv.Value.ToString() + "_O_" + exto));
                    arr.Add(new cArrayList("@fileinv_f", hdinv.Value.ToString() + "_F_" + extf));
                    uplo.SaveAs(bll.sGetControlParameter("image_path") + "/invoice_doc/" + hdinv.Value.ToString() + "_O_" + exto);
                    uplf.SaveAs(bll.sGetControlParameter("image_path") + "/invoice_doc/" + hdinv.Value.ToString() + "_F_" + extf);
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
        else
        {
            if (uplo.HasFile)
            {
                FileInfo fio = new FileInfo(uplo.FileName);
                string exto = fio.Extension;
                byte[] fso = uplo.FileBytes;
                if (fso.Length <= 104857600)
                {
                    if ((uplo.FileName != "") || (uplo.FileName != null))
                    {
                        arr.Add(new cArrayList("@fileinv", hdinv.Value.ToString() + "_O_" + exto));
                        uplo.SaveAs(bll.sGetControlParameter("image_path") + "/invoice_doc/" + hdinv.Value.ToString() + "_O_" + exto);
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
            }
            else if (uplf.HasFile)
            {
                FileInfo fif = new FileInfo(uplf.FileName);
                string extf = fif.Extension;
                byte[] fsf = uplf.FileBytes;
                if (fsf.Length <= 104857600)
                {
                    if ((uplf.FileName != "") || (uplf.FileName != null))
                    {
                        arr.Add(new cArrayList("@fileinv_f", hdinv.Value.ToString() + "_F_" + extf));
                        uplf.SaveAs(bll.sGetControlParameter("image_path") + "/invoice_doc/" + hdinv.Value.ToString() + "_F_" + extf);
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
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Please attach all the documents required','Invoice Scan!');", true);
                return;
            }
        }
        arr.Add(new cArrayList("@inv_no", hdinv.Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vUploadInvoiceUpd(arr);
    }
}