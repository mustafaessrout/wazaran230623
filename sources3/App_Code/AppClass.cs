using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for AppClass
/// </summary>
namespace AppClassTools
{
    public class AppClass
    {
        public BootstrapAlertType alertType { get; set; }

        public enum BootstrapAlertType
        {

            Plain,

            Success,

            Information,

            Warning,

            Danger,

            Primary,
        }

        public void BootstrapAlert(Label MsgLabel, string Message, BootstrapAlertType MessageType = BootstrapAlertType.Plain, bool Dismissable = false)
        {
            string style = string.Empty;
            // Warning!!! Optional parameters not supported
            // Warning!!! Optional parameters not supported
            string icon = string.Empty;
            switch (MessageType)
            {
                case BootstrapAlertType.Plain:
                    style = "default";
                    icon = "";
                    break;
                case BootstrapAlertType.Success:
                    style = "success";
                    icon = "check";
                    break;
                case BootstrapAlertType.Information:
                    style = "info";
                    icon = "info-circle";
                    break;
                case BootstrapAlertType.Warning:
                    style = "warning";
                    icon = "warning";
                    break;
                case BootstrapAlertType.Danger:
                    style = "danger";
                    icon = "remove";
                    break;
                case BootstrapAlertType.Primary:
                    style = "primary";
                    icon = "info";
                    break;
            }
            if (((!MsgLabel.Page.IsPostBack
                        || MsgLabel.Page.IsPostBack)
                        && (Message == null)))
            {
                MsgLabel.Visible = false;
            }
            else
            {
                MsgLabel.Text = string.Empty;
                MsgLabel.Visible = true;
                MsgLabel.CssClass = "alert alert-" + style + (Dismissable == true ? " alert-dismissible fade in font2" : "");
                MsgLabel.Text = "<i class='fa fa-" + icon + "'></i>" + Message;
                if (Dismissable == true)
                {
                    MsgLabel.Text += "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
                }
                MsgLabel.Focus();
                Message = "";
            }

        }
    }
}