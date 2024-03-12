//===============================================//
// MODULE NAME   : RFC ERP 
// PAGE NAME     : INC Completion Apply
// CREATION DATE : 
// CREATED BY    : Gaurav Kalbande
// Modified BY   : Ekansh Moundekar
// Modified Date : 
//===============================================//


var liveurl = "../../../";
var localurl = "../../";
var Campusno = 0;
var Schemeno = 0;
var branchno = 0;
var SemesterNO = 0;
var College_id = 0;
var short_name = 0;
var CourseNo = 0;
var DegreeNo = 0;
var ADMBatchNO = 0;
var DAmount = 0;
var Recon;
var Total_Amount = 0;
var countrcon=0;


function commonDatatabless(tablessID){
    //-- Set time out function for dynamic column visibility --// 
    setTimeout(function () { 

        var table = $(tablessID).DataTable({
            responsive: true,
            lengthChange: true,
            fixedColumns:   {
                left:0,
                right: 1
            },
            dom: 'lBfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',

                    extend: 'excelHtml5',
                    exportOptions: {
                        columns: function (idx, data, node) {
                            var arr = [0,8];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $(tablessID).DataTable().column(idx).visible();
                            }
                        },
                        format: {
                            body: function (data, column, row, node) {
                                var nodereturn;
                                if ($(node).find("input:text").length > 0) {
                                    nodereturn = "";
                                    nodereturn += $(node).find("input:text").eq(0).val();
                                }
                                else if ($(node).find("textarea").length > 0) {
                                    nodereturn = "";
                                    $(node).find("textarea").each(function () {
                                        nodereturn += $(this).text();
                                    });
                                }
                                else if ($(node).find("input:checkbox").length > 0) {
                                    nodereturn = "";
                                    $(node).find("input:checkbox").each(function () {
                                        if ($(this).is(':checked')) {
                                            nodereturn += "On";
                                        } else {
                                            nodereturn += "Off";
                                        }
                                    });
                                }
                                else if ($(node).find("a").length > 0) {
                                    nodereturn = "";
                                    $(node).find("a").each(function () {
                                        nodereturn += $(this).text();
                                    });
                                }
                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                    nodereturn = "";
                                    $(node).find("span").each(function () {
                                        nodereturn += $(this).text();
                                    });
                                }
                                else if ($(node).find("select").length > 0) {
                                    nodereturn = "";
                                    $(node).find("select").each(function () {
                                        var thisOption = $(this).find("option:selected").text();
                                        if (thisOption !== "Please Select") {
                                            nodereturn += thisOption;
                                        }
                                    });
                                }
                                else if ($(node).find("img").length > 0) {
                                    nodereturn = "";
                                }
                                else if ($(node).find("input:hidden").length > 0) {
                                    nodereturn = "";
                                }
                                else {
                                    nodereturn = data;
                                }
                                return nodereturn;
                            },
                        },
                    }
                },
            ],
            "bDestroy": true,
        });

        //------------ preloader hide after data table load -------------//
        $("[id*=preloader]").hide();
        //return true;

        //-----------------------------------------------------------//
        $(tablessID).wrap("<div class='table-responsive'></div>");

    },10);
    
}


$(document).ready(function () {
    $("#divNote").hide();
    GetPersonalDetails();
    $('#paymentoption').hide();
    $('#approveBox').hide();
    $("#btnApply").hide();
    $("#BindDynamicPaymenttbl").hide();
    var DAmount = 0;
    $(document).on("change", "input[type=checkbox]:checked", function () { 
        var amount = 0;
        var ind = 0; 
        
        for (var i = 0; i < countrcon; i++) {
            
            if ($('#hdncheckrecon' + i).val() == 'False' && $('#chk'+i).is(':checked')) {
                var checkboxAmount = parseInt($(this).siblings("#hdnAmount").val()); 
                amount += checkboxAmount;
            }
            ind = i; 
        }
        Total_Amount = amount;
        $("#lblfees").html(amount);
    });

    //$(document).on("change", "input[type=checkbox]:checked:not(:disabled)", function () {
        
    //    var amount = 0;
    //    $("input[type=checkbox]:checked:not(:disabled)").each(function () {
    //        var checkboxAmount = parseInt($(this).siblings("#hdnAmount").val());
    //        amount += checkboxAmount;
    //    });
    //    Total_Amount = amount;
    //});

    //$(document).on("change", "input[type=checkbox]:checked", function () { 
       
    //    if ($(this).is('#selectall'))  
    //    {
    //    }
    //    else
    //    {
               
    //            var uncheckedCheckboxAmount = parseInt($(this).siblings("#hdnAmount").val());
    //            var currentAmount = parseInt($('#lblfees').html());
    //            var updatedAmount = currentAmount - uncheckedCheckboxAmount;
    //            $('#lblfees').html(updatedAmount);
    //            Total_Amount = updatedAmount;
             
    //    }
       
        
    //});
    $(document).on("change", "input[type=checkbox]:not(:checked)", function () {
 
        var uncheckedCheckboxAmount = parseInt($(this).siblings("#hdnAmount").val());
        var currentAmount = parseInt($('#lblfees').html());
        var updatedAmount = currentAmount - uncheckedCheckboxAmount;
        $('#lblfees').html(updatedAmount);
        Total_Amount = updatedAmount;
    });

   

    //$(document).on("change", "#selectall", function () {
    //    var amount = 0;
    //    var ind=0;
    //    var checkboxes = $("tbody input[type=checkbox]");
    //    checkboxes.prop("checked", this.checked);
    //    checkboxes.each(function () {
    //        if (this.checked) {
    //            if($('#hdncheckrecon'+ind+'').val() == 'False'){
    //                var checkboxAmount = parseInt($(this).siblings("#hdnAmount").val());
    //                amount += checkboxAmount; 
    //            }
    //            ind++;
    //        }
    //    });
    //    $('#lblfees').html(amount);
    //    Total_Amount = amount;
    //});


  

    //$(document).on("change", "#selectall", function () { 
    //    var checkboxes = $("tbody input[type=checkbox]:not(:disabled)");
    //    var allChecked = checkboxes.filter(":not(:disabled)").length === checkboxes.filter(":checked:not(:disabled)").length;
    //    var updatedAmount=0;
    //    if (allChecked) {
    //        var amount = 0;
    //        var ind = 0;
    //        for (var i = 0; i < countrcon; i++) {
    //            if ($('#hdn_incStatus').val()==0) { 

    //                var uncheckedCheckboxAmount = parseInt($(this).siblings("#hdnAmount").val());  
    //            }
    //            ind = i; 
    //        }
    //        // Update the total amount
    //        $('#lblfees').html(updatedAmount);
    //        Total_Amount = updatedAmount;
    //    }
    //});



   
    //$(document).on("change", "#selectall", function () {
    
    //    var checkboxes = $("tbody input[type=checkbox]:not(:disabled)");
    //    var amount = 0;
       
    //    if (checkboxes.length > 0) {
    //        checkboxes.prop("checked", this.checked);

    //        checkboxes.each(function () {
    //            if (this.checked) {
    //                amount += parseInt($(this).siblings("#hdnAmount").val());
    //            }
    //        });
    //        $('#lblfees').html(amount);
    //        Total_Amount = amount;
    //    }
    //});

});

function GetPersonalDetails() {
    var idno = $('#ctl00_ContentPlaceHolder1_hdnClientId').val();
    var obj = {};
    obj.Idnos = idno;
    $.ajax({
        url: localurl + "Exam/INC_Completion_Apply.aspx/GetPersonalDetails",
        type: "POST",
        data: JSON.stringify(obj),
        dataType: "json",
        async: false,
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response.d == '') {
            }
            else {
                $.each(response.d, function (index, GetValue) {
                    $('#lblStudentID').html(GetValue.StudentID)
                    $('#lblStudentName').html(GetValue.StudentName);
                    $('#lblCampus').html(GetValue.campus);
                    $('#lblCollege').html(GetValue.College);
                    $('#lblCourse').html(GetValue.Course);
                    $('#lblCurriculum').html(GetValue.Curriculum);
                    $('#lblSemester').html(GetValue.Semester);
                    College_id = GetValue.College_Id;
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

$('#ctl00_ContentPlaceHolder1_ddlAcademicSession').on('change', function () {
    BindList();
    checkbuttonHide();
    BindPaymentStatusTable();
});

function checkbuttonHide(){
    var rowcount = 0;
    var count = 0;
    $("tr.Trclass").each(function(){
        var td = $(this).closest("tr")[0];
        var Inc_status = $(td).find("input[type=checkbox]").is(":checked:disabled") ? 1 : 0;
        if(Inc_status == 1){
            count++;
        }
        rowcount++;

    });

    if(rowcount == count){
        $("#btnApply").hide();
        $('#boxPayCash').hide();
    }
}

function BindList() {
    var StartDate; var EndDate;
    var idno = $('#ctl00_ContentPlaceHolder1_hdnClientId').val();
    var obj = {};
    obj.Idnos = idno;
    obj.Session = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
    $.ajax({
        url: localurl + "Exam/INC_Completion_Apply.aspx/GetStudentData",
        type: "POST",
        data: JSON.stringify(obj),
        dataType: "json",
        async: false,
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response.d == '') {
                Swal.fire({
                    html: 'Record Not Found !!!',
                    icon: 'question'
                });
                $("#BindDynamicStudentINCTable").hide();
                $("[id*=preloader]").hide();
                $('#paymentoption').hide();
                $('#approveBox').hide();
                $("#lblIncStartDate").hide();
                $("#lblIncEndDate").hide();
                $("#btnApply").hide();
                $("#divNote").hide();
                $("#BindDynamicPaymenttbl").hide();
                $(".buttons-excel").hide();
                $(".dataTables_filter").hide();
                return false;
            }
            else {
              
                $.each(response.d, function (index, GetValue) {
                    StartDate = GetValue.INCStartDate;
                    EndDate = GetValue.INCEndDate;
                    SemesterNO = GetValue.SemesterNo;
                    short_name = GetValue.Short_Name;
                    CourseNo = GetValue.CourseNo;
                    DegreeNo = GetValue.DegreeNo;
                    ADMBatchNO = GetValue.ADMBatchNO;
                    DmdAmount = GetValue.Total_Amt;
                    Recon = GetValue.Recon_flag;
                    $('#lblfees').html(DmdAmount);
                    $("#divNote").hide();
                    var today = new Date();
                    var TodaysDate = today.toISOString().slice(0, 10);
                    if (TodaysDate >= StartDate && TodaysDate <= EndDate) {
                      
                        if(GetValue.Amount == 0){
                            $("#btnApply").show();
                            $('#boxPayCash').hide();
                        }
                        else{
                            $("#btnApply").hide();
                            $('#boxPayCash').show();
                        }
                        //  if (GetValue.Recon_flag == "False") {
                       
                        $("#BindDynamicStudentINCTable").show();
                        $('#paymentoption').show();
                        $('#approveBox').show();
                        $("#lblIncStartDate").show();
                        $("#lblIncEndDate").show();
                        $("#BindDynamicPaymenttbl").show();
                        $(".buttons-excel").show();
                        $(".dataTables_filter").show();
                        str = '<thead><tr><th></th><th><span class="SubjectCode">Subject Code</span></th><th><span class="SubjectName">Subject Name</span></th><th><span class="Teacher">Teacher Name</span></th><th><span class="ClsGrades">Grade</span></th><th><span class="Status">Status</span></th></tr></thead><tbody>';
                        $.each(response.d, function (index, GetValue) {
                            var checkboxId = "chk" + index;
                            str = str + '<tr class="Trclass">'
                            str = str + '<td> <input type="checkbox" id ="' + checkboxId + '"/><input type="hidden" id="hdncheckrecon'+index+'" value="' + GetValue.Recon_flag + '"/><input type="hidden" id="hdnAmount" value="' + GetValue.Amount + '"/><input type="hidden" id="hdnccode" value="' + GetValue.CCODE + '"/><input type="hidden" id="hdncourse" value="' + GetValue.Course + '"/><input type="hidden" id="hdnTeacher" value="' + GetValue.TeacherName + '"/><input type="hidden" id="hdnShortName" value="' + GetValue.Short_Name + '"/><input type="hidden" id="hdnSemesterNo" value="' + GetValue.SemesterNo + '"/><input type="hidden" id="hdnCourseNo" value="' + GetValue.BranchNo + '"/><input type="hidden" id="hdnSchemeno" value="' + GetValue.Schemeno + '"/><input type="hidden" id="hdnCampusno" value="' + GetValue.CampusNo + '"/><input type="hidden" id="hdnBranchNo" value="' + GetValue.CourseNo + '"/><input type="hidden" id="hdnDegreeNo" value="' + GetValue.DegreeNo + '"/><input type="hidden" id="hdnADMBatchNO" value="' + GetValue.ADMBatchNO + '"/><input type="hidden" id="hdnINC_Payment_ID" value="' + GetValue.INC_payment_ID + '"/><input type="hidden" id="hdn_incStatus" value="' + GetValue.Status + '"/></td>'
                           
                            str = str + '<td>' + GetValue.CCODE + '</td>'
                            str = str + '<td>' + GetValue.Course + '</td>'
                            str = str + '<td>' + GetValue.TeacherName + '</td>'
                            str = str + '<td>' + GetValue.Short_Name + '</td>'
                            if (GetValue.Status == 0) {
                                str = str + '<td><span class="badge badge-warning">Not Applied</span>' +
                                  '<input type="hidden" id="ActivityStatus" value="true"/></td>'
                                str = str + '<script>$("#chk").prop("checked", false);</script>';
                            }
                            else {

                                if(GetValue.Faculty_Flag == 2 && GetValue.Dean_Flag == 2){
                                    str = str + '<td><span class="badge badge-success">Applied</span>' +
                                   '<input type="hidden" id="ActivityStatus" value="true"/></td>'
                                    str += '<script>$("#' + checkboxId + '").prop("disabled", true);</script>';
                                }
                                else if(GetValue.Faculty_Flag == 1){
                                    if(GetValue.Dean_Flag == 1){
                                        str = str + '<td><span class="badge badge-success">Approved by Dean</span>' +
                                  '<input type="hidden" id="ActivityStatus" value="true"/></td>'
                                        str += '<script>$("#' + checkboxId + '").prop("disabled", true);</script>';
                                    }
                                    else{
                                        if(GetValue.Dean_Flag == 0){
                                            str = str + '<td><span class="badge badge-danger">Rejected by Dean</span>' +
                                      '<input type="hidden" id="ActivityStatus" value="true"/></td>'
                                            str += '<script>$("#' + checkboxId + '").prop("disabled", true);</script>';
                                        }
                                        else{
                                            str = str + '<td><span class="badge badge-success">Approved by Faculty</span>' +
                                       '<input type="hidden" id="ActivityStatus" value="true"/></td>'
                                            str += '<script>$("#' + checkboxId + '").prop("disabled", true);</script>';
                                        }
                                    }
                                }
                                else{
                                    str = str + '<td><span class="badge badge-danger">Rejected by Faculty</span>' +
                                    '<input type="hidden" id="ActivityStatus" value="true"/></td>'
                                    str += '<script>$("#' + checkboxId + '").prop("disabled", true);</script>';
                                }
                                str += '<script>$("#' + checkboxId + '").prop("checked", true);</script>';
                               
                                //if (GetValue.Recon_flag == "True") {
                                // //   str += '<script>$("#' + checkboxId + '").prop("disabled", true);</script>';
                                //    $("#divNote").hide();
                                       
                                //} else {
                          
                                //   // str += '<script>$("#' + checkboxId + '").prop("disabled", false);</script>';
                                   
                                //}

                            } 
                           
                        });
                        // }
                        //else{
                        //    BindTable(response);
                        //}
                        
                    }
                    else {
                        BindTable(response);
                    }
                
                    $("#lblIncStartDate").html(StartDate);
                    $("#lblIncEndDate").html(EndDate);
                    countrcon++;
                });
                str = str + '</tbody>';
                RemoveTableDynamically('#BindDynamicStudentINCTable');
                $("#BindDynamicStudentINCTable").append(str);
                $("[id*=preloader]").hide();
                
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

function BindTable(response){
    $("#BindDynamicStudentINCTable").show();
    $('#paymentoption').show();
    $('#approveBox').show();
    $('#boxPayCash').hide();
    $("#btnApply").hide();
   
    str = '<thead><tr><th><span class="SubjectCode">Subject Code</span></th><th><span class="SubjectName">Subject Name</span></th><th><span class="TeacherName">Teacher Name</span></th><th><span class="Grade">Grade</span></th><th><span class="Status">Status</span></th></tr></thead><tbody>';
    $.each(response.d, function (index, GetValue) {
        str = str + '<tr class="Trclass">'
        str = str + '<td>' + GetValue.CCODE + '</td>'
        str = str + '<td>' + GetValue.Course + '</td>'
        str = str + '<td>' + GetValue.TeacherName + '</td>'
        str = str + '<td>' + GetValue.Short_Name + '</td>'
        if (GetValue.Status == 1) {
            if(GetValue.Faculty_Flag == 2 && GetValue.Dean_Flag == 2){
                str = str + '<td><span class="badge badge-success">Applied</span>' +
               '<input type="hidden" id="ActivityStatus" value="true"/></td>'
                // str += '<script>$("#' + checkboxId + '").prop("disabled", true);</script>';
            }
            else if(GetValue.Faculty_Flag == 1){
                if(GetValue.Dean_Flag == 1){
                    str = str + '<td><span class="badge badge-success">Approved by Dean</span>' +
              '<input type="hidden" id="ActivityStatus" value="true"/></td>'
                    str += '<script>$("#' + checkboxId + '").prop("disabled", true);</script>';
                }
                else{
                    if(GetValue.Dean_Flag == 0){
                        str = str + '<td><span class="badge badge-danger">Rejected by Dean</span>' +
                  '<input type="hidden" id="ActivityStatus" value="true"/></td>'
                        str += '<script>$("#' + checkboxId + '").prop("disabled", true);</script>';
                    }
                    else{
                        str = str + '<td><span class="badge badge-success">Approved by Faculty</span>' +
                   '<input type="hidden" id="ActivityStatus" value="true"/></td>'
                        str += '<script>$("#' + checkboxId + '").prop("disabled", true);</script>';
                    }
                }
            }
            else{
                str = str + '<td><span class="badge badge-danger">Rejected by Faculty</span>' +
                '<input type="hidden" id="ActivityStatus" value="true"/></td>'
                str += '<script>$("#' + checkboxId + '").prop("disabled", true);</script>';
            }
            // str += '<script>$("#' + checkboxId + '").prop("checked", true);</script>';
        }
        else{
            str = str + '<td><span class="badge badge-warning">Not Applied</span>' +
                 '<input type="hidden" id="ActivityStatus" value="true"/></td>'
        }
        $("#lblIncStartDate").html(GetValue.INCStartDate).show();
        $("#lblIncEndDate").html(GetValue.INCEndDate).show();
    });
}

function BindPaymentStatusTable(){
    var idno = $('#ctl00_ContentPlaceHolder1_hdnClientId').val();
    var obj = {};
    obj.Idnos = idno;
    obj.Session = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
    $.ajax({
        url: localurl + "Exam/INC_Completion_Apply.aspx/GetSubjectPaymentStatus",
        type: "POST",
        data: JSON.stringify(obj),
        dataType: "json",
        async: false,
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if(response.d == ''){
                $("#BindDynamicPaymenttbl").hide();
            }
            else{
                var rownum = 0;
                $("#BindDynamicPaymenttbl").show();
                str = '<thead><tr><th>Sr.No.</th><th><span class="Registration No.">Registration No.</span></th><th><span class="ReceiptType">Receipt Type</span></th><th><span class="Date">Date</span></th><th><span class="SemesterName">Semester Name</span></th><th><span class="PayType">Pay Type</span></th><th><span class="Amount">Amount</span></th><th><span class="Status">Payment Status</span></th><th><span class="Print">Print</span></th></tr></thead><tbody>';  
                $.each(response.d,function(index,GetValue){
                    rownum = rownum + 1;
                    str = str + '<tr class="DrClass">'
                    str = str + '<td>' + rownum + '</td>'
                    str = str + '<td>' + GetValue.StudentID + '</td>'
                    str = str + '<td>' + GetValue.Short_Name + '</td>'
                    str = str + '<td>' + GetValue.INCStartDate + '</td>'
                    str = str + '<td>' + GetValue.Semester + '</td>'
                    if(GetValue.PayType == 1){
                        str = str + '<td><span class="">Offline</span></td>'
                    }
                    str = str + '<td>' + GetValue.Amount + '</td>'
                    if(GetValue.Recon_flag == 'True'){
                        if(GetValue.Amount == '0.00'){
                            str = str + '<td><span class="badge badge-success">Payment Done</span>' +
                         '<input type="hidden" id="ActivityStatus" value="true"/></td>'
                            str = str + '<td>' + "  _  " + '</td>'
                        }
                        else{
                            str = str + '<td><span class="badge badge-success">Payment Done</span>' +
                            '<input type="hidden" id="ActivityStatus" value="true"/></td>'
                            str = str + '<td><a id="PrintRecordActivity" class="fa fa-print text-success" title="Print" href="#" onclick="PrintRecord(this)"></a><input type="hidden" id="hdnDCR_NO'+index+'" value="' + GetValue.DCR_NO + '"/></td>'
                        }
                        
                    }
                    else{
                        str = str + '<td><span class="badge badge-warning">Payment Pending</span>' +
                          '<input type="hidden" id="ActivityStatus" value="true"/></td>'
                        str = str + '<td>' + "  _  " + '</td>'
                        $("#divNote").show();
                    }
                    
                });
                str = str + '</tbody>';
                RemoveTableDynamically('#BindDynamicPaymenttbl');
                $("#BindDynamicPaymenttbl").append(str);
                var BindDynamicPaymenttbl = $('#BindDynamicPaymenttbl')
                commonDatatabless(BindDynamicPaymenttbl);
                $("[id*=preloader]").hide();
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

$("#btnPayCash").click(function () {
    var msg1 = '';
    if ($("input[type='checkbox']:checked:not(:disabled)").length === 0 ) {
        msg1 += "\r Please select at least one subject !!! <br/>";
    }

    if (msg1 != '') {
        iziToast.warning({
            message: msg1,
        });
        return false;
    }

    var Obj = {};

    $("tr.Trclass").each(function () {
        $("input[type=checkbox]:checked:not(:disabled)").each(function () {
            var td = $(this).closest("tr")[0];
            Obj.Session = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
            Obj.SemesterNO = $("[id*=hdnSemesterNo]", td).val();
            Obj.CampusNo   = $("[id*=hdnCampusno]", td).val();
            Obj.Amount     = $("[id*=hdnAmount]", td).val();
            Obj.BranchNO = $("[id*=hdnBranchNo]", td).val();
            Obj.College_ID = College_id;
        });
    });

    $.ajax({
        url: localurl + "Exam/INC_Completion_Apply.aspx/CheckInspect",
        type: "POST",
        data: JSON.stringify(Obj),
        dataType: "json",
        async: false,
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if(response.d == 1){
                InsertRecord();
            }
            else{
                Swal.fire({
                    html: 'Something went wrong, please try again !!!',
                    icon: 'error'
                });
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
});

function InsertRecord(){
    var msg1 = '';
    if ($("input[type='checkbox']:checked:not(:disabled)").length === 0 ) {
        msg1 += "\r Please select at least one subject !!! <br/>";
    }

    if (msg1 != '') {
        iziToast.warning({
            message: msg1,
        });
        return false;
    }
    var delconf;
    Swal.fire({
        title: 'Are You Sure To Apply INC Completion For Selected Subject?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes',
        delconf: 'Yes'
    }).then((result) => {
        if (result.isConfirmed) {
       
    var Obj = {};
Obj.SessionNo = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
Obj.Idno = $('#ctl00_ContentPlaceHolder1_hdnClientId').val();
Obj.StudentName = $('#lblStudentName').html();
Obj.StudentID = $('#lblStudentID').html();
Obj.SemesterNo = SemesterNO;
Obj.PayTypeNo = 1;
Obj.f1=Total_Amount;
Obj.Total_amt = Total_Amount;
Obj.College_ID = College_id;
Obj.Short_Name = short_name;
Obj.CourseNO = CourseNo;
Obj.DegreeNO = DegreeNo ;
Obj.ADMBatchNO = ADMBatchNO;

$("tr.Trclass").each(function () {
    var td = $(this).closest("tr")[0];
    Obj.INC_ST = $(td).find("input[type=checkbox]").is(":checked") ? 1 : 0;
});

$.ajax({
    url: localurl + "Exam/INC_Completion_Apply.aspx/InsertStudentINCDemand",
    type: "POST",
    data: JSON.stringify(Obj),
    dataType: "json",
    async: false,
    beforeSend: function () { $("[id*=preloader]").show(); },
    complete: function () { $("[id*=preloader]").hide(); },
    contentType: "application/json;charset=utf-8",
    success: function (response) {
        if(response.d != ''){
                
            $.each(response.d, function (index, GetValue) {
                if (GetValue.Status == 2) {
                    Swal.fire({
                        html: 'Error Occurred !!!',
                        icon: 'error'
                    });    
                }
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

var data = JSON.stringify(BindINCData());
$.ajax({
    url: localurl + "Exam/INC_Completion_Apply.aspx/InsertStudentINCRecord",
    type: "POST",
    data: JSON.stringify({ 'INCData': data }),
    dataType: "json",
    async: false,
    beforeSend: function () {
        $("[id*=preloader]").show();
    },
    complete: function () {
        $("[id*=preloader]").hide();
    },
    contentType: "application/json;charset=utf-8",
    success: function (response) {
        if (response.d!=='') {
            $.each(response.d, function(index,GetValue){
                if(GetValue.INC_STATUS == 1){
                    Swal.fire(
                  'Submitted!',
                  'Record Submitted successfully.....',
                  'success'
                    )
                       
                    BindList();
                    $("#divFees").show();
                    BindPaymentStatusTable();
                    checkbuttonHide();
                    $('#lblfees').html(DAmount);
                    $("[id*=preloader]").hide();
                }
                else{
                    Swal.fire(
                      'Submitted!',
                      'Record Submitted successfully.....',
                      'success'
                        )
                       
                    BindList();
                    BindPaymentStatusTable();
                    $('#lblfees').html(DAmount);
                    $("[id*=preloader]").hide();
                }
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
} else {
    Swal.fire({
        html: response.d + '!!!',
        icon: 'error'
    });
    $("[id*=preloader]").hide();
    return false;
}
});
}

function BindINCData() {
    var data = [];
    var valno = 0;
    var idno = $('#ctl00_ContentPlaceHolder1_hdnClientId').val();
    var Idnos = idno;
    var Session = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
    var College_ID = College_id 
    DAmount = $('#lblfees').html();
        
    $("tr.Trclass").each(function () {
        //     $("input[type=checkbox]:not(:disabled)").each(function () {
        var td = $(this).closest("tr")[0];
        var Ccode = $("[id*=hdnccode]", td).val();
        var CourseName = $("[id*=hdncourse]", td).val();
        var TeacherName = $("[id*=hdnTeacher]", td).val();
        var ShortName = $("[id*=hdnShortName]", td).val();
        var SemesterNO = $("[id*=hdnSemesterNo]", td).val();
        var CampusNo   = $("[id*=hdnCampusno]", td).val();
        var SchemeNo   = $("[id*=hdnSchemeno]", td).val();
        var CourseNO   = $("[id*=hdnCourseNo]", td).val();
        var Amount     = $("[id*=hdnAmount]", td).val();
        var INC_Payment_ID = $("[id*=hdnINC_Payment_ID]", td).val();
        //  var Inc_status = $("input[type=checkbox]").is(":checked") ? 1 : 0;
        var Inc_status = $(td).find("input[type=checkbox]").is(":checked") ? 1 : 0;
        var Recon_Status = Recon == "False" ? 0 : 1;
        var BranchNO = $("[id*=hdnBranchNo]", td).val();

        var alldata = {
            'IDNO': Idnos,
            'SESSIONNO': Session,
            'CAMPUSNO': CampusNo,
            'COLLEGEID': College_ID,
            'SCHEMENO': SchemeNo,
            'COURSENO': CourseNO,
            'SEMESTERNO': SemesterNO,
            'CCODE': Ccode,
            'SHORTNAME': ShortName,
            'AMOUNT': Amount,
            'INC_STATUS' : Inc_status,
            'PAYMENT_STATUS':INC_Payment_ID,
            'RECON_STATUS':Recon_Status,
            'BRANCHNO' : BranchNO

        }
        data.push(alldata);
        // });
    });

    console.log(data);
    return data;
}

$("#btnApply").click(function(){
    var msg1 = '';
   
    if ($("input[type='checkbox']:checked").length === 0) {
        msg1 += "\r Please select at least one subject !!! <br/>";
    }

    if (msg1 != '') {
        iziToast.warning({
            message: msg1,
        });
        return false;
    }
   
    var delconf;
    Swal.fire({
        title: 'Are You Sure To Apply INC Completion For Selected Subject?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes',
        delconf: 'Yes'
    }).then((result) => {
        if (result.isConfirmed) {

         var dataINC = JSON.stringify(GetZeroAmount());
    $.ajax({
        url:localurl+"Exam/INC_Completion_Apply.aspx/ZeroAmountDataInsert",
        type: "POST",
        data: JSON.stringify({ 'ZeroData': dataINC }),
        dataType: "json",
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if(response.d != ''){
                $.each(response.d, function (index, GetValue) {
                    if (GetValue.INC_STATUS == 1) {
                        Swal.fire(
                                 'Submitted!',
                                 'Record Submitted successfully.....',
                                 'success'
                                 )
                        BindList();
                        BindPaymentStatusTable();
                        $("[id*=preloader]").hide();
                        $('#boxPayCash').hide();
                        checkbuttonHide();
                    }
                });
            }
        },
        error: function ajaxError(response) {
            Swal.fire({
                html: 'Error occured !!!',
                icon: 'error'
            });
        }
    });
}
else {
        Swal.fire({
html: response.d + '!!!',
icon: 'error'
});
$("[id*=preloader]").hide();
return false;
}
});
});

function GetZeroAmount(){
    var Obj = {};
    var data = [];
    $("tr.Trclass input[type=checkbox]:checked:not(:disabled)").each(function(){
        // $("tr.Trclass").each(function(){
        var td = $(this).closest("tr")[0];
        var SchemeNo   = $("[id*=hdnSchemeno]", td).val();
        var CampusNo   = $("[id*=hdnCampusno]", td).val();
        var Inc_status = $(td).find("input[type=checkbox]").is(":checked") ? 1 : 0;
        var Ccode = $("[id*=hdnccode]", td).val();
        var ShortName = $("[id*=hdnShortName]", td).val();
        var Amount     = $("[id*=hdnAmount]", td).val();
        var INC_Payment_ID = $("[id*=hdnINC_Payment_ID]", td).val();
        var CourseNO   = $("[id*=hdnCourseNo]", td).val();
        var SessionNo = $('#ctl00_ContentPlaceHolder1_ddlAcademicSession').val();
        var Idno = $('#ctl00_ContentPlaceHolder1_hdnClientId').val();
        var StudentName = $('#lblStudentName').html();
        var StudentID = $('#lblStudentID').html();
        var SemesterNo = SemesterNO;
        var PayTypeNo = 1;
        var f1=0;
        var Total_amt = 0;
        var College_ID = College_id;
        var Short_Name = short_name;
        var BranchNO = CourseNo;
        var DegreeNO = DegreeNo ;
        var ADMBatchnO = ADMBatchNO;
        var Branch = $('#lblCourse').html();

        var alldata = {
            'IDNO': Idno,
            'SESSIONNO': SessionNo,
            'CAMPUSNO': CampusNo,
            'COLLEGEID': College_id,
            'SCHEMENO': SchemeNo,
            'COURSENO': CourseNO,
            'SEMESTERNO': SemesterNO,
            'CCODE': Ccode,
            'SHORTNAME': ShortName,
            'AMOUNT': Amount,
            'INC_STATUS' : Inc_status,
            'PAYMENT_STATUS':INC_Payment_ID,
            'RECON_STATUS':1,
            'F1':f1,
            'TOTAL_AMOUNT':Total_amt,
            'STUDENT_ID':StudentID,
            'STUDENT_NAME':StudentName,
            'PAY_TYPE':PayTypeNo,
            'ADM_BATCHNO':ADMBatchnO,
            'DEGREENO':DegreeNO,
            'BRANCHNO':BranchNO,
            'BRANCH_NAME':Branch

        }
        data.push(alldata);
    });
    console.log(data);
    return data;
}


function PrintRecord(clickValue){
    var idno = $('#ctl00_ContentPlaceHolder1_hdnClientId').val();
    var obj = {};
    var indx = 0;
    obj.Idnos = idno;
    var td = $(clickValue).closest("tr").find("td");
    obj.Dcr_NO = $("[id*=hdnDCR_NO]", td).val()
   
    $.ajax({
        url: localurl + "Exam/INC_Completion_Apply.aspx/GetPaymentReport",
        type: "POST",
        data: JSON.stringify(obj),
        dataType: "json",
        async: false,
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
        error: function ajaxError(response) {
            Swal.fire({
                html: 'Error occured !!!',
                icon: 'error'
            });
        }
    });
}


