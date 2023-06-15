using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class maintenance_fm_fldvalue : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@fld_nm", "dataanalyst"));
            bll.vBindingComboToSp(ref cbfldname, "sp_tfield_value_get", "fld_valu","fld_desc", arr);
            cbfldname_SelectedIndexChanged(sender, e);
        }
    }
    protected void cbfldname_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@fld_nm", cbfldname.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tfield_value_getbyquery", arr);
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@fld_nm", cbfldname.SelectedValue.ToString()));
        arr.Add(new cArrayList("@fld_valu", txvalue.Text));
        arr.Add(new cArrayList("@fld_desc", txdesc.Text));
        arr.Add(new cArrayList("@hiddendata", cbhidden.SelectedValue.ToString()));
        arr.Add(new cArrayList("@fld_arabic", txarabic.Text));
        arr.Add(new cArrayList("@isactive", cbactive.SelectedValue.ToString()));
        arr.Add(new cArrayList("@orderby", txorder.Text));
        bll.vInsertFieldValue(arr);
        arr.Clear();
        arr.Add(new cArrayList("@fld_nm", cbfldname.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tfield_value_getbyquery", arr);
       
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbfldvalue = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbfldvalue");
        Label lbdesc = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbdesc");
        Label lborder = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lborder");
        Label lbarabic = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbarabic");
        Label lbhiddendata = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbhiddendata");
        Label lbdeleted = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbdeleted");
        //DropDownList cbactive = (DropDownList)grd.Rows[e.NewSelectedIndex].FindControl("cbactive");
        txvalue.Text = lbfldvalue.Text;
        txdesc.Text = lbdesc.Text;
        txorder.Text = lborder.Text;
        txarabic.Text = lbarabic.Text;
        cbhidden.SelectedValue = ((lbhiddendata.Text == "false"?"0":"1"));
        cbactive.SelectedValue = (lbdeleted.Text=="false"?"0":"1");
        //txhidden.CssClass = "form-control ro";
        //txvalue.CssClass = "form-control ro";
        //txorder.CssClass = "form-control ro";
        //txdesc.CssClass = "form-control ro";
        //txarabic.CssClass = "form-control ro";
        //cbactive.CssClass = "form-control ro";
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@fld_nm", cbfldname.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tfield_value_getbyquery", arr);
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@fld_nm", cbfldname.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tfield_value_getbyquery", arr);
    }
}