using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
public partial class fm_mstloc : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@loc_cd"))
            vInitTree();
        //    bll.vBindingFieldValueToCombo(ref cbloctype, "loc_typ");
            bll.vBindingFieldValueToCombo(ref cblevelno, "level_no");
            bll.vBindingComboToSp(ref cbparent, "sp_tmst_location_get2","loc_cd","loc_nm");
            MakeReadOnly();
            //txlocationcode.CssClass = "makeitreadonly";
            //txlocationcode.ReadOnly = true;
            //txarabic.CssClass = "makeitreadonly";
            //txarabic.ReadOnly = true;
            //txlocationname.CssClass = "makeitreadonly";
            //txlocationname.ReadOnly = true;
            //cblevelno.CssClass = "makeitreadonly";
            //cblevelno.Enabled = false;
            //cbparent.CssClass = "makeitreadonly";
            //cbparent.Enabled = false;
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }

    void vInitTree()
    {
        trv.Nodes.Clear();
        SqlDataReader rs = null; SqlDataReader rs2 = null; SqlDataReader rs3 = null;
        SqlDataReader rs4 = null; SqlDataReader rs5 = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@level_no",1));
        bll.vGetMstLocation(ref rs, arr);
        while (rs.Read())
        {
            TreeNode tnode = new TreeNode();
            tnode.Value = rs["loc_cd"].ToString();
            tnode.Text = rs["loc_nm"].ToString();
            arr.Clear();
            arr.Add(new cArrayList("@level_no", 2));
            arr.Add(new cArrayList("@loc_cd_parent", rs["loc_cd"].ToString()));
            bll.vGetMstLocation(ref rs2, arr);
            while (rs2.Read())
            { 
                TreeNode tchild = new TreeNode();
                tchild.Value = rs2["loc_cd"].ToString();
                tchild.Text = rs2["loc_nm"].ToString();
                arr.Clear();
                arr.Add(new cArrayList("@level_no", 3));
                arr.Add(new cArrayList("@loc_cd_parent", rs2["loc_cd"].ToString()));
                bll.vGetMstLocation(ref rs3, arr);
                while (rs3.Read())
                {
                    TreeNode tchild2 = new TreeNode();
                    tchild2.Value = rs3["loc_cd"].ToString();
                    tchild2.Text = rs3["loc_nm"].ToString();
                    arr.Clear();
                    arr.Add(new cArrayList("@level_no", 4));
                    arr.Add(new cArrayList("@loc_cd_parent", rs3["loc_cd"].ToString()));
                    bll.vGetMstLocation(ref rs4, arr);
                    while (rs4.Read())
                    {
                        TreeNode tchild3 = new TreeNode();
                        tchild3.Value = rs4["loc_cd"].ToString();
                        tchild3.Text = rs4["loc_nm"].ToString();
                        arr.Clear();
                        arr.Add(new cArrayList("@level_no", 5));
                        arr.Add(new cArrayList("@loc_cd_parent",rs4["loc_cd"].ToString()));
                        bll.vGetMstLocation(ref rs5, arr);
                        while (rs5.Read())
                        {
                            TreeNode tchild4 = new TreeNode();
                            tchild4.Value = rs5["loc_cd"].ToString();
                            tchild4.Text = rs5["loc_nm"].ToString();
                            tchild3.ChildNodes.Add(tchild4);
                        } rs5.Close();
                        tchild2.ChildNodes.Add(tchild3);
                    } rs4.Close();
                    tchild.ChildNodes.Add(tchild2);
                } rs3.Close();

                tnode.ChildNodes.Add(tchild);
            } rs2.Close();
            trv.Nodes.Add(tnode);
        } rs.Close();
    }
    protected void trv_SelectedNodeChanged(object sender, EventArgs e)
    {
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@loc_cd", trv.SelectedNode.Value.ToString()));
        bll.vGetMstLocation2(ref rs, arr);
        while (rs.Read())
        { 
            txlocationcode.Text = rs["loc_cd"].ToString();
            txlocationname.Text = rs["loc_nm"].ToString();
            //cbloctype.SelectedValue = rs["loc_typ"].ToString();
            cblevelno.SelectedValue = rs["level_no"].ToString();
            cbparent.SelectedValue = rs["loc_cd_parent"].ToString();
            txarabic.Text = rs["loc_arabic"].ToString();
        }
        rs.Close();
        MakeReadOnly();
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        MakeReadWrite();
        txlocationcode.Text = "";
        txarabic.Text = "";
        txlocationname.Text = "";
        txlocationcode.Focus();
    }

    void MakeReadWrite()
    {
        txlocationcode.CssClass = "form-control";
        txlocationcode.Enabled = true;
        txarabic.CssClass = "form-control";
        txarabic.Enabled = true;
        txlocationname.CssClass = "form-control";
        txlocationname.Enabled = true;
        cblevelno.CssClass = "form-control";
        cblevelno.Enabled = true;
        cbparent.CssClass = "form-control";
        cbparent.Enabled = true;
   
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@loc_cd", txlocationcode.Text));
        arr.Add(new cArrayList("@loc_nm", txlocationname.Text.ToUpper()));
     //   arr.Add(new cArrayList("@loc_typ", cbloctype.SelectedValue.ToString()));
        arr.Add(new cArrayList("@loc_cd_parent", cbparent.SelectedValue.ToString()));
        arr.Add(new cArrayList("@level_no", cblevelno.SelectedValue.ToString()));
        arr.Add(new cArrayList("@isactive", "1"));
        arr.Add(new cArrayList("@loc_arabic", txarabic.Text));
        bll.vInsertMstLocation(arr);
        vInitTree();
        trv.ExpandAll();
        MakeReadOnly();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data saved','Data saved successfully !','success');", true);

    }
    protected void btedit_Click(object sender, EventArgs e)
    {
        MakeReadWrite();
        txlocationcode.CssClass = "form-control ro";
        txlocationcode.Enabled = false;
        txlocationname.Focus();
    }

    void MakeReadOnly()
    {
        txlocationcode.CssClass = "form-control ro";
        txlocationcode.Enabled = false;
        txarabic.CssClass = "form-control ro";
        txarabic.Enabled = false;
        txlocationname.CssClass = "form-control ro";
        txlocationname.Enabled = false;
        cblevelno.CssClass = "form-control ro";
        cblevelno.Enabled = false;
        cbparent.CssClass = "form-control ro";
        cbparent.Enabled = false;
    }
}