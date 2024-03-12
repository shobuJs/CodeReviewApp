<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Bank_Details.aspx.cs" Inherits="ACADEMIC_Bank_Details" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
    .badge {
  display: inline-block;
  padding: .25em .4em;
  font-size: 120%;
  font-weight: 700;
  line-height: 1;
  text-align: center;
  white-space: nowrap;
  vertical-align: baseline;
  border-radius: .25rem;
  transition: color .15s ease-in-out,background-color .15s ease-in-out,border-color .15s ease-in-out,box-shadow .15s ease-in-out;
}
</style>
    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

    <div>

        <%-- <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updsetting"
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
        </asp:UpdateProgress>--%>
    </div>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server">Bank Mapping</asp:Label>
                    </h3>
                </div>

                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" onclick="document.getElemtbyId('sel-tab').value=1;" href="#tab_1">Bank</a>
                                <%-- <a class="nav-link active" data-toggle="tab" onclick="document.getElemtbyId('sel-tab').value=1;" href="#tab_1">Bank</a>--%>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" onclick="document.getElemtbyId('sel-tab').value=2;" href="#tab_2">Bank Mapping</a>
                                <%-- <a class="nav-link" data-toggle="tab" onclick="document.getElemtbyId('sel-tab').value=2;" href="#tab_2">Bank Mapping</a>--%>
                            </li>
                        </ul>

                        <div class="tab-content">

                            <div class="tab-pane active mt-3" id="tab_1">
                                <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updsetting"
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
                                <div class="box-body">
                                    <asp:UpdatePanel ID="updsetting" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-12">
                                                    <%-- <div class="box box-primary">--%>
                                                    <%--  <div class="box-header with-border">
                                                            <h3 class="box-title"><span>Bank</span></h3>
                                                        </div>--%>

                                                    <div class="box-body">
                                                        <div class="col-12">
                                                            <div class="row">

                                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Bank Name</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtbankname" runat="server" CssClass="form-control" type="text" ClientIDMode="Static" TabIndex="1" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Bank Code</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtbankCode" runat="server" CssClass="form-control" type="text" ClientIDMode="Static" TabIndex="2" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Bank Address</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtBankAddress" runat="server" CssClass="form-control" type="text" ClientIDMode="Static" TabIndex="3" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Account</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtBankAccount" runat="server" CssClass="form-control" type="text" ClientIDMode="Static" TabIndex="4" onblur="return IsNumeric(this);"/>
                                                                </div>

                                                                <div id="Div1" class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="lblStatus" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <div class="switch form-inline">
                                                                        <input type="checkbox" id="switch" name="switch" class="switch" checked />
                                                                        <label data-on="Active" data-off="Inactive" for="switch"></label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-12 btn-footer">

                                                            <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" ClientIDMode="Static" TabIndex="5" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                                            <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" TabIndex="6" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                                            <asp:HiddenField ID="HiddenField2" runat="server" />
                                                        </div>




                                                        <div class="col-12">
                                                            <asp:Panel ID="Panel2" runat="server">
                                                                <asp:ListView ID="lvBankDetails" runat="server" Visible="true">
                                                                    <LayoutTemplate>
                                                                        <div class="sub-heading">
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>Action</th>
                                                                                    <th>Bank Name</th>
                                                                                    <th>Bank Code</th>
                                                                                    <th>Bank Address</th>
                                                                                    <th>Bank Account</th>
                                                                                    <th>Status</th>

                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>
                                                                    </LayoutTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                                                                    AlternateText="Edit Record" OnClick="btnEdit_Click" CommandArgument='<%# Eval("BANKNO") %>' />
                                                                            </td>
                                                                            <td><%# Eval("BANKNAME")%></td>
                                                                            <td><%# Eval("BANKCODE") %></td>
                                                                            <td><%# Eval("BANKADDR") %></td>
                                                                            <td><%# Eval("ACCOUNT_NO") %></td>
                                                                            <td><asp:Label runat="server" ID="lblStatus" Text='<%# Eval("ACTIVE")%>'></asp:Label></td>

                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>

                                                            </asp:Panel>

                                                        </div>

                                                    </div>
                                                    <%--  </div>--%>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <%--   <Triggers>
                                            <asp:PostBackTrigger ControlID="btnSubmit" />
                                            <asp:PostBackTrigger ControlID="btnCancel" />
                                        </Triggers>--%>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                            <div class="tab-pane mt-3" id="tab_2">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
                                <div class="box-body">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12 col-12">
                                                    <%-- <div class="box-header with-border">
                                                            <h3 class="box-title"><span>Bank</span></h3>
                                                        </div>--%>

                                                    <div class="box-body">
                                                        <div class="col-12">
                                                            <div class="row">
                                                                <div id="Div3" class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <%--<asp:Label ID="Label2" runat="server" Font-Bold="true"></asp:Label>--%>
                                                                        <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlcollegename" runat="server" CssClass="form-control" data-select2-enable="true"
                                                                        AppendDataBoundItems="true" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlcollegename_SelectedIndexChanged">
                                                                        <asp:ListItem>Please Select </asp:ListItem>
                                                                    </asp:DropDownList>

                                                                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" ControlToValidate="ddlcollegename"
                                                                        Display="None" ErrorMessage="Faculty /School Name"
                                                                        SetFocusOnError="True" ValidationGroup="banksubmit">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <%--  <label>Bank Name</label>--%>
                                                                        <asp:Label ID="lblbank" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:ListBox ID="lstbank" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo"
                                                                        SelectionMode="multiple" OnSelectedIndexChanged="ddlbank_SelectedIndexChanged"></asp:ListBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="lstbank"
                                                                        Display="None" ErrorMessage="Please Bank Name" InitialValue="0" ValidationGroup="banksubmit" />
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-12 btn-footer">

                                                            <asp:LinkButton ID="LnkSubmit" runat="server" CssClass="btn btn-outline-info" ClientIDMode="Static" TabIndex="5" ValidationGroup="banksubmit" OnClick="LnkSubmit_Click">Submit</asp:LinkButton>
                                                            <asp:LinkButton ID="LnkButtonCancel" runat="server" AutoPostBack="True" CssClass="btn btn-outline-danger" TabIndex="6" OnClick="LnkButtonCancel_Click">Cancel</asp:LinkButton>
                                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                                        </div>
                                                        <div class="col-12">
                                                            <asp:Panel ID="Panel1" runat="server">
                                                                <asp:ListView ID="ListView1" runat="server" Visible="true">
                                                                    <LayoutTemplate>
                                                                        <div class="sub-heading">
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <%-- <th>Action</th>--%>
                                                                                    <th>Faculty /School</th>
                                                                                    <th>Bank Name</th>
                                                                                    <th>Bank Code</th>
                                                                                    <th>Bank Address</th>
                                                                                    <th>Bank Account No</th>
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
                                                                                <asp:ImageButton ID="btnEditbank" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                                                                    AlternateText="Edit Record" OnClick="btnEditbank_Click" CommandArgument='<%# Eval("COLLEGE_ID") %>'
                                                                                    ToolTip='<%# Eval("BANK_NO") %>' />
                                                                            </td>--%>
                                                                            <td>
                                                                                <%# Eval("COLLEGE_NAME") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("BANKNAME") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("BANKCODE") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("BANKADDR") %>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("ACCOUNT_NO") %>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <%-- <asp:PostBackTrigger ControlID="LnkSubmit" />--%>
                                            <%-- <asp:PostBackTrigger ControlID="LnkButtonCancel" />--%>
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>

        var summary = "";
        $(function () {
            $('#btnSubmit').click(function () {
                localStorage.setItem("currentId", "#btnSubmit,Submit");
                debugger;
                ShowLoader('#btnSubmit');
                if ($('#txtbankname').val() == "")
                    summary += '<br>Please Enter Bank Name';
                if ($('#txtbankCode').val() == "")
                    summary += '<br>Please Enter Bank Code';
                if ($('#txtBankAddress').val() == "")
                    summary += '<br>Please Enter Bank Address';
                if ($('#txtBankAccount').val() == "")
                    summary += '<br>Please Enter Bank Account';
                if (summary != "") {
                    customAlert(summary);
                    summary = "";
                    return false
                }
            });
        });

    </script>
    <script type="text/javascript">
        function IsNumeric(txt) {
            var ValidChars = "0123456789-";
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

    <script type="text/javascript">
        function UserDeleteConfirmation() {
            if (confirm("Are you sure you want to delete this Entry?"))
                return true;
            else
                return false;
        }
    </script>
      <script>
          function SetStat(val) {
              $('#switch').prop('checked', val);
          }

          var summary = "";
          $(function () {

              $('#btnSubmit').click(function () {
                  localStorage.setItem("currentId", "#btnSubmit,Submit");
                  debugger;
                  ShowLoader('#btnSave');
                  if (summary != "") {
                      customAlert(summary);
                      summary = "";
                      return false
                  }
                  $('#hfdStat').val($('#switch').prop('checked'));
              });
          });

          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function () {
              $(function () {
                  $('#btnSubmit').click(function () {
                      localStorage.setItem("currentId", "#btnSubmit,Submit");
                      ShowLoader('#btnSave');
                      if (summary != "") {
                          customAlert(summary);
                          summary = "";
                          return false
                      }
                      $('#hfdStat').val($('#switch').prop('checked'));
                  });
              });
          });
    </script>

</asp:Content>


