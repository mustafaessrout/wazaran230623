using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
public partial class frmMonthYear : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@YearCD", txYearCD.Text));
        bll.vBindingGridToSp(ref grdMonth, "sp_MonthYear_get2", arr);
    }
    protected void btAdd_Click(object sender, EventArgs e)
    {

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@seqID", txseqID.Text));
        arr.Add(new cArrayList("@YearCD", txYearCD.Text));
        arr.Add(new cArrayList("@MonthCD", txMonthCD.Text));
        arr.Add(new cArrayList("@ymtName", txymtName.Text));
        arr.Add(new cArrayList("@ymtStart", txymtStart.Text));
        arr.Add(new cArrayList("@ymtEnd", txymtEnd.Text));
        bll.vInsertMonthYear(arr);
        bll.vBindingGridToSp(ref grdMonth, "sp_MonthYear_get2");
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        //string sYearCD = txYearCD.Text;
        arr.Add(new cArrayList("@YearCD", txYearCD.Text));
        arr.Add(new cArrayList("@yeaStart", txyeaStart.Text));
        arr.Add(new cArrayList("@yeaEnd", txyeaEnd.Text));
        bll.vInsertYear(arr);
    }
    protected void grdMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lbseqID = (Label)grdMonth.SelectedRow.FindControl("lbseqID");
        Label lblMonthCD = (Label)grdMonth.SelectedRow.FindControl("lblMonthCD");
        Label lblymtName = (Label)grdMonth.SelectedRow.FindControl("lblymtName");
        Label lblymtStart = (Label)grdMonth.SelectedRow.FindControl("lblymtStart");
        Label lblymtEnd = (Label)grdMonth.SelectedRow.FindControl("lblymtEnd");
        txseqID.Text = lbseqID.Text;
        txMonthCD.Text = lblMonthCD.Text;
        txymtName.Text = lblymtName.Text;
        txymtStart.Text = lblymtStart.Text;
        txymtEnd.Text = lblymtEnd.Text;
    }
    protected void txYearCD_TextChanged(object sender, EventArgs e)
    {
        //SqlDataReader rs = null;
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@YearCD", txYearCD.Text));
        //bll.vGetYear(arr, ref rs);
        //while (rs.Read())
        //{
        //    txyeaStart.Text = rs["yeaStart"].ToString();
        //    txyeaEnd.Text = rs["yeaEnd"].ToString();
        //} rs.Close();
        ////Display grid
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@YearCD", txYearCD.Text));
        bll.vBindingGridToSp(ref grdMonth, "sp_MonthYear_get2", arr);
    }
    protected void btWiz_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@varYearCD", txYearCD.Text));
        bll.vInsertMonthYearWiz(arr);
        arr.Clear();
        arr.Add(new cArrayList("@YearCD", txYearCD.Text));
        bll.vBindingGridToSp(ref grdMonth, "sp_MonthYear_get2",arr);
    }
    protected void bttmp_Click(object sender, EventArgs e)
    {

        SqlDataReader rs = null;
        txYearCD.Text = Convert.ToString(Session["looYearYearCD"]);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@YearCD", txYearCD.Text));
        bll.vGetTblYear(arr, ref rs);
        while (rs.Read())
        {
            txYearCD.Text = rs["YearCD"].ToString();
            txyeaStart.Text = String.Format("{0:dd-MM-yyyy}", rs["yeaStart"].ToString());
            txyeaEnd.Text = String.Format("{0:dd-MM-yyyy}", rs["yeaEnd"].ToString());
          
        } rs.Close();
        arr.Clear();
        arr.Add(new cArrayList("@YearCD", txYearCD.Text));
        bll.vBindingGridToSp(ref grdMonth, "sp_MonthYear_get2", arr);
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {

    }
    protected void grdMonth_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        Label lblMonthCD = (Label)grdMonth.SelectedRow.FindControl("lblMonthCD");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@MonthCD", lblMonthCD.Text));
        bll.vDelMontYear(arr);
        arr.Clear();
        arr.Add(new cArrayList("@YearCD", txYearCD.Text));
        bll.vBindingGridToSp(ref grdMonth, "sp_MonthYear_get2", arr);
    }
}
