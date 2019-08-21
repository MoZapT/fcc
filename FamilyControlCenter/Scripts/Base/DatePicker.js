Window.DatePicker = {};

(function () {
    function initializeComponent() {
        $('.datepicker:not([readonly])').datepicker({
            format: 'dd.mm.yyyy',
            language: 'de',
            todayHighlight: true,
            calendarWeeks: true,
            autoclose: true,
            weekStart: 1,
            orientation: "bottom"
        });

        $('.datepicker').on('change', function (e) {
            var prev = $(e.currentTarget).prev('[type=hidden]');
            $(prev).attr('value', $(e.currentTarget).datepicker('getFormattedDate'));
        });
    }

    Window.DatePicker = {
        Init: function () {
            initializeComponent();
        }
    };
})();
