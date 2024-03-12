<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Scholarship_Update.aspx.cs" Inherits="Projects_Branch_Change" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <script>
          $(document).ready(function () {
              var table = $('#mytable1').DataTable({
                  responsive: true,
                  lengthChange: true,
                  scrollY: 450,
                  scrollX: true,
                  scrollCollapse: true,
                  paging: false,

                  dom: 'lBfrtip',
                  buttons: [
                      {
                          extend: 'colvis',
                          text: 'Column Visibility',
                          columns: function (idx, data, node) {
                              var arr = [0, 4];
                              if (arr.indexOf(idx) !== -1) {
                                  return false;
                              } else {
                                  return $('#mytable1').DataTable().column(idx).visible();
                              }
                          }
                      },
                      {
                          extend: 'collection',
                          text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                          buttons: [
                                  {
                                      extend: 'copyHtml5',
                                      exportOptions: {
                                          columns: function (idx, data, node) {
                                              var arr = [0, 4];
                                              if (arr.indexOf(idx) !== -1) {
                                                  return false;
                                              } else {
                                                  return $('#mytable1').DataTable().column(idx).visible();
                                              }
                                          },
                                          format: {
                                              body: function (data, column, row, node) {
                                                  var nodereturn;
                                                  if ($(node).find("input:text").length > 0) {
                                                      nodereturn = "";
                                                      nodereturn += $(node).find("input:text").eq(0).val();
                                                  }
                                                  else if ($(node).find("input:checkbox").length > 0) {
                                                      nodereturn = "";
                                                      $(node).find("input:checkbox").each(function () {
                                                          if ($(this).is(':checked')) {
                                                              nodereturn += "On";
                                                          } else {
                                                              nodereturn += "Off";
                                                          }
                                                      });
                                                  }
                                                  else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                      nodereturn = "";
                                                      $(node).find("span").each(function () {
                                                          nodereturn += $(this).html();
                                                      });
                                                  }
                                                  else if ($(node).find("select").length > 0) {
                                                      nodereturn = "";
                                                      $(node).find("select").each(function () {
                                                          var thisOption = $(this).find("option:selected").text();
                                                          if (thisOption !== "Please Select") {
                                                              nodereturn += thisOption;
                                                          }
                                                      });
                                                  }
                                                  else {
                                                      nodereturn = data;
                                                  }
                                                  return nodereturn;
                                              },
                                          },
                                      }
                                  },
                                  {
                                      extend: 'excelHtml5',
                                      exportOptions: {
                                          columns: function (idx, data, node) {
                                              var arr = [0, 4];
                                              if (arr.indexOf(idx) !== -1) {
                                                  return false;
                                              } else {
                                                  return $('#mytable1').DataTable().column(idx).visible();
                                              }
                                          },
                                          format: {
                                              body: function (data, column, row, node) {
                                                  var nodereturn;
                                                  if ($(node).find("input:text").length > 0) {
                                                      nodereturn = "";
                                                      nodereturn += $(node).find("input:text").eq(0).val();
                                                  }
                                                  else if ($(node).find("input:checkbox").length > 0) {
                                                      nodereturn = "";
                                                      $(node).find("input:checkbox").each(function () {
                                                          if ($(this).is(':checked')) {
                                                              nodereturn += "On";
                                                          } else {
                                                              nodereturn += "Off";
                                                          }
                                                      });
                                                  }
                                                  else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                      nodereturn = "";
                                                      $(node).find("span").each(function () {
                                                          nodereturn += $(this).html();
                                                      });
                                                  }
                                                  else if ($(node).find("select").length > 0) {
                                                      nodereturn = "";
                                                      $(node).find("select").each(function () {
                                                          var thisOption = $(this).find("option:selected").text();
                                                          if (thisOption !== "Please Select") {
                                                              nodereturn += thisOption;
                                                          }
                                                      });
                                                  }
                                                  else {
                                                      nodereturn = data;
                                                  }
                                                  return nodereturn;
                                              },
                                          },
                                      }
                                  },
                                  {
                                      extend: 'pdfHtml5',
                                      exportOptions: {
                                          columns: function (idx, data, node) {
                                              var arr = [0, 4];
                                              if (arr.indexOf(idx) !== -1) {
                                                  return false;
                                              } else {
                                                  return $('#mytable1').DataTable().column(idx).visible();
                                              }
                                          },
                                          format: {
                                              body: function (data, column, row, node) {
                                                  var nodereturn;
                                                  if ($(node).find("input:text").length > 0) {
                                                      nodereturn = "";
                                                      nodereturn += $(node).find("input:text").eq(0).val();
                                                  }
                                                  else if ($(node).find("input:checkbox").length > 0) {
                                                      nodereturn = "";
                                                      $(node).find("input:checkbox").each(function () {
                                                          if ($(this).is(':checked')) {
                                                              nodereturn += "On";
                                                          } else {
                                                              nodereturn += "Off";
                                                          }
                                                      });
                                                  }
                                                  else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                      nodereturn = "";
                                                      $(node).find("span").each(function () {
                                                          nodereturn += $(this).html();
                                                      });
                                                  }
                                                  else if ($(node).find("select").length > 0) {
                                                      nodereturn = "";
                                                      $(node).find("select").each(function () {
                                                          var thisOption = $(this).find("option:selected").text();
                                                          if (thisOption !== "Please Select") {
                                                              nodereturn += thisOption;
                                                          }
                                                      });
                                                  }
                                                  else {
                                                      nodereturn = data;
                                                  }
                                                  return nodereturn;
                                              },
                                          },
                                      }
                                  },
                          ]
                      }
                  ],
                  "bDestroy": true,
              });
          });
          var parameter = Sys.WebForms.PageRequestManager.getInstance();
          parameter.add_endRequest(function () {
              $(document).ready(function () {
                  var table = $('#mytable1').DataTable({
                      responsive: true,
                      lengthChange: true,
                      scrollY: 450,
                      scrollX: true,
                      scrollCollapse: true,
                      paging: false,

                      dom: 'lBfrtip',
                      buttons: [
                          {
                              extend: 'colvis',
                              text: 'Column Visibility',
                              columns: function (idx, data, node) {
                                  var arr = [0, 4];
                                  if (arr.indexOf(idx) !== -1) {
                                      return false;
                                  } else {
                                      return $('#mytable1').DataTable().column(idx).visible();
                                  }
                              }
                          },
                          {
                              extend: 'collection',
                              text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                              buttons: [
                                      {
                                          extend: 'copyHtml5',
                                          exportOptions: {
                                              columns: function (idx, data, node) {
                                                  var arr = [0, 4];
                                                  if (arr.indexOf(idx) !== -1) {
                                                      return false;
                                                  } else {
                                                      return $('#mytable1').DataTable().column(idx).visible();
                                                  }
                                              },
                                              format: {
                                                  body: function (data, column, row, node) {
                                                      var nodereturn;
                                                      if ($(node).find("input:text").length > 0) {
                                                          nodereturn = "";
                                                          nodereturn += $(node).find("input:text").eq(0).val();
                                                      }
                                                      else if ($(node).find("input:checkbox").length > 0) {
                                                          nodereturn = "";
                                                          $(node).find("input:checkbox").each(function () {
                                                              if ($(this).is(':checked')) {
                                                                  nodereturn += "On";
                                                              } else {
                                                                  nodereturn += "Off";
                                                              }
                                                          });
                                                      }
                                                      else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                          nodereturn = "";
                                                          $(node).find("span").each(function () {
                                                              nodereturn += $(this).html();
                                                          });
                                                      }
                                                      else if ($(node).find("select").length > 0) {
                                                          nodereturn = "";
                                                          $(node).find("select").each(function () {
                                                              var thisOption = $(this).find("option:selected").text();
                                                              if (thisOption !== "Please Select") {
                                                                  nodereturn += thisOption;
                                                              }
                                                          });
                                                      }
                                                      else {
                                                          nodereturn = data;
                                                      }
                                                      return nodereturn;
                                                  },
                                              },
                                          }
                                      },
                                      {
                                          extend: 'excelHtml5',
                                          exportOptions: {
                                              columns: function (idx, data, node) {
                                                  var arr = [0, 4];
                                                  if (arr.indexOf(idx) !== -1) {
                                                      return false;
                                                  } else {
                                                      return $('#mytable1').DataTable().column(idx).visible();
                                                  }
                                              },
                                              format: {
                                                  body: function (data, column, row, node) {
                                                      var nodereturn;
                                                      if ($(node).find("input:text").length > 0) {
                                                          nodereturn = "";
                                                          nodereturn += $(node).find("input:text").eq(0).val();
                                                      }
                                                      else if ($(node).find("input:checkbox").length > 0) {
                                                          nodereturn = "";
                                                          $(node).find("input:checkbox").each(function () {
                                                              if ($(this).is(':checked')) {
                                                                  nodereturn += "On";
                                                              } else {
                                                                  nodereturn += "Off";
                                                              }
                                                          });
                                                      }
                                                      else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                          nodereturn = "";
                                                          $(node).find("span").each(function () {
                                                              nodereturn += $(this).html();
                                                          });
                                                      }
                                                      else if ($(node).find("select").length > 0) {
                                                          nodereturn = "";
                                                          $(node).find("select").each(function () {
                                                              var thisOption = $(this).find("option:selected").text();
                                                              if (thisOption !== "Please Select") {
                                                                  nodereturn += thisOption;
                                                              }
                                                          });
                                                      }
                                                      else {
                                                          nodereturn = data;
                                                      }
                                                      return nodereturn;
                                                  },
                                              },
                                          }
                                      },
                                      {
                                          extend: 'pdfHtml5',
                                          exportOptions: {
                                              columns: function (idx, data, node) {
                                                  var arr = [0, 4];
                                                  if (arr.indexOf(idx) !== -1) {
                                                      return false;
                                                  } else {
                                                      return $('#mytable1').DataTable().column(idx).visible();
                                                  }
                                              },
                                              format: {
                                                  body: function (data, column, row, node) {
                                                      var nodereturn;
                                                      if ($(node).find("input:text").length > 0) {
                                                          nodereturn = "";
                                                          nodereturn += $(node).find("input:text").eq(0).val();
                                                      }
                                                      else if ($(node).find("input:checkbox").length > 0) {
                                                          nodereturn = "";
                                                          $(node).find("input:checkbox").each(function () {
                                                              if ($(this).is(':checked')) {
                                                                  nodereturn += "On";
                                                              } else {
                                                                  nodereturn += "Off";
                                                              }
                                                          });
                                                      }
                                                      else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                          nodereturn = "";
                                                          $(node).find("span").each(function () {
                                                              nodereturn += $(this).html();
                                                          });
                                                      }
                                                      else if ($(node).find("select").length > 0) {
                                                          nodereturn = "";
                                                          $(node).find("select").each(function () {
                                                              var thisOption = $(this).find("option:selected").text();
                                                              if (thisOption !== "Please Select") {
                                                                  nodereturn += thisOption;
                                                              }
                                                          });
                                                      }
                                                      else {
                                                          nodereturn = data;
                                                      }
                                                      return nodereturn;
                                                  },
                                              },
                                          }
                                      },
                              ]
                          }
                      ],
                      "bDestroy": true,
                  });
              });
          });
    </script>

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>
     <div>

        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updbranchchange"
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

    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <asp:UpdatePanel ID="updbranchchange" runat="server">
        <ContentTemplate>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                     <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                    <%--<h3 class="box-title">Scholarship</h3>--%>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                           <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                      <asp:Label ID="lblACAcademicSession" runat="server" Font-Bold="true"></asp:Label>
                                    <label> Intake</label>
                                </div>
                                <asp:DropDownList ID="ddlBatch" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1"  AppendDataBoundItems="true" ClientIDMode="Static"> 
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                              
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBatch"
                                                         Display="None" ErrorMessage="Please Select  Intake" InitialValue="0" ValidationGroup="Uploade" />
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlBatch"
                                                        Display="None" ErrorMessage="Please Select  Intake" InitialValue="0" ValidationGroup="Conform" />
                            </div>--%>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                      <%--<asp:Label ID="lblFaculty" runat="server" Font-Bold="true"></asp:Label>--%>
                                    <label>Faculty</label> 	
                                </div>
                                <asp:DropDownList ID="ddlFaculty" runat="server" CssClass="form-control" data-select2-enable="true" ClientIDMode="Static"  TabIndex="2" AppendDataBoundItems="true" >
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                           
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlFaculty"
                                                        Display="None" ErrorMessage="Please Select Faculty" InitialValue="0"
                                                        ValidationGroup="Uploade" />
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlFaculty"
                                                        Display="None" ErrorMessage="Please Select Faculty" InitialValue="0"
                                                        ValidationGroup="Conform" />
                                                       
                            </div>
                           
                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" >
                                <sup>*</sup>
                                <%--<asp:Label ID="lblDYUploadExcelFile" runat="server" Font-Bold="true"></asp:Label>--%>
                                <label>Upload Exel File</label>
                                <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Please Select file to Import" TabIndex="4" />
                          
                                <%--<asp:RequiredFieldValidator ID="rfvintake" runat="server" ControlToValidate="FileUpload1"
                                    Display="None" ErrorMessage="Please select file to upload." ValidationGroup="Uploade"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                              
                            </div>

                           
                        </div>
                        <div class="form-group col-md-12">
                            <asp:Label ID="lblTotalMsg" Style="font-weight: bold; color: red;" runat="server" TabIndex="3"></asp:Label>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:LinkButton ID="btnExel" runat="server" CssClass="btn btn-outline-primary" OnClick="btnExel_Click" TabIndex="5">Excel Sheet</asp:LinkButton>
                        <%--<asp:Button  ID="btnUploade" runat="server" CssClass="btn btn-outline-primary" OnClick="btnUploade_Click1"  ValidationGroup="Uploade" TabIndex ="4" Text="Upload" />--%>
                        <asp:LinkButton ID="btnUploade" runat="server" CssClass="btn btn-outline-primary" OnClick="btnUploade_Click"  TabIndex ="6" ValidationGroup="Uploade" >Upload</asp:LinkButton>
                        
                        <asp:LinkButton ID="btnConform" runat="server" CssClass="btn btn-outline-primary" OnClick="btnConform_Click" TabIndex="7" ValidationGroup="Uploade">Confirm</asp:LinkButton>
                        <asp:Button  ID="btnview" runat="server" CssClass="btn btn-outline-primary" OnClick="btnview_Click" TabIndex ="4" Text="View" ValidationGroup="Uploade" />
                        <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" TabIndex="8" OnClick="btnCancel_Click" >Cancel</asp:LinkButton>
                         
                         <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Uploade" />
                         <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Conform" />
                    </div>

                       <div class="col-12">
                                <asp:Panel ID="Panel2" runat="server">
                                    <asp:ListView ID="lvScholarship" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                            </div>
                                            <div class="table-responsive" style="height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                <table class="table table-striped table-bordered nowrap mb-0" style="width: 100%;" id="tableOnline">
                                                    <%--  <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="#mytable1">--%>
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>
                                                                <asp:CheckBox ID="chkAll" runat="server" OnClick="checkAllCheckbox(this)"  /></th>
                                                            <th>Student ID</th>
                                                            <th>Student Name</th>
                                                             <th>Faculty</th>
                                                            <th>Semester</th>
                                                            <th>Program</th>
                                                            <th>Amount</th>
                                                            <th>Status</th>

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

                                                    <asp:CheckBox ID="chkapp" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblregno" runat="server" Text='<%# Eval("REGNO") %>'></asp:Label>
                                                     <asp:Label ID="lblIdno" runat="server" Text='<%# Eval("IDNO") %>' Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStudentname" runat="server" Text='<%# Eval("NAME_WITH_INITIAL") %>' ></asp:Label>
                                                </td>
                                                 <td>
                                                    <asp:Label ID="lblFaculty" runat="server" Text='<%# Eval("COLLEGE_NAME") %>' ></asp:Label>
                                                </td>
                                                 <td>
                                                     <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTERNAME") %>' ></asp:Label>
                                                    <%--<asp:Label ID="lblSemesterno" runat="server" Text='<%# Eval("SEMESTERNO") %>' Visible="false" ></asp:Label>--%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblProgram" runat="server" Text='<%# Eval("PROGRAM") %>'></asp:Label>  
                                                    <asp:Label ID="lbldegreeNo" runat="server" Text='<%# Eval("DEGREENO") %>' Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                     <asp:TextBox runat="server" ID="txtAmount" onblur="return IsNumeric(this);" TEXT='<%#Eval("TRAN_AMT") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                   <asp:Label ID="lblRecon" runat="server" Text='<%# Eval("RECON") %>'></asp:Label>
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </asp:Panel>

                            </div>

                    <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvDuplicaterecord" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Duplicate Record list</h5>
                                            </div>
                                                 <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <%--  <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="#mytable1">--%>
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Student ID</th>
                                                            <th>Student Name</th>
                                                             <th>Faculty</th>
                                                            <th>Semester</th>
                                                            <th>Program</th>
                                                            <th>Amount</th>
                                                            <th>Status</th>

                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                            
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                               
                                                <td>
                                                    <asp:Label ID="lblregno" runat="server" Text='<%# Eval("REGNO") %>'></asp:Label>
                                                     <asp:Label ID="lblIdno" runat="server" Text='<%# Eval("IDNO") %>' Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStudentname" runat="server" Text='<%# Eval("NAME_WITH_INITIAL") %>' ></asp:Label>
                                                </td>
                                                 <td>
                                                    <asp:Label ID="lblFaculty" runat="server" Text='<%# Eval("COLLEGE_NAME") %>' ></asp:Label>
                                                </td>
                                                 <td>
                                                     <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTERNAME") %>' ></asp:Label>
                                                    <%--<asp:Label ID="lblSemesterno" runat="server" Text='<%# Eval("SEMESTERNO") %>' Visible="false" ></asp:Label>--%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblProgram" runat="server" Text='<%# Eval("PROGRAM") %>'></asp:Label>  
                                                    <asp:Label ID="lbldegreeNo" runat="server" Text='<%# Eval("DEGREENO") %>' Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                     <asp:Label runat="server" ID="lblAmount" TEXT='<%#Eval("TRAN_AMT") %>'></asp:Label>
                                                </td>
                                                <td>
                                                   <asp:Label ID="lblRecon" runat="server" Text="Duplicate Record"></asp:Label>
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
</ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUploade" />
           <asp:PostBackTrigger ControlID="btnExel"  />
        </Triggers>
</asp:UpdatePanel>

     <script type="text/javascript" language="javascript">

         function checkAllCheckbox(headchk) {
             var frm = document.forms[0]
             for (i = 0; i < document.forms[0].elements.length; i++) {
                 var e = frm.elements[i];
                 var s = e.name.split("ctl00$ContentPlaceHolder1$lvScholarship$ctrl");
                 var b = 'ctl00$ContentPlaceHolder1$lvScholarship$ctrl';
                 var g = b + s[1];
                 if (e.name == g) {
                     if (headchk.checked == true)
                         e.checked = true;
                     else
                         e.checked = false;
                 }
             }
         }

    </script>
   <script type="text/javascript">
       function IsNumeric(txt) {
           var ValidChars = "0123456789.";
           var num = true;
           var mChar;
           cnt = 0

           for (i = 0; i < txt.value.length && num == true; i++) {
               mChar = txt.value.charAt(i);

               if (ValidChars.indexOf(mChar) == -1) {
                   num = false;
                   txt.value = '';
                   alert("Please enter Numeric values only")
                   txt.select();
                   txt.focus();
               }
           }
           return num;
       }
    </script>
</asp:Content>

