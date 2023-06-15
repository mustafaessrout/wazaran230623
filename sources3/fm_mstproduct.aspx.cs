using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
public partial class fm_mstproduct : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@qry_cd", "productspv"));
                bll.vBindingFieldValueToCombo(ref cblevel, "prod_level_no");
                bll.vBindingComboToSp(ref cbparent, "sp_tmst_product_get", "prod_cd", "prod_nm");
                bll.vBindingComboToSp(ref cbsupervisor, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
                vInitTree();
                MakeReadOnly();

            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstproduct");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }

    void MakeReadOnly()
    {
        cbparent.CssClass = "makeitreadonly";
        cblevel.CssClass = "makeitreadonly";
        txarabic.CssClass = "makeitreadonly";
        txprodname.CssClass = "makeitreadonly";
        txprodcode.CssClass = "makeitreadonly";
        cblevel.Enabled = false;
        cbparent.Enabled = false;
        txarabic.ReadOnly = true;
        txprodcode.ReadOnly = true;
        txprodname.ReadOnly = true;
        cbsupervisor.Enabled = false;
    }

    void MakeReadWrite()
    {
        cbparent.CssClass = "makeitreadwrite";
        cblevel.CssClass = "makeitreadwrite";
        txarabic.CssClass = "makeitreadwrite";
        txprodname.CssClass = "makeitreadwrite";
        txprodcode.CssClass = "makeitreadonly";
        cblevel.Enabled = true;
        cbparent.Enabled = true;
        txarabic.ReadOnly = false;
        txprodcode.ReadOnly = false;
        txprodname.ReadOnly = false;
        cbsupervisor.Enabled = true;
    }
    void vInitTree()
    {
        try
        {
            trv.Nodes.Clear();
            List<cArrayList> arr = new List<cArrayList>();
            SqlDataReader rs = null; SqlDataReader rs2 = null; SqlDataReader rs3 = null;
            arr.Add(new cArrayList("@level_no", 1));
            bll.vGetMstProduct(arr, ref rs);
            while (rs.Read())
            {
                TreeNode tchild1 = new TreeNode();
                tchild1.Text = rs["prod_nm"].ToString();
                tchild1.Value = rs["prod_cd"].ToString();
                arr.Clear();
                arr.Add(new cArrayList("@level_no", 2));
                arr.Add(new cArrayList("@prod_cd_parent", rs["prod_cd"].ToString()));
                bll.vGetMstProduct(arr, ref rs2);
                while (rs2.Read())
                {
                    TreeNode tchild2 = new TreeNode();
                    tchild2.Text = rs2["prod_cd"].ToString() + "-" + rs2["prod_nm"].ToString();
                    tchild2.Value = rs2["prod_cd"].ToString();
                    arr.Clear();
                    arr.Add(new cArrayList("@level_no", 3));
                    arr.Add(new cArrayList("@prod_cd_parent", rs2["prod_cd"].ToString()));
                    bll.vGetMstProduct(arr, ref rs3);
                    while (rs3.Read())
                    {
                        TreeNode tchild3 = new TreeNode();
                        tchild3.Value = rs3["prod_cd"].ToString();
                        tchild3.Text = rs3["prod_cd"].ToString() + "-" + rs3["prod_nm"].ToString();
                        tchild2.ChildNodes.Add(tchild3);
                    }
                    rs3.Close();
                    tchild1.ChildNodes.Add(tchild2);
                }
                rs2.Close();
                trv.Nodes.Add(tchild1);
            }
            rs.Close();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstproduct");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    
    }
    protected void trv_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            SqlDataReader rs = null;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@prod_cd", trv.SelectedNode.Value.ToString()));
            bll.vGetMstProduct2(arr, ref rs);
            while (rs.Read())
            {
                cblevel.SelectedValue = rs["level_no"].ToString();
                if (!rs["prod_cd_parent"].Equals(DBNull.Value))
                {
                    cbparent.SelectedValue = rs["prod_cd_parent"].ToString();
                }
                txprodcode.Text = rs["prod_cd"].ToString();
                txprodname.Text = rs["prod_nm"].ToString();
                txarabic.Text = rs["prod_arabic"].ToString();
                if (rs["supervisor_cd"] != DBNull.Value)
                {
                    cbsupervisor.SelectedValue = rs["supervisor_cd"].ToString();
                }
            }
            rs.Close(); MakeReadOnly();
            //lbprod.Text =  trv.SelectedNode.Text;

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstproduct");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btedit_Click(object sender, EventArgs e)
    {
        MakeReadWrite();
        txprodcode.ReadOnly = true;
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        MakeReadWrite();
        txprodcode.Text = "";
        txprodcode.Focus();
        txprodname.Text = "";
        txarabic.Text = "";
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@prod_cd", txprodcode.Text));
            arr.Add(new cArrayList("@prod_nm", txprodname.Text));
            arr.Add(new cArrayList("@prod_arabic", txarabic.Text));
            arr.Add(new cArrayList("@level_no", cblevel.SelectedValue.ToString()));
            arr.Add(new cArrayList("@prod_cd_parent", cbparent.SelectedValue.ToString()));
            arr.Add(new cArrayList("@supervisor_cd", cbsupervisor.SelectedValue.ToString()));
            bll.vInsertMstProduct(arr);
            vInitTree();
            MakeReadOnly();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data saved successfully !','OK','success')", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstproduct");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btdelete_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@prod_cd", trv.SelectedNode.Value.ToString()));
            bll.vDelMstProduct(arr);
            vInitTree();
            txarabic.Text = "";
            txprodcode.Text = "";
            txprodname.Text = "";
            MakeReadOnly();
            trv.ExpandAll();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('Deleted successfully !')", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstproduct");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}