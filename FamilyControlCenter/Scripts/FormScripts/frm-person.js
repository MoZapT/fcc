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
                Window.CustomizedTypeahead.InitElement($('#NewRelationPersonName'));
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
        initHandlePersonNamesControls();

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

    function loadNamesAndPatronyms(personId, sections) {
        $.ajax({
            url: 'NamesAndPatronymPartialView',
            data: JSON.stringify({ personId: personId }),
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            traditional: true,
            complete: function (response) {
                if (response.status !== 200) {
                    return;
                }

                sections.html(response.responseText);
                $('a#DeletePersonName').off();
                $('a#DeletePersonName').on('click', function (e) {
                    var pnId = $(e.currentTarget).closest('a').siblings('[name="PersonNameId"][type="hidden"]').val();
                    var sections = $(e.currentTarget).parents('#NamesAndPatronymHistorySections');

                    deletePersonName(pnId, sections);
                });
            }
        });
    }

    function savePersonName(firstname, lastname, patronym, datechanged) {
        $.ajax({
            url: getApiRoute() + 'personname/set/' +
                firstname + '/' +
                lastname + '/' +
                patronym + '/' +
                datechanged + '/' +
                $('#Model_Id').val(),
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response === false) {
                    $('#PersonNameAddingErrorMsg').removeClass('hide');
                    $('.modal-backdrop').removeAll();
                    $('#AddPreviousNameAndPatronymModal').modal();
                }

                $('#PersonNameAddingErrorMsg').addClass('hide');
            }
        });
    }

    function deletePersonName(id, sections) {
        $.ajax({
            url: getApiRoute() + 'personname/delete/' + id,
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response === false) {
                    return;
                }

                //$('.modal-backdrop').remove();
                loadNamesAndPatronyms($('#Model_Id').val(), sections);
            }
        });
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

    function initHandlePersonNamesControls() {
        $('button#SaveNamesAndPatronym').off();
        $('[data-target="#AddPreviousNameAndPatronymModal"]').off();
        $('[data-target="#ShowPreviousNamesAndPatronymsModal"]').off();
        //-------------------------------------------------------
        $('#ActiveFrom,#NewName,#NewLastname,#NewPatronym').on('change', function (e) {
            var todayDate = new Date();
            todayDate.setHours(0);
            todayDate.setMinutes(0);
            todayDate.setSeconds(0);
            todayDate.setMilliseconds(0);
            var selectedDate = $('#ActiveFrom').datepicker("getDate");
            selectedDate.setHours(0);
            selectedDate.setMinutes(0);
            selectedDate.setSeconds(0);
            selectedDate.setMilliseconds(0);

            var isNotValidDateChangedForAddNamesAndPatronym = false;
            if (todayDate <= selectedDate) {
                isNotValidDateChangedForAddNamesAndPatronym = true;
            }

            var isNotValidFieldsForAddNamesAndPatronym =
                $('#NewName').val() === $('#Model_Firstname').val() &&
                $('#NewLastname').val() === $('#Model_Lastname').val() &&
                $('#NewPatronym').val() === $('#Model_Patronym').val() ? true : false;

            if (isNotValidDateChangedForAddNamesAndPatronym || isNotValidFieldsForAddNamesAndPatronym) {
                $('button#SaveNamesAndPatronym').addClass('disabled');
            }
            else {
                $('button#SaveNamesAndPatronym').removeClass('disabled');
            }
        });
        $('button#SaveNamesAndPatronym').on('click', function (e) {
            if ($(e.currentTarget).hasClass('disabled')) {
                return;
            }

            savePersonName($('#NewName').val(), $('#NewLastname').val(), $('#NewPatronym').val(), $('#ActiveFrom').val());

            var sections = $(e.currentTarget)
                .closest('.modal-footer')
                .siblings('.modal-body')
                .find('#NamesAndPatronymHistorySections');
            loadNamesAndPatronyms($('#Model_Id').val(), sections);
        });
        $('[data-target="#AddPreviousNameAndPatronymModal"]').on('click', function (e) {
            $('#NewName').val($('#Model_Firstname').val());
            $('#NewLastname').val($('#Model_Lastname').val());
            $('#NewPatronym').val($('#Model_Patronym').val());
            $('#ActiveFrom').datepicker("setDate", new Date());

            var isNotValidDateChangedForAddNamesAndPatronym = true;
            var isNotValidFieldsForAddNamesAndPatronym = true;

            if (isNotValidDateChangedForAddNamesAndPatronym || isNotValidFieldsForAddNamesAndPatronym) {
                $('button#SaveNamesAndPatronym').addClass('disabled');
            }
            else {
                $('button#SaveNamesAndPatronym').removeClass('disabled');
            }

            loadNamesAndPatronyms($('#Model_Id').val(), $('#AddPreviousNameAndPatronymModal #NamesAndPatronymHistorySections'));
        });
        $('[data-target="#ShowPreviousNamesAndPatronymsModal"]').on('click', function (e) {
            loadNamesAndPatronyms($('#Model_Id').val(), $('#ShowPreviousNamesAndPatronymsModal #NamesAndPatronymHistorySections'));
        });
    }

    Window.FormScripts = {
        Init: function () {
            initializeComponent();
        }
    };
})();
