<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_reqcust.aspx.cs" Inherits="fm_reqcust" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
     <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="css/jquery-1.9.1.js"></script>
    <script src="css/jquery-ui.js"></script>
     <script type="text/javascript">
         $(function () {
             $("#<%=dtreg.ClientID%>").datepicker({ dateFormat: "mm/dd/yy" }).val();
         });

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="divheader">
     Customer Registration Voucher

 </div>
 <img src="div2.png" class="divid" />
 <div class="divgrid">
      <table>
          <tr>
              <td>
                  Registration Date&nbsp;
              </td>
              <td>:</td>
              <td>
                  <asp:TextBox ID="dtreg" runat="server"></asp:TextBox>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          
          <tr>
              <td>
                  Customer Type</td>
              <td>:</td>
              <td>
                  <asp:DropDownList ID="cbcusttyp" runat="server" OnSelectedIndexChanged="cbcusttyp_SelectedIndexChanged" AutoPostBack="true">
                  </asp:DropDownList>
              </td>
              <td>
                  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                      <ContentTemplate>
                            <asp:Label ID="lbcredit" runat="server" Text="Label" style="color:red"></asp:Label>
                      </ContentTemplate>
                      <Triggers>
                          <asp:AsyncPostBackTrigger ControlID="cbcusttyp" EventName="SelectedIndexChanged" />
                      </Triggers>
                  </asp:UpdatePanel>
                
              </td>
          </tr>
          
          <tr>
              <td>
                  Credit Limit</td>
              <td>:</td>
              <td>
                  <asp:TextBox ID="txCL" runat="server"></asp:TextBox>
              </td>
              <td>
                  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                      <ContentTemplate>
                          <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Unmatch criteria !" Font-Size="Small" ForeColor="Red" ControlToValidate="txCL"></asp:RangeValidator>
                      </ContentTemplate>
                      <Triggers>
                          <asp:AsyncPostBackTrigger ControlID="cbcusttyp" EventName="SelectedIndexChanged" />
                      </Triggers>
                  </asp:UpdatePanel>
                
              </td>
          </tr>
          
          <tr>
              <td>
                  Customer Name</td>
              <td>:</td>
              <td>
                  <asp:TextBox ID="txcustname" runat="server"></asp:TextBox>
              </td>
              <td>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txcustname" ErrorMessage="*"></asp:RequiredFieldValidator>
              </td>
          </tr>
          
          <tr>
              <td>
                  Customer Code</td>
              <td>:</td>
              <td>
                  <asp:TextBox ID="txcustcode" runat="server"></asp:TextBox>
              </td>
              <td>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txcustcode" ErrorMessage="*"></asp:RequiredFieldValidator>
              </td>
          </tr>
          
          <tr>
              <td>
                  Arabic</td>
              <td>:</td>
              <td>
                  <asp:TextBox ID="txarabic" runat="server"></asp:TextBox>
              </td>
              <td>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txarabic" ErrorMessage="*"></asp:RequiredFieldValidator>
              </td>
          </tr>
          
          <tr>
              <td>
                  Customer Group</td>
              <td>:</td>
              <td>
                  <asp:DropDownList ID="cbcustgroup" runat="server">
                  </asp:DropDownList>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          
          <tr>
              <td>
                  Due Date</td>
              <td>:</td>
              <td>
                  <asp:DropDownList ID="cbterm" runat="server">
                  </asp:DropDownList>
              </td>
              <td>
                  &nbsp;</td>
          </tr>
          
          <tr>
              <td>
                  &nbsp;</td>
              <td>&nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          
          <tr>
              <td>
                  &nbsp;</td>
              <td>&nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
          
      </table>  
     <div style="padding:10px;border:5px solid black">
         For credit customer , please attach copy of document : License Chamber of Commerce , Licence of Outlet, ID Card of outlet responsible and Photo of Outlet
     </div>
     <div>
     <p style="font-size:smaller;color:red">
         Note.<br />
         1. Customer with credit limit more than Sr. 10.000 , should use letter with their company header and stamp by Chamber of Commerece.<br />
         2. Customer No Legal (NL) maximum credit limit Sr. 2000 and limit is due date is 30 days, all amount is responsible 100%&nbsp; by salesman<br />
         3. Cash Customer can not take credit sales.<br />
         4. Cash customer within repeat order more than 3 times should be have own file in system, control by BM SPV.<br />
         5. Cash Customer (CS) and No Legal (NL) just fill the form and approve by branch SPV.<br />
     </p>
     </div>
 </div>
    <img src="div2.png" class="divid" />
    <div class="navi">
        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="button2 save" OnClick="btsave_Click" style="left: 0px; top: 0px" />
    </div>
</asp:Content>

