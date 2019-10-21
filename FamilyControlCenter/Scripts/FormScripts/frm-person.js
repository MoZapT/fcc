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

    function setRelations() {
        var from = createPersonRelationModel($('#Model_Id').val());
        var to = createPersonRelationModel($('#NewRelationPersonId').val());
        var type = $('#NewRelationRelationTypeId').val();

        $.ajax({
            url: getApiRoute(),// + 'relation/delete/' + personId + '/' + groupId,
            type: 'GET',
            dataType: 'json',
            complete: function (response) {
                //TODO update list
            }
        });
    }

    function deleteRelation() {
        var btn = $('button#DestroyRelation');
        var personId = $(btn).attr('person-id');
        var groupId = $(btn).attr('group-id');

        $.ajax({
            url: getApiRoute() + 'relation/delete/' + personId + '/' + groupId,
            type: 'GET',
            dataType: 'json',
            complete: function (response) {
                //TODO update list
            }
        });
    }

    //TODO delete or move to .js model factory
    function createPersonRelationModel(personId) {
        var model = {
            Id: null,
            DateCreated: null,
            DateModified: null,
            IsActive: true,
            Member: null,
            PersonId: personId,
            PersonRelationGroupId: null
        };

        return model;
    }

    function initializeComponent() {
        $('button[data-target="#NewRelationModal"]').on('click', function (e) {
            $('#NewRelationPersonId').val('');
            $('#NewRelationPersonName').typeahead('val', '');
            $('#NewRelationRelationTypeId').html('');
        });

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
            setRelations();
        });

        $('button#DestroyRelation').on('click', function (e) {
            deleteRelation();
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
