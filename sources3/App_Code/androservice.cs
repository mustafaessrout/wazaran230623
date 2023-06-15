using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "androservice" in code, svc and config file together.
public class androservice : Iandroservice
{
    cbll bll = new cbll();
	public void DoWork()
	{
        string sTemp = "Preketek";
	}
    public string ssBack()
    {
        return ("similikit");
    }

    public List<tmst_van_stock> lmstvanstock(string sSalespoint)
    {
        SqlDataReader rs = null;
        List<tmst_van_stock> lvanstock = new List<tmst_van_stock>();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", sSalespoint));
        try
        {
            bll.vTabletMstVanStock(ref rs, arr);
            while (rs.Read())
            {
                lvanstock.Add(new tmst_van_stock(rs["item_cd"].ToString(), rs["item_nm"].ToString(), rs["size"].ToString(), rs["branded_nm"].ToString(), rs["item_arabic"].ToString(), rs["vhc_cd"].ToString(), rs["bin_cd"].ToString(), Convert.ToDouble(rs["stock_amt"]), Convert.ToDouble(rs["stock_display"])));
            } rs.Close();
        }
        catch (Exception ex)
        {
            arr.Clear();
            arr.Add(new cArrayList("@err_source", "lmvanstock"));
            arr.Add(new cArrayList("@err_description", ex.Message.ToString()));
            bll.vInsertErrorLog(arr);
        }
        return (lvanstock);
    }
    public List<tmst_stock> lmststock(string sSalespoint)
    {
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        List<tmst_stock> lstock = new List<tmst_stock>();
        try
        {
            arr.Add(new cArrayList("@salespointcd", sSalespoint));
            bll.vGetTabletMstStock(arr, ref rs);
            while (rs.Read())
            {
              lstock.Add(new tmst_stock(rs["item_cd"].ToString(), rs["item_nm"].ToString(), rs["item_arabic"].ToString(), rs["size"].ToString(), Convert.ToDouble(rs["stock_amt"]), Convert.ToDouble(rs["stock_display"]), rs["whs_cd"].ToString(), rs["bin_cd"].ToString(), rs["branded_nm"].ToString()));
            } rs.Close();
        }
        catch (Exception ex)
        {
            arr.Clear();
            arr.Add(new cArrayList("@err_source", "lmststock"));
            arr.Add(new cArrayList("@err_description", ex.Message.ToString()));
            bll.vInsertErrorLog(arr);
        }
        return (lstock);
    }
    public List<tmst_customer> lmstcustomer(string sSalespoint)
    {
        SqlDataReader rs = null;
        List<tmst_customer> lmstcust = new List<tmst_customer>();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", sSalespoint));
        bll.vGetMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            lmstcust.Add(new tmst_customer(rs["cust_cd"].ToString(), rs["cust_nm"].ToString(), rs["addr"].ToString(), rs["city_cd"].ToString(), rs["cusgrcd"].ToString(), rs["otlcd"].ToString(), Convert.ToDouble(rs["credit_limit"]), rs["salesman_cd"].ToString()));
        }
        rs.Close(); return (lmstcust);
    }
    public List<tcustomertype_price> lcustomertypeprice()
    {
        
        SqlDataReader rs = null;
        List<tcustomertype_price> lcusttype = new List<tcustomertype_price>();
        bll.vGetCustomerTypePrice(ref rs);
        while (rs.Read())
        { 
            lcusttype.Add(new tcustomertype_price(rs["cust_typ"].ToString(),rs["item_cd"].ToString(),Convert.ToDouble( rs["unitprice"])));
        } 
        rs.Close();
        return (lcusttype);
    }
    public List<tuser_profiles> lup(string sEmpCode)
    {
        SqlDataReader rs = null;
        List<tuser_profiles> tp = new List<tuser_profiles>();
        bll.vTabletUserProfile(ref rs);
        while (rs.Read())
        {
            tp.Add(new tuser_profiles(rs["usr_id"].ToString(), rs["passwd"].ToString(), rs["email"].ToString()));
        }rs.Close();
        return (tp);
        
    }

    public List<tmst_product> lmstproduct()
    {
        List<tmst_product> lp = new List<tmst_product>();
        SqlDataReader rs = null;
        bll.vTabletMstProduct(ref rs);
        while (rs.Read())
        { 
            lp.Add(new tmst_product(rs["prod_cd"].ToString(),rs["prod_nm"].ToString()));
        } rs.Close();
        return (lp);
    }

    public List<tmst_item> lmstitem()
    {
        SqlDataReader rs=null;
        List<tmst_item> litem = new List<tmst_item>();
        List<cArrayList> arr = new List<cArrayList>();
        bll.vTabletMstItem(ref rs);
        while (rs.Read())
        {
             litem.Add(new tmst_item(rs["item_cd"].ToString(), rs["item_nm"].ToString(),rs["item_arabic"].ToString(),rs["size"].ToString(), rs["branded_nm"].ToString() )); 
        }rs.Close();
        return (litem);
    }
    public string vInsertCanvasOrder(ttablet_canvasorder lcanvas)
    {
        List<cArrayList> arr = new List<cArrayList>();
        try
        {
            arr.Add(new cArrayList("@so_cd", lcanvas.so_cd));
            arr.Add(new cArrayList("@so_dt", lcanvas.so_dt));
            arr.Add(new cArrayList("@salesman_cd", lcanvas.salesman_cd));
            arr.Add(new cArrayList("@uom", lcanvas.uom));
            arr.Add(new cArrayList("@qty", lcanvas.qty));
            arr.Add(new cArrayList("@item_cd", lcanvas.item_cd));
            bll.vInsertTtableCanvasorder(arr);
        }
        catch (Exception ex)
        {
            arr.Clear();
            arr.Add(new cArrayList("@err_source", "vinserttabletcanvas"));
            arr.Add(new cArrayList("@err_description", ex.Message.ToString()));
            bll.vInsertErrorLog(arr);
        }
            return ("Data Saved");
        
    }
}
