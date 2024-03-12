<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudentIDCardReport.aspx.cs" Inherits="ACADEMIC_StudentIDCardReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updStudent"
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

    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#myTable1').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: false, // Added by Gaurav for Hide pagination

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#myTable1').DataTable().column(idx).visible();
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
                                       var arr = [0];
                                       if (arr.indexOf(idx) !== -1) {
                                           return false;
                                       } else {
                                           return $('#myTable1').DataTable().column(idx).visible();
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
                                           else if ($(node).find("a").length > 0) {
                                               nodereturn = "";
                                               $(node).find("a").each(function () {
                                                   nodereturn += $(this).text();
                                               });
                                           }
                                           else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                               nodereturn = "";
                                               $(node).find("span").each(function () {
                                                   nodereturn += $(this).text();
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
                                           else if ($(node).find("img").length > 0) {
                                               nodereturn = "";
                                           }
                                           else if ($(node).find("input:hidden").length > 0) {
                                               nodereturn = "";
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
                                       var arr = [0];
                                       if (arr.indexOf(idx) !== -1) {
                                           return false;
                                       } else {
                                           return $('#myTable1').DataTable().column(idx).visible();
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
                                           else if ($(node).find("a").length > 0) {
                                               nodereturn = "";
                                               $(node).find("a").each(function () {
                                                   nodereturn += $(this).text();
                                               });
                                           }
                                           else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                               nodereturn = "";
                                               $(node).find("span").each(function () {
                                                   nodereturn += $(this).text();
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
                                           else if ($(node).find("img").length > 0) {
                                               nodereturn = "";
                                           }
                                           else if ($(node).find("input:hidden").length > 0) {
                                               nodereturn = "";
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
                var table = $('#myTable1').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false, // Added by Gaurav for Hide pagination

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#myTable1').DataTable().column(idx).visible();
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
                                           var arr = [0];
                                           if (arr.indexOf(idx) !== -1) {
                                               return false;
                                           } else {
                                               return $('#myTable1').DataTable().column(idx).visible();
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
                                               else if ($(node).find("a").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("a").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                   nodereturn = "";
                                                   $(node).find("span").each(function () {
                                                       nodereturn += $(this).text();
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
                                               else if ($(node).find("img").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else if ($(node).find("input:hidden").length > 0) {
                                                   nodereturn = "";
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
                                           var arr = [0];
                                           if (arr.indexOf(idx) !== -1) {
                                               return false;
                                           } else {
                                               return $('#myTable1').DataTable().column(idx).visible();
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
                                               else if ($(node).find("a").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("a").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                   nodereturn = "";
                                                   $(node).find("span").each(function () {
                                                       nodereturn += $(this).text();
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
                                               else if ($(node).find("img").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else if ($(node).find("input:hidden").length > 0) {
                                                   nodereturn = "";
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

    <asp:UpdatePanel ID="updStudent" runat="server">
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
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Faculty/School Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select Faculty/School Name" InitialValue="0"
                                            ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Intake</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmbatch" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlAdmbatch_SelectedIndexChanged" TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvAdmission" runat="server" ControlToValidate="ddlAdmbatch"
                                            Display="None" ErrorMessage="Please Select Intake" InitialValue="0"
                                            ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="True" TabIndex="3">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup> </sup>
                                            <label>Specialization</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True"
                                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="True" TabIndex="4">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                       <%-- <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Specialization" InitialValue="0" ValidationGroup="show">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Year</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" TabIndex="5" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Year" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>ID Card Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlIdCardType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" TabIndex="6" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlIdCardType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Regular Student</asp:ListItem>
                                            <asp:ListItem Value="2">Hosteller Student</asp:ListItem>
                                            <asp:ListItem Value="3">Transport Student</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvIdCardType" runat="server" ControlToValidate="ddlIdCardType"
                                            Display="None" ErrorMessage="Please Select ID Card Type" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Valid Upto</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar" id="imgCalFromDate"></i>
                                            </div>
                                            <asp:TextBox ID="txtValidUpto" runat="server" TabIndex="7" CssClass="form-control" ValidationGroup="show" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtValidUpto" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtValidUpto" Mask="99/99/9999"
                                                MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                        <div class="label-dynamic">
                                            <label>ID Card Format</label>
                                        </div>
                                        <div class="checkbox">
                                            <asp:CheckBox ID="chkIDCard" runat="server" Checked="false" Text="Default Format" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>Upload Principal Sign</label>
                                        </div>
                                        <asp:Image ID="imgCollegeLogo" runat="server" ImageUrl="~/images/nophoto.jpg" BorderColor="#0099FF"
                                            BorderStyle="Solid" BorderWidth="1px" Width="30%" Height="10%" />
                                        <asp:FileUpload ID="fuRegistrarSign" runat="server" onchange="LoadImage()" />
                                        <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show Students" CssClass="btn btn-outline-info" OnClick="btnShow_Click"
                                    ToolTip="Shows Students under Selected Criteria." ValidationGroup="show" TabIndex="8" />

                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-outline-info" OnClick="btnSubmit_Click"
                                    ToolTip="Submit Students Data." ValidationGroup="submit" TabIndex="8" />

                                <%--<asp:Button ID="btnPrintReport" runat="server" Text="Print Front ID Card" CssClass="btn btn-outline-primary" OnClick="btnPrintReport_Click" ValidationGroup="show" />
                                <asp:Button ID="btnbackReport" runat="server" Text="Print Back ID Card" CssClass="btn btn-outline-primary" ValidationGroup="show" OnClick="btnbackReport_Click" />--%>
                                <asp:Button ID="btnFrontBackReport" runat="server" Text="Print Front/Back ID Card" CssClass="btn btn-outline-info"
                                    ValidationGroup="show" OnClick="btnFrontBackReport_Click" TabIndex="9" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="10"
                                    CssClass="btn btn-outline-danger" ToolTip="Cancel Selected under Selected Criteria." />
                                <asp:ValidationSummary ID="validationsummary" runat="server" ValidationGroup="show" DisplayMode="list" ShowMessageBox="true" ShowSummary="false" />
                            </div>

                            <div class="col-12 btn-footer">
                                <div class="label-dynamic">
                                    <label>Total Selected</label>
                                </div>
                                <asp:TextBox ID="txtTotStud" runat="server" Text="0" Enabled="False" CssClass="watermark form-control"
                                    Style="text-align: center;" Font-Bold="True" Font-Size="Small"></asp:TextBox>
                                <ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" TargetControlID="txtTotStud"
                                    WatermarkText="0" WatermarkCssClass="watermarked" Enabled="True" />
                                <asp:HiddenField ID="hftot" runat="server" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvStudentRecords" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Search Results</h5>
                                            </div>

                                            <table class="table table-striped table-bordered nowrap " style="width: 100%" id="myTable1">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="chkIdentityCard" runat="server" onClick="SelectAll(this);" ToolTip="Select or Deselect All Records" TabIndex="9" />
                                                        </th>
                                                        <th>Application ID
                                                        </th>
                                                        <th>Enrollment No.
                                                        </th>
                                                        <th>Student Name
                                                        </th>
                                                        <th>Mobile
                                                        </th>
                                                        <th>Email
                                                        </th>
                                                        <th>Year<%--Semester--%></th>
                                                        <th>RF ID</th>
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
                                                    <asp:CheckBox ID="chkReport" runat="server" onClick="totSubjects(this);" />
                                                    <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("REGNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ENROLLNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("NAME_WITH_INITIAL")%>
                                                </td>
                                                <td>
                                                    <%# Eval("STUDENTMOBILE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("EMAILID")%>
                                                </td>

                                                <td>
                                                    <%# Eval("YEARNAME")%>
                                                </td>

                                                <td>
                                                    <asp:TextBox ID="txtRfId" runat="server" CssClass="form-control" Text='<%# Eval("RF_ID") %>' Enabled='<%# Eval("RF_ID").ToString().Equals("")  %>'></asp:TextBox>
                                                    <%--Enabled='<%# Eval("RF_ID") %>'--%>
                                                </td>


                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btnFrontBackReport" />
        </Triggers>
    </asp:UpdatePanel>


    <script type="text/javascript">
        function SelectAll(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hftot = document.getElementById('<%= hftot.ClientID %>');
            for (i = 0; i < hftot.value; i++) {

                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_chkReport');
                if (lst.type == 'checkbox') {
                    if (chk.checked == true) {
                        lst.checked = true;
                        txtTot.value = hftot.value;
                    }
                    else {
                        lst.checked = false;
                        txtTot.value = 0;
                    }
                }

            }
        }

        function totSubjects(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;

        }

        function validateAssign() {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>').value;
        if (txtTot == 0) {
            alert('Please Check atleast one student ');
            return false;
        }
        else
            return true;
    }

    function LoadImage() {
        document.getElementById("ctl00_ContentPlaceHolder1_imgCollegeLogo").src = document.getElementById("ctl00_ContentPlaceHolder1_fuCollegeLogo").value;
    }

    </script>


    <script type="text/javascript">
        $(function () {
            //Enable Disable all TextBoxes when Header Row CheckBox is checked.
            $("[id*=chkIdentityCard]").bind("click", function () {
                var chkHeader = $(this);

                //Find and reference the GridView.
                var grid = $(this).closest("table");

                //Loop through the CheckBoxes in each Row.
                $("td", grid).find("input[type=checkbox]").each(function () {

                    //If Header CheckBox is checked.
                    //Then check all CheckBoxes and enable the TextBoxes.
                    if (chkHeader.is(":checked")) {
                        $(this).attr("checked", "checked");
                        var td = $("td", $(this).closest("tr"));
                        td.css({ "background-color": "#D8EBF2" });
                        $("input[type=text]", td).removeAttr("disabled");
                    }
                    else {
                        $(this).removeAttr("checked");
                        var td = $("td", $(this).closest("tr"));
                        td.css({ "background-color": "#FFF" });
                        $("input[type=text]", td).attr("disabled", "disabled");
                    }
                });
            });

            //Enable Disable TextBoxes in a Row when the Row CheckBox is checked.
            $("[id*=chkReport]").bind("click", function () {

                //Find and reference the GridView.
                var grid = $(this).closest("table");

                //Find and reference the Header CheckBox.
                var chkHeader = $("[id*=chkIdentityCard]", grid);

                //If the CheckBox is Checked then enable the TextBoxes in thr Row.
                if (!$(this).is(":checked")) {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#FFF" });
                    $("input[type=text]", td).attr("disabled", "disabled");
                } else {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#D8EBF2" });
                    $("input[type=text]", td).removeAttr("disabled");
                }

                //Enable Header Row CheckBox if all the Row CheckBoxes are checked and vice versa.
                if ($("[id*=chkReport]", grid).length == $("[id*=chkReport]:checked", grid).length) {
                    chkHeader.attr("checked", "checked");
                } else {
                    chkHeader.removeAttr("checked");
                }
            });
        });
    </script>


</asp:Content>
