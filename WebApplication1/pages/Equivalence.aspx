<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Equivalence.aspx.cs" Inherits="ACADEMIC_Equivalence" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title"><span>Subject Equivalence</span></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College/School Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" ValidationGroup="Summary" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"
                                            CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College/School Name." InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" ValidationGroup="Summary"
                                            CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session." InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Univ. Reg. No./ TAN/PAN</label>
                                        </div>
                                        <span class="form-inline">
                                            <asp:TextBox runat="server" ID="txtRegno" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvRegno" runat="server" ControlToValidate="txtRegno"
                                                Display="None" ErrorMessage="Please Enter Univ. Reg. No. / TAN/PAN" SetFocusOnError="True" ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                            
                                            <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="Summary" CssClass="btn btn-outline-info m-top"
                                                OnClick="btnShow_Click" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="Summary" />
                                        </span>
                                    </div>
                                </div>
                            </div>
                                   
                            <asp:Panel runat="server" ID="pnlCourseList" Visible="false">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Old Curriculum</label>
                                            </div>
                                            <asp:DropDownList ID="ddlOldScheme" runat="server" AppendDataBoundItems="True" ValidationGroup="Summary1"
                                                CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlOldScheme_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvoldScheme" runat="server" ControlToValidate="ddlOldScheme"
                                                Display="None" ErrorMessage="Please Select Old Curriculum." InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="Summary1"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Old Subject</label>
                                            </div>
                                            <asp:DropDownList ID="ddlOldCourse" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                ValidationGroup="Summary1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvOldCourse" runat="server" ControlToValidate="ddlOldCourse"
                                                Display="None" ErrorMessage="Please Select Old Subject." InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="Summary1"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>New Curriculum</label>
                                            </div>
                                            <asp:DropDownList ID="ddlNewScheme" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                CssClass="form-control" data-select2-enable="true" ValidationGroup="Summary1" OnSelectedIndexChanged="ddlNewScheme_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvNewScheme" runat="server" ControlToValidate="ddlNewScheme"
                                                Display="None" ErrorMessage="Please Select New Curriculum." InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="Summary1"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>New Subject</label>
                                            </div>
                                            <asp:DropDownList ID="ddlNewCourse" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                ValidationGroup="Summary1" AutoPostBack="true" OnSelectedIndexChanged="ddlNewCourse_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvNewCourse" runat="server" ControlToValidate="ddlNewCourse"
                                                Display="None" ErrorMessage="Please Select New Subject." InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="Summary1"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSave" Text="Save" runat="server" CssClass="btn btn-outline-info" OnClick="btnSave_Click" ValidationGroup="Summary1" />

                                    <asp:Button ID="btnCancel" Text="Cancel" CssClass="btn btn-outline-danger" runat="server" OnClick="btnCancel_Click" />

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Summary1" />
                                </div>

                                <div class="form-group col-lg-12 col-md-12 col-12">
                                     <div class=" note-div">
                                        <h5 class="heading">Note</h5>
                                        <p><i class="fa fa-star" aria-hidden="true"></i>
                                            <span>New Subject automatically registered for student and
                                            selected old Subject get canceled if already registered by student.</span>  
                                        </p>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <asp:Panel ID="pnlCourse" runat="server" Width="100%" ScrollBars="Vertical">
                                        <asp:ListView ID="lvCourse" runat="server">
                                            <LayoutTemplate>
                                                <div id="listViewGrid">
                                                    <table class="table table-striped table-bordered nowrap display" style="width:100%" id="myTable1">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th style="display: none">Edit
                                                                </th>
                                                                <th>Old Subject Code
                                                                </th>
                                                                <th>Old Curriculum
                                                                </th>
                                                                <th>Old Credits
                                                                </th>
                                                                <th>New Subject Code
                                                                </th>
                                                                <th>New Curriculum
                                                                </th>
                                                                <th>New Credits
                                                                </th>
                                                                <th>Registered
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <EmptyDataTemplate>
                                            </EmptyDataTemplate>
                                            <ItemTemplate>
                                                <tr class="item">
                                                    <td style="display: none">
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("EQNO") %>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" Visible='<%#Convert.ToInt32(Eval("CREG_STATUS"))==1?false:true%>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("OLD_CCODE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("OLD_SCHEME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("OLD_CREDITS")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NEW_CCODE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NEW_SCHEME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NEW_CREDITS")%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Convert.ToInt32(Eval("CREG_STATUS"))==1?"REG":"-"%>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </asp:Panel>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
