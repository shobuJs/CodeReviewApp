<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="AdmissionApprovalByAdmin.aspx.cs" Inherits="ACADEMIC_AdmissionApprovalByAdmin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script>
        function UserConfirmation() {
            return confirm("Are you sure you want to submit?");
        }
    </script>

    <script>
        function myFunction() {
            debugger;
            var browser;
            var control = '<%=hdnfldVariable.ClientID%>';

            if ((navigator.userAgent.indexOf("Opera") || navigator.userAgent.indexOf('OPR')) != -1) {
                document.getElementById(control).value = 'opera';
            }
            else if (navigator.userAgent.indexOf("Chrome") != -1) {
                document.getElementById(control).value = 'chrome';
            }
            else if (navigator.userAgent.indexOf("Safari") != -1) {
                document.getElementById(control).value = 'safari';
            }
            else if (navigator.userAgent.indexOf("Firefox") != -1) {
                document.getElementById(control).value = 'firefox';
            }
            else if ((navigator.userAgent.indexOf("MSIE") != -1) || (!!document.documentMode == true)) //IF IE > 10
            {
                document.getElementById(control).value = 'iexplore';
            }
            else {
                document.getElementById(control).value = '';
            }
        }
    </script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updAdmissionDetails"
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

    <asp:UpdatePanel ID="updAdmissionDetails" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border"> 
                            <h3 class="box-title">STUDENT VERIFICATION BY ADMISSION SECTION</h3>
                        </div>

                        <div class="box-body">
                            <asp:HiddenField ID="hdnfldVariable" runat="server" />
                            <div class="col-12" id="divSearch" runat="server">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading"><h5>Search Student</h5></div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Application No.</label>
                                        </div>
                                        <asp:TextBox ID="txtApplNo" runat="server" CssClass="form-control" ToolTip="Enter Admission No." TabIndex="1" placeholder="Please Enter Application No." />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" TabIndex="2" CssClass="btn btn-outline-info" ValidationGroup="search" OnClick="btnSearch_Click" />
                                            <%--OnClientClick="myFunction()"--%>
                                        <asp:RequiredFieldValidator ID="valSearchText" runat="server" ControlToValidate="txtApplNo" Display="None"
                                            ErrorMessage="Please Enter Application No.!" SetFocusOnError="true" ValidationGroup="search" Width="10%" />
                                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                                            ValidationGroup="search" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divimg" runat="server" visible="false">
                                        <asp:Image ID="imgPhoto" runat="server" Height="100px" Width="94px" /></td>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none" id="DivApproveDegree" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Already Approve Degrees</label>
                                        </div>
                                        <asp:Label ID="lblAppliedDegrees" runat="server" CssClass="pull-left"></asp:Label>
                                    </div>

                                </div>
                            </div>

                            <div id="divdata" runat="server" visible="false">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading"><h5>Student Information</h5></div>
                                        </div>

                                        <div class="col-lg-6 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Student Name :</b>
                                                    <a class="sub-label"><asp:Label ID="lblStudName" runat="server" Font-Bold="True" ToolTip='<%#Eval("IDNO") %>' /> </a>
                                                </li>
                                                <li class="list-group-item"><b>DOB :</b>
                                                    <a class="sub-label"><asp:Label ID="lblDOB" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>College :</b>
                                                    <a class="sub-label"><asp:Label ID="lblCollege" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>Email :</b>
                                                    <a class="sub-label"><asp:Label ID="lblEmail" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>   
                                                <li class="list-group-item d-none"><b>In-House :</b>
                                                    <a class="sub-label"><asp:Label ID="lblIN_HOUSE" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>        
                                            </ul>
                                        </div>

                                        <div class="col-lg-6 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item d-none"><b>12 % / CGPA :</b>
                                                    <a class="sub-label"><asp:Label ID="lbl12Per" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>   
                                                <li class="list-group-item"><b>Application No. :</b>
                                                    <a class="sub-label"><asp:Label ID="lblApplNo" runat="server" Font-Bold="True" /> </a>
                                                </li>
                                                <li class="list-group-item"><b>Degree :</b>
                                                    <a class="sub-label"><asp:Label ID="lblDegree" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>Mobile :</b>
                                                    <a class="sub-label"><asp:Label ID="lblMobile" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item d-none"><b>Undertaking :</b>
                                                    <a class="sub-label"><asp:Label ID="lblUndertaking" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>   
                                                 
                                            </ul>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12" id="dvUG" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading"><h5>Student UG Information</h5></div>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Qualify Exam :</b>
                                                    <a class="sub-label"><asp:Label ID="lblQlyExam" runat="server" Font-Bold="True" /> </a>
                                                </li>
                                                <li class="list-group-item"><b>Board/University :</b>
                                                    <a class="sub-label"><asp:Label ID="lblBoardUni" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>Per/CGPA :</b>
                                                    <a class="sub-label"><asp:Label ID="lblPercentage" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>       
                                            </ul>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Subjects :</b>
                                                    <a class="sub-label"><asp:Label ID="lblSubject" runat="server" Font-Bold="True"></asp:Label> </a>
                                                </li>
                                                <li class="list-group-item"><b>School Name :</b>
                                                    <a class="sub-label"><asp:Label ID="lblSchool" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>      
                                            </ul>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Year Of Passing :</b>
                                                    <a class="sub-label"><asp:Label ID="lblYearPass" runat="server" Font-Bold="True"></asp:Label> </a>
                                                </li>
                                                <li class="list-group-item"><b>Month of Passing :</b>
                                                    <a class="sub-label"><asp:Label ID="lblMonthPass" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>      
                                            </ul>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="col-12 mt-3" id="divDoc" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading"><h5>Document List</h5></div>
                                    </div>
                                    <div class="col-12">
                                        <asp:ListView ID="lvDocument" runat="server">
                                            <EmptyDataTemplate>
                                                <p class="text-center text-bold">
                                                    <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Record Found"></asp:Label>
                                                </p>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Sr. No.</th>
                                                            <th>Delete</th>
                                                            <th>Document Name</th>
                                                            <th>View/ Download</th>
                                                            <th>Verified</th>
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
                                                        <%# Container.DataItemIndex + 1%>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("DOCUMENTNO")%>'
                                                            AlternateText='<%# Eval("FILENAME") %>' ImageUrl="~/images/delete.gif" ToolTip="Delete" OnClientClick="return confirm('Do you really want to delete record?')"
                                                            OnClick="btnDelete_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDocument" runat="server" Text='<%#Eval("DOCUMENTNAME")%>'></asp:Label>
                                                        <asp:HiddenField ID="hdnDocno" runat="server" Value='<%# Eval("DOCUMENTNO") %>' />
                                                        <asp:HiddenField ID="hdnDoc" runat="server" Value='<%# Eval("DOCUMENTNAME") %>' />
                                                        <asp:HiddenField ID="hdfFilePath" runat="server" Value='<%# Eval("PATH") %>' />
                                                        <%--<asp:HiddenField ID="hdnDoc" runat="server" Value='<%# Eval("DOCUMENTNO") %>' />--%>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click" CommandArgument='<%# Eval("FILENAME") %>' ToolTip='<%# Eval("DOCUMENTNO") %>'><image style="height:35px" src="../IMAGES/viewdetail.png"></image></asp:LinkButton>
                                                        <%--<image id="btnView" runat="server" style="height: 35px" tabindex="3" type="button" UseSubmitBehavior="False" OnServerClick="btnView_Click" src="../IMAGES/view.png" data-toggle="modal" data-target="#myModal22"></image>--%>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkVerified" runat="server" TabIndex="4" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                            </div>
                        
                            <div class="col-12 d-none" id="divBoard" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading"><h5>Board Studied  (As per the 12th Appeared Exam)</h5></div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session List</label>
                                        </div>
                                        <asp:RadioButton ID="rbMaha" runat="server" AutoPostBack="true" Style="padding-left: 0px" TabIndex="6" TextAlign="Right"
                                            GroupName="Board" Font-Bold="true" OnCheckedChanged="rbMaha_CheckedChanged" EnableViewState="true" />
                                        <label style="font-size: 15px; padding-left: 0px; font-weight: bold">Maharashtra Board</label>

                                        <asp:RadioButton ID="rbOther" runat="server" AutoPostBack="true" TabIndex="7" TextAlign="Right" GroupName="Board" Font-Bold="true"
                                            OnCheckedChanged="rbOther_CheckedChanged" EnableViewState="true" />
                                        <label style="font-size: 15px; padding-left: 0px; font-weight: bold">Other Board</label>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="col-12 mt-3" id="divPayment" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading"><h5>Payment</h5></div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="1"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lblDegreeSelected" runat="server" Visible="false"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0"
                                            ValidationGroup="g">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="1"
                                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0"
                                            ValidationGroup="g">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Payment Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlPaymentType" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="5"
                                            OnSelectedIndexChanged="ddlPayment_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                       <%-- <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlPaymentType" Display="None"
                                            ErrorMessage="Please Select Payment Type" InitialValue="0" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:Label ID="lblAmt" runat="server" Font-Bold="true" Text="Amount To Be Paid : "></asp:Label>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Applied Programe</label>
                                        </div>
                                        <asp:Label ID="lblPrograme" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Approved College</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true"
                                            CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select Approved College" InitialValue="0" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 mt-3" id="divApprove" runat="server" visible="false">
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-12 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Status</label>
                                        </div>
                                        <asp:RadioButton ID="rbApprove" runat="server" AutoPostBack="true" Style="padding-left: 0px" TabIndex="6" TextAlign="Right"
                                            GroupName="Approve" Font-Bold="true" OnCheckedChanged="rbApprove_CheckedChanged" Enabled="false" />
                                        <label>Approve</label>
                                        
                                        <asp:RadioButton ID="rbReject" runat="server" AutoPostBack="true" TabIndex="7" TextAlign="Right" GroupName="Approve"
                                            Font-Bold="true" OnCheckedChanged="rbReject_CheckedChanged" Enabled="false" />
                                        <label>Reject</label>
                                       
                                        <asp:RadioButton ID="rbHold" runat="server" AutoPostBack="true" TabIndex="8" TextAlign="Right" GroupName="Approve"
                                            Font-Bold="true" OnCheckedChanged="rbHold_CheckedChanged" Enabled="false" />
                                        <label>On Hold</label>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-12 col-12" id="divRemark" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Remark</label>
                                        </div>
                                        <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" TabIndex="9" CssClass="form-control" MaxLength="200" Visible="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer" id="divSuccess" runat="server" visible="false">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmit_Click" ValidationGroup="submit" TabIndex="10" Visible="true" Text="Submit" OnClientClick="if ( ! UserConfirmation()) return false;" />
                                <asp:Button ID="btnReport" runat="server" CssClass="btn btn-outline-primary" OnClick="btnReport_Click" Text="Admission Details Report" TabIndex="12" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" Visible="true" Text="Cancel" TabIndex="11" />
                                
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit" />
                            </div>

                        </div>
                    </div>
                </div>
            </div>

            <div id="myModal22" class="modal fade" role="dialog">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="margin-top: -25px">
                        <div class="modal-body">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" style="margin-top:-18px">x</button>
                            </div>

                            <iframe id="iframe1" runat="server" frameborder="0" width="100%" height="800px"></iframe>
                            <%--<iframe id="iframe1" runat="server" src="../FILEUPLOAD/Transcript Report.pdf" frameborder="0" width="100%" height="547px"></iframe>--%>                        

                            <div class="modal-footer" style="height: 0px">
                                <button type="button" style="margin-top: -10px" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnCancel" />
              <asp:PostBackTrigger ControlID="lvDocument" />           
        </Triggers>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server"></div>

</asp:Content>
