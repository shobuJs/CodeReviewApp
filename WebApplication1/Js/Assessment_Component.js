
//===============================================//
// MODULE NAME   : RFC ERP Portal
// PAGE NAME     : Exam Components
// CREATION DATE : 30-09-2023
// CREATED BY    : Ekansh Moundekar
// Modified BY   : 
// Modified Date : 30-01-2024
//===============================================//
var liveurl = "../../../../";
var localurl = "../../../";
var LockNo;
var  CheckWeigh = 0;
$(document).ready(function () {
    //$("#btnLock").hide();
    $('#divcomponent').hide();
    $('#divweightages').hide();
    $("#btnSubmit").hide();
});

$('#ctl00_ContentPlaceHolder1_ddlAcademicSession').on('change', function (e) {
    $("#ctl00_ContentPlaceHolder1_ddlSection").val('');
    $("#ctl00_ContentPlaceHolder1_ddlSubject").val('');
    $('#divwieghtage').empty();
    $('#BindDynamicAssessmentComponentTable').empty();
    $('#divasstable').hide(); 
    $("#btnSubmit").val('Submit');
    $("#btnSubmit").hide();
    //$("#btnLock").hide();
    $('#divcomponent').hide();
    $('#divweightages').hide();
    $('#ctl00_ContentPlaceHolder1_ddlSection').empty();
    $('#ctl00_ContentPlaceHolder1_ddlSection').append("<option value='0'>Please Select</option>");
    $('#ctl00_ContentPlaceHolder1_ddlSubject').empty();
    $('#ctl00_ContentPlaceHolder1_ddlSubject').append("<option value='0'>Please Select</option>");
    if($('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val()!=0){
        try {   
            var optionSelected = $("option:selected", this);
            var valueSelected = this.value;
            var Obj = {};
            Obj.SessionNo = valueSelected;
            Obj.UANO = valueSelected;
            Obj.Type = "COURSEDROP";
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: localurl + "EXAM/Assessment_Components.aspx/GetCourse",
                data: JSON.stringify(Obj),
                dataType: "json",
                success: function (result) {
                    if(result.d ==''){
                        Swal.fire({
                            html: 'Subjects are not found !!!',
                            icon: 'question'
                        });
                        $("#ctl00_ContentPlaceHolder1_ddlSection").val('');
                        $("#ctl00_ContentPlaceHolder1_ddlSubject").val('');
                        $('#divwieghtage').empty();
                        $('#BindDynamicAssessmentComponentTable').empty();
                        $('#divasstable').hide(); 
                        $("#btnSubmit").val('Submit');
                        $("#btnSubmit").hide();
                        //$("#btnLock").hide();
                        $('#divcomponent').hide();
                        $('#divweightages').hide();
                        $('#ctl00_ContentPlaceHolder1_ddlSection').empty();
                        $('#ctl00_ContentPlaceHolder1_ddlSection').append("<option value='0'>Please Select</option>");
                        $('#ctl00_ContentPlaceHolder1_ddlSubject').empty();
                        $('#ctl00_ContentPlaceHolder1_ddlSubject').append("<option value='0'>Please Select</option>");
                        return false;
                    }
                    else{
                        $("[id*=preloader]").show();
                        $('#ctl00_ContentPlaceHolder1_ddlSubject').empty();
                        $('#ctl00_ContentPlaceHolder1_ddlSubject').append("<option value='0'>Please Select</option>");
                        $.each(result.d, function (key, value) { 
                            $("#ctl00_ContentPlaceHolder1_ddlSubject").append($("<option></option>").val(value.CAMPUSNOO).html(value.COURSE_NAME));
                        });

                    }
                    $("[id*=preloader]").hide();
                },
                error: function ajaxError(result) {
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
});

$('#ctl00_ContentPlaceHolder1_ddlSubject').on('change', function (e) {
    try
    {
        $('#divwieghtage').empty();
        $('#BindDynamicAssessmentComponentTable').empty();
        $('#divcomponent').hide();
        $('#divweightages').hide();
        $('#ctl00_ContentPlaceHolder1_ddlSection').empty();
        $('#ctl00_ContentPlaceHolder1_ddlSection').append("<option value='0'>Please Select</option>");
        var optionSelected = $("option:selected", this);
        var valueSelected = this.value;
        var CourseNo = $('#ctl00_ContentPlaceHolder1_ddlSubject').val();
        if (CourseNo > 0) {
            var Obj = {};
            Obj.SessionNo = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
            Obj.COURSENO = CourseNo;
            Obj.Type = "SECTIONDROP";
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: localurl + "EXAM/Assessment_Components.aspx/GetSection",
                data: JSON.stringify(Obj),
                dataType: "json",
                beforeSend: function () { $("[id*=preloader]").show(); },
                success: function (response) {
                    if (response.d == '') {
                        $("[id*=preloader]").hide();
                        $("#ctl00_ContentPlaceHolder1_ddlSection").val('');
                        $('#divwieghtage').empty();
                        $('#BindDynamicAssessmentComponentTable').empty();
                        $('#divasstable').hide(); 
                        $("#btnSubmit").val('Submit');
                        $("#btnSubmit").hide();
                        //$("#btnLock").hide();
                        $('#divcomponent').hide();
                        $('#divweightages').hide();
                        $('#ctl00_ContentPlaceHolder1_ddlSection').empty();
                        $('#ctl00_ContentPlaceHolder1_ddlSection').append("<option value='0'>Please Select</option>");
                        return false;
                    }
                    else {
                        $('#divcomponent').hide();
                        //$("#btnLock").hide();
                        $('#ctl00_ContentPlaceHolder1_ddlSection').empty();
                        $('#ctl00_ContentPlaceHolder1_ddlSection').append("<option value='0'>Please Select</option>");
                        $.each(response.d, function (key, value) { 
                            $("#ctl00_ContentPlaceHolder1_ddlSection").append($("<option></option>").val(value.SectionNO).html(value.SectionName));
                        });
                    }
                },
            });
            $("[id*=preloader]").hide();
        }

        if (CourseNo > 0) {
            var Obj = {};
            Obj.SessionNo = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
            Obj.COURSENO = CourseNo;
            Obj.SECTIONNO = 0;
            Obj.Type = "WEIGHTAGES";
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: localurl + "EXAM/Assessment_Components.aspx/GetWEIGHTAGES",
                data: JSON.stringify(Obj),
                dataType: "json",
                beforeSend: function () { $("[id*=preloader]").show(); },
                complete: function () { $("[id*=preloader]").hide(); },
                success: function (response) {
                    if (response.d == '') {
                        Swal.fire({
                            html: 'Exam component weightages are not defined !!!',
                            icon: 'question'
                        });
                        $("#btnSubmit").val('Submit');
                        //$("#btnLock").hide();
                        $('#divweightages').hide();
                        CheckWeigh = 0;
                        return false;
                    }
                    else {
                        var uniqueExamCompNos = [];
                        var hasDuplicates = false;
                        var CourseName = '';
                        $.each(response.d, function (index, Value) {
                            if (uniqueExamCompNos.includes(Value.EXAMCOMNO)) {
                                hasDuplicates = true;
                                CourseName = Value.CourseName;
                                return false; 
                            }
                            uniqueExamCompNos.push(Value.EXAMCOMNO);
                        });
                        if (hasDuplicates) {
                            Swal.fire({
                                html: 'Multiple exam component weightages are found for this course !!! '+" ("+ CourseName +") "+'Please contact the administrator.',
                                icon: 'warning'
                            });
                            CheckWeigh = 1;
                        } else {
                            CheckWeigh = 0;
                            $.each(response.d, function (index, GetValue){
                                //$("#btnLock").hide();
                                $("#btnSubmit").hide();
                                $('#divwieghtage').show();
                                $('#divcomponent').hide();
                                $('#divasstable').show();
                                $('#divweightages').show();
                                $('#divwieghtage').append($('<div class="col-xl-6 col-lg-6 col-sm-6 col-6"><div class="form-group"> <div class="label-dynamic"> <sup></sup> <label>' + GetValue.EXAMNAME + ' (%)</label> </div> <input   Class="form-control"  value="' + GetValue.MARKS + '"   disabled   TabIndex="0" placeholder="60"/> </div> </div>'));
                            });
                        }
                    }
                },
                error: function ajaxError(response) {
                    Swal.fire({
                        html: 'Error Ocured !!!',
                        icon: 'error'
                    });
                    return false;
                }
            });
        }
    }
    catch(ex)
    {
    }
});

$('#ctl00_ContentPlaceHolder1_ddlSection').on('change', function () {
    if(CheckWeigh !== 1){
        GetTotalMarksData();
    }
});
function GetTotalMarksData(){
    try {
        var Objtotal = {};
        var CourseNo = $('#ctl00_ContentPlaceHolder1_ddlSubject').val();
        var Sectionno = $('#ctl00_ContentPlaceHolder1_ddlSection').val();
        if(Sectionno > 0){
            Objtotal.SessionNo = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
            Objtotal.SectionNo = Sectionno;
            Objtotal.COURSENO = CourseNo;
            Objtotal.Type = "TOTALMARK";
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: localurl + "EXAM/Assessment_Components.aspx/GetTotalMark",
                data: JSON.stringify(Objtotal),
                dataType: "json",
                beforeSend: function () { $("[id*=preloader]").show(); },
                complete: function () { $("[id*=preloader]").hide(); },
                success: function (response) {
                    if (response.d == '') {
                        $("[id*=preloader]").hide();
                        Swal.fire({
                            html: 'Record Not Found !!!',
                            icon: 'question'
                        });
                        $('#divcomponent').show();
                        $("#btnSubmit").val('Submit');
                        $("#btnSubmit").hide();
                        //$("#btnLock").hide();
                        CheckWeigh = 0;
                        return false;
                    }
                    else {
                        var allCheckboxesDisabled = true;
                        $("#btnSubmit").show();
                        $('#divasstable').show();
                        $("#btnSubmit").val('Submit').prop('disabled', false);
                        $('#divcomponent').show();
                        //$("#btnLock").hide();
                        $("[id*=preloader]").show();
                        str = '<thead> <tr><th><span></span></th><th><span class="Marks">Total Marks </span></th></tr></thead><tbody>';
                        $.each(response.d, function (index, GetValue) {
                            var ExamName = GetValue.EXAMNAME.substring(0, GetValue.EXAMNAME.indexOf("-"));
                            str = str + '<tr class="assesssclass ' + ExamName + ' ">'
                            str = str + '<td><label  id="lblexamname">' + GetValue.EXAMNAME + '</label><input type="hidden" id="hdnexname" value="' + GetValue.EXAMNAME + '"/><input type="hidden" id="hdnCompno" value="' + GetValue.ASSESS_COMP_NO + '"/><input type="hidden" id="hdnexamno" value="' + GetValue.EXAMCOMNO + '"/><input type="hidden" id="hdnexcomwei" value="' + GetValue.EXAM_COMP_WEIGHT_NO + '"/></td>'
                            if (GetValue.ID ==3) { 
                                if (GetValue.checkcourse==0)
                                {
                                    str = str + '<td><input type="text" id="txtmarks' + index + '" maxlength="6"   oninput="validateNumber(event)"  onblur="checkValue(this)"  class="form-control ' + ExamName + '" value=' + GetValue.MARKS + ' placeholder="eg.100"/></td>'
                                    allCheckboxesDisabled = false;
                                }
                                else
                                {      str = str + '<td><input type="text" id="txtmarks' + index + '" maxlength="6"   oninput="validateNumber(event)"  onblur="checkValue(this)"  class="form-control ' + ExamName + '" value=' + GetValue.MARKS + ' placeholder="eg.100" disabled/></td>'
                                      
                                }
                            }
                            else
                            {  
                                if (GetValue.checkcourse==0)
                                {   
                                    str = str + '<td><input type="text" id="txtmarks' + index + '" maxlength="6"  oninput="validateNumber(event)"   onkeyup="validateSum(this)"   class="form-control ' + ExamName + '" value=' + GetValue.MARKS + ' placeholder="eg.100"/></td>'
                                    allCheckboxesDisabled = false;
                                }
                            else
                            {   str = str + '<td><input type="text" id="txtmarks' + index + '" maxlength="6"  oninput="validateNumber(event)"   onkeyup="validateSum(this)"   class="form-control ' + ExamName + '" value=' + GetValue.MARKS + ' placeholder="eg.100" disabled/></td>'
                  
                            }
                            }

                           
                            str = str + '</tr>'
                            //if(GetValue.LockID == 0){
                            //    if (GetValue.ASSESS_COMP_NO == 0) {
                            //        $("#btnSubmit").val('Submit').show();
                            //    }
                            //    else{
                            //        $("#btnSubmit").val('Update').show();
                            //        $("#btnSubmit").val('Update').prop('disabled', false);
                            //        //$("#btnLock").show().prop('disabled', false);
                            //    }
                            //}
                            //else{
                            //    $("#btnSubmit").val('Update').prop('disabled', true);
                            //    //$("#btnLock").show().prop('disabled', true);
                            //}
                            if (allCheckboxesDisabled) {
                                $("#btnSubmit").hide();
                            } else {
                                $("#btnSubmit").show();
                                $("#btnSubmit").val('Update').prop('disabled', false);
                            }
                            LockNo = GetValue.LockID;
                        });
                        str = str + '</tbody>';
                        RemoveTableDynamically('#BindDynamicAssessmentComponentTable');
                        $("#BindDynamicAssessmentComponentTable").append(str);
                        $("[id*=preloader]").hide();
                    }
                    //if (LockNo === 1) {
                    //    $('#BindDynamicAssessmentComponentTable').prop('disabled', true);
                    //    $('#divasstable').show();
                    //    $("#btnSubmit").val('Submit').prop('disabled', true);
                    //    //$("#btnLock").show().prop('disabled', true);
                    //    $("#txtmarks0").prop('disabled', true);
                    //    $("#txtmarks1").prop('disabled', true);
                    //    $("#txtmarks2").prop('disabled', true);
                    //    $("#txtmarks3").prop('disabled', true);
                    //    $("#txtmarks4").prop('disabled', true);
                    //    $("#txtmarks5").prop('disabled', true);
                    //    $("#btnSubmit").hide();
                    //    //$("#btnLock").hide();
                    //}
                },
                error: function ajaxError(response) {
                    Swal.fire({
                        html: 'Error Occured !!!',
                        icon: 'error'
                    });
                    return false;
                }
            });
        }
        $("[id*=preloader]").hide();
    }
    catch (ex) {
    }
}

function checkValue(currentInput) { 
    var previousValue = currentInput.value.trim(); 
    var msg = ''; var str = ""; var count = 0;
    if (previousValue !== '100') {
        msg += "\r The total marks value should be 100 !!! <br/>";
        if (msg != '') {
            iziToast.warning({
                message: msg,
            });
            currentInput.value ='0';
            return 0;
        }
    }
}

function validateNumber(event) {
    var input = event.target.value;
    var regex = /^\d*\.?\d*$/; 

    if (!regex.test(input)) {
        event.target.value = ''; 
    }
}
function validateSum(textbox) {
    var msg = ''; var str = ""; var count = 0;
    var row = textbox.parentNode.parentNode; // Get the table row
    var textboxes = row.getElementsByClassName(textbox.className); 
    var sum = 0;
    for (var i = 0; i < textboxes.length; i++) {
        var value = parseFloat(textboxes[i].value);
        if (!isNaN(value)) {
            sum += value;
        }
    }
    if (sum > 1000000) {
        textbox.value = '0';
        msg += "\r The total marks are not greater than 100. !!! <br/>";
        if (msg != '') {
            iziToast.warning({
                message: msg,
            });
            return 0;
        }
    }
} 
 
function BindExamAss() {
    try
    {
        var CourseNo = $('#ctl00_ContentPlaceHolder1_ddlSubject').val();
        var data = [];
        var VarSession = $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val();
        var Varsubject = CourseNo;
        var SectionNo = $('#ctl00_ContentPlaceHolder1_ddlSection').val();
        var id = 0;
        $('#BindDynamicAssessmentComponentTable .assesssclass').each(function () {
            var row = $(this).closest("tr")[0];
            var VarCompId = $(this).find('#hdnCompno').val();
            var VarExamNo = $(this).find('#hdnexamno').val();
            var VarMarks = $(this).find('#txtmarks'+id+'').val();
            var VarCompName = $(this).find('#hdnexname').val();
            var VarExamComWeight = $(this).find('#hdnexcomwei').val();
            var varActive = 1;
            var alldata = {
                'ASSESS_COMP_NO': VarCompId,
                'EXAM_COMP_WEIGHT_NO': VarExamComWeight,
                'SESSIONNO': VarSession,
                'COURSENO': Varsubject,
                'EXAMCOMNO': VarExamNo,
                'ASSESS_COMP_NAME': VarCompName,
                'SECTIONNO':SectionNo,
                'COLUMN2': "",
                'MARKS': VarMarks,
                'ACTIVE': varActive,
                'SCHEMENO':0
            }
            data.push(alldata);
            id++;
        });
      
        return data;
    }
    catch (ex) { 
    }
}

$("#btnSubmit").click(function () {
    InsUdtAssessmentComponent();
});

function InsUdtAssessmentComponent(){
    try {
        var msg = ''; var str = ""; var count = 0; var str1 = "";
        if ($("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val() == '0')
            msg += "\r Please Select  Academic Session !!!</br>";
        if ($("#ctl00_ContentPlaceHolder1_ddlSubject").val() == '0')
            msg += "\r Please Select Subject !!!</br>";
        if ($("#ctl00_ContentPlaceHolder1_ddlSection").val() == '0')
            msg += "\r Please Select Section !!!</br>";
        $('#BindDynamicAssessmentComponentTable td input[type="text"]').each(function () {
            var decimalPattern =  /^\d+$/;
            var $textbox = $(this);
            if ($textbox.val() === '' )  
            {
                $textbox.focus(); 
                str += "\r Please enter total marks !!!</br>";
                return false;  
            }

            if(!decimalPattern.test($textbox.val())){
                $textbox.focus(); 
                str1 += "\r Decimal values are not allowed !!!</br>";
                return false; 
            }
        });
        if (str != '') {
            iziToast.warning({
                message: str,
            });
            return false;
        }
        if (str1 != '') {
            iziToast.warning({
                message: str1,
            });
            return false;
        }
        if (msg != '' ) {
            iziToast.warning({
                message: msg,
            });
            return false;
        }
        //if(LockNo == 0){
            var AW = JSON.stringify(BindExamAss());
        
            $.ajax({
                url: localurl + 'Exam/Assessment_Components.aspx/InUpExamAssWeightage',
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({ 'AssWaigtage': AW }),
                beforeSend: function () { $("[id*=preloader]").show(); },
                complete: function () { $("[id*=preloader]").hide(); },
                contentType: "application/json;charset=utf-8",
                success: function (response) {
                    if (response.d == "1") {
                        if ($("#btnSubmit").val() == "Submit") {

                            iziToast.success({
                                message: 'Assessment Component Marks Added Successfully !!!',
                            });
                        }
                        else {
                            iziToast.success({
                                message: 'Assessment Component Marks Update Successfully !!!',
                            });
                        }
                        GetTotalMarksData();
                        //$("#btnLock").show();
                        //$("#btnLock").show().prop('disabled', false);
                        $("#btnSubmit").val('Update').show();
                        //cleardata();
                    }
                    else {
                        iziToast.success({
                            message: 'Error while inserting data !!!',
                        });
                        cleardata();
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
        //}
        //else{
        //    Swal.fire({
        //        html: 'Error Occurred..! Try Again',
        //        icon: 'error'
        //    });
        //    return false;
        //}
    }
    
    catch (ex) {
    }
    
}

$('#btnCancel').click(function () {
    cleardata();
});
function cleardata() {
    CheckWeigh = 0;
    $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val(0).change();
    $("#ctl00_ContentPlaceHolder1_ddlSection").val('');
    $("#ctl00_ContentPlaceHolder1_ddlSubject").val('');
    $('#divwieghtage').empty();
    $('#BindDynamicAssessmentComponentTable').empty();
    $('#divasstable').hide(); 
    $("#btnSubmit").val('Submit');
    $("#btnSubmit").hide();
    $("#btnLock").hide();
    $('#divcomponent').hide();
    $('#divweightages').hide();
    $('#ctl00_ContentPlaceHolder1_ddlSection').empty();
    $('#ctl00_ContentPlaceHolder1_ddlSection').append("<option value='0'>Please Select</option>");
    $('#ctl00_ContentPlaceHolder1_ddlSubject').empty();
    $('#ctl00_ContentPlaceHolder1_ddlSubject').append("<option value='0'>Please Select</option>");
}

$("#btnLock").click(function () {
    AssessmentLock();
})
function AssessmentLock() {
    var msg = ''; var str = ""; var count = 0;var str1 = "";
    if ($("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val() == '0')
        msg += "\r Please Select  Academic Session !!!</br>";
    if ($("#ctl00_ContentPlaceHolder1_ddlSubject").val() == '0')
        msg += "\r Please Select Subject !!!</br>";
    if ($("#ctl00_ContentPlaceHolder1_ddlSection").val() == '0')
        msg += "\r Please Select Section !!!</br>";
    $('#BindDynamicAssessmentComponentTable td input[type="text"]').each(function () {
        var decimalPattern =  /^\d+$/;
        var $textbox = $(this);
        if ($textbox.val() === '' )
           
        {
            $textbox.focus();
            str += "\r Please enter total marks !!!</br>";
            return false;
        }

        if(!decimalPattern.test($textbox.val())){
            $textbox.focus(); 
            str1 += "\r Decimal values are not allowed !!!</br>";
            return false; 
        }
    });
    if (str != '') {
        iziToast.warning({
            message: str,
        });
        return false;
    }

    if (str1 != '') {
        iziToast.warning({
            message: str1,
        });
        return false;
    }

    if (msg != '') {
        iziToast.warning({
            message: msg,
        });
        return false;
    }
    try {
        var delconf; 
        Swal.fire({
            title: 'Are you sure you want to lock assessment component marks ?', 
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            textsize:'10px',
            confirmButtonText: 'Yes',
            delconf:'Yes'
        }).then((result) => { 
            if (result.isConfirmed) { 

            var AW = JSON.stringify(BindExamAss());
        $.ajax({
            url: localurl + 'Exam/Assessment_Components.aspx/InUpExamAssWeightage',
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ 'AssWaigtage': AW }),
            beforeSend: function () { $("[id*=preloader]").show(); },
            complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                if (response.d == "1") {
                    if ($("#btnSubmit").val() == "Submit") {
                    }
                    else {
                    }
                    GetTotalMarksData();
                    $("#btnLock").show();
                    $("#btnLock").show().prop('disabled', false);
                    $("#btnSubmit").val('Update').show();
                    //cleardata();

                }
                else {
                    iziToast.success({
                        message: 'Error while inserting data !!!',
                    });
                    cleardata();
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
        //assesssment lock code
        var Obj = {};
        Obj.CourseNo = $('#ctl00_ContentPlaceHolder1_ddlSubject').val();
        Obj.VarSession = $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val();
        Obj.SectionNO = $('#ctl00_ContentPlaceHolder1_ddlSection').val();
        $.ajax({
            url: localurl + 'Exam/Assessment_Components.aspx/AssessmentLock',
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(Obj),
            beforeSend: function () { $("[id*=preloader]").show(); },
            complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                if (response.d !== '') {
                    Swal.fire({
                        html: "Locked Successfully!",
                        icon: 'success'
                    });
                    CheckWeigh = 0;
                    $("#ctl00_ContentPlaceHolder1_ddlAcademicSession").val(0).change();
                    $('#BindDynamicAssessmentComponentTable').prop('disabled', true);
                    $('#divasstable').show();
                    $("#btnSubmit").val('Submit').prop('disabled', true);
                    //$("#btnLock").show().prop('disabled', true);
                    $("#txtmarks0").prop('disabled', true);
                    $("#txtmarks1").prop('disabled', true);
                    $("#txtmarks2").prop('disabled', true);
                    $("#txtmarks3").prop('disabled', true);
                    $("#txtmarks4").prop('disabled', true);
                    $("#txtmarks5").prop('disabled', true);
                    $('#ctl00_ContentPlaceHolder1_ddlSection').empty();
                    $('#ctl00_ContentPlaceHolder1_ddlSection').append("<option value='0'>Please Select</option>");
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
});
}
    catch(ex){
    
    }
}