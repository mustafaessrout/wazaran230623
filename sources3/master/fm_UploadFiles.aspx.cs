using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_UploadFiles : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cd = new cdal();
    string domainName = string.Empty;
    string moduleName = string.Empty;
    Utitlity ut = new Utitlity();
    protected void Page_Load(object sender, EventArgs e)
    {
    if (!Page.IsPostBack)
        {
            
            if (Request.QueryString["operation"] != null) {
            }

        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {

            string fileName = upl.FileName;
            
            if (upl.HasFile)
            {
                FileInfo fi = new FileInfo(upl.FileName);
                string ext = fi.Extension;
                byte[] fs = upl.FileBytes;
                if (fs.Length <= 6000000)
                {
                    if ((upl.FileName != "") || (upl.FileName != null))
                    {
                        Session["fileSize"] = fs;
                        Session["fileName"] = Request.Cookies["sp"].Value.ToString() + Convert.ToString(upl.FileName);
                        //upl.SaveAs(bll.sGetControlParameter("image_path") + "/account/asset/" + Request.Cookies["sp"].Value.ToString() + Convert.ToString(upl.FileName));
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "UploadFIlesData", "closewin()", true);
                    }
                }
            }
            //if (upl.PostedFile.ContentLength < 1048576)//1048576
            //{
            //    Session["fileName"] = fileName;
            //    Session["fileLocation"] = baseName;
                
            //    upl.SaveAs(baseName);
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "UploadFIlesData", "closewin()", true);
            //}
            //else {
            //    lblUploadMessage.Text = "File size of " + Convert.ToString(upl.PostedFile.ContentLength / 1024 / 1024) + " MB is exceeding the uploading limit.";
            //    lblUploadMessage.ForeColor = Color.Red;
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','File is greater that defintion','error');", true);
            //}
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during upload','error');", true);
            ut.Logs("", "Admin", "Upload Files", "fm_UploadFiles", "btnUpload_Click", "Exception", ex.Message + ex.InnerException);

        }
    }
    
    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Session["tfield_value"] = dataKeys + "," + Convert.ToString(Request.QueryString["fld_nm"]);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "closewin()", true);
    }
}