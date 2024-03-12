//===============================================//
// MODULE NAME   : RFC ERP Portal
// PAGE NAME     : Exam Component Weightage
// CREATION DATE : 02-06-2023
// CREATED BY    : Gaurav Kalbande
// Modified BY   : Harshal Bobde 
// Modified Date : 28-06-2023
//===============================================// 
var liveurl = "../../../../";
var localurl = "../";

$(document).ready(function () {
    $("#btnSubmit").hide();
    $('#chkboxDynamic').hide();
    $('#labelsDiv').hide();
    $('#sHead').hide();
    $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").focus();
});

function checkAndSetZero(input) {
    if (input.value.trim() === '') {
        input.value = '0';
    }
}

function moveToNextTextbox(event, input) {
    if (event.key === 'Tab') {
        checkAndSetZero(input);
        var currentCell = input.parentElement;
        var nextCell = currentCell.nextElementSibling;
        if (nextCell) {
            var nextInput = nextCell.querySelector('input');
            if (nextInput) {
                event.preventDefault();
                nextInput.focus();
            }
        } else {
            var currentRow = currentCell.parentElement;
            var nextRow = currentRow.nextElementSibling;
            if (nextRow) {
                var firstInput = nextRow.querySelector('input');
                if (firstInput) {
                    event.preventDefault();
                    firstInput.focus();
                }
            }
        }
    }
}

function fnc100(value, min, max) {
    var msg = ''; var str = ""; var count = 0;
    if (parseInt(value) < 0 || isNaN(value))
        return 0;
    else if (parseInt(value) > 100) {
        msg += "\r Exam Weightages Should not be Above 100 !!! <br/>";
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

$("#btnShow").click(function () {       //Modified By Vinay Mishra on 05/02/2024
    showsubjects();
});

function showsubjects() {           //Modified By Vinay Mishra on 05/02/2024
    try {
            var Patterno = ''; var subjectoffer = '';
            var msg = ''; var str = ""; var count = 0;
            var Scheme = $("#ctl00_ContentPlaceHolder1_ddlCurriculum").val();

            if ($("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val() == '0')
                msg += "\r Please select Academic Session !!!</br>"; 
            if ($("#ctl00_ContentPlaceHolder1_ddlCollege").val() == '0')
                msg += "\r Please select College !!!</br>";
            if (Scheme == "0" || Scheme == "" || Scheme == "ALL" || Scheme == "all")
                msg += "\r Please select Curriculum !!!</br>";

            if (msg != '') {
                iziToast.warning({
                    message: msg,
                });
                return false;
                $("[id*=preloader]").hide();
            }

            var Obj = {};
            //var VarSemester = []; var StrSem = "";
            //VarSemester = $("#ctl00_ContentPlaceHolder1_ddlSemester").val();
            var VarCurriculum = []; var StrCur = "";
            VarCurriculum = $("#ctl00_ContentPlaceHolder1_ddlCurriculum").val();

            //if (VarSemester == "" || VarSemester == "0" || VarSemester == "ALL" || VarSemester == "all") {
            //    StrSem = "0";
            //}
            //else {
            //    for (var i = 0; i < VarSemester.length; i++) {
            //        StrSem += VarSemester[i] + ',';
            //    }
            //    if (VarSemester == "" || VarSemester == "0") {
            //        StrSem = "0";
            //    }
            //    else {
            //        StrSem = StrSem.slice(0, -1);
            //    }
            //}

            if (VarCurriculum == "" || VarCurriculum == "0" || VarCurriculum == "ALL" || VarCurriculum == "all") {
                StrCur = "0";
            }
            else {
                for (var j = 0; j < VarCurriculum.length; j++) {
                    StrCur += VarCurriculum[j] + ',';
                }
                if (VarCurriculum == "" || VarCurriculum == "0") {
                    StrCur = "0";
                }
                else {
                    StrCur = StrCur.slice(0, -1);
                }
            }

            Obj.sessionid = $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val();
            Obj.SchemeId = StrCur;
            Obj.CollegeId = $("#ctl00_ContentPlaceHolder1_ddlCollege").val();
            //Obj.campusId = $("#ctl00_ContentPlaceHolder1_ddlcampus").val();
            //Obj.SemNo = StrSem;
            //Obj.PMO_LEARNINGMODALITYNO = $("#ctl00_ContentPlaceHolder1_ddlModelity").val();
            var ObjPattrn = {};
            ObjPattrn.SchemeId = StrCur;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: liveurl + "Exam/Exam_Component_Weightages.aspx/CheckSubjectOffered",
                data: JSON.stringify(ObjPattrn),
                dataType: "json",
                beforeSend: function () { $("[id*=preloader]").show(); },
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    if (result.d == '') {
                        Swal.fire({
                            html: 'Subjects are not yet offered for the selected academic session, please contact the Registrar for Subject Offering  !!!',
                            icon: 'question'
                        });
                        $("#BindDynamicExamComponentWtTable").hide();
                        $("#btnSubmit").hide();
                        $("[id*=preloader]").hide();
                        return false;
                    }
                    else {
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: liveurl + "Exam/Exam_Component_Weightages.aspx/CheckPattrnBinded",
                            data: JSON.stringify(ObjPattrn),
                            dataType: "json",
                            beforeSend: function () { $("[id*=preloader]").show(); },
                            contentType: "application/json;charset=utf-8",
                            success: function (result) {
                                if (result.d == '') {
                                    Swal.fire({
                                        html: 'Exam pattern is not yet tagged with the curriculum, Please contact the Admin for Exam Pattern Tagging !!!',
                                        icon: 'question'
                                    });
                                    $('#sHead').hide();
                                    $("#BindDynamicExamComponentWtTable").hide();
                                    $("#btnSubmit").hide();
                                    $("[id*=preloader]").hide();
                                    return false;
                                }
                                else {
                                    $.ajax({
                                        type: 'POST',
                                        contentType: 'application/json; charset=utf-8',
                                        dataType: 'json',
                                        url: liveurl + "Exam/Exam_Component_Weightages.aspx/GetDataSet",
                                        data: JSON.stringify(Obj),
                                        beforeSend: function () { $("[id*=preloader]").show(); },
                                        success: function (response) {
                                            if (response.d == '') {
                                                Swal.fire({
                                                    html: 'Record Not Found !!!',
                                                    icon: 'question'
                                                });
                                                $('#sHead').hide();
                                                $("#BindDynamicExamComponentWtTable").hide();
                                                $("#btnSubmit").hide();
                                                $("[id*=preloader]").hide();
                                                return false;
                                            }
                                            else {
                                                populateTable(response.d);
                                                $('#sHead').show();
                                                $("#BindDynamicExamComponentWtTable").show();
                                                $("#btnSubmit").show();
                                                $("[id*=preloader]").hide();
                                            }
                                        },
                                        error: function (xhr, textStatus, errorThrown) {
                                            $("#btnSubmit").hide();
                                            Swal.fire({
                                                html: 'Error Occored  !!!',
                                                icon: 'error'
                                            });
                                            $("[id*=preloader]").hide();
                                            return false;
                                        }
                                    });
                                }
                            },
                            error: function (xhr, textStatus, errorThrown) {
                                Swal.fire({
                                    html: 'Error Occured  !!!',
                                    icon: 'error'
                                });
                                $("[id*=preloader]").hide();
                                return false;
                            }
                        });
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    Swal.fire({
                        html: 'Error occured  !!!',
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

function populateTable(data) {              //Modified By Vinay Mishra on 05/02/2024
    var valno = 0; var lastSixColumns = []; var cellValue;
    var table = $('#BindDynamicExamComponentWtTable');
    var headerRow = $('#headerRow');
    
    data = JSON.parse(data);
    headerRow.empty();
    table.find('tbody').empty();

    $.each(data, function (index, item) {
        var row = $('<tr>');
        if (index === 0) {
            $.each(item, function (key, value) {
                if (key == "Semester" || key == "SCHEMENO" || key == "WEIGHTAGESNO" || key == "ExamNo" || key == "COURSENO" || key == "SEMESTERNO" || key == "EXAM_COMP_WEIGHT_NO" || key == "ALERT")
                {
                    headerRow.append($('<th style="Display:none;">').text(key));
                }
                else
                {
                    if (key == "SubjectType")
                    {
                        headerRow.append($('<th>').text('Subject Type'));
                    }
                    else if (key == "SCHEMENAME")
                    {
                        headerRow.append($('<th>').text('Curriculum'));
                    }
                    else if (key == "Code") {
                        headerRow.append($('<th>').text('Subject Code'));
                    }
                    else if (key == "Name") {
                        headerRow.append($('<th>').text('Subject Name'));
                    }
                    else
                    {
                        headerRow.append($('<th>').text(key));
                    }
                }

                if (key != "SCHEMENAME" && key != "SCHEMENO" && key != "WEIGHTAGESNO" && key != "ExamNo" && key != "COURSENO" && key != "SEMESTERNO" && key != "EXAM_COMP_WEIGHT_NO" && key != "Semester" && key != "SubjectType" && key != "Code" && key != "Name" && key != "ALERT")
                {
                    lastSixColumns.push(key);
                }
            });

            //headerRow.append($('<th>').text("REMARK"));
        }
        table.append(row);
        $.each(item, function (key, value) {
            valno++;
            if (value == "") {
                value = "0";
            }
            if (value == null) {
                value = 0;
            }
            var cell = $('<td>');
            if (key == "SubjectType" || key == "Code" || key == "Name" || key == "SCHEMENAME") {
                row.append($('<td>').text(value))
            }
            else if (key == "Semester" || key == "WEIGHTAGESNO" || key == "ExamNo" || key == "COURSENO" || key == "SCHEMENO" || key == "SEMESTERNO" || key == "EXAM_COMP_WEIGHT_NO") 
            {
                row.append($('<td style="Display:none;">').text(value))
            }
            else if (key == "ALERT")
            {
                row.append($('<td style="Display:none;">').text(value))
                cellValue = value;
            }
            else { 
                if (key != "WEIGHTAGESNO" || key != "ExamNo" || key != "COURSENO" || key != "SCHEMENO" || key != "SCHEMENAME" || key != "SEMESTERNO" || key != "EXAM_COMP_WEIGHT_NO" || key != "Semester" || key != "SubjectType" || key != "Code" || key != "Name" || key != "ALERT") {
                    var ExamName = key.substring(0, key.indexOf("-"));

                    if (cellValue == 0 || cellValue == "0") {
                        var textbox = '<input type="text" id="marks' + valno + '" maxlength="5" oninput="validateNumber(event)" onchange="handleQuantityChange(this)" onkeyup="validateSum(this)" class="form-control ' + ExamName + ' my-textbox" value=' + value + ' /> ';
                    }
                    else {
                        var textbox = '<input type="text" id="marks' + valno + '" maxlength="5" oninput="validateNumber(event)" onchange="handleQuantityChange(this)" onkeyup="validateSum(this)" disabled="true" class="form-control ' + ExamName + ' my-textbox" value=' + value + ' /> ';
                    }
                }

                cell.append(textbox);
                row.append(cell);
            }
        });

        table.append(row);
    });

    $('#chkboxDynamic').show();

    $('.checkmark').on('change', function () {
        if ($(this).prop('checked')) {
            // Create div elements with labels for the last six columns - Added By Vinay Mishra on 05/02/2024
            var labelsDiv = $('#labelsDiv');
            labelsDiv.empty();

            //var divContainer = $('<div style="display: flex; padding: 10px 0;">');

            //for (var i = 0; i < lastSixColumns.length; i++) {
            //    var label = $('<label>').text(lastSixColumns[i]);
            //    var ExamNames = lastSixColumns[i].substring(0, lastSixColumns[i].indexOf("-"));
            //    var classNames = ExamNames.split('-')[0].trim().split(' ').slice(0, 2).join('');
            //    var textbox = '<input type="text" id="label' + (i + 1) + '" maxlength="5" oninput="validateNumber(event)" onchange="handleQuantityChange(this)" onkeyup="validateSum(this)" class="form-control ' + classNames + ' my-textbox" />';
            //    var div = $('<div style="margin-right: 5px;">').addClass('col-xl-2 col-lg-2 col-sm-2 col-2').append($('<div>').addClass('form-group').append(label).append(textbox));
            //    divContainer.append(div);
            //}

            //labelsDiv.append(divContainer);

            labelsDiv.append($('<div>').append($('<h5>').text("Common Weightage Configuration")).addClass('sub-heading'));
            var table = $('<table>').addClass('table table-striped table-bordered nowrap');
            var thead = $('<thead>'); var tbody = $('<tbody>');
            var headerRow = $('<tr>'); var bodyRow = $('<tr>').append();

            for (var i = 0; i < lastSixColumns.length; i++) {
                var ExamNames = lastSixColumns[i].substring(0, lastSixColumns[i].indexOf("-"));
                var classNames = ExamNames.split('-')[0].trim().split(' ').slice(0, 2).join('');

                var label = $('<label>').text(lastSixColumns[i]);
                var textbox = '<input type="text" id="label' + (i + 1) + '" maxlength="5" oninput="validateNumber(event)" onchange="handleQuantityChange(this)" onkeyup="validateSum(this)" class="form-control ' + classNames + ' my-textbox" />';

                headerRow.append($('<th>').append(label));
                bodyRow.append($('<td>').append(textbox));
            }

            thead.append(headerRow); tbody.append(bodyRow);
            table.append(thead).append(tbody);

            $('#labelsDiv').append(table);

            $('#labelsDiv').show();
        } else {
            $('#labelsDiv').hide();
        }
    });
}

function validateNumber(event) {
    var input = event.target.value;
    var regex = /^\d*\.?\d*$/;
    if (!regex.test(input)) {
        event.target.value = '';
    }
}

function handleQuantityChange(input) {
    var value = input.value;
    if (value === '') {
        input.value = '0';
    }
}

function validateSum(textbox) {
    var inputValue = textbox.value.trim();

    var msg = ''; var str = ""; var count = 0;
    var row = textbox.parentNode.parentNode;
    var textboxes = row.getElementsByClassName(textbox.className);
    var sum = 0;
    for (var i = 0; i < textboxes.length; i++) {
        var value = parseFloat(textboxes[i].value);
        if (!isNaN(value)) {
            sum += value;
        }
    }
    if (sum > 100) {
        textbox.value = '0';
        msg += "\r Exam Weightages Should not be Above 100 !!! <br/>";
        if (msg != '') {
            iziToast.warning({
                message: msg,
            });
            return 0;
        }
    }
}

$('#ctl00_ContentPlaceHolder1_ddlAcademicSession').on('change', function (e) {
    $('#sHead').hide();
    $("#BindDynamicExamComponentWtTable").hide();
    $("#btnSubmit").hide();
    $("#hdnWEIGHTAGESNO").val('');
    $('.checkmark').prop('checked', false);
    $('#chkboxDynamic').hide();
    $('#labelsDiv').hide();
    $('#ctl00_ContentPlaceHolder1_ddlSemester').empty();
    $('#ctl00_ContentPlaceHolder1_ddlCurriculum').empty();

    if ($('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val() != "0") {
        try
        {
            var valueSelected = 1;
            $('#sHead').hide();
            $("#BindDynamicExamComponentWtTable").hide();
            $("#btnSubmit").hide();
            var Obj = {};
            Obj.CampusId = valueSelected;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: liveurl + "EXAM/Exam_Component_Weightages.aspx/GetCollege",
                data: JSON.stringify(Obj),
                dataType: "json",
                beforeSend: function () { $("[id*=preloader]").show(); },
                complete: function () { $("[id*=preloader]").hide(); },
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    $('#ctl00_ContentPlaceHolder1_ddlCollege').empty();
                    $('#ctl00_ContentPlaceHolder1_ddlCollege').append("<option value='0'>Please Select</option>");
                    $.each(result.d, function (key, value) {
                        $("[id*=preloader]").show();
                        $("#ctl00_ContentPlaceHolder1_ddlCollege").append($("<option></option>").val(value.COLLEGE_ID).html(value.COLLEGE_NAME));
                    });

                    $('#ctl00_ContentPlaceHolder1_ddlCurriculum').empty().multiselect('rebuild');
                    $("[id*=preloader]").hide();
                    return false;
                },
                error: function ajaxError(result) {
                    Swal.fire({
                        html: 'Error Occurred !!!',
                        icon: 'error'
                    });
                    return false;;
                }
            });
        }
        catch (ex) {
        }
    }
    else {
        $('#ctl00_ContentPlaceHolder1_ddlCollege').empty();
        $('#ctl00_ContentPlaceHolder1_ddlCollege').append("<option value='0'>Please Select</option>");
        $('#ctl00_ContentPlaceHolder1_ddlCurriculum').empty().multiselect('rebuild');
    }
});

$('#ctl00_ContentPlaceHolder1_ddlcampus').on('change', function (e) {
    //$("#ctl00_ContentPlaceHolder1_ddlCollege").val(0).change();
    //$("#ctl00_ContentPlaceHolder1_ddlCurriculum").val(0).change();
    //$("#ctl00_ContentPlaceHolder1_ddlModelity").val(0).change(); 
    $("#ctl00_ContentPlaceHolder1_ddlSemester").empty();
    $('#sHead').hide();
    $("#BindDynamicExamComponentWtTable").hide();
    $('.checkmark').prop('checked', false);
    $('#chkboxDynamic').hide();
    $('#labelsDiv').hide();
    $("#btnSubmit").hide();
    if ($('#ctl00_ContentPlaceHolder1_ddlcampus').val() != "0") {
        try {
            var optionSelected = $("option:selected", this);
            var valueSelected = this.value;
            $('#sHead').hide();
            $("#BindDynamicExamComponentWtTable").hide();
            $("#btnSubmit").hide();
            var Obj = {};
            Obj.CampusId = valueSelected;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: liveurl + "EXAM/Exam_Component_Weightages.aspx/GetCollege",
                data: JSON.stringify(Obj),
                dataType: "json",
                beforeSend: function () { $("[id*=preloader]").show(); },
                complete: function () { $("[id*=preloader]").hide(); },
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    $('#ctl00_ContentPlaceHolder1_ddlCollege').empty();
                    $('#ctl00_ContentPlaceHolder1_ddlCollege').append("<option value='0'>Please Select</option>");
                    $.each(result.d, function (key, value) {
                        $("[id*=preloader]").show();
                        $("#ctl00_ContentPlaceHolder1_ddlCollege").append($("<option></option>").val(value.COLLEGE_ID).html(value.COLLEGE_NAME));
                    });
                    $("[id*=preloader]").hide();
                    return false;
                },
                error: function ajaxError(result) {
                    Swal.fire({
                        html: 'Error Occurred !!!',
                        icon: 'error'
                    });
                    return false;;
                }
            });
        }
        catch (ex) {
        }
    }
    else {
        $('#ctl00_ContentPlaceHolder1_ddlCollege').empty();
        $('#ctl00_ContentPlaceHolder1_ddlCollege').append("<option value='0'>Please Select</option>");
        $('#ctl00_ContentPlaceHolder1_ddlModelity').empty();
        $('#ctl00_ContentPlaceHolder1_ddlModelity').append("<option value='0'>Please Select</option>");
        $('#ctl00_ContentPlaceHolder1_ddlCurriculum').empty();
        $('#ctl00_ContentPlaceHolder1_ddlCurriculum').append("<option value='0'>Please Select</option>");
    }
});

$('#ctl00_ContentPlaceHolder1_ddlCollege').on('change', function (e) {

    //$("#ctl00_ContentPlaceHolder1_ddlCurriculum").val(0).change();
    //$("#ctl00_ContentPlaceHolder1_ddlModelity").val(0).change();
    $("#ctl00_ContentPlaceHolder1_ddlSemester").empty();
    $('.checkmark').prop('checked', false);
    $('#chkboxDynamic').hide();
    $('#labelsDiv').hide();
    $('#sHead').hide();
    $("#BindDynamicExamComponentWtTable").hide();
    $("#btnSubmit").hide();
    if ($('#ctl00_ContentPlaceHolder1_ddlCollege').val() != "0") {
        try {
            var optionSelected = $("option:selected", this);
            var valueSelected = this.value;
            var Obj = {};
            Obj.CollegeId = valueSelected;
           // Obj.modelityid = $("#ctl00_ContentPlaceHolder1_ddlModelity").val();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: liveurl + "EXAM/Exam_Component_Weightages.aspx/GetCurriculam",
                data: JSON.stringify(Obj),
                dataType: "json",
                beforeSend: function () { $("[id*=preloader]").show(); },
                complete: function () { $("[id*=preloader]").hide(); },
                contentType: "application/json;charset=utf-8",
                success: function (result) {
                    //$('#ctl00_ContentPlaceHolder1_ddlCurriculum').empty();
                    //$('#ctl00_ContentPlaceHolder1_ddlCurriculum').append("<option value='0'>Please Select</option>");
                    var ddlCurriculum = $('#ctl00_ContentPlaceHolder1_ddlCurriculum');
                    ddlCurriculum.empty();

                    $.each(result.d, function (key, value) {
                        ddlCurriculum.append($("<option></option>").val(value.SCHEMENO).html(value.SCHEMENAME));
                    });

                    // Refresh the ListBox to apply changes
                    $('#ctl00_ContentPlaceHolder1_ddlCurriculum').multiselect('rebuild');

                    $("[id*=preloader]").hide();
                    return false;
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
});

function BindSemester(dropdown) {
    try {
        $("#ctl00_ContentPlaceHolder1_ddlSemester").empty();
        $('#sHead').hide();
        $("#BindDynamicExamComponentWtTable").hide();
        $("#btnSubmit").hide();
        $('.checkmark').prop('checked', false);
        $('#chkboxDynamic').hide();
        $('#labelsDiv').hide();

        //var selectedValue = dropdown.value;
        //var selectedText = dropdown.options[dropdown.selectedIndex].innerHTML;
        //var Obj = {};
        //Obj.SchemeId = selectedValue;
        //$.ajax({
        //    type: "POST",
        //    contentType: "application/json; charset=utf-8",
        //    url: liveurl + "Exam/Exam_Component_Weightages.aspx/GetSemester",
        //    data: JSON.stringify(Obj),
        //    dataType: "json",
        //    beforeSend: function () { $("[id*=preloader]").show(); },
        //    complete: function () { $("[id*=preloader]").hide(); },
        //    contentType: "application/json;charset=utf-8",
        //    success: function (result) {
        //        if (result.d == '') {
        //            Swal.fire({
        //                html: 'Semester not found  for selected ' + selectedText + ' Curriculum !!!',
        //                icon: 'question'
        //            });
        //            $('#ctl00_ContentPlaceHolder1_ddlSemester').empty();
        //            //$('#ctl00_ContentPlaceHolder1_ddlSemester').append("<option value='all'>ALL</option>");
        //            $("[id*=preloader]").hide();
        //            return false;
        //        } else {
        //            $('#ctl00_ContentPlaceHolder1_ddlSemester').empty();
        //            //$('#ctl00_ContentPlaceHolder1_ddlSemester').append("<option value='all'>ALL</option>");
        //            $.each(result.d, function (key, value) {
        //                $("#ctl00_ContentPlaceHolder1_ddlSemester").append($("<option></option>").val(value.SEMESTERNO).html(value.SEMESTERNAME));
        //            });
        //        }
        //    },
        //    error: function ajaxError(result) {
        //        Swal.fire({
        //            html: 'Error Occurred !!!',
        //            icon: 'error'
        //        });
        //    }
        //});
    }
    catch (ex) {
    }
}

function BindExamCompnent() {
    debugger
    var data = [];
    var valno = 12;
    var VarSession = $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val();
    var VarCollege = $("#ctl00_ContentPlaceHolder1_ddlCollege").val();
    var VarCurriculum = []; //var strCurriculum = "";
    VarCurriculum = $('#ctl00_ContentPlaceHolder1_ddlCurriculum').val();
    var VarModality = 0;
    var VarISACTIVE = 1;

    $('#BindDynamicExamComponentWtTable tbody tr').each(function () {
        var row = $(this).closest("tr")[0];
        var checkDisable = row.cells[11].innerHTML;

        if (checkDisable == 0 || checkDisable == "0") {
            var EEXAMNO = row.cells[0].innerHTML;
            var EXAM_WEIGHTAGESNO = "";
            EXAM_WEIGHTAGESNO = row.cells[1].innerHTML;
            if (EXAM_WEIGHTAGESNO == "") {
                EXAM_WEIGHTAGESNO = "0";
            }
            var VarEXAM_COMP_WEIGHT_NO = row.cells[9].innerHTML;
            //var VarSemster = row.cells[7].innerHTML;
            var VarSchemeNo = row.cells[10].innerHTML;
            var VarCourseNo = row.cells[7].innerHTML;
            var colCount = $("#BindDynamicExamComponentWtTable tr th").length;
            var VarWeightage = "";
            for (i = 0; i < colCount - 12; i++) {
                valno++;
                if ($('.checkmark').prop('checked')) {
                    VarWeightage += $('#label' + (i + 1) + '').val() + ',';
                    VarModality = 1;
                } else {
                    VarWeightage += $(this).find('#marks' + valno + '').val() + ',';
                }
            }
            valno = valno + 12;
            VarWeightage = VarWeightage.slice(0, -1);
            if (VarEXAM_COMP_WEIGHT_NO == "-")
                VarEXAM_COMP_WEIGHT_NO = 0;
            var alldata = {
                'WEIGHTAGESNOString': EXAM_WEIGHTAGESNO,
                'SESSIONNO': VarSession,
                'COLLEGE_ID': VarCollege,
                //'CAMPUSNO': VarCampus,
                'SCHEMENO': VarSchemeNo,
                'COURSENO': VarCourseNo,
                //'SEMESTERNO': VarSemster,
                'EXAMNOstring': EEXAMNO,
                'WEIGHTAGEstring': VarWeightage,
                'PMO_LEARNINGMODALITYNO': VarModality
            }
            data.push(alldata);
        }
        else {
            valno = valno + 18;
        }
    });
    return data;
}

$("#btnSubmit").click(function () {         //Modified By Vinay Mishra on 05/02/2024
    try {
        debugger
        var msg = ''; var str = ""; var count = 0; var curr = $("#ctl00_ContentPlaceHolder1_ddlCurriculum").val();
        
        if ($("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val() == '0')
            msg += "\r Please select Academic Session !!!</br>";

        if ($("#ctl00_ContentPlaceHolder1_ddlCollege").val() == '0')
            msg += "\r Please select College !!!</br>";

        if (curr == "" || curr == "0" || curr == "ALL" || curr == "all")
            msg += "\r Please select Curriculum !!!</br>";

        if (msg != '') {
            iziToast.warning({
                message: msg,
            });
            return false;
        }

        if ($('.checkmark').prop('checked')) {
            $('#labelsDiv').each(function () {      //Modified By Vinay Mishra on 05/02/2024
                var sums = {}; // Object to store sums for different class names

                $(this).find("[class^='form-control']").each(function () {
                    var classNames = $(this).attr('class').split(/\s+/);
                    var value = parseFloat($(this).val()) || 0;

                    classNames.forEach(function (classNames) {
                        if (classNames !== 'form-control' && classNames !== 'my-textbox') {
                            if (sums[classNames]) {
                                sums[classNames] += value;
                            } else {
                                sums[classNames] = value;
                            }
                        }
                    });
                });

                var allSumsAreZeroOrHundred = true;
                for (var classNames in sums) {
                    if (sums[classNames] !== 0 && sums[classNames] !== 100) {
                        allSumsAreZeroOrHundred = false;
                        break;
                    }
                }

                if (allSumsAreZeroOrHundred) {
                    $(this).css('background-color', 'white');
                } else {
                    $(this).css('background-color', '#ffefea'); // Apply red background for rows with sums other than 0 or 100
                    str += "\r Kindly Check Exam Component Weightages should be 100  !!!</br>";
                    if (str != '') {
                        iziToast.warning({
                            message: str,
                        });
                        return false;
                    }
                }
            });
        } else {
            $('#BindDynamicExamComponentWtTable tbody tr').each(function () {      //Modified By Vinay Mishra on 05/02/2024
                var sums = {}; // Object to store sums for different class names
                var row = $(this).closest("tr")[0];
                var checkDisable = row.cells[11].innerHTML;

                if (checkDisable == 0 || checkDisable == "0") {
                    $(this).find("[class^='form-control']").each(function () {
                        var classNames = $(this).attr('class').split(/\s+/);
                        var value = parseFloat($(this).val()) || 0;

                        classNames.forEach(function (classNames) {
                            if (classNames !== 'form-control' && classNames !== 'my-textbox') {
                                if (sums[classNames]) {
                                    sums[classNames] += value;
                                } else {
                                    sums[classNames] = value;
                                }
                            }
                        });
                    });

                    var allSumsAreZeroOrHundred = true;
                    for (var classNames in sums) {
                        if (sums[classNames] !== 0 && sums[classNames] !== 100) {
                            allSumsAreZeroOrHundred = false;
                            break;
                        }
                    }

                    if (allSumsAreZeroOrHundred) {
                        $(this).css('background-color', 'white');
                    } else {
                        $(this).css('background-color', '#ffefea'); // Apply red background for rows with sums other than 0 or 100
                        str += "\r Kindly Check Exam Component Weightages should be 100  !!!</br>";
                        if (str != '') {
                            iziToast.warning({
                                message: str,
                            });
                            return false;
                        }
                    }
                }
            });
        }

        if (str == '') {
            var data = JSON.stringify(BindExamCompnent());
            $.ajax({
                url: liveurl + "Exam/Exam_Component_Weightages.aspx/InsUpdExamComponentWeightages",
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({ 'ExamComwightages': data }),
                beforeSend: function () { $("[id*=preloader]").show(); },
                complete: function () { $("[id*=preloader]").hide(); },
                contentType: "application/json;charset=utf-8",
                success: function (response) {
                    //var responseData = JSON.parse(response.d);

                    var colCount = $("#BindDynamicExamComponentWtTable tr th").length;
                    for (i = 0; i < colCount - 12; i++) {
                        $('#label' + (i + 1) + '').val("");
                    }

                    showsubjects();

                    if (response.d == "1") {
                        iziToast.success({
                            message: 'Exam Component Weightages are Added Successfully !!!',
                        });

                        $('.checkmark').prop('checked', false);
                        $('#labelsDiv').hide();
                    }
                    else if (response.d == "2") {
                        iziToast.success({
                            message: 'Exam Component Weightages are Updated Successfully !!!',
                        });

                        $('.checkmark').prop('checked', false);
                        $('#labelsDiv').hide();
                    }
                    else {
                        iziToast.success({
                            message: 'Error While Inserting Data !!!',
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
        }
    }
    catch (ex) {
    }
});

$("#btnClear").click(function () {
    cleardata();
});

function cleardata() {
    $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val(0).change();
    $("#ctl00_ContentPlaceHolder1_ddlCollege").val(0).change();
    //$("#ctl00_ContentPlaceHolder1_ddlcampus").val(0).change();
    $('#ctl00_ContentPlaceHolder1_ddlCurriculum').empty();
    //$('#ctl00_ContentPlaceHolder1_ddlCurriculum').append("<option value='0'>Please Select</option>");
    //$('#ctl00_ContentPlaceHolder1_ddlModelity').empty();
    //$('#ctl00_ContentPlaceHolder1_ddlModelity').append("<option value='0'>Please Select</option>");
    $("#ctl00_ContentPlaceHolder1_ddlSemester").empty();
    $('#sHead').hide();
    $("#BindDynamicExamComponentWtTable").hide();
    $("#btnSubmit").hide();
    $("#hdnWEIGHTAGESNO").val('');
    $('.checkmark').prop('checked', false);
    $('#chkboxDynamic').hide();
    $('#labelsDiv').hide();
}
