jQuery(document).ready(function($) {

    // Enable the correct options for this slider type
    var checkSlideCompatibility = function(slider) {
        // slides - set red background on incompatible slides
        jQuery("#compatibilityWarning").remove();

        if (jQuery('.metaslider .slide:not(.' + slider + ')').length) {
            var message = ucFirst(slider) + " Slider is only compatible with Image and Post Feed slides.";
            var warningDiv = jQuery("<div id='compatibilityWarning' class='updated'><p><b>Warning:</b> " + message + "</p></div>");
            jQuery(".metaslider .left").prepend(warningDiv);
        };
    };

    var ucFirst = function(string) {
        return string.charAt(0).toUpperCase() + string.slice(1);
    }

    // handle slide libary switching
    $(".metaslider").on('click', '.select-slider', function() {
        checkSlideCompatibility(jQuery(this).attr('rel'));
    });

    checkSlideCompatibility(jQuery('.metaslider .select-slider:checked').attr('rel'));

    function loadCodeMirror(textarea_id) {

        $('#' + textarea_id).hide().siblings('.CodeMirror').remove();

        var codeMirror = CodeMirror.fromTextArea(document.getElementById(textarea_id), {
            tabMode: 'indent',
            mode: 'xml',
            lineNumbers: true,
            lineWrapping: true,
            theme: 'monokai',
            onChange: function(cm) {
                cm.save();
            }
        });
    }

    $(".metaslider").on('click', '.slide .codemirror', function() {
        textarea_id = $(this).attr('data-editor');
        setTimeout(loadCodeMirror(textarea_id), 50);
    });

    $('.slide.post_feed .codemirror').each(function() {
        textarea_id = $(this).attr('data-editor');
        setTimeout(loadCodeMirror(textarea_id), 50);
    });

    $(".metaslider .left table").live("slideAdded", function(event) {
        $('.slide.post_feed .codemirror').each(function() {
            textarea_id = $(this).attr('data-editor');
            setTimeout(loadCodeMirror(textarea_id), 50);
        });
    });

    $(".metaslider select[name='template_tags']").on('change', function(e) {
        e.preventDefault();

        var tag = $(this).val();

        var codeMirror = $(this).closest('.tab').find('.CodeMirror')[0].CodeMirror;

        if (codeMirror.getSelection() == "") {
            codeMirror.replaceRange(tag, codeMirror.getCursor());
        } else {
            codeMirror.replaceSelection(tag);
        }

        codeMirror.focus();
    });

});