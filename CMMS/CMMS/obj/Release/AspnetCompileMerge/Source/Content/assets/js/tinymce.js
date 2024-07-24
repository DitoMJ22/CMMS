$(function() {
  'use strict';

  //Tinymce editor
  if ($("#tinymceExample").length) {
    tinymce.init({
      selector: '#tinymceExample',
      height: 400,
    theme: 'silver',
    plugins: [
        'advlist  lists ',
    ],
      toolbar1: 'undo redo | insert | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent ',
      toolbar2: 'print preview | forecolor backcolor emoticons ',
      image_advtab: true,
    });
  }

    tinymce.init({
        selector: '.readonlyeditor',
        height: 400,
        readonly: true,
        theme: 'silver',
        plugins: [
            'advlist  lists ',
        ],
        toolbar1: 'undo redo | insert | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent ',
        toolbar2: 'print preview | forecolor backcolor emoticons ',
        image_advtab: true,
    });
  
});