<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="StudentApprovalStatus.aspx.cs" Inherits="ACADEMIC_StudentApprovalStatus" Title="" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function ConfirmNoti(evt) {
            if ($(evt).is(":checked")) {
                var ret = confirm("SMS and Email notifications will be sent. Do you want to proceed?");
                if (ret == true) {
                    CallServer();
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                ////alert("Bye");
                //CallServer();
                //return false;
                var ret1 = confirm("Do you want to change the Approval Status?");
                if (ret1 == true) {
                    CallServer();
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        function ConfirmRej(evt) {
            if ($(evt).is(":checked")) {
                var ret = confirm("SMS and Email notifications will be sent. Do you want to proceed?");
                if (ret == true) {
                    CallServer();
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                ////alert("Bye");
                //CallServer();
                //return false;
                var ret1 = confirm("Do you want to Reject this Application?");
                if (ret1 == true) {
                    CallServer();
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        function CallServer() {
            __doPostBack('', '');
        }
    </script>

    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnl"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">STUDENT APPROVAL STATUS</h3>
                        </div>

                        <div style="color: Red; font-weight: bold">
                            <%-- &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory--%>
                        </div>

                        <div class="box-body" runat="server">
                            <div class="form-group col-md-3" visible="false" runat="server">
                                <label><span style="color: red;">&nbsp;</span> Admission Batch</label>
                                <asp:DropDownList ID="ddlAdmBach" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlAdmBach_SelectedIndexChanged" AutoPostBack="true" TabIndex="1" CssClass="form-control" placeholder="Please select admission batch.">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvAdmbatch" runat="server" ControlToValidate="ddlAdmBach" Display="None" ErrorMessage="Please Select Session." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-md-12">
                                <label><span style="color: red;">&nbsp;</span> Admission Type</label>
                                <asp:RadioButtonList ID="rblAdmType" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblAdmType_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0" Selected="True">All &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="1">Consortium (Management)&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="2">Tamil Nadu Engineering Admissions(TNEA/TANCET)&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="3">Other &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="4">Ph.D.</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>

                        <div class="box-footer">
                            <p class="text-center">
                            </p>
                            <div class="col-md-12">
                                <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl"></asp:Label>
                            </div>
                            <div class="col-md-12" id="dvCourse" runat="server" visible="true">

                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="400px">
                                    <label>
                                        <h4>Student Approve list</h4>
                                    </label>
                                    <asp:GridView ID="lvStudApproveList" runat="server" CssClass="table table-hover table-striped" AutoGenerateColumns="False" Visible="false" EmptyDataText="No Record Found" Width="1200px">
                                        <Columns>
                                            <asp:TemplateField AccessibleHeaderText="Edit" HeaderText="Ua. No. " ControlStyle-BackColor="#2A4563" Visible="false">
                                                <ItemTemplate>
                                                    <%#Eval("UA_NO")%>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SN." ControlStyle-BackColor="#2A4563">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1%>
                                                </ItemTemplate>
                                                <ItemStyle />
                                                <ItemStyle Width="3%" Height="47px" VerticalAlign="Middle" />

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" ControlStyle-BackColor="#2A4563">
                                                <ItemTemplate>
                                                    <center>    
                                                     <asp:CheckBox ID="chkReject" runat="server" tooltip='<%# Eval("UA_NO") %>' Checked='<%# ((Int32) Eval("CANCEL") ==1)?true:false %>' Enabled='<%# ((Int32) Eval("UA_DEC") ==1)?true:false %>' CommandName='<%#Eval("UA_DEC")%>' AutoPostBack="True" OnCheckedChanged="chkReject_CheckedChanged" OnClick="return ConfirmRej();"/>
                                                      <ajaxToolkit:ToggleButtonExtender ID="ToggButtonchkReject" runat="server" TargetControlID="chkReject" ImageWidth="50" ImageHeight="35"
                                                        CheckedImageAlternateText='<%#((Int32)Eval("UA_DEC")==1)?"Click to Reject":"Already approved"%>' CheckedImageUrl="~/Images/reject.png" 
                                                        UncheckedImageAlternateText='<%#((Int32)Eval("UA_DEC")==1)?"Click to Reject":"Already approved"%>' UncheckedImageUrl="~/Images/reject.png"/>
                                                         </center>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" Height="47px" VerticalAlign="Middle" />

                                            </asp:TemplateField>
                                            <%--   <asp:TemplateField HeaderText="User Name" ControlStyle-BackColor="#2A4563">
                                                <ItemTemplate>
                                                    <%# Eval("USER_NAME")%>
                                                </ItemTemplate>
                                        <ItemStyle Width="10%" Height="47px" VerticalAlign="Middle" />

                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Student Name" ControlStyle-BackColor="#2A4563">
                                                <ItemTemplate>
                                                    <%# Eval("STUDNAME")%>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" Height="47px" VerticalAlign="Middle" />

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reg. Date" ControlStyle-BackColor="#2A4563">
                                                <ItemTemplate>
                                                    <%# Eval("REG_DT")%>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" Height="47px" VerticalAlign="Middle" />

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Contact">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblContact" Text='<%# Eval("MOBILE")%>' runat="server" ToolTip='<%# Eval("EMAIL_ID")%>'></asp:Label>
                                                    <asp:HiddenField ID="hdfEmailId" runat="server" Value='<%#Eval("EMAIL_ID")%>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" Height="47px" VerticalAlign="Middle" />

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Aprv. Date" ControlStyle-BackColor="#2A4563">
                                                <ItemTemplate>
                                                    <%# Eval("APRV_DATE")%>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" Height="47px" VerticalAlign="Middle" />

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Approved by" ControlStyle-BackColor="#2A4563">
                                                <ItemTemplate>
                                                    <%# Eval("APRV_BY")%>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" Height="47px" VerticalAlign="Middle" />

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Adm. Type" ControlStyle-BackColor="#2A4563">
                                                <ItemTemplate>
                                                    <%# Eval("ADMTYPE")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CATNO" ControlStyle-BackColor="#2A4563">
                                                <ItemTemplate>
                                                    <%# Eval("CATNO")%>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" Height="47px" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Status" ControlStyle-BackColor="#2A4563">
                                                <ItemTemplate>
                                                    <center>

                                                      <asp:CheckBox ID="chkAllow" runat="server" tooltip='<%# Eval("UA_NO") %>' Checked='<%# ((Int32) Eval("UA_DEC") ==1)?false:true %>' CommandName='<%#Eval("UA_DEC")%>' AutoPostBack="True" OnCheckedChanged="chkAllow_CheckedChanged" OnClick="return ConfirmNoti();"/>
                                                                                                        
                                                     <ajaxToolkit:ToggleButtonExtender ID="ToggleEx" runat="server" TargetControlID="chkAllow" ImageWidth="70" ImageHeight="30"
                                                        CheckedImageAlternateText='<%#((Int32)Eval("UA_DEC")==1)?"UnCheck":"Check"%>' CheckedImageUrl="~/Images/ON2.png" 
                                                        UncheckedImageAlternateText="UnCheck" UncheckedImageUrl="~/Images/OFF1.png"/>
                                                    </center>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" Height="47px" VerticalAlign="Middle" />

                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Document">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkdocDownload" runat="server" OnClick="lnkdocDownload_Click" Text="Download" Visible='<%# ((Int32)(Eval("FILEPATH").ToString().Trim().Length)==0)?false:true %>' CommandArgument='<%# Eval("UA_NO") %>' ToolTip='<%# Eval("DOC") %>'>
                                                    </asp:LinkButton>
                                                    <asp:HiddenField ID="hdfFilename" runat="server" Value='<%#Eval("DOC") %> ' />
                                                    <asp:HiddenField ID="hdfFilePath" runat="server" Value='<%#Eval("FILEPATH") %> ' />
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" Height="47px" VerticalAlign="Middle" />

                                            </asp:TemplateField>

                                        </Columns>
                                        <HeaderStyle CssClass="bg-light-blue" />
                                    </asp:GridView>
                                </asp:Panel>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lvStudApproveList" />
        </Triggers>
    </asp:UpdatePanel>


    <div runat="server" id="divMsg">
    </div>
</asp:Content>
