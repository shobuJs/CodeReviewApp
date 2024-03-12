<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ScrunityRegistrationthroughPayment.aspx.cs" Inherits="ACADEMIC_ScrunityRegistrationthroughPayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <script src="https://bankofceylon.gateway.mastercard.com/checkout/version/61/checkout.js" data-error="errorCallback" data-cancel="cancelCallback"></script>

    <script type="text/javascript">
        function errorCallback(error) {
            console.log(JSON.stringify(error));
        }
        function cancelCallback() {
            //console.log('Payment cancelled');
        }
        cancelCallback = "https://sims.sliit.lk/OnlineResponse.aspx"
        function completeCallback(resultIndicator, sessionVersion) {
            //handle payment completion
            completeCallback = "https://sims.sliit.lk/OnlineResponse.aspx"
        }
        //completeCallback = "http://localhost:55158/PresentationLayer/OnlineResponse.aspx"
        Checkout.configure({
            session: {
                id: '<%= Session["ERPPaymentSessionSCRUTINY"] %>'
            },
            interaction: {
                merchant: {
                    name: 'SLIIT',
                    address: {
                        line1: 'Malabe',
                        line2: 'SriLanka'
                    }
                }
            }
        });
    </script>
     <script type="text/javascript" language="javascript">

        function exefunction(chkRedressal) {
            debugger;
            if (document.getElementById("ctl00_ContentPlaceHolder1_ddlrevalseeing").value == "1") {
                var dataRows = document.getElementsByTagName('tr');
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        if (chkRedressal.checked) {
                            document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = Number(document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value) + Number(document.getElementById("ctl00_ContentPlaceHolder1_lblRedressal").innerHTML);
                            break;
                        }
                        else {
                            document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = Number(document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value) - Number(document.getElementById("ctl00_ContentPlaceHolder1_lblRedressal").innerHTML);
                            break;
                        }
                    }
                }
            }
            else if (document.getElementById("ctl00_ContentPlaceHolder1_ddlrevalseeing").value == "2") {
                var dataRows = document.getElementsByTagName('tr');
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        if (chkRedressal.checked) {
                            document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = Number(document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value) + Number(document.getElementById("ctl00_ContentPlaceHolder1_lblpaper").innerHTML);
                            break;
                        }
                        else {
                            document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = Number(document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value) - Number(document.getElementById("ctl00_ContentPlaceHolder1_lblpaper").innerHTML);
                            break;
                        }
                    }
                }
            }

        }
    </script>
   <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updDetails"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div class="loader-container">
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__ball"></div>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div> 
   <asp:UpdatePanel ID="updDetails" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlstart" runat="server">
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title"><asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                            </div>
                            <div class="box-body">
                                <div class="col-12">
                                    <div id="divNote" runat="server" visible="true">
                                        <div class="exam-note">
                                            <h5 class="heading">Note (Steps To Follow For Scrutiny Registration.)</h5>
                                            <p><span>1.</span> Please select the module .</p>
                                            <p><span>2.</span> After Selecting the modules, Click on Submit.</p>
                                            <%--<p><span>3.</span> Then click on Print button to take the print of appiled modules.</p>--%>
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnProceed" runat="server" Text="Proceed to Registration" CssClass="btn btn-success" TabIndex="1" OnClick="btnProceed_Click"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlSearch" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="div_enrollno" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                   <%-- <label>Session</label>--%>
                                                     <asp:Label ID="lblAcademicSemester" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" Font-Bold="true" TabIndex="1">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                </asp:DropDownList>&nbsp;&nbsp;
                                                      <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                      Display="None" ErrorMessage="Please Select Academic Semester" InitialValue="0" ValidationGroup="search"></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                 <%--   <label>Roll No </label>--%>
                                                     <asp:Label ID="lblStudentId" runat="server" Font-Bold="true"></asp:Label> 
                                                </div>

                                                <asp:TextBox ID="txtEnrollno" runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer" id="div_btn" runat="server">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success" ValidationGroup="search"
                                                Text="Show" OnClick="btnSearch_Click" TabIndex="3" />
                                            <asp:Button ID="btnCancel" runat="server"
                                                Text="Clear" CssClass="btn btn-outline-danger" CausesValidation="false"  OnClick="btnCancel_Click" TabIndex="4"/>
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                                ValidationGroup="search" ShowSummary="false" DisplayMode="List"/>
                                        </div>
                                    </div>
                                    <div class="col-md-3" style="margin-top: 20px; display: none">
                                        <asp:Image ID="imgPhoto" runat="server" Width="40%" Height="70%" Visible="false" />
                                    </div>

                                </asp:Panel>
                                <br />
                                <%-- <hr />--%>
                                <div class="col-12" id="divCourses" runat="server" visible="false">
                                    <div id="tblInfo" runat="server">
                                        <div class="row">

                                            <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><%--<b>Student Name :</b>--%>
                                                     <b><asp:Label ID="lblnamewithinitials" runat="server" Font-Bold="true"></asp:Label> :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblName" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><%--<b>College :</b>--%>
                                                      <b><asp:Label ID="lblfaculty" runat="server" Font-Bold="true"></asp:Label> :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblCollege" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                            <asp:HiddenField ID="hdnCollege" Value="" runat="server" />
                                                            <asp:HiddenField ID="hdnEmailID" Value="" runat="server" />
                                                            <asp:HiddenField ID="hdnMobileNo" Value="" runat="server" />
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><%--<b>Admission Batch:</b>--%>
                                                       <b><asp:Label ID="lblIntake" runat="server" Font-Bold="true"></asp:Label> :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblAdmBatch" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                 

                                                </ul>
                                            </div>
                                            <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><%--<b>Father Name :</b>--%>
                                                       <b><asp:Label ID="lbllFatherN" runat="server" Font-Bold="true"></asp:Label> :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblFatherName" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><%--><b>Roll No :</b>--%>
                                                      <b> <asp:Label ID="lblRStudentId" runat="server" Font-Bold="true"></asp:Label> :</b> 
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblEnrollNo" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><%--<b>Current Semester :</b>--%>
                                                      <b><asp:Label ID="lblDYSemester" runat="server" Font-Bold="true"></asp:Label> :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblSemester" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><%--<b>Mother Name :</b>--%>
                                                        <b><asp:Label ID="lblMotherN" runat="server" Font-Bold="true"></asp:Label> :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblMotherName" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><%--<b>Session :</b>--%>
                                                      <b><asp:Label ID="lblAcademicRSemester" runat="server" Font-Bold="true"></asp:Label> :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblsession" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                 <%--   <li class="list-group-item"><b>Program:</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblBranch" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>--%>
                                                       <li class="list-group-item"><%--<b>Program:</b>--%>
                                                          <b><asp:Label ID="lblprogram" runat="server" Font-Bold="true"></asp:Label> :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblScheme" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>

                                                </ul>
                                            </div>
                                        </div>
                                        <div class="row mt-4">

                                            <div id="Div1" class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Apply For</label>
                                                </div>
                                                <asp:DropDownList ID="ddlrevalseeing" runat="server" CssClass="form-control" data-select2-enable="true" 
                                                    AppendDataBoundItems="True"
                                                    ValidationGroup="backsem" TabIndex="3">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Scrutiny</asp:ListItem>
                                                    <%--  <asp:ListItem value="2">Paper Seeing</asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvreval" runat="server" ControlToValidate="ddlrevalseeing"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Redressal/Paper Seeing" ValidationGroup="backsem"></asp:RequiredFieldValidator>
                                            </div>
                                            <div id="Div2" class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Applying For Semester</label>
                                                </div>
                                                <asp:DropDownList ID="ddlRevalRegSemester" runat="server" AutoPostBack="True" data-select2-enable="true" CssClass="form-control" 
                                                    AppendDataBoundItems="True"
                                                    ValidationGroup="backsem" TabIndex="2">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlBackLogSem" runat="server" ControlToValidate="ddlRevalRegSemester"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="backsem"></asp:RequiredFieldValidator>

                                                <asp:HiddenField ID="hdfCategory" runat="server" />
                                                <asp:HiddenField ID="hdfDegreeno" runat="server" />
                                            </div>
                                            <div id="Div3" class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Total Amount To Pay </label>
                                                </div>
                                                <asp:TextBox ID="totamtpay" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:Label ID="lblOrderID" runat="server" CssClass="data_label" Visible="false">0</asp:Label>
                                            </div>

                                            <div id="Div4" class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                <div class=" note-div">
                                                    <h5 class="heading">Note (Scrutiny Fees) </h5>
                                                    <p>
                                                        <i class="fa fa-star" aria-hidden="true"></i><span>Amount for Scrutiny Registration Per modules .</span> <span class="fa fa-inr text-brown">100</span>
                                                        <asp:Label ID="lblpaper" Visible="false" runat="server"></asp:Label>
                                                    </p>

                                                </div>

                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-3 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Selected Module Fee :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblSelectedCourseFee" runat="server" Text="0.00"></asp:Label>
                                                            <asp:HiddenField ID="hdnSelectedCourseFee" runat="server" Value="0" />                                    
                                                             <asp:HiddenField ID="hdncoursefee" runat="server" Value="0" />
                                                            <asp:HiddenField ID="hdfAmount" runat="server" Value="0" />
                                                            <asp:HiddenField ID="hdfServiceCharge" runat="server" Value="0" />
                                                            <asp:HiddenField ID="hdfTotalAmount" runat="server" Value="0" />
                                                        </a>
                                                    </li>
                                                </ul>

                                            </div>

                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnPrintReport" runat="server" Text="Print Reciept" CssClass="btn btn-outline-primary" Font-Bold="true" Visible="false" TabIndex="5" />
                                            <asp:Label ID="lblStatus" runat="server" Visible="false" CssClass="data_label"></asp:Label><p></p>
                                            <asp:Button ID="btnreprint" runat="server" Text="Re-print Challan" CssClass="btn btn-outline-primary" Visible="false" TabIndex="6" />
                                        </div>
                                        <div class="col-12 d-none" style="color: green;">
                                            <b>
                                                <asp:Label ID="lblRegStatus" runat="server" Visible="false" CssClass="data_label"></asp:Label></b>
                                        </div>

                                        <div class="col-12">
                                            <asp:ListView ID="lvCurrentSubjects" runat="server" Visible="false" OnItemDataBound="lvCurrentSubjects_ItemDataBound">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Module List for Scrutiny</h5>
                                                    </div>

                                                    <table id="tblCurrentSubjects" class="table table-hover table-bordered table-striped display" style="width: 100%">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>Sr.No
                                                                </th>
                                                                <th>Select
                                                                </th>
                                                                <th>Module Code
                                                                </th>
                                                                <th>Module Name
                                                                </th>
                                                                <th>Grade Obtained
                                                                </th>
                                                                <th>Status</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>

                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr id="trCurRow" class="item">
                                                        <td><%# Container.DataItemIndex + 1%></td>
                                                        <td>
                                                            <asp:CheckBox ID="chkRedressal" runat="server" onclick="ValidateFeeDetail(),exefunction(this)"   
                                                                 Enabled='<%# Eval("ISACCEPTED").ToString()=="0" &&  Eval("RECHECKORREASS").ToString()=="1" &&  Eval("RECON").ToString()=="1" ? false:true %>'
                                                                 Checked='<%# Eval("ISACCEPTED").ToString() =="0" &&  Eval("RECHECKORREASS").ToString()=="1" ? true:false %>'
                                                                TabIndex="3" />
                                                        </td>
                                                     
                                                        <td>
                                                            <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                            <asp:HiddenField ID="hdnextmark" runat="server" Value='<%# Eval("EXTERMARK") %>' />
                                                            <asp:HiddenField ID="hdnschemeno" runat="server" Value='<%# Eval("SCHEMENO") %>' />
                                                            <asp:HiddenField ID="hdnCAmarks" runat="server" Value='<%# Eval("CA_MARKS") %>' />
                                                            <asp:HiddenField ID="hdfmarks" runat="server" Value='<%# Eval("MARKS") %>' />
                                                            <asp:HiddenField ID="hdfgrade" runat="server" Value='<%# Eval("GRADE") %>' />
                                                            <asp:HiddenField ID="hdfrecon" runat="server" Value='<%# Eval("RECON") %>' />
                                                        </td>
                                                        <td>
                                                            <%#Eval("GRADE") %>
                                                            <asp:HiddenField ID="hdnRevalApprove" runat="server" Value='<%# Eval("REV_APPROVE_STAT") %>' />
                                                            <asp:HiddenField ID="hdnApplied" runat="server" Value='<%# Eval("ISACCEPTED") %>' />
                                                        </td>
                                                        <td>
                                                         <asp:Label runat="server" ID="lblStatus" Font-Bold="true" Text='<%# (Eval("STATUS"))%>' ForeColor='<%# (Convert.ToInt32(Eval("CHANGEBLE") )== 1 ?  System.Drawing.Color.Green : System.Drawing.Color.Red )%>'></asp:Label>

                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>

                                        </div>

                                        <div class="row" id="trNote" runat="server" visible="false">
                                            <div class="col-12">
                                                <span style="font-weight: bold; color: green;">Note:- Appeal Scrutiny Registration will proceed after checking the checkbox for the particular module component.<br />
                                                </span>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit & Pay" Visible="false" TabIndex="7"
                                                CssClass="btn btn-outline-info" OnClick="btnSubmit_Click"/>
                                               <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Visible="false"
                                               Text="Print Slip" ValidationGroup="backsem" CssClass="btn btn-outline-primary d-none" Font-Bold="true" TabIndex="8"/>
                                            <asp:Button ID="btnRemoveList" runat="server" Text="Clear List" Font-Bold="true" Visible="false" TabIndex="9"
                                                CssClass="btn btn-outline-danger"  OnClick="btnRemoveList_Click"/>
                                          <asp:Button ID="btnPayment" runat="server" CssClass="btn btn-outline-info d-none" Visible="false" TabIndex="7" Text="Pay" OnClick="btnPayment_Click"/>
                                        </div>
                                        <br />
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnPrcdToPay" runat="server" Text="Proceed To Pay" Font-Bold="true" TabIndex="10"
                                                CssClass="btn btn-success" />
                                        </div>
                                        <br />                                                                         
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnPayment" />
        </Triggers>
    </asp:UpdatePanel>
        <div class="col-md-12">
        <div id="divMsg" runat="server">
        </div>
        <div id="myModal33" class="modal fade" role="dialog" data-backdrop="static">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4>Online Payment</h4>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="form-group col-xl-12 col-md-12">
                                        <div class="label-dynamic">
                                            <label>Order ID</label>
                                        </div>
                                        <asp:TextBox ID="txtOrderid" TabIndex="1" runat="server" MaxLength="15" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>


                                    <div class="form-group col-xl-12 col-md-12">
                                        <div class="label-dynamic">
                                            <label>Amount to be Paid</label>
                                        </div>
                                        <asp:TextBox ID="txtTotalPayAmount" TabIndex="4" runat="server" MaxLength="15" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>


                                    <div class="form-group col-xl-12 col-md-12">
                                        <div class="label-dynamic">
                                            <label>Service Charge</label>
                                        </div>
                                        <asp:TextBox ID="txtServiceCharge" TabIndex="3" runat="server" MaxLength="15" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-xl-12 col-md-12">
                                        <div class="label-dynamic">
                                            <label>Total to be Paid</label>
                                        </div>
                                        <asp:TextBox ID="txtAmountPaid" TabIndex="2" runat="server" MaxLength="15" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>



                                    <div class="col-12 btn-footer mt-3">
                                        <input type="button" value="Pay with Lightbox" onclick="Checkout.showLightbox();" class="btn btn-outline-info d-none" />
                                        <asp:LinkButton ID="lnkPay" runat="server" CssClass="btn btn-outline-info d-none" Text="Pay" OnClick="lnkPay_Click"></asp:LinkButton>
                                        <input type="button" value="Pay" onclick="Checkout.showPaymentPage();" class="btn btn-outline-info" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
   <script type="text/javascript">
        function ValidateFeeDetail() {
            debugger;
       
            var totCoursefee=0.0;
            var listview = document.getElementById('tblCurrentSubjects');
            var coursefee = document.getElementById('<%= hdncoursefee.ClientID%>').value; 
 
            try {

                for (j = 0; j < listview.rows.length - 1 ; j++) {
                    var chkid = 'ctl00_ContentPlaceHolder1_lvCurrentSubjects_ctrl' + j + '_chkRedressal';

                    if (document.getElementById(chkid).checked) {      
                        var selAmt = 0.0;
                        selAmt = parseInt(coursefee);
                        totCoursefee = totCoursefee + selAmt;                      
                    }                 
                }
  
                document.getElementById('ctl00_ContentPlaceHolder1_lblSelectedCourseFee').innerHTML = totCoursefee;
                document.getElementById('ctl00_ContentPlaceHolder1_hdnSelectedCourseFee').value = totCoursefee;
             
            }
            catch (e) {
                alert(e);
                valid = false;
            }
            return valid;
        }
    </script>
</asp:Content>

