//===============================================//
// MODULE NAME   : RFC ERP 
// PAGE NAME     : Rubrics
// CREATION DATE : 15-07-2023
// CREATED BY    : Gaurav Kalbande
// Modified BY   : Harshal Bobde
// Modified Date : 12-08-2023
//===============================================//
var liveurl = "../../../";
var localurl = "../../"; 
//0134319740
 
function fnc100(value, min, max) {
    var msg = ''; var str = ""; var count = 0;
    value = value.replace(/\s/g, '');  
    if (parseFloat(value) < 0 || isNaN(value))
        return 0;
    else if (parseFloat(value) > max) {
        msg += "\r Marks should not greater than "+max+" !!! <br/>";
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
function handleBlur(textbox) {
    var value = textbox.value.trim();  
    if (value === '') {
        textbox.value = ''; 
    }
}
function loadData(tabName) {
    switch (tabName) {
        case 'GradeEncoding':  
            if ($('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val()!='0' && $('#ctl00_ContentPlaceHolder1_ddlCampus').val()!='0' && $('#ctl00_ContentPlaceHolder1_ddlSubject').val()!='0')
            {     
                $("#BindDynamicGradeEncodingTable").empty();  
                var Obj = {};
                Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
                Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlCampus').val(); 
                Obj.Scheme = 0;
                Obj.CourseNo = $('#ctl00_ContentPlaceHolder1_ddlSubject').val();
                Obj.Modelity =0; 
                Obj.SectionNo = $('#ctl00_ContentPlaceHolder1_ddlSection').val();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                   url: liveurl + "Exam/MarkEntry.aspx/GetWeightages",
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
                            $("#BindDynamicGradeEncodingTable").empty();
                            $("#divwieghtage").empty();
                            $("#divstudelist").hide();
                            $("[id*=preloader]").hide();
                        }
                        else {
                            str = '<thead><tr><th><span>Report </span></th><th><span class="SctionName">Section Name</span></th> <th><span class="AssessmentComponents">Assessment Components</span></th><th><span class="Marks">Marks</span></th><th><span class="EncodedCount">Encoded Count</span></th> <th><span class="Status">Status</span></th>  <th><span class="SDate">Start Date</span></th> <th><span class="EDate">End Date</span></th></tr></thead><tbody>';
                            $.each(response.d, function (index, GetValue) {
                                str = str + '<tr>' 
                                str = str + '<td> <i class="fa fa-file-excel-o text-success display-6 me-2" title="Excel" onclick="ExcelDownload(this)"></i></td>'    //<i class="bi bi-filetype-pdf text-success display-6" title="PDF"></i></td>'
                                str = str + '<td>'+GetValue.SECTIONAME+'</td>' 
                                str = str + '<td><a id="btnclick" class="asscomp"   href="#" onclick="Edit(this)">' + GetValue.EXAMNAME + '</a> <input type="hidden" id="hdnasscomname" value="' + GetValue.EXAMNAME + '"/> <input type="hidden" id="hdnasscomid" value="' + GetValue.EXAMCOMNO + '"/><input type="hidden" id="hdnexamno" value="' + GetValue.EXAMNO + '"/><input type="hidden" id="hdninwightages" value="' + GetValue.WEIGHTAGE + '"/><input type="hidden" id="hdninc" value="' + GetValue.INC + '"/><input type="hidden" id="hdnabs" value="' + GetValue.ABS + '"/><input type="hidden" id="hdnSectionno" value="' + GetValue.SECTIONNO + '"/><input type="hidden" id="hdnSubid" value="' + GetValue.SUBID + '"/> <input type="hidden" id="hdnHNA" value="' + GetValue.HNA + '"/><input type="hidden" id="hdnDRP" value="' + GetValue.DRP + '"/><input type="hidden" id="hdnWPF" value="' + GetValue.WPF + '"/><input type="hidden" id="hdnp" value="' + GetValue.P + '"/><input type="hidden" id="hdnNC" value="' + GetValue.NC + '"/><input type="hidden" id="hdnexamtype" value="' + GetValue.ExamType + '"/></td>'
                                str = str + '<td>' + GetValue.WEIGHTAGE + '</td>' 
                                if (GetValue.MARKSTATUS=="Pending")
                                {
                                    str = str + '<td> <span class="border border-warning">' + GetValue.STUD_ASSESSMENT_RESULT_COUNT +'/'+ GetValue.STUD_RESULT_COUNT  + '</span></td>'
                                }
                                else
                                {
                                    if (GetValue.STUD_RESULT_COUNT > GetValue.STUD_ASSESSMENT_RESULT_COUNT )
                                    {
                                        str = str + '<td> <span class="border border-warning">' + GetValue.STUD_ASSESSMENT_RESULT_COUNT +'/'+ GetValue.STUD_RESULT_COUNT  + '</span></td>'
                                    }
                                    else
                                    {
                                        str = str + '<td> <span class="border border-success">'  + GetValue.STUD_ASSESSMENT_RESULT_COUNT +'/'+ GetValue.STUD_RESULT_COUNT  + '</span></td>'
                                    }
                                }
                                if (GetValue.LOCKSTATUS!="")
                                { 
                                    if (GetValue.LOCKSTATUS=="LC")
                                    { 
                                        str = str + '<td><span class="badge badge-success">Submitted to Program Head</span></td>'
                                    }
                                    else if (GetValue.LOCKSTATUS=="OM")
                                    {
                                        str = str + '<td><span class="badge badge-warning">Open for Moderation</span></td>'
                                    }
                                    else if (GetValue.LOCKSTATUS=="SD")
                                    {
                                        str = str + '<td><span class="badge badge-secondary">Submitted to Dean</span></td>'
                                    }
                                    else if (GetValue.LOCKSTATUS=='F')
                                    {str = str + '<td><span class="badge badge-secondary">Finalized</span></td>'
                                    }
                                    else if (GetValue.LOCKSTATUS=="RS")
                                    {
                                        str = str + '<td><span class="badge badge-success">Released for Students</span></td>'
                                    }
                                        //10-10-2023
                                    else if (GetValue.LOCKSTATUS=='CFR')
                                    {
                                        str = str + '<td><span class="badge badge-success">Released Grade for Students</span></td>'
                                    }
                                } 
                                else
                                {  
                                    if (GetValue.MARKSTATUS=="Pending")
                                    {
                                        str = str + '<td><span class="badge badge-warning">' + GetValue.MARKSTATUS + '</span></td>'
                                    }
                                    else
                                    { 
                                        str = str + '<td><span class="badge badge-success">' +GetValue.MARKSTATUS + '</span></td>'
                                    } 
                                }
                                  
                                str = str + '<td>' + GetValue.startdate + '</td>'
                                str = str + '<td>' + GetValue.EndDate + '</td>'
                                str = str + '</tr>'
                            });
                            str = str + '</tbody>';
                            RemoveTableDynamically('#BindDynamicGradeEncodingTable');
                            $("#BindDynamicGradeEncodingTable").append(str); 
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
            break;
        case 'UploadGradeEncoding':
            binduplodgrade();
            break;
        case 'GradeSubmissions':
            Bindmodelity();
            break;
        default:
            break;
    }
}

$(document).ready(function () {  

    $("#divsmpmarkEntry").hide(); 
    $("#divstudelist").hide(); 
    //--------------------------------//
    $(".assessment-components").hide();   
    $("#btnBack").click(function () {      
        $(".assessment-components").hide();
        $(".grade-encoding").show();
        backbuttonclear(); 
        $("#BindDynamicGradeEncodingTable").empty();  
        var Obj = {};
        Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
        Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlCampus').val(); 
        Obj.Scheme = 0;
        Obj.CourseNo = $('#ctl00_ContentPlaceHolder1_ddlSubject').val();
        Obj.Modelity =0;
        Obj.SectionNo = $('#ctl00_ContentPlaceHolder1_ddlSection').val();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
           url: liveurl + "Exam/MarkEntry.aspx/GetWeightages",
            data: JSON.stringify(Obj),
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); }, 
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                if (response.d == '') {
                    Swal.fire({
                        html: 'Record not found !!!',
                        icon: 'question'
                    });
                    $("#BindDynamicGradeEncodingTable").empty();
                    $("#divwieghtage").empty();
                    $("#divstudelist").hide();
                    $("[id*=preloader]").hide();
                }
                else {
                    str = '<thead><tr><th><span>Report </span></th><th><span class="SctionName">Section Name</span></th> <th><span class="AssessmentComponents">Assessment Components</span></th><th><span class="Marks">Marks</span></th><th><span class="EncodedCount">Encoded Count</span></th> <th><span class="Status">Status</span></th>  <th><span class="SDate">Start Date</span></th> <th><span class="EDate">End Date</span></th></tr></thead><tbody>';
                    $.each(response.d, function (index, GetValue) {
                        str = str + '<tr>' 
                        str = str + '<td> <i class="fa fa-file-excel-o text-success display-6 me-2" title="Excel" onclick="ExcelDownload(this)"></i></td>'    //<i class="bi bi-filetype-pdf text-success display-6" title="PDF"></i></td>'
                        str = str + '<td>'+GetValue.SECTIONAME+'</td>' 
                        str = str + '<td><a id="btnclick" class="asscomp"   href="#" onclick="Edit(this)">' + GetValue.EXAMNAME + '</a> <input type="hidden" id="hdnasscomname" value="' + GetValue.EXAMNAME + '"/> <input type="hidden" id="hdnasscomid" value="' + GetValue.EXAMCOMNO + '"/><input type="hidden" id="hdnexamno" value="' + GetValue.EXAMNO + '"/><input type="hidden" id="hdninwightages" value="' + GetValue.WEIGHTAGE + '"/><input type="hidden" id="hdninc" value="' + GetValue.INC + '"/><input type="hidden" id="hdnabs" value="' + GetValue.ABS + '"/><input type="hidden" id="hdnSectionno" value="' + GetValue.SECTIONNO + '"/><input type="hidden" id="hdnSubid" value="' + GetValue.SUBID + '"/> <input type="hidden" id="hdnHNA" value="' + GetValue.HNA + '"/><input type="hidden" id="hdnDRP" value="' + GetValue.DRP + '"/><input type="hidden" id="hdnWPF" value="' + GetValue.WPF + '"/> <input type="hidden" id="hdnp" value="' + GetValue.P + '"/><input type="hidden" id="hdnNC" value="' + GetValue.NC + '"/><input type="hidden" id="hdnexamtype" value="' + GetValue.ExamType + '"/></td>'
                        str = str + '<td>' + GetValue.WEIGHTAGE + '</td>' 
                        if (GetValue.MARKSTATUS=="Pending")
                        {
                            str = str + '<td> <span class="border border-warning">' + GetValue.STUD_ASSESSMENT_RESULT_COUNT +'/'+ GetValue.STUD_RESULT_COUNT  + '</span></td>'
                        }
                        else
                        {
                            if (GetValue.STUD_RESULT_COUNT > GetValue.STUD_ASSESSMENT_RESULT_COUNT )
                            {
                                str = str + '<td> <span class="border border-warning">' + GetValue.STUD_ASSESSMENT_RESULT_COUNT +'/'+ GetValue.STUD_RESULT_COUNT  + '</span></td>'
                            }
                            else
                            {
                                str = str + '<td> <span class="border border-success">'  + GetValue.STUD_ASSESSMENT_RESULT_COUNT +'/'+ GetValue.STUD_RESULT_COUNT  + '</span></td>'
                            }
                        }
                        if (GetValue.LOCKSTATUS!="")
                        { 
                            if (GetValue.LOCKSTATUS=="LC")
                            { 
                                str = str + '<td><span class="badge badge-success">Submitted to Program Head</span></td>'
                            }
                            else if (GetValue.LOCKSTATUS=="OM")
                            {
                                str = str + '<td><span class="badge badge-warning">Open for Moderation</span></td>'
                            }
                            else if (GetValue.LOCKSTATUS=="SD")
                            {
                                str = str + '<td><span class="badge badge-secondary">Submitted to Dean</span></td>'
                            }
                            else if (GetValue.LOCKSTATUS=='F')
                            {str = str + '<td><span class="badge badge-secondary">Finalized</span></td>'
                            }
                            else if (GetValue.LOCKSTATUS=="RS")
                            {
                                str = str + '<td><span class="badge badge-success">Released for Students</span></td>'
                            }
                                //10-10-2023
                            else if (GetValue.LOCKSTATUS=='CFR')
                            {
                                str = str + '<td><span class="badge badge-success">Released Grade for Students</span></td>'
                            }
                        } 
                        else
                        {  
                            if (GetValue.MARKSTATUS=="Pending")
                            {
                                str = str + '<td><span class="badge badge-warning">' + GetValue.MARKSTATUS + '</span></td>'
                            }
                            else
                            { 
                                str = str + '<td><span class="badge badge-success">' +GetValue.MARKSTATUS + '</span></td>'
                            } 
                        }
                                  
                        str = str + '<td>' + GetValue.startdate + '</td>'
                        str = str + '<td>' + GetValue.EndDate + '</td>'
                        str = str + '</tr>'
                    });
                    str = str + '</tbody>';
                    RemoveTableDynamically('#BindDynamicGradeEncodingTable');
                    $("#BindDynamicGradeEncodingTable").append(str); 
                    $("[id*=preloader]").hide();
                } 
            },
            error: function ajaxError(response) {
                Swal.fire({
                    html: 'Error Occurred !!!',
                    icon: 'error'
                });
                $("[id*=preloader]").hide();
            }
        });   
    }); 
     

});
$("#GradeEncoding").click(function () {
    $(".assessment-components").hide();
    $(".grade-encoding").show();
}); 

//--------------------------------//
   
//--------------------------------//
$(".submission-components").hide();
 
$("#btnBackMenu").click(function () {
    $(".submission-components").hide();
    $(".grade-submissions").show(); 
    Bindmodelity();
});  

$("#btnclear").click(function () {   
    clear();
});

function clear() 
{ 
    $("[id*=preloader]").show();
    $('#ctl00_ContentPlaceHolder1_ddlMarkEntryStatus').empty();  
    var optionPleaseSelect = $("<option>").val(0).text("Please Select");
    var optionPending = $("<option>").val(1).text("Pending");
    var optionCompleted = $("<option>").val(2).text("Completed"); 
    $("#ctl00_ContentPlaceHolder1_ddlMarkEntryStatus").append(optionPleaseSelect);
    $("#ctl00_ContentPlaceHolder1_ddlMarkEntryStatus").append(optionPending);
    $("#ctl00_ContentPlaceHolder1_ddlMarkEntryStatus").append(optionCompleted);


    $('#ctl00_ContentPlaceHolder1_ddlSortBy').empty();  
    var optionSelect = $("<option>").val(0).text("Please Select");
    var optionStudent = $("<option>").val(1).text("Student Name");
    var optionStudentId= $("<option>").val(4).text("Student Id"); 
    var optionSection = $("<option>").val(5).text("Section"); 
    $("#ctl00_ContentPlaceHolder1_ddlSortBy").append(optionSelect);
    $("#ctl00_ContentPlaceHolder1_ddlSortBy").append(optionStudent);
    $("#ctl00_ContentPlaceHolder1_ddlSortBy").append(optionStudentId);
    $("#ctl00_ContentPlaceHolder1_ddlSortBy").append(optionSection);

    $("#tbnLMSMark").prop("disabled", false);
    $("#tbnLMSMark").val("Transfer LMS Marks"); 
    $("[id*=preloader]").hide();
}

function backbuttonclear() 
{
    $("[id*=preloader]").show();
    $('#ctl00_ContentPlaceHolder1_ddlMarkEntryStatus').empty();  
    var optionPleaseSelect = $("<option>").val(0).text("Please Select");
    var optionPending = $("<option>").val(1).text("Pending");
    var optionCompleted = $("<option>").val(2).text("Completed"); 
    $("#ctl00_ContentPlaceHolder1_ddlMarkEntryStatus").append(optionPleaseSelect);
    $("#ctl00_ContentPlaceHolder1_ddlMarkEntryStatus").append(optionPending);
    $("#ctl00_ContentPlaceHolder1_ddlMarkEntryStatus").append(optionCompleted);


    $('#ctl00_ContentPlaceHolder1_ddlSortBy').empty();  
    var optionSelect = $("<option>").val(0).text("Please Select");
    var optionStudent = $("<option>").val(1).text("Student Name");
    var optionStudentId= $("<option>").val(4).text("Student Id"); 
    var optionSection = $("<option>").val(5).text("Section"); 
    $("#ctl00_ContentPlaceHolder1_ddlSortBy").append(optionSelect);
    $("#ctl00_ContentPlaceHolder1_ddlSortBy").append(optionStudent);
    $("#ctl00_ContentPlaceHolder1_ddlSortBy").append(optionStudentId);
    $("#ctl00_ContentPlaceHolder1_ddlSortBy").append(optionSection); 
    $("[id*=preloader]").hide();
}
// --------------------------------------------- First tab ---------------------------------------------------------------------//
$('#ctl00_ContentPlaceHolder1_ddlAcademicSession').on('change', function (e) {
    //  $('#ctl00_ContentPlaceHolder1_ddlCampus').trigger.change();
    $('#ctl00_ContentPlaceHolder1_ddlSubject').empty();
    $('#ctl00_ContentPlaceHolder1_ddlSubject').append("<option value='0'>Please Select</option>");  
    $('#ctl00_ContentPlaceHolder1_ddlCampus').val(1);
    BindSubjectData(); 
    $("#BindDynamicGradeEncodingTable").empty();
    $("#divwieghtage").empty();
    $("[id*=preloader]").hide();
});


function BindSubjectData()
{
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
  
    $('#ctl00_ContentPlaceHolder1_ddlSubject').empty();
    $('#ctl00_ContentPlaceHolder1_ddlSubject').append("<option value='0'>Please Select</option>");   
    $("#BindDynamicGradeEncodingTable").empty();
    $("#divwieghtage").empty(); 
    if ($('#ctl00_ContentPlaceHolder1_ddlCampus').val() != '0' ) {
        try { 
            var Obj = {};
            Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
            Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlCampus').val();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: localurl +"Exam/MarkEntry.aspx/GetCourse",
                data: JSON.stringify(Obj),
                dataType: "json",
                beforeSend: function () { $("[id*=preloader]").show(); },
                complete: function () { $("[id*=preloader]").hide(); },
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (result.d == '') { 
                        Swal.fire({
                            html: 'Subjects not found !!!',
                            icon: 'question'
                        });
                        $("#BindDynamicGradeEncodingTable").empty();
                        $("#divwieghtage").empty(); 
                        return false; 
                    }
                    else 
                    {
                        $('#ctl00_ContentPlaceHolder1_ddlSubject').empty();
                        $('#ctl00_ContentPlaceHolder1_ddlSubject').append("<option value='0'>Please Select</option>");
                        $.each(result.d, function (key, value) {
                            $("[id*=preloader]").show();
                            $("#ctl00_ContentPlaceHolder1_ddlSubject").append($("<option></option>").val(value.COURSENO).html(value.COURSE_NAME));
                        });
                        $("#BindDynamicGradeEncodingTable").empty();
                        $("#divwieghtage").empty(); 
                        return false; 
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
    else {
        $('#ctl00_ContentPlaceHolder1_ddlSubject').empty();
        $('#ctl00_ContentPlaceHolder1_ddlSubject').append("<option value='0'>Please Select</option>");
    } 
}

$('#ctl00_ContentPlaceHolder1_ddlSubject').on('change', function (e) {  
    BindSection();
    BindExamEntry(); 
}); 

$('#ctl00_ContentPlaceHolder1_ddlSection').on('change', function (e) {   
    BindExamEntry(); 
});


function BindSection()
{
    var Obj = {};
    Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
    Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlCampus').val();
    Obj.Scheme = 0;
    Obj.CourseNo = $('#ctl00_ContentPlaceHolder1_ddlSubject').val();
    Obj.Type = "SECTIONDROP";
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
       url: liveurl +"Exam/MarkEntry.aspx/GetSection",
        data: JSON.stringify(Obj),
        dataType: "json",
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json;charset=utf-8",
        success: function (result) {
            $('#ctl00_ContentPlaceHolder1_ddlSection').empty();
            $('#ctl00_ContentPlaceHolder1_ddlSection').append("<option value='0'>ALL</option>");
            $.each(result.d, function (key, value) {
                $("[id*=preloader]").show();
                $('#ctl00_ContentPlaceHolder1_ddlSection').empty();
                $('#ctl00_ContentPlaceHolder1_ddlSection').append("<option value='0'>ALL</option>");
                $.each(result.d, function (key, value) { 
                    $("#ctl00_ContentPlaceHolder1_ddlSection").append($("<option></option>").val(value.SectionNO).html(value.SectionName));
                });
            });
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

function BindExamEntry()
{
    if ($('#ctl00_ContentPlaceHolder1_ddlSubject').val() != '0') {
        $("#BindDynamicGradeEncodingTable").empty();
        $("#divwieghtage").empty();
        
        var Obj = {};
        Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
        Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlCampus').val();
        Obj.Scheme = 0;
        Obj.CourseNo = $('#ctl00_ContentPlaceHolder1_ddlSubject').val();
        Obj.SectionNo = $('#ctl00_ContentPlaceHolder1_ddlSection').val();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
           url: liveurl +"Exam/MarkEntry.aspx/CheckWightagesBind",
            data: JSON.stringify(Obj),
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (result.d == '1') { 
                    Swal.fire({
                        html: 'Weightages  Not Defined for Subject, Please Contact to Admin !!!',
                        icon: 'question'
                    });
                    $("#BindDynamicGradeEncodingTable").empty();
                    $("#divwieghtage").empty();
                    $('#ctl00_ContentPlaceHolder1_ddlSubject').val(0).change(); 
                    return false; 
                }
                else if (result.d == '2') { 
                    Swal.fire({
                        html: 'Assessment Component Marks Not Defined for Subject !!!',
                        icon: 'question'
                    });
                    $("#BindDynamicGradeEncodingTable").empty();
                    $("#divwieghtage").empty();
                    $('#ctl00_ContentPlaceHolder1_ddlSubject').val(0).change(); 
                    return false; 
                }
                else {

                    if ($('#ctl00_ContentPlaceHolder1_ddlSubject').val() != '0') { 
                        var ObjShow = {};
                        ObjShow.sessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
                        ObjShow.CampusId = $('#ctl00_ContentPlaceHolder1_ddlCampus').val(); 
                        ObjShow.Scheme = 0;
                        ObjShow.CourseNo = $('#ctl00_ContentPlaceHolder1_ddlSubject').val();
                        ObjShow.Modelity = 0; 
                        ObjShow.SectionNo = $('#ctl00_ContentPlaceHolder1_ddlSection').val();
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                           url: liveurl + "Exam/MarkEntry.aspx/ShowWightages",
                            data: JSON.stringify(ObjShow),
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
                                    $("#BindDynamicGradeEncodingTable").empty();
                                    $("#divwieghtage").empty();
                                    $("#divstudelist").hide();
                                    $("[id*=preloader]").hide();
                                }
                                else {
                                    $("#BindDynamicGradeEncodingTable").empty();
                                    $("#divwieghtage").empty();
                                    $("#divstudelist").show();
                                    $.each(response.d, function (index, GetValue) {
                                        $("[id*=preloader]").show();
                                        $('#divwieghtage').show();
                                        $('#divwieghtage').append($('<div class="col-xl-3 col-lg-3 col-sm-3 col-6"><div class="form-group">    <div class="label-dynamic">   <label><span>' + GetValue.EXAMNAME + ' (%)</span></label></div> <input type="text" value="' + GetValue.WEIGHTAGE + '"  class="form-control" disabled="disabled" placeholder="20" /> </div> </div>'));
                                    });
                                }

                                $.ajax({
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                   url: liveurl + "Exam/MarkEntry.aspx/GetWeightages",
                                    data: JSON.stringify(ObjShow),
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
                                            $("#BindDynamicGradeEncodingTable").empty();
                                            $("#divwieghtage").empty();
                                            $("#divstudelist").hide();
                                            $("[id*=preloader]").hide();
                                        }
                                        else {
                                            str = '<thead><tr><th><span>Report </span></th><th><span class="SctionName">Section Name</span></th> <th><span class="AssessmentComponents">Assessment Components</span></th><th><span class="Marks">Marks</span></th><th><span class="EncodedCount">Encoded Count</span></th> <th><span class="Status">Status</span></th>  <th><span class="SDate">Start Date</span></th> <th><span class="EDate">End Date</span></th></tr></thead><tbody>';
                                            $.each(response.d, function (index, GetValue) {
                                                str = str + '<tr>' 
                                                str = str + '<td> <i class="fa fa-file-excel-o text-success display-6 me-2" title="Excel" onclick="ExcelDownload(this)"></i></td>'    //<i class="bi bi-filetype-pdf text-success display-6" title="PDF"></i></td>'
                                                str = str + '<td>'+GetValue.SECTIONAME+'</td>' 
                                                str = str + '<td><a id="btnclick" class="asscomp"   href="#" onclick="Edit(this)">' + GetValue.EXAMNAME + '</a> <input type="hidden" id="hdnasscomname" value="' + GetValue.EXAMNAME + '"/> <input type="hidden" id="hdnasscomid" value="' + GetValue.EXAMCOMNO + '"/><input type="hidden" id="hdnexamno" value="' + GetValue.EXAMNO + '"/><input type="hidden" id="hdninwightages" value="' + GetValue.WEIGHTAGE + '"/><input type="hidden" id="hdninc" value="' + GetValue.INC + '"/><input type="hidden" id="hdnabs" value="' + GetValue.ABS + '"/><input type="hidden" id="hdnSectionno" value="' + GetValue.SECTIONNO + '"/><input type="hidden" id="hdnSubid" value="' + GetValue.SUBID + '"/> <input type="hidden" id="hdnHNA" value="' + GetValue.HNA + '"/><input type="hidden" id="hdnDRP" value="' + GetValue.DRP + '"/><input type="hidden" id="hdnWPF" value="' + GetValue.WPF + '"/> <input type="hidden" id="hdnp" value="' + GetValue.P + '"/><input type="hidden" id="hdnNC" value="' + GetValue.NC + '"/><input type="hidden" id="hdnexamtype" value="' + GetValue.ExamType + '"/></td>'
                                                str = str + '<td>' + GetValue.WEIGHTAGE + '</td>' 
                                                if (GetValue.MARKSTATUS=="Pending")
                                                {
                                                    str = str + '<td> <span class="border border-warning">' + GetValue.STUD_ASSESSMENT_RESULT_COUNT +'/'+ GetValue.STUD_RESULT_COUNT  + '</span></td>'
                                                }
                                                else
                                                {
                                                    if (GetValue.STUD_RESULT_COUNT > GetValue.STUD_ASSESSMENT_RESULT_COUNT )
                                                    {
                                                        str = str + '<td> <span class="border border-warning">' + GetValue.STUD_ASSESSMENT_RESULT_COUNT +'/'+ GetValue.STUD_RESULT_COUNT  + '</span></td>'
                                                    }
                                                    else
                                                    {
                                                        str = str + '<td> <span class="border border-success">'  + GetValue.STUD_ASSESSMENT_RESULT_COUNT +'/'+ GetValue.STUD_RESULT_COUNT  + '</span></td>'
                                                    }
                                                }
                                                if (GetValue.LOCKSTATUS!="")
                                                { 
                                                    if (GetValue.LOCKSTATUS=="LC")
                                                    { 
                                                        str = str + '<td><span class="badge badge-success">Submitted to Program Head</span></td>'
                                                    }
                                                    else if (GetValue.LOCKSTATUS=="OM")
                                                    {
                                                        str = str + '<td><span class="badge badge-warning">Open for Moderation</span></td>'
                                                    }
                                                    else if (GetValue.LOCKSTATUS=="SD")
                                                    {
                                                        str = str + '<td><span class="badge badge-secondary">Submitted to Dean</span></td>'
                                                    }
                                                    else if (GetValue.LOCKSTATUS=='F')
                                                    {str = str + '<td><span class="badge badge-secondary">Finalized</span></td>'
                                                    }
                                                    else if (GetValue.LOCKSTATUS=="RS")
                                                    {
                                                        str = str + '<td><span class="badge badge-success">Released for Students</span></td>'
                                                    } 
                                                    else if (GetValue.LOCKSTATUS=='CFR')
                                                    {
                                                        str = str + '<td><span class="badge badge-success">Released Grade for Students</span></td>'
                                                    }
                                                } 
                                                else
                                                {  
                                                    if (GetValue.MARKSTATUS=="Pending")
                                                    {
                                                        str = str + '<td><span class="badge badge-warning">' + GetValue.MARKSTATUS + '</span></td>'
                                                    }
                                                    else
                                                    { 
                                                        str = str + '<td><span class="badge badge-success">' +GetValue.MARKSTATUS + '</span></td>'
                                                    } 
                                                }
                                  
                                                str = str + '<td>' + GetValue.startdate + '</td>'
                                                str = str + '<td>' + GetValue.EndDate + '</td>'
                                                str = str + '</tr>'
                                            });
                                            str = str + '</tbody>';
                                            RemoveTableDynamically('#BindDynamicGradeEncodingTable');
                                            $("#BindDynamicGradeEncodingTable").append(str);  
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
                            },
                            error: function ajaxError(response) {
                                Swal.fire({
                                    html: 'Error Occurred !!!',
                                    icon: 'error'
                                });
                            }
                        });  
                    }
                    else {
                
                        $("#BindDynamicGradeEncodingTable").empty();
                        $("#divwieghtage").empty();
                        $("#divstudelist").hide();
                        $("[id*=preloader]").hide();
                    }
                
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
                
        $("#BindDynamicGradeEncodingTable").empty();
        $("#divwieghtage").empty();
        $("#divstudelist").hide();
        $("[id*=preloader]").hide();
    }
 
 
}
 
function Edit(ClickValue) {
    try { 
       
        var Obj = {};
        var msg = ''; var str = ""; var count = 0;

        if ($("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val() == '0')
            msg += "\r Please select Academic Session !!!</br>";


        if ($("#ctl00_ContentPlaceHolder1_ddlCampus").val() == '0')
            msg += "\r Please select Campus !!!</br>";

        if ($("#ctl00_ContentPlaceHolder1_ddlSubject").val() == '0')
            msg += "\r Please select  Subject !!!</br>";  

        if (msg != '') {
            iziToast.warning({
                message: msg,
            });
            return false;
        }
        var td = $("td", $(ClickValue).closest("tr")); 

        $("#hdnAssComponetid").val($("[id*=hdnasscomid]", td).val());
        $("#hdnasscomname").val($("[id*=hdnasscomname]", td).val());
        $("#hdnExamno").val($("[id*=hdnexamno]", td).val());
        $("#hdnExamwightages").val($("[id*=hdninwightages]", td).val());
        $("#hdnincval").val($("[id*=hdninc]", td).val());
        $("#hdnabsval").val($("[id*=hdnabs]", td).val());

        $("#hdnuHNA").val($("[id*=hdnHNA]", td).val());
        $("#hdnuDRP").val($("[id*=hdnDRP]", td).val());
        $("#hdnUwp").val($("[id*=hdnWPF]", td).val());

        $("#hdUnp").val($("[id*=hdnp]", td).val());
        $("#hdnUNC").val($("[id*=hdnNC]", td).val());

        $("#hdnSectionNoref").val($("[id*=hdnSectionno]", td).val());
        $("#hdnStrSUBID").val($("[id*=hdnSubid]", td).val()); 
        $("#hdnstrexamtype").val($("[id*=hdnexamtype]", td).val()); 
        $("#tbnLMSMark").prop("disabled", false);
        $("#tbnLMSMark").val("Transfer LMS Marks");
        var Obj = {};
        Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
        Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlCampus').val();   
        Obj.Scheme = 0;
        Obj.CourseNo = $('#ctl00_ContentPlaceHolder1_ddlSubject').val();
        Obj.Modelity = 0;  
        Obj.SectionNo =  $("#hdnSectionNoref").val();  
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
           url: liveurl + "Exam/MarkEntry.aspx/GetStudenttype",
            data: JSON.stringify(Obj),
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            //complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $('#ctl00_ContentPlaceHolder1_ddlStudentType').empty();
                $('#ctl00_ContentPlaceHolder1_ddlStudentType').append("<option value='0'>Please Select</option>");
                $.each(result.d, function (key, value) {
                    $("[id*=preloader]").show();
                    $("#ctl00_ContentPlaceHolder1_ddlStudentType").append($("<option></option>").val(value.StudentId).html(value.StudentType));
                });
            },
            error: function ajaxError(result) {
                Swal.fire({
                    html: 'ssss Error Occurred !!!',
                    icon: 'error'
                });
                return false;
            }
        }); 
        BindStudentData();
      
    }
    catch (ex) { 
        
    }
}

function BindStudentData()
{ 
    var Obj = {};
    Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
    Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlCampus').val();   
    Obj.Scheme = 0;
    Obj.CourseNo = $('#ctl00_ContentPlaceHolder1_ddlSubject').val();
    Obj.Modelity = 0; 
    Obj.EXAMCOMNO = $("#hdnAssComponetid").val();
    Obj.EXAMNO = $("#hdnExamno").val();   
    Obj.SECTIONNO =  $("#hdnSectionNoref").val();  
    // Check Student Marks IS lock or not
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
       url: liveurl +"Exam/MarkEntry.aspx/ExamActivityCheck",
        data: JSON.stringify(Obj),
        dataType: "json",
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json;charset=utf-8",
        success: function (result) { 
            if (result.d == '') {   
                Swal.fire({
                    html: 'Mark Encoding Activity Not Started !!!',
                    icon: 'question'
                });
            }
            else {   
                if (result.d=="1")
                { 
                    $.ajax({ 
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                       url: liveurl +"Exam/MarkEntry.aspx/GetStudentList",
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
                                return false;
                            }
                            else {  
                                $("#spncoursename").text($('#ctl00_ContentPlaceHolder1_ddlSubject option:selected').text());
                                $.ajax({
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                   url: liveurl +"Exam/MarkEntry.aspx/CheckMarkLock",
                                    data: JSON.stringify(Obj),
                                    dataType: "json",
                                    beforeSend: function () { $("[id*=preloader]").show(); },
                                    complete: function () { $("[id*=preloader]").hide(); },
                                    contentType: "application/json;charset=utf-8",
                                    success: function (result) { 
                                        if (result.d == '') {   
                                            $("#btnSave").show(); 
                                            $("#tbnLMSMark").show(); 
                                            $("#hdnlockmark").val(0);
                                            BindStudentList(response.d); 
                                            $("[id*=preloader]").hide();
                                        }
                                        else { 
                                            $("#hdnlockmark").val(1);
                                            $("#btnSave").hide(); 
                                            $("#tbnLMSMark").hide(); 
                                            BindStudentList(response.d);   
                                            $("[id*=preloader]").hide();
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
                        },
                        error: function ajaxError(response) {
                            Swal.fire({
                                html: ' Error Occurred !!!',
                                icon: 'error'
                            });
                        }
                    }); 
                }
                else
                {   
                    var message=result.d;
                    Swal.fire({
                        html: message +' !!!',
                        icon: 'question'
                    }); 
                    return false; 
                }
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




function BindMarkConversion()
{ 
    var Obj = {};
    Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
    Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlCampus').val();   
    Obj.Scheme = 0;
    Obj.CourseNo = $('#ctl00_ContentPlaceHolder1_ddlSubject').val();
    Obj.Modelity = 0; 
    Obj.EXAMCOMNO = $("#hdnAssComponetid").val();
    Obj.EXAMNO = $("#hdnExamno").val();   
    Obj.SECTIONNO =  $("#hdnSectionNoref").val();   
    $("#spncoursename").text($('#ctl00_ContentPlaceHolder1_ddlSubject option:selected').text());
    $.ajax({ 
        type: "POST",
        contentType: "application/json; charset=utf-8",
       url: liveurl +"Exam/MarkEntry.aspx/GetStudentList",
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
                return false;
            }
            else {  
                $("#spncoursename").text($('#ctl00_ContentPlaceHolder1_ddlSubject option:selected').text());
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                   url: liveurl + "Exam/MarkEntry.aspx/CheckMarkLock",
                    data: JSON.stringify(Obj),
                    dataType: "json",
                    beforeSend: function () { $("[id*=preloader]").show(); },
                    complete: function () { $("[id*=preloader]").hide(); },
                    contentType: "application/json;charset=utf-8",
                    success: function (result) { 
                        if (result.d == '') {   
                            $("#btnSave").show(); 
                            $("#tbnLMSMark").show(); 
                            $("#hdnlockmark").val(0);
                            BindStudentList(response.d); 
                            $("[id*=preloader]").hide();
                        }
                        else { 
                            $("#hdnlockmark").val(1);
                            $("#btnSave").hide(); 
                            $("#tbnLMSMark").hide(); 
                            BindStudentList(response.d);  
                            $("[id*=preloader]").hide();
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

function BindStudentList(response) {
 
    var strmar=$("#hdnExamwightages").val();
 
    $("[id*=preloader]").show();
    var rowCount = 1;
    $("#BindDynamicStdListTable").show();
    str = '<thead>  <th > Sr.No.</th> <th>Student ID</th><th>Student Name</th>  <th>Section</th>    <th>Semester</th>' 
 
    if ($("#hdnincval").val()=="1")
    {
        str =str +  '<th>INC</th>'    
    }
    if ($("#hdnabsval").val()=="1")
    {
        str = str + '<th>ABS</th>'  
    }
    if ($("#hdnuHNA").val()=="1")
    {
        str = str + '<th>HNA</th>'  
    }
    if ($("#hdnuDRP").val()=="1")
    {
        str = str + '<th>DRP</th>'  
    }
    if ($("#hdnUwp").val()=="1")
    {
        str = str + '<th>WP</th>'  
    }
    if ($("#hdUnp").val()=="1")
    {
        str = str + '<th>PASS</th>'  
    }
    if ($("#hdnUNC").val()=="1")
    {
        str = str + '<th>NC</th>'  
    }
    str =str +  '<th>LMS MARKS </th>'    
    if ($("#hdnStrSUBID").val()=="3" || $("#hdnstrexamtype").val()=="4")
    {
        str = str + '<th>Transmuted Marks </th>'
    }
    else
    {  
        str = str + '<th>Marks / '+parseInt(strmar, 10)+' </th>'
        str = str + '<th>Scaled 100%</th>' 
    }  
    str = str + '</thead > <tbody>';
 
    var lmsmarks="";
    $.each(response, function (index, GetValue) {
        str = str + '<tr class="trstudentmark">'
        str = str + '<td>' + rowCount + '</td >'
        str = str + '<td style="display:none;">' + GetValue.IDNO + '</td>'
        str = str + '<td style="display:none;">' + GetValue.CAMPUSNOS + '</td>'
        str = str + '<td style="display:none;">' + GetValue.SEMESTERNO + '</td>'
        str = str + '<td style="display:none;">' + GetValue.SCHEMNO + '</td>'
        str = str + '<td style="display:none;">' + GetValue.SECTIONNO + '</td>'
        str = str + '<td>' + GetValue.REGNO + '</td>'
        str = str + ' <td>' + GetValue.STUDNAME + '</td>' 
        str = str + '<td>' + GetValue.SECTIONNAME + '</td>'
        str = str + '<td>' + GetValue.SEMESTERNAME + '</td>' 
        if ($("#hdnincval").val()=="1")
        {  
            if ( $("#tbnLMSMark").val()=="LMS Marks Transfer")
            {  
                str = str + '<td><input  type="checkbox"  id="chkINC' + index + '" class="chkINC"   onchange="toggleINC(this)" /> </td>'
           
            }else{
                if (GetValue.MARKS == "905.00") {
                    str = str + '<td><input  type="checkbox"  id="chkINC' + index + '" class="chkINC"   onchange="toggleINC(this)" checked /> </td>'
                }
                else {
                    str = str + '<td><input  type="checkbox"  id="chkINC' + index + '" class="chkINC"   onchange="toggleINC(this)" /> </td>'
                } 
            }
        }
        if ($("#hdnabsval").val()=="1")
        {
            if ( $("#tbnLMSMark").val()=="LMS Marks Transfer")
            {  
                str = str + ' <td> <input type="checkbox" id="chkAbS' + index + '" class="chkAbsent"    onchange="toggleAbsent(this)" /></td>'  
            }
            else 
            {
                if (GetValue.MARKS == "902.00") { 
                    str = str + ' <td> <input type="checkbox" id="chkAbS' + index + '" class="chkAbsent"    onchange="toggleAbsent(this)" checked /></td>'
                }
                else {
                    str = str + ' <td> <input type="checkbox" id="chkAbS' + index + '" class="chkAbsent"    onchange="toggleAbsent(this)" /></td>' 
                }
            }
        }
        if ($("#hdnuHNA").val()=="1")
        {
            if ( $("#tbnLMSMark").val()=="LMS Marks Transfer")
            {  
                str = str + ' <td> <input type="checkbox" id="chkHNA' + index + '" class="chkHNA"    onchange="toggleHNA(this)" /></td>'  
            }
            else 
            {
                if (GetValue.MARKS == "906.00") { 
                    str = str + ' <td> <input type="checkbox" id="chkHNA' + index + '" class="chkHNA"    onchange="toggleHNA(this)" checked /></td>'
                }
                else {
                    str = str + ' <td> <input type="checkbox" id="chkHNA' + index + '" class="chkHNA"    onchange="toggleHNA(this)" /></td>' 
                }
            }
        }
        if ($("#hdnuDRP").val()=="1")
        {
            if ( $("#tbnLMSMark").val()=="LMS Marks Transfer")
            {  
                str = str + ' <td> <input type="checkbox" id="chkDRP' + index + '" class="chkDRP"    onchange="toggleDRP(this)" /></td>'  
            }
            else 
            {
                if (GetValue.MARKS == "907.00") { 
                    str = str + ' <td> <input type="checkbox" id="chkDRP' + index + '" class="chkDRP"    onchange="toggleDRP(this)" checked /></td>'
                }
                else {
                    str = str + ' <td> <input type="checkbox" id="chkDRP' + index + '" class="chkDRP"    onchange="toggleDRP(this)" /></td>' 
                }
            }
        }
        if ($("#hdnUwp").val()=="1")
        {
            if ( $("#tbnLMSMark").val()=="LMS Marks Transfer")
            {  
                str = str + ' <td> <input type="checkbox" id="chkWPF' + index + '" class="chkWPF"    onchange="toggleWPF(this)" /></td>'  
            }
            else 
            {
                if (GetValue.MARKS == "908.00") { 
                    str = str + ' <td> <input type="checkbox" id="chkWPF' + index + '" class="chkWPF"    onchange="toggleWPF(this)" checked /></td>'
                }
                else {
                    str = str + ' <td> <input type="checkbox" id="chkWPF' + index + '" class="chkWPF"    onchange="toggleWPF(this)" /></td>' 
                }
            }
        }

        if ($("#hdUnp").val()=="1")
        {
            if ( $("#tbnLMSMark").val()=="LMS Marks Transfer")
            {  
                str = str + ' <td> <input type="checkbox" id="chknp' + index + '" class="chknp"    onchange="togglenp(this)" /></td>'  
            }
            else 
            {
                if (GetValue.MARKS == "900.00") { 
                    str = str + ' <td> <input type="checkbox" id="chknp' + index + '" class="chknp"    onchange="togglenp(this)" checked /></td>'
                }
                else {
                    str = str + ' <td> <input type="checkbox" id="chknp' + index + '" class="chknp"    onchange="togglenp(this)" /></td>' 
                }
            }
        } 

        if ($("#hdnUNC").val()=="1")
        {
            if ( $("#tbnLMSMark").val()=="LMS Marks Transfer")
            {  
                str = str + ' <td> <input type="checkbox" id="chkNC' + index + '" class="chkNC"    onchange="toggleNC(this)" /></td>'  
            }
            else 
            {
                if (GetValue.MARKS == "901.00") { 
                    str = str + ' <td> <input type="checkbox" id="chkNC' + index + '" class="chkNC"    onchange="toggleNC(this)" checked /></td>'
                }
                else {
                    str = str + ' <td> <input type="checkbox" id="chkNC' + index + '" class="chkNC"    onchange="toggleNC(this)" /></td>' 
                }
            }
        }
        str = str + '<td>' + GetValue.LMSMARKS + '</td>' 
   
        if ( $("#tbnLMSMark").val()=="LMS Marks Transfer")
        {
            str += '<td  class="tdencoding"><input type="text" id="txtmark' + index + '" tabindex="' + rowCount + '" onblur="handleBlur(this)" maxlength="5" onkeyup="this.value = fnc100(this.value, 0, \'' + strmar + '\')" value="' + GetValue.LMSMARKS + '" class="form-control txtmark" /></td>';
        }
        else
        { 
            if (GetValue.MARKS == "999.00") { 
                str = str + ' <td> <input  type="text" id="txtmark' + index + '" tabindex=' + rowCount + '  maxlength="5" onkeyup="this.value = fnc100(this.value, 0, '+strmar+')" value="" class="form-control txtmark"/></td> '
            }
            else
            {    
                if (GetValue.MARKS == "902.00") {
                    str = str + ' <td> <input  type="text" id="txtmark' + index + '" tabindex=' + rowCount + '  maxlength="5" onkeyup="this.value = fnc100(this.value, 0, '+strmar+')" value="ABS" class="form-control txtmark" disabled/></td> '
                }
                else if (GetValue.MARKS == "905.00") {
                    str = str + ' <td> <input  type="text" id="txtmark' + index + '" tabindex=' + rowCount + '  maxlength="5" onkeyup="this.value = fnc100(this.value, 0, '+strmar+')" value="INC" class="form-control txtmark" disabled/></td> '
                }  
                else if (GetValue.MARKS == "906.00") {
                    str = str + ' <td> <input  type="text" id="txtmark' + index + '" tabindex=' + rowCount + '  maxlength="5" onkeyup="this.value = fnc100(this.value, 0, '+strmar+')" value="HNA" class="form-control txtmark" disabled/></td> '
                }  
                else if (GetValue.MARKS == "907.00") {
                    str = str + ' <td> <input  type="text" id="txtmark' + index + '" tabindex=' + rowCount + '  maxlength="5" onkeyup="this.value = fnc100(this.value, 0, '+strmar+')" value="DRP" class="form-control txtmark" disabled/></td> '
                }  
                else if (GetValue.MARKS == "908.00") {
                    str = str + ' <td> <input  type="text" id="txtmark' + index + '" tabindex=' + rowCount + '  maxlength="5" onkeyup="this.value = fnc100(this.value, 0, '+strmar+')" value="WP" class="form-control txtmark" disabled/></td> '
                } 
                
                else if (GetValue.MARKS == "900.00") {
                    str = str + ' <td> <input  type="text" id="txtmark' + index + '" tabindex=' + rowCount + '  maxlength="5" onkeyup="this.value = fnc100(this.value, 0, '+strmar+')" value="PASS" class="form-control txtmark" disabled/></td> '
                }  
                else if (GetValue.MARKS == "901.00") {
                    str = str + ' <td> <input  type="text" id="txtmark' + index + '" tabindex=' + rowCount + '  maxlength="5" onkeyup="this.value = fnc100(this.value, 0, '+strmar+')" value="NC" class="form-control txtmark" disabled/></td> '
                }  
                else 
                { 
                    str += '<td  class="tdencoding"><input type="text" id="txtmark' + index + '" tabindex="' + rowCount + '" onblur="handleBlur(this)" maxlength="5" onkeyup="this.value = fnc100(this.value, 0, \'' + strmar + '\')" value="' + GetValue.MARKS + '" class="form-control txtmark" /></td>';
                }
            }
           
              
        }
        if ($("#hdnStrSUBID").val()!="3")
        {
            if ($("#hdnstrexamtype").val()!="4")
            {
                if ( $("#tbnLMSMark").val()=="LMS Marks Transfer")
                {
                    str = str + '<td>0.00</td>'
                }
                else 
                {
                    str = str + ' <td>'+ GetValue.MARKS_CONVERSION + '</td> '
                }
            }
        }
        str = str + '</tr>'

        rowCount++;
    });
    str = str + '</tbody>';

  
    RemoveTableDynamically('#BindDynamicStdListTable');
    $("#BindDynamicStdListTable").append(str);
    var BindStudentTable = $('#BindDynamicStdListTable')
    commonDatatables(BindStudentTable) 

    $(".grade-encoding").hide();
    $(".assessment-components").show();  
    if ($("#hdnlockmark").val() == "1")
    { 
        var rows =  document.querySelectorAll('.trstudentmark'); 
        for (var i = 0; i < rows.length; i++) {
            var inputs = rows[i].getElementsByTagName('input');
            for (var j = 0; j < inputs.length; j++) {
                inputs[j].disabled = true;
            }
        } 
    } 
    $("[id*=preloader]").hide();  

} 

 

function toggleINC(checkbox) {
    
    var row = checkbox.parentElement.parentElement;
    var textbox = row.querySelector('input[type="text"]'); 
    if (checkbox.checked) {
        textbox.disabled = true; 
        textbox.value = "INC";
        if ($("#hdnuHNA").val()=="1")
        {
            var strchkHNA = row.querySelector('.chkHNA');
            strchkHNA.checked = false;
        }
        if ($("#hdnabsval").val()=="1")
        {
            var strchkABS = row.querySelector('.chkAbsent');
            strchkABS.checked = false;
        }
        if ($("#hdnuDRP").val()=="1")
        {
            var strchkDRP = row.querySelector('.chkDRP');
            strchkDRP.checked = false;
        }
        if ($("#hdnUwp").val()=="1")
        {
            var strchkWP = row.querySelector('.chkWPF');
            strchkWP.checked = false;
        }
        if ($("#hdUnp").val()=="1")
        {
            var strchkP = row.querySelector('.chknp');
            strchkP.checked = false;
        }
        if ($("#hdnUNC").val()=="1")
        {
            var strchkNC = row.querySelector('.chkNC');
            strchkNC.checked = false;
        }
    } else {
        textbox.disabled = false;
        textbox.value = "";
    }
} 
function toggleAbsent(checkbox) {
    var row = checkbox.parentElement.parentElement;
    var textbox = row.querySelector('input[type="text"]');  
    if (checkbox.checked) {
        textbox.disabled = true; 
        textbox.value = "ABS";
        if ($("#hdnincval").val()=="1")
        {
            var strchkINC = row.querySelector('.chkINC');
            strchkINC.checked = false;
        }
        if ($("#hdnuHNA").val()=="1")
        {
            var strchkHNA = row.querySelector('.chkHNA');
            strchkHNA.checked = false;
        }
        if ($("#hdnuDRP").val()=="1")
        {
            var strchkDRP = row.querySelector('.chkDRP');
            strchkDRP.checked = false;
        }
        if ($("#hdnUwp").val()=="1")
        {
            var strchkWP = row.querySelector('.chkWPF');
            strchkWP.checked = false;
        }
        if ($("#hdUnp").val()=="1")
        {
            var strchkP = row.querySelector('.chknp');
            strchkP.checked = false;
        }
        if ($("#hdnUNC").val()=="1")
        {
            var strchkNC = row.querySelector('.chkNC');
            strchkNC.checked = false;
        }
       
       
    } else {
        textbox.disabled = false;
        textbox.value = "";
    }
}
function toggleHNA(checkbox) {
    var row = checkbox.parentElement.parentElement;
    var textbox = row.querySelector('input[type="text"]');  
    if (checkbox.checked) {
        textbox.disabled = true; 
        textbox.value = "HNA"; 
 
        if ($("#hdnincval").val()=="1")
        {
            var strchkINC = row.querySelector('.chkINC');
            strchkINC.checked = false;
        }
        if ($("#hdnabsval").val()=="1")
        {
            var strchkABS = row.querySelector('.chkAbsent');
            strchkABS.checked = false;
        }
        if ($("#hdnuDRP").val()=="1")
        {
            var strchkDRP = row.querySelector('.chkDRP');
            strchkDRP.checked = false;
        }
        if ($("#hdnUwp").val()=="1")
        {
            var strchkWP = row.querySelector('.chkWPF');
            strchkWP.checked = false;
        }
        if ($("#hdUnp").val()=="1")
        {
            var strchkP = row.querySelector('.chknp');
            strchkP.checked = false;
        }
        if ($("#hdnUNC").val()=="1")
        {
            var strchkNC = row.querySelector('.chkNC');
            strchkNC.checked = false;
        }
       
    } else {
        textbox.disabled = false;
        textbox.value = "";
    }
}

function toggleDRP(checkbox) {
    var row = checkbox.parentElement.parentElement;
    var textbox = row.querySelector('input[type="text"]');  
    if (checkbox.checked) {
        textbox.disabled = true; 
        textbox.value = "DRP";
 
        if ($("#hdnincval").val()=="1")
        {
            var strchkINC = row.querySelector('.chkINC');
            strchkINC.checked = false;
        }
        if ($("#hdnuHNA").val()=="1")
        {
            var strchkHNA = row.querySelector('.chkHNA');
            strchkHNA.checked = false;
        }
        if ($("#hdnabsval").val()=="1")
        {
            var strchkABS = row.querySelector('.chkAbsent');
            strchkABS.checked = false;
        }
        if ($("#hdnUwp").val()=="1")
        {
            var strchkWP = row.querySelector('.chkWPF');
            strchkWP.checked = false;
        }
        if ($("#hdUnp").val()=="1")
        {
            var strchkP = row.querySelector('.chknp');
            strchkP.checked = false;
        }
        if ($("#hdnUNC").val()=="1")
        {
            var strchkNC = row.querySelector('.chkNC');
            strchkNC.checked = false;
        }
    } else {
        textbox.disabled = false;
        textbox.value = "";
    }
}

function toggleWPF(checkbox) {
    var row = checkbox.parentElement.parentElement;
    var textbox = row.querySelector('input[type="text"]');  
    if (checkbox.checked) {
        textbox.disabled = true; 
        textbox.value = "WP";
 
        if ($("#hdnincval").val()=="1")
        {
            var strchkINC = row.querySelector('.chkINC');
            strchkINC.checked = false;
        }
        if ($("#hdnuHNA").val()=="1")
        {
            var strchkHNA = row.querySelector('.chkHNA');
            strchkHNA.checked = false;
        }
        if ($("#hdnabsval").val()=="1")
        {
            var strchkABS = row.querySelector('.chkAbsent');
            strchkABS.checked = false;
        }
        if ($("#hdnuDRP").val()=="1")
        {
            var strchkDRP = row.querySelector('.chkDRP');
            strchkDRP.checked = false;
        } 
        if ($("#hdUnp").val()=="1")
        {
            var strchkP = row.querySelector('.chknp');
            strchkP.checked = false;
        }
        if ($("#hdnUNC").val()=="1")
        {
            var strchkNC = row.querySelector('.chkNC');
            strchkNC.checked = false;
        }
    } else {
        textbox.disabled = false;
        textbox.value = "";
    }
}


function togglenp(checkbox) {

    var row = checkbox.parentElement.parentElement;
    var textbox = row.querySelector('input[type="text"]');  
    if (checkbox.checked) {
        textbox.disabled = true; 
        textbox.value = "PASS";
 
        if ($("#hdnincval").val()=="1")
        {
            var strchkINC = row.querySelector('.chkINC');
            strchkINC.checked = false;
        }
        if ($("#hdnuHNA").val()=="1")
        {
            var strchkHNA = row.querySelector('.chkHNA');
            strchkHNA.checked = false;
        }
        if ($("#hdnabsval").val()=="1")
        {
            var strchkABS = row.querySelector('.chkAbsent');
            strchkABS.checked = false;
        }
        if ($("#hdnUwp").val()=="1")
        {
            var strchkWP = row.querySelector('.chkWPF');
            strchkWP.checked = false;
        }
        if ($("#hdnuDRP").val()=="1")
        {
            var strchkDRP = row.querySelector('.chkDRP');
            strchkDRP.checked = false; 
        }
        if ($("#hdnUNC").val()=="1")
        {
            var strchkNC = row.querySelector('.chkNC');
            strchkNC.checked = false;
        }

    } else {
        textbox.disabled = false;
        textbox.value = "";
    }
}


function toggleNC(checkbox) {
    var row = checkbox.parentElement.parentElement;
    var textbox = row.querySelector('input[type="text"]');  
    if (checkbox.checked) {
        textbox.disabled = true; 
        textbox.value = "NC";
 
        if ($("#hdnincval").val()=="1")
        {
            var strchkINC = row.querySelector('.chkINC');
            strchkINC.checked = false;
        }
        if ($("#hdnuHNA").val()=="1")
        {
            var strchkHNA = row.querySelector('.chkHNA');
            strchkHNA.checked = false;
        }
        if ($("#hdnabsval").val()=="1")
        {
            var strchkABS = row.querySelector('.chkAbsent');
            strchkABS.checked = false;
        }
        if ($("#hdnuDRP").val()=="1")
        {
            var strchkDRP = row.querySelector('.chkDRP');
            strchkDRP.checked = false; 
        }
        if ($("#hdnUwp").val()=="1")
        {
            var strchkWP = row.querySelector('.chkWPF');
            strchkWP.checked = false;
        }
        if ($("#hdUnp").val()=="1")
        {
            var strchkP = row.querySelector('.chknp');
            strchkP.checked = false; 
        }
    } else {
        textbox.disabled = false;
        textbox.value = "";
    }
} 

$('#ctl00_ContentPlaceHolder1_ddlStudentType').on('change', function (e) {
    try {
        $("#BindDynamicStdListTable").empty();
        var rowCount = 1;
        var Obj = {};
        Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
        Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlCampus').val(); 
        Obj.Scheme = 0;
        Obj.CourseNo =  $('#ctl00_ContentPlaceHolder1_ddlSubject').val(); 
        Obj.Modelity =0;
        Obj.EXAMCOMNO = $("#hdnAssComponetid").val();
        Obj.EXAMNO = $("#hdnExamno").val();
        Obj.STUDENT_TYPE = $('#ctl00_ContentPlaceHolder1_ddlStudentType').val(); 
        Obj.Section_Type=$("#hdnSectionNoref").val();
        var StrBRANCH = ""; 
        Obj.BRANCH_CODE = StrBRANCH
        Obj.orderby = $('#ctl00_ContentPlaceHolder1_ddlSortBy').val();
        Obj.status = $('#ctl00_ContentPlaceHolder1_ddlMarkEntryStatus').val();
        Obj.SEMESTERNO =0
       
        $.ajax({

            type: "POST",
            contentType: "application/json; charset=utf-8",
           url: liveurl +"Exam/MarkEntry.aspx/GetFilterdata",
            data: JSON.stringify(Obj),
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                if (response.d == '') {
                    Swal.fire({
                        html: 'Record Not Found !!!',
                        icon: 'question'
                    });
                    $("#BindDynamicStdListTable").hide();
                    $("[id*=preloader]").hide();
                    return false;
                }
                else {
                    BindStudentList(response.d); 
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
    catch (ex) {
    }
});
 
$('#ctl00_ContentPlaceHolder1_ddlSortBy').on('change', function (e) {
    try {
        
        $("#BindDynamicStdListTable").empty();
        var rowCount = 1;
        var Obj = {};
        Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
        Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlCampus').val(); 
        Obj.Scheme = 0;
        Obj.CourseNo = $('#ctl00_ContentPlaceHolder1_ddlSubject').val(); 
        Obj.Modelity = 0;
        Obj.EXAMCOMNO =   $("#hdnAssComponetid").val();
        Obj.EXAMNO = $("#hdnExamno").val();
        Obj.STUDENT_TYPE = $('#ctl00_ContentPlaceHolder1_ddlStudentType').val();
        Obj.Section_Type =$("#hdnSectionNoref").val();;
        Obj.orderby = $('#ctl00_ContentPlaceHolder1_ddlSortBy').val();
        Obj.status = $('#ctl00_ContentPlaceHolder1_ddlMarkEntryStatus').val();
        Obj.SEMESTERNO = 0; 
        $.ajax({

            type: "POST",
            contentType: "application/json; charset=utf-8",
           url: liveurl +"Exam/MarkEntry.aspx/GetOrderBy",
            data: JSON.stringify(Obj),
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                if (response.d == '') {
                    Swal.fire({
                        html: 'Record Not Found !!!',
                        icon: 'question'
                    });
                    $("#BindDynamicStdListTable").hide();
                    $("[id*=preloader]").hide();
                    return false;
                }
                else {
                    BindStudentList(response.d); 
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
    catch (ex) {
    }
});
$('#ctl00_ContentPlaceHolder1_ddlMarkEntryStatus').on('change', function (e) {
    try {  
        var rowCount = 1;
        var Obj = {};
        Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
        Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlCampus').val();
        Obj.Scheme = 0;
        Obj.CourseNo = $('#ctl00_ContentPlaceHolder1_ddlSubject').val(); 
        Obj.Modelity = 0;
        Obj.EXAMCOMNO =   $("#hdnAssComponetid").val();
        Obj.EXAMNO = $("#hdnExamno").val();
        Obj.STUDENT_TYPE = $('#ctl00_ContentPlaceHolder1_ddlStudentType').val(); 
        Obj.Section_Type = $("#hdnSectionNoref").val();;
        Obj.orderby = $('#ctl00_ContentPlaceHolder1_ddlSortBy').val(); 
        Obj.status = $('#ctl00_ContentPlaceHolder1_ddlMarkEntryStatus').val();
        Obj.SEMESTERNO =0;
        $.ajax({ 
            type: "POST",
            contentType: "application/json; charset=utf-8",
           url: liveurl +"Exam/MarkEntry.aspx/GetStatusWise",
            data: JSON.stringify(Obj),
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                if (response.d == '') {
                    Swal.fire({
                        html: 'Record Not Found !!!',
                        icon: 'question'
                    });
                    $("#BindDynamicStdListTable").hide();
                    $("[id*=preloader]").hide();
                    return false;
                }
                else {
                    BindStudentList(response.d);
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
    catch (ex) {
    }
});


$("#tbnLMSMark").click(function () { 
   
    var Obj = {};
    Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
    Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlCampus').val();  
    Obj.Scheme = 0;
    Obj.CourseNo = $('#ctl00_ContentPlaceHolder1_ddlSubject').val();
    Obj.Modelity = 0; 
    Obj.EXAMCOMNO = $("#hdnAssComponetid").val();
    Obj.EXAMNO = $("#hdnExamno").val();   
    Obj.SEMESTERNO = 0;  
    Obj.SECTIONNO = $("#hdnSectionNoref").val();
    $.ajax({ 
        type: "POST",
        contentType: "application/json; charset=utf-8",
       url: liveurl +"Exam/MarkEntry.aspx/CheckLMSAMRK",
        data: JSON.stringify(Obj),
        dataType: "json",
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json;charset=utf-8",
        success: function (response) { 
            if (response.d =='0') {
                Swal.fire({
                    html: 'LMS MARKS are Not Avaliable !!!',
                    icon: 'error'
                });
                return false;
            } 
            else
            {
                $("#tbnLMSMark").prop("disabled", true);
                $("#tbnLMSMark").val('LMS Marks Transfer');
                BindStudentData();
            }
        }
    });

 
   
});
function GetStudentMark() { 
    var data = [];
    var valno = 0;
    VarsessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
    VarCampusId = $('#ctl00_ContentPlaceHolder1_ddlCampus').val(); 
    VarCourseNo = $('#ctl00_ContentPlaceHolder1_ddlSubject').val();
    VarScheme = 0
    VarModelity = 0;
    varAssName = $('#hdnasscomname').val();
    varAsscomid = $('#hdnAssComponetid').val();
    varWeigtages = $('#hdnExamwightages').val();
    var strIDNOS = "";
    var VarIDNOS = "";
    var strMARKS = "";
    var VarMARKS = "";
    var strINC = "";
    var VarINC = "";
    var strABS = "";
    var VarABS = "";
    var strHNA = "";
    var VarHNA = "";
    var strDRP = "";
    var VarDRP = "";
    var strWPF = "";
    var VarWPF = ""; 
    var varasscomp = "";
    var strrepetstatus = "";
    var varrepetstatus = "";
    var strCAMPUSNOS = "";
    var strSemesterno = "";
    var strSchemno = "";

    $('tr.trstudentmark').each(function () {   
        var row = $(this).closest("tr")[0];
        if ($("#hdnincval").val()=="1") 
        {     var checkboxINC = $(this).find('.chkINC')[0];
            VarINC = checkboxINC.checked ? 1 : 0; 
            if (VarINC == 1) {
                VarINC = 1;
            }
            else {
                VarINC = 0;
            }
            if ($(this).find('.txtmark').val() == "INC") {
                VarMARKS= "905";
            }
        }

        if ($("#hdnabsval").val()=="1")
        {
            var checkboxABS = $(this).find('.chkAbsent')[0];
            VarABS = checkboxABS.checked ? 1 : 0;
            if (VarABS == 1) {
                VarABS = 1;
            }
            else {
                VarABS = 0;
            }
            if ($(this).find('.txtmark').val() == "ABS") {
                VarMARKS = "902";
            }
        } 
 
        if ($("#hdnuHNA").val()=="1")
        {   
            var checkboxHNA = $(this).find('.chkHNA')[0];
            VarHNA = checkboxHNA.checked ? 1 : 0;
            if (VarHNA == 1) {
                VarHNA = 1;
            }
            else {
                VarHNA = 0;
            }
            if ($(this).find('.txtmark').val() == "HNA") {
                VarMARKS = "906";
            }
        } 
        if ($("#hdnuDRP").val()=="1")
        {
            var checkboxDRP = $(this).find('.chkDRP')[0];
            VarDRP = checkboxDRP.checked ? 1 : 0;
            if (VarDRP == 1) {
                VarDRP = 1;
            }
            else {
                VarDRP = 0;
            }
            if ($(this).find('.txtmark').val() == "DRP") {
                VarMARKS = "907";
            }
        } 
        if ($("#hdnUwp").val()=="1")
        {
            var checkboxABS = $(this).find('.chkWPF')[0];
            VarWPF = checkboxABS.checked ? 1 : 0;
            if (VarWPF == 1) {
                VarWPF = 1;
            }
            else {
                VarWPF = 0;
            }
            if ($(this).find('.txtmark').val() == "WP") {
                VarMARKS = "908";
            }
        } 

        if ($("#hdUnp").val()=="1")
        {
            var checkboxABS = $(this).find('.chknp')[0];
            VarP= checkboxABS.checked ? 1 : 0;
            if (VarP == 1) {
                VarP = 1;
            }
            else {
                VarP = 0;
            }
            if ($(this).find('.txtmark').val() == "PASS") {
                VarMARKS = "900";
            }
        } 

        if ($("#hdnUNC").val()=="1")
        {
            var checkboxABS = $(this).find('.chkNC')[0];
            VarNC = checkboxABS.checked ? 1 : 0;
            if (VarNC == 1) {
                VarNC = 1;
            }
            else {
                VarNC = 0;
            }
            if ($(this).find('.txtmark').val() == "NC") {
                VarMARKS = "901";
            }
        } 
        if ($(this).find('.txtmark').val().trim() == "") {
            VarMARKS = "999";
        } 
        else { 
            if ($(this).find('.txtmark').val() == "INC")
            {   VarMARKS = "905" }
            else if ($(this).find('.txtmark').val() == "ABS")
            {  VarMARKS ="902"; 
            }
            else if ($(this).find('.txtmark').val() == "HNA")
            {  VarMARKS ="906"; 
            }
            else if ($(this).find('.txtmark').val() == "DRP")
            {  VarMARKS ="907"; 
            }
            else if ($(this).find('.txtmark').val() == "WP")
            {  VarMARKS ="908"; 
            }
            else if ($(this).find('.txtmark').val() == "PASS")
            {  VarMARKS ="900"; 
            }
            else if ($(this).find('.txtmark').val() == "NC")
            {  VarMARKS ="901"; 
            }
            else
            { VarMARKS = $(this).find('.txtmark').val(); 
            }
        }  
        strIDNOS += row.cells[1].innerHTML + "$";
        strMARKS += VarMARKS + "$";
        strINC += VarINC + "$";
        strABS += VarABS + "$";
        strCAMPUSNOS += row.cells[2].innerHTML + "$";
        strHNA += VarHNA + "$";
        strDRP += VarDRP + "$";
        strWPF += VarWPF + "$";
        strSemesterno+= row.cells[3].innerHTML + "$";
        VarScheme+= row.cells[4].innerHTML + "$"; 
    });

    strIDNOS = strIDNOS.slice(0, -1);
    strMARKS = strMARKS.slice(0, -1);
    strINC = strINC.slice(0, -1);
    strABS = strABS.slice(0, -1);
    strCAMPUSNOS = strCAMPUSNOS.slice(0, -1);
    strHNA = strINC.slice(0, -1);
    strDRP = strABS.slice(0, -1);
    strWPF = strABS.slice(0, -1);
    strSemesterno= strSemesterno.slice(0, -1);
    strSchemno= VarScheme.slice(0, -1);

    var alldata = {
        'MARKNO': 0,
        'SESSIONNO': VarsessionId,
        'COURSENO': VarCourseNo,
        'SCHEMNOS': strSchemno,
        'LEARNINGMODALITYNO': VarModelity,
        'ASSESSMENT_NO': varAsscomid,
        'CAMPUSNO': VarCampusId,
        'IDNOS': strIDNOS,
        'MARKS': strMARKS,
        'IC_GRADE_STUDS': strINC,
        'ABS_STUDS': strABS,
        'CAMPUSNOS': strCAMPUSNOS,
        'ASSESSMENT_COMPONENT_NAME': varAssName,
        'REPEAT_STATUS': "",
        'WIGHTAGES': varWeigtages, 
        'SECTIONNO': $("#hdnSectionNoref").val(),
        'SEMESTERNOS':strSemesterno,
        'HNA_GRADE': strHNA,
        'DRP_GRADE': strDRP,
        'WPF_GRADE': strWPF

    }

    data.push(alldata);

    return data;
}
$("#btnSave").click(function () { 
    try {  
        var msg = ''; var str = ""; var count = 0;

        if ($("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val() == '0')
            msg += "\r Please select Academic Session !!!</br>";


        if ($("#ctl00_ContentPlaceHolder1_ddlCampus").val() == '0')
            msg += "\r Please select Campus !!!</br>";

        if ($("#ctl00_ContentPlaceHolder1_ddlSubject").val() == '0')
            msg += "\r Please select  Subject !!!</br>"; 

   
        var checkMark = "0";
        $('tr.trstudentmark').each(function () {
            if ($(this).find('.txtmark').val() != "") {
                checkMark = 1;
                return false;
            }
        }); 
        if (checkMark == 0) {
            msg += "\r Please insert  marks for student !!!</br>";
        } 

        if (msg != '') {
            iziToast.warning({
                message: msg,
            });
            return false;
        }
        var data = JSON.stringify(GetStudentMark());
        $.ajax({
           url: liveurl +"Exam/MarkEntry.aspx/InUpMarkEntry", 
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ 'markentry': data }),
            beforeSend: function () { $("[id*=preloader]").show(); },
            success: function (response) {

                if (response.d == "1") {
                    iziToast.success({
                        message: 'Grade Encoding Completed Successfully !!!',
                    }); 
             
                    $("#tbnLMSMark").val("Transfer LMS Marks");
                    $("#tbnLMSMark").prop("disabled", false);
                    BindMarkConversion(); 
                    return false; 
                } 
                else  if (response.d == "3") {
                    Swal.fire({
                        html: 'Grade Encoding Already Lock !!!',
                        icon: 'error'
                    });  

                    return false; 
                } 

                else  if (response.d == "4") {
                    Swal.fire({
                        html: 'Please enter  Marks   between 0 and '+$("#hdnExamwightages").val()+' !!!',
                        icon: 'error'
                    });  
                    return false; 
                } 
               
             
            },
            error: function () {
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

$('#btnClassList').click(function () { 
    try {
        $("#tblstudentList").empty(); 

        var msg = ''; var str = ""; var count = 0;

        if ($("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val() == '0')
            msg += "\r Please select Academic Session !!!</br>"; 

        if ($("#ctl00_ContentPlaceHolder1_ddlCampus").val() == '0')
            msg += "\r Please select Campus !!!</br>";

        if ($("#ctl00_ContentPlaceHolder1_ddlSubject").val() == '0')
            msg += "\r Please select  Subject !!!</br>";
 
        if (msg != '') {
            iziToast.warning({
                message: msg,
            });
            return false;
        } 
        var Obj = {};
        Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
        Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlCampus').val(); 
        Obj.Scheme = 0;  
        Obj.CourseNo = $('#ctl00_ContentPlaceHolder1_ddlSubject').val();  
        Obj.Modelity =0;
        Obj.EXAMCOMNO =0;
        Obj.EXAMNO =0;
        Obj.SECTIONNO = $('#ctl00_ContentPlaceHolder1_ddlSection').val();
        $.ajax({ 
            type: "POST",
            contentType: "application/json; charset=utf-8",
           url: liveurl +"Exam/MarkEntry.aspx/GetclassList",
            data: JSON.stringify(Obj),
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                if (response.d == '') {
                    Swal.fire({
                        html: 'Record Not Found !!!',
                        icon: 'question'
                    });
                }
                else { 
                    var rowCount = 1;
                    $("#tblstudentList").show();
                    str = '<thead><tr><th> Sr.No.</th> <th>Student ID</th><th>Student Name</th><th>Course</th><th>Campus</th><th>Section</th><th>Semester</th></tr></thead><tbody>';
                    $.each(response.d, function (index, GetValue) { 
                        str = str + '<tr>'
                        str = str + '<td>' + rowCount + '</td>' 
                        str = str + '<td>' + GetValue.REGNO +'</td>'
                        str = str + '<td>' + GetValue.STUDNAME +'</td>'
                        str = str + '<td>' + GetValue.BRANCH_CODE +'</td>'
                        str = str + '<td>' + GetValue.CAMPUSNAME + '</td>'
                        str = str + '<td>' + GetValue.SECTIONNAME +'</td>'
                        str = str + '<td>' + GetValue.SEMESTERNAME +'</td>'
                        str = str + '</tr>' 
                        rowCount++;
                    });
                    str = str + '</tbody>';
                    RemoveTableDynamically('#tblstudentList');
                    $("#tblstudentList").append(str);
                    var BindStudentTable = $('#tblstudentList')
                    commonDatatable(BindStudentTable)   
                    
                    $("#ClassListModal").modal("show");
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
    catch (ex) {
    }
}); 

// check table student marks 0 or not
function checkTableForZero() {
    var isZeroAvailable = false; 
    $("#BindDynamicStdListTable input[type='text']").each(function () {
        if ($(this).val() === 0) {
            isZeroAvailable = true;
            return false; // Break out of the loop
        }
    });
    return isZeroAvailable;
}  
function ExcelDownload(ClickValue) {
    var msg = ''; var str = ""; var count = 0;

    if ($("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val() == '0')
        msg += "\r Please select Academic Session !!!</br>";


    if ($("#ctl00_ContentPlaceHolder1_ddlCampus").val() == '0')
        msg += "\r Please select Campus !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlSubject").val() == '0')
        msg += "\r Please select  Subject !!!</br>"; 
    if (msg != '') {
        iziToast.warning({
            message: msg,
        });
        return false;
    }
    try
    {
        var td = $("td", $(ClickValue).closest("tr")); 
        var Obj = {};
        Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
        Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlCampus').val();
        Obj.Scheme = 0;
        Obj.CourseNo = $('#ctl00_ContentPlaceHolder1_ddlSubject').val();
        Obj.Modelity = 0;
        $("#hdnAssComponetid").val($("[id*=hdnasscomid]", td).val());
        $("#hdnasscomname").val($("[id*=hdnasscomname]", td).val());
        $("#hdnExamno").val($("[id*=hdnexamno]", td).val());
        $("#hdnExamwightages").val($("[id*=hdninwightages]", td).val());
        $("#hdnSectionNoref").val($("[id*=hdnSectionno]", td).val());
        Obj.EXAMCOMNO = $("#hdnAssComponetid").val();
        Obj.EXAMNO = $("#hdnExamno").val(); 
        Obj.SECTIONNO =  $("#hdnSectionNoref").val();   
        // Check Student Marks IS lock or not 
        $.ajax({ 
            type: "POST",
            contentType: "application/json; charset=utf-8",
           url: liveurl +"Exam/MarkEntry.aspx/GetStudentList",
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
                }
                else {   
                    var jsonData = response.d;  
                    var headerMapping = {
                        REGNO: 'Student Id',
                        STUDNAME: 'Student Name',
                        BRANCH_CODE: 'Course',
                        CAMPUSNAME: 'Campus Name',
                        SECTIONNAME: 'Section Name',
                        SEMESTERNAME: 'Semester',
                        MARKS: 'Marks'
                    };
                    var limitedData = response.d.map(function (row) {
                        var newRow = {};
                        for (var originalHeader in headerMapping) {
                            if (row.hasOwnProperty(originalHeader)) {
                                var newHeader = headerMapping[originalHeader];
                                newRow[newHeader] = (originalHeader === 'MARKS') ? transformMarks(row[originalHeader]) : row[originalHeader];
                            }
                        }
                        return newRow;
                    });
                    function transformMarks(value) {
                        if (value === 905) {
                            return "INC";
                        } else if (value === 902) {
                            return "ABS";
                        } else if (value === 906) {
                            return "HNA";
                        } else if (value === 907) {
                            return "DRP";
                        } else if (value === 908) {
                            return "WP";
                        } else if (value === 999) {
                            return "";
                        } else if (value === 900) {
                            return "PASS";
                        } else if (value === 901) {
                            return "NC";
                        } else {
                            return value;
                        }
                    } 

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
                    var excelFileName = 'StudentList.xlsx';

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
    catch (ex) {
    }
}

// ---------------------------------------------END First tab ---------------------------------------------------------------------//

// ---------------------------------------------Start Second tab ---------------------------------------------------------------------//
$('#ctl00_ContentPlaceHolder1_ddlsctabSession').on('change', function (e) { 
    $('#ctl00_ContentPlaceHolder1_ddlsctabSubject').empty();
    $('#ctl00_ContentPlaceHolder1_ddlsctabSubject').append("<option value='0'>Please Select</option>"); 
    $('#ctl00_ContentPlaceHolder1_ddlsctabcampus').val(1);
    BinduploadSubjectData();
    $("#BindDynamicUploadGradeTable").empty(); 

});
$('#ctl00_ContentPlaceHolder1_ddlSectabSection').on('change', function (e) { 
    binduplodgrade();
});

function BindSecondtabSection()
{
    var Obj = {};
    Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlsctabSession').val();
    Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlsctabcampus').val();
    Obj.Scheme = 0;
    Obj.CourseNo = $('#ctl00_ContentPlaceHolder1_ddlsctabSubject').val();
    Obj.Type = "SECTIONDROP";
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
       url: liveurl +"Exam/MarkEntry.aspx/GetSection",
        data: JSON.stringify(Obj),
        dataType: "json",
        beforeSend: function () { $("[id*=preloader]").show(); },
        //complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json;charset=utf-8",
        success: function (result) {
            $('#ctl00_ContentPlaceHolder1_ddlSectabSection').empty();
            $('#ctl00_ContentPlaceHolder1_ddlSectabSection').append("<option value='0'>ALL</option>");
            $.each(result.d, function (key, value) {
                $("[id*=preloader]").show();
                $('#ctl00_ContentPlaceHolder1_ddlSectabSection').empty();
                $('#ctl00_ContentPlaceHolder1_ddlSectabSection').append("<option value='0'>ALL</option>");
                $.each(result.d, function (key, value) { 
                    $("#ctl00_ContentPlaceHolder1_ddlSectabSection").append($("<option></option>").val(value.SectionNO).html(value.SectionName));
                });
            });
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
$('#ctl00_ContentPlaceHolder1_ddlsctabSubject').on('change', function (e) {
    if ($('#ctl00_ContentPlaceHolder1_ddlsctabSubject').val() != '0') {  
        var Obj = {};
        Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlsctabSession').val();
        Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlsctabcampus').val();
        Obj.Scheme = 0;
        Obj.CourseNo =  $('#ctl00_ContentPlaceHolder1_ddlsctabSubject').val();
        Obj.SectionNo =  $('#ctl00_ContentPlaceHolder1_ddlSectabSection').val();;
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url:localurl +"Exam/MarkEntry.aspx/CheckWightagesBind",
            data: JSON.stringify(Obj),
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (result.d == '1') { 
                    Swal.fire({
                        html: 'Weightages  Not Defined for Subject, Please Contact to Admin !!!',
                        icon: 'question'
                    });
                    $("#BindDynamicUploadGradeTable").empty(); 
                    return false; 
                }
                else if (result.d == '2') { 
                    Swal.fire({
                        html: 'Assessment Component Marks Not Defined for Subject !!!',
                        icon: 'question'
                    });
                    $("#BindDynamicUploadGradeTable").empty(); 
                    return false; 
                } 
                else {
                    BindSecondtabSection();
                    binduplodgrade();
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
});


function BinduploadSubjectData()
{
    var msg = ''; var str = ""; var count = 0;

    if ($("#ctl00_ContentPlaceHolder1_ddlsctabSession").val() == '0')
        msg += "\r Please select Academic Session !!!</br>";  

    if (msg != '') {
        iziToast.warning({
            message: msg,
        });
        $('#ctl00_ContentPlaceHolder1_ddlsctabcampus').val(0);
        return false;
    } 

    $('#ctl00_ContentPlaceHolder1_ddlsctabSubject').empty();
    $('#ctl00_ContentPlaceHolder1_ddlsctabSubject').append("<option value='0'>Please Select</option>");  
    $("#BindDynamicUploadGradeTable").empty(); 
    if ($('#ctl00_ContentPlaceHolder1_ddlsctabcampus').val() != '0') {
        try { 
            var Obj = {};
            Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlsctabSession').val();
            Obj.CampusId =  $('#ctl00_ContentPlaceHolder1_ddlsctabcampus').val();

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
               url: liveurl +"Exam/MarkEntry.aspx/GetSecTabCourse",
                data: JSON.stringify(Obj),
                dataType: "json",
                beforeSend: function () { $("[id*=preloader]").show(); },
                complete: function () { $("[id*=preloader]").hide(); },
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (result.d == '') { 
                        Swal.fire({
                            html: 'Subjects not found !!!',
                            icon: 'question'
                        });
                        return false; 

                    }else{
                        $('#ctl00_ContentPlaceHolder1_ddlsctabSubject').empty();
                        $('#ctl00_ContentPlaceHolder1_ddlsctabSubject').append("<option value='0'>Please Select</option>");
                        $.each(result.d, function (key, value) {
                            $("[id*=preloader]").show();
                            $("#ctl00_ContentPlaceHolder1_ddlsctabSubject").append($("<option></option>").val(value.COURSENO).html(value.COURSE_NAME));
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
    else { 
        $('#ctl00_ContentPlaceHolder1_ddlsctabSubject').empty();
        $('#ctl00_ContentPlaceHolder1_ddlsctabSubject').append("<option value='0'>Please Select</option>"); 
    } 
}
function binduplodgrade()
{
    if ($('#ctl00_ContentPlaceHolder1_ddlsctabSubject').val() != '0') { 
        var optionSelected = $("option:selected", this);
        var valueSelected = this.value;
        var Obj = {};
        Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlsctabSession').val();
        Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlsctabcampus').val(); 
        Obj.Scheme = 0;
        Obj.CourseNo = $('#ctl00_ContentPlaceHolder1_ddlsctabSubject').val();
        Obj.Modelity = 0  
        Obj.SectionNo = $('#ctl00_ContentPlaceHolder1_ddlSectabSection').val(); 
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
           url: liveurl +"Exam/MarkEntry.aspx/GetSecTabWeightages",
            data: JSON.stringify(Obj),
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
                    $("#BindDynamicUploadGradeTable").empty(); 
                    $("#divsmpmarkEntry").hide(); 
                    $("#divmarkEntry").hide(); 
                    $("[id*=preloader]").hide();
                }
                else
                { 
                    var rowCount = 1;
                    $("#BindDynamicStdListTable").show();
                    $("#divmarkEntry").show(); 
                    $("#divsmpmarkEntry").show(); 
                    str = '<thead><tr><th><span>Sr.No.</span></th><th><span class="SctionName">Section Name</span></th> <th><span class="AssessmentComponents">Assessment Components</span></th><th><span class="MarksOutof">Marks Out of</span></th> <th><span class="Status">Status</span></th>  <th><span class="SDate">Start Date</span></th> <th><span class="EDate">End Date</span></th> <th><span class="Remark">Remark</span></th> <th><span class="BrowseExcel">Browse Excel</span></th> <th><span class="UploadExcel">Upload Excel</span></th><th><span class="DownloadTemplate">Download Template</span></th></tr></thead><tbody>';
                    $.each(response.d, function (index, GetValue) {
                        str = str + '<tr>'
                        str = str + '<td>' + rowCount + '</td>' 
                        str = str + '<td>' + GetValue.SECTIONAME + '</td>' 
                        str = str + '<td>' + GetValue.EXAMNAME + '</td>' 
                        str = str + '<td>' + GetValue.WEIGHTAGE + '</td>' 
                        if (GetValue.LOCKSTATUS!="")
                        { 
                            if (GetValue.LOCKSTATUS=="LC")
                            { 
                                str = str + '<td><span class="badge badge-success">Submitted to Program Head</span></td>'
                            }
                            else if (GetValue.LOCKSTATUS=="OM")
                            {
                                str = str + '<td><span class="badge badge-warning">Open for Moderation</span></td>'
                            }
                            else if (GetValue.LOCKSTATUS=="SD")
                            {
                                str = str + '<td><span class="badge badge-secondary">Submitted to Dean</span></td>'
                            }
                            else if (GetValue.LOCKSTATUS=='F')
                            {str = str + '<td><span class="badge badge-secondary">Finalized</span></td>'
                            }
                            else if (GetValue.LOCKSTATUS=="RS")
                            {
                                str = str + '<td><span class="badge badge-success">Released for Students</span></td>'
                            }
                        } 
                        else
                        {
                            if (GetValue.MARKSTATUS=="Pending")
                            {
                                str = str + '<td><span class="badge badge-warning">' + GetValue.MARKSTATUS + '</span></td>'
                            }
                            else
                            { str = str + '<td><span class="badge badge-success">' + GetValue.MARKSTATUS + '</span></td>'
                            } 
                        }
                        str = str + '<td>' + GetValue.startdate + '</td>'
                        str = str + '<td>' + GetValue.EndDate + '</td>'
                        str = str + '<td>-</td>'
                     
                        if (GetValue.LOCK=="0")
                        {
                            str = str + '<td><input type="file" id="fileInput'+index+'" name="fileInput'+index+'" class="file-input" /> <input type="hidden" id="hdnindex" value="' + index + '"/></td>'
                            str = str + '<td><input type="button" class="btn btn-sm btn-outline-success" tabindex="0" value="Upload" id="btnUpload"   onclick="uploadFile(this)" /></td>'
                        }
                        else
                        {
                            str = str + '<td><input type="file" id="fileInputhde'+index+'" name="fileInputhde'+index+'"  disabled /></td>'
                            str = str + '<td><input type="button" class="btn btn-sm btn-outline-success" tabindex="0" value="Upload" id="btnUpload1"  disabled  /></td>'
                       
                        }
                        str = str + '<td><a id="btnExportMarks" class="fa fa-download text-success display-6"  href="#" onclick="EditExportMarks(this)"></a><input type="hidden" id="hnduplsectioname" value="' + GetValue.SECTIONAME + '"/><input type="hidden" id="hdnuplasscomname" value="' + GetValue.EXAMNAME + '"/><input type="hidden" id="hdnuplwightages" value="' + GetValue.WEIGHTAGE + '"/> <input type="hidden" id="hdnuplasscomid" value="' + GetValue.EXAMCOMNO + '"/><input type="hidden" id="hdnuplexamno" value="' + GetValue.EXAMNO + '"/> <input type="hidden" id="hdupdinc" value="' + GetValue.INC + '"/><input type="hidden" id="hdupdnabs" value="' + GetValue.ABS + '"/><input type="hidden" id="hdupdSECNo" value="' + GetValue.SECTIONNO + '"/> <input type="hidden" id="hdnupdHNA" value="' + GetValue.HNA + '"/><input type="hidden" id="hdnupdDRP" value="' + GetValue.DRP + '"/><input type="hidden" id="hdnupdWPF" value="' + GetValue.WPF + '"/> <input type="hidden" id="hdnupdP" value="' + GetValue.P + '"/><input type="hidden" id="hdnupdNC" value="' + GetValue.NC + '"/></td>' 
                        str = str + '</tr>'
                        rowCount++;

                    });
                    str = str + '</tbody>';
                    RemoveTableDynamically('#BindDynamicUploadGradeTable');
                    $("#BindDynamicUploadGradeTable").append(str);
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
    else 
    { 
        $("#BindDynamicUploadGradeTable").empty(); 
        $("#divmarkEntry").hide();  
        $("[id*=preloader]").hide();
        return false; 
    }
}  

 
function EditExportMarks(ClickValue) { 
    try
    {  
        var msg = ''; var str = ""; var count = 0;

        if ($("#ctl00_ContentPlaceHolder1_ddlsctabSession").val() == '0')
            msg += "\r Please select Academic Session !!!</br>";


        if ($("#ctl00_ContentPlaceHolder1_ddlsctabcampus").val() == '0')
            msg += "\r Please select Campus !!!</br>";

        if ($("#ctl00_ContentPlaceHolder1_ddlsctabSubject").val() == '0')
            msg += "\r Please select  Subject !!!</br>"; 
 
        if (msg != '') {
            iziToast.warning({
                message: msg,
            });
            return false;
        } 

        var td = $("td", $(ClickValue).closest("tr")); 
        var Obj = {}; 
      
        Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlsctabSession').val();
        Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlsctabcampus').val();
        Obj.Scheme = 0;
        Obj.CourseNo = $('#ctl00_ContentPlaceHolder1_ddlsctabSubject').val();
        Obj.Modelity = 0; 
        Obj.EXAMCOMNO = $("[id*=hdnuplasscomid]", td).val();
        Obj.EXAMNO = $("[id*=hdnuplexamno]", td).val(); 
        Obj.SEMESTERNO =0; 
        Obj.SECTIONNO = $("[id*=hdupdSECNo]", td).val(); 
        $.ajax({ 
            type: "POST",
            contentType: "application/json; charset=utf-8",
           url: liveurl +"Exam/MarkEntry.aspx/GetSecTabStudentData",
            data: JSON.stringify(Obj),
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            complete: function () { $("[id*=preloader]").hide(); },
            success: function (response) {
                if (response.d == '') {
                    Swal.fire({
                        html: 'Record Not Found !!!',
                        icon: 'question'
                    });
                }
                else {  

                    var jsonData = response.d; 
                    var headerMapping = {
                        REGNO: 'Student Id',
                        STUDNAME: 'Student Name',
                        BRANCH_CODE: 'Subject',
                        CAMPUSNAME: 'Campus',
                        SECTIONNAME: 'Section',
                        SEMESTERNAME: 'Semester',
                        MARKS: 'Marks'
                    };
                    var limitedData = response.d.map(function (row) {
                        var newRow = {};
                        for (var originalHeader in headerMapping) {
                            if (row.hasOwnProperty(originalHeader)) {
                                var newHeader = headerMapping[originalHeader];
                                newRow[newHeader] = (originalHeader === 'MARKS') ? transformMarks(row[originalHeader]) : row[originalHeader];
                            }
                        }
                        return newRow;
                    });
                    function transformMarks(value) {
                        if (value === 902) {
                            return "ABS";
                        } else if (value === 905) {
                            return "INC";
                        } else if (value === 906) {
                            return "HNA";
                        } else if (value === 907) {
                            return "DRP";
                        } else if (value === 908) {
                            return "WP";

                        } else if (value === 900) {
                            return "PASS";

                        } else if (value === 901) {
                            return "NC";

                        } else if (value === 999) {
                            return "";
                        } else {
                            return value;
                        }
                    } 

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
                    var excelFileName = 'GradeEncodingStudentList.xlsx';

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
    catch (ex) {
    }
} 


function uploadFile(ClickValue) {   
    var msg = ''; var str = ""; var count = 0; 
    var td = $("td", $(ClickValue).closest("tr"));
    if ($("#ctl00_ContentPlaceHolder1_ddlsctabSession").val() == '0')
        msg += "\r Please select Academic Session !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlsctabcampus").val() == '0')
        msg += "\r Please select Campus !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlsctabSubject").val() == '0')
        msg += "\r Please select  Subject !!!</br>"; 
  
    var filevalue = td.find('.file-input'); 
   
    var fileName = filevalue.val();
    var fileExtension = fileName.split('.').pop().toLowerCase(); 

    if (filevalue[0].files.length === 0) {
        msg += "\r Please select  File !!!</br>";  
    }   
    else { 
    }  

    if (msg != '') {
        iziToast.warning({
            message: msg,
        });
        return false;
    }
   
    $("#hdnupGradAssComponetid").val($("[id*=hdnuplasscomid]", td).val());
    $("#hdnupGradasscomname").val($("[id*=hdnuplasscomname]", td).val());  

    $("#hdnincval").val($("[id*=hdupdinc]", td).val());
    $("#hdnabsval").val($("[id*=hdupdnabs]", td).val()); 
    $("#hdnuHNA").val($("[id*=hdnupdHNA]", td).val());
    $("#hdnuDRP").val($("[id*=hdnupdDRP]", td).val()); 
    $("#hdnUwp").val($("[id*=hdnupdWPF]", td).val()); 


    
    $("#hdUnp").val($("[id*=hdnupdP]", td).val()); 
    $("#hdnUNC").val($("[id*=hdnupdNC]", td).val()); 

    var SectionName=$("[id*=hnduplsectioname]", td).val();
    var comwightage=$("[id*=hdnuplwightages]", td).val();
     
    var ObjCheck = {};
    ObjCheck.sessionId = $('#ctl00_ContentPlaceHolder1_ddlsctabSession').val();
    ObjCheck.CampusId = $('#ctl00_ContentPlaceHolder1_ddlsctabcampus').val();   
    ObjCheck.Scheme = 0;
    ObjCheck.CourseNo =  $('#ctl00_ContentPlaceHolder1_ddlsctabSubject').val();
    ObjCheck.Modelity = 0; 
    ObjCheck.EXAMCOMNO = $("[id*=hdnuplasscomid]", td).val();
    ObjCheck.EXAMNO = $("[id*=hdnuplexamno]", td).val(); 
    ObjCheck.SEMESTERNO = 0; 
    ObjCheck.SECTIONNO = $("[id*=hdupdSECNo]", td).val(); 
    // Check Student Marks IS lock or not
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
       url: liveurl +"Exam/MarkEntry.aspx/ExamActivityCheck",
        data: JSON.stringify(ObjCheck),
        dataType: "json",
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json;charset=utf-8",
        success: function (result) { 
            if (result.d == '') { 
                Swal.fire({
                    html: 'Mark Encoding Activity Not Started !!!',
                    icon: 'question'
                });
            }
            else {  
                if (result.d=="1")
                {   
                    $("[id*=preloader]").hide();
                    
                    var Schemno=0;
                    var CourseNo=$('#ctl00_ContentPlaceHolder1_ddlsctabSubject').val();  
                    var rowCount = 1; 
                    uplsessionId = $('#ctl00_ContentPlaceHolder1_ddlsctabSession').val();
                    uplCampusId = $('#ctl00_ContentPlaceHolder1_ddlsctabcampus').val();
                    uplCourseNo = CourseNo;
                    uplModelity = 0;
                    uplAssName = $('#hdnupGradasscomname').val();
                    uplAsscomid = $('#hdnupGradAssComponetid').val();

                    var strRegNo = "";
                    var uplRegNo = "";
                    var strMARKS = "";
                    var uplMARKS = "";
                    var strINC = "";
                    var uplINC = "";
                    var strABS = "";
                    var uplABS = "";
                    var strABS = "";
                    var uplABS = ""; 
                    var flpname='fileInput'+$("[id*=hdnindex]", td).val()+'';
                    var fileInput = document.getElementById(flpname);
                    var file = fileInput.files[0];  
                    var reader = new FileReader();


                    reader.onload = function (event) {   
                        var jsonData ="";
                        var data ="";
                        var ConvertData ="";
                        var workbook ="";
                        var sheetName ="";
                        var sheet ="";
                      
                        if (fileExtension=="xls")
                        { 
                            data = event.target.result;
                            workbook = XLS.read(data, { type: 'binary' }); // Using the xls library
                            sheetName = workbook.SheetNames[0];
                            sheet = workbook.Sheets[sheetName];
                            // Convert the sheet to JSON format (xls does not have the sheet_to_json function)
                            jsonData = XLS.utils.sheet_to_row_object_array(sheet);
                            ConvertData = JSON.stringify(convertToJsonObject(jsonData)); 
                        }
                        else if (fileExtension=="xlsx")
                        {
                            data = event.target.result;
                            workbook = XLSX.read(data, { type: 'binary' });
                            sheetName = workbook.SheetNames[0];
                            sheet = workbook.Sheets[sheetName];
                            jsonData= XLSX.utils.sheet_to_json(sheet, { header: 1 });
                            ConvertData = JSON.stringify(convertToJsonObject(jsonData)); 
                        } 
                        var obtainINC="";
                        var obtainABS=""; 
                        var obtainHNA=""; 
                        var obtainDRP=""; 
                        var obtainWP=""; 
                        var obtainP=""; 
                        var obtainNC=""; 
                        // Extract header row 
 
                        var jsonArray = [];   

                        for (var i = 1; i < jsonData.length; i++) {   
                            var item = jsonData[i];


                            if (item[4] !== SectionName) {
                                var str = "\r  Invalid Excel Template, Please Upload Valid Template!!!</br>";  
                                if (str !== "") { 
                                    Swal.fire({
                                        html: str,
                                        icon: 'warning'
                                    }); 
                                    return false;
                                } 
                            }


                            obtainINC="";
                            obtainABS=""; 
                            obtainHNA="";
                            obtainDRP=""; 
                            obtainWP="";  
                            obtainP=""; 
                            obtainNC=""; 

                            uplRegNo += item[0] + "$"  
                            if (typeof item[6] !== 'undefined' && item[6] !== null) { 
                                var cellValue = item[6]; 
                                if (cellValue === null || (typeof cellValue !== 'string' && typeof cellValue !== 'number') || cellValue === "") { 
                                
                                    uplMARKS += '999' + "$";
                                } 
                                else if (typeof item[6] === 'string' && item[6].trim().toUpperCase() === "INC" ) {
                                    
                                    if ($("#hdnincval").val()=="1")
                                    {
                                        obtainINC = "905";
                                        uplMARKS +=obtainINC + "$"  
                                    }else
                                    {
                                        str = "\r 'INC' Characters are not allowed in 'Marks'  !!!</br>";  
                                        if (str !="") { 
                                            Swal.fire({
                                                html: str,
                                                icon: 'warning'
                                            });
                                            $("#"+flpname+"").val("");
                                            return false;
                                        } 
                                    }
                                } else if (typeof item[6] === 'string' && item[6].trim().toUpperCase() === "ABS") { 
                                    if ($("#hdnabsval").val()=="1")
                                    {
                                        obtainABS = "902";
                                        uplMARKS +=obtainABS + "$"  
                                    }
                                    else
                                    { 
                                        str = "\r 'ABS' Characters are not allowed in 'Marks'  !!!</br>";  
                                        if (str !="") { 
                                            Swal.fire({
                                                html: str,
                                                icon: 'warning'
                                            });
                                            $("#"+flpname+"").val("");
                                            return false;
                                        } 
                                    }
                                } 
                                else if (typeof item[6] === 'string' && item[6].trim().toUpperCase() === "HNA" ) {
                                   
                                    if ($("#hdnuHNA").val()=="1")
                                    {
                                        obtainHNA = "906";
                                        uplMARKS +=obtainHNA + "$"  
                                    }else
                                    {
                                        str = "\r 'HNA' Characters are not allowed in 'Marks'  !!!</br>";  
                                        if (str !="") { 
                                            Swal.fire({
                                                html: str,
                                                icon: 'warning'
                                            });
                                            $("#"+flpname+"").val("");
                                            return false;
                                        } 
                                    }
                                }
                                else if (typeof item[6] === 'string' && item[6].trim().toUpperCase() === "DRP" ) {
                                    
                                    if ($("#hdnuDRP").val()=="1")
                                    {
                                        obtainDRP = "907";
                                        uplMARKS +=obtainDRP + "$"  
                                    }else
                                    {
                                        str = "\r 'DRP' Characters are not allowed in 'Marks'  !!!</br>";  
                                        if (str !="") { 
                                            Swal.fire({
                                                html: str,
                                                icon: 'warning'
                                            });
                                            $("#"+flpname+"").val("");
                                            return false;
                                        } 
                                    }
                                }
                                else if (typeof item[6] === 'string' && item[6].trim().toUpperCase() === "WP" ) {
                                    
                                    if ($("#hdnUwp").val()=="1")
                                    {
                                        obtainWP = "908";
                                        uplMARKS +=obtainWP + "$"  
                                    }else
                                    {
                                        str = "\r 'WP' Characters are not allowed in 'Marks'  !!!</br>";  
                                        if (str !="") { 
                                            Swal.fire({
                                                html: str,
                                                icon: 'warning'
                                            });
                                            $("#"+flpname+"").val("");
                                            return false;
                                        } 
                                    }

                                }
                                else if (typeof item[6] === 'string' && item[6].trim().toUpperCase() === "PASS" ) {
                                    
                                    if ($("#hdUnp").val()=="1")
                                    {
                                        obtainP = "900";
                                        uplMARKS +=obtainP + "$"  
                                    }else
                                    {
                                        str = "\r 'PASS' Characters are not allowed in 'Marks'  !!!</br>";  
                                        if (str !="") { 
                                            Swal.fire({
                                                html: str,
                                                icon: 'warning'
                                            });
                                            $("#"+flpname+"").val("");
                                            return false;
                                        } 
                                    }

                                }
                                else if (typeof item[6] === 'string' && item[6].trim().toUpperCase() === "NC" ) {
                                    
                                    if ($("#hdnUNC").val()=="1")
                                    {
                                        obtainNC = "901";
                                        uplMARKS +=obtainNC + "$"  
                                    }else
                                    {
                                        str = "\r 'NC' Characters are not allowed in 'Marks'  !!!</br>";  
                                        if (str !="") { 
                                            Swal.fire({
                                                html: str,
                                                icon: 'warning'
                                            });
                                            $("#"+flpname+"").val("");
                                            return false;
                                        } 
                                    }

                                }
                                else {
                                    var hasWhiteSpace = /\s/.test(item[6]); 
                                    if (hasWhiteSpace) {
                                        str = "\r Please enter a value without any spaces in the 'Marks' field.  !!!</br>"; 
                                        if (str !="") { 
                                            Swal.fire({
                                                html: str,
                                                icon: 'warning'
                                            });
                                            $("#"+flpname+"").val("");
                                            return false;
                                        } 
                                    }  
                                    else
                                    {   
                                        var inputValue = parseFloat(item[6]);  
                                        if (isNaN(inputValue) || inputValue < 0 || inputValue > comwightage) { 
                                            if (isNaN(inputValue))
                                            {     
                                                if ($("#hdnincval").val()=="0")
                                                {
                                                    str = "\r   'INC'  Characters are not  allowed in 'Marks'  !!!</br>";  
                                                }
                                                else
                                                {str = "\r  Only 'INC'  Characters are   allowed in 'Marks'  !!!</br>";  
                                                }
                                                if ($("#hdnabsval").val()=="0")
                                                { 
                                                    str = "\r  'ABS' Characters are not allowed in 'Marks'  !!!</br>";  
                                                }
                                                else
                                                {
                                                   
                                                    str = "\r Only  'ABS' Characters are  allowed in 'Marks'  !!!</br>";  
                                                }
                                                if ($("#hdnuHNA").val()=="0")
                                                { 
                                                    str = "\r  'HNA' Characters are not allowed in 'Marks'  !!!</br>";  
                                                }
                                                else
                                                {
                                                   
                                                    str = "\r Only  'HNA' Characters are  allowed in 'Marks'  !!!</br>";  
                                                }
                                                if ($("#hdnuDRP").val()=="0")
                                                { 
                                                    str = "\r  'DRP' Characters are not allowed in 'Marks'  !!!</br>";  
                                                }
                                                else
                                                {                                                   
                                                    str = "\r Only  'DRP' Characters are  allowed in 'Marks'  !!!</br>";  
                                                }
                                                if ($("#hdnUwp").val()=="0")
                                                { 
                                                    str = "\r  'WP' Characters are not allowed in 'Marks'  !!!</br>";  
                                                }
                                                else
                                                {                                                   
                                                    str = "\r Only  'WP' Characters are  allowed in 'Marks'  !!!</br>";  
                                                }

                                                if ($("#hdUnp").val()=="0")
                                                { 
                                                    str = "\r  'PASS' Characters are not allowed in 'Marks'  !!!</br>";  
                                                }
                                                else
                                                {                                                   
                                                    str = "\r Only  'PASS' Characters are  allowed in 'Marks'  !!!</br>";  
                                                }

                                                if ($("#hdnUNC").val()=="0")
                                                { 
                                                    str = "\r  'NC' Characters are not allowed in 'Marks'  !!!</br>";  
                                                }
                                                else
                                                {                                                   
                                                    str = "\r Only  'NC' Characters are  allowed in 'Marks'  !!!</br>";  
                                                }

                                                if ($("#hdnincval").val()=="0"&&$("#hdnabsval").val()=="0"&&$("#hdnuHNA").val()=="0"&&$("#hdnuDRP").val()=="0"&&$("#hdnUwp").val()=="0" &&$("#hdUnp").val()=="0" &&$("#hdnUNC").val()=="0")
                                                {
                                                    str = "\r Only 'INC' &  'ABS'  &  'HNA'  &  'DRP'  &  'WP' &  'PASS'  &  'NC' Characters are  allowed in 'Marks'  !!!</br>";  
                                                }

                                                if (str !="") { 
                                                    Swal.fire({
                                                        html: str,
                                                        icon: 'warning'
                                                    });
                                                    $("#"+flpname+"").val("");
                                                    return false;
                                                } 
                                            }
                                            else
                                            {
                                                str = "\r Please enter 'Marks'  between 0 and "+comwightage+" !!!</br>";  
                                                if (str !="") { 
                                                    Swal.fire({
                                                        html: str,
                                                        icon: 'warning'
                                                    });
                                                    $("#"+flpname+"").val("");
                                                    return false;
                                                }  
                                            }
                                        } 
                                    }   
                                    uplMARKS +=item[6]+ "$"  
                                   
                                }
                            } else { 
                                uplMARKS +='999'+ "$"  
                            } 
                        } 
                       
                        uplRegNo = uplRegNo.slice(0, -1);
                        uplMARKS = uplMARKS.slice(0, -1);  
                        var data = [];
                        var alldata = { 
                            'MARKNO': 0,
                            'SESSIONNO': uplsessionId,
                            'SCHEMNO': Schemno,
                            'LEARNINGMODALITYNO': uplModelity,
                            'COURSENO':uplCourseNo,
                            'ASSESSMENT_NO': uplAsscomid,
                            'CAMPUSNO': uplCampusId,
                            'RegNo': uplRegNo,
                            'MARKS': uplMARKS, 
                            'ASSESSMENT_COMPONENT_NAME': uplAssName,
                            'REPEAT_STATUS': "",
                            'WIGHTAGES':comwightage,
                            'SEMESTERNO':0,
                            'SECTIONNO':$("[id*=hdupdSECNo]", td).val()
                        } 
                        data.push(alldata);

                        var exportmarks = JSON.stringify(data); 
                        $.ajax({
                           url: liveurl +"Exam/MarkEntry.aspx/uploadMarkEntry",//Home.aspx is the page   
                            type: 'POST',
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            data: JSON.stringify({ 'markentry': exportmarks }),
                            beforeSend: function () { $("[id*=preloader]").show(); },
                            success: function (response) { 
                                if (response.d == "1") {
                                    iziToast.success({
                                        message: 'Grade Encoding Completed Successfully !!!',
                                    });
                                    $("#"+flpname+"").val("");
                                    $("[id*=preloader]").hide(); 
                                    binduplodgrade();
                                } 
                                else  if (response.d == "3") {
                                    Swal.fire({
                                        html: 'Grade Encoding Already Lock !!!',
                                        icon: 'error'
                                    }); 
                                    $("#"+flpname+"").val("");
                                    $("[id*=preloader]").hide(); 
                                    binduplodgrade();
                                } 
                                else  if (response.d == "4") {
                                    Swal.fire({
                                        html: 'Please enter  Marks   between 0 and '+comwightage+' !!!',
                                        icon: 'error'
                                    });  
                                    return false; 
                                    $("#"+flpname+"").val("");
                                    $("[id*=preloader]").hide(); 
                                    binduplodgrade();
                                } 
                                else {
                                    Swal.fire({
                                        html: 'Please choose a valid template file !!!',
                                        icon: 'error'
                                    });
                                    $("[id*=preloader]").hide();
                                    return false;
                                } 
                                $("#"+flpname+"").val("");
                                $("[id*=preloader]").hide();
                            },
                            error: function ajaxError(response) {
                
                                Swal.fire({
                                    html: 'Error Occurred !!!',
                                    icon: 'error'
                                });
                                return false;
                            }
                        });
                    };
                    reader.readAsBinaryString(file); 
                    
                }
                else
                {  
                    var message=result.d;
                    Swal.fire({
                        html: message +' !!!',
                        icon: 'question'
                    }); 
                    return false;
                }
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

// Function to convert data to a JSON object
function convertToJsonObject(data) {

    var headers = data[0];
    var jsonArray = [];

    for (var i = 1; i < data.length; i++) {
        var obj = {};
        for (var j = 0; j < headers.length; j++) {
            obj[headers[j]] = data[i][j];
        }
        jsonArray.push(obj);
    }
    return jsonArray;
}
 
// --------------------------------------------------- End Second tab -------------------------------------------------------------------------//



// -----------------------------------------------------Start Third tab ------------------------------------------------------------------------------//
$('#ctl00_ContentPlaceHolder1_ddlGradeSession').on('change', function (e) { 
    $('#ctl00_ContentPlaceHolder1_ddlGradesubject').empty();
    $('#ctl00_ContentPlaceHolder1_ddlGradesubject').append("<option value='0'>Please Select</option>"); 
    $('#ctl00_ContentPlaceHolder1_ddlGradeCampus').val(1);
    BindSubmissionSubject();
    $("#BindDynamicGardeSubmissionTable").empty(); 
}); 
 
function BindSubmissionSubject()
{

    var msg = ''; var str = ""; var count = 0;

    if ($("#ctl00_ContentPlaceHolder1_ddlGradeSession").val() == '0')
        msg += "\r Please select Academic Session !!!</br>";  

    if (msg != '') {
        iziToast.warning({
            message: msg,
        });
        $('#ctl00_ContentPlaceHolder1_ddlGradeCampus').val(0);
        return false;
    } 

    $('#ctl00_ContentPlaceHolder1_ddlGradesubject').empty();
    $('#ctl00_ContentPlaceHolder1_ddlGradesubject').append("<option value='0'>Please Select</option>"); 
  
    $("#BindDynamicGardeSubmissionTable").empty();
    //$("#divwieghtage").empty(); 
    if ($('#ctl00_ContentPlaceHolder1_ddlGradeCampus').val() != '0') {
        try {
           
            var Obj = {};
            Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlGradeSession').val();
            Obj.CampusId =$('#ctl00_ContentPlaceHolder1_ddlGradeCampus').val();

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
               url: liveurl +"Exam/MarkEntry.aspx/GetThirdTabCourse",
                data: JSON.stringify(Obj),
                dataType: "json",
                beforeSend: function () { $("[id*=preloader]").show(); },
                complete: function () { $("[id*=preloader]").hide(); },
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (result.d == '') { 
                        Swal.fire({
                            html: 'Subjects not found !!!',
                            icon: 'question'
                        }); 
                        return false; 
                    }
                    else 
                    {
                        $('#ctl00_ContentPlaceHolder1_ddlGradesubject').empty();
                        $('#ctl00_ContentPlaceHolder1_ddlGradesubject').append("<option value='0'>Please Select</option>");
                        $.each(result.d, function (key, value) {
                            $("[id*=preloader]").show();
                            $("#ctl00_ContentPlaceHolder1_ddlGradesubject").append($("<option></option>").val(value.COURSENO).html(value.COURSE_NAME));
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
    else {
        $('#ctl00_ContentPlaceHolder1_ddlGradesubject').empty();
        $('#ctl00_ContentPlaceHolder1_ddlGradesubject').append("<option value='0'>Please Select</option>");
    }

}

$('#ctl00_ContentPlaceHolder1_ddlGradesubject').on('change', function (e) {
    if ($('#ctl00_ContentPlaceHolder1_ddlGradesubject').val() != '0') {
        //$('#ctl00_ContentPlaceHolder1_ddlGradeModelity').val(0).change();
        $("#BindDynamicGardeSubmissionTable").empty(); 
        var Obj = {}; 
        Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlGradeSession').val();
        Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlGradeCampus').val();
        Obj.Scheme = 0;
        Obj.CourseNo = $('#ctl00_ContentPlaceHolder1_ddlGradesubject').val();
        Obj.SectionNo = 0;

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
           url: liveurl +"Exam/MarkEntry.aspx/CheckWightagesBind",
            data: JSON.stringify(Obj),
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                if (result.d == '1') { 
                    Swal.fire({
                        html: 'Weightages  Not Defined for Subject, Please Contact to Admin !!!',
                        icon: 'question'
                    });
                    $("#BindDynamicGardeSubmissionTable").empty(); 
                    return false; 
                }
                else if (result.d == '2') { 
                    Swal.fire({
                        html: 'Assessment Component Marks Not Defined for Subject !!!',
                        icon: 'question'
                    });
                    $("#BindDynamicGardeSubmissionTable").empty(); 
                    return false; 
                }  
                else {
                    Bindmodelity();
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

        //$("#divwieghtage").empty();
    }
});

function Bindmodelity()
{
    if ($('#ctl00_ContentPlaceHolder1_ddlGradesubject').val() != '0') {
        try {
            $("#BindDynamicGardeSubmissionTable").empty();
            // $("#divwieghtage").empty();
            var optionSelected = $("option:selected", this);
            var valueSelected = this.value;
            var Obj = {};
            Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlGradeSession').val();
            Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlGradeCampus').val();  
            Obj.Scheme = 0;
            Obj.CourseNo =  $('#ctl00_ContentPlaceHolder1_ddlGradesubject').val();
            Obj.Modelity = 0;   
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
               url: liveurl +"Exam/MarkEntry.aspx/GetGradeSubmssion",
                data: JSON.stringify(Obj),
                dataType: "json",
                beforeSend: function () { $("[id*=preloader]").show(); }, 
                contentType: "application/json;charset=utf-8",
                success: function (response) { 
                    if (response.d == '') { 
                        Swal.fire({
                            html: 'Records not found !!!',
                            icon: 'question'
                        });
                        $("#BindDynamicGardeSubmissionTable").empty(); 
                        $("#divgradesub").hide();
                        $("[id*=preloader]").hide();
                    }
                    else
                    { 
                        $("#divgradesub").show()
                        $("[id*=preloader]").show();
                        str = '<thead><tr><th><span>Report </span></th>  <th><span class="Exam">Exam</span></th><th><span class="Status">Status</span></th> <th><span class="DateofSubmission">Date of Submission</span></th>  <th><span class="StatusbyProgramHead">Status by Program Head</span></th> <th><span class="DateProgramHead">Date (Program Head)</span></th><th><span class="RemarksbyProgramHead">Remarks by Program Head</span></th><th><span class="StatusbyDean">Status by Dean</span></th><th><span class="DateDean">Date (Dean)</span></th><th><span class="RemarkbyDean">Remark by Dean</span></th></tr></thead><tbody>';
                        $.each(response.d, function (index, GetValue) {
                            str = str + '<tr>'
                            str = str + '<td>'
                            str = str + '<i class="fa fa-file-excel-o text-success display-6 me-2" title="Excel" onclick="GradeSubExcel(this)"></i> </td>' 
                            //str = str + '<i class="bi bi-filetype-pdf text-success display-6" title="PDF"></i>'
                            //str = str + '<td>'+GetValue.SEMESTERNAME+'</td>'  
                            str = str + '<td><a href="#" class="GradeSub" href="#" onclick="EditSubmission(this)">'+GetValue.EXAMNAME+'</a> <input type="hidden" id="hdnSubENO" value="' + GetValue.EXAMNO + '"/><input type="hidden" id="hdnsubLoc" value="' + GetValue.LOCK_DATE + '"/><input type="hidden" id="hdnsubsem" value="' + GetValue.SEMESTERNO + '"/><input type="hidden" id="hdnteacherid" value="' + GetValue.TeacherId + '"/></td>'
                     
                            if (GetValue.SUBMIT_TO_HOD_FLAG=="0")
                            { 
                                if (GetValue.HOD_FLAG_REMARK=="1"|| GetValue.DEAN_FLAG_REMARK=="2" ||  GetValue.REGISTAR_FLAG_REMARK=="3")
                                {
                                    str = str + '<td><span class="badge badge-warning">Open for Moderation</span></td>'
                                    str = str + '<td>-</td>' 
                                } 
                                else
                                { str = str + '<td><span class="badge badge-warning">Pending</span></td>'
                                    str = str + '<td>'+ GetValue.SUBMIT_TO_HOD_DATE+'</td>' 
                                }
                            }
                            else
                            {
                                str = str + '<td><span class="badge badge-success">Submitted</span></td>'
                                str = str + '<td>'+ GetValue.SUBMIT_TO_HOD_DATE+'</td>' 

                            } 
                            if (GetValue.SUBMIT_TO_DEAN_BY=="0")
                            {
                                //28-09-2023
                                if (GetValue.REGISTAR_FLAG_REMARK=="3")
                                {
                                    str = str + '<td><span class="badge badge-warning">Open for Moderation</span></td>'
                                    str = str + '<td>'+GetValue.REGISTARDATE+'</td>'
                                    str = str + '<td>'+GetValue.REGIDTAR_REMARK+'</td>'
                                } 
                                else  if (GetValue.HOD_FLAG_REMARK=="1" || GetValue.DEAN_FLAG_REMARK=="2")
                                {
                                    str = str + '<td><span class="badge badge-warning">Open for Moderation</span></td>'
                                    str = str + '<td>'+GetValue.HODMODDATE+'</td>'
                                    str = str + '<td>'+GetValue.HODREMARK+'</td>'
                                }
                              
                                else
                                {
                                    str = str + '<td><span class="badge badge-warning">Pending</span></td>'
                                    str = str + '<td>-</td>'
                                    str = str + '<td>-</td>'
                                } 
                            }
                            else
                            {
                                str = str + '<td><span class="badge badge-success">Approved</span></td>'
                                str = str + '<td>'+GetValue.SUBMIT_TO_DEAN_DATE+'</td>'
                                str = str + '<td>-</td>'
                            }  
                            if (GetValue.RELEASEGRAD=="0")
                            {
                                //28-09-2023
                                if (GetValue.REGISTAR_FLAG_REMARK=="3")
                                {
                                    str = str + '<td><span class="badge badge-warning">Open for Moderation</span></td>'
                                    str = str + '<td>'+GetValue.REGISTARDATE+'</td>'
                                    str = str + '<td>'+GetValue.REGIDTAR_REMARK+'</td>'
                                } 
                                else if (GetValue.DEAN_FLAG_REMARK=="2")
                                {
                                    str = str + '<td><span class="badge badge-warning">Open for Moderation</span></td>'
                                    str = str + '<td>'+GetValue.DEANMODDATE+'</td>'
                                    str = str + '<td>'+GetValue.DEANREMARK+'</td>'
                                } 
                               
                                else
                                {
                                    str = str + '<td><span class="badge badge-warning">Pending</span></td>'
                                    str = str + '<td>-</td>'
                                    str = str + '<td>-</td>'
                                } 
                            }
                            else
                            { 
                                str = str + '<td><span class="badge badge-success">Approved</span></td>'
                                str = str + '<td>'+GetValue.RELEASEGRAD_DATE+'</td>'
                                str = str + '<td>'+GetValue.DEANREMARK+'</td>'
                            }
                            str = str + '</tr>'
                        });
                        str = str + '</tbody>';
                        RemoveTableDynamically('#BindDynamicGardeSubmissionTable');
                        $("#BindDynamicGardeSubmissionTable").append(str);
                        $("[id*=preloader]").hide();
                    }
                },
                error: function ajaxError(response) { 
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
    }else
    {    $("#BindDynamicGardeSubmissionTable").empty(); 
        $("#divgradesub").hide();
        $("[id*=preloader]").hide();
    }
}
 
function EditSubmission(ClickValue) { 
    var msg = ''; var str = ""; var count = 0;

    if ($("#ctl00_ContentPlaceHolder1_ddlGradeSession").val() == '0')
        msg += "\r Please select Academic Session !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlGradeCampus").val() == '0')
        msg += "\r Please select Campus !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlGradesubject").val() == '0')
        msg += "\r Please select  Subject !!!</br>";  

    if (msg != '') {
        iziToast.warning({
            message: msg,
        });
        return false;
    }

 
    try {    
        var td = $("td", $(ClickValue).closest("tr"));    
        var rowCount = 1; 
        var checkstatus= $("[id*=hdnsubLoc]", td).val(); 
        if (checkstatus=="Pending")
        { 
            $("#btnSubmitProgramHead").show();
        }
        else
        {  $("#btnSubmitProgramHead").hide();
        } 
        $('#hdnsubexamno').val($("[id*=hdnSubENO]", td).val());
        $('#hdnsubsemester').val($("[id*=hdnsubsem]", td).val());
        $('#hdnsubteacher').val($("[id*=hdnteacherid]", td).val());
        var Obj = {}; 
        Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlGradeSession').val();
        Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlGradeCampus').val(); 
        Obj.Scheme = 0; 
        Obj.CourseNo = $('#ctl00_ContentPlaceHolder1_ddlGradesubject').val(); 
        Obj.Modelity =0; 
        Obj.EXAMCOMNO = 0;
        Obj.EXAMNO =  $("[id*=hdnSubENO]", td).val();
        Obj.SEMESTERNO =  0;
        $.ajax({ 
            type: "POST",
            contentType: "application/json; charset=utf-8",
           url: liveurl +"Exam/MarkEntry.aspx/CheckComponentwight",
            data: JSON.stringify(Obj),
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            //complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                if (response.d == '') { 
                }  
                else {  
                    if (response.d=="DONE")
                    { 
                        $.ajax({ 
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                           url: liveurl +"Exam/MarkEntry.aspx/Checkmarksfill",
                            data: JSON.stringify(Obj),
                            dataType: "json",
                            beforeSend: function () { $("[id*=preloader]").show(); },
                            complete: function () { $("[id*=preloader]").hide(); },
                            contentType: "application/json;charset=utf-8",
                            success: function (response) {
                                if (response.d !='0') {
                                    Swal.fire({
                                        html: 'Grade encoding is pending for few students, Please check once !!!',
                                        icon: 'error'
                                    });
                                    return false;
                                } 
                                else
                                {
                                    $.ajax({ 
                                        type: "POST",
                                        contentType: "application/json; charset=utf-8",
                                       url: liveurl +"Exam/MarkEntry.aspx/StudentSubmissionList",
                                        data: JSON.stringify(Obj),
                                        dataType: "json",
                                        beforeSend: function () { $("[id*=preloader]").show(); },
                                        complete: function () { $("[id*=preloader]").hide(); },
                                        contentType: "application/json;charset=utf-8",
                                        success: function (response) {
                                            if (response.d == '') {
                                                Swal.fire({
                                                    html: 'Records not found !!!',
                                                    icon: 'error'
                                                });
                                                $("[id*=preloader]").hide();
                                                return false;
                                            }
                                            else {   
                                                $(".grade-submissions").hide();
                                                $(".submission-components").show();   
                                                BindGradSubmission(response.d);
                                                $("[id*=preloader]").hide();
                                            } 
                                        },
                                        error: function ajaxError(response) { 
                                            Swal.fire({
                                                html: 'Error Occurred !!!',
                                                icon: 'error'
                                            });
                                            $("[id*=preloader]").hide();
                                            return false;
                                        }
                                    }); 
                                }
                            },
                            error: function ajaxError(response) { 
                                Swal.fire({
                                    html: 'Error Occurred !!!',
                                    icon: 'error'
                                });
                                $("[id*=preloader]").hide();
                                return false;
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
            },
            error: function ajaxError(response) { 
                Swal.fire({
                    html: 'Error Occurred !!!',
                    icon: 'error'
                });
                $("[id*=preloader]").hide();
            }
        });  
    }
    catch (ex) {
    }
}


function BindGradSubmission(data) {
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
                if (key == "REGNO" )
                {
                    headerRow.append($('<th>').text('Student ID'));
                }
                else if(key == "STUDENTNAME")
                {
                    headerRow.append($('<th>').text('Student Name'));
                }
                else if(key == "BRANCH_CODE")
                {
                    headerRow.append($('<th>').text('Course'));
                }
                else if(key == "SEMESTERNAME")
                {
                    headerRow.append($('<th>').text('Semester'));
                }
                else if (key == "EXAMNO" || key == "IDNO" || key == "CAMPUSNAME") {
                    headerRow.append($('<th style="Display:none;">').text(key))
                }
                else {
                    headerRow.append($('<th>').text(key));
                }
            });
        }
        table.append(row); 
        $.each(item, function (key, value) { 
            if (key == "SRNo")
            {
                row.append($('<td>').text(valno)); 
            }
            else  if (key == "EXAMNO" ||  key == "IDNO"||  key == "CAMPUSNAME")
            {
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

 

$("#btnSubmitProgramHead").click(function () {
    var msg = '';
    var str = "";
    var count = 0;

    if ($("#ctl00_ContentPlaceHolder1_ddlGradeSession").val() == '0')
        msg += "\r Please select Academic Session !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlGradeCampus").val() == '0')
        msg += "\r Please select Campus !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlGradesubject").val() == '0')
        msg += "\r Please select  Subject !!!</br>"; 

    if (msg != '') {
        iziToast.warning({
            message: msg,
        });
        return false;
    }
    var ObjCheckdate = {};
    ObjCheckdate.sessionId = $('#ctl00_ContentPlaceHolder1_ddlGradeSession').val();
    ObjCheckdate.CampusId = $('#ctl00_ContentPlaceHolder1_ddlGradeCampus').val(); 
    ObjCheckdate.CourseNo = $('#ctl00_ContentPlaceHolder1_ddlGradesubject').val();
    ObjCheckdate.Modelity = 0;
    ObjCheckdate.EXAMNO = $("#hdnsubexamno").val();
    ObjCheckdate.EXAMCOMNO = 0;
    ObjCheckdate.Scheme = 0;
    ObjCheckdate.SEMESTERNO = 0;  
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
       url: liveurl + "Exam/MarkEntry.aspx/ExamSubActivityCheck",
        data: JSON.stringify(ObjCheckdate),
        dataType: "json",
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json;charset=utf-8",
        success: function (result) {
            if (result.d == '') {
                Swal.fire({
                    html: 'Record not found !!!',
                    icon: 'question'
                });
                return false;
            } else {
                if (result.d == "1") {
                    var delconfirm;
                    Swal.fire({
                        title: 'This will lock grade encoding and it will be submitted to Program Head for Approval, Do you wish to Continue?',
                        icon: 'warning',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33',
                        confirmButtonText: 'Yes',
                        delconfirm: 'Yes'
                    }).then((result) => {
                        if (result.isConfirmed) {
                            var Obj = {};
                Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlGradeSession').val();
                Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlGradeCampus').val(); 
                Obj.CourseNo = $('#ctl00_ContentPlaceHolder1_ddlGradesubject').val();
                Obj.Modelity = 0;
                Obj.EXAMNO = $("#hdnsubexamno").val();
                Obj.EXAMCOMNO = 0;
                Obj.scheme = 0;
                Obj.SEMESTERNO = 0;
                Obj.TeacherId = $('#hdnsubteacher').val();

                $.ajax({
                   url: liveurl + "Exam/MarkEntry.aspx/SubmitProgramHead",
                    type: "POST",
                    data: JSON.stringify(Obj),
                    dataType: "json",
                    beforeSend: function () { $("[id*=preloader]").show(); },
                    contentType: "application/json;charset=utf-8",
                    success: function (response) {
                        if (response.d == "1") {
                            Swal.fire(
                                'Submitted!',
                                'Student Mark has been submitted to Program Head .',
                                'success'
                            );
                            $("#btnSubmitProgramHead").hide();
                            $("[id*=preloader]").hide();
                            Bindmodelity();
                            return false;
                        } else if (response.d == "2") {
                            Swal.fire({
                                html: 'Student Mark already submitted to Program Head !!!',
                                icon: 'error'
                            });
                            $("#btnSubmitProgramHead").hide();
                            $("[id*=preloader]").hide();
                            Bindmodelity();
                            return false;
                        } else {
                            Swal.fire({
                                html: 'Please check some Student Mark Encoding is pending !!!',
                                icon: 'error'
                            });
                            $("[id*=preloader]").hide();
                            return false;
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
            $("[id*=preloader]").hide();
        });
} else {

 var message = result.d;
Swal.fire({
    html: message + ' !!!',
    icon: 'error'
});
 
return false;
}
}
}
});
});

function GradeSubExcel(ClickValue) { 

    var msg = ''; var str = ""; var count = 0;

    if ($("#ctl00_ContentPlaceHolder1_ddlGradeSession").val() == '0')
        msg += "\r Please select Academic Session !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlGradeCampus").val() == '0')
        msg += "\r Please select Campus !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlGradesubject").val() == '0')
        msg += "\r Please select  Subject !!!</br>";  

    if (msg != '') {
        iziToast.warning({
            message: msg,
        });
        return false;
    }

 
    try {    
        var td = $("td", $(ClickValue).closest("tr"));    
        var rowCount = 1; 
        var checkstatus= $("[id*=hdnsubLoc]", td).val(); 
        if (checkstatus=="Pending")
        { 
            $("#btnSubmitProgramHead").show();
        }
        else
        {  $("#btnSubmitProgramHead").hide();
        } 
        $('#hdnsubexamno').val($("[id*=hdnSubENO]", td).val());
        $('#hdnsubsemester').val($("[id*=hdnsubsem]", td).val());
        $('#hdnsubteacher').val($("[id*=hdnteacherid]", td).val());
        var Obj = {}; 
        Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlGradeSession').val();
        Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlGradeCampus').val(); 
        Obj.Scheme = 0; 
        Obj.CourseNo = $('#ctl00_ContentPlaceHolder1_ddlGradesubject').val(); 
        Obj.Modelity =0; 
        Obj.EXAMCOMNO = 0;
        Obj.EXAMNO =  $("[id*=hdnSubENO]", td).val();
        Obj.SEMESTERNO =  0;
        $.ajax({ 
            type: "POST",
            contentType: "application/json; charset=utf-8",
           url: liveurl +"Exam/MarkEntry.aspx/CheckComponentwight",
            data: JSON.stringify(Obj),
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            //complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                if (response.d == '') { 
                }  
                else {  
                    if (response.d=="DONE")
                    { 
                        $.ajax({ 
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                           url: liveurl +"Exam/MarkEntry.aspx/Checkmarksfill",
                            data: JSON.stringify(Obj),
                            dataType: "json",
                            beforeSend: function () { $("[id*=preloader]").show(); },
                            complete: function () { $("[id*=preloader]").hide(); },
                            contentType: "application/json;charset=utf-8",
                            success: function (response) {
                                if (response.d !='0') {
                                    Swal.fire({
                                        html: 'Grade encoding is pending for few students, Please check once !!!',
                                        icon: 'error'
                                    });
                                    return false;
                                } 
                                else
                                {
                                    $.ajax({ 
                                        type: "POST",
                                        contentType: "application/json; charset=utf-8",
                                       url: liveurl +"Exam/MarkEntry.aspx/ExportSubmissionList",
                                        data: JSON.stringify(Obj),
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
                                                    'BRANCH_CODE': 'Course',
                                                    'CAMPUSNAME': 'Campus Name',
                                                    'SEMESTERNAME': 'Semester',  
                                                    'COLLEGE_NAME': 'College Name',
                                                    'COURSE_NAME': 'Subject Name',
                                                    'CCODE': 'Course Code',
                                                    'UA_FULLNAME': 'Faculty Name',
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
                                                var excelFileName = 'GradeEncodingStudentList.xlsx';

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
                            },
                            error: function ajaxError(response) { 
                                Swal.fire({
                                    html: 'Error Occurred !!!',
                                    icon: 'error'
                                });
                                $("[id*=preloader]").hide();
                                return false;
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
            },
            error: function ajaxError(response) { 
                Swal.fire({
                    html: 'Error Occurred !!!',
                    icon: 'error'
                });
                $("[id*=preloader]").hide();
            }
        });  
    }
    catch (ex) {
    } 
}

// ---------------------------------------------End Third tab ---------------------------------------------------------------------//



//// ---------------------------------------------Start Generate Report---------------------------------------------------------------------//

 
$("#btngenratereport").click(function () {   
    try { 
       
        var msg = ''; var str = ""; var count = 0;

        if ($("#ctl00_ContentPlaceHolder1_ddlGradeSession").val() == '0')
            msg += "\r Please select Academic Session !!!</br>";

        if ($("#ctl00_ContentPlaceHolder1_ddlGradeCampus").val() == '0')
            msg += "\r Please select Campus !!!</br>";

        if ($("#ctl00_ContentPlaceHolder1_ddlGradesubject").val() == '0')
            msg += "\r Please select  Subject !!!</br>"; 

        //if ($("#ctl00_ContentPlaceHolder1_ddlGradeModelity").val() == '0')
        //    msg += "\r Please select  Learning Modality !!!</br>";   

        if (msg != '') {
            iziToast.warning({
                message: msg,
            });
            return false;
        } 
       
        var Obj = {};
        Obj.sessionId = $('#ctl00_ContentPlaceHolder1_ddlGradeSession').val();
        Obj.CampusId = $('#ctl00_ContentPlaceHolder1_ddlGradeCampus').val(); 
        var splitsubject = $('#ctl00_ContentPlaceHolder1_ddlGradesubject').val().split('$$'); 
        var Schemno=splitsubject[0];
        var CourseNo=splitsubject[1];
        Obj.Scheme = Schemno;
        Obj.CourseNo = CourseNo;
        Obj.Modelity = 0;   
        Obj.div ="divMsg.InnerHtml";
        Obj.SemNo = 0;
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
           url: liveurl +"Exam/MarkEntry.aspx/Submissionreport",
            data: JSON.stringify(Obj), 
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
                else
                {
                    var url = response.d;  
                    window.open(url,'GRADING ENCODING REPORT','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                Swal.fire({
                    html: 'Error Occurred: ' + errorThrown,
                    icon: 'error'
                });
                return false; 
            }
        });

    }
    catch (ex) { 

    } 
});


// ---------------------------------------------End Generate Report ---------------------------------------------------------------------//




 