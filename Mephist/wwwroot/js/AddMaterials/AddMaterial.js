$(document).ready(function () {

    var subjects;
    var works;
    var employees;

    if ($("#Type").val() != "") {
        $("#subject_block").fadeIn(250);
        if ($("#Type").val() == 4) {
            $("#add-material-form").prop('action', '/EducationalMaterials/AddLabJournal');
            getLaboratorySubjects()
            $("#Subject").autocomplete({
                source: function (request, response) {
                    response(subjects);
                },
                delay: 10,
                minLength: 0
            });
            $("#Name").prop('disabled', true);
            $("#Name").val("Лабораторная работа. " + $("#Work").val());
        } else {
            $("#add-material-form").prop('action', '/EducationalMaterials/AddMaterial');
            getAllSubjects();
            $("#Subject").autocomplete({
                source: function (request, response) {
                    var results = $.ui.autocomplete.filter(subjects, request.term);
                    if (results.length == 0)
                        response(subjects.slice(0, 12));
                    else
                        response(results.slice(0, 12));
                    
                },
                delay: 200,
                minLength: 0
            });
        }

    }
    if ($("#Subject").val() != "") {
        getEmployees();
        $("#general_block").fadeIn(250);
        $("#upload_block").fadeIn(250);
        if ($("#Type").val() == 4) {

            $("#laboratory_block").fadeIn(250);
            getWorks($("#Subject").val());
            $("#Work").autocomplete({
                source: function (request, response) {
                    var results = $.ui.autocomplete.filter(works, request.term);
                    if (results.length == 0)
                        response(works.slice(0, 10));
                    else
                        response(results.slice(0, 10));
                },
                delay: 100,
                minLength: 0
            });
        }
    }
        

    $("#Type").on('input', function () {
        $("#general_block").fadeOut(250);
        $("#laboratory_block").fadeOut(250);
        $("#upload_block").fadeOut(250);
        $("#subject_block").fadeIn(250);
        $("#Subject").val(null);
        if ($("#Type").val() == 4) {
            $("#add-material-form").prop('action', '/EducationalMaterials/AddLabJournal');
            getLaboratorySubjects();
            $("#Subject").autocomplete({
                source: function (request, response) {
                    response(subjects);
                },
                delay: 10,
                minLength: 0
            });
            $("#Name").prop('disabled', true);
            $("#Name").val("Лабораторная работа. " + $("#Work").val());
        }
        else {
            $("#add-material-form").prop('action', '/EducationalMaterials/AddMaterial');
            $("#Name").prop('disabled', false);
            $("#Name").val(null);
            getAllSubjects();
            $("#Subject").autocomplete({
                source: function (request, response) {
                    var results = $.ui.autocomplete.filter(subjects, request.term);
                    if (results.length == 0)
                        response(subjects.slice(0, 12));
                    else
                        response(results.slice(0, 12));
                    
                },
                delay: 200,
                minLength: 0
            });
        }
    });

    $("#Subject").autocomplete({
        close: function () {
            $("#general_block").fadeIn(250);
            if ($("#Type").val() == 4) {
                getWorks($("#Subject").val());
                $("#laboratory_block").fadeIn(250);
                $("#Work").autocomplete({
                    source: function (request, response) {
                        var results = $.ui.autocomplete.filter(works, request.term);
                        if (results.length == 0)
                            response(works.slice(0, 10));
                        else
                            response(results.slice(0, 10));
                    },
                    delay: 100,
                    minLength: 0
                });
            }
            else
                $("#laboratory_block").fadeOut(250);
            $("#upload_block").fadeIn(250);
            getEmployees();
        }
    });

    $("#EmployeeFullName").autocomplete({
        source: function (request, response) {
            var results = $.ui.autocomplete.filter(employees, request.term);
            if (results.length == 0)
                response(employees.slice(0, 10));
            else
                response(results.slice(0, 10));
        },
        delay: 100,
        minLength: 0
    });

    $("#Work").change(function () {
        $("#Name").val("Лабораторная работа. " + $("#Work").val());
    });


    function getAllSubjects() {
        $.ajax({
            type: "GET",
            url: '/api/university/subjects',
            dataType: "json",
            data: { isLaboratory: false },
            error: function () {
                console.log("Failed to load subjects");
            },
            success: function (res) {
                console.log(res);
            }
        }).done(function (res) {
            subjects = res;
        });
    };

    function getLaboratorySubjects() {
        $.ajax({
            type: "GET",
            url: '/api/university/subjects',
            dataType: "json",
            data: { isLaboratory: true },
            error: function () {
                console.log("Failed to load laboratory subjects");
            },
            success: function (res) {
                console.log(res);
            }
        }).done(function (res) {
            subjects = res;
        });
    };

    function getWorks(subject) {
        $.ajax({
            type: "GET",
            url: '/api/university/works',
            dataType: "json",
            data: { subject: subject },
            error: function () {
                console.log("Failed to load emplyees");
            },
            success: function (res) {
                console.log(res);
            }
        }).done(function (res) {
            works = res;
        });
    };

    function getEmployees() {
        $.ajax({
            type: "GET",
            url: '/api/university/employees',
            dataType: "json",
            error: function () {
                console.log("Failed to load emplyees");
            },
            success: function (res) {
                console.log(res);
            }
        }).done(function (res) {
            employees = res;
        });
    };
});

