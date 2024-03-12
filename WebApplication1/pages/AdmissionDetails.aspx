<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdmissionDetails.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="ACADEMIC_AdmissionDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 500px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updAdmissionDetails"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px; text-align: center">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updAdmissionDetails" runat="server">
        <ContentTemplate>
            <div class="box box-info">
                <div class="box-header with-border">
                    <span class="glyphicon glyphicon-user text-blue"></span>
                    <h3 class="box-title">STUDENT INFORMATION</h3>
                    <div class="box-tools pull-right">
                    </div>
                </div>
                <div class="box-body">

                    <div class="row">
                        <div class="col-md-3" id="divtabs" runat="server">
                            <div class="col-md-12">
                                <div class="panel panel-info" style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);">
                                    <div class="panel panel-heading"><b>Click To Open Respective Information</b></div>
                                    <div class="panel-body">
                                        <aside class="sidebar">

                                            <!-- sidebar: style can be found in sidebar.less -->
                                            <section class="sidebar" style="background-color: #12aae2">
                                                <ul class="sidebar-menu">
                                                    <!-- Optionally, you can add icons to the links -->
                                                    <br />
                                                    <div id="divhome" runat="server">

                                                        <li class="treeview">&nbsp; <i class="fa fa-search"><span>
                                                            <asp:LinkButton runat="server" ID="lnkGoHome"
                                                                ToolTip="Please Click Here To Go To Home" OnClick="lnkGoHome_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Search New Student"> 

                                                            </asp:LinkButton>
                                                        </span>
                                                        </i>
                                                            <hr />
                                                        </li>
                                                    </div>
                                                    <li class="treeview">&nbsp <i class="fa fa-user"><span>
                                                        <asp:LinkButton runat="server" ID="lnkPersonalDetail"
                                                            ToolTip="Please select Personal Details." OnClick="lnkPersonalDetail_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Personal Details"> 

                                                        </asp:LinkButton>
                                                    </span>
                                                    </i>
                                                        <hr />
                                                    </li>

                                                    <li class="treeview">&nbsp <i class="fa fa-map-marker"><span>
                                                        <asp:LinkButton runat="server" ID="lnkAddressDetail"
                                                            ToolTip="Please select Address Details." OnClick="lnkAddressDetail_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Address Details"> 
                                                        </asp:LinkButton>
                                                    </span>
                                                    </i>

                                                        <hr />
                                                    </li>

                                                    <div id="divadmissiondetailstreeview" runat="server">
                                                        <li class="treeview">&nbsp<i class="fa fa-university"><span>
                                                            <asp:LinkButton runat="server" ID="lnkAdmissionDetail"
                                                                ToolTip="Please Click Here For Personal Details." OnClick="lnkAdmissionDetail_Click" Style="color: yellow; font-size: 16px;" Text="Admission Details"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        </i>

                                                            <hr />
                                                        </li>
                                                    </div>
                                                    <li class="treeview">
                                                        <i class="fa fa-info-circle"><span>
                                                            <asp:LinkButton runat="server" ID="lnkDasaStudentInfo"
                                                                ToolTip="Please Click Here For DASA Student Information." OnClick="lnkDasaStudentInfo_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="DASA Student Information"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        </i>
                                                        <hr />
                                                    </li>
                                                    <li class="treeview">&nbsp<i class="fa fa-graduation-cap"><span>
                                                        <asp:LinkButton runat="server" ID="lnkQualificationDetail"
                                                            ToolTip="Please Click Here For Qualification Details." OnClick="lnkQualificationDetail_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Qualification Details"> 
                                                        </asp:LinkButton>



                                                    </span>
                                                    </i>
                                                        <hr />
                                                    </li>
                                                    <li class="treeview">&nbsp<i class="fa fa-link"><span>
                                                        <asp:LinkButton runat="server" ID="lnkotherinfo"
                                                            ToolTip="Please Click Here For Other Information." OnClick="lnkotherinfo_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Other Information"> 
                                                        </asp:LinkButton>
                                                    </span>
                                                    </i>
                                                        <p></p>
                                                        <hr />
                                                    </li>
                                                    <li class="treeview">&nbsp;<i class="glyphicon glyphicon-print"><span>
                                                        <asp:LinkButton runat="server" ID="lnkprintapp" OnClick="lnkprintapp_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Print"></asp:LinkButton>
                                                    </span>

                                                    </i>
                                                        <p></p>
                                                    </li>
                                                </ul>
                                            </section>
                                        </aside>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="AdmDetails" runat="server" class="col-md-9">
                            <div class="box box-info">
                                <div class="box-header with-border" id="trAdmission" runat="server">
                                    <h1 class="box-title"><b>Admission Details</b></h1>
                                    <div class="box-tools pull-right">
                                    </div>
                                    <div>
                                        <span style="color: red">*</span> This Information is very critical. A separate permission
                                    is required to change this critical data.
                                    </div>
                                </div>
                                <br />
                                <div style="color: Red; font-weight: bold; margin-top: -20px; margin-right: -5px;">
                                    &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                </div>
                                <div class="box-body">
                                    <div id="divAdmissionDetails" runat="server" style="display: block;">
                                        <div id="tbladmission">
                                            <div class="form-group col-md-4">
                                                <label>Date of Admission</label>
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtDateOfAdmission" runat="server" Enabled="False" ToolTip="Please Enter Date Of Addmission"
                                                        TabIndex="1" CssClass="noteditable form-control" />
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtDateOfAdmission" PopupButtonID="imgAdmDate" Enabled="True">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="meeAdmDate" runat="server" TargetControlID="txtDateOfAdmission"
                                                        Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                        CultureTimePlaceholder="" Enabled="True" />
                                                </div>
                                            </div>

                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;">*</span> School/College Admitted</label>
                                                <asp:DropDownList ID="ddlSchoolCollege" runat="server" ValidationGroup="Academic" AutoPostBack="true"
                                                    CssClass="noteditable form-control" AppendDataBoundItems="true" ToolTip="Please Select School Admitted"
                                                    TabIndex="2" Enabled="False" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlSchoolCollege"
                                                    InitialValue="0" Display="None" SetFocusOnError="True" ErrorMessage="Please Select School Admitted"
                                                    ValidationGroup="Academic">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;">*</span> Degree</label>
                                                <asp:DropDownList ID="ddlDegree" runat="server" AutoPostBack="true" AppendDataBoundItems="True"
                                                    ToolTip="Please Select Degree" TabIndex="3" ValidationGroup="Academic" Enabled="false"
                                                    CssClass="form-control" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" />
                                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                    InitialValue="0" Display="None" SetFocusOnError="True" ErrorMessage="Please Select Degree"
                                                    ValidationGroup="Academic">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;">*</span> Branch</label>
                                                <asp:DropDownList ID="ddlBranch" runat="server" ValidationGroup="Academic"
                                                    CssClass="form-control" AppendDataBoundItems="true" ToolTip="Please Select Branch"
                                                    TabIndex="4" Enabled="False">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlBranch" runat="server" ControlToValidate="ddlBranch"
                                                    ErrorMessage="Please Select Branch" Display="None" ValidationGroup="Academic"
                                                    SetFocusOnError="true" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;">*</span> Admission Batch</label>
                                                <asp:DropDownList ID="ddlBatch" runat="server" AppendDataBoundItems="True"
                                                    ToolTip="Please Select Batch" TabIndex="5" ValidationGroup="Academic" Enabled="false"
                                                    CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="rfvBatch" runat="server" ControlToValidate="ddlBatch"
                                                    Display="None" SetFocusOnError="True" ErrorMessage="Please Select Batch" ValidationGroup="Academic"
                                                    InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;">*</span> Year</label>
                                                <asp:DropDownList ID="ddlYear" runat="server" AppendDataBoundItems="True"
                                                    ToolTip="Please Enter Year" TabIndex="6" ValidationGroup="Academic" CssClass="form-control"
                                                    Enabled="False" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" AutoPostBack="true" />
                                                <asp:RequiredFieldValidator ID="rfvYear" runat="server" ControlToValidate="ddlYear"
                                                    Display="None" ErrorMessage="Please Select Year" SetFocusOnError="True" ValidationGroup="Academic"
                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;">*</span> Semester</label>
                                                <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True"
                                                    ToolTip="Please select Semester" TabIndex="7" ValidationGroup="Academic" CssClass="form-control"
                                                    Enabled="False" />
                                                <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                                    Display="None" SetFocusOnError="True" ErrorMessage="Please Select Semester" ValidationGroup="Academic"
                                                    InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;">*</span> Claimed Category</label>
                                                <asp:DropDownList ID="ddlclaim" runat="server" CssClass="form-control"
                                                    AppendDataBoundItems="True" TabIndex="8" ToolTip="Please Enter Claimed Category"
                                                    ValidationGroup="Academic" Enabled="False">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlclaim"
                                                    Display="None" SetFocusOnError="True" ErrorMessage="Please Select Claimed Category" ValidationGroup="Academic"
                                                    InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;">*</span> Alloted Category</label>
                                                <asp:DropDownList ID="ddlAllotedCategory" runat="server" CssClass="form-control"
                                                    AppendDataBoundItems="True" TabIndex="9" ToolTip="Please Enter Alloted Category"
                                                    ValidationGroup="Academic" Enabled="False">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvPayType" runat="server" ControlToValidate="ddlAllotedCategory"
                                                    Display="None" SetFocusOnError="True" ErrorMessage="Please Select Alloted Category" ValidationGroup="Academic"
                                                    InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                            </div>


                                            <%--Shrikant Ramekar Added 21 feb 2019--%>
                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;"></span>SR No.</label>
                                                <asp:TextBox ID="txtSRNO" runat="server" CssClass="form-control" ValidationGroup="Academic" Enabled="False" TabIndex="10"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;"></span>SR Category</label>
                                                <asp:DropDownList ID="ddlSRCatg" runat="server" CssClass="form-control"
                                                    AppendDataBoundItems="True" TabIndex="11" ToolTip="Please Select Category"
                                                    ValidationGroup="Academic" Enabled="False">
                                                </asp:DropDownList>
                                            </div>                                        

                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;"></span>College Code</label>
                                                <asp:DropDownList runat="server" ID="ddlcolcode" CssClass="form-control" AppendDataBoundItems="true" TabIndex="12">
                                                    <asp:ListItem Text="Please select" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="B292" Value="B292"></asp:ListItem>
                                                    <asp:ListItem Text="B292BC" Value="B292BC"></asp:ListItem>
                                                    <asp:ListItem Text="B292BD" Value="B292BD"></asp:ListItem>
                                                    <asp:ListItem Text="B292BR" Value="B292BR"></asp:ListItem>
                                                    <asp:ListItem Text="C480" Value="C480"></asp:ListItem>
                                                    <asp:ListItem Text="E021" Value="E021"></asp:ListItem>
                                                    <asp:ListItem Text="E057" Value="E057"></asp:ListItem>
                                                    <asp:ListItem Text="E721" Value="E721"></asp:ListItem>
                                                    <asp:ListItem Text="L480" Value="L480"></asp:ListItem>
                                                    <asp:ListItem Text="T606" Value="T606"></asp:ListItem>
                                                    <asp:ListItem Text="T873" Value="T873"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;"></span>Payement Type</label>
                                                <asp:DropDownList ID="ddlPaymentTp" runat="server" CssClass="form-control"
                                                    AppendDataBoundItems="True" TabIndex="13" ToolTip="Please Select Payment Type"
                                                    ValidationGroup="Academic" Enabled="False">
                                                </asp:DropDownList>

                                            </div>
                                            <div class="form-group col-md-4" style="display: none;">
                                                <label>State of Eligibility</label>
                                                <asp:DropDownList ID="ddlStateOfEligibility" runat="server" TabIndex="14" AppendDataBoundItems="True"
                                                    ToolTip="Please Select State Of Eligibility" ValidationGroup="Academic"
                                                    Enabled="False" CssClass="form-control" />
                                            </div>

                                            <div class="form-group col-md-4" style="display: none;">
                                                <label>Hosteler</label>
                                                <div class="radio">
                                                    <label>
                                                        <asp:RadioButton ID="rdoHostelerYes" runat="server" Text="Yes" GroupName="Hosteler"
                                                            Enabled="False" />
                                                    </label>

                                                    <label>
                                                        <asp:RadioButton ID="rdoHostelerNo" runat="server" Text="No" GroupName="Hosteler"
                                                            Checked="True" Enabled="False" />
                                                    </label>

                                                </div>

                                            </div>

                                            <div class="form-group col-md-4" style="display: none;">
                                                <label>Document List</label>
                                                <asp:CheckBoxList ID="chkDoc" runat="server" BorderColor="#FF9900" BorderStyle="Solid"
                                                    BorderWidth="1px" CellPadding="2" CellSpacing="2" RepeatColumns="3" RepeatDirection="Horizontal"
                                                    Font-Size="8pt" CssClass="form-control">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="box box-footer text-center">
                                    <asp:Button ID="btnSubmit" runat="server" TabIndex="15" Text="Save & Continue >>" ToolTip="Click to Submit"
                                        class="btn btn-outline-primary" OnClick="btnSubmit_Click" ValidationGroup="Academic" />

                                    &nbsp<asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="Academic" />

                                    <button runat="server" id="btnGohome" tabindex="16" visible="false" onserverclick="btnGohome_Click" class="btn btn-outline-danger btnGohome" tooltip="Click to Go Back Home">
                                        <i class="fa fa-home"></i>Go Back Home
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server"></div>
</asp:Content>
