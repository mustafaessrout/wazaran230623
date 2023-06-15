<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_docreject.aspx.cs" Inherits="fm_docreject" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style>
        .main-content #mCSB_2_container{
            min-height: 520px;
        }
    </style>
   </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Document Reject / Missing</div>
    <div class="h-divider"></div>
    <div class="container-fluid">
        <div class="row">
            <div class="clearfix form-group col-sm-6">
                <label class="control-label col-sm-3" >Manual Number</label>
                <div class="col-sm-9">
                    <asp:TextBox ID="txdocno" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="clearfix form-group col-sm-6">
                <label class="control-label col-sm-3" >Date</label>
                <div class="col-sm-9 drop-down-date">
                     <asp:TextBox ID="dtdoc" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:CalendarExtender CssClass="date" ID="dtdoc_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtdoc">
                    </asp:CalendarExtender>
              
                </div>
            </div>
            <div class="clearfix form-group col-sm-6">
                <label class="control-label col-sm-3" >Document Type</label>
                <div class="col-sm-9 drop-down">
                    <asp:DropDownList ID="cbdoctype" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                
                </div>
            </div>
            <div class="clearfix form-group col-sm-6">
                <label class="control-label col-sm-3" >reason</label>
                <div class="col-sm-9 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbreason" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbdoctype" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
         
                </div>
            </div>

        </div>
        <div class="row navi padding-top padding-bottom">
            <asp:Button ID="btnew" runat="server" Text="New" CssClass="btn-success btn btn-add" OnClick="btnew_Click" />
            <asp:Button ID="btsave" runat="server" CssClass="btn-warning btn btn-save" OnClick="btsave_Click" Text="Save" />
        </div>
    </div>
    
    
</asp:Content>

