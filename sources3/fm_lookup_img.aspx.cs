using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_lookup_img : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        cbll bll = new cbll();
        if (!IsPostBack)
        { 
            string sCust = Request.QueryString["cust"];
            string sLoc = bll.vLookUp("select fileloc from tcustomer_document where cust_cd='"+sCust+"'");
            if ((sLoc == null) || (sLoc==""))
            { sLoc = "/noimage.jpg"; }
            img.ImageUrl = sLoc;
        }
    }
}