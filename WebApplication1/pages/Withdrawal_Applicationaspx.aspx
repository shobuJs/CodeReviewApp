<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Withdrawal_Applicationaspx.aspx.cs" Inherits="Projects_Withdrawal_Applicationaspx" %>

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
      <style>
        #ctl00_ContentPlaceHolder1_Panel1 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <div>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updwithapp"
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
    <asp:UpdatePanel ID="updwithapp" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Withdrawal / Postponement Application</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Student ID :</b>
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
                                            <li class="list-group-item"><b>Semester :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSemester" runat="server" Text="" Font-Bold="true"></asp:Label></a>
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
                                            <label>Request Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlWithdrawalType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                           <%-- <asp:ListItem Value="1">Admission Withdrawal</asp:ListItem>
                                            <asp:ListItem Value="2">Semester Registration</asp:ListItem>
                                            <asp:ListItem Value="3">Postponement</asp:ListItem>
                                            <asp:ListItem Value="4">Pro-Rata</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="Please Select Request Type"
                                            ControlToValidate="ddlWithdrawalType" InitialValue="0" Display="None" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Reason for Withdrawal/Postponement</label>
                                        </div>
                                        <asp:TextBox ID="txtReasonWithdrawal" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Reason for Withdrawal/Postponement"
                                            ControlToValidate="txtReasonWithdrawal" Display="None" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
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
                                    <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Upload Documents</label>
                                        </div>
                                        <input type="file" id="myFile" name="filename2">
                                    </div>--%>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnSubmitRequest" runat="server" CssClass="btn btn-outline-info" ValidationGroup="submit" OnClick="btnSubmitRequest_Click">Submit Request</asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit" />
                            </div>

                            
                            <div class="col-12 table table-responsive">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvlWithdra" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading" id="dem">
                                                <h5>Withdrawal / Postponement Application List</h5>
                                            </div>

                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable1">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Request ID</th>
                                                        <th>Request Type</th>
                                                        <th>Reason </th>
                                                        <th>Date</th>
                                                        <th>Status</th>
                                                        <th>Remark</th>
                                                        <th>Uploaded Document</th>

                                                        <%--<th>Finance Uploaded Document</th>--%>                                                     
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
                                                <td><%# Eval("REQUEST_TYPE") %></td>
                                                <td><%# Eval("REASON") %>
                                                </td>
                                                <td><%# Eval("STUDENT_APPLIED_DATE") %></td>
                                                <td class="text-center"><span class="badge badge-success"><%#Eval("WITH_POST_APPROVAL") %> </span></td>
                                                <asp:Label ID="lblstatus" Text='<%#Eval("WITH_POST_APPROVAL") %>' runat="server" Visible="false"></asp:Label>
                                                <td>
                                                 <%# Eval("STU_REMARK") %> 
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lnkstuddoc" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%#Eval("SRNO") %>' CommandName='<%#Eval("DOCUMENT") %>' OnClick="lnkstuddoc_Click"><i class="fa fa-eye"></i> View</asp:LinkButton>
                                                    <asp:Label ID="lbladminstatus" runat="server"></asp:Label>
                                                    <asp:Label ID="lblwithrefundstatus" runat="server" Text='<%#Eval("REFUND_WIHDRWAL_STATUS") %>' Visible="false" ></asp:Label>
                                                    <asp:Label ID="lbldocument" runat="server" Text='<%#Eval("DOCUMENT") %>' Visible="false" ></asp:Label>
                                                </td>
                                                <%--<td>
                                                    <asp:LinkButton ID="lnkfinancedoc" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%#Eval("SRNO") %>' CommandName='<%#Eval("DOCUMENT") %>'  OnClick="lnkfinancedoc_Click"><i class="fa fa-eye"></i> View</asp:LinkButton>
                                                </td>--%>
                                                
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
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
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmitRequest" />
        </Triggers>
    </asp:UpdatePanel>
      <script type="text/ecmascript">
          function closepopup() {
              debugger
              newWindow = window.close("", "#myModal22", "width=200,height=100");
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

