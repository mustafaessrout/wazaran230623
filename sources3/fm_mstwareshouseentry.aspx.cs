using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstwareshouseentry : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbstocktyp, "stock_typ");
            bll.vBindingFieldValueToCombo(ref cblevelno, "whs_level_no");
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            bll.vBindingComboToSp(ref cbparent, "sp_tmst_warehouse_get", "whs_cd", "whs_nm");
        }
    }
}