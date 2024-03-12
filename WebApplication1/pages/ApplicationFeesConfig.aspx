<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ApplicationFeesConfig.aspx.cs" Inherits="ApplicationFeesConfig" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        /*--======= toggle switch css added by gaurav 29072021 =======--*/
        .switch input[type=checkbox] {
	        height: 0;
	        width: 0;
	        visibility: hidden;
        }
        .switch label {
	        cursor: pointer;
	        width: 82px;
            height: 34px;
	        background: #dc3545;
	        display: block;
	        border-radius: 4px;
	        position: relative;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }
        .switch label:hover {
            background-color: #c82333;
        }
        .switch label:before {
	        content: attr(data-off);
	        position: absolute;
	        right: 0;
	        font-size: 16px;
            padding: 4px 8px;
            font-weight: 400;
            color: #fff;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;

        }
        .switch input:checked + label:before {
	        content: attr(data-on);
	        position: absolute;
	        left: 0;
	        font-size: 16px;
            padding: 4px 15px;
            font-weight: 400;
            color: #fff;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }
        .switch label:after {
	        content: '';
	        position: absolute;
	        top: 1.5px;
            left: 1.7px;
            width: 10.2px;
            height: 31.5px;
            background: #fff;
            border-radius: 2.5px;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }
        .switch input:checked + label {
	        background: #28a745;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }
        .switch input:checked + label:hover {
	        background: #218838;
        }
        .switch input:checked + label:after {
	        transform: translateX(68px);
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }
   </style>
      <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static"/>
    <asp:UpdatePanel ID="UpdFees" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYSession" runat="server" Font-Bold="true"></asp:Label>
                                            <%-- <label>Intake</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlIntake" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="1" ToolTip="Intake">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlIntake"
                                            Display="None" ErrorMessage="Please Select Intake" InitialValue="0"
                                            ValidationGroup="submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYPrgmtype" runat="server" Font-Bold="true"></asp:Label>
                                            <%-- <label>Study Level</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlStudyLevel" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true" TabIndex="1" ToolTip="Study Level">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlStudyLevel"
                                            Display="None" ErrorMessage="Please Select Study Level" InitialValue="0"
                                            ValidationGroup="submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblApplicationFees" runat="server" Font-Bold="true"></asp:Label>
                                            <%--   <label>Application Fees</label>--%>
                                        </div>
                                        <asp:TextBox ID="txtApplicationFees" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Application Fees"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtApplicationFees"
                                            Display="None" ErrorMessage="Please Enter Application Fees"
                                            ValidationGroup="submit" />
                                    </div>
                                   <div class="form-group col-lg-3 col-md-6 col-12">
                                         <div class="label-dynamic">
                                              <sup>* </sup>
                                            <asp:Label ID="lblStatus" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                         <div class="switch form-inline">
                                             <input type="checkbox" id="switch" name="switch" class="switch" checked/>
                                             <label data-on="Active" data-off="Inactive" for="switch"></label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-outline-info" TabIndex="1" OnClick="btnSave_Click" ValidationGroup="submit" ToolTip="Submit" ClientIDMode="Static">Submit</asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" TabIndex="1" OnClick="btnCancel_Click" ToolTip="btnCancel">Cancel</asp:LinkButton>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <%--<div class="col-12 mt-4">
                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Action</th>
                                            <th>Intake</th>
                                            <th>Study Level</th>
                                            <th>Application Fees</th>
                                            <th>Status</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" ToolTip="Edit Record" /></td>
                                            <td>Jan-June 2022</td>
                                            <td>Graduation</td>
                                            <td>1,00,000</td>
                                            <td><span class="badge badge-pill badge-danger">InActive</span></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" ToolTip="Edit Record" /></td>
                                            <td>Jan-June 2023</td>
                                            <td>Under Graduation</td>
                                            <td>1,00,000</td>
                                            <td><span class="badge badge-pill badge-success">Active</span></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>--%>
                            <div class="col-md-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvFees" runat="server">
                                        <LayoutTemplate>
                                            <%--<div class="sub-heading">
                                                <h5>Module List</h5>
                                            </div>--%>
                                            <table class="table table-striped table-bordered nowrap display" id="mytable" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action</th>
                                                        <th>Intake</th>
                                                        <th>Study Level</th>
                                                        <th>Application Fees</th>
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
                                                        CommandArgument='<%# Eval("CONG_NO") %>' AlternateText="Edit Record"
                                                        OnClick="btnEdit_Click" />
                                                    <asp:HiddenField ID="hdfconf_no" runat="server" Value='<%# Eval("CONG_NO") %>'/>
                                                </td>                                                                                           
                                                 <td>
                                                    <%# Eval("BATCHNAME")%>
                                                </td>
                                                 <td>
                                                    <%# Eval("UA_SECTIONNAME")%>
                                                </td>
                                                 <td>
                                                    <%# Eval("ADMISSION_FEES")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ACTIVESTATUS")%>
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
    </asp:UpdatePanel>
      <script>
          function SetStat(val) {
              $('#switch').prop('checked', val);
            
          }
          var summary = "";
          $(function () {

              $('#btnSave').click(function () {
                  localStorage.setItem("currentId", "#btnSave,Submit");
                  debugger;
                

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
                  $('#btnSave').click(function () {
                      localStorage.setItem("currentId", "#btnSave,Submit");
                     

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

