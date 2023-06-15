using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_manualno : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //DateTime dtwaz_dt = DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //DateTimeFormatInfo mfi = new DateTimeFormatInfo();
            //var firstDayOfMonth = new DateTime(dtwaz_dt.Month, dtwaz_dt.Year, 1);
            //var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            //dtstart.Text = firstDayOfMonth.ToString("d/M/yyyy");
            //dtend.Text = lastDayOfMonth.ToString("d/M/yyyy");
            showCheck.Attributes.Remove("style");
            showInvoice.Attributes.Add("style", "display:none");
        }
    }

    protected void btcheck_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        System.Data.SqlClient.SqlDataReader rs = null;

        
        List<int> list_invoice = new List<int>();
        List<int> list_invoice_missing = new List<int>();

        arr.Clear();
        arr.Add(new cArrayList("@type", "invoice"));
        bll.vGetManualNo(arr, ref rs);
        while (rs.Read())
        {
            list_invoice.Add(int.Parse(rs["manual_no"].ToString()));
        }

        int[] arr_invoice = list_invoice.ToArray();

        int min_no = list_invoice.Min();
        int max_no = list_invoice.Max();
        int len_data = max_no - min_no;

        int[] register_invoice = new int[len_data];
        int min_new = min_no;
        for (int i = 1; i < len_data; i++ )
        {
            min_new++;
            if (!list_invoice.Contains(min_new)) {
                list_invoice_missing.Add(min_no);
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@manual_no", min_new));
                arr.Add(new cArrayList("@manual_type", "INV"));
                bll.vInsertManualNo(arr);
            }
        }
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdinvoice, "sp_get_manualno_ins", arr);
        showInvoice.Attributes.Remove("style");
    }

    protected void grdinvoice_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void grdinvoice_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void grdinvoice_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
}