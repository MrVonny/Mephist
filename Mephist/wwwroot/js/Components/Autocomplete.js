
$(document).ready(function () {
    var data;
    $.ajax({
        type: "GET",
        url: '/Employee/AutocompleteSearch',
        dataType: "json",
        error: function () {
            console.log("Failed to load emplyees");
        },
        success: function (res) {
            console.log(res);
        }
    }).done(function (res) {
        $(".employee-input").autocomplete({
            source: res,
            delay: 100,
            minLength: 3
        });
    });

    $.ajax({
        type: "GET",
        url: '/EducationalMaterials/SubjectsAutocompleteSearch',
        dataType: "json",
        error: function () {
            console.log("Failed to load emplyees");
        },
        success: function (res) {
            console.log(res);
        }
    }).done(function (res) {
        $(".subject-input").autocomplete({
            source: res,
            delay: 100,
            minLength: 3
        });
    });
    
});