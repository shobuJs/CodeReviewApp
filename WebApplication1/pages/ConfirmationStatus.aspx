<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ConfirmationStatus.aspx.cs" Inherits="ACADEMIC_ConfirmationStatus" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
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
    </script>

    <script src="/Content/jquery.js" type="text/javascript"></script>

    <script src="/Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>

    <!-- jQuery library -->
    <script src="/jquery/jquery-3.2.1.min.js"></script>
    <%--<link href="../jquery/jquery.multiselect.css" rel="stylesheet" />
    <script src="../jquery/jquery.multiselect.js"></script>--%>
    <link href="/jquery/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="/jquery/bootstrap-multiselect.js"></script>

    <%--Added By Abhinay Lad [24-02_2020]--%>
    <script>
        //var MulSel = $.noConflict();
        $(document).ready(function () {
            $('.multi-select-demo').multiselect();
            $('.multiselect').css("width", "100%");
            $(".multiselect-container").css("width", "100%");
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.multi-select-demo').multiselect({
                    allSelectedText: 'All',
                    maxHeight: 200,
                    maxWidth: '100%',
                    includeSelectAllOption: true
                });
                $('.multiselect').css("width", "100%");
                $(".multiselect-container").css("width", "100%")
            });
        });
    </script>
    <%--Ended By Abhinay Lad [24-02_2020]--%>

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
                //   InitAutoCompl();
            }
        });

    </script>


    <style>
        div.dd_chk_select
        {
            height: 35px;
            font-size: 14px !important;
            padding-left: 12px !important;
            line-height: 2.2 !important;
            width: 100%;
        }

            div.dd_chk_select div#caption
            {
                height: 35px;
            }
    </style>

    <style type="text/css">
        #load
        {
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

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

      <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="pnlFeeTable" DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="pnlFeeTable" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">

                <div class="col-md-12">

                    <div class="box box-info">

                        <div class="box-header with-border">
                            <h3 class="box-title">REGISTRATION STATUS REPORT</h3>
                            <br />
                            <br />
                            <p><span style="color: Red; font-weight: bold;">Note : * Marked fields Are Mandatory</span></p>
                        </div>

                        <form role="form">
                            <div class="box-body">

                                <div class="form-group col-md-3">
                                    <label><span style="color: red">*</span>Admission Batch</label>
                                    <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="1" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlAdmBatch"
                                        Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" SetFocusOnError="True" ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-md-3">
                                    <label>Degree</label>
                                    <%-- <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="2" CssClass="form-control"
                             OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                        </asp:DropDownList>--%>
                                    <asp:ListBox ID="ddlDegree" runat="server" AppendDataBoundItems="true" TabIndex="2" CssClass="form-control multi-select-demo" SelectionMode="multiple" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                </div>

                                <div class="form-group col-md-3">
                                    <label>Branch</label>
                                    <%-- <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="3" CssClass="form-control">
                            <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                        </asp:DropDownList>--%>

                                    <asp:ListBox ID="ddlBranch" runat="server" AppendDataBoundItems="true" TabIndex="3" CssClass="form-control multi-select-demo" SelectionMode="multiple" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                </div>
                                <div class="form-group col-md-3">                                    
                                    <label for="city">Status</label>
                                    <asp:DropDownList ID="ddlStatus" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="4">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem> 
                                    </asp:DropDownList>

                                    <%--<asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                </div>

                            </div>
                        </form>

                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="submit" OnClick="btnShow_Click" TabIndex="4" CssClass="btn btn-outline-info" />
                                &nbsp;
                    <asp:Button ID="btnExcel" runat="server" Text="Status Report" ValidationGroup="submit" OnClick="btnExcel_Click" TabIndex="4" CssClass="btn btn-success" />
                                &nbsp;
                     <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="5" OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" />

                                <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submit" />
                            </p>

                            <div class="form-group col-md-12 table-responsive">
                                <asp:ListView ID="lvHday" runat="server" Visible="false">
                                    <LayoutTemplate>
                                        <h3>
                                            <label class="label label-default">Student Confirmation List</label>
                                        </h3>
                                        <table id="Table1" class="table table-bordered table-hover table-fixed">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Sr No.</th>
                                                    <th>Temp. No.</th>
                                                    <th>Admission No.</th>
                                                    <th>Name</th>
                                                    <th>Admission Batch</th>
                                                    <th>Degree</th>
                                                    <th>Branch</th>
                                                    <th>Student Status</th>
                                                    <th>Approval</th>
                                                    <th>Tution Fees</th>
                                                    <th>Transport Facility</th>
                                                    <th>Transport Fees</th>
                                                    <th>Admission Status</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item">
                                            <td><%# Container.DataItemIndex + 1 %></td>
                                            <td><%#Eval("IDNO") %></td>
                                            <td><%#Eval("ENROLLNO") %></td>
                                            <td><%#Eval("STUDNAME") %></td>
                                            <td><%#Eval("BATCHNAME") %></td>
                                            <td><%#Eval("DEGREE") %></td>
                                            <td><%#Eval("BRANCH") %></td>
                                            <td><%#Eval("STUD_STATUS") %></td>
                                            <td><%#Eval("APPROVAL") %></td>
                                            <td><%#Eval("TUITION_FEES") %></td>
                                            <td><%#Eval("TRANSPORT_FACILTY") %></td>
                                            <td><%#Eval("TRANSPORT_FEES") %></td>
                                            <td><%#Eval("ADM_CONFIRM") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>

                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>
    <br />
</asp:Content>
