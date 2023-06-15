using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class invoicedoc : System.Web.UI.Page
{
    cbll bll = new cbll();
    string invoiceFolder = "invoice_scan";


    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        { 
            
        }
    }
    protected void btscan_ServerClick(object sender, EventArgs e)
    {
        //string filePath, fileName;
        //if (upl.PostedFile != null)
        //{
        //    filePath = upl.PostedFile.FileName; // file name with path.
        //    //fileName = upl.FileName;// Only file name.
        //    lbfileloc.Text = filePath.ToString();
        //    string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
        //    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Uploads/") + fileName);
        //    Response.Redirect(Request.Url.AbsoluteUri);
        //}
        string sPathFr = bll.sGetControlParameter("image_path") + "/invoice_scan/";
        string sPathTo = bll.sGetControlParameter("image_path") + "/invoice_doc/";
        string[] filePaths = Directory.GetFiles(sPathFr);
        List<ListItem> files = new List<ListItem>();
        foreach (string filePath in filePaths)
        {
            files.Add(new ListItem(Path.GetFileName(filePath), filePath));
        }
        grdinvoice.DataSource = files;
        grdinvoice.DataBind();
        lbtotalfile.Text = grdinvoice.Rows.Count.ToString() + " Files.";
    }

    protected void DownloadFile(object sender, EventArgs e)
    {
        string sPathFr = bll.sGetControlParameter("image_path") + "/invoice_scan/";
        string filePath = (sender as LinkButton).CommandArgument;

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('"+filePath.ToString()+"','Invoice No','warning');", true);
        return;


        //Response.ContentType = ContentType;
        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        //Response.WriteFile(Path.GetFileName(filePath));
        //Response.End();
    }

    protected void DeleteFile(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        File.Delete(filePath);
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btupload_ServerClick(object sender, EventArgs e)
    {
        try
        {
            DateTime dtpayp1 = DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy",System.Globalization.CultureInfo.InvariantCulture);
            string statusInvoice = "", statusFile = "", stInvoice = "";
            string Tranfiles, ProcessedFiles;
            List<cArrayList> arr = new List<cArrayList>();
            string inv_no = ""; string[] inv_no_arr;
            string sPathFr = bll.sGetControlParameter("image_path") + "/invoice_scan/";
            string sPathTo = bll.sGetControlParameter("image_path") + "/invoice_doc/";
            string[] filePaths = Directory.GetFiles(sPathFr);
            List<ListItem> files = new List<ListItem>();
            foreach (string filePath in filePaths)
            {
                inv_no = Path.GetFileName(filePath).ToString();
                inv_no_arr = inv_no.Split('.');
                if (inv_no_arr[0].Length == 13)
                {
                    statusInvoice = bll.vLookUp("select dbo.fn_checkinvoicetypestatus('"+inv_no_arr[0]+"')");
                    
                    if (statusInvoice == "REGULAR"){
                        
                        stInvoice = bll.vLookUp("select inv_sta_id from tdosales_invoice where inv_no='"+inv_no_arr[0]+"'");

                        if (stInvoice == "F" || stInvoice == "L")
                        {
                            File.Delete(filePath);
                        }
                        else
                        {
                            //Tranfiles = Server.MapPath(sPathTo + inv_no_arr[0] + "_O_." + inv_no_arr[1]);
                            //if (File.Exists(sPathTo + inv_no_arr[0] + "_O_." + inv_no_arr[1]))
                            //{
                            //    File.Delete(sPathTo + inv_no_arr[0] + "_O_." + inv_no_arr[1]);
                            //}
                            //File.Move(filePath, Tranfiles);

                            Tranfiles = Server.MapPath(@"~\images\invoice_scan\" + Path.GetFileName(filePath).ToString());
                            if (File.Exists(Server.MapPath(@"~\images\invoice_doc\" + inv_no_arr[0] + "_O_." + inv_no_arr[1])))
                            {
                                File.Delete(Server.MapPath(@"~\images\invoice_doc\" + inv_no_arr[0] + "_O_." + inv_no_arr[1]));
                            }

                            ProcessedFiles = Server.MapPath(@"~\images\invoice_doc\" + inv_no_arr[0] + "_O_." + inv_no_arr[1]);

                            File.Move(Tranfiles, ProcessedFiles);

                            arr.Clear();
                            arr.Add(new cArrayList("@fileinv", inv_no_arr[0] + "_O_." + inv_no_arr[1]));
                            arr.Add(new cArrayList("@inv_no", inv_no_arr[0]));
                            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                            bll.vUploadInvoice(arr);
                            arr.Clear();
                            arr.Add(new cArrayList("@disc_cd", ""));
                            arr.Add(new cArrayList("@isstamp", "1"));
                            arr.Add(new cArrayList("@remark", ""));
                            arr.Add(new cArrayList("@issign", "1"));
                            arr.Add(new cArrayList("@isexclude", "1"));
                            arr.Add(new cArrayList("@inv_no", inv_no_arr[0]));
                            arr.Add(new cArrayList("@received_dt", dtpayp1.Year + "-" + dtpayp1.Month + "-" + dtpayp1.Day));
                            bll.vInsertClaimConfirm(arr);

                            //break;
                        }
                        
                    }
                    else if (statusInvoice == "RETURN")
                    {
                        stInvoice = bll.vLookUp("select retur_sta_id from tsalesreturn where retur_no='" + inv_no_arr[0] + "'");

                        if (stInvoice == "L")
                        {
                            File.Delete(filePath);
                        }
                        else
                        {
                            //Tranfiles = Server.MapPath(sPathTo + inv_no_arr[0] + "_O_." + inv_no_arr[1]);
                            //if (File.Exists(sPathTo + inv_no_arr[0] + "_O_." + inv_no_arr[1]))
                            //{
                            //    File.Delete(sPathTo + inv_no_arr[0] + "_O_." + inv_no_arr[1]);
                            //}
                            //File.Move(filePath, Tranfiles);

                            Tranfiles = Server.MapPath(@"~\images\invoice_scan\" + Path.GetFileName(filePath).ToString());
                            if (File.Exists(Server.MapPath(@"~\images\invoice_doc\" + inv_no_arr[0] + "." + inv_no_arr[1])))
                            {
                                File.Delete(Server.MapPath(@"~\images\invoice_doc\" + inv_no_arr[0] + "." + inv_no_arr[1]));
                            }

                            ProcessedFiles = Server.MapPath(@"~\images\invoice_doc\" + inv_no_arr[0] + "." + inv_no_arr[1]);

                            File.Move(Tranfiles, ProcessedFiles);

                            arr.Clear();
                            arr.Add(new cArrayList("@fileinv", inv_no_arr[0] + "_O_." + inv_no_arr[1]));
                            arr.Add(new cArrayList("@inv_no", inv_no_arr[0]));
                            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                            bll.vUploadInvoice(arr);
                            arr.Clear();
                            arr.Add(new cArrayList("@disc_cd", ""));
                            arr.Add(new cArrayList("@isstamp", "1"));
                            arr.Add(new cArrayList("@remark", ""));
                            arr.Add(new cArrayList("@issign", "1"));
                            arr.Add(new cArrayList("@isexclude", "1"));
                            arr.Add(new cArrayList("@inv_no", inv_no_arr[0]));
                            arr.Add(new cArrayList("@received_dt", dtpayp1.Year + "-" + dtpayp1.Month + "-" + dtpayp1.Day));
                            bll.vInsertClaimConfirm(arr);

                            //break;
                        }

                    }
                    else
                    {
                        statusFile = bll.vLookUp("select inv_no from tclaim_invoice where inv_no='"+inv_no_arr[0]+"'");
                        if (statusFile != "")
                        {
                            File.Delete(filePath);
                        }
                    }
                    statusInvoice = "";
                    statusFile = "";

                }
                //files.Add(new ListItem(Path.GetFileName(filePath), filePath));
            }
            btscan_ServerClick(sender, e);
        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : Upload Invoice");
        }
        
    }
    protected void grdinvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}