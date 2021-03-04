
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

            source: function (request, response) {
                var results = $.ui.autocomplete.filter(res, request.term);

                response(results.slice(0, 10));
            },
            delay: 100,
            minLength: 1           
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
            source: function (request, response) {
                var results = $.ui.autocomplete.filter(res, request.term);

                response(results.slice(0, 10));
            },
            delay: 100,
            minLength: 1
        });
    });
    
});