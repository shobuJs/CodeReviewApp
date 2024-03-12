<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="SessionWiseOfferedCoure.aspx.cs" Inherits="ACADEMIC_SessionWiseOfferedCoure" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updpnl"
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
                <!--academic Calendar-->
                <div class="col-md-12">

                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Offered Courses</h3>

                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <form role="form">
                            <div class="box-body">
                                <div class="col-md-12">
                                    <div class="form-group col-md-3">
                                        <label for="city">Session</label>
                                        <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="Report"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSession"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="offered"></asp:RequiredFieldValidator>

                                    </div>

                                    <div class="form-group col-md-3">
                                        <label for="city">Offered To Semester</label>
                                        <asp:DropDownList ID="ddlToTerm" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlToTerm_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlToTerm"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select  Offered To Term" ValidationGroup="Report"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlToTerm"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select  Offered To Term" ValidationGroup="offered"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-md-3">
                                        <label for="city">Degree</label>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                            AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" TabIndex="5">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="offered"></asp:RequiredFieldValidator>



                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-md-3">
                                        <label for="city">Branch</label>
                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                            AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="offered"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="Report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-md-3">
                                        <label for="city">Scheme</label>
                                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                            AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="offered"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-md-3">
                                        <label for="city">Semester</label>
                                        <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="offered"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <!-- /.box-body -->

                            <div class="box-footer">
                                <p class="text-center">
                                    <asp:LinkButton ID="btnAd" runat="server" Text="Submit" ValidationGroup="offered" OnClick="btnAd_Click" CssClass="btn btn-outline-info">
                        <i class="fa fa-save"> Submit</i>
                                    </asp:LinkButton>
                                    <asp:Button ID="btnCancel0" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" />
                                    <asp:LinkButton ID="btnPrint" runat="server" ValidationGroup="Report" OnClick="btnPrint_Click" CssClass="btn btn-outline-primary"><i class="fa fa-file"></i> Report</asp:LinkButton>

                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="offered"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Report"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </p>

                                <div class=" col-md-12">
                                    <asp:Panel ID="pnlCourse" runat="server" ScrollBars="Vertical" Height="400px" Width="100%"
                                        Visible="false">
                                        <asp:ListView ID="lvCourse" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <div>
                                                        <h3>
                                                            <label class="label label-default">Courses</label></h3>
                                                    </div>
                                                    <table id="example1" class="table table-bordered table-hover table-striped">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>Code
                                                                </th>
                                                                <th>CourseName
                                                                </th>
                                                                <th>Credits
                                                                </th>
                                                                <th>Offered
                                                                </th>
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
                                                        <%# Eval("CCODE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("COURSE_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("CREDITS") %>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkoffered" runat="server" ToolTip='<%# Eval("OFFERED") %>' Checked='<%#(Convert.ToInt32( Eval("OFFERED"))==1?true:false )%>' />
                                                        <asp:HiddenField ID="hf_course" runat="server" Value='<%# Eval("COURSENO") %>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
                <!--academic Calendar-->
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPrint" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server"></div>
    <script type="text/javascript" lang="javascript">
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

</asp:Content>
