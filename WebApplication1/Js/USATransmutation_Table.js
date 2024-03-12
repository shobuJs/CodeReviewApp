//===============================================//
// MODULE NAME   :  PMO
// PAGE NAME     : Grade Master
// CREATION DATE : 02-06-2023
// CREATED BY    : Gaurav Kalbande
// Modified BY   : Harshal Bobde
// Modified Date : 07-06-2023
//===============================================//


//-------------  Validation in page-------------//
function fnc100(value, min, max) {
    var msg = ''; var str = ""; var count = 0;
    if (parseFloat(value) < 0 || isNaN(value))
        return 0;
    else if (parseFloat(value) > 100) {
        alert('Marks is not greater than 100');
        return 0;
    }
    else {
        return value;
    }
}

function fnc10(value, min, max) {
    if (parseFloat(value) < 0 || isNaN(value))
        return 0;
    else if (parseFloat(value) > 10) {
        alert('Grade Point is not greater than 10');
        return 0;
    }
    else { return value; }
}

function checknumeric(value) {
    if (parseFloat(value) < 0 || isNaN(value))
        return 0;
    else if (parseFloat(value) > 100) {
        alert('Transnumeration should be less than 100');
        return 0;
    }
    else
        return value;
}

function updateSerialNumbers() {
    var table = document.getElementById("tblgrade");
    var rows = table.rows;
    var rowCount = rows.length;
    for (var i = 1; i < rowCount - 1; i++) {
        var row = rows[i];
        var serialCell = row.cells[0];
        serialCell.innerHTML = i;
    }
}
//-------------  End Validation-------------//

//-------------  Get Grade Master data Distinct Scheme No wise -------------//
$(document).ready(function () {
    BindGrade();
    DynamicRow();
    $('#preloader').hide();
    $(".f-Grade01").focus();
});

function DynamicRow() {
    var rowCount = $('.AcdGrade').length + 1;
    var str;
    str = str + '<tr class="AcdGrade">'
    str = str + '<td>' + rowCount + ' </td>'
    str = str + '<td><input type="hidden" id="hdtblGradeNo" name="f-gradeno' + rowCount + '" class="form-control f-gradeno01" /><input type="text" name="f-Grade' + rowCount + '" class="form-control f-Grade01" /></td>'
    str = str + '<td><input type="text"  maxlength="5"    name="f-Min' + rowCount + '" class="form-control f-Min01"  onkeyup="this.value = fnc100(this.value, 0, 100)" /></td>' // onchange="validateMarks(this)" 
    str = str + '<td><input type="text"   maxlength="5"   name="f-Max' + rowCount + '" class="form-control f-Max01"  onkeyup="this.value = fnc100(this.value, 0, 100)"  /></td>' // onchange="validateMarks(this)"

    str = str + '<td><input type="text"  maxlength="5" id="txtTransmutation"   name="f-Transmutation' + rowCount + '" class="form-control f-Transmutation01"  onkeyup="this.value = checknumeric(this.value)" /></td>'
    str = str + '<td><input type="checkbox" id="checkbox1" name="f-Check' + rowCount + '" class="f-Check01" /></td>'


    str = str + '<td>'
    str = str + '<i type="button" id="btnDelete" class="deleteContact fa fa-trash text-danger display-6"></i></td>'
    str = str + '</tr>';
    $('#tblgrade').append(str);
}

function BindGrade() {

    var str = "";
    $("#tblGradeMaster").empty();
    $.ajax({
        url: "../../../EXAMModule/GradeSchema_USA.aspx/BindGrade",
        type: "POST",
        dataType: "json",
        beforeSend: function () { $("[id*=preloader]").show(); },
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response.d == '') {
                return false;
            } else {
                str = '<thead> <tr><th><span class="Action">Action</span></th><th><span class="GradeScheme">Grade Scheme</span></th><th><span class="Createdby">Created/Modify By</span></th><th><span class="CreatedDate">Created/Modify Date</span></th><th><span class="Status">Status</span></th></tr></thead><tbody>';
                $.each(response.d, function (index, GetValue) {
                    str = str + '<tr>'
                    str = str + '<td><a id="btnEditGrade" class="fa fa-pencil-square-o text-primary" title="Edit Record" href="#" onclick="EditGrade(this)"></a>' +
                        ' <input type="hidden" id="hdntblschemeno" value="' + GetValue.GRADING_SCHEME_NO + '"/></td>'

                    str = str + '<td>' + GetValue.GRADING_SCHEME_NAME + '</td>'
                    str = str + '<td>' + GetValue.CREMODIBY + '</td>'
                    str = str + '<td>' + GetValue.CREMODIDATE + '</td>'


                    if (GetValue.ACTIVE == true) {
                        str = str + '<td><span class="badge bg-success">Active</span>' +
                            '<input type="hidden" id="GradeStatus" value="true"/></td>'
                    }
                    else {
                        str = str + '<td><span class="badge bg-danger">Inactive</span>' +
                            '<input type="hidden" id="GradeStatus" value="false"/></td>'
                    }

                    str = str + '</tr>'
                });
                str = str + '</tbody>';
                $("#tblGradeMaster").append(str);
                $("#preloader").hide();
            }
        },
        error: function (errResponse) {

        }
    });

}
//-------------  End Grade Master data Distinct Scheme No wise -------------//

//------------- insert data in grade master-------------//
function getAllGradeData() {
    var data = [];
    var valno = 0;
    var hdnscheno = $("#hdnscheno").val().trim();
    var txtGradeScheme = $("#txtGradeScheme").val().trim();
    var Statusgrade = $('#Status').prop('checked');
    if (Statusgrade == true) {
        Statusgrade = 1;
    }
    else {
        Statusgrade = 0;
    }
    $('tr.AcdGrade').each(function () {
        var strno;
        var strname = $(this).find('.f-Grade01').val();
        strno = $(this).find('.f-gradeno01').val();
        if (strno == "")
            strno = "0";
        var strpoint = "0.00";
        var Min = $(this).find('.f-Min01').val();
        var Max = $(this).find('.f-Max01').val();
        var transmutation = $(this).find('.f-Transmutation01').val();

        var pass_fail = $(this).find('#checkbox1').prop('checked');
        if (pass_fail == true)
            pass_fail = 0;
        else
            pass_fail = 1;
        var alldata = {
            'INTMGRADENO': strno,
            'GRADING_SCHEME_NAME': txtGradeScheme,
            'GRADENAME': strname,
            'GRADEPOINT': strpoint,
            'NUMBER': Max,
            'MINMARK': Min,
            'ACTIVE': Statusgrade,
            'GRADING_SCHEME_NO': hdnscheno,
            'COLLEGE_CODE': '',
            'TRANSMUTATION': transmutation,
            'PASS_FAIL': pass_fail
        }
        data.push(alldata);
    });
    return data;
}

function areTableValuesUnique() {
    var data = getAllGradeData();
    var seenValues = {};

    for (var i = 0; i < data.length; i++) {
        var entry = data[i];
        var key = entry.GRADENAME + entry.MINMARK + entry.NUMBER + entry.TRANSMUTATION + entry.PASS_FAIL;

        if (seenValues[key]) {
            return false;
        } else {
            seenValues[key] = true;
        }
    }
    return true;
}

$("#btnSubmit").click(function () {
    var isValid = "";
    $('#tblgrade td input[type="text"]').each(function () {
        var $textbox = $(this);
        if ($textbox.val() === '') {
            $textbox.focus();
            isValid = "No";
        }
    });

    if (isValid == "No") {
        alert('Please enter Grade Scheme Details');
        return false;
    }
    // Check Grade Scheme name
    if ($("#txtGradeScheme").val().trim() != '') {
        var Row = document.getElementById("tblGradeMaster");
        var Cells = Row.getElementsByTagName("td");
        if ($("#btnSubmit").val().trim() == 'Submit') {
            for (var i = 0; i < Cells.length; i++) {
                if ($("#txtGradeScheme").val().trim() == Cells[i].innerText) {
                    alert(Cells[i].innerText + ' Grade Scheme Name already Exists');
                    return false;
                }
            }
        }
        else {
            if ($("#hdnschemename").val() != $("#txtGradeScheme").val()) {
                for (var i = 0; i < Cells.length; i++) {
                    if ($("#txtGradeScheme").val().trim() == Cells[i].innerText) {
                        alert(Cells[i].innerText + ' Grade Scheme Name already Exists');
                        return false;
                    }
                }

            }
        }

    }
    // Check Grade Scheme name

    checkval = 0;
    $('tr.AcdGrade').each(function () {
        if (parseInt($(this).find('.f-Min01').val()) > parseInt($(this).find('.f-Max01').val())) {
            checkval = 1;
        }
    });
    if (checkval == 1) {
        alert('Min. Marks value is less than Max. Marks');
        return false;
    }

    if ($("#txtGradeScheme").val().trim() == '') {
        alert('Please enter Grade Scheme');
        return false;
    }

    //var data = JSON.stringify(getAllGradeData());
    if (areTableValuesUnique())
        var data = JSON.stringify(getAllGradeData());
    else
        alert('Duplicate data found in table!!!');

    $.ajax({
        url: '../../../EXAMModule/GradeSchema_USA.aspx/SaveData',//Home.aspx is the page   
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ 'Gradedata': data }),
        success: function () {
            if ($("#hdnscheno").val() == "0") {
                alert('Grade Scheme Details Inserted Successfully');
            }
            else {
                alert('Grade Scheme Details Updated Successfully');
            }
            BindGrade();
            ClearGrade();
        },
        error: function () {
        }
    });
});
//------------- End insert data in grade master -------------//


//------------- Edit data in grade master-------------//
function validateMarks(input) {
    var msg = ''; var str = ""; var count = 0;
    var row = input.parentNode.parentNode; // Get the parent row
    var minInput = row.getElementsByClassName('f-Min01')[0];
    var maxInput = row.getElementsByClassName('f-Max01')[0];

    if (row.rowIndex > 1) {
        var prevRow = row.previousElementSibling; // Get the previous row
        var prevMaxInput = prevRow.getElementsByClassName('f-Min01')[0];

        if (parseFloat(maxInput.value) > parseFloat(prevMaxInput.value)) {
            alert('Max. Marks should be less than the previous rows Min. Marks');

            maxInput.value = "";
            maxInput.focus();
        }
    }

    if (parseFloat(minInput.value) > parseFloat(maxInput.value)) {
        alert('Min. Marks should be less than  Max. Marks');
        maxInput.value = "";
        maxInput.focus();
    }
}


function EditGrade(ClickValue) {

    try {
        ClearGrade();
        var tableBody = document.getElementById("tblgrade").getElementsByTagName("tbody")[0];
        tableBody.deleteRow(0);
        var td = $("td", $(ClickValue).closest("tr"));
        $("#hdGradeNo").val($("[id*=hdntblGradeNo]", td).val());
        //$("#hdntblschemeno").val($("[id*=hdntblschemeno]", td).val()); 
        $("#hdnscheno").val($("[id*=hdntblschemeno]", td).val());
        $("#txtGradeScheme").val(td[1].innerText);
        $("#hdnschemename").val(td[1].innerText);
        $("#btnSubmit").val('Update');
        if ($("[id*=GradeStatus]", td).val() == "false")
            $('#Status').prop('checked', false);
        else
            $('#Status').prop('checked', true);
        var Obj = {};
        //Obj.schemano = $("[id*=hdntblschemeno]", td).val();

        Obj.schemano = $("#hdnscheno").val();
        var str = "";
        $.ajax({
            url: "../../../EXAMModule/GradeSchema_USA.aspx/EditSchemNoGrade",
            type: "POST",
            data: JSON.stringify(Obj),
            dataType: "json",
            beforeSend: function ()
            { $("[id*=preloader]").show(); },
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                if (response.d == '') {
                    //Swal.fire({
                    //    html: 'Record Not Found !!!',
                    //    icon: 'question'
                    //});
                    //$("[id*=preloader]").hide();
                    $("#preloader").hide();
                    return false;
                } else {
                    $("#preloader").hide();
                    var rowCount = 1;
                    $.each(response.d, function (index, GetValue) {
                        str = str + '<tr class="AcdGrade">'
                        var length = response.d.length;
                        str = str + '<td>' + rowCount + ' </td>'
                        str = str + '<td style="display:none">' + GetValue.INTMGRADENO + '</td>'
                        str = str + '<td><input type="hidden" id="hdtblGradeNo" name="f-gradeno' + rowCount + '" class="form-control f-gradeno01"  value="' + GetValue.INTMGRADENO + '"/> <input type="text" name="f-Grade' + rowCount + '" class="form-control f-Grade01"  value="' + GetValue.GRADENAME + '" /></td>'
                        str = str + '<td><input type="text"  maxlength="5"   name="f-Min' + rowCount + '" class="form-control f-Min01"  value="' + GetValue.MINMARK + '"   onkeyup="this.value = fnc100(this.value, 0, 100)" /></td>' // onchange="validateMarks(this)"
                        str = str + '<td><input type="text"  maxlength="5"   name="f-Max' + rowCount + '" class="form-control f-Max01"   value="' + GetValue.NUMBER + '"  onkeyup="this.value = fnc100(this.value, 0, 100)"  /></td>' // onchange="validateMarks(this)"

                        str = str + '<td><input type="text"  maxlength="5"    name="f-Transmutation' + rowCount + '" class="form-control f-Transmutation01" value="' + GetValue.TRANSMUTATION + '"  onkeyup="this.value = checknumeric(this.value)" /></td>'
                        if (GetValue.PASS_FAIL === 1) {
                            str = str + '<td><input type="checkbox" id="checkbox1" name="f-Check' + rowCount + '" class="f-Check01" /></td>'
                        } else {
                            str = str + '<td><input type="checkbox" id="checkbox1" name="f-Check' + rowCount + '" class="f-Check01" checked /></td>'
                        }
                        str = str + '<td><i type="button" id="btnDelete"  class="deletefromdb fa fa-trash text-danger display-6"></i></td>'
                        str = str + '</tr>';
                        rowCount++;
                    });
                    str = str + '</tbody>';
                    $("#tblgrade").append(str);
                    $("#preloader").hide();
                    BindGrade();
                }
            },
            error: function (errResponse) {
            }
        });
    }
    catch (ex) {
    }
}
//------------- End Edit data in grade master-------------//

//------------- Add Row in table-------------//
$(document).on("click", ".classAdd", function () {
    var msg = ''; var str = ""; var count = 0;
    var isValid = true;
    var checkval = 0;

    $('#tblgrade td input[type="text"]').each(function () {
        var $textbox = $(this);
        if ($textbox.val() === '') {
            $textbox.focus();
            alert('Please enter Grade Scheme Details');
            return false;
        }
    });

    $("#tblgrade tr").each(function () {
        var row = $(this);
        var textboxes = row.find("input[type='text']");
        textboxes.each(function () {
            if ($(this).val() === '') {
                isValid = false;
                return false;
            }
        });
        var textboxes = row.find("input[type='number']");
        textboxes.each(function () {
            if ($(this).val() === '') {
                isValid = false;
                return false;
            }
        });
        if (!isValid) {
            return false;
        }
    });

    if (!isValid) {
        alert('Please enter Grade Scheme Details');
    }

    DynamicRow();

});
//------------- End Add Row in table-------------//

//------------- Delete Row in table from database-------------//
$(document).on("click", ".deletefromdb", function () {
    var msg = ''; var str = ""; var count = 0;
    var tablrowcount = $('#tblgrade tr').length;

    var isValid = true;
    var checkval = 0;
    if (tablrowcount == '2') {
        // Check Blank Input Value
        $("#tblgrade tr").each(function () {
            var row = $(this);
            var textboxes = row.find("input[type='text']");
            textboxes.each(function () {
                if ($(this).val() === '') {
                    isValid = false;
                    return false;
                }
            });
            var textboxes = row.find("input[type='number']");
            textboxes.each(function () {
                if ($(this).val() === '') {
                    isValid = false;
                    return false;
                }
            });
            if (!isValid) {
                return false;
            }
        });
        if (!isValid) {
            alert('Please enter Grade Scheme Details');

        }
    }

    if (tablrowcount == '2') {
        alert('You can not delete Grade Scheme Details');
    }

    if (confirm("Are you sure you want to delete this Grade?") == true) {
        var obj = {};
        var td = $("td", $(this).closest("tr"));
        obj.GradeNo = td[1].innerText;
        $(this).closest("tr").remove();
        $.ajax({
            url: "../../../EXAMModule/GradeSchema_USA.aspx/Deleteadata",
            type: "POST",
            data: JSON.stringify(obj),
            dataType: "json",
            beforeSend: function () { $("[id*=preloader]").show(); },
            contentType: "application/json;charset=utf-8",
            success: function (response) {
                $("[id*=preloader]").hide();
                BindGradeSheame();
            },
            error: function () {
                $("[id*=preloader]").hide();
            }
        });
    } else {

    }


});
//------------- End Delete Row in table from database-------------//

//------------- Delete Row in table  -------------//
$(document).on("click", ".deleteContact", function () {
    var msg = ''; var str = ""; var count = 0;
    var isValid = true;
    var checkval = 0;
    var tablrowcount = $('#tblgrade tr').length;

    if (tablrowcount == '2') {
        // Check Blank Input Value
        $("#tblgrade tr").each(function () {
            var row = $(this);
            var textboxes = row.find("input[type='text']");
            textboxes.each(function () {
                if ($(this).val() === '') {
                    isValid = false;
                    return false;
                }
            });
            var textboxes = row.find("input[type='number']");
            textboxes.each(function () {
                if ($(this).val() === '') {
                    isValid = false;
                    return false;
                }
            });
            if (!isValid) {
                return false;
            }
        });
        if (!isValid) {
            alert('Please enter Grade Scheme Details');
            return false;
        }
    }
    if (tablrowcount == '2') {
        alert('You can not delete Grade Scheme Details');
        return false;
    }

    if (confirm("Are you sure you want to delete this Grade?") == true) {
        var obj = {};
        var td = $("td", $(this).closest("tr"));
        obj.GradeNo = td[1].innerText;
        $(this).closest("tr").remove();
    }
    else {
    }


});
//------------- End Delete Row in table -------------//

//------------- clear Data in from  -------------//
$("#btnClear").click(function () {

    ClearGrade();
    BindGrade();
});
function ClearGrade() {
    $('#txtGradeScheme').val('');
    $(".f-Grade01").val('');
    $('#hdnGradeNo').val('0');
    $('#hdnscheno').val('0');
    $('#hdnschemename').val('');
    $('#hdtblGradeNo').val('');
    $("#btnSubmit").val('Submit');
    $('#btnDelete').show();
    $('#Status').prop('checked', true);
    $('#checkbox1').prop('checked', false);

    var tableBody = document.getElementById("tblgrade").getElementsByTagName("tbody")[0];
    while (tableBody.rows.length > 1) {
        tableBody.deleteRow(1);
    }
    ClearGradeTableData();
}
function ClearGradeTableData() {
    var table = document.getElementById('tblgrade');
    for (var i = 0; i < table.rows.length; i++) {
        var row = table.rows[i];
        for (var j = 0; j < row.cells.length; j++) {
            var cell = row.cells[j];
            var input = cell.querySelector('input');
            if (input) {
                input.value = '';
            }
        }
    }
}
//------------- end clear Data in from  -------------//