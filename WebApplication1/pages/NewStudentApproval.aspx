<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="NewStudentApproval.aspx.cs" Inherits="Academic_NewStudentApproval" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="pnlFeeTable"
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
        $('#myDatepicker2').datetimepicker({
            format: 'DD/MM/YYYY'
        });
    </script>

    <style>
        @media (min-width:576px) and (max-width:991px) {
            .ipad-view {
                margin-bottom:45px;
            }
        }
    </style>

    <asp:UpdatePanel ID="pnlFeeTable" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><span>New Student Registration Approval</span></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Search Student</h5>
                                </div>

                                <div class="col-12" id="dvdisplay" runat="server">
                                    <div class="row">
                                        <div class="col-lg-9 col-md-9 col-12" style="padding-right: 0px; padding-left:0px;">
                                            <div class="row">
                                                <div class="form-group col-lg-4 col-md-7 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Temp. Adm. No. : </label>
                                                    </div>
                                                    <span class="form-inline">
                                                        <asp:TextBox ID="txtEnrollmentSearch" runat="server" ValidationGroup="Show" width="160px" class="form-control"
                                                            PlaceHolder="Enter Temp. Adm. No."></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtEnrollmentSearch"
                                                            Display="None" ErrorMessage="Please Enter Temp. Adm. No.!" ValidationGroup="Show">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                            ShowSummary="False" ValidationGroup="Show" />
                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-outline-info m-top"
                                                            ValidationGroup="Show" />                                                      
                                                    </span>
                                                </div>
                                                
                                                <div class="form-group col-lg-4 col-md-5 col-12">
                                                    <ul class="list-group list-group-unbordered mt-3">
                                                        <li class="list-group-item"><b>Temp. Adm. No. :</b>
                                                            <a class="sub-label"><asp:Label ID="lblapp" runat="server" Text="" Font-Bold="true"></asp:Label> </a>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="form-group col-lg-4 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Student Email Id </label>
                                                    </div>
                                                    <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="Enter Student Email Id"></asp:TextBox>
                                                    <asp:Label ID="lblEmail" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                                                </div>
                                                <div class="form-group col-lg-4 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Student Mobile Number </label>
                                                    </div>
                                                    <asp:TextBox ID="txtMobile" runat="server" class="form-control" placeholder="Enter Student Mobile Number"></asp:TextBox>
                                                    <asp:Label ID="lblmobile" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                                                </div>
                                                <div class="form-group col-lg-4 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Student Name </label>
                                                    </div>
                                                    <asp:TextBox ID="txtStudName" runat="server" class="form-control" placeholder="Enter Student Name"></asp:TextBox>
                                                    <asp:Label ID="lblname" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                                                </div>
                                                <div class="form-group col-lg-4 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Student Initial/Surname </label>
                                                    </div>
                                                    <asp:TextBox ID="txtStudInitial" runat="server" class="form-control" placeholder="Enter Student Initial/Surname"></asp:TextBox>
                                                    <asp:Label ID="lbllastname" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-lg-3 col-md-3 col-12">
                                            <div class="col-md-12">
                                                <%--  <label>Student Photo</label>--%>
                                                <div class="flex-box-inner text-center">
                                                    <asp:Image runat="server" ID="imgpreview" Height="125" Width="125" Style="border: 1px solid rgba(0,0,0,0.4)" Visible="false" />
                                                </div>
                                            </div>
                                            <div class="col-md-12 form-group" id="divAdmNo" runat="server" visible="false">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Admission No. :</b>
                                                        <a class="sub-label"><asp:Label ID="lblenrollno" runat="server" Text="" Font-Bold="true"></asp:Label> </a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                        
                                        <div class="col-lg-4 col-md-6 col-12 pl-md-0 pl-3">
                                            <ul class="list-group list-group-unbordered ipad-view">
                                                <li class="list-group-item"><b>Degree Name :</b>
                                                    <a class="sub-label"><asp:Label ID="lbldegree" runat="server" Text="" Font-Bold="true"></asp:Label> </a>
                                                </li>
                                                <li class="list-group-item"><b>Branch Name :</b>
                                                    <a class="sub-label"><asp:Label ID="lblbranch" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>Semester/ Year :</b>
                                                    <a class="sub-label"><asp:Label ID="lblsem" runat="server" Text="" ToolTip="1" Font-Bold="true"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>Session :</b>
                                                    <a class="sub-label"><asp:Label ID="lblSession" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                </li>        
                                            </ul>
                                        </div>

                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Community :</b>
                                                    <a class="sub-label"><asp:Label ID="lblcategory" runat="server" Text="" Font-Bold="true"></asp:Label> </a>
                                                </li>
                                                <li class="list-group-item"><b>Admission Batch :</b>
                                                    <a class="sub-label"><asp:Label ID="lbladmbatch" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>Hostel Facility :</b>
                                                    <a class="sub-label"><asp:RadioButton ID="rdbhostelyes" runat="server" Text="YES" GroupName="Link2" Enabled="false" TabIndex="6" />
                                                        &nbsp;&nbsp;&nbsp;
                                                        <asp:RadioButton ID="rdbhostelNo" runat="server" Text="NO" GroupName="Link2" Enabled="false" TabIndex="6" />
                                                    </a>
                                                </li>
                                                <li class="list-group-item" style="display: none"><b>Hostel Type :</b>
                                                    <a class="sub-label"><asp:DropDownList ID="ddlhostel" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </a>
                                                </li> 
                                            </ul>
                                        </div>

                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Transport Facility :</b>
                                                    <a class="sub-label"><asp:RadioButton ID="rdbtransportyes" runat="server" Text="YES" GroupName="Link3" Enabled="false" TabIndex="6" />
                                                        &nbsp;&nbsp;&nbsp;
                                                        <asp:RadioButton ID="rdbtransportNo" runat="server" Text="NO" GroupName="Link3" Enabled="false" TabIndex="6" />
                                                    </a>
                                                </li>  
                                                <li class="list-group-item" style="display: none"><b>Transport With AC :</b>
                                                    <a class="sub-label">
                                                        <asp:RadioButton ID="rdbTransportACyes" runat="server" Text="YES" GroupName="Link4" Enabled="false" TabIndex="6" />
                                                        &nbsp;&nbsp;&nbsp;
                                                        <asp:RadioButton ID="rdbTransportACno" runat="server" Text="NO" GroupName="Link4" Enabled="false" TabIndex="6" />
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Fees Amount :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblAmount" runat="server" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Fees Paid Status :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblFeeStatus" runat="server" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>      
                                            </ul>
                                        </div>

                                        <div class="clearfix"></div>
                                        
                                        <div class="form-group col-lg-3 col-md-6 col-12 pt-md-3">
                                            <div class="label-dynamic">
                                                <label> Admission Quota</label>
                                            </div>
                                            <asp:DropDownList ID="ddlAdmQuota" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlAdmQuota"
                                                Display="None" ErrorMessage="Please Select Admission Quota" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="show">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlAdmQuota"
                                                Display="None" ErrorMessage="Please Select Admission Quota" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="submit">
                                            </asp:RequiredFieldValidator>
                                            <asp:Label ID="lbladmquota" runat="server" Visible="false" Text="" Font-Bold="true"></asp:Label>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 pt-md-3">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label> Student Admission Date</label>
                                            </div>
                                            <div class='input-group date'>
                                                <span class="input-group-addon">
                                                    <i id="txtAdmdate1" class="fa fa-calendar"></i>
                                                </span>
                                                <asp:TextBox runat="server" ID="txtAdmdate" class="form-control" data-inputmask="'mask': '99/99/9999'"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtAdmdate" PopupButtonID="txtAdmdate1" Enabled="true"
                                                    EnableViewState="true">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="valStartDate" runat="server" ControlToValidate="txtAdmdate"
                                                    Display="None" ErrorMessage="Please Enter Student Admission Date." SetFocusOnError="true"
                                                    ValidationGroup="show" />
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 pt-md-3">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label> Payment Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlpaytype" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlpaytype_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlpaytype"
                                                Display="None" ErrorMessage="Please Select Payment Type" InitialValue="0" SetFocusOnError="True" ValidationGroup="submit">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlpaytype"
                                                Display="None" ErrorMessage="Please Select Payment Type" InitialValue="0" SetFocusOnError="True" ValidationGroup="show">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                       
                                        <div class="form-group col-md-3 col-sm-6 col-xs-12" style="display: none">
                                            <asp:FileUpload ID="fuAttachment" runat="server" />
                                        </div>

                                    </div>
                                </div>

                            </div>

                            <div class="col-12" id="dvaction" runat="server">
                                <div class="btn-footer">
                                    <asp:HiddenField ID="hdfadmbatch" runat="server" />
                                    <asp:Button ID="btnapproval" runat="server" Text="Approve " OnClick="btnapproval_Click" ValidationGroup="show" CssClass="btn btn-outline-info"
                                        OnClientClick="return confirm ('Do you want to Approve This Student!');" />

                                    <asp:Button ID="btncancel" runat="server" Text="Cancel " OnClick="btncancel_Click" CssClass="btn btn-outline-danger" />
                                    <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Report" Visible="false" />
                                    <asp:Label ID="lblLabel" runat="server" Visible="true"></asp:Label>
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False"
                                        ValidationGroup="submit" />
                                     <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="show" />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnapproval" />
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnSearch" />
        </Triggers>
    </asp:UpdatePanel>

  
    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '0';
                alert('Only Numeric Value Allowed!');
                txt.focus();
                return;
            }
        };
    </script>

    <script>
        function showpreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgpreview').css('visibility', 'visible');
                    $('#imgpreview').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>

</asp:Content>
