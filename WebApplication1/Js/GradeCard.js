
//===============================================//
// MODULE NAME   : RFC ERP Portal

var liveurl = "../../../../";
var localurl = "../../";
var LockNo;
var CheckWeigh = 0; 

function BindGradeCard(linkButton, idno) { 
    var Obj = {};
    Obj.sessionId = $("#ctl00_ContentPlaceHolder1_ddlAcdSession").val();
    Obj.Idno = idno; 
    Obj.Semesterno = $("#ctl00_ContentPlaceHolder1_ddlSemesterResult").val();  
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: localurl + "Projects/Result_Process.aspx/GetReportDAta",
        data: JSON.stringify(Obj),
        dataType: "json",
        beforeSend: function () { $("[id*=preloader]").show(); },
        complete: function () { $("[id*=preloader]").hide(); },
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            if (response.d == '') {
                Swal.fire({
                    html: 'No Record Found !!!',
                    icon: 'question'
                });
                return false;
            }
            else {
                $("#DivReport").empty();
                $.each(response.d, function (index, GetValue) {
                    str = '<div class="col-12 mt-3" id="element-to-print">'
                    str = str + '<div class="" id="DivReport">'
                    str = str + '<div class="col-12">'
                    str = str + '<div class="row">'
                    str = str + '<div class="col-md-12 col-lg-12">'
                    str = str + '<div class="border" style="border: 2px solid black;">'
                    str = str + '<div class="text-center">'
                    str = str + '<p class="font-weight-bolder mt-2 mb-0">' + GetValue.UnivercityName + '</p>'
                    str = str + '<p class="mb-0">' + GetValue.city + '</p>'
                    str = str + '<p class="font-weight-bolder mb-0">' + GetValue.reportcard + '</p>'
                    str = str + '<p class="mb-0">' + GetValue.sessionname + '</p>'
                    str = str + '<p class="font-weight-bolder mb-0">' + GetValue.studentName + '</p>'
                    str = str + '<p class="mb-2">' + GetValue.regno + '</p>'
                    str = str + '</div>'

                    str = str + '<div style="height: 300px;">'

                    str = str + '<table id="BindDynamicExamComponentWtTable" class="table">'
                    str = str + ' <thead>'
                    str = str + ' <tr id="headerRow">'
                    str = str + ' </tr>'
                    str = str + ' </thead>'
                    str = str + '<tbody></tbody>'
                    str = str + '</table>'

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: localurl + "Projects/Result_Process.aspx/GetGradeDeatils",
                        data: JSON.stringify(Obj),
                        dataType: "json",
                        beforeSend: function () { $("[id*=preloader]").show(); },
                        complete: function () { $("[id*=preloader]").hide(); },
                        contentType: "application/json;charset=utf-8",
                        success: function (response) {
                            populateTable(response.d);
                        }
                    });

                    str = str + '</div>'
                    str = str + '<div class="row">'
                    str = str + '<div class="col-12">'
                    str = str + '<hr class="my-0" />'
                    str = str + '</div>'
                    str = str + '<div class="col-4 text-center">'
                    str = str + '<p class="mb-0">' + GetValue.Date + '</p>'
                    str = str + '<hr class="my-0" />'
                    str = str + '<label>Date</label>'
                    str = str + '</div>'
                    str = str + '<div class="col-8 text-center pl-0">'
                    str = str + '<p class="mb-0">' + GetValue.Dean + '</p>'
                    str = str + '<hr class="my-0" />'
                    str = str + '<label>Dean</label>'
                    str = str + '</div>'
                    str = str + '</div>'
                    str = str + '<div class="text-center my-2">'
                    str = str + '<label>OFFICIAL MARKS (' + GetValue.Datestatement + ')</label>'
                    str = str + '</div>'
                    str = str + '<div class="col-12">'
                    str = str + '<div class="row" style="font-size: 10px;">'
                    str = str + '<div class="col-12 col-md-2">'
                    str = str + '<span>100 - 1.0</span><br />'
                    str = str + '<span>99 - 1.1</span><br />'
                    str = str + '<span>98 - 1.2</span><br />'
                    //str = str + '<span>97 - 1.3</span><br />'
                    str = str + '<span>96-97- 1.3</span><br />'
                    str = str + '<span>95 - 1.4</span><br />'
                    //str = str + '<span>94 - 1.6</span><br />'
                    str = str + '</div>'
                    str = str + '<div class="col-12 col-md-2">'
                    str = str + '<span>94 - 1.5</span><br />'
                    str = str + '<span>93 - 1.6</span><br />'
                    str = str + '<span>91-92 - 1.7</span><br />'
                    str = str + '<span>90 - 1.8</span><br />'
                    str = str + '<span>89 - 1.9</span><br />'

                    str = str + '</div>'
                    str = str + '<div class="col-12 col-md-2">'
                    //str = str + '<span>86 - 2.4</span><br />'
                    str = str + '<span>88 - 2.0</span><br />'
                    str = str + '<span>86-87 - 2.1</span><br />'
                    str = str + '<span>85 - 2.2</span><br />'
                    str = str + '<span>84 - 2.3</span><br />'
                    str = str + '<span>83 - 2.4</span><br />'
                    //str = str + '<span>82 - 2.8</span><br />'

                    str = str + '</div>'
                    str = str + '<div class="col-12 col-md-2">'
                    str = str + '<span>81 - 2.5</span><br />'
                    str = str + '<span>80 - 2.6</span><br />'
                    str = str + '<span>79 - 2.7</span><br />'
                    str = str + '<span>75 - 3.0</span><br />'
                    str = str + '<span>74-Below - 5.0</span><br />'
                    //str = str + '<span>78 - 3.4</span><br />'
                    //str = str + '<span>77 - 3.5</span><br />'
                    // str = str + '<span>74 - below - 3.6</span><br />'
                    str = str + '</div>'
                    str = str + '<div class="col-12 col-md-4">'
                    str = str + '<p class="mb-0">INC - Incomplete</p>'
                    str = str + '<p class="mb-0">DRP - Dropped</p>'
                    str = str + '<p class="mb-0">WP - Withdrawn</p>'
                    str = str + '<p class="mb-0">NG - No Grade</p>'
                    str = str + '<p class="mb-0">HNA - Has Never</p>'
                    str = str + '<p class="mb-0">Attended</p>'
                    str = str + '</div>'
                    str = str + '</div>'
                    str = str + '</div>'
                    str = str + '<div class="text-center">'
                    str = str + '<p class="mb-2">PG:Prelim Grade, MG:Midterm Grade, FR:Final Rating, CG:Completion Grade, CR:Credit</p>'
                    str = str + '</div>'
                    str = str + '</div>'
                    str = str + '</div>'
                    str = str + '</div>'
                    str = str + '</div>'
                    str = str + '<div id="tblasstypes">'
                    str = str + '</div>'
                    str = str + '</div>'
                    str = str + '</div>'
                    $("#DivReport").append(str);
                    $("#myModal1").modal("show");
                });
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


function populateTable(data) { 
    var cellValue;
    var table = $('#BindDynamicExamComponentWtTable');
    var headerRow = $('#headerRow');
    data = JSON.parse(data);
    headerRow.empty();
    table.find('tbody').empty();
    $.each(data, function (index, item) {

        var row = $('<tr>');
        $.each(item, function (key, value) {
            if (index === 0) {
                if (key == "sort_order") {
                    headerRow.append($('<th style="Display:none;">').text(key));
                }
                else {
                    headerRow.append($('<th>').text(key));
                }
            }
        });
        table.append(row);
        $.each(item, function (key, value) {
            if (key == "sort_order") {
                row.append($('<td style="Display:none;">').text(value))
            } else {
                row.append($('<td>').text(value))
            }
        });
        table.append(row);
    });
}



