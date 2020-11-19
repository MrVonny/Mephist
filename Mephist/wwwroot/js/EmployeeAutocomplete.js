
$(document).ready(function () {
    var data;
    $.ajax({
        type: "GET",
        url: '/Employee/AutocompleteSearch',// + '?term='+ $("#EmployeeFullName").val(),
        dataType: "json",
        error: function () {
            console.log("Failed to load emplyees");
        },
        success: function (res) {
            console.log(res);
        }
    }).done(function (res) {
        $("#EmployeeFullName").autocomplete({
            source: res
        });
    });
    
});