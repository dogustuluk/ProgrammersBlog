$(document).ready(function () {
    $('#text-editor').trumbowyg({
        lang:['tr'],
        btns: [
            ['viewHTML'],
            ['undo', 'redo'], // Only supported in Blink browsers
            ['formatting'],
            ['strong', 'em', 'del'],
            ['superscript', 'subscript'],
            ['link'],
            ['insertImage'],
            ['justifyLeft', 'justifyCenter', 'justifyRight', 'justifyFull'],
            ['unorderedList', 'orderedList'],
            ['horizontalRule'],
            ['removeformat'],
            ['fullscreen'],
            ['foreColor', 'backColor'],
            ['emoji'],
            ['fontfamily'],
            ['fontsize']
        ],
        plugins: {
            colors: {
                foreColorList: [
                    'ff0000', '00ff00', '0000ff', '42855B', 'FF4A4A', '472D2D','774360'
                ],
                backColorList: [
                    '000', '333', '555'
                ],
                displayAsList: false
            }
        }
        
    });
    
});