<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CollegeSchemeConfig.aspx.cs" Inherits="ACADEMIC_CollegeSchemeConfig" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        table.cbl tr td label {
            margin-left: 5px;
            margin-right: 20px;
        }
        table.cbl tr td {
            padding-bottom: 10px;
        }
    </style>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPnl"
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

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title"><span>College Regulation Configuration</span></h3>
                </div>

                <asp:UpdatePanel ID="updPnl" runat="server">
                    <ContentTemplate>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label> College</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" />
                                        <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege" ValidationGroup="submit" Display="None"
                                            ErrorMessage="Please Select College" InitialValue="0" SetFocusOnError="true" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divDegree" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <div class="form-group col-md-12 checkbox-list-box">
                                            <asp:CheckBoxList ID="cblDegree" runat="server" AppendDataBoundItems="true" RepeatDirection="Horizontal" CssClass="checkbox-list-style"
                                                OnSelectedIndexChanged="cblDegree_SelectedIndexChanged" AutoPostBack="true" RepeatColumns="1">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divBranch" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch</label>
                                        </div>
                                        <div class="form-group col-md-12 checkbox-list-box">
                                            <asp:CheckBoxList ID="cblBranch" runat="server" AppendDataBoundItems="true" RepeatDirection="Horizontal" CssClass="checkbox-list-style"
                                                OnSelectedIndexChanged="cblBranch_SelectedIndexChanged" CellPadding="5" RepeatLayout="Table" RepeatColumns="1"
                                                AutoPostBack="true">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divScheme" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Regulation</label>
                                        </div>
                                        <div class="form-group col-md-12 checkbox-list-box">
                                            <asp:CheckBoxList ID="cblScheme" runat="server" AppendDataBoundItems="true" RepeatDirection="Horizontal" CssClass="checkbox-list-style"
                                                OnSelectedIndexChanged="cblScheme_SelectedIndexChanged" CellPadding="5" RepeatLayout="Table" RepeatColumns="1"
                                                AutoPostBack="true">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                       

                            <div class="col-12 btn-footer mt-3">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="submit" Enabled="false" CssClass="btn btn-outline-info" OnClientClick="return UserConfirmation();" />
                                <asp:Button ID="btnCancel" runat="server" Text="Clear" OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" />
                                
                                <asp:ValidationSummary ID="vsReport" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit" />
                                <div id="divMsg" runat="server"></div>
                          
                            </div>
                         </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <script type="text/javascript" language="javascript">
        function UserConfirmation() {
            if (confirm("Are you sure you want to Submit?"))
                return true;
            else
                return false;
        }
    </script>

</asp:Content>
