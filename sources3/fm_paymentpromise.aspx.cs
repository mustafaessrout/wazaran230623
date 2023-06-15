using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_paymentpromise : System.Web.UI.Page
{
    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            cbapprove.Items.Insert(0, "-- Select Approval --");
            cbapprove.Items.Insert(1, "By Paid");
            cbapprove.Items.Insert(2, "By Promise");
            cbpayement.Items.Insert(0, "Bank Transfer");
            cbpayement.Items.Insert(1, "Cheque");
            bll.vBindingFieldValueToCombo(ref cbbankcq, "bank_cd");
            txCustomer.Attributes.Add("readonly", "readonly");
            cbapprove_SelectedIndexChanged(sender, e);
            grdlist.Visible = false;

        }
    }

    protected void btsearchCust_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "popupwindow('lookcustomer.aspx');", true);
    }

    protected void btcust_click(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        Session["hdcust"] = hdcust.Value.ToString();
        bll.vGetMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            txCustomer.Text = rs["cust_cd"].ToString() + " - " + rs["cust_nm"].ToString();
            lbaddress.Text = rs["addr"].ToString();
            lbcusttype.Text = rs["otlnm"].ToString();
            lbcity.Text = bll.vLookUp("select loc_nm from tmst_location where loc_cd='" + rs["city_cd"].ToString() + "'");
            lbterm.Text = rs["term"].ToString();
            var culture = System.Globalization.CultureInfo.GetCultureInfo("id-ID");
            lbcredit.Text = string.Format(culture, "{0:n}", Convert.ToDouble(rs["credit_limit"]));
            lbclremain.Text = bll.vLookUp("select dbo.fn_getremaincl('" + hdcust.Value.ToString() + "')");
        }
        arr.Clear();
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grdlist, "sp_inv_customer_get", arr);
        grdlist.Visible = true;
    }

    protected void cbapprove_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbapprove.SelectedValue.ToString() == "By Paid")
        {
            tblPaid.Attributes.Remove("style");
            tblPaid.Attributes.Add("style", "background-color:silver");
            tblPromise.Attributes.Remove("style");
            tblPromise.Attributes.Add("style", "display:none");
        }
        else if (cbapprove.SelectedValue.ToString() == "By Promise")
        {
            tblPaid.Attributes.Remove("style");
            tblPaid.Attributes.Add("style", "display:none");
            tblPromise.Attributes.Remove("style");
            tblPromise.Attributes.Add("style", "background-color:silver");
        }
        else
        {
            tblPaid.Attributes.Remove("style");
            tblPaid.Attributes.Add("style", "display:none");
            tblPromise.Attributes.Remove("style");
            tblPromise.Attributes.Add("style", "display:none");
        }           
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_paymentpromise.aspx");
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        if (cbapprove.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Select Approval can not empty','Payment Promise','warning');", true);
            return;
        }

        if (txCustomer.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer can not empty','Payment Promise','warning');", true);
            return;
        }

        if (cbapprove.SelectedIndex != 0)
        {
            if (cbapprove.SelectedValue.ToString() == "By Paid")
            {
                if (txduedate1.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Due Date can not empty','Payment Promise','warning');", true);
                    return;
                }
                if (txNoCheque.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('No Cheque / Bank Transfer can not empty','Payment Promise','warning');", true);
                    return;
                }
                if (uplpaid.FileName == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('No Image uploaded','Please scan the document and upload','warning');", true);
                    return;
                }
                if ((txAmountPaid.Text == "") || (txAmountPaid.Text == "0"))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount can not empty / zero','Payment Promise','warning');", true);
                    return;
                }
            }
            else
            {
                if (txduedate2.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Due Date can not empty','Payment Promise','warning');", true);
                    return;
                }
                if (uplPromise.FileName == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('No Image uploaded','Please scan the document and upload','warning');", true);
                    return;
                }
                if ((txAmountPromise.Text == "") || (txAmountPromise.Text == "0"))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount can not empty / zero','Payment Promise','warning');", true);
                    return;
                }
            }
        }

        string promiseNo = "";
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@sys", "promise"));
        arr.Add(new cArrayList("@sysno", ""));
        bll.vGetDiscountNo(arr, ref rs);
        while (rs.Read())
        {
            promiseNo = rs["generated"].ToString();
        }
        arr.Clear();

        arr.Add(new cArrayList("@promise_no", promiseNo));
        arr.Add(new cArrayList("@promise_dt", DateTime.Now));
        arr.Add(new cArrayList("@promise_sta_id", 'N'));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@approve", cbapprove.SelectedIndex.ToString()));
        if (cbapprove.SelectedValue.ToString() == "By Paid")
        {
            arr.Add(new cArrayList("@amount", txAmountPaid.Text));
            arr.Add(new cArrayList("@balance", "0"));
            arr.Add(new cArrayList("@paidby", cbpayement.SelectedValue.ToString()));
            arr.Add(new cArrayList("@bank_no", txNoCheque.Text));
            arr.Add(new cArrayList("@bank_code", cbbankcq.SelectedValue.ToString()));
            arr.Add(new cArrayList("@duedate", DateTime.ParseExact(txduedate1.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            FileInfo fi = new FileInfo(uplpaid.FileName);
            string ext = fi.Extension;
            byte[] fs = uplpaid.FileBytes;
            if (fs.Length <= 500000)
            {
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".bmp" || ext == ".gif" || ext == ".png" || ext == ".JPEG" || ext == ".JPG" || ext == ".BMP" || ext == ".GIF" || ext == ".PNG")
                {
                    if ((uplpaid.FileName != "") || (uplpaid.FileName != null))
                    {
                        arr.Add(new cArrayList("@filename", promiseNo + ext));
                        uplpaid.SaveAs(bll.sGetControlParameter("image_path") + "/payment_promise/" + promiseNo + ext);
                        uplpaid_nm.Visible = true;
                        uplpaid.Visible = false;
                        lbfilelocpaid.Text = promiseNo + ext;
                        uplpaid_nm.NavigateUrl = "/images/payment_promise/" + promiseNo + ext;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('You should upload image','jpg,bmp,gif and png upload document again');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 500KB');", true);
                return;
            }
        }
        else
        {
            arr.Add(new cArrayList("@amount", txAmountPromise.Text));
            arr.Add(new cArrayList("@balance", "0"));
            arr.Add(new cArrayList("@paidby", null));
            arr.Add(new cArrayList("@bank_no", null));
            arr.Add(new cArrayList("@bank_code", null));
            arr.Add(new cArrayList("@duedate", DateTime.ParseExact(txduedate2.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            FileInfo fi = new FileInfo(uplPromise.FileName);
            string ext = fi.Extension;
            byte[] fs = uplPromise.FileBytes;
            if (fs.Length <= 500000)
            {
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".bmp" || ext == ".gif" || ext == ".png" || ext == ".JPEG" || ext == ".JPG" || ext == ".BMP" || ext == ".GIF" || ext == ".PNG")
                {
                    if ((uplPromise.FileName != "") || (uplPromise.FileName != null))
                    {
                        arr.Add(new cArrayList("@filename", promiseNo + ext));
                        uplPromise.SaveAs(bll.sGetControlParameter("image_path") + "/payment_promise/" + promiseNo + ext);
                        uplpromise_nm.Visible = true;
                        uplPromise.Visible = false;
                        lbfilelocpromise.Text = promiseNo + ext;
                        uplpromise_nm.NavigateUrl = "/images/payment_promise/" + promiseNo + ext;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('You should upload image','jpg,bmp,gif and png upload document again');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 500KB');", true);
                return;
            }
        }

        bll.vInsertPaymentPromise(arr);
        arr.Clear();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('New Promise has been created','" + promiseNo + "','success');", true);

        btsave.Visible = false;
    }


}