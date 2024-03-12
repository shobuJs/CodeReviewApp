//===============================================//
// MODULE NAME   : RFC ERP 
// PAGE NAME     : Grade Correction
// CREATION DATE : 
// CREATED BY    : 
// Modified BY   : Ekansh Moundekar
// Modified Date : 14-12-2023
//===============================================//
var liveurl = "../../../../";
var localurl = "../../";
function fnc100(value, min, max) {
    var msg = ''; var str = ""; var count = 0;
    value = value.replace(/\s/g, '');
    if (parseFloat(value) < 0 || isNaN(value))
        return 0;
    else if (parseFloat(value) > 100) {
        msg += "\r Marks should not greater than 100 !!! <br/>";
        if (msg != '') {
            iziToast.warning({
                message: msg,
            });
            return 0;
        }
    }
    else {
        return value;
    }
}

$(document).ready(function () {
    $('#hdncheck').val(0); 
});

$('#ctl00_ContentPlaceHolder1_ddlAcademicSession').on('change', function (e) {
    $('#divsubject').hide();
    $('#divstudent').hide();
    $('#hdncheck').val(0);
    $('#ctl00_ContentPlaceHolder1_ddlStudent').empty();
    $('#ctl00_ContentPlaceHolder1_ddlStudent').append("<option value='0'>Please Select</option>");
    $('#ctl00_ContentPlaceHolder1_ddlCollege').empty();
    $('#ctl00_ContentPlaceHolder1_ddlCollege').append("<option value='0'>Please Select</option>");
    $("#btnComputeGrade").hide(); 
});

$('#ctl00_ContentPlaceHolder1_ddlCampus').on('change', function (e) {
    if ($('#ctl00_ContentPlaceHolder1_ddlCampus').val() != "0") {
        var optionSelected = $("option:selected", this);
        var valueSelected = this.value;
        var Obj = {};
        Obj.SessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
        Obj.CampusId = valueSelected;
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: localurl+ "Exam/GradeCorrection.aspx/GetCollege",
            data: JSON.stringify(Obj),
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (result.d == '') {
                    Swal.fire({
                        html: 'College not found !!!',
                        icon: 'question'
                    });
                    $('#ctl00_ContentPlaceHolder1_ddlStudent').empty();
                    $('#ctl00_ContentPlaceHolder1_ddlStudent').append("<option value='0'>Please Select</option>");
                    $('#ctl00_ContentPlaceHolder1_ddlCollege').empty();
                    $('#ctl00_ContentPlaceHolder1_ddlCollege').append("<option value='0'>Please Select</option>");
                    $("[id*=preloader]").hide();
                    $('#divsubject').hide();
                    $('#divstudent').hide();
                    $('#hdncheck').val(0);
                    $("#btnComputeGrade").hide();
                    return false;
                } else {
                    $('#divsubject').hide();
                    $('#divstudent').hide();
                    $('#hdncheck').val(0);
                    $("#btnComputeGrade").hide();;

                    $('#ctl00_ContentPlaceHolder1_ddlStudent').empty();
                    $('#ctl00_ContentPlaceHolder1_ddlStudent').append("<option value='0'>Please Select</option>");
                    $('#ctl00_ContentPlaceHolder1_ddlCollege').empty();
                    $('#ctl00_ContentPlaceHolder1_ddlCollege').append("<option value='0'>Please Select</option>");
                    $.each(result.d, function (key, value) {
                        $("[id*=preloader]").show();
                        $("#ctl00_ContentPlaceHolder1_ddlCollege").append($("<option></option>").val(value.COLLEGE_ID).html(value.COLLEGE_NAME));

                    });
                    $("[id*=preloader]").hide();
                    return false;
                }
            },
            error: function ajaxError(result) {

                Swal.fire({
                    html: 'Error Occurred !!!',
                    icon: 'error'
                });
                return false;
            }
        });
    }
    else {
        $('#ctl00_ContentPlaceHolder1_ddlCollege').empty();
        $('#ctl00_ContentPlaceHolder1_ddlCollege').append("<option value='0'>Please Select</option>");
        $('#ctl00_ContentPlaceHolder1_ddlStudent').empty();
        $('#ctl00_ContentPlaceHolder1_ddlStudent').append("<option value='0'>Please Select</option>");
    }
});
$('#ctl00_ContentPlaceHolder1_ddlCollege').on('change', function (e) {

    if ($('#ctl00_ContentPlaceHolder1_ddlCollege').val() != "0") {
        try {
            var optionSelected = $("option:selected", this);
            var valueSelected = this.value;
            var Obj = {};
            Obj.SessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
            Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlCampus').val();
            Obj.College = valueSelected;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: localurl+ "Exam/GradeCorrection.aspx/Getstudent",
                data: JSON.stringify(Obj),
                dataType: "json",
                beforeSend: function () { $("[id*=preloader]").show(); },
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (result.d == '') {
                        Swal.fire({
                            html: 'Student not found !!!',
                            icon: 'question'
                        });
                        $('#ctl00_ContentPlaceHolder1_ddlStudent').empty();
                        $('#ctl00_ContentPlaceHolder1_ddlStudent').append("<option value='0'>Please Select</option>");
                        $("[id*=preloader]").hide();
                        $('#divsubject').hide();
                        $('#divstudent').hide();
                        $('#hdncheck').val(0);
                        $("#btnComputeGrade").hide();
                        return false;
                    } else {

                        $('#ctl00_ContentPlaceHolder1_ddlStudent').empty();
                        $('#ctl00_ContentPlaceHolder1_ddlStudent').append("<option value='0'>Please Select</option>");
                        $.each(result.d, function (key, value) {
                            $("[id*=preloader]").show();
                            $("#ctl00_ContentPlaceHolder1_ddlStudent").append($("<option></option>").val(value.IDNO).html(value.STUDENTNAME));

                        });
                        $("[id*=preloader]").hide();
                        $('#divsubject').hide();
                        $('#divstudent').hide();
                        $('#hdncheck').val(0);
                        $("#btnComputeGrade").hide();
                        return false;
                    }
                },
                error: function ajaxError(result) {
                    Swal.fire({
                        html: 'Error Occurred !!!',
                        icon: 'error'
                    });
                    return false;
                }
            });
        }
        catch (ex) {

        }
    }
    else {
        $('#ctl00_ContentPlaceHolder1_ddlStudent').empty();
        $('#ctl00_ContentPlaceHolder1_ddlStudent').append("<option value='0'>Please Select</option>");
    }
});
$('#ctl00_ContentPlaceHolder1_ddlStudent').on('change', function (e) {
    GetStudentGrade();
});

function GetStudentGrade() {
    var msg = ''; var str = ""; var count = 0;

    if ($("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val() == '0')
        msg += "\r Please select  Academic Session !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlCampus").val() == '0')
        msg += "\r Please Select Campus  !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlCollege").val() == '0')
        msg += "\r Please select College !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlStudent").val() == '0')
        msg += "\r Please select Student Name !!!</br>";

    if (msg != '') {
        iziToast.warning({
            message: msg,
        });
        return false;
    }
    var Obj = {};
    Obj.SessionId = $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val();
    Obj.CampusId = $("#ctl00_ContentPlaceHolder1_ddlCampus").val();
    Obj.CollegId = $("#ctl00_ContentPlaceHolder1_ddlCollege").val();
    Obj.StudentNo = $("#ctl00_ContentPlaceHolder1_ddlStudent").val();

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: localurl+ "Exam/GradeCorrection.aspx/GetData",
        data: JSON.stringify(Obj),
        dataType: "json",
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response.d == '') {
                Swal.fire({
                    html: 'Record not found !!!',
                    icon: 'question'
                });
                RemoveTableDynamically('#BindDynamicSubjectTable');
                RemoveTableDynamically('#BindDynamicStudentTable');
                $('#divsubject').hide();
                $('#divstudent').hide();
                $('#hdncheck').val(0);
                $("#btnComputeGrade").hide();
            }
            else {
                $('#hdncheck').val(0);
                $("#btnComputeGrade").hide();
                $('#divsubject').show();
                $("#BindDynamicSubjectTable").show();
                var rownum = 0;
                str = '<thead><tr><th>Sr.No.</th><th><span>Subject Code</span></th> <th><span class="SubjectName">Subject Name</span></th><th><span class="SubjectType">Subject Type</span></th> <th><span class="OverallGrade">Overall Grade</span></th></tr></thead><tbody>';
                $.each(response.d, function (index, GetValue) {
                    rownum = rownum + 1;
                    str = str + '<tr>'
                    str = str + '<td>' + rownum + '</td>'
                    str = str + '<td><a href="#" data-toggle="modal" data-target="#SubjDetailsModal" onclick="EditModeration(this)">' + GetValue.CCODE + ' </a>'
                    str = str + '<input type="hidden" id="hdnSchemno" value="' + GetValue.SCHEMENO + '"/><input type="hidden" id="hdnCourseNo" value="' + GetValue.COURSENO + '"/><input type="hidden" id="hdnsemsterno" value="' + GetValue.SEMESTERNO + '"/>'
                    str = str + '<input type="hidden" id="hdnidno" value="' + GetValue.IDNO + '"/><input type="hidden" id="hdnlmno" value="' + GetValue.LM_NO + '"/></td>'
                    str = str + '<td>' + GetValue.COURSE_NAME + '</td>'
                    str = str + '<td>' + GetValue.SUBNAME + '</td>'
                    str = str + '<td>' + GetValue.OVERALLGRADE + '</td>'
                    str = str + '</tr>'
                });
                str = str + '</tbody>';
                RemoveTableDynamically('#BindDynamicSubjectTable');
                $("#BindDynamicSubjectTable").append(str);
                var BindDynamicSubjectTable = $('#BindDynamicSubjectTable')
                commonDatatables(BindDynamicSubjectTable);
              
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: localurl+ "Exam/GradeCorrection.aspx/GetStudentHistory",
                    data: JSON.stringify(Obj),
                    dataType: "json",
                    beforeSend: function () { $("[id*=preloader]").show(); },
                    complete: function () { $("[id*=preloader]").hide(); },
                    contentType: "application/json;charset=utf-8",
                    success: function (response) {
                        if (response.d == '') {
                            //Swal.fire({
                            //    html: 'Record not found !!!',
                            //    icon: 'question'
                            //});
                            RemoveTableDynamically('#BindDynamicStudentTable'); 
                            $('#divstudent').hide();
                        }
                        else {
                            var SrNo = 1;
                            $('#divstudent').show();
                            $("#BindDynamicStudentTable").show();
                            str = '<thead><tr><th><span>Sr.No</span></th> <th><span class="StudentID">Student ID </span></th><th><span class="StudentName">Student Name </span></th> <th><span class="Course">Course</span></th><th><span class="Semester">Semester</span></th><th><span class="Subject">Subject</span></th><th><span class="CreatedBy">Created By</span></th><th><span class="CreatedDate">Created Date</span></th>  </tr></thead><tbody>';// <th><span class="CreatedTime">Created Time</span></th>
                            $.each(response.d, function (index, GetValue) {
                                str = str + '<tr>'
                                str = str + '<td>' + SrNo + '</td>'
                                str = str + '<td>' + GetValue.REGNO + '</td>'
                                str = str + '<td>' + GetValue.STUDNAME + '</td>'
                                str = str + '<td>' + GetValue.SCHEMENAME + '</td>'
                                str = str + '<td>' + GetValue.SEMESTERNAME + '</td>'
                                str = str + '<td>' + GetValue.COURSE_NAME + '</td>'
                                str = str + '<td>' + GetValue.CREATEDBYName + '</td>'
                                str = str + '<td>' + GetValue.CREATEDATE + '</td>' 
                                str = str + '</tr>'
                                SrNo++;
                            });
                            str = str + '</tbody>';
                            RemoveTableDynamically('#BindDynamicStudentTable');
                            $("#BindDynamicStudentTable").append(str);
                            var BindDynamicSubjectTable = $('#BindDynamicStudentTable')
                            commonDatatables(BindDynamicSubjectTable);
                            $("[id*=preloader]").hide(); 
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
        },
        error: function ajaxError(response) {
            Swal.fire({
                html: 'Error Occurred !!!',
                icon: 'error'
            });
        }
    });
    
}


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
        if ( !isNaN(userInput) && Number(userInput) <= 100) { 
        } else {
            msg += "Invalid Marks. Please enter Marks  between 0 and 100 !!!";
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


var schemno='';
var courseno='';
var semno='';
var lmno=0;
function EditModeration(ClickValue) { 
    var obtainmark;
    var textboxid;
  
    var ABSflag;
    var td = $("td", $(ClickValue).closest("tr")); 
    var msg = ''; var str = ""; var count = 0; 
    if ($("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val() == '0')
        msg += "\r Please select  Academic Session !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlCampus").val() == '0')
        msg += "\r Please Select Campus  !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlCollege").val() == '0')
        msg += "\r Please select College !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlStudent").val() == '0')
        msg += "\r Please select Student Name !!!</br>";

    if (msg != '') {
        iziToast.warning({
            message: msg,
        });
        return false;
    }
    schemno = $("[id*=hdnSchemno]", td).val()
    courseno = $("[id*=hdnCourseNo]", td).val()
    semno = $("[id*=hdnsemsterno]", td).val()
    lmno = 0;
    var Obj = {};
    var ObjCHECK = {};
    ObjCHECK.SessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
    ObjCHECK.CampusId = $('#ctl00_ContentPlaceHolder1_ddlCampus').val();
    ObjCHECK.CollegId = $('#ctl00_ContentPlaceHolder1_ddlCollege').val();
    ObjCHECK.StudentNo = $('#ctl00_ContentPlaceHolder1_ddlStudent').val();
    ObjCHECK.schemno = $("[id*=hdnSchemno]", td).val()
    ObjCHECK.courseno = $("[id*=hdnCourseNo]", td).val()
    ObjCHECK.semno = $("[id*=hdnsemsterno]", td).val()
    ObjCHECK.lmno =0;
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: localurl+ "Exam/GradeCorrection.aspx/GetStudentMarks",
        data: JSON.stringify(ObjCHECK),
        dataType: "json",
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response.d != '') {
                str = '';
                $("#divmakrs").empty();
                $.each(response.d, function (index, GetValue) {
                    str = str + '<div class="col-xl-3 col-lg-4 col-sm-6 col-12" >'
                    str = str + '<div class="row">'
                    str = str + '<div class="col-12">'
                    str = str + '<div class="form-group">'
                    str = str + '<div class="label-dynamic">'
                    str = str + '<sup></sup>'
                    str = str + '<label>' + GetValue.EXAMCOMNONAME + '</label>'
                    str = str + '</div>'
                    if (GetValue.MARKS == 905) {
                        obtainmark = 'INC' 
                    }
                    else if (GetValue.MARKS == 902) {
                        obtainmark = 'ABS'
                    }
                    else {
                        obtainmark = GetValue.MARKS;
                    } 
                    var textboxid = "txt" + index; 
                    str = str + '<input id=' + textboxid + ' class="form-control blnkcheck"  onchange="checkmarkvalue(' + GetValue.Inc + ', ' + GetValue.Abs + ',' + textboxid + ')"   tabindex="0" spellcheck="true"  value=' + obtainmark + ' />  <input type="hidden" id="hdnExamno_' + index + '" value="' + GetValue.EXAMNO + '"/><input type="hidden" id="hdnExamCompno_' + index + '" value="' + GetValue.EXAMCOMNO + '"/><input type="hidden" id="hdnWeightages_' + index + '" value="' + GetValue.WEIGHTAGE + '"/> <input type="hidden" id="hdnTeacherId" value="' + GetValue.TeacherId + '"/> <input type="hidden" id="hdnincflag' + index + '" value="' + GetValue.Inc + '"/><input type="hidden" id="hdnabsflag' + index + '" value="' + GetValue.Abs + '"/>'
                    str = str + '</div>'
                    str = str + '</div>'
                    str = str + '</div>'
                    str = str + '</div>'
                    str = str + '</div>'
                });
                $("#btnSaveGrade").show();
                $(".blnkcheck").prop("disabled", false);
                $("#divmakrs").append(str);
                $("#SubjDetailsModal").modal("show");
            }
            else {
                Swal.fire({
                    html: 'no record found !!!',
                    icon: 'error'
                });
                $("#SubjDetailsModal").modal("hide");
                $("[id*=preloader]").hide();
                return false;
            }
        }
    });
} 
$("#btnClear").click(function () {
    
    GetStudentGrade();
    $("#SubjDetailsModal").modal("hide");
});
//$("#btnmoclose").click(function () {
//    GetStudentGrade();
//});
$("#btnSaveGrade").click(function () { 
    var msg = ''; var str = ""; var count = 0;

    if ($("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val() == '0')
        str += "\r Please select  Academic Session !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlCampus").val() == '0')
        str += "\r Please Select Campus  !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlCollege").val() == '0')
        str += "\r Please select College !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlStudent").val() == '0')
        str += "\r Please select Student Name !!!</br>";

    if (str != '') {
        iziToast.warning({
            message: str,
        });
        return false;
    }
    var data = [];
    var index = 0;
    $('.blnkcheck').each(function () {
        var inputValue = $(this).val().trim();
        // Check if the input is empty 

        if (inputValue === '') {
            isValid = false;
            msg += "\r Exam Component marks should not be empty !!!</br>";
        }
        else {
            var VarSessionNo = $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val();
            var VarCampusNo = $("#ctl00_ContentPlaceHolder1_ddlCampus").val();
            var VarCollegeId = $("#ctl00_ContentPlaceHolder1_ddlCollege").val();
            var VarLMno = 0;
            var VarUANo = $("#hdnTeacherId").val();
            var VarIdNo = $("#ctl00_ContentPlaceHolder1_ddlStudent").val();
            var VarREGNO = 0;
            //var VarSchemeNo = $('#hdnSchemno').val();
            //var VarCourseNo = $('#hdnCourseNo').val();
            //var VarSemeterNo = $('#hdnlmno').val();

            var VarSchemeNo =schemno;
            var VarCourseNo = courseno;
            var VarSemeterNo = semno;

            var VarExamNo = $('#hdnExamno_' + index + '').val();
            var VarExamCompNo = $('#hdnExamCompno_' + index + '').val();
            var VarWeightages = $('#hdnWeightages_' + index + '').val();
            var VarMarks;
         
            if  ($(this).val().trim().toUpperCase() =='INC')
            {
                VarMarks='905';
            }
            else if ($(this).val().trim().toUpperCase() =='ABS')
            {
                VarMarks='902';
            }
            else
            {   VarMarks = $(this).val().trim();
            }
            var alldata = {
                'SessionNo': VarSessionNo,
                'CampusId': VarCampusNo,
                'CollegeId': VarCollegeId,
                'Lmno': VarLMno,
                'UANO': VarUANo,
                'REGNO': 0,
                'SCHEMENO': VarSchemeNo,
                'COURSENO': VarCourseNo,
                'SEMESTERNO': VarSemeterNo,
                'EXAMNO': VarExamNo,
                'EXAMCOMNO': VarExamCompNo,
                'MARKS': VarMarks,
                'IDNO': VarIdNo,
                'WEIGHTAGE': VarWeightages
            }
            index++;
            data.push(alldata);
        } 
    });
    if (msg != '') {
        iziToast.warning({
            message: msg,
        });
        return false;
    }
    var StudentMarks = JSON.stringify(data);
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: localurl+ "Exam/GradeCorrection.aspx/InsertMarks",
        dataType: 'json',
        data: JSON.stringify({ 'UpdateMarks': StudentMarks }),
        beforeSend: function () { $("[id*=preloader]").show(); },
        //complete: function () { $("[id*=preloader]").hide(); },
        success: function () {
            iziToast.success({ 
                message: 'Subject Marks  Updated Successfully !!!',
            });
            $("#btnSaveGrade").hide();
            $('#hdncheck').val(1); 
                $(".blnkcheck").prop("disabled", true);
            $("#btnComputeGrade").show();
        },
        error: function () {
            Swal.fire({
                html: 'Error Occurred !!!',
                icon: 'error'
            });
            return false;
        }

    });
});
 


$("#btnComputeGrade").click(function () {  
    var msg = ''; var str = ""; var count = 0;

    if ($("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val() == '0')
        str += "\r Please select  Academic Session !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlCampus").val() == '0')
        str += "\r Please Select Campus  !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlCollege").val() == '0')
        str += "\r Please select College !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlStudent").val() == '0')
        str += "\r Please select Student Name !!!</br>";

    if (str != '') {
        iziToast.warning({
            message: str,
        });
        return false;
    }

    $('.blnkcheck').each(function () {
        var inputValue = $(this).val().trim();
        // Check if the input is empty 

        if (inputValue === '') {
            isValid = false;
            msg += "\r Exam Component marks should not be empty !!!</br>";
        }
    });
    if (msg != '') {
        iziToast.warning({
            message: msg,
        });
        return false;
    }
    var delconfirm; 
    Swal.fire({
        title: 'Are you sure to compute Grade?', 
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
Obj.Modelity = 0;
Obj.scheme = schemno;
Obj.strSemesterNo = semno;

//Obj.CourseNo = $('#hdnCourseNo').val();
//Obj.Modelity = $("#hdnlmno").val();
//Obj.scheme = $('#hdnSchemno').val();
//Obj.strSemesterNo = $('#hdnlmno').val();
Obj.TeacherId = $("#hdnTeacherId").val();
Obj.IDNO = $("#ctl00_ContentPlaceHolder1_ddlStudent").val();
$.ajax({
    url: localurl+ "Exam/GradeCorrection.aspx/finalGradecompute",
    type: "POST",
    data: JSON.stringify(Obj),
    dataType: "json",
    beforeSend: function () { $("[id*=preloader]").show(); },
    contentType: "application/json;charset=utf-8",
    success: function (response) {
        if (response.d == "1") {
            Swal.fire(
                'Submitted!',
                'Grade Compute Successfully.',
                'success'
            );
            GetStudentGrade();
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

