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

    function refreshRelations(personId) {
        var container = $('div#RelationListContainer');

        $.ajax({
            url: 'PersonRelations',
            data: JSON.stringify({ personId: personId }),
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            traditional: true,
            complete: function (response) {
                if (response.status !== 200) {
                    return;
                }

                container.html(response.responseText);
                var panel = container.find('#RelationsPanel');
                panel.addClass('in');
                panel.attr('aria-expanded', 'true');
                panel.attr('style', '');
                $('.modal-backdrop').remove();
                initRelationListControls();
            }
        });
    }

    function updateSpouseOrLivePartnerSection(personId, spouseId) {
        var container = $('div#MarriageStatus');

        $.ajax({
            url: 'MarriagePartialView',
            data: JSON.stringify({ personId: personId, spouseId: spouseId }),
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            traditional: true,
            complete: function (response) {
                if (response.status !== 200) {
                    return;
                }

                container.html(response.responseText);
                initRelationListControls();
                Window.CustomizedTypeahead.InitElement($('#NewSpouseName'));
                $('.modal-backdrop').remove();
            }
        });
    }

    function setRelations(inviter, invited, type) {
        $.ajax({
            url: getApiRoute() + 'relation/set/' + inviter + '/' + invited + '/' + type,
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                refreshRelations(inviter);
            }
        });
    }

    function deleteRelation(inviter, invited, type) {
        $.ajax({
            url: getApiRoute() + 'relation/delete/' + inviter + '/' + invited + '/' + type,
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                refreshRelations(inviter);
            }
        });
    }

    function addSpouse(inviter, invited, type) {
        $.ajax({
            url: getApiRoute() + 'relation/set/' + inviter + '/' + invited + '/' + type,
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                updateSpouseOrLivePartnerSection(inviter, invited);
            }
        });
    }

    function removeSpouse(inviter, invited, type) {
        $.ajax({
            url: getApiRoute() + 'relation/delete/' + inviter + '/' + invited + '/' + type,
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                updateSpouseOrLivePartnerSection(inviter, invited);
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
        initRelationListControls();

        $('#Model_HasBirthDate, #Model_HasDeathDate').on('click', function (e) {
            DatepickerVisibility();
        });

        /*ListFeatures*/
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
        $('input#NewRelationPersonId').off();
        $('button[data-target="#NewRelationModal"]').off();
        $('button#DestroyRelation').off();
        $('a#RemoveSpouse').off();
        $('#NewSpouseId').off();
        $('button#SaveRelation').off();
        //-------------------------------------------------------
        $('input#NewRelationPersonId').on('change', function (e) {
            var row = $(e.currentTarget).closest('div.row');
            var btn = row.find('button#SaveRelation');

            if ($(e.currentTarget).val()) {
                btn.removeClass('hide');
            }
            else {
                btn.addClass('hide');
            }

            getRelationTypes();
        });
        $('button[data-target="#NewRelationModal"]').on('click', function (e) {
            $('#NewRelationPersonId').val('');
            $('#NewRelationPersonName').typeahead('val', '');
            $('#NewRelationRelationTypeId').html('');
            $('button#SaveRelation').attr('disabled', 'disabled');
        });
        $('button#DestroyRelation').on('click', function (e) {
            var inviter = $(e.currentTarget).attr('inviter');
            var invited = $(e.currentTarget).attr('invited');
            var type = $(e.currentTarget).attr('reltype');

            deleteRelation(inviter, invited, type);
        });
        $('a#RemoveSpouse').on('click', function (e) {
            var inviter = $(e.currentTarget).attr('inviter');
            var invited = $(e.currentTarget).attr('invited');
            var type = $(e.currentTarget).attr('reltype');

            removeSpouse(inviter, invited, type);
        });
        $('#NewSpouseId').on('change', function (e) {
            var inviter = $('#Model_Id').val();
            var invited = $(e.currentTarget).val();
            var type = $('a#AddSpouse').attr('reltype');

            addSpouse(inviter, invited, type);
        });
        $('button#SaveRelation').on('click', function (e) {
            var inviter = $('#Model_Id').val();
            var invited = $('#NewRelationPersonId').val();
            var type = $('#NewRelationRelationTypeId').val();

            setRelations(inviter, invited, type);
        });
    }

    Window.FormScripts = {
        Init: function () {
            initializeComponent();
        }
    };
})();
