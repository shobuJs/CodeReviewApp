<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CertificateApproveRequest.aspx.cs" Inherits="CertificateApproveRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updRequest"
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
    <asp:UpdatePanel ID="updRequest" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div id="Tabs" role="tabpanel">
                            <div class="box-body">
                                <div class="nav-tabs-custom">
                                    <ul class="nav nav-tabs" role="tablist">
                                        <li class="nav-item">
                                            <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Document Request</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="1">Track Request</a>
                                        </li>
                                    </ul>

                                    <div class="tab-content" id="my-tab-content">
                                        <div class="tab-pane active" id="tab_1">
                                            <div>
                                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updRequest"
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
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-12 mt-3">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <asp:Label ID="lblFaculty" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlFaculty" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <asp:Label ID="lblDyProgram" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlProgram" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <asp:Label ID="lblDYDocument" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlDocument" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="4" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDocument_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="form-group col-lg-2 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <asp:Label ID="lblDYStatus" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="5" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    <asp:ListItem Value="1">Approved</asp:ListItem>
                                                                    <asp:ListItem Value="2">Pending</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12 btn-footer" runat="server" visible="false" id="DivButton">
                                                        <asp:LinkButton ID="btnSubmit" runat="server" OnClientClick="javascript:document.forms[0].encoding = 'multipart/form-data';" CssClass="btn btn-outline-info" TabIndex="6" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                                        <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" TabIndex="7" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                                    </div>

                                                    <div class="col-12">
                                                        <asp:Panel ID="Panel2" runat="server">
                                                            <asp:ListView ID="lvDocument" runat="server" Visible="true">
                                                                <LayoutTemplate>
                                                                    <div class="table-responsive" style="height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%;">
                                                                            <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                                <tr>
                                                                                    <th>
                                                                                        <asp:CheckBox ID="chkhead" runat="server" onclick="return totAll(this);" /></th>
                                                                                    <th>Request ID</th>
                                                                                    <th>Student Name</th>
                                                                                    <th>Document Name</th>
                                                                                    <th>Amount</th>
                                                                                    <th>Print</th>
                                                                                    <th>Upload</th>
                                                                                    <th>Email</th>
                                                                                    <th>Issue Mode</th>
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
                                                                            <asp:CheckBox ID="Chk" runat="server" Checked='<%#Eval("DOC_NAME").ToString() == "" ? false : true  %>' Enabled='<%#Eval("DOC_NAME").ToString() == "" ? true : false %>' /></td>
                                                                        <td><%#Eval("REQUEST_ID") %>
                                                                            <asp:Label runat="server" ID="lblIdno" Text='<%#Eval("IDNO") %>' Visible="false"></asp:Label>
                                                                        </td>
                                                                        <td><%#Eval("NAME") %></td>
                                                                        <td><%#Eval("DOCNAME") %></td>
                                                                        <td><%#Eval("FEES") %></td>
                                                                        <td>
                                                                            <asp:LinkButton ID="btnGenerate" runat="server" CssClass="btn btn-outline-info btn-sm" OnClientClick="javascript:document.forms[0].encoding = 'multipart/form-data';" OnClick="btnGenerate_Click" CommandArgument='<%#Eval("IDNO") %>' CommandName='<%#Eval("DOC_NO") %>' ToolTip='<%#Eval("SEMESTERNO") %>'>Generate</asp:LinkButton></td>

                                                                        <td>
                                                                            <asp:FileUpload ID="FuDocument" runat="server" onchange="setUploadButtonState(this);" CssClass="DemandAmount" Style="margin-top: 8px;" TabIndex="7" /><br />
                                                                            <%--<asp:LinkButton ID="btnUpload" runat="server" OnClientClick="javascript:document.forms[0].encoding = 'multipart/form-data';" CommandArgument='<%#Eval("IDNO") %>'> <i class="fa fa-upload" aria-hidden="true" style="color: #28a745; font-size: 20px;"></i></asp:LinkButton>--%></td>
                                                                        <td>
                                                                            <asp:LinkButton ID="btnEmail" runat="server" CommandArgument='<%#Eval("IDNO") %>' CssClass="btn btn-outline-info btn-sm" OnClick="btnEmail_Click" CommandName='<%#Eval("DOC_NAME") %>' ToolTip='<%#Eval("NAME") %>' Visible='<%#Eval("DOC_NAME").ToString() == "" ? false : true %>'>Email</asp:LinkButton></td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlIssueMode" runat="server" CssClass="form-control" data-select2-enable="true">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                <asp:ListItem Value="1">Email</asp:ListItem>
                                                                                <asp:ListItem Value="2">Physical Handover</asp:ListItem>
                                                                                <asp:ListItem Value="3">Download From The Portal</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:Label runat="server" ID="lblIssue" Text='<%#Eval("ISSUEMODE") %>' Visible="false"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>

                                                        </asp:Panel>

                                                    </div>


                                                    <div id="myModal22" class="modal fade" role="dialog">
                                                        <div class="modal-dialog modal-lg">
                                                            <div class="modal-content" style="margin-top: -25px">
                                                                <div class="modal-body">
                                                                    <div class="modal-header">
                                                                        <h4 class="modal-title" style="font: bold">Document</h4>
                                                                        <button type="button" class="close" data-dismiss="modal" style="margin-top: -18px">x</button>
                                                                    </div>
                                                                    <asp:Image ID="ImageViewer" runat="server" Width="100%" Height="500px" />
                                                                    <asp:Literal ID="ltEmbed" runat="server" />
                                                                    <div id="imageViewerContainer" runat="server"></div>
                                                                    <asp:HiddenField ID="hdfImagePath" runat="server" />

                                                                    <div class="modal-footer" style="height: 0px">
                                                                        <button type="button" style="margin-top: -10px" class="btn btn-default" data-dismiss="modal">Close</button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>

                                                    <%--  <asp:PostBackTrigger ControlID="lvDocument" />--%>
                                                    <asp:PostBackTrigger ControlID="btnSubmit" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="tab-pane fade" id="tab_2">
                                            <div>
                                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updRequest"
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
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-12 mt-3">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlFacutlyTrack" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlFacutlyTrack_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlFacutlyTrack"
                                                                    Display="None" ErrorMessage="Please Select Faculty" InitialValue="0" ValidationGroup="Submit" />
                                                            </div>
                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <asp:Label ID="lblSAProgram" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlPragramTrack" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3" AppendDataBoundItems="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPragramTrack"
                                                                    Display="None" ErrorMessage="Please Select Program" InitialValue="0" ValidationGroup="Submit" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <asp:Label ID="lblDocument" runat="server" Font-Bold="true">Document</asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlDocumentTrack" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="4" AppendDataBoundItems="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDocumentTrack"
                                                                    Display="None" ErrorMessage="Please Select Document" InitialValue="0" ValidationGroup="Submit" />
                                                            </div>
                                                            <div class="form-group col-lg-2 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <asp:Label ID="lblStatus" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlStatusTrack" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="5" AppendDataBoundItems="true">
                                                                    <%--   <asp:ListItem Value="0">Please Select</asp:ListItem>--%>
                                                                    <asp:ListItem Selected="True" Value="1">Approved</asp:ListItem>
                                                                    <%-- <asp:ListItem Value="2">Pending</asp:ListItem>--%>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:LinkButton ID="btnShow" runat="server" CssClass="btn btn-outline-info" TabIndex="6" ValidationGroup="Submit" OnClick="btnShow_Click">Show</asp:LinkButton>
                                                        <asp:LinkButton ID="btnCancel2" runat="server" CssClass="btn btn-outline-danger" TabIndex="7" OnClick="btnCancel2_Click">Cancel</asp:LinkButton>
                                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Submit"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                    </div>

                                                    <div class="col-12">
                                                        <asp:Panel ID="Panel1" runat="server">
                                                            <asp:ListView ID="lvtrackRequest" runat="server" Visible="true">
                                                                <LayoutTemplate>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Request ID</th>
                                                                                <th>Name</th>
                                                                                <th>Request Type</th>
                                                                                <th>Request Date</th>
                                                                                <th>Email on</th>
                                                                                <th>Available for Downloading</th>
                                                                                <th>Officer Name</th>
                                                                                <%--   <th>Hard Copy Issued on</th>--%>
                                                                                <th>Issued By</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td><%#Eval("REQUEST_ID") %></td>
                                                                        <td><%#Eval("NAME") %></td>
                                                                        <td><%#Eval("DOCNAME") %></td>
                                                                        <td><%#Eval("REQUEST_DATE") %></td>

                                                                        <td><%#Eval("EMAILDATE") %> </td>
                                                                        <td><%#Eval("DOWNLOADING_DATE") %></td>
                                                                        <td><%#Eval("UA_FULLNAME") %></td>
                                                                        <td><%#Eval("ISSUE") %></td>

                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>

                                                        </asp:Panel>

                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
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
    <script>
        function TabShow() {
            var tabName = "tab_2";
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
        }
    </script>
</asp:Content>

