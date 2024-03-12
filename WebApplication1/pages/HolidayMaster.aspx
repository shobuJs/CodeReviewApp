<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="HolidayMaster.aspx.cs" Inherits="ACADEMIC_HolidayMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_ceStartDate_popupDiv {
            z-index: 100;
        }

        #ctl00_ContentPlaceHolder1_CalendarExtender2_popupDiv {
            z-index: 100;
        }

        #ctl00_ContentPlaceHolder1_txtSLongName {
            z-index: 100;
        }

        .radio label {
            min-height: 20px;
            padding-left: 10px;
            margin-bottom: 0;
            font-weight: 400;
            cursor: pointer;
        }

        .txtboxClass {
            font-size: inherit;
            font-family: inherit;
            padding: 5px 12px;
            letter-spacing: normal;
            background: #fff !important;
            color: #3c4551;
            border-radius: 5px;
            font-weight: 400;
            border-left: 6px solid #25CD7F !important;
        }
    </style>

    <div style="z-index: 1; position: absolute; top: 50%; left: 50%; transform: translate(-50%,-50%);">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upHDay"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div>
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>


    <asp:UpdatePanel ID="upHDay" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Restricted Holiday Master</h3>
                        </div>

                        <div class=" col-sm-12 form-group">
                            &nbsp
                            <label>Note:</label>
                            <span style="color: #FF0000">* Marked is Mandatory !</span>
                        </div>

                       <%-- <form role="form">--%>
                            <div class="box-body">

                                <div class="row">
                                    <div class="form-group col-md-3">
                                        <span style="color: red;">*</span>
                                        <label for="city">Session</label>
                                        <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"
                                            AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-md-3">
                                        <span style="color: red">*</span>
                                        <label for="city">Degree </label>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            CssClass="form-control" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-md-3">
                                        <span style="color: red">*</span>
                                        <label for="city">Branch</label>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            CssClass="form-control" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" TabIndex="3">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-md-3">
                                        <span style="color: red">*</span>
                                        <label for="city">Regulation</label>
                                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                            AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" TabIndex="4">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Scheme\Regulation" ValidationGroup="submit"></asp:RequiredFieldValidator>

                                    </div>


                                    <div class="form-group col-md-3">
                                        <span style="color: red">*</span>
                                        <label for="city">Semester</label>
                                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" AutoPostBack="true"
                                            CssClass="form-control" TabIndex="5">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-md-3">
                                        <span style="color: red;">*</span>
                                        <label for="city">Subject Type</label>
                                        <asp:DropDownList ID="ddlSubjectType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged"
                                            ValidationGroup="Submit" CssClass="form-control" TabIndex="6"
                                            AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlSubjectType"
                                            ErrorMessage="Please Select Subject Type" InitialValue="0" Display="None" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>



                                    <div class="form-group col-md-3">
                                        <span style="color: red;">*</span>
                                        <label for="city">Section</label>
                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true"
                                            ValidationGroup="teacherallot" TabIndex="7" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSection" runat="server" ControlToValidate="ddlSection"
                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-md-3" id="Batch" runat="server" visible="false">
                                        <span style="color: red;">*</span>
                                        <label for="city">Batch</label>
                                        <asp:DropDownList ID="ddlBatches" runat="server" TabIndex="8" AppendDataBoundItems="true"
                                            OnSelectedIndexChanged="ddlBatches_SelectedIndexChanged"
                                            AutoPostBack="True" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlBatches"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Batch" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </div>


                                      <div class="form-group col-md-3">
                                <span style="color: red;">*</span> <label for="city">Slot Type</label>
                                <asp:DropDownList ID="ddlSlotType" runat="server" AppendDataBoundItems="True" TabIndex="9"
                                   AutoPostBack="true" OnSelectedIndexChanged="ddlSlotType_SelectedIndexChanged" CssClass="form-control">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlSlotType" runat="server" ControlToValidate="ddlSlotType"
                                    Display="None" ErrorMessage="Please Select Slot Type" InitialValue="0" ValidationGroup="submit">
                                </asp:RequiredFieldValidator>
                            </div>



                                       <div class="form-group col-md-3">
                                <span style="color: red;">*</span> <label for="city">Existing Dates</label>
                                <asp:DropDownList ID="ddlExistingDates" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                  TabIndex="10"  OnSelectedIndexChanged="ddlExistingDates_SelectedIndexChanged" CssClass="form-control">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlExistingDates"
                                    Display="None" ErrorMessage="Please Select Existing Dates" InitialValue="0" ValidationGroup="submit">
                                </asp:RequiredFieldValidator>
                            </div>



                                      <div class="form-group col-md-3">
                                        <span style="color: #FF0000; font-weight: bold">*</span>
                                        <label>Holiday Name</label>
                                        <asp:TextBox ID="txtHdayName" runat="server" CssClass="form-control" TabIndex="11" 
                                            placeholder="Enter Holiday Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvHdayName" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Enter Holiday Name" ControlToValidate="txtHdayName"
                                            Display="None" ValidationGroup="submit" />
                                    </div>


                                    <div class="form-group col-md-3">
                                        <span style="color: red;">*</span>
                                        <label for="city">Change Time Table Day</label>
                                        <asp:DropDownList ID="ddlChangeTimetableDays" runat="server" AppendDataBoundItems="true"
                                            OnSelectedIndexChanged="ddlChangeTimetableDays_SelectedIndexChanged" AutoPostBack="true"
                                            ValidationGroup="teacherallot" TabIndex="12" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlChangeTimetableDays"
                                            Display="None" ErrorMessage="Please Select Time Table Day" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>



                                     <div class="form-group col-md-3">
                                        <span style="color: #FF0000; font-weight: bold">*</span>
                                        <label for="city">Restricted Holiday Date</label>
                                          <div class="input-group">
                                            <div class="input-group-addon"  id="Date">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                        <asp:TextBox ID="txtHdayDate" runat="server" TabIndex="13" ValidationGroup="submit" CssClass="form-control"
                                         AutoPostBack="true" OnTextChanged="txtHdayDate_TextChanged"   placeholder="DD/MM/YYYY" />
                                        <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtHdayDate" PopupButtonID="Date" />
                                        <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtHdayDate"
                                            Display="None" ErrorMessage="Please Enter Holiday Date" SetFocusOnError="True"
                                            ValidationGroup="submit" />
                                        <ajaxToolKit:MaskedEditExtender ID="meeStartDate" runat="server" OnInvalidCssClass="errordate"
                                            TargetControlID="txtHdayDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                            DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                        <ajaxToolKit:MaskedEditValidator ID="mevStartDate" runat="server" ControlExtender="meeStartDate"
                                            ControlToValidate="txtHdayDate" EmptyValueMessage="Please Enter Holiday Date"
                                            InvalidValueMessage="Holiday Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                            TooltipMessage="Please Enter Holiday Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                            ValidationGroup="submit" SetFocusOnError="True" />
                                              </div>
                                    </div>


                                   

                                    <div class="form-group col-md-3">
                                        <span style="color: red;">*</span>
                                        <label for="city">Reason</label>
                                        <asp:TextBox ID="txtReason" runat="server" AppendDataBoundItems="true" TextMode="MultiLine" TabIndex="14"
                                            ValidationGroup="teacherallot" CssClass="form-control">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtReason"
                                            Display="None" ErrorMessage="Please Enter Reason" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>


                                     <div class="form-group col-md-6">
                                        <span style="color: red;">*</span>
                                        <label for="city">Slots</label>
                                        <asp:CheckBoxList ID="chkSelectdSlots" CssClass="col-lg-12" RepeatDirection="Horizontal" TabIndex="15"
                                            RepeatColumns="3" runat="server">
                                        </asp:CheckBoxList>
                                    </div>


                                </div>

                            </div>
                            <!-- /.box-body -->

                          
 


                            <div class="box-footer">
                                
                                 <p class="text-center">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit" OnClick="btnSubmit_Click" class="btn btn-outline-info" TabIndex="16" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" class="btn btn-outline-danger" TabIndex="17" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                        ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                </p>
                                <p>
                                    <i class="fa fa-pencil text-center" style="color: #3c8dbc"></i><strong>Note :</strong> For <span style="COLOR: #f20943; FONT-WEIGHT: BOLD">Compulsory Holiday </span>(You can't mark attendance on this holiday).
                                    <br />
                                    Please Refer : Academic --> Masters  -->  Common Master -->  Holiday.
                                </p>


                                <div class="col-sm-12">
                                    <asp:Panel ID="pnllvHday" runat="server" Visible="true">
                                        <div class="col-md-12" style="width:100%;overflow:auto;">
                                          <table id="example1" class="table table-bordered table-hover table-fixed">
                                    <asp:ListView ID="lvHday" runat="server">
                                        <LayoutTemplate>
                                            <div>
                                                <h3>
                                                    <label class="label label-default">
                                                        Restricted Holiday List
                                                    </label>
                                                </h3>
                                          
                                                    <thead>
                                                        <tr class="bg-light-blue" style="text-align: center">
                                                            <th style="text-align: center">Edit</th>
                                                            <th style="text-align: center">Holiday Name</th>
                                                            <th style="text-align: center">Date</th>
                                                            <th style="text-align: center">Session</th>
                                                           <%-- <th style="text-align: center">Branch</th>--%>
                                                            <th style="text-align: center">Regulation</th>
                                                            <th style="text-align: center">Semester</th>
                                                            <th style="text-align: center">Section</th>
                                                            <th style="text-align: center">Batch</th>
                                                            <th style="text-align: center">DayName</th>
                                                            <th style="text-align: center;width:150px;" >Slots</th>
                                                            <th style="text-align: center">Reason</th>


                                                            <%-- <th style="text-align: center">Status For Attendance</th>--%>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                              
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr style="text-align: center">
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/IMAGES/edit.gif"
                                                        CommandArgument='<%# Eval("HOLIDAY_NO")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                        OnClick="btnEdit_Click" />
                                                </td>
                                                <td><%#Eval("HOLIDAY_NAME") %></td>
                                                <td>
                                                    <asp:Label ID="lblhdate" runat="server" Text='<%#Eval("HOLIDAY_DATE")%>' ToolTip='<%#Eval("HOLIDATE")%>'></asp:Label>
                                                </td>

                                                <td><%#Eval("SESSION_PNAME")%></td>
                                                <%--<td><%#Eval("BRANCHNAME")%></td>--%>
                                                <td><%#Eval("SCHEMENAME")%></td>
                                                <td><%#Eval("SEMESTERNAME")%></td>
                                                <td><%#Eval("SECTIONNAME")%></td>
                                                <td><%#Eval("BATCHNAME")%></td>
                                                <td><%#Eval("DAY_NAME")%></td>
                                                <td style="width:150px;"><%#Eval("SLOTNAME")%></td>
                                                <td><%#Eval("REASON")%></td>

                                                <%-- <td><%#Eval("LOCK")%></td>--%>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr style="text-align: center">
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server"  
                                                        CommandArgument='<%# Eval("HOLIDAY_NO")%>' AlternateText="Edit Record" ToolTip="EditRecord"
                                                        OnClick="btnEdit_Click" />
                                                </td>
                                                <td><%#Eval("HOLIDAY_NAME") %></td>
                                                <td>
                                                    <asp:Label ID="lblhdate" runat="server" Text='<%#Eval("HOLIDAY_DATE")%>' ToolTip='<%#Eval("HOLIDATE")%>'></asp:Label>
                                                </td>


                                                <td><%#Eval("SESSION_PNAME")%></td>
                                              <%--  <td><%#Eval("BRANCHNAME")%></td>--%>
                                                <td><%#Eval("SCHEMENAME")%></td>
                                                <td><%#Eval("SEMESTERNAME")%></td>
                                                <td><%#Eval("SECTIONNAME")%></td>
                                                <td><%#Eval("BATCHNAME")%></td>
                                                <td><%#Eval("DAY_NAME")%></td>
                                                <td style="width:150px;"><%#Eval("SLOTNAME")%></td>
                                                <td><%#Eval("REASON")%></td>


                                                <%-- <td><%#Eval("LOCK")%></td>--%>
                                            </tr>
                                        </AlternatingItemTemplate>
                                    </asp:ListView>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                         <%-- </table>--%>
                                   <%-- <div class="vista-grid_datapager" style="text-align: center">
                                        <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvHday" PageSize="10"
                                            OnPreRender="dpPager_PreRender">
                                            <Fields>
                                                <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                    ShowLastPageButton="false" ShowNextPageButton="false" />
                                                <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                                <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                    ShowLastPageButton="true" ShowNextPageButton="true" />
                                            </Fields>
                                        </asp:DataPager>
                                    </div>--%>
                                </div>
                            </div>
                      <%--  </form>--%>
                    </div>
                </div>
                <!--academic Calendar-->
            </div>

</ContentTemplate>
        <Triggers>
           <%-- <asp:PostBackTrigger ControlID="ddlSession" />
             <asp:PostBackTrigger ControlID="ddlDegree"  />
             <asp:PostBackTrigger ControlID="ddlBranch"  />
             <asp:PostBackTrigger ControlID="ddlScheme" />
             <asp:PostBackTrigger ControlID="ddlsem"  />
             <asp:PostBackTrigger ControlID="ddlSubjectType"  />
             <asp:PostBackTrigger ControlID="ddlSection" />
             <asp:PostBackTrigger ControlID="ddlBatches"  />
             <asp:PostBackTrigger ControlID="ddlSlotType"  />--%>
               <asp:PostBackTrigger ControlID="ddlExistingDates"  />
             <%--<asp:PostBackTrigger ControlID="txtHdayDate"  />
              <asp:PostBackTrigger ControlID="ddlChangeTimetableDays"  />
              <asp:PostBackTrigger ControlID="btnSubmit"  />
               <asp:PostBackTrigger ControlID="btnCancel"  />
            <asp:PostBackTrigger ControlID="lvHday"  />--%>
            </Triggers>
    </asp:UpdatePanel>


         <script type="text/javascript">
             $(document).ready(function () {
                 bindDataTable1();
                 Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable1);
             });
             function bindDataTable1() {
                 var myDT = $('#example1').DataTable();
                 $('#example1').removeClass("dataTable no-footer");
             }
          
    </script>
</asp:Content>

