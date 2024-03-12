//===============================================//
// MODULE NAME   : RFC ERP Portal
// PAGE NAME     : Subject Transmutation Tagging
// CREATION DATE : 05-12-2023
// CREATED BY    : Gaurav Kalbande
// Modified BY   : Ekansh Moundekar
// Modified Date : 16-02-2024
//===============================================//  
$(document).ready(function () {
    // $("#btnLock").hide();
    $("#ctl00_ContentPlaceHolder1_ddlCollege").focus();
});
function BindSubjectTagging() {
    var data = [];
    var valno = 0;
    var VarCOLLEGE_ID = $("#ctl00_ContentPlaceHolder1_ddlCollege").val();
    var VarSCHEMENO = $("#ctl00_ContentPlaceHolder1_ddlCurriculum").val();
    // var VarSUBCLASSIFIC_NO = $("#ctl00_ContentPlaceHolder1_ddlSubjectBoardClassification").val();
    var VarSUBCLASSIFIC_NO = 0;
    var VarGRADING_SCHEME_NO = $("#ctl00_ContentPlaceHolder1_ddlTransmutation").val();
    var VarISACTIVE = 1; 
    $("tr.Trclass input[type=checkbox]:checked").each(function () {
        var row = $(this).closest("tr")[0]; 
        var VarCCODE = row.cells[2].innerHTML;
        var VarCOURSENAME = row.cells[3].innerHTML;
        var VarCREDITS = row.cells[5].innerHTML;
        var VarCOURSETYPE = row.cells[4].innerHTML; 
      
        var VarTAGGING_NO = ($("[id*=hdnSubId]", row).val());
        var VarCOURSENO = ($("[id*=hdnCourseId]", row).val()); 
        
        //  var VarSUBCLASSIFIC_NAME = $("select#ctl00_ContentPlaceHolder1_ddlSubjectBoardClassification option:selected").text();
        var VarGRADING_SCHEME_NAME = $("select#ctl00_ContentPlaceHolder1_ddlTransmutation option:selected").text();
        if (VarTAGGING_NO == "") {
            VarTAGGING_NO = "0";
        }

        var alldata = {
            'SUBJECT_TAGGING_NO': VarTAGGING_NO,
            'COLLEGE_ID': VarCOLLEGE_ID,
            'SCHEMENO': VarSCHEMENO,
            'SUBCLASSIFIC_NO': VarSUBCLASSIFIC_NO,
            'GRADING_SCHEME_NO': VarGRADING_SCHEME_NO,
            'COURSENO': VarCOURSENO,
            'CCODE': VarCCODE,
            'COURSENAME': VarCOURSENAME,
            'CREDITS': VarCREDITS,
            'IS_ACTIVE': VarISACTIVE,
            'COURSETYPE': VarCOURSETYPE,
            // 'SUBCLASSIFIC_NAME': VarSUBCLASSIFIC_NAME,
            'GRADING_SCHEME_NAME': VarGRADING_SCHEME_NAME
        }
        data.push(alldata);
    });
    console.log(data);//  
    return data;
}

$("#btnSubmit").click(function () {
    var msg = '';
    var str = "";
    var count = 0;

    if ($("#ctl00_ContentPlaceHolder1_ddlCollege").val() == '0')
        msg += "\r Please Select College !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlCurriculum").val() == '0')
        msg += "\r Please Select Curriculum !!!</br>";

    if ($("#ctl00_ContentPlaceHolder1_ddlTransmutation").val() == '0')
        msg += "\r Please Select  Transmutation !!!</br>";

    var checked = $("#BindDynamicSubTransTagTable tbody input:checked").length > 0;

    if (!checked) {
        msg += "\r Please Select at least one Subject to Tag  !!!</br>";
    }

    if (msg != '') {
        iziToast.warning({
            message: msg,
        });
        return false;
    }

    var data = JSON.stringify(BindSubjectTagging());
    $.ajax({
        url: '../Exam/Subject_Transmutation_Tagging.aspx/SubjectTaggingCheckData',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ 'SubjectData': data }),
        beforeSend: function () {
            $("[id*=preloader]").show();
        },
        complete: function () {
            $("[id*=preloader]").hide();
        },
        success: function (response) {
            var credits = response.d[0].CREDITS;
            if (credits == "1") {
                var delconf;
                Swal.fire({
                    title: 'Subject is already tagged in other curriculum. Still want to continue?',
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Yes',
                    delconf: 'Yes'
                }).then((result) => {
                    if (result.isConfirmed) {
                        BindSubjectDataTagging();
            } else {
                $('#selectall').prop('checked', false);
                $("[id*=preloader]").hide();
                return false;
            }
        });
}
else if(credits == "0"){
    Swal.fire({
        html: ' Mark entry has started, but you are not able to tag transmutation.!!!',
        icon: 'error'
    });
    return false;    
}
else{
BindSubjectDataTagging();
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

function BindSubjectDataTagging(){
    var data = JSON.stringify(BindSubjectTagging());
    $.ajax({
        url: '../Exam/Subject_Transmutation_Tagging.aspx/InUpSubjectTagging', 
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8', 
        data: JSON.stringify({ 'SubjectData': data }),
        beforeSend: function () { $("[id*=preloader]").show(); },
        //complete: function () { $("[id*=preloader]").hide(); },
        success: function (response) {
            var credits = response.d[0].CREDITS;
            if (credits == "1") {
                iziToast.success({
                    message: 'Subject Transmutation Tagging Added Successfully !!!',
                });
                BindSubject($("#ctl00_ContentPlaceHolder1_ddlCurriculum").val())
                Onsubmitclear();
               
            }
            else {
                iziToast.success({
                    message: 'Error while inserting data !!!',
                });
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
    $("[id*=preloader]").hide();
}

function collegeChange(dropdown) {
    try {
        $("#btnSubmit").show();
        $("#divshow").hide();
        $("#ctl00_ContentPlaceHolder1_ddlTransmutation").val('0');
        var selectedValue = dropdown.value;
        var selectedText = dropdown.options[dropdown.selectedIndex].innerHTML;
        var Obj = {};
        Obj.CollegeId = selectedValue;

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../Exam/Subject_Transmutation_Tagging.aspx/GetCurriculam",
            data: JSON.stringify(Obj),
            dataType: "json",
            success: function (result) {
                $('#ctl00_ContentPlaceHolder1_ddlCurriculum').empty();
                $('#ctl00_ContentPlaceHolder1_ddlCurriculum').append("<option value='0'>Please Select</option>");
                $.each(result.d, function (key, value) {
                    $("#ctl00_ContentPlaceHolder1_ddlCurriculum").append($("<option></option>").val(value.SCHEMENO).html(value.SCHEMENAME));
                });
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
    catch (ex) {
    }
}


function BoardClassification(dropdown) {
    $("#ctl00_ContentPlaceHolder1_ddlTransmutation").val(0).change();
}

function Curriculam(dropdown) {
    var selectedValue = dropdown.value;
    BindSubject(selectedValue)
} 
function BindSubject(selectedValue)
{
    try { 
        $("#btnSubmit").show();
        // $("#btnLock").hide();
     
        $("#ctl00_ContentPlaceHolder1_ddlSubjectBoardClassification").val('0');
        $("#ctl00_ContentPlaceHolder1_ddlTransmutation").val('0').change();
        var classific = $("select#ctl00_ContentPlaceHolder1_ddlSubjectBoardClassification option:selected").text();
        var Transmutation = $("select#ctl00_ContentPlaceHolder1_ddlTransmutation option:selected").text();
      
        var Obj = {};
        Obj.COLLEGE_ID = $("#ctl00_ContentPlaceHolder1_ddlCollege").val();
        Obj.SCHEMENO = selectedValue;
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../Exam/Subject_Transmutation_Tagging.aspx/GetSubjectTagging",
            data: JSON.stringify(Obj),
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            // complete: function () { $("[id*=preloader]").hide(); },
            success: function (response) {
                if (response.d == '') {
                    Swal.fire({
                        html: 'Record Not Found !!!',
                        icon: 'question'
                    });
                    RemoveTableDynamically('#BindDynamicSubTransTagTable');
                    $("#divshow").hide();
                    $("[id*=preloader]").hide();
                    return false;
                } else {
                    $("#divshow").show();
                    // str = '<thead><tr><th><input type="checkbox" id="selectall"  onClick="checkPage(this)" /><span class="SelectAll">  SelectAll</span></th><th><span class="Semester">Semester</span></th> <th><span class="SubjectCode">Subject Code</span></th> <th><span class="SubjectName">Subject Name</span></th> <th><span class="SubjectType">Type</span></th> <th><span class="Units">Units</span></th><th><span class="Classification">Classification</span></th>   <th><span class="Transmutation">Transmutation</span><th><span class="Createdby">Created/Modify By</span></th><th><span class="CreatedDate">Created/Modify Date</span></th</th><th Style="display:none"></th> <th Style="display:none"></th></tr></thead><tbody>';
                    str = '<thead><tr><th><input type="checkbox" id="selectall"  onClick="checkPage(this)" /><span class="SelectAll">  SelectAll</span></th><th><span class="Semester">Semester</span></th> <th><span class="SubjectCode">Subject Code</span></th> <th><span class="SubjectName">Subject Name</span></th> <th><span class="SubjectType">Type</span></th> <th><span class="Units">Units</span></th><th><span class="Transmutation">Transmutation</span><th><span class="Createdby">Created/Modify By</span></th><th><span class="CreatedDate">Created/Modify Date</span></th</th><th Style="display:none"></th> <th Style="display:none"></th></tr></thead><tbody>';
                    $.each(response.d, function (index, GetValue) {
                        str = str + '<tr class="Trclass">'
                        str = str + '<td>  <input type="checkbox" ' + (GetValue.MARKSTATUS == 2 ? 'disabled' : '') + ' /> </td>'
                        str = str + '<td class="CSEMESTER">' + GetValue.SEMESTER + '</td>'
                        str = str + '<td class="CCODE">' + GetValue.CCODE + '</td>'
                        str = str + '<td class="COURSENAME">' + GetValue.COURSE_NAME + '</td>'
                        str = str + '<td class="COURSETYPE">' + GetValue.SUBNAME + '</td>'
                        str = str + '<td class="CREDITS">' + GetValue.CREDITS + '</td>'
                        //   str = str + '<td class="SubjectBoardClassification">' + GetValue.SUBCLASSIFIC_NAME + '</td>'
                        str = str + '<td class="Transmulation">' + GetValue.GRADING_SCHEME_NAME + '</td>'
                        str = str + '<td>' + GetValue.CREMODIBY + '</td>'
                        str = str + '<td>' + GetValue.CREMODIDATE + '</td>'
                        str = str + '<td class="TAGGING_NO" class="tdHide" Style="display:none"><input type="hidden" id="hdnSubId" value="' + GetValue.SUBJECT_TAGGING_NO + '"/></td>'
                        str = str + '<td class="COURSENO" class="tdHide"  Style="display:none"><input type="hidden" id="hdnCourseId" value="' + GetValue.COURSENO + '"/></td>'
                        str = str + '</tr>'
                    });
                    str = str + '</tbody>';
                    RemoveTableDynamically('#BindDynamicSubTransTagTable');
                    $("#BindDynamicSubTransTagTable").append(str);
                    var BindDynamicTable = $('#BindDynamicSubTransTagTable')
                    commonDatatables(BindDynamicTable);
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
    catch (ex) {
    }
}

$('#btnClear').click(function () {
    ClearSubjecttagging();
});
function ClearSubjecttagging() {
    try { 
        $("#ctl00_ContentPlaceHolder1_ddlCollege").val(0).change();  
        //  $("#ctl00_ContentPlaceHolder1_ddlSubjectBoardClassification").val(0).change();
        $("#ctl00_ContentPlaceHolder1_ddlTransmutation").val(0).change();
        $("#divshow").hide();
        // $("#btnLock").hide(); 
        $('#selectall').prop('checked', false);
    }
    catch (ex) {

    }
}

function Onsubmitclear() {
    try {
     
        $("#ctl00_ContentPlaceHolder1_ddlSubjectBoardClassification").val(0).change();
        $("#ctl00_ContentPlaceHolder1_ddlTransmutation").val(0).change();
         
        $('#selectall').prop('checked', false);
    }
    catch (ex) {

    }
}

//function checkPage(bx) {
//    var cbs = document.getElementsByTagName('input');
//    for (var i = 0; i < cbs.length; i++) {
//        if (cbs[i].type == 'checkbox') {
//            cbs[i].checked = bx.checked;
//        }
//    }
//}

function checkPage(bx) {
    var cbs = document.getElementsByTagName('input');
    for (var i = 0; i < cbs.length; i++) {
        if (cbs[i].type == 'checkbox' && !cbs[i].disabled) {
            cbs[i].checked = bx.checked;
        }
    }
}
