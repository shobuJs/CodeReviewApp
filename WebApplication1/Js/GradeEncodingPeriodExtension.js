//===============================================//
// MODULE NAME   : RFC ERP 
// PAGE NAME     : Grade Encoding Period Extension
// CREATION DATE : 23-10-2023
// CREATED BY    : Ekansh Moundekar
// Modified BY   : Ekansh Moundekar
// Modified Date : 24-10-2023
//===============================================//
let GlobalTeacher = 0;
let GlobalSubject = 0;
let GlobalComponent = 0;
let  GlobalEdit=0;
let cancle = 0;

var liveurl = "../../../../";
var localurl = "../../";

function DropDownCampusChange(CheckStatus) {
    var msg='';
    if ($("#ctl00_ContentPlaceHolder1_ddlCampus").val() != "0") {
        if ($("#ctl00_ContentPlaceHolder1_ddlSession").val() == '0')
            msg += "\r Please select Academic Session !!!</br>"; 
      
    }
    if (msg != '') {
        iziToast.warning({
            message: msg,
        });
        $("#ctl00_ContentPlaceHolder1_ddlCampus").val('0').change();
        return false;
    }
    $("#ctl00_ContentPlaceHolder1_ddlTeacher").empty();
    $("#ctl00_ContentPlaceHolder1_ddlTeacher").append($('<option></option>').val(0).html("Please Select"));
    $("#ctl00_ContentPlaceHolder1_ddlSubject").empty();
    $("#ctl00_ContentPlaceHolder1_ddlSubject").append($('<option></option>').val(0).html("Please Select"));
   
    //if($("#ctl00_ContentPlaceHolder1_ddlCampus").val()!="0")
    //{
    //    if($("#ctl00_ContentPlaceHolder1_ddlSession").val()!='0'){
    //        ShowGradeActivityData();
    //    }
    //}

    if($("#ctl00_ContentPlaceHolder1_ddlCampus").val()!="0")
    {
        ShowGradeActivityData();
    }
    DropDownCourse(2);
    RemoveTableDynamically('#BindDynamicGradePeriodTables');
    $('.dataTables_filter').hide();
    $('.dataTables_info').hide();
    $('.dataTables_length').hide();
    $('.buttons-excel').show();
    $('#BindDynamicGradePeriodTables').show();
    $('#BindDynamicGradePeriodTables_paginate').hide();
}
function DropDownTeacherChange(CheckStatus) {
    $("#ctl00_ContentPlaceHolder1_ddlSubject").empty();
    $("#ctl00_ContentPlaceHolder1_ddlSubject").append($('<option></option>').val(0).html("Please Select"));
    DropDownCourse(3);
}
function DropDownSubjectChange(CheckStatus) {
    $("#ctl00_ContentPlaceHolder1_ddlAssessmentComponent").empty();
    $("#ctl00_ContentPlaceHolder1_ddlAssessmentComponent").append($('<option></option>').val(0).html("Please Select"));
    DropDownCourse(4);
}

//$("#ctl00_ContentPlaceHolder1_ddlSession").change(function () {
//    
//    try {
//        if(GlobalEdit!=1)
//        {
//            $("#ctl00_ContentPlaceHolder1_ddlCampus").val("0").change();
//        }
        
//    }
//    catch (ex) {

//    }
//});

function DropDownSessionChange(CheckStatus){
    try {
        if(GlobalEdit!=1)
        {
            if($("#ctl00_ContentPlaceHolder1_ddlSession").val()!='0'){
               
                $("#ctl00_ContentPlaceHolder1_ddlCampus").val("0").change();
            }
            
        }
        
    }
    catch (ex) {

    }
}

function DropDownCourse(CheckStatus) {
    try {
        var Session = $("#ctl00_ContentPlaceHolder1_ddlSession").val();
        var Campus = $("#ctl00_ContentPlaceHolder1_ddlCampus").val();
        var Teacher = $("#ctl00_ContentPlaceHolder1_ddlTeacher").val();
        var Subject = $("#ctl00_ContentPlaceHolder1_ddlSubject").val();
        var Obj = {};
        if (CheckStatus == 2) {
            Obj.DropDownNo = Session + ',' + Campus;
            Obj.Command_Type = CheckStatus;
            Obj.SessionNo=0;
            Obj.UserNo=0
            if (Campus != "0") {

            }
        }
        else if (CheckStatus == 3) {
            Obj.DropDownNo = Campus;
            Obj.Command_Type = CheckStatus;
            Obj.SessionNo=Session;
            Obj.UserNo=Teacher
        }
        else {
            Obj.DropDownNo = Subject;
            Obj.Command_Type = CheckStatus;
            Obj.SessionNo=Session;
            Obj.UserNo=Teacher
        }
        $.ajax({
            url: localurl+"GradeEncodingPeriodExtension.aspx/BindDropDown",
            type: "POST",
            data: JSON.stringify(Obj),
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            success: function (res) {
                var count = 0;
                if (CheckStatus == 2) {
                    if (res.d == '') {
                        return false;
                    }
                }
                else if (CheckStatus == 3) {
                    if (res.d == '') {
                        return false;
                    }
                }
                else  {
                    if (res.d == '') {
                        return false;
                    }
                }
              
                var count = 0;
               
                $.each(res.d, function (data, value) {
                    if (CheckStatus == 2) {
                        if (count == 0) {
                            count++;
                        }
                        $("#ctl00_ContentPlaceHolder1_ddlTeacher").append($("<option></option>").val(value.DropDownFieldNo).html(value.DropDownFieldName));
                    }
                    else if (CheckStatus == 3) {
                        if (count == 0) {
                            count++;
                        }
                        $("#ctl00_ContentPlaceHolder1_ddlSubject").append($("<option></option>").val(value.DropDownFieldNo).html(value.DropDownFieldName));
                    }
                       
                    else  {
                        if (count == 0) {
                            count++;
                        }
                        $("#ctl00_ContentPlaceHolder1_ddlAssessmentComponent").append($("<option></option>").val(value.DropDownFieldNo).html(value.DropDownFieldName));
                    }
                })
                if (CheckStatus == 2) {
                    if (GlobalTeacher != 0) {
                        $("#ctl00_ContentPlaceHolder1_ddlTeacher").val(GlobalTeacher).change();
                    }
                    GlobalTeacher = 0;
                }
                else if (CheckStatus == 3) {
                    if (GlobalSubject != 0) {
                        $("#ctl00_ContentPlaceHolder1_ddlSubject").val(GlobalSubject).change();
                    }
                    GlobalSubject = 0;
                }
                else  {
                    if (GlobalComponent != 0) {
                        $("#ctl00_ContentPlaceHolder1_ddlAssessmentComponent").val(GlobalComponent).change();
                    }
                    GlobalComponent = 0;
                }
            },
            error: function (errResponse) {

            }
        });
    } catch (ex) {

    }
}
//---------------------------------Drop Down Bind End ---------------------------------------------//
//---------------------------------Submit Grade Encoding Extension Start ---------------------------------------------//
$('#btnSubmit').click(function(){
    try{
        ShowLoader('#btnSubmit');
        var msg = ''; var str = ""; var count = 0;
        if ($("#ctl00_ContentPlaceHolder1_ddlSession").val() == '0')
            msg += "\r Please Select Academic Session !!! <br/>";
        if ($("#ctl00_ContentPlaceHolder1_ddlCampus").val() == '0')
            msg += "\r Please Select Campus !!! <br/>";
        if ($("#ctl00_ContentPlaceHolder1_ddlTeacher").val() == '0')
            msg += "\r Please Select Teacher !!! <br/>";
        if ($("#ctl00_ContentPlaceHolder1_ddlSubject").val() == '0')
            msg += "\r Please Select Subject !!! <br/>";
        if ($("#ctl00_ContentPlaceHolder1_ddlAssessmentComponent").val() == '0')
            msg += "\r Please Select Assessment Component !!! <br/>";
        if ($("#txtStartDate").val().trim() == '')
            msg += "\r Please Select Start Date !!! <br/>";
        if ($("#txtEndDate").val().trim() == '')
            msg += "\r Please Select End Date !!! <br/>";
        if (msg != '') {
            iziToast.warning({
                message: msg,
            });
            return false;
        }
        var Obj={};
        Obj.ExtensNo=$("#hdnExtensNo").val(); 
        Obj.SessionNo=$("#ctl00_ContentPlaceHolder1_ddlSession").val();
        Obj.CampusNo=$("#ctl00_ContentPlaceHolder1_ddlCampus").val();
        Obj.TeacherNo=$("#ctl00_ContentPlaceHolder1_ddlTeacher").val();
        Obj.DropDownNo=$("#ctl00_ContentPlaceHolder1_ddlSubject").val();
        Obj.ComponentNo=$("#ctl00_ContentPlaceHolder1_ddlAssessmentComponent").val();
        Obj.StartDate=$("#txtStartDate").val();
        Obj.EndDate=$("#txtEndDate").val();

        const today = new Date();
        const Todays = today.toISOString().split('T')[0];
        const TodaysDate = new Date(Todays);
        const startDate = new Date($('#txtStartDate').val());
        const endDate = new Date($('#txtEndDate').val());
       
            if (TodaysDate > startDate) {
                Swal.fire({
                    html: 'Start Date should not be less than Today Date !!!',
                    icon: 'warning'
                });
                return false;
            } 

        if (startDate > endDate) {
            Swal.fire({
                html: 'End Date should not be less than Start Date !!!',
                icon: 'warning'
            });
           
            return false;
        } 
        $.ajax({
            url: localurl+"GradeEncodingPeriodExtension.aspx/SaveGradeEncodingExtension",
            type: "POST",
            data: JSON.stringify(Obj),
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            success: function (response) {
               
                str = '<thead><tr><th><span class="Action">Action</span></th><th><span class="AcaSession">Academic Session</span></th><th><span class="Campus">Campus</span></th><th><span class="Teacher">Teacher</span></th><th><span class="Subjects">Subjects</span></th><th><span class="Curriculums">Curriculums</span></th><th><span class="AssessmentComponent">Assessment Component</span></th><th><span class="ClsStartDate">Start Date</span></th><th><span class="ClsEndDate">End Date</span></th></tr></thead><tbody>';
                $.each(response.d, function (index, GetValue) {
                     
                    if (GetValue.CheckStatus == "1") {
                        iziToast.success({
                            message: 'Record Added Successfully  !!!',
                        });
                        ShowGradeData($("#ctl00_ContentPlaceHolder1_ddlSession").val());
                         Clear();
                        return false;
                    }
                    else if (GetValue.CheckStatus == "2") {
                        iziToast.success({
                            message: 'Record Updated Successfully!!!',
                        });
                        ShowGradeData($("#ctl00_ContentPlaceHolder1_ddlSession").val());
                         Clear();
                        return false;
                    }
                    else if (GetValue.CheckStatus == "3") {
                        Swal.fire({
                            html: 'Record Already Exists !!!',
                            icon: 'warning'
                        });
                        ShowGradeData($("#ctl00_ContentPlaceHolder1_ddlSession").val());
                        Clear();
                        return false;
                    }
                    else {
                        Swal.fire({
                            html: 'Error Occurred !!!',
                            icon: 'error'
                        });
                        $("[id*=preloader]").hide();
                        Clear();
                        return false;
                    }
                    
                });
            }
        });
    }
    catch (ex) {

    }
});

function ShowGradeData(clickValue){
    var Obj = {}; var CDB_NO = ''; var Semester = '';
    var Campus = $("#ctl00_ContentPlaceHolder1_ddlCampus").val();
 
    Obj.SessionNo=clickValue;
    Obj.DropDownNo =Campus;
    Obj.Command_Type=5;
    
    Obj.Dynamic_Filter = Campus;
    Obj.UserNo = 0;
    $.ajax({
        url: localurl+"GradeEncodingPeriodExtension.aspx/BindList",
        type: "POST",
        data: JSON.stringify(Obj),
        dataType: "json",
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            str = '<thead><tr><th><span class="Action">Action</span></th><th><span class="AcaSession">Academic Session</span></th><th><span class="Campus">Campus</span></th><th><span class="Teacher">Teacher</span></th><th><span class="Subjects">Subjects</span></th><th><span class="Curriculums">Curriculums</span></th><th><span class="AssessmentComponent">Assessment Component</span></th><th><span class="ClsStartDate">Start Date</span></th><th><span class="ClsEndDate">End Date</span></th></tr></thead><tbody>';
            $.each(response.d, function (index, GetValue) {
                str = str + '<tr>'
                str = str + '<td><a id="btnEditGradeExtension" class="fa fa-pencil-square-o text-primary" title="Edit Record" href="#" onclick="EditGradeExtension(this)"></a>' +
                    '<input type="hidden" id="hdnTableExtensNo" value="' + GetValue.ExtensNo + '"/><input type="hidden" id="hdnSessionNo" value="' + GetValue.SessionNo + '"/><input type="hidden" id="hdnCampusNo" value="' + GetValue.CampusNo + '"/>'+
                    '<input type="hidden" id="hdnTeacherNo" value="' + GetValue.TeacherNo + '"/><input type="hidden" id="hdnSchemeNo" value="' + GetValue.SchemeNo + '"/><input type="hidden" id="hdnCourseNo" value="' + GetValue.SubjectNo + '"/>'+
                    '<input type="hidden" id="hdnComponentNo" value="' + GetValue.ComponentNo + '"/></td>'
                str = str + '<td>' + GetValue.SessionName + '</td>'
                str = str + '<td>' + GetValue.CampusName + '</td>'
                str = str + '<td>' + GetValue.TeacherName + '</td>'
                str = str + '<td>' + GetValue.SubjectName + '</td>'
                str = str + '<td>' + GetValue.SchemeName + '</td>'
                str = str + '<td>' + GetValue.ComponentName + '</td>'
                str = str + '<td>' + GetValue.StartDateStri + '</td>'
                str = str + '<td>' + GetValue.EndDateStri + '</td>'
                str = str + '</tr>'
            });
            str = str + '</tbody>';
            RemoveTableDynamically('#BindDynamicGradePeriodTables');
            $("#BindDynamicGradePeriodTables").append(str);
            var BindDynamicGradePeriodTables = $('#BindDynamicGradePeriodTables')
            commonDatatable(BindDynamicGradePeriodTables)
        },
        error: function (errResponse) {

        }
    });
}

//---------------------------------Submit Grade Encoding Extension End ---------------------------------------------//
//---------------------------------Show Grade Encoding Extension Start ---------------------------------------------//
//$(document).ready(function () {
//    $("#ctl00_ContentPlaceHolder1_ddlSession").focus();
//});

function ShowGradeActivityData(){
    var Obj = {}; var CDB_NO = ''; var Semester = '';
    var Session = $("#ctl00_ContentPlaceHolder1_ddlSession").val();
    var Campus = $("#ctl00_ContentPlaceHolder1_ddlCampus").val();
 
    Obj.SessionNo=Session;
    Obj.DropDownNo =Campus;
    Obj.Command_Type=5;
    
    Obj.Dynamic_Filter = Campus;
    Obj.UserNo = 0;
    $.ajax({
        url: localurl+"GradeEncodingPeriodExtension.aspx/BindList",
        type: "POST",
        data: JSON.stringify(Obj),
        dataType: "json",
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json;charset=utf-8",
        success: function (response) {
             
                $('.dataTables_filter').show();
                $('.dataTables_info').show();
                $('.dataTables_length').show();
                $('.buttons-excel').show();
                $('#BindDynamicGradePeriodTables').show();
                $('#BindDynamicGradePeriodTables_paginate').show();
                str = '<thead><tr><th><span class="Action">Action</span></th><th><span class="AcaSession">Academic Session</span></th><th><span class="Campus">Campus</span></th><th><span class="Teacher">Teacher</span></th><th><span class="Subjects">Subjects</span></th><th><span class="Curriculums">Curriculums</span></th><th><span class="AssessmentComponent">Assessment Component</span></th><th><span class="ClsStartDate">Start Date</span></th><th><span class="ClsEndDate">End Date</span></th></tr></thead><tbody>';
                $.each(response.d, function (index, GetValue) {
                    str = str + '<tr>'
                    str = str + '<td><a id="btnEditGradeExtension" class="fa fa-pencil-square-o text-primary" title="Edit Record" href="#" onclick="EditGradeExtension(this)"></a>' +
                        '<input type="hidden" id="hdnTableExtensNo" value="' + GetValue.ExtensNo + '"/><input type="hidden" id="hdnSessionNo" value="' + GetValue.SessionNo + '"/><input type="hidden" id="hdnCampusNo" value="' + GetValue.CampusNo + '"/>'+
                        '<input type="hidden" id="hdnTeacherNo" value="' + GetValue.TeacherNo + '"/><input type="hidden" id="hdnSchemeNo" value="' + GetValue.SchemeNo + '"/><input type="hidden" id="hdnCourseNo" value="' + GetValue.SubjectNo + '"/>'+
                        '<input type="hidden" id="hdnComponentNo" value="' + GetValue.ComponentNo + '"/></td>'
                    str = str + '<td>' + GetValue.SessionName + '</td>'
                    str = str + '<td>' + GetValue.CampusName + '</td>'
                    str = str + '<td>' + GetValue.TeacherName + '</td>'
                    str = str + '<td>' + GetValue.SubjectName + '</td>'
                    str = str + '<td>' + GetValue.SchemeName + '</td>'
                    str = str + '<td>' + GetValue.ComponentName + '</td>'
                    str = str + '<td>' + GetValue.StartDateStri + '</td>'
                    str = str + '<td>' + GetValue.EndDateStri + '</td>'
                    str = str + '</tr>'
                });
                str = str + '</tbody>';
                RemoveTableDynamically('#BindDynamicGradePeriodTables');
                $("#BindDynamicGradePeriodTables").append(str);
                var BindDynamicGradePeriodTables = $('#BindDynamicGradePeriodTables')
                commonDatatable(BindDynamicGradePeriodTables)
            
        },
        error: function (errResponse) {

        }
    });
}
//---------------------------------Show Grade Encoding Extension End ---------------------------------------------//
//----------------------------Edit Grade Encoding Extension Started------------------------------//
function EditGradeExtension(ClickValue) {
    try {
        GlobalEdit=1;
        var td = $("td", $(ClickValue).closest("tr"));
        $("#txtStartDate").val(td[7].innerText);
        $("#txtEndDate").val(td[8].innerText);
        $("#hdnExtensNo").val($("[id*=hdnTableExtensNo]", td).val());
        $("#ctl00_ContentPlaceHolder1_ddlSession").val($("[id*=hdnSessionNo]", td).val()).change();
        $("#ctl00_ContentPlaceHolder1_ddlCampus").val($("[id*=hdnCampusNo]", td).val()).change();
        GlobalTeacher=$("[id*=hdnTeacherNo]", td).val();
        var SchemeCourse=$("[id*=hdnSchemeNo]", td).val()+','+$("[id*=hdnCourseNo]", td).val();
        GlobalSubject=SchemeCourse;
        GlobalComponent=$("[id*=hdnComponentNo]", td).val();
        $("#ctl00_ContentPlaceHolder1_ddlSession").prop("disabled", "disabled");
        $("#ctl00_ContentPlaceHolder1_ddlCampus").prop("disabled", "disabled");
        $("#ctl00_ContentPlaceHolder1_ddlTeacher").prop("disabled", "disabled");
        $("#ctl00_ContentPlaceHolder1_ddlSubject").prop("disabled", "disabled");
        $("#ctl00_ContentPlaceHolder1_ddlAssessmentComponent").prop("disabled", "disabled");
    }
    catch (ex) {

    }
}
//----------------------------Edit Grade Encoding Extension End------------------------------------//
//----------------------------Clear Grade Encoding Extension Started------------------------------//
function Clear() {
    GlobalTeacher = 0;
    GlobalSubject = 0;
    GlobalComponent = 0;
    GlobalEdit=0;
    $("#hdnExtensNo").val(0);
    $("#txtStartDate").val('');
    $("#txtEndDate").val('');
    $("#ctl00_ContentPlaceHolder1_ddlTeacher").val(0).change();
    $("#ctl00_ContentPlaceHolder1_ddlSubject").val(0).change();
    $("#ctl00_ContentPlaceHolder1_ddlAssessmentComponent").val(0).change();
    $("#ctl00_ContentPlaceHolder1_ddlCampus").val(0).change();
    $("#ctl00_ContentPlaceHolder1_ddlSession").val(0).change();
    //ShowGradeActivityData();
    $("#ctl00_ContentPlaceHolder1_ddlSession").prop("disabled", false);
    $("#ctl00_ContentPlaceHolder1_ddlCampus").prop("disabled", false);
    $("#ctl00_ContentPlaceHolder1_ddlTeacher").prop("disabled", false);
    $("#ctl00_ContentPlaceHolder1_ddlSubject").prop("disabled", false);
    $("#ctl00_ContentPlaceHolder1_ddlAssessmentComponent").prop("disabled", false);
}

$("#btnClear").click(function(){
    try{
        GlobalTeacher = 0;
        GlobalSubject = 0;
        GlobalComponent = 0;
        GlobalEdit=0;
        $("#hdnExtensNo").val(0);
        $("#txtStartDate").val('');
        $("#txtEndDate").val('');
        $("#ctl00_ContentPlaceHolder1_ddlTeacher").val(0).change();
        $("#ctl00_ContentPlaceHolder1_ddlSubject").val(0).change();
        $("#ctl00_ContentPlaceHolder1_ddlAssessmentComponent").val(0).change();
        $("#ctl00_ContentPlaceHolder1_ddlCampus").val(0).change();
         $('#BindDynamicGradePeriodTables').hide();
        $("#ctl00_ContentPlaceHolder1_ddlSession").prop("disabled", false);
        $("#ctl00_ContentPlaceHolder1_ddlCampus").prop("disabled", false);
        $("#ctl00_ContentPlaceHolder1_ddlTeacher").prop("disabled", false);
        $("#ctl00_ContentPlaceHolder1_ddlSubject").prop("disabled", false);
        $("#ctl00_ContentPlaceHolder1_ddlAssessmentComponent").prop("disabled", false);
        $('.dataTables_filter').hide();
        $('.dataTables_length').hide();
        $('.buttons-excel').hide();
        $('.dataTables_info').hide();
        $('#BindDynamicGradePeriodTables_paginate').hide();
        $("#ctl00_ContentPlaceHolder1_ddlSession").val(0).change();
        
    }
    catch (ex) {

    }
});

