<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BranchChangeAppered.aspx.cs" Inherits="ACADEMIC_ExamRegistration" Title=""
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:Panel ID="pnlStart" runat="server">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div id="div1" runat="server"></div>
                    <div class="box-header with-border"> 
                          <h3 class="box-title">APPLY BRANCH CHANGE</h3>
                    </div>

                    <div class="box-body">
                        <div class="col-12" id="divNote" runat="server" visible="false">
                            <div class="row">
                                <div class="col-12">
                                    <div>
                                        <div class="exam-note">
                                            <h5 class="heading">Note (Steps To Follow For MakeUp Exam Registration)</h5>
                                            <p><span>1.</span> Please click Proceed to Exam Registration button.</p>
                                            <p><span>2.</span> Please select the one semester in Arrear Semester dropdownlist and Click the show button.</p>
                                            <p><span>3.</span> Arrear (Supplimentary) Courses will be displayed on the below for selected semester.</p>
                                            <p><span>4.</span> Please verify and select Arrear (Supplimentary) Courses.</p>
                                            <p><span>5.</span> Finally Click the Submit Button.</p>
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnProceed" runat="server" Text="Proceed to Exam Registration" OnClick="btnProceed_Click" CssClass="btn btn-outline-info"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:Panel ID="pnlSearch" runat="server">
                            <div class="col-12" id="tblSearch">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Enter Roll No</label>
                                        </div>
                                        <asp:TextBox ID="txtEnrollno" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:Button ID="btnSearch" runat="server" OnClick="btnProceed_Click" Text="Show" CssClass="btn btn-outline-info"/>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <div id="divCourses" runat="server" visible="false">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                             <h5>Apply Branch Change</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12" id="tblInfo" runat="server">
                                <div class="row">
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Student Name :</b>
                                                <a class="sub-label"><asp:Label ID="lblName" runat="server" Font-Bold="True" /> </a>
                                            </li>
                                            <li class="list-group-item"><b></b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblFatherName" runat="server" Font-Bold="False" />&nbsp;
                                                    <asp:Label ID="lblMotherName" runat="server" Font-Bold="False" />
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Enrollment No. :</b>
                                                <a class="sub-label"><asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Admission Batch :</b>
                                                <a class="sub-label"><asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>        
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Semester :</b>
                                                <a class="sub-label"><asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label> </a>
                                            </li>
                                            <li class="list-group-item"><b>Degree / Branch :</b>
                                                <a class="sub-label"><asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label> </a>
                                            </li>
                                            <li class="list-group-item"><b>PH :</b>
                                                <a class="sub-label"><asp:Label ID="lblPH" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Regulation :</b>
                                                <a class="sub-label"><asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>        
                                        </ul>
                                    </div>

                                    <div class="col-lg-3 col-md-6 col-12">
                                        <asp:Image ID="imgPhoto" runat="server" Width="60%" Height="90%"
                                                ImageUrl="~/IMAGES/nophoto.jpg" />
                                    </div>

                                    <asp:HiddenField ID="hdfDegreeno" runat="server" />
                                    <asp:HiddenField ID="hdfCategory" runat="server" />
                                </div>

                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                             <h5><asp:Label ID="Label1" runat="server" Font-Bold="True">Please Select Branch Preference</asp:Label></h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Pref 1</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranchPref1" runat="server"
                                                AppendDataBoundItems="True" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlBranchPref1_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvPref" runat="server"
                                                ControlToValidate="ddlBranchPref1" Display="None"
                                                ErrorMessage="Please Select Pref1" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Pref 2</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranchPref2" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlBranchPref2_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Pref 3 </label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranchPref3" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlBranchPref3_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Pref 4 </label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranchPref4" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlBranchPref4_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Pref 5 </label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranchPref5" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlBranchPref5_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Pref 6 </label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranchPref6" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlBranchPref6_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Pref 7 </label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranchPref7" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlBranchPref7_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Pref 8 </label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranchPref8" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlBranchPref8_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Pref 9 </label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranchPref9" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlBranchPref9_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Pref 10 </label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranchPref10" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-12">
                                        <div class=" note-div"> 
                                            <h5 class="heading">Note</h5> 
                                            <p>
                                                <i class="fa fa-star" aria-hidden="true"></i>
                                                <span> 
                                                    Student submitting application for change of branch are advised to consult their parent before exercising the above option. 
                                                    Once the branch is allotted as per the above option, it will be mandatory to the student to accept the change and no further request will be entertained. 
                                                    Further, student submitting the option will be deemed to have consulted with their parents and no further request from the parent shall be entertained in this regard. 
                                                    Student may also exercise option of branch where vacancies are indicated as NIL as vacancies may create subsequently in allotment process.
                                                </span> 
                                            </p>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" 
                                        OnClientClick="javascript:return confirm('Once the branch is allotted as per the above option, it will be mandatory to the student to accept the change and no further request will be entertained....Are you sure?');" 
                                        Text=" Submit" CssClass="btn btn-outline-info" ValidationGroup="Submit" />
                                    <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text=" Print " Visible="false" CssClass="btn btn-outline-primary" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" />
                                    
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                        DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />
        <div id="divMsg" runat="server">
        </div>
    </asp:Panel>

    <script type="text/javascript" language="javascript">
        function SelectAll(headchk) {
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
        }


        function CheckSelectionCount(chk) {
            var count = -1;
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (count == 2) {
                    chk.checked = false;
                    alert("You have reached maximum limit!");
                    return;
                }
                else if (count < 2) {
                    if (e.checked == true) {
                        count += 1;
                    }
                }
                else {
                    return;
                }
            }
        }
    </script>
</asp:Content>
