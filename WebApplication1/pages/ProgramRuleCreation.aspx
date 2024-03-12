<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ProgramRuleCreation.aspx.cs" Inherits="ProgramRuleCreation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<script runat="server">

    protected void ddlPrograms_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlProgramBrid_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlProgramBrid_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
</script>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <link href="../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
     <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
     <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>


    <script type="text/javascript" language="javascript">
        function RunThisAfterEachAsyncPostback() {
            Autocomplete();
        }


        //function cnt() {
        //    var a = document.getElementById("txtRemark").value;
        //    document.getElementById("lblCount").innerHTML = 15 - a.length;
        //}

    </script>

    <%--<script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>

    <style>
        .nav-tabs-custom {
            box-shadow: none !important;
        }
    </style>
    <style>
        .table-responsive {
            display: inline-table;
        }

        .form-box {
            width: auto;
        }

        .multiselect-container > li > a > label {
            position: relative;
        }

        .dropdown-menu.show {
            width: 100%;
        }

        .multiselect.dropdown-toggle::after {
            display: none;
        }

        .daterangepicker .drp-calendar .table-condensed {
            display: none;
        }

        .daterangepicker.show-ranges .drp-calendar .table-condensed,
        .daterangepicker.single .drp-calendar .table-condensed {
            display: block;
        }

        .form-control-new {
            width: 100%;
            height: calc(1.5em + .75rem + 2px);
            padding: .375rem .75rem;
            font-weight: 400;
            line-height: 1.5;
            color: #495057;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            border-top-color: rgb(206, 212, 218);
            border-right-color: rgb(206, 212, 218);
            border-bottom-color: rgb(206, 212, 218);
            border-left-color: rgb(206, 212, 218);
            border-radius: .25rem;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }

        /*#ctl00_ContentPlaceHolder1_pnTest input {
            padding: .375rem .32rem;
            margin-right: 8px;
        }*/
       
    </style>
    <style>
        #ctl00_ContentPlaceHolder1_Panel1 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
        
    </style>
    
    <script>
        $(function () {
            $('.numbers').keyup(function () {
                this.value = this.value.replace(/[^0-9\.]/g, '');
            });
        });

    </script>

   
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="nav-tabs-custom">
                               
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Rule Creation</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2">Rule Allocation</a>
                                    </li>
                                     <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_3" tabindex="3">Bridging Eligibility</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_4" tabindex="4">Bridging Allocation</a>
                                    </li>
                                </ul>

                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab_1">
                                        <div>
                                            <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />
                                            <asp:HiddenField ID="HiddenField1" runat="server" ClientIDMode="Static" />
                                              <asp:HiddenField ID="HiddenField3" runat="server" ClientIDMode="Static" />
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updRule"
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
                                       
                                        <asp:UpdatePanel ID="updRule" runat="server">
                                            <ContentTemplate>
                                                <div id="divCourses" runat="server">
                                                    <div class="box-body">
                                                        <div class="col-12">
                                                            <div class="row">

                                                                <%-- Selection Here --%>
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <%-- <asp:Label ID="lblSessionLongName" runat="server" Font-Bold="true"></asp:Label>--%>
                                                                        <asp:Label runat="server" ID="lblRuleName" Font-Bold="true">Rule Name</asp:Label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtRuleName" runat="server" CssClass="form-control" MaxLength="100" TabIndex="5"
                                                                        ToolTip="Please Enter Rule Name" ClientIDMode="Static" />
                                                                    
                                                                </div>
                                                               
                                                                <div class="form-group col-lg-3 col-md-6 col-12">

                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="lblDyAL" runat="server" Font-Bold="true">ALType</asp:Label>
                                                                    </div>
                                                                    <asp:ListBox ID="ddlALType" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo"
                                                                        SelectionMode="multiple" TabIndex="6"></asp:ListBox>

                                                                </div>
                                                                <div class="form-group col-lg-2 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="lblAptitude" runat="server" Font-Bold="true" >Aptitude Test</asp:Label>
                                                                        
                                                                    </div>
                                                                    <div class="switch form-inline">
                                                                        <input type="checkbox" id="switchAptitude" name="switchAptitude" class="switchAptitude" checked TabIndex="7"/>
                                                                        <label data-on="Active" data-off="Inactive" for="switchAptitude"></label>
                                                                    </div>
                                                                </div>

                                                                 <div class="form-group col-lg-2 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="lblInterview" runat="server" Font-Bold="true">Interview Test</asp:Label>
                                                                        
                                                                    </div>
                                                                    <div class="switch form-inline">
                                                                        <input  type="checkbox" id="switchInterview" name="switchInterview" class="switchInterview" checked TabIndex="8"/>
                                                                        <label data-on="Active" data-off="Inactive" for="switchInterview"></label>
                                                                    </div>
                                                                </div>

                                                                 
                                                                <%--  --%>
                                                                <div class="form-group col-lg-2 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="lblStatus" runat="server" Font-Bold="true"></asp:Label>
                                                                        
                                                                    </div>
                                                                    <div class="switch form-inline">
                                                                        <input type="checkbox" id="switch" name="switch" class="switch" checked TabIndex="8"/>
                                                                        <label data-on="Active" data-off="Inactive" for="switch"></label>
                                                                    </div>
                                                                </div>

                                                                <%--  --%>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="col-12">

                                                            <table class="table table-striped table-bordered " style="width: 100%" id="myTable">
                                                                <thead class="bg-light-blue">
                                                                    <tr>

                                                                        <th style="width:40px">Type</th>
                                                                        <th style="width:150px"><sup>*</sup>Stream </th>
                                                                        <th><sup>*</sup>Min Courses</th>
                                                                        <th><sup>*</sup>Min Grade</th>
                                                                        <th class="text-center">&&</th>
                                                                        <th>Max Course</th>
                                                                        <th>Max Grade</th>
                                                                        <th><sup>*</sup>Subjects</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <%-- <tr id="itemPlaceholder" runat="server" />--%>
                                                                    <tr>
                                                                        <th><sup>*</sup>A/L</th>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlStreamAL" runat="server" TabIndex="9" CssClass="form-control" data-select2-enable="true"
                                                                                AppendDataBoundItems="True"
                                                                                ToolTip=" Select" ClientIDMode="Static">
                                                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td style="width:100px">
                                                                            <asp:DropDownList ID="ddlMinCourseAl" runat="server" TabIndex="10" CssClass="form-control" data-select2-enable="true"
                                                                                AppendDataBoundItems="True"
                                                                                ToolTip="Please Select Type" ClientIDMode="Static">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                                            </asp:DropDownList>

                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlMinGradeAL" runat="server" TabIndex="11" CssClass="form-control" data-select2-enable="true"
                                                                                AppendDataBoundItems="True"
                                                                                ToolTip="Select " ClientIDMode="Static">
                                                                                <asp:ListItem Value="0"> Select</asp:ListItem>

                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td class="text-center">&&
                                                                        </td>
                                                                        <td style="width:100px">
                                                                            <asp:DropDownList ID="ddlMaxCourseAL" runat="server" TabIndex="12" CssClass="form-control" data-select2-enable="true"
                                                                                AppendDataBoundItems="True"
                                                                                ToolTip="Please Select Type" ClientIDMode="Static">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td >
                                                                            <asp:DropDownList ID="ddlMaxGradeAL" runat="server" TabIndex="13" CssClass="form-control" data-select2-enable="true"
                                                                                AppendDataBoundItems="True"
                                                                                ToolTip="Select" >
                                                                                <asp:ListItem Value="0">Select</asp:ListItem>

                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>

                                                                            <asp:ListBox ID="ddlSubjectAL" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo"
                                                                                SelectionMode="multiple" TabIndex="14" ></asp:ListBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th>&nbsp;O/L </th>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlStreamOL" runat="server" TabIndex="15" CssClass="form-control" data-select2-enable="true"
                                                                                AppendDataBoundItems="True"
                                                                                ToolTip="Please Select Type" >
                                                                                <asp:ListItem Value="1">Please Select</asp:ListItem>
                                                                               <%-- <asp:ListItem Value="">Other</asp:ListItem>--%>

                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td style="width:100px">
                                                                            <asp:DropDownList ID="ddlMinCourseOL" runat="server" TabIndex="16" CssClass="form-control" data-select2-enable="true"
                                                                                AppendDataBoundItems="True"
                                                                                ToolTip="Please Select Type" >
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                                            </asp:DropDownList>


                                                                        </td>
                                                                        <td >
                                                                            <asp:DropDownList ID="ddlMinGradeOL" runat="server" TabIndex="17" CssClass="form-control" data-select2-enable="true"
                                                                                AppendDataBoundItems="True"
                                                                                ToolTip="Please Select Type" >
                                                                                <asp:ListItem Value="0">Select</asp:ListItem>

                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td class="text-center">&&
                                                                        </td>
                                                                        <td style="width:100px">
                                                                            <asp:DropDownList ID="ddlMaxCourseOl" runat="server" TabIndex="18" CssClass="form-control" data-select2-enable="true"
                                                                                AppendDataBoundItems="True"
                                                                                ToolTip="Please Select Type">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                 <asp:ListItem Value="1">1</asp:ListItem>
                                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td >
                                                                            <asp:DropDownList ID="ddlMaxGradeOL" runat="server" TabIndex="19" CssClass="form-control" data-select2-enable="true"
                                                                                AppendDataBoundItems="True"
                                                                                ToolTip="Select " >
                                                                                <asp:ListItem Value="0">Select</asp:ListItem>

                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>
                                                                            <asp:ListBox ID="ddlSubjectOL" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo" 
                                                                                SelectionMode="multiple" TabIndex="20"></asp:ListBox>

                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>

                                                        </div>

                                                        <div class="col-12 btn-footer">

                                                            <asp:LinkButton ID="btnSave" runat="server" ToolTip="Submit" ValidationGroup="submit" OnClientClick="return validate()"
                                                                CssClass="btn btn-outline-info btnX" OnClick="btnSave_Click" ClientIDMode="Static" TabIndex="21">Submit</asp:LinkButton>

                                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                                                TabIndex="22" CssClass="btn btn-outline-danger" />
                                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                                                ShowSummary="false" DisplayMode="List" ValidationGroup="report" />
                                                             <asp:HiddenField ID="hdndeptno" runat="server" />
                                                        </div>


                                                        <div class="col-12">

                                                            <asp:Panel ID="Panel1" runat="server">
                                                                <asp:ListView ID="lvProgram" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div id="demo-grid">
                                                                            <div class="sub-heading" id="dem">
                                                                                <h5>Program Rule List</h5>
                                                                            </div>
                                                                            <table class="table table-striped table-bordered nowrap display " style="width: 100%">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                      <th style="display:none">Sr No</th>
                                                                                       <th>Edit</th>
                                                                                        <th>Type</th>
                                                                                       <th >Rule Name</th>
                                                                                        <th>Stream</th>
                                                                                        <th>Min Courses</th>
                                                                                        <th>Min Grade</th>
                                                                                        <th class="text-center">&&</th>
                                                                                        <th>Max Course</th>
                                                                                        <th>Max Grade</th>
                                                                                        <th>Subject</th>
                                                                                        <th>Aptitude</th>
                                                                                        <th>Interview</th>
                                                                                        <th>Active</th>
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
                                                                        
                                                                            <th style="display:none">Sr No</th>
                                                                            <td>
                                                                                <asp:ImageButton ID="btneditRuleCreation" runat="server" ImageUrl="~/IMAGES/edit.png" CommandArgument='<%# Eval("COURSERULENO") %>'
                                                                                    AlternateText="Edit Record " OnClick="btneditRuleCreation_Click"
                                                                                    ToolTip="Edit Record" />
                                                                            </td>
                                                                            <th>A/L</th>
                                                                            <td><%# Eval("RULENAME")%></td>
                                                                            <td><%# Eval("ALSTREAMNAME")%></td>
                                                                            <td><%# Eval("ALMIN_COURSE")%></td>
                                                                            <td><%# Eval("ALMIN_GRADE")%></td>
                                                                            <td class="text-center">&&</td>
                                                                            <td><%# Eval("ALMAX_COURSE")%></td>
                                                                            <td><%# Eval("ALMAX_GRADE")%></td>
                                                                            <td><%# Eval("AL_COURSES")%></td>
                                                                            <td><%# Eval("APTITUDE_TEST")%></td>
                                                                            <td><%# Eval("INTERVIEW_TEST")%></td>
                                                                            <td><%# Eval("STATUS")%></td>

                                                                        </tr>
                                                                        <tr>
                                                                            
                                                                            <th style="display:none">Sr No</th>
                                                                            <th>
                                                                             <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/IMAGES/edit.png" Visible="false" CommandArgument='<%# Eval("COURSERULENO") %>'
                                                                                    AlternateText="Edit Record "
                                                                                    ToolTip="Edit Record" />
                                                                                </th>
                                                                            <th>O/L</th>
                                                                            <td><%# Eval("RULENAME")%></td>
                                                                            <td><%# Eval("OLSTREAMNAME")%></td>
                                                                            <td><%# Eval("OLMIN_COURSE")%></td>
                                                                            <td><%# Eval("OLMINGRADES")%></td>
                                                                            <td class="text-center">&&</td>
                                                                            <td><%# Eval("OLMAX_COURSE")%></td>
                                                                            <td><%# Eval("OLMAXGARDE")%></td>
                                                                            <td><%# Eval("OLCOURSES")%></td>
                                                                            <td><%# Eval("APTITUDE_TEST")%></td>
                                                                            <td><%# Eval("INTERVIEW_TEST")%></td>
                                                                            <td><%# Eval("STATUS")%></td>

                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>



                                                        </div>

                                                        <%--  --%>
                                                    </div>
                                                </div>

                                            </ContentTemplate>

                                        </asp:UpdatePanel>
                                    </div>
                                    <style>
                                        #ctl00_ContentPlaceHolder1_Panel2 input {
                                            padding: .375rem .32rem;
                                            margin-right: 8px;
                                        }

                                        }
                                    </style>
                                    <div class="tab-pane fade" id="tab_2">
                                        <div>
                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updPanel2"
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
                                        <asp:UpdatePanel ID="updPanel2" runat="server">
                                            <ContentTemplate>
                                                <div id="div1" runat="server">
                                                    <div class="box-body">
                                                        <div class="col-12">
                                                            <div class="row">
                                                                <%-- Fields Here --%>
                                                                <div class="form-group col-lg-3 col-md-6 col-12">

                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="Label2" runat="server" Font-Bold="true"></asp:Label>
                                                                        <label>Intake</label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlIntake" runat="server" TabIndex="5" CssClass="form-control" data-select2-enable="true"
                                                                        AppendDataBoundItems="True" 
                                                                        ToolTip="Please Select Intake">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                                    </asp:DropDownList>
                                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlIntake"
                                                                Display="None" ErrorMessage="Please Select Intake" InitialValue="0" ValidationGroup="CourseCreation"
                                                              />

                                                                </div>
                                                                <%--  --%>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">

                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="Label3" runat="server" Font-Bold="true"></asp:Label>
                                                                        <label>Discipline</label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlDiscipline" runat="server" TabIndex="6" CssClass="form-control" data-select2-enable="true"
                                                                        AppendDataBoundItems="True" OnSelectedIndexChanged="ddlDiscipline_SelectedIndexChanged" AutoPostBack="true"
                                                                        ToolTip="Please Select Discipline"> 
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                                    </asp:DropDownList>
                                                                    
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDiscipline"
                                                                Display="None" ErrorMessage="Please Select Discipline" InitialValue="0" ValidationGroup="CourseCreation"
                                                              />
                                                                    

                                                                </div>

                                                                <%--  --%>
                                                                <div class="form-group col-lg-3 col-md-6 col-12">

                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="Label4" runat="server" Font-Bold="true"></asp:Label>
                                                                        <label>Program</label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlProgram" runat="server" TabIndex="7" CssClass="form-control" data-select2-enable="true"
                                                                        AppendDataBoundItems="True" 
                                                                        ToolTip="Please Select Program">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                                    </asp:DropDownList>
                                                                             
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlProgram"
                                                                Display="None" ErrorMessage="Please Select Program" InitialValue="0" ValidationGroup="CourseCreation"
                                                              />

                                                                </div>

                                                                <%--  --%>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">

                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="Label5" runat="server" Font-Bold="true"></asp:Label>
                                                                        <label>Rules</label>
                                                                    </div>
                                                                    <asp:ListBox ID="ddlRulename" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo"
                                                                        SelectionMode="multiple" TabIndex="8" ValidationGroup="CourseCreation">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                                    </asp:ListBox>
                                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlRulename"
                                                                Display="None" ErrorMessage="Please Select Rule"  ValidationGroup="CourseCreation"></asp:RequiredFieldValidator>
                                                              

                                                                </div>
                                                                <%--  --%>
                                                            </div>
                                                        </div>
                                                        <%--  --%>
                                                        <div class="col-12 btn-footer">
                                                            <asp:LinkButton ID="btnSaveClick" runat="server" ToolTip="Submit" OnClientClick="return validate()"  ValidationGroup="CourseCreation"
                                                                CssClass="btn btn-outline-info btnX" OnClick="btnSaveClick_Click" TabIndex="9" >Submit</asp:LinkButton>

                                                            <asp:Button ID="btnCancelAllocation" runat="server" Text="Cancel"
                                                                TabIndex="10" CssClass="btn btn-outline-danger" OnClick="btnCancelAllocation_Click" />
                                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="CourseCreation"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                            <asp:HiddenField ID="HiddenField2" runat="server" />
                                                        </div>
                                                        <%-- Table Here --%>
                                                        <div class="col-12">

                                                            <asp:Panel ID="Panel2" runat="server">
                                                                <asp:ListView ID="lvCollegeDetails" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div id="demo-grid">
                                                                            <div class="sub-heading" id="dem">
                                                                                <h5>Course Rule Allocation</h5>
                                                                            </div>

                                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable1">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <th>Edit</th>
                                                                                        <th>Intake</th>
                                                                                        <th>Discipline</th>
                                                                                        <th>Program</th>
                                                                                        <th>Rules</th>
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
                                                                                <asp:ImageButton ID="btnedit" runat="server" ImageUrl="~/IMAGES/edit.png" CommandArgument='<%# Eval("ALLOCATIONNO") %>'
                                                                                    AlternateText="Edit Record " OnClick="btnedit_Click"
                                                                                    ToolTip="Edit Record" />
                                                                            </td>
                                                                            <td>

                                                                                <%# Eval("BATCHNAME")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("UA_SECTIONNAME")%>
                                                                            </td>

                                                                            <td>
                                                                                <%# Eval("BRANCHNAME")%>
                                                                            </td>

                                                                            <td>
                                                                                <%# Eval("RULENAME")%>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="div2" runat="Server">
                                                </div>

                                            </ContentTemplate>
                                            <Triggers>
                                               <asp:AsyncPostBackTrigger ControlID="btnSaveClick" />
                                           <%--   <asp:PostBackTrigger ControlID="ddlRulename" />--%>

                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="tab-pane fade" id="tab_3">
                                        <div>
                                            <asp:UpdateProgress ID="updprogre" runat="server" AssociatedUpdatePanelID="updBridging"
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
                                           
                                             <asp:UpdatePanel ID="updBridging" runat="server">
                                            <ContentTemplate>
                                                <div id="div3" runat="server">
                                                    <div class="box-body">
                                                        <div class="col-12">
                                                            <div class="row">
                                                                <%-- Fields Here --%>
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <%-- <asp:Label ID="lblSessionLongName" runat="server" Font-Bold="true"></asp:Label>--%>
                                                                        <asp:Label runat="server" ID="lblBridging" Font-Bold="true">Rule Name</asp:Label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtBridgingRule" runat="server" CssClass="form-control" MaxLength="100" TabIndex="5"
                                                                        ToolTip="Please Enter Rule Name"  ValidationGroup="bridging"/>
                                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtBridgingRule"
                                                                Display="None" ErrorMessage="Please Enter Rule Name"  ValidationGroup="bridging"></asp:RequiredFieldValidator>

                                                                </div>
                                                             
                                                              
                                                                <div class="form-group col-lg-3 col-md-6 col-12">

                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="lblColleges" runat="server" Font-Bold="true">Faculty /School Name</asp:Label>
                                                                       
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlFaculty" runat="server" TabIndex="24" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"
                                                                        AppendDataBoundItems="True" AutoPostBack="true"
                                                                        ToolTip="Please Select Faculty /School Name">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                                    </asp:DropDownList>
                                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlFaculty"
                                                                Display="None" ErrorMessage="Please Select Faculty /School Name" InitialValue="0" ValidationGroup="bridging"
                                                              />
                                                                </div>

                                                                <%--  --%>
                                                                <div class="form-group col-lg-6 col-md-6 col-12">

                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="lblProgrambrid" runat="server" Font-Bold="true">Program</asp:Label>
                                                                      
                                                                    </div>
                                                                      <asp:DropDownList ID="ddlProgramBrid" runat="server" TabIndex="24" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlPrograms_SelectedIndexChanged1"
                                                                        AppendDataBoundItems="True" AutoPostBack="true"
                                                                        ToolTip="Please Select Faculty /School Name">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                                    </asp:DropDownList>
                                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlProgramBrid"
                                                                Display="None" ErrorMessage="Please Select Program" InitialValue="0" ValidationGroup="bridging"/>
                                                                  <%-- <asp:ListBox ID="ddlProgramBrid" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo"
                                                                        SelectionMode="multiple" TabIndex="25"  AutoPostBack="true" OnSelectedIndexChanged="ddlPrograms_SelectedIndexChanged1">
                                                                       --%>
                                                                  <%-- </asp:ListBox>--%>
                                                                      
                                                                </div>
                                                               
                                                            </div>
                                                        </div>
                                                        <%--  --%>
                                                        <div class="col-12 btn-footer">
                                                            <asp:LinkButton ID="btnSaveBridging" runat="server" ToolTip="Submit" OnClientClick="return validate()"  ValidationGroup="bridging"
                                                                CssClass="btn btn-outline-info btnX" OnClick="btnSaveBridging_Click" TabIndex="26" >Submit</asp:LinkButton>

                                                            <asp:Button ID="btnCancelBridging" runat="server" Text="Cancel"
                                                                TabIndex="27" CssClass="btn btn-outline-danger" OnClick="btnCancelBridging_Click" />

                                                             <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="bridging"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                            <asp:HiddenField ID="hfbridging" runat="server" />
                                                        </div>
                                                        <%-- Table Here --%>
                                                        <div class="col-12">

                                                            <asp:Panel ID="Panel3" runat="server">
                                                                <asp:ListView ID="lvlBridging" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div id="demo-grid">
                                                                            <div class="sub-heading" id="dem">
                                                                                <h5>Bridging Eligibility</h5>
                                                                            </div>

                                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" >
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                    <%--    <th>Edit</th>--%>
                                                                                        <th>Select</th>
                                                                                        <th style="display:none">CourseNo</th>
                                                                                      <%--  <th>faculty</th>--%>
                                                                                        <th>Module Name</th>
                                                                                        <th>Program</th>
                                                                                        <th>Stream</th>
                                                                                        <td style="display:none">Modulecode</th>
                                                                                        <td style="display:none">Module</th>

                                                                                        
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
                                                                           <%-- <th>
                                                                             <asp:ImageButton ID="btneditBridging" runat="server" CausesValidation="false" ImageUrl="~/IMAGES/edit.png" 
                                                                                    AlternateText="Edit Record " ToolTip="Edit Record" CommandArgument='<%# Eval("BRIDGINGNO") %>'
                                                                                     />
                                                                            </th>--%>
                                                                            <td>
                                                                              <asp:CheckBox ID="chkCheck" runat="server" />
                                                                            </td>
                                                                             <td style="display:none">
                                                                                
                                                                                <asp:Label ID="lblCourseNo" runat="server" ></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblModuleName" runat="server"  ></asp:Label>
                                                                         
                                                                            </td>
                                                                             <td>
                                                                                <asp:Label ID="lblProgram" runat="server"  ></asp:Label>
                                                                        
                                                                               
                                                                            </td>
                                                                            <td>
                                                                            <asp:ListBox ID="ddlStreams" runat="server"  CssClass="form-control multi-select-demo"
                                                                        SelectionMode="multiple" TabIndex="25"  />
                                                                            </td>
                                                                              <td style="display:none">
                                                                                <asp:Label ID="lblModuleCode" runat="server"  ></asp:Label>
                                                                         
                                                                            </td>
                                                                              <td style="display:none">
                                                                                <asp:Label ID="lblModule" runat="server"  ></asp:Label>
                                                                         
                                                                            </td>

                                                                            <%--<td>
                                                                              <asp:CheckBox ID="chkCheck" runat="server" Tool='<%# Eval("CHECKED") %>'/>
                                                                            </td>
                                                                             <td style="text-align:center">
                                                                                <%# Container.DataItemIndex + 1 %>
                                                                                <asp:HiddenField  ID="hdfvalue" runat="server" Value='<% #Eval("COURSENO") %>' />
                                                                             </td>
                                                                             <td style="display:none">
                                                                                
                                                                                <asp:Label ID="lblCourseNo" runat="server"  Text='<% #Eval("COURSENO")%>'>></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                
                                                                                <asp:Label ID="lblModuleCode" runat="server"  Text='<% #Eval("CCODE")%>'>></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblModuleName" runat="server"  Text='<% #Eval("COURSE_NAME")%>'>></asp:Label>
                                                                         
                                                                               
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblProgram" runat="server"  Text='<% #Eval("PROGRAM")%>'>></asp:Label>
                                                                        
                                                                               
                                                                            </td>
                                                                            <td>
                                                                            <asp:ListBox ID="ddlStreams" runat="server"  CssClass="form-control multi-select-demo"
                                                                        SelectionMode="multiple" TabIndex="25"  />
                                                                         
                                                                               
                                                                            </td>--%>
                                                                            
                                                                            
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="div4" runat="Server">
                                                </div>

                                            </ContentTemplate>
                                            <Triggers>
                                              <%--<asp:AsyncPostBackTrigger ControlID="btnSaveClick" />
                                              <asp:PostBackTrigger ControlID="ddlRulename" />--%>

                                            </Triggers>
                                        </asp:UpdatePanel>
                                       </div>


                                        </div>
                                    <div class="tab-pane fade" id="tab_4">
                                        <div>
                                            <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updBridgingAllocation"
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
                                        <asp:UpdatePanel ID="updBridgingAllocation" runat="server">
                                            <ContentTemplate>
                                                <div id="div5" runat="server">
                                                    <div class="box-body">
                                                        <div class="col-12">
                                                            <div class="row">
                                                                <%-- Fields Here --%>
                                                                <div class="form-group col-lg-3 col-md-6 col-12">

                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="lblIntakeBA" runat="server" Font-Bold="true"></asp:Label>
                                                                        <label>Intake</label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlIntakeBA" runat="server" TabIndex="5" CssClass="form-control" data-select2-enable="true"
                                                                        AppendDataBoundItems="True" 
                                                                        ToolTip="Please Select Intake" >
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                         
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlIntakeBA"
                                                                Display="None" ErrorMessage="Please Select Intake" InitialValue="0" ValidationGroup="bridgingAllo"
                                                              />

                                                                </div>
                                                              
                                                               
                                                                <div class="form-group col-lg-3 col-md-6 col-12">

                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="lblRuleNameBA" runat="server" Font-Bold="true"></asp:Label>
                                                                        <label>Rules</label>
                                                                    </div>
                                                                    <asp:ListBox ID="ddlRuleNameBA" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo"
                                                                        SelectionMode="multiple" TabIndex="8" >
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                                    </asp:ListBox>
                                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlRuleNameBA"
                                                                Display="None" ErrorMessage="Please Select Rule Name" InitialValue="0" ValidationGroup="bridgingAllo"
                                                              />

                                                                </div>
                                                            
                                                            </div>
                                                        </div>
                                                        <%--  --%>
                                                        <div class="col-12 btn-footer">
                                                            <asp:LinkButton ID="btnSaveBridgingAllo" runat="server" ToolTip="Submit" ValidationGroup="bridgingAllo" 
                                                                CssClass="btn btn-outline-info btnX" OnClick="btnSaveBridgingAllo_Click" TabIndex="9" OnClientClick="return validate()">Submit</asp:LinkButton>

                                                            <asp:Button ID="Button1" runat="server" Text="Cancel"
                                                                TabIndex="10" CssClass="btn btn-outline-danger" OnClick="Button1_Click"/>
                                                              <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="bridgingAllo"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                            <asp:HiddenField ID="HiddenField4" runat="server" />
                                                        </div>
                                                        <%-- Table Here --%>
                                                        <div class="col-12">

                                                            <asp:Panel ID="Panel4" runat="server">
                                                                <asp:ListView ID="ListView1" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div id="demo-grid">
                                                                            <div class="sub-heading" id="dem">
                                                                                <h5>Bridging Allocation</h5>
                                                                            </div>

                                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" >
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                      
                                                                                        <th>Intake</th>
                                                                                        <th>Rules</th>
                                                                                        <th>Program</th>
                                                                                          <th>Module Name</th>
                                                                                         <th>Compulsory Stream</th>

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

                                                                                <%# Eval("BATCHNAME")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("RULENAME")%>
                                                                            </td>
                                                                            <td>
                                                                                 <%# Eval("PROGRAM")%>
                                                                            </td>
                                                                            <td>
                                                                                 <%# Eval("COURSENAME")%>
                                                                            </td>
                                                                           <td>
                                                                                 <%# Eval("STREAMNAME")%>
                                                                            </td>
                                                                            
                                                                           
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="div6" runat="Server">
                                                </div>

                                            </ContentTemplate>
                                          
                                        </asp:UpdatePanel>
                                    </div>
                                    <%--  --%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

    
      
   <script>
       function SetStat1(val) {

           $('#switchAptitude').prop('checked', val)
          
       }
      
    </script>
    <script>
        function SetStat2(val) {

            $('#switchInterview').prop('checked', val)
        }
    </script>

   

    <script>
        function SetStat(val) {
            $('#switch').prop('checked', val);
            //$('#switchAptitude').prop('checked', val)
            //$('#switchInterview').prop('checked', val)

        }

       
        var summary = "";
        $(function () {
            
            $('#btnSave').click(function () {
                localStorage.setItem("currentId", "#btnSave,Submit");
                debugger;
                ShowLoader('#btnSave');
              
                if ($('#txtRuleName').val() == "")
                    summary += '<br>Please Enter Rule Name';
                if ($('#ddlStreamAL').val() == "0")
                    summary += '<br>Please Select AL Stream ';
                if ($('#ddlMinCourseAl').val() == "0")
                    summary += '<br>Please Select Min Course AL';
                if ($('#ddlMinGradeAL').val() == "0")
                    summary += '<br>Please Select Min Grade AL';
               
                if (summary != "") {
                    customAlert(summary); 
                    summary = "";
                    return false
                }
                $('#hfdStat').val($('#switch').prop('checked'));
                $('#HiddenField1').val($('#switchAptitude').prop('checked'));
                $('#HiddenField3').val($('#switchInterview').prop('checked'));
            
            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSave').click(function () {
                    localStorage.setItem("currentId", "#btnSave,Submit");
                    ShowLoader('#btnSave');
                   
                    if ($('#txtRuleName').val() == "")
                        summary += '<br>Please Enter Rule Name';
                    if ($('#ddlStreamAL').val() == "0")
                        summary += '<br>Please Select AL Stream ';
                    if ($('#ddlMinCourseAl').val() == "0")
                        summary += '<br>Please Select Min Course AL';
                    if ($('#ddlMinGradeAL').val() == "0")
                        summary += '<br>Please Select Min Grade AL';
                

                    if (summary != "") {
                        customAlert(summary);
                        summary = "";
                        return false
                    }
                    $('#hfdStat').val($('#switch').prop('checked'));
                    $('#HiddenField1').val($('#switchAptitude').prop('checked'));
                    $('#HiddenField3').val($('#switchInterview').prop('checked'));
                });
            });
        });
    </script>

    




<%--     <script>
       
       
        var summary = "";
        $(function () {
            
            $('#btnSaveClick').click(function () {
                localStorage.setItem("currentId", "#btnSaveClick,Submit");
                debugger;
                ShowLoader('#btnSaveClick');

                if ($('#ddlIntake').val() == "0")
                    summary += '<br>Please Select Intake';
                if ($('#ddlDiscipline').val() == "0")
                    summary += '<br>Please Select Discipline';
                if ($('#ddlProgram').val() == "0")
                    summary += '<br>Please Select Program';
                if ($('#ddlRulename').val() == "0")
                    summary += '<br>Please Select Rules';
              
               
                if (summary != "") {
                    customAlert(summary); 
                    summary = "";
                    return false
                }
              
            
            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSaveClick').click(function () {
                    localStorage.setItem("currentId", "#btnSaveClick,Submit");
                    ShowLoader('#btnSaveClick');
                   
                    if ($('#ddlIntake').val() == "0")
                        summary += '<br>Please Select Intake';
                    if ($('#ddlDiscipline').val() == "0")
                        summary += '<br>Please Select Discipline';
                    if ($('#ddlProgram').val() == "0")
                        summary += '<br>Please Select Program';
                    if ($('#ddlRulename').val() == "0")
                        summary += '<br>Please Select Rules';

                    if (summary != "") {
                        customAlert(summary);
                        summary = "";
                        return false
                    }
                   
                 
                });
            });
        });
    </script>--%>

   <%-- <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
        var parameter = Sys.WebForms.PageRequestManager;
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200
                });
            });
        });--%>

   <style>
        #ctl00_ContentPlaceHolder1_pnlCourse .form-control {
            padding: 0.15rem 0.15rem;
        }
    </style>

     <script type="text/javascript">
             $(document).ready(function () {
                 $('.multi-select-demo').multiselect({
                     includeSelectAllOption: true,
                     maxHeight: 200
                 });
             });
             var parameter = Sys.WebForms.PageRequestManager.getInstance();
             parameter.add_endRequest(function () {
                 $('.multi-select-demo').multiselect({
                     includeSelectAllOption: true,
                     maxHeight: 200
                 });
             });

        </script>

    
     <%--<script type="text/javascript" language="javascript">
        function RunThisAfterEachAsyncPostback() {
            Autocomplete();
        }
      
    </script>--%>

  <%--  <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>
  

</asp:Content>

