Window.FormScripts = {};

(function () {
    var relationsLoaded = false;
    var namesLoaded = false;
    var biographyLoaded = false;
    var documentsLoaded = false;

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

        $('#documents-tab').on('click', function (e) {
            if (documentsLoaded) {
                return;
            }

            loadDocuments();
            documentsLoaded = true;
        });
    }

    //Detail view
    function initializeDetail() {
        $('#Model_HasBirthDate, #Model_HasDeathDate').on('click', function (e) {
            DatepickerVisibility();
        });

        /*ListFeatures*/
        $('.custom-checkbox').on('click, touchstart, dblclick', function (e) {
            e.stopPropagation();
        });

        $('.row.item').on('touchstart, dblclick', function (e) {
            $(e.currentTarget).find('button.crud[args="3"]').click();
        });

        initPictureSection();
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

    function initPictureSection() {
        $('.custom-carousel-control').on('click', function(e) {
            //setMainPictureBtnVisibility(e, true);
        });

        $('#UploadPicture').on('change', function (e) {
            uploadPicture();
        });

        $('#SetMainPhoto').on('click', function(e) {
            setMainPicture();
        });

        $('#DeletePhoto').on('click', function(e) {
            deletePicture();
        });

        //setMainPictureBtnVisibility();
    }

    function uploadPicture() {
        personId = $('#Model_Id').val();
        var fd = new FormData();
        var files = $('#UploadPicture[type="file"]')[0].files;
        for (var i = 0; i < files.length; i++) {
            fd.append("file_" + i, files[i], files[i].name);
        }

        $.ajax({
            url: getApiRoute() + 'person/photo/upload/' + personId,
            type: 'POST',
            data: fd,
            enctype: 'multipart/form-data',
            contentType: false,
            processData: false,
            complete: function (response) {
                if (response.status === 200) {
                    reloadPictures();
                }
            }
        });
    }

    function setMainPicture() {
        var id = $('.item.active').attr('value');
        var personId = $('#Model_Id').val();

        $.ajax({
            url: getApiRoute() + 'person/photo/setmain/' + personId + '/' + id,
            type: 'GET',
            dataType: 'json',
            success: function(response) {
                $('#SetMainPhoto').attr('value', id);
            }
        });
    }

    function deletePicture() {
        var id = $('.item.active').attr('value');
        var personId = $('#Model_Id').val();

        $.ajax({
            url: getApiRoute() + 'person/photo/delete/' + personId + '/' + id,
            type: 'GET',
            dataType: 'json',
            success: function(response) {
                reloadPictures();
            }
        });
    }

    function reloadPictures() {
        var container = $('div#PhotoSection');
        var personId = $('#Model_Id').val();

        $.ajax({
            url: 'PersonPhotoSection',
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
                initPictureSection();
            }
        });
    }

    //SpouseManagementInDetailView
    function initSpouseManagement() {
        $('a#RemoveSpouse').off();
        $('#NewSpouseId').off();
        $('#NewPartnerId').off();
        $('#AddSpouse').off();
        $('#AddPartner').off();

        $('#AddSpouse').on('click', function (e) {
            e.preventDefault();
            $('#SpouseContainer').removeClass('hide');
            $('#HideSpouseContainer').removeClass('hide');
            $('#AddSpouse').addClass('hide');
        });

        $('#AddPartner').on('click', function (e) {
            e.preventDefault();
            $('#PartnerContainer').removeClass('hide');
            $('#HidePartnerContainer').removeClass('hide');
            $('#AddPartner').addClass('hide');
        });

        $('#HideSpouseContainer').on('click', function (e) {
            e.preventDefault();
            $('#SpouseContainer').addClass('hide');
            $('#HideSpouseContainer').addClass('hide');
            $('#AddSpouse').removeClass('hide');
            $('#NewSpouseId').val('');
            $('#NewSpouseName').typeahead('val', '');
        });

        $('#HidePartnerContainer').on('click', function (e) {
            e.preventDefault();
            $('#PartnerContainer').addClass('hide');
            $('#HidePartnerContainer').addClass('hide');
            $('#AddPartner').removeClass('hide');
            $('#NewPartnerId').val('');
            $('#NewPartnerName').typeahead('val', '');
        });

        $('a#RemoveSpouse').on('click', function (e) {
            e.preventDefault();

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

        $('#NewPartnerId').on('change', function (e) {
            var inviter = $('#Model_Id').val();
            var invited = $(e.currentTarget).val();
            var type = $('a#AddPartner').attr('reltype');

            addPartner(inviter, invited, type);
        });
    }

    function addSpouse(inviter, invited, type) {
        $.ajax({
            url: getApiRoute() + 'relation/set/' + inviter + '/' + invited + '/' + type,
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                updateSpouseOrLivePartnerSection(inviter, invited, null);
            }
        });
    }

    function addPartner(inviter, invited, type) {
        $.ajax({
            url: getApiRoute() + 'relation/set/' + inviter + '/' + invited + '/' + type,
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                updateSpouseOrLivePartnerSection(inviter, null, invited);
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

    function updateSpouseOrLivePartnerSection(personId, spouseId, partnerId) {
        var container = $('div#MarriageStatus');

        $.ajax({
            url: 'MarriagePartialView',
            data: JSON.stringify({ personId: personId, spouseId: spouseId, partnerId: partnerId }),
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
                Window.CustomizedTypeahead.InitElement($('#NewPartnerName'));
            }
        });
    }

    //RelationsTab
    function initRelationsTab() {
        SaveRelationButtonHandler();
        $('.toast').toast('show');

        $('input#NewRelationPersonId').on('change', function (e) {
            SaveRelationButtonHandler();
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

    function SaveRelationButtonHandler() {
        var btn = $('button#SaveRelation');

        if ($('input#NewRelationPersonId').val()) {
            btn.removeClass('disabled');
        }
        else {
            btn.addClass('disabled');
        }
    }

    //NamesTab
    function initNamesTab() {
        $('button#SaveNamesAndPatronym').addClass('disabled');
        Window.DatePicker.ReInitElement($('#ActiveFrom'));

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
            deletePersonName($(e.currentTarget).attr('nameid'));
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
        $('button#SaveActivity').on('click', function (e) {
            savePersonActivity();
        });
        //TODO delete

        Window.DatePicker.ReInitElement($('#NewActivityDateBegin'));
        $('#NewHasBegun').on('change', function (e) {
            var field = $('#NewActivityDateBegin');

            if (e.currentTarget.checked) {
                SetUpNewActivityDateField(field);
            }
            else {
                DestroyNewActivityDateField(field);
            }
        });
        $('#NewHasEnded').on('change', function (e) {
            var field = $('#NewActivityDateEnd');

            if (e.currentTarget.checked) {
                SetUpNewActivityDateField(field);
            }
            else {
                DestroyNewActivityDateField(field);
            }
        });
    }

    function SetUpNewActivityDateField(field) {
        field.removeAttr('readonly');
        Window.DatePicker.ReInitElement(field);
        field.datepicker('setDate', new Date());
    }

    function DestroyNewActivityDateField(field) {
        field.datepicker('setDate', '');
        field.attr('readonly', 'readonly');
        Window.DatePicker.DestroyElement(field);
    }

    function deletePersonActivity() {
        $.ajax({
            url: getApiRoute() + 'personactivity/delete/' + id,
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                loadBiography();
            }
        });
    }

    function savePersonActivity() {
        var model = {
            "Id": null,
            "DateCreated": null,
            "DateModified": null,
            "IsActive": true,
            "BiographyId": $('#PersonBiography_Id').val(),
            "Activity": $('#NewActivityActivity').val(),
            "ActivityType": $('#NewActivityActivityType').val(),
            "HasBegun": $('#NewHasBegun').is(':checked'),
            "DateBegin": $('#NewActivityDateBegin').val(),
            "HasEnded": $('#NewHasEnded').is(':checked'),
            "DateEnd": $('#NewActivityDateEnd').val()
        };

        $.ajax({
            url: 'SavePersonActivity',
            data: JSON.stringify({
                personId: $('#Model_Id').val(),
                bioId: $('#PersonBiography_Id').val(),
                newact: model
            }),
            type: 'POST',
            //dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            traditional: true,
            complete: function (response) {
                if (response.status !== 200) {
                    return;
                }

                loadBiography();
            }
        });
    }

    function savePersonBiography() {
        $.ajax({
            url: getApiRoute() + 'personbiography/save/' + $('#Model_Id').val() + '/' + $('#BiographyText').val(),
            type: 'GET',
            dataType: 'json',
            success: function (response) {
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

    //DocumentsTab
    function initDocumentsTab() {
        Window.CustomizedTypeahead.InitElement($('#DocumentCategoryTa'));
        $('#DocumentCategoryTa').off('typeahead:close');
        $('#DocumentCategoryTa').on('change', function (e) {
            $('#DocumentCategory').val(e.currentTarget.value);
        });

        $('#UploadFile').on('change', function (e) {
            if (!$('#DocumentCategory').val()) {
                $('#ErrorDocumentCategory').removeClass('hide');
                return;
            }
            $('#ErrorDocumentCategory').addClass('hide');

            uploadFile();
        });

        $('a#DeleteDocument').on('click', function (e) {
            deleteFile($(e.currentTarget).attr('fileid'));
        });
    }

    function deleteFile(fileid) {
        var personId = $('#Model_Id').val();

        $.ajax({
            url: getApiRoute() + 'person/file/delete/' + personId + '/' + fileid,
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                loadDocuments();
            }
        });
    }

    function uploadFile() {
        personId = $('#Model_Id').val();
        var fd = new FormData();
        var files = $('#UploadFile[type="file"]')[0].files;
        for (var i = 0; i < files.length; i++) {
            fd.append("file_" + i, files[i], files[i].name);
        }

        var category = $('#DocumentCategory').val();
        //var activityId = '';

        $.ajax({
            url: getApiRoute() + 'person/file/upload/'
                + personId + '/'
                + category,// + '/'
                //+ activityId,
            type: 'POST',
            data: fd,
            enctype: 'multipart/form-data',
            contentType: false,
            processData: false,
            complete: function (response) {
                if (response.status === 200) {
                    loadDocuments();
                }
            }
        });
    }

    function loadDocuments() {
        $.ajax({
            url: 'PersonDocuments',
            data: JSON.stringify({ personId: $('#Model_Id').val() }),
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            traditional: true,
            complete: function (response) {
                if (response.status !== 200) {
                    return;
                }

                $('.tab-pane#documents').html(response.responseText);
                initDocumentsTab();
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
