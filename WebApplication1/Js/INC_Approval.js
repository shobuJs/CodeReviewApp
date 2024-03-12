//===============================================//
// MODULE NAME   : RFC ERP 
// PAGE NAME     : INC Approval
// CREATION DATE : 05-01-2024
// CREATED BY    : Gaurav Kalbande
// Modified BY   : Ekansh Moundekar
// Modified Date : 21-02-2024
//===============================================//

var liveurl = "../../../";
var localurl = "../../";
let GolbleCancel=0;
let GloblaCampus = 0;
function txtReasonRejectionLength(el) {
    var Lenght = el.value.length;
    if (Lenght == 200) {
        var str = $("#txtReasonRejection").val();
        str.slice(0, -1);
        iziToast.warning({
            message: 'Characters length should not be greater than 200 !!!',
        });
    }
} 
$(document).ready(function () {
    $('.select2Task').select2({ dropdownParent: $('#MarkEntryModal') });
});
 
function ClearData() {
    GolbleCancel=1;
    GloblaCampus = 1;
    $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val(0).change();
    $("#ctl00_ContentPlaceHolder1_LstCourses").val(0).change();
    $("#ctl00_ContentPlaceHolder1_INCStauts").val(3).change(); 
    $("#fhdnIdno").val(0);
    $("#fhdnSchemno").val(0);
    $("#fhdnCourseNo").val(0);
    $("#fhdnSemesterNo").val(0);
    $("#fhdnCollegeID").val(0);
    $("#fhdnCCode").val(0);
    $("#fhdnfacultyflag").val(0); 
    $("#BindDynamicINCTable").empty();
    $("#BindDynamicINCTable_wrapper").hide();
    $("#ctl00_ContentPlaceHolder1_ddlCampus").val(0).change();
    $("[id*=preloader]").hide();
    
} 
function ClearMarksData() {
    $("#ctl00_ContentPlaceHolder1_ddlStatus").val("2").change();
    $("#txtReasonRejection").val("").change();
    //$("#ctl00_ContentPlaceHolder1_ddlStatus").prop("disabled", true);
    $("#MarkEntryModal").modal("hide");
}

$('#btnClearMarks').click(function () {
    ClearMarksData();
});

$('#btnClear').click(function () { 
    ClearData();
}); 
$('#btnShow').click(function () {
    GolbleCancel=1;
    try { 
        var msg = ''; var str = ""; var count = 0; 
        if ($("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val() == '0')
            msg += "\r Please select academic session!!!  <br/>";
        if ($("#ctl00_ContentPlaceHolder1_ddlCampus").val() == '0')
            msg += "\r Please select campus!!  <br/>"; 
        if (msg != '') {
            iziToast.warning({
                message: msg,
            });
            return false;
        } 
        BindTable();
    }
    catch (ex) { 
    }
});

function BindTable() { 
    var Obj = {};
    var splitsubject = $("#ctl00_ContentPlaceHolder1_LstCourses").val().split('$$');
    var unk = splitsubject[0];
    courseNO = splitsubject[1];
    Obj.CampusNo = $("#ctl00_ContentPlaceHolder1_ddlCampus").val();
    Obj.SessionNo = $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val(); 
    Obj.Status = 0; 
    if (courseNO == undefined) {
        Obj.COURSENO = 0;
    }
    else {
        Obj.COURSENO = courseNO;
    }

    $.ajax({
        type: "POST",
        url: localurl + "Exam/INC_Approval.aspx/BindIncInfo",
        data: JSON.stringify(Obj),
        dataType: "json",
        contentType: "application/json",
        success: function (res) {
            if (res.d == '') {
                Swal.fire({
                    html: 'Record not found !!!',
                    icon: 'question'

                });
                $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val(0).change();
                $("#ctl00_ContentPlaceHolder1_ddlCampus").val(0).change();
                $("[id*=preloader]").hide();
                $("#dvtable").hide();
                return false; 
            }
            else{

                var str = "";
                var SRNo = 1;
                $("#BindDynamicINCTable").empty(''); 
                $("#BindDynamicINCTable_wrapper").show();
                if (res.d.length > 0) {
                    str = '<thead><tr><th><span class="SrNO">Sr.No</span></th><th><span class="studentId"></span></th><th><span class="studentName"></span></th><th><span class="Courses"></span></th><th><span class="Subjects"></span></th><th><span class="SemesterName"></span></th><th><span class="Status"></span></th></tr></thead><tbody>';
                    $.each(res.d, function (index, GetValue) {
                        str = str + '<tr>';
                        str = str + '<td> ' + SRNo + '</td>';
                        str = str + '<td><a href="#" data-bs-toggle="modal" data-bs-target="#MarkEntryModal" onclick="EditMarks(this)">' + GetValue.StudentID + ' </a><input type="hidden" id="hdnStudID" value="' + GetValue.StudentID + '"/><input type="hidden" id="hdnSchemno" value="' + GetValue.SCHEMNO + '"/><input type="hidden" id="hdnCourseNo" value="' + GetValue.COURSENO + '"/><input type="hidden" id="hdnSemesterNo" value="' + GetValue.SEMESTERNO + '"/><input type="hidden" id="hdnIdno" value="' + GetValue.Idno + '"/><input type="hidden" id="hdnCollegeID" value="' + GetValue.College_ID + '"/><input type="hidden" id="hdnCCode" value="' + GetValue.CCode + '"/><input type="hidden" id="hdnfacultyflag" value="' + GetValue.FACULTY_APPROVE_FLAG + '"/><input type="hidden" id="fhdnincflag" value="' + GetValue.INC_PROCESS_STATUS + '"/></td>'
                        str = str + '<td>' + GetValue.StudentName + '</td>';
                        str = str + '<td>' + GetValue.Course + '</td>';
                        str = str + '<td>' + GetValue.Subject + '</td>';
                        str = str + '<td>' + GetValue.Semester + '</td>'; 
                        if (GetValue.INC_PROCESS_STATUS=='')
                        {
                            str = str + '<td><span class="badge badge-warning">Pending</span></td>';
                        }
                        else if (GetValue.INC_PROCESS_STATUS=='1')
                        {
                            str = str + '<td><span class="badge badge-success">Approved</span></td>';
                        }
                        else if (GetValue.INC_PROCESS_STATUS=='0')
                        {
                            str = str + '<td><span class="badge badge-danger">Rejected</span></td>';
                        } 
                        str = str + '</tr>';
                        SRNo++;
                    });
                    str = str + '</tbody>'; 
                }
             
                $("#BindDynamicINCTable").append(str); 
                if (res.d.length > 0) {
                    $('#BindDynamicINCTable').show();
                    $("#BindDynamicINCTable_wrapper").show();
                    $("#dvtable").show();
                } else {
                    $('#BindDynamicINCTable').hide();
                    $("#BindDynamicINCTable_wrapper").hide();
                    $("#dvtable").hide();
                }
                RemoveTableDynamically('#BindDynamicINCTable');
                $("#BindDynamicINCTable").append(str);
                var BindDynamicINCTable = $('#BindDynamicINCTable')
                commonDatatable(BindDynamicINCTable)
            }
        }
    });
}

$("#ctl00_ContentPlaceHolder1_ddlCampus").change(function () { 
    try {
        if(GloblaCampus!=1){
            var msg='';
            var Obj1 = {};
            Obj1.SessionNO = $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val();
            Obj1.CampusNo = $("#ctl00_ContentPlaceHolder1_ddlCampus").val();

            if($("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val() == "0"){
                msg += "Please select academic session first !!!";
                GloblaCampus = 1;
                $("#ctl00_ContentPlaceHolder1_ddlCampus").val(0).change();
            }
            if (msg != '') {
                iziToast.warning({
                    message: msg,
                });
                return false;
            }

            $.ajax({
                url: localurl + "Exam/INC_Approval.aspx/BindCources",
                type: "POST",
                data: JSON.stringify(Obj1),
                dataType: "json",
                async: false,
                beforeSend: function () { $("[id*=preloader]").show(); },
                complete: function () { $("[id*=preloader]").hide(); },
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (result.d == '') {
                        if(GolbleCancel==0)
                        {
                            Swal.fire({
                                html: 'Course not found for this campus !!!',
                                icon: 'question'
                            });
                        } 
                        $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val(0).change();
                        $("#ctl00_ContentPlaceHolder1_ddlCampus").val(0); 
                        $("#ctl00_ContentPlaceHolder1_LstCourses").empty();
                        $("#ctl00_ContentPlaceHolder1_LstCourses").append($('<option></option>').val(0).html("Please Select"));
                        $("[id*=preloader]").hide();
                        $("#dvtable").hide();
                        GolbleCancel=0;
                        return false;
                    }
                    else {
                        $.each(result.d, function (data, value) {
                            $("#ctl00_ContentPlaceHolder1_LstCourses").append($("<option></option>").val(value.COURSENO).html(value.COURSENAME));
                        });
                    }
                },
                error: function ajaxError(result) {
                    Swal.fire({
                        html: 'Error Occurred !!!',
                        icon: 'error'
                    });
                }
            });
        }
    }
    catch (ex) {

    }
});

function checkmarkvalue(inc, abs, textboxid) {
    var txt = '#' + textboxid.id;
    var userInput = textboxid.value.trim();
    var msg = '';
    if (inc === 1 || abs === 1) {
        if (userInput.trim().toUpperCase() === "INC" || userInput.trim().toUpperCase() === "ABS" || !isNaN(userInput) && Number(userInput) <= 100) {
        } else {
            msg += "Invalid Marks. Please enter 'INC', 'ABS' or Marks  between 0 and 100 !!!";
            textboxid.value = '';
        }

    } else if (inc === 1 && abs == 0) {
        if (userInput.trim().toUpperCase() === "INC" || !isNaN(userInput) && Number(userInput) <= 100) {
        } else {
            msg += "Invalid Marks. Please enter 'INC'  or  Marks  between 0 and 100 !!!";
            textboxid.value = '';
        }

    } else if (inc === 0 && abs == 1) {
        if (userInput.trim().toUpperCase() === "ABS" || !isNaN(userInput) && Number(userInput) <= 100) {
        } else {
            msg += "Invalid Marks. Please enter 'ABS' or  Marks  between 0 and 100 !!!";
            textboxid.value = '';
        }
    }
    else if (inc === 0 || abs === 0) {
        if (!isNaN(userInput) && Number(userInput) <= 100) {
        } else {
            msg += "Invalid Marks. Please enter marks between 0 and 100 !!!";
            textboxid.value = '';
        }
    }
    if (msg != '') {
        iziToast.warning({
            message: msg,
        });
        return false;
    }
} 

function EditMarks(ClickValue) { 
    ClearMarksData();
    var obtainmark;
    var textboxid; 
    var ABSflag;
    var td = $("td", $(ClickValue).closest("tr"));
    var msg = ''; var str = ""; var count = 0; 
    $("#fhdnIdno").val($("[id*=hdnIdno]", td).val());
    $("#fhdnSchemno").val($("[id*=hdnSchemno]", td).val());
    $("#fhdnCourseNo").val($("[id*=hdnCourseNo]", td).val());
    $("#fhdnSemesterNo").val($("[id*=hdnSemesterNo]", td).val());
    $("#fhdnCollegeID").val($("[id*=hdnCollegeID]", td).val());
    $("#fhdnCCode").val($("[id*=hdnCCode]", td).val());
    $("#fhdnfacultyflag").val($("[id*=hdnfacultyflag]", td).val());
    $("#fhdnincflag").val($("[id*=hdnincflag]", td).val());

    if ( $("#fhdnSchemno").val()!=0 && $("#fhdnCourseNo").val()!=0)
    {
        GetStudentData();
    }
}

function GetStudentData() 
{
    if ( $("#fhdnSchemno").val()!=0 && $("#fhdnCourseNo").val()!=0)
    {
        var ObjCHECK = {};

        ObjCHECK.SessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
        ObjCHECK.CampusId = $('#ctl00_ContentPlaceHolder1_ddlCampus').val();
        ObjCHECK.schemno = $("#fhdnSchemno").val();
        ObjCHECK.courseno = $("#fhdnCourseNo").val();
        ObjCHECK.semno = $("#fhdnSemesterNo").val();
        ObjCHECK.idno =$("#fhdnIdno").val();
        //ObjCHECK.StudentNo = $("[id*=hdnStudID]", td).val();
        $.ajax({
            url: localurl + "Exam/INC_Approval.aspx/GetStudentMarks",
            type: "POST",
            data: JSON.stringify(ObjCHECK),
            dataType: "json",
            async: false,
            beforeSend: function () { $("[id*=preloader]").show(); },
            complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                if (response.d != '') {
                    str = '';
                    $("#divmakrs").empty();
                    $.each(response.d, function (index, GetValue) { 
                        str = str + '<div class="col-xl-3 col-lg-4 col-sm-6 col-12">';
                        str = str + '<div class="row">';
                        str = str + '<div class="col-12">';
                        str = str + '<div class="form-group">';
                        str = str + '<div class="label-dynamic">';
                        str = str + '<sup></sup>';
                        str = str + '<label>' + GetValue.EXAMCOMNONAME + '</label>';
                        str = str + '</div>'; 
                        if (GetValue.MARKS == 905) {
                            obtainmark = 'INC';
                        } else if (GetValue.MARKS == 902) {
                            obtainmark = 'ABS';
                        }
                        else if(GetValue.MARKS == 906){
                            obtainmark = 'HNA';
                        }
                        else if(GetValue.MARKS == 907){
                            obtainmark = 'DRP';
                        }
                        else if(GetValue.MARKS == 908){
                            obtainmark = 'WP';
                        }
                        else {
                            obtainmark = GetValue.MARKS;
                        } 
                        var textboxid = "txtUpdateMark_" + index; 
                        //if ( obtainmark == 'INC'     && $("#fhdnfacultyflag").val()==0 && $("#fhdnincflag").val()==0) {
                        //    str = str + '<input type="text" id="' + textboxid + '" class="form-control blnkcheck" value="' + obtainmark + '" onchange="checkmarkvalue(' + GetValue.Inc + ', ' + GetValue.Abs + ', ' + textboxid + ')" tabindex="0" spellcheck="true" /><input type="hidden" id="hdnINColdVal' + index + '" value="' + GetValue.MARKS + '"/> <input type="hidden" id="hdnfinalINC' + index + '" value="' + GetValue.INCStatus + '"/>';
                        //}  
                        //else if (GetValue.INCStatus == 1 && $("#fhdnfacultyflag").val()==0 && $("#fhdnincflag").val()==0) {
                        //    str = str + '<input type="text" id="' + textboxid + '" class="form-control blnkcheck" value="' + obtainmark + '" onchange="checkmarkvalue(' + GetValue.Inc + ', ' + GetValue.Abs + ', ' + textboxid + ')" tabindex="0" spellcheck="true" /><input type="hidden" id="hdnINColdVal' + index + '" value="' + GetValue.MARKS + '"/> <input type="hidden" id="hdnfinalINC' + index + '" value="' + GetValue.INCStatus + '"/>';
                        //}  
                        //else {
                        //    //str = str + '<label class="form-control blnkcheck dis-able" id= "Markval"' + index + '" >' + obtainmark + '</label>';

                        //    str = str + '<input type="text" id= "Markval"' + index + '" class="form-control blnkcheck" value="' + obtainmark + '" disabled />'; 
                        //}  

                        if (obtainmark === 'INC' && $("#fhdnfacultyflag").val() == 0 && $("#fhdnincflag").val() == 0) {
                            str += '<input type="text" id="' + textboxid + '" class="form-control blnkcheck" value="' + obtainmark + '" onchange="checkmarkvalue(' + GetValue.Inc + ', ' + GetValue.Abs + ', ' + textboxid + ')" tabindex="0" spellcheck="true" />';
                            str += '<input type="hidden" id="hdnINColdVal' + index + '" value="' + GetValue.MARKS + '"/> <input type="hidden" id="hdnfinalINC' + index + '" value="' + GetValue.INCStatus + '"/>';
                        } else {
                            str += '<input type="text" id="' + textboxid + '" class="form-control blnkcheck" value="' + obtainmark + '" disabled />';
                        }
                        str = str + '<input type="hidden" id="hdnExamno_' + index + '" value="' + GetValue.EXAMNO + '"/>';
                        str = str + '<input type="hidden" id="hdnExamCompno_' + index + '" value="' + GetValue.EXAMCOMNO + '"/>';
                        str = str + '<input type="hidden" id="hdnWeightages_' + index + '" value="' + GetValue.WEIGHTAGE + '"/>';
                        str = str + '<input type="hidden" id="hdnTeacherId" value="' + GetValue.TeacherId + '"/>';
                        str = str + '<input type="hidden" id="hdnincflag' + index + '" value="' + GetValue.Inc + '"/>';
                        str = str + '<input type="hidden" id="hdnabsflag' + index + '" value="' + GetValue.Abs + '"/>'; 
                        str = str + '</div>';
                        str = str + '</div>';
                        str = str + '</div>';
                        str = str + '</div>';
                        str = str + '</div>';
                    }); 
                    $("#divmakrs").append(str);  
                    if ($("#fhdnfacultyflag").val()==1)
                    {
                        $("#Checklock").hide(); 
                        $("#divstatus").hide(); 
                        $("#ctl00_ContentPlaceHolder1_ddlStatus").prop("disabled", true);
                    }
                    else
                    {
                        $("#Checklock").show();
                        $("#divstatus").show(); 
                        $("#ctl00_ContentPlaceHolder1_ddlStatus").prop("disabled", false);
                    }
                    $("#MarkEntryModal").modal("show");
                    $(".dis-able").css("background-color", "var(--bs-gray-100)");
                }
                else {
             
                    Swal.fire({
                        html: 'Record Not Found !!!',
                        icon: 'question'
                    });
                    $("#MarkEntryModal").modal("hide");
                    $("[id*=preloader]").hide();
                    return false;
                }
            },
            error: function ajaxError(response) {
                Swal.fire({
                    html: 'Error Occurred !!!',
                    icon: 'error'
                });
            }
        });
    }
} 
$("#btnSubmitMarks").click(function () { 
    var msg = '';
    var str = "";
    var count = 0;
    var data = [];
    var index = 0; 
    if ($("#ctl00_ContentPlaceHolder1_ddlStatus").val() == "" || $("#ctl00_ContentPlaceHolder1_ddlStatus").val() == null)
        msg += "\r Please Select Status!!  <br/>";
    if ($("#ctl00_ContentPlaceHolder1_ddlStatus").val() == "0" )
    {
        if ($("#txtReasonRejection").val() == '')
            msg += "\r Please Enter Reason!!  <br/>";
    }
    if (msg != '') {
        iziToast.warning({
            message: msg,
        });
        return false;
    } 
    $('.blnkcheck').each(function () {
        var inputValue = $(this).val().trim();
        if (inputValue === '') {
            isValid = false; 
            str = "\r Exam Component marks should not be empty !!!</br>";
        }
            //if (inputValue === 'INC') {
            //    isValid = false; 
            //    str = "\r Please udpate INC Exam Component marks !!!</br>";
            //}
        else 
        {   
            if ($('#hdnINColdVal'+ index + '').val() == "905" || $('#hdnfinalINC'+ index + '').val()=="1") {
                var VarINCStatus = $("#ctl00_ContentPlaceHolder1_ddlStatus").val(); 
                var VarReason = $("#txtReasonRejection").val();
                var VarIDNO = $("#fhdnIdno").val();
                var VarSESSIONNO = $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val();
                var VarCAMPUSNO = $("#ctl00_ContentPlaceHolder1_ddlCampus").val();
                var VarSCHEMNO =$("#fhdnSchemno").val();
                var VarCOURSENO =$("#fhdnCourseNo").val();
                var VarSEMESTERNO = $("#fhdnSemesterNo").val();
                var VarCOLLEGE_ID =  $("#fhdnCollegeID").val();
                var VarCCODE = $("#fhdnCCode").val();
                var VarEXAMCOMNO = $('#hdnExamCompno_'+ index + '').val();  
                var VarOLD_MARK = $('#hdnINColdVal'+ index + '').val();
                var VarEXAMNO = $('#hdnExamno_'+ index + '').val();  
                var VarMarks = $('#txtUpdateMark_'+ index + '').val(); 
                var VarNEW_MARK = VarMarks;  
                if(VarMarks == 'DRP'){
                    var VarNEW_MARK = 907; 
                    VarMarks = 907;
                }
                if(VarMarks == 'HNA'){
                    var VarNEW_MARK = 906; 
                    VarMarks = 906;
                }
                if(VarMarks == 'ABS'){
                    var VarNEW_MARK = 902;
                    VarMarks = 902;
                }
                if(VarMarks == 'WP'){
                    var VarNEW_MARK = 908;
                    VarMarks = 908;
                }
                if(VarMarks == 'INC'){
                    if (VarINCStatus == "0") {
                        VarNEW_MARK = 999;
                        VarMarks = 905;
                        VarOLD_MARK = 905;
                    }
                    else {
                        VarNEW_MARK = VarMarks;
                        VarOLD_MARK = 905; 
                        if (VarMarks == 'INC' || VarMarks == '' ) {
                            str += "\r Pease Insert INC Marks  !!!</br>";
                        } 
                    } 
                }
                var alldata = {
                    'INCStatus': VarINCStatus,
                    'Reason': VarReason,
                    'IDNO': VarIDNO,
                    'SESSIONNO': VarSESSIONNO,
                    'CAMPUSNO': VarCAMPUSNO,
                    'SCHEMNO': VarSCHEMNO,
                    'COURSENO': VarCOURSENO,
                    'SEMESTERNO': VarSEMESTERNO,
                    'COLLEGE_ID': VarCOLLEGE_ID,
                    'CCODE': VarCCODE,
                    'EXAMCOMNO': VarEXAMCOMNO,
                    'OLD_MARK': VarMarks,
                    'EXAMNO': VarEXAMNO,
                    'Marks': VarMarks,
                    'OLD_MARK': VarOLD_MARK,
                    'NEW_MARK': VarNEW_MARK
                } 
                data.push(alldata); 
            }
            index++;
        }
    });
    if (str != '') {
        iziToast.warning({
            message: str,
        });
        return false;
    } else {
        var delconf;
        Swal.fire({
            title: 'Are you sure you want to update the marks? Once locked, updates will not be allowed.?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            delconf: 'Yes'
        }).then((result) => {
            if (result.isConfirmed) {
        var StudentMarks = JSON.stringify(data);
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: localurl + "Exam/INC_Approval.aspx/SaveINCRecord",
            dataType: 'json',
            data: JSON.stringify({ 'UpdateMarks': StudentMarks }),
            beforeSend: function () { $("[id*=preloader]").show(); },
            //complete: function () { $("[id*=preloader]").hide(); },
            
            success: function (response) {
                iziToast.success({
                    message: 'Subject Marks  Updated Successfully !!!',
                });
                BindTable();
                $("#ctl00_ContentPlaceHolder1_ddlStatus").val(0).change();
                $("#txtReasonRejection").val("").change();
                $("#MarkEntryModal").modal("hide");

                $(".blnkcheck").prop("disabled", true),
                $("[id*=preloader]").hide();
            },
            error: function () {
                Swal.fire({
                    html: 'Error Occurred !!!',
                    icon: 'error'
                });
                return false;
            }
        });
    }
else {
            Swal.fire({
                html: response.d + '!!!',
                icon: 'error'
            });
    $("[id*=preloader]").hide();
    return false;
}
});
} 
});




function chagestatusinc()
{  
    GetStudentData();
    var index = 0;
    if ($("#ctl00_ContentPlaceHolder1_ddlStatus").val()==1)
    {
        $('#idsubstar').hide();
    }
    else
    {
        $('#idsubstar').show();
    } 
    $('.blnkcheck').each(function () {
        var inputValue = $(this).val().trim(); 
        var VarINCStatus = $("#ctl00_ContentPlaceHolder1_ddlStatus").val();  
        var VarMarks = $('#txtUpdateMark_'+ index + '').val();
        if (VarINCStatus == "0" ) {
            if ($('#hdnincflag' + index + '').val() == 1) {
                $('#txtUpdateMark_' + index).prop('disabled', true);
            }
            //else
            //{
            //    $('#txtUpdateMark_' + index).prop('disabled', false);
            //} 
        }
        else
        {
            if($('#txtUpdateMark_'+ index + '').val() == 'INC'){
                $('#txtUpdateMark_' + index).prop('disabled', false);
            }
            else{
                $('#txtUpdateMark_' + index).prop('disabled', true);
            }
        } 
        index++;
    });
}


