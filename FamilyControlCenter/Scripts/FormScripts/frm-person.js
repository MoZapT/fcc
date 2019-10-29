Window.FormScripts = {};

(function () {
    var relationsLoaded = false;
    var namesLoaded = false;
    var biographyLoaded = false;

    //Switch tabs
    function initializeComponent() {
        initializeDetail();

        $('#relations-tab').on('click', function (e) {
            if (relationsLoaded) {
                return;
            }

            loadRelations();
            relationsLoaded = true;
        });

        $('#names-tab').on('click', function (e) {
            if (namesLoaded) {
                return;
            }

            loadNames();
            namesLoaded = true;
        });

        $('#biography-tab').on('click', function (e) {
            if (biographyLoaded) {
                return;
            }

            loadBiography();
            biographyLoaded = true;
        });
    }

    //Detail view
    function initializeDetail() {
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

        initSpouseManagement();
    }

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

    //SpouseManagementInDetailView
    function initSpouseManagement() {
        $('a#RemoveSpouse').off();
        $('#NewSpouseId').off();

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
                initSpouseManagement();
                Window.CustomizedTypeahead.InitElement($('#NewSpouseName'));
                $('.modal-backdrop').remove();
            }
        });
    }

    //RelationsTab
    function initRelationsTab() {
        $('input#NewRelationPersonId').on('change', function (e) {
            var row = $(e.currentTarget).closest('div.row');
            var btn = row.find('button#SaveRelation');

            if ($(e.currentTarget).val()) {
                btn.removeClass('disabled');
            }
            else {
                btn.addClass('disabled');
            }

            getRelationTypes();
        });

        $('button#SaveRelation').on('click', function (e) {
            if ($(this).hasClass('disabled')) {
                return;
            }

            var inviter = $('#Model_Id').val();
            var invited = $('#NewRelationPersonId').val();
            var type = $('#NewRelationRelationTypeId').val();

            setRelations(inviter, invited, type);
        });

        $('button#DestroyRelation').on('click', function (e) {
            var inviter = $(e.currentTarget).attr('inviter');
            var invited = $(e.currentTarget).attr('invited');
            var type = $(e.currentTarget).attr('reltype');

            deleteRelation(inviter, invited, type);
        });
    }

    function loadRelations() {
        $.ajax({
            url: 'PersonRelations',
            data: JSON.stringify({ personId: $('#Model_Id').val() }),
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            traditional: true,
            complete: function (response) {
                if (response.status !== 200) {
                    return;
                }

                $('.tab-pane#relations').html(response.responseText);
                //$('.modal-backdrop').remove();
                initRelationsTab();
                Window.CustomizedTypeahead.InitElement($('#NewRelationPersonName'));
            }
        });
    }

    function setRelations(inviter, invited, type) {
        $.ajax({
            url: getApiRoute() + 'relation/set/' + inviter + '/' + invited + '/' + type,
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                loadRelations();
            }
        });
    }

    function deleteRelation(inviter, invited, type) {
        $.ajax({
            url: getApiRoute() + 'relation/delete/' + inviter + '/' + invited + '/' + type,
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                loadRelations();
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

    //NamesTab
    function initNamesTab() {
        $('button#SaveNamesAndPatronym').addClass('disabled');

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

            isNotValidFieldsForAddNamesAndPatronym =
                $('#NewName').val() === '' &&
                $('#NewLastname').val() === '' &&
                $('#NewPatronym').val() === '' ? true : false;

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
        });

        $('button#DeletePersonName').on('click', function (e) {
            deletePersonName(('#PersonNameId').val());
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
                    //$('.modal-backdrop').removeAll();
                }

                $('#PersonNameAddingErrorMsg').addClass('hide');
                loadNames();
            }
        });
    }

    function deletePersonName(id) {
        $.ajax({
            url: getApiRoute() + 'personname/delete/' + id,
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                loadNames();
            }
        });
    }

    function loadNames() {
        $.ajax({
            url: 'NamesAndPatronymPartialView',
            data: JSON.stringify({ personId: $('#Model_Id').val() }),
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            traditional: true,
            complete: function (response) {
                if (response.status !== 200) {
                    return;
                }

                $('.tab-pane#names').html(response.responseText);
                initNamesTab();
            }
        });
    }

    //BiographyTab
    function initBiographyTab() {
        $('button#SaveBiography').on('click', function (e) {
            savePersonBiography();
        });
    }

    function savePersonBiography() {
        $.ajax({
            url: getApiRoute() + 'personbiography/save/' + $('#Model_Id').val() + '/' + $('#BiographyText').val(),
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                //if (response === false) {
                    //$('#PersonNameAddingErrorMsg').removeClass('hide');
                    //$('.modal-backdrop').removeAll();
                //}

                //$('#PersonNameAddingErrorMsg').addClass('hide');
                loadBiography();
            }
        });
    }

    function loadBiography() {
        $.ajax({
            url: 'PersonBiography',
            data: JSON.stringify({ personId: $('#Model_Id').val() }),
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            traditional: true,
            complete: function (response) {
                if (response.status !== 200) {
                    return;
                }

                $('.tab-pane#biography').html(response.responseText);
                initBiographyTab();
            }
        });
    }

    //Base
    Window.FormScripts = {
        Init: function () {
            initializeComponent();
        }
    };

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
})();
