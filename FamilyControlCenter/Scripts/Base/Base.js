Window.Base = {};

(function () {
    function initializeComponent() {
        $('#ru,#de,#us').on('click', function (e) {

        });

        $('.crud').on('click', function (e) {
            e.preventDefault();

            crud = e.currentTarget;
            var args = $(crud).attr('args');
            var id = $(crud).attr('obj');

            $('#Command').val(args);
            $('#Model_Id').val(id);

            $('form#PersonView').submit();
        });
    }

    Window.Base = {
        Init: function () {
            initializeComponent();
        }
    };
})();
