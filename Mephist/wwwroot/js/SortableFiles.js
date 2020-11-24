$(document).ready(function () {
    $.uploadPreview({
        input_field: "#uploads",
        preview_box: "#uploads-preview",
        label_field: "#uploads-label"
    });
    $("#uploads-preview").sortable();
    $("#uploads-preview").disableSelection();
    $("form").submit(function (event) {
        $("#Name").prop('disabled', false);
        CreateQuery();
    });

    function CreateQuery(event, ui) {
        var list = [];
        $("#uploads-preview").children().each(function (index) {
            var id = $(this).attr('id');
            if (id != undefined)
                list.push(id.slice("sortable-item".length));
        });

        console.log(list);
        $("#query").val(list.toString());
    }
});

(function ($) {
    $.extend({
        uploadPreview: function (options) {

            // Options + Defaults
            var settings = $.extend({
                input_field: ".image-input",
                preview_box: ".image-preview",
                label_field: ".image-label",
                label_default: "Choose File",
                label_selected: "Change File",
                no_label: false,
                success_callback: null,
            }, options);

            // Check if FileReader is available
            if (window.File && window.FileList && window.FileReader) {
                if (typeof ($(settings.input_field)) !== 'undefined' && $(settings.input_field) !== null) {
                    $(settings.input_field).change(function () {
                        var files = this.files;
                        

                        if (files.length > 0) {
                            $(settings.preview_box).empty();
                            for (let i = 0; i < files.length; i++) {
                                var file = files[i];
                                var reader = new FileReader();
                                

                                // Load file
                                reader.addEventListener("load", function (event) {
                                    var loadedFile = event.target;

                                    // Check format
                                    if (file.type.match('image')) {
                                        // Image

                                        $('<div class="file-sortable ui-state-default col-6 col-md-4 col-lg-2 m-1" id="sortable-item' + i + '">' +                                                         
                                                    '<img src="' + loadedFile.result + '" class="img-thumbnail"/>' +
                                                    '<span class="thumb-name">' + files[i].name + '</span>' +                          
                                          '</div>'
                                        ).appendTo($(settings.preview_box));

                                    } else if (file.type.match('audio')) {
                                        // Audio
                                        $(settings.preview_box).html("<audio controls><source src='" + loadedFile.result + "' type='" + file.type + "' />Your browser does not support the audio element.</audio>");
                                    } else {
                                        alert("This file type is not supported yet.");
                                    }
                                });

                                if (settings.no_label == false) {
                                    // Change label
                                    $(settings.label_field).html(settings.label_selected);
                                }

                                // Read the file
                                reader.readAsDataURL(file);

                                // Success callback function call
                                if (settings.success_callback) {
                                    settings.success_callback();
                                }

                            }
                        } else {
                            if (settings.no_label == false) {
                                // Change label
                                $(settings.label_field).html(settings.label_default);
                            }

                            // Clear background
                            $(settings.preview_box).css("background-image", "none");

                            // Remove Audio
                            $(settings.preview_box + " audio").remove();
                        }
                    });
                }
            } else {
                alert("You need a browser with file reader support, to use this form properly.");
                return false;
            }
        }
    });
})(jQuery);