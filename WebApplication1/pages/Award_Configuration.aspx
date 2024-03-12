<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Award_Configuration.aspx.cs" EnableEventValidation="false" Inherits="Award_Configuration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

    <style>
        .chk-btn {
            border: 1px solid #ccc;
            padding: 8px;
        }

        p {
            margin-bottom: 2px;
        }

        .table-striped tbody table tbody tr:nth-of-type(odd),
        .table-striped tbody table tbody tr:nth-of-type(even) {
            background-color: transparent !important;
            border-bottom: 0px solid black !important;
        }

        .table-bordered tbody table tbody td, .table-bordered tbody table tbody th {
            border: 0px solid #dee2e6;
        }

        .list-group .list-group-item .sub-label {
            float: none;
            margin-left: 1rem;
        }
    </style>



    <div class="row">

        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Award Configuration</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-12 col-md-12 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Award Title</label>
                                </div>
                                <asp:TextBox ID="txtAwardTitle" runat="server" TabIndex="1" CssClass="form-control" TextMode="MultiLine" Rows="1" />

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtAwardTitle"
                                    Display="None" ErrorMessage="Please Add AwardTitle" ValidationGroup="Submit" />


                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>*</sup>
                                    <label>Faculty </label>
                                </div>
                                <%-- <asp:ListBox ID="lstbxFaculty" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>--%>
                                <asp:DropDownList ID="ddlFaculty" runat="server" CssClass="form-control" data-select2-enable="true"
                                    AppendDataBoundItems="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged1" AutoPostBack="true" TabIndex="2">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlFaculty"
                                    Display="None" ErrorMessage="Please Select Faculty" InitialValue="0" ValidationGroup="Submit" />

                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>*</sup>
                                    <label>Program</label>
                                </div>
                                <asp:ListBox ID="lstbxProgram" runat="server" TabIndex="3" CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="lstbxProgram"
                                    Display="None" ErrorMessage="Please Select Program" ValidationGroup="Submit" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>*</sup>
                                    <label>Awarding Institute</label>
                                </div>
                                <asp:DropDownList ID="ddlAwardingInstitute" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="4" AppendDataBoundItems="True">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlAwardingInstitute"
                                    Display="None" ErrorMessage="Please Select Awarding Institute" InitialValue="0" ValidationGroup="Submit" />

                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>WGPA (>=)</label>
                                </div>
                                <asp:TextBox ID="txtWGPA" runat="server" MaxLength="5" TabIndex="5" CssClass="form-control" />

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtWGPA"
                                    Display="None" ErrorMessage="Add WGPA" ValidationGroup="Submit" />



                            </div>

                            <div class="col-lg-9 col-md-12 col-12">
                                <div class="table table-responsive">
                                    <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                        <tbody>
                                            <tr>
                                                <td>Consider Postponement Student </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdoPoastPonement" runat="server" RepeatDirection="Horizontal" TabIndex="6">
                                                        <asp:ListItem Value="1"> Yes &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="0"> No </asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rdoPoastPonement"
                                                        Display="None" ErrorMessage="Please Select PoastPonement" ValidationGroup="Submit" />
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>Consider Students who were not graduated in their Regular Batch </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdoRegularBatch" runat="server" RepeatDirection="Horizontal" TabIndex="7">
                                                        <asp:ListItem Value="1"> Yes &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="0"> No </asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="rdoRegularBatch"
                                                        Display="None" ErrorMessage="Please Select Regular Batch" ValidationGroup="Submit" />
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>Consider IC Status in any module </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdoICStatus" runat="server" RepeatDirection="Horizontal" TabIndex="8">
                                                        <asp:ListItem Value="1"> Yes &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="0"> No </asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="rdoICStatus"
                                                        Display="None" ErrorMessage="Please Select IC Status" ValidationGroup="Submit" />
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>Consider students with Repeat attempts </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdoRepeatAttp" runat="server" RepeatDirection="Horizontal" TabIndex="9">
                                                        <asp:ListItem Value="1"> Yes &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="0"> No </asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="rdoRepeatAttp"
                                                        Display="None" ErrorMessage="Please Select Repeat Attpt" ValidationGroup="Submit" />
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>Consider highest WGPA in relevant Specialization </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdoWPGASpe" runat="server" RepeatDirection="Horizontal" TabIndex="10">
                                                        <asp:ListItem Value="1"> 1st &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="0"> 2nd </asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="rdoWPGASpe"
                                                        Display="None" ErrorMessage="Please Select WGPA in relevant Specialization" ValidationGroup="Submit" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>

                            <%--<div class="form-group col-lg-6 col-md-12 col-12">
                                <div class="chk-btn">
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                    Consider Postponement Student
                                    <br />
                                    <asp:CheckBox ID="CheckBox2" runat="server" />
                                    Consider Students who were not graduated in their Regular Batch
                                    <br />
                                    <asp:CheckBox ID="CheckBox3" runat="server" />
                                    Consider IC Status in any module
                                    <br />

                                    <asp:CheckBox ID="CheckBox4" runat="server" />
                                    Consider 
                                            <asp:TextBox ID="TextBox1" runat="server" Style="width: 60px; text-align:center;" />
                                    highest WGPA in relevant Specialization
                                    <br />

                                    <asp:CheckBox ID="CheckBox5" runat="server" />
                                    Consider students with Repeat attempts
                                    <br />
                                </div>
                            </div>--%>
                        </div>

                    </div>

                    <div class="col-12 btn-footer">
                        <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" ValidationGroup="Submit" OnClick="btnSubmit_Click">Submit</asp:LinkButton>

                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Submit"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                        <asp:HiddenField ID="hdnAwardId" runat="server" Value="0" />
                    </div>

                    <%--<div class="col-12 mt-3">
                         <asp:ListView ID="ListView1" runat="server" Visible="False">
                             <LayoutTemplate>
                        <div class="sub-heading">
                            <h5>Award Configuration List</h5>
                        </div>
                        <div class="table table-responsive">
                            <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                <thead class="bg-light-blue">
                                    <tr>
                                        <th>Edit</th>
                                        <th>Award Title</th>
                                        <th>Faculty</th>
                                        <th>Degree</th>
                                        <th>Program</th>
                                        <th>Awarding Institute</th>
                                        <th>View</th>
                                    </tr>
                                </thead>
                                <tbody>

                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="btnEdit" runat="server" CssClass="fa fa-pencil-square-o" Style="font-size: 16px;"></asp:LinkButton>
                                        </td>
                                        <td>Award Title Name</td>
                                        <td>Faculty</td>
                                        <td>Degree</td>
                                        <td>Program</td>
                                        <td>Awarding Institute Name</td>
                                        <td><i class="fa fa-eye" aria-hidden="true" data-toggle="modal" data-target="#myModal_view" style="color: #28a745; font-size: 20px;"></i></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                             </asp:ListView>
                           <div class="col-12">
                                
                            </div>
                    </div>--%>

                    <div class="col-12">
                        <asp:ListView ID="lvAward" runat="server" Visible="true">
                            <LayoutTemplate>
                                <div class="sub-heading">
                                    <h5>Award Configuration List</h5>
                                </div>
                                <div class="table table-responsive">
                                    <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Edit</th>
                                                <asp:Label ID="lblAward" runat="server" Text='<%#Eval("AwardNo")%>' Visible="false"></asp:Label>
                                                <%--<th visible ="false">AwardNo</th>--%>
                                                <th>Award Title</th>
                                                <th>Faculty</th>
                                                <%--<th>Degree</th>--%>
                                                <th>Program</th>
                                                <th>Awarding Institute</th>
                                                <th>View</th>
                                            </tr>
                                        </thead>
                                        <tbody>

                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <%-- <td>
                                            <asp:LinkButton ID="btnEdit" runat="server" CssClass="fa fa-pencil-square-o" Style="font-size: 16px;" CommandArgument='<%#Eval("AwardNo")%>' OnClick="btnEdit_Click"></asp:LinkButton>
                                        </td>--%>
                                    <td>
                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                            CommandArgument='<%# Eval("AwardNo") %>' ToolTip='<%# Eval("AwardNo") %>' AlternateText="Edit Record"
                                            OnClick="btnEdit_Click" />

                                    </td>


                                    <%--  <td > <%# Eval("AwardNo")%>  </td>--%>
                                    <td><%# Eval("AwardTitle")%></td>
                                    <td><%# Eval("COLLEGE_NAME")%></td>
                                    <%--<td>      <%# Eval("CODE")%></td>--%>
                                    <td><%# Eval("longname")%></td>
                                    <td><%# Eval("AFFILIATED_LONGNAME")%></td>

                                    <td>
                                        <asp:LinkButton ID="LinkButton1" runat="server" class="fa fa-eye" Style="color: #28a745; font-size: 20px;" aria-hidden="true"
                                            CommandArgument='<%#Eval("AwardNo")%>' OnClick="LinkButton1_Click"></asp:LinkButton>

                                    </td>
                                    <%--data-toggle="modal"  data-target="#myModal_view"--%>
                                    <%--<td><i class="fa fa-eye" aria-hidden="true" data-toggle="modal"  data-target="#myModal_view" onclick="POPUP_Click"></i></td>--%>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>

                </div>
            </div>
        </div>
    </div>


    <!-- The Modal -->
    <div class="modal fade" id="myModal_view">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">Award Configuration Details</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <!-- Modal body -->
                <div class="modal-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-12">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item"><b>Award Title :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblAwardTitle" runat="server" Text="" Font-Bold="true" align="left"></asp:Label>
                                        </a>
                                    </li>
                                    <%-- <li class="list-group-item"><b>Degree :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblDegree" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                    </li>--%>

                                    <li class="list-group-item"><b>Faculty :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblFaculty" runat="server" Text="" Font-Bold="true" align="left"></asp:Label></a>
                                    </li>
                                    <li class="list-group-item"><b>Program :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblProgram" runat="server" Text="" ToolTip="1" Font-Bold="true" align="left"></asp:Label></a>
                                    </li>

                                    <li class="list-group-item"><b>Awarding Institute :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblAwardingInstitute" runat="server" Text="" Font-Bold="true" align="left"></asp:Label></a>
                                    </li>
                                </ul>
                            </div>

                            <div class="form-group col-lg-12 col-md-12 col-12 mt-2 table table-responsive">
                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                    <tbody>
                                        <tr>
                                            <td>Consider Postponement Student </td>
                                            <td>
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1" Enabled="true"> Yes &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                    <asp:ListItem Value="0" Enabled="true"> No </asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Consider Students who were not graduated in their Regular Batch </td>
                                            <td>
                                                <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1" Enabled="true"> Yes &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                    <asp:ListItem Value="0" Enabled="true"> No </asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Consider IC Status in any module </td>
                                            <td>
                                                <asp:RadioButtonList ID="RadioButtonList3" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1" Enabled="true"> Yes &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                    <asp:ListItem Value="0" Enabled="true"> No </asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Consider students with Repeat attempts </td>
                                            <td>
                                                <asp:RadioButtonList ID="RadioButtonList4" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1" Enabled="true"> Yes &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                    <asp:ListItem Value="0" Enabled="true"> No </asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Consider highest WGPA in relevant Specialization </td>
                                            <td>
                                                <asp:RadioButtonList ID="RadioButtonList5" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1" Enabled="true"> 1st &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                    <asp:ListItem Value="0" Enabled="true"> 2nd </asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Modal footer -->
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>

            </div>
        </div>
    </div>

    <!-- MultiSelect Script -->
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
            ShowPUP();
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
    </script>

    <script type="text/javascript">
        function ShowPUP() {
            debugger
            //var myval = document.getElementById(ctl00_ContentPlaceHolder1_hdnAwardId).value; //document.getElementById('ctl00_ContentPlaceHolder1_hdnAwardId').va;
            // var myval = $("#hdnAwardId").val();

            var myval = $("#ctl00_ContentPlaceHolder1_hdnAwardId").val();
            if (myval != 0) {

                $('#myModal_view').modal('show');
                $('#myModal_view').fadeIn();
            }
            else {
                $('#myModal_view').modal('hide');
            }

        }
    </script>
</asp:Content>

