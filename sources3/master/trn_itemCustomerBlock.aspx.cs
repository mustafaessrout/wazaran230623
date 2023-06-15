using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using AppClassTools;
using System.Net.NetworkInformation;

public partial class trn_itemCustomerBlock : System.Web.UI.Page
{
    cbll bll = new cbll();
    Utitlity ut = new Utitlity()
        ;
    cdal cdl = new cdal();
    AppClass app = new AppClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindControl();
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@blockID", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            dtstart.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
            bll.vDeleteItemCustomer_Block(arr);
            ddlProduct2_SelectedIndexChanged(sender, e);
        }
    }
    private void bindControl()
    {
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
        bll.vBindingFieldValueToCombo(ref ddlCustomer, "otlcd");
        arr.Add(new cArrayList("@level_no", 2));
        arr.Add(new cArrayList("@prod_cd_parent", null));
        bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
        bll.vBindingComboToSp(ref ddlProduct2, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);

        BindGrid();
    }
    protected void ddlProduct2_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@level_no", 3));
        arr.Add(new cArrayList("@prod_cd_parent", ddlProduct2.SelectedValue));
        bll.vBindingComboToSp(ref ddlProduct, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
        ddlProduct_SelectedIndexChanged(sender, e);
        BindGrid();
    }
    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@blockID", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));



        bll.vDeleteItemCustomer_Block(arr);

        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Clear();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        bll.vGetMstSalespoint(arr, ref rs);
        while (rs.Read())
        {
            //lblBranchName.Text = rs["comp_cd"].ToString();
            Ping pg = new Ping();
            PingReply reply = pg.Send(rs["comp_cd"].ToString());
            bool status = reply.Status == IPStatus.Success;
            if (status == false)
            {
                lblHOStat.Text = "disconnected";
                dvHOStatusValue.Style.Add("box-shadow", "inset 0 0 5px rgba(200, 236, 214, 0.71)");
                dvHOStatusValue.Style.Add("background", "radial-gradient(#e74c3c 80%,#a7f1c6)");
                hdfHOConnected.Value = "false";
            }
            else
            {
                lblHOStat.Text = "connected";
                dvHOStatusValue.Style.Add("box-shadow", "inset 0 0 5px rgba(200, 236, 214, 0.71)");
                dvHOStatusValue.Style.Add("background", "radial-gradient(#2ecc71 80%,#a7f1c6)");
                hdfHOConnected.Value = "true";
            }
        }
        rs.Close();
        BindGrid();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DateTime dtst = DateTime.ParseExact(dtstart.Text.ToString(), "dd/MM/yyyy", null);
        DateTime dtend = DateTime.ParseExact(dtEnd.Text.ToString(), "dd/MM/yyyy", null);
        if (dtend <= dtst)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt1", "sweetAlert('End date should greater than start date','Wrong End date','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@blockID", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@parentProd_cd", ddlProduct2.SelectedValue.ToString()));
        arr.Add(new cArrayList("@prod_cd", ddlProduct.SelectedValue.ToString()));
        arr.Add(new cArrayList("@start_dt", System.DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@end_dt", System.DateTime.ParseExact(dtEnd.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@otlcd", ddlCustomer.SelectedValue.ToString()));

        bll.vInsertItemCustomer_Block(arr);
        BindGrid();

    }
    protected void btSearchHO_Click(object sender, EventArgs e)
    {
        try
        {
            System.Data.SqlClient.SqlDataReader rs = null;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            bll.vGetMstSalespoint(arr, ref rs);
            while (rs.Read())
            {
                Ping pg = new Ping();
                PingReply reply = pg.Send(rs["comp_cd"].ToString());
                bool status = reply.Status == IPStatus.Success;
                if (status == false)
                {
                    dvHOStatusValue.Style.Add("background-color", "red");
                    hdfHOConnected.Value = "false";
                }
                else
                {
                    dvHOStatusValue.Style.Add("background-color", "green");
                    hdfHOConnected.Value = "true";
                }
            }
            rs.Close();
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Appraisal", "Target Driver", "fm_targetdriver", "Page_Load", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    private void BindGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@blockID", null));
        arr.Add(new cArrayList("@prod_cd", ddlProduct.SelectedValue.ToString()));
        arr.Add(new cArrayList("@parentProd_cd", ddlProduct2.SelectedValue.ToString()));
        dt = cdl.GetValueFromSP("sp_tmst_itemCustomer_Block_get", arr);
        arr.Clear();
        if (dt.Rows.Count == 0)
        {
            grdHO.DataSource = null;
            grdHO.DataBind();
        }
        else
        {
            grdHO.DataSource = dt;
            grdHO.DataBind();

        }

    }

    protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Deactive1")
        {
            int salespointcd = Convert.ToInt32(e.CommandArgument);
            //HttpClient client = new HttpClient();
            //Task<string> str = client.GetStringAsync("http://172.16.3.5:8081/api/values/101/9/3091/201901");
            //var response = await client.GetAsync(new Uri("http://172.16.3.5:8081/api/values/101/9/3091/201901"));
            ////HttpResponseMessage response =  client.GetStringAsync("http://172.16.3.5:8081/api/values/101/9/3091/201901");


            ////response.EnsureSuccessStatusCode();
            //var httpClient = new HttpClient();
            //var response = await client.GetAsync(new Uri("http://172.16.3.5:8081/api/values/101/9/3091/201901"));
            //string responseString = await response.Content.ReadAsStringAsync();

            //Task<string> str = GetTextByGet("http://172.16.3.5:8081/api/values/101/9/3091/201901");
            //Test();

            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:58295/fetch_information");
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //string postBody = JsonConvert.SerializeObject("");
            //HttpContent content = new StringContent(postBody, Encoding.UTF8, "application/json");
            //HttpResponseMessage response = client.PostAsync("http://172.16.3.5:8081/api/values/101/9/3091/201901", content).Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    // Read the response body as string
            //    string json = response.Content.ReadAsStringAsync().Result;

            //    // deserialize the JSON response returned from the Web API back to a login_info object
            //    //return JsonConvert.DeserializeObject<login_info>(json);
            //}



            var t = Task.Run(() => GetURI(new Uri("http://172.16.3.5:8081/api/values/101/9/3091/201901")));
            System.Threading.Thread.Sleep(5000);
            var objResponse1 = JsonConvert.DeserializeObject<List<serviceResponse>>(t.Result);
            if (objResponse1[0].isSyncSuccess == "true")
            {
                int columnIndex = 7;
                GridViewRow row = grd.SelectedRow;

                // And you respective cell's value
                //row.Cells[columnIndex].Text = "UpdateValue";
                //TextBox1.Text = row.Cells[1].Text


                foreach (GridViewRow row1 in grd.Rows)
                {
                    if (row1.Cells[0].Text == Convert.ToString(e.CommandArgument))
                    {
                        row1.Cells[columnIndex].Text = "UpdateValue";
                    }
                }

            }
            //serviceResponse obj = JsonConvert.DeserializeObject<serviceResponse>(t.Result);
            //JObject json = JObject.Parse(t.Result);
            //dynamic results = JsonConvert.DeserializeObject<dynamic>(t.Result);
            //var id = results.Id;
            //var name = results.Name;
            //var brn = results.BranchName;
            //var position = t.Result;
            //t.Wait();

            //var task = Task.Factory.StartNew(async () => await GetURI(new Uri("http://172.16.3.5:8081/api/values/101/9/3091/201901")));
            ////task.Wait();
            //System.Threading.Thread.Sleep(5000);
            //var position = task.Result;


            //Uri u = new Uri("http://172.16.3.5:8081/api/values/");
            //var payload1 = "{\"branchID\": 101,\"syncID\": 9,\"createBy\": 3091\"}";
            //var payload = "{\"branchID\": 101,\"syncID\": 9,\"createBy\": 3091\"}";
            //HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
            //var t = Task.Run(() => PostURI(u, c));
            //t.Wait();


        }
        else if (e.CommandName != "Active1")
        {
            int salespointcd = Convert.ToInt32(e.CommandArgument);
        }
    }

    public class serviceResponse
    {
        public string BranchName { get; set; }
        public string SyncName { get; set; }
        public string isSyncSuccess { get; set; }
        public string error { get; set; }
    }

    public class RootObject
    {
        public List<serviceResponse> lstServiceResponse { get; set; }
    }
    static async Task<string> PostURI(Uri u, HttpContent c)
    {
        var response = string.Empty;
        using (var client = new HttpClient())
        {
            HttpResponseMessage result = await client.PostAsync(u, c);
            if (result.IsSuccessStatusCode)
            {
                response = result.StatusCode.ToString();
            }
        }
        return response;
    }
    static async Task<string> GetURI(Uri u)
    {
        var response = string.Empty;
        using (var client = new HttpClient())
        {
            HttpResponseMessage result = await client.GetAsync(u);
            if (result.IsSuccessStatusCode)
            {
                response = await result.Content.ReadAsStringAsync();
            }
        }
        return response;
    }
    static async void Test()
    {
        var r = await DownloadPage("http://172.16.3.5:8081/api/values/101/9/3091/201901");
        Console.WriteLine(r.Substring(0, 50));
    }

    static async Task<string> DownloadPage(string url)
    {
        using (var client = new HttpClient())
        {
            using (var r = await client.GetAsync(new Uri(url)))
            {
                string result = await r.Content.ReadAsStringAsync();
                return result;
            }
        }
    }

    public static async Task<string> GetTextByGet(string posturi)
    {

        try
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(new Uri(posturi));
            string responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        string salespointcd = Request.Cookies["sp"].Value;
        string syncDBID = "10";
        string createDBBy = Request.Cookies["usr_id"].Value;
        Label lblsalespointcd = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblsalespointcd");
        HiddenField hdfcomp_cd = (HiddenField)grd.Rows[e.NewSelectedIndex].FindControl("hdfcomp_cd");
        hdfcomp_cd.Value = "http://localhost:3796/";
        string url = hdfcomp_cd.Value + "/api/values/" + lblsalespointcd.Text + "/" + syncDBID + "/" + createDBBy;
        //var t = Task.Run(() => GetURI(new Uri("http://"+ hdfcomp_cd.Value + "/api/values/"+ salespointcd + "/"+syncDBID+"/"+createDBBy+"/1/1/")));
        var t = Task.Run(() => GetURI(new Uri(url)));
        System.Threading.Thread.Sleep(5000);
        var objResponse1 = JsonConvert.DeserializeObject<List<serviceResponse>>(t.Result);
        if (objResponse1[0].isSyncSuccess == "true")
        {
            Label lblDeactive1 = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblDeactive1");
            lblDeactive1.Text = "UpdateValue";

        }

    }


    protected void btUpdateHO_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@prod_Cd", ddlProduct.SelectedValue));
        arr.Add(new cArrayList("@otlcd", ddlCustomer.SelectedValue));
        string totCount = string.Empty;
        //bll.vInsItemCustomerBlock(arr);
    }

    protected void grdHO_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }

    protected void grdHO_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            HiddenField hdfblockID = (HiddenField)grdHO.Rows[e.RowIndex].FindControl("hdfblockID");
            HiddenField hdfSalespointcd = (HiddenField)grdHO.Rows[e.RowIndex].FindControl("hdfSalespointcd");
            HiddenField hdfserialNumber = (HiddenField)grdHO.Rows[e.RowIndex].FindControl("hdfserialNumber");
            HiddenField hdfcomp_cd = (HiddenField)grdHO.Rows[e.RowIndex].FindControl("hdfcomp_cd");

            List<cArrayList> arr = new List<cArrayList>();
            DataTable dt = new DataTable();
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            arr.Add(new cArrayList("@blockID", hdfblockID.Value));
            dt = cdl.GetValueFromSP("sp_tmst_itemCustomer_Block_get", arr);

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToString(dt.Rows[0]["branchUpdateStatus"]) == "Yes")
                {
                    string urlService = string.Empty;
                    urlService = "http://" + hdfcomp_cd.Value.ToString() + ":8081" + "/";
                    //urlService = "http://localhost:3796/";
                    string url = urlService + "/api/values/" + cbsalespoint.SelectedValue + "/" + "11" + "/" + hdfserialNumber.Value.ToString();
                    ;
                    var t = Task.Run(() => GetURI(new Uri(url)));
                    System.Threading.Thread.Sleep(5000);
                    var objResponse1 = JsonConvert.DeserializeObject<List<serviceResponse>>(t.Result);
                    if (objResponse1[0].isSyncSuccess == "true")
                    {
                        arr.Clear();
                        arr.Add(new cArrayList("@blockID", hdfblockID.Value));
                        bll.vDeleteItemCustomer_Block_hist(arr);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Data deleted succesfully!','Succesfully','success');", true);
                        BindGrid();
                    }
                }
                else
                {
                    arr.Clear();
                    arr.Add(new cArrayList("@blockID", hdfblockID.Value));
                    arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue));
                    bll.vDeleteItemCustomer_Block(arr);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Data deleted succesfully!','Succesfully','success');", true);
                    BindGrid();
                }
            }
            else
            {
                arr.Clear();
                arr.Add(new cArrayList("@blockID", hdfblockID.Value));
                arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue));
                bll.vDeleteItemCustomer_Block(arr);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Data deleted succesfully!','Succesfully','success');", true);
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt1", "sweetAlert('Error Delete','Error Delete','warning');", true);
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : Item Delete Customer Block");
        }
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        string urlIP = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@blockID", null));
        dt = cdl.GetValueFromSP("sp_tmst_itemCustomer_Block_get", arr);
        try
        {
            if (hdfHOConnected.Value == "true")
            {
               

                foreach (DataRow dr in dt.Rows)
                {
                    urlIP = Convert.ToString(dr["comp_cd"]);
                    arr.Clear();
                    arr.Add(new cArrayList("@blockID", Request.Cookies["usr_id"].Value.ToString()));
                    arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@parentProd_cd",dr["parentProd_cd"].ToString()));
                    arr.Add(new cArrayList("@prod_cd", dr["prod_cd"].ToString()));
                    arr.Add(new cArrayList("@otlcd", dr["otlcd"].ToString()));

                    bll.vUpdateItemCustomer_Block(arr);


                }
                string urlService = string.Empty;
                urlService = "http://" + urlIP + ":8081" +"/";
                //urlService = "http://localhost:3796/";
                string url = urlService + "/api/values/" + cbsalespoint.SelectedValue + "/" + "10" + "/" + Request.Cookies["usr_id"].Value.ToString();
                var t = Task.Run(() => GetURI(new Uri(url)));
                System.Threading.Thread.Sleep(5000);
                var objResponse1 = JsonConvert.DeserializeObject<List<serviceResponse>>(t.Result);
                if (objResponse1[0].isSyncSuccess == "true")
                {
                    bll.vLookUp("update tmst_itemCustomer_Block set isBranchUpdate=1 where createdBy='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + cbsalespoint.SelectedValue + "'");
                    BindGrid();
                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Data saved succesfully!','Succesfully','success');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt1", "sweetAlert('Branch Not connected','Branch Not connected','warning');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt1", "sweetAlert('Error During Sync','Error During Sync','warning');", true);
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : Add Item Customer Block");
            foreach (DataRow dr in dt.Rows)
            {
                arr.Clear();
                arr.Add(new cArrayList("@user", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@blockID", dr["blockID"].ToString()));
                arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
                arr.Add(new cArrayList("@parentProd_cd", ddlProduct2.SelectedValue.ToString()));
                arr.Add(new cArrayList("@prod_cd", ddlProduct.SelectedValue.ToString()));
                arr.Add(new cArrayList("@otlcd", ddlCustomer.SelectedValue.ToString()));

                bll.vUpdateItemCustomer_Block_back(arr);
            }
        }
    }

    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
}