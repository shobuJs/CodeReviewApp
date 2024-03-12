<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Intake_Lead_Mapping.aspx.cs" Inherits="Projects_Intake_Lead_Mapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

    <div>
        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updIntake"
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
      <asp:UpdatePanel ID="updIntake" runat="server" UpdateMode="Conditional">
         <ContentTemplate>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Intake Lead Mapping</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Intake</label>
                                </div>
                                <asp:DropDownList ID="ddlIntake" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="rfvIntake" runat="server" ControlToValidate="ddlIntake"
                                            Display="None" ErrorMessage="Please Select Intake" InitialValue="0"
                                            ValidationGroup="submit" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Study Level</label>
                                </div>
                                <asp:ListBox ID="lstbxStudyLevel" runat="server" CssClass="form-control multi-select-demo"
                                    AppendDataBoundItems="true" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                           
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Discipline</label>
                                </div>
                                <asp:ListBox ID="lstbxDiscipline" runat="server" CssClass="form-control multi-select-demo"
                                    AppendDataBoundItems="true" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                          
                        </div>
                               
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-primary" OnClick="btnSubmit_Click" ValidationGroup="submit">Submit</asp:LinkButton>
                        <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                        <asp:ValidationSummary ID="vsSubmit" runat="server" ValidationGroup="submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>

                    <div class="col-12">
                        <asp:Panel ID="Panel1" runat="server">
                            <asp:ListView ID="lvIntakeDetail" runat="server">
                                <LayoutTemplate>
                                    <div id="demo-grid">
                                        <div class="sub-heading" id="dem">
                                            <h5>Intake Details List</h5>
                                        </div>

                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable1">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Edit</th>
                                                    <th>Intake</th>
                                                    <th>Study Level</th>
                                                    <th>Discipline</th>
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
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("MAPPING_ID") %>'
                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                        </td>
                                        <td><%# Eval("BATCHNAME")%></td>
                                        <td><%# Eval("STUDYLEVEL")%></td>
                                        <td><%# Eval("DESCIPLINE")%></td>

                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>


                    </div>

                    <%--<div class="col-12 mt-3">
                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                            <thead class="bg-light-blue">
                                <tr>
                                    <th>Edit</th>
                                    <th>Intake</th>
                                    <th>Study Level</th>
                                    <th>Discipline</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td><asp:LinkButton ID="btnJobAnnouncement" class="btnEditX" runat="server" CssClass="fa fa-pencil-square-o" /></td>
                                    <td>22</td>
                                    <td>UG, PG</td>
                                    <td>Engineering, law</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>--%>

                </div>
            </div>
        </div>
    </div>
     </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                });
            });
        });
    </script>

</asp:Content>

