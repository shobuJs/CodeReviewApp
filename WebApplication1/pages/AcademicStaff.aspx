<%@ Page Language="C#"  Title="" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AcademicStaff.aspx.cs" Inherits="ACADEMIC_AcademicStaff" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
          .dataTables_scrollHeadInner {
  width: max-content !important;
}
        </style>



     <script>
         $(document).ready(function () {
             var table = $('#mytable').DataTable({
                 responsive: true,
                 lengthChange: true,
                 scrollY: 450,
                 scrollX: true,
                 scrollCollapse: true,

                 dom: 'lBfrtip',
                 buttons: [
                     {
                         extend: 'colvis',
                         text: 'Column Visibility',
                         columns: function (idx, data, node) {
                             var arr = [-1, 9];
                             if (arr.indexOf(idx) !== -1) {
                                 return false;
                             } else {
                                 return $('#mytable').DataTable().column(idx).visible();
                             }
                         }
                     },
                     {
                         extend: 'collection',
                         text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                         buttons: [
                                 {
                                     extend: 'copyHtml5',
                                     exportOptions: {
                                         columns: function (idx, data, node) {
                                             var arr = [-1, 9];
                                             if (arr.indexOf(idx) !== -1) {
                                                 return false;
                                             } else {
                                                 return $('#mytable').DataTable().column(idx).visible();
                                             }
                                         },
                                         format: {
                                             body: function (data, column, row, node) {
                                                 var nodereturn;
                                                 if ($(node).find("input:text").length > 0) {
                                                     nodereturn = "";
                                                     nodereturn += $(node).find("input:text").eq(0).val();
                                                 }
                                                 else if ($(node).find("input:checkbox").length > 0) {
                                                     nodereturn = "";
                                                     $(node).find("input:checkbox").each(function () {
                                                         if ($(this).is(':checked')) {
                                                             nodereturn += "On";
                                                         } else {
                                                             nodereturn += "Off";
                                                         }
                                                     });
                                                 }
                                                 else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                     nodereturn = "";
                                                     $(node).find("span").each(function () {
                                                         nodereturn += $(this).html();
                                                     });
                                                 }
                                                 else if ($(node).find("select").length > 0) {
                                                     nodereturn = "";
                                                     $(node).find("select").each(function () {
                                                         var thisOption = $(this).find("option:selected").text();
                                                         if (thisOption !== "Please Select") {
                                                             nodereturn += thisOption;
                                                         }
                                                     });
                                                 }
                                                 else {
                                                     nodereturn = data;
                                                 }
                                                 return nodereturn;
                                             },
                                         },
                                     }
                                 },
                                 {
                                     extend: 'excelHtml5',
                                     exportOptions: {
                                         columns: function (idx, data, node) {
                                             var arr = [-1, 9];
                                             if (arr.indexOf(idx) !== -1) {
                                                 return false;
                                             } else {
                                                 return $('#mytable').DataTable().column(idx).visible();
                                             }
                                         },
                                         format: {
                                             body: function (data, column, row, node) {
                                                 var nodereturn;
                                                 if ($(node).find("input:text").length > 0) {
                                                     nodereturn = "";
                                                     nodereturn += $(node).find("input:text").eq(0).val();
                                                 }
                                                 else if ($(node).find("input:checkbox").length > 0) {
                                                     nodereturn = "";
                                                     $(node).find("input:checkbox").each(function () {
                                                         if ($(this).is(':checked')) {
                                                             nodereturn += "On";
                                                         } else {
                                                             nodereturn += "Off";
                                                         }
                                                     });
                                                 }
                                                 else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                     nodereturn = "";
                                                     $(node).find("span").each(function () {
                                                         nodereturn += $(this).html();
                                                     });
                                                 }
                                                 else if ($(node).find("select").length > 0) {
                                                     nodereturn = "";
                                                     $(node).find("select").each(function () {
                                                         var thisOption = $(this).find("option:selected").text();
                                                         if (thisOption !== "Please Select") {
                                                             nodereturn += thisOption;
                                                         }
                                                     });
                                                 }
                                                 else {
                                                     nodereturn = data;
                                                 }
                                                 return nodereturn;
                                             },
                                         },
                                     }
                                 },
                                 {
                                     extend: 'pdfHtml5',
                                     exportOptions: {
                                         columns: function (idx, data, node) {
                                             var arr = [-1, 9];
                                             if (arr.indexOf(idx) !== -1) {
                                                 return false;
                                             } else {
                                                 return $('#mytable').DataTable().column(idx).visible();
                                             }
                                         },
                                         format: {
                                             body: function (data, column, row, node) {
                                                 var nodereturn;
                                                 if ($(node).find("input:text").length > 0) {
                                                     nodereturn = "";
                                                     nodereturn += $(node).find("input:text").eq(0).val();
                                                 }
                                                 else if ($(node).find("input:checkbox").length > 0) {
                                                     nodereturn = "";
                                                     $(node).find("input:checkbox").each(function () {
                                                         if ($(this).is(':checked')) {
                                                             nodereturn += "On";
                                                         } else {
                                                             nodereturn += "Off";
                                                         }
                                                     });
                                                 }
                                                 else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                     nodereturn = "";
                                                     $(node).find("span").each(function () {
                                                         nodereturn += $(this).html();
                                                     });
                                                 }
                                                 else if ($(node).find("select").length > 0) {
                                                     nodereturn = "";
                                                     $(node).find("select").each(function () {
                                                         var thisOption = $(this).find("option:selected").text();
                                                         if (thisOption !== "Please Select") {
                                                             nodereturn += thisOption;
                                                         }
                                                     });
                                                 }
                                                 else {
                                                     nodereturn = data;
                                                 }
                                                 return nodereturn;
                                             },
                                         },
                                     }
                                 },
                         ]
                     }
                 ],
                 "bDestroy": true,
             });
         });
         var parameter = Sys.WebForms.PageRequestManager.getInstance();
         parameter.add_endRequest(function () {
             $(document).ready(function () {
                 var table = $('#mytable').DataTable({
                     responsive: true,
                     lengthChange: true,
                     scrollY: 450,
                     scrollX: true,
                     scrollCollapse: true,

                     dom: 'lBfrtip',
                     buttons: [
                         {
                             extend: 'colvis',
                             text: 'Column Visibility',
                             columns: function (idx, data, node) {
                                 var arr = [-1, 9];
                                 if (arr.indexOf(idx) !== -1) {
                                     return false;
                                 } else {
                                     return $('#mytable').DataTable().column(idx).visible();
                                 }
                             }
                         },
                         {
                             extend: 'collection',
                             text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                             buttons: [
                                     {
                                         extend: 'copyHtml5',
                                         exportOptions: {
                                             columns: function (idx, data, node) {
                                                 var arr = [-1, 9];
                                                 if (arr.indexOf(idx) !== -1) {
                                                     return false;
                                                 } else {
                                                     return $('#mytable').DataTable().column(idx).visible();
                                                 }
                                             },
                                             format: {
                                                 body: function (data, column, row, node) {
                                                     var nodereturn;
                                                     if ($(node).find("input:text").length > 0) {
                                                         nodereturn = "";
                                                         nodereturn += $(node).find("input:text").eq(0).val();
                                                     }
                                                     else if ($(node).find("input:checkbox").length > 0) {
                                                         nodereturn = "";
                                                         $(node).find("input:checkbox").each(function () {
                                                             if ($(this).is(':checked')) {
                                                                 nodereturn += "On";
                                                             } else {
                                                                 nodereturn += "Off";
                                                             }
                                                         });
                                                     }
                                                     else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                         nodereturn = "";
                                                         $(node).find("span").each(function () {
                                                             nodereturn += $(this).html();
                                                         });
                                                     }
                                                     else if ($(node).find("select").length > 0) {
                                                         nodereturn = "";
                                                         $(node).find("select").each(function () {
                                                             var thisOption = $(this).find("option:selected").text();
                                                             if (thisOption !== "Please Select") {
                                                                 nodereturn += thisOption;
                                                             }
                                                         });
                                                     }
                                                     else {
                                                         nodereturn = data;
                                                     }
                                                     return nodereturn;
                                                 },
                                             },
                                         }
                                     },
                                     {
                                         extend: 'excelHtml5',
                                         exportOptions: {
                                             columns: function (idx, data, node) {
                                                 var arr = [-1, 9];
                                                 if (arr.indexOf(idx) !== -1) {
                                                     return false;
                                                 } else {
                                                     return $('#mytable').DataTable().column(idx).visible();
                                                 }
                                             },
                                             format: {
                                                 body: function (data, column, row, node) {
                                                     var nodereturn;
                                                     if ($(node).find("input:text").length > 0) {
                                                         nodereturn = "";
                                                         nodereturn += $(node).find("input:text").eq(0).val();
                                                     }
                                                     else if ($(node).find("input:checkbox").length > 0) {
                                                         nodereturn = "";
                                                         $(node).find("input:checkbox").each(function () {
                                                             if ($(this).is(':checked')) {
                                                                 nodereturn += "On";
                                                             } else {
                                                                 nodereturn += "Off";
                                                             }
                                                         });
                                                     }
                                                     else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                         nodereturn = "";
                                                         $(node).find("span").each(function () {
                                                             nodereturn += $(this).html();
                                                         });
                                                     }
                                                     else if ($(node).find("select").length > 0) {
                                                         nodereturn = "";
                                                         $(node).find("select").each(function () {
                                                             var thisOption = $(this).find("option:selected").text();
                                                             if (thisOption !== "Please Select") {
                                                                 nodereturn += thisOption;
                                                             }
                                                         });
                                                     }
                                                     else {
                                                         nodereturn = data;
                                                     }
                                                     return nodereturn;
                                                 },
                                             },
                                         }
                                     },
                                     {
                                         extend: 'pdfHtml5',
                                         exportOptions: {
                                             columns: function (idx, data, node) {
                                                 var arr = [-1, 9];
                                                 if (arr.indexOf(idx) !== -1) {
                                                     return false;
                                                 } else {
                                                     return $('#mytable').DataTable().column(idx).visible();
                                                 }
                                             },
                                             format: {
                                                 body: function (data, column, row, node) {
                                                     var nodereturn;
                                                     if ($(node).find("input:text").length > 0) {
                                                         nodereturn = "";
                                                         nodereturn += $(node).find("input:text").eq(0).val();
                                                     }
                                                     else if ($(node).find("input:checkbox").length > 0) {
                                                         nodereturn = "";
                                                         $(node).find("input:checkbox").each(function () {
                                                             if ($(this).is(':checked')) {
                                                                 nodereturn += "On";
                                                             } else {
                                                                 nodereturn += "Off";
                                                             }
                                                         });
                                                     }
                                                     else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                         nodereturn = "";
                                                         $(node).find("span").each(function () {
                                                             nodereturn += $(this).html();
                                                         });
                                                     }
                                                     else if ($(node).find("select").length > 0) {
                                                         nodereturn = "";
                                                         $(node).find("select").each(function () {
                                                             var thisOption = $(this).find("option:selected").text();
                                                             if (thisOption !== "Please Select") {
                                                                 nodereturn += thisOption;
                                                             }
                                                         });
                                                     }
                                                     else {
                                                         nodereturn = data;
                                                     }
                                                     return nodereturn;
                                                 },
                                             },
                                         }
                                     },
                             ]
                         }
                     ],
                     "bDestroy": true,
                 });
             });
         });
    </script>
     <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updAcademicStaff"
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
    <asp:UpdatePanel ID="updAcademicStaff" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border"> 
                             <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                             </h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDownloadFormat" runat="server"></asp:Label>
                                        </div>
                                        <asp:LinkButton ID="lbExcelFormat" runat="server" OnClick="lbExcelFormat_Click" TabIndex="1" Font-Underline="true" 
                                            ToolTip="Click Here For Downloading Sample Format" Style="font-weight: bold;" CssClass="stylink">
                                            <span class="btn btn-outline-info">Pre-requisite excel format for upload</span></asp:LinkButton>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYUploadExcelFile" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div id="Div1" class="logoContainer" runat="server">
                                            <img src="../IMAGES/excel.png" alt="upload image" runat="server" id="imgUpFile" TabIndex="2"/>
                                        </div>
                                        <div class="fileContainer sprite pl-1" >
                                            <span runat="server" id="ufFile"
                                               CssClass="form-control" TabIndex="7" >Upload File</span>
                                            <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Select file to upload"
                                                CssClass="form-control" onkeypress=""/>
                                            <asp:RequiredFieldValidator ID="rfvintake" runat="server" ControlToValidate="FileUpload1"
                                                Display="None" ErrorMessage="Please select file to upload." ValidationGroup="Submit"
                                                SetFocusOnError="True" ></asp:RequiredFieldValidator>
                                        </div>
                                        <%--<asp:FileUpload ID="" runat="server" ToolTip="Please Select file to Import" TabIndex="2" />--%>
                                        
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Label ID="lblTotalMsg" Style="font-weight: bold; color: red;" runat="server" TabIndex="3"  ></asp:Label>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnUpload" runat="server" ValidationGroup="Submit" TabIndex="4" 
                                    Text="Upload and Verify" CssClass="btn btn-outline-primary" ToolTip="Click to Upload  & Verify" OnClick="btnUpload_Click" /> 
                                                                          
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                    TabIndex="5" OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" />                         
                                  
                                <asp:ValidationSummary ID="validationsummary3" runat="server"  ValidationGroup="Submit" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <div class="col-12">
                                <div id="divexcelsheet" runat="server" visible="false">                                          
                                    <asp:Panel ID="Panel2" runat="server">
                                        <asp:ListView ID="lvexcelsheet" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5><asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label> List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="mytable">
                                                    <thead class="bg-light-blue"> 
                                                        <tr id="trRow" >
                                                            <th><asp:Label ID="lblSrno" runat="server"></asp:Label></th>
                                                            <th><asp:Label ID="lblUserNo" runat="server"></asp:Label></th>
                                                            <th><asp:Label ID="lblEmployeeName" runat="server"></asp:Label></th>
                                                            <th><asp:Label ID="lblEmployeeCode" runat="server"></asp:Label></th>
                                                            <th><asp:Label ID="lblEmail" runat="server"></asp:Label></th>
                                                            <th><asp:Label ID="lblMobileNo" runat="server"></asp:Label></th>
                                                            <th><asp:Label ID="lblDepartment" runat="server"></asp:Label></th>
                                                            <th><asp:Label ID="lblDesignation" runat="server"></asp:Label></th>
                                                           
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="text-center"><%#Container.DataItemIndex+1 %></td>
                                                    <%--  <td><asp:Label ID="lblUser" runat="server" Text='<%# Eval("UA_FULLNAME")%>' ToolTip='<%# Eval("UA_NO")%>'></asp:Label></td>--%>
                                                    <td><%# Eval("UA_NAME") %> </td>
                                                    <td><%# Eval("UA_FULLNAME") %> </td>
                                                    <td><%# Eval("EMP_CODE") %></td>

                                                    <td><%# Eval("UA_EMAIL") %></td>                                              
                                                    <td><%# Eval("UA_MOBILE") %></td>

                                                    <td><%# Eval("DEPTNAME") %>
                                                        <asp:HiddenField ID="hdnDeptno" runat="server" Visible="false" />

                                                    </td>
                                                    <td><%# Eval("DESIGNATION") %>
                                                         <asp:HiddenField ID="hdnDesigno" runat="server" Visible="false"/>

                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>    
                            </div>   


                                <%--<div class="col-md-12">
                                         <div id="divlvExcel" runat="server" visible="false">                                          
                                                 <asp:Panel ID="Panel1" runat="server" Height="350px" ScrollBars="Auto">
                                                     <asp:ListView ID="lvExcel" runat="server">
                                                         <LayoutTemplate>
                                                             <div>
                                                                 <h4>Excel Records</h4>
                                                                 <table class="table table-striped table-bordered nowrap" id="mytable" style="width:100%">
                                                                         <thead class="bg-light-blue" >
                                                                         <tr id="trRow" >
                                                                             <th class="text-center">Sr.No. </th>
                                                                             <th class="text-center">User Name </th>
                                                                             <th class="text-center">Employee Name </th>
                                                                             <th class="text-center">Employee Code</th>
                                                                             <th class="text-center">Email</th>
                                                                            <%-- <th class="text-center">Department</th>
                                                                      <%--<%--       <th class="text-center">Mobile No. </th>--%>
                                                                        <%-- </tr>
                                                                     </thead>
                                                                     <tbody>
                                                                         <tr id="itemPlaceholder" runat="server" />
                                                                     </tbody>
                                                                 </table>
                                                             </div>
                                                         </LayoutTemplate>
                                                         <ItemTemplate>
                                                             <tr>
                                                                 <td class="text-center"><%#Container.DataItemIndex+1 %></td>--%>
                                                                 
                                                               <%--  <td><asp:Label ID="lblUser" runat="server" Text='<%# Eval("UA_FULLNAME")%>' ToolTip='<%# Eval("UA_NO")%>'></asp:Label></td>--%>
                                                                <%-- <td class="text-center"><%# Eval("UserName") %> </td>
                                                                 <td class="text-center"><%# Eval("EmployeeName") %> </td>
                                                                 <td class="text-center"><%# Eval("EmployeeCode") %></td>
                                                                 <td class="text-center"><%# Eval("Email") %></td>--%>
                                                                <%-- <td class="text-center"><%# Eval("Department") %></td>--%>
                                                                <%-- <td class="text-center"><%# Eval("MobileNo") %></td>--%>
                                                        <%--     </tr>
                                                         </ItemTemplate>
                                                     </asp:ListView>
                                                 </asp:Panel>
                                             </div>
                                         
                                    </div>--%>       
                           
                                                                                                                                                                                               
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger  ControlID="lbExcelFormat" />
            <asp:PostBackTrigger  ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>

     <%-- file upload script add by arpana --%>
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
            document.getElementById("<%=imgUpFile.ClientID%>").focus();
        }
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
            if (res != "PDF" && res != "XLSX" && res != "PNG" && res != "XLS") {
                alert("Please Select pdf,xlsx,XLS File Only.");
                $('.logoContainer img').attr('src', "../IMAGES/excel.png");
                $(this).val('');
            }

            for (var i = 0; i <= fi.files.length - 1; i++) {
                var fsize = fi.files.item(i).size;
                if (fsize >= maxFileSize) {
                    alert("File Size should be less than 1 MB");
                    $('.logoContainer img').attr('src', "../IMAGES/excel.png");
                    $("#ctl00_ContentPlaceHolder1_FileUpload1").val("");

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

</asp:Content>