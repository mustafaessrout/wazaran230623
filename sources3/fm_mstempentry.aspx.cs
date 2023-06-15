using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class fm_mstempentry : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.Cookies["lang"].Value.ToString() == "SA")
                {
                    bll.vLang(ref lbempno);
                    bll.vLang(ref lbname);
                }
                bll.vBindingComboToSp(ref cbdepartment, "sp_tmst_department_get", "dept_cd", "dept_nm");
                bll.vBindingFieldValueToCombo(ref cbnationality, "nationality");
                bll.vBindingFieldValueToCombo(ref cbpos, "job_title_cd");
                bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
                bll.vBindingComboToSp(ref cbsalespointacc, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
                bll.vBindingFieldValueToCombo(ref cblevel, "level_cd");
                bll.vBindingComboToSp(ref cbrole, "sp_tmst_role_get", "role_cd", "roleName");
                inputAccess.Attributes.Add("style", "display:none");
                inputTablet.Attributes.Add("style", "display:none");

                if (Request.QueryString["emp"] != null)
                {
                    SqlDataReader rs = null;
                    string sEmp = Request.QueryString["emp"].ToString();
                    List<cArrayList> arr = new List<cArrayList>();
                    arr.Add(new cArrayList("@emp_cd", sEmp));
                    //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vGetMstEmployee(ref rs, arr);
                    while (rs.Read())
                    {
                        txempcd.Text = sEmp;
                        txname.Text = rs["emp_nm"].ToString();
                        txname_ar.Text = rs["emp_nm_arabic"].ToString();
                        cbpos.SelectedValue = rs["job_title_cd"].ToString();
                        cbsalespoint.SelectedValue = rs["salespointcd"].ToString();
                        cbnationality.SelectedValue = rs["nationality"].ToString();
                        cbdepartment.SelectedValue = rs["dept_cd"].ToString();
                        cblevel.SelectedValue = rs["level_cd"].ToString();
                        dtjoin.Text = String.Format("{0:dd/M/yyyy}", DateTime.Parse(rs["join_dt"].ToString()));
                        if (rs["pict_nm"].ToString() == "") { img.ImageUrl = "/noimage.jpg"; } else { img.ImageUrl = "/images/employee/" + rs["pict_nm"].ToString(); }                        
                        if (rs["usr_access"].ToString() == "Available")
                        {
                            chaccess.Checked = true;
                            chaccess_CheckedChanged(sender, e);
                            txusr_nm.Text = rs["usr_id"].ToString();
                            txpassword.Text = rs["password"].ToString();
                            txmobile.Text = rs["mobile_no"].ToString();
                            txemail.Text = rs["email"].ToString();
                            cbrole.SelectedValue = rs["role_cd"].ToString();
                            bll.vBindingGridToSp(ref grdsalespoint, "sp_tuser_salespoint_get", arr);
                        }
                        else { chaccess.Checked = false; }
                        if (rs["usr_tablet"].ToString() == "Available")
                        {
                            chtablet.Checked = true;
                            chtablet_CheckedChanged(sender, e);
                            tximei.Text = rs["imei"].ToString();
                            txsim.Text = rs["sim_number"].ToString();
                            txgmail.Text = rs["gmail"].ToString();
                            txgmailpass.Text = rs["passwd"].ToString();
                        }
                        else { chtablet.Checked = false; }
                    }
                    rs.Close();
                    txempcd.Enabled = false;
                    txusr_nm.Enabled = false;
                    btedit.Visible = true;
                }
                else { img.ImageUrl = "/noimage.jpg"; }
                if (Request.Cookies["lang"].Value.ToString() == "SA")
                {
                    //bll.vLang(ref Label1);
                    //bll.vLang(ref lblempnumber);
                    //bll.vLang(ref lbposition);
                    //bll.vLang(ref lbdept);
                    //bll.vLang(ref lbnational);
                    //bll.vLang(ref lbcarcode);
                    //bll.vLang(ref lbjoin);

                }
                vInitButton(true);
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstempentry");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }

    protected void vInitButton(bool bEdit)
    {
        if (bEdit)
        {
            btadd.Visible = true;
            btedit.Visible = false;
            btsave.Visible = true;
            //chaccess.Enabled = false;
            //chtablet.Enabled = false;
            txempcd.CssClass = "makeitreadonly form-control ";
            txname.CssClass = "makeitreadonly form-control ";
            cbdepartment.CssClass = "makeitreadonly form-control ";
            cblevel.CssClass = "makeitreadonly form-control ";
            cbnationality.CssClass = "makeitreadonly form-control ";
            cbpos.CssClass = "makeitreadonly form-control ";
            cbsalespoint.CssClass = "makeitreadonly form-control ";
        }
        else
        {
            btadd.Visible = true;
            btedit.Visible = false;
            btsave.Visible = false;
            txempcd.CssClass = "makeitreadonly form-control ";
            txname.CssClass = "makeitreadonly form-control ";
            cbdepartment.CssClass = "makeitreadonly form-control ";
            cblevel.CssClass = "makeitreadonly form-control ";
            cbnationality.CssClass = "makeitreadonly form-control ";
            cbpos.CssClass = "makeitreadonly form-control ";
            cbsalespoint.CssClass = "makeitreadonly form-control";
        }
    }

   
    protected void vEditButton()
    { 
        
    }
     protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {

            if (txempcd.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Employee ID can not empty','Check Emp ID','warning');", true);
                return;
            }
            if (chaccess.Checked)
            {
                if (txusr_nm.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('User Id can not empty','Check User ID','warning');", true);
                    return;
                }
                //if (txpassword.Text == "")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Password can not empty','Check Password,'warning');", true);
                //    return;
                //}
                if (txmobile.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Mobile No can not empty','Check Mobile No,'warning');", true);
                    return;
                }
                if (txemail.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Email can not empty','Check Email,'warning');", true);
                    return;
                }
                string userAvailable = "";
                //userAvailable = bll.vLookUp("select usr_id from tuser_profile where usr_id='" + txusr_nm.Text + "'");
                //if (userAvailable != "")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('User Id already used.','Check User ID,'warning');", true);
                //    return;
                //}
            }
            if (chtablet.Checked)
            {
                if (tximei.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Imei can not empty','Check Imei','warning');", true);
                    return;
                }
                if (txsim.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Mobile No can not empty','Check Mobile No for Tablet,'warning');", true);
                    return;
                }
                string imeiAvailable = "";
                imeiAvailable = bll.vLookUp("select imei from ttablet_profile where imei='" + tximei.Text + "'");
                if (imeiAvailable != "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Imei already used.','Check Imei,'warning');", true);
                    return;
                }
            }

            string sLocFile = bll.sGetControlParameter("image_path") + "employee/";
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@emp_cd", txempcd.Text));
            arr.Add(new cArrayList("@emp_nm", txname.Text.ToUpper()));
            arr.Add(new cArrayList("@emp_nm_arabic", txname_ar.Text));
            arr.Add(new cArrayList("@job_title_cd", cbpos.SelectedValue.ToString()));
            arr.Add(new cArrayList("@dept_cd", cbdepartment.SelectedValue.ToString()));
            arr.Add(new cArrayList("@nationality", cbnationality.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            arr.Add(new cArrayList("@level_cd", cblevel.SelectedValue.ToString()));
            arr.Add(new cArrayList("@pict_nm", "EMP" + txempcd.Text + ".jpg"));
            arr.Add(new cArrayList("@join_dt", System.DateTime.ParseExact(dtjoin.Text,"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@deleted", "0"));
            bll.vInsertMstEmployee(arr);
            if (upl.FileName != "")
            {
                upl.SaveAs(sLocFile + "EMP" + txempcd.Text + ".jpg");
                img.ImageUrl = "/images/employee/EMP" + txempcd.Text + ".jpg";
            }

            if (chaccess.Checked)
            {
                arr.Clear();
                arr.Add(new cArrayList("@emp_cd", txempcd.Text));
                arr.Add(new cArrayList("@usr_id", txusr_nm.Text));
                arr.Add(new cArrayList("@fullname", txname.Text.ToUpper()));
                arr.Add(new cArrayList("@email", txemail.Text));
                arr.Add(new cArrayList("@mobile_no", txmobile.Text));
                arr.Add(new cArrayList("@role_cd", cbrole.SelectedValue.ToString()));
                arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
                arr.Add(new cArrayList("@password", txpassword.Text));
                bll.vInsertMstUserProfile(arr);
            }
            if (chtablet.Checked)
            {
                arr.Clear();
                arr.Add(new cArrayList("@emp_cd", txempcd.Text));
                arr.Add(new cArrayList("@imei", tximei.Text));
                arr.Add(new cArrayList("@sim", txsim.Text));
                arr.Add(new cArrayList("@gmail", txgmail.Text));
                arr.Add(new cArrayList("@passwd", txgmailpass.Text));
                arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
                bll.vInsertMstTabletProfile(arr);
            }

            btsave.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Employee has been saved successfully','Employee','success');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstempentry");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
     protected void btadd_Click(object sender, EventArgs e)
     {
        Response.Redirect("fm_mstempentry.aspx");
     }

    protected void chaccess_CheckedChanged(object sender, EventArgs e)
    {
        if (chaccess.Checked)
        {
            if (txempcd.Text == "")
            {
                chaccess.Checked = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Employee ID can not empty','Check Emp ID','warning');", true);
                return;
            }
            inputAccess.Attributes.Remove("style");
            txusr_nm.Text = txempcd.Text.ToString();
            txusr_nm.Enabled = false;
        }
        else
        {
            inputAccess.Attributes.Add("style", "display:none");
        }
        
    }

    protected void chtablet_CheckedChanged(object sender, EventArgs e)
    {
        if (chtablet.Checked)
        {
            inputTablet.Attributes.Remove("style");
        }
        else
        {
            inputTablet.Attributes.Add("style","display:none");
        }        
    }

    protected void btaddsalespointacc_Click(object sender, EventArgs e)
    {
        try
        {
            if (txempcd.Text == "")
            {
                chaccess.Checked = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Employee ID can not empty','Check Emp ID','warning');", true);
                return;
            }

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@emp_cd", txempcd.Text.ToString()));
            arr.Add(new cArrayList("@salespointcd", cbsalespointacc.SelectedValue.ToString()));
            bll.vInsertEmployeeAccessSalespoint(arr);
            arr.Clear();
            arr.Add(new cArrayList("@emp_cd", txempcd.Text.ToString()));
            bll.vBindingGridToSp(ref grdsalespoint, "sp_tuser_salespoint_get", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_mstempentry");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grdsalespoint_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        HiddenField hdsalespointcd = (HiddenField)grdsalespoint.Rows[e.RowIndex].FindControl("hdsalespointcd");
        arr.Add(new cArrayList("@emp_cd", txempcd.Text.ToString()));
        arr.Add(new cArrayList("@salespointcd", hdsalespointcd.Value.ToString()));
        bll.vDeleteEmployeeAccessSalespoint(arr);
        arr.Clear();
        arr.Add(new cArrayList("@emp_cd", txempcd.Text.ToString()));
        bll.vBindingGridToSp(ref grdsalespoint, "sp_tuser_salespoint_get", arr);
    }

    protected void grdsalespoint_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdsalespoint.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", txempcd.Text.ToString()));
        bll.vBindingGridToSp(ref grdsalespoint, "sp_tuser_salespoint_get", arr);
    }

}