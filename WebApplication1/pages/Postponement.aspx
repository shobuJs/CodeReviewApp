<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Postponement.aspx.cs" Inherits="EXAMINATION_Projects_Postponement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <script src="https://test-bankofceylon.mtf.gateway.mastercard.com/checkout/version/60/checkout.js" data-error="errorCallback" data-cancel="cancelCallback"></script> 
    <style>
        .badge {
            display: inline-block;
            padding: 5px 10px 7px;
            border-radius: 15px;
            font-size: 100%;
            width: 80px;
        }
        .badge-warning {
            color: #fff !important;
        }
        td .fa-eye
            font-size:18px;
            color: #0d70fd;
        }
        input[type=checkbox], input[type=radio] {
            margin: 0px 5px 0;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updpost"
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
    <asp:UpdatePanel ID="updpost" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                             
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Student ID :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStudent_id" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Student Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStudentn" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Faculty :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblFacultyname" runat="server" Text="" ToolTip="1" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Program :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblPrograms" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Current Semester :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblCurrentSemester" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 mt-3 ">
                                <div class="row">
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Reason for Postponement</label>
                                        </div>
                                        <asp:TextBox ID="txtReasonWithdrawal" runat="server" CssClass="form-control" TextMode="MultiLine" TabIndex="1" />
                                    </div>

                                    <div  class="form-group col-lg-6 col-md-6 col-12" runat="server"  >

                                                    <div class="label-dynamic">
                                                             <label>Upload Supporting Documents</label>
                                                    </div>
                                                    <asp:FileUpload ID="fuDocument" CssClass="fuDocumentX" runat="server" TabIndex="2"
                                                        onkeypress="" onchange="setUploadButtonState()" ClientIDMode="Static"  />
                                                    
                                                </div>


                                    <%--<div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Upload Supporting Documents</label>
                                        </div>
                                        <input type="file" id="myFile" name="filename2">
                                    </div>--%>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Fees Paid :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblfeepaid" runat="server"  Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Balance Fees :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblbalance" runat="server" ToolTip="1" Font-Bold="true"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Expected Refund : 
                                                <asp:LinkButton ID="btnCalculate" runat="server" CssClass="btn btn-outline-info ml-2" Style="font-size: 11px; padding: .175rem .75rem; text-transform: capitalize;">Calculate</asp:LinkButton></b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblRefund" runat="server" Text="4000" Font-Bold="true"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 mt-3">
                                <div class="row">
                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                        <asp:CheckBox ID="chkAbPa" runat="server" Text="I wish to adjust payment, when i will join back the program in future" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Bank Details</h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic"><sup>* </sup>
                                            <label>Bank Name</label>
                                        </div>
                                        <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control" ClientIDMode="Static" TabIndex="3"/>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic"><sup>* </sup>
                                            <label>Branch Name</label>
                                        </div>
                                        <asp:TextBox ID="txtBranchName" runat="server" CssClass="form-control"
                                            ToolTip="Please Enter Branch Name" ControlToValidate="txtBranchName" ClientIDMode="Static" TabIndex="4"/>
                                       
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic"><sup>* </sup>
                                            <label>Account Number</label>
                                        </div>
                                        <asp:TextBox ID="txtAccountNumber" runat="server" CssClass="form-control" MaxLength="10" onkeypress="return onlyDotsAndNumbers(this,event);" ClientIDMode="Static" TabIndex="5" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic"><sup>* </sup>
                                            <label>IFSC Code</label>
                                        </div>
                                        <asp:TextBox ID="txtIFSCCode" runat="server" CssClass="form-control" ClientIDMode="Static" TabIndex="6"/>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmit_Click" ValidationGroup="submit" TabIndex="7" ClientIDMode="Static" ToolTip="Submit">Submit </asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" TabIndex="8">Cancel</asp:LinkButton>
                                 <asp:HiddenField ID="hdsrno" runat="server" />
                            </div>

                             <div class="col-12 table table-responsive">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvBranch" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading" id="dem">
                                                <h5>Postponement List</h5>
                                            </div>

                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable1">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Edit</th>
                                                        <th>Request ID</th>
                                                        <th>Request Date</th>
                                                        <th>Document</th>
                                                        <th>Details</th>
                                                        <th>Status</th>
                                                    </tr>
                                                    <thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnedit" runat="server" ImageUrl="~/IMAGES/edit.png" CommandArgument='<%# Eval("SRNO") %>'
                                                        AlternateText="Edit Record " OnClick="btnedit_Click"
                                                        ToolTip="Edit Record" />
                                                </td>
                                                <td> <%# Eval("SRNO") %></td>
                                                <td><%# Eval("Date") %>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lnkViewDoc" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%# Eval("SRNO") %>' CommandName='<%# Eval("DOCUMENT") %>' OnClick="lnkViewDoc_Click1"><i class="fa fa-eye"></i> View</asp:LinkButton>
                                                </td>
                                                  <td class="text-center"><i class="fa fa-eye" aria-hidden="true" data-toggle="modal" data-target="#veiw"></i></td>
                                                <td class="text-center"><span class="badge badge-success"><%#Eval("ADMIN_APPROVAL") %></span></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                           
                           

                        </div>

                        <!-- View Modal -->
                        <div class="modal" id="veiw">
                            <div class="modal-dialog">
                                <div class="modal-content">

                                    <!-- Modal Header -->
                                    <div class="modal-header">
                                        <h4 class="modal-title">View Details</h4>
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    </div>

                                    <!-- Modal body -->
                                    <div class="modal-body pl-0 pr-0">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-12 col-md-12 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Reason for Postponement</label>
                                                    </div>
                                                    <asp:TextBox ID="txtReason" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false" />
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Fees Paid :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblfee" runat="server" Text="5000" Font-Bold="true"></asp:Label>
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Balance Fees :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblbalancefee" runat="server" Text="5000" ToolTip="1" Font-Bold="true"></asp:Label></a>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Expected Refund :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="Label6" runat="server" Text="4000" Font-Bold="true"></asp:Label></a>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Bank Details</h5>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Bank Name :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblBankName" runat="server"  Font-Bold="true"></asp:Label>
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Branch Name :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblBranchName" runat="server"  ToolTip="1" Font-Bold="true"></asp:Label></a>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Account Number :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblAccountNumber" runat="server"  Font-Bold="true"></asp:Label></a>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>IFSC Code :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblIFSCCode" runat="server"  Font-Bold="true"></asp:Label></a>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>


                    </div>
                </div>
            </div>
             <div id="myModal22" class="modal fade" role="dialog">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="margin-top: -25px">
                        <div class="modal-body">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" style="margin-top:-18px">x</button>
                            </div>

                            <asp:Image ID="ImageViewer" runat="server" Width="100%" Height="500px" Visible ="false"/>
                            <asp:Literal ID="ltEmbed" runat="server" />
                          <%--  <iframe id="iframe1" runat="server" frameborder="0" width="100%" height="800px" visible="false"></iframe>--%>
                            <%--<iframe id="iframe1" runat="server" src="../FILEUPLOAD/Transcript Report.pdf" frameborder="0" width="100%" height="547px"></iframe>--%>                        

                           
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            <%--<asp:PostBackTrigger ControlID="fuDocument" />--%>
        </Triggers>
    </asp:UpdatePanel>


    <script type="text/javascript">
        function setUploadButtonState() {

            var maxFileSize = 1000000;
            var fi = document.getElementById('ctl00_ContentPlaceHolder1_FuChallan');
            for (var i = 0; i <= fi.files.length - 1; i++) {

                var fsize = fi.files.item(i).size;

                if (fsize >= maxFileSize) {
                    alert("Image Size Greater Than 1MB");
                    $("#ctl00_ContentPlaceHolder1_FuChallan").val("");

                }
            }
            var fileExtension = ['jpeg', 'jpg', 'JPG', 'JPEG', 'PNG', 'png'];
            if ($.inArray($('#ctl00_ContentPlaceHolder1_FuChallan').val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                alert("Only formats are allowed : " + fileExtension.join(', '));
                $("#ctl00_ContentPlaceHolder1_FuChallan").val("");
            }
        }

    </script>
     <script>


         var summary = "";
         $(function () {

             $('#btnSave').click(function () {
                 localStorage.setItem("currentId", "#btnSave,Submit");
                 debugger;
                 ShowLoader('#btnSave');
                 if ($('#txtReasonWithdrawal').val() == "")
                     summary += '<br>Please Enter Reason';
                 if ($('#fuDocument').val() == "")
                     summary += '<br>Please Upload Supporting Documents';
                 if ($('#txtBankName').val() == "")
                     summary += '<br>Please Enter Bank Name';
                 if ($('#txtBranchName').val() == "")
                     summary += '<br>Please Enter Branch Name';
                 if ($('#txtAccountNumber').val() == "")
                     summary += '<br>Please Enter Account Number';
                 if ($('#txtIFSCCode').val() == "")
                     summary += '<br>Please Enter IFSC Code';
                

                 if (summary != "") {
                     customAlert(summary);
                     summary = "";
                     return false
                 }

             });
         });

         var prm = Sys.WebForms.PageRequestManager.getInstance();
         prm.add_endRequest(function () {
             $(function () {
                 $('#btnSave').click(function () {
                     localStorage.setItem("currentId", "#btnSave,Submit");
                     ShowLoader('#btnSave');

                     if ($('#txtReasonWithdrawal').val() == "")
                         summary += '<br>Please Enter Reason';
                     if ($('#fuDocument').val() == "")
                         summary += '<br>Please Upload Supporting Documents';
                     if ($('#txtBankName').val() == "")
                         summary += '<br>Please Enter Bank Name';
                     if ($('#txtBranchName').val() == "")
                         summary += '<br>Please Enter Branch Name';
                     if ($('#txtAccountNumber').val() == "")
                         summary += '<br>Please Enter Account Number';
                     if ($('#txtIFSCCode').val() == "")
                         summary += '<br>Please Enter IFSC Code';


                     if (summary != "") {
                         customAlert(summary);
                         summary = "";
                         return false
                     }

                 });
             });
         });
    </script>
      <script>
          function onlyDotsAndNumbers(txt, event) {
              var charCode = (event.which) ? event.which : event.keyCode
              if (charCode == 46) {
                  if (txt.value.indexOf(".") < 0)
                      return true;
                  else
                      return false;
              }

              if (txt.value.indexOf(".") > 0) {
                  var txtlen = txt.value.length;
                  var dotpos = txt.value.indexOf(".");
                  //Change the number here to allow more decimal points than 2
                  if ((txtlen - dotpos) > 2)
                      return false;
              }

              if (charCode > 31 && (charCode < 48 || charCode > 57))
                  return false;

              return true;
          }
     </script>
</asp:Content>

