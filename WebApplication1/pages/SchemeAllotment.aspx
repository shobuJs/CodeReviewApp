<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="SchemeAllotment.aspx.cs" Inherits="ACADEMIC_SchemeAllotment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_Panel1 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <script>
        $(document).ready(function () {
            var table = $('#table1').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                //scrollCollapse: true,
                paging: false,
                dom: 'lBfrtip',



                //Export functionality
                buttons: [
                {
                    extend: 'colvis',
                    text: 'Column Visibility',
                    columns: function (idx, data, node) {
                        var arr = [0, 3];
                        if (arr.indexOf(idx) !== -1) {
                            return false;
                        } else {
                            return $('#table1').DataTable().column(idx).visible();
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
                                var arr = [0, 3];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#table1').DataTable().column(idx).visible();
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
                                    else if ($(node).find("img").length > 0) {
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
                                var arr = [0, 3];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#table1').DataTable().column(idx).visible();
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
                                    else if ($(node).find("img").length > 0) {
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
                        extend: 'pdfHtml5',
                        exportOptions: {
                            columns: function (idx, data, node) {
                                var arr = [0, 3];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#table1').DataTable().column(idx).visible();
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
                                    else if ($(node).find("img").length > 0) {
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
            $('#ctl00_ContentPlaceHolder1_lvonlineadm_cbHead').on('click', function () {
                // Get all rows with search applied
                var rows = table.rows({ 'search': 'applied' }).nodes();
                // Check/uncheck checkboxes for all rows in the table
                $('input[type="checkbox"]', rows).prop('checked', this.checked);
            });







            // Handle click on checkbox to set state of "Select all" control
            $('#table1 tbody').on('change', 'input[type="checkbox"]', function () {
                // If checkbox is not checked
                if (!this.checked) {
                    var el = $('#ctl00_ContentPlaceHolder1_lvonlineadm_cbHead').get(0);
                    // If "Select all" control is checked and has 'indeterminate' property
                    if (el && el.checked && ('indeterminate' in el)) {
                        // Set visual state of "Select all" control
                        // as 'indeterminate'
                        el.indeterminate = true;
                    }
                }
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('#table1').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    //scrollCollapse: true,
                    paging: false,
                    dom: 'lBfrtip',



                    //Export functionality
                    buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0, 3];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#table1').DataTable().column(idx).visible();
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
                                    var arr = [0, 3];
                                    if (arr.indexOf(idx) !== -1) {
                                        return false;
                                    } else {
                                        return $('#table1').DataTable().column(idx).visible();
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
                                        else if ($(node).find("img").length > 0) {
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
                                    var arr = [0, 3];
                                    if (arr.indexOf(idx) !== -1) {
                                        return false;
                                    } else {
                                        return $('#table1').DataTable().column(idx).visible();
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
                                        else if ($(node).find("img").length > 0) {
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
                            extend: 'pdfHtml5',
                            exportOptions: {
                                columns: function (idx, data, node) {
                                    var arr = [0, 3];
                                    if (arr.indexOf(idx) !== -1) {
                                        return false;
                                    } else {
                                        return $('#table1').DataTable().column(idx).visible();
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
                                        else if ($(node).find("img").length > 0) {
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
                $('#ctl00_ContentPlaceHolder1_lvonlineadm_cbHead').on('click', function () {
                    // Get all rows with search applied
                    var rows = table.rows({ 'search': 'applied' }).nodes();
                    // Check/uncheck checkboxes for all rows in the table
                    $('input[type="checkbox"]', rows).prop('checked', this.checked);
                });







                // Handle click on checkbox to set state of "Select all" control
                $('#table1 tbody').on('change', 'input[type="checkbox"]', function () {
                    // If checkbox is not checked
                    if (!this.checked) {
                        var el = $('#ctl00_ContentPlaceHolder1_lvonlineadm_cbHead').get(0);
                        // If "Select all" control is checked and has 'indeterminate' property
                        if (el && el.checked && ('indeterminate' in el)) {
                            // Set visual state of "Select all" control
                            // as 'indeterminate'
                            el.indeterminate = true;
                        }
                    }
                });
            });
        });



    </script>
    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updScheme"
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

    <asp:UpdatePanel ID="updScheme" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                             <h3 class="box-title"><asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                           <%-- <h3 class="box-title"><span>CURRICULUM ALLOTMENT</span></h3>--%>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trstype" runat="server" visible="false">
                                        <div class="label-dynamic">
                                      <%--      <label> Curriculum Type</label>--%>
                                                <asp:Label ID="lblCurriculumType" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSType" runat="server" AppendDataBoundItems="True" TabIndex="1" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="clg">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                             <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                           <%-- <label> College / School Name</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Faculty / School Name" ControlToValidate="ddlCollege" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="showstud"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                              <asp:Label ID="lblDegreeAndBranch" runat="server" Font-Bold="true"></asp:Label>
                                            <%--  <label>Degree & Branch</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" data-select2-enable="true" TabIndex="3"
                                            ValidationGroup="showstud" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Degree" ControlToValidate="ddlDegree" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="showstud"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                              <asp:Label ID="lblBatchYear" runat="server" Font-Bold="true"></asp:Label>
                                          <%--  <label> Batch Year</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlBatchYear" runat="server" AppendDataBoundItems="true" AutoPostBack="True" TabIndex="4"
                                            OnSelectedIndexChanged="ddlBatchYear_SelectedIndexChanged" ValidationGroup="showstud" CssClass="form-control" data-select2-enable="true"
                                            Visible="false">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                   <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                       <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label> Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="5"
                                            ValidationGroup="showstud" AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="showstud">
                                        </asp:RequiredFieldValidator>
                                    </div>--%>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                              <asp:Label ID="lblAdmissionBatch" runat="server" Font-Bold="true"></asp:Label>
                                            <%--<label> Admission Batch</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="6" 
                                            ValidationGroup="showstud">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBatch" runat="server" ControlToValidate="ddlAdmBatch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Intake" ValidationGroup="showstud">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                              <asp:Label ID="lblDYSemesterOrYear" runat="server" Font-Bold="true"></asp:Label>
                                           <%-- <label> Semester / Year</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="7"
                                            ValidationGroup="showstud">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Semester / Year" ValidationGroup="showstud">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnShowStudent" runat="server" OnClick="btnShowStudent_Click" TabIndex="8"
                                    Text="Show Student" ValidationGroup="showstud" class="btn btn-outline-info"><i class=" fa fa-eye"></i> Show Student</asp:LinkButton>
                              
                                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-outline-danger" TabIndex="9" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="showstud"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                <div>
                                    <asp:Label ID="lblStatus" runat="server" Font-Bold="true" ForeColor="Red" SkinID="lblmsg" Visible="false" />
                                </div>
                                <div>
                                    <asp:Label ID="lblSch" runat="server" Font-Bold="true" ForeColor="Red" SkinID="lblmsg" Text="Select Curriculum from following list and assign to Selected Students" Visible="false" />
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <asp:Label ID="lbltotalstudent" runat="server" Font-Bold="true"></asp:Label>
                                         <%--   <label> Total Selected Student</label>--%>
                                        </div>
                                        <asp:TextBox ID="txtTotStud" runat="server" CssClass="form-control" Enabled="false" Height="34px"/>
                                        <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <asp:Label ID="lblDYScheme" runat="server" Font-Bold="true"></asp:Label>
                                           <%-- <label> Curriculum</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="10" Enabled="False">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme" Display="None" ErrorMessage="Please Select Curriculum" InitialValue="0" ValidationGroup="assignsch">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:Button ID="btnAssignSch" runat="server" TabIndex="11"  CssClass="btn btn-outline-info" OnClick="btnAssignSch_Click" OnClientClick="return validateAssign();" Text="Allot Curriculum" ValidationGroup="assignsch" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="assignsch" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="btn-footer">
                                    <asp:Label ID="lblStatus2" runat="server" Font-Bold="true" ForeColor="Red" SkinID="Errorlbl" />
                                </div>
                            </div>
                         
                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <table class="table table-striped table-bordered nowrap" style="width:100%" id="table1">

                                    <asp:Repeater ID="lvStudents" runat="server" OnItemDataBound="lvStudents_ItemDataBound"
                                        Visible="false">
                                        <HeaderTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading">
                                                    <h5>Student List</h5>
                                                </div>
                                            </div>
                                                    
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>
                                                        <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" />
                                                    </th>
                                                    <th>Applicant ID.
                                                    </th>
                                                    <th>Registration No
                                                    </th>
                                                    <%--<th>Per Adm. No.
                                                    </th>--%>
                                                    <th>Student Name 
                                                    </th>
                                                    <th>Year / Sem
                                                    </th>
                                                    <th>Intake
                                                    </th>
                                                    <th>Current Curriculum
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <%--<tr id="itemPlaceholder" runat="server" />--%>
                                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%--  Enabled='<%# Eval("SCHEMENO").ToString()!="0"? false:true %>' Checked='<%# Eval("SCHEMENO").ToString()!="0" ? true : false %>'--%>
                                                    <asp:CheckBox ID="cbRow" runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("IDNo")%>' />
                                                </td>
                                                <td>
                                                    <%# Eval("REGNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ENROLLNO")%>
                                                </td>
                                                <%-- <td></td>--%>

                                                <td>
                                                    <%# Eval("NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("YEARSEM")%>
                                                    <%--<%#Convert.ToInt32(Eval("YEARWISE"))==1?Eval("YEARNAME"):Eval("SEMESTERNAME") %>--%>
                                                </td>
                                                <td>
                                                    <%# Eval("BatchName")%>
                                                </td>
                                                <td style="color:#623a00; font-weight:bold">
                                                    <%# Eval("SchemeName")%>
                                                            
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            </tbody>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    </table>
                                </asp:Panel>
                            </div>
                              
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">

        function totSubjects(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;

        }

        function totAllSubjects(headchk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }

            if (headchk.checked == true)
                txtTot.value = hdfTot.value - 2;
            else
                txtTot.value = 0;
        }

        function validateAssign() {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>').value;

            if (txtTot == 0 || document.getElementById('<%= ddlScheme.ClientID %>').selectedIndex == 0) {
                alert('Please Select Atleast One Curriculum/Student from Curriculum/Student List');
                return false;
            }
            else
                return true;
        }

    </script>

</asp:Content>
