<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ReviewRegistration.aspx.cs"
    Inherits="ACADEMIC_ReviewRegistration" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
 <div style="z-index: 1; position: absolute; top: 10%; left: 40%;">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updDetails"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updDetails" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">


                        <div class="box-header with-border">
                            <h3 class="box-title">REVIEW REGISTRATION</h3>
                            <div class="box-tools pull-right">
                            </div>
                        </div>

                   

                            <div class="box-body" id="divCourses" runat="server" visible="false">

                             <div class="col-md-12" id="tblSession" runat="server" visible="false">
                                    <div class="col-md-4">
                                        <label><span style="color: red;">* </span> Examination</label>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlSession" runat="server" Display="None"
                                            InitialValue="0" ErrorMessage="Please Select Examination." ValidationGroup="Show" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlSession" runat="server" Display="None"
                                            InitialValue="0" ErrorMessage="Please Select Examination." ValidationGroup="Submit" />
                                    </div>
                                    <div class="col-md-4"  >
                                        <label><span style="color: red;">* </span> Univ. Reg. No. / Adm. No.</label>
                                        <asp:TextBox ID="txtRollNo" runat="server" MaxLength="15" CssClass="form-control" />
                                         <asp:RequiredFieldValidator ID="rfvRollNo" ControlToValidate="txtRollNo" runat="server" 
                                          Display="None" ErrorMessage="Please Enter Univ. Reg. No. / Adm. No." ValidationGroup="Show" />

                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtRollNo" runat="server" Display="None" 
                                     ErrorMessage="Please Enter Univ. Reg. No. / Adm. No." ValidationGroup="Submit" />
                                    </div>
                                    <div class="col-md-4" style="margin-top: 25px">
                                        <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show" Font-Bold="true" ValidationGroup="Show" CssClass="btn btn-outline-info" />

                                        <asp:Button ID="btnCancel" runat="server" Text="Clear" Font-Bold="true" CssClass="btn btn-outline-danger" CausesValidation="false" OnClick="btnCancel_Click" />

                                        <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Show" />
                                    </div>
                                    <div class="col-md-12" style="margin-top: 25px">
                                        <p class="text-center">
                                        </p>
                                    </div>
                                </div>

                                <br />

                                <div class="col-md-12" id="tblInfo" runat="server" visible="false">

                                    <div class="col-md-6">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item">
                                                <b>Student Name :</b><a class="pull-right">
                                                    <asp:Label ID="lblName" runat="server" Font-Bold="True" /></a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Father's Name :</b><a class="pull-right">
                                                    <asp:Label ID="lblFatherName" runat="server" Font-Bold="True" /></a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Mother's Name :</b><a class="pull-right">
                                                    <asp:Label ID="lblMotherName" runat="server" Font-Bold="True" /></a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Univ. Reg. No. / Adm. No. :</b><a class="pull-right">
                                                    <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Admission Batch :</b><a class="pull-right">
                                                    <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>

                                        </ul>
                                    </div>

                                    <div class="col-md-6">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item">
                                                <b>College Name :</b><a class="pull-right">
                                                    <asp:Label ID="lblCollegeName" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Degree / Branch :</b><a class="pull-right">
                                                    <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                                <asp:HiddenField ID="hfDegreeNo" runat="server" />
                                            </li>
                                            <li class="list-group-item">
                                                <b>Phone No. :</b><a class="pull-right">
                                                    <asp:Label ID="lblPH" runat="server" Style="font-weight: 700"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Regulation :</b><a class="pull-right">
                                                    <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Semester :</b><a class="pull-right">
                                                    <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                                <asp:Label ID="lblOrderID" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                                            </li>
                                            <li class="list-group-item" style="display: none">
                                                <b>Total Subjects :</b>
                                                <a class="pull-right">
                                                    <asp:TextBox ID="txtAllSubjects" runat="server" Enabled="false" Text="0" Style="text-align: center;"></asp:TextBox>
                                                </a>
                                            </li>
                                            <li class="list-group-item" style="display: none">
                                                <b>Total Credits :</b>
                                                <a class="pull-right">
                                                    <asp:TextBox ID="txtCredits" runat="server" Enabled="false" Text="0" Style="text-align: center;"></asp:TextBox>
                                                </a>
                                                <asp:HiddenField ID="hdfCredits" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="col-md-4"></div>

                                </div>

                                <div class="col-md-7 text-center" style="margin-left: -60px; color: white; font-size: 14px; font-weight: bold" id="divNote" runat="server" visible="false">
                                    .
                                </div>

                                <div class="col-md-2 text-center" style="margin-left: 470px" id="divSem" runat="server" visible="false">
                                    <label>Apply For Semester</label>
                                    <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" AppendDataBoundItems="True" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlSemester" runat="server" Display="None"
                                        InitialValue="0" ErrorMessage="Please Select Semester." ValidationGroup="Submit" />
                                </div>

                                <div class="col-md-3 text-center" style="margin-left: 830px; margin-top: -22px;" id="divTotalCourseAmount" runat="server" visible="false">
                                    <label style="color: forestgreen; font-weight: bold; font-size: 16px;">Total Amount :</label>
                                    &nbsp; 
                                    <asp:Label ID="lblTotalAmount" runat="server" Style="color: darkgreen; font-weight: bold; font-size: 16px;"></asp:Label>
                                </div>

                                <div class="col-md-12">
                                    <asp:Label ID="lblErrorMsg" runat="server" Style="color: red; font-size: medium; font-weight: bold;" Text="">
                                    </asp:Label>
                                </div>
                          
                           </div>

                               <div class="box-footer">

                                <div class="col-md-12" id="divAllCoursesFromHist" runat="server" visible="false">
                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                        <asp:ListView ID="lvCurrentSubjects" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <h3><label class="label label-default">Subject List for Review</label></h3>
                                                        <table id="example" class="table table-hover table-bordered table-responsive">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th style="text-align: center;">Select
                                                                </th>
                                                                <th style="text-align: center;">Sr. No.</th>
                                                                <th style="text-align: center;">Subject Code
                                                                </th>
                                                                <th style="text-align: center;">Subject Name
                                                                </th>
                                                                <th style="text-align: center;">Semester
                                                                </th>
                                                                <th style="text-align: center;" hidden="hidden">Grades
                                                                </th>
                                                                <th style="text-align: center;">DD DETAILS
                                                                </th>
                                                                <%--<th style="text-align: center;">Oral Mark
                                                        </th>--%>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                        </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr id="trCurRow">
                                                    <td style="text-align: center;">
                                                        <asp:CheckBox ID="chkAccept" runat="server" ToolTip='<%# Eval("COURSENO")%>' ClientIDMode="Static"
                                                            AutoPostBack="true" OnCheckedChanged="chkAccept_CheckedChanged" />
                                                        <%-- onclick="CalTotalFee(this)" Enabled='<%# Eval("FAIL_MORE_THAN_2_SUB").ToString()== "1" ? false : true %>'--%>
                                                    </td>

                                                    <td style="text-align: center;"><%# Container.DataItemIndex+1 %></td>

                                                    <td style="text-align: center;">
                                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' ToolTip='<%# Eval("SCHEMENO") %>' />
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:Label ID="lblSEMSCHNO" runat="server" Text='<%# Eval("SEMESTER") %>' ToolTip='<%# Eval("SEMESTERNO") %>' />
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:Label ID="lblExtermark" runat="server" Text='<%# Eval("GRADE") %>' Visible="false" />
                                                           <asp:Label ID="lblMarks" runat="server" Text='<%# Eval("EXTERMARK") %>' Visible="false"/>
                                                        <asp:TextBox ID="txtddno" runat="server" MaxLength="20" CssClass="form-control" Text='<%# Eval("DDNO") %>' ></asp:TextBox>
                                                        <%--<asp:HiddenField ID="hdnddno" runat="server" Value='<%# Eval("DD_NO")%>' />      --%>   
                                                    </td>                                                 
                                                    <%--<td>
                                                <asp:Label ID="LblOral" runat="server" Text='<%# Eval("S2MARK") %>' />
                                            </td>--%>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                   
                                          </asp:Panel>

                                     <p class="text-center">
                                        <asp:HiddenField ID="requestparams" runat="server"></asp:HiddenField>

                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" Visible="false"
                                            ValidationGroup="Submit" OnClientClick="return showConfirm();" CssClass="btn btn-outline-info" />

                                        <asp:Button ID="btnPrintRegSlip" runat="server" Text="Registration Slip" OnClick="btnPrintRegSlip_Click"
                                            Visible="false" CssClass="btn btn-outline-primary" />

                                          <%-- <asp:Button ID="btnCancel" runat="server" Text="Clear" Font-Bold="true" CssClass="btn btn-outline-danger" CausesValidation="false" 
                                               OnClick="btnCancel_Click" />--%>
                                      
                                           <asp:ValidationSummary ID="vssubmit" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="Submit" />
                                    </p>
                                   

                                </div>

                            
                          <%--     <div class="col-md-5 text-center" style="margin-left: 630px; margin-top: -22px;" id="divRegisteredCoursesTotalAmt" runat="server" visible="false">
                                    <label style="color: darkgreen; font-weight: bold; font-size: 16px;">Registered Subjects Total Amount :</label>
                                    &nbsp; 
                                    <asp:Label ID="lblRegisteredCoursesTotalAmt" runat="server" Style="color: darkgreen; font-weight: bold; font-size: 16px;"></asp:Label>
                                  </div>--%>

                              
                                <div class="col-md-12" id="divRegCourses" runat="server" visible="false">
                                    <asp:Panel ID="pnlFinalCourses" runat="server" ScrollBars="Auto">
                                        <asp:ListView ID="lvFinalCourses" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <h3>
                                                        <label class="label label-default">Registered Subject List for Revaluation</label></h3>
                                                    <table id="tblCurrentSubjects" class="table table-hover table-bordered table-responsive">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th style="text-align: center;">Sr. No.</th>
                                                                <th style="text-align: center;">Subject Code</th>
                                                                <th style="text-align: center;">Subject Name</th>
                                                                <th style="text-align: center;">Semester</th>
                                                                <th style="text-align: center;">Grades</th>
                                                                <th style="text-align: center;">Amount</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                        </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr id="trCurRow">
                                                    <td style="text-align: center;"><%# Container.DataItemIndex+1 %></td>
                                                    <td style="text-align: center;">
                                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>'  />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:Label ID="lblSEMSCHNO" runat="server" Text='<%# Eval("SEMESTERNAME") %>'  />
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:Label ID="lblExtermark" runat="server" Text='<%# Eval("GRADE") %>' />
                                                          <asp:Label ID="lblMarks" runat="server" Text='<%# Eval("EXTERMARK") %>' Visible="false"/>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("RF_AMOUNT") %>' />
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
            <%-- <asp:AsyncPostBackTrigger ControlID="btnPrintRegSlip" EventName="Click"/>--%>
            <asp:PostBackTrigger ControlID="btnPrintRegSlip" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
            
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
    <script type="text/javascript" language="JavaScript">

        //function FreezeScreen(msg) {
        //    scroll(0, 0);
        //    var outerPane = document.getElementById('FreezePane');
        //    var innerPane = document.getElementById('InnerFreezePane');
        //    if (outerPane) outerPane.className = 'FreezePaneOn';
        //    if (innerPane) innerPane.innerHTML = msg;
        //}

        function showConfirm() {
            var ret = confirm('Do you Really want to Submit this Subjects for Review Registration?');
            if (ret == true) {
                //FreezeScreen('Please Wait, Your Data is Being Processed...');
                validate = true;
            }
            else
                validate = false;
            return validate;
        }


        function showPayConfirm() {
            var ret = confirm('Applied Revaluation details Will be confirmed only after successful payments.Do you Really want to Pay Amount Online ?');
            if (ret == true) {
                //FreezeScreen('Please Wait, Your Data is Being Processed...');
                validate = true;
            }
            else
                validate = false;
            return validate;
        }


        function showChallanConfirm() {
            var ret = confirm('Do you Really want to Print Revaluation Challan ?');
            if (ret == true) {
                //FreezeScreen('Please Wait, Your Data is Being Processed...');
                validate = true;
            }
            else
                validate = false;
            return validate;
        }
    </script>
  

     <script src="../Content/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("[id$='cbHead']").live('click', function () {
                $("[id$='chkAccept']").attr('checked', this.checked);
            });
        });
    </script>


</asp:Content>
