<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubjectIntakeDefination.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="ACADEMIC_SubjectIntakeDefination" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title"><span>SUBJECT INTAKE DEFINITION</span></h3>
                </div>
                <div class="box-body">
                    <div class="col-12">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College/School Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"
                                            runat="server" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Select College/School Name" ValidationGroup="Acd"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"
                                            runat="server" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSession"
                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Select Session" ValidationGroup="Acd"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" AutoPostBack="True" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                                            runat="server" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Select Degree" ValidationGroup="Acd"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                            runat="server" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Select Branch" ValidationGroup="Acd"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Curriculum</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged"
                                            runat="server" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Select Curriculum" ValidationGroup="Acd"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester/Year</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"
                                            runat="server" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Select Semester" ValidationGroup="Acd"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Subject</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubject" AutoPostBack="true" AppendDataBoundItems="true"
                                            runat="server" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSubject"
                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Select Subject" ValidationGroup="Acd"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <asp:HiddenField ID="hidTAB" runat="server" Value="1a" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnShow" class="btn btn-outline-info" ValidationGroup="Acd" Text="Show" runat="server" OnClick="btnShow_Click" />
                        <asp:Button ID="btnSubmit" class="btn btn-outline-info" ValidationGroup="Acd" Text="Submit" runat="server" 
                                OnClientClick="return btnLock_validation();" OnClick="btnSubmit_Click" Visible="false"/>
                        <asp:Button ID="btnLock" class="btn btn-outline-info" ValidationGroup="Acd" Text="Lock" OnClientClick="return btnLock_validation();" runat="server"
                            OnClick="btnLock_Click" Visible="false"/>

                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btnClear_Click" />
                   
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Acd" />
                    </div>
                     
                    <div class="col-12" id="listview_div">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                            <ContentTemplate>
                                <asp:ListView ID="ListView1" runat="server" ItemPlaceholderID="itemPlaceHolder">
                                    <LayoutTemplate>
                                        <div class="vista-grid">
                                            <table id="id1" class="table table-striped table-bordered nowrap display" style="width:100%">
                                                <thead class="lstview_head bg-light-blue">
                                                    <tr>
                                                        <th>SR.NO</th>
                                                        <th>FACULTY NAME</th>
                                                        <th style="text-align:center;">SECTION</th>
                                                        <th>SUBJECT</th>
                                                        <th style="width:150px">SUBJECT INTAKE</th>
                                                    </tr>
                                                </thead>
                                                <tbody class="lstview_body">
                                                    <tr id="itemPlaceholder" runat="server"></tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item">
                                            <td><%# Container.DataItemIndex + 1 %></td>
                                            <td><%# Eval("UA_FULLNAME") %></td>
                                            <td style="text-align:center;"><%# Eval("SECTIONNAME") %></td>
                                            <td><%# Eval("COURSE_NAME") %></td>
                                            <td style="width:150px">
                                                <asp:TextBox runat="server" ID="txtLimit" Enabled='<%# Eval("LOCK_INTAKE").ToString()=="0"?true:false %>' Text='<%# Eval("INTAKE") %>' CssClass="form-control"></asp:TextBox></td>
                                                <asp:HiddenField ID="htnCTNO" runat="server"  Value='<%# Eval("CT_NO") %>'/>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnLock" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <div class="modal fade" id="myModal1" role="dialog">
        <div class="modal-dialog" style="width: 30%">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                </div>
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <div class="box-body modal-warning">
                            <div class="form-group" style="text-align: center">
                                <asp:Label ID="lblmessageShow" Style="font-weight: bold" runat="server" Text="Regno"></asp:Label>
                            </div>
                            <div class="box-footer">
                                <p class="text-center">
                                    <asp:Button ID="Button1" runat="server" Text="Close" CssClass="btn btn-default"
                                        data-dismiss="modal" />
                                </p>
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function showModal() {
            $("#myModal1").modal('show');
        }
    </script>
    <script>
        $(document).ready(function () {
            $('td').click(function () {
                var row = $(this).parent().parent().children().index($(this).parent());
                $('#ctl00_ContentPlaceHolder1_ListView1_ctrl' + row + '_txtLimit').keypress(function (event) {
                    if ($('#ctl00_ContentPlaceHolder1_ListView1_ctrl' + row + '_txtLimit').val().length > 2) {
                        event.preventDefault();
                    }
                    if (event.which != 8 && isNaN(String.fromCharCode(event.which))) {
                        event.preventDefault();
                    }
                });
            });
        });
    </script>
    <script>
        function btnLock_validation() {
            var rowCount = $("#id1 td").closest("tr").length;
            while (rowCount--) {
                if ($.trim($('#ctl00_ContentPlaceHolder1_ListView1_ctrl' + rowCount + '_txtLimit').val()).length == 0) {
                    alert('Please enter intake of all faculty');
                    return false;
                }
            }
        }
    </script>
    
</asp:Content>
