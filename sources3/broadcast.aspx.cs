using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class broadcast : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<string> lapp = bll.lGetApproval("2540");
            bll.vSendMail(lapp[1], "test", "teset saja dari saaa");

        }
    }
}