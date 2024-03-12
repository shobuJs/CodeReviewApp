//===============================================//
// MODULE NAME   : RFC ERP 
// PAGE NAME     : INC Approval
// CREATION DATE : 18-10-2023
// CREATED BY    : Gaurav Kalbande
// Modified BY   : Ekansh Moundekar
// Modified Date : 
//===============================================//
var liveurl = "../../../";
var localurl = "../../";
$(document).ready(function () { 
    $('.select2Task').select2({ dropdownParent: $('#MarkEntryModal') });    

}); 
$("#ctl00_ContentPlaceHolder1_ddlCampus").change(function () {
    try {
     
        var ObjCourse = {};
        ObjCourse.SessionNO = $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val();
        ObjCourse.CampusNo = $("#ctl00_ContentPlaceHolder1_ddlCampus").val();
        $.ajax({
            url: localurl + "Exam/INC_Approval_Dean.aspx/BindCources",
            type: "POST",
            data: JSON.stringify(ObjCourse),
            dataType: "json",
            async: false,
            beforeSend: function () { $("[id*=preloader]").show(); },
            complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $("#ctl00_ContentPlaceHolder1_ddlCourse").empty();
                $("#ctl00_ContentPlaceHolder1_ddlCourse").append($('<option></option>').val(0).html("Please Select"));
                $.each(result.d, function (data, value) {
                    $("#ctl00_ContentPlaceHolder1_ddlCourse").append($("<option></option>").val(value.COURSENO).html(value.COURSENAME));
                });
            },
            error: function ajaxError(result) {
                Swal.fire({
                    html: 'Error Occurred !!!',
                    icon: 'error'
                });
            }
        });
    }
    catch (ex) {

    }
});

$('#btnShow').click(function () {
    try { 
        var msg = ''; var str = ""; var count = 0;

        if ($("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val() == '0')
            msg += "\r Please Select Accademic Session!!!  <br/>";
        if ($("#ctl00_ContentPlaceHolder1_ddlCampus").val() == '0')
            msg += "\r Please Select Campus!!  <br/>"; 

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
    var splitsubject = $("#ctl00_ContentPlaceHolder1_ddlCourse").val().split('$$');
    var arraySize = splitsubject.length;
    Obj.SessionNo = $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val();
    Obj.CampusNo = $("#ctl00_ContentPlaceHolder1_ddlCampus").val();
    if (arraySize == 1) {
        Obj.Schemno = 0;
        Obj.CourseNo = 0;
    }
    else {
        Obj.Schemno = splitsubject[0];
        Obj.CourseNo = splitsubject[1];
    }
    $.ajax({
        type: "POST",
        url: localurl + "Exam/INC_Approval_Dean.aspx/BindStudentInfo",
        data: JSON.stringify(Obj),
        dataType: "json",
        beforeSend: function () { $("[id*=preloader]").show(); },
        //complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response.d == '') {
                Swal.fire({
                    html: 'Record Not Found !!!',
                    icon: 'question'
                });
                $("#divshow").hide();
                $("[id*=preloader]").hide();
                return false;
            } else {
                var SRNo = 1;
                var str = "";
                $("#divshow").show();
                $("#BindDynamicINCTable").show();
                $("#BindDynamicINCTable").empty('');  
                str = '<thead><tr><th><span class="SrNO">Sr.No</span></th><th><span class="studentId"></span></th><th><span class="studentName">studentName</span></th><th><span class="Courses"></span></th> <th><span class="Curriculum"></span></th> <th><span class="Subject">Subjects</span></th><th><span class="FNGRelease">Final Grade Released Status</span></th><th><span class="GWACStatus">GWA Computation Status</span></th></tr></thead><tbody>';
                $.each(response.d, function (index, GetValue) {
                    str = str + '<tr>';
                    str = str + '<td> ' + SRNo + '</td>';
                    str = str + '<td><a href="#" data-bs-toggle="modal" data-bs-target="#MarkEntryModal" onclick="EditMarks(this)">' + GetValue.StudentID + ' </a><input type="hidden" id="hdnSchemno" value="' + GetValue.SCHEMNO + '"/><input type="hidden" id="hdnCourseNo" value="' + GetValue.COURSENO + '"/><input type="hidden" id="hdnSemesterNo" value="' + GetValue.SEMESTERNO + '"/><input type="hidden" id="hdnIdno" value="' + GetValue.Idno + '"/><input type="hidden" id="hdnUANO" value="' + GetValue.UA_NO + '"/><input type="hidden" id="hdnmodelity" value="' + GetValue.LEARNINGMODALITYNO + '"/><input type="hidden" id="hdncollegeid" value="' + GetValue.COLLEGE_ID + '"/><input type="hidden" id="hdnrelease" value="' + GetValue.RELEASEGRADE_FLAG + '"/><input type="hidden" id="hdngwagrade" value="' + GetValue.GWA_FLAG + '"/></td>'
                    str = str + '<td>' + GetValue.StudentName + '</td>';
                    str = str + '<td>' + GetValue.Course + '</td>';
                    str = str + '<td>' + GetValue.SCHEMENAME + '</td>';
                    str = str + '<td>' + GetValue.Subject + '</td>';
                    if (GetValue.RELEASEGRADE_FLAG == 1) {
                        str = str + '<td><span class="badge badge-success">Approved</span></td>';
                    }
                    else {
                        str = str + '<td><span class="badge badge-warning">Pending</span></td>';
                    }
                    if (GetValue.GWA_FLAG == 1) {
                        str = str + '<td><span class="badge badge-success">Approved</span></td>';
                    }
                    else {
                        str = str + '<td><span class="badge badge-warning">Pending</span></td>';
                    } 
                    str = str + '</tr>';
                    SRNo++;
                });
                str = str + '</tbody>';  
          
                RemoveTableDynamically('#BindDynamicINCTable');
                $("#BindDynamicINCTable").append(str);
                var BindDynamicINCTable = $('#BindDynamicINCTable')
                commonDatatable(BindDynamicINCTable)
             
            }
        },
        error: function (errResponse) {
            $("[id*=preloader]").hide();
            return false;
         
        }
    });
}

$('#btnClear').click(function () {

    ClearData();
});

$('#btnClearMarks').click(function () {

    $("#MarkEntryModal").modal("hide");
});

function ClearData() { 
    $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val(0).change();
    $("#ctl00_ContentPlaceHolder1_ddlCampus").val(0).change();
    $("#ctl00_ContentPlaceHolder1_LstCourses").val(0).change();
    $("#ctl00_ContentPlaceHolder1_INCStauts").val(0).change(); 
    $("#BindDynamicINCTable").hide();
    $("#divshow").hide(); 
}
var schemno='';
var courseno='';
var semno='';
var lmno='';
var idno='';
var uano='';
var collegeid='';
var ReleaseGradeFlage='';
var Resultprocessfleg='';
var indexcheck='';

function EditMarks(ClickValue) { 
    try {
       
        var td = $("td", $(ClickValue).closest("tr"));
        //var ObjSubmission = {};
        //ObjSubmission.strSessionId = $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val();
        //ObjSubmission.strCampusId = $("#ctl00_ContentPlaceHolder1_ddlCampus").val();;
        //ObjSubmission.strCourseNo = $("[id*=hdnCourseNo]", td).val();
        //ObjSubmission.strModelity = $("[id*=hdnmodelity]", td).val();
        //ObjSubmission.strSchemno = $("[id*=hdnSchemno]", td).val();
        //ObjSubmission.strSemesterNo = $("[id*=hdnSemesterNo]", td).val();
        //ObjSubmission.strTecharID = $("[id*=hdnUANO]", td).val();
        //ObjSubmission.IDNO = $("[id*=hdnIdno]", td).val();

        schemno = $("[id*=hdnSchemno]", td).val();
        courseno = $("[id*=hdnCourseNo]", td).val();
        semno = $("[id*=hdnSemesterNo]", td).val();
        lmno = $("[id*=hdnmodelity]", td).val();
        idno = $("[id*=hdnIdno]", td).val();
        uano = $("[id*=hdnUANO]", td).val();
        collegeid = $("[id*=hdncollegeid]", td).val();
        ReleaseGradeFlage= $("[id*=hdnrelease]", td).val();
        Resultprocessfleg= $("[id*=hdngwagrade]", td).val(); 
        indexcheck= $("[id*=hdnindex]", td).val()
        refreshGrade();
      
    }
    catch (ex) {
    } 
}

function refreshGrade()
{
    if (ReleaseGradeFlage==1)
    {
        $("#btnComputeGrade").hide();
        $("#btncomun").show();
        $("#btnGWA").show();
        $("#btngwaun").hide();

    }else
    {
        $("#btnComputeGrade").show();
        $("#btncomun").hide();
        $("#btngwaun").show();
        $("#btnGWA").hide();
    }
    if (Resultprocessfleg==1)
    {  $("#btngwaun").show();
        $("#btnGWA").hide();
    }
 
    var ObjSubmission = {};
    ObjSubmission.strSessionId = $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val();
    ObjSubmission.strCampusId = $("#ctl00_ContentPlaceHolder1_ddlCampus").val();;
    ObjSubmission.strCourseNo = courseno;
    ObjSubmission.strModelity =lmno
    ObjSubmission.strSchemno = schemno;
    ObjSubmission.strSemesterNo =semno;
    ObjSubmission.strTecharID = uano;
    ObjSubmission.IDNO =idno;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: localurl + "Exam/INC_Approval_Dean.aspx/StudentList",
        data: JSON.stringify(ObjSubmission),
        dataType: "json",
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json;charset=utf-8",
        success: function (Result) {
            if (Result.d == '') {
            }
            else {

                BindGradSubmission(Result.d, indexcheck);
                $("#MarkEntryModal").modal("show");
            }
        },
        error: function ajaxError(Result) {
            Swal.fire({
                html: 'Error Occurred !!!',
                icon: 'error'
            });
        }
    });
}

function BindGradSubmission(data, index) {
    var valno = 1;
    var table = $('#BindDynamicSubmissionTable');
    var headerRow = $('#thaverage');
    data = JSON.parse(data);
    headerRow.empty();
    table.find('tbody').empty();
    $.each(data, function (index, item) {
        var row = $('<tr>');
        if (index === 0) {
            $.each(item, function (key, value) {
                if (key == "SRNo") {
                    headerRow.append($('<th style="Display:none;">').text(key))
                }
                else if (key == "CCODE") {
                    headerRow.append($('<th>').text('Subject Code'));
                }
                else if (key == "COURSE_NAME") {
                    headerRow.append($('<th>').text('Subject Name'));
                }
                else if (key == "UA_FULLNAME") {
                    headerRow.append($('<th>').text('Teacher Name'));
                }
                else if (key == "CAMPUSNAME") {
                    headerRow.append($('<th>').text('Campus Name'));
                }
                else if (key == "SEMESTERNAME") {
                    headerRow.append($('<th>').text('Semester'));
                }
                else if (key == "IDNO") {
                    headerRow.append($('<th style="Display:none;">').text(key))
                }
                else {
                    headerRow.append($('<th>').text(key));
                }
            });
        }
        table.append(row);
        $.each(item, function (key, value) { 

            if (key == "SRNo") {
                row.append($('<td style="Display:none;">').text(value));
            }
            else if (key == "IDNO") {
                row.append($('<td style="Display:none;">').text(value));
            }
            else {
                if (!isNaN(value) && parseFloat(value) === value) {
                    row.append($('<td>').text(value.toFixed(2)));   // Use toFixed to add .00

                } else {
                    row.append($('<td>').text(value));
                }
            }
        });
        valno++;
        table.append(row);
    });
}

$("#btnComputeGrade").click(function () {  
 
    var msg = ''; var str = ""; var count = 0;

    if ($("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val() == '0')
        str += "\r Please select  Academic Session !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlCampus").val() == '0')
        str += "\r Please Select Campus  !!!</br>"; 
     
    if (str != '') {
        iziToast.warning({
            message: str,
        });
        return false;
    } 
   
    var delconfirm; 
    Swal.fire({
        title: 'Are you sure to release Grade?',   
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes',
        delconfirm:'Yes'
    }).then((result) => { 
        if (result.isConfirmed) {  
        var Obj = {};
Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlCampus').val(); 
Obj.CourseNo = courseno;
Obj.Modelity = lmno;
Obj.scheme = schemno;
Obj.strSemesterNo = semno;  
Obj.TeacherId =  uano;
Obj.IDNO = idno;
$.ajax({
    url: localurl+ "Exam/INC_Approval_Dean.aspx/finalGradecompute",
    type: "POST",
    data: JSON.stringify(Obj),
    dataType: "json",
    beforeSend: function () { $("[id*=preloader]").show(); },
    contentType: "application/json;charset=utf-8",
    success: function (response) {

        if (response.d == "1") {
           
            BindTable();
            refreshGrade();
            Swal.fire(
                'Submitted!',
                'Grade Computed Successfully.',
                'success'
            ); 
            $("#btnComputeGrade").hide();
            $("#btncomun").show();
            $("#btnGWA").show();
            $("#btngwaun").hide(); 
            $("[id*=preloader]").hide();
        }
    }, 
    error: function () {  
        Swal.fire({
            html: 'Error Occurred !!!',
            icon: 'error'
        });
        $("[id*=preloader]").hide();
    }
});   
}
});     
});

$("#btnGWA").click(function () {  
    var msg = ''; var str = ""; var count = 0;

    if ($("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val() == '0')
        str += "\r Please select  Academic Session !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlCampus").val() == '0')
        str += "\r Please Select Campus  !!!</br>"; 
     
    if (str != '') {
        iziToast.warning({
            message: str,
        });
        return false;
    } 
   
    var delconfirm; 
    Swal.fire({
        title: 'Are you sure to Process Result?',   
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes',
        delconfirm:'Yes'
    }).then((result) => { 
        if (result.isConfirmed) {  
        var Obj = {};
Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlCampus').val();  
Obj.scheme = schemno;
Obj.strSemesterNo = semno;  
Obj.collegeid =  collegeid; 
Obj.IDNO = idno;
Obj.Modelity = lmno; 
$.ajax({
    url: localurl+ "Exam/INC_Approval_Dean.aspx/GWAProcess",
    type: "POST",
    data: JSON.stringify(Obj),
    dataType: "json",
    beforeSend: function () { $("[id*=preloader]").show(); },
    contentType: "application/json;charset=utf-8",
    success: function (response) {
        if (response.d == "1") {
            BindTable();
            Swal.fire(
                'Submitted!',
                'Result Processed Successfully.',
                'success'
            ); 
            $("#btnComputeGrade").hide();
            $("#btncomun").show();
            $("#btnGWA").hide();
            $("#btngwaun").show();
          
            $("[id*=preloader]").hide();
        }
        else
        {
            if (response.d == "2") {
                Swal.fire({
                    html: 'The grades for all subjects have not been released yet. !!!',
                    icon: 'error'
                });
                $("[id*=preloader]").hide();
            } 
        }
    }, 
    error: function () {  
        Swal.fire({
            html: 'Error Occurred !!!',
            icon: 'error'
        });
        $("[id*=preloader]").hide();
    }
});   
}
});     
});

