using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_claimtrack : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    vInitTree();
        //}
    }
    void vInitTree()
    {
        trclaimtrack.CollapseImageUrl = "../image/add.PNG";
        trclaimtrack.ExpandImageUrl = "../image/minus.PNG";
        trclaimtrack.ShowLines = false;
    
        trclaimtrack.Nodes.Clear();
        List<cArrayList> arr = new List<cArrayList>();
        SqlDataReader rs = null; SqlDataReader rs2 = null; SqlDataReader rs3 = null;
        arr.Add(new cArrayList("@type", "PROP"));
        arr.Add(new cArrayList("@prop", hdprop.Value.ToString()));
        bll.vGetClaimtracking(arr, ref rs);
        while (rs.Read())
        {
            TreeNode tchild1 = new TreeNode();
            tchild1.Text = rs["prop_no"].ToString();
            tchild1.Value = rs["prop_no"].ToString();
            arr.Clear();
            arr.Add(new cArrayList("@type", "CLAIM"));
            arr.Add(new cArrayList("@PROP", rs["PROP_NO"].ToString()));
            bll.vGetClaimtracking(arr, ref rs2);
            while (rs2.Read())
            {
                TreeNode tchild2 = new TreeNode();
                tchild2.Text = rs2["DESCRIPTION"].ToString();
                tchild2.Value = rs2["claim_no"].ToString();
                arr.Clear();
                arr.Add(new cArrayList("@type", "TRACK"));
                arr.Add(new cArrayList("@CLAIM", rs2["CLAIM_NO"].ToString()));
                bll.vGetClaimtracking(arr, ref rs3);
                while (rs3.Read())
                {
                    TreeNode tchild3 = new TreeNode();
                    tchild3.Value = rs3["claim_no"].ToString();
                    tchild3.Text = rs3["DESCRIPTION"].ToString() + "- " + Convert.ToDateTime(rs3["date"].ToString()).Date.ToShortDateString();
                    tchild2.ChildNodes.Add(tchild3);
                } rs3.Close();
                tchild1.ChildNodes.Add(tchild2);
            } rs2.Close();
            trclaimtrack.Nodes.Add(tchild1);
        }
        rs.Close();
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lprop = new List<string>();
        string sProp = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        // arr.Add(new cArrayList("@salesman_cd", contextKey));
        // arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@prop_no", prefixText));
        bll.vGetProposalTRACK(arr, ref rs);
        while (rs.Read())
        {
            sProp = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["prop_no"].ToString() + "-" + rs["remark"].ToString(), rs["prop_no"].ToString());
            lprop.Add(sProp);
        }
        rs.Close();
        return (lprop.ToArray());
    }
    protected void btcheck_Click(object sender, EventArgs e)
    {
        vInitTree();
    }
}