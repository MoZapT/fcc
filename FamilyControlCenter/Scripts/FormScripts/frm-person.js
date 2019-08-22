Window.FormScripts = {};

(function () {
    function BornTimeVisibility(checked) {
        if (checked) {
            $('#DpBornTime').removeClass('hide');
            //$('#DpBornTime').datepicker('setDate', new Date()); //WARNING! causing datepicker to fail!
        }
        else {
            $('#DpBornTime').addClass('hide');
            //$('#DpBornTime').datepicker('setDate', ''); //WARNING! causing datepicker to fail!
        }
    }
    function DeadTimeVisibility(checked) {
        if (checked) {
            $('#DpDeadTime').removeClass('hide');
            //$('#DpDeadTime').datepicker('setDate', new Date()); //WARNING! causing datepicker to fail!
        }
        else {
            $('#DpDeadTime').addClass('hide');
            //$('#DpDeadTime').datepicker('setDate', ''); //WARNING! causing datepicker to fail!
        }
    }

    function setRelations(panel) {
        var model = createPersonRelationModel();

        $.ajax({
            url: 'SetPersonRelation',//'api/set/relations',
            data: JSON.stringify({ entity: model, personid: $('#NewRelationOwnerId').val() }),
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            traditional: true,
            complete: function (response) {
                $(panel).html(response.responseText);
                //initDynamicScripts();
            }
        });
    }

    function deleteRelation(panel, relationid) {
        $.ajax({
            url: 'DeletePersonRelation',//'api/set/relations',
            data: JSON.stringify({ relationid: relationid, personid: $('#Model_Id').val() }),
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            traditional: true,
            complete: function (response) {
                $(panel).html(response.responseText);
                //initDynamicScripts();
            }
        });
    }

    function createPersonRelationModel() {
        var model = {
            Id: null,
            DateCreated: null,
            DateModified: null,
            IsActive: true,
            Member: null,
            PersonId: $('#NewRelationPersonId option:selected').val(),
            PersonRelationGroupId: null
        };

        return model;
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
            setRelations($panel);
        });

        $('button#DestroyRelation').on('click', function (e) {
            e.preventDefault();
            var btn = e.currentTarget;
            $panel = $(btn).closest('.panel-body');
            deleteRelation($panel, $(btn).attr('member-id'));
        });
    }

    Window.FormScripts = {
        Init: function () {
            initializeComponent();
        }
    };
})();
