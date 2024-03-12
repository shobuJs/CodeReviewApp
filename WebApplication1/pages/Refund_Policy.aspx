<%@ Page Language="C#" Title=""  MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Refund_Policy.aspx.cs" Inherits="ACADEMIC_Refund_Policy" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <style>
        input[type=checkbox], input[type=radio] {
            margin: 2px 3px 0;
        }

        .badge {
            display: inline-block;
            padding: 5px 10px 7px;
            border-radius: 15px;
            font-size: 100%;
            width: 80px;
        }

        .badge-warning {
            color: #fff !important;
        }

        td .fa-eye {
            font-size: 18px;
            color: #0d70fd;
        }
    </style>

    <script>
        $(document).ready(function () {
            debugger;
            var table = $('#mytable1').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 450,
                scrollX: true,
                scrollCollapse: true,
                paging: false,
                searching: false,

                //dom: 'lBfrtip',
                //buttons: [
                //    {
                //        extend: 'colvis',
                //        text: 'Column Visibility',
                //        columns: function (idx, data, node) {
                //            var arr = [0, 4];
                //            if (arr.indexOf(idx) !== -1) {
                //                return false;
                //            } else {
                //                return $('#mytable1').DataTable().column(idx).visible();
                //            }
                //        }
                //    }
                //],
                "bDestroy": true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            debugger;
            $(document).ready(function () {
                var table = $('#mytable1').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 450,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false,
                    searching: false,

                    //dom: 'lBfrtip',
                    //buttons: [
                    //    {
                    //        extend: 'colvis',
                    //        text: 'Column Visibility',
                    //        columns: function (idx, data, node) {
                    //            var arr = [0, 4];
                    //            if (arr.indexOf(idx) !== -1)
                    //            {
                    //                return false;
                    //            }
                    //            else
                    //            {
                    //                return $('#mytable1').DataTable().column(idx).visible();
                    //            }
                    //        }
                    //    }
                    //],
                    "bDestroy": true,
                });
            });
        });
    </script>

    <script>

        function functionx(evt) {
            if (evt.charCode > 31 && (evt.charCode < 48 || evt.charCode > 57)) {
                alert("Enter Only Numeric Value ");
                return false;
            }
        }



        function fn_validateNumeric(thi, dec) {

            if (window.event) keycode = window.event.keyCode;

            else if (e) keycode = e.which;

            else return true;

            if (((keycode >= 65) && (keycode <= 90)) || ((keycode >= 97) && (keycode <= 122)) || (keycode == 32)) {

                return true;
            }

            else {
                alert("Please enter only alphabets");
                return false;
            }
        }


        function SetStat(val) {
            $('#switch').prop('checked', val);
        }

        function validate() {
            $('#hfdStat').val($('#switch').prop('checked'));
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validate();
                });
            });
        });


        //$(function () {
        //    $('#btnSubmit').click(function () {
        //        $('#hfdStat').val($('#switch').prop('checked'));
        //    });
        //});


    </script>

  
    <style>
        #addGrades {
            font-size: 18px;
        }

            #addGrades:hover {
                color: #0d70fd;
            }

        .fa-times {
            color: grey;
        }

            .fa-times:hover {
                color: #f94144;
                cursor: pointer;
            }
    </style>

    <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updRefundPolicy"
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

    <asp:UpdatePanel ID="updRefundPolicy" runat="server">
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
                                    <div class="col col-lg-6 col-md-6 col-12">                                    
                                        <div class="row mt-4" id="DivAdd" runat="server" visible="true">                                         
                                            <div class="form-group col-12 text-right">
                                                <asp:Button ID="btnAddPolicySlab" runat="server" CssClass="btn btn-outline-info" Text="Add Policy Details" OnClick="btnAddPolicySlab_Click" />
                                            </div>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="gradingTable">
                                            <tr>
                                                <td>
                                                    <div class="col-12 pl-0 pr-0">
                                                        <asp:Panel ID="pnllst" runat="server">
                                                            <asp:ListView ID="lvRefundPolicy" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Enter Refund Details</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap" id="mytable1" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th style="text-align: center">SrNo
                                                                                </th>
                                                                                <th style="text-align: center">No Of Days
                                                                                </th>
                                                                                <th style="text-align: center">Percentage
                                                                                </th>
                                                                                <th style="text-align: center">Action
                                                                                </th>
                                                                                
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
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                             <asp:HiddenField ID="hfsrno" runat="server" Value='<%# Container.DataItemIndex + 1 %>' />
                                                                            <asp:HiddenField ID="hfdvalue" runat="server" Value='<%# Eval("ID") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtNoOfDays" runat="server" CssClass="form-control" MaxLength="25" Text='<%# Eval("Column1") %>' onkeypress="return functionx(event)" ToolTip="Please enter No Of Days"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtPercentage" runat="server" CssClass="form-control" onkeyup="return validation_2Absent(this);" MaxLength="15" Text='<%# Eval("Column2") %>' onkeypress="return functionx(event)" ToolTip="Please enter Percentage"></asp:TextBox>
                                                                        </td>
                                                                       
                                                                        <td style="text-align: center">                                                                          
                                                                            <asp:ImageButton ID="btnRemove" runat="server" ImageUrl="~/IMAGES/remove.png" OnClick="btnRemove_Click" CommandArgument='<%#Eval("ID")%>' CommandName='<%# Container.DataItemIndex + 1 %>' />
                                                                        </td>
                                                                    </tr>

                                                                </ItemTemplate>

                                                            </asp:ListView>
                                                           
                                                        </asp:Panel>
                                                    </div>
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </div>

                                    <div class="col col-lg-6 col-md-6 col-12">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblPolicyName" runat="server" Font-Bold="true"></asp:Label>
                                                <label>Refund Policy Name</label>
                                            </div>
                                            <asp:TextBox ID="txtPolicyName" runat="server" CssClass="form-control" MaxLength="100" TabIndex="2"
                                                ToolTip="Please enter refund policy name" />

                                            <asp:RequiredFieldValidator ID="rfvtxtPolicyName" runat="server" ControlToValidate="txtPolicyName" Display="None" ErrorMessage="Please Enter Refund Policy Name." ValidationGroup="submit"></asp:RequiredFieldValidator>


                                        </div>
                                        <div class="form-group col-lg-6 col-md-6 col-12">

                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblPaymentType" runat="server" Font-Bold="true"></asp:Label>
                                                <label>Payment Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPaymentType" runat="server" TabIndex="4" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="True"
                                                ToolTip="Please Select Payment Type">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="F">Full Payment</asp:ListItem>
                                                <asp:ListItem Value="I">Installments</asp:ListItem>
                                                <%--<asp:ListItem Value="B">Both</asp:ListItem>--%>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlPaymentType" runat="server" ControlToValidate="ddlPaymentType" Display="None" ErrorMessage="Please Select Payment Type" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>


                                        <div class="form-group col-lg-6 col-md-6 col-12" style="display:none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>With Effect From</label>
                                            </div>
                                            <asp:TextBox ID="dtpEffectFromDate" runat="server" TabIndex="5" CssClass="form-control" Width="100%"
                                                ToolTip="Please Enter Exam Date" />
                                            <i class="fa fa-calendar input-prefix" aria-hidden="true" style="float: right; margin-top: -23px; margin-right: 10px;"></i>
                                            <%--<asp:RequiredFieldValidator ID="rfvdtpEffectFromDate" runat="server" ControlToValidate="dtpEffectFromDate" Display="None" ErrorMessage="Please Select Date" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>




                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblStatus" runat="server" Font-Bold="true"></asp:Label>
                                                <label>Status</label>
                                            </div>
                                            <div class="switch form-inline">
                                                <input type="checkbox" id="switch" name="switch" class="switch" checked />
                                                <label data-on="Active" data-off="Inactive" for="switch"></label>
                                            </div>
                                        </div>

                                        <%--  --%>
                                    </div>

                                </div>
                            </div>
                            <div class="col-12 btn-footer">                              
                                <asp:LinkButton ID="btnSubmit" runat="server" ToolTip="Submit" ValidationGroup="submit" OnClientClick="return validate()"
                                    CssClass="btn btn-outline-info btnX" TabIndex="10" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                               <%-- <asp:Button ID="btnReport" runat="server" Text="Report" ValidationGroup="report"
                                    TabIndex="11" CssClass="btn btn-outline-primary" />--%>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                    TabIndex="10" CssClass="btn btn-outline-danger" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="report" />
                            </div>

                            <%-- Table Here --%>
                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvData" runat="server">
                                        <LayoutTemplate>
                                            <table id="myTable" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Edit</th>
                                                        <th>Refund Policy Name</th>
                                                        <th>Payment Type</th>
                                                        <%--<th>Witheffectfrom</th>--%>
                                                        <th>Status</th>
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CommandArgument='<%# Eval("REFUNDPOLICY_ID")%>' ImageUrl="~/images/edit.gif" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblColName" runat="server" Text='<%# Eval("REFUNDPOLICY_NAME") %>'></asp:Label>
                                                </td>
                                                <td><%# Eval("PAYMENTTYPE")%></td>
                                               <%-- <td><%# Eval("WITHEFFECTFROM", "{0:dd-MM-yyyy}")%></td>--%>
                                                     <%--<td class="text-center"><span class="badge badge-success"><%#Eval("STATUS1") %></span></td>--%>
                                                 <td class="text-center">
                                                  <%--   <%#Eval("STATUS1") %>--%>
                                                      <%--<asp:Label ID="lblStatus" runat="server" Text='<%# Eval("STATUS1") %>'></asp:Label>--%>
                                                       <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("STATUS1").ToString()=="ACTIVE" ? "ACTIVE" :"INACTIVE" %>'
                                                                                        ForeColor='<%# Eval("STATUS1").ToString()=="ACTIVE" ? System.Drawing.Color.Green :System.Drawing.Color.Red %>'></asp:Label>
                                                 </td>

                                            </tr>

                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <%-- Table End --%>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Modals Here -->
            <!--== Script for date picker Exam date ==-->
            <script>
                $(function () {
                    $('#ctl00_ContentPlaceHolder1_dtpEffectFromDate').daterangepicker({
                        singleDatePicker: true,
                        locale: {
                            format: 'DD-MM-YYYY'
                        },
                    });
                    $('#ctl00_ContentPlaceHolder1_dtpEffectFromDate').val('');

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
                <script>
                    function validation_2Absent(txtMarks) {
                        var FinalAmount = 0;
                        var txtMaxMark = 100;
                        if (txtMarks.value != "") {

                            if (txtMarks != null && txtMarks.value != "") {
                                if (isNaN(txtMarks.value)) {
                                    document.getElementById(txtMarks.id).value = '';
                                }
                            }
                            if (Number(txtMarks.value) > Number(txtMaxMark)) {
                                alert("Percentage should not be Greater Than " + txtMaxMark + "");
                                txtMarks.value = '';
                                return;
                            }
                        }
                    }
    </script>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

