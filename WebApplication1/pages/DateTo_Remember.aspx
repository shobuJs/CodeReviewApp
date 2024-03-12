<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DateTo_Remember.aspx.cs" Inherits="ACADEMIC_DateTo_Remember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>

        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updsetting"
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



    <asp:UpdatePanel ID="updsetting" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><span>Application Date To Remember</span></h3>
                        </div>

                        <div class="box-body">
                            <%--   <div class="col-12">
                                <div class="row">
                                  
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" >
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Heading</label>
                                        </div>
                                        <asp:TextBox ID="txtHeading" runat="server" CssClass="form-control" type="text" ClientIDMode="Static"  TabIndex="1"/>
                                    </div>
                                        
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" >
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Date</label>
                                        </div>
                                        <asp:TextBox ID="txtdate" runat="server" CssClass="form-control" type="text" ClientIDMode="Static" TabIndex="2"/>
                                    </div>
                                </div>
                            </div>--%>

                            <div class="col-12">
                                <asp:Panel ID="Panel2" runat="server">
                                    <asp:ListView ID="lvBankDetails" runat="server" Visible="true">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                            </div>
                                            <table class="table table-striped table-bordered " style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Application</th>
                                                        <th>Date  (Formate=29th September 2022)</th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("HEADING")%>
                                                    <asp:Label runat="server" ID="lvlHedNo" Text='<%# Eval("APTIDATE_NO") %>' Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtDateAplli" Text='<%# Eval("DATE_OF_REMEMBER") %>' CssClass="form-control"></asp:TextBox></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </asp:Panel>

                            </div>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Maximum Limit Of Applied Program </label>
                                        </div>
                                        <asp:TextBox ID="TxtProgmAplied" runat="server" CssClass="form-control" MaxLength="2" onkeyup="IsNumeric(this);"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 btn-footer">
                            <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" ClientIDMode="Static" TabIndex="5" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                            <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" TabIndex="6" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>

        var summary = "";
        $(function () {

            $('#btnSubmit').click(function () {
                localStorage.setItem("currentId", "#btnSubmit,Submit");
                debugger;
                ShowLoader('#btnSubmit');
                if ($('#txtbankname').val() == "")
                    summary += '<br>Please Enter Bank Name';
                if ($('#txtbankCode').val() == "")
                    summary += '<br>Please Enter Bank Code';
                if ($('#txtBankAddress').val() == "")
                    summary += '<br>Please Enter Bank Address';
                if ($('#txtBankAccount').val() == "")
                    summary += '<br>Please Enter Bank Account';


                if (summary != "") {
                    customAlert(summary);
                    summary = "";
                    return false
                }

            });
        });


    </script>
    <script type="text/javascript">
        function IsNumeric(txt) {
            var ValidChars = "0123456789";
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
    <script>
        function IsNumeric(textbox) {
            if (textbox != null && textbox.value != "") {
                if (isNaN(textbox.value)) {
                    alert("Enter Only Numeric Value")
                    document.getElementById(textbox.id).value = "";
                }
            }
        }
    </script>
</asp:Content>


