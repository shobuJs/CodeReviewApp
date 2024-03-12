<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Module_Outline.aspx.cs" Inherits="Projects_Module_Outline" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    5+36<script src='<%=Page.ResolveUrl("~/plugins/TinyMce/jquery.tinymce.min.js") %>'></script><div class="row">
        <asp:HiddenField ID="hdnUserNo" runat="server" />
        <asp:HiddenField ID="hdndeptno" runat="server" />
        <asp:HiddenField ID="hdnmoduleoutlineid" runat="server" />
        <asp:HiddenField ID="hdnidno" runat="server" />

        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
             <%--       <h3 class="box-title">Module Outline</h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                            
                                                      <asp:Label ID="lblDYDept" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlDepartment" runat="server" ControlToValidate="ddlDepartment" Display="None" InitialValue="0"
                                    ErrorMessage="Please Select Department." SetFocusOnError="true" ValidationGroup="submit" />
                            </div>

                            <div class="form-group col-lg-6 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                 <%--   <label>Module</label>--%>
                                      <asp:Label ID="lblRAModule" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlModule" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <%--<asp:ListItem Value="0">Code - Name</asp:ListItem>--%>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlModule" runat="server" ControlToValidate="ddlModule" Display="None" InitialValue="0"
                                    ErrorMessage="Please Select Module." SetFocusOnError="true" ValidationGroup="submit" />
                            </div>

                            <div class="form-group col-lg-9 col-md-12 col-12">
                                <div class="label-dynamic">
                                    <%--<sup>* </sup>--%>
                                    <label> <asp:Label ID="lblRASLModule" runat="server" Font-Bold="true"></asp:Label> Outline</label>
                                </div>
                                <asp:TextBox ID="txtModuleOutline" runat="server" Visible="true" TextMode="MultiLine" ClientIDMode="Static" CssClass="form-control TextBox1"></asp:TextBox>
                                <%-- <asp:RequiredFieldValidator ID="rfvModuleOutline" runat="server" ControlToValidate="txtModuleOutline" Display="None"
                                            ErrorMessage="Please Enter Outline." SetFocusOnError="true" ValidationGroup="submit" />--%>
                            </div>



                            <div class="form-group col-lg-3 col-md-12 col-12">
                                <div class="row">
                                    <div class="form-group col-lg-12 col-md-6 col-12 mb-md-4">

                                        <div class="label-dynamic">
                                          <%--  <sup>* </sup>--%>
                                            <label>Upload Outline Doc. <small style="color: red;">(Upload PDF Only)</small></label>
                                            <asp:Label ID="lblTeacher" runat="server" Font-Bold="true"></asp:Label>

                                        </div>
                                        <div id="Div1" class="logoContainer" runat="server">
                                            <%-- <img src="IMAGES/default-fileupload.png" alt="upload image" runat="server" id="imgUpFile" tabindex="6" />--%>
                                        </div>

                                        <asp:FileUpload ID="fuUploadOutlineDocument" runat="server" ToolTip="Select file to upload"
                                            accept=".pdf" onkeypress="" TabIndex="20" />
                                        <asp:Label ID="lblDocument" runat="server"></asp:Label>
                                       <%-- <asp:Label ID ="lblUploadOutlineDocument" runat="server"></asp:Label>--%>
                                       <%-- <asp:RequiredFieldValidator ID="rfvfuUploadOutlineDocument" runat="server" ControlToValidate="fuUploadOutlineDocument" Display="None"
                                            ErrorMessage="Please Upload file." SetFocusOnError="true" ValidationGroup="submit" />--%>

                                        <br />
                                        <div class="fileContainer sprite pl-1">
                                            <span runat="server" id="upLetter" style="color: red;"
                                                cssclass="form-control" tabindex="7">Upload file </span>

                                            <%-- <span runat="server" id="Span1"
                                                                    cssclass="form-control" tabindex="7">Upload File </span>--%>
                                        </div>
                                        
                                        
                                        <script>
                                            $(document).ready(function () {
                                                $(document).on("click", ".logoContainer", function () {
                                                    $("#ctl00$ContentPlaceHolder1$fuUploadOutlineDocument").click();
                                                });
                                                $(document).on("keydown", ".logoContainer", function () {
                                                    if (event.keyCode === 13) {
                                                        // Cancel the default action, if needed
                                                        event.preventDefault();
                                                        // Trigger the button element with a click
                                                        $("#ctl00$ContentPlaceHolder1$fuUploadOutlineDocument").click();
                                                    }
                                                });
                                            });
                                        </script>
                                    </div>

                                    <div class="form-group col-lg-12 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>With Effect From</label>
                                        </div>
                                        <asp:TextBox ID="dtpEffectFromDate" runat="server" TabIndex="5" CssClass="form-control" Width="100%"
                                            ToolTip="Please Enter  Date" />
                                        <i class="fa fa-calendar input-prefix" aria-hidden="true" style="float: right; margin-top: -23px; margin-right: 12px;"></i>
                                        <asp:RequiredFieldValidator ID="rfvdtpEffectFromDate" runat="server" ControlToValidate="dtpEffectFromDate" Display="None" SetFocusOnError="true" ErrorMessage="Please Select Date" ValidationGroup="submit"></asp:RequiredFieldValidator>


                                    </div>

                                </div>
                            </div>


                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-primary" OnClick="btnSubmit_Click" ValidationGroup="submit">Submit</asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit" />
                           </div>
                            <div class="col-12">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div id="div13" runat="server">

                                            <asp:Panel ID="Panel4" runat="server">
                                                <asp:ListView ID="lvModuleOutline" runat="server">
                                                    <LayoutTemplate>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Edit</th>
                                                                    <th>Department</th>
                                                                    <th>Module</th>
                                                                   <%-- <th>Outline</th>--%>
                                                                    <td>Outline Document</td>
                                                                    <th>With Effect From</th>
                                                                    <th>Added By</th>

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
                                                                <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CommandArgument='<%# Eval("MODULEOUTLINEID")%>' ImageUrl="~/images/edit.gif" ToolTip="Edit Record" OnClick="btnEdit_Click1" /></td>
                                                            <td><%# Eval("DEPTNAME")%> </td>
                                                            <td><%# Eval("COURSE_NAME")%> </td>
                                                         <%--   <td><%# Eval("OUTLINE")%> </td>--%>

                                                            <td>
                                                                <asp:LinkButton ID="btnDownloadDocumentPath" runat="server" CommandArgument='<%# Eval("DOCUMENT_PATH")%>' OnClick="btnDownloadDocumentPath_Click1" Visible='<%# Eval("DOCUMENT_PATH").ToString() == string.Empty ? false:true%>'> <i class="fa fa-download" aria-hidden="true" style="color: #28a745;font-size:20px;"></i></asp:LinkButton>
                                                                <asp:Label ID="lblmsg" runat="server" Visible='<%# Eval("DOCUMENT_PATH").ToString() == string.Empty ? true:false%>' Text="Not Available"></asp:Label>
                                                            </td>
                                                             <%--<td>
                                                                <asp:LinkButton ID="btnDownloadDocumentPath" runat="server" CommandArgument='<%# Eval("DOCUMENT_PATH")%>' OnClick="btnDownloadDocumentPath_Click1" Text='<%# Eval("DOCUMENT_PATH").ToString() == string.Empty ? "aaa":"mmm" %>'> </asp:LinkButton>
                                                                <asp:Label ID="lblmsg" runat="server" Visible='<%# Eval("DOCUMENT_PATH").ToString() == string.Empty ? true:false%>' Text="Not avilable"></asp:Label>
                                                            </td>--%>
                                                            <td><%# Eval("WITH_EFFECT_FROM","{0:dd-MMM-yyyy}")%> </td>
                                                            <td><%# Eval("CREATED_BY")%> </td>
                                                         <%--   <asp:Image ImageUrl='<%# (int)Eval("Enable")==1 ? "yes.gif" : "no.gif" %>' />--%>

                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>

                                        <div id="div3" runat="Server">
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="lvModuleOutline"/>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <!-- TinyMce Script -->
    <%--   <script>
        $(document).ready(function () {
            LoadTinyMCE();
        });
    </script>--%>
    <script>
        $(document).ready(function () {
            LoadTinyMCE();


        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            LoadTinyMCE();

        });

    </script>

    <script>
        function LoadTinyMCE() {
            $('#txtModuleOutline').tinymce({
                script_url: '<%=Page.ResolveUrl("~/plugins/TinyMce/tinymce.min.js")%>',
                placeholder: 'Enter the course content here ...',
                height: 300,
                menubar: 'file edit view insert format tools table tc help',
                plugins: [
                  'advlist autolink lists link image charmap print preview anchor',
                  'searchreplace visualblocks code fullscreen',
                  'insertdatetime media table paste code help wordcount'
                ],
                toolbar: 'undo redo | formatselect | ' +
                'bold italic backcolor | alignleft aligncenter ' +
                'alignright alignjustify | bullist numlist outdent indent | ' +
                'removeformat | help',
                content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:14px }',
                //encoding: 'xml'
                //init_instance_callback: function (editor) {
                //    editor.on('mouseup', function (e) {
                //        alert('okoko');
                //    });
                //}
            });
        }
    </script>

    <!-- Upload PDF Script Script -->
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

      <script type="text/javascript">
        function Focus() {
            //  alert("hii");
            document.getElementById("<%=fuUploadOutlineDocument.ClientID%>").focus();
        }
    </script>

    <script>
        $("input:file").change(function () {
            //$('.fuCollegeLogo').on('change', function () {

            var maxFileSize = 1000000;
            var fi = document.getElementById('ctl00$ContentPlaceHolder1$fuUploadOutlineDocument');
            myfile = $(this).val();
            var ext = myfile.split('.').pop();
            var res = ext.toUpperCase();

            //alert(res)
            //===========zip/rar file upload changes==========
            //if (res != "RAR" && res != "ZIP") {
            //  alert("Please Select rar,zip File Only.");

            if (res != "PDF" && res != "XLSX" && res != "XLS") {
                alert("Please Upload PDF Only.");
                $('.logoContainer img').attr('src', "../IMAGES/excel.png");
                $(this).val('');
            }

            for (var i = 0; i <= fi.files.length - 1; i++) {
                var fsize = fi.files.item(i).size;
                if (fsize >= maxFileSize) {
                    alert("File Size should be less than 1 MB");
                    $('.logoContainer img').attr('src', "../IMAGES/excel.png");
                    $("#ctl00$ContentPlaceHolder1$fuUploadOutlineDocument").val("");

                }
            }

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
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('.logoContainer img').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
        $("input:file").change(function () {
            readURL(this);
        });
    </script>

    <script>
        $(function () {
            $('#ctl00_ContentPlaceHolder1_dtpEffectFromDate').daterangepicker({
                singleDatePicker: true,
                locale: {
                    format: 'DD-MM-YYYY'
                },
            });
            //$('#ctl00_ContentPlaceHolder1_dtpEffectFromDate').val('');

        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {

            $(function () {
                $('#ctl00_ContentPlaceHolder1_dtpEffectFromDate').daterangepicker({
                    singleDatePicker: true,
                    locale: {
                        format: 'DD-MM-YYYY'
                    },
                });
                //$('#ctl00_ContentPlaceHolder1_dtpEffectFromDate').val('');

            });
        });

    </script>
</asp:Content>

