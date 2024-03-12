<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="DocumentMapping.aspx.cs" Inherits="ACADEMIC_DocumentMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>
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
        //$("#ctl00_ContentPlaceHolder1_ddlDegree").multiselect('rebuild');
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
        $(document).ready(function () {

            //$('.display thead tr')
            //   .clone(true)
            //   .addClass('filters')
            //   .appendTo('.display thead');

            var table = $('#divdocumentlist').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: false,
                dom: 'lBfrtip',


                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#divdocumentlist').DataTable().column(idx).visible();
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
                                            var arr = [];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#divdocumentlist').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                                {
                                    extend: 'excelHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#divdocumentlist').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                                {
                                    extend: 'pdfHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#divdocumentlist').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                        ]
                    }
                ],
                "bDestroy": true,
                //order: "desc",
            });
            $('#ctl00_ContentPlaceHolder1_lvMeritList_cbHead').on('click', function () {

                // Get all rows with search applied
                var rows = table.rows({ 'search': 'applied' }).nodes();
                // Check/uncheck checkboxes for all rows in the table
                $('input[type="checkbox"]', rows).prop('checked', this.checked);
            });

            // Handle click on checkbox to set state of "Select all" control
            $('#divdocumentlist tbody').on('change', 'input[type="checkbox"]', function () {
                // If checkbox is not checked
                if (!this.checked) {
                    var el = $('#ctl00_ContentPlaceHolder1_lvMeritList_cbHead').get(0);
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

                //$('.display thead tr')
                //   .clone(true)
                //   .addClass('filters')
                //   .appendTo('.display thead');

                var table = $('#divdocumentlist').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    //paging: false,
                    dom: 'lBfrtip',


                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#divdocumentlist').DataTable().column(idx).visible();
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
                                                var arr = [];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#divdocumentlist').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                                    {
                                        extend: 'excelHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#divdocumentlist').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                                    {
                                        extend: 'pdfHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#divdocumentlist').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                            ]
                        }
                    ],
                    "bDestroy": true,
                    //order : "desc",
                });
                $('#ctl00_ContentPlaceHolder1_lvMeritList_cbHead').on('click', function () {

                    // Get all rows with search applied
                    var rows = table.rows({ 'search': 'applied' }).nodes();
                    // Check/uncheck checkboxes for all rows in the table
                    $('input[type="checkbox"]', rows).prop('checked', this.checked);
                });

                // Handle click on checkbox to set state of "Select all" control
                $('#divdocumentlist tbody').on('change', 'input[type="checkbox"]', function () {
                    // If checkbox is not checked
                    if (!this.checked) {
                        var el = $('#ctl00_ContentPlaceHolder1_lvMeritList_cbHead').get(0);
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
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updGradeEntry"
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


    <%--<script>
        function getVal() {
            var array = []
            var docsNo;
            var checkboxes = document.querySelectorAll('input[type=checkbox]:checked')

            for (var i = 0; i < checkboxes.length; i++) {
                //array.push(checkboxes[i].value)    
                if (docsNo == undefined) {
                    docsNo = checkboxes[i].value + ',';
                }
                else {
                    docsNo += checkboxes[i].value + ',';
                }
            }
            //alert(degreeNo);
            $('#<%= hdndocsno.ClientID %>').val(docsNo);
            //document.getElementById(inpHide).value = "degreeNo";
        }
    </script>--%>

    <style>
        div.dd_chk_select {
            height: 35px;
            font-size: 14px !important;
            padding-left: 12px !important;
            line-height: 2.2 !important;
            width: 100%;
        }

            div.dd_chk_select div#caption {
                height: 35px;
            }
    </style>

    <script type="text/javascript">
        document.onreadystatechange = function () {
            var state = document.readyState
            if (state == 'interactive') {
                document.getElementById('contents').style.visibility = "hidden";
            } else if (state == 'complete') {
                setTimeout(function () {
                    document.getElementById('interactive');
                    document.getElementById('load').style.visibility = "hidden";
                    document.getElementById('contents').style.visibility = "visible";
                }, 1000);
            }
        }
    </script>

    <div id="contents">
        <%--This is testing--%>
    </div>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <%--      <h3 class="box-title"><strong>DOCUMENT MAPPING</strong> </h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <asp:Label ID="lblDYSession" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlcurreentintake" runat="server" TabIndex="1" CssClass="form-control select2 select-click"
                                    AppendDataBoundItems="True"
                                    ToolTip="Please Select Intake">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlcurreentintake"
                                    Display="None" ErrorMessage="Please Select Intake" InitialValue="0"
                                    ValidationGroup="Submit" />

                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <%--  <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label> College/School Name</label>
                                </div>--%>
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true"
                                    AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"
                                    ToolTip="Please Select Faculty /School Name" TabIndex="1">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                    Display="None" ErrorMessage="Please Select Faculty /School Name" ValidationGroup="submit"
                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-6 col-md-9 col-12">
                                <%--<div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Degree</label>
                                </div>--%>
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <asp:Label ID="lblDYDegree" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <%--   <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" multiple="multiple"
                                    AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                                    ToolTip="Please Select Degree" TabIndex="2">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>--%>
                                <asp:ListBox ID="ddlDegree" runat="server" CssClass="form-control multi-select-demo" SelectionMode="multiple" AppendDataBoundItems="true"></asp:ListBox>
                                <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please Select Degree" ValidationGroup="submit"
                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                <%-- <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Document</label>
                                </div>--%>
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <asp:Label ID="lblDYDocument" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <%--data-select2-enable="true"--%>
                                <asp:DropDownList ID="ddlDocument" runat="server" CssClass="form-control"
                                    multiple="multiple" TabIndex="3">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDocument"
                                    Display="None" ErrorMessage="Please Select Document" ValidationGroup="submit"
                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-12" id="divAllCoursesFromHist" runat="server">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvDocument" runat="server">
                                        <LayoutTemplate>
                                            <div>
                                                <div class="sub-heading">
                                                    <h5>Document List</h5>
                                                </div>
                                                <div class="table-responsive" style="max-height: 300px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="example">
                                                        <thead class="bg-light-blue" style="position: sticky; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                            <tr>
                                                                <%--<th style="text-align: center">
                                                                <asp:CheckBox ID="cbHead" runat="server" OnClick="checkAllCheckbox(this)" />
                                                            </th>--%>
                                                                <th style="text-align: center">Select
                                                                </th>
                                                                <th>Sr. No.</th>
                                                                <th>Document Name
                                                                </th>
                                                                <th>Predefine File
                                                                </th>
                                                                <th style="text-align: center">Mandatory
                                                                </th>
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
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="chkall" runat="server" />

                                                </td>
                                                <td>
                                                    <%#Container.DataItemIndex + 1 %>
                                                    <asp:Label ID="lblDocNo" runat="server" Text='<%#Eval("DOCUMENTNO") %>' Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblname" runat="server" Text='<%#Eval("DOCUMENTNAME") %>' /><br />
                                                    <asp:Label ID="lblImageFile" runat="server" Style="color: red"></asp:Label>
                                                    <asp:Label ID="lblFileFormat" runat="server" Style="color: red"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:FileUpload ID="fuDocument" runat="server" CssClass="form-control" onchange="setUploadButtondoc(this)" TabIndex='<%#Container.DataItemIndex + 1 %>' />
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="chkadm" runat="server" Checked='<%#  Convert.ToString(Eval("MANDATORY"))=="1"? true : Convert.ToString(Eval("MANDATORY"))=="1"? true: false %>' />
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </asp:Panel>
                            </div>
                        </div>
                    </div>

                    <asp:UpdatePanel ID="updGradeEntry" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit"
                                    CssClass="btn btn-outline-info" OnClick="btnSave_Click" OnClientClick="return getVal();" TabIndex="4" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                    CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" TabIndex="5" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                <asp:HiddenField ID="hdndocsno" runat="server" />
                                <asp:HiddenField ID="hdndegreeno" runat="server" />
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSave" />
                            <%--   <asp:AsyncPostBackTrigger ControlID="ddlDegree" EventName="SelectedIndexChanged"/>--%>
                        </Triggers>
                    </asp:UpdatePanel>

                    <div class="col-12">
                        <asp:Panel ID="pnlSession" runat="server">
                            <asp:ListView ID="lvlist" runat="server">
                                <LayoutTemplate>
                                    <div id="demo-grid">
                                        <div class="sub-heading">
                                            <h5>Document Mapping List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="divdocumentlist">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <%-- <th style="text-align: center;">Sl.NO
                                                        </th>--%>
                                                    <th style="text-align: center;">Action
                                                    </th>
                                                    <th>Intake
                                                    </th>
                                                    <th>Program-Document Mapping
                                                    </th>
                                                    <th>View
                                                    </th>
                                                    <th>Mandatory
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

                                        <%-- <td style="text-align: center;">
                                                <%#Container.DataItemIndex+1 %>
                                            </td>--%>
                                        <td style="text-align: center;">
                                            <%--   <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/IMAGES/delete.gif" CommandArgument='<%# Eval("DOC_DEGREE_NO") %>'
                                                AlternateText="Delete Record" OnClientClick="return UserDeleteConfirmation();" OnClick="btnDel_Click"
                                                TabIndex="6" ToolTip='<%# Eval("DOC_DEGREE_NO") %>' />--%>
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("DOC_DEGREE_NO") %>'
                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                        </td>
                                        <td>
                                            <%#Eval("BATCHNAME") %>
                                        </td>
                                        <td>
                                            <%# Eval("DEGREENAME") %>  - <%# Eval("DOCUMENTNAME") %>   
                                          <%--  <%# Eval("DEGREENAME") %>--%>
                                        </td>

                                        <td>
                                            <asp:LinkButton ID="lnkViewDoc" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%#Eval("DOC_DEGREE_NO") %>' CommandName='<%#Eval("DOC_FILENAME") %>' Visible='<%# (Convert.ToString(Eval("DOC_FILENAME") ) == string.Empty ?  false : true )%>' OnClick="lnkViewDoc_Click"><i class="fa fa-eye"></i> View</asp:LinkButton>
                                        </td>
                                        <td><%#Eval("MANDATORY") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>

                            <%-- <div class="vista-grid_datapager" style="text-align: center">
                                            <asp:DataPager ID="dpPager" runat="server" OnPreRender="dpPager_PreRender" PagedControlID="lvlist" PageSize="10">
                                                <Fields>
                                                    <asp:NextPreviousPagerField ButtonType="Link" FirstPageText="&lt;&lt;" PreviousPageText="&lt;" RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowLastPageButton="false" ShowNextPageButton="false" ShowPreviousPageButton="true" />
                                                    <asp:NumericPagerField ButtonCount="7" ButtonType="Link" CurrentPageLabelCssClass="current" />
                                                    <asp:NextPreviousPagerField ButtonType="Link" LastPageText="&gt;&gt;" NextPageText="&gt;" RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowLastPageButton="true" ShowNextPageButton="true" ShowPreviousPageButton="false" />
                                                </Fields>
                                            </asp:DataPager>
                                        </div>--%>
                        </asp:Panel>
                    </div>
                    <asp:UpdatePanel ID="updModelPopup" runat="server">
                        <ContentTemplate>
                            <div id="myModal22" class="modal fade" role="dialog" data-backdrop="static">
                                <div class="modal-dialog modal-lg">
                                    <div class="modal-content" style="margin-top: -25px">
                                        <div class="modal-body">
                                            <div class="modal-header">
                                                <asp:LinkButton ID="lnkClose" runat="server" CssClass="close" Style="margin-top: -18px" OnClick="lnkClose_Click">x</asp:LinkButton>
                                                <%--<button type="button" class="close" data-dismiss="modal" style="margin-top: -18px">x</button>--%>
                                            </div>

                                            <iframe style="width: 100%; height: 500px;" id="irm1" src="~/PopUp.aspx" runat="server"></iframe>
                                            <asp:Literal ID="ltEmbed" runat="server" Visible="false" />
                                            <%--  <iframe id="iframe1" runat="server" frameborder="0" width="100%" height="800px" visible="false"></iframe>--%>
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
                </div>


            </div>
        </div>
    </div>

    <%-- document upload validation start --%>
    <script type="text/javascript">

        function checkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvDocument$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvDocument$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }

    </script>
    <script type="text/javascript">
        function setUploadButtondoc(chk) {
            var maxFileSize = 1000000;
            var fi = document.getElementById(chk.id);
            var tabValue = $(chk).attr('TabIndex');

            for (var i = 0; i <= fi.files.length - 1; i++) {
                var fsize = fi.files.item(i).size;
                if (fsize >= maxFileSize) {
                    alert("Image Size Greater Than 1MB");
                    $(chk).val("");
                }
            }
            //var fileExtension = ['jpeg', 'jpg', 'JPG', 'JPEG', 'PNG', 'png'];
            //var fileExtension = ['jpeg', 'jpg', 'JPG', 'JPEG', 'PNG', 'png'];
            var fileExtension1 = ['pdf'];
            if (tabValue == "1") {
                if ($.inArray($('#ctl00_ContentPlaceHolder1_lvDocument_ctrl0_fuDocument').val().split('.').pop().toLowerCase(), fileExtension1) == -1) {
                    alert("Only formats are allowed : " + fileExtension1.join(', '));
                    $("#ctl00_ContentPlaceHolder1_lvDocument_ctrl0_fuDocument").val("");
                }
            }
            else {
                if ($.inArray($(chk).val().replace(',', '.').split('.').pop().toLowerCase(), fileExtension1) == -1) {
                    alert("Only formats are allowed : " + fileExtension1.join(', '));
                    $(chk).val("");
                }
            }
        }
    </script>

    <%-- document upload validation end --%>

    <script type="text/javascript">
        function UserDeleteConfirmation() {
            if (confirm("Are you sure you want to delete this Record?"))
                return true;
            else
                return false;
        }
    </script>


    <%--  <script>
        $(document).ready(function () {
            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });
        function bindDataTable() {
            var myDT = $('#divdocumentlist').DataTable({
            });
        }

    </script>--%>
    <%-- <script src="../jqury/jquery-1.11.3.min.js" type="text/javascript"></script>  
   <script src="../jqury/jquery.dataTables.min.js" type="text/javascript"></script>  --%>
    <%--<script type="text/javascript">
        $(document).ready(function () {
            $(":input:not(:hidden)").each(function (i) { $(this).attr('tabindex', i + 1); });

            $('#divdocumentlist').DataTable();
        });
    </script>--%>

    <script>
        function getVal() {

            var id = (document.getElementById("<%=btnSave.ClientID%>"));
            ShowLoader(id);
            var rfvCollege = (document.getElementById("<%=lblDYCollege.ClientID%>").innerHTML);

            var alertmsg = '';

            if (document.getElementById("<%=ddlCollege.ClientID%>").value == "0") {
                if (alertmsg == '') {
                    document.getElementById("<%=ddlCollege.ClientID%>").focus();
                }
                alertmsg += 'Please Select ' + rfvCollege + ' \n';
            }

            var array = []
            var degreeNo;
            var checkboxes = document.querySelectorAll('input[type=checkbox]:checked')

            for (var i = 0; i < checkboxes.length; i++) {
                //array.push(checkboxes[i].value)    
                if (degreeNo == undefined) {
                    degreeNo = checkboxes[i].value + '$';
                }
                else {
                    degreeNo += checkboxes[i].value + '$';
                }
            }
            //alert(degreeNo);
            $('#<%= hdndegreeno.ClientID %>').val(degreeNo);
            //document.getElementById(inpHide).value = "degreeNo";


            if (alertmsg != '') {
                alert(alertmsg);
                HideLoader(id, 'Submit');
                return false;
            }

            return true;
        }
    </script>

</asp:Content>

