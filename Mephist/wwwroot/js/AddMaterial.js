$(document).ready(function () {

    $("#typeInput").change(function () {
        if ($("#typeInput").val() == 4)
            $("#labJournalInput").show();
        else
            $("#labJournalInput").hide();

    })
    

})