Window.FormScripts = {};

(function () {
    function DatepickerVisibility(checkbox) {
        if (checkbox.id === 'Model_HasBirthDate') {
            if (checkbox.checked) {
                $('#DpBornTime').removeClass('hide');
            }
            else {
                $('#DpBornTime').addClass('hide');
            }
        }
        else if (checkbox.id === 'Model_HasDeathDate') {
            if (checkbox.checked) {
                $('#DpDeadTime').removeClass('hide');
            }
            else {
                $('#DpDeadTime').addClass('hide');
            }
        }
    }

    function setRelations(panel) {
        var model = createPersonRelationModel();

        $.ajax({
            url: 'SetPersonRelation',//'api/set/relations',
            data: JSON.stringify({ entity: model, personid: $('#Model_Id').val() }),
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
        $('#Model_HasBirthDate, #Model_HasDeathDate').on('click', function (e) {
            DatepickerVisibility(e.currentTarget);
        });

        $('#Model_Sex').on('click', function (e) {
            var textSpan = $(e.currentTarget).siblings('span');

            if (e.currentTarget.checked) {
                textSpan.text($('[for="Sex_Checkbox_Female_Text"]').text());
            }
            else {
                textSpan.text($('[for="Sex_Checkbox_Male_Text"]').text());
            }
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

        $('#NewRelationPersonId').on('change', function (e) {
            $.ajax({
                url: getApiRoute() + 'relationtype/all/' + $('#NewRelationPersonId').val(),
                type: 'GET',
                dataType: 'json',
                complete: function (response) {
                    var json = response.responseJSON;
                    if (json === undefined || json === null) {
                        return;
                    }

                    var ddlBox = $('select#NewRelationRelationTypeId');

                    ddlBox.html('');
                    for (var i = 0; i < json.length; i++) {
                        var optionHtml = '<option value = "' + json[i].Value + '">' + json[i].Text + '</option>';
                        var prevHtml = ddlBox.html();

                        ddlBox.html(prevHtml + optionHtml);
                    }
                }
            });
        });
    }

    function getApiRoute() {
        var apiDebug = 'http://localhost:55057/';
        var apiLive = '';

        var url = '';
        if (window.location.hostname === 'localhost') {
            url += apiDebug;
        }
        var lang = window.location.pathname.split('/')[1] + '/';
        url += lang;

        return url + 'api/';
    }

    Window.FormScripts = {
        Init: function () {
            initializeComponent();
        }
    };
})();
