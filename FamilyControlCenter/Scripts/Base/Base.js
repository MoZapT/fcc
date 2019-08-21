Window.Base = {};

(function () {
    function initializeComponent() {
        $('.crud').on('click', function (e) {
            e.preventDefault();

            crud = e.currentTarget;
            var args = $(crud).attr('args');
            var id = $(crud).attr('obj');

            $('#Command').val(args);
            $('#Model_Id').val(id);

            var forms = $('form');
            if (forms.length > 1) {
                $('form')[1].submit();
            }
            else {
                $('form')[0].submit();
            }
        });
    }

    Window.Base = {
        Init: function () {
            initializeComponent();
        }
    };
})();
