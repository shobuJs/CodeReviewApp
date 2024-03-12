<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SectionAllotment.aspx.cs" Inherits="ACADEMIC_SectionAllotment" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.12.13/xlsx.full.min.js"></script>

    <link href="../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updSection"
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
            var table = $('#tblstd').DataTable({
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
                                return $('#tblstd').DataTable().column(idx).visible();
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
                                            return $('#tblstd').DataTable().column(idx).visible();
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
                                            return $('#tblstd').DataTable().column(idx).visible();
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
                var table = $('#tblstd').DataTable({
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
                                    return $('#tblstd').DataTable().column(idx).visible();
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
                                                return $('#tblstd').DataTable().column(idx).visible();
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
                                                return $('#tblstd').DataTable().column(idx).visible();
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

    <asp:UpdatePanel ID="updSection" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                            <%--<h3 class="box-title"><span>SECTION/CLASS ROLLNO ALLOTMENT</span></h3>--%>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblSessionName" runat="server" Font-Bold="true"></asp:Label>
                                            <%--<label> College & Curriculum </label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlsession" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlsession_SelectedIndexChanged"
                                            ValidationGroup="teacherallot">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlsession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblfaculty" runat="server" Font-Bold="true"></asp:Label>
                                            <%--<label> College & Curriculum </label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlfaculty" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true"
                                            ValidationGroup="teacherallot" OnSelectedIndexChanged="ddlfaculty_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlfaculty"
                                            Display="None" ErrorMessage="Please Select Faculty" InitialValue="0" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDyProgram" runat="server" Font-Bold="true"></asp:Label>
                                            <%--<label> College & Curriculum </label>--%>
                                        </div>
                                        <asp:ListBox ID="lstProgram" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo" AutoPostBack="true" OnSelectedIndexChanged="lstProgram_SelectedIndexChanged"
                                            SelectionMode="multiple" TabIndex="8" ValidationGroup="CourseCreation">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:ListBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="lstProgram"
                                            Display="None" ErrorMessage="Please Select Program" InitialValue="0" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <%--                                    <div class="form-group col-lg-6 col-md-6 col-12" >
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                              <asp:Label ID="lblDYClgReg" runat="server" Font-Bold="true"></asp:Label>
                                            <label> College & Curriculum </label>
                                        </div>
                                        <asp:DropDownList ID="ddlClgReg" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="teacherallot" OnSelectedIndexChanged="ddlClgReg_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvClgReg" runat="server" ControlToValidate="ddlClgReg"
                                            Display="None" ErrorMessage="Please Select Faculty & Curriculum" InitialValue="0" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                    </div>--%>

                                    <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                             <asp:Label ID="lblAdmissionBatch" runat="server" Font-Bold="true"></asp:Label>
                                    
                                        </div>
                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                                            ValidationGroup="teacherallot">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvAdmbatch" runat="server"
                                            ControlToValidate="ddlAdmBatch" Display="None"
                                            ErrorMessage="Please Select Intake" InitialValue="0"
                                            ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                    </div>
                                    --%>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--  <label> Semester / Year </label>--%>
                                            <asp:Label ID="lblDYSemesterOrYear" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                    </div>



                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <%--  <label> Semester / Year </label>--%>
                                            <asp:Label ID="Label1" runat="server" Font-Bold="true">Campus</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCampus" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCampus"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Campus" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <%--  <label> Semester / Year </label>--%>
                                            <asp:Label ID="Label2" runat="server" Font-Bold="true">	Weekday/Weekend</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlweekday" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlweekday"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Weekday/Weekend" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <%--  <label> Semester / Year </label>--%>
                                            <asp:Label ID="Label4" runat="server" Font-Bold="true">Status</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlStudenType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">All Student</asp:ListItem>
                                            <asp:ListItem Value="2">Pending Students</asp:ListItem>
                                            <asp:ListItem Value="3">Completed Student</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-12 col-md-6 col-12" runat="server" >
                                        <div class="label-dynamic">
                                          <label>Short Name</label> <asp:TextBox ID="txtSectionName" runat="server" ></asp:TextBox>
                                           <label> Group No </label> <asp:TextBox ID="txtSectionNo" runat="server" ></asp:TextBox>                                           
                                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtSectionName"
                                            Display="None" ErrorMessage="Please Enter Short Name" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtSectionNo"
                                            Display="None" ErrorMessage="Please Enter Group No" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>--%>
                                            <asp:Label ID="Label3" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                       
                                        <br />
                                        <asp:RadioButton ID="rbAll" runat="server" GroupName="stud" Text="All Students" Checked="True" Visible="false"/>&nbsp;&nbsp;
                                        <asp:RadioButton ID="rbRemaining" runat="server" GroupName="stud" Text="Remaining Students" Visible="false"/>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-6 col-12" runat="server" Visible="false">
                                        <label>Sort By</label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rbAdmNo" runat="server" GroupName="sort" Text="Student ID" Checked="True" />&nbsp;&nbsp;
                                        <asp:RadioButton ID="rbRegNo" runat="server" GroupName="sort" Text="Aplication ID" />&nbsp;&nbsp;
                                        <asp:RadioButton ID="rbStudName" runat="server" GroupName="sort" Text="Name With Initial" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnFilter" runat="server" Text="Filter" ValidationGroup="teacherallot"
                                    CssClass="btn btn-outline-info" OnClick="btnFilter_Click" />
                                <asp:Button ID="bntExcelReport" runat="server" Text="Report" ValidationGroup="teacherallot"
                                    CssClass="btn btn-outline-info" OnClick="bntExcelReport_Click" />
                                <asp:Button ID="btnClear" runat="server" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btnClear_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="teacherallot"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<asp:Label ID="lbltotalstudent" runat="server" Font-Bold="true">Alloted Student</asp:Label>--%>
                                            <asp:Label ID="lblAllotedStudent" runat="server" Font-Bold="true"></asp:Label>
                                            <%--  <label>Total Selected Students</label>--%>
                                        </div>
                                        <asp:TextBox ID="txtTotStud" runat="server" CssClass="form-control watermarked" Enabled="false" Style="text-align: center" />
                                        <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divGroup" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <asp:Label ID="lblDYSection" runat="server" Font-Bold="true"></asp:Label>
                                            <%-- <label> Section</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click"
                                    Text="Submit" CssClass="btn btn-outline-info" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlStudent" runat="server">
                                    <div runat="server" id="DivTotal" visible="false">
                                        <asp:Label ID="Label5" runat="server" Font-Bold="true">Total Student: </asp:Label><asp:Label ID="lblTotal" runat="server" Font-Bold="true"></asp:Label>

                                    </div>
                                    <asp:ListView ID="lvStudents" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                            </div>
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblstd">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            SrNo
                                                        </th>
                                                        <th style="width:100px">
                                                            <asp:CheckBox ID="chkAll" runat="server" Checked="false" onclick="checkAllCheckbox(this);" Visible="false"/>
                                                            <input type="text" ID="txtSelect" Class="form-control Range" maxlength="4" onkeyup="return SelectCheckBox(this);"/>
                                                        </th>
                                                        <th>Student ID
                                                        </th>
                                                        <th>Aplication ID
                                                        </th>
                                                        <th>Name With Initial
                                                        </th>
                                                        <th>Group
                                                        </th>
                                                        <%-- <th style="display:none">Roll No
                                                            </th>--%>
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
                                                    <asp:CheckBox ID="chkStu" runat="server" CssClass="Selectrange"/></td>
                                                <td>
                                                    <%#Eval("ENROLLNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("REGNO")%>
                                                    <asp:Label ID="lblIdno" runat="server" Text='<%# Eval("IDNo")%>' Visible="false"></asp:Label>
                                                    <%--<asp:CheckBox ID="cbRow" runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("IDNo")%>' Visible="false" />--%>
                                                </td>

                                                <td>
                                                    <%# Eval("STUDNAME")%>
                                                </td>
                                                <td>
                                                    <asp:HiddenField ID="hdfSection" runat="server" Value='<%# Eval("SECTIONNO")%>' />

                                                   <asp:Label ID="lblSection" runat="server" Text='<%# Eval("SECTION_NAME")%>'></asp:Label>
                                                </td>
                                                <%--<td style="display:none">
                                                    <asp:TextBox ID="txtRollNo" runat="server" Text='<%# Eval("ROLLNO")%>' CssClass="form-control" Visible="false" />
                                                </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="bntExcelReport" />
            <asp:PostBackTrigger ControlID="lvStudents" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">

        function checkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvStudents$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvStudents$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true) {
                        e.checked = true;

                    }
                    else {
                        e.checked = false;

                    }
                }

            }
        }

    </script>
    <%--   <script>
        function checkAllCheckbox(headchk) {
          var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var txtTot1 = document.getElementById('<%= lblTotal.ClientID %>');
            if (txtTot1 == "" && txtTot == "")
                if (txtTot == txtTot1)
                    headchk.checked = true;

                else {
                    headchk.checked = false;
                    headchk.checked = null;;
                }
        }
    </script>--%>
    <%--  <script language="javascript" type="text/javascript">
        function totAll(chkAll) {

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (chkAll.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>--%>
    <%--    <script type="text/javascript" language="javascript">

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
                txtTot.value = hdfTot.value;
            else
                txtTot.value = 0;
        }

        function validateAssign() {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>').value;

        if (txtTot == 0) {
            alert('Please Select atleast one student/batch from student/batch list');
            return false;
        }
        else
            return true;
    }

    </script>
        <script>
            function toggleSearch(searchBar, table) {
                var tableBody = table.querySelector('tbody');
                var allRows = tableBody.querySelectorAll('tr');
                var val = searchBar.value.toLowerCase();
                allRows.forEach((row, index) => {
                    var insideSearch = row.innerHTML.trim().toLowerCase();
                //console.log('data',insideSearch.includes(val),'searchhere',insideSearch);
                if (insideSearch.includes(val)) {
                    row.style.display = 'table-row';
                }
                else {
                    row.style.display = 'none';
                }



            });
            }



            function test5() {
                var searchBar5 = document.querySelector('#FilterData');
                var table5 = document.querySelector('#MainLeadTable');



                //console.log(allRows);
                searchBar5.addEventListener('focusout', () => {
                    toggleSearch(searchBar5, table5);
            });



            $(".saveAsExcel").click(function () {

                //if (confirm('Do You Want To Apply for New Program?') == true) {
                // return false;
                //}
                var workbook = XLSX.utils.book_new();
                var allDataArray = [];
                allDataArray = makeTableArray(table5, allDataArray);
                var worksheet = XLSX.utils.aoa_to_sheet(allDataArray);
                workbook.SheetNames.push("Test");
                workbook.Sheets["Test"] = worksheet;
                XLSX.writeFile(workbook, "SectionAllotementList.xlsx");
            });
            }



            function makeTableArray(table, array) {
                var allTableRows = table.querySelectorAll('tr');
                allTableRows.forEach(row => {
                   var rowArray = [2];
                if (row.querySelector('td')) {
                    var allTds = row.querySelectorAll('td');
                    allTds.forEach(td => {
                        if (td.querySelector('span')) {
                    rowArray.push(td.querySelector('span').textContent);
                }
                else if (td.querySelector('input')) {
                    rowArray.push(td.querySelector('input').value);
                }
                else if (td.querySelector('select')) {
                    rowArray.push(td.querySelector('select').value);
                }
                else if (td.innerText) {
                    rowArray.push(td.innerText);
                }
                else{
                    rowArray.push('');
                }
            });
            }
            if (row.querySelector('th')) {
                var allThs = row.querySelectorAll('th');
                allThs.forEach(th => {
                    if (th.textContent) {
                rowArray.push(th.textContent);
            }
            else {
                rowArray.push('');
            }
            });
            }
            // console.log(allTds);



            array.push(rowArray);
            });
            return array;
            }
    </script>--%>
   <script type="text/javascript">
       $(document).ready(function () {
           $('.multi-select-demo').multiselect({
               includeSelectAllOption: true,
               maxHeight: 200,
               enableFiltering: true,
               filterPlaceholder: 'Search',
               enableCaseInsensitiveFiltering: true,
           });
       });
       var parameter = Sys.WebForms.PageRequestManager.getInstance();
       parameter.add_endRequest(function () {
           $(document).ready(function () {
               $('.multi-select-demo').multiselect({
                   includeSelectAllOption: true,
                   maxHeight: 200,
                   enableFiltering: true,
                   filterPlaceholder: 'Search',
                   enableCaseInsensitiveFiltering: true,
               });
           });
       });
    </script>
    <script>
        function SelectCheckBox(txt)
        {
                var ValidChars = "0123456789.-";
                var num = true;
                var mChar; var count = 1;
                mChar = txt.value.charAt();
                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt.value = '';
                    alert("Error! Only Number Are Allowed")
                }
                else {
                    $(".Selectrange").each(function (index, value) {

                        var List = $(this).closest("table");
                        var td = $("td", $(this).closest("tr"));
                        var CheckBoxValue = $("[id*=chkStu]", td).is(":checked");
                        var ddlsec = $("[id*=lblSection]", td).html();
                       
                        if ($('.Range').val() != '') {
                            if (ddlsec == '') {
                                if ($('.Range').val() >= count) {
                                    $("[id*=chkStu]", td).prop('checked', true);
                                    count++;
                                }
                                else {
                                    if (ddlsec == '') {
                                        $("[id*=chkStu]", td).prop('checked', false);
                                        count++;
                                    }
                                }
                            }
                        }
                        else {
                            if (ddlsec == '') {
                                $("[id*=chkStu]", td).prop('checked', false);
                                count++;
                            }
                        }
                    });
                }
        }
    </script>
</asp:Content>

