<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Intake.aspx.cs" Inherits="ACADEMIC_Intake" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
      <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>
     <style>
        /*--======= toggle switch css added by gaurav 29072021 =======--*/
        .switch input[type=checkbox] {
	        height: 0;
	        width: 0;
	        visibility: hidden;
        }
        .switch label {
	        cursor: pointer;
	        width: 82px;
            height: 34px;
	        background: #dc3545;
	        display: block;
	        border-radius: 4px;
	        position: relative;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }
        .switch label:hover {
            background-color: #c82333;
        }
        .switch label:before {
	        content: attr(data-off);
	        position: absolute;
	        right: 0;
	        font-size: 16px;
            padding: 4px 8px;
            font-weight: 400;
            color: #fff;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;

        }
        .switch input:checked + label:before {
	        content: attr(data-on);
	        position: absolute;
	        left: 0;
	        font-size: 16px;
            padding: 4px 15px;
            font-weight: 400;
            color: #fff;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }
        .switch label:after {
	        content: '';
	        position: absolute;
	        top: 1.5px;
            left: 1.7px;
            width: 10.2px;
            height: 31.5px;
            background: #fff;
            border-radius: 2.5px;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }
        .switch input:checked + label {
	        background: #28a745;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }
        .switch input:checked + label:hover {
	        background: #218838;
        }
        .switch input:checked + label:after {
	        transform: translateX(68px);
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }
   </style>
     <script type="text/javascript">
         $(document).ready(function () {
             $('.multi-select-demo').multiselect({
                 includeSelectAllOption: true,
                 maxHeight: 200
             });
         });
         var parameter = Sys.WebForms.PageRequestManager.getInstance();
         parameter.add_endRequest(function () {
             $('.multi-select-demo').multiselect({
                 includeSelectAllOption: true,
                 maxHeight: 200
             });
         });

    </script>
        
     <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static"/>
    
    
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

    <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function () {
            document.onreadystatechange = function () {
                var state = document.readyState
                if (state == 'interactive') {
                    document.getElementById('contents').style.visibility = "hidden";
                } else if (state == 'complete') {
                    setTimeout(function () {
                        document.getElementById('interactive');
                        //document.getElementById('load').style.visibility = "hidden";
                        document.getElementById('contents').style.visibility = "visible";
                    }, 1000);
                }
            }
        });
    </script>

    <div id="contents">
        <%--This is testing--%>
    </div>
    <asp:UpdatePanel ID="updGradeEntry" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--<h3 class="box-title"><strong>ADMABATCH YEAR MONTH MAPPING</strong> </h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <%--  <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label> College/School Name</label>
                                </div>--%>
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYAdmbatch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <%--<asp:TextBox ID="txtAdmbatch" runat="server" CssClass="form-control" ToolTip="Please Enter Intake" TabIndex="1" placeholder="Example :- 2020-21" MaxLength="7" onblur="return CheckAdmbatch();" ClientIDMode="Static"></asp:TextBox>--%> 
                                      <asp:TextBox ID="txtAdmbatch" runat="server" CssClass="form-control" ToolTip="Please Enter Intake" TabIndex="1" MaxLength="40" ClientIDMode="Static" placeholder="Maximum Length of Intake is 40"></asp:TextBox> 
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                     
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYMonth" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True"
                                            ToolTip="Please Select Start Month" TabIndex="2" ClientIDMode="Static">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                   
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblEndMonth" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlEndMonth" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True"
                                            ToolTip="Please Select End Month" TabIndex="3" ClientIDMode="Static">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                           
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                   
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="DylblIntakeStart" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div id="picker" class="form-control" TabIndex="3">
                                            <i class="fa fa-calendar"></i>&nbsp;
                                            <span id="date"></span>
                                            <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                        </div>
                                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" TabIndex="4" ClientIDMode="Static" style="display:none"/>
                                        <asp:HiddenField ID="hdnDate" runat="server"/>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYYear" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" 
                                            ToolTip="Please Select Year" TabIndex="5" ClientIDMode="Static">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                  
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="DivStudylevel" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYPrgmtype" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                      <%--  <asp:ListBox ID="ddlstudylevel" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" SelectionMode="Multiple"
                                            ToolTip="Please Select Study Level" TabIndex="5" ClientIDMode="Static">                                          
                                        </asp:ListBox>--%>
                                        <asp:ListBox ID="ddlstudylevel" runat="server" CssClass="form-control multi-select-demo" 
                                  SelectionMode="Multiple" AppendDataBoundItems="true"></asp:ListBox>
                                  
                                    </div>
                           
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblActive" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                         <div  class="switch form-inline">
                                            <input type="checkbox" id="switch" name="switch" class="switch" checked/>
                                             <label data-on="Active" data-off="Inactive" for="switch"></label>
                                         </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                 <asp:LinkButton ID="btnSave" runat="server" ToolTip="Submit" OnClientClick="return validate()"
                                    CssClass="btn btn-outline-info" OnClick="btnSave_Click" TabIndex="8" ClientIDMode="Static">Submit</asp:LinkButton>

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                    CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" TabIndex="8" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Branch"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlSession" runat="server">
                                    <asp:ListView ID="lvlist" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading">
                                                    <h5>Intake Creation List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divdepartmentlist">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th style="text-align: center;">Action </th>
                                                            <th>Intake</th>
                                                            <th>Intake Start-End Date</th>
                                                          <%--  <th>End Month</th>
                                                            <th>Start/End Date</th>--%>
                                                            <th>Year</th>
                                                            <%--<th>Study Level</th>--%>
                                                            <th>Status </th>
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
                                                <td style="text-align: center;">
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("BATCHNO") %>'
                                                        CommandName='<%# Eval("INTAKE_STUDY_MAP_ID") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" OnClientClick="return InitAutoCompl();" />
                                                </td>
                                                <td><%# Eval("BATCHNAME") %></td>
                                        <%--        <td><%# Eval("MONTH") %></td>
                                                <td><%# Eval("ENDMONTH") %> </td>--%>
                                                <td><%# Eval("START_END_DATE", "{0:dd-MMM-yyyy}") %> </td>
                                                <td><%# Eval("YEARNAME") %> </td>
                                                <%-- <td><%# Eval("UA_SECTIONNAME") %> </td>--%>
                                                <td><%# Eval("ACTIVE") %>
                                                    <asp:HiddenField ID="hdfid" runat="server" Value='<%# Eval("INTAKE_STUDY_MAP_ID") %>' />
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
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lvlist" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>

    <script>
        function SetStat(val) {
            $('#switch').prop('checked', val);
        }

        var summary = "";
        $(function () {
            //debugger;
            $('#btnSave').click(function () {
                localStorage.setItem("currentId", "#btnSave,Submit");
                debugger;
                ShowLoader('#btnSave');
                
                if ($('#txtAdmbatch').val() == "")
                    summary += '<br>Please Enter Intake.';
                if ($('#ddlstudylevel').val() == "0")
                    summary += '<br>Please Select Study Level.';
                //if ($('#ddlEndMonth').val() == "0")
                //    summary += '<br>Please Select End Month.';
                if ($('#ddlYear').val() == "0")
                    summary += '<br>Please Select Year.';


                if (summary != "") {
                    customAlert(summary);
                    summary = "";
                    return false
                }
                $('#hfdStat').val($('#switch').prop('checked'));
                return GetSelectedTextValue();
                
            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSave').click(function () {
                    localStorage.setItem("currentId", "#btnSave,Submit");
                    ShowLoader('#btnSave');

                    if ($('#txtAdmbatch').val() == "")
                        summary += '<br>Please Enter Intake.';
                    //if ($('#ddlMonth').val() == "0")
                    //    summary += '<br>Please Select Start Month.';
                    //if ($('#ddlEndMonth').val() == "0")
                    //    summary += '<br>Please Select End Month.';
                    if ($('#ddlYear').val() == "0")
                        summary += '<br>Please Select Year.';

                    if (summary != "") {
                        customAlert(summary);
                        summary = "";
                        return false
                    }
                    $('#hfdStat').val($('#switch').prop('checked'));
                    return GetSelectedTextValue();
                });
            });
        });
    </script>


    <script type="text/javascript">
        function UserDeleteConfirmation() {
            if (confirm("Are you sure you want to delete this Record?"))
                return true;
            else
                return false;
        }
    </script>
    <script type="text/javascript">
        function UserDeleteConfirmation() {
            if (confirm("Are you sure you want to delete this Entry?"))
                return true;
            else
                return false;
        }
    </script>
    <script type="text/javascript">

        function validate() {
           
        }
    </script>
    <script type="text/javascript">
         $(document).ready(function () {
             $('#picker').daterangepicker({
                 startDate: moment().subtract(00, 'days'),
                 endDate: moment(),
                 locale: {
                     format: 'DD MMM, YYYY'
                 },
                 //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                 ranges: {
                     //                    'Today': [moment(), moment()],
                     //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                     //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                     //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                     //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                     //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                 },
                 //<!-- ========= Disable dates after today ========== -->
                 //maxDate: new Date(),
                 //<!-- ========= Disable dates after today END ========== -->
             },
         function (start, end) {
             $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
             document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
        });

            $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            document.getElementById('<%=hdnDate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#picker').daterangepicker({
                    startDate: moment().subtract(00, 'days'),
                    endDate: moment(),
                    locale: {
                        format: 'DD MMM, YYYY'
                    },
                    //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                    ranges: {
                        //                    'Today': [moment(), moment()],
                        //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                        //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                        //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                        //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                        //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                    },
                    //<!-- ========= Disable dates after today ========== -->
                    //maxDate: new Date(),
                    //<!-- ========= Disable dates after today END ========== -->
                },
            function (start, end) {
                debugger
                $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
                document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });

                $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
                document.getElementById('<%=hdnDate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            });
        });
    </script>
      <script>

          function Setdate(date) {
              var prm = Sys.WebForms.PageRequestManager.getInstance();
              prm.add_endRequest(function () {
                  $(document).ready(function () {
                      debugger;
                      //var startDate = moment(date.split('-')[0], "MMM DD, YYYY");
                      //var endtDate = moment(date.split('-')[1], "MMM DD, YYYY");


                      var startDate = moment(date.split('-')[0], "DD MMM, YYYY");
                      var endtDate = moment(date.split('-')[1], "DD MMM, YYYY");


                      //$('#date').html(date);

             //         $('#date').html(startDate.format("MMM DD, YYYY") + ' - ' + endtDate.format("MMM DD, YYYY"));
                      $('#date').html(startDate.format("DD MMM, YYYY") + ' - ' + endtDate.format("DD MMM, YYYY"));
                      document.getElementById('<%=hdnDate.ClientID%>').value = date;
                    //$('#picker').daterangepicker({ startDate: startDate, endDate: endtDate});
                    $('#picker').daterangepicker({
                        startDate: startDate.format("MM/DD/YYYY"),
                        endDate: endtDate.format("MM/DD/YYYY"),

              //          startDate: startDate.format("DD MMM, YYYY"),
              //          endDate: endtDate.format("DD MMM, YYYY"),
                        ranges: {
                        },
                    },
            function (start, end) {
                debugger
   //             $('#date').html(start.format('MMM DD, YYYY') + ' - ' + end.format('MMM DD, YYYY'))
   //             document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('MMM DD, YYYY') + ' - ' + end.format('MMM DD, YYYY'))


                $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
                document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });
                    
                });
            });
};
    </script>
   <script type="text/javascript">
       function GetSelectedTextValue() {
           debugger;
           var ddlEndMonths = document.getElementById("<%=ddlEndMonth.ClientID %>");
           var selectedEndMonthText = parseInt(ddlEndMonths.value);

           var ddlMonths = document.getElementById("<%=ddlMonth.ClientID %>");
           var selectedStartMonthText = parseInt(ddlMonths.value);

           var ddlYear = document.getElementById("<%=ddlYear.ClientID %>");
           var year = ddlYear.options[ddlYear.selectedIndex].innerHTML;
           
           var date = document.getElementById("<%=txtStartDate.ClientID %>").value;
           
           var d = new Date();
           var CurrentYear = d.getFullYear();
           if (parseInt(year) == parseInt(CurrentYear)) {
               if (selectedEndMonthText >= selectedStartMonthText) {
                   var datearray = new Array();
                   datearray = date.split('-');

                   var startDate = datearray[0];
                   var endtDate = datearray[1];
                   var StartMonth = startDate.substring(0, 3);
                   var EndMonth = endtDate.substring(0, 3);

                   if (selectedStartMonthText <= 9) {
                       var x = "0" + selectedStartMonthText;
                       if (parseInt(selectedStartMonthText) != parseInt(StartMonth)) {
                           customAlert("Selected Start Date Should Be Match With Selected Start Month !!!")
                               //alert("Selected Start Date Should Be Match With Selected Start Month !!!");
                           return false;
                       }
                   }
                   else {
                       if (parseInt(selectedStartMonthText) != parseInt(StartMonth)) {
                           customAlert("Selected Start Date Should Be Match With Selected Start Month !!!")
                              // alert("Selected Start Date Should Be Match With Selected Start Month !!!");
                           return false;
                       }
                   }

                   if (selectedEndMonthText <= 9) {
                       var x = "0" + selectedEndMonthText;
                       if (parseInt(selectedEndMonthText) != parseInt(EndMonth)) {
                           customAlert("Selected End Date Should Be Match With Selected End Month !!!")
                           //alert("Selected End Date Should Be Match With Selected End Month !!!");
                           return false;
                       }
                   }
                   else {
                       if (parseInt(selectedEndMonthText) != parseInt(EndMonth)) {
                           customAlert("Selected End Date Should Be Match With Selected End Month !!!")
                          // alert("Selected End Date Should Be Match With Selected End Month !!!");
                           return false;
                       }
                   }
               }
               else {
                   customAlert("Start Month Cannot Be Greater Than End Month !!!")
                  // alert("Start Month Cannot Be Greater Than End Month !!!");

                   return false;
               }
           }
           else {
               var datearray = new Array();
               datearray = date.split('-');
               
               var startDate = datearray[0];
               var endtDate = datearray[1];
               
               var StartYear = startDate.substring(6, 10);
               var Endyear = endtDate.substring(7, 12);
               
               var StartMonth = startDate.substring(0, 3);
               var EndMonth = endtDate.substring(0, 3);
               if (StartYear != CurrentYear) {
                   customAlert("Selected Start Date Year Should Be Match With Current Year !!!");
                  // alert("Selected Start Date Year Should Be Match With Current Year !!!");
                   return false;
               }
               else if (Endyear != year) {
                   customAlert("Selected End Date Year Should Be Match With Selected Year !!!");
                   //alert("Selected End Date Year Should Be Match With Selected Year !!!");
                   return false;
               }
                   if (selectedStartMonthText <= 9) {
                       var x = "0" + selectedStartMonthText;
                       if (parseInt(selectedStartMonthText) != parseInt(StartMonth)) {
                           customAlert("Selected Start Date Should Be Match With Selected Start Month !!!");
                           // alert("Selected Start Date Should Be Match With Selected Start Month !!!");
                           return false;
                       }
                   }
                   else {
                       if (parseInt(selectedStartMonthText) != parseInt(StartMonth)) {
                           customAlert("Selected Start Date Should Be Match With Selected Start Month !!!");
                           // alert("Selected Start Date Should Be Match With Selected Start Month !!!");
                           return false;
                       }
                   }

                   if (selectedEndMonthText <= 9) {
                       var x = "0" + selectedEndMonthText;
                       if (parseInt(selectedEndMonthText) != parseInt(EndMonth)) {
                           customAlert("Selected End Date Should Be Match With Selected End Month !!!");
                          // alert("Selected End Date Should Be Match With Selected End Month !!!");
                           return false;
                       }
                   }
                   else {
                       if (parseInt(selectedEndMonthText) != parseInt(EndMonth)) {
                           customAlert("Selected End Date Should Be Match With Selected End Month !!!");
                          // alert("Selected End Date Should Be Match With Selected End Month !!!");
                           return false;
                       }
                   }
              
           }
          
    }
</script>

     <script type="text/javascript">
         function Validate1(e) {
             // debugger;
             var keyCode = e.keyCode || e.which;
             //Regex for Valid Characters i.e. Alphabets.
             var regex = /^[A-Za-z ]+$/;
             //Validate TextBox value against the Regex.
             var isValid = regex.test(String.fromCharCode(keyCode));
             if (!isValid) {
                 alert("Only Alphabets allowed.");
             }

             return isValid;

         }
    </script>


    <script>
        function CheckAdmbatch()
        {
            debugger;
            var txtAdmBatch = document.getElementById("<%=txtAdmbatch.ClientID %>").value;
           
            var a = /^[0-9]+(-[0-9]+)+$/;
            if (txtAdmBatch != "") {
                if (a.test(txtAdmBatch) == true) {
                    var Admarray = new Array();
                    Admarray = txtAdmBatch.split('-');

                    if (Admarray[0].length == 4) {
                        if (Admarray[1].length == 2) {

                        }
                        else {
                            customAlert("Please Enter Valid Admission batch(Example:-2021-22)")
                           // alert("Please Enter Valid Admission batch(Example:-2021-22)");
                            document.getElementById("<%=txtAdmbatch.ClientID %>").value = "";
                        }
                    }
                    else {
                        customAlert("Please Enter Valid Admission batch(Example:-2021-22)")
                      //  alert("Please Enter Valid Admission batch(Example:-2021-22-2)");
                        document.getElementById("<%=txtAdmbatch.ClientID %>").value = "";
                    }
                }
                else {
                    customAlert("Please Enter Valid Admission Batch(example:-2021-22)")
                   // alert("Please Enter Valid Admission Batch(example:-2021-22-3)");
                    document.getElementById("<%=txtAdmbatch.ClientID %>").value = "";
                }
            }
      
        }
    </script>
</asp:Content>

