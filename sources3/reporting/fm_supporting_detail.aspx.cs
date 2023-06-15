using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class reporting_fm_supporting_detail : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            string sNo = Request.QueryString["no"];
            lbtitle.Text = bll.vLookUp("select problem_title+'('+remark+')' from ttab_problem_solving where ps_cd='"+sNo+"'");
            lbstatus.Text = bll.vLookUp("select (case when ps_sta_id = 'N' then 'NEW' else 'COMPLETED' end) from ttab_problem_solving where ps_cd='" + sNo + "'");
            lbcreated.Text = bll.vLookUp("select (a.creator+'-'+cr.emp_nm) from ttab_problem_solving a left join tmst_employee cr on a.creator=cr.emp_cd where a.ps_cd='"+sNo+"'");
            arr.Clear();
            arr.Add(new cArrayList("@no", sNo));
            bll.vBindingGridToSp(ref grdchat, "sp_supporting_detail_get", arr);
        }
    }

    protected void btreply_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@no", Request.QueryString["no"]));
        arr.Add(new cArrayList("@message", txMessage.Text));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vSuportingTicketReply(arr);
        arr.Clear();
        arr.Add(new cArrayList("@no", Request.QueryString["no"]));
        bll.vBindingGridToSp(ref grdchat, "sp_supporting_detail_get", arr);
        txMessage.Text = "";
    }

    protected void btclose_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@no", Request.QueryString["no"]));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@status", "C"));
        bll.vSuportingTicketClose(arr);
        arr.Clear();
        arr.Add(new cArrayList("@no", Request.QueryString["no"]));
        bll.vBindingGridToSp(ref grdchat, "sp_supporting_detail_get", arr);
        lbstatus.Text = bll.vLookUp("select (case when ps_sta_id = 'N' then 'NEW' else 'COMPLETED' end) from ttab_problem_solving where ps_cd='" + Request.QueryString["no"] + "'");
        txMessage.Text = "";
    }

    protected void btopen_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@no", Request.QueryString["no"]));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@status", "N"));
        bll.vSuportingTicketClose(arr);
        arr.Clear();
        arr.Add(new cArrayList("@no", Request.QueryString["no"]));
        bll.vBindingGridToSp(ref grdchat, "sp_supporting_detail_get", arr);
        lbstatus.Text = bll.vLookUp("select (case when ps_sta_id = 'N' then 'NEW' else 'COMPLETED' end) from ttab_problem_solving where ps_cd='" + Request.QueryString["no"] + "'");
        txMessage.Text = "";
    }
}