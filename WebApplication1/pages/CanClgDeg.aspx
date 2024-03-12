<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CanClgDeg.aspx.cs" Inherits="ACADEMIC_CanClgDeg" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updDetails"
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

    <asp:UpdatePanel ID="updDetails" runat="server">
        <ContentTemplate>

            <div class="row">
                 <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">

                        <div class="box-header with-border">
                           <%-- <h3 class="box-title">Admission Cancellation and Refund Request</h3>--%>
                              <h3 class="box-title"><asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>


                          <div class="box-body">
                        <div class="col-12" id="divCourses" runat="server">
                            <div class="row" id="DivAdm" runat="server">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                     <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Enter Application Number</label>
                                        </div>
                                    <asp:TextBox ID="txtApplication" runat="server"  Visible="true" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12 mt-3">
                                    <asp:Button ID="btnShow" runat="server" Text="Show" Visible="true" OnClick="btnShow_Click" CssClass="btn btn-outline-info" />
                                </div>
                         
                            </div>
                          </div>
                            <div class="col-12" id="tblInfo" runat="server">
                                <div class="row">
                                <div class="col-lg-6 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item">
                                            <b>Student Name :</b><a class="sub-label">
                                                <asp:Label ID="lblName" runat="server" Font-Bold="True" /></a>
                                        </li>
                                        <li class="list-group-item">
                                            <b>Mobile No :</b><a class="sub-label">
                                                <asp:Label ID="lblMobileNo" runat="server" Font-Bold="True" /></a>
                                        </li>
                                        <%--<li class="list-group-item">
                                            <b>Student Address :</b><a class="pull-right">
                                                <asp:Label ID="lblStudAddress" runat="server" Font-Bold="True" /></a>
                                        </li>  --%>
                                    </ul>
                                </div>
                                <div class="col-lg-6 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item">
                                            <b>Application No. :</b><a class="sub-label">
                                                <asp:Label ID="lblApplication" runat="server" Font-Bold="True" /></a>
                                        </li>
                                        <li class="list-group-item">
                                            <b>Email :</b><a class="sub-label">
                                                <asp:Label ID="lblEmail" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>

                                    </ul>
                                </div>
                                <div class="col-lg-12 col-md-6 col-12 mt-1">

                                     <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item">
                                            <b>Address :</b><a class="sub-label">
                                                <asp:Label ID="lblStudAddress" runat="server" Font-Bold="True" /></a>
                                        </li>
                                         </ul>
                                
                                </div>
                                </div>
                            </div>
                           <div class="col-12 mt-3">
                            <div class="row" id="Div2" runat="server">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Student Name (As per Bank Account)</label>
                                        </div>
                                    <asp:TextBox ID="txtStudNameBank" runat="server" CssClass="form-control" onkeypress="return alphaOnly(event);" ValidationGroup="Submit" ToolTip="Enter Student Name (As per Bank Account)" TabIndex="1" MaxLength="100"></asp:TextBox>
                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter Student Name (As per Bank Account)" ControlToValidate="txtStudNameBank"
                                        Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>

                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbemiddle" runat="server" TargetControlID="txtStudNameBank"
                                        InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />


                                </div>
                               <div class="form-group col-lg-3 col-md-6 col-12">
                                   <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Bank Name</label>
                                        </div>
                                    <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control" ToolTip="Enter Bank Name" TabIndex="2" MaxLength="50"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Enter Bank Name" ControlToValidate="txtBankName"
                                        Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                </div>
                               <div class="form-group col-lg-3 col-md-6 col-12">
                                   <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Account Number</label>
                                        </div>
                                    <asp:TextBox ID="txtAccountNumber" runat="server" CssClass="form-control" ToolTip="Enter Account Number" TabIndex="3" MaxLength="18"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter Account Number" ControlToValidate="txtAccountNumber"
                                        Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtAccountNumber"
                                        ValidChars="1234567890" FilterMode="ValidChars" />
                                </div>
                                  <div class="form-group col-lg-3 col-md-6 col-12" hidden="hidden">
                                      <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label> Branch Code</label>
                                        </div>
                                    <asp:TextBox ID="txtBranchCode" runat="server" CssClass="form-control" ToolTip="Enter Branch Code" TabIndex="4" MaxLength="10"></asp:TextBox>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>IFSC Code</label>
                                        </div>
                                    <asp:TextBox ID="txtIFSCCode" runat="server" CssClass="form-control" ToolTip="Enter IFSC Code" TabIndex="5" MaxLength="12"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Enter IFSC Code" ControlToValidate="txtIFSCCode"
                                        Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                </div>
                               <div class="form-group col-lg-3 col-md-6 col-12">
                                   <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch Address</label>
                                        </div>
                                    <asp:TextBox ID="txtAddress" runat="server" TextMode="SingleLine" CssClass="form-control" ToolTip="Enter Full Postal Address" TabIndex="6" MaxLength="150"></asp:TextBox>
                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Enter Branch Address" ControlToValidate="txtAddress"
                                        Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                </div>
                             <div class="form-group col-lg-3 col-md-6 col-12">
                                 <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Student Mobile No.</label>
                                        </div>
                                    <asp:TextBox ID="txtStudMob" runat="server" CssClass="form-control" ToolTip="Enter Student Mobile No." TabIndex="7" MaxLength="12"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Enter Student Mobile No." ControlToValidate="txtStudMob"
                                        Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtStudMob"
                                        ValidChars="1234567890" FilterMode="ValidChars" />
                                </div>

                                  <div class="form-group col-lg-3 col-md-6 col-12">
                                      <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Pin Code</label>
                                        </div>
                                    <asp:TextBox ID="txtPinCode" runat="server" CssClass="form-control" ToolTip="Enter Pin Code" TabIndex="8" MaxLength="6"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Enter Pin Code" ControlToValidate="txtPinCode"
                                        Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                </div>

                                 <div class="form-group col-lg-3 col-md-6 col-12">
                                     <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>City</label>
                                        </div>
                                    <asp:DropDownList ID="ddlCity" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                        TabIndex="9" AutoPostBack="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select City" ControlToValidate="ddlCity"
                                        Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                </div>

                                  <div class="form-group col-lg-3 col-md-6 col-12">
                                      <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>State</label>
                                        </div>
                                    <asp:DropDownList ID="ddlState" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                        TabIndex="10" AutoPostBack="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select State" ControlToValidate="ddlState"
                                        Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                </div>
                                 <div class="form-group col-lg-12 col-md-6 col-12">
                                
                                    <asp:CheckBox ID="chkDeclaration" runat="server" Checked="true" Enabled="false" AutoPostBack="true" Text=" Declaration :  I, hereby request you to cancel my admission from the following selected courses of the respective colleges." TabIndex="11" />
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Please check Declaration" ControlToValidate="chkDeclaration"
                                        Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                </div>
                                  <div class="form-group col-lg-8 col-md-6 col-12">
                                  <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label> Remark For Cancellation (Max Length 100 Charecter)&nbsp &nbsp &nbsp</label>
                                        </div>
                                    <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine"  Visible="true" TabIndex="12"  CssClass="form-control" MaxLength="100" onkeyup="return CountCharactersStudRemark();"></asp:TextBox>
                                      </div>
                                  <div class="form-group col-lg-8 col-md-6 col-12">
                                  <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admin Remark (Max Length 100 Character) (Office Used Only) &nbsp &nbsp &nbsp &nbsp</label>
                                        </div>
                                    <asp:TextBox ID="txtAdminRemark" runat="server" TextMode="MultiLine"  Visible="true" TabIndex="13"  CssClass="form-control" MaxLength="100" onkeyup="return CountCharactersAdminRemark();"></asp:TextBox>

                                </div>
                                </div>
                            </div>
                      

                      
                            <div class="col-12" id="div1" runat="server" visible="true" hidden="hidden">
                                <asp:Panel ID="Panel2" runat="server">
                                    <asp:ListView ID="lvAut" runat="server">
                                        <LayoutTemplate>
                                            <div>
                                                  <div class="sub-heading">
                                            <h5>Admission Auto Cancel Courses List</h5>
                                                </div>
                                                <label>Note:- Courses You Have been selected for ( For Reference only )</label>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th style="text-align: center;">Select
                                                            </th>
                                                            <th>Sr. No.</th>
                                                            <th>College Name
                                                            </th>
                                                            <th>Degree Name (Preference)
                                                            </th>
                                                            <%-- <th style="text-align: center;">Amount
                                                            </th>   
                                                              <th style="text-align: center;">Order Id
                                                            </th>   
                                                              <th style="text-align: center;">Transaction Date
                                                            </th> --%>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr id="trCurRow">
                                                <td style="text-align: center;">
                                                    <asp:CheckBox ID="chkAccept" runat="server" ToolTip='<%# Eval("DEGREENO")%>' ClientIDMode="Static"
                                                        AutoPostBack="true" Checked="true" Enabled="false" />
                                                    <%-- onclick="CalTotalFee(this)" Enabled='<%# Eval("FAIL_MORE_THAN_2_SUB").ToString()== "1" ? false : true %>'--%>
                                                </td>

                                                <td><%# Container.DataItemIndex+1 %></td>

                                                <td>
                                                    <asp:Label ID="lblCollege" runat="server" Text='<%# Eval("COLLEGE_NAME") %>' ToolTip='<%# Eval("COLLEGE_ID")%>' />
                                                    <asp:HiddenField ID="hdnDEGREENO" Value='<%# Eval("DEGREENO")%>' runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDegree" runat="server" Text='<%# Eval("DEGREENAME") %>' ToolTip='<%# Eval("DEGREENO") %>' />
                                                    <asp:HiddenField ID="hdnDegreePreference" runat="server" Value='<%# Eval("PREFERENCE") %>' />
                                                </td>
                                                <%--  <td style="text-align: center;">
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("AMOUNT") %>' ToolTip='<%# Eval("AMOUNT") %>' />
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:Label ID="lblOrderId" runat="server" Text='<%# Eval("ORDERID") %> ' ToolTip='<%# Eval("ORDERID") %>'  />
                                                </td>
                                                <td style="text-align: center;">                                                   
                                                    <asp:Label ID="lblTransdate" runat="server" Text='<%# Eval("TRANSDATE") %>' ToolTip='<%# Eval("TRANSDATE") %>' />
                                                </td>     --%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </asp:Panel>
                            </div>


                            <div class="col-12" id="div4" runat="server" visible="true">
                                <asp:Panel ID="Panel4" runat="server" >
                                    <asp:ListView ID="lvCANCELDATE" runat="server">
                                        <LayoutTemplate>
                                            <div>

                                                 <div class="sub-heading">
                                            <h5>Admitted Cancelled  Courses Approved List</h5>
                                                </div>
                                              
                                                <label>Note:- The list displays courses that you have Cancelled admission.</label>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th style="text-align: center;">Select
                                                            </th>
                                                            <th>Sr. No.</th>
                                                            <th>College Name
                                                            </th>
                                                            <th>Degree Name (Preference)
                                                            </th>
                                                            <th>Amount
                                                            </th>
                                                            <th>Cancel Date
                                                            </th>
                                                            <th>Approved Date
                                                            </th>
                                                            <%-- <th style="text-align: center;">Order Id
                                                            </th>
                                                            <th style="text-align: center;">Transaction Date
                                                            </th>--%>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr id="trCurRow">
                                                <td style="text-align: center;">
                                                    <asp:CheckBox ID="chkAccept" runat="server" ToolTip='<%# Eval("DEGREENO")%>' ClientIDMode="Static"
                                                        AutoPostBack="true" Enabled="false" Checked="true" />
                                                    <%-- onclick="CalTotalFee(this)" Enabled='<%# Eval("FAIL_MORE_THAN_2_SUB").ToString()== "1" ? false : true %>'--%>
                                                </td>

                                                <td><%# Container.DataItemIndex+1 %></td>

                                                <td>
                                                    <asp:Label ID="lblCollege" runat="server" Text='<%# Eval("COLLEGE_NAME") %>' ToolTip='<%# Eval("COLLEGE_ID")%>' />
                                                    <asp:HiddenField ID="hdnDEGREENO" Value='<%# Eval("DEGREENO")%>' runat="server" />
                                                    <asp:HiddenField ID="hdfValid" Value='<%# Eval("InsertStatus")%>' runat="server" />
                                                    <%--   <asp:HiddenField ID="hdfPrefe" Value='<%# Eval("BRPREF")%>' runat="server" />--%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDegree" runat="server" Text='<%# Eval("DEGREENAME") %>' ToolTip='<%# Eval("DEGREENO") %>' />
                                                    <asp:HiddenField ID="hdnDegreePreference" runat="server" Value='<%# Eval("PREFERENCE") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("AMOUNT") %>' ToolTip='<%# Eval("AMOUNT") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblcanceldDate" runat="server" Text='<%# Eval("CANCELDATE") %>' ToolTip='<%# Eval("CANCELDATE") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblApproved" runat="server" Text='<%# Eval("APPROVAL_DATE") %>' ToolTip='<%# Eval("APPROVAL_DATE") %>' />
                                                </td>
                                                <%--<td style="text-align: center;">
                                                    <asp:Label ID="lblOrderId" runat="server" Text='<%# Eval("ORDERID") %> ' ToolTip='<%# Eval("ORDERID") %>' />
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:Label ID="lblTransdate" runat="server" Text='<%# Eval("TRANSDATE") %>' ToolTip='<%# Eval("TRANSDATE") %>' />
                                                </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </asp:Panel>

                                <p class="text-center">
                                    <asp:HiddenField ID="HiddenField2" runat="server"></asp:HiddenField>

                                </p>


                            </div>

                            <div class="col-12" id="divAllCoursesFromHist" runat="server" visible="true">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvCurrentSubjects" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                            <h5>Admission Cancel Courses List</h5>
                                                </div>
                                          
                                                <%--<h3>
                                                    <label class="label label-default">Admission Cancel Courses List</label></h3>--%>
                                                <label>
                                                    Note:- The list displays courses that you have taken admission to and paid fees for (The check box is already pre-selected for auto cancelled courses as you have opted for and admitted to another course which is unchecked in the list
                                                        Don't select the unchecked box and please click submit if you wish to continue in the currect programme you are admitted to.</label>
                                               <table class="table table-striped table-bordered nowrap display" id="mytable" style="width:100%">
                                                  <thead class="bg-light-blue" >
                                                       <tr>
                                                            <th>Select
                                                            </th>
                                                            <th>Sr. No.</th>
                                                            <th>College Name
                                                            </th>
                                                            <th>Degree Name (Preference)
                                                            </th>
                                                            <th>Amount
                                                            </th>
                                                            <th>Order Id
                                                            </th>
                                                            <th>Transaction Date
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                        
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr id="trCurRow">
                                                <td style="text-align: center;">
                                                    <asp:CheckBox ID="chkAccept" runat="server" ToolTip='<%# Eval("DEGREENO")%>' ClientIDMode="Static"
                                                        AutoPostBack="true" Enabled="true" />
                                                    <%-- onclick="CalTotalFee(this)" Enabled='<%# Eval("FAIL_MORE_THAN_2_SUB").ToString()== "1" ? false : true %>'--%>
                                                </td>

                                                <td ><%# Container.DataItemIndex+1 %></td>

                                                <td>
                                                    <asp:Label ID="lblCollege" runat="server" Text='<%# Eval("COLLEGE_NAME") %>' ToolTip='<%# Eval("COLLEGE_ID")%>' />
                                                    <asp:HiddenField ID="hdnDEGREENO" Value='<%# Eval("DEGREENO")%>' runat="server" />
                                                    <asp:HiddenField ID="hdfValid" Value='<%# Eval("InsertStatus")%>' runat="server" />
                                                    <%--   <asp:HiddenField ID="hdfPrefe" Value='<%# Eval("BRPREF")%>' runat="server" />--%>
                                                </td>
                                                <td >
                                                    <asp:Label ID="lblDegree" runat="server" Text='<%# Eval("DEGREENAME") %>' ToolTip='<%# Eval("DEGREENO") %>' />
                                                    <asp:HiddenField ID="hdnDegreePreference" runat="server" Value='<%# Eval("PREFERENCE") %>' />
                                                </td>
                                                <td >
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("AMOUNT") %>' ToolTip='<%# Eval("AMOUNT") %>' />
                                                </td>
                                                <td >
                                                    <asp:Label ID="lblOrderId" runat="server" Text='<%# Eval("ORDERID") %> ' ToolTip='<%# Eval("ORDERID") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTransdate" runat="server" Text='<%# Eval("TRANSDATE") %>' ToolTip='<%# Eval("TRANSDATE") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </asp:Panel>

                                <p class="text-center">
                                    <asp:HiddenField ID="requestparams" runat="server"></asp:HiddenField>

                                </p>


                            </div>

                     
                         <div class="col-12 btn-footer" id="divSuccess" runat="server">
                       
                            <%--  <asp:Button ID="btnSubmit" runat="server" Text="Submit" Visible="true" OnClick="btnSubmit_Click"
                                        ValidationGroup="Submit" OnClientClick="return showConfirm();" CssClass="btn btn-primary"  TabIndex="13" />--%>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" Visible="true" OnClick="btnSubmit_Click"
                                ValidationGroup="Submit" CssClass="btn btn-outline-info" TabIndex="13" OnClientClick="return showConfirm();" />
                            <asp:Button ID="btnApprove" runat="server" Text="Approve" Visible="false" OnClick="btnApprove_Click" OnClientClick="return showConfirmApprove();" CssClass="btn btn-outline-info" />
                          
                            <asp:Button ID="btnReport" runat="server" Text="Report" Font-Bold="true" CssClass="btn btn-outline-primary" CausesValidation="false" OnClick="btnReport_Click" TabIndex="15" />
                              <asp:Button ID="btnCancel" runat="server" Text="Cancel" Font-Bold="true" CssClass="btn btn-outline-danger" CausesValidation="false" OnClick="btnCancel_Click" TabIndex="14" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Submit" />

                             </div>
                        <div id="myModal33" class="modal fade" role="dialog">
                            <asp:Panel ID="pnlOTP" runat="server">
                                <div class="modal-dialog">
                                    <!-- Modal content-->
                                    <div class="modal-content">

                                        <div class="modal-body">

                                            <div class="" runat="server" id="undertaking" visible="false">
                                                <span>
                                                    <p style="text-decoration-style: solid">
                                                        <span></span>                                                      
                                                        <ul>
                                                            <li>Your Admission Courses  Cancel Successfully.<br />
                                                            </li>
                                                            <li>Now you can proceed with payment of fees to the next programme ,navigate to online payment page to paying the fees
                                                                                <br />
                                                            </li>                                                          
                                                        </ul>
                                                    </p>
                                                </span>
                                                <div runat="server" style="text-align: center">
                                                    <asp:Button ID="OKButton" runat="server" Text="Close" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
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
    <div id="divMsg" runat="server">
    </div>
    <script type="text/javascript" language="JavaScript">

        //function FreezeScreen(msg) {
        //    scroll(0, 0);
        //    var outerPane = document.getElementById('FreezePane');
        //    var innerPane = document.getElementById('InnerFreezePane');
        //    if (outerPane) outerPane.className = 'FreezePaneOn';
        //    if (innerPane) innerPane.innerHTML = msg;
        //}

        function showConfirm() {

            var txtStudNameBank = document.getElementById('ctl00_ContentPlaceHolder1_txtStudNameBank').value.trim();
            var txtBankName = document.getElementById('ctl00_ContentPlaceHolder1_txtBankName').value.trim();
            var txtAccountNumber = document.getElementById('ctl00_ContentPlaceHolder1_txtAccountNumber').value.trim();
            var txtIFSCCode = document.getElementById('ctl00_ContentPlaceHolder1_txtIFSCCode').value.trim();
            var txtAddress = document.getElementById('ctl00_ContentPlaceHolder1_txtAddress').value.trim();
            var txtStudMob = document.getElementById('ctl00_ContentPlaceHolder1_txtStudMob').value.trim();
            var txtPinCode = document.getElementById('ctl00_ContentPlaceHolder1_txtPinCode').value.trim();
            var txtRemark = document.getElementById('ctl00_ContentPlaceHolder1_txtRemark').value.trim();
            //     var txtAdminRemark = document.getElementById('ctl00_ContentPlaceHolder1_txtAdminRemark').value.trim();

            ///

            if (txtStudNameBank == "") {
                // alert(chkDeclaration)
                alert("Please Enter Student Name (As per Bank Account)")
                return false;
            }

            if (txtBankName == "") {
                alert("Please Enter Bank Name")
                return false;
            }

            if (txtAccountNumber == "") {
                alert("Please Enter Account Number")
                return false;
            }

            if (txtIFSCCode == "") {
                alert("Please Enter IFSC Code")
                return false;
            }

            if (txtAddress == "") {
                alert("Please Enter Branch Address")
                return false;
            }

            if (txtStudMob == "") {
                alert("Please Enter Student Mobile No.")
                return false;
            }
            ///

            if (txtPinCode == "") {
                alert("Please Enter Pin Code")
                return false;
            }

            var city = document.getElementById("<%=ddlCity.ClientID %>").value.trim();
            if (city == 0) {
                alert("Please Select City")
                return false;
            }
            var state = document.getElementById("<%=ddlState.ClientID %>").value.trim();
            if (state == "0") {
                alert("Please Select State")
                return false;
            }
            if (document.getElementById("<%=chkDeclaration.ClientID %>").checked == false) {
                alert("Please Check Declaration")
                return false;
            }

            if (txtRemark == "") {
                alert("Please Enter Remark For Cancellation")
                return false;
            }

            //if (txtAdminRemark == "") {
            //    alert("Please Enter Admin Remark.")
            //    return false;
            //}
            ///

            var ret = confirm('Do you Really want to Cancel the Admission Courses?');
            if (ret == true) {
                //FreezeScreen('Please Wait, Your Data is Being Processed...');
                validate = true;
            }
            else
                validate = false;
            return validate;
        }

        function showConfirmApprove() {
            var ret = confirm('Do you Really want to Approve the Cancel Admitted Courses?');
            if (ret == true) {
                //FreezeScreen('Please Wait, Your Data is Being Processed...');
                validate = true;
            }
            else
                validate = false;
            return validate;
        }


        function showPayConfirm() {
            var ret = confirm('Applied Revaluation details Will be confirmed only after successful payments.Do you Really want to Pay Amount Online ?');
            if (ret == true) {
                //FreezeScreen('Please Wait, Your Data is Being Processed...');
                validate = true;
            }
            else
                validate = false;
            return validate;
        }


        function showChallanConfirm() {
            var ret = confirm('Do you Really want to Print Revaluation Challan ?');
            if (ret == true) {
                //FreezeScreen('Please Wait, Your Data is Being Processed...');
                validate = true;
            }
            else
                validate = false;
            return validate;
        }
    </script>


    <script src="../Content/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("[id$='cbHead']").live('click', function () {
                $("[id$='chkAccept']").attr('checked', this.checked);
            });
        });
    </script>
    <script language="javascript" type="text/javascript">
        function alphaOnly(e) {
            var code;
            if (!e) var e = window.event;

            if (e.keyCode) code = e.keyCode;
            else if (e.which) code = e.which;

            if ((code >= 48) && (code <= 57)) { return false; }
            return true;
        }
    </script>

    <script type="text/javascript">

        function CountCharactersStudRemark() {
            var maxSize = 100;
            // ctl00_ContentPlaceHolder1_txtRemark  txtAdminRemark   
            if (document.getElementById('<%= txtAdminRemark.ClientID %>')) {
                var ctrl = document.getElementById('<%= txtAdminRemark.ClientID %>');
                var len = document.getElementById('<%= txtAdminRemark.ClientID %>').value.length;
                if (len <= maxSize) {
                    return;
                }
                else {
                    alert("Max Of length Should be only 100 Characters ");
                    ctrl.value = ctrl.value.substring(0, maxSize);

                }
            }

            return false;
        }
    </script>
    <script type="text/javascript">
        function CountCharactersStudRemark() {
            var maxSize = 100;
            // ctl00_ContentPlaceHolder1_txtRemark  txtAdminRemark   
            if (document.getElementById('<%= txtRemark.ClientID %>')) {
                var ctrl = document.getElementById('<%= txtRemark.ClientID %>');
                var len = document.getElementById('<%= txtRemark.ClientID %>').value.length;
                if (len <= maxSize) {
                    return;
                }
                else {
                    alert("Max Of length Should be only 100 Characters ");
                    ctrl.value = ctrl.value.substring(0, maxSize);

                }
            }

            return false;
        }
    </script>
</asp:Content>


