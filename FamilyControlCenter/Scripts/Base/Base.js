Windows = {};

// CRUD ---------------------------------------------------------------------------
// --------------------------------------------------------------------------------
function initCrud(eventArgument, id) {
    $('.crud').on('click', function (e) {
        e.preventDefault();

        crud = e.currentTarget;
        var args = $(crud).attr('args');
        var id = $(crud).attr('obj');

        $('#EventArgument').val(args);
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

// DATEPICKER ---------------------------------------------------------------------
// --------------------------------------------------------------------------------
function initDatepicker() {
    $('.datepicker').datepicker({
        format: 'dd.mm.yyyy',
        locale: 'de',
        autoclose: true,
        calendarWeeks: true,
        orientation: 'bottom'
    });
}

// SERVICE ------------------------------------------------------------------------ 
// --------------------------------------------------------------------------------
function SetdNone(obj) {
    if (!obj.hasClass('dNone')) {
        obj.addClass('dNone');
    }
}

function ResetdNone(obj) {
    if (obj.hasClass('dNone')) {
        obj.removeClass('dNone');
    }
}

// INIT ---------------------------------------------------------------------------
// --------------------------------------------------------------------------------
function InitializeComponent() {
    initDatepicker();
    initCrud();
}

Windows.Base = {
    Init: function () {
        InitializeComponent();
    }
};