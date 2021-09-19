$(document).ready(function () {
    $("form").keydown(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });


    var subjects;
    var works;
    var employees;

    var isValid = true;

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



    $("#Type").on('input change blur', function () {
        resetValidation($("input"));
        if (checkValidation($("#Type"), typeValidation)) {
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
                    minLength: 0,
                    close: function() {
                        resetValidation($("input").not("#Type #Subject"));
                        if (checkValidation($("#Subject"), subjectValidation)) {
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
                                    minLength: 0,
                                    close: function () {
                                        checkValidation($("#Work"), workValidation);
                                    }
                                }).focus(function () {
                                    $(this).autocomplete('search', $(this).val())
                                });
                            }
                            else
                                $("#laboratory_block").fadeOut(250);
                            $("#upload_block").fadeIn(250);
                            getEmployees();
                        } else {
                            $("#laboratory_block").fadeOut(250);
                            $("#upload_block").fadeOut(250);
                            $("#general_block").fadeOut(250);
                        }
                    }
                }).focus(function () {
                    $(this).autocomplete('search', $(this).val())
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
                    minLength: 0,
                    close: function() {
                        resetValidation($("input").not("#Type #Subject"));
                        if (checkValidation($("#Subject"), subjectValidation)) {
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
                                    minLength: 0,
                                    close: function () {
                                        checkValidation($("#Work"), workValidation);
                                    }
                                }).focus(function () {
                                    $(this).autocomplete('search', $(this).val())
                                });
                            }
                            else
                                $("#laboratory_block").fadeOut(250);
                            $("#upload_block").fadeIn(250);
                            getEmployees();
                        } else {
                            $("#laboratory_block").fadeOut(250);
                            $("#upload_block").fadeOut(250);
                            $("#general_block").fadeOut(250);
                        }
                    }
                }).focus(function () {
                    $(this).autocomplete('search', $(this).val())
                });
            }
        } else {
            
            $("#subject_block").fadeOut(250);
        }
        $("#general_block").fadeOut(250);
        $("#laboratory_block").fadeOut(250);
        $("#upload_block").fadeOut(250);

    });

    $("#Subject").on("input change blur", function () {
        resetValidation($("input").not("#Type #Subject"));
        if (checkValidation($("#Subject"), subjectValidation)) {
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
                    minLength: 0,
                    close: function () {
                        $("#Name").val("Лабораторная работа");
                        if (checkValidation($("#Work"), workValidation))
                            $("#Name").val("Лабораторная работа. " + $("#Work").val());
                    }
                }).focus(function () {
                    $(this).autocomplete('search', $(this).val())
                });
            }
            else
                $("#laboratory_block").fadeOut(250);
            $("#upload_block").fadeIn(250);
            getEmployees();
        } else {
            $("#laboratory_block").fadeOut(250);
            $("#upload_block").fadeOut(250);
            $("#general_block").fadeOut(250);
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
        minLength: 0,
        close: function () {
            checkValidation($("#EmployeeFullName"), employeeValidation);
        }
    }).focus(function () {
        $(this).autocomplete('search', $(this).val())
    });
    $("#EmployeeFullName").on("input change blur", function () {
        checkValidation($("#EmployeeFullName"), employeeValidation);
    });

    $("#Work").on("input change blur", function () {
        $("#Name").val("Лабораторная работа");
        if (checkValidation($("#Work"), workValidation))
            $("#Name").val("Лабораторная работа. " + $("#Work").val());
    });

    $("#Name").on("input change blur", function () {
        checkValidation($("#Name"), nameValidation)
    });


    $("#uploads").on("change", function () {
        checkValidation($("#uploads"), fileValidation);
    });

    $("#Description").on("input change blur", function () {
        checkValidation($("#Description"), descriptionValidation);
    });

    $("#Year").on("input change blur", function () {
        checkValidation($("#Year"), yearValidation);
    });

    $("#Mark").on("input change blur", function () {
        checkValidation($("#Mark"), markValidation);
    });

    $("#add-material-form").submit(function (event) {
        var valid = checkValidation($("#Type"), typeValidation) *
            checkValidation($("#Subject"), subjectValidation) *
            checkValidation($("#EmployeeFullName"), employeeValidation) *
            checkValidation($("#Name"), nameValidation) *
            checkValidation($("#Description"), descriptionValidation) *
            checkValidation($("#uploads"), fileValidation) 
        if ($("#Type").val() == 4) {
            valid *= checkValidation($("#Work"), workValidation) *
                checkValidation($("#Year"), yearValidation) *
                checkValidation($("#Mark"), markValidation);

        }

        if (valid == false) {
            event.preventDefault();
            event.stopPropagation();
        }

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

    function typeValidation(element) {
        return (element.val() != 0);
    }

    function subjectValidation(element) {
        return subjects.includes(element.val());
    }

    function employeeValidation(element) {
        return employees.includes(element.val());
    }

    function workValidation(element) {
        return works.includes(element.val());
    }

    function nameValidation(element) {
        return (element.val().length != 0);
    }

    function fileValidation(element) {
        return (element.val().length != 0);
    }

    function descriptionValidation(element) {
        return (element.val().length < 10000);
    }

    function yearValidation(element) {
        return (parseInt(element.val()) <= 2023) && (parseInt(element.val()) >= 2000);
    }

    function markValidation(element) {
        return (parseInt(element.val()) <= 100) && (parseInt(element.val()) >= 60);
    }

    function checkValidation(element, validator) {
        var valid = validator(element);
        if (valid) {
            element.addClass("is-valid");
            element.removeClass("is-invalid");
        } else {
            element.addClass("is-invalid");
            element.removeClass("is-valid");
            isValid = false;
        }
        return valid;
    }

    function resetValidation(el) {
        el.removeClass("is-invalid");
        el.removeClass("is-valid");
    }
});

