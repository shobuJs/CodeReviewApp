<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SignUp.aspx.cs" Inherits="ACADEMIC_SignUp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <%--<link href="../newbootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../studInfo/css/admin-style.css">--%>
    <link href="../studInfo/plugin/calendar/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <%--<link rel="stylesheet" type="text/css" href="css/font-awesome.min.css">--%>
    <style type="text/css">
        .form-group sup {color:#f00;}
        </style>

    <style>
        /* Radio Buttons */

[type="radio"]:checked,
[type="radio"]:not(:checked) {
position: absolute;
left: -9999px;
}
[type="radio"]:checked + label,
[type="radio"]:not(:checked) + label
{
position: relative;
padding-left: 22px;
cursor: pointer;
line-height: 20px;
display: inline-block;
color: #666;
margin-right: 8px;
}
[type="radio"]:checked + label:before,
[type="radio"]:not(:checked) + label:before {
content: '';
position: absolute;
left: 0;
top: 0;
width: 18px;
height: 18px;
border: 1px solid #ddd;
border-radius: 100%;
background: #fff;
}
[type="radio"]:checked + label:after,
[type="radio"]:not(:checked) + label:after {
content: '';
width: 10px;
height: 10px;
background: #0D9EDF;
position: absolute;
top: 4px;
left: 4px;
border-radius: 100%;
-webkit-transition: all 0.2s ease;
transition: all 0.2s ease;
}
[type="radio"]:not(:checked) + label:after {
opacity: 0;
-webkit-transform: scale(0);
transform: scale(0);
}
[type="radio"]:checked + label:after {
opacity: 1;
-webkit-transform: scale(1);
transform: scale(1);
}

table.vertical-radio-btn, table.vertical-radio-btn > tbody, table.vertical-radio-btn > tbody tr{
    display:flex; display:-webkit-flex; flex-wrap:wrap; height:inherit !important
}
    table.vertical-radio-btn > tbody tr td{
        flex-direction:column
    }


/* -- Radio Button -- */

        </style>

    <%--<script src="../bootstrap/js/bootstrap.min.js"></script>--%>
    <%-- <script src="../studInfo/js/admin.js"></script>--%>

    <script src="https://code.jquery.com/jquery-3.4.1.js"></script>
    <script src="../newbootstrap/js/bootstrap.min.js"></script>

    <script src="../validation/buttonValidation.js"></script>
    <script src="../validation/validation.js"></script>

  
    <script src="../studInfo/plugin/inputmask/jquery.inputmask.bundle.min.js"></script>




    <%-- <script src="../studInfo/js/bootstrap.min.js"></script>--%>

    <%-- <script src="../studInfo/js/admin-custom.js"></script>--%>






    <%--below link use to check onChange validation using js--%>



    <%-- <script src="../studInfo/js/admin-custom.js"></script>--%>

    <%--  <script src="../studInfo/js/admin.js"></script>--%>

    <div style="z-index: 1; position: absolute; top: 20%; left: 48%;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updPanel1"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel runat="server" ID="updPanel1">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">

                        <div class="box-body">
                            <legend>Sign Up - Direct Entry</legend>

                            <div class="col-md-8 col-sm-6 col-xs-12 form-group padd-t-10">
                                <label><sup>*</sup> Admission Type : </label>
                                <asp:RadioButtonList CssClass="vertical-radio-btn" ID="rblCourse" runat="server" Height="30px" RepeatDirection="Horizontal" ToolTip="Select Course" TabIndex="1">
                                    <asp:ListItem Value="1" Selected="True">Consortium (Management)</asp:ListItem>
                                    <asp:ListItem Value="2">Tamil Nadu Engineering Admissions (TNEA)/TANCET/DOTE</asp:ListItem>
                                    <asp:ListItem Value="3">Others</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                            <div class="col-md-4 col-sm-6 col-xs-12 form-group">
                                <label id="lblCatNo"><sup></sup>Consortium/ TNEA/ TANCET/ DOTE Number :</label>
                                <asp:TextBox ID="txtCatNo" runat="server" CssClass="form-control" placeholder="Enter Consortium/ TNEA Number" TabIndex="2" MaxLength="20"></asp:TextBox>

                            </div>

                            <div class="clearfix"></div>

                            <div class="col-md-4 col-sm-6 col-xs-12 form-group">
                                <label><sup>*</sup> Student Name :</label>
                                <asp:TextBox ID="txtStudName" runat="server" CssClass="form-control" placeholder="Enter Student Name" TabIndex="3" MaxLength="60"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Student Name" SetFocusOnError="True"
                                    ControlToValidate="txtStudName" ForeColor="Red" ValidationGroup="academic" Display="None">
                                </asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-4 col-sm-6 col-xs-12 form-group">
                                <label><sup>*</sup> Date of Birth :</label>
                                <div class="input-group date" id="myDatepickerz1" runat="server">
                                    <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" TabIndex="4" data-inputmask="'mask': '99/99/9999'"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter Date of Birth" SetFocusOnError="True"
                                    ControlToValidate="txtDOB" ForeColor="Red" ValidationGroup="academic" Display="None">
                                </asp:RequiredFieldValidator>
                            </div>



                            <div class="col-md-4 col-sm-6 col-xs-12 form-group">
                                <label><sup>*</sup>Student Mobile Number :</label>
                                <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" placeholder="Enter Mobile Number" TabIndex="5" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter Mobile Number" SetFocusOnError="True"
                                    ControlToValidate="txtMobileNo" ForeColor="Red" ValidationGroup="academic" Display="None">
                                </asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-4 col-sm-6 col-xs-12 form-group">
                                <label><sup>*</sup>Student Email ID :</label>
                                <asp:TextBox ID="txtEmailId" runat="server" CssClass="form-control" placeholder="Enter Email Address" TabIndex="6" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Enter Email ID" SetFocusOnError="True"
                                    ControlToValidate="txtEmailId" ForeColor="Red" ValidationGroup="academic" Display="None">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12 form-group">
                                <label><sup>*</sup>Degree :</label>
                                <asp:DropDownList runat="server" ID="ddlDegree" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true" class="form-control" TabIndex="7">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12 form-group">
                                <label><sup>*</sup>Branch :</label>
                                <asp:DropDownList runat="server" ID="ddlBranch" AppendDataBoundItems="true" class="form-control" TabIndex="8" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12 form-group">
                                <label><sup>*</sup>Application Type :</label>
                                <asp:DropDownList runat="server" ID="ddlAdmissionType" AppendDataBoundItems="true" AutoPostBack="true" class="form-control" TabIndex="9" OnSelectedIndexChanged="ddlAdmissionType_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>


                            <div class="col-md-4 col-sm-6 col-xs-12 form-group">
                                <label>Attach Allotment Order (<code style="font-style: italic; color: #0D9EDF">It Is Mandatory For TNEA/Others</code>) :</label>
                                <asp:FileUpload runat="server" ID="fuDocUpload" class="uploadlogo" accept=".jpg,.jpeg,.png,.pdf,.doc,.docx" />
                                <span style="color: red;">Only .jpg,.jpeg,.png,.pdf,.doc,.docx file allowed with max 100 Kb size</span>
                            </div>
                        </div>
                        <asp:Label ID="lblMsgToUser" Visible="true" runat="server" Style="color: green; font-weight: 700"></asp:Label>



                        <div class="box-footer">
                            <div class="col-sm-12 col-xs-12 text-center form-group ">
                                <asp:LinkButton ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="academic"
                                    OnClientClick="return Validate();" ToolTip="Click to Submit Data" TabIndex="10" CssClass="btn btn-outline-info"> Submit</asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" TabIndex="11" runat="server" Text="Cancel" ToolTip="Click To Cancel" OnClick="btnCancel_Click"
                                    CssClass="btn btn-danger"> Cancel</asp:LinkButton>
                                <asp:ValidationSummary ID="vs1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="academic"
                                    Style="text-align: center" />
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

    <%--<script src="../bootstrap/js/jquery-3.3.1.min.js"></script>
    <script src="../newbootstrap/js/bootstrap.min.js"></script>--%>

      <!--   Calendar   -->
    <script src="../studInfo/plugin/calendar/moment.min.js"></script>
    <script src="../studInfo/plugin/calendar/bootstrap-datetimepicker.min.js"></script>

    <script>



        $(document).ready(function () {

            init_InputMask();// for fileupload control change event work after postback
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(init_InputMask);

        });

        function init_InputMask() {

            if (typeof ($.fn.inputmask) === 'undefined') { return; }
            console.log('init_InputMask');

            $(":input").inputmask();

        };
    </script>
    <script>

        $(document).ready(function () {
            loadDate();// for fileupload control change event work after postback
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(loadDate);
        });

        function loadDate() {

            $('#ctl00_ContentPlaceHolder1_myDatepickerz1').datetimepicker({
                format: 'DD/MM/YYYY',
                maxDate: moment(),
                useCurrent: false


            });
        }


        $('#ctl00_ContentPlaceHolder1_myDatepickerz1').datetimepicker({
            format: 'DD/MM/YYYY',
            maxDate: moment(),
            useCurrent: false

        });


    </script>
    <script>
        $(function () {
            $('#ctl00_ContentPlaceHolder1_myDatepickerz1').datetimepicker({
                format: 'DD/MM/YYYY',
                maxDate: moment(),
                useCurrent: false
            });
        });

    </script>

    <!-- Loader Script -->
    <script type="text/javascript">
        $(document).ready(function () {
            $(".loader-area").fadeOut('slow');
        }, 2000);
    </script>

</asp:Content>

