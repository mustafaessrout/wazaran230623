using System;

public partial class lookupimagegr : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sSrc = Request.QueryString["src"];
            string sType = Request.QueryString["f"];
            string sLoc = bll.sGetControlParameter("image_path");
            string sFilename = string.Empty;
            if (sType == "n")
            {

                sFilename = bll.vLookUp("select receiptfile from tgoodreceipt_info where receipt_no='" + sSrc + "'");
                
            }
            else if (sType == "g")
            {
                sFilename = bll.vLookUp("select gdnfile from tgoodreceipt_info where receipt_no='" + sSrc + "'");
            }
            img.ImageUrl = "~/images/" + sFilename;


        }
    }

    public string GetVirtualPath(string physicalPath)
    {
        if (!physicalPath.StartsWith(Request.PhysicalApplicationPath))
        {
            throw new InvalidOperationException("Physical path is not within the application root");
        }

        return "~/" + physicalPath.Substring(Request.PhysicalApplicationPath.Length)
              .Replace("\\", "/");
    }
}