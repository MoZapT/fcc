Window.FormScripts = {};

(function () {
    function DatepickerVisibility() {
        var hasBirthdate = $('#Model_HasBirthDate').is(':checked');
        var hasDeathdate = $('#Model_HasDeathDate').is(':checked');

        if (hasBirthdate) {
            $('#DpBornTime').removeAttr('readonly', 'readonly');
            Window.DatePicker.ReInitElement($('#DpBornTime'));
        }
        else {
            $('#DpBornTime').attr('readonly', 'readonly');
            Window.DatePicker.DestroyElement($('#DpBornTime'));
        }

        if (hasDeathdate) {
            $('#DpDeadTime').removeAttr('readonly', 'readonly');
            Window.DatePicker.ReInitElement($('#DpDeadTime'));
        }
        else {
            $('#DpDeadTime').attr('readonly', 'readonly');
            Window.DatePicker.DestroyElement($('#DpDeadTime'));
        }
    }

    function refreshRelations() {
        var container = $('div#RelationListContainer');

        $.ajax({
            url: 'PersonRelations',
            data: JSON.stringify({ personId: $('#Model_Id').val() }),
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            traditional: true,
            complete: function (response) {
                container.html(response.responseText);
                initRelationListControls();
            }
        });
    }

    function setRelations() {
        var inviter = $('#Model_Id').val();
        var invited = $('#NewRelationPersonId').val();
        var type = $('#NewRelationRelationTypeId').val();

        $.ajax({
            url: getApiRoute() + 'relation/set/' + inviter + '/' + invited + '/' + type,
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                refreshRelations();
            }
        });
    }

    function deleteRelation() {
        var btn = $('button#DestroyRelation');

        var inviter = $(btn).attr('inviter');
        var invited = $(btn).attr('invited');
        var type = $(btn).attr('reltype');

        $.ajax({
            url: getApiRoute() + 'relation/delete/' + inviter + '/' + invited + '/' + type,
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                refreshRelations();
            }
        });
    }

    function getRelationTypes() {
        var ddlBox = $('select#NewRelationRelationTypeId');

        if (!$('#NewRelationPersonId').val()) {
            ddlBox.html('');
            $('button#SaveRelation').attr('disabled', 'disabled');
        }
        else {
            $('button#SaveRelation').removeAttr('disabled');
        }

        $.ajax({
            url: getApiRoute() + 'relationtype/all/' + $('#Model_Id').val() + '/' + $('#NewRelationPersonId').val(),
            type: 'GET',
            dataType: 'json',
            complete: function (response) {
                var json = response.responseJSON;
                if (json === undefined || json === null) {
                    return;
                }

                ddlBox.html('');
                for (var i = 0; i < json.length; i++) {
                    var optionHtml = '<option value = "' + json[i].Value + '">' + json[i].Text + '</option>';
                    var prevHtml = ddlBox.html();

                    ddlBox.html(prevHtml + optionHtml);
                }
            }
        });
    }

    function initializeComponent() {
        $('#NewRelationPersonId').on('change', function (e) {
            getRelationTypes();
        });

        $('button[data-target="#NewRelationModal"]').on('click', function (e) {
            $('#NewRelationPersonId').val('');
            $('#NewRelationPersonName').typeahead('val', '');
            $('#NewRelationRelationTypeId').html('');
            $('button#SaveRelation').attr('disabled', 'disabled');
        });

        $('#Model_HasBirthDate, #Model_HasDeathDate').on('click', function (e) {
            DatepickerVisibility();
        });

        $('button#SaveRelation').on('click', function (e) {
            setRelations(e.currentTarget);
        });

        initRelationListControls();

        $('input#NewRelationPersonId').on('change', function (e) {
            var row = $(e.currentTarget).closest('div.row');
            var btn = row.find('button#SetRelation');

            if ($(e.currentTarget).val()) {
                btn.removeClass('hide');
            }
            else {
                btn.addClass('hide');
            }
        });

        $('.row.item input[type="checkbox"] + label').on('click, touchstart, dblclick', function (e) {
            e.stopPropagation();
        });

        $('.row.item').on('touchstart, dblclick', function (e) {
            $(e.currentTarget).find('button.crud[args="3"]').click();
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

    function initRelationListControls() {
        $('button#DestroyRelation').off();
        //-------------------------------------------------------
        $('button#DestroyRelation').on('click', function (e) {
            deleteRelation(e.currentTarget);
        });
    }

    Window.FormScripts = {
        Init: function () {
            initializeComponent();
        }
    };
})();
