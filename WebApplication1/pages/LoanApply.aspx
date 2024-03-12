<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="LoanApply.aspx.cs" Inherits="LoanApply" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updloan" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><span>Loan Apply </span></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Student Information</h5>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-12 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Student Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStudentName1" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Student ID :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStdID" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Date Of Birth :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblDOB1" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Gender :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblGender" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Mobile No. :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblMobileNo" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Email ID :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblEmailID" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-6 col-md-12 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Program :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblProgram1" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Semester :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSem" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item text-center">
                                                <asp:Image ID="imgPhoto" runat="server" Width="140px" Height="140px" />
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 mt-3">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Applicable Amount</label>
                                        </div>
                                        <asp:TextBox ID="txtAppAmt" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Requested Loan Amount</label>
                                        </div>
                                        <asp:TextBox ID="TxtLoanAmt" runat="server" CssClass="form-control" onkeyup="IsNumeric(this);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtLoanAmt"
                                            Display="None" ErrorMessage="Please Enter Requested Loan Amount." ValidationGroup="Submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Reason for Loan</label>
                                        </div>
                                        <asp:TextBox ID="TxtLoanReason" runat="server" CssClass="form-control" onkeypress="return alphaOnly(event);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtLoanReason"
                                            Display="None" ErrorMessage="Please Enter Reason for Loan." ValidationGroup="Submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label style="color: black;">Upload  Document</label>
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
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmit_Click" ValidationGroup="Submit">Submit</asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                            </div>

                            <%--<div class="col-12 mt-3">
                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Student ID</th>
                                            <th>Applicable Amount</th>
                                            <th>Requested Loan Amount</th>
                                            <th>Status</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>SLR00008</td>
                                            <td>3,00,000</td>
                                            <td>2,30,000</td>
                                            <td style="color: green !important;">Approved</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>--%>
                            <asp:Panel ID="Panel2" runat="server" Visible="false">
                                <asp:ListView ID="LvLoan" runat="server">
                                    <LayoutTemplate>

                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Sr.No.
                                                    </th>
                                                    <th>Student ID</th>
                                                    <th>Applicable Amount</th>
                                                    <th>Requested Loan Amount</th>
                                                     <th>Requested Loan Reason</th>
                                                    <th>Status</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <%# Container.DataItemIndex + 1 %>
                                            </td>
                                            <td>
                                                <%# Eval("regno")%> 
                                            </td>
                                            <td>
                                                <%# Eval("DEMAND_AMT")%>
                                            </td>
                                            <td>
                                                <%# Eval("LOAN__SCHOL_AMOUNT")%>
                                            </td>
                                          
                                            <td>
                                                <%# Eval("LOAN_SCHOL_REASON")%>
                                            </td>
                                           
                                            <td>
                                                <%# Eval("status")%>
                                                <asp:HiddenField ID="hdfsrno" runat="server" Value=' <%# Eval("SCH_LOAN_NO")%>' />
                                               
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>


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
    <script language="javascript" type="text/javascript">
        function alphaOnly(e) {
            var code;
            if (!e) var e = window.event;

            if (e.keyCode) code = e.keyCode;
            else if (e.which) code = e.which;

            if ((code >= 48) && (code <= 57)) { return false; }
            return true;
        }
    </script>
    <script>
        function IsNumeric(textbox) {
            if (textbox != null && textbox.value != "") {
                if (isNaN(textbox.value)) {
                    //document.getElementById(textbox.id).value = 0;
                    alert("Enter Only Numeric Value")
                    document.getElementById(textbox.id).value = '';
                }
            }
        }
    </script>
</asp:Content>

