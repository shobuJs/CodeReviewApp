<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="EntranceMapping.aspx.cs" Inherits="ACADEMIC_EntranceMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script src="../jquery/jquery-3.2.1.min.js"></script>
    <link href="../jquery/jquery.multiselect.css" rel="stylesheet" />
    <script src="../jquery/jquery.multiselect.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {

            //--*========Added by akhilesh on [2019-05-11]==========*--          
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(InitAutoCompl)

            //--*========Added by akhilesh on [2019-05-11]==========*--          
            // if you use jQuery, you can load them when dom is read.          
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);

            // Place here the first init of the autocomplete
            InitAutoCompl();

            function InitializeRequest(sender, args) {
            }

            function EndRequest(sender, args) {
                // after update occur on UpdatePanel re-init the Autocomplete
                InitAutoCompl();
            }

            function InitAutoCompl() {
                $('select[multiple]').val('').multiselect({
                    columns: 2,     // how many columns should be use to show options            
                    search: true, // include option search box
                    searchOptions: {
                        delay: 250,                         // time (in ms) between keystrokes until search happens
                        clearSelection: true,
                        showOptGroups: false,                // show option group titles if no options remaining

                        searchText: true,                 // search within the text

                        searchValue: true,                // search within the value

                        onSearch: function (element) { } // fires on keyup before search on options happens
                    },

                    // plugin texts

                    texts: {

                        placeholder: 'Select Entrance Exam', // text to use in dummy input

                        search: 'Search',         // search input placeholder text

                        selectedOptions: ' selected',      // selected suffix text

                        selectAll: 'Select all Entrance Exam',     // select all text

                        unselectAll: 'Unselect all Entrance Exam',   // unselect all text

                        //noneSelected: 'None Selected'   // None selected text

                    },

                    // general options

                    selectAll: true, // add select all option

                    selectGroup: false, // select entire optgroup

                    minHeight: 200,   // minimum height of option overlay

                    maxHeight: null,  // maximum height of option overlay

                    maxWidth: null,  // maximum width of option overlay (or selector)

                    maxPlaceholderWidth: null, // maximum width of placeholder button

                    maxPlaceholderOpts: 10, // maximum number of placeholder options to show until "# selected" shown instead

                    showCheckbox: true,  // display the checkbox to the user

                    optionAttributes: [],

                });
            }

        });

    </script>
    <script>
        function getVal() {
            var array = []
            var entranceexamNo;
            var checkboxes = document.querySelectorAll('input[type=checkbox]:checked')

            for (var i = 0; i < checkboxes.length; i++) {
                //array.push(checkboxes[i].value)    
                if (entranceexamNo == undefined) {
                    entranceexamNo = checkboxes[i].value + ',';
                }
                else {
                    entranceexamNo += checkboxes[i].value + ',';
                }
            }
            //alert(degreeNo);
            $('#<%= hdnentranceexamNo.ClientID %>').val(entranceexamNo);
            //document.getElementById(inpHide).value = "degreeNo";
        }
    </script>

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

    <style type="text/css">
        #load {
            width: 100%;
            height: 100%;
            position: fixed;
            z-index: 9999;
            /*background: url("/images/loading_icon.gif") no-repeat center center rgba(0,0,0,0.25);*/
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





    <%--    <script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            RepeaterDiv();

        }
        function RepeaterDiv() {
            $(document).ready(function () {

                $(".display").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers"
                });

            });

        }
    </script>--%>

    <%--    <script src="../Content/jquery.js" type="text/javascript"></script>

    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>--%>

    <%-- <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>
    <div id="load" style="z-index: 1; position: absolute; top: 10px;">
        <div style="width: 120px; padding-left: 45%;">
            <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
            <p class="text-success"><b>Loading..</b></p>
        </div>
    </div>
    <div id="contents">
        <%--This is testing--%>
    </div>


    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">ENTRANCE EXAM MAPPING</h3>
                    <br />
                    <div class="pull-left">
                        <div style="color: Red; font-weight: bold">
                            Note : * Marked fields are mandatory
                        </div>
                    </div>
                </div>


                <%--<form role="form">--%>
                <div class="box-body">
                     <%--<div class="col-md-2"></div>--%>
                    <div class="col-md-4">
                        <label><span style="color: red;">*</span> College</label>
                        <asp:DropDownList ID="ddlCollege" runat="server" TabIndex="1" CssClass="form-control"
                            AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"
                            ToolTip="Please Select College">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                            Display="None" ErrorMessage="Please Select College" ValidationGroup="submit"
                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-4">
                        <label><span style="color: red;">*</span> Degree</label>
                        <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="2" CssClass="form-control"
                            AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                            ToolTip="Please Select Degree">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                            Display="None" ErrorMessage="Please Select Degree" ValidationGroup="submit"
                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-4">
                        <label><span style="color: red;">*</span> Entrance Exam</label>
                        <asp:DropDownList ID="ddlEntrance" runat="server" TabIndex="3" CssClass="form-control"
                            multiple="multiple">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvEntrance" runat="server" ControlToValidate="ddlEntrance"
                            Display="None" ErrorMessage="Please Select Entrance Exam" ValidationGroup="submit"
                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <%--  </form>--%>

                <asp:UpdatePanel ID="updGradeEntry" runat="server">
                    <ContentTemplate>
                        <div class="box-footer">
                            <p class="text-center">
                                <td class="form_left_label">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit"
                                        TabIndex="4" OnClick="btnSave_Click" CssClass="btn btn-outline-info" OnClientClick="return getVal();" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                        TabIndex="5" OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    <asp:HiddenField ID="hdnentranceexamNo" runat="server" />
                            </p>

                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSave" />
                        <%-- <asp:AsyncPostBackTrigger ControlID="ddlDegree" EventName="SelectedIndexChanged"/>--%>
                    </Triggers>
                </asp:UpdatePanel>

                <div class="box-footer">
                    <div class="col-md-12">
                        <asp:Panel ID="pnlSession" runat="server">
                            <asp:ListView ID="lvlist" runat="server">
                                <LayoutTemplate>
                                    <div id="demo-grid">

                                        <h4>Entrance Exam Mapping List</h4>

                                        <table id="diventrancelist" class="table table-hover table-bordered">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Action
                                                    </th>
                                                    <th>Degree-Entrance Mapping
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
                                            <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/IMAGES/delete.gif" CommandArgument='<%# Eval("ENTR_DEGREE_NO") %>'
                                                AlternateText="Delete Record" OnClientClick="return UserDeleteConfirmation();" OnClick="btnDel_Click"
                                                TabIndex="6" ToolTip="Delete" />
                                        </td>
                                        <td>
                                            <%# Eval("DEGREENAME") %>  - <%# Eval("QUALIEXMNAME") %>   
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


    <script type="text/javascript">
        function UserDeleteConfirmation() {
            if (confirm("Are you sure you want to delete this Entry?"))
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
    <script type="text/javascript">
        $(document).ready(function () {
            $('#diventrancelist').DataTable();
        });
    </script>
</asp:Content>

