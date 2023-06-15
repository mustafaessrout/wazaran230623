using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.NetworkInformation;
using System.Linq;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class UtitlityBasePage : System.Web.UI.Page
{
    private string _metakeywords;
    private string _metadescription;

    public string MetaKeywords
    {
        get
        {
            return _metakeywords;
        }
        set
        {
            _metakeywords = value;
        }
    }
    public string MetaDescription
    {
        get
        {
            return _metadescription;
        }
        set
        {
            _metadescription = value;
        }
    }
    protected override void OnLoad(EventArgs e)
    {
        if (!String.IsNullOrEmpty(MetaKeywords))
        {
            HtmlMeta tag = new HtmlMeta();
            tag.Name = "keywords";
            tag.Content = MetaKeywords;
            Header.Controls.Add(tag);
        }

        if (!String.IsNullOrEmpty(MetaDescription))
        {
            HtmlMeta tag = new HtmlMeta();
            tag.Name = "description";
            tag.Content = MetaDescription;
            Header.Controls.Add(tag);
        }
        base.OnLoad(e);

        var macAddr =
            (
                from nic in NetworkInterface.GetAllNetworkInterfaces()
                where nic.OperationalStatus == OperationalStatus.Up
                select nic.GetPhysicalAddress().ToString()
            ).FirstOrDefault();

        var a = NetworkInterface.GetAllNetworkInterfaces();
    }

    void Page_Error(object sender, EventArgs e)
    {
        var path = HttpContext.Current.Request.Url.AbsolutePath;
        Exception exc = Server.GetLastError();
        Session["error"] = "Server error  " + exc.Message ;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "lp", "window.open('fm_Error.aspx','mywindow','toolbar=n,scrollbars=y,width=800,height=800,top=75,left=300',true);", true);
        Response.Redirect(HttpContext.Current.Request.Url.AbsolutePath.Split('/')[1]);
        //Server.Transfer("fm_Error.aspx");
    }
}
