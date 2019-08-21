Window.FormScripts = {};

(function () {
    function BornTimeVisibility(checked) {
        if (checked) {
            $('#DpBornTime').removeClass('hide');
            $('#DpBornTime').datepicker('setDate', new Date());
        }
        else {
            $('#DpBornTime').addClass('hide');
            $('#DpBornTime').datepicker('setDate', '');
        }
    }

    function DeadTimeVisibility(checked) {
        if (checked) {
            $('#DpDeadTime').removeClass('hide');
            $('#DpDeadTime').datepicker('setDate', new Date());
        }
        else {
            $('#DpDeadTime').addClass('hide');
            $('#DpDeadTime').datepicker('setDate', '');
        }
    }

    function initializeComponent() {
        BornTimeVisibility($('#Model_BornTimeKnown').is(':checked'));
        DeadTimeVisibility($('#Model_DeadTimeKnown').is(':checked'));

        $('#Model_BornTimeKnown').on('click', function (e) {
            BornTimeVisibility($('#Model_BornTimeKnown').is(':checked'));
        });
        $('#Model_DeadTimeKnown').on('click', function (e) {
            DeadTimeVisibility($('#Model_DeadTimeKnown').is(':checked'));
        });
    }

    Window.FormScripts = {
        Init: function () {
            initializeComponent();
        }
    };
})();
