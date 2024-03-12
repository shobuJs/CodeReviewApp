<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage2.master" AutoEventWireup="true"
    CodeFile="Srcategory.aspx.cs" Inherits="ACADEMIC_MASTERS_Srcategory"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--    <script type="text/javascript" language="javascript" src="../../Javascripts/FeeCollection.js"></script>
        <script type="text/javascript" language="javascript" src="../../includes/prototype.js"></script>
        <script type="text/javascript" language="javascript" src="../../includes/scriptaculous.js"></script>
        <script type="text/javascript" language="javascript" src="../../includes/modalbox.js"></script>--%>

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
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Admission Intake Category Wise</h3>
                                <div class="box-tools pull-right">
                                </div>
                            </div>
                            <br />
                            <div style="color: Red; font-weight: bold; margin-top: -20px; margin-right: -5px;">
                                &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                            </div>
                            <div class="box-body">
                                <div class="row">

                                    <div class="col-md-12">
                                        <div class="form-group col-md-4">
                                            <span style="color: red">*</span>
                                            <label>Degree</label>
                                            <asp:DropDownList ID="ddldegree" runat="server" TabIndex="1" CssClass="form-control"
                                                AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddldegree_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvdegree" runat="server" ControlToValidate="ddldegree"
                                                ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Degree." InitialValue="0" />
                                        </div>
                                        <div class="form-group col-md-4">
                                            <span style="color: red">*</span>
                                            <label>SR Category</label>
                                            <asp:DropDownList ID="ddlsrcategory" runat="server" TabIndex="2" CssClass="form-control"
                                                AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlsrcategory_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvsrcatgry" runat="server" ControlToValidate="ddlsrcategory"
                                                ValidationGroup="Submit" Display="None" ErrorMessage="Please Select SR Category."
                                                InitialValue="0" />
                                        </div>
                                        <div class="form-group col-md-4">
                                            <span style="color: red">*</span><label>College Code</label>
                                            <asp:DropDownList ID="ddlcollegecode" runat="server" TabIndex="3" CssClass="form-control"
                                                AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlcollegecode_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">E021</asp:ListItem>
                                                <asp:ListItem Value="2">E057</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvcollegecode" runat="server" ControlToValidate="ddlcollegecode"
                                                ValidationGroup="Submit" Display="None" ErrorMessage="Please Select College Code."
                                                InitialValue="0" />
                                        </div>
                                    </div>
                                    <br />
                                    &nbsp;
                                    <div class="row text-center">

                                        <asp:Button ID="btnshow" runat="server" TabIndex="4" CssClass="btn btn-outline-info" ValidationGroup="Submit" Text="Show" OnClick="btnshow_Click" />
                                        <asp:Button ID="btnsubmit" runat="server" ValidationGroup="Submit" TabIndex="5" Enabled="false" CssClass="btn btn-outline-info" Text="Submit" OnClick="btnsubmit_Click" />
                                        <asp:Button ID="btnclear" runat="server" TabIndex="6" CssClass="btn btn-danger" Text="Cancel" OnClick="btnclear_Click" />
                                        <asp:ValidationSummary ID="validationsummary1" runat="server" ValidationGroup="Submit" EnableTheming="true"
                                            ShowMessageBox="true" ShowSummary="false" />

                                    </div>


                                    <div class="row" id="trListview" runat="server">
                                        <div class="col-md-12">
                                            <div class="container-fluid">
                                                <asp:ListView ID="lvcategorylist" runat="server">
                                                    <LayoutTemplate>


                                                        <h4>List of Branches:</h4>

                                                        <table id="tblSearchResults" class="table table-hovered table-bordered">
                                                            <tr class="bg-light-blue">
                                                                <th style="width: 15px">Sr.No
                                                                </th>
                                                                <th>Branch Name
                                                                </th>
                                                                <th>Intake
                                                                </th>

                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </table>
                                                    </LayoutTemplate>
                                                    <EmptyDataTemplate>
                                                    </EmptyDataTemplate>
                                                    <ItemTemplate>
                                                        <%-- DO NOT FORMAT FOLLOWING 5-6 LINES. JAVASCRIPT IS DEPENDENT ON ELEMENT HIERARCHY --%>
                                                        <tr class="item">
                                                            <td>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("LONGNAME")%>
                                                                <asp:HiddenField ID="hdncat" runat="server" Value='<%# Eval("INTAKEID")%>' />
                                                                <asp:HiddenField ID="hdnbranch" runat="server" Value='<%# Eval("BRANCHNO")%>' />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtintake" Width="70px" Text='<%# Eval("INTAKE")%>' runat="server" MaxLength="5" onkeyup="chk(this);"></asp:TextBox>
                                                            </td>

                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>

            <script type="text/javascript">
                function chk(txt) {
                    if (isNaN(txt.value)) {
                        txt.value = '';
                        alert('Enter Numbers Only!');
                        txt.focus();
                        return;
                    }
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
