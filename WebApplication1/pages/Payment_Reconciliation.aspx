<%@ page title="" language="C#" masterpagefile="~/SiteMasterPage.master" autoeventwireup="true" codefile="Payment_Reconciliation.aspx.cs" inherits="ACADEMIC_Payment_Reconcilation" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>
    <style>

.loader-container {
    transform: translate(50%, 50%);
    position: relative;
    /* width: 75px; */
    height: 100px;
}
.loader-container__bar {
  position: absolute;
  bottom: 0;
  width: 10px;
  height: 50%;
  background: #00007b;
  -webkit-transform-origin: center bottom;
          transform-origin: center bottom;
  -webkit-box-shadow: 1px 1px 0 rgba(0, 0, 0, 0.2);
          box-shadow: 1px 1px 0 rgba(0, 0, 0, 0.2);
}

.loader-container__bar:nth-child(1) {
  left: 0px;
  -webkit-transform: scale(1, 0.2);
          transform: scale(1, 0.2);
  -webkit-animation: barUp1 5s infinite;
          animation: barUp1 5s infinite;
}

.loader-container__bar:nth-child(2) {
  left: 15px;
  -webkit-transform: scale(1, 0.4);
          transform: scale(1, 0.4);
  -webkit-animation: barUp2 5s infinite;
          animation: barUp2 5s infinite;
}

.loader-container__bar:nth-child(3) {
  left: 30px;
  -webkit-transform: scale(1, 0.6);
          transform: scale(1, 0.6);
  -webkit-animation: barUp3 5s infinite;
          animation: barUp3 5s infinite;
}

.loader-container__bar:nth-child(4) {
  left: 45px;
  -webkit-transform: scale(1, 0.8);
          transform: scale(1, 0.8);
  -webkit-animation: barUp4 5s infinite;
          animation: barUp4 5s infinite;
}

.loader-container__bar:nth-child(5) {
  left: 60px;
  -webkit-transform: scale(1, 1);
          transform: scale(1, 1);
  -webkit-animation: barUp5 5s infinite;
          animation: barUp5 5s infinite;
}

.loader-container__ball {
  position: absolute;
  bottom: 12px;
  left: 0;
  width: 10px;
  height: 10px;
  background: #00007b;
  border-radius: 50%;
  -webkit-animation: ball 5s infinite;
          animation: ball 5s infinite;
}

@-webkit-keyframes ball {
  0% {
    -webkit-transform: translate(0, 0);
            transform: translate(0, 0);
    background: #ff7300;
  }
  5% {
    -webkit-transform: translate(10px, -14px);
            transform: translate(10px, -14px);
    background: #ff7300;
  }
  10% {
    -webkit-transform: translate(15px, -10px);
            transform: translate(15px, -10px);
    background: #ff7300;
  }
  15% {
    -webkit-transform: translate(25px, -24px);
            transform: translate(25px, -24px);
    background: #ff7300;
  }
  20% {
    -webkit-transform: translate(30px, -20px);
            transform: translate(30px, -20px);
    background: #ff7300;
  }
  25% {
    -webkit-transform: translate(38px, -34px);
            transform: translate(38px, -34px);
    background: #ff7300;
  }
  30% {
    -webkit-transform: translate(45px, -30px);
            transform: translate(45px, -30px);
    background: #ff7300;
  }
  35% {
    -webkit-transform: translate(53px, -44px);
            transform: translate(53px, -44px);
    background: #ff7300;
  }
  40% {
    -webkit-transform: translate(60px, -40px);
            transform: translate(60px, -40px);
    background: #ff7300;
  }
  50% {
    -webkit-transform: translate(60px, 0);
            transform: translate(60px, 0);
    background: #00007b;
  }
  55% {
    -webkit-transform: translate(53px, -14px);
            transform: translate(53px, -14px);
    background: #00007b;
  }
  60% {
    -webkit-transform: translate(45px, -10px);
            transform: translate(45px, -10px);
    background: #00007b;
  }
  65% {
    -webkit-transform: translate(37px, -24px);
            transform: translate(37px, -24px);
    background: #00007b;
  }
  70% {
    -webkit-transform: translate(30px, -20px);
            transform: translate(30px, -20px);
    background: #00007b;
  }
  75% {
    -webkit-transform: translate(22px, -34px);
            transform: translate(22px, -34px);
    background: #00007b;
  }
  80% {
    -webkit-transform: translate(15px, -30px);
            transform: translate(15px, -30px);
    background: #00007b;
  }
  85% {
    -webkit-transform: translate(7px, -44px);
            transform: translate(7px, -44px);
    background: #00007b;
  }
  90% {
    -webkit-transform: translate(0, -40px);
            transform: translate(0, -40px);
    background: #00007b;
  }
  100% {
    -webkit-transform: translate(0, 0);
            transform: translate(0, 0);
    background: #00007b;
  }
}

@keyframes ball {
  0% {
    -webkit-transform: translate(0, 0);
            transform: translate(0, 0);
    background: #ff7300;
  }
  5% {
    -webkit-transform: translate(10px, -14px);
            transform: translate(10px, -14px);
    background: #ff7300;
  }
  10% {
    -webkit-transform: translate(15px, -10px);
            transform: translate(15px, -10px);
    background: #ff7300;
  }
  15% {
    -webkit-transform: translate(25px, -24px);
            transform: translate(25px, -24px);
    background: #ff7300;
  }
  20% {
    -webkit-transform: translate(30px, -20px);
            transform: translate(30px, -20px);
    background: #ff7300;
  }
  25% {
    -webkit-transform: translate(38px, -34px);
            transform: translate(38px, -34px);
    background: #ff7300;
  }
  30% {
    -webkit-transform: translate(45px, -30px);
            transform: translate(45px, -30px);
    background: #ff7300;
  }
  35% {
    -webkit-transform: translate(53px, -44px);
            transform: translate(53px, -44px);
    background: #ff7300;
  }
  40% {
    -webkit-transform: translate(60px, -40px);
            transform: translate(60px, -40px);
    background: #ff7300;
  }
  50% {
    -webkit-transform: translate(60px, 0);
            transform: translate(60px, 0);
    background: #00007b;
  }
  55% {
    -webkit-transform: translate(53px, -14px);
            transform: translate(53px, -14px);
    background: #00007b;
  }
  60% {
    -webkit-transform: translate(45px, -10px);
            transform: translate(45px, -10px);
    background: #00007b;
  }
  65% {
    -webkit-transform: translate(37px, -24px);
            transform: translate(37px, -24px);
    background: #00007b;
  }
  70% {
    -webkit-transform: translate(30px, -20px);
            transform: translate(30px, -20px);
    background: #00007b;
  }
  75% {
    -webkit-transform: translate(22px, -34px);
            transform: translate(22px, -34px);
    background: #00007b;
  }
  80% {
    -webkit-transform: translate(15px, -30px);
            transform: translate(15px, -30px);
    background: #00007b;
  }
  85% {
    -webkit-transform: translate(7px, -44px);
            transform: translate(7px, -44px);
    background: #00007b;
  }
  90% {
    -webkit-transform: translate(0, -40px);
            transform: translate(0, -40px);
    background: #00007b;
  }
  100% {
    -webkit-transform: translate(0, 0);
            transform: translate(0, 0);
    background: #00007b;
  }
}

@-webkit-keyframes barUp1 {
  0% {
    -webkit-transform: scale(1, 0.2);
            transform: scale(1, 0.2);
    background: #00007b;
  }
  40% {
    -webkit-transform: scale(1, 0.2);
            transform: scale(1, 0.2);
    background: #00007b;
  }
  50% {
    -webkit-transform: scale(1, 1);
            transform: scale(1, 1);
    background: #ff7300;
  }
  90% {
    -webkit-transform: scale(1, 1);
            transform: scale(1, 1);
    background: #ff7300;
  }
  100% {
    -webkit-transform: scale(1, 0.2);
            transform: scale(1, 0.2);
    background: #ff7300;
  }
}

@keyframes barUp1 {
  0% {
    -webkit-transform: scale(1, 0.2);
            transform: scale(1, 0.2);
    background: #00007b;
  }
  40% {
    -webkit-transform: scale(1, 0.2);
            transform: scale(1, 0.2);
    background: #00007b;
  }
  50% {
    -webkit-transform: scale(1, 1);
            transform: scale(1, 1);
    background: #ff7300;
  }
  90% {
    -webkit-transform: scale(1, 1);
            transform: scale(1, 1);
    background: #ff7300;
  }
  100% {
    -webkit-transform: scale(1, 0.2);
            transform: scale(1, 0.2);
    background: #ff7300;
  }
}

@-webkit-keyframes barUp2 {
  0% {
    -webkit-transform: scale(1, 0.4);
            transform: scale(1, 0.4);
    background: #00007b;
  }
  40% {
    -webkit-transform: scale(1, 0.4);
            transform: scale(1, 0.4);
    background: #00007b;
  }
  50% {
    -webkit-transform: scale(1, 0.8);
            transform: scale(1, 0.8);
    background: #ff7300;
  }
  90% {
    -webkit-transform: scale(1, 0.8);
            transform: scale(1, 0.8);
    background: #ff7300;
  }
  100% {
    -webkit-transform: scale(1, 0.4);
            transform: scale(1, 0.4);
    background: #ff7300;
  }
}

@keyframes barUp2 {
  0% {
    -webkit-transform: scale(1, 0.4);
            transform: scale(1, 0.4);
    background: #00007b;
  }
  40% {
    -webkit-transform: scale(1, 0.4);
            transform: scale(1, 0.4);
    background: #00007b;
  }
  50% {
    -webkit-transform: scale(1, 0.8);
            transform: scale(1, 0.8);
    background: #ff7300;
  }
  90% {
    -webkit-transform: scale(1, 0.8);
            transform: scale(1, 0.8);
    background: #ff7300;
  }
  100% {
    -webkit-transform: scale(1, 0.4);
            transform: scale(1, 0.4);
    background: #ff7300;
  }
}

@-webkit-keyframes barUp3 {
  0% {
    -webkit-transform: scale(1, 0.6);
            transform: scale(1, 0.6);
    background: #00007b;
  }
  40% {
    background: #00007b;
  }
  50% {
    background: #ff7300;
  }
  100% {
    -webkit-transform: scale(1, 0.6);
            transform: scale(1, 0.6);
    background: #ff7300;
  }
}

@keyframes barUp3 {
  0% {
    -webkit-transform: scale(1, 0.6);
            transform: scale(1, 0.6);
    background: #00007b;
  }
  40% {
    background: #00007b;
  }
  50% {
    background: #ff7300;
  }
  100% {
    -webkit-transform: scale(1, 0.6);
            transform: scale(1, 0.6);
    background: #ff7300;
  }
}

@-webkit-keyframes barUp4 {
  0% {
    -webkit-transform: scale(1, 0.8);
            transform: scale(1, 0.8);
    background: #00007b;
  }
  40% {
    -webkit-transform: scale(1, 0.8);
            transform: scale(1, 0.8);
    background: #00007b;
  }
  50% {
    -webkit-transform: scale(1, 0.4);
            transform: scale(1, 0.4);
    background: #ff7300;
  }
  90% {
    -webkit-transform: scale(1, 0.4);
            transform: scale(1, 0.4);
    background: #ff7300;
  }
  100% {
    -webkit-transform: scale(1, 0.8);
            transform: scale(1, 0.8);
    background: #ff7300;
  }
}

@keyframes barUp4 {
  0% {
    -webkit-transform: scale(1, 0.8);
            transform: scale(1, 0.8);
    background: #00007b;
  }
  40% {
    -webkit-transform: scale(1, 0.8);
            transform: scale(1, 0.8);
    background: #00007b;
  }
  50% {
    -webkit-transform: scale(1, 0.4);
            transform: scale(1, 0.4);
    background: #ff7300;
  }
  90% {
    -webkit-transform: scale(1, 0.4);
            transform: scale(1, 0.4);
    background: #ff7300;
  }
  100% {
    -webkit-transform: scale(1, 0.8);
            transform: scale(1, 0.8);
    background: #ff7300;
  }
}

@-webkit-keyframes barUp5 {
  0% {
    -webkit-transform: scale(1, 1);
            transform: scale(1, 1);
    background: #00007b;
  }
  40% {
    -webkit-transform: scale(1, 1);
            transform: scale(1, 1);
    background: #00007b;
  }
  50% {
    -webkit-transform: scale(1, 0.2);
            transform: scale(1, 0.2);
    background: #ff7300;
  }
  90% {
    -webkit-transform: scale(1, 0.2);
            transform: scale(1, 0.2);
    background: #ff7300;
  }
  100% {
    -webkit-transform: scale(1, 1);
            transform: scale(1, 1);
    background: #ff7300;
  }
}

@keyframes barUp5 {
  0% {
    -webkit-transform: scale(1, 1);
            transform: scale(1, 1);
    background: #00007b;
  }
  40% {
    -webkit-transform: scale(1, 1);
            transform: scale(1, 1);
    background: #00007b;
  }
  50% {
    -webkit-transform: scale(1, 0.2);
            transform: scale(1, 0.2);
    background: #ff7300;
  }
  90% {
    -webkit-transform: scale(1, 0.2);
            transform: scale(1, 0.2);
    background: #ff7300;
  }
  100% {
    -webkit-transform: scale(1, 1);
            transform: scale(1, 1);
    background: #ff7300;
  }
}
    </style>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <label id="lblDynamicPageTitle" runat="server"></label>
                    </h3>
                </div>
                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1" onclick="DynamicTabRefresh()">Application</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2" onclick="DynamicTabRefresh()">Admission Proper</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_3" onclick="DynamicTabRefresh()">Higher Semester</a>
                            </li>
                        </ul>

                        <div class="col-12 mt-3">
                            <div class="col-12">
                                <div class="form-group">

                                    <input type="radio" id="rdOffline" tabindex="0" value="1" name="rdtype" onclick="return RdType(this)" />Offline &nbsp;&nbsp;&nbsp;
                                                <input type="radio" id="rdOnline" tabindex="0" value="2" name="rdtype" onclick="return RdType(this)" />Online 
                                                
                                </div>
                            </div>
                            <div class="row d-none" id="MainFields">

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12 d-none" id="divSession">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><span id="Span6">Session Name</span></label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true" TabIndex="0">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><span id="lblCourses">Study Level</span></label>
                                        </div>
                                        <asp:DropDownList ID="ddlStudyLevel" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true" TabIndex="0">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12" id="divIntake">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><span id="Span1">Intake</span></label>
                                        </div>
                                        <asp:DropDownList ID="ddlIntake" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true" TabIndex="0">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12 d-none" id="divSemester">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label><span id="Span4">Semester</span></label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true" TabIndex="0">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12 d-none" id="divProgram">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label><span id="Span5">Program</span></label>
                                        </div>
                                        <asp:ListBox ID="ddlProgram" runat="server" TabIndex="0" AppendDataBoundItems="true" CssClass="form-control multi-select-demo" SelectionMode="multiple"></asp:ListBox>
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12" id="divPaymentStatus">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><span id="Span2">Payment Status</span></label>
                                        </div>
                                        <asp:DropDownList ID="ddlPaymentStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true" TabIndex="0">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12" id="divBank">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label><span id="Span3">Bank</span></label>
                                        </div>
                                        <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true" TabIndex="0">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="tab_1">
                                <div class="col-12 mt-3">
                                    <!-- Buttons -->
                                    <div class="text-center mt-2 mb-3">
                                        <input type="button" value="Show" id="btnShow" class="btn btn-outline-info" tabindex="0" />
                                        <input type="button" value="Submit" id="btnSubmit" class="btn btn-outline-primary d-none" tabindex="0" />
                                        <input type="button" value="Cancel" id="btnCancel" class="btn btn-outline-danger" tabindex="0" />
                                    </div>
                                </div>
                            </div>

                            <div class="tab-pane fade" id="tab_2">
                                <div class="col-12 mt-3">
                                    <!-- Buttons -->
                                    <div class="text-center mt-2 mb-3">
                                        <input type="button" value="Show" id="btnShowFreshman" class="btn btn-outline-info" tabindex="0" />
                                        <input type="button" value="Submit" id="btnSubmitfreshmen" class="btn btn-outline-primary d-none" tabindex="0" />
                                        <input type="button" value="Cancel" id="btnFreshmenCancel" class="btn btn-outline-danger" tabindex="0" />
                                    </div>
                                </div>
                            </div>

                            <div class="tab-pane fade" id="tab_3">
                                <div class="col-12 mt-3">
                                    <!-- Buttons -->
                                    <div class="text-center mt-2 mb-3">
                                        <input type="button" value="Show" id="btnShowContinuing" class="btn btn-outline-info" tabindex="0" />
                                        <input type="button" value="Submit" id="btnSubmitContinuing" class="btn btn-outline-primary d-none" tabindex="0" />
                                        <input type="button" value="Cancel" id="btnContinuingCancel" class="btn btn-outline-danger" tabindex="0" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-12 mt-3 d-none" id="DivDynamicSwitch">
                            <div class="sub-heading">
                                <h5>Student List</h5>
                            </div>
                            <div class="table-responsive" style="max-height: 480px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="TblDynamic">
                                </table>
                            </div>
                        </div>

                        <div class="col-12 mt-3 d-none" id="DivDynamicTable">
                            <div class="sub-heading">
                                <h5>Student Count List For Application</h5>
                            </div>
                            <div class="table table-responsive">
                                <table class="table table-striped table-bordered nowrap display" style="width: 100%;" id="TblDynamicTabTableCount">
                                </table>
                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModal22" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" style="margin-top: -25px">
                <div class="modal-body">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" style="margin-top: -18px">x</button>
                    </div>
                    <img id="blobImage" src="" alt="Blob Image" width="100%" height="500px" class="d-none">
                    <embed id="blobPdf" src="" type="application/pdf" width="100%" height="500px" class="d-none">
                    <div class="modal-footer" style="height: 0px">
                        <button type="button" style="margin-top: -10px" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="<%=Page.ResolveClientUrl("~/js/PaymentReconciliation.js")%>"></script>
</asp:Content>

