<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentApplicant_Preview.aspx.cs" Inherits="Applicant_Preview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../css/ImageViewer.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../css/jquery.verySimpleImageViewer.css">
    <script src="../js/jquery.verySimpleImageViewer.js"></script>
    <style>
        #ctl00_ContentPlaceHolder1_imageViewerContainer {
            max-width: 800px;
            height: 500px;
            margin: 50px auto;
            border: 1px solid #000;
            border-radius: 3px;
        }

        .image_viewer_inner_container {
            overflow: scroll !important;
        }

        #ctl00_ContentPlaceHolder1_imageViewerContainer .jqvsiv_main_image_content {
            text-align: center !important;
        }

            #ctl00_ContentPlaceHolder1_imageViewerContainer .jqvsiv_main_image_content img {
                /*position: initial !important;*/
                z-index: 3;
                cursor: n-resize;
            }
    </style>
    <style>
        .card-header .title {
            font-size: 15px;
            color: #000;
        }

        .card-header .accicon {
            float: right;
            font-size: 20px;
            width: 1.2em;
        }

        .card-header {
            cursor: pointer;
            border-bottom: none;
            padding: .3rem 0.7rem;
        }

        .card {
            border: 1px solid #ddd;
        }

        .card-body {
            border-top: 1px solid #ddd;
            padding: 1.25rem 0rem;
        }

        .card-header:not(.collapsed) .rotate-icon {
            transform: rotate(180deg);
        }

        td .fa-eye {
            font-size: 18px;
            color: #0d70fd;
        }

        td .fa-download {
            font-size: 18px;
            color: green;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Applicant Preview</h3>
                </div>

                <div class="box-body">
                    <div class="col-12 btn-footer d-none">
                        <asp:LinkButton ID="btnBack" runat="server" CssClass="btn btn-outline-danger">Back</asp:LinkButton>
                    </div>

                    <div class="accordion" id="accordionExample">
                        <div class="card">
                            <div class="card-header" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true">
                                <span class="title">Offer Acceptance </span>
                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                            </div>
                            <div id="collapseOne" class="collapse show">
                                <div class="card-body">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Application No. :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblRegNo" Font-Bold="true" runat="server" />
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>Student Name :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblStudName" Font-Bold="true" runat="server" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Gender :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblSex" Font-Bold="true" runat="server" /></a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Year :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblYear" Font-Bold="true" runat="server" />
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>Intake :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblBatch" Font-Bold="true" runat="server" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Semester :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblSemester" Font-Bold="true" runat="server" /></a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Mobile No. :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblMobileNo" Font-Bold="true" runat="server" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Email :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblEmailID" Font-Bold="true" runat="server" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Student Type :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblStudentType" Font-Bold="true" runat="server" /></a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <div class="sub-heading">
                                                <h5>Program</h5>
                                            </div>
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="col-lg-4 col-md-8 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Faculty/College :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblFaculty" Font-Bold="true" runat="server" />
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-5 col-md-8 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Program :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblProgram" Font-Bold="true" runat="server" />
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-3 col-md-8 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Program Acceptance Date :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblAcceptanceDate" Font-Bold="true" runat="server" />
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divRecieptType" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Receipt Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlReceiptType" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="true" Enabled="false">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="DIVPRGM" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Program Name</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlProgramName" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlProgramName" InitialValue="0"
                                                        Display="None" ErrorMessage="Please Select Program Name" ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                                </div>


                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divsemester" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Semester</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSemester" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class="card">
                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseThree" aria-expanded="false">
                                <span class="title">Documents Uploaded</span>
                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                            </div>
                            <div id="collapseThree" class="collapse show">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="updDocument"
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
                                <asp:UpdatePanel ID="updDocument" runat="server">
                                    <ContentTemplate>
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-12 col-lg-12" id="divAllCoursesFromHist" runat="server">
                                                    <asp:Panel ID="Panel6" runat="server">
                                                        <asp:ListView ID="lvDocument" runat="server">
                                                            <LayoutTemplate>
                                                                <div>
                                                                    <div class="table-responsive">
                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="table1">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th>Sr. No.</th>
                                                                                    <th>Document Name</th>
                                                                                    <th>Mandatory</th>
                                                                                    <th>View</th>
                                                                                    <th>Status</th>
                                                                                    <th>Remark</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr id="trCurRow">
                                                                    <td>
                                                                        <%#Container.DataItemIndex + 1 %>
                                                                        <asp:Label ID="lblDocNo" runat="server" Text='<%#Eval("DOCUMENTNO") %>' Visible="false"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblname" runat="server" Text='<%#Eval("DOCUMENTNAME") %>' /><br />
                                                                        <asp:Label ID="lblImageFile" runat="server" Style="color: red"></asp:Label>
                                                                        <asp:Label ID="lblFileFormat" runat="server" Style="color: red"></asp:Label>
                                                                        <asp:Label ID="lblmandatory" runat="server" Visible="false" Text='<%#Eval("MANDATORY") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblMande" runat="server" Text='<%#Eval("MANDATORY") %>' />
                                                                        <asp:HiddenField runat="server" ID="hdfMandatory" Value='<%#Eval("MANDATORY") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:LinkButton ID="lnkViewDoc" runat="server" CssClass="btn btn-outline-white" CommandArgument='<%#Eval("DOCUMENTNO") %>' CommandName='<%#Eval("DOC_FILENAME") %>' OnClick="lnkViewDoc_Click" Visible="false"><i class="fa fa-eye"></i></asp:LinkButton>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            <asp:ListItem Value="1">Verified</asp:ListItem>
                                                                            <asp:ListItem Value="2">Not Verified</asp:ListItem>
                                                                            <asp:ListItem Value="3">Pending</asp:ListItem>
                                                                            <asp:ListItem Value="4">Incomplete</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("DOC_STATUS") %>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblDocFileName" runat="server" Text='<%#Eval("DOC_FILENAME") %>' Visible="false"></asp:Label>

                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtremark" runat="server" CssClass="form-control" MaxLength="150" Text='<%#Eval("DOC_REMARK") %>'></asp:TextBox>
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
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnDocumentVarify" runat="server" CssClass="btn btn-outline-info" OnClick="btnDocumentVarify_Click">Submit</asp:LinkButton>
                            </div>
                        </div>


                        <div class="card">
                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseFive" aria-expanded="false">
                                <span class="title">Payment Details</span>
                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                            </div>

                            <div id="collapseFive" class="collapse show">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updFees"
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
                                <asp:UpdatePanel ID="updFees" runat="server">
                                    <ContentTemplate>
                                        <div class="card-body">
                                            <div class="col-12">
                                                <asp:Panel ID="pnlStudentsFees" runat="server">
                                                    <asp:ListView ID="lvStudentFees" runat="server">
                                                        <LayoutTemplate>
                                                            <div>
                                                             
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblHead">
                                                                    <thead class="bg-light-blue">
                                                                        <tr id="trRow">
                                                                            <th>UserName
                                                                            </th>
                                                                            <th>Amount
                                                                            </th>
                                                                             <th>Paid Amount
                                                                            </th>
                                                                            <th>Date
                                                                            </th>
                                                                            <th>Receipt No
                                                                            </th>
                                                                            <th>Payment Status
                                                                            </th>
                                                                            <th>View
                                                                            </th>
                                                                        </tr>

                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblEnrollmentno" runat="server" Text='<%#Eval("USERNAME") %>'></asp:Label>
                                                                    <asp:Label ID="lblIdno" runat="server" Text='<%#Eval("USERNO") %>' Visible="false"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("TOTAL_AMT") %>'></asp:Label>
                                                                </td>
                                                                 <td>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("PAID_AMOUNT") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblDate" runat="server" Text='<%#Eval("RECON_DATE") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblReceipt" runat="server" Text='<%#Eval("RECEIPTNO") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("PAY_STATUS") %>' ForeColor='<%# Convert.ToInt32(Eval("RECON")) == 1 ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkViewSlip" runat="server" CommandArgument='<%#Eval("DCR_NO") %>' OnClick="lnkViewSlip_Click" CommandName='<%#Eval("DOC_FILENAME") %>' CssClass="btn btn-outline-info" Visible='<%#Eval("DOC_FILENAME").ToString() == "" ? false:true %>'>
                                                                 <i class="fa fa-search" aria-hidden="true"></i> View  
                                                                    </asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>

                                                </asp:Panel>



                                            </div>

                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div>
                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updfinalblock"
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
                    <asp:UpdatePanel ID="updfinalblock" runat="server">
                        <ContentTemplate>
                            <div class="col-12 mt-4" runat="server">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Enrollment Confirmation</h5>
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Confirmation Status : </b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblstatusConfi" runat="server" Text="" Font-Bold="true">Not Confirm</asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Enrollment No :</b>
                                                <a class="sub-label">
                                                    <label class="float-start"><span></span></label>
                                                    <asp:Label ID="lblEnrollmentno" runat="server" Text="" Font-Bold="true">-</asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer" runat="server" id="DivButton" visible="false">
                                <asp:LinkButton ID="lnkFinalSubmit" runat="server" CssClass="btn btn-outline-info d-none">Submit</asp:LinkButton>
                                <asp:LinkButton ID="lnkSendEmail" runat="server" CssClass="btn btn-outline-info" OnClick="lnkSendEmail_Click" ValidationGroup="btnSubmit">Send Admission Confirmation</asp:LinkButton>
                                <asp:LinkButton ID="lnkCertiAdmis" runat="server" CssClass="btn btn-outline-info" OnClick="lnkCertiAdmis_Click" Visible="false">Certificate of Admission</asp:LinkButton>
                                <asp:LinkButton ID="lnkGeneratereport" runat="server" CssClass="btn btn-outline-info d-none" OnClick="lnkGeneratereport_Click" Visible="false">Print Enrollment Form</asp:LinkButton>
                                <asp:LinkButton ID="lnkPrintReport" runat="server" CssClass="btn btn-outline-primary d-none" OnClick="lnkPrintReport_Click" Visible="false">Summary Sheet</asp:LinkButton>
                                <asp:LinkButton ID="btnRefundInitiated" runat="server" CssClass="btn btn-outline-primary d-none" OnClick="btnRefundInitiated_Click" Visible="false">Refund</asp:LinkButton>
                                <asp:Button ID="btnFrontBackReport" runat="server" Text="Print Front/Back ID Card" CssClass="btn btn-outline-primary d-none"
                                    ValidationGroup="show" Visible="false" OnClick="btnFrontBackReport_Click" TabIndex="9" />
                                <asp:LinkButton ID="LinkButton4" runat="server" CssClass="btn btn-outline-danger d-none">Back</asp:LinkButton>
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="btnSubmit" />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lnkPrintReport" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="updModel" runat="server">
        <ContentTemplate>
            <div id="myModal22" class="modal fade" role="dialog" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="margin-top: -25px">
                        <div class="modal-body">
                            <div class="modal-header">
                                <asp:LinkButton ID="lnkClose" runat="server" CssClass="close" Style="margin-top: -18px" OnClick="lnkClose_Click">x</asp:LinkButton>
                                <%--<button type="button" class="close" data-dismiss="modal" style="margin-top: -18px">x</button>--%>
                            </div>

                            <asp:Image ID="ImageViewer" runat="server" Width="100%" Height="500px" Visible="false" />
                            <asp:Literal ID="ltEmbed" runat="server" Visible="false" />
                            <div id="imageViewerContainer" runat="server" visible="false"></div>
                            <asp:HiddenField ID="hdfImagePath" runat="server" />
                            <iframe style="width: 100%; height: 500px;" id="irm1" src="~/PopUp.aspx" runat="server"></iframe>
                            <%--<iframe id="iframe1" runat="server" frameborder="0" width="100%" height="800px" visible="false"></iframe>--%>
                            <%--<iframe id="iframe1" runat="server" src="../FILEUPLOAD/Transcript Report.pdf" frameborder="0" width="100%" height="547px"></iframe>--%>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkClose" />
        </Triggers>
    </asp:UpdatePanel>
    <script>
        $(document).ready(function () {
            var curect_file_path = $('#ctl00_ContentPlaceHolder1_hdfImagePath').val();
            $("#ctl00_ContentPlaceHolder1_imageViewerContainer").verySimpleImageViewer({
                imageSource: curect_file_path,
                frame: ['100%', '100%'],
                maxZoom: '900%',
                zoomFactor: '10%',
                mouse: true,
                keyboard: true,
                toolbar: true,
                rotateToolbar: true
            });
        });
    </script>

    <script>
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var curect_file_path = $('#ctl00_ContentPlaceHolder1_hdfImagePath').val();
                $("#ctl00_ContentPlaceHolder1_imageViewerContainer").verySimpleImageViewer({
                    imageSource: curect_file_path,
                    frame: ['100%', '100%'],
                    maxZoom: '900%',
                    zoomFactor: '10%',
                    mouse: true,
                    keyboard: true,
                    toolbar: true,
                    rotateToolbar: true
                });
            });
        });
    </script>
    <script>
        $(document).ready(function () {
            $(".right-container-button").hover(function () {
                $(".long-text").addClass("show-long-text");
            }, function () {
                $(".long-text").removeClass("show-long-text");
            });
        });
    </script>
    <script language="javascript" type="text/javascript">
        function CheckNumeric(e) {

            if (window.event) // IE 
            {
                if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8) {
                    event.returnValue = false;
                    return false;

                }
            }
            else { // Fire Fox
                if ((e.which < 48 || e.which > 57) & e.which != 8) {
                    e.preventDefault();
                    return false;

                }
            }
        }

    </script>
    <script>
        $(document).ready(function () {
            $("[id*=ddlstatus]").bind("change", function () {
                var List = $(this).closest("table");
                var ddlValue = $(this).val();
                if (ddlValue == "1") {
                    var td = $("td", $(this).closest("tr"));
                    $("[id*=txtremark]", td).val("Verified");

                }

                else if (ddlValue == "2") {
                    var td = $("td", $(this).closest("tr"));
                    $("[id*=txtremark]", td).val("Not Verified");
                }
                else if (ddlValue == "3") {
                    var td = $("td", $(this).closest("tr"));
                    $("[id*=txtremark]", td).val("Pending");
                }
                else if (ddlValue == "4") {
                    var td = $("td", $(this).closest("tr"));
                    $("[id*=txtremark]", td).val("Incomplete");
                }
                else {
                    var td = $("td", $(this).closest("tr"));
                    $("[id*=txtremark]", td).val('');


                }
            });
        });
    </script>

    <script>
        $(document).ready(function () {
            var parameter = Sys.WebForms.PageRequestManager.getInstance();
            parameter.add_endRequest(function () {
                $("[id*=ddlstatus]").bind("change", function () {
                    var List = $(this).closest("table");
                    var ddlValue = $(this).val();
                    if (ddlValue == "1") {
                        var td = $("td", $(this).closest("tr"));
                        $("[id*=txtremark]", td).val("Verified");

                    }

                    else if (ddlValue == "2") {
                        var td = $("td", $(this).closest("tr"));
                        $("[id*=txtremark]", td).val("Not Verified");
                    }
                    else if (ddlValue == "3") {
                        var td = $("td", $(this).closest("tr"));
                        $("[id*=txtremark]", td).val("Pending");
                    }
                    else if (ddlValue == "4") {
                        var td = $("td", $(this).closest("tr"));
                        $("[id*=txtremark]", td).val("Incomplete");
                    }
                    else {
                        var td = $("td", $(this).closest("tr"));
                        $("[id*=txtremark]", td).val('');


                    }
                });
            });
        });
    </script>
    <script type="text/javascript">
        function setUploadButtondoc(chk) {
            var maxFileSize = 1000000;
            var fi = document.getElementById(chk.id);
            var tabValue = $(chk).attr('TabIndex');

            for (var i = 0; i <= fi.files.length - 1; i++) {
                var fsize = fi.files.item(i).size;
                if (fsize >= maxFileSize) {
                    alert("Document Size Greater Than 1MB");
                    $(chk).val("");
                }
            }
            var fileExtension1 = ['pdf'];

            if ($.inArray($(chk).val().replace(',', '.').split('.').pop().toLowerCase(), fileExtension1) == -1) {
                alert("Only formats are allowed : " + fileExtension1.join(', '));
                $(chk).val("");
            }
        }
    </script>
</asp:Content>

