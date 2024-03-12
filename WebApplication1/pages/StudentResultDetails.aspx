<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentResultDetails.aspx.cs" Inherits="ACADEMIC_StudentResultDetails" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnlExam" DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updpnlExam" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary" style="width: 1145px">
                        <div class="box-header with-border">
                            <h3 class="box-title">STUDENT RESULT INFORMATION</h3>
                        </div>

                        <div class="box-body">

                            <div class="panel panel-info text-center" style="width: 1080px; margin-left: 22px">
                                <div class="panel panel-heading" style="height: 40px"><span class="pull-left">Student Information</span></div>
                                <div class="panel panel-body" style="margin-bottom: auto">

                                    <div class="col-md-6 text-center">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item" style="height: 40px">
                                                <b class="pull-left">Student Name :</b>
                                                <a class="pull-right">
                                                    <asp:Label ID="lblStudName" runat="server" Font-Bold="True" />
                                                </a>
                                            </li>
                                            <li class="list-group-item" style="height: 40px">
                                                <b class="pull-left">Univ. Reg. No. :</b>
                                                <a class="pull-right">
                                                    <asp:Label ID="lblRegNo" runat="server" Font-Bold="True" />
                                                </a>
                                            </li>
                                            <li class="list-group-item" style="height: 40px">
                                                <b class="pull-left">Degree :</b>
                                                <a class="pull-right">
                                                    <asp:Label ID="lblDegree" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item" style="height: 40px">
                                                <b class="pull-left">Email :</b>
                                                <a class="pull-right">
                                                    <asp:Label ID="lblEmail" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="col-md-6 text-center">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item" style="height: 40px">
                                                <b class="pull-left">Admission No. :</b>
                                                <a class="pull-right">
                                                    <asp:Label ID="lblEnrollno" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item" style="height: 40px">
                                                <b class="pull-left">Admission Batch :</b>
                                                <a class="pull-right">
                                                    <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item" style="height: 40px">
                                                <b class="pull-left">Branch :</b>
                                                <a class="pull-right">
                                                    <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item" style="height: 40px">
                                                <b class="pull-left">Mobile :</b>
                                                <a class="pull-right">
                                                    <asp:Label ID="lblMobile" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>

                                </div>
                            </div>


                            <div id="divDetails" runat="server" visible="false">
                                <div class="panel" style="width: 1080px; margin-left: 22px;">

                                    <div class="col-md-12">
                                        <span class="pull-left" style="margin-left: -14px">
                                            <h3>
                                                <label class="label label-default">
                                                    Result Details
                                                </label>
                                            </h3>
                                        </span>
                                        <span class="pull-right" style="margin-right: -24px">
                                            <h3>
                                                <label class="label" style="color: mediumseagreen">
                                                    CGPA :
                                                    <asp:Label ID="lblcgpa" runat="server"></asp:Label>
                                                </label>
                                            </h3>
                                        </span>
                                    </div>

                                    <asp:ListView ID="lvResultCGPA" runat="server" Visible="false">
                                        <EmptyDataTemplate>
                                            <br />
                                            <p class="text-center text-bold">
                                                <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Record Found"></asp:Label>
                                            </p>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <h4 class="box-title">Result Details</h4>
                                                <table class="table table-bordered table-hover">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th class="text-center">Semester</th>
                                                            <th class="text-center">GPA</th>
                                                            <th class="text-center">CGPA</th>
                                                        </tr>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </thead>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="text-center">
                                                    <asp:Label ID="lblSemester" runat="server" Text='<%#Eval("SEMESTER")%>'></asp:Label>
                                                </td>
                                                <td class="text-center">
                                                    <asp:Label ID="lblCCode" runat="server" Text='<%#Eval("SGPA")%>'></asp:Label>
                                                </td>
                                                <td class="text-center">
                                                    <asp:Label ID="lblSubject" runat="server" Text='<%#Eval("CGPA")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <%--  </div>


                            <div class="panel" style="width: 1080px; margin-left: 22px; margin-top: 30px">--%>
                                    <asp:ListView ID="lvResult" runat="server">
                                        <EmptyDataTemplate>
                                            <br />
                                            <p class="text-center text-bold">
                                                <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Record Found"></asp:Label>
                                            </p>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <%-- <h4 class="box-title">Result Details</h4>--%>
                                                <table class="table table-bordered table-hover">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th class="text-center">Semester</th>
                                                            <th class="text-center">Subject Code</th>
                                                            <th class="text-center">Subject Name</th>
                                                            <th class="text-center">Credits</th>
                                                            <th class="text-center">Internal</th>
                                                            <th class="text-center">External</th>
                                                            <th class="text-center">Total</th>
                                                            <th class="text-center">Grade Point</th>
                                                            <th class="text-center">Grade</th>
                                                            <th class="text-center">Result</th>
                                                            <%--<th class="text-center">GPA</th>
                                                        <th class="text-center">CGPA</th>--%>
                                                            <th class="text-center">Passing Year</th>
                                                        </tr>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </thead>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="text-center">
                                                    <asp:Label ID="lblSemester" runat="server" Text='<%#Eval("SEMESTERNO")%>'></asp:Label>
                                                </td>
                                                <td class="text-center">
                                                    <asp:Label ID="lblCCode" runat="server" Text='<%#Eval("CCODE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSubject" runat="server" Text='<%#Eval("COURSENAME")%>'></asp:Label>
                                                </td>
                                                <td class="text-center">
                                                    <asp:Label ID="lblCredits" runat="server" Text='<%#Eval("CREDITS")%>'></asp:Label>
                                                </td>
                                                <td class="text-center">
                                                    <asp:Label ID="lblInternal" runat="server" Text='<%#Eval("INTERMARK")%>'></asp:Label>
                                                </td>
                                                <td class="text-center">
                                                    <asp:Label ID="lblExternal" runat="server" Text='<%#Eval("EXTERMARK")%>'></asp:Label>
                                                </td>
                                                <td class="text-center">
                                                    <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("MARKTOTAL")%>'></asp:Label>
                                                </td>
                                                <td class="text-center">
                                                    <asp:Label ID="lblGDPoint" runat="server" Text='<%#Eval("GDPOINT")%>'></asp:Label>
                                                </td>
                                                <td class="text-center">
                                                    <asp:Label ID="lblGrade" runat="server" Text='<%#Eval("GRADE")%>'></asp:Label>
                                                </td>
                                                <td class="text-center" style="display: '<%# Eval("RESULTFLAG") == "P" %>'">
                                                    <asp:Label ID="lblResult" runat="server" Text='<%#Eval("RESULTFLAG")%>'></asp:Label>
                                                </td>
                                                <%--<td class="text-center">
                                                <asp:Label ID="lblSGPA" runat="server" Text='<%#Eval("SGPA1")%>'></asp:Label>
                                            </td>
                                            <td class="text-center">
                                                <asp:Label ID="lblCGPA" runat="server" Text='<%#Eval("CGPA1")%>'></asp:Label>
                                            </td>    --%>
                                                <td class="text-center">
                                                    <asp:Label ID="lblPassYear" runat="server" Text='<%#Eval("PASSINGYEAR")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>

                                <div runat="server" style="width: 1080px; margin-left: 22px;">
                                    <div class="col-md-12">
                                        <label style="font-weight: bold;"><span>Do you want to mention the remark for the above result?</span></label>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:RadioButton ID="rbYes" runat="server" GroupName="Confirm" OnCheckedChanged="rbYes_CheckedChanged" AutoPostBack="true" EnableViewState="true" />
                                        <label style="margin-right: 26px">Yes</label>
                                        <asp:RadioButton ID="rbNo" runat="server" GroupName="Confirm" Checked="true" OnCheckedChanged="rbNo_CheckedChanged" AutoPostBack="true" Enabled="true" />
                                        <label>No</label>
                                    </div>
                                </div>

                                <div id="divRemark" runat="server" style="width: 1080px; margin-left: 22px; margin-top: 30px" visible="false">
                                    <div class="col-md-12" style="margin-top: 20px">
                                        <label style="font-weight: bold;">Note : <span style="color: red">Please mention the details below, if any changes in the result are requested!</span></label>
                                    </div>
                                    <div class="col-md-12">
                                        <label>Remark :</label>
                                        <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="form-control" Width="700px" Height="80px" MaxLength="5"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="text-center" style="width: 1080px; margin-left: 22px; margin-top: 30px; display: inline-block; margin-bottom: 10px">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-outline-info" OnClick="btnSubmit_Click" OnClientClick="return confirm('Are you sure, you want to submit?');" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" />
                                </div>

                            </div>
                    </div>

                </div>
            </div>
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="rbYes" EventName="CheckedChanged" />
            <asp:AsyncPostBackTrigger ControlID="rbNo" EventName="CheckedChanged" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>

    </asp:UpdatePanel>

</asp:Content>
