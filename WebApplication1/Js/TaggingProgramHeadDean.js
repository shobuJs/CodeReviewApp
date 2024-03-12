//===============================================//
// MODULE NAME   : RFC ERP 
// PAGE NAME     : Tagging of Program Head and Dean
// CREATION DATE : 11-10-2023
// CREATED BY    : Ekansh Moundekar
// Modified BY   : Ekansh Moundekar
// Modified Date :  11-10-2023
//===============================================//
var liveurl = "../../../";
var localurl = "../";
$(document).ready(function () {
    $('#btnSubmit').hide();
    $("#supProgramHead").hide();
    $("#supDean").hide();
});
$('#ctl00_ContentPlaceHolder1_ddlAcademicSession').on('change', function (e) {
    $("#BindDynamicTaggingTable").empty();
    $("#btnSubmit").hide();
    // $("#divtable").hide();
    $('#ctl00_ContentPlaceHolder1_ddlCampus').val(0).change();
});
// -------------------------- Get ProgramHead and Dean -----------------------//
$('#ctl00_ContentPlaceHolder1_ddlCampus').on('change', function (e) {
    $("#BindDynamicTaggingTable").empty();
    $("#btnSubmit").hide();
    //  $("#divtable").hide();
    if ($('#ctl00_ContentPlaceHolder1_ddlCampus').val() != '0') {
        var msg = '';
        if ($('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val() == '0') {
            msg += "\r Please Select Academic Session !!! <br/>";
            $('#ctl00_ContentPlaceHolder1_ddlCampus').val('0');
        }
        if (msg != '') {
            iziToast.warning({
                message: msg,
            });
            return false;
        }
        $("[id*=preloader]").show();
        var Obj = {};
        //  Obj.dropdwon = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val() + ',' + $('#ctl00_ContentPlaceHolder1_ddlCampus').val();
        Obj.dropdwon = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: liveurl + "Exam/TaggingProgramHeadDean.aspx/GetFacultyname",
            data: JSON.stringify(Obj),
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (result.d == '') {
                    Swal.fire({
                        html: 'No ProgramHead Found !!!',
                        icon: 'question'
                    });
                    $('#ctl00_ContentPlaceHolder1_ddlProgramHead').empty();
                    $('#ctl00_ContentPlaceHolder1_ddlProgramHead').append("<option value='0'>Please Select</option>");
                    $('#ctl00_ContentPlaceHolder1_ddlDean').empty();
                    $('#ctl00_ContentPlaceHolder1_ddlDean').append("<option value='0'>Please Select</option>");
                    return false;
                }
                else {
                    $("[id*=preloader]").show();
                    $('#ctl00_ContentPlaceHolder1_ddlProgramHead').empty();
                    $('#ctl00_ContentPlaceHolder1_ddlProgramHead').append("<option value='0'>Please Select</option>");
                    $('#ctl00_ContentPlaceHolder1_ddlDean').empty();
                    $('#ctl00_ContentPlaceHolder1_ddlDean').append("<option value='0'>Please Select</option>");
                    $.each(result.d, function (key, value) {
                        $("#ctl00_ContentPlaceHolder1_ddlProgramHead").append($("<option></option>").val(value.UA_NO).html(value.UA_FULLNAME));
                        $("#ctl00_ContentPlaceHolder1_ddlDean").append($("<option></option>").val(value.UA_NO).html(value.UA_FULLNAME));
                    });
                    $("[id*=preloader]").hide();
                }
            },
            error: function ajaxError(result) {
                Swal.fire({
                    html: 'Error Occurred !!!',
                    icon: 'error'
                });
                $("[id*=preloader]").hide();
                return false;
            }
        });
        //Bindig College 
        var ObjCollege = {};
        ObjCollege.CampusID = $('#ctl00_ContentPlaceHolder1_ddlCampus').val();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: liveurl + "Exam/TaggingProgramHeadDean.aspx/GetCollege",
            data: JSON.stringify(ObjCollege),
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            success: function (res) {
                if (res.d == '') {
                    swal.fire({
                        html: 'No College Found !!!',
                        icon: 'question'
                    });
                    return false;
                }
                else {
                    $("[id*=preloader]").show();
                    $('#ctl00_ContentPlaceHolder1_ddlCollege').empty();
                    $('#ctl00_ContentPlaceHolder1_ddlCollege').append("<option value='0'>please select</option>");
                    $.each(res.d, function (key, value) {
                        $("#ctl00_ContentPlaceHolder1_ddlCollege").append($("<option></option>").val(value.COLLEGE_ID).html(value.COLLEGE_NAME));
                    });
                    $("[id*=preloader]").hide();
                }
            },
            error: function ajaxError(result) {
                Swal.fire({
                    html: 'Error Occurred !!!',
                    icon: 'error'
                });
                $("[id*=preloader]").hide();
                return false;
            }
        });
        // for Binding Subject Classfication
        //$.ajax({
        //    type: "POST",
        //    contentType: "application/json; charset=utf-8",
        //    url: liveurl + "Exam/TaggingProgramHeadDean.aspx/GetSubjectClassification",
        //    dataType: "json",
        //    beforeSend: function () { $("[id*=preloader]").show(); },
        //    contentType: "application/json;charset=utf-8",
        //    success: function (res) {
        //        if (res.d == '') {
        //            swal.fire({
        //                html: 'No Subject Found !!!',
        //                icon: 'question'
        //            });
        //            return false;
        //        }
        //        else {
        //            $("[id*=preloader]").show();
        //            $('#ctl00_ContentPlaceHolder1_ddlSubjectClassification').empty();
        //            $('#ctl00_ContentPlaceHolder1_ddlSubjectClassification').append("<option value='0'>please select</option>");
        //            $.each(res.d, function (key, value) {
        //                $("#ctl00_ContentPlaceHolder1_ddlSubjectClassification").append($("<option></option>").val(value.SUBCLASSIFIC_NO).html(value.SUBCLASSIFIC_NAME));
        //            });
        //            $("[id*=preloader]").hide();
        //        }
        //    },
        //    error: function ajaxError(result) {
        //        Swal.fire({
        //            html: 'Error Occurred !!!',
        //            icon: 'error'
        //        });
        //        $("[id*=preloader]").hide();
        //        return false;
        //    }
        //});
    }

    Getcurriculum(0);
    GetSemester(0);
});
// -------------------------- End ProgramHead and Dean -----------------------//
// ----------------------- Get Curriculam Start ---------------------------- //
function Getcurriculum(selectedValue) {
    var Obj = {};
    Obj.CollegeID = selectedValue;
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: liveurl + "Exam/TaggingProgramHeadDean.aspx/GetCurriculam",
        data: JSON.stringify(Obj),
        dataType: "json",
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json;charset=utf-8",
        success: function (res) {
            if (res.d == '') {
                swal.fire({
                    html: 'No Curriculam Found !!!',
                    icon: 'question',
                });
                $('#ctl00_ContentPlaceHolder1_ddlCurriculum').empty();
                $('#ctl00_ContentPlaceHolder1_ddlCurriculum').append("<option value='0'>please select</option>");

                $("[id*=preloader]").hide();
                return false;
            }
            else {
                $('#ctl00_ContentPlaceHolder1_ddlCurriculum').empty();
                $('#ctl00_ContentPlaceHolder1_ddlCurriculum').append("<option value='0'>please select</option>");
                $.each(res.d, function (key, value) {
                    $("#ctl00_ContentPlaceHolder1_ddlCurriculum").append($("<option></option>").val(value.SCHEMENO).html(value.SCHEMENAME));
                });
                $("[id*=preloader]").hide();
            }
        },
        error: function ajaxError(result) {
            Swal.fire({
                html: 'Error Occurred !!!',
                icon: 'error'
            });
            $("[id*=preloader]").hide();
            return false;
        }
    });
}
$('#ctl00_ContentPlaceHolder1_ddlCollege').on('change', function (e) {
    var CollegeId = $('#ctl00_ContentPlaceHolder1_ddlCollege').val();
    Getcurriculum(CollegeId);
});
// ------------------------ Curriculam End --------------------------- //
// ----------------------- Bind Semester ----------------------------- //
$('#ctl00_ContentPlaceHolder1_ddlCurriculum').on('change', function (e) {

    var SchemeId = $('#ctl00_ContentPlaceHolder1_ddlCurriculum').val();
    GetSemester(SchemeId)
});
function GetSemester(selectedvalue) {
    try {
        $("#ctl00_ContentPlaceHolder1_ddlSemester").empty();
        var Obj = {};
        Obj.SchemeId = selectedvalue;
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: liveurl + "Exam/TaggingProgramHeadDean.aspx/GetSemester",
            data: JSON.stringify(Obj),
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (result.d == '') {
                    Swal.fire({
                        html: 'Semester not found',
                        icon: 'question'
                    });
                    $('#ctl00_ContentPlaceHolder1_ddlSemester').empty();
                    $('#ctl00_ContentPlaceHolder1_ddlSemester').append("<option value='0'>Please select</option>");
                    $("[id*=preloader]").hide();

                } else {
                    $('#ctl00_ContentPlaceHolder1_ddlSemester').empty();
                    $('#ctl00_ContentPlaceHolder1_ddlSemester').append("<option value='0'>Please select</option>");
                    $.each(result.d, function (key, value) {
                        $("#ctl00_ContentPlaceHolder1_ddlSemester").append($("<option></option>").val(value.SEMESTERNO).html(value.SEMESTERNAME));
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
    catch (ex) {
    }
}
// ----------------------- End Semester -------------------------//
//-------------------- Cancle Button Start //----------------------------------//
function ClearTaggingProgram() {
    $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val(0).change();
    $('#ctl00_ContentPlaceHolder1_ddlCampus').val(0).change();
    $('#ctl00_ContentPlaceHolder1_ddlProgramHead').empty();
    $('#ctl00_ContentPlaceHolder1_ddlProgramHead').append("<option value='0'>Please select</option>");
    $('#ctl00_ContentPlaceHolder1_ddlDean').empty();
    $('#ctl00_ContentPlaceHolder1_ddlDean').append("<option value='0'>Please select</option>");
    $('#ctl00_ContentPlaceHolder1_ddlCollege').empty();
    $('#ctl00_ContentPlaceHolder1_ddlCollege').append("<option value='0'>Please select</option>");
    $('#ctl00_ContentPlaceHolder1_ddlCurriculum').empty();
    $('#ctl00_ContentPlaceHolder1_ddlCurriculum').append("<option value='0'>Please select</option>");
    $('#ctl00_ContentPlaceHolder1_ddlSemester').empty();
    $('#ctl00_ContentPlaceHolder1_ddlSemester').append("<option value='0'>Please select</option>");
    //$('#ctl00_ContentPlaceHolder1_ddlSubjectClassification').empty();
    //$('#ctl00_ContentPlaceHolder1_ddlSubjectClassification').append("<option value='0'>Please select</option>");
    $("#btnSubmit").hide();
    $("#BindDynamicTaggingTable").empty();
    $("#BindDynamicTaggingTable").hide();
    $("#divtable").hide();
    $('#selectall').prop('checked', false);
    $("#supProgramHead").hide();
    $("#supDean").hide();
}
$("#btnClear").click(function () {
    try {
        ClearTaggingProgram();
    }
    catch (ex) {

    }
});
//--------------- Cancel Button End ----------------
function GetRecord() {
    try {
        $("#divtable").hide();
        $('#selectall').prop('checked', false);
        var msg = '';
        if ($('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val() == '0')
            msg += "\r Please Select Academic Session !!! <br/>";
        if ($('#ctl00_ContentPlaceHolder1_ddlCampus').val() == '0')
            msg += "\r Please Select Campus !!! <br/>";

        if (msg != '') {
            iziToast.warning({
                message: msg,
            });
            return false;
        }
        var Obj = {};
        Obj.SessionId = $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val();
        Obj.CampusId = $("#ctl00_ContentPlaceHolder1_ddlCampus").val();
        if ($("#ctl00_ContentPlaceHolder1_ddlCollege").val() == '') {
            Obj.COLLEGE_ID = 0;
        } else {
            Obj.COLLEGE_ID = $("#ctl00_ContentPlaceHolder1_ddlCollege").val();
        }
        if ($("#ctl00_ContentPlaceHolder1_ddlCurriculum").val() == '') {
            Obj.Schemeno = 0;
        } else {
            Obj.Schemeno = $("#ctl00_ContentPlaceHolder1_ddlCurriculum").val();
        }

        //if ($("#ctl00_ContentPlaceHolder1_ddlSubjectClassification").val() == '') {
        //    Obj.SubClassId = 0;
        //} else {
        //    Obj.SubClassId = $("#ctl00_ContentPlaceHolder1_ddlSubjectClassification").val();
        //}

        if ($("#ctl00_ContentPlaceHolder1_ddlSemester").val() == null) {
            Obj.Semsterno = 0;
        } else {
            Obj.Semsterno = $("#ctl00_ContentPlaceHolder1_ddlSemester").val();
        }

        if ($("#ctl00_ContentPlaceHolder1_ddlProgramHead").val() == '') {
            Obj.ProgrammHeadId = 0;
        } else {
            Obj.ProgrammHeadId = $("#ctl00_ContentPlaceHolder1_ddlProgramHead").val();
        }

        if ($("#ctl00_ContentPlaceHolder1_ddlDean").val() == '') {
            Obj.DeanId = 0;
        } else {
            Obj.DeanId = $("#ctl00_ContentPlaceHolder1_ddlDean").val();
        }

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: liveurl + "Exam/TaggingProgramHeadDean.aspx/GetSubjectTagging",
            data: JSON.stringify(Obj),
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            timeout: 100000,
            success: function (response) {
                if (response.d.length === 0) {
                    Swal.fire({
                        html: 'Record not found !!!',
                        icon: 'question'
                    });
                    $("#supProgramHead").hide();
                    $("#supDean").hide();
                    $("#btnSubmit").hide();
                    $("#divtable").hide();
                    RemoveTableDynamically('#BindDynamicTaggingTable');
                    $("[id*=preloader]").hide();
                    return false;
                } else {
                    $("#divtable").show();
                    $("#BindDynamicTaggingTable").show();
                    var str = '<thead><tr><th><span><input type="checkbox" id="selectall"  onClick="checkPage(this)"></span></th><th><span class="Semester">Semester</span></th><th><span class="Subject">Subject</span></th> <th><span class="Curriculum">Curriculum</span></th>  <th><span class="ProgramHead">Program Head</span></th> <th><span class="Dean">Dean</span></th></tr></thead><tbody>';
                   // var str = '<thead><tr><th><span><input type="checkbox" id="selectall"  onClick="checkPage(this)"></span></th><th><span class="Semester">Semester</span></th><th><span class="Subject">Subject</span></th> <th><span class="SubjectClassification">Subject Classification</span></th> <th><span class="Curriculum">Curriculum</span></th>  <th><span class="ProgramHead">Program Head</span></th> <th><span class="Dean">Dean</span></th></tr></thead><tbody>';
                    $.each(response.d, function (index, GetValue) {
                        str += '<tr class="Trclass">';
                        str += '<td> <input type="checkbox" /> <input type="hidden" id="hdncollgeid" value="' + GetValue.COLLEGEID + '"/><input type="hidden" id="hdnPrGid" value="' + GetValue.PRGDEAN_TAGGINGID + '"/><input type="hidden" id="hdncourseno" value="' + GetValue.COURSENO + '"/><input type="hidden" id="hdnSchemeno" value="' + GetValue.SCHEMENO + '"/><input type="hidden" id="hdnSemeter" value="' + GetValue.SEMESTERNO + '"/><input type="hidden" id="hdnClassId" value="' + GetValue.SUBCLASSIFICID + '"/></td>';
                        str += '<td>' + GetValue.SEMESTERNAME + '</td>';
                        str += '<td>' + GetValue.SubjectName + '</td>';
                       // str += '<td>' + GetValue.SUBCLASSIFIC_NAME + '</td>';
                        str += '<td>' + GetValue.Curriculum + '</td>';
                        str += '<td>' + GetValue.PROGRAMHEADNAME + '</td>';
                        str += '<td>' + GetValue.DEANNAME + '</td>';
                        str += '</tr>';
                    });

                    str += '</tbody>';
                    RemoveTableDynamically('#BindDynamicTaggingTable');
                    $("#BindDynamicTaggingTable").append(str);
                    var BindDynamicTable = $('#BindDynamicTaggingTable');
                    commonDatatables(BindDynamicTaggingTable);
                    $("#supProgramHead").show();
                    $("#supDean").show();
                    $("#btnSubmit").show();
                }
            },
            error: function ajaxError(response) {
                Swal.fire({
                    html: 'Error Occured !!!',
                    icon: 'error'
                });
            }
        });
    }
    catch (ex) {
    }
}
$('#btnShow').click(function () {
    GetRecord();
})
function checkPage(bx) {
    var cbs = document.getElementsByTagName('input');
    for (var i = 0; i < cbs.length; i++) {
        if (cbs[i].type == 'checkbox') {
            cbs[i].checked = bx.checked;
        }
    }
}
// -------------------Button Submit Start ----------------------//
function BindSubjectTagging() {
    var data = [];
    var valno = 0;
    var VarSessionId = $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val();
    var VarCampus = $("#ctl00_ContentPlaceHolder1_ddlCampus").val();
    var VarProgramHead = $("#ctl00_ContentPlaceHolder1_ddlProgramHead").val();
    var VarDean = $("#ctl00_ContentPlaceHolder1_ddlDean").val();
    var VarISACTIVE = 1;
    $("tr.Trclass input[type=checkbox]:checked").each(function () {
        var td = $(this).closest("tr")[0];
        var VarCollegeId = $("[id*=hdncollgeid]", td).val();
        var VarClasssId = $("[id*=hdnClassId]", td).val();
        var VarScheme = $("[id*=hdnSchemeno]", td).val();
        var VarCourse = $("[id*=hdncourseno]", td).val();
        var VarSem = $("[id*=hdnSemeter]", td).val();
        var VarTAGGINGID = $("[id*=hdnPrGid]", td).val();

        var alldata = {
            'TAGGINGID': VarTAGGINGID,
            'SESSIONNO': VarSessionId,
            'CAMPUSNO': VarCampus,
            'COLLEGEID': VarCollegeId,
            'SCHEMENO': VarScheme,
            'COURSENO': VarCourse,
            'SUBCLASSIFIC_NO': VarClasssId,
            'SEMESTERNO': VarSem,
            'PROGRAMHEAD_UANO': VarProgramHead,
            'DEAN_UANO': VarDean,
            'ALLOTED': 1,
            'ACTIVE': VarISACTIVE

        }
        data.push(alldata);
    });
    console.log(data);//  
    return data;
}

$("#btnSubmit").click(function () {

    var msg = ''; var msg1 = '';
    if ($('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val() == '0')
        msg += "\r Please Select Academic Session !!! <br/>";
    if ($('#ctl00_ContentPlaceHolder1_ddlCampus').val() == '0')
        msg += "\r Please Select Campus !!! <br/>";

    if ($('#ctl00_ContentPlaceHolder1_ddlProgramHead').val() == '0')
        msg += "\r Please Select Program Head !!! <br/>";
    if ($('#ctl00_ContentPlaceHolder1_ddlDean').val() == '0')
        msg += "\r Please Select Dean !!! <br/>";
    if ($("input[type='checkbox']:checked").length === 0) {
        msg1 += "\r Please Select at least one CheckBox From Below Table !!! <br/>";
    }
    if (msg != '') {
        iziToast.warning({
            message: msg,
        });
        return false;
    }
    if (msg1 != '') {
        iziToast.warning({
            message: msg1,
        });
        return false;
    }

    var data = JSON.stringify(BindSubjectTagging());
    $.ajax({
        url: liveurl + "Exam/TaggingProgramHeadDean.aspx/InUpSubjectTagging",
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ 'SubjectData': data }),
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        success: function (response) {
            if (response.d == "1") {
                iziToast.success({
                    message: 'Subject Tagging Added Successfully !!!',
                });
                $("[id*=preloader]").show();
                GetRecord();
                ClearTaggingProgram();
                return false;
            }
            else {
                iziToast.success({
                    message: 'Error while inserting data !!!',
                });
                ClearTaggingProgram();
                return false;
            }


        },
        error: function (errResponse) {
            Swal.fire({
                html: 'Error Occurred !!!',
                icon: 'error'
            });
            return false;
        }
    });
});

// -------------------Button Submit End ----------------------//