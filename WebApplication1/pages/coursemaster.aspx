<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="coursemaster.aspx.cs" Inherits="Administration_courseMaster" Title="" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UPDCOURSE"
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
    <asp:UpdatePanel runat="server" ID="UPDCOURSE" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><span>Subject Creation</span></h3>
                        </div>   
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-lg-6 col-md-12 col-12">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <div class="row">
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>College/School Name</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                        CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCollege"
                                                        Display="None" ErrorMessage="Please Select College/School Name" InitialValue="0" ValidationGroup="submit" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="ddlCollege"
                                                        Display="None" ErrorMessage="Please Select College/School Name" InitialValue="0" ValidationGroup="modify"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="ddlCollege"
                                                        Display="None" ErrorMessage="Please Select College/School Name" InitialValue="0" ValidationGroup="CheckList"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Degree</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                        CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged1">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDegree"
                                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="submit" />
                                                    <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlDegree"
                                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="modify"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlDegree"
                                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="CheckList"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Department Name</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                        CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged1">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDept"
                                                        Display="None" ErrorMessage="Please Select Department" InitialValue="0" ValidationGroup="submit" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlDept"
                                                        Display="None" ErrorMessage="Please Select Department" InitialValue="0" ValidationGroup="modify"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlDept"
                                                        Display="None" ErrorMessage="Please Select Department" InitialValue="0" ValidationGroup="CheckList"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Branch</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                         AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlBranch"
                                                        Display="None" ErrorMessage="Please Select Branch" InitialValue="0"
                                                        ValidationGroup="submit" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlBranch"
                                                        Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="modify"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlBranch"
                                                        Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="CheckList"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Curriculum</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlScheme"
                                                        Display="None" ErrorMessage="Please Select Curriculum" InitialValue="0" ValidationGroup="submit" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlScheme"
                                                        Display="None" ErrorMessage="Please Select Curriculum" InitialValue="0" ValidationGroup="modify"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlScheme"
                                                        Display="None" ErrorMessage="Please Select Curriculum" InitialValue="0" ValidationGroup="CheckList"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Existing Subjects / Paper</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlExtCourse" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                        Enabled="False">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlExtCourse"
                                                        Display="None" ErrorMessage="Please Select Existing Subjects" InitialValue="0" ValidationGroup="modify"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                    
                                            <div class="col-12">
                                                <asp:Label ID="lblMsg" runat="server" SkinID="lblmsg" /><br />
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="ModifyCourse" runat="server" OnClick="btnModifyCourse_Click" Text="Modify Existing Subject"
                                                    ValidationGroup="modify" CssClass="btn btn-outline-info" />
                                                <asp:Button ID="btnReset" runat="server" CausesValidation="false" OnClick="btnReset_Click"
                                                    Text="Reset" CssClass="btn btn-outline-info" />
                                                <asp:Button ID="btnCheckListReport" runat="server"
                                                    Text="Check List Report" CssClass="btn btn-outline-primary" ValidationGroup="CheckList"
                                                    OnClick="btnCheckListReport_Click" />

                                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="modify" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="CheckList" />
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    
                                    <div class="col-lg-6 col-md-12 col-12">
                                        <div class="sub-heading">
                                            <h5>Pre-Defined Mark Pattern for Respective Exams</h5>
                                        </div>

                                        <asp:Panel ID="Panel2" runat="server">
                                            <table class="table table-striped table-bordered nowrap display" style="width:100%">
                                                <asp:Repeater ID="rtpScheme" runat="server">
                                                    <HeaderTemplate>
                                                        <%--<div id="demo-grid">
                                                            <div class="sub-heading">
                                                                <h5>Marks</h5>
                                                            </div>
                                                        </div>--%>
                                                        <thead  class="bg-light-blue">
                                                            <tr>
                                                                <th>Exam
                                                                </th>
                                                                <th>Exam Name
                                                                </th>
                                                                <th>Passing marks
                                                                </th>
                                                                <th>Total Marks
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <%--<tr id="itemPlaceholder" runat="server" />--%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblFldName" Text=' <%# Eval("FLDNAME")%>' ToolTip=' <%# Eval("EXAMNO")%>'
                                                                    runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <%# Eval("EXAMNAME")%>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMinMarks" runat="server" TextMode="SingleLine" MaxLength="3"
                                                                    Text='<%#Eval("MIN") %>' CssClass="form-control"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtMinMarks" runat="server" FilterType="Numbers"
                                                                    TargetControlID="txtMinMarks">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMaxMarks" runat="server" TextMode="SingleLine" MaxLength="3"
                                                                    Text='<%#Eval("MAX") %>' CssClass="form-control"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtMaxMarks" runat="server" FilterType="Numbers"
                                                                    TargetControlID="txtMaxMarks">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </tbody>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </asp:Panel>
                                        
                                        <div class="col-12 btn-footer mt-3" runat="server" id="trbtn">
                                            <asp:Button ID="btnUpdate" runat="server" CausesValidation="False" Text="Update" CssClass="btn btn-outline-info"
                                                ToolTip="Update Default Marks" Visible="true" OnClick="btnUpdate_Click" />
                                            <asp:Button ID="btnCancel1" runat="server" Text="Cancel" CausesValidation="False"
                                                OnClick="btnClear_Click" CssClass="btn btn-outline-info" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="col-12 mt-3">
                                <div class="sub-heading">
                                    <h5>Subject Details</h5>
                                </div>
                                        
                                <asp:Panel ID="pnl_course" runat="server">
                                    <div class="row">
                                    
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Subject Code</label>
                                            </div>
                                            <asp:TextBox ID="txtCCode" runat="server" MaxLength="15" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCCode"
                                                Display="None" ErrorMessage="Please Enter Subject Code" ValidationGroup="submit" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Subject Name</label>
                                            </div>
                                            <asp:TextBox ID="txtCourseName" runat="server" MaxLength="150" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCourseName"
                                                Display="None" ErrorMessage="Please Enter Subject Name" ValidationGroup="submit" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divd" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Specialization</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSpecialisation" runat="server" Enabled="False" AppendDataBoundItems="true"
                                                CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem>Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlSpecialisation"
                                                Display="None" ErrorMessage="Please Select Specialisation" InitialValue="0"
                                                ValidationGroup="submit" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Lecture</label>
                                            </div>
                                            <asp:TextBox ID="txtLectures" runat="server" MaxLength="3" onblur="AddLTP();" onkeyup="validateNumeric(this);"
                                                CssClass="form-control" />
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtLectures"
                                                Display="None" ErrorMessage="Please Enter Numeric Value for Lectures" Operator="DataTypeCheck"
                                                Type="Double" ValidationGroup="submit"></asp:CompareValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Tutorial/Seminar</label>
                                            </div>
                                            <asp:TextBox ID="txtTutorial" runat="server" MaxLength="3" onblur="AddLTP();" onkeyup="validateNumeric(this);"
                                                CssClass="form-control" />
                                            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtTutorial"
                                                Display="None" ErrorMessage="Please Enter Numeric Value for Tutorial" Operator="DataTypeCheck"
                                                Type="Double" ValidationGroup="submit"></asp:CompareValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtTutorial"
                                                Display="None" ErrorMessage="Please Enter Tutorial"
                                                ValidationGroup="submit" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Practical / Clinical</label>
                                            </div>
                                            <asp:TextBox ID="txtPract" runat="server" MaxLength="3" onblur="AddLTP();" onkeyup="validateNumeric(this);"
                                                CssClass="form-control" />
                                            <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtPract"
                                                Display="None" ErrorMessage="Please Enter Numeric Value for Practical" Operator="DataTypeCheck"
                                                Type="Double" ValidationGroup="submit"></asp:CompareValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>SDL/Drawing</label>
                                            </div>
                                            <asp:TextBox ID="txtDrawing" runat="server" MaxLength="3" onblur="AddLTP();"
                                                onkeyup="validateNumeric(this);" CssClass="form-control" />
                                            <asp:CompareValidator ID="CompareValidator6" runat="server"
                                                ControlToValidate="txtDrawing" Display="None"
                                                ErrorMessage="Please Enter Numeric Value for Practical"
                                                Operator="DataTypeCheck" Type="Double" ValidationGroup="submit"></asp:CompareValidator>
                                        </div>                                                

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Journal Club</label>
                                            </div>
                                            <asp:TextBox ID="txtJournalClub" runat="server" MaxLength="3" onblur="AddLTP();"
                                                onkeyup="validateNumeric(this);" CssClass="form-control" />
                                            <asp:CompareValidator ID="CompareValidator7" runat="server"
                                                ControlToValidate="txtJournalClub" Display="None"
                                                ErrorMessage="Please Enter Numeric Value for Journal Club"
                                                Operator="DataTypeCheck" Type="Double" ValidationGroup="submit"></asp:CompareValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Case Discussion</label>
                                            </div>
                                            <asp:TextBox ID="txtCaseDiscuss" runat="server" MaxLength="3" onblur="AddLTP();"
                                                onkeyup="validateNumeric(this);" CssClass="form-control" />
                                            <asp:CompareValidator ID="CompareValidator8" runat="server"
                                                ControlToValidate="txtCaseDiscuss" Display="None"
                                                ErrorMessage="Please Enter Numeric Value for Case Discussion"
                                                Operator="DataTypeCheck" Type="Double" ValidationGroup="submit"></asp:CompareValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Guest Lecture</label>
                                            </div>
                                            <asp:TextBox ID="txtGuestLecture" runat="server" MaxLength="3" onblur="AddLTP();"
                                                onkeyup="validateNumeric(this);" CssClass="form-control" />
                                            <asp:CompareValidator ID="CompareValidator9" runat="server"
                                                ControlToValidate="txtGuestLecture" Display="None"
                                                ErrorMessage="Please Enter Numeric Value for Guest Lecture"
                                                Operator="DataTypeCheck" Type="Double" ValidationGroup="submit"></asp:CompareValidator>
                                        </div>
                                        
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Poster Presentation</label>
                                            </div>
                                            <asp:TextBox ID="txtPosterPresentation" runat="server" MaxLength="3" onblur="AddLTP();"
                                                onkeyup="validateNumeric(this);" CssClass="form-control" />
                                            <asp:CompareValidator ID="CompareValidator10" runat="server"
                                                ControlToValidate="txtPosterPresentation" Display="None"
                                                ErrorMessage="Please Enter Numeric Value for Poster Presentation"
                                                Operator="DataTypeCheck" Type="Double" ValidationGroup="submit"></asp:CompareValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Oral Presentation</label>
                                            </div>
                                            <asp:TextBox ID="txtOralPresentation" runat="server" MaxLength="3" onblur="AddLTP();"
                                                onkeyup="validateNumeric(this);" CssClass="form-control" />
                                            <asp:CompareValidator ID="CompareValidator11" runat="server"
                                                ControlToValidate="txtOralPresentation" Display="None"
                                                ErrorMessage="Please Enter Numeric Value for Oral Presentation"
                                                Operator="DataTypeCheck" Type="Double" ValidationGroup="submit"></asp:CompareValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Publication</label>
                                            </div>
                                            <asp:TextBox ID="txtPublication" runat="server" MaxLength="3" onblur="AddLTP();"
                                                onkeyup="validateNumeric(this);" CssClass="form-control" />
                                            <asp:CompareValidator ID="CompareValidator12" runat="server"
                                                ControlToValidate="txtPublication" Display="None"
                                                ErrorMessage="Please Enter Numeric Value for Publication"
                                                Operator="DataTypeCheck" Type="Double" ValidationGroup="submit"></asp:CompareValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Total</label>
                                            </div>
                                            <asp:TextBox ID="txtTotal" runat="server" Enabled="false" MaxLength="3" CssClass="form-control" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Subject Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlTP" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlTP_SelectedIndexChanged">
                                                <asp:ListItem Value="0"><span style="color: red;">*</span> Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlTP"
                                                Display="None" ErrorMessage="Please Select Subject Type" InitialValue="0"
                                                ValidationGroup="submit" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Elective</label>
                                            </div>
                                            <asp:CheckBox ID="chkElective" runat="server" OnCheckedChanged="chkElective_CheckedChanged" AutoPostBack="true" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div5" runat="server">
                                            <div class="label-dynamic">
                                                <label>Elective Group</label>
                                            </div>
                                            <asp:DropDownList ID="ddlElectiveGroup" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" Enabled="false">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>BOS (Board Of Study) Dept.</label>
                                            </div>
                                            <asp:DropDownList ID="ddlParentDept" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlParentDept"
                                                Display="None" ErrorMessage="Please Select Parent(BOS) Department" InitialValue="0"
                                                ValidationGroup="submit" />
                                        </div>
                                        
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div1" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Total Hrs. In Semester</label>
                                            </div>
                                            <asp:TextBox ID="txtHrsInSem" runat="server" MaxLength="4" onkeyup="validateNumeric(this);"
                                                CssClass="form-control" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Subject Category</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCElectiveGroup" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="ddlCElectiveGroup"
                                                Display="None" ErrorMessage="Please Select Subject Category" InitialValue="0"
                                                ValidationGroup="submit" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Paper Hrs.</label>
                                            </div>
                                            <asp:TextBox ID="txtPaper" runat="server" CssClass="form-control" onkeyup="validateNumeric(this);"
                                                MaxLength="1">
                                            </asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Credits</label>
                                            </div>
                                            <asp:TextBox ID="txtTheory" runat="server" MaxLength="3" CssClass="form-control" onblur="AddLTP();" onkeyup="validateNumeric(this);" />
                                            <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="txtTheory"
                                                Display="None" ErrorMessage="Please Enter Numeric Value for Credit" Operator="DataTypeCheck"
                                                Type="Double" ValidationGroup="submit"></asp:CompareValidator>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtTheory"
                                                Display="None" ErrorMessage="Please Enter Credit Value"
                                                ValidationGroup="submit" />--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display:none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Prerequisite Credit Required</label>
                                            </div>
                                            <asp:TextBox ID="txtPreCredit" runat="server" MaxLength="3" CssClass="form-control" onkeyup="validateNumeric(this);" Visible="false" />
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPreCredit"
                                                Display="None" ErrorMessage="Please Enter Numeric Value for Prerequisite Credit"
                                                Operator="DataTypeCheck" Type="Integer" ValidationGroup="submit"></asp:CompareValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Ref. Material<span style="color: red; font-size: 10px;">(Upload Only Excel, PDF and Word file having 100kb size only)</span></label>
                                            </div>
                                            <asp:FileUpload ID="furefMaterial" runat="server" ValidationGroup="submit" ToolTip="Select file to upload"/>

                                            <div style="padding-top: 10px">
                                                <asp:LinkButton ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload" Visible="false"
                                                    ValidationGroup="submit" ToolTip="Click to Upload" class="btn btn-success"><i class="fa fa-upload" aria-hidden="true"></i> Upload</asp:LinkButton>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Lecture Hrs.</label>
                                            </div>
                                            <asp:TextBox ID="txtLectHours" runat="server" MaxLength="3" CssClass="form-control calc" />
                                        </div>
                                                
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Min. Lec. Hrs.</label>
                                            </div>
                                            <asp:TextBox ID="txtMinLHours" runat="server" MaxLength="3" CssClass="form-control" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Practical Hrs.</label>
                                            </div>
                                            <asp:TextBox ID="txtPracHours" runat="server" MaxLength="3" CssClass="form-control calc"/>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Min. Pr. Hrs.</label>
                                            </div>
                                            <asp:TextBox ID="txtMinPHours" runat="server" MaxLength="3" CssClass="form-control"/>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Clinical Hrs.</label>
                                            </div>
                                            <asp:TextBox ID="txtClinHours" runat="server" MaxLength="3" CssClass="form-control calc" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Min. Clin. Hrs.</label>
                                            </div>
                                            <asp:TextBox ID="txtMinCHours" runat="server" MaxLength="3" CssClass="form-control" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Integrated. Lec. Hrs.</label>
                                            </div>
                                            <asp:TextBox ID="txtIntegrateLHrs" runat="server" MaxLength="3" CssClass="form-control calc" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Min. Integrated Hrs.</label>
                                            </div>
                                            <asp:TextBox ID="txtMinIntegrateHrs" runat="server" MaxLength="3" CssClass="form-control" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Total Hrs.</label>
                                            </div>
                                            <asp:TextBox ID="txtTotalHours" runat="server" MaxLength="3" CssClass="form-control" Enabled="false" />
                                            <asp:HiddenField ID="hfTotal" runat="server" />
                                        </div>

                                        <div class="col-12 mt-3">
                                            <asp:Panel ID="Panel3" runat="server">
                                                <table class="table table-striped table-bordered nowrap display" style="width:100%" id="myTable1">
                                                    <asp:Repeater ID="lvCourseMaterial" runat="server">
                                                        <HeaderTemplate>
                                                            <div id="demo-grid">
                                                                <div class="sub-heading">
                                                                    <h5>Subject Material</h5>
                                                                </div>
                                                            </div>
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Subject Code
                                                                    </th>
                                                                    <th>Document Name
                                                                    </th>
                                                                    <th></th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <%--<tr id="itemPlaceholder" runat="server" />--%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <%#Eval("CCODE") %>                                                              
                                                                </td>
                                                                <td>
                                                                    <%#Eval("FILE_NAME") %>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="btnDownload" runat="server" Text="Download" CommandArgument='<%#Eval("FILE_NAME") %>'
                                                                        OnClick="btnDownload_Click"></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </tbody> 
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </asp:Panel>
                                        </div>

                                        <div class="col-12 btn-footer mt-3">
                                            <asp:Button ID="btnShow" runat="server" CausesValidation="False" OnClientClick="return ShowHideMarks();"
                                                Text="Pre-Defined Mark" class="btn btn-outline-info" Visible="false" />
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                                ValidationGroup="submit" class="btn btn-outline-info" />
                                            <asp:Button ID="btnCancel" class="btn btn-outline-danger" runat="server" Text="Cancel" CausesValidation="False"
                                                OnClick="btnCancel_Click" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />
                                        </div>

                                    </div>
                                </asp:Panel>
                            
                            </div>

                            <div class="col-12">
                                <%-- <div class="col-md-6">
                                    <div class="box box-primary">
                                        <asp:Panel ID="pnlPreCorList" runat="server" ScrollBars="Auto">
                                            <asp:ListView ID="lvCourse" runat="server" OnItemDataBound="lvCourse_ItemDataBound">
                                                <LayoutTemplate>
                                                    <div id="demo-grid">
                                                        <h4>Prerequisite Course List </h4>
                                                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="400px">
                                                            <table class="table table-bordered table-hover table-fixed">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>Select
                                                                        </th>
                                                                        <th>Code
                                                                        </th>
                                                                        <th>Course Name
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </asp:Panel>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                                        <td>
                                                            <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("CourseNo")%>' />
                                                            <asp:Label ID="lblCNO" runat="server" Text='<%# Eval("CNo")%>' Visible="false" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("CCode")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("Coursename")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
            <asp:PostBackTrigger ControlID="ModifyCourse" />
            <asp:PostBackTrigger ControlID="btnCheckListReport" />
            <asp:PostBackTrigger ControlID="btnReset" />
            <asp:PostBackTrigger ControlID="chkElective" />
            <asp:PostBackTrigger ControlID="lvCourseMaterial" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>

    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript" language="javascript">
        function ScalingZero(txt) {

            if (txt.value == '') {
                document.getElementById('ctl00_ContentPlaceHolder1_txtScaling').value = 0;
            }

        }
        function ShowHideMarks() {
            if (document.getElementById('pnlMarks').style.display == 'none')
                document.getElementById('pnlMarks').style.display = 'block';
            else
                document.getElementById('pnlMarks').style.display = 'none';

            return false;
        }

        function AddLTP() {
            var lec = document.getElementById("<%= txtLectures.ClientID %>").value == "" ? "0" : document.getElementById("<%= txtLectures.ClientID %>").value;
            var tut = document.getElementById("<%= txtTutorial.ClientID %>").value == "" ? "0" : document.getElementById("<%= txtTutorial.ClientID %>").value;
            var prac = document.getElementById("<%= txtPract.ClientID %>").value == "" ? "0" : document.getElementById("<%= txtPract.ClientID %>").value;

            var sdlDrw = document.getElementById("<%= txtDrawing.ClientID %>").value == "" ? "0" : document.getElementById("<%= txtDrawing.ClientID %>").value;
            var JuCb = document.getElementById("<%= txtJournalClub.ClientID %>").value == "" ? "0" : document.getElementById("<%= txtJournalClub.ClientID %>").value;
            var CaseDis = document.getElementById("<%= txtCaseDiscuss.ClientID %>").value == "" ? "0" : document.getElementById("<%= txtCaseDiscuss.ClientID %>").value;
            var Glec = document.getElementById("<%= txtGuestLecture.ClientID %>").value == "" ? "0" : document.getElementById("<%= txtGuestLecture.ClientID %>").value;
            var PoPr = document.getElementById("<%= txtPosterPresentation.ClientID %>").value == "" ? "0" : document.getElementById("<%= txtPosterPresentation.ClientID %>").value;
            var Orpr = document.getElementById("<%= txtOralPresentation.ClientID %>").value == "" ? "0" : document.getElementById("<%= txtOralPresentation.ClientID %>").value;
            var Pub = document.getElementById("<%= txtPublication.ClientID %>").value == "" ? "0" : document.getElementById("<%= txtPublication.ClientID %>").value;

            document.getElementById("<%= txtTotal.ClientID %>").value = parseInt(lec) + parseInt(tut) + parseInt(prac) + parseInt(sdlDrw) + parseInt(JuCb) + parseInt(CaseDis)
            + parseInt(Glec) + parseInt(PoPr) + parseInt(Orpr) + parseInt(Pub);
        }

        function validateNumeric(txt) {
            debugger;
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }

        function ShowHideElecGroup(chk) {
        }
      
    </script>

    <script>
         $(document).ready(function () {
             BindControls();
         });

         function BindControls() {
             $('.calc').keyup(function () {
                 calcAll();
             });
         }

         function calcAll(x) {

             var z = 0;
             debugger;
             for (var i = 0; i < $('.calc').length; i++) {

                 if ($('.calc').eq(i).val() != '') {
                     if (isNaN($('.calc').eq(i).val())) {
                         $('.calc').eq(i).val('');
                         alert('Only Numeric Characters Allowed!');
                         $('.calc').eq(i).focus();
                         return;
                     }
                 }

                 var y = $('.calc').eq(i).val() == '' ? 0 : parseInt($('.calc').eq(i).val());
                 z = parseInt(z) + parseInt(y);
             }
             $('#<%=hfTotal.ClientID %>').val(z);
            $('#<%=txtTotalHours.ClientID %>').val(z);
        }

        var req = Sys.WebForms.PageRequestManager.getInstance();
        req.add_endRequest(function () {
            BindControls();
        });
    </script>

</asp:Content>
