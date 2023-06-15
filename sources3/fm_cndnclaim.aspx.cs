using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_cndnclaim : System.Web.UI.Page
{
	cbll bll = new cbll();

	protected void Page_Load(object sender, EventArgs e)
	{
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
		//public string rdCustGlobal = "";
		Session["rdCustGlobal"] = "";
		if (!IsPostBack)
		{
			List<cArrayList> arr = new List<cArrayList>();
			arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
			bll.vDelWrkClaimCNDN(arr);
			btsave.CssClass = "divhid"; btprint.CssClass = "divhid";
			vContract.Attributes.Add("style", "display:none");
			totAmount.Attributes.Add("style", "display:none");
		}
		ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
		btapprove.Visible = false;
		btreject.Visible = false;
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
	   // arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
		arr.Add(new cArrayList("@prop_no", prefixText));
		bll.vGetProposalPaymentByDate(arr, ref rs);
		while (rs.Read())
		{
			sProp = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["prop_no"].ToString() + "-" + rs["bgremark"].ToString(), rs["prop_no"].ToString());
			lprop.Add(sProp);
		}
		rs.Close();
		return (lprop.ToArray());
	}
	protected void btsearch_Click(object sender, EventArgs e)
	{
		System.Data.SqlClient.SqlDataReader rs = null;
		string sRdCust=string.Empty;
		List<cArrayList> arr = new List<cArrayList>();
		arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
		bll.vGetMstProposal(arr, ref rs);
		while (rs.Read())
		{ 
			sRdCust = rs["rdcust"].ToString();
			dtprop.Text = Convert.ToDateTime(rs["prop_dt"]).ToString("d/M/yyyy");
			dtstart.Text = Convert.ToDateTime(rs["start_dt"]).ToString("d/M/yyyy");
			dtend.Text = Convert.ToDateTime(rs["end_dt"]).ToString("d/M/yyyy");
			dtdelivery.Text = Convert.ToDateTime(rs["delivery_dt"]).ToString("d/M/yyyy");
			lbcust.Text = rs["rdcust"].ToString() + ":"+ bll.vLookUp("select fld_desc from tfield_value where fld_nm='rdcust' and fld_valu='"+rs["rdcust"].ToString()+"'");
			txremark.Text = rs["remark"].ToString();
			hdrdcust.Value = rs["rdcust"].ToString();
            lbbudget.Text = rs["totalbudget"].ToString();
			dtprop.CssClass = "form-control ro";
			dtprop.Enabled = false;
			dtstart.CssClass = "form-control ro";
			dtstart.Enabled = false;
			dtend.CssClass = "form-control ro";
			dtend.Enabled = false;
			dtdelivery.CssClass = "form-control ro";
			dtdelivery.Enabled = false;
			txremark.CssClass = "form-control ro";
			txremark.Enabled = false;
			txproposal.Text = rs["prop_no"].ToString();
			hdprop.Value = rs["prop_no"].ToString();
			txproposal.CssClass = "form-control ro";
			txproposal.Enabled = false;
			btsave.CssClass = "btn btn-info btn-print";
		}

        // Remain Budget CNDN
        lbremain.Text = bll.vLookUp("(select cast(isnull(budget_limit,0) as numeric(18,2))-(cast(isnull((select isnull(sum(b.amt), 0) from tclaim_reqcndn a left join tclaimreqcndn_dtl b on a.cndn_cd = b.cndn_cd left join tmst_proposal c on a.prop_no = c.prop_no  where a.cndn_sta_id in ('A', 'N') and a.prop_no = '"+hdprop.Value.ToString()+"'), 0) as numeric(18, 2))) from tmst_proposal where prop_no = '"+hdprop.Value.ToString()+"')");

        double dRemain = Convert.ToDouble(lbremain.Text);

        if (dRemain <= 0)
        {
            btsave.Visible = false;
            btprint.Visible = false;
            btreject.Visible = false;
            btapprove.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('This Proposal Budget already used. ','" + hdprop.Value.ToString() + "','warning');", true);
            return;
        }

        // Need Contract or Not
        string sType = bll.vLookUp("select promogroup from tmst_proposal where prop_no='" + hdprop.Value.ToString() + "'");
		if (sType == "DR" || sType == "TB" || sType == "SB" || sType == "DS")
		{
			vContract.Attributes.Remove("style");
		}
		else
		{
			vContract.Attributes.Add("style", "display:none");
		}


		Session["rdCustGlobal"] = sRdCust;
		rs.Close();
		if (sRdCust == "C")
		{
			bll.vBindingComboToSp(ref cbcode, "sp_tproposal_customer_get", "code", "name", arr);
		  //  txcust.Text = cbcode.SelectedValue.ToString()+':'+bll.vLookUp("select cust_nm from tmst_customer where cust_cd='"+cbcode.SelectedValue.ToString()+"'");
			txcust.CssClass = "form-control ro";
			txcust.Enabled = false;
		  //  bll.vBindingGridToSp(ref grd, "sp_tproposal_customer_get", arr);
			cbcode_SelectedIndexChanged(sender, e);
		   
		}
		else if (sRdCust == "T")
		{
			bll.vBindingComboToSp(ref cbcode, "sp_tproposal_custtype_get", "code", "name", arr);
			txcust.CssClass = "form-control";
			txcust.Enabled = true;
		 //   bll.vBindingGridToSp(ref grd, "sp_tproposal_custtype_get", arr);
		}
		else if (sRdCust == "G")
		{
			bll.vBindingComboToSp(ref cbcode, "sp_tproposal_cusgrcd_get", "code", "name", arr);
			txcust.CssClass = "form-control";
			txcust.Enabled = true;
		 //   bll.vBindingGridToSp(ref grd, "sp_tproposal_cusgrcd_get", arr);
		}
	}

	[System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
	public static string[] GetCompletionList2(string prefixText, int count, string contextKey)
	{
		return default(string[]);
	}
	protected void btadd_Click(object sender, EventArgs e)
	{
		List<cArrayList> arr = new List<cArrayList>();
		arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
		arr.Add(new cArrayList("@inv_no", ""));
		arr.Add(new cArrayList("@cust_cd",""));
		arr.Add(new cArrayList("@amt", ""));
	}
	protected void btsearchcust_Click(object sender, EventArgs e)
	{
		ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opd", "popupwindow('lookupcustomer.aspx?ct="+hdrdcust.Value.ToString()+"&val="+cbcode.SelectedValue.ToString()+"');", true);
	}
	protected void btsearchinv_Click(object sender, EventArgs e)
	{
        List<cArrayList> arr = new List<cArrayList>();
		//arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
		//bll.vBindingGridToSp(ref grdinv, "sp_tdosales_invoice_getbycust", arr);
		arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
		bll.vDelWrkClaimCNDN(arr);
		arr.Clear();

        // Enh : 22 June 2019 : Customer Transfer Blocked - CIN
        string sCustomerTransferBlock = bll.vLookUp("select dbo.fn_customertransferpending('" + hdcust.Value.ToString() + "')");
        if (sCustomerTransferBlock != "ok")
        {
            hdcust.Value = ""; txcust.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('This customer block for sales caused there is pending in customer transfer ','" + sCustomerTransferBlock + "','warning');", true);
            return;
        }

        arr.Add(new cArrayList("@cust_cd",hdcust.Value.ToString()));
		arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
		arr.Add(new cArrayList("@amt",0));
		arr.Add(new cArrayList("@vat", 0));
		arr.Add(new cArrayList("@isvat", 0));
		bll.vInsertWrkClaimCNDN(arr);
		arr.Clear();
		arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
		bll.vBindingGridToSp(ref grdinv, "sp_twrk_claimcndn_get", arr);
		vFooter();
	}
	protected void grdinv_RowEditing(object sender, GridViewEditEventArgs e)
	{
		ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editTable", "editTable();", true);
		grdinv.EditIndex = e.NewEditIndex;
		List<cArrayList> arr = new List<cArrayList>();
		arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
		bll.vBindingGridToSp(ref grdinv, "sp_twrk_claimcndn_get", arr);

		

	}
	protected void btsave_Click(object sender, EventArgs e)
	{


        double dRemain = Convert.ToDouble(lbremain.Text);

        if (dRemain <= 0)
        {
            btsave.Visible = false;
            btprint.Visible = false;
            btreject.Visible = false;
            btapprove.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('This Proposal Budget already used. ','" + hdprop.Value.ToString() + "','warning');", true);
            return;
        }

        if (upl.HasFile)
			{
				FileInfo fi1 = new FileInfo(upl.FileName);
				string ext1 = fi1.Extension;
				byte[] fs1 = upl.FileBytes;
				if (fs1.Length > 5242880)
				{
					ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Picture Size must be less than 1 MB','Images Size','warning');", true);
					return;
				}
			}
			else
			{
				ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Images Must be uploaded','Images not uploaded','warning');", true);
				return;
			}
		
		if (grdinv.Rows.Count == 0)
		{
			ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('No Invoice for this Customer','Please choose other Proposal.','warning');", true);
			return;
		}
		
		string sType = bll.vLookUp("select promogroup from tmst_proposal where prop_no='" + hdprop.Value.ToString() + "'");
		if (sType == "DR" || sType == "TB" || sType == "SB" || sType == "DS")
		{
			if (txcontract.Text == "")
			{
				ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('This CNDN need Business Agreement. ','BA No.','warning');", true);
				return;
			}
		}

		// Upload Document 
		FileInfo fi = new FileInfo(upl.FileName);
		string ext = fi.Extension;
		byte[] fs = upl.FileBytes;

		if ((upl.FileName == "") || (upl.FileName == null))
		{
			ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('This CNDN need Document Upload. ','CNDN Document.','warning');", true);
			return;
		}

		string sNo = string.Empty;
		List<cArrayList> arr = new List<cArrayList>();
		arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
		arr.Add(new cArrayList("@cndn_dt", DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
		arr.Add(new cArrayList("@rdcust", hdrdcust.Value.ToString()));
		arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value));
		arr.Add(new cArrayList("@deleted", 0));
		arr.Add(new cArrayList("@cndn_sta_id","N"));
		arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
		arr.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
		if (sType == "DR" || sType == "TB" || sType == "SB" || sType == "DS")
		{
			arr.Add(new cArrayList("@contract_no", txcontract.Text));
		}
		bll.vInsertClaimCNDN(arr, ref sNo);

		// Insert Document 

		if (fs.Length <= 5242880)
		{
			if (ext == ".jpg" || ext == ".jpeg" || ext == ".bmp" || ext == ".gif" || ext == ".png" || ext == ".pdf" || ext == ".JPEG" || ext == ".JPG" || ext == ".BMP" || ext == ".GIF" || ext == ".PNG" || ext == ".PDF")
			{
				if ((upl.FileName != "") || (upl.FileName != null))
				{
					arr.Clear();
					arr.Add(new cArrayList("@cndn_cd", sNo));
					arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
					arr.Add(new cArrayList("@file", sNo.ToString() + ext));
					upl.SaveAs(bll.sGetControlParameter("image_path") + "/claim_cndn/" + sNo.ToString() + ext);

					bll.vInsertCNDNDocument(arr);
					arr.Clear();
					//ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Image Uploaded','" + sNo.ToString() + ext + "','success');", true);
					hpfile_nm.Visible = true;
					upl.Visible = false;
					lblocfile.Text = sNo.ToString() + ext;
					hpfile_nm.NavigateUrl = "/images/claim_cndn/" + sNo.ToString() + ext;
					//return;
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



		txcnno.Text = sNo; Random rToken = new Random();
		Label lbtotpaid = (Label)grdinv.FooterRow.FindControl("lbtotpaid");
		int nToken = rToken.Next(1, 9999);
		string stoken = bll.vLookUp("select short_ip from tmst_salespoint where salespointcd=(select parm_valu from tcontrol_parameter where parm_nm='salespoint')") + nToken.ToString();
		string totalamount = bll.vLookUp("select sum(amt+isnull(vat,0)) from tclaimreqcndn_dtl where cndn_cd='"+sNo+"'");
		string sMail = "#Claim CN Branch:" + Request.Cookies["spn"].Value.ToString() + ", Request No." + sNo + ",tot amt:" + totalamount + ",Prop No:" + hdprop.Value.ToString() + " Pls reply (y/n)" + stoken;
		
		arr.Clear();
		
		List<string> lapp= bll.lGetApproval("claimcndn", 1);
		arr.Add(new cArrayList("@msg", sMail));
		arr.Add(new cArrayList("@doc_no", sNo));
		arr.Add(new cArrayList("@doc_typ", "claimcndn"));
		arr.Add(new cArrayList("@to", lapp[0]));
		arr.Add(new cArrayList("@token", stoken));

		bll.vInsertSmsOutbox(arr);
		btsave.CssClass = "divhid";
		btprint.CssClass = "btn btn-info btn-print";
		ScriptManager.RegisterStartupScript(Page,Page.GetType(),"al","sweetAlert('New Request CN Claim has been completed','"+sNo+"','success');",true);
	}
	protected void grdinv_RowUpdating(object sender, GridViewUpdateEventArgs e)
	{
		double vat = double.Parse(bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='vat'"));
		double formula = vat + 1;

		TextBox txpaid = (TextBox)grdinv.Rows[e.RowIndex].FindControl("txpaid");
		Label lbinvoiceno = (Label)grdinv.Rows[e.RowIndex].FindControl("lbinvoiceno");
		Label lbremain = (Label)grdinv.Rows[e.RowIndex].FindControl("lbremain");
		double dOut = 0;
		if (!double.TryParse(txpaid.Text, out dOut))
		{
			ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Please enter numeric format','Numeric Only','warning');", true);
			return;
		}

		if (dOut > Convert.ToDouble(lbbudget.Text))
		{
			ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrxxt", "sweetAlert('Paid can not bigger than budget','Over Budget','warning');", true);
			return;
		}

		double dRemain = Convert.ToDouble(lbremain.Text);
		if (dOut > dRemain)
		{
			ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrxt", "sweetAlert('Paid can not bigger than Remain Balance Invoice','Over Payment','warning');", true);
			return;
		}
		List<cArrayList> arr = new List<cArrayList>();

		if (rdVat.SelectedValue.ToString() == "1")
		{
			arr.Add(new cArrayList("@amt", (double.Parse(txpaid.Text) / (formula)).ToString()));
			arr.Add(new cArrayList("@vat", (double.Parse(txpaid.Text) - (double.Parse(txpaid.Text) / (formula))).ToString()));
			arr.Add(new cArrayList("@isvat", "0.05"));
		}
		else
		{
			arr.Add(new cArrayList("@amt", txpaid.Text));
			arr.Add(new cArrayList("@vat", "0.0"));
			arr.Add(new cArrayList("@isvat", "0.0"));
		}

		arr.Add(new cArrayList("@inv_no",lbinvoiceno.Text));
		arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));        
		bll.vInsertWrkClaimCNDN(arr);
		grdinv.EditIndex = -1;
		arr.Clear();
		 arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
		bll.vBindingGridToSp(ref grdinv, "sp_twrk_claimcndn_get", arr);
		vFooter();
	   
	}
	protected void btprint_Click(object sender, EventArgs e)
	{
		//ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opre", "openreport('fm_report2.aspx?src=claimcn');", true);
		ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=claimcn&no=" + hdclaim.Value.ToString() + "');", true);
	}

	void vFooter()
	{
		if (grdinv.Rows.Count > 0)
		{
			Label lbtotpaid = (Label)grdinv.FooterRow.FindControl("lbtotpaid");
			lbtotpaid.Text = bll.vLookUp("select sum(amt) from twrk_claimcndn where usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'");
		}

	}



	protected void btsearchcn_Click(object sender, EventArgs e)
	{
		ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "popupwindow('lookupclaimcndn.aspx');", true);
	}
	protected void btclaim_Click(object sender, EventArgs e)
	{
		List<cArrayList> arr = new List<cArrayList>();
		arr.Add(new cArrayList("@cndn_cd", hdclaim.Value.ToString()));
		System.Data.SqlClient.SqlDataReader rs = null;
		bll.vGetClaimCNDN(arr, ref rs);
		while (rs.Read())
		{
			dtdelivery.Text = Convert.ToDateTime(rs["delivery_dt"]).ToString("d/M/yyyy");
			dtstart.Text = Convert.ToDateTime(rs["start_dt"]).ToString("d/M/yyyy");
			dtend.Text = Convert.ToDateTime(rs["end_dt"]).ToString("d/M/yyyy");
			dtprop.Text = Convert.ToDateTime(rs["cndn_dt"]).ToString("d/M/yyyy");
			txcnno.Text = hdclaim.Value.ToString();
			txproposal.Text = rs["prop_no"].ToString();
			cbcode.SelectedValue = rs["rdcust"].ToString();
			hpfile_nm.Visible = true;
			upl.Visible = false;
			lblocfile.Text = rs["fileloc"].ToString();
			hpfile_nm.NavigateUrl = "/images/claim_cndn/" + rs["fileloc"].ToString();
		} rs.Close();
		btprint.CssClass = "btn btn-info btn-print";
		arr.Clear();
		arr.Add(new cArrayList("@cndn_cd", hdclaim.Value.ToString()));
		bll.vBindingGridToSp(ref grdviewinv, "sp_tclaimreqcndn_dtl_get", arr);
		//vFooter();
		btapprove.Visible = true;
		btreject.Visible = true;
	}
	protected void btnew_Click(object sender, EventArgs e)
	{
		Response.Redirect("fm_cndnclaim.aspx");
	}
	protected void btse_Click(object sender, EventArgs e)
	{
		ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ops", "popupwindow('lookproposal2.aspx');", true);
	}
	protected void cbcode_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (Session["rdCustGlobal"].ToString() == "C")
		{
			List<cArrayList> arr = new List<cArrayList>();
			arr.Add(new cArrayList("@cust_cd", cbcode.SelectedValue.ToString()));
			arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
			arr.Add(new cArrayList("@amt", 0));
			arr.Add(new cArrayList("@vat", 0));
			arr.Add(new cArrayList("@isvat", 0));
			bll.vInsertWrkClaimCNDN(arr);
			arr.Clear();
			arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
			bll.vBindingGridToSp(ref grdinv, "sp_twrk_claimcndn_get", arr);
			vFooter();
		}

	}
	protected void btsearchcontract_Click(object sender, EventArgs e)
	{
		ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "popupwindow('fm_lookupcontract.aspx?prop=" + hdprop.Value.ToString() + "');", true);
	}
	protected void txcontract_TextChanged(object sender, EventArgs e)
	{

	}
	protected void btsearch3_Click(object sender, EventArgs e)
	{
		txcontract.CssClass = "form-control input-sm ro";
		txcontract.Enabled = false;
		txcontract.Text = hdcontract.Value.ToString();
	}
	protected void cbpaymentType_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (cbpaymentType.SelectedValue.ToString() == "F") 
		{
			totAmount.Attributes.Remove("style");
			grdinv.Columns[11].Visible = false;
		}
		else
		{
			totAmount.Attributes.Add("style","display:none");
			grdinv.Columns[11].Visible = true;
		}        
	}
	protected void btnapplyamount_Click(object sender, EventArgs e)
	{
		double vat = double.Parse(bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='vat'"));

		double dOut = 0;
		if (!double.TryParse(txTotalAmount.Text, out dOut))
		{
			ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Please enter numeric format','Numeric Only','warning');", true);
			return;
		}

		if (dOut > Convert.ToDouble(lbbudget.Text))
		{
			ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrxxt", "sweetAlert('Paid can not bigger than budget','Over Budget','warning');", true);
			return;
		}

		List<cArrayList> arr = new List<cArrayList>();

		arr.Add(new cArrayList("@amt", txTotalAmount.Text));
		arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
		bll.vInsertWrkClaimCNDN(arr);
		arr.Clear();
		arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
		bll.vBindingGridToSp(ref grdinv, "sp_twrk_claimcndn_get", arr);
		vFooter();
	}
	protected void btapprove_Click(object sender, EventArgs e)
	{
		if (bll.nCheckAccess("cndnapprove", Request.Cookies["usr_id"].Value.ToString()) == 0)
		{
			ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To Aprove this CNDN Claim, contact Administrator !!','warning');", true);
			return;
		}
		List<cArrayList> arr = new List<cArrayList>();
		arr.Add(new cArrayList("@cndn_cd", hdclaim.Value));
		arr.Add(new cArrayList("@status", "A"));
		bll.vApprovalClaimCNDN(arr);
		ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Request CN Claim has been approved','" + hdclaim.Value + "','success');", true);
	}
	protected void btreject_Click(object sender, EventArgs e)
	{
		if (bll.nCheckAccess("cndnreject", Request.Cookies["usr_id"].Value.ToString()) == 0)
		{
			ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To Reject this CNDN CLAIM, contact Administrator !!','warning');", true);
			return;
		}
		List<cArrayList> arr = new List<cArrayList>();
		arr.Add(new cArrayList("@cndn_cd", hdclaim.Value));
		arr.Add(new cArrayList("@status", "E"));
		bll.vApprovalClaimCNDN(arr);
		ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('New Request CN Claim has been rejected','" + hdclaim.Value + "','success');", true);
	}
}