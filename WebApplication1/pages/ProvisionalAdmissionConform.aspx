<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ProvisionalAdmissionConform.aspx.cs" Inherits="ACADEMIC_CheckStudentInfo" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Content/jquery.js" type="text/javascript"></script>

    <div style="width: 100%">
        <table width="100%" style="text-align: left">

            <tr>
                <td>
                    <table class="vista_page_title_bar" width="100%">
                        <tr>
                            <td style="height: 30px">PROVISIONAL ADMISSION CONFIRM / CANCEL
                                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                                    AlternateText="Page Help" ToolTip="Page Help" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                    </div>
                    <!-- Info panel to be displayed as a flyout when the button is clicked -->
                    <div id="info" style="display: none; width: 250px; z-index: 2; font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                        <div id="btnCloseParent" style="float: right;">
                            <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                                ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                        </div>
                        <div>
                            <p class="page_help_head">
                                <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                                <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                                Edit Record&nbsp;&nbsp;
                                <asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" />
                                Delete Record
                            </p>
                            <p class="page_help_text">
                                <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                            </p>
                        </div>
                    </div>

                    <script type="text/javascript" language="javascript">
                        // Move an element directly on top of another element (and optionally
                        // make it the same size)
                        function Cover(bottom, top, ignoreSize) {
                            var location = Sys.UI.DomElement.getLocation(bottom);
                            top.style.position = 'absolute';
                            top.style.top = location.y + 'px';
                            top.style.left = location.x + 'px';
                            if (!ignoreSize) {
                                top.style.height = bottom.offsetHeight + 'px';
                                top.style.width = bottom.offsetWidth + 'px';
                            }
                        }
                    </script>

                    <ajaxToolKit:AnimationExtender ID="OpenAnimation" runat="server" TargetControlID="btnHelp">
                        <Animations>
                        <OnClick>
                            <Sequence>
                                <%-- Disable the button so it can't be clicked again --%>
                                <EnableAction Enabled="false" />
                                
                                <%-- Position the wire frame on top of the button and show it --%>
                                <ScriptAction Script="Cover($get('ctl00$ContentPlaceHolder1$btnHelp'), $get('flyout'));" />
                                <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>
                                
                                <%-- Move the wire frame from the button's bounds to the info panel's bounds --%>
                                <Parallel AnimationTarget="flyout" Duration=".3" Fps="25">
                                    <Move Horizontal="150" Vertical="-50" />
                                    <Resize Width="260" Height="280" />
                                    <Color PropertyKey="backgroundColor" StartValue="#AAAAAA" EndValue="#FFFFFF" />
                                </Parallel>
                                
                                <%-- Move the info panel on top of the wire frame, fade it in, and hide the frame --%>
                                <ScriptAction Script="Cover($get('flyout'), $get('info'), true);" />
                                <StyleAction AnimationTarget="info" Attribute="display" Value="block"/>
                                <FadeIn AnimationTarget="info" Duration=".2"/>
                                <StyleAction AnimationTarget="flyout" Attribute="display" Value="none"/>
                                
                                <%-- Flash the text/border red and fade in the "close" button --%>
                                <Parallel AnimationTarget="info" Duration=".5">
                                    <Color PropertyKey="color" StartValue="#666666" EndValue="#FF0000" />
                                    <Color PropertyKey="borderColor" StartValue="#666666" EndValue="#FF0000" />
                                </Parallel>
                                <Parallel AnimationTarget="info" Duration=".5">
                                    <Color PropertyKey="color" StartValue="#FF0000" EndValue="#666666" />
                                    <Color PropertyKey="borderColor" StartValue="#FF0000" EndValue="#666666" />
                                    <FadeIn AnimationTarget="btnCloseParent" MaximumOpacity=".9" />
                                </Parallel>
                            </Sequence>
                        </OnClick>
                        </Animations>
                    </ajaxToolKit:AnimationExtender>
                    <ajaxToolKit:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="btnClose">
                        <Animations>
                        <OnClick>
                            <Sequence AnimationTarget="info">
                                <%--  Shrink the info panel out of view --%>
                                <StyleAction Attribute="overflow" Value="hidden"/>
                                <Parallel Duration=".3" Fps="15">
                                    <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                    <FadeOut />
                                </Parallel>
                                
                                <%--  Reset the sample so it can be played again --%>
                                <StyleAction Attribute="display" Value="none"/>
                                <StyleAction Attribute="width" Value="250px"/>
                                <StyleAction Attribute="height" Value=""/>
                                <StyleAction Attribute="fontSize" Value="12px"/>
                                <OpacityAction AnimationTarget="btnCloseParent" Opacity="0" />
                                
                                <%--  Enable the button so it can be played again --%>
                                <EnableAction AnimationTarget="btnHelp" Enabled="true" />
                            </Sequence>
                        </OnClick>
                        <OnMouseOver>
                            <Color Duration=".2" PropertyKey="color" StartValue="#FFFFFF" EndValue="#FF0000" />
                        </OnMouseOver>
                        <OnMouseOut>
                            <Color Duration=".2" PropertyKey="color" StartValue="#FF0000" EndValue="#FFFFFF" />
                        </OnMouseOut>
                        </Animations>
                    </ajaxToolKit:AnimationExtender>
                    <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
                </td>
            </tr>
        </table>
        <fieldset class="fieldset">
            <legend class="legend">Search Student</legend>
            <table cellpadding="0" cellspacing="0" width="100%">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <tr>
                            <td>Roll. No :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtRollNo" runat="server" ValidationGroup="submit"></asp:TextBox>
                                &nbsp;
                        <ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" TargetControlID="txtRollNo"
                            WatermarkText="Enter Roll No" WatermarkCssClass="watermarked" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtTempno" runat="server" TargetControlID="txtRollNo"
                                    FilterType="Numbers" />
                                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="15%" OnClick="btnSearch_Click"
                                    ValidationGroup="submit" />

                                &nbsp;<asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtRollNo"
                                    Display="None" ErrorMessage="Please Enter Roll no." ValidationGroup="submit"></asp:RequiredFieldValidator><asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                        ShowMessageBox="True" Width="20%" ShowSummary="False" ValidationGroup="submit" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </table>
        </fieldset>
        <table id="tblStudent" width="100%" style="text-align: left" runat="server">
            <tr>
                <td></td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="2" cellspacing="2" width="100%" class="section_header">
                        <tr>
                            <td align="left">Student Information
                            </td>
                            <td align="right">
                                <img id="img1" style="cursor: pointer;" alt="" src="../IMAGES/collapse_blue.jpg"
                                    onclick="javascript:toggleExpansion(this,'divStudentInfo')" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td>
                    <div id="divStudentInfo" style="display: block;">
                        <table cellpadding="1" cellspacing="1" width="100%" border="0">
                            <tr>
                                <td width="15%">Temp. No :
                                </td>
                                <td width="35%">
                                    <asp:Label ID="lblTempNo" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="15%">Branch :
                                </td>
                                <td width="35%">
                                    <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="15%">Enrollment No:</td>
                                <td width="35%">
                                    <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="15%">Semester :</td>
                                <td width="35%">
                                    <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="15%">Student Name :</td>
                                <td width="35%">
                                    <asp:Label ID="lblName" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="15%">Degree :</td>
                                <td width="35%">
                                    <asp:Label ID="lblDegree" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>

                            <tr>
                                <td width="15%" valign="top">Father's Name :
                                </td>
                                <td width="35%" valign="top">
                                    <asp:Label ID="lblMName" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="15%" valign="top">Payment Type</td>
                                <td width="35%">
                                    <asp:Label ID="lblPaymenttype" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="15%">Date of Birth :
                                </td>
                                <td width="35%">
                                    <asp:Label ID="lblDOB" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="15%">Admission
                                    Category : </td>
                                <td width="35%">
                                    <asp:Label ID="lblCategory" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td width="15%">Religion :
                                </td>
                                <td width="30%">
                                    <asp:Label ID="lblReligion" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="15%">Nationality :
                                </td>
                                <td width="35%">
                                    <asp:Label ID="lblNationality" runat="server" Text='<%# Eval("") %>' Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="15%">Postal&nbsp; Address :
                                </td>
                                <td width="35%">
                                    <asp:Label ID="lblLAdd" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="15%">City :
                                </td>
                                <td width="35%">
                                    <asp:Label ID="lblCity" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="15%">Landline No :
                                </td>
                                <td width="35%">
                                    <asp:Label ID="lblLLNo" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="15%">Mobile No :
                                </td>
                                <td width="35%">
                                    <asp:Label ID="lblMobNo" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="15%">Permanent Address :&nbsp;
                                </td>
                                <td width="35%">
                                    <asp:Label ID="lblPAdd" runat="server" Font-Bold="True"></asp:Label>
                                    &nbsp;
                                </td>
                                <td width="15%">City :&nbsp;
                                </td>
                                <td width="35%">
                                    <asp:Label ID="lblPCity" runat="server" Font-Bold="True"></asp:Label>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr id="trPayStatus" visible="false" runat="server">
                                <td width="15%">Payment Status :</td>
                                <td width="35%">
                                    <asp:RadioButtonList ID="rblPaymentStatus" runat="server"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="1">YES</asp:ListItem>
                                        <asp:ListItem Value="0">NO</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td width="15%">&nbsp;</td>
                                <td width="35%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td width="15%">&nbsp;</td>
                                <td width="35%">&nbsp;</td>
                                <td width="15%">&nbsp;</td>
                                <td width="35%">&nbsp;</td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr id="trStatus" runat="server" visible="true">
                <td style="font-size: medium; color: Red">
                    <fieldset class="fieldset" style="background-color: #B9FFB9">
                        <table width="100%">
                            <tr>
                                <td align="center">

                                    <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal"
                                        Font-Bold="true" AutoPostBack="True"
                                        OnSelectedIndexChanged="rblStatus_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="1">Provisional Admission Confirm</asp:ListItem>
                                        <asp:ListItem Value="0">Provisional Admission Cancel</asp:ListItem>
                                    </asp:RadioButtonList>

                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr id="trMsg" runat="server" visible="false">
                <td style="font-size: medium; color: Red">
                    <fieldset class="fieldset" style="background-color: #B9FFB9">
                        <table width="100%">
                            <tr>
                                <td align="center" style="font-size: medium">Please Click the Submit button Automaticaly Reconcile the Student.                                 
                                </td>
                            </tr>
                        </table>
                    </fieldset>

                </td>
            </tr>
            <tr>
                <td>
                    <div id="divDDDetails" runat="server" style="display: none">
                        <fieldset class="fieldset">
                            <legend>Demand Draft Details</legend>
                            <table>
                                <tr>
                                    <td>DD/Check No.:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDDNo" runat="server" TabIndex="6" CssClass="data_label" />
                                        <asp:RequiredFieldValidator ID="valDDNo" ControlToValidate="txtDDNo" runat="server"
                                            Display="None" ErrorMessage="Please enter demand draft number." ValidationGroup="dd_info" />
                                    </td>
                                    <td>Amount:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDDAmount" onkeyup="IsNumeric(this);" runat="server" TabIndex="7"
                                            CssClass="data_label" />
                                        <asp:RequiredFieldValidator ID="valDdAmount" ControlToValidate="txtDDAmount" runat="server"
                                            Display="None" ErrorMessage="Please enter amount of demand draft." ValidationGroup="dd_info" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Date:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDDDate" runat="server" TabIndex="8" CssClass="data_label" />
                                        <asp:Image ID="imgCalDDDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />
                                        <ajaxToolKit:CalendarExtender ID="ceDDDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDDDate"
                                            PopupButtonID="imgCalDDDate" />
                                        <ajaxToolKit:MaskedEditExtender ID="meeDDDate" runat="server" TargetControlID="txtDDDate"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="true"
                                            OnInvalidCssClass="errordate" />
                                        <ajaxToolKit:MaskedEditValidator ID="mevDDDate" runat="server" ControlExtender="meeDDDate"
                                            ControlToValidate="txtDDDate" IsValidEmpty="False" EmptyValueMessage="Demand draft date is required"
                                            InvalidValueMessage="Demand draft date is invalid" EmptyValueBlurredText="*"
                                            InvalidValueBlurredMessage="*" Display="Dynamic" ValidationGroup="dd_info" />
                                    </td>
                                    <td>City:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDDCity" runat="server" TabIndex="9" CssClass="data_label" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Bank:
                                    </td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="ddlBank" AppendDataBoundItems="true" TabIndex="10" runat="server"
                                            Width="250px" />
                                        <asp:RequiredFieldValidator ID="valBankName" runat="server" ControlToValidate="ddlBank"
                                            Display="None" ErrorMessage="Please select bank name." ValidationGroup="dd_info"
                                            InitialValue="0" SetFocusOnError="true" />
                                    </td>
                                    <td>
                                        <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="dd_info" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>

                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr id="trPrint" runat="server" visible="True">
                <td align="center">
                    <asp:Button ID="btnPrintChallan" runat="server" Text="Print Challan"
                        Width="15%" OnClick="btnPrintChallan_Click" />
                </td>
            </tr>
            <tr id="trSubmit" runat="server" visible="false">
                <td align="center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                        OnClick="btnSubmit_Click" Width="15%" ValidationGroup="SUBMIT" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ValidationSummary ID="vssubmit" runat="server" DisplayMode="List" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="SUBMIT" />
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divFeePaid" style="display: block;">
                    </div>
                </td>
            </tr>
            <%--  Shrink the info panel out of view --%>
            <tr>
                <td style="height: 20px"></td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr style="display: none">
                <td></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>

        </table>
    </div>

    <script type="text/javascript" language="javascript">

        /* To collapse and expand page sections */
        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../IMAGES/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../IMAGES/collapse_blue.jpg";
            }
        }

        function ValidatePayType(txtPayType) {
            try {
                if (txtPayType != null && txtPayType.value != '') {
                    if (txtPayType.value.toUpperCase() == 'D') {


                        txtPayType.value = "D";
                        if (document.getElementById('<%= divDDDetails.ClientID %>') != null) {
                        document.getElementById('<%= divDDDetails.ClientID %>').style.display = "block";
                        document.getElementById('<%= txtDDNo.ClientID%>').focus();
                    }
                }
                else {
                    if (txtPayType.value.toUpperCase() == 'C') {
                        txtPayType.value = "C";
                        if (document.getElementById('<%= divDDDetails.ClientID %>') != null) {
                            document.getElementById('<%= divDDDetails.ClientID %>').style.display = "none";
                        }
                    }
                    else {
                        alert("Please enter only 'C' for Cash payment OR 'D' for payment through Demand Drafts.");
                        if (document.getElementById('<%= divDDDetails.ClientID %>') != null)
                            document.getElementById('<%= divDDDetails.ClientID %>').style.display = "none";

                        txtPayType.value = "";
                        txtPayType.focus();
                    }
                }
            }
        }
        catch (e) {
            alert("Error: " + e.description);
        }
    }

    function DevideTotalAmount() {
        try {
            var totalAmt = 0;
            if (document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount') != null &&
            document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value.trim() != '')
                totalAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value.trim());

            var dataRows = null;
            if (document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems') != null)
                dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems').getElementsByTagName('tr');

            if (dataRows != null) {
                for (i = 1; i < dataRows.length; i++) {
                    var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                    var dataCell = dataCellCollection.item(2);
                    var controls = dataCell.getElementsByTagName('input');
                    var originalAmt = controls.item(1).value;
                    if (originalAmt.trim() != '')
                        originalAmt = parseFloat(originalAmt);

                    if ((totalAmt - originalAmt) >= originalAmt) {
                        document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_ctrl' + (i - 1) + '_txtFeeItemAmount').value = originalAmt;
                        totalAmt = (totalAmt - originalAmt);
                    }
                    else {
                        if ((totalAmt - originalAmt) >= 0) {
                            document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_ctrl' + (i - 1) + '_txtFeeItemAmount').value = originalAmt;
                            totalAmt = (totalAmt - originalAmt);
                        }
                        else {
                            document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_ctrl' + (i - 1) + '_txtFeeItemAmount').value = totalAmt;
                            totalAmt = 0;
                        }
                    }
                }
            }
            UpdateTotalAndBalance();
        }
        catch (e) {
            alert("Error: " + e.description);
        }
    }

    function UpdateTotalAndBalance() {
        try {
            var totalFeeAmt = 0.00;
            var dataRows = null;

            if (document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems') != null)
                dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems').getElementsByTagName('tr');

            if (dataRows != null) {
                for (i = 1; i < dataRows.length; i++) {
                    var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                    var dataCell = dataCellCollection.item(2);
                    var controls = dataCell.getElementsByTagName('input');
                    var txtAmt = controls.item(0).value;
                    if (txtAmt.trim() != '')
                        totalFeeAmt += parseFloat(txtAmt);
                }
                if (document.getElementById('ctl00_ContentPlaceHolder1_txtTotalFeeAmount') != null)
                    document.getElementById('ctl00_ContentPlaceHolder1_txtTotalFeeAmount').value = totalFeeAmt;

                var totalPaidAmt = 0;
                if (document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount') != null && document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value.trim() != '')
                    totalPaidAmt = document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value.trim();

                if (document.getElementById('ctl00_ContentPlaceHolder1_txtFeeBalance') != null)
                    document.getElementById('ctl00_ContentPlaceHolder1_txtFeeBalance').value = (parseFloat(totalPaidAmt) - parseFloat(totalFeeAmt));
            }
            UpdateCash_DD_Amount();
        }
        catch (e) {
            alert("Error: " + e.description);
        }
    }

    function UpdateCash_DD_Amount() {
        try {
            var txtPayType = document.getElementById('ctl00_ContentPlaceHolder1_txtPayType');
            var txtPaidAmt = document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount');

            if (txtPayType != null && txtPaidAmt != null) {
                if (txtPayType.value.trim() == "C" && document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt') != null) {
                    document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt').value = txtPaidAmt.value.trim();
                }
                else if (txtPayType.value.trim() == "D" && document.getElementById('tblDD_Details') != null) {
                    var totalDDAmt = 0.00;
                    var dataRows = document.getElementById('tblDD_Details').getElementsByTagName('tr');
                    if (dataRows != null) {
                        for (i = 1; i < dataRows.length; i++) {
                            var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                            var dataCell = dataCellCollection.item(6);
                            if (dataCell != null) {
                                var txtAmt = dataCell.innerHTML.trim();
                                totalDDAmt += parseFloat(txtAmt);
                            }
                        }
                        if (document.getElementById('ctl00_ContentPlaceHolder1_txtTotalDDAmount') != null &&
                        document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt') != null) {
                            document.getElementById('ctl00_ContentPlaceHolder1_txtTotalDDAmount').value = totalDDAmt;
                            document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt').value = parseFloat(txtPaidAmt.value.trim()) - parseFloat(totalDDAmt);
                        }
                    }
                }
            }
        }
        catch (e) {
            alert("Error: " + e.description);
        }
    }


    function ShowHideDivPaidReceipts(btnControl, divId) {
        if (btnControl != null && divId != '') {
            if (document.getElementById(divId) != null && document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                btnControl.value = "Hide Paid Receipts Information";
            }
            else if (document.getElementById(divId) != null && document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                btnControl.value = "Show Paid Receipts Information";
            }
        }
    }

    function IsNumeric(textbox) {
        if (textbox != null && textbox.value != "") {
            if (isNaN(textbox.value)) {
                document.getElementById(textbox.id).value = '';
            }
        }
    }

    function ValidateNumeric(txt) {
        if (isNaN(txt.value)) {
            txt.value = '';
            alert('Only Numeric Characters are Allowed.');
            txt.focus();
            return;
        }
    }
    </script>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>

