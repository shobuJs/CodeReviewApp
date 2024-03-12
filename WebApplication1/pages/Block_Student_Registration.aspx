<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Block_Student_Registration.aspx.cs" Inherits="Block_Student_Registration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>

        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updAppyCon"
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
    <asp:UpdatePanel ID="updAppyCon" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Block Student Registration </h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Faculty </label>
                                        </div>
                                        <asp:DropDownList ID="ddlFaculty" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlFaculty"
                                            Display="None" ErrorMessage="Please Select Faculty" InitialValue="0" ValidationGroup="Show" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Program </label>
                                        </div>
                                        <asp:DropDownList ID="ddlProgram" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlFaculty"
                                            Display="None" ErrorMessage="Please Select Program" InitialValue="0" ValidationGroup="Show" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Semester </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="3" AutoPostBack="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>

                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlFaculty"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Show" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Criteria  </label>
                                        </div>
                                        <asp:DropDownList ID="ddlCriteria" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="4">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Pending Modules</asp:ListItem>
                                            <asp:ListItem Value="2">Pending Dues</asp:ListItem>
                                            <asp:ListItem Value="3">Disciplinary Action</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCriteria"
                                            Display="None" ErrorMessage="Please Select Criteria" InitialValue="0" ValidationGroup="Show" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="divSession">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Session </label>
                                        </div>
                                        <asp:DropDownList ID="ddlsession" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="7">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlsession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Submit" />
                                    </div>

                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnShow" runat="server" CssClass="btn btn-outline-info" TabIndex="5" ValidationGroup="Show" OnClick="btnShow_Click">Show</asp:LinkButton>
                                <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" TabIndex="8" Visible="false" OnClick="btnSubmit_Click" ValidationGroup="Submit">Submit</asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" TabIndex="6" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Show"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <%--<div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>*</sup>
                                    <label>Session </label>
                                </div>
                                <asp:DropDownList ID="ddlsession" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="7">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label></label>
                                </div>
                                <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info"  TabIndex="8">Submit</asp:LinkButton>
                            </div>
                        </div>
                    </div>--%>
                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvblockStudent" runat="server" Visible="true">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="chkhead" runat="server" onclick="return totAll(this);" /></th>

                                                        <th>Reg.No.
                                                        </th>
                                                        <th>Name
                                                        </th>
                                                        <th>Program
                                                        </th>
                                                        <th>Current Semester</th>
                                                        <th>Criteria</th>
                                                        <th>View Details</th>

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
                                                    <asp:CheckBox ID="chkstud" runat="server" />

                                                </td>

                                                <td>
                                                    <asp:Label ID="lblRegno" runat="server" Text='<%#Eval("REGNO") %>'></asp:Label>
                                                    <asp:Label ID="lblIdno" runat="server" Text='<%#Eval("IDNO") %>' Visible="false"></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("NAME_WITH_INITIAL") %>'></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:Label ID="lblProgram" runat="server" Text='<%#Eval("PROGRAM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblsemester" runat="server" Text='<%#Eval("SEMESTERNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblcriteria" runat="server" Text='<%#Eval("CRITERIA")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-outline-primary btn-sm" OnClick="btnView_Click" CommandArgument='<%#Eval("IDNO") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </asp:Panel>

                            </div>
                            <%--  <div class="col-12 mt-4">
                        <div class="sub-heading">
                            <h5>Student List</h5>
                        </div>
                        <div class="table-responsive">
                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                <thead class="bg-light-blue">
                                    <tr id="tr1">
                                        <th><asp:CheckBox ID="CheckBox2" runat="server" /></th>
                                        <th>Sr.No.
                                        </th>
                                        <th>Reg.No.
                                        </th>
                                        <th>Name
                                        </th>
                                        <th>Program
                                        </th>
                                        <th>Current Semester</th>
                                        <th>Criteria</th>
                                        <th>View Details</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox1" runat="server" /></td>
                                        <td>S1</td>
                                        <td>0123456789</td>
                                        <td>Chaminda Vass</td>
                                        <td>Program Name</td>
                                        <td>SemII</td>
                                        <td>Pending Criteria</td>
                                        <td>
                                            <asp:Button ID="btnView" runat="server" text="View" CssClass="btn btn-outline-primary btn-sm"/>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>--%>
                        </div>
                    </div>
                </div>
            </div>
           <div class="modal fade" id="myModal">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">

                        <!-- Modal Header -->
                        <div class="modal-header">
                            <h4 class="modal-title">Student Details</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>

                        <!-- Modal body -->
                        <div class="modal-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-4 col-md-12 col-12">

                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Student ID</label>
                                        </div>
                                        <asp:TextBox ID="txtStudentId" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-8 col-md-12 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Student Name</label>
                                        </div>
                                        <asp:TextBox ID="txtStudentName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-12 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Current Semester</label>
                                        </div>
                                        <asp:TextBox ID="txtSemester" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-8 col-md-12 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Program</label>
                                        </div>
                                        <asp:TextBox ID="txtProgram" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    
                                     <div class="form-group col-lg-4 col-md-12 col-12" runat="server" visible="false" id="divDemand">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Total Demand</label>
                                        </div>
                                        <asp:TextBox ID="txtDemand" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-12 col-12" runat="server" visible="false" id="divTotalfees">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Total Balance</label>
                                        </div>
                                        <asp:TextBox ID="txtTotalPaidFee" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>

                                     <div class="form-group col-lg-4 col-md-12 col-12" runat="server" visible="false" id="divPaidfees">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Paid Fees</label>
                                        </div>
                                        <asp:TextBox ID="txtPaidFee" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>

                       
                                </div>
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="Panel2" runat="server">
                                    <asp:ListView ID="lvPendingModule" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Pending Module</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>CCode
                                                        </th>
                                                        <th>Module
                                                        </th>
                                                        <th>Semester
                                                        </th>
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
                                                    <asp:Label ID="lblRegno" runat="server" Text='<%#Eval("CCODE") %>'></asp:Label>
                                                  
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("COURSE_NAME") %>'></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:Label ID="lblProgram" runat="server" Text='<%#Eval("SEMESTERNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblsemester" runat="server" >Pending</asp:Label>
                                                </td>
                                              
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </asp:Panel>

                            </div>
                            <%--  <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnPay" runat="server" CssClass="btn btn-outline-info">Pay</asp:LinkButton>
                                <asp:LinkButton ID="btnCancl" runat="server" CssClass="btn btn-outline-danger">Cancel</asp:LinkButton>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
<%--            <asp:AsyncPostBackTrigger ControlID="lvblockStudent" />--%>
         <%--   <asp:PostBackTrigger ControlID="btnView" />--%>
            
        </Triggers>

    </asp:UpdatePanel>
     
    <script language="javascript" type="text/javascript">
        function totAll(chkhead) {

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (chkhead.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>
</asp:Content>

