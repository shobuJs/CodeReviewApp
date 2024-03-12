<%@ Page Language="C#" Title="" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SignUpExcel.aspx.cs" Inherits="ACADEMIC_SignUpExcel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                            var arr = [0];
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
                                            var arr = [0];
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
                                            var arr = [0];
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
                                            var arr = [0];
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

    $('#ctl00_ContentPlaceHolder1_lvexcelsheet_chkallot').on('click', function () {

        // Get all rows with search applied
        var rows = table.rows({ 'search': 'applied' }).nodes();
        // Check/uncheck checkboxes for all rows in the table
        $('input[type="checkbox"]', rows).prop('checked', this.checked);
    });

            // Handle click on checkbox to set state of "Select all" control
            $('#mytable tbody').on('change', 'input[type="checkbox"]', function () {
                // If checkbox is not checked
                if (!this.checked) {
                    var el = $('#ctl00_ContentPlaceHolder1_lvexcelsheet_chkallot').get(0);
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
                                var arr = [0];
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
                                                var arr = [0];
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
                                                var arr = [0];
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
                                                var arr = [0];
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

    $('#ctl00_ContentPlaceHolder1_lvexcelsheet_chkallot').on('click', function () {

        // Get all rows with search applied
        var rows = table.rows({ 'search': 'applied' }).nodes();
        // Check/uncheck checkboxes for all rows in the table
        $('input[type="checkbox"]', rows).prop('checked', this.checked);
    });

                // Handle click on checkbox to set state of "Select all" control
                $('#mytable tbody').on('change', 'input[type="checkbox"]', function () {
                    // If checkbox is not checked
                    if (!this.checked) {
                        var el = $('#ctl00_ContentPlaceHolder1_lvexcelsheet_chkallot').get(0);
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
     <style>
     

        #ctl00_ContentPlaceHolder1_Panel2 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updSignupexcel"
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

    <asp:UpdatePanel ID="updSignupexcel" runat="server">
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
                                            <label>Download Format</label>
                                        </div>
                                        <asp:LinkButton ID="lbExcelFormat" runat="server" OnClick="lbExcelFormat_Click" TabIndex="1" Font-Underline="true"
                                            ToolTip="Click Here For Downloading Sample Format" Style="font-weight: bold;" CssClass="stylink">
                                            <span class="btn btn-outline-info">Pre-requisite excel format for upload</span></asp:LinkButton>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlcurreentintake" runat="server" TabIndex="1" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" OnSelectedIndexChanged="ddlcurreentintake_SelectedIndexChanged" AutoPostBack="true" 
                                            ToolTip="Please Select Intake">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlcurreentintake"
                                            Display="None" ErrorMessage="Please Select Intake" InitialValue="0"
                                            ValidationGroup="Submit" />
                                      
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYUploadExcelFile" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div id="Div1" class="logoContainer" runat="server">
                                            <img src="../IMAGES/excel.png" alt="upload image" runat="server" id="imgUpFile" tabindex="2" />
                                        </div>
                                        <div class="fileContainer sprite pl-1">
                                            <span runat="server" id="ufFile"
                                                cssclass="form-control" tabindex="7">Upload File</span>
                                            <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Select file to upload"
                                                CssClass="form-control" onkeypress="" />
                                            <asp:RequiredFieldValidator ID="rfvintake" runat="server" ControlToValidate="FileUpload1"
                                                Display="None" ErrorMessage="Please select file to upload." ValidationGroup="Submit"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Label ID="lblTotalMsg" Style="font-weight: bold; color: red;" runat="server" TabIndex="3"></asp:Label>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnUpload" runat="server" ValidationGroup="Submit" TabIndex="4"
                                    Text="Upload and Verify" CssClass="btn btn-outline-primary" ToolTip="Click to Upload  & Verify" OnClick="btnUpload_Click" />
                                <asp:Button ID="btnemail" runat="server"  TabIndex="5" Visible="false"
                                    Text="Send Bulk Email" CssClass="btn btn-outline-primary" ToolTip="Send Bulk Email" OnClick="btnemail_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                    TabIndex="5" OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" />

                                <asp:ValidationSummary ID="validationsummary3" runat="server" ValidationGroup="Submit" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <div class="col-12">
                                <div id="divexcelsheet" runat="server" visible="false">
                                    <asp:Panel ID="Panel2" runat="server">
                                        <asp:ListView ID="lvexcelsheet" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Previous Excel Records</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="mytable">
                                                    <thead class="bg-light-blue">
                                                        <tr id="trRow">
                                                            
                                                          <th style="text-align: center">
                                                           <asp:CheckBox ID="chkallot" runat="server"  />
                                                            </th>
                                                            <th>Sr.No. </th>
                                                            <th>UserName </th>
                                                            <th>Name </th>
                                                            <th>Email Id</th>
                                                            <th>Mobile No</th>
                                                            <%--<th>Home No</th>--%>
                                                            <th>Date Of Birth</th>
                                                            <th>Study Level</th>
                                                            <th>Gender</th>
                                                            <%--<th>Passport No</th>--%>
                                                            <th>NIC</th>
                                                            <th>A/L Index Number</th>
                                                            <%--<th>Email Already Send</th>--%>
                                                          

                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: center">
                                                            <asp:CheckBox ID="chkemail" runat="server" />
                                                        </td>
                                                    <td class="text-center"><%#Container.DataItemIndex+1 %></td>
                                                    <td>
                                                       <%--<%# Eval("LASTNAME") %>--%>
                                                       <asp:Label ID="lblusername" runat="server" Text='<%# Eval("USERNAME")%>' ToolTip='<%# Eval("USERNAME")%>'></asp:Label>
                                                   </td>
                                                   <td>
                                                       <%--<%# Eval("FIRSTNAME") %>--%> 
                                                       <asp:Label ID="lblname" runat="server" Text='<%# Eval("NAME")%>' ToolTip='<%# Eval("NAME")%>'></asp:Label>
                                                   </td>
                                                   
                                                   <td>
                                                       <%--<%# Eval("EMAILID") %>--%> 
                                                       <asp:Label ID="lblemail" runat="server" Text='<%# Eval("EMAILID")%>' ToolTip='<%# Eval("EMAILID")%>'></asp:Label>
                                                   </td>
                                                   <td>
                                                       <%--<%# Eval("MOBILENO") %>--%> 
                                                       <asp:Label ID="lblmobile" runat="server" Text='<%# Eval("MOBILENO")%>' ToolTip='<%# Eval("MOBILENO")%>'></asp:Label>
                                                   </td>
                                                 <%--  <td>
                                                      
                                                       <asp:Label ID="Label4" runat="server" Text='<%# Eval("FIRSTNAME")%>' ToolTip='<%# Eval("FIRSTNAME")%>'></asp:Label>
                                                   </td>--%>
                                                   <td><%# Eval("DOB") %> </td>
                                                   <td><%# Eval("STUDYLEVEL") %> </td>
                                                   <td><%# Eval("GENDER") %> </td>
                                                   <%--<td><%# Eval("PASSPORTNO") %> </td>--%>
                                                   <td><%# Eval("NIC") %> </td>
                                                   <td><%# Eval("ALINDEXNUMBER") %> </td>
                                                   <%--<td><asp:Label ID="lblmeg" runat="server"></asp:Label> </td>--%>

                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                    <div align="center">Record Not Found</div>
                                                </EmptyDataTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lbExcelFormat" />
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>

<%--<script type="text/javascript" language="javascript">

    function checkbox(headchk) {
        var frm = document.forms[0]
        for (i = 0; i < document.forms[0].elements.length; i++) {
            var e = frm.elements[i];
            var s = e.name.split("ctl00$ContentPlaceHolder1$lvexcelsheet$ctrl");
            var b = 'ctl00$ContentPlaceHolder1$lvexcelsheet$ctrl';
            var g = b + s[1];
            if (e.name == g) {
                if (headchk.checked == true)
                    e.checked = true;
                else
                    e.checked = false;
            }
        }
    }

    </script>--%>




    <div class="modal fade" id="ModelEmailPopup">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">Send Mail</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div>
                    <asp:UpdateProgress ID="UpdateProgress19" runat="server" AssociatedUpdatePanelID="updEmailSend"
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
                <asp:UpdatePanel ID="updEmailSend" runat="server">
                    <ContentTemplate>
                        <!-- Modal body -->
                        <div class="modal-body">
                            
                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <label>Subject</label>
                                    <asp:TextBox ID="txtEmailSubject" runat="server" CssClass="form-control">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="True"
                                        ErrorMessage="Please Enter Subject" ControlToValidate="txtEmailSubject" Display="None"
                                        ValidationGroup="EmailSubmit" />
                                </div>
                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <label>Message</label>
                                    <asp:TextBox ID="txtEmailMessage" runat="server" CssClass="form-control" TextMode="MultiLine" MaxLength="350">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" SetFocusOnError="True"
                                        ErrorMessage="Please Enter Message" ControlToValidate="txtEmailMessage" Display="None"
                                        ValidationGroup="EmailSubmit" />
                                </div>
                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <label>Add Attachment</label>
                                    <asp:FileUpload ID="fuAttachFile" runat="server" CssClass="form-control" />
                                </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSendBulkEmail" runat="server" Text="Send Email" CssClass="btn btn-outline-info" ValidationGroup="EmailSubmit"  />
                                <asp:ValidationSummary ID="emailsummary" runat="server" ValidationGroup="EmailSubmit" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSendBulkEmail" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>








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
            if (res != "PDF" && res != "XLSX" && res != "XLS") {
                alert("Please Select pdf,xlsx,xls File Only.");
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

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $("input:file").change(function () {
                //$('.fuCollegeLogo').on('change', function () {

                var maxFileSize = 1000000;
                var fi = document.getElementById('ctl00_ContentPlaceHolder1_FileUpload1');
                myfile = $(this).val();
                var ext = myfile.split('.').pop();
                var res = ext.toUpperCase();

                //alert(res)
                if (res != "PDF" && res != "XLSX" && res != "XLS") {
                    alert("Please Select pdf,xlsx,xls File Only.");
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

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
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

        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ctl00_ContentPlaceHolder1_lnkViewApplication").click(function () {
                //alert('hii')
                $("#ModelEmailPopup").modal();

            });
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(function () {

                $("#ctl00_ContentPlaceHolder1_lnkViewApplication").click(function () {
                    // alert('bye')
                    $("#ModelEmailPopup").modal();
                    $("#ModelEmailPopup").fadeIn();

                });
            });
        });
    </script>
</asp:Content>
