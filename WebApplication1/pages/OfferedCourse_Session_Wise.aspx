<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="OfferedCourse_Session_Wise.aspx.cs" Inherits="ACADEMIC_OfferedCourse_Session_Wise" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnl"
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
    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title"><span>OFFERED SUBJECT</span></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><asp:Label ID="lblDYClgReg" runat="server" Font-Bold="true"></asp:Label></label>
                                            <%--<label><span style="color: red;">*</span> College & Regulation </label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="offered" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College & Regulation" InitialValue="0" ValidationGroup="RegNoAllot">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><asp:Label ID="lblDYSession" runat="server" Font-Bold="true"></asp:Label></label>
                                            <%--<label><span style="color: red;">*</span> Session</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" />
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            ValidationGroup="offered" Display="None" ErrorMessage="Please Select session"
                                            InitialValue="0" SetFocusOnError="true" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="offered"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="3"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="offered"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Regulation</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="4"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Regulation" ValidationGroup="offered"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><asp:Label ID="lblDYSemester" runat="server" Font-Bold="true"></asp:Label></label>
                                            <%-- <label><span style="color: red;">*</span> Semester</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"
                                            TabIndex="5" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server"
                                            ControlToValidate="ddlSemester" Display="None"
                                            ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="offered" />
                                    </div>

                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading">Note</h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i>
                                                <span>While changing any sem offered subjects, please select all 
                                                the applied subjects once again for that semester and then submit the record.</span> 
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnAd" runat="server" OnClick="btnAd_Click" 
                                        TabIndex="11" Text="Submit" ValidationGroup="offered" CssClass="btn btn-outline-info"><i class=" fa fa-save"></i> Submit</asp:LinkButton>
                                <asp:LinkButton ID="btnPrint" runat="server" OnClick="btnPrint_Click"
                                        TabIndex="12" Text=" Report" ValidationGroup="submit" CssClass="btn btn-outline-primary"><i class=" fa fa-file-pdf-o" aria-hidden="true"></i> Report</asp:LinkButton>
                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                    CssClass="btn btn-outline-danger" />             
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="offered" />
                            </div>
                            <div class="col-12 btn-footer">  
                                <asp:Label ID="lblStatus" runat="server" SkinID="lblmsg"></asp:Label>
                            </div>

                            <div class="col-md-12">
                                <asp:Panel ID="pnlCourse" runat="server" Visible="false">
                                    <asp:ListView ID="lvMandatoryCourse" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading">
                                                    <h5>Mandatory Subject</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width:100%" id="myTable1">
                                                    <thead class="bg-light-blue">
                                                        <tr>                                                    
                                                            <th class="text-center">Subject Code</th>
                                                            <th class="text-center">Subject Name</th>
                                                            <th class="text-center">Offered Sem/Year</th>
                                                            <th class="text-center">Elective Group</th>
                                                            <th class="text-center">Credits</th>
                                                            <th class="text-center">Offered</th>
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
                                                <td><%# Eval("CCODE")%></td>
                                                <td><%# Eval("COURSE_NAME")%></td>
                                                <td style="text-align:center"><%# Eval("SEMESTERNAME")%></td>
                                                <td style="text-align:center"><%# Eval("GROUPNAME")%></td>
                                                <td class="text-center"><%# Eval("CREDITS") %></td>
                                                <td class="text-center">
                                                    <asp:CheckBox ID="chkmoffered" runat="server" Checked="true" Enabled="false"/>
                                                    <asp:HiddenField ID="hf_mcourse" runat="server" Value='<%# Eval("COURSENO") %>' />
                                                    <asp:HiddenField ID="hf_moffered" runat="server" Value='<%# Eval("OFFERED") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                    <asp:ListView ID="lvCourse" runat="server" OnItemDataBound="lvCourse_ItemDataBound">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading">
                                                    <h5>Offered Subject</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width:100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>                                                    
                                                            <th class="text-center">Subject Code</th>
                                                            <th class="text-center">Subject Name</th>
                                                            <th class="text-center">Offered Sem/Year</th>
                                                            <th class="text-center">Elective Group</th>
                                                            <th class="text-center">Credits</th>
                                                            <th class="text-center">Offered</th>
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
                                                <td><%# Eval("CCODE")%></td>
                                                <td><%# Eval("COURSE_NAME")%></td>
                                                <td style="text-align:center"><%# Eval("SEMESTERNAME")%></td>
                                                <td style="text-align:center"><%# Eval("GROUPNAME")%></td>
                                                <td class="text-center"><%# Eval("CREDITS") %></td>
                                                <td class="text-center">
                                                    <asp:CheckBox ID="chkoffered" runat="server" AutoPostBack="true" OnCheckedChanged="chkoffered_CheckedChanged" ToolTip="Please Select Subject" />
                                                    <asp:HiddenField ID="hf_course" runat="server" Value='<%# Eval("COURSENO") %>' />
                                                    <asp:HiddenField ID="hf_offered" runat="server" Value='<%# Eval("OFFERED") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </asp:Panel>
                            </div>

                        </div>

                    </div>
                </div>
            </div>
            
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

