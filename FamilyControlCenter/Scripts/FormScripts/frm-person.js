Window.FormScripts = {};

(function () {
    function BornTimeVisibility(checked) {
        if (checked) {
            $('#DpBornTime').removeClass('hide');
            //$('#DpBornTime').datepicker('setDate', new Date()); //WARNING! causing datepicker to fail!
        }
        else {
            $('#DpBornTime').addClass('hide');
            $('#DpBornTime').datepicker('setDate', '');
        }
    }

    function DeadTimeVisibility(checked) {
        if (checked) {
            $('#DpDeadTime').removeClass('hide');
            //$('#DpDeadTime').datepicker('setDate', new Date()); //WARNING! causing datepicker to fail!
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

        $('button#SaveRelation').on('click', function (e) {
            e.preventDefault();
            var btn = e.currentTarget;
            $panel = $(btn).closest('.panel-footer').siblings('.panel-body');

            //TODO ajax call, rerender the $panel
        });
    }

    Window.FormScripts = {
        Init: function () {
            initializeComponent();
        }
    };
})();
