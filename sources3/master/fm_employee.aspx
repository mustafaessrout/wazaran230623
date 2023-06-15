<%@ Page Title="" Language="C#" MasterPageFile="~/master/homaster.master" AutoEventWireup="true" CodeFile="fm_employee.aspx.cs" Inherits="master_fm_employee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
        function SelectData(sVal)
        {
            $get('<%=hdemp.ClientID%>').value = sVal;
            $get('<%=btlookup.ClientID%>').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <asp:HiddenField ID="hdemp" runat="server" />
    <div class="form-horizontal" style="font-family:Calibri;font-size:small">
        <h4 class="jajarangenjang">Master Employee</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <label class="control-label col-md-1">Emp Code</label>
            <div class="col-md-2">
                <div class="input-group">
                     <asp:TextBox ID="txempcode" CssClass="form-control input-group-sm" runat="server"></asp:TextBox>
                    <div class="input-group-btn">
                        <asp:LinkButton ID="btsearch" CssClass="btn btn-primary" runat="server"><span class="glyphicon glyphicon-search"></span></asp:LinkButton>
                    </div>
                </div>
               
            </div>
            <label class="control-label col-md-1">Name</label>
            <div class="col-md-8">
                <asp:TextBox ID="txfullname" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
          
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">Birth Date</label>
            <div class="col-md-2">
                <asp:TextBox ID="dtbirth" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            

            <label class="control-label col-md-1">Join</label>
            <div class="col-md-2">
                <asp:TextBox ID="dtjoin" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
             <label class="control-label col-md-1">Gender</label>
            <div class="col-md-2">
                <asp:DropDownList ID="cbgender" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
             <label class="control-label col-md-1">Job Title</label>
            <div class="col-md-2">
                <asp:DropDownList ID="cbjobtitle" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">Job Level</label>
            <div class="col-md-2">
                <asp:DropDownList ID="cbjoblevel" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
            <label class="control-label col-md-1">Nationality</label>
            <div class="col-md-2">
                <asp:DropDownList ID="cbnationality" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
            <label class="control-label col-md-1">Iqoma</label>
            <div class="col-md-2">
                <asp:TextBox ID="txiqoma" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <label class="control-label col-md-1">Passport</label>
            <div class="col-md-2">
                <asp:TextBox ID="txpassport" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
             <label class="control-label col-md-1">Mobile no</label>
             <div class="col-md-2">
                <asp:TextBox ID="txmobileno" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
             <label class="control-label col-md-1">Email</label>
             <div class="col-md-2">
                <asp:TextBox ID="txemail" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
              <label class="control-label col-md-1">Religion</label>
             <div class="col-md-2">
                 <asp:DropDownList ID="cbreligion" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
            <label class="control-label col-md-1">Sponsor</label>
             <div class="col-md-2">
                 <asp:DropDownList ID="cbsponsor" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">Education</label>
             <div class="col-md-2">
                 <asp:DropDownList ID="cbeducation" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
             <label class="control-label col-md-1">Job In Visa</label>
             <div class="col-md-4">
                 <asp:DropDownList ID="cbjobvisa" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbjobvisa_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="col-md-4">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                          <asp:Label ID="lbvisaarabic" runat="server" CssClass="form-control"></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbjobvisa" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
              
            </div>
            
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">Status</label>
            <div class="col-md-2">
             <asp:DropDownList ID="cbempstatus" runat="server" CssClass="form-control" ></asp:DropDownList>
            </div>
        </div>
        <div class="h-divider"></div>
        <div class="form-group">
            <div class="col-md-12" style="text-align:center">
               
                <asp:LinkButton ID="btnew" CssClass="btn btn-primary" runat="server" OnClick="btnew_Click">New</asp:LinkButton>
                 <asp:LinkButton ID="btedit" runat="server" CssClass="btn btn-success" OnClick="btedit_Click">Edit</asp:LinkButton>
                 <asp:LinkButton ID="btsave" CssClass="btn btn-info" runat="server" OnClick="btsave_Click">Save</asp:LinkButton>
                 <asp:LinkButton ID="btprint" CssClass="btn btn-danger" runat="server" OnClick="btprint_Click">Print</asp:LinkButton>
                 <asp:LinkButton ID="btprintall" runat="server" CssClass="btn btn-danger" OnClick="btprintall_Click">Print All</asp:LinkButton>
                <asp:Button ID="btlookup" runat="server" Text="Button" OnClick="btlookup_Click" style="display:none" />
            
            </div>
        </div>
    </div>
      <script>
          $(document).ready(function () {
              $("#<%=btsearch.ClientID%>").click(function () {
            PopupCenter('lookupemployee.aspx', 'xtf', '900', '500');
        });
    });


    </script>  
</asp:Content>

