//===============================================//
// MODULE NAME   : RFC ERP 
// PAGE NAME     : Rubrics
// CREATION DATE : 15-07-2023
// CREATED BY    : Ekansh Moundekar
// Modified BY   : Ekansh Moundekar
// Modified Date : 12-02-2024
//===============================================//
var liveurl = "../../../../";
var localurl = "../../";
var GlobalValue = 0;
var GlobalEdit = 0;
var GlobalExam =0;
var GlobalModality =0;
var GlobalCourse = 0;
var globalSem = 0;
$(document).ready(function () {
    $('.multi-select-demo').multiselect({
        includeSelectAllOption: true,
        maxHeight: 200,
        enableFiltering: true,
        filterPlaceholder: 'Search',
        enableCaseInsensitiveFiltering: true,
    });
});
$("#ctl00_ContentPlaceHolder1_ddlExamType").change(function(e){
    try{
        
        if(GlobalEdit!=1){
            CheckStatus = 2;
            var College = $("#ctl00_ContentPlaceHolder1_ddlCollege").val();
            var PatternNo = $("#ctl00_ContentPlaceHolder1_ddlPatternNo").val();
            var Sessionno = $("#ctl00_ContentPlaceHolder1_ddlSession").val();
            var ExamTypeID = $("#ctl00_ContentPlaceHolder1_ddlExamType").val();
            var Obj = {};
            var count = 0;
            //$("#ctl00_ContentPlaceHolder1_ddlCourses").empty();
            //Obj.DropDownNo = PatternNo;
            //Obj.Command_Type = CheckStatus;
            //Obj.SessionNo = Sessionno;
            //Obj.CollegeID = College;
            //$.ajax({
            //    url: localurl+"EXAM/GradeEncodingPeriod.aspx/BindDropDown",
            //    type: "POST",
            //    data: JSON.stringify(Obj),
            //    dataType: "json",
            //    beforeSend: function () { $("[id*=preloader]").show(); },
            //    complete: function () { $("[id*=preloader]").hide(); },
            //    contentType: "application/json;charset=utf-8",
            //    success: function (res) {
            //        $('#ctl00_ContentPlaceHolder1_ddlCourses').empty();
            //        $.each(res.d, function (key, value) {
            //            var option = $("<option></option>").val(value.Coursenno).html(value.CoursesName);
            //            $("#ctl00_ContentPlaceHolder1_ddlCourses").append(option);
            //        });
            //        $("#ctl00_ContentPlaceHolder1_ddlCourses").multiselect('rebuild');
            //    }
            //});

            var obj = {};
            obj.PatternNO = PatternNo;
            obj.CommandType = 5;
            obj.SessionNO = Sessionno;
            obj.ExamTypeNO = ExamTypeID;
            $.ajax({
                url: localurl+"EXAM/GradeEncodingPeriod.aspx/BindComponent",
                type: "POST",
                data: JSON.stringify(obj),
                dataType: "json",
                beforeSend: function () { $("[id*=preloader]").show(); },
                complete: function () { $("[id*=preloader]").hide(); },
                contentType: "application/json;charset=utf-8",
                success: function (res) {
                    $('#ctl00_ContentPlaceHolder1_ddlExam').empty();
                    $.each(res.d, function (key, value) {
                        var option = $("<option></option>").val(value.ComponentNo).html(value.ComponentName);
                        $("#ctl00_ContentPlaceHolder1_ddlExam").append(option);
                    });
                    $("#ctl00_ContentPlaceHolder1_ddlExam").multiselect('rebuild');
                }
            });
            if($("#ctl00_ContentPlaceHolder1_ddlExamType").val()!= "0"){
                SelectionDropDownShow();
            }
            
        }
    }
    catch (ex) {
    }
})
$("#ctl00_ContentPlaceHolder1_ddlSession").change(function () {
    try {
        if(GlobalEdit!=1)
        {
            var Obj = {};
            Obj.SessionNo = $("#ctl00_ContentPlaceHolder1_ddlSession").val();
            Obj.Command_Type = 7
            //$.ajax({
            //    url: localurl+"EXAM/GradeEncodingPeriod.aspx/BindSemester",
            //    type: "POST",
            //    data: JSON.stringify(Obj),
            //    dataType: "json",
            //    beforeSend: function () { $("[id*=preloader]").show(); },
            //    complete: function () { $("[id*=preloader]").hide(); },
            //    contentType: "application/json;charset=utf-8",
            //    success: function (response) {
            //        $('#ctl00_ContentPlaceHolder1_ddlSemester').empty();
            //        $.each(response.d, function (key, value) {
            //            var option = $("<option></option>").val(value.SemesterNo).html(value.SemesterName);
            //            $("#ctl00_ContentPlaceHolder1_ddlSemester").append(option);
            //        });
            //        $("#ctl00_ContentPlaceHolder1_ddlSemester").multiselect('rebuild');
            //    },
            //    error: function (errResponse) {

            //    }
            //});

            $.ajax({
                url: localurl+"EXAM/GradeEncodingPeriod.aspx/BindExamType",
                type: "POST",
                data: JSON.stringify(Obj),
                dataType: "json",
                beforeSend: function () { $("[id*=preloader]").show(); },
                complete: function () { $("[id*=preloader]").hide(); },
                contentType: "application/json;charset=utf-8",
                success: function (response) {
                    $('#ctl00_ContentPlaceHolder1_ddlExamType').empty();
                    $('#ctl00_ContentPlaceHolder1_ddlExamType').append("<option value='0'>Please Select</option>");
                    $.each(response.d, function (key, value) {
                        $("#ctl00_ContentPlaceHolder1_ddlExamType").append($("<option></option>").val(value.ExamTypeNo).html(value.ExamTypeName));
                    });
                },
                error: function (errResponse) {
                }
            });
            $("#ctl00_ContentPlaceHolder1_ddlCollege").val("0").change();
            $("#ctl00_ContentPlaceHolder1_ddlPatternNo").val("0").change();
            $("#ctl00_ContentPlaceHolder1_ddlExam").val("0").change();
            $("#txtStartDate").val("").change();
            $("#txtEndDate").val("").change();
            $("#txtApprovalDueDateHead").val("").change;
            $("#ApprovalDueDateDean").val("").change;
            $("#BindDynamicGradePeriodTable").empty();
            $('#BindDynamicGradePeriodTable_wrapper').hide();
        }
    }
    catch (ex) {

    }
});
$("#ctl00_ContentPlaceHolder1_ddlCollege").change(function () {
    try {
        var msg = '';
      
        if(GlobalEdit!=1)
        {
            $("#txtStartDate").val("").change();
            $("#txtEndDate").val("").change();
            $("#txtApprovalDueDateHead").val("").change;
            $("#ApprovalDueDateDean").val("").change;
            $("#ctl00_ContentPlaceHolder1_ddlPatternNo").val("0").change();
            $("#ctl00_ContentPlaceHolder1_ddlExam").val("0").change();
            $("#ctl00_ContentPlaceHolder1_ddlExamType").val("0").change();
            $("#BindDynamicGradePeriodTable").empty();
            $('#BindDynamicGradePeriodTable_wrapper').hide();
            $("#ctl00_ContentPlaceHolder1_ddlSemester").multiselect('select', false);
        }
    }
    catch (ex) {

    }
});
$("#ctl00_ContentPlaceHolder1_ddlPatternNo").change(function () {
    try {
        if(GlobalEdit!=1)
        {
            if($("#ctl00_ContentPlaceHolder1_ddlSession").val()!="0"){
                if($("#ctl00_ContentPlaceHolder1_ddlCollege").val()!="0")
                {
                    if($("#ctl00_ContentPlaceHolder1_ddlPatternNo").val()!="0")
                    {
                        //SelectionDropDownShow();
                    }
                }
            }
            $("#ctl00_ContentPlaceHolder1_ddlExam").val("0").change();
            $("#ctl00_ContentPlaceHolder1_ddlExamType").val("0").change();
            $("#txtStartDate").val("").change();
            $("#txtEndDate").val("").change();
            $("#txtApprovalDueDateHead").val("").change;
            $("#ApprovalDueDateDean").val("").change;
            $("#BindDynamicGradePeriodTable").empty();
            $('#BindDynamicGradePeriodTable_wrapper').hide();
            //$("#ctl00_ContentPlaceHolder1_ddlCourses").multiselect("enable");
            //$("#ctl00_ContentPlaceHolder1_ddlCourses").multiselect('select', false);
            //$("#ctl00_ContentPlaceHolder1_ddlSemester").multiselect("enable");
            //$("#ctl00_ContentPlaceHolder1_ddlSemester").multiselect('select', false);
        }
        else{
            //$("#ctl00_ContentPlaceHolder1_ddlCourses").multiselect('deselectAll', false);
            //var selectedCourses =GlobalCourse;
            //$("#ctl00_ContentPlaceHolder1_ddlCourses").multiselect('select', selectedCourses);
            //var selectedSemesters =globalSem;
            //$("#ctl00_ContentPlaceHolder1_ddlSemester").multiselect('deselectAll', false);
            //$("#ctl00_ContentPlaceHolder1_ddlSemester").multiselect('select', selectedSemesters);
        }
    }
    catch (ex) {

    }
});

function cleardropdown()
{
    try
    {
        GlobalEdit=0;
        GlobalExam=0;
        GlobalValue=0;
        GlobalModality=0;
        $("#ctl00_ContentPlaceHolder1_ddlCollege").val("0").change();
        $("#ctl00_ContentPlaceHolder1_ddlPatternNo").val("0").change();
        $("#ctl00_ContentPlaceHolder1_ddlExam").val("0").change();
        $("#ctl00_ContentPlaceHolder1_ddlSession").val("0").change();
        $("#ctl00_ContentPlaceHolder1_ddlExamType").val("0").change();
        $("#txtStartDate").val("").change();
        $("#txtEndDate").val("").change();
     //   $("#ctl00_ContentPlaceHolder1_ddlCourses").selectedIndex = -1;
        $("#txtApprovalDueDateHead").val("").change;
        $("#ApprovalDueDateDean").val("").change;
        $("#BindDynamicGradePeriodTable").empty();
        $('#BindDynamicGradePeriodTable_wrapper').hide();
        $('#Status').prop('checked', true);
        $("#hdfGradeEncodeNo").val("0").change();
        $("#ctl00_ContentPlaceHolder1_ddlSession").prop("disabled", false);
        $("#ctl00_ContentPlaceHolder1_ddlCollege").prop("disabled", false);
        $("#ctl00_ContentPlaceHolder1_ddlPatternNo").prop("disabled", false);
        $("#ctl00_ContentPlaceHolder1_ddlExam").prop("disabled", false);
        $("#ctl00_ContentPlaceHolder1_ddlExamType").prop("disabled", false);
    }
    catch (ex) {

    }
}  
$('#btnClear').click(function () {
    cleardropdown();
})

$('#btnSubmit').click(function () {
    try {
        ShowLoader('#btnSubmit');
        var msg = ''; var str = ""; var count = 0;
        if ($("#ctl00_ContentPlaceHolder1_ddlSession").val() == '0')
            msg += "\r Please select academic session !!! <br/>";
        if ($("#ctl00_ContentPlaceHolder1_ddlCollege").val() == '0')
            msg += "\r Please select college !!! <br/>";
        if ($("#ctl00_ContentPlaceHolder1_ddlPatternNo").val() == '0')
            msg += "\r Please select exam pattern !!! <br/>";
        //if ($("#ctl00_ContentPlaceHolder1_ddlCourses").val() == '0' || $("#ctl00_ContentPlaceHolder1_ddlCourses").val() == '')
        //    msg += "\r Please Select Curriculum !!! <br/>";
        //if ($("#ctl00_ContentPlaceHolder1_ddlSemester").val() == '')
        //    msg += "\r Please Select Semester !!! <br/>";
        if ($("#ctl00_ContentPlaceHolder1_ddlExamType").val() == '0')
            msg += "\r Please select exam type !!! <br/>";
        var selectedExamComponents = $("#ctl00_ContentPlaceHolder1_ddlExam").val();
        if (!selectedExamComponents || selectedExamComponents.length === 0) {
            msg += "\r Please select at least one exam component !!! <br/>";
        }
        if ($("#txtStartDate").val() == '')
            msg += "\r Please enter start date !!! <br/>";
        if ($("#txtEndDate").val() == '')
            msg += "\r Please enter end date !!! <br/>";
        if ($("#txtApprovalDueDateHead").val() == '')
            msg += "\r Please enter approval due date(Head) !!! <br/>";
        if ($("#ApprovalDueDateDean").val() == '')
            msg += "\r Please enter approval due date(Dean) !!! <br/>";
        if (msg != '') {
            iziToast.warning({
                message: msg,
            });
            return false;
        }
        const today = new Date();
        const Todays = today.toISOString().split('T')[0];
        const TodaysDate = new Date(Todays);
        const startDate = new Date($('#txtStartDate').val());
        const endDate = new Date($('#txtEndDate').val());
        const ApprovalDueDateHead = new Date($('#txtApprovalDueDateHead').val());
        const ApprovalDueDateDean = new Date($('#ApprovalDueDateDean').val());
        if(GlobalEdit!=1)
        {
            if (TodaysDate > startDate) {
                Swal.fire({
                    html: 'The start date should not be less than today date !!!',
                    icon: 'warning'
                });
           
                return false;
            } 
        }
        if (startDate > endDate) {
            Swal.fire({
                html: 'End date should not be less than start date !!!',
                icon: 'warning'
            });
           
            return false;
        } 
        if (ApprovalDueDateHead < endDate) {
            Swal.fire({
                html: 'Approval due date(Head) should not be less than startDate !!!',
                icon:'warning'
            });
            return false;
        }
        if (ApprovalDueDateDean < ApprovalDueDateHead) {
            Swal.fire({
                html: 'Approval due date(Dean) should not be less than startDate !!!',
                icon:'warning'
            });
            return false;
        }
        var Obj = {}; var CDB_NO = ''; var Semester = ''; var ExamComp='';
        Obj.SessionNo = $("#ctl00_ContentPlaceHolder1_ddlSession").val();
        Obj.College = $("#ctl00_ContentPlaceHolder1_ddlCollege").val();
        Obj.PatternNo = $("#ctl00_ContentPlaceHolder1_ddlPatternNo").val();
        //$.each($("#ctl00_ContentPlaceHolder1_ddlCourses").find("option:selected"), function (index, value) {
        //    CDB_NO = CDB_NO + $(value).val() + ",";
        //});
        //$.each($("#ctl00_ContentPlaceHolder1_ddlSemester").find("option:selected"), function (index, value) {
        //    Semester = Semester + $(value).val() + ",";
        ////});
        //var splitsubject =CDB_NO.split('$$');
        //var Schemno = splitsubject[0];
        //var CourseNo = splitsubject[1];

        $.each($("#ctl00_ContentPlaceHolder1_ddlExam").find("option:selected"), function (index, value) {
            ExamComp = ExamComp + $(value).val() + ",";
        });

        Obj.SchemeNo=0;
        Obj.SemesterNo=0;
        Obj.ComponentNo =ExamComp;
        Obj.StartDate = $("#txtStartDate").val();
        Obj.EndDate = $("#txtEndDate").val();
        Obj.Status = $('#Status').prop('checked');
        Obj.GradeEncodeNo = $("#hdfGradeEncodeNo").val();
        Obj.ApprovalDueDateHead = $("#txtApprovalDueDateHead").val();
        Obj.ApprovalDueDateDean = $("#ApprovalDueDateDean").val();
        Obj.ExamTypeNo =  $("#ctl00_ContentPlaceHolder1_ddlExamType").val();
        $.ajax({
            url: localurl+"EXAM/GradeEncodingPeriod.aspx/SaveGradeEncoding",
            type: "POST",
            data: JSON.stringify(Obj),
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                $.each(response.d, function (index, GetValue) {
                    if (count == 0) {
                        if (GetValue.Command_Type == "1") {
                            iziToast.success({
                                message: 'Record added successfully  !!!',
                            });
                            SelectionDropDownShow();
                        }
                        else if (GetValue.Command_Type == "2") {

                            iziToast.success({
                                message: 'Record update successfully!!!',
                            });
                            SelectionDropDownShow();
                        }
                        else if (GetValue.Command_Type == "3") {
                            Swal.fire({
                                html: 'Record already exists !!!',
                                icon: 'warning'
                            });
                            $("[id*=preloader]").hide();
                            return false;
                        }
                        else {
                            Swal.fire({
                                html: 'Error Occurred !!!',
                                icon: 'error'
                            });
                            $("[id*=preloader]").hide();
                            return false;
                        }
                    }
                    count++;
                });
            }
        });
    }  
    catch (ex) {

    }
})
function SelectionDropDownShow(){
    var Obj = {}; var CDB_NO = ''; var Semester = '';
    Obj.DropDownNo = $("#ctl00_ContentPlaceHolder1_ddlSession").val();
    Obj.Command_Type=4;
    Obj.Dynamic_Filter = $("#ctl00_ContentPlaceHolder1_ddlCollege").val();
    Obj.UserNo = $("#ctl00_ContentPlaceHolder1_ddlPatternNo").val();
    Obj.ExamtypeNO = $("#ctl00_ContentPlaceHolder1_ddlExamType").val();
    $.ajax({
        url: localurl+"EXAM/GradeEncodingPeriod.aspx/ShowGradeEncoding",
        type: "POST",
        data: JSON.stringify(Obj),
        dataType: "json",
        beforeSend: function () { $("[id*=preloader]").show(); },
        //complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            $('#BindDynamicGradePeriodTable_wrapper').show();
            //str = '<thead><tr><th><span class="Action">Action</span></th><th><span class="AcaSession">Academic Session</span></th><th><span class="Campus">Campus</span></th><th><span class="ExamPattern">Exam Pattern</span></th> <th><span class="Curriculums">Curriculums</span></th><th><span class="SemesterName">Semester Name</span></th><th><span class="ClsExamComponent">Exam Componant</span></th><th><span class="StartDate">Start Date</span></th><th><span class="EndDate">End Date</span></th><th><span class="ClsApprovalDueDateHead">Approval Due Date(Head)</span></th><th><span class="ClsApprovalDueDateDean">Approval Due Date(Dean)</span></th><th><span class="Status">Status</span></th></tr></thead><tbody>';
            str = '<thead><tr><th><span class="Action">Action</span></th><th><span class="AcaSession">Academic Session</span></th><th><span class="Campus">Campus</span></th><th><span class="ExamPattern">Exam Pattern</span></th> <th><span class="ExamType">Exam Type</span></th><th><span class="ClsExamComponent">Exam component</span></th><th><span class="StartDate">Start Date</span></th><th><span class="EndDate">End Date</span></th><th><span class="ClsApprovalDueDateHead">Approval Due Date(Head)</span></th><th><span class="ClsApprovalDueDateDean">Approval Due Date(Dean)</span></th><th><span class="Status">Status</span></th></tr></thead><tbody>';

            $.each(response.d, function (index, GetValue) {
                str += '<tr class="Trclass">';
                str += '<td><a id="btnEditGrade" class="fa fa-pencil-square-o text-primary" title="Edit Record" href="#" onclick="EditGrade(this)"></a>' +
                    '<input type="hidden" id="hdnTableGradeEncodeNo" value="' + GetValue.GradeEncodeNo + '"/><input type="hidden" id="hdnSessionNo" value="' + GetValue.SessionNo + '"/><input type="hidden" id="hdnCampusNo" value="' + GetValue.Campus + '"/>' +
                    '<input type="hidden" id="hdnPatternNo" value="' + GetValue.PatternNo + '"/>'+
                    '<input type="hidden" id="hdnComponentNo" value="' + GetValue.ComponentNo + '"/><input type="hidden" id="hdnBlockId" value="' + GetValue.BlockId + '"/>'+
                    '<input type="hidden" id="hdnCollegeID" value="' + GetValue.CollegeID + '"/><input type="hidden" id="hdnExamTypeNO" value="' + GetValue.ExamTypeNo + '"/></td>'
                str += '<td>' + GetValue.SessionName + '</td>'
                str += '<td>' + GetValue.CampusName + '</td>'
                str += '<td>' + GetValue.PatternName + '</td>'
                //str += '<td>' + GetValue.SchemeName + '</td>'
                //str += '<td>' + GetValue.SemesterName + '</td>'
                str += '<td>' + GetValue.ExamTypeName + '</td>'
                str += '<td>' + GetValue.ComponentName + '</td>'
                str += '<td>' + GetValue.StartDateStri + '</td>'
                str += '<td>' + GetValue.EndDateStri + '</td>'
                str += '<td>' + GetValue.ApprovalDueDateHeadStri + '</td>'
                str += '<td>' + GetValue.ApprovalDueDateDeanStri + '</td>'
                if (GetValue.Status == true) {
                    str += '<td><span class="badge bg-success">Active</span>' +
                        '<input type="hidden" id="ActivityStatus" value="true"/></td>';
                } else {
                    str += '<td><span class="badge bg-danger">Inactive</span>' +
                        '<input type="hidden" id="ActivityStatus" value="false"/></td>';
                }
                str += '</tr>';
            });
            str += '</tbody>';
            RemoveTableDynamically('#BindDynamicGradePeriodTable');
            $("#BindDynamicGradePeriodTable").append(str);
            var BindDynamicGradePeriodTable = $('#BindDynamicGradePeriodTable');
            commonDatatable(BindDynamicGradePeriodTable);
            $("[id*=preloader]").hide();
        },
        error: function (errResponse) {
        }
    });
}
function EditGrade(ClickValue) {
    try {
        GlobalEdit = 1;
        var td = $("td", $(ClickValue).closest("tr"));
        //GlobalCourse = $("[id*=hdnSchemeNo]", td).val().split(',');
        //globalSem = $("[id*=hdnSemesterNo]", td).val().split(',');
        GlobalModality = $("[id*=hdnBlockId]", td).val();
        $("#txtStartDate").val(td[6].innerText);
        $("#txtEndDate").val(td[7].innerText);
        $("#txtApprovalDueDateHead").val(td[8].innerText);
        $("#ApprovalDueDateDean").val(td[9].innerText);
        $("#hdfGradeEncodeNo").val($("[id*=hdnTableGradeEncodeNo]", td).val());
        $("#ctl00_ContentPlaceHolder1_ddlSession").val($("[id*=hdnSessionNo]", td).val()).change();
        $("#ctl00_ContentPlaceHolder1_ddlCollege").val($("[id*=hdnCollegeID]", td).val()).change();
        //$("#ctl00_ContentPlaceHolder1_ddlExam").val($("[id*=hdnComponentNo]", td).val()).change();
        $("#ctl00_ContentPlaceHolder1_ddlExamType").val($("[id*=hdnExamTypeNO]", td).val()).change();
        $("#ctl00_ContentPlaceHolder1_ddlExam").multiselect('deselectAll', false);
        var selectedCourses =$("[id*=hdnComponentNo]", td).val().split(',');;
        $("#ctl00_ContentPlaceHolder1_ddlExam").multiselect('select', selectedCourses);
        $("#ctl00_ContentPlaceHolder1_ddlPatternNo").val($("[id*=hdnPatternNo]", td).val()).change();

        $("#ctl00_ContentPlaceHolder1_ddlSession").prop("disabled", "disabled");
        $("#ctl00_ContentPlaceHolder1_ddlCollege").prop("disabled", "disabled");
        $("#ctl00_ContentPlaceHolder1_ddlPatternNo").prop("disabled", "disabled");
        $("#ctl00_ContentPlaceHolder1_ddlExamType").prop("disabled", "disabled");
       // $("#ctl00_ContentPlaceHolder1_ddlCourses").multiselect("disable");
      //  $("#ctl00_ContentPlaceHolder1_ddlSemester").multiselect("disable");
        $("#ctl00_ContentPlaceHolder1_ddlExam").multiselect("disable");
        if ($("[id*=ActivityStatus]", td).val() == "false")
            $('#Status').prop('checked', false);
        else
            $('#Status').prop('checked', true);
    }
    catch (ex) {

    }
}



