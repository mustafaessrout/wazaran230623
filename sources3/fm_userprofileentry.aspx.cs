using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_userprofileentry : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", txusrid.Text));
        arr.Add(new cArrayList("@fullname", txfullname.Text));
        arr.Add(new cArrayList("@mobile_no", txmobile.Text));
        arr.Add(new cArrayList("@home_no", txhome.Text));
        arr.Add(new cArrayList("@pwd_exp_dt", dtexp.Text));
        arr.Add(new cArrayList("@resign_dt", dtresign.Text));
        arr.Add(new cArrayList("@isactive", "1"));
        arr.Add(new cArrayList("@passwd", txpassword.Text));
        arr.Add(new cArrayList("@whatsapp", txwa.Text));
        arr.Add(new cArrayList("@email", txemail.Text));
        bll.vInsertUserProfile(arr);

    }
}