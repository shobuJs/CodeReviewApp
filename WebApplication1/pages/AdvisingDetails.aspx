<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AdvisingDetails.aspx.cs" Inherits="AdvisingDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updAdvising"
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

    <asp:UpdatePanel ID="updAdvising" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                            <%--   <h3 class="box-title">Advising Details</h3>--%>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-lg-5 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>
                                                <asp:Label ID="lblStudName" runat="server"></asp:Label>
                                            </b>
                                                <a class="sub-label">
                                                    <asp:Label ID="BindStudentName" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>
                                                <asp:Label ID="lblStudentId" runat="server"></asp:Label></b>
                                                <a class="sub-label">
                                                    <asp:Label ID="BindStdID" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Date Of Birth </b>
                                                <a class="sub-label">
                                                    <asp:Label ID="BindDOB" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>
                                                <asp:Label ID="lblGender" runat="server"></asp:Label></b>
                                                <a class="sub-label">
                                                    <asp:Label ID="BindGender" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>
                                                <asp:Label ID="lblMobile" runat="server"></asp:Label></b>
                                                <a class="sub-label">
                                                    <asp:Label ID="BindMobNo" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="col-lg-5 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">

                                            <li class="list-group-item"><b>
                                                <asp:Label ID="lblStudentEmail" runat="server"></asp:Label></b>
                                                <a class="sub-label">
                                                    <asp:Label ID="BindEmailID" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>
                                                <asp:Label ID="lblDyProgram" runat="server"></asp:Label></b>
                                                <a class="sub-label">
                                                    <asp:Label ID="BindProgram" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>
                                                <asp:Label ID="lblDYSemester" runat="server"></asp:Label></b>
                                                <a class="sub-label">
                                                    <asp:Label ID="BindSem" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Previous School </b>
                                                <a class="sub-label">
                                                    <asp:Label ID="BindPreviousSchool" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <%--   <li class="list-group-item"><b>Documents View :</b>
                                        <a class="sub-label">
                                            <i class="fa fa-eye text-success" aria-hidden="true"></i>
                                        </a>
                                    </li>--%>
                                        </ul>
                                    </div>

                                    <div class="col-lg-2 col-md-6 col-12 mt-2 text-center">
                                        <asp:Image ID="imgPhoto" runat="server" ImageUrl="~/IMAGES/nophoto.jpg" Style="width: 110px; height: auto;" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 mt-3">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblActive" runat="server" Font-Bold="true"></asp:Label>
                                            <%--  <label>Status</label>--%>
                                        </div>
                                        <asp:DropDownList runat="server" ID="ddlStatus" class="form-control" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="1">Approve</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblRemark" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtRemark" runat="server" TabIndex="1" CssClass="form-control" placeholder="Enter Remark" TextMode="MultiLine" Rows="1" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtRemark"
                                            Display="None" ErrorMessage="Please Enter Remark" ValidationGroup="Submit"
                                            SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup> </sup>
                                            <asp:Label ID="lblNotExempted" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:CheckBox runat="server" ID="chkNotExempted" OnCheckedChanged="chkNotExempted_CheckedChanged" AutoPostBack="true" />
                                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtRemark"
                                            Display="None" ErrorMessage="Please Enter Remark" ValidationGroup="Submit"
                                            SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>


                            <div class="col-12 mt-3" runat="server" id="DivExempted">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Credited <small class="text-danger">(Exempted from Study)</small></h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-4 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblPreviousSubjectCode" runat="server" Font-Bold="true"></asp:Label>
                                            <%--   <label>Previous Subject Code</label>--%>
                                        </div>
                                        <asp:TextBox ID="txtPreSubCode" runat="server" TabIndex="1" CssClass="form-control" palceholder="e.g. CS101" MaxLength="30"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPreSubCode"
                                            Display="None" ErrorMessage="Please Enter Previous Subject Code" ValidationGroup="AddCourse"
                                            SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-4 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblPreviousSubjName" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtPreSubName" runat="server" TabIndex="1" CssClass="form-control" palceholder="e.g. Computeer Science" MaxLength="60"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPreSubName"
                                            Display="None" ErrorMessage="Please Enter Previous Subject Name" ValidationGroup="AddCourse"
                                            SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-1 col-md-4 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYCredits" runat="server" Font-Bold="true"></asp:Label>

                                        </div>
                                        <asp:TextBox ID="txtUnits" runat="server" TabIndex="1" CssClass="form-control" palceholder="e.g. 2" onblur="return IsNumeric(this);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtUnits"
                                            Display="None" ErrorMessage="Please Enter Units" ValidationGroup="AddCourse"
                                            SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-1 col-md-4 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblGradeNew" runat="server" Font-Bold="true"></asp:Label>

                                        </div>
                                        <asp:TextBox ID="txtGrade" runat="server" TabIndex="1" CssClass="form-control" palceholder="e.g. A" MaxLength="5" onkeyup="this.value=this.value.toUpperCase()"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtGrade"
                                            Display="None" ErrorMessage="Please Enter Grade" ValidationGroup="AddCourse"
                                            SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-5 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblCurriculumSubject" runat="server" Font-Bold="true"></asp:Label>

                                        </div>
                                        <asp:DropDownList ID="ddlCurriculumSubject" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCurriculumSubject"
                                            Display="None" ErrorMessage="Please Select Curriculum Subject" ValidationGroup="AddCourse"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-1 col-md-2 col-12">
                                        <asp:LinkButton runat="server" ID="btnAddSubject" OnClick="btnAddSubject_Click" ValidationGroup="AddCourse" class="fa fa-plus text-success border border-success rounded p-2 mt-3"></asp:LinkButton>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="AddCourse"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>
                                </div>
                                <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvExemptedfromStudy" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading" id="dem">
                                                    <h5>
                                                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h5>
                                                </div>

                                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>
                                                                <asp:Label ID="lblPreviousSubjectCode" runat="server"></asp:Label></th>
                                                            <th>
                                                                <asp:Label ID="lblPreviousSubjName" runat="server"></asp:Label></th>
                                                            <td>
                                                                <asp:Label ID="lblDYCredits" runat="server"></asp:Label></td>
                                                            <th>
                                                                <asp:Label ID="lblGradeNew" runat="server"></asp:Label></th>
                                                            <th>
                                                                <asp:Label ID="lblCurriculumSubject" runat="server"></asp:Label></th>
                                                            <th>
                                                                <asp:Label ID="lblDyRemove" runat="server">Remove</asp:Label></th>
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
                                                    <asp:Label runat="server" ID="lblpCode" Text='<%# Eval("PSubjectCode")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblpCourseName" Text='<%# Eval("PSubjectName")%>'></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:Label runat="server" ID="lblpUnit" Text='<%# Eval("PUnits")%>'></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:Label runat="server" ID="lblpGrade" Text='<%# Eval("PGrade")%>'></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:Label runat="server" ID="lblCourseNameNew" Text='<%# Eval("PCurriculumSubject")%>'></asp:Label>
                                                    <asp:Label runat="server" ID="lblCourseNo" Text='<%# Eval("PCourseNo")%>' Visible="false"></asp:Label>
                                                    <asp:Label runat="server" ID="lblPrev_No" Text='<%# Eval("PEquSubNo")%>' Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:LinkButton runat="server" ID="btnRemove" OnClick="btnRemove_Click" CommandArgument='<%#Eval("PCourseNo") %>' CommandName='<%#Eval("PEquSubNo")%>' OnClientClick="return confirm('Are you sure you want to delete Exempted Subject?');"><i class="fa fa-close" style="font-size:25px;color:red"></i></asp:LinkButton></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            </div>
                            
                            <div class="col-12 mt-3 d-none">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Not Credited <small class="text-danger">(Need to Study)</small></h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Curriculum Subject</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCurriculumSubjectNot" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-1 col-md-2 col-12">
                                        <i class="fa fa-plus text-success border border-success rounded p-2 mt-3" aria-hidden="true"></i>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 mt-3" runat="server" id="DivAdditional">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Additional Learning </h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-4 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblAddPreviousSubjectCode" runat="server" Font-Bold="true"></asp:Label>
                                            <%--  <label>Previous Subject Code</label>--%>
                                        </div>
                                        <asp:TextBox ID="txtPreSubCodeAdd" runat="server" TabIndex="1" CssClass="form-control" palceholder="e.g. CS101" MaxLength="30"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPreSubCodeAdd"
                                            Display="None" ErrorMessage="Please Enter Previous Subject Code" ValidationGroup="AdditionalCourse"
                                            SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-4 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblAddPreviousSubjName" runat="server" Font-Bold="true"></asp:Label>
                                            <%--      <label>Previous Subject Name</label>--%>
                                        </div>
                                        <asp:TextBox ID="txtPreSubNameAdd" runat="server" TabIndex="1" CssClass="form-control" palceholder="e.g. Computeer Science" MaxLength="60"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtPreSubNameAdd"
                                            Display="None" ErrorMessage="Please Enter Previous Subject Name" ValidationGroup="AdditionalCourse"
                                            SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-1 col-md-4 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Units</label>
                                        </div>
                                        <asp:TextBox ID="txtUnitsAdd" runat="server" TabIndex="1" CssClass="form-control" palceholder="e.g. 2" onblur="return IsNumeric(this);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtUnitsAdd"
                                            Display="None" ErrorMessage="Please Enter Previous Units" ValidationGroup="AdditionalCourse"
                                            SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-1 col-md-4 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Grade</label>
                                        </div>
                                        <asp:TextBox ID="txtGradeAdd" runat="server" TabIndex="1" CssClass="form-control" palceholder="e.g. A" MaxLength="5" onkeyup="this.value=this.value.toUpperCase()"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtGradeAdd"
                                            Display="None" ErrorMessage="Please Enter Grade" ValidationGroup="AdditionalCourse"
                                            SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-1 col-md-2 col-12">
                                        <asp:LinkButton runat="server" ID="btnAdditionalSubj" OnClick="btnAdditionalSubj_Click" ValidationGroup="AdditionalCourse" class="fa fa-plus text-success border border-success rounded p-2 mt-3"></asp:LinkButton>
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="AdditionalCourse"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>
                                </div>
                                <div class="col-12">
                                    <asp:Panel ID="Panel2" runat="server">
                                        <asp:ListView ID="lvAdditional" runat="server">
                                            <LayoutTemplate>
                                                <div id="demo-grid">
                                                    <div class="sub-heading" id="dem">
                                                        <h5>
                                                            <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h5>
                                                    </div>

                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>
                                                                    <asp:Label ID="lblAddPreviousSubjectCode" runat="server"></asp:Label></th>
                                                                <th>
                                                                    <asp:Label ID="lblAddPreviousSubjName" runat="server"></asp:Label></th>
                                                                <td>
                                                                    <asp:Label ID="lblGradeNew" runat="server"></asp:Label></td>
                                                                <th>
                                                                    <asp:Label ID="lblDYGradePoint" runat="server"></asp:Label></th>
                                                                <th>
                                                                    <asp:Label ID="lblDyRemove" runat="server">Remove</asp:Label></th>
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
                                                        <asp:Label runat="server" ID="lblpAddiCode" Text='<%# Eval("PSubjectCode")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblpAddiCourseName" Text='<%# Eval("PSubjectName")%>'></asp:Label>
                                                    </td>

                                                    <td>
                                                        <asp:Label runat="server" ID="lblpAddiUnit" Text='<%# Eval("PUnits")%>'></asp:Label>
                                                    </td>

                                                    <td>
                                                        <asp:Label runat="server" ID="lblpAddiGrade" Text='<%# Eval("PGrade")%>'></asp:Label>
                                                        <asp:Label runat="server" ID="lblAddNo" Text='<%# Eval("PEquSubNo")%>' Visible="false"></asp:Label>

                                                    </td>
                                                    <td>
                                                        <asp:LinkButton runat="server" ID="btnRemoveAdditi" OnClick="btnRemoveAdditi_Click" CommandArgument='<%#Eval("PSubjectCode") %>' CommandName='<%#Eval("PEquSubNo")%>' OnClientClick="return confirm('Are you sure you want to delete Additional ?');"><i class="fa fa-close" style="font-size:25px;color:red"></i></asp:LinkButton></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </div>


                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" TabIndex="1" OnClick="btnSubmit_Click" ValidationGroup="Submit">Submit</asp:LinkButton>
                                <asp:LinkButton ID="btncanceldata" runat="server" CssClass="btn btn-outline-danger" TabIndex="1" OnClick="btncanceldata_Click">Cancel</asp:LinkButton>
                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="Submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        $(document).ready(function () {
            //--------------------------------//
            $(".reject-remark").hide();
            $(".approve-status").hide();

            //----------------- Status DropDown JS --------------------//
            $('#ddlStatus').on('change', function (e) {
                var optionSelected = $("option:selected", this);
                var valueSelected = this.value;
                if (this.value == '0') {
                    $(".reject-remark").hide();
                    $(".approve-status").hide();
                }
                if (this.value == '1') {
                    $(".approve-status").show();
                    $(".reject-remark").hide();
                }
                if (this.value == '2') {
                    $(".reject-remark").show();
                    $(".approve-status").hide();
                }
            });

        });
    </script>
    <script type="text/javascript">
        function IsNumeric(txt) {
            var ValidChars = "0123456789.";
            var num = true;
            var mChar;
            cnt = 0

            for (i = 0; i < txt.value.length && num == true; i++) {
                mChar = txt.value.charAt(i);

                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt.value = '';
                    alert("Please enter Numeric values only")
                    txt.select();
                    txt.focus();
                }
            }
            return num;
        }
    </script>
    <%--   <script>
        $('#FirstName').bind('keyup', function () {

            // Get the current value of the contents within the text box
            var val = $('#FirstName').val().toUpperCase();

            // Reset the current value to the Upper Case Value
            $('#FirstName').val(val);

        });
    </script>--%>
</asp:Content>

