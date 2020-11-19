$(document).ready(function () {

    var _name;

    OnTypeChanged();

    $("#Name").on('input', function () {
        if ($("#Name").is(":enabled"))
            _name = $("#Name").val();
    })

    $("#typeInput").on('input', OnTypeChanged)
    $("#Work").on('input', OnTypeChanged);

    //$("#uploads-preview").bind("DOMSubtreeModified", GetSortedList());

    function OnTypeChanged() {
        if ($("#typeInput").val() == 4) {
            $("#labJournalInput").show();
            $("#Name").prop('disabled', true);
            $("#Name").val("Лабораторная работа " + $("#Work").val());
            $("form").prop('action', '/EducationalMaterials/AddLabJournal');
        }
        else {
            $("#labJournalInput").hide();
            $("#Name").prop('disabled', false);
            $("#Name").val(_name);
            $("form").prop('action', '/EducationalMaterials/AddMaterial');
        }
    }

    
})

