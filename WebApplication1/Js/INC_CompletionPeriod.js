/// <reference path="../../../../../../../../../../../../../../../../../../../../scripts/postadmission/commonmaster.js" />

//===============================================//
// MODULE NAME   : RFC ERP
// PAGE NAME     : INC Completion Period
// CREATION DATE : 28-10-2023
// CREATED BY    : Sakshi Mohadikar
// Modified BY   :
// Modified Date :
//===============================================//
var liveurl = "../../../../../../";
var localurl = "../../../../";

$(document).ready(function () {
    BindAcademicSession();
    BindCollege();
    BindSemester();
});

function BindAcademicSession() {
    var DependedId = 0;

    $.ajax({
        type: "POST",
        url: '../../Exam/INC_CompletionPeriod.aspx/BindMaster',
        data: JSON.stringify({ Id: DependedId, Process: ProcessFor.SESSION }),
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {            
            //var data = JSON.parse(response.d);
            $("#ddlAcademicSession").empty();
            $("#ddlAcademicSession").append($("<option></option>").val('0').html('Please Select'));
            
            $.each(response.d, function (key, value) {
                if (value.MasterNo != 0) {
                    $("#ddlAcademicSession").append($("<option></option>").val(value.MasterNo).html(value.MasterName));                    
                }                
            });
        },
        failure: function (response) {

        },
        error: function (response) {
            DisplayMessage(MessageProvider.SweetAlert, AlertType.Error, 'Error Occurred !!!');
        }
    });
}


function BindCollege() {
    //var DropdownId = $("#ddlCollege").val();
    var MasterId = 0;
    var MasterName = '';
    var DependedId = 0;

    $.ajax({
        type: "POST",
        url: '../../Exam/INC_CompletionPeriod.aspx/BindMaster',
        data: JSON.stringify({ Id: DependedId, Process: ProcessFor.COLLEGE }),
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#ddlCollege").empty();
            $("#ddlCollege").append($("<option></option>").val('0').html('Please Select'));

            $.each(response.d, function (key, value) {
                if (value.MasterNo != MasterId) {
                    $("#ddlCollege").append($("<option></option>").val(value.MasterNo).html(value.MasterName));
                }
            });
        },
        failure: function (response) {

        },
        error: function (response) {
            DisplayMessage(MessageProvider.SweetAlert, AlertType.Error, 'Error Occurred !!!');
        }
    });
    if (MasterId != 0) {
        var newOption = $("<option>").val(MasterId).text(MasterName);

        $(DropdownId).append(newOption);
    }
}


$('#ddlCollege').change(function () {
    var CollegeId = $(this).val();
    
    $.ajax({
        type: "POST",
        url: '../../Exam/INC_CompletionPeriod.aspx/BindCourse',
        data: JSON.stringify({ Id: CollegeId, Process: ProcessFor.COURSE }),
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#ddlProgram").empty();
            $("#ddlProgram").append($("<option></option>").val('0').html('Please Select'));

            $.each(response.d, function (key, value) {
                $("#ddlProgram").append($("<option></option>").val(value.Master).html(value.MasterName));                
            });
            $('#divTable').show();
            BindINCPeriod();
        },
        failure: function (response) {

        },
        error: function (response) {
            DisplayMessage(MessageProvider.SweetAlert, AlertType.Error, 'Error Occurred !!!');
        }
    });
})

function BindCampus() {
    var DropdownId = $("#ddlCampus").val();
    var MasterId = 0;
    var MasterName = '';
    var DependedId = 0;

    $.ajax({
        type: "POST",
        url: '../../Exam/INC_CompletionPeriod.aspx/BindMaster',
        data: JSON.stringify({ Id: DependedId, Process: ProcessFor.CAMPUS }),
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {            
            $("#ddlCampus").empty();
            $("#ddlCampus").append($("<option></option>").val('0').html('Please Select'));
            
            $.each(response.d, function (key, value) {
                if (value.MasterNo != MasterId) {
                    $("#ddlCampus").append($("<option></option>").val(value.MasterNo).html(value.MasterName));
                }
            });
        },
        failure: function (response) {

        },
        error: function (response) {
            DisplayMessage(MessageProvider.SweetAlert, AlertType.Error, 'Error Occurred !!!');
        }
    });
    if (MasterId != 0) {
        var newOption = $("<option>").val(MasterId).text(MasterName);

        $(DropdownId).append(newOption);
    }
}

//$('#ddlCampus').change(function () {
//    var CampusId = $(this).val();
//    var DependedId = CampusId;

//    $.ajax({
//        type: "POST",
//        url: '../../Exam/INC_CompletionPeriod.aspx/BindMaster',
//        data: JSON.stringify({ Id: DependedId, Process: ProcessFor.COLLEGE }),
//        beforeSend: function () { $("[id*=preloader]").show(); },
//        complete: function () { $("[id*=preloader]").hide(); },
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        async: false,
//        success: function (response) {
//            if (response.d == '') {
//                $("#ddlCollege").empty();
//                $("#ddlCollege").append($("<option></option>").val('0').html('Please Select'));
//                $("[id*=preloader]").hide();
//                return false;
//            }
//            else {
//                $("#ddlCollege").empty();
//                $("#ddlCollege").append($("<option></option>").val('0').html('Please Select'));
//                $.each(response.d, function (key, value) {
//                    $("#ddlCollege").append($("<option></option>").val(value.MasterNo).html(value.MasterName));                    
//                });

//                $('#divTable').show();
//                BindINCPeriod();
//            }
//        },
//        failure: function (response) {

//        },
//        error: function (response) {
//            DisplayMessage(MessageProvider.SweetAlert, AlertType.Error, 'Error Occurred !!!');
//        }
//    });
    
//})

function BindSemester() {
    var MasterId = 0;
    var MasterName = '';
    var DependedId = 0;
    
    $.ajax({
        type: "POST",
        url: '../../Exam/INC_CompletionPeriod.aspx/BindMaster',
        data: JSON.stringify({ Id: DependedId, Process: ProcessFor.SEMESTER }),
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d == '') {
                $("#ddlSemester").empty();
                $("#ddlSemester").append($("<option></option>").val('0').html('Please Select'));
                $("[id*=preloader]").hide();
                return false;
            }
            else {
                //var data = JSON.parse(response.d);
                $("#ddlSemester").empty();
                $("#ddlSemester").append($("<option></option>").val('0').html('Please Select'));

                $.each(response.d, function (key, value) {
                    if (value.MasterNo != MasterId) {
                        $("#ddlSemester").append($("<option></option>").val(value.MasterNo).html(value.MasterName));
                    }

                });
            }
        },
        failure: function (response) {

        },
        error: function (response) {
            DisplayMessage(MessageProvider.SweetAlert, AlertType.Error, 'Error Occurred !!!');
        }
    });
    if (MasterId != 0) {
        var newOption = $("<option>").val(MasterId).text(MasterName);

        $(DropdownId).append(newOption);
    }
}

function validateDropPeriod() {
    //var setretstatus = true;
    if ($("#ddlAcademicSession").val() == '0') {
        DisplayMessage(MessageProvider.ToastAlert, AlertType.Warning, 'Please Select Academic Session');
        $('#ddlAcademicSession').focus();
        return false;
    }
    else if ($("#ddlCampus").val() == '0') {
        DisplayMessage(MessageProvider.ToastAlert, AlertType.Warning, 'Please Select Campus');
        $('#ddlCampus').focus();
        return false;
    }
    else if ($("#ddlCollege").val() == '0') {
        DisplayMessage(MessageProvider.ToastAlert, AlertType.Warning, 'Please Select College');
        $('#ddlCollege').focus();
        return false;
    }
    else if ($("#ddlProgram").val() == '0') {
        DisplayMessage(MessageProvider.ToastAlert, AlertType.Warning, 'Please Select Program');
        $('#ddlProgram').focus();
        return false;
    }
    else if ($("#ddlSemester").val() == '0') {
        DisplayMessage(MessageProvider.ToastAlert, AlertType.Warning, 'Please Select Semester');
        $('#ddlSemester').focus();
        return false;
    }
    else if ($("#txtStartDate").val() == '') {
        DisplayMessage(MessageProvider.ToastAlert, AlertType.Warning, 'Please Select Start Date');
        $('#txtStartDate').focus();
        return false;
    }
    else if ($("#txtEndDate").val() == '') {
        DisplayMessage(MessageProvider.ToastAlert, AlertType.Warning, 'Please Select End Date');
        $('#txtEndDate').focus();
        return false;
    }
    else if ($("#txtamount").val() == '') {
        DisplayMessage(MessageProvider.ToastAlert, AlertType.Warning, 'Please Enter INC Fee Per Subject');
        $('#txtamount').focus();
        return false;
    }
    return true;
}


$("[id*=btnSubmit]").click(function () {
    if (validateDropPeriod() == true) {
        var DropPeriod, DropPeriodId;
        
        DropPeriod = $("#ctl00_ContentPlaceHolder1_hdnDropPeriod").val();

        if (DropPeriod != 0) {
            DropPeriodId = DropPeriod;
        }
        else {
            DropPeriodId = 0;
        }
        var programValue = $('#ddlProgram').val();
        var programValues = programValue.replace(/[()]/g, '').split(',');
        var INCPeriod = {};
        
        INCPeriod.INC_Period_id = DropPeriodId;
        INCPeriod.academic_session_id = $('#ddlAcademicSession').val(),
        INCPeriod.campus_id = 1,
        INCPeriod.college_id = $('#ddlCollege').val(),
        INCPeriod.semester_no = $('#ddlSemester').val(),
        INCPeriod.degree_no= programValues[0]; 
        INCPeriod.branch_no = programValues[1];
        INCPeriod.start_date = $('#txtStartDate').val(),
        INCPeriod.end_date = $('#txtEndDate').val(),
        INCPeriod.amount = $('#txtamount').val(),
        INCPeriod.active_status = $('#Status').prop('checked') == true ? 1 : 0;

        var INCPeriodDATA = new Array();
        INCPeriodDATA.push(INCPeriod);
        SubmitAddDropPeriod(INCPeriodDATA);
    }
});

function SubmitAddDropPeriod(INCPeriodDATA) {

    var isupdate = $("#ctl00_ContentPlaceHolder1_hdnDropPeriod").val();
    var INC_PeriodID = isupdate ? isupdate : 0;
    if (isupdate != "") { INC_PeriodID = isupdate; }
    else { INC_PeriodID = 0; }

    $.ajax({
        type: "POST",
        url: '../../Exam/INC_CompletionPeriod.aspx/INCPeriod',
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        data: JSON.stringify({ INCPeriodData: INCPeriodDATA, INC_PERIOD_ID: INC_PeriodID }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var data = JSON.parse(response.d);
            if (data != '') {
                if (data.trim() == '2627') {
                    DisplayMessage(MessageProvider.SweetAlert, AlertType.Warning, 'Record Already Exists For Academic Session, Campus, College, and Semester');
                    $("#ctl00_ContentPlaceHolder1_hdnDropPeriod").val("0");
                }
                else if (data.trim() == '1') {
                    DisplayMessage(MessageProvider.ToastAlert, AlertType.Success, 'Record Saved Successfully');
                    BindINCPeriod();
                    ClearControlsAddDropPeriod();
                }
                else if (data.trim() == '2') {
                    DisplayMessage(MessageProvider.ToastAlert, AlertType.Success, 'Record Updated Successfully');
                    BindINCPeriod();
                    ClearControlsAddDropPeriod();
                }
                else {
                    DisplayMessage(MessageProvider.SweetAlert, AlertType.Error, 'Error !!!');
                }
            }
        },
        error: function (xhr, status, error) {
            console.error("AJAX Error: " + status + " - " + error);
            // Handle the error, e.g., display an error message to the user
        }
    });
}

function ClearControlsAddDropPeriod() {

    $("[id*=preloader]").show();

    $("#ctl00_ContentPlaceHolder1_hdnDropPeriod").val('0').change();

    //$('#ddlAcademicSession').val('0').change();
    $('#ddlCollege').val('0').change();
    $('#ddlProgram').val('0').change();
    $('#ddlSemester').val('0').change();
    $('#txtStartDate').val('');
    $('#txtEndDate').val('');
    $('#txtamount').val('');
    $("[id*=Status]").prop("checked", true);
    $("#btnSubmit").val('SUBMIT');

    $("#ddlAcademicSession, #ddlCollege, #ddlProgram, #ddlSemester").prop('disabled', false);
   
    //$('#divTable').show();
}

function ClearupdatDropPeriod() {

    $("[id*=preloader]").show();

    $("#ctl00_ContentPlaceHolder1_hdnDropPeriod").val('0').change();

    $('#ddlAcademicSession').val('0').change();
    $('#ddlCollege').val('0').change();
    $('#ddlProgram').val('0').change();
    $('#ddlSemester').val('0').change();
    $('#txtStartDate').val('');
    $('#txtEndDate').val('');
    $('#txtamount').val('');
    $("[id*=Status]").prop("checked", true);
    $("#btnSubmit").val('SUBMIT');
    $("#ddlAcademicSession, #ddlCollege, #ddlProgram, #ddlSemester").prop('disabled', false);

    //$('#divTable').hide();    
}


$("[id*=btnClear]").click(function () {
    ClearupdatDropPeriod();
    $('#divTable').hide();
});

$('#ddlAcademicSession').change(function () {
    $('#ddlCollege').val('0').change();
    $('#ddlProgram').val('0').change();
    $('#ddlSemester').val('0').change();
    $('#txtStartDate').val('');
    $('#txtEndDate').val('');
    $('#txtamount').val('');
    $("[id*=Status]").prop("checked", true);
})

$("#ddlCollege").change(function () {
    $('#ddlProgram').val('0').change();
    $('#ddlSemester').val('0').change();
    $('#txtStartDate').val('');
    $('#txtEndDate').val('');
    $('#txtamount').val('');
    $("[id*=Status]").prop("checked", true);
})


function BindINCPeriod() {    
    var Obj = {};
    Obj.Session = $('#ddlAcademicSession').val();
    Obj.College = $('#ddlCollege').val();
    $.ajax({
        type: "POST",
        url: '../../Exam/INC_CompletionPeriod.aspx/BindINCPeriod',
        data: JSON.stringify(Obj),
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var data = JSON.parse(response.d);
            if (data != '') {
                var _str = '';
                var _tbody = '';
                var rownum = 0;
                str = '<thead><tr><th><span class="Action">Action</span></th><th><span class="AcademicSession">Academic Session</span></th><th><span class="Campus">Campus</span></th><th><span class="College">College</span></th><th><span class="Program">Program</span></th><th><span class="SemesterName">Semester</span></th><th><span class="StartDate">Start Date</span></th><th><span class="EndDate">End Date</span></th><th><span class="INCFeePerSubject">INC Fee Per Subject</span></th><th><span class="Status">Status</span></th></tr></thead><tbody>';
                $.each(data, function (a, b) {
                    
                    rownum = rownum + 1;
                    str = str + '<tr>'
                    str = str + '<td><a href="#" id="btnEditSchTagging" class="fa fa-edit text-primary" onclick="Get_INCPeriod_ById(this)" title="Edit Record"></a>' +
                        '<input type="hidden" id="hdnincperiodid" value="' + b.INC_COMPLETION_PERIOD_ID + '"/></td>'
                    str = str + '<td>' + b.SESSION_NAME + '</td>'
                    str = str + '<td>' + b.CAMPUSNAME + '</td>'
                    str = str + '<td>' + b.COLLEGE_NAME + '</td>'
                    str = str + '<td>' + b.PROGRAM + '</td>'
                    str = str + '<td>' + b.SEMESTERNAME + '</td>'

                    var databaseDateStr = b.START_DATE;
                    var databaseDate = new Date(databaseDateStr);
                    var day = String(databaseDate.getDate()).padStart(2, '0');
                    var month = String(databaseDate.getMonth() + 1).padStart(2, '0'); // Months are zero-based, so we add 1
                    var year = databaseDate.getFullYear();
                    var formattedDate = day + '-' + month + '-' + year;

                    str = str + '<td>' + formattedDate + '</td>';

                    var endDateStr = b.END_DATE;
                    var endDate = new Date(endDateStr);
                    var day = String(endDate.getDate()).padStart(2, '0');
                    var month = String(endDate.getMonth() + 1).padStart(2, '0'); // Months are zero-based, so we add 1
                    var year = endDate.getFullYear();
                    var formattedEndDate = day + '-' + month + '-' + year;

                    str = str + '<td>' + formattedEndDate + '</td>';


                    str = str + '<td>' + b.AMOUNT + '</td>'

                    if (b.ACTIVE_STATUS == 'Active') {
                        str = str + '<td><span class="badge badge-success">' + b.ACTIVE_STATUS + '</span></td>'
                    }
                    else {
                        str = str + '<td><span class="badge badge-danger">' + b.ACTIVE_STATUS + '</span></td>'
                    }

                    str = str + '</tr>'
                });
                str = str + '</tbody>'
                RemoveTableDynamically('#BindDynamicINCPeriodTable');
                $("#BindDynamicINCPeriodTable").append(str);
                var BindDynamicDocumentsTable = $('#BindDynamicINCPeriodTable')
                commonDatatable(BindDynamicDocumentsTable);
                //BindtableLabelsDyanamically();
            }
            else {
                RemoveTableDynamically('#BindDynamicINCPeriodTable');
                $("#divTable").css({ 'display': 'none' });
            }
        },
        failure: function (response) {

        },
        error: function (response) {
            DisplayMessage(MessageProvider.SweetAlert, AlertType.Error, 'Error Occurred !!!');
        }
    });
}
//edit
function Get_INCPeriod_ById(INCPeriod_Id) {
    $("#btnSubmit").val('UPDATE');
    var td = $("td", $(INCPeriod_Id).closest("tr"));
    var strincperiodid = $("[id*=hdnincperiodid]", td).val();
    $('#ctl00_ContentPlaceHolder1_hdnDropPeriod').val(strincperiodid);
    $.ajax({
        type: "POST",
        url: '../../Exam/INC_CompletionPeriod.aspx/BindINCPeriodById',
        data: JSON.stringify({ Id: strincperiodid }),
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var data = JSON.parse(response.d);
            if (data != '') {                
                $("#ddlAcademicSession").val(data[0].SESSIONNO).change();
                //$("#ddlCampus").val(data[0].CAMPUSNO).change();
                $("#ddlCollege").val(data[0].COLLEGE_ID).change();
                $("#ddlProgram").val(data[0].PROGRAMNO).change();
                $("#ddlSemester").val(data[0].SEMESTERNO).change();


                var databaseDateStr = data[0].START_DATE;
                var databaseDate = new Date(databaseDateStr);
                var year = databaseDate.getFullYear();
                var month = (databaseDate.getMonth() + 1).toString().padStart(2, '0');
                var day = databaseDate.getDate().toString().padStart(2, '0');
                var formattedDate = year + '-' + month + '-' + day;
                $("#txtStartDate").val(formattedDate);

                var endDateStr = data[0].END_DATE;
                var endDate = new Date(endDateStr);
                var year = endDate.getFullYear();
                var month = (endDate.getMonth() + 1).toString().padStart(2, '0');
                var day = endDate.getDate().toString().padStart(2, '0');
                var formattedEndDate = year + '-' + month + '-' + day;
                $("#txtEndDate").val(formattedEndDate);

                $("#txtamount").val(data[0].AMOUNT);

                if (data[0].ACTIVE_STATUS == 'Active') {
                    $('#Status').prop('checked', true);
                }
                else {
                    $('#Status').prop('checked', false);
                }
                //$("#ddlCourse").val(data[0].BRANCHNO).change();
                $("#ddlAcademicSession, #ddlCollege, #ddlProgram, #ddlSemester").prop('disabled', true);
                
            }
            else {

            }
        },
        failure: function (response) {

        },
        error: function (response) {
            DisplayMessage(MessageProvider.SweetAlert, AlertType.Error, 'Error Occurred !!!');
        }
    });
}


//function ValidateAuthorizedDate() {

//    var From_date = new Date($('#txtStartDate').val());
//    var To_date = new Date($('#txtEndDate').val());
//    var currentDate = new Date(); 

   
//     if (From_date < currentDate.setHours(0, 0, 0, 0)) {
//            var msg = "Back Date is not allowed!";
//            DisplayMessage(MessageProvider.ToastAlert, AlertType.Warning, msg);
//            $('#txtStartDate').val('');
//           return;
//     }
    
//    else if (To_date < From_date) {
//        var msg = "The end date should not be earlier than the start date. Please enter valid dates.";
//        DisplayMessage(MessageProvider.ToastAlert, AlertType.Warning, msg);
//        $('#txtEndDate').val('');
//        return;
//    }
//}

// Assuming you have an input field with ID "txtStartDate"
$('#txtStartDate').on('blur', function () { 

    var From_date = new Date($('#txtStartDate').val());
    var To_date = new Date($('#txtEndDate').val());
    var currentDate = new Date();   
        if (From_date < currentDate.setHours(0, 0, 0, 0)) {
            var msg = "Back Date is not allowed!";
            DisplayMessage(MessageProvider.ToastAlert, AlertType.Warning, msg); 
            $('#txtStartDate').val('');
            return;
        } 
     
});


$('#txtEndDate').on('blur', function () {

    var From_date = new Date($('#txtStartDate').val());
    var To_date = new Date($('#txtEndDate').val());

    if (To_date < From_date) {
        var msg = "The end date should not be earlier than the start date. Please enter valid dates.";
        DisplayMessage(MessageProvider.ToastAlert, AlertType.Warning, msg);
        $('#txtEndDate').val('');
        return;
    }
    //ValidateAuthorizedDate();
});


var ProcessFor = {
    SESSION: "SESSION",                     // use to bind Academic Session dropdown
    CAMPUS: "CAMPUS",                      // use to bind Campus dropdown
    COLLEGE: "COLLEGE",                   // use to bind College dropdown
    COURSE: "COURSE",                    // use to bind Course / Program dropdown 
    SEMESTER: "SEMESTER",               // use to bind semester dropdown
    INTAKE: "INTAKE"                   // use to bind intake dropdown (admission batch)
};
