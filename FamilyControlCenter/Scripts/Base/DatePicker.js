Window.DatePicker = {};

(function () {
    function initializeComponent() {
        $('.datepicker:not([readonly])').datepicker({
            format: 'dd.mm.yyyy',
            language: window.location.pathname.split('/')[1],
            todayHighlight: true,
            calendarWeeks: true,
            autoclose: true,
            weekStart: 1,
            orientation: "bottom",
            clearBtn: true
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
