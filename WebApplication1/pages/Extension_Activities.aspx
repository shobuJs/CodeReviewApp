<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Extension_Activities.aspx.cs"
    Inherits="ACADEMIC_Extension_Activities" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../Content/jquery.js" type="text/javascript"></script>
    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>
    <script type="text/javascript" charset="utf-8">
        $(document).ready(function () {

            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            //$('.btn_close').click(function () {
            //    $('#toggle_div').slideUp();
            //});

            $(".btn_close").click(function () {
                $('#toggle_div').hide();
            });
        });
    </script>

    <style>
        .not-allowed {cursor: not-allowed !important;opacity:.4;}
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.div-collapse').hide();
            $('tr.first-div').find('.img-collapse').click(function () {
                $(this).parents('.first-div').next('tr').slideToggle();
                //$(this).parents('.first-div').next('tr').toggleClass('active');
            });

            $('tr.first-div').find('.img-collapse').each(function () {
                // alert('hai');
                var $uv = $(this).parents('.first-div').next('tr').find('td');
                if ($uv.contents().length == 1) {
                    //$(this).parents('tr.first-div').find('.img-collapse').addClass('not-allowed');
                    //$uv.hide();
                } else {
                    //$(this).parents('tr.first-div').find('.img-collapse').removeClass('not-allowed');
                    //$uv.show();
                }
            });

        });
    </script>

    <div class="row" id="divExtension" runat="server" visible="true">
        <div class="col-md-12 form-group">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">EXTENSION ACTIVITIES</h3>
                    <div class="box-tools pull-right">
                        <div style="color: Red; font-weight: bold">Note : * Marked fields are mandatory</div>
                    </div>
                </div>

                <div class="box-body">

                    <div class="col-md-12 from-group" runat="server">

                        <div class="form-group col-md-4" id="trNameActivity" runat="server">
                            <label><span style="color: red;">*</span> Name of the Activity</label>
                            <asp:TextBox ID="txtNameofActivity" runat="server" TabIndex="1" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvProject" runat="server" ControlToValidate="txtNameofActivity"
                                Display="None" ErrorMessage="Please Enter Name of the Activity" SetFocusOnError="true" ValidationGroup="AddActivity">
                            </asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="AddActivity" />
                        </div>

                        <div class="form-group col-md-4" id="trAwardRecognition" runat="server">
                            <label><%--<span style="color: red;">*</span>--%> Name of the Award/Recognition</label>
                            <asp:TextBox ID="txtNameofAward" runat="server" TabIndex="2" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-4" id="trNameofGovtAward" runat="server">
                            <label><%--<span style="color: red;">*</span>--%> Name of Awarding Govt/Govt.Recognized bodies</label>
                            <asp:TextBox ID="txtNameofGovtAward" runat="server" TabIndex="3" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-4" id="div7" runat="server">
                            <label>
                                <%--<span style="color: red;">*</span>--%> Year of Award                                 
                            </label>

                            <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar text-blue"></i>
                                </div>
                                <asp:TextBox ID="txtYearofAwardDate" runat="server" TabIndex="4" CssClass="form-control" />

                                <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtYearofAwardDate" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true" />
                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtYearofAwardDate"
                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                    AcceptNegative="none" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtYearofAwardDate"
                                    ErrorMessage="Please Select Year of Award Date" ValidationGroup="Show" Display="None"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group col-md-4" id="trOrganizingUnit" runat="server">
                            <label><%--<span style="color: red;">*</span>--%> Organizing Unit/Agency/Collaborating agency</label>
                            <asp:TextBox ID="txtOrganizingUnit" runat="server" TabIndex="5" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="form-group col-md-4" id="trNameScheme" runat="server">
                            <label><%--<span style="color: red;">*</span>--%> Name of the Curriculum</label>
                            <asp:TextBox ID="txtSchemeName" runat="server" TabIndex="6" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-4" id="trGrant" runat="server">
                            <label><%--<span style="color: red;">*</span>--%> Student Participated</label>
                            <asp:TextBox ID="txtStudPar" runat="server" MaxLength="10" TabIndex="7" CssClass="form-control"></asp:TextBox>
                            <ajaxToolKit:FilteredTextBoxExtender ID="fteStudPar" runat="server" TargetControlID="txtStudPar" FilterType="Numbers"></ajaxToolKit:FilteredTextBoxExtender>
                        </div>

                        <div class="col-md-8 col-sm-8 col-xs-12 form-group">
                            <%--<div class="col-md-12">--%>
                            <label for="city">
                                Supportive Documents Attachment <span style="color: red; font-size: 10px;">1) Only .jpg, .jpeg, .png , .pdf format with max 50 KB size are allowed                    
                                                        2) Please Select Multiple Certificates by pressing Ctrl Key
                                                        <br />
                                    3) Please Ensure all Certificates Available in one Folder Only </span>
                            </label>
                            <%-- </div>--%>
                            <asp:FileUpload ID="fuExtensionCertificate" runat="server" ToolTip="Select file to upload"
                                allowmultiple="true" CssClass="form-control" TabIndex="8" accept=".jpg,.jpeg,.pdf,.JPG,.JPEG,.PNG,.pdf,.PDF" />

                            <br />


                            <asp:Panel ID="Panel3" runat="server" ScrollBars="Auto">
                                <asp:Repeater ID="lvProjectCertificate" runat="server">
                                    <HeaderTemplate>
                                        <table class="table table-bordered table-hover table-fixed">
                                            <thead>
                                                <tr class="bg-light blue">
                                                    <th>Action</th>
                                                    <th>Project Name
                                                    </th>
                                                    <th>Certificate Name
                                                    </th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <%--<asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif"  AlternateText='<%# Eval("SPORTS_FILE_NAME") %>'
                                                                          CommandArgument='<%#Eval("SPORTS_DOCS_NO") %>' ToolTip='<%# Eval("SPORTS_NAME") %>' 
                                                                           OnClick="btnDelete_Click"  OnClientClick="return UserDeleteConfirmation();" />--%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblName" runat="server" Text='<%#Eval("SPORTS_NAME") %>' ToolTip='<%#Eval("IDNO") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <%#Eval("SPORTS_FILE_NAME") %>
                                            </td>
                                            <td>
                                                <%--<asp:LinkButton ID="btnDownload" runat="server" Text="Download" CommandArgument='<%#Eval("SPORTS_FILE_NAME") %>'
                                                                            OnClick="btnDownload_Click"></asp:LinkButton>--%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody> </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </asp:Panel>

                        </div>
                        <div class="box-footer col-md-12 ">
                            <p class="text-center">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" Text="Submit" TabIndex="9" ValidationGroup="AddActivity" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-outline-danger" TabIndex="10" OnClick="btnCancel_Click" />

                            </p>

                        </div>
                    </div>

                    <div id="divStatus" runat="server" style="transition: 0.8s;" class="col-md-12">
                        <asp:ListView ID="lvExtension" runat="server" OnItemDataBound="lvExtension_ItemDataBound">
                            <LayoutTemplate>
                                <div>
                                    <table class="table table-hover table-bordered table-striped">
                                        <thead>

                                            <tr class="bg-light-blue">
                                                <th>Delete</th>
                                                <th>Edit</th>
                                                <th>Activity Name
                                                </th>
                                                <th>Award Name
                                                </th>
                                                <th>Award Recognized By
                                                </th>
                                                <th>Year of Award
                                                </th>
                                                <th>Organizing Unit
                                                </th>
                                                <th>Scheme Name
                                                </th>
                                                <th>Student's Participated
                                                </th>
                                                <th>Upload Certificates
                                                </th>

                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </div>
                            </LayoutTemplate>
                            <EmptyDataTemplate>
                                <div align="left" class="info">
                                    There are no records to display
                                </div>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr class="first-div">
                                    <%--<tr>--%>
                                    <td>
                                        <center>
                                            <asp:ImageButton ID="btnExtensionDelete" runat="server" AlternateText="Delete" CommandArgument='<%# Eval("ACTIVITYNO")%>' 
                                             OnClick="btnExtensionDelete_Click" ImageUrl="~/images/delete.gif" ToolTip="Delete" />
                                        </center>
                                    </td>
                                    <td>
                                        <center>
                                            <asp:ImageButton ID="btnEditforExtension" runat="server" AlternateText="Edit" CommandArgument='<%# Eval("ACTIVITYNO")%>' 
                                                ImageUrl="~/images/edit.gif" OnClick="btnEditforExtension_Click" ToolTip="Edit" />
                                        </center>
                                    </td>

                                    <td>
                                        <asp:Label ID="lblActivityName" runat="server" Text='<%# Eval("ACTIVITY_NAME") %>'
                                            ToolTip='<%# Eval("ACTIVITYNO") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <%# Eval("AWARD_NAME") %>
                                    </td>
                                    <td>
                                        <%# Eval("AWARD_GOVT_NAME") %>
                                    </td>
                                    <td>
                                        <%# Eval("AWARD_DATE") %>
                                    </td>

                                    <td>
                                        <%# Eval("ORGANIZING_NAME") %>
                                    </td>
                                    <td>
                                        <%# Eval("SCHEME_NAME") %>
                                    </td>
                                    <td>
                                        <%# Eval("STUDENT_PARTICIPATED") %>
                                    </td>                                    
                                    <td style="text-align: center">
                                        <img alt="" class="img-collapse" style="cursor: pointer" src="/images/plus.gif" />
                                    </td>

                                    <tr class="div-collapse">
                                        <td colspan="5">

                                            <asp:ListView ID="lvActivityDetails" runat="server" Style="display: none;">
                                                <LayoutTemplate>
                                                    <div id="demo-grid">
                                                        <table class="table table-hover table-bordered">
                                                            <thead>
                                                                <tr class="bg-success">
                                                                    <th style="text-align:center">Action
                                                                    </th>
                                                                    <th style="text-align: center">
                                                                        <center>Sr No.</center>
                                                                    </th>
                                                                    <th style="text-align: center">File Name
                                                                    </th>
                                                                    <th style="text-align: center">Download
                                                                    </th>
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
                                                        <td style="text-align:center">
                                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText='<%# Eval("FILE_NAME")%>'
                                                                CommandArgument='<%#Eval("ACTIVITY_DOCS_NO") %>' ToolTip='<%#Eval("ACTIVITY_NAME") %>' CommandName='<%# Eval("ACTIVITYNO") %>'
                                                                OnClick="btnDelete_Click" OnClientClick="if ( ! UserDeleteConfirmation()) return false;" />
                                                        </td>
                                                        <td style="text-align: center">
                                                            <%# Container.DataItemIndex + 1%>
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("FILE_NAME")%>'></asp:Label>

                                                        </td>

                                                        <td style="text-align: center">
                                                            <asp:LinkButton ID="btnDownloadFile" runat="server" Text="Download"
                                                                CommandArgument='<%#Eval("ACTIVITY_NAME") %>' AlternateText='<%# Eval("ACTIVITY_DOCS_NO")%>'
                                                                ToolTip='<%# Eval("FILE_NAME")%>' OnClick="btnDownloadFile_Click"></asp:LinkButton>

                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </td>
                                    </tr>
                                    <%--</tr>--%>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>


                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>
    <%--<script type="text/javascript">
        function UserDeleteConfirmation() {
            return confirm("Are you sure you want to delete this user?");
            if (confirm("Are you sure you want to Delete this File?"))
                return true;
            else
                return false;
        }
    </script>--%>
    <script type="text/javascript">
        function UserDeleteConfirmation() {
            return confirm("Are you sure you want to Delete this File?");            
        }
    </script>
</asp:Content>

