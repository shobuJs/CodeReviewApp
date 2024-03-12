<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudentInfoEntry.aspx.cs" Inherits="Academic_StudentInfoEntry" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script type="text/javascript" language="javascript" src="../includes/prototype.js"></script>

    <script type="text/javascript" language="javascript" src="../includes/scriptaculous.js"></script>

    <script type="text/javascript" language="javascript" src="../includes/modalbox.js"></script>--%>
    <div id="myModal2" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Search</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updEdit" runat="server">
                        <ContentTemplate>
                            <div class="form-group col-md-12">
                                <label>Search Criteria</label>
                                <br />
                                <asp:RadioButton ID="rbName" runat="server" Text="Name" GroupName="edit" />
                                <asp:RadioButton ID="rbIdNo" runat="server" Text="IdNo" GroupName="edit" />
                                <asp:RadioButton ID="rbBranch" runat="server" Text="Branch" GroupName="edit" />
                                <asp:RadioButton ID="rbSemester" runat="server" Text="Semester" GroupName="edit" />
                                <asp:RadioButton ID="rbEnrollmentNo" runat="server" Text="SR NO" GroupName="edit"
                                    Checked="True" />

                                <asp:RadioButton ID="rbRegNo" runat="server" Text="Rollno" GroupName="edit" Checked="True" />
                            </div>
                            <div class="form-group col-md-12">
                                <div class="col-md-6">
                                    <label>Search String</label>
                                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <p class="text-center">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClientClick="return submitPopup(this.name);" CssClass="btn btn-outline-info" />
                                    <asp:Button ID="btnCancelModal" runat="server" Text="Cancel" OnClientClick="return ClearSearchBox(this.name)" CssClass="btn btn-outline-danger" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </p>
                                <div>
                                    <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                </div>
                                <div>
                                    <asp:UpdateProgress ID="updProgress" runat="server" AssociatedUpdatePanelID="updEdit">
                                        <ProgressTemplate>
                                            <asp:Image ID="imgProg" runat="server" ImageUrl="~/images/ajax-loader.gif" />
                                            Loading.. Please Wait!
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div class="col-md-12">
                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                        <asp:ListView ID="lvStudent" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <h4>Login Details</h4>
                                                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Height="300px">
                                                        <table class="table table-hover table-bordered">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Name
                                                                    </th>
                                                                    <th>IdNo
                                                                    </th>
                                                                    <th>Roll No.
                                                                    </th>
                                                                    <th>Branch
                                                                    </th>
                                                                    <th>Semester
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                    </asp:Panel>
                                                </div>

                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                            OnClick="lnkId_Click"></asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <%# Eval("idno")%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblstuenrollno" runat="server" Text='<%# Eval("EnrollmentNo")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblstudentfullname" runat="server" Text='<%# Eval("longname")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                    </td>
                                                    <td>
                                                        <%# Eval("SEMESTERNO")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                </div>
            </div>

        </div>
    </div>

    <asp:Panel ID="pnDisplay" Visible="true" Enabled="true" runat="server">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info">
                    <div class="box-header with-border">
                        <span class="glyphicon glyphicon-user text-blue"></span>
                        <h3 class="box-title">STUDENT INFORMATION</h3>
                        <div class="box-tools pull-right">
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-3" id="divtabs" runat="server">
                                <div class="col-md-12">
                                    <div class="panel panel-info">
                                        <div class="panel panel-heading"><b>Click To Open Respective Information</b></div>
                                        <div class="panel-body">
                                            <aside class="sidebar">

                                                <!-- sidebar: style can be found in sidebar.less -->
                                                <section class="sidebar" style="background-color: #12aae2">
                                                    <ul class="sidebar-menu">
                                                        <!-- Optionally, you can add icons to the links -->
                                                        <br />
                                                        <li class="treeview">&nbsp <i class="fa fa-user"><span>
                                                            <asp:LinkButton runat="server" ID="lnkPersonalDetail"
                                                                ToolTip="Please select Personal Details." OnClick="lnkPersonalDetail_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Personal Details"> 

                                                            </asp:LinkButton>
                                                        </span>
                                                        </i>
                                                            <hr />
                                                        </li>

                                                        <li class="treeview">&nbsp <i class="fa fa-map-marker"><span>
                                                            <asp:LinkButton runat="server" ID="lnkAddressDetail"
                                                                ToolTip="Please select Address Details." OnClick="lnkAddressDetail_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Address Details"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        </i>
                                                           
                                                            <hr />
                                                        </li>

                                                        <div id="divadmissiondetails" runat="server">
                                                            <li class="treeview">&nbsp<i class="fa fa-university"><span>
                                                                <asp:LinkButton runat="server" ID="lnkAdmissionDetail"
                                                                    ToolTip="Please select Personal Details." OnClick="lnkAdmissionDetail_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Admission Details"> 
                                                                </asp:LinkButton>
                                                            </span>
                                                            </i>
                                                              
                                                                <hr />
                                                            </li>
                                                        </div>
                                                        <li class="treeview">
                                                            <i class="fa fa-info-circle"><span>
                                                                <asp:LinkButton runat="server" ID="lnkDasaStudentInfo"
                                                                    ToolTip="Please select DASA Student Information." OnClick="lnkDasaStudentInfo_Click" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Style="color: white; font-size: 16px;" Text="DASA Student Information"> 
                                                                </asp:LinkButton>
                                                            </span>
                                                            </i>
                                                          
                                                            <hr />
                                                        </li>
                                                        <li class="treeview">&nbsp<i class="fa fa-graduation-cap"><span>
                                                            <asp:LinkButton runat="server" ID="lnkQualificationDetail"
                                                                ToolTip="Please select Qualification Details." OnClick="lnkQualificationDetail_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Qualification Details"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        </i>
                                                      
                                                            <hr />
                                                        </li>
                                                        <li class="treeview">&nbsp<i class="fa fa-link"><span>
                                                            <asp:LinkButton runat="server" ID="lnkotherinfo"
                                                                ToolTip="Please select Other Information." OnClick="lnkotherinfo_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Other Information"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        </i>
                                                          
                                                            <p></p>
                                                            <hr />
                                                        </li>

                                                        <li class="treeview">&nbsp;<i class="glyphicon glyphicon-print"><span>
                                                            <asp:LinkButton runat="server" ID="lnkprintapp" OnClick="lnkprintapp_Click" Style="color: white; font-size: 16px; padding-left: -65px" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Print"></asp:LinkButton>
                                                        </span>

                                                        </i>
                                                            <p></p>
                                                        </li>
                                                    </ul>
                                                </section>
                                            </aside>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-9">
                                <div class="row">

                                    <asp:Panel ID="pnlId" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="col-md-12">

                                                <div class="col-md-3">
                                                    <label>Search</label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtIDNo" runat="server" CssClass="form-control" TabIndex="1" Enabled="False" />

                                                        <span class="input-group-addon"><a href="#" title="Search Student for Modification" data-toggle="modal" data-target="#myModal2">
                                                            <asp:Image ID="imgSearch" runat="server" ImageUrl="~/images/search.png" TabIndex="1"
                                                                AlternateText="Search Student by IDNo, Name, Reg. No, Branch, Semester" Style="padding-left: -500px" ToolTip="Search Student by IDNo, Name, Reg. No, Branch, Semester" /></a>
                                                           
                                                        </span>
                                                    </div>
                                                </div>
                                                <div class="col-md-1">
                                                </div>


                                                <div class="col-md-8"></div>
                                            </div>
                                        </div>
                                        <br />
                                        <div id="divstudentidno" runat="server">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group col-md-6">
                                                        <label>Enrollment No.</label>
                                                        <asp:TextBox ID="txtRegNo" CssClass="form-control" runat="server" TabIndex="2" ToolTip="Please Enter Roll No."
                                                            Enabled="false" />
                                                    </div>
                                                    <div class="form-group col-md-6">
                                                        <label><span style="color: red;">&nbsp;</span> Student Full Name</label>
                                                        <asp:TextBox ID="txtStudFullname" CssClass="form-control" runat="server" Enabled="false" TabIndex="3" ToolTip="Please Enter Student Full Name" />
                                                    </div>
                                                </div>
                                            </div>



                                        </div>
                                    </asp:Panel>

                                 
                                </div>
                            </div>

                        </div>
                    </div>

                </div>
            </div>
        </div>
    </asp:Panel>
    <script type="text/javascript" language="javascript">

        function submitPopup(btnsearch) {

            var rbText;
            var searchtxt;
            if (document.getElementById('<%=rbName.ClientID %>').checked == true)
                  rbText = "name";
              else if (document.getElementById('<%=rbIdNo.ClientID %>').checked == true)
                  rbText = "idno";
              else if (document.getElementById('<%=rbBranch.ClientID %>').checked == true)
                  rbText = "branch";
              else if (document.getElementById('<%=rbSemester.ClientID %>').checked == true)
                  rbText = "sem";
              else if (document.getElementById('<%=rbEnrollmentNo.ClientID %>').checked == true)
                rbText = "enrollmentno";
            else if (document.getElementById('<%=rbRegNo.ClientID %>').checked == true)
                rbText = "regno";


    searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;

               __doPostBack(btnsearch, rbText + ',' + searchtxt);

               return true;
           }

           function ClearSearchBox(btncancelmodal) {
               document.getElementById('<%=txtSearch.ClientID %>').value = '';
              __doPostBack(btncancelmodal, '');
              return true;
          }
    </script>
   
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
