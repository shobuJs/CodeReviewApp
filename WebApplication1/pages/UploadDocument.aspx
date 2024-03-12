<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="UploadDocument.aspx.cs" Inherits="ACADEMIC_UploadDocument" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <%--<script type="text/javascript">
      function CheckSize() {

          var maxFileSize = 10000000;

          var fi = document.getElementById('ctl00_PageContent_fuDocument');

          for (var i = 0; i <= fi.files.length - 1; i++) {

              var fsize = fi.files.item(i).size;

              if (fsize >= maxFileSize) {
                  alert("Image Size Greater Than 10MB");
                  $("#ctl00_PageContent_fuDocument").val("");
              }
          }
      }

    </script>--%>

    <script>
        $(document).ready(function () {
            $('.fuDocumentX').on('change', function () {
                myfile = $(this).val();
                var ext = myfile.split('.').pop();
                if (ext != "pdf") {
                    alert("Please Select PDF File Only.");
                    $(this).val('');
                }
            });
        });
    </script>

   <%-- <script>
        $(document).ready(function () {
            $('.photo').on('change', function () {
                myfile = $(this).val();
                var ext = myfile.split('.').pop();
                if (ext != 'jpg') {
                    alert("Please Select JPG File Only.");
                    $(this).val('');
                }
            });
        });
    </script>--%>


<style>
    .ctl00_imgDash {
        display: none;
    }
    </style>
    <%--<div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updSession"
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
    </div>--%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border"> 
                   <%-- <h3 class="box-title">DOCUMENT UPLOAD</h3>--%>
                      <h3 class="box-title"><asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-lg-4 col-md-6 col-12">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item"><b>Enrollment No. :</b>
                                        <a class="sub-label"><asp:Label ID="lblEnroll" Font-Bold="true" runat="server" /> </a>
                                    </li>
                                    <li class="list-group-item"><b>Mother Name :</b>
                                        <a class="sub-label"><asp:Label ID="lblm" Font-Bold="true" runat="server" /></a>
                                    </li>       
                                </ul>
                            </div>
                            <div class="col-lg-4 col-md-6 col-12">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item"><b>Student Name :</b>
                                        <a class="sub-label"><asp:Label ID="lblName" Font-Bold="true" runat="server" /> </a>
                                    </li>
                                    <li class="list-group-item"><b>Mobile Number :</b>
                                        <a class="sub-label"><asp:Label ID="lblmn" Font-Bold="true" runat="server" /></a>
                                    </li>       
                                </ul>
                            </div>
                            <div class="col-lg-4 col-md-6 col-12">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item"><b>Email :</b>
                                        <a class="sub-label"><asp:Label ID="lble" Font-Bold="true" runat="server" /> </a>
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
                                    <label>Degree </label>
                                </div>
                                <asp:DropDownList ID="ddlSchClg" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                    TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlSchClg_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Degree"
                                    ControlToValidate="ddlSchClg" Display="None" InitialValue="0" ValidationGroup="degreeSub"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-lg-9">
                                <div class="row">
                                    <%--<div class="col-12">
                                        <div class="sub-heading"><h5>Upload Documents</h5></div>
                                    </div>--%>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>
                                                <asp:Label ID="lblDoc" runat="server" Visible="false">Document List</asp:Label>
                                                <asp:Label ID="lblUndertaking" runat="server" Visible="false">Upload Undertaking
                                                </asp:Label>
                                            </label>
                                        </div>
                                        <asp:DropDownList ID="ddlDocument" runat="server" AppendDataBoundItems="true"
                                            TabIndex="1" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDocument_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Select value</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvdocument" runat="server" ControlToValidate="ddlDocument"
                                            ValidationGroup="submit" Display="None" SetFocusOnError="true" InitialValue="0"
                                            ErrorMessage="Please Select Document">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                  
                                        <div class="label-dynamic">
                                             <label><asp:Label ID="lblDocUpload" runat="server"></asp:Label></label>
                                        </div>
                                        <asp:FileUpload ID="fuDocument" CssClass="fuDocumentX" runat="server" TabIndex="2" onchange="setUploadButtonStatefEED()" />
                                        <asp:Button ID="btnSaveAndContinue" runat="server" TabIndex="4" Text="UPLOAD" OnClick="btnSaveAndContinue_Click" CssClass="btn btn-outline-info mt-2" />
                                    </div>

                                <div class="form-group col-lg-4 col-md-6 col-12">
                                    <div class="label-dynamic">
                                            <label><asp:Label ID="lblPhotoUpload" runat="server"></asp:Label></label>
                                        </div>
                                        <div id="Div4" class="logoContainer" runat="server">
                                            <img src="../IMAGES/default-fileupload.png" alt="upload image" runat="server" id="img2" TabIndex="6"/>
                                        </div>
                                        <div class="fileContainer sprite pl-1" >
                                            <span runat="server" id="Span1"
                                               CssClass="form-control" TabIndex="7" >Upload File</span>
                                            <asp:FileUpload ID="photoupload" runat="server" ToolTip="Select file to upload"
                                                CssClass="photo" onkeypress="" onchange="setUploadButtonState()" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Photo"
                                                ControlToValidate="photoupload" Display="None" InitialValue="0" ValidationGroup="degreeSub"></asp:RequiredFieldValidator>

                                        </div>
                                        <script>
                                            $(document).ready(function () {
                                                $(document).on("click", ".logoContainer", function () {
                                                    $("#ctl00_ContentPlaceHolder1_photoupload").click();
                                                });
                                                $(document).on("keydown", ".logoContainer", function () {
                                                    if (event.keyCode === 13) {
                                                        // Cancel the default action, if needed
                                                        event.preventDefault();
                                                        // Trigger the button element with a click
                                                        $("#ctl00_ContentPlaceHolder1_photoupload").click();
                                                    }
                                                });
                                            });
                                        </script>


                                        <div class="fileContainer sprite pl-1" >
                                            
                                          <%--<asp:FileUpload ID="" CssClass="photo" runat="server" TabIndex="2"  />--%>
                                     <%--  <asp:Button ID="btnphoto" runat="server" TabIndex="5" Text="PHOTO UPLOAD"  OnClick="btnphoto_Click" CssClass="btn btn-outline-info mt-2" />--%>
                                               
                                         <asp:Image  ID="ImgPhoto" Height="50px" Width="80px" runat="server" Visible="false"  style="display:none;"/>
                                   
                                        </div>
                                        
                                    </div>


                                    <div class="form-group col-lg-4 col-md-12 col-12 d-none" > 
                                        <div class="note-div" id="docmsg" runat="server"> 
                                            <h5 class="heading">Note</h5> 
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Uploading of All Listed Documents Is Mandatory</span> </p>
                                        </div>
                                        <div class="note-div" id="docUnder" runat="server"> 
                                            <h5 class="heading">Note</h5> 
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Uploading of Undertaking Is Mandatory</span> </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 d-none">
                        <div class="row">
                            <div class="col-lg-4 col-md-6 col-12" id="divLastAdmittedPrograme" runat="server" visible="false">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item"><b>Last Admitted Programe :</b>
                                        <a class="sub-label"><asp:Label ID="lblAdmittedProgram1" runat="server" Font-Bold="True" /> </a>
                                    </li>      
                                </ul>
                            </div>
                        </div>
                    </div>

                    <div class="form-group col-md-4 d-none" id="trDegree" runat="server" visible="false">
                        <label>
                            <span style="color: red;">*</span> Degree And College</label>
                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                            Display="None" ErrorMessage="Please Select Degree And College" SetFocusOnError="true" ValidationGroup="Show"></asp:RequiredFieldValidator>
                        <asp:ListBox ID="ddlDegree" runat="server" AppendDataBoundItems="true" TabIndex="3"
                            CssClass="form-control multi-select-demo" SelectionMode="multiple"
                            AutoPostBack="true"></asp:ListBox>

                        <asp:ListBox ID="ddldegreeclg" runat="server" AppendDataBoundItems="true" TabIndex="3"
                            CssClass="form-control multi-select-demo" SelectionMode="multiple"
                            AutoPostBack="true"></asp:ListBox>
                    </div>
                    
                    <div class="form-group d-none">
                        <asp:CheckBoxList ID="CheckBoxUndertaking" runat="server" OnSelectedIndexChanged="CheckBoxUndertaking_SelectedIndexChanged" AutoPostBack="true" Visible="false">
                            <asp:ListItem Text="Upload Documents" Value="1" onclick="MutExChkList(this);" />

                        </asp:CheckBoxList>
                    </div>

                    <div class="mt-2" id="updDocs" runat="server">

                        <asp:UpdatePanel ID="upd" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="row">
                                </div>
                                <div class="row">
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSaveAndContinue" />
                                <asp:PostBackTrigger ControlID="btnSub" />
                            </Triggers>
                        </asp:UpdatePanel>

                        <div class="col-12" id="divAllCoursesFromHist" runat="server">
                            <asp:Panel ID="Panel1" runat="server">
                                <asp:ListView ID="lvDocument" runat="server">
                                    <LayoutTemplate>
                                        <div>
                                            <div class="sub-heading"><h5>Document List</h5></div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="example">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Sr. No.</th>
                                                        <%--  <th style="text-align: center;">Action
                                                        </th>--%>
                                                        <th>Name
                                                        </th>
                                                        <th>File Name
                                                        </th>
                                                        <%--<th style="text-align: center;">
                                                        </th>--%>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr id="trCurRow">
                                            <td>
                                                <%#Container.DataItemIndex + 1 %>
                                            </td>
                                            <%--<td style="width: 5%; text-align: center">
                                                <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("DOCNO")%>'
                                                    AlternateText='<%# Eval("DOCNO") %>' ImageUrl="~/images/delete.gif" ToolTip="Delete"
                                                    OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure want to delete record?')"/>
                                            </td>--%>
                                            <td>
                                                <asp:Label ID="lblname" runat="server" Text='<%#Eval("DOCNAME") %> ' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lbldoc" runat="server" Text='<%#Eval("FILENAME") %> ' ToolTip='<%# Eval("DOCNO") %>'
                                                    CommandArgument='<%#Eval("DOCNO")%>' />
                                            </td>
                                            <%-- <td class="form_button" style="width: 15%; text-align: left">--%>
                                            <%--<asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("FILENAME").ToString())%>' Text="Download"> </asp:HyperLink>--%>
                                            <%--<asp:Button ID="btnDownload" runat="server" CommandArgument='<%#Eval("FILENAME") %>' ToolTip='<%# Eval("DOCNAME") %>'
                                            Text="Download" OnClick="btnDownload_Click" CssClass="btn btn-info" />--%>
                                                <%--  </td>--%>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>

                            </asp:Panel>
                        </div>

                        <div class="col-12 mt-4">
                            <div class="row">
                                <div class="col-lg-4 col-md-6 col-12">
                                    <asp:CheckBoxList ID="CheckUndertaking" runat="server" OnSelectedIndexChanged="CheckUndertaking_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text=" Terms and Conditions" Value="2" />
                                        <%--Text="Undertaking"--%>
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                        </div>

                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnSub" runat="Server" Text="SUBMIT" OnClick="btnSub_Click" ValidationGroup="degreeSub"
                                 CssClass="btn btn-outline-info" />
                            <%--   <asp:LinkButton runat="server" ID="lnkCancelCourse" Font-Bold="true" Text="Cancel Course"
                                OnClick="lnkCancelCourse_Click" ForeColor="Blue"></asp:LinkButton>--%>
                            <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="degreeSub" />
                        </div>

                        <div id="myModal33" class="modal fade" role="dialog">
                            <asp:Panel ID="pnlOTP" runat="server">
                                <div class="modal-dialog">
                                    <!-- Modal content-->
                                    <div class="modal-content">

                                        <div class="modal-body">

                                            <div class="" runat="server" id="undertaking" visible="false">
                                                <div class="form-group col-lg-12 col-md-12 col-12"> 
                                                    <div class="note-div"> 
                                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>I certify that all the information given and documents uploaded are true to the best of my knowledge.</span> </p>
                                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>Furthermore, I undertake to produce originals of all the documents on demand, failing which my admission is liable to be treated as cancelled.</span> </p>
                                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>I hereby agree, if admitted, to abide by the rules and regulations in force, or that may hereafter be made for the administration of the college. 
                                                            I undertake that so long as I am student of the college, I will do nothing either inside or outside the college, that will interfere with its orderly working, discipline and anti-ragging policy. 
                                                            Passing and marks certificates of University Degree Examination / Board Examination / Transfer Certificate from the previous college or university / School leaving certificate / final eligibility certificate of the previous University, as the case may be, will be produced on demand. 
                                                            Otherwise, I am aware the admission shall be provisional or even cancelled. If my admission is cancelled as per the rules and regulations of the college, I shall not have any grievance whatsoever. 
                                                            I hereby undertake to attend minimum 75% of lectures in the college. I am aware that if I fail to do so, my term will not be granted.
                                                            </span> 
                                                        </p>
                                                    </div>
                                                </div>
                                                <div runat="server" style="text-align: center">
                                                    <asp:Button ID="OKButton" runat="server" Text="Close" CssClass="btn btn-outline-danger"/>
                                                </div>
                                            
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>

                    </div>
                </div>
                
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>

    <%--<script type="text/javascript">
          function CheckSize() {

              var maxFileSize = 10000000;

              var fi = document.getElementById('ctl00_PageContent_fuDocument');

              for (var i = 0; i <= fi.files.length - 1; i++) {

                  var fsize = fi.files.item(i).size;

                  if (fsize >= maxFileSize) {
                      alert("Image Size Greater Than 10MB");
                      $("#ctl00_PageContent_fuDocument").val("");

                  }
              }
          }


    </script>--%>

    <script type="text/javascript">
        function MutExChkList(chk) {
            var chkList = chk.parentNode.parentNode.parentNode;
            var chks = chkList.getElementsByTagName("input");
            for (var i = 0; i < chks.length; i++) {
                if (chks[i] != chk && chk.checked) {
                    //  chks[i].checked = false;
                    chks[i].checked = true;
                }
            }
        }

        function setUploadButtonStatefEED() {

            var maxFileSize = 1000000;

            var fi = document.getElementById('ctl00_ContentPlaceHolder1_fuDocument');

            for (var i = 0; i <= fi.files.length - 1; i++) {

                var fsize = fi.files.item(i).size;

                if (fsize >= maxFileSize) {
                    alert("File Size should be less than 1 MB");
                    $("#ctl00_ContentPlaceHolder1_fuDocument").val("");

                }
            }

        }


    </script>
   
    <!-- file upload Start-->
    <script>
        $("#ctl00_ContentPlaceHolder1_photoupload").change(function () {
            //$('.fuCollegeLogo').on('change', function () {

            var maxFileSize = 1000000;
            var fi = document.getElementById('ctl00_ContentPlaceHolder1_photoupload');
            myfile = $(this).val();
            var ext = myfile.split('.').pop();
            var res = ext.toUpperCase();

            //alert(res)
            if (res != "JPG" && res != "JPEG" && res != "PNG") {
                alert("Please Select jpg,jpeg,JPG,JPEG,PNG File Only.");
                $('.logoContainer img').attr('src', "../IMAGES/default-fileupload.png");
                $(this).val('');
            }

            for (var i = 0; i <= fi.files.length - 1; i++) {
                var fsize = fi.files.item(i).size;
                if (fsize >= maxFileSize) {
                    alert("File Size should be less than 1 MB");
                    $('.logoContainer img').attr('src', "../IMAGES/default-fileupload.png");
                    $("#ctl00_ContentPlaceHolder1_photoupload").val("");

                }
            }

        });
    </script>
    <script>
        $("#ctl00_ContentPlaceHolder1_photoupload").change(function () {
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
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('.logoContainer img').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
        $(".fileContainer input:file").change(function () {
            readURL(this);
        });
    </script>
    <!-- file upload END-->

    <%--<script type="text/javascript">
        function setUploadButtonState() {

            var maxFileSize = 50000;
            var fi = document.getElementById('ctl00_ContentPlaceHolder1_photoupload');
            for (var i = 0; i <= fi.files.length - 1; i++) {

                var fsize = fi.files.item(i).size;

                if (fsize >= maxFileSize) {
                    alert("Image Size Greater Than 50kb");
                    $("#ctl00_ContentPlaceHolder1_photoupload").val("");

                }
            }
            var fileExtension = ['jpeg', 'jpg', 'JPG', 'JPEG', 'PNG', 'png'];
            if ($.inArray($('#ctl00_ContentPlaceHolder1_photoupload').val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                alert("Only formats are allowed : " + fileExtension.join(', '));
                $("#ctl00_ContentPlaceHolder1_photoupload").val("");
            }
        }

    </script>--%>
</asp:Content>
