using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class fm_goodreceived_nav : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            vBindingGrid();
        }
    }

    public void vBindingGrid()
    {
        string _url = @"https://nav.transworld.com.eg/api/nav_do/header.php?do_no=All";
        using (WebClient wc = new WebClient())
        {
            try
            {
                wc.BaseAddress = _url;
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                // var _data = wc.DownloadString("/PO/Getbystatus?sta=" + cbstatus.SelectedValue.ToString() + "&sp=" + cbbranch.SelectedValue.ToString());
                var _data = wc.DownloadString(_url);
                if (!_data.Equals(null))
                {
                    List<tgdn_header_nav> json = JsonConvert.DeserializeObject<List<tgdn_header_nav>>(_data);
                    grd.DataSource = json;
                    grd.DataBind();
                    Session["tgdn_header_nav"] = json;

                }
            }
            catch (Exception ex)
            {
                bll.vHandledError(ref ex, "PO Branch");
            }
        }
    }


    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (LinkButton button in e.Row.Cells[2].Controls.OfType<LinkButton>())
                {
                    if ((button.CommandName == "Edit") || (button.CommandName == "Update") || button.CommandName == "Select")
                    {
                        button.Attributes.Add("OnClick", "ShowProgress();");
                    }
                }
            }
    }
}
