using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_dailyrefresh : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        cbll bll=new cbll();
        if (!IsPostBack)
        {
            bll.vBatchBanner2();
            bll.vBatchGenerateDailyRPS();
            bll.vBatchTargetCollectionBranch();
            //exec sp_batch_generatedailyrps;
         
            //exec sp_batchbranchitemsold;

        }
    }
}