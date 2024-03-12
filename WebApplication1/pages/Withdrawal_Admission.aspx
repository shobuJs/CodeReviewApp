<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Withdrawal_Admission.aspx.cs" Inherits="EXAMINATION_Projects_Withdrawal_Admission" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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

        td .fa-eye {
            font-size: 18px;
            color: #0d70fd;
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
                            <h3 class="box-title">Withdrawal Admission</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Application No :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStudentID" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Student Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStudentName" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Faculty/School Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblFaculty" runat="server" Text="" ToolTip="1" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Program :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblProgram" runat="server" Text="" Font-Bold="true"></asp:Label></a>
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

                            <div class="col-12 mt-3">
                                <div class="row">
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label style="color: black;">Reason for Withdrawal</label>
                                        </div>
                                        <asp:TextBox ID="txtReasonWithdrawal" runat="server" CssClass="form-control" TextMode="MultiLine" TabIndex="1" />
                                    </div>
                                    
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label style="color: black;">Upload Supportng Documnet</label>
                                        </div>
                                        <div class="logoContainer">
                                            <img src='<%=Page.ResolveClientUrl("~/IMAGES/default-fileupload.png")%>' alt="upload image" tabindex="2" />
                                        </div>
                                        <div class="fileContainer sprite pl-1">
                                            <span runat="server" id="ufFile"
                                                cssclass="form-control" tabindex="7">Upload File</span>
                                            <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Select file to upload"
                                                CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="rfvintake" runat="server" ControlToValidate="FileUpload1"
                                                Display="None" ErrorMessage="Please select file to upload." ValidationGroup="Submit"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Fees Paid :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblfeepaid" runat="server" Font-Bold="true"></asp:Label>
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
                                                        <asp:Label ID="lblexpectedrefund" runat="server" Font-Bold="true"></asp:Label></a>
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
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Bank Name</label>
                                            </div>
                                            <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control" ClientIDMode="Static" TabIndex="3" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Bank Branch Name</label>
                                            </div>
                                            <asp:TextBox ID="txtBranchName" runat="server" CssClass="form-control" ClientIDMode="Static" TabIndex="4" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Account Number</label>
                                            </div>
                                            <asp:TextBox ID="txtAccountNumber" runat="server" CssClass="form-control" ClientIDMode="Static" TabIndex="5" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>IFSC Code</label>
                                            </div>
                                            <asp:TextBox ID="txtIFSCCode" runat="server" CssClass="form-control" ClientIDMode="Static" TabIndex="6" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:LinkButton ID="btnSubmitRequest" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmitRequest_Click" ClientIDMode="Static" TabIndex="7">Submit Request</asp:LinkButton>
                                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" TabIndex="8">Cancel</asp:LinkButton>
                                </div>

                                <div class="col-12 table table-responsive">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <asp:ListView ID="lvlWithdra" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading" id="dem">
                                                    <h5>Withdrawal Admission List</h5>
                                                </div>

                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable1">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Request ID</th>
                                                            <th>Request Date</th>
                                                            <th>Details</th>
                                                            <th>Status</th>
                                                            <%--<th>Status by Finance</th>--%>
                                                        </tr>
                                                        <thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("SRNO") %></td>
                                                    <td><%# Eval("STUDENT_APPLIED_DATE") %>
                                                    </td>
                                                    <td class="text-center">
                                                        <%--<asp:LinkButton ID="lnkViewDoc" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%#Eval("IDNO") %>' CommandName='<%#Eval("DOCUMENT") %>' OnClick="lnkViewDoc_Click"><i class="fa fa-eye"></i> View</asp:LinkButton>--%>
                                                        <asp:LinkButton ID="lnkViewDoc" runat="server" CommandArgument='<%#Eval("IDNO") %>' CommandName='<%# Eval("SRNO") %>' OnClick="lnkViewDoc_Click"><i class="fa fa-eye" aria-hidden="true" style="color: #0d70fd; font-size: 24px;"></i></asp:LinkButton>
                                                    </td>
                                                    <%--<td class="text-center"><i class="fa fa-eye" aria-hidden="true" data-toggle="modal" data-target="#veiw"></i></td>--%>
                                                    <td class="text-center"><span class="badge badge-success"><%#Eval("FINANCE_APPROVAL") %></span></td>
                                                     <%--<td class="text-center"><span class="badge badge-success"><%#Eval("ADMIN_APPROVAL") %></span></td>--%>
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
                                                            <label style="color: black;">Reason for Withdrawal</label>
                                                        </div>
                                                        <asp:TextBox ID="txtReason" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false" />
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Fees Paid :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblfee" runat="server" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Balance Fees :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblbalancefee" runat="server" ToolTip="1" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Expected Refund :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblrefund" runat="server" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Document :</b>
                                                                <a class="sub-label">
                                                                    <asp:LinkButton ID="lnkViewDoc1" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%#Eval("IDNO") %>' CommandName='<%#Eval("DOCUMENT") %>' OnClick="lnkViewDoc_Click1" Visible="false"><i class="fa fa-eye"></i> View</asp:LinkButton></a>
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
                                                                    <asp:Label ID="lblBankName" runat="server" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Branch Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblBranchName" runat="server" ToolTip="1" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Account Number :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblAccountNumber" runat="server" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>IFSC Code :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblIFSCCode" runat="server" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                        </ul>
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
                                                <%--<button type="button" class="close" data-dismiss="modal" style="margin-top: -18px"  >x</button>--%>
                                                 <button type="button" class="close" data-dismiss="modal">&times;</button>
                                            </div>
                                            <asp:Image ID="ImageViewer" runat="server" Width="100%" Height="500px" Visible="false" />
                                            <asp:Literal ID="ltEmbed" runat="server" />
                                            <%--<iframe id="iframe1" runat="server" src="../FILEUPLOAD/Transcript Report.pdf" frameborder="0" width="100%" height="547px"></iframe>--%>

                                            <%--<div class="modal-footer" style="height: 0px">
                                                <button type="button" style="margin-top: -10px" class="btn btn-default" data-dismiss="modal">Close</button>
                                            </div>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmitRequest" />
            <%--<asp:PostBackTrigger ControlID="fuDocument" />--%>
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/ecmascript">
        function closepopup() {
            debugger
            newWindow = window.close("", "#myModal22", "width=200,height=100");
        }
    </script>

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

            $('#btnSubmitRequest').click(function () {
                localStorage.setItem("currentId", "#btnSubmitRequest,Submit");
                debugger;
                //ShowLoader('#btnSubmitRequest');
                if ($('#txtReasonWithdrawal').val() == "")
                    summary += '<br>Please Enter Reason';
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
                $('#btnSubmitRequest').click(function () {
                    localStorage.setItem("currentId", "#btnSubmitRequest,Submit");
                    ShowLoader('#btnSubmitRequest');

                    if ($('#txtReasonWithdrawal').val() == "")
                        summary += '<br>Please Enter Reason';
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
    <script type="text/javascript" charset="utf-8">

        function ToUpper(ctrl) {

            var t = ctrl.value;

            ctrl.value = t.toUpperCase();

        }
    </script>
    
    <script>
        $(document).ready(function () {
            $(document).on("click", ".logoContainer", function () {
                $("#ctl00_ContentPlaceHolder1_FileUpload1").click();
            });
            $(document).on("keydown", ".logoContainer", function () {
                if (event.keyCode === 13) {
                    // Cancel the default action, if needed
                    event.preventDefault();
                    // Trigger the button element with a click
                    $("#ctl00_ContentPlaceHolder1_FileUpload1").click();
                }
            });
        });
            </script>

            


    <script>
        $("input:file").change(function () {
            var fileName = $(this).val();

            newText = fileName.replace(/fakepath/g, '');
            var newtext1 = newText.replace(/C:/, '');
            //newtext2 = newtext1.replace('//', ''); 
            var result = newtext1.substring(2, newtext1.length);


            if (result.length > 0) {
                $(this).parent().children('span').html(result);
            }
            else {
                $(this).parent().children('span').html("Choose file");
            }
        });
        //file input preview
        //function readURL(input) {
        //    if (input.files && input.files[0]) {
        //        var reader = new FileReader();
        //        reader.onload = function (e) {
        //            $('.logoContainer img').attr('src', e.target.result);
        //        }
        //        reader.readAsDataURL(input.files[0]);
        //    }
        //}
        //$("input:file").change(function () {
        //    readURL(this);
        //});
    </script>
    
    <script>
        $("input:file").change(function () {
            //$('.fuCollegeLogo').on('change', function () {

            var maxFileSize = 1000000;
            var fi = document.getElementById('ctl00_ContentPlaceHolder1_FileUpload1');
            myfile = $(this).val();
            var ext = myfile.split('.').pop();
            var res = ext.toUpperCase();

            //alert(res)
            if (res != "JPG" && res != "JPEG" && res != "PNG" && res != "PDF") {
                alert("Please Select PDF,PNG,JPEG,JPG File Only.");
                //$('.logoContainer img').attr('src', "../IMAGES/default-fileupload.png");
                $(this).val('');
            }

            for (var i = 0; i <= fi.files.length - 1; i++) {
                var fsize = fi.files.item(i).size;
                if (fsize >= maxFileSize) {
                    alert("File Size should be less than 1 MB");
                    //$('.logoContainer img').attr('src', "../IMAGES/default-fileupload.png");
                    $("#ctl00_ContentPlaceHolder1_FileUpload1").val("");

                }
            }

        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $("input:file").change(function () {
                //$('.fuCollegeLogo').on('change', function () {

                var maxFileSize = 1000000;
                var fi = document.getElementById('ctl00_ContentPlaceHolder1_FileUpload1');
                myfile = $(this).val();
                var ext = myfile.split('.').pop();
                var res = ext.toUpperCase();

                //alert(res)
                if (res != "JPG" && res != "JPEG" && res != "PNG" && res != "PDF") {
                    alert("Please Select PDF,PNG,JPEG,JPG File Only.");
                    //$('.logoContainer img').attr('src', "../IMAGES/default-fileupload.png");
                    $(this).val('');
                }

                for (var i = 0; i <= fi.files.length - 1; i++) {
                    var fsize = fi.files.item(i).size;
                    if (fsize >= maxFileSize) {
                        alert("File Size should be less than 1 MB");
                        //$('.logoContainer img').attr('src', "../IMAGES/default-fileupload.png");
                        $("#ctl00_ContentPlaceHolder1_FileUpload1").val("");

                    }
                }

            });
        });
    </script>
</asp:Content>

