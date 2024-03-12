<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Module_Revision.aspx.cs" Inherits="Projects_Module_Revision" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
      <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updInvigilation"
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
    <asp:UpdatePanel ID="updInvigilation" runat="server">
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
                                    <div class="form-group col-lg-5 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                             <asp:Label ID="lblDYDept" runat="server" Font-Bold="true"></asp:Label>
                                          <%--  <label>Department</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlDepartment"
                                            Display="None" ErrorMessage="Please Select Department" InitialValue="0" ValidationGroup="Submit" />
                                    </div>
                                    <div class="form-group col-lg-5 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                             <asp:Label ID="lblICCourse" runat="server" Font-Bold="true"></asp:Label>
                                          <%--  <label>Module</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlModule" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlModule"
                                            Display="None" ErrorMessage="Please Select Module" InitialValue="0" ValidationGroup="Submit" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12" runat="server" visible="false" id="MyDiv">
                                <div class="row">
                                    <div class="col-lg-5 col-md-6 col-12">
                                        <div class="sub-heading">
                                            <h5>Existing Module Details</h5>
                                        </div>
                                        <ul class="list-group list-group-unbordered mb-3">
                                            <li class="list-group-item"><b>Module Code :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblModuleCode" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Module Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblModuleName" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Credits :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblCredits" runat="server" Text="" ToolTip="1" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Type :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblType" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                     <asp:Label ID="lblSubID" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Revision Details</h5>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                     <asp:Label ID="lblDYModuleCode" runat="server" Font-Bold="true"></asp:Label>
                                                   <%-- <label>Module Code</label>--%>
                                                </div>
                                                <asp:TextBox ID="txtModuleCode" runat="server" Visible="true" CssClass="form-control" TabIndex="3" ValidationGroup="Submit" Enabled="true"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtModuleCode"
                                            Display="None" ErrorMessage="Please Insert Module Code" ValidationGroup="Submit" />
                                            </div>
                                            <div class="form-group col-lg-8 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                     <asp:Label ID="lblDYModuleName" runat="server" Font-Bold="true"></asp:Label>
                                                    <%--<label>Module Name</label>--%>
                                                </div>
                                                <asp:TextBox ID="txtModuleName" runat="server" Visible="true" CssClass="form-control" TabIndex="4" ValidationGroup="Submit"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtModuleName"
                                            Display="None" ErrorMessage="Please Insert Module Name" ValidationGroup="Submit" />
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                     <asp:Label ID="lblDYCredits" runat="server" Font-Bold="true"></asp:Label>
                                                   <%-- <label>Credits</label>--%>
                                                </div>
                                                <asp:TextBox ID="txtCredits" runat="server" Visible="true" CssClass="form-control" TabIndex="5" ValidationGroup="Submit"  onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCredits"
                                            Display="None" ErrorMessage="Please Insert Credits" ValidationGroup="Submit" />
                                              <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                                    ControlToValidate="txtCredits" ErrorMessage="Enter only 0-9 numbers."
                                                    ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>--%>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12" >
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>From Year</label>
                                                </div>
                                                <asp:DropDownList ID="ddlFromYear" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlFromYear"
                                            Display="None" ErrorMessage="Please Select From Year" InitialValue="0" ValidationGroup="Submit" />
                                            </div>
                                             <div class="form-group col-lg-4 col-md-6 col-12" >
                                                <div class="label-dynamic">
                                                    <label>To Year</label>
                                                </div>
                                                <asp:DropDownList ID="ddlToYear" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnShow" runat="server" CssClass="btn btn-outline-info"  ValidationGroup="Submit"  TabIndex="6" Visible="false">Show</asp:LinkButton>
                                <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" TabIndex="7" OnClick="btnSubmit_Click"  ValidationGroup="Submit" Visible="true">Submit</asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" TabIndex="8" OnClick="btnCancel_Click" Visible="true">Cancel</asp:LinkButton>
                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="Submit"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                 <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Submits"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                            </div>


                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvCourseRevision" runat="server" Visible="true">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Module Revision List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" id="mytable" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th >Edit </th>
                                                        <th>Department</th>
                                                        <th>Existing Module Code</th>
                                                        <th>Existing Module Name</th>
                                                        <th>Revision Module Code</th>
                                                        <th>Revision Module Name</th>
                                                        <th>From Year</th>
                                                        <th>To Year</th>
                                                        <th>Revision Date</th>
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
                                                      AlternateText="Edit Record" OnClick="btnEdit_Click" CommandArgument='<%# Eval("REVNO") %>' />
                                                </td>
                                                 <td><%# Eval("DEPTNAME") %></td>
                                                 <td><%# Eval("OLD_CCODE") %></td>
                                                 <td><%# Eval("OLD_COURSENAME") %></td>
                                                 <td><%# Eval("NEW_CCODE") %></td>
                                                 <td><%# Eval("NEW_COURSENAME") %></td>
                                                 <td><%# Eval("FROM_YEAR") %></td>
                                                 <td><%# Eval("TO_YEAR") %></td>
                                                 <td><%# Eval("DATE") %></td>
                                                 <td>
                                                     <asp:Label ID="status" runat="server" class="badge" Text='<%#Eval("REVISION_STATUS") %>' ></asp:Label>
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

        function onlyDotsAndNumbers(txt, event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode == 46) {
                if (txt.value.indexOf(".") < 0)
                    return true;
                else
                    return false;
            }

            if (txt.value.indexOf(".") > 0) {
                var txtlen = txt.value.length;
                var dotpos = txt.value.indexOf(".");
                //Change the number here to allow more decimal points than 2
                if ((txtlen - dotpos) > 2)
                    return false;
            }

            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
     </script>

    <script>
        function CheckMark(id) {
           
            var ValidChars = "0123456789.-";
         
            var num = true;
            var mChar;
            mChar = id.value.charAt(0);
            if (ValidChars.indexOf(mChar) == -1) {
                num = false;
                id.value = '';
                alert("Error! Only Numeric Values Are Allowed")
                id.select();
                id.focus();
                
            }
    </script>
</asp:Content>
            

