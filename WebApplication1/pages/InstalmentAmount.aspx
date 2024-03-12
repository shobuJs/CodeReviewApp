<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InstalmentAmount.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="ACADEMIC_StudentDocumentList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upBulkInstalment"
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

    <script type="text/javascript">
        function exefunction() {
            debugger;
            var count = 0;
            list = 'lvDocumentList';
            var dataRows = document.getElementsByTagName('tr');
            if (dataRows != null) {
                for (i = 0; i < dataRows.length - 1; i++) {

                    if (document.getElementById("ctl00_ContentPlaceHolder1_lvDocumentList_ctrl" + i + "_chkOriCopy").checked) {
                        count++;
                    }
                }
                if (count == 0) {
                    alert("Please check atleast one check box !!!");
                    return false;
                }
            }
        }
    </script>

    <%--<script type="text/javascript">
        try{
            RunThisAfterEachAsyncPostback();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
        }
        catch
        {}
    </script>--%>

    <asp:UpdatePanel ID="upBulkInstalment" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border"> 
                            <h3 class="box-title"><span>FEES INSTALLAMENT DETAILS</span></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Registration No.</label>
                                        </div>
                                        <asp:TextBox ID="txtAdmissionNo" runat="server" TabIndex="1" CssClass="form-control"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter Registration No."
                                            ControlToValidate="txtAdmissionNo" Display="None" SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Receipt Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlReceiptType" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged" 
                                            CssClass="form-control" data-select2-enable="true" TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlReceiptType" runat="server" Display="None"
                                            InitialValue="0" ErrorMessage="Please Select Receipt Type." ValidationGroup="Show" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true"  
                                            CssClass="form-control" data-select2-enable="true" TabIndex="3">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlSemester" runat="server" Display="None"
                                            InitialValue="0" ErrorMessage="Please Select Semester." ValidationGroup="Show" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">          
                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-outline-info" TabIndex="4" ValidationGroup="Show"
                                    OnClick="btnShow_Click" />
                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-outline-danger" TabIndex="5" 
                                    OnClick="btnClear_Click" />
                                <asp:ValidationSummary ID="valShowSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Show" />
                            </div>

                            <div class="col-md-12" id="divStudInfo" runat="server" visible="false">
                                <div class="sub-heading"><h5>Student Information</h5></div>
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Name of Student :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStudName" runat="server" Font-Bold="True" />
                                                    <asp:Label ID="lblidno" Visible="false" runat="server" Font-Bold="True" />
                                                    <asp:HiddenField ID="hdfDmno" Value="0" runat="server" />
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Degree :</b>
                                                <a class="sub-label"><asp:Label ID="lblDegree" runat="server" Font-Bold="True" /></a>
                                            </li>
                                            <li class="list-group-item"><b>Branch :</b>
                                                <a class="sub-label"><asp:Label ID="lblBranch" runat="server" Font-Bold="True" /></a>
                                            </li>
                                            <li class="list-group-item"><b>No.of Installment :</b>
                                                <a class="sub-label"><asp:Label ID="lblinstalment" runat="server" Font-Bold="True" /></a>
                                            </li>        
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Semester/Year :</b>
                                                <a class="sub-label"><asp:Label ID="lblSemester" runat="server" Font-Bold="True" /></a>
                                            </li>
                                            <li class="list-group-item"><b>Payment Type :</b>
                                                <a class="sub-label"><asp:Label ID="lblPaymentType" runat="server" Font-Bold="True" /></a>
                                            </li>
                                            <li class="list-group-item"><b>Total Demand :</b>
                                                <a class="sub-label"><asp:Label ID="lblDemand" runat="server" Font-Bold="True" /></a>
                                            </li>      
                                        </ul>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Remark by Approval Authority</label>
                                        </div>
                                        <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="form-control" MaxLength="20" TabIndex="5"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <asp:Panel ID="pnlStudinstalment" runat="server" Visible="true">
                                        <asp:GridView ID="grdinstalment" runat="server" AutoGenerateColumns="False"
                                            ShowFooter="True" CssClass="table table-hovered table-bordered">
                                            <Columns>
                                                <%--<asp:BoundField DataField="RowNumber" HeaderText="Sr.No." ItemStyle-Width="200px" />--%>
                                                    <asp:TemplateField HeaderText="Sr.No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="INSTALMENT_NO" HeaderText="Installment No." ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="200px" />
                                                </asp:BoundField>

                                                <asp:TemplateField HeaderText="Due Date">
                                                    <ItemTemplate>
                                                        <div class="input-group">
                                                            <div class="input-group-addon" id="ToDate" runat="server">
                                                                <i class="fa fa-calendar" runat="server" id="Cal"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtDueDate" runat="server" class="form-control"
                                                                ondrop="return false;" placeholder="Due Date" onpaste="return false;" Text='<%# Bind("DUE_DATE") %>'  onkeypress="return RestrictCommaSemicolon(event);" onkeyup="ConvertEachFirstLetterToUpper(this.id)">
                                                            </asp:TextBox>
                                                            <ajaxToolKit:CalendarExtender ID="meetodate" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtDueDate" PopupButtonID="Cal" Enabled="True">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtDueDate" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mvToDate" runat="server" ControlExtender="meToDate"
                                                                ControlToValidate="txtDueDate" Display="None" EmptyValueMessage="Please Enter Due Date"
                                                                ErrorMessage="Please Enter Due Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />
                                                            <ajaxToolKit:MaskedEditValidator ID="rfvMonth" runat="server" ControlExtender="meToDate"
                                                                ControlToValidate="txtDueDate" Display="None" EmptyValueMessage="Please Enter Due Date"
                                                                ErrorMessage="Please Enter Due Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Daywise" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Enter DueDate."
                                                                ControlToValidate="txtDueDate" Display="None" SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                    </EditItemTemplate>
                                                    <ControlStyle Width="300px"></ControlStyle>
                                                    <ItemStyle Width="300px"></ItemStyle>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAmount" runat="server" MaxLength="10" Text='<%# Bind("INSTALL_AMOUNT") %>' 
                                                            Enabled='<%# (Convert.ToInt32(Eval("RECON") ) == 1 ?  false : true )%>'  
                                                            class="form-control" placeholder="0.00">
                                                        </asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteValue" runat="server"
                                                            FilterType="Custom" FilterMode="ValidChars" ValidChars="0.123456789" TargetControlID="txtAmount">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter Amount."
                                                            ControlToValidate="txtAmount" Display="None" SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                    </EditItemTemplate>
                                                    <ControlStyle Width="200px"></ControlStyle>
                                                    <ItemStyle Width="200px"></ItemStyle>

                                                    <FooterStyle />
                                                    <FooterTemplate>
                                                        <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                                                            Text="Add New Installment" CssClass="btn btn-primary" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Remove">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="lnkRemove" runat="server" OnClick="lnkRemove_Click"
                                                            ImageUrl="~/IMAGES/delete.gif" AlternateText="Remove Row" Visible='<%# (Convert.ToInt32(Eval("RECON") ) == 1 ?  false : true )%>'
                                                            OnClientClick="return UserDeleteConfirmation();"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                    </EditItemTemplate>
                                                    <ControlStyle Width="20px"></ControlStyle>
                                                    <ItemStyle Width="50px"></ItemStyle>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# (Convert.ToInt32(Eval("RECON") )== 1 ?  "Paid" : "Not Paid" )%>'
                                                            ForeColor='<%# (Convert.ToInt32(Eval("RECON") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100px" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <HeaderStyle CssClass="bg-light-blue" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" ValidationGroup="Show" CssClass="btn btn-outline-info" TabIndex="6"
                                        OnClick="submit_Click" Font-Bold="True" />
                                    <asp:Button ID="btncancel" runat="server" Text="Cancel" ToolTip="Cancel Student Information"
                                        CausesValidation="false" CssClass="btn btn-outline-danger" TabIndex="7" OnClick="btnCancel_Click" Font-Bold="True" />
                                    <asp:Button ID="btnRemove" runat="server" Text="Remove All Installment" CssClass="btn btn-outline-danger" TabIndex="6"
                                        OnClick="btnRemove_Click" Font-Bold="True" />
                                </div>

                                <div id="divMsg" runat="server"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
