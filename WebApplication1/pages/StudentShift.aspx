<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentShift.aspx.cs" Inherits="ACADEMIC_StudentShift" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updStudent"
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
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <%--<h3 class="box-title">Student Allocation</h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>

                <asp:UpdatePanel ID="updStudent" runat="server">
                    <ContentTemplate>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYAdmbatch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlIntake" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="true" AutoPostBack="true" TabIndex="1" OnSelectedIndexChanged="ddlIntake_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlIntake"
                                            ValidationGroup="show" Display="None" InitialValue="0" SetFocusOnError="true" ErrorMessage="Please Select Intake"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlfaculty" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="true" AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddlfaculty_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvFaculty" runat="server" ControlToValidate="ddlfaculty"
                                            ValidationGroup="show" Display="None" InitialValue="0" SetFocusOnError="true" ErrorMessage="Please Select Faculty /School Name"></asp:RequiredFieldValidator>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlfaculty"
                                            ValidationGroup="excel" Display="None" InitialValue="0" SetFocusOnError="true" ErrorMessage="Please Select Faculty /School Name"></asp:RequiredFieldValidator>
                                    
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblprogram" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:ListBox ID="lstbxProgram" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo"
                                            SelectionMode="multiple" AutoPostBack="true"></asp:ListBox>
                                        <%-- <asp:DropDownList ID="ddlProgram" runat="server" CssClass="form-control" data-select2-enable="true"
                                                    AppendDataBoundItems="true" TabIndex="3" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>--%>

                                        <asp:RequiredFieldValidator ID="rfvProgram" runat="server" ControlToValidate="lstbxProgram"
                                            ValidationGroup="show" Display="None" InitialValue="0" SetFocusOnError="true" ErrorMessage="Please Select Program"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:LinkButton ID="btnshow" runat="server" CssClass="btn btn-outline-primary"
                                            ValidationGroup="show" TabIndex="4" OnClick="btnshow_Click">Show</asp:LinkButton>
                                        <asp:LinkButton ID="btncancel" runat="server" CssClass="btn btn-outline-danger"
                                            TabIndex="5" OnClick="btncancel_Click" >Cancel</asp:LinkButton>
                                        <asp:LinkButton ID="btnExcel" runat="server" CssClass="btn btn-outline-info" OnClick="btnExcel_Click"  ValidationGroup="excel">Export Excel</asp:LinkButton>
                                        <asp:ValidationSummary ID="vsshow" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="show" DisplayMode="List" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="excel" DisplayMode="List" />
                                    </div>


                                    <div class="col-12 mt-3">
                                        <asp:Panel ID="pnlStudent" runat="server">
                                            <asp:ListView ID="lvStudList" runat="server" Visible="false">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Students List</h5>
                                                    </div>
                                                    <div class="table table-responsive">
                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%;">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>
                                                                        <asp:CheckBox ID="chkAll" runat="server" onclick="SelectAll(this)" /></th>
                                                                    <th>Sr No.</th>
                                                                    <th>Student Id</th>
                                                                    <th>Name With Initial</th>
                                                                    <th>Faculty/School Name</th>
                                                                    <th>Fail Count</th>
                                                                    <th>Studentship Expired Status</th>
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
                                                            <asp:CheckBox ID="chkAccept" runat="server" Enabled='<%# Eval("STUDENTSHIFT").ToString() == "0" ? true : false %>'
                                                                Checked='<%# Eval("STUDENTSHIFT").ToString() == "0" ? false : true %>' />
                                                        </td>
                                                        <td><%# Container.DataItemIndex+1 %></td>
                                                        <td>
                                                            <%# Eval("ENROLLNO") %>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("NAME_WITH_INITIAL") %>' ToolTip='<%# Eval("IDNO") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblcollegeid" runat="server" Text='<%# Eval("SHORT_NAME") %>' ToolTip='<%# Eval("COLLEGE_ID") %>'></asp:Label>
                                                            <asp:Label ID="lbldergreeno" runat="server" Text='<%# Eval("DEGREENO") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblbranchno" runat="server" Text='<%# Eval("BRANCHNO") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblaffilitedno" runat="server" Text='<%# Eval("AFFILIATED_NO") %>' Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <%# Eval("FAILCOUNT") %>              
                                                        </td>
                                                        <td><%# Eval("EXPIRED_STATUS") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:LinkButton ID="btnSave" runat="server" Visible="false" CssClass="btn btn-outline-info" OnClick="btnSave_Click">Save</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <script type="text/javascript" language="javascript">

        function SelectAll(headchk) {

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true) {
                        if (e.disabled == false) {
                            e.checked = true;
                        }
                    }
                    else {
                        if (e.disabled == false) {
                            e.checked = false;
                            headchk.checked = false;
                        }
                    }
                }

            }

        }


    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });

    </script>
</asp:Content>

