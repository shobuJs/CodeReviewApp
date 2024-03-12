//===============================================//
// MODULE NAME   : RFC ERP Portal
// PAGE NAME     : Exam Pattern
// CREATION DATE : 02-06-2023
// CREATED BY    : Gaurav Kalbande
// Modified BY   : Harshal Bobde
// Modified Date : 09-06-2023
//===============================================// 
//------------- EXAM PATTERN START------------- // 
$(document).ready(function () {
    $('#txtExamPattern').focus();
    $('#txtAssType').focus();
    $('#ctl00_ContentPlaceHolder1_ddlExamPattern').focus();
    $('#ctl00_ContentPlaceHolder1_ddlPattern').focus();
    // CallLabelBindingDynamically();
    bindExamPatternType();
    BindExamPattern();
    BindAssessmentType();
    //BindExamComponent();
    BindCurriculumtag();
    //BindExamType();

    $("#btnLock").hide();
    $('#BindExam').DataTable({
        dom: 'lBfrtip',
        buttons: [],
    });
});

function loadData(tabName) {
    switch (tabName) {
        case 'ExamPattern':
            bindExamPatternType();
            break;
        case 'Creation':
            bindExamPatterndrp();
            bindAsstypedrp();
            bindExamTypedrp();
            clearExamCreation();
            $('#BindExam').empty();
            break;
        case 'Components':
            bindExamPatterndrp();
            bindExamTypedrp2();
            break;
        case 'Curriculum':
            bindExamPatterndrp();
            break;
        default:
            break;
    }
}

function bindExamPatterndrp() {
    $.ajax({
        type: "POST",
        url: "../Exam/Exam_Pattern.aspx/BindDropPattern",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            var ddlExamPattern = $("[id*=ddlExamPattern]");
            var ddlPattern = $("[id*=ddlPattern]");
            var ddlCurriculumPattern = $("[id*=ddlCurriculumPattern]");
            ddlExamPattern.empty().append('<option selected="selected" value="0">Please select</option>');
            ddlPattern.empty().append('<option selected="selected" value="0">Please select</option>');
            ddlCurriculumPattern.empty().append('<option selected="selected" value="0">Please select</option>');
            $.each(r.d, function () {
                ddlExamPattern.append($("<option></option>").val(this['PatternNo']).html(this['Pattern_Name']));
                ddlPattern.append($("<option></option>").val(this['PatternNo']).html(this['Pattern_Name']));
                ddlCurriculumPattern.append($("<option></option>").val(this['PatternNo']).html(this['Pattern_Name']));
            });
        }
    });
}
function bindAsstypedrp() {
    $.ajax({
        type: "POST", url: "../Exam/Exam_Pattern.aspx/BindDropAssessmentType", dataType: "json", contentType: "application/json", success: function (res) {
            $.each(res.d, function (data, value) {
                $("#ddlAssessmentType").append($("<option></option>").val(value.ASSESSMENTID).html(value.ASSESSMENTNAME));
            })
        }
    });
}
function bindExamTypedrp() {
    $.ajax({
        type: "POST", url: "../Exam/Exam_Pattern.aspx/BindExamTypeList", dataType: "json", contentType: "application/json", success: function (res) {
            $('#ctl00_ContentPlaceHolder1_ddlExamType').empty();
            $('#ctl00_ContentPlaceHolder1_ddlExamType').append("<option value='0'>Please Select</option>");
            $.each(res.d, function (data, value) {
                $("#ctl00_ContentPlaceHolder1_ddlExamType").append($("<option></option>").val(value.EXAMTYPEID).html(value.EXAMTYPENAME));
            })
        }
    });
}
function bindExamTypedrp2() {
    $.ajax({
        type: "POST", url: "../Exam/Exam_Pattern.aspx/BindExamTypeList", dataType: "json", contentType: "application/json", success: function (res) {
            $('#ctl00_ContentPlaceHolder1_ddlExamType2').empty();
            $('#ctl00_ContentPlaceHolder1_ddlExamType2').append("<option value='0'>Please Select</option>");
            $.each(res.d, function (data, value) {
                $("#ctl00_ContentPlaceHolder1_ddlExamType2").append($("<option></option>").val(value.EXAMTYPEID).html(value.EXAMTYPENAME));
            })
        }
    });
}
function bindExamPatternType() {
    $.ajax({
        //00//
        type: "POST", url: "../Exam/Exam_Pattern.aspx/ExamPatternType", dataType: "json", contentType: "application/json", success: function (res) {
            $('#ctl00_ContentPlaceHolder1_ddlPatterntype').empty();
            $('#ctl00_ContentPlaceHolder1_ddlPatterntype').append("<option value='0'>Please Select</option>");
            $.each(res.d, function (data, value) {
                $("#ctl00_ContentPlaceHolder1_ddlPatterntype").append($("<option></option>").val(value.PATTERN_TYPENO).html(value.PATTERN_TYPE));
            })
        }
    });
}
function BindExamPattern() {
    try {
        $.ajax({
            url: "../Exam/Exam_Pattern.aspx/BindTable",
            type: "POST",
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                if (response.d == '') {
                    //Swal.fire({
                    //    //html: 'Record Not Found !!!',
                    //    //html: 'Record Not Found !!!',
                    //});
                    $("[id*=preloader]").hide();
                    return false;
                } else {
                    str = '<thead> <tr><th><span class="Action">Action</span></th><th><span class="ExamPattern">Exam Pattern </span></th><th><span class="ExamPatternType">Exam Pattern Type</span></th> <th><span class="Createdby">Created/Modify By</span></th><th><span class="CreatedDate">Created/Modify Date</span></th><th><span class="Status">Status</span></th></tr></thead><tbody>';
                    $.each(response.d, function (index, GetValue) {
                        str = str + '<tr class="patternclass">'
                        str = str + '<td><a id="btnEditExamPattern" class="fa fa-pencil-square-o text-primary" title="Edit Record" href="#" onclick="EditExamPattern(this)"></a>' +
                            '<input type="hidden" id="hdnTablePatternNo" value="' + GetValue.PatternNo + '"/><input type="hidden" id="hdnexampatterntypeno" value="' + GetValue.PatternTypeNo + '"/><input type="hidden" id="hdnpattername" value="' + GetValue.Pattern_Name + '"/></td>'
                        str = str + '<td>' + GetValue.Pattern_Name + '</td>'
                        str = str + '<td>' + GetValue.PatternType + '</td>'
                        str = str + '<td>' + GetValue.CREMODIBY + '</td>'
                        str = str + '<td>' + GetValue.CREMODIDATE + '</td>'
                        if (GetValue.IsActive == 1) {
                            str = str + '<td><span class="badge badge-success">Active</span>' +
                                '<input type="hidden" id="hdnExamPatternStatus" value="true"/></td>'
                        }
                        else {
                            str = str + '<td><span class="badge badge-danger">Inactive</span>' +
                                '<input type="hidden" id="hdnExamPatternStatus" value="false"/></td>'
                        }

                        str = str + '</tr>'
                    });
                    str = str + '</tbody>';
                    RemoveTableDynamically('#BindDynamicExamPatternTable');
                    $("#BindDynamicExamPatternTable").append(str);
                    var BindDynamicExamPatternTable = $('#BindDynamicExamPatternTable')
                    commonDatatable(BindDynamicExamPatternTable)
                    //BindtableLabelsDyanamically();
                }
            },
            error: function (errResponse) {
            }
        });
    }
    catch (ex) {

    }
}
$('#btnSubmitExamPattern').click(function () {
    try {

        var msg = ''; var str = ""; var count = 0;
        if ($('#txtExamPattern').val().trim() == '') {
            msg += "\r Please enter Exam Pattern !!!</br>";
        }
        if ($('#txtExamPattern').val().trim().length > 50) {
            msg += "\r Exam Pattern exceeds the maximum allowed limit !!!</br>";
        }
        if ($("#ctl00_ContentPlaceHolder1_ddlPatterntype").val() == '0')
            msg += "\r Please Select  Exam Pattern Type!!!</br>";

        if (msg != '') {

            iziToast.warning({
                message: msg,
            });
            return false;
        }


        Obj = {};
        Obj.PatternNo = $('#hdfPatternNo').val();
        Obj.ExamPattern = $('#txtExamPattern').val().trim();
        Obj.ExamPatternType = $("#ctl00_ContentPlaceHolder1_ddlPatterntype").val();
        Obj.IsActive = $('#Status').prop('checked');
        if (Obj.IsActive == true) {
            Obj.IsActive = 1;
        }
        else {
            Obj.IsActive = 0;
        }

        $.ajax({
            url: "../Exam/Exam_Pattern.aspx/SaveExamPattern",
            type: "POST",
            data: JSON.stringify(Obj),
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                $.each(response.d, function (index, GetValue) {
                    if (count == 0) {
                        if (GetValue.CheckStatus == "1") {
                            iziToast.success({
                                message: 'Exam Pattern Added Successfully !!!',
                            });
                        }
                        else if (GetValue.CheckStatus == "2") {
                            iziToast.success({
                                message: 'Exam Pattern  Updated Successfully!!!',
                            });
                        }
                        else if (GetValue.CheckStatus == "3") {
                            Swal.fire({
                                html: '' + $('#txtExamPattern').val() + ' Exam Pattern Already Exists !!!',
                                icon: 'warning'
                            });
                        }
                        else {
                            Swal.fire({
                                html: 'Error Occurred !!!',
                                icon: 'error'
                            });
                            $("[id*=preloader]").hide();
                            return false;
                        }
                    }
                    BindExamPattern();
                    ClearExamPattern();
                    count++;
                });
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
    catch (ex) {

    }

});
$('#btnClearExamPattern').click(function () {
    ClearExamPattern();
});
function ClearExamPattern() {
    $('#hdnpattername').val('');
    $('#hdfPatternNo').val('0');
    $('#hdnexampatterntypeno').val('0');
    $('#txtExamPattern').val('');
    $('#Status').prop('checked', true);
    $("#btnSubmitExamPattern").val('Submit');
    $("#ctl00_ContentPlaceHolder1_ddlPatterntype").val(0).change();

}
function EditExamPattern(ClickValue) {
    try {
        var td = $("td", $(ClickValue).closest("tr"));
        $("#hdfPatternNo").val($("[id*=hdnTablePatternNo]", td).val());
        $("#btnSubmitExamPattern").val('Update');
        $("#txtExamPattern").val(td[1].innerText);

        $("#ctl00_ContentPlaceHolder1_ddlPatterntype").val($("[id*=hdnexampatterntypeno]", td).val()).change();
        if ($("[id*=hdnExamPatternStatus]", td).val() == "false")
            $('#Status').prop('checked', false);
        else
            $('#Status').prop('checked', true);
    }
    catch (ex) {

    }
}
//------------- EXAM PATTERN END------------- // 

//------------- EXAM TYPE START------------- //  
//function BindExamType() {
//    try {
//        $.ajax({
//            url: "../Exam/Exam_Pattern.aspx/BindExamType",
//            type: "POST",
//            dataType: "json",
//            beforeSend: function () { $("[id*=preloader]").show(); },
//            complete: function () { $("[id*=preloader]").hide(); },
//            contentType: "application/json;charset=utf-8",
//            success: function (response) {
//                if (response.d == '') {
//                    //Swal.fire({
//                    //    //html: 'Record Not Found !!!',
//                    //    //html: 'Record Not Found !!!',
//                    //});
//                    alert('Record Not Found');
//                    $("[id*=preloader]").hide();
//                    return false;
//                } else {
//                    str = '<thead> <tr><th><span class="Action">Action</span></th><th><span class="ExamType">Exam Type</span></th><th><span class="Createdby">Created/Modify By</span></th><th><span class="CreatedDate">Created/Modify Date</span></th><th><span class="Status">Status</span></th></tr></thead><tbody>';
//                    $.each(response.d, function (index, GetValue) {
//                        str = str + '<tr class="classExamtype">'
//                        str = str + '<td><a id="btnEditExamType" class="fa fa-pencil-square-o text-primary" title="Edit Record" href="#" onclick="EditExamType(this)"></a>' +
//                            '<input type="hidden" id="hdntblExamId" value="' + GetValue.EXAMTYPEID + '"/><input type="hidden" id="hdnExamtypeame" value="' + GetValue.EXAMTYPENAME + '"/></td></td>'
//                        str = str + '<td>' + GetValue.EXAMTYPENAME + '</td>'
//                        str = str + '<td>' + GetValue.CREMODIBY + '</td>'
//                        str = str + '<td>' + GetValue.CREMODIDATE + '</td>'
//                        if (GetValue.ACTIVE == 1) {
//                            str = str + '<td><span class="badge badge-success">Active</span>' +
//                                '<input type="hidden" id="hdnExamTypeStatus" value="true"/></td>'
//                        }
//                        else {
//                            str = str + '<td><span class="badge badge-danger">Inactive</span>' +
//                                '<input type="hidden" id="hdnExamTypeStatus" value="false"/></td>'
//                        }
//                        str = str + '</tr>'
//                    });
//                    str = str + '</tbody>';
//                    RemoveTableDynamically('#tblExamtype');
//                    $("#tblExamtype").append(str);
//                    var BindDynamicExamPatternTable = $('#tblExamtype')
//                    commonDatatable(BindDynamicExamPatternTable)
//                    // BindtableLabelsDyanamically();
//                    //------------- Data Table JS for Campus Master -------------//
//                    var tblExamPatternMaster = $('#tblExamtype')
//                    commonDatatable(tblExamPatternMaster);
//                }
//            },
//            error: function (errResponse) {
//            }
//        });
//    }
//    catch (ex) {

//    }
//}
//$('#btndubmitEtype').click(function () {
//    try {
//        var msg = ''; var str = ""; var count = 0;
//        if ($('#txtExamType').val().trim() == '') {
//            msg += "\r Please Enter Exam Type!!!</br>";
//        }
//        if ($('#txtExamType').val().trim().length > 50) {
//            msg += "\r Exam Type exceeds the maximum allowed limit !!!</br>";
//        }

//        if (msg != '') {

//            iziToast.warning({
//                message: msg,
//            });
//            return false;
//        }

//        Obj = {};
//        Obj.EXAMTYPEID = $('#hdnetype').val();
//        Obj.EXAMTYPENAME = $('#txtExamType').val().trim();
//        Obj.ACTIVE = $('#chketype').prop('checked');
//        if (Obj.ACTIVE == true) {
//            Obj.ACTIVE = 1;
//        }
//        else {
//            Obj.ACTIVE = 0;
//        }

//        $.ajax({
//            url: "../Exam/Exam_Pattern.aspx/SaveExamType",
//            type: "POST",
//            data: JSON.stringify(Obj),
//            dataType: "json",
//            beforeSend: function () { $("[id*=preloader]").show(); },
//            complete: function () { $("[id*=preloader]").hide(); },
//            contentType: "application/json;charset=utf-8",
//            success: function (response) {

//                $.each(response.d, function (index, GetValue) {
//                    if (count == 0) {
//                        if (GetValue.CheckStatus == "1") {
//                            iziToast.success({
//                                message: 'Exam Type Added Successfully !!!',
//                            });
//                        }
//                        else if (GetValue.CheckStatus == "2") {
//                            iziToast.success({
//                                message: 'Exam Type Updated Successfully!!!',
//                            });
//                        }
//                        else if (GetValue.CheckStatus == "3") {
//                            Swal.fire({
//                                html: '' + $('#txtExamType').val() + ' Exam Type Already Exists !!!',
//                                icon: 'warning'
//                            });
//                        }
//                        else {
//                            Swal.fire({
//                                html: 'Error Occurred !!!',
//                                icon: 'error'
//                            });
//                            $("[id*=preloader]").hide();
//                            return false;
//                        }
//                    }
//                    ClearExamType();
//                    BindExamType();
//                    count++;
//                });
//            },
//            error: function (errResponse) {
//                Swal.fire({
//                    html: 'Error Occurred !!!',
//                    icon: 'error'
//                });
//                return false;
//            }
//        });
//    }
//    catch (ex) {

//    }

//});
//$('#btnCancleEtype').click(function () {
//    ClearExamType();
//});
//function ClearExamType() {
//    $('#hdnetype').val('0');
//    $('#txtExamType').val('');
//    $('#chketype').prop('checked', true);
//    $("#btndubmitEtype").val('Submit');

//}
//function EditExamType(ClickValue) {
//    try {
//        var td = $("td", $(ClickValue).closest("tr"));
//        $("#hdnetype").val($("[id*=hdntblExamId]", td).val());
//        $("#btndubmitEtype").val('Update');
//        $("#txtExamType").val(td[1].innerText);

//        if ($("[id*=hdnExamTypeStatus]", td).val() == "false")
//            $('#chketype').prop('checked', false);
//        else
//            $('#chketype').prop('checked', true);
//    }
//    catch (ex) {
//    }
//}
//------------- EXAM TYPE END------------- // 

//------------- Assessment TYPE START------------- //  
function BindAssessmentType() {

    try {
        $.ajax({
            url: "../Exam/Exam_Pattern.aspx/BindAssessmentType",
            type: "POST",
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                if (response.d == '') {
                    $("[id*=preloader]").hide();
                    return false;
                } else {

                    str = '<thead> <tr><th><span class="Action">Action</span></th><th><span class="AssessmentType">Assessment Type</span></th><th><span class="Createdby">Created/Modify By</span></th><th><span class="CreatedDate">Created/Modify Date</span></th><th><span class="Status">Status</span></th></tr></thead><tbody>';
                    $.each(response.d, function (index, GetValue) {
                        str = str + '<tr class="classAsstype">'
                        str = str + '<td><a id="btnEditAss" class="fa fa-pencil-square-o text-primary" title="Edit Record" href="#" onclick="EditAss(this)"></a>' +
                            '<input type="hidden" id="hdntblAssId" value="' + GetValue.ASSESSMENTID + '"/><input type="hidden" id="hdnassname" value="' + GetValue.ASSESSMENTNAME + '"/></td>'
                        str = str + '<td>' + GetValue.ASSESSMENTNAME + '</td>'
                        str = str + '<td>' + GetValue.CREMODIBY + '</td>'
                        str = str + '<td>' + GetValue.CREMODIDATE + '</td>'
                        if (GetValue.ACTIVE == 1) {
                            str = str + '<td><span class="badge badge-success">Active</span>' +
                                '<input type="hidden" id="hdnAssTypeStatus" value="true"/></td>'
                        }
                        else {
                            str = str + '<td><span class="badge badge-danger">Inactive</span>' +
                                '<input type="hidden" id="hdnAssTypeStatus" value="false"/></td>'
                        }

                        str = str + '</tr>'
                    });
                    str = str + '</tbody>';
                    RemoveTableDynamically('#tblasstype');
                    $("#tblasstype").append(str);
                    var BindDynamicExamPatternTable = $('#tblasstype')
                    commonDatatable(BindDynamicExamPatternTable)
                    // BindtableLabelsDyanamically();

                }
            },
            error: function (errResponse) {
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
$('#btninsertexam').click(function () {
    try {

        var msg = ''; var str = ""; var count = 0;
        if ($('#txtAssType').val().trim() == '') {
            msg += "\r Please Enter Assessment Type !!!</br>";
        }
        if ($('#txtAssType').val().trim().length > 50) {
            msg += "\r Assessment Type exceeds the maximum allowed limit !!!</br>";
        }

        if (msg != '') {

            iziToast.warning({
                message: msg,
            });
            return false;
        }

        Obj = {};
        Obj.ASSESSMENTID = $('#hdnexamtypeid').val();
        Obj.ASSESSMENTNAME = $('#txtAssType').val().trim();
        Obj.ACTIVE = $('#statustype').prop('checked');
        if (Obj.ACTIVE == true) {
            Obj.ACTIVE = 1;
        }
        else {
            Obj.ACTIVE = 0;
        }

        $.ajax({
            url: "../Exam/Exam_Pattern.aspx/SaveAsssType",
            type: "POST",
            data: JSON.stringify(Obj),
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                $.each(response.d, function (index, GetValue) {
                    if (count == 0) {
                        if (GetValue.CheckStatus == "1") {
                            iziToast.success({
                                message: 'Assessment Type Added Successfully !!!',
                            });
                        }
                        else if (GetValue.CheckStatus == "2") {
                            iziToast.success({
                                message: 'Assessment Type Updated Successfully!!!',
                            });
                        }
                        else if (GetValue.CheckStatus == "3") {
                            Swal.fire({
                                html: '' + $('#txtAssType').val() + ' Assessment Type Already Exists !!!',
                                icon: 'warning'
                            });
                        }
                        else {
                            Swal.fire({
                                html: 'Error Occurred !!!',
                                icon: 'error'
                            });
                            $("[id*=preloader]").hide();
                            return false;
                        }
                    }
                    BindAssessmentType();
                    ClearAssesssType();
                    count++;
                });
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
    catch (ex) {

    }

});
$('#btnclearexam').click(function () {
    ClearAssesssType();
});
function ClearAssesssType() {
    $('#hdnexamtypeid').val('0');
    $('#txtAssType').val('');
    $('#statustype').prop('checked', true);
    $("#btninsertexam").val('Submit');

}
function EditAss(ClickValue) {
    try {
        var td = $("td", $(ClickValue).closest("tr"));
        $("#hdnexamtypeid").val($("[id*=hdntblAssId]", td).val());
        $("#btninsertexam").val('Update');
        $("#txtAssType").val(td[1].innerText);

        if ($("[id*=hdnAssTypeStatus]", td).val() == "false")
            $('#statustype').prop('checked', false);
        else
            $('#statustype').prop('checked', true);
    }
    catch (ex) {
    }
}
//------------- EXAM TYPE END------------- // 

//------------- Exam Creation-------------//
function BindExamvalue() {
    var data = [];
    var valno = 0;
    var VarPATTERNNO = $("#hdPatterNo").val();
    $('tr.trExamName').each(function () {
        var Active;
        var VarFLDNAME = $(this).find('#txtShortName').val();
        var VarEXAMNAME = $(this).find('#txtExamName').val();
        var VarASSESSMENTTYPE = $(this).find("#ddlAssessmentType" + valno + " option:selected").val();
        var VarEXAMTYPE = $("#ctl00_ContentPlaceHolder1_ddlExamType").val();
        var VarOrganizationId = 0;
        var isChecked = $(this).find('#chkactive').prop('checked');
        if (isChecked == 1) {
            Active = 1;
        }
        else {
            Active = 0;
        }
        var alldata = {
            'FLDNAME': VarFLDNAME,
            'EXAMNAME': VarEXAMNAME,
            'EXAMTYPE': VarEXAMTYPE,
            'PATTERNNO': VarPATTERNNO,
            'ASSESSMENTID': VarASSESSMENTTYPE,
            'OrganizationId': VarOrganizationId,
            'ACTIVESTATUS': Active
        }
        valno++;
        data.push(alldata);
    });
    //console.log(data);//  
    return data;
}

$("#btnSubmitExamCreation").click(function() {
    var No = 0;
    var msg = ''; var str = ""; var count = 0;
    if ($("#ctl00_ContentPlaceHolder1_ddlExamPattern").val() == '0')
        msg += "\r Please Select  Exam Pattern !!!</br>";
    if ($("#ctl00_ContentPlaceHolder1_ddlExamType").val() == '0')
        msg += "\r Please Select Exam Type !!!</br>";

    $('tr.trExamName').each(function () {
        var isChecked = $(this).find('#chkactive').prop('checked');

        if (isChecked == 0) {
            str = "\r Please add  Atleast one Exam !!!</br>";
        }
        else {
            str = '';
            return false;
        }

    });
    if (str != '') {
        iziToast.warning({
            message: str,
        });
        return false;
    }

    $('tr.trExamName').each(function () {
        var isChecked = $(this).find('#chkactive').prop('checked');
        if (isChecked == 1) {
            var VarASSESSMENTTYPE = $(this).find("#ddlAssessmentType" + No + " option:selected").val();
            //var VarEXAMTYPE = $(this).find("#ddlExamType" + No + " option:selected").val();
            var VarEXAMNAME = $(this).find('#txtExamName').val();
            if (VarEXAMNAME == '') {
                msg += "\r Please enter Exam Name  !!!</br>";
                $(this).find('#txtExamName').focus();
                return false;
            }
            if (VarASSESSMENTTYPE == 0) {
                msg += "\r Please select  Assessment Type for Active Exam !!!</br>";
                $(this).find("#ddlAssessmentType" + No + "").focus();
                return false;
            }
            //if (VarEXAMTYPE == 0) {
            //    msg += "\r Please select  Exam Type for Active Exam !!!</br>";
            //    $(this).find("#ddlExamType" + No + "").focus();
            //    return false;
            //}
        }
        No++;
    });
    if (msg != '') {
        iziToast.warning({
            message: msg,
        });
        return false;
    }

    var strdup = '';
    var table = document.getElementById("BindExam");
    var rows = table.getElementsByClassName("trExamName");
    var valueSet = new Set();
    for (var i = 0; i < rows.length; i++) {
        var inputs = rows[i].querySelectorAll(".row-input");
        for (var j = 0; j < inputs.length; j++) {
            var inputValue = inputs[j].value.trim(); // Trim to remove leading/trailing whitespace 
            if (inputValue !== "") {
                if (valueSet.has(inputValue)) {
                    strdup += "\r Duplicate Exam Name found: " + inputValue + "!!!</br>";
                }
                else {
                    valueSet.add(inputValue);
                }
            }
        }
    }

    if (strdup != '') {
        iziToast.warning({
            message: strdup,
        });
        return false;
    }

    var data = JSON.stringify(BindExamvalue());

    $.ajax({
        url: '../Exam/Exam_Pattern.aspx/InsertExam',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ 'ExamData': data }),
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        success: function () {
            iziToast.success({
                message: 'Exam Updated Successfully !!!',
            });
            //clearExamCreation();
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

$("#ctl00_ContentPlaceHolder1_ddlExamPattern").change(function () {
    // Reset the value of ddlExamType to "0"
    $("#ctl00_ContentPlaceHolder1_ddlExamType").val(0).change();
});

function dropdownChanged(dropdown) {

    var msg = ''; var str = ""; var str2 = ""; var count = 0;
    var PatternValue = $("#ctl00_ContentPlaceHolder1_ddlExamPattern").val();
    var selectedValue = dropdown.value;
    if (selectedValue != "0" && selectedValue != "") {
        if (dropdown !== null && dropdown.selectedIndex !== -1) {
            var selectedText = dropdown.options[dropdown.selectedIndex].innerHTML;
        }
        if (PatternValue != "0" && PatternValue != "") {
            var Patterntext = $("#ctl00_ContentPlaceHolder1_ddlExamPattern option:selected").html();
        }
        var Obj = {};
        Obj.PatterNo = PatternValue;
        Obj.Examtypeno = selectedValue;
        var patternid = PatternValue;
        $('#hdPatterNo').val(patternid);
        try {
            $("#divexam").show();
            $.ajax({
                url: "../Exam/Exam_Pattern.aspx/GetExamDetails",
                type: "POST",
                data: JSON.stringify(Obj),
                dataType: "json",
                beforeSend: function () { $("[id*=preloader]").show(); },
                contentType: "application/json;charset=utf-8",
                success: function (response) {
                    $("#BindExam").html(response.d);
                    if (response.d == '') {
                        str = "";
                        str = '<thead> <tr><th><span class="SRNO">Sr.No.</span></th><th><span class="ExamName">Exam Name</span></th><th style="display:none; class="hidden"><span class="ShortName" >Short Name</span></th><th><span class="AssessmentType w-25">Assessment Type</span></th><th><span class="ExamType w-25">Exam Type</span></th><th><span class="ExamPattern">Exam Pattern</span></th><th><span class="Createdby">Created/Modify By</span></th><th><span class="CreatedDate">Created/Modify Date</span></th><th><span class="Status">Status</span></th></tr></thead><tbody>';
                        var i = 0; //var j = 0;
                        var loop = 0; //var loop2 = 0;

                        for (i = 0; i < 11; i++) {
                            var srno = i + 1;
                            str = str + '<tr class="trExamName">'
                            str = str + '<td>' + srno + ' </td>'
                            str = str + '<td><input type="text"  class="form-control restrict-alphabet-space row-input" maxlength="100" spellcheck="true" placeholder="e.g. P1" id="txtExamName" name="txtExamName" /> </td>'
                            if (srno == 11) {
                                str = str + '<td  style="display:none;" class="hidden"><input type="text"  class="form-control restrict-alphabet-space" maxlength="15" spellcheck="true" placeholder="e.g. S1"  value="External"  id="txtShortName" name="txtShortName"/> </td>'
                            }
                            else {
                                str = str + '<td  style="display:none;" class="hidden"><input type="text"  class="form-control restrict-alphabet-space" maxlength="15" spellcheck="true" placeholder="e.g. S1"  value="' + "S" + srno + '"  id="txtShortName" name="txtShortName"/> </td>'
                            }
                            str = str + '<td><select ID="ddlAssessmentType' + i + '" runat="server" class="form-control form-select" data-select2-enable="true"> <option value="0">Please Select</option></select></td>'
                            //str = str + '<td><select ID="ddlExamType' + j + '" runat="server" class="form-control form-select" data-select2-enable="true"> <option value="0">Please Select</option></select></td>'
                            str = str + '<td>' + selectedText + '</td>'
                            str = str + '<td>' + Patterntext + '</td>'
                            str = str + '<td>' + "-" + '</td>'
                            str = str + '<td>' + "-" + '</td>'
                            str = str + '<td><input type="checkbox" id="chkactive"/> Active </td>'
                            str = str + '</tr>'
                            $.ajax({
                                type: "POST", url: "../Exam/Exam_Pattern.aspx/BindDropAssessmentType", dataType: "json", contentType: "application/json", success: function (res) {
                                    $("[id*=preloader]").show();
                                    $.each(res.d, function (data, value) {

                                        $("#ddlAssessmentType" + loop + "").append($("<option></option>").val(value.ASSESSMENTID).html(value.ASSESSMENTNAME));
                                    })

                                    loop++;
                                    $("[id*=preloader]").hide();
                                }
                            });
                        }
                        str = str + '</tbody>';
                        RemoveTableDynamically('#BindExam');
                        $("#BindExam").append(str);
                        var tbl = $('#BindExam')
                        commonDatatables(tbl);
                        $("[id*=preloader]").hide();
                        //BindtableLabelsDyanamically();
                    }
                    else {
                        str2 = '<thead><tr><th><span class="SRNO">Sr.No.</span></th><th><span class="ExamName">Exam Name</span></th><th style="display:none;" class="hidden"><span class="ShortName">Short Name</span></th><th><span class="AssessmentType w-25">Assessment Type</span></th><th><span class="ExamType w-25">Exam Type</span></th><th><span class="ExamPattern">Exam Pattern</span></th><th><span class="Createdby">Created/Modify By</span></th><th><span class="CreatedDate">Created/Modify Date</span></th><th><span class="Status">Status</span></th></tr></thead><tbody>';
                        var rowCount = 1;
                        $.each(response.d, function (index, GetValue) {
                            $.ajax({
                                type: "POST", url: "../Exam/Exam_Pattern.aspx/BindDropAssessmentType",
                                dataType: "json",
                                contentType: "application/json",
                                success: function (res) {

                                    $.each(res.d, function (data, value) {

                                        $("[id*=preloader]").show();
                                        $("#ddlAssessmentType" + index + "").append($("<option></option>").val(value.ASSESSMENTID).html(value.ASSESSMENTNAME));
                                        var SelExamtype = GetValue.ASSESSMENTID
                                        $('#ddlAssessmentType' + index + '').val(SelExamtype);
                                    })
                                    $("[id*=preloader]").hide();
                                }
                            });
                            str2 = str2 + '<tr class="trExamName">'
                            str2 = str2 + '<td>' + rowCount + '</td>'
                            str2 = str2 + '<td><input type="text"  class="form-control restrict-alphabet-space  row-input" value="' + GetValue.EXAMNAME + '" maxlength="100" spellcheck="true" placeholder="e.g. Internal Assessment" id="txtExamName" name="txtExamName" /> </td>'
                            str2 = str2 + '<td style="display:none;" class="hidden"><input type="text"  class="form-control restrict-alphabet-space" value="' + GetValue.FLDNAME + '" maxlength="15" spellcheck="true" placeholder="e.g. S1"  value="' + "S" + srno + '"  id="txtShortName" name="txtShortName"/> </td>'
                            str2 = str2 + '<td><select ID="ddlAssessmentType' + index + '" runat="server" class="form-control form-select"    data-select2-enable="true"> <option value="0">Please Select</option></select></td>'
                            str2 = str2 + '<td>' + GetValue.EXAMTYPE + '</td>'
                            str2 = str2 + '<td>' + Patterntext + '</td>'
                            str2 = str2 + '<td>' + GetValue.CREMODIBY + '</td>'
                            str2 = str2 + '<td>' + GetValue.CREMODIDATE + '</td>'
                            if (GetValue.ACTIVESTATUS == 1) {
                                str2 = str2 + '<td><input type="checkbox" id="chkactive" checked="true" /> Active </td>'
                            }
                            else {
                                str2 = str2 + '<td><input type="checkbox" id="chkactive" /> Active </td>'
                            }
                            str2 = str2 + '</tr>'
                            rowCount++;
                        });
                        str2 = str2 + '</tbody>';
                        RemoveTableDynamically('#BindExam');
                        $("#BindExam").append(str2);
                        var tbl1 = $('#BindExam')
                        commonDatatables(tbl1);
                        $("[id*=preloader]").hide();
                        //  BindtableLabelsDyanamically();
                    }
                },
                error: function (errResponse) {
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
        $("#divexam").hide();
    }

}
$('#btnClearExamCreation').click(function () {
    clearExamCreation();
});
function clearExamCreation() {
    $("#ctl00_ContentPlaceHolder1_ddlExamPattern").val(0).change();
    $("#ctl00_ContentPlaceHolder1_ddlExamType").val(0).change();
    $("#divexam").hide();
}

//-------------End Exam Creation-------------//

//--------- Start Exam Component ----------//

function ddlExamType2(dropdown) {
    try {

        BindExamComponent();

    }
    catch (ex) {

    }
}

$("#ctl00_ContentPlaceHolder1_ddlPattern").change(function () {
    // Reset the value of ddlExamType to "0"
    $("#ctl00_ContentPlaceHolder1_ddlExamType2").val(0).change();
});

$("#ctl00_ContentPlaceHolder1_ddlExamType2").change(function () {

    if ($("#ctl00_ContentPlaceHolder1_ddlExamType2").val() != 0) {
        debugger
        BindExamComponent();
        $('#hdnexamname').val('0');
        $("#ctl00_ContentPlaceHolder1_ddlExamName").val('0');
        Obj.Examtypeno = $("#ctl00_ContentPlaceHolder1_ddlExamType2").val();
        Obj.PatterNo = $("#ctl00_ContentPlaceHolder1_ddlPattern").val();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../Exam/Exam_Pattern.aspx/GetActiveExam",
            data: JSON.stringify(Obj),
            dataType: "json",
            success: function (result) {
                $('#ctl00_ContentPlaceHolder1_ddlExamName').empty();
                $('#ctl00_ContentPlaceHolder1_ddlExamName').append("<option value='0'>Please Select</option>");
                $.each(result.d, function (key, value) {
                    $("#ctl00_ContentPlaceHolder1_ddlExamName").append($("<option></option>").val(value.EXAMNO).html(value.EXAMNAME));

                    if ($("#hdnexamname").val() != "0")
                        $('#ctl00_ContentPlaceHolder1_ddlExamName').val($("#hdnexamname").val());
                });
            },
            error: function ajaxError(result) {
                //alert(result.status + ' : ' + result.statusText);
            }
        });
    }
    else {
        $("#ctl00_ContentPlaceHolder1_ddlExamName").val(0).change();
        RemoveTableDynamically('#BindDynamicExamComponentsTable');
        $("#divcomponents").hide();
    }

})
$('#btnComponentSubmit').click(function () {
    try {
        var msg = ''; var str = ""; var count = 0;

        if ($("#ctl00_ContentPlaceHolder1_ddlPattern").val() == '0')
            msg += "\r Please select Exam Pattern !!!</br>";

        if ($("#ctl00_ContentPlaceHolder1_ddlExamName").val() == '0')
            msg += "\r Please select  Exam Name !!!</br>";

        if ($("#ctl00_ContentPlaceHolder1_ddlExamType2").val() == '0')
            msg += "\r Please select  Exam Type !!!</br>";

        if ($("#txtComponentName").val().trim() == '')
            msg += "\r Please enter  Component Name !!!</br>";

        if ($('#txtComponentName').val().trim().length > 50) {
            msg += "\r Component Name exceeds the maximum allowed limit !!!</br>";
        }

        if (msg != '') {
            iziToast.warning({
                message: msg,
            });
            return false;
        }

        Obj = {};
        Obj.EXAMCOMNO = $('#hdnComponetno').val();
        Obj.PATTERNNO = $("#ctl00_ContentPlaceHolder1_ddlPattern").val();
        Obj.EXAMTYPENO = $("#ctl00_ContentPlaceHolder1_ddlExamType2").val();
        Obj.EXAMNO = $('#ctl00_ContentPlaceHolder1_ddlExamName').val();
        Obj.COMPONAME = $('#txtComponentName').val();
        Obj.COLLEGE_CODE = 0;
        Obj.ACTIVE = $('#StatusComponent').prop('checked');
        if (Obj.ACTIVE == true) {
            Obj.ACTIVE = 1;
        }
        else {
            Obj.ACTIVE = 0;
        }

        $.ajax({
            url: "../Exam/Exam_Pattern.aspx/ExamCompIU",
            type: "POST",
            data: JSON.stringify(Obj),
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            //complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                if (response.d == "1") {
                    iziToast.success({
                        message: 'Exam Component  Added Successfully !!!',
                    });
                }
                else if (response.d == "2") {
                    iziToast.success({
                        message: 'Exam Component  Updated Successfully !!!',
                    });
                }
                else if (response.d == "3") {
                    Swal.fire({
                        html: '' + $('#txtComponentName').val() + ' Exam Component Name Already Exists !!!',
                        icon: 'warning'
                    });
                }
                else {
                    Swal.fire({
                        html: 'Error Occurred !!!',
                        icon: 'error'
                    });
                    $("[id*=preloader]").hide();
                    return false;
                }

                BindExamComponent();
                ClearExamComponent();
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
    catch (ex) {
    }
});

function BindExamComponent() {
    try {
        Obj = {};
        Obj.PatterNo = $("#ctl00_ContentPlaceHolder1_ddlPattern").val();
        Obj.Examtypeno = $("#ctl00_ContentPlaceHolder1_ddlExamType2").val();
        $.ajax({
            url: "../Exam/Exam_Pattern.aspx/GetExamCompntDetails",
            type: "POST",
            dataType: "json",
            data: JSON.stringify(Obj),
            beforeSend: function () { $("[id*=preloader]").show(); },
            complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                if (response.d == '') {
                    RemoveTableDynamically('#BindDynamicExamComponentsTable');
                    $("#divcomponents").hide();
                    $("[id*=preloader]").hide();
                    return false;

                } else {
                    $("#divcomponents").show();
                    //str = '<thead> <tr><th><span class="Action">Action</span></th><th><span class="ExamPattern">Exam Pattern</span></th><th><span class="ExamName">Exam Name</span></th><th><span class="SubjectType">Subject Type</span></th><th><span class="ComponentType">Component Name</span></th><th><span class="Createdby">Created/Modify By</span></th><th><span class="CreatedDate">Created/Modify Date</span></th><th><span class="Status">Status</span></th></tr></thead><tbody>';
                    str = '<thead> <tr><th><span class="Action">Action</span></th><th><span class="ExamPattern">Exam Pattern</span></th><th><span class="ExamName">Exam Name</span></th><th><span class="ComponentName">Component Name</span></th><th><span class="Createdby">Created/Modify By</span></th><th><span class="CreatedDate">Created/Modify Date</span></th><th><span class="Status">Status</span></th></tr></thead><tbody>';

                    $.each(response.d, function (index, GetValue) {
                        str = str + '<tr class="compcheck">'
                        str = str + '<td><a id="btnEditCompnent" class="fa fa-pencil-square-o text-primary" title="Edit Record" href="#" onclick="EditCompnent(this)"></a>' +
                            '<input type="hidden" id="hdntblCompId" value="' + GetValue.EXAMCOMNO + '" /><input type="hidden" id="hdnpattornno" value="' + GetValue.PATTERNNO + '"><input type="hidden" id="hdnexamno" value="' + GetValue.EXAMNO + '"><input type="hidden" id="hdncomname" value="' + GetValue.COMPONAME + '"><input type="hidden" id="hdnsubno" value="' + GetValue.SUBID + '"><input type="hidden" id="hdnexamtype" value="' + GetValue.EXAMTYPENO + '"></td>'
                        str = str + '<td>' + GetValue.PATTERN_NAME + '</td>'
                        str = str + '<td>' + GetValue.EXAMNAME + ' </td>'
                        //str = str + '<td>' + GetValue.SUBNAME + ' </td>'
                        str = str + '<td>' + GetValue.COMPONAME + ' </td>'
                        str = str + '<td>' + GetValue.CREMODIBY + '</td>'
                        str = str + '<td>' + GetValue.CREMODIDATE + '</td>'
                        if (GetValue.ACTIVE == 1) {
                            str = str + '<td><span class="badge badge-success">Active</span>' +
                                '<input type="hidden" id="hdnComponetStatus" value="true"/></td>'
                        }
                        else {
                            str = str + '<td><span class="badge badge-danger">Inactive</span>' +
                                '<input type="hidden" id="hdnComponetStatus" value="false"/></td>'
                        }

                        str = str + '</tr>'
                    });
                    str = str + '</tbody>';
                    RemoveTableDynamically('#BindDynamicExamComponentsTable');
                    $("#BindDynamicExamComponentsTable").append(str);
                    var BindDynamicExamPatternTable = $('#BindDynamicExamComponentsTable')
                    commonDatatable(BindDynamicExamPatternTable)
                    // BindtableLabelsDyanamically();
                }
            },
            error: function (errResponse) {
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

$('#btnComponentClear').click(function () {
    ClearExamComponent();
});

function ClearExamComponent() {
    $('#hdnexamname').val('0');
    $('#hdnComponetno').val('0');
    $('#txtComponentName').val('');
    $('#StatusComponent').prop('checked', true);
    $("#ctl00_ContentPlaceHolder1_ddlPattern").val(0).change();
    $("#ctl00_ContentPlaceHolder1_ddlExamName").val(0).change();
    $("#ctl00_ContentPlaceHolder1_ddlExamType2").val(0).change();
    $("#ctl00_ContentPlaceHolder1_ddlSubjectType").val(0).change();
    $("#btnComponentSubmit").val('Submit');
    $("#ctl00_ContentPlaceHolder1_ddlPattern").prop('disabled', false);
    $("#ctl00_ContentPlaceHolder1_ddlExamName").prop('disabled', false);
    // $("#ctl00_ContentPlaceHolder1_ddlSubjectType").prop('disabled', true);
    $("#ctl00_ContentPlaceHolder1_ddlExamType2").prop('disabled', false);
}

function EditCompnent(ClickValue) {
    try {
        var td = $("td", $(ClickValue).closest("tr"));
        $('#hdnComponetno').val($("[id*=hdntblCompId]", td).val());
        var pattornno = $("[id*=hdnpattornno]", td).val();
        var hdnexamno = $("[id*=hdnexamno]", td).val();
        var subjectno = $("[id*=hdnsubno]", td).val();
        var examtypeno = $("[id*=hdnexamtype]", td).val();

        $("#ctl00_ContentPlaceHolder1_ddlPattern").val(pattornno).change();
        $("#ctl00_ContentPlaceHolder1_ddlExamName").val(hdnexamno).change();
        $("#ctl00_ContentPlaceHolder1_ddlSubjectType").val(subjectno).change();
        $("#ctl00_ContentPlaceHolder1_ddlExamType2").val(examtypeno).change();
        $("#hdnexamname").val(hdnexamno);
        $("#txtComponentName").val(td[3].innerText);
        $("#btnComponentSubmit").val('Update');
        if ($("[id*=hdnComponetStatus]", td).val() == "false")
            $('#StatusComponent').prop('checked', false);
        else
            $('#StatusComponent').prop('checked', true);

        $("#ctl00_ContentPlaceHolder1_ddlPattern").prop('disabled', true);
        $("#ctl00_ContentPlaceHolder1_ddlExamName").prop('disabled', true);
       // $("#ctl00_ContentPlaceHolder1_ddlSubjectType").prop('disabled', true);
        $("#ctl00_ContentPlaceHolder1_ddlExamType2").prop('disabled', true);
    }
    catch (ex) {
    }
}
//--------- End Exam Componenet-----------//


//--------- Start Exam Curriculum Tagging -----------// 

$('#ctl00_ContentPlaceHolder1_ddlCollege').on('change', function (e) {
    try {
        var selectedValue = $("option:selected", this);
        var selectedText = this.value;
        BindCurriculum(selectedText, "");
    }
    catch (ex) {
    }
});
function BindCurriculum(selectedText, setvalue) {
    var Obj = {};
    Obj.CollegeId = selectedText;
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../Exam/Exam_Pattern.aspx/GetCurriculum",
        data: JSON.stringify(Obj),
        dataType: "json",
        success: function (result) {
            $("[id*=preloader]").show();
            $('#ctl00_ContentPlaceHolder1_ddlCurriculum').empty();
            $.each(result.d, function (key, value) {
                $("#ctl00_ContentPlaceHolder1_ddlCurriculum").append($("<option></option>").val(value.SCHEMENO).html(value.SCHEMENAME));
            });
            if (setvalue != "") {
                $("#ctl00_ContentPlaceHolder1_ddlCurriculum").val(setvalue);
            }
            $("[id*=preloader]").hide();
        },
        error: function ajaxError(result) {
            Swal.fire({
                html: 'Error Occurred !!!',
                icon: 'error'
            });
            $("[id*=preloader]").hide();
            return false;
            // alert(result.status + ' : ' + result.statusText);
        }
    });
}
$('#btnAddCurriculum').click(function () {
    try {

        var msg = ''; var str = ""; var count = 0;

        if ($("#ctl00_ContentPlaceHolder1_ddlCurriculumPattern").val() == '0' || $("#ctl00_ContentPlaceHolder1_ddlCurriculumPattern").val() == '')
            msg += "\r Please select Exam Pattern !!!</br>";


        if ($("#ctl00_ContentPlaceHolder1_ddlCollege").val() == '0')
            msg += "\r Please select College !!!</br>";

        if ($("#ctl00_ContentPlaceHolder1_ddlCurriculum").val() == '')
            msg += "\r Please select  Curriculum !!!</br>";

        var ddlCurriculumxcheck = $("#ctl00_ContentPlaceHolder1_ddlCurriculum").val();
        $('tr.curriculumclass').each(function () {
            var hexampattid = $(this).find('#hdnexampattid').val();
            var hcollegeid = $(this).find('#hdncollegeid').val();
            var hscheme = $(this).find('#hdnscheme').val();
            for (i = 0; i < ddlCurriculumxcheck.length; ++i) {
                if (hexampattid == $("#ctl00_ContentPlaceHolder1_ddlCurriculumPattern").val() && hcollegeid == $("#ctl00_ContentPlaceHolder1_ddlCollege").val() && hscheme == ddlCurriculumxcheck[i]) {
                    msg += "\r  Exam Pattern  is already tag to curriculum  !!!";
                }
            }
        });
        if (msg != '') {
            iziToast.warning({
                message: msg,
            });
            return false;
        }
        Obj = {};
        var multiSelectElement = document.querySelector('#ctl00_ContentPlaceHolder1_ddlCurriculum');
        if (multiSelectElement.multiple) {
            var StrSCHEMENO = "";
            VarSCHEMENO = $("#ctl00_ContentPlaceHolder1_ddlCurriculum").val();

            for (var i = 0; i < VarSCHEMENO.length; i++) {
                StrSCHEMENO += VarSCHEMENO[i] + ',';
            }
            StrSCHEMENO = StrSCHEMENO.slice(0, -1);
        } else {
            StrSCHEMENO = $("#ctl00_ContentPlaceHolder1_ddlCurriculum").val();
        }

        Obj.PATTERNNO = $("#ctl00_ContentPlaceHolder1_ddlCurriculumPattern").val();
        Obj.SCHEMENO = StrSCHEMENO;
        Obj.COLLEGE_ID = $('#ctl00_ContentPlaceHolder1_ddlCollege').val();
        Obj.ESCHEMENO = $('#hdnEschemeNo').val();
        $.ajax({
            url: "../Exam/Exam_Pattern.aspx/ExamCurriculum_IP",
            type: "POST",
            data: JSON.stringify(Obj),
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                iziToast.success({
                    message: 'Curriculum Tag Successfully !!!',
                });
                BindCurriculumtag();
                ClearExamCurriculum();
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
    catch (ex) {
    }
});
function BindCurriculumtag() {
    try {
        $.ajax({
            url: "../Exam/Exam_Pattern.aspx/BindCurriculum",
            type: "POST",
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            // complete: function () { $("[id*=preloader]").hide(); },
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                if (response.d == '') {
                    //Swal.fire({
                    //    //html: 'Record Not Found !!!',
                    //    //html: 'Record Not Found !!!',
                    //});
                    $("[id*=preloader]").hide();
                    return false;
                } else {
                    str = '<thead> <tr><th><span class="Action">Action</span></th><th><span class="ExamPattern">Exam Pattern </span></th><th><span class="CollegeName">College Name </span></th> <th><span class="Curriculum">Curriculum </span></th>  <th><span class="Createdby">Created/Modify By</span></th><th><span class="CreatedDate">Created/Modify Date</span></th> </tr></thead><tbody>';
                    $.each(response.d, function (index, GetValue) {
                        str = str + '<tr class="curriculumclass">'
                        str = str + '<td><a id="btnEditCurriculum" class="fa fa-pencil-square-o text-primary" title="Edit Record" href="#" onclick="EditCurriculum(this)"></a>' +
                            '<input type="hidden" id="hdnexampattid" value="' + GetValue.PATTERNNO + '"/><input type="hidden" id="hdncollegeid" value="' + GetValue.COLLEGE_ID + '"/><input type="hidden" id="hdnscheme" value="' + GetValue.SCHEMENO + '"/></td>'
                        str = str + '<td>' + GetValue.pattern_name + '</td>'
                        str = str + '<td>' + GetValue.college_name + '</td>'
                        str = str + '<td>' + GetValue.schemename + '</td>'
                        str = str + '<td>' + GetValue.CREMODIBY + '</td>'
                        str = str + '<td>' + GetValue.CREMODIDATE + '</td>'
                        str = str + '</tr>'
                    });
                    str = str + '</tbody>';
                    RemoveTableDynamically('#tblCarriculum');
                    $("#tblCarriculum").append(str);
                    var BindCarriculum = $('#tblCarriculum')
                    commonDatatable(BindCarriculum);
                    // BindtableLabelsDyanamically();
                }
            },
            error: function (errResponse) {
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
$('#btnClearCurriculum').click(function () {
    ClearExamCurriculum();
});

function ClearExamCurriculum() {
    $("#ctl00_ContentPlaceHolder1_ddlCurriculumPattern").val(0).change();
    $("#ctl00_ContentPlaceHolder1_ddlCurriculum").val('');
    $("#ctl00_ContentPlaceHolder1_ddlCollege").val(0).change();
    $("#btnAddCurriculum").val('Submit');
    $("#hdnEschemeNo").val(0);
    $("#hdfbinddrop").val(0);
    var dropdown = document.getElementById('ctl00_ContentPlaceHolder1_ddlCurriculum');
    dropdown.setAttribute('multiple', 'multiple');
}



function EditCurriculum(ClickValue) {
    try {

        var td = $("td", $(ClickValue).closest("tr"));
        var hdnexampattid = $("[id*=hdnexampattid]", td).val();
        var hdncollegeid = $("[id*=hdncollegeid]", td).val();
        var hdnscheme = $("[id*=hdnscheme]", td).val();
        $("#hdnEschemeNo").val(hdnscheme);
        $("#ctl00_ContentPlaceHolder1_ddlCollege").val(hdncollegeid).change();
        BindCurriculum(hdncollegeid, hdnscheme);
        $("#ctl00_ContentPlaceHolder1_ddlCurriculumPattern").val(hdnexampattid).change();
        var dropdown = document.getElementById('ctl00_ContentPlaceHolder1_ddlCurriculum');
        dropdown.removeAttribute('multiple');
        $("#btnAddCurriculum").val('Update');
    }
    catch (ex) {
    }
}
//----------------------------get Subject Curriculum Mapping data Start------------------------------------//
$("#btnShowinc").click(function () {
    try {
        var str = "";
        var count = 0;
        var Obj = {};
        var msg = '';

        if ($("#ctl00_ContentPlaceHolder1_ddlExamInc").val() == '0')
            msg += "\r Please Select Exam Pattern !!! <br/>";
        //if ($("#ctl00_ContentPlaceHolder1_ddlModality").val() == '0')
        //    msg += "\r Please Select Learning Modality !!! <br/>";
        if (msg != '') {
            iziToast.warning({
                message: msg,
            });
            return false;
        }
        Obj.PaternNo = $("#ctl00_ContentPlaceHolder1_ddlExamInc").val();
        // Obj.ModalityNo = $("#ctl00_ContentPlaceHolder1_ddlModality").val();

        $.ajax({
            url: "../Exam/Exam_Pattern.aspx/BindIncData",
            type: "POST",
            data: JSON.stringify(Obj),
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                // if ($("#ctl00_ContentPlaceHolder1_ddlExamInc").val() != '0' && $("#ctl00_ContentPlaceHolder1_ddlModality").val() != '0') {
                if (response.d == '') {
                    Swal.fire({
                        html: 'Record Not Found !!!',
                        icon: 'question'
                    });
                    $("[id*=preloader]").hide();
                    $('#tableInc').hide();
                    return false;
                }
                else {
                    str = '<thead><tr><th>Sr.No.</th><th><span class="ClsExamComponent">Exam Component</span></th><th><span class="INC">INC</span></th><th><span class="ABS">ABS</span></th> <th><span class="HNA">HNA</span></th> <th><span class="DRP">DRP</span></th> <th><span class="WP">WP</span></th><th><span class="PASS">PASS</span></th><th><span class="NC">NC</span></th></tr></thead><tbody>';
                    //}
                }
                $.each(response.d, function (index, GetValue) {
                    count++;
                    str = str + '<tr>'
                    str = str + '<td>' + count + '<input type="hidden" id="HdfIncValue" value=' + GetValue.ExamCompNo + '/></td>'
                    str = str + '<td>' + GetValue.ExamComponentName + '</td>'
                    if (GetValue.Inc == true) {
                        str = str + '<td><input type="checkbox" id="checkInc" checked=true /></td>'
                    }
                    else {
                        str = str + '<td><input type="checkbox" id="checkInc" /></td>'
                    }

                    if (GetValue.Abs == true) {
                        str = str + '<td><input type="checkbox" id="checkAbs" checked=true /></td>'
                    }
                    else {
                        str = str + '<td><input type="checkbox" id="checkAbs"  /></td>'
                    }

                    if (GetValue.HNA == true) {
                        str = str + '<td><input type="checkbox" id="checkHNA" checked=true /></td>'
                    }
                    else {
                        str = str + '<td><input type="checkbox" id="checkHNA"  /></td>'
                    }

                    if (GetValue.DRP == true) {
                        str = str + '<td><input type="checkbox" id="checkDRP" checked=true /></td>'
                    }
                    else {
                        str = str + '<td><input type="checkbox" id="checkDRP"  /></td>'
                    }

                    if (GetValue.WP == true) {
                        str = str + '<td><input type="checkbox" id="checkWP" checked=true /></td>'
                    }
                    else {
                        str = str + '<td><input type="checkbox" id="checkWP"  /></td>'
                    }

                    if (GetValue.PASS == true) {
                        str = str + '<td><input type="checkbox" id="checkPASS" checked=true /></td>'
                    }
                    else {
                        str = str + '<td><input type="checkbox" id="checkPASS"  /></td>'
                    }

                    if (GetValue.NC == true) {
                        str = str + '<td><input type="checkbox" id="checkNC" checked=true /></td>'
                    }
                    else {
                        str = str + '<td><input type="checkbox" id="checkNC"  /></td>'
                    }

                    str = str + '</tr>'
                });
                $('#tableInc').show();
                str = str + '</tbody>';
                RemoveTableDynamically('#tblInc');
                $("#tblInc").append(str);
                var tblInc = $('#tblInc')
                commonDatatables(tblInc)
                //BindtableLabelsDyanamically();

            },
            error: function (errResponse) {
                aa
            }
        });
    }
    catch (ex) {

    }
});

//----------------------------get Subject Curriculum Mapping data End------------------------------------//
//----------------------------------get clear modality started------------------------------------------------------------------------//
$("#ctl00_ContentPlaceHolder1_ddlExamInc").change(function () {
    //  $("#ctl00_ContentPlaceHolder1_ddlModality").val(0).change();
    $('#tableInc').hide();
});

//-----------------------------------get clear modality end------------------------------------------------------------------------//

//-------------- clear curriculum Mapping started --------------------//
function ClearIns() {
    try {
        $("#ctl00_ContentPlaceHolder1_ddlExamInc").val(0).change();
        // $("#ctl00_ContentPlaceHolder1_ddlModality").val(0).change();
    }
    catch (ex) {

    }
}

$('#btnCancelInc').click(function () {
    try {
        ShowLoader('#btnCancelInc');
        ClearIns();
    }
    catch (ex) {

    }
});
//-------------- clear curriculum Mapping End --------------------//
//-----------------Submit Inc Started --------------------//
function SaveAverage() {
    var data = [];
    var VarPatternNo = $("#ctl00_ContentPlaceHolder1_ddlExamInc").val();
    //var varModalityNo = $("#ctl00_ContentPlaceHolder1_ddlModality").val();
    $('#tblInc tbody tr').each(function () {

        //var row = $(this).closest("tr")[0];
        //var colCount = $("#tblInc tr th").length;
        var VarInc = ""; var VarAbs = ""; var VarHNA = ""; var VarDRP = ""; var VarWP = ""; var VarExamCompNo = ""; var VarPASS = ""; var VarNC = ""
        // valno++;
        var isChecked = $(this).find('#checkInc').prop('checked');
        if (isChecked == true) {
            Active = 1;
        }
        else {
            Active = 0;
        }
        VarInc += Active + '$';

        var isChecked = $(this).find('#checkAbs').prop('checked');
        if (isChecked == true) {
            Active = 1;
        }
        else {
            Active = 0;
        }
        VarAbs += Active + '$';


        var isCheckedHNA = $(this).find('#checkHNA').prop('checked');
        if (isCheckedHNA == true) {
            Active = 1;
        }
        else {
            Active = 0;
        }
        VarHNA += Active + '$';

        var isCheckedDRP = $(this).find('#checkDRP').prop('checked');
        if (isCheckedDRP == true) {
            Active = 1;
        }
        else {
            Active = 0;
        }
        VarDRP += Active + '$';

        var isCheckedWP = $(this).find('#checkWP').prop('checked');
        if (isCheckedWP == true) {
            Active = 1;
        }
        else {
            Active = 0;
        }
        VarWP += Active + '$';

        var isCheckedPASS = $(this).find('#checkPASS').prop('checked');
        if (isCheckedPASS == true) {
            Active = 1;
        }
        else {
            Active = 0;
        }
        VarPASS += Active + '$';

        var isCheckedNC = $(this).find('#checkNC').prop('checked');
        if (isCheckedNC == true) {
            Active = 1;
        }
        else {
            Active = 0;
        }
        VarNC += Active + '$';
        // valno = VarAbs;

        var x = $(this).find('#HdfIncValue').val();
        VarExamCompNo += x + '$';
        VarExamCompNo = VarExamCompNo.slice(0, -1);
        var alldata = {
            'PATTERNNO': VarPatternNo,
            'EXAMCOMNO': VarExamCompNo,
            'INCFLAG': VarInc,
            'ABSFLAG': VarAbs,
            'HNAFLAG': VarHNA,
            'DRPFLAG': VarDRP,
            'WPFLAG': VarWP,
            'PASSFLAG': VarPASS,
            'NCFLAG': VarNC
        }
        data.push(alldata);
    });
    //  console.log(data);//  
    //'MODALITYNO': varModalityNo,//
    return data;
}
$("#btnSubmitInc").click(function () {
    var data = "";
    var count = 0;
    var msg = ''; var str = "";
    if ($("#ctl00_ContentPlaceHolder1_ddlExamInc").val() == '0')
        msg += "\r Please Select Exam Pattern !!! <br/>";
    //if ($("#ctl00_ContentPlaceHolder1_ddlModality").val() == '0')
    //    msg += "\r Please Select Learning Modality !!! <br/>";

    if (msg != '') {
        iziToast.warning({
            message: msg,
        });
        return false;
    }
    data = JSON.stringify(SaveAverage());
    var posdata = JSON.stringify({ 'IncData': data });
    $.ajax({
        url: "../Exam/Exam_Pattern.aspx/InsertIncData",
        type: "POST",
        dataType: 'json',
        beforeSend: function () { $("[id*=preloader]").show(); },
        contentType: "application/json;charset=utf-8",
        data: posdata,

        success: function (response) {
            str = '<thead><tr><th>Sr.No.</th><th><span class="ClsExamComponent">Exam Component</span></th><th><span class="INC">INC</span></th><th><span class="ABS">ABS</span></th> <th><span class="HNA">HNA</span></th> <th><span class="DRP">DRP</span></th> <th><span class="WP">WP</span></th></tr></thead><tbody>';
            $.each(response.d, function (index, GetValue) {
                if (count == 0) {
                    if (GetValue.CheckStatus == "1") {
                        iziToast.success({
                            message: 'Record Added Successfully !!!',
                        });
                        $('#tableInc').show();
                    }
                    else {
                        Swal.fire({
                            html: 'Error Occurred !!!',
                            icon: 'error'
                        });
                        $("[id*=preloader]").hide();
                        $('#tableInc').hide();
                        return false;
                    }
                }
                count++;
                str = str + '<tr>'
                str = str + '<td>' + count + '<input type="hidden" id="HdfIncValue" value=' + GetValue.ExamCompNo + '/></td>'
                str = str + '<td>' + GetValue.ExamComponentName + '</td>'
                if (GetValue.Inc == true) {
                    str = str + '<td><input type="checkbox" id="checkInc" checked=true /></td>'
                }
                else {
                    str = str + '<td><input type="checkbox" id="checkInc" /></td>'
                }
                if (GetValue.Abs == true) {
                    str = str + '<td><input type="checkbox" id="checkAbs" checked=true /></td>'
                }
                else {
                    str = str + '<td><input type="checkbox" id="checkAbs"  /></td>'
                }
                if (GetValue.HNA == true) {
                    str = str + '<td><input type="checkbox" id="checkHNA" checked=true /></td>'
                }
                else {
                    str = str + '<td><input type="checkbox" id="checkHNA"  /></td>'
                }
                if (GetValue.DRP == true) {
                    str = str + '<td><input type="checkbox" id="checkDRP" checked=true /></td>'
                }
                else {
                    str = str + '<td><input type="checkbox" id="checkDRP"  /></td>'
                }

                if (GetValue.WP == true) {
                    str = str + '<td><input type="checkbox" id="checkWP" checked=true /></td>'
                }
                else {
                    str = str + '<td><input type="checkbox" id="checkWP"  /></td>'
                }

                if (GetValue.PASS == true) {
                    str = str + '<td><input type="checkbox" id="checkPASS" checked=true /></td>'
                }
                else {
                    str = str + '<td><input type="checkbox" id="checkPASS"  /></td>'
                }

                if (GetValue.NC == true) {
                    str = str + '<td><input type="checkbox" id="checkNC" checked=true /></td>'
                }
                else {
                    str = str + '<td><input type="checkbox" id="checkNC"  /></td>'
                }
                str = str + '</tr>'
            });
            str = str + '</tbody>';

            $('#tableInc').show();
            str = str + '</tbody>';
            RemoveTableDynamically('#tblInc');
            $("#tblInc").append(str);
            var tblInc = $('#tblInc')
            commonDatatables(tblInc)
            //BindtableLabelsDyanamically();
        },
        error: function (errResponse) {

        }
    });
});

//----------------Submit Inc  end ------------------//
//--------- End Exam Carriculum Tagging -----------//