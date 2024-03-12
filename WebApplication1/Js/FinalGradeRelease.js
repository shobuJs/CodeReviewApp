//===============================================//
// MODULE NAME   : RFC ERP 
// PAGE NAME     : Final Grade Release
// CREATION DATE : 21-08-2023
// CREATED BY    : Gaurav Kalbande
// Modified BY   : 
// Modified Date : 
//===============================================//
var liveurl = "../../../../";
var localurl = "../../../";

$(document).ready(function () {
    //---------------- css for badge ---------------------//
    $(".text-orange").css("color", "#ff9422");
    $(".text-green").css("color", "#44d793");
    $(".bg-green").css({ "background-color": "#44d793", "color": "#ffffff" });
    $(".bg-orange").css({ "background-color": "#ff9422", "color": "#ffffff" });

   
});
$('#ctl00_ContentPlaceHolder1_ddlAcademicSession').on('change', function (e) {
    $("#tblmarkdata").empty();
    $('#ctl00_ContentPlaceHolder1_ddlCampus').val(0).change();
});

$('#ctl00_ContentPlaceHolder1_ddlCampus').on('change', function (e) {
    var msg = ''; var str = ""; var count = 0; 

    if ($('#ctl00_ContentPlaceHolder1_ddlCampus').val() != "0") {

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
            $('#ctl00_ContentPlaceHolder1_ddlExamPattern').empty();
            $('#ctl00_ContentPlaceHolder1_ddlExamPattern').append("<option value='0'>Please Select</option>"); 
            var optionSelected = $("option:selected", this);
            var valueSelected = this.value;
            var Obj = {};
            Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
            Obj.CampusId = valueSelected;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: localurl + "Exam/FinalGradeRelease.aspx/GetCurriculum",
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
                        $("[id*=preloader]").hide();
                        $('#ctl00_ContentPlaceHolder1_ddlCurriculum').empty();
                        $('#ctl00_ContentPlaceHolder1_ddlCurriculum').append("<option value='0'>Please Select</option>");
                   
                        return false;
                    } else {
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
            //$.ajax({
            //    type: "POST",
            //    contentType: "application/json; charset=utf-8",
            //    url: localurl + "Exam/FinalGradeRelease.aspx/GetModelity",
            //    data: JSON.stringify(Obj),
            //    dataType: "json",
            //    beforeSend: function () { $("[id*=preloader]").show(); },
            //    complete: function () { $("[id*=preloader]").hide(); },
            //    contentType: "application/json;charset=utf-8",
            //    success: function (result) {
            //        if (result.d == '') {
            //            $("[id*=preloader]").hide();
            //            return false;
            //            $('#ctl00_ContentPlaceHolder1_ddlCurriculum').empty();
            //            $('#ctl00_ContentPlaceHolder1_ddlCurriculum').append("<option value='0'>Please Select</option>");
            //            $('#ctl00_ContentPlaceHolder1_ddlModelity').empty();
            //            $('#ctl00_ContentPlaceHolder1_ddlModelity').append("<option value='0'>Please Select</option>");
            //            $('#ctl00_ContentPlaceHolder1_ddlExamPattern').empty();
            //            $('#ctl00_ContentPlaceHolder1_ddlExamPattern').append("<option value='0'>Please Select</option>"); 
            //        }
            //        else {
            //            $('#ctl00_ContentPlaceHolder1_ddlModelity').empty();
            //            $('#ctl00_ContentPlaceHolder1_ddlModelity').append("<option value='0'>Please Select</option>");
            //            $.each(result.d, function (key, value) {
            //                $("[id*=preloader]").show();
            //                $("#ctl00_ContentPlaceHolder1_ddlModelity").append($("<option></option>").val(value.PMO_LEARNINGMODALITYNO).html(value.PMO_LEARNING_MODALITYNAME));
            //            });

            //        }
            //    },
            //    error: function ajaxError(result) {
            //        Swal.fire({
            //            html: 'Error Occurred !!!',
            //            icon: 'error'
            //        });
            //        return false;;
            //    }
            //});
            $("#tblmarkdata").empty();
        }
        catch (ex) {

        }
    }
    else {
        $('#ctl00_ContentPlaceHolder1_ddlCurriculum').empty();
        $('#ctl00_ContentPlaceHolder1_ddlCurriculum').append("<option value='0'>Please Select</option>");
        $('#ctl00_ContentPlaceHolder1_ddlModelity').empty();
        $('#ctl00_ContentPlaceHolder1_ddlModelity').append("<option value='0'>Please Select</option>");
        $('#ctl00_ContentPlaceHolder1_ddlExamPattern').empty();
        $('#ctl00_ContentPlaceHolder1_ddlExamPattern').append("<option value='0'>Please Select</option>"); 
        $("#tblmarkdata").empty();
    }
});

$('#ctl00_ContentPlaceHolder1_ddlCurriculum').on('change', function (e) {

    if ($('#ctl00_ContentPlaceHolder1_ddlCurriculum').val() != "0") {
        try {
            var optionSelected = $("option:selected", this);
            var valueSelected = this.value;
            var Obj = {};
            Obj.ColllegId = valueSelected;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: localurl + "Exam/FinalGradeRelease.aspx/GetExamPattern",
                data: JSON.stringify(Obj),
                dataType: "json",
                beforeSend: function () { $("[id*=preloader]").show(); },
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (result.d == '') {
                        Swal.fire({
                            html: 'Exam Pattern not found !!!',
                            icon: 'question'
                        });
                        $("#ctl00_ContentPlaceHolder1_ddlModelity").val(0).change();
                        $('#ctl00_ContentPlaceHolder1_ddlExamPattern').empty();
                        $('#ctl00_ContentPlaceHolder1_ddlExamPattern').append("<option value='0'>Please Select</option>");
                        $("#tblmarkdata").empty();
                        $('#ctl00_ContentPlaceHolder1_ddlModelity').val(0).change();
                        $("[id*=preloader]").hide();
                        return false;
                    } else {
                        $("#ctl00_ContentPlaceHolder1_ddlModelity").val(0).change();
                        $('#ctl00_ContentPlaceHolder1_ddlExamPattern').empty();
                        $('#ctl00_ContentPlaceHolder1_ddlExamPattern').append("<option value='0'>Please Select</option>");
                        $.each(result.d, function (key, value) {
                            $("[id*=preloader]").show();
                            $("#ctl00_ContentPlaceHolder1_ddlExamPattern").append($("<option></option>").val(value.PATTERNNO).html(value.pattern_name));

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
                    return false;
                }
            });
        }
        catch (ex) {

        }
    }
    else {
        $("#ctl00_ContentPlaceHolder1_ddlModelity").val(0).change();
        $('#ctl00_ContentPlaceHolder1_ddlExamPattern').empty();
        $('#ctl00_ContentPlaceHolder1_ddlExamPattern').append("<option value='0'>Please Select</option>");
        $("#tblmarkdata").empty();
    }
});
 
//$('#ctl00_ContentPlaceHolder1_ddlModelity').on('change', function (e) {
//    $("#tblmarkdata").empty();
//});

function Approvalmarkdata()
{
     
    
    var msg = ''; var str = ""; var count = 0;

    if ($("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val() == '0')
        msg += "\r Please select  Academic Session !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlCampus").val() == '0')
        msg += "\r Please Select Campus  !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlCurriculum").val() == '0')
        msg += "\r Please select Curriculum !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlExamPattern").val() == '0')
        msg += "\r Please select  Exam Pattern !!!</br>";

    if (msg != '') {
        iziToast.warning({
            message: msg,
        });
        return false;
    } 
    var Obj = {};
    Obj.SessionId = $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val();
    Obj.CampusId = $("#ctl00_ContentPlaceHolder1_ddlCampus").val();
    Obj.CurriculumId = $("#ctl00_ContentPlaceHolder1_ddlCurriculum").val();
    Obj.ExamPatternId = $("#ctl00_ContentPlaceHolder1_ddlExamPattern").val();
  //  Obj.ModalityId = $("#ctl00_ContentPlaceHolder1_ddlModelity").val(); 

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: localurl + "Exam/FinalGradeRelease.aspx/GetData",
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
                $("[id*=preloader]").hide();
            }
            else {
                $("#tblmarkdata").show();
                str = '<thead><tr><th><span>Report </span></th> <th><span class="View">View</span></th><th><span class="Semester">Semester</span></th><th><span class="SubjectCode">Subject Code</span></th> <th><span class="SubjectName">Subject Name</span></th> <th><span class="Status">Status</span></th> <th><span class="Date">Date</span></th><th></th><th></th></tr></thead><tbody>';
                $.each(response.d, function (index, GetValue) {
                    str = str + '<tr>'
                    str = str + '<td> <i class="fa fa-file-excel-o text-success display-6 me-2" title="Excel" onclick="FinalExcel(this)"></i>  </td>'//<i class="bi bi-filetype-pdf text-success display-6" title="PDF"></i>
                    str = str + '<td><i  class="fa fa-plus-square-o text-primary display-6 bi-plus-square" id="StdList' + index + '"   onclick="StdList(this)" ></i><input type="hidden" id="hdnSemNo" value="' + GetValue.SEMESTERNO + '"/><input type="hidden" id="hdnExamNo" value="' + GetValue.EXAMNO + '"/><input type="hidden" id="hdnSchemeno" value="' + GetValue.SCHEMENO + '"/><input type="hidden" id="hdnccode" value="' + GetValue.CCODE + '"/> <input type="hidden" id="hdncourseNo" value="' + GetValue.COURSENO + '"/><input type="hidden" id="hdncoursename" value="' + GetValue.COURSENAME + '"/><input type="hidden" id="hdnindex" value="' + index + '"/></td>'
                    str = str + '<td>' + GetValue.SEMESTERNAME + '</td>'
                    str = str + '<td>' + GetValue.CCODE + '</td>'
                    str = str + '<td>' + GetValue.COURSENAME + '</td>'
                    //str = str + '<td>' + GetValue.Techername + '</td>'
                    if (GetValue.Substatus=='P')
                    {
                        str = str + '<td><span class="badge badge-warning">Pending</span></td>'
                    }
                    else if (GetValue.Substatus=='NP')
                    {
                        str = str + '<td><span class="badge badge-primary">Need Approval</span></td>'
                    
                    }
                    else if (GetValue.Substatus=='OM')
                    {str = str + '<td><span class="badge badge-warning">Open for Moderation</span></td>'
                    
                    }  else if (GetValue.Substatus=='SD')
                    {str = str + '<td><span class="badge badge-secondary">Submitted to Dean</span></td>'
                    
                    }
                    else if (GetValue.Substatus=='F')
                    {str = str + '<td><span class="badge badge-secondary">Finalized</span></td>'
                    }
                    else if (GetValue.Substatus=='CFR')
                    {
                        str = str + '<td><span class="badge badge-success">Released for Students</span></td>'
                    }
                    else if (GetValue.Substatus=='C')
                    {
                        str = str + '<td><span class="badge badge-primary">Grade Computed</span></td>'
                    }
                    
                    str = str + '<td>' + GetValue.DateStatus + '</td>'
                    
                    if (GetValue.Substatus=='CFR'){
                        str = str + '<td> <input type="button" class="btn btn-sm btn-outline-primary" tabindex="0" value="Compute Grade"   disabled/></td>'

                        str = str + '<td> <input type="button" class="btn btn-sm btn-outline-primary" tabindex="0" value="Release for Students"  disabled/></td>'
               
                    }
                    else
                    {  
                        str = str + '<td> <input type="button" class="btn btn-sm btn-outline-primary" tabindex="0" value="Compute Grade" id="btnModeration" onclick="EditCompute(this)"/> </td>'
                  
                        if (GetValue.Substatus=='C')
                        {
                            str = str + '<td> <input type="button" class="btn btn-sm btn-outline-primary" tabindex="0" value="Release for Students" onclick="SubmitRelease(this)" id="btnSubmitDean" /></td>'
                        }else
                        {
                            str = str + '<td> <input type="button" class="btn btn-sm btn-outline-primary" tabindex="0" value="Release for Students" disabled /></td>'
                        } 
                  
                    }
                    str = str + '</tr>' 
                    str = str + '<tr>'
                    str = str + '<td></td>'
                    str = str + '<td colspan="9">'
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
                commonDatatables(BindDynamicTable)
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

        if ($("#ctl00_ContentPlaceHolder1_ddlCurriculum").val() == '0')
            msg += "\r Please select Curriculum !!!</br>";

        if ($("#ctl00_ContentPlaceHolder1_ddlExamPattern").val() == '0')
            msg += "\r Please select  Exam Pattern !!!</br>";

       

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
            // ObjSubmission.strModelity = $('#ctl00_ContentPlaceHolder1_ddlModelity').val();
            ObjSubmission.strEXAMCOMNO = $('#ctl00_ContentPlaceHolder1_ddlExamPattern').val();
            ObjSubmission.strEXAMNO = 0;
            ObjSubmission.strSchemno = $("[id*=hdnSchemeno]", td).val();
            ObjSubmission.strSemesterNo = $("[id*=hdnSemNo]", td).val();
            ObjSubmission.strTecharID = 0;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: localurl + "Exam/FinalGradeRelease.aspx/StudentList",
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

    } else {
        ClickValue.classList.remove('bi-file-minus');
        ClickValue.classList.add('bi-plus-square');
        table.style.display = "none";
    } 
}

function BindGradSubmission(data, index) {
    var valno = 1;
    var table = $('#BindDynamicSubmissionTable' + index + '');
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
                    headerRow.append($('<th>').text('Course'));
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
            if (key == "SRNo" || key=="SRNO") {
                row.append($('<td>').text(valno));
            }
            else if (key == "IDNO") {
                row.append($('<td style="Display:none;">').text(value));
            }
            else {    
                if (!isNaN(value) && parseFloat(value) === value) {
                    row.append($('<td>').text(value.toFixed(2))); // Use toFixed to add .00

                } else {
                    row.append($('<td>').text(value));
                }

            }

        });
        valno++;
        table.append(row);
    });
}


$("#btnCancel").click(function () {
    cleardata();
});
function cleardata() {  
    $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val(0).change();
    $("#ctl00_ContentPlaceHolder1_ddlCampus").val(0).change();
    $('#ctl00_ContentPlaceHolder1_ddlCurriculum').empty();
    $('#ctl00_ContentPlaceHolder1_ddlCurriculum').append("<option value='0'>Please Select</option>");
    $('#ctl00_ContentPlaceHolder1_ddlExamPattern').empty();
    $('#ctl00_ContentPlaceHolder1_ddlExamPattern').append("<option value='0'>Please Select</option>");
    $("#tblmarkdata").empty();
    $("#tblmarkdata").hide();
}

function EditCompute(ClickValue) { 
 
    var td = $("td", $(ClickValue).closest("tr"));  
     
    var msg = ''; var str = ""; var count = 0;

    if ($("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val() == '0')
        msg += "\r Please select  Academic Session !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlCampus").val() == '0')
        msg += "\r Please Select Campus  !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlCurriculum").val() == '0')
        msg += "\r Please select Curriculum !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlExamPattern").val() == '0')
        msg += "\r Please select  Exam Pattern !!!</br>";

 
    if (msg != '') {
        iziToast.warning({
            message: msg,
        });
        return false;
    } 
    var ObjCHECK = {};  
    ObjCHECK.sessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
    ObjCHECK.CampusId = $('#ctl00_ContentPlaceHolder1_ddlCampus').val();  
    ObjCHECK.CourseNo = $("[id*=hdncourseNo]", td).val();
   // ObjCHECK.Modelity =  $('#ctl00_ContentPlaceHolder1_ddlModelity').val();    
    ObjCHECK.Scheme = $("[id*=hdnSchemeno]", td).val();
    ObjCHECK.SEMESTERNO =  $("[id*=hdnSemNo]", td).val();
    ObjCHECK.TeacherId =  0; 
    $.ajax({ 
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: localurl+"Exam/FinalGradeRelease.aspx/Checkmarksfill",
        data: JSON.stringify(ObjCHECK),
        dataType: "json",
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response.d == 1) { 
                var delconf; 
                Swal.fire({
                    title: 'Are you sure to compute Grade?', 
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
           // Obj.Modelity =  $('#ctl00_ContentPlaceHolder1_ddlModelity').val();   
            Obj.CurriculumId =$('#ctl00_ContentPlaceHolder1_ddlCurriculum').val(); 
            Obj.CCODE =$("[id*=hdnccode]", td).val(); 
            Obj.scheme = $("[id*=hdnSchemeno]", td).val();
            Obj.strSemesterNo =  $("[id*=hdnSemNo]", td).val();
            Obj.TeacherId =  0;
            $.ajax({
                url: localurl +"Exam/FinalGradeRelease.aspx/finalGradecompute",
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
                          'Grade Computed Sucessfully.',
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

function SubmitRelease(ClickValue) {
 
    var msg = ''; var str = ""; var count = 0;

    if ($("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val() == '0')
        msg += "\r Please select  Academic Session !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlCampus").val() == '0')
        msg += "\r Please Select Campus  !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlCurriculum").val() == '0')
        msg += "\r Please select Curriculum !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlExamPattern").val() == '0')
        msg += "\r Please select  Exam Pattern !!!</br>";

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
    ObjCHECK.CourseNo = $("[id*=hdncourseNo]", td).val();
   // ObjCHECK.Modelity =  $('#ctl00_ContentPlaceHolder1_ddlModelity').val();    
    ObjCHECK.Scheme = $("[id*=hdnSchemeno]", td).val();
    ObjCHECK.SEMESTERNO =  $("[id*=hdnSemNo]", td).val();
    ObjCHECK.TeacherId = 0; 
    $.ajax({ 
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: localurl+"Exam/FinalGradeRelease.aspx/Checkmarksfill",
        data: JSON.stringify(ObjCHECK),
        dataType: "json",
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response.d == 1) { 
    var delconf; 
    Swal.fire({
        title: 'Are you sure to Release Student Grade Encoding?', 
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
// Obj.Modelity =  $('#ctl00_ContentPlaceHolder1_ddlModelity').val();   
Obj.CurriculumId =$('#ctl00_ContentPlaceHolder1_ddlCurriculum').val(); 
Obj.CCODE =$("[id*=hdnccode]", td).val(); 
Obj.scheme = $("[id*=hdnSchemeno]", td).val();
Obj.strSemesterNo =  $("[id*=hdnSemNo]", td).val();
Obj.TeacherId =  0;
$.ajax({
    url: localurl +"Exam/FinalGradeRelease.aspx/ReleaseGrade",
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
              'Grade Encoding has been Released for students.',
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



function FinalExcel(ClickValue) {
    var td = $("td", $(ClickValue).closest("tr"));
    var filename = $("[id*=hdncoursename]", td).val();
    var StrCode = $("[id*=hdnccode]", td).val();   
    //$("[id*=preloader]").show();
    var rowCount = 1;

    var msg = ''; var str = ""; var count = 0;

    if ($("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val() == '0')
        msg += "\r Please select  Academic Session !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlCampus").val() == '0')
        msg += "\r Please Select Campus  !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlCurriculum").val() == '0')
        msg += "\r Please select Curriculum !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlExamPattern").val() == '0')
        msg += "\r Please select  Exam Pattern !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlModelity").val() == '0')
        msg += "\r Please select  learning Modality !!!</br>";
     

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
    ObjSubmission.strModelity = $('#ctl00_ContentPlaceHolder1_ddlModelity').val();
    ObjSubmission.strPatternNo = $('#ctl00_ContentPlaceHolder1_ddlExamPattern').val();
    ObjSubmission.strEXAMNO = 0;
    ObjSubmission.strCCODE=StrCode;
    ObjSubmission.strSchemno = $("[id*=hdnSchemeno]", td).val();
    ObjSubmission.strSemesterNo = $("[id*=hdnSemNo]", td).val();
    ObjSubmission.strTecharID =  0;
    // Check Student Marks IS lock or not 
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: localurl + "Exam/FinalGradeRelease.aspx/ExportStudentList",
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
                    'CAMPUSNAME': 'Campus Name',
                    'SEMESTERNAME': 'Semester',
                    'SCHEMENAME': ' Curriculum Name',
                    'COURSE_NAME': 'Subject Name',
                    'CCODE': 'Subject Code'
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
                var excelFileName = '' + filename + '.xlsx';

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
