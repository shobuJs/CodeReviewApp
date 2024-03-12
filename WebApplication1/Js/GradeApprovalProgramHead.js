//===============================================//
// MODULE NAME   : RFC ERP 
// PAGE NAME     : Grade Approval by Program Head
// CREATION DATE : 03-08-2023
// CREATED BY    : harshal bobde
// Modified BY   : Harsal Bobde
// Modified Date : 12-02-2024
//===============================================//
var liveurl = "../../../../";
var localurl = "../";
$(document).ready(function () {
    //---------------- css for badge ---------------------//
    $(".text-orange").css("color", "#ff9422");
    $(".text-green").css("color", "#44d793");
    $(".bg-green").css({ "background-color": "#44d793", "color": "#ffffff" });
    $(".bg-orange").css({ "background-color": "#ff9422", "color": "#ffffff" });
 
});
$('#ctl00_ContentPlaceHolder1_ddlAcademicSession').on('change', function (e) {   
    var optionSelected = $("option:selected", this);
    var valueSelected = this.value;
    var Obj = {};
    Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
    Obj.CampusId = valueSelected;
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url:  liveurl + "Exam/GradeApprovalProgramHead.aspx/GetExamPattern",
        data: JSON.stringify(Obj),
        dataType: "json",
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json;charset=utf-8",
        success: function (result) {
            if (result.d == '') { 
                $("[id*=preloader]").hide();
                return false;
                $('#ctl00_ContentPlaceHolder1_ddlExamPattern').empty();
                $('#ctl00_ContentPlaceHolder1_ddlExamPattern').append("<option value='0'>Please Select</option>");
                $('#ctl00_ContentPlaceHolder1_ddlExam').empty();
                $('#ctl00_ContentPlaceHolder1_ddlExam').append("<option value='0'>Please Select</option>"); 
            }
            else {
                $('#ctl00_ContentPlaceHolder1_ddlExamPattern').empty();
                $('#ctl00_ContentPlaceHolder1_ddlExamPattern').append("<option value='0'>Please Select</option>");
                $.each(result.d, function (key, value) { 
                    $("[id*=preloader]").show();
                    $("#ctl00_ContentPlaceHolder1_ddlExamPattern").append($("<option></option>").val(value.PATTERNNO).html(value.pattern_name));
                }); 
                       
            }
        },
        error: function ajaxError(result) {
            Swal.fire({
                html: 'Error Occurred !!!',
                icon: 'error'
            });
            return false;;
        }
    });
    $("#tblmarkdata").empty(); 
});
 
$('#ctl00_ContentPlaceHolder1_ddlCampus').on('change', function (e) {
    if ($('#ctl00_ContentPlaceHolder1_ddlCampus').val() != "0") {
        var msg = ''; var str = ""; var count = 0;

        if ($("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val() == '0')
            msg += "\r Please select Academic Session !!!</br>";    
        if (msg != '') { 
            iziToast.warning({
                message: msg,
            }); 
            $('#ctl00_ContentPlaceHolder1_ddlCampus').val(0);
            return false; 
        } 
        try {
         
            $('#ctl00_ContentPlaceHolder1_ddlExam').empty();
            $('#ctl00_ContentPlaceHolder1_ddlExam').append("<option value='0'>Please Select</option>"); 
            var optionSelected = $("option:selected", this);
            var valueSelected = this.value;
            var Obj = {};
            Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
            Obj.CampusId = valueSelected;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url:  liveurl + "Exam/GradeApprovalProgramHead.aspx/GetCurriculum",
                data: JSON.stringify(Obj),
                dataType: "json",
                beforeSend: function () { $("[id*=preloader]").show(); },
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (result.d == '') {
                        Swal.fire({
                            html: 'Curriculum  not found !!!',
                            icon: 'question'
                        }); 
                        $("#tblmarkdata").empty();  
                        $('#ctl00_ContentPlaceHolder1_ddlExamPattern').empty();
                        $('#ctl00_ContentPlaceHolder1_ddlExamPattern').append("<option value='0'>Please Select</option>");
                        $('#ctl00_ContentPlaceHolder1_ddlExam').empty();
                        $('#ctl00_ContentPlaceHolder1_ddlExam').append("<option value='0'>Please Select</option>"); 
                        $("[id*=preloader]").hide();
                        return false;
                    }else
                    {
                        $('#ctl00_ContentPlaceHolder1_ddlCurriculum').empty();
                        $('#ctl00_ContentPlaceHolder1_ddlCurriculum').append("<option value='0'>Please Select</option>");
                        $.each(result.d, function (key, value) {
                            $("[id*=preloader]").show();
                            $("#ctl00_ContentPlaceHolder1_ddlCurriculum").append($("<option></option>").val(value.SCHEMENO).html(value.SCHEMENAME));

                        });
                        $("#tblmarkdata").empty(); 
                        $("[id*=preloader]").hide();
                        return false;
                    }
                },
                error: function ajaxError(result) {
                    Swal.fire({
                        html: 'Error Occurred !!!',
                        icon: 'error'
                    });
                    return false;;
                }
            });
      
            $("#tblmarkdata").empty(); 
        }
        catch (ex) {

        }
    }
    else { 
        $('#ctl00_ContentPlaceHolder1_ddlExamPattern').empty();
        $('#ctl00_ContentPlaceHolder1_ddlExamPattern').append("<option value='0'>Please Select</option>");
        $('#ctl00_ContentPlaceHolder1_ddlExam').empty();
        $('#ctl00_ContentPlaceHolder1_ddlExam').append("<option value='0'>Please Select</option>"); 
        $("#tblmarkdata").empty(); 
    }
});

$('#ctl00_ContentPlaceHolder1_ddlExamPattern').on('change', function (e) {
 
    if ($('#ctl00_ContentPlaceHolder1_ddlExamPattern').val() != "0") {
        try {
            var optionSelected = $("option:selected", this);
            var valueSelected = this.value;
            var Obj = {};
            Obj.PatternNo = valueSelected;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url:  liveurl + "Exam/GradeApprovalProgramHead.aspx/GetExamName",
                data: JSON.stringify(Obj),
                dataType: "json",
                beforeSend: function () { $("[id*=preloader]").show(); },
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (result.d == '') {
                        Swal.fire({
                            html: 'Exam Name not found !!!',
                            icon: 'question'
                        }); 
                        $('#ctl00_ContentPlaceHolder1_ddlExam').empty();
                        $('#ctl00_ContentPlaceHolder1_ddlExam').append("<option value='0'>Please Select</option>");
                        $("#tblmarkdata").empty(); 
                        $("[id*=preloader]").hide();
                        return false;
                    }else
                    {

                        $('#ctl00_ContentPlaceHolder1_ddlExam').empty();
                        $('#ctl00_ContentPlaceHolder1_ddlExam').append("<option value='0'>Please Select</option>");
                        $.each(result.d, function (key, value) {
                            $("[id*=preloader]").show();
                            $("#ctl00_ContentPlaceHolder1_ddlExam").append($("<option></option>").val(value.EXAMNO).html(value.EXAMNAME));

                        });
                        $("[id*=preloader]").hide();
                        $("#tblmarkdata").empty(); 
                        return false;
                    }
                },
                error: function ajaxError(result) {
                    Swal.fire({
                        html: 'Error Occurred !!!',
                        icon: 'error'
                    });
                    $("#tblmarkdata").empty(); 
                    return false;;
                }
            });
        }
        catch (ex) {

        }
    }
    else { 
        $('#ctl00_ContentPlaceHolder1_ddlExam').empty();
        $('#ctl00_ContentPlaceHolder1_ddlExam').append("<option value='0'>Please Select</option>");
        $("#tblmarkdata").empty(); 
    }
}); 
$('#ctl00_ContentPlaceHolder1_ddlExam').on('change', function (e) {
    $("#tblmarkdata").empty(); 
});

function Approvalmarkdata()
{
    var msg = ''; var str = ""; var count = 0;

    if ($("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val() == '0')
        msg += "\r Please select  Academic Session !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlCampus").val() == '0')
        msg += "\r Please Select Campus  !!!</br>"; 

     
    if ($("#ctl00_ContentPlaceHolder1_ddlExamPattern").val() == '0')
        msg += "\r Please select  Exam Pattern !!!</br>"; 

    if ($("#ctl00_ContentPlaceHolder1_ddlExam").val() == '0')
        msg += "\r Please select  Exam !!!</br>";

    if (msg != '') {
        iziToast.warning({
            message: msg,
        });
        return false;
    } 
    var Obj = {};
    Obj.SessionId = $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val();
    Obj.CampusId = $("#ctl00_ContentPlaceHolder1_ddlCampus").val();
    Obj.CurriculumId = 0;
    Obj.ExamPatternId = $("#ctl00_ContentPlaceHolder1_ddlExamPattern").val();
    Obj.ModalityId = 0;
    Obj.ExamId = $("#ctl00_ContentPlaceHolder1_ddlExam").val();

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url:  liveurl + "Exam/GradeApprovalProgramHead.aspx/GetData",
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
                $("#tblmarkdata").empty(); 
                $("#tblmarkdata").hide(); 
            } 
            else {
                $("#tblmarkdata").show();
                str = '<thead><tr><th><span>Report</span></th> <th><span class="View">View</span></th><th><span class="SubjectCode">Subject Code</span></th> <th><span class="SubjectName">Subject Name</span></th>  <th><span class="TeacherName">Teacher Name</span></th> <th><span class="StartDate">Start Date</span></th> <th><span class="Apprdate">Approval Due Date</span></th> <th><span class="Status">Status</span></th> <th><span class="Date">Date</span></th><th></th><th></th></tr></thead><tbody>';

                $.each(response.d, function (index, GetValue) {
                    str = str + '<tr>'
                    str = str + '<td><i class="fa fa-file-excel-o text-success display-6 me-2" title="Excel" onclick="HODExcel(this)"></i>  </td>'//<i class="bi bi-filetype-pdf text-success display-6" title="PDF"></i>
                    str = str + '<td><i class="fa fa-plus-square-o text-primary display-6 bi-plus-square" id="StdList'+index+'" onclick="StdList(this)" ></i><input type="hidden" id="hdnExamNo" value="' + GetValue.EXAMNO + '"/><input type="hidden" id="hdncoursename" value="' + GetValue.COURSENAME + '"/>  <input type="hidden" id="hdncourseNo" value="' + GetValue.COURSENO + '"/> <input type="hidden" id="hdnteacherid" value="' + GetValue.TeacherId + '"/><input type="hidden" id="hdnindex" value="' + index + '"/></td>'
 
                    str = str + '<td>' + GetValue.CCODE + '</td>'
                    str = str + '<td>' + GetValue.COURSENAME + '</td>'
                    str = str + '<td>' + GetValue.Techername + '</td>'
                    str = str + '<td>' + GetValue.STARTDATE + '</td>'
                    str = str + '<td>' + GetValue.PROGENDDATE + '</td>'
                    if (GetValue.Substatus=='P')
                    {
                        str = str + '<td><span class="badge badge-warning">Pending</span></td>'
                        str = str + '<td>' + GetValue.DateStatus + '</td>'
                    }
                    else if (GetValue.Substatus=='NP')
                    {
                        str = str + '<td><span class="badge badge-primary">Need Approval</span></td>'
                        str = str + '<td>' + GetValue.DateStatus + '</td>'
                    
                    }
                    else if (GetValue.Substatus=='OM')
                    {
                        str = str + '<td><span class="badge badge-warning">Open for Moderation</span></td>'
                        str = str + '<td>' + GetValue.MODDATE + '</td>'
                    
                    }  
                    else if (GetValue.Substatus=='SD')
                    {str = str + '<td><span class="badge badge-secondary">Submitted to Dean</span></td>'
                        str = str + '<td>' + GetValue.DateStatus + '</td>'
                    
                    }
                    else if (GetValue.Substatus=='F')
                    {str = str + '<td><span class="badge badge-secondary">Finalized</span></td>'
                        str = str + '<td>' + GetValue.DateStatus + '</td>'
                    }
                    else if (GetValue.Substatus=='RS')
                    {
                        str = str + '<td><span class="badge badge-success">Released for Students</span></td>'
                        str = str + '<td>' + GetValue.DateStatus + '</td>'
                    }
                   

                    if (GetValue.Substatus=='NP'){
                        str = str + '<td> <input type="button" class="btn btn-sm btn-outline-primary" tabindex="0" value="Open for Moderation" id="btnModeration" onclick="EditModeration(this)"/></td>'
                        str = str + '<td><input type="button" class="btn btn-sm btn-outline-primary" tabindex="0" value="Submit to Dean" onclick="SubmitDean(this)" id="btnSubmitDean" /></td>'
                    }
                    else
                    {
                        str = str + '<td> <input type="button" class="btn btn-sm btn-outline-primary" tabindex="0" value="Open for Moderation"   disabled/></td>'
                        str = str + '<td><input type="button" class="btn btn-sm btn-outline-primary" tabindex="0" value="Submit to Dean"  disabled/></td>'
                    }
                    str = str + '</tr>' 
                    str = str + '<tr>'
                    str = str + '<td></td>'
                    str = str + '<td colspan="11">'
                    str = str + '<table class="table table-striped table-bordered nowrap" id="BindDynamicSubmissionTable'+index+'">'
                    str = str + ' <thead><tr id="thaverage'+index+'"></tr></thead>'
                    str = str + ' <tbody></tbody>   </table>' 
                    str = str + '</td>'
                    str = str + '</tr>' 
                });
                str = str + '</tbody>';
                RemoveTableDynamically('#tblmarkdata');
                $("#tblmarkdata").append(str);
                var BindDynamicTable = $('#tblmarkdata')
                commonDatatable(BindDynamicTable)
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

$("#btnShow").click(function () { 
    Approvalmarkdata();
}); 
function StdList(ClickValue) { 

    var td = $("td", $(ClickValue).closest("tr"));
    var table = document.getElementById("BindDynamicSubmissionTable" + $("[id*=hdnindex]", td).val() + "");

    if (ClickValue.classList.contains('bi-plus-square')) {
        ClickValue.classList.remove('bi-plus-square');
        ClickValue.classList.add('bi-file-minus');
        table.style.display = "table"; 
        var msg = ''; var str = ""; var count = 0;

        if ($("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val() == '0')
            msg += "\r Please select  Academic Session !!!</br>";

        if ($("#ctl00_ContentPlaceHolder1_ddlCampus").val() == '0')
            msg += "\r Please Select Campus  !!!</br>";
         

        if ($("#ctl00_ContentPlaceHolder1_ddlExamPattern").val() == '0')
            msg += "\r Please select Exam Pattern !!!</br>"; 

        if ($("#ctl00_ContentPlaceHolder1_ddlExam").val() == '0')
            msg += "\r Please select  Exam !!!</br>";

        if (msg != '') {
            iziToast.warning({
                message: msg,
            });
            return false;
        }
        try {
            var ObjSubmission = {};
            ObjSubmission.strSessionId = $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val();
            ObjSubmission.strCampusId = $("#ctl00_ContentPlaceHolder1_ddlCampus").val();;
            ObjSubmission.strCourseNo = $("[id*=hdncourseNo]", td).val();
            ObjSubmission.strModelity = 0;
            ObjSubmission.strEXAMCOMNO = 0;
            ObjSubmission.strEXAMNO = $("#ctl00_ContentPlaceHolder1_ddlExam").val();
            ObjSubmission.strSchemno =  0;
            ObjSubmission.strSemesterNo =  0;
            ObjSubmission.strTecharID =  $("[id*=hdnteacherid]", td).val();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url:  liveurl + "Exam/GradeApprovalProgramHead.aspx/StudentList",
                data: JSON.stringify(ObjSubmission),
                dataType: "json",
                beforeSend: function () { $("[id*=preloader]").show(); },
                complete: function () { $("[id*=preloader]").hide(); },
                contentType: "application/json;charset=utf-8",
                success: function (Result) {
                    if (Result.d == '') {
                    }
                    else {
                        BindGradSubmission(Result.d, $("[id*=hdnindex]", td).val());
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
        catch (ex) {
        }

    } 
    else { 
        ClickValue.classList.remove('bi-file-minus');
        ClickValue.classList.add('bi-plus-square');
        table.style.display = "none";  
    } 
   
}
function BindGradSubmission(data, index) { 
    var valno = 1;
    var table = $('#BindDynamicSubmissionTable'+index+'');
    var headerRow = $('#thaverage' + index + '');
    data = JSON.parse(data);
    headerRow.empty();
    table.find('tbody').empty();
    $.each(data, function (index, item) {
        var row = $('<tr>');
        if (index === 0) {
            $.each(item, function (key, value) {
                if (key == "REGNO") {
                    headerRow.append($('<th>').text('Student ID'));
                }
                else if (key == "STUDENTNAME") {
                    headerRow.append($('<th>').text('Student Name'));
                }
                else if (key == "BRANCH_CODE") {
                    headerRow.append($('<th>').text('Subject Name'));
                }
                else if (key == "CAMPUSNAME") {
                    headerRow.append($('<th>').text('Campus Name'));
                }
                else if (key == "SEMESTERNAME") {
                    headerRow.append($('<th>').text('Semester'));
                }
                else if (key == "EXAMNO" || key == "COMPONENTNO" || key == "IDNO") {
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
                row.append($('<td>').text(valno));
            }
            else if (key == "EXAMNO" || key == "COMPONENTNO" || key == "IDNO") {
                row.append($('<td style="Display:none;">').text(value));
            }
            else {
                if (!isNaN(value) && parseFloat(value) === value) { 
                    row.append($('<td>').text(value.toFixed(2))); 

                } else { 
                    row.append($('<td>').text(value));
                }
                
            }

        });
        valno++;
        table.append(row);
    });  
}
var vcourseno="";
var vExamNo="";
var vSchemeno="";
var vteacherid="";
var vsemno="";
function EditModeration(ClickValue) {    
     
    var td = $("td", $(ClickValue).closest("tr"));    
    vcourseno=$("[id*=hdncourseNo]", td).val();
    vExamNo=$("[id*=hdnExamNo]", td).val();
    vSchemeno=0; 
    vteacherid=$("[id*=hdnteacherid]", td).val(); 
    vsemno=0; 
    var msg = ''; var str = ""; var count = 0;

    if ($("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val() == '0')
        msg += "\r Please select  Academic Session !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlCampus").val() == '0')
        msg += "\r Please Select Campus  !!!</br>"; 

    if ($("#ctl00_ContentPlaceHolder1_ddlExamPattern").val() == '0')
        msg += "\r Please select  Exam Pattern !!!</br>"; 

    if ($("#ctl00_ContentPlaceHolder1_ddlExam").val() == '0')
        msg += "\r Please select  Exam !!!</br>";

    if (msg != '') {
        iziToast.warning({
            message: msg,
        });
        return false;
    }  
    try {   
        var ObjCHECK = {};  
        ObjCHECK.sessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
        ObjCHECK.CampusId = $('#ctl00_ContentPlaceHolder1_ddlCampus').val();  
        ObjCHECK.Modelity = 0; 
        ObjCHECK.Scheme = 0
        ObjCHECK.SEMESTERNO =  0
        ObjCHECK.PATTERNNO =$('#ctl00_ContentPlaceHolder1_ddlExamPattern').val(); 
        ObjCHECK.ExamNo =$('#ctl00_ContentPlaceHolder1_ddlExam').val(); 
        $.ajax({ 
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url:  liveurl+"Exam/GradeApprovalProgramHead.aspx/CheckWightagesBind",
            data: JSON.stringify(ObjCHECK),
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                if (response.d == '1') {  
                    $("#ModerationList").modal("show");
                }
                else
                { 
                    Swal.fire({
                        html: response.d  + '!!!',
                        icon: 'error'
                    });
                    $("[id*=preloader]").hide();
                    return false;
                } 
            }
        });
    }
    catch (ex) {
 
    } 
}
$("#btnCancel").click(function () {
    cleardata();
});
function cleardata() {
    $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val(0).change();
    $("#ctl00_ContentPlaceHolder1_ddlCampus").val(0).change(); 
    $('#ctl00_ContentPlaceHolder1_ddlExamPattern').empty();
    $('#ctl00_ContentPlaceHolder1_ddlExamPattern').append("<option value='0'>Please Select</option>");
    $('#ctl00_ContentPlaceHolder1_ddlExam').empty();
    $('#ctl00_ContentPlaceHolder1_ddlExam').append("<option value='0'>Please Select</option>"); 
    $("#tblmarkdata").hide(); 
}
$(document).ready(function() {
    $("#btnmodcancel").click(function() { 
        $("#txtremark").val('');
        vcourseno="";
        vExamNo="";
        vteacherid="";
        vSchemeno="";
        vsemno=""; 
        $("#ModerationList").modal("hide"); 
    }); 
    $("#btnmodupdate").click(function() {
    
        try {   
            var msg = ''; var str = ""; var count = 0; 
            if ($("#txtremarks").val() == '')
                msg += "\r Please Enter Remark !!!</br>"; 

            if (msg != '') {
                iziToast.warning({
                    message: msg,
                });
                return false;
            }
            var Obj = {}; 
            Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
            Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlCampus').val(); 
            Obj.CourseNo =vcourseno;
            Obj.Modelity = 0; 
            Obj.CurriculumId =0;  
            Obj.EXAMNO =  vExamNo;
            Obj.scheme =  0;
            Obj.TeacherId =  vteacherid;
            Obj.strSemesterNo =  0;
            Obj.rmk =$('#txtremarks').val();
            $.ajax({
                url:  liveurl +"Exam/GradeApprovalProgramHead.aspx/SubmitModeration",
                type: "POST",
                data: JSON.stringify(Obj),
                dataType: "json",
                beforeSend: function () { $("[id*=preloader]").show(); },
                contentType: "application/json;charset=utf-8",
                success: function (response) {   
                    if (response.d=="1")
                    { 
                        Swal.fire(
                                  'Submitted!',
                                  'Students Grade Encoding has been opened for moderation.',
                                  'success'
                        )
                        $("[id*=preloader]").hide();
                        $("#ModerationList").modal("hide"); 
                        Approvalmarkdata();
                    }
                }, 
                error: function () { 
                    $("[id*=preloader]").hide();
                }
            });  

        }
        catch (ex) {
        }
    });
});
function SubmitDean(ClickValue) {
    var msg = ''; var str = ""; var count = 0;

    if ($("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val() == '0')
        msg += "\r Please select  Academic Session !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlCampus").val() == '0')
        msg += "\r Please Select Campus  !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlCurriculum").val() == '0')
        msg += "\r Please select Curriculum !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlExamPattern").val() == '0')
        msg += "\r Please select  Exam Pattern !!!</br>"; 

    if ($("#ctl00_ContentPlaceHolder1_ddlExam").val() == '0')
        msg += "\r Please select  Exam !!!</br>";

    if (msg != '') {
        iziToast.warning({
            message: msg,
        });
        return false;
    } 
    var td = $("td", $(ClickValue).closest("tr")); 
    var ObjCHECK = {};  
    ObjCHECK.sessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
    ObjCHECK.CampusId = $('#ctl00_ContentPlaceHolder1_ddlCampus').val();  
    ObjCHECK.Modelity =  0;    
    ObjCHECK.Scheme = 0; 
    ObjCHECK.SEMESTERNO =  0;
    ObjCHECK.PATTERNNO =   $('#ctl00_ContentPlaceHolder1_ddlExamPattern').val(); 
    ObjCHECK.ExamNo =  $('#ctl00_ContentPlaceHolder1_ddlExam').val(); 
    $.ajax({ 
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url:  liveurl+"Exam/GradeApprovalProgramHead.aspx/CheckWightagesBind",
        data: JSON.stringify(ObjCHECK),
        dataType: "json",
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response.d == '1') {  
                var delconf; 
                Swal.fire({
                    title: 'Are you sure to Submit Student Grade Encoding to Dean?', 
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Yes',
                    delconf:'Yes'
                }).then((result) => { 
                    if (result.isConfirmed) { 
                     var Obj = {};  

            Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
            Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlCampus').val();  
            Obj.CourseNo = $("[id*=hdncourseNo]", td).val();
            Obj.Modelity =  0;   
            Obj.CurriculumId =0; 
            Obj.EXAMNO =$("[id*=hdnExamNo]", td).val(); 
            Obj.scheme =0;
            Obj.strSemesterNo =0;
            Obj.TeacherId =  $("[id*=hdnteacherid]", td).val();
            $.ajax({
                url:  liveurl +"Exam/GradeApprovalProgramHead.aspx/SubmitToDean",
                type: "POST",
                data: JSON.stringify(Obj),
                dataType: "json",
                beforeSend: function () { $("[id*=preloader]").show(); },
                contentType: "application/json;charset=utf-8",
                success: function (response) {   
                    if (response.d=="1")
                    { 
                        Swal.fire(
                          'Submitted!',
                          'Student Grade Encoding has been submitted to Dean.',
                          'success'
                            )
                        Approvalmarkdata();
                        $("[id*=preloader]").hide();
                    }
                },
                error: function () { 
                    $("[id*=preloader]").hide();
                }
            });  
        }
    });     
}  
else
{ 
    Swal.fire({
        html: response.d  + '!!!',
        icon: 'error'
    });
$("[id*=preloader]").hide();
return false;
} 
} 
});  
};
function HODExcel(ClickValue) {  
    var td = $("td", $(ClickValue).closest("tr"));   
    //$("[id*=preloader]").show();
    var filename=$("[id*=hdncoursename]", td).val();
    var rowCount = 1;
    var msg = ''; var str = ""; var count = 0;

    if ($("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val() == '0')
        msg += "\r Please select  Academic Session !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlCampus").val() == '0')
        msg += "\r Please Select Campus  !!!</br>"; 

    if ($("#ctl00_ContentPlaceHolder1_ddlExamPattern").val() == '0')
        msg += "\r Please select  Exam Pattern !!!</br>";
     

    if ($("#ctl00_ContentPlaceHolder1_ddlExam").val() == '0')
        msg += "\r Please select  Exam !!!</br>";

    if (msg != '') {
        iziToast.warning({
            message: msg,
        });
        return false;
    }
    var ObjSubmission = {};
    ObjSubmission.strSessionId = $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val();
    ObjSubmission.strCampusId = $("#ctl00_ContentPlaceHolder1_ddlCampus").val();;
    ObjSubmission.strCourseNo = $("[id*=hdncourseNo]", td).val();
    ObjSubmission.strModelity = 0;
    ObjSubmission.strEXAMCOMNO = 0;
    ObjSubmission.strEXAMNO = $("#ctl00_ContentPlaceHolder1_ddlExam").val();
    ObjSubmission.strSchemno =  0;
    ObjSubmission.strSemesterNo =  0;
    ObjSubmission.strTecharID =  $("[id*=hdnteacherid]", td).val(); 
    $.ajax({ 
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url:  liveurl + "Exam/GradeApprovalProgramHead.aspx/StudentList",
        data: JSON.stringify(ObjSubmission),
        dataType: "json",
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json;charset=utf-8",
        success: function (response) {  
            if (response.d == '') {
                Swal.fire({
                    html: 'Records not found !!!',
                    icon: 'question'
                });
                return false;
            }
            else { 
                var jsonData = JSON.parse(response.d);
                var headerMapping = {
                    'REGNO': 'Student ID',
                    'STUDENTNAME': 'Student Name',
                    'BRANCH_CODE': 'Subject Name',
                    'CAMPUSNAME': 'Campus Name',
                    'SEMESTERNAME': 'Semester'  
                };
                var headersToRemove = ['EXAMNO', 'IDNO', 'SRNo']; // Add the headers you want to remove here
                var limitedData = jsonData.map(function (row) {
                    var newRow = {};
                    for (var originalHeader in row) { // Iterate over properties of each row
                        if (row.hasOwnProperty(originalHeader) && headersToRemove.indexOf(originalHeader) === -1) {
                            var newHeader = headerMapping[originalHeader] || originalHeader; // Use dynamic header mapping or original if not mapped
                            newRow[newHeader] = row[originalHeader]; // Copy the value to the new property
                        }
                    }
                    return newRow;
                }); 
                var worksheet = XLSX.utils.json_to_sheet(limitedData);
                var workbook = XLSX.utils.book_new();
                XLSX.utils.book_append_sheet(workbook, worksheet, 'Sheet1');
                var excelData = XLSX.write(workbook, { bookType: 'xlsx', type: 'binary' });
                var blob;
                if (typeof Blob !== 'undefined') {
                    blob = new Blob([s2ab(excelData)], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
                } else {
                    var BlobBuilder = window.WebKitBlobBuilder || window.MozBlobBuilder;
                    var bb = new BlobBuilder();
                    bb.append(s2ab(excelData));
                    blob = bb.getBlob('application/vnd.openxmlformats-officedocument.spreadsheetml.sheet');
                } 
                var excelFileName = ''+filename+'.xlsx';

                if (window.navigator && window.navigator.msSaveOrOpenBlob) {
                    // For IE browser
                    window.navigator.msSaveOrOpenBlob(blob, excelFileName);
                } else {
                    // For modern browsers
                    var downloadLink = document.createElement('a');
                    downloadLink.href = window.URL.createObjectURL(blob);
                    downloadLink.download = excelFileName;
                    document.body.appendChild(downloadLink);
                    downloadLink.click();
                    document.body.removeChild(downloadLink);
                }  
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
function s2ab(s) {
    var buf = new ArrayBuffer(s.length);
    var view = new Uint8Array(buf);
    for (var i = 0; i < s.length; i++) {
        view[i] = s.charCodeAt(i) & 0xFF;
    }
    return buf;
}