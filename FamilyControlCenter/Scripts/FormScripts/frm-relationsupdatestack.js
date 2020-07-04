Window.FormScripts = {};

(function () {
    function initializeComponent() {
        $('button[index]').on('click', function (e) { saveRelation(e.currentTarget); });
        loadPersonsWithRelations();
        loadRelations();

        $('select#PersonId').on('change', function (e) {
            loadPersonsWithRelations();
        });

        $('select#SelectedPersonIdWithRelations').on('change', function (e) {
            loadRelations();
        });
    }

    function loadPossiblePersons() {
        var options = 0;

        $.ajax({
            url: getApiRoute() + 'person/personwithpossibilities',
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                $('select#PersonId').html(createOptionConstruct(response));
                options = $('select#PersonId option').length;
            }
        });

        return options;
    }

    function loadPersonsWithRelations() {
        $.ajax({
            url: getApiRoute() + 'person/possiblerelations/' + $('select#PersonId').val(),
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                $('select#SelectedPersonIdWithRelations').html(createOptionConstruct(response));
                loadRelations();
            }
        });
    }

    function loadRelations() {
        $.ajax({
            url: 'LoadRelationsPartialForPersonRelationsUpdateStack',
            data: JSON.stringify(
                {
                    personId: $('select#PersonId').val(),
                    selectedId: $('select#SelectedPersonIdWithRelations').val()
                }),
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            traditional: true,
            complete: function (response) {
                if (response.status !== 200) {
                    return;
                }

                $('#RelationsUpdateStackContainer .card-body').html(response.responseText);
                $('button[index]').on('click', function (e) { saveRelation(e.currentTarget); });
            }
        });
    }

    function createOptionConstruct(json) {
        var html = '';
        for (var i = 0; i < json.length; i++) {
            var selected = '';
            if (i === 0)
                selected = 'selected="selected"';

            html += '<option ' + selected + 'value="' + json[i].Key + '">';
            html += json[i].Value;
            html += '</option >';
        }

        return html;
    }

    function saveRelation(btn) {
        var type = $('#Type_' + $(btn).attr('index')).val();
        var inviter = $('select#PersonId option:selected').val();
        var invited = $(btn).attr('invited');

        $.ajax({
            url: getApiRoute() + 'relation/set/' + inviter + '/' + invited + '/' + type,
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                $(btn).closest('div.row').remove();

                if ($('button[index]').length <= 0) {
                    var options = loadPossiblePersons();

                    if (options <= 0) {
                        reloadView();
                        return;
                    }

                    loadRelations();
                }
            }
        });
    }

    function reloadView() {
        $.ajax({
            url: 'PersonRelationsUpdateStackAsPartial',
            data: JSON.stringify(
                {
                    personId: $('select#PersonId').val(),
                    selectedId: $('select#SelectedPersonIdWithRelations').val()
                }),
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            traditional: true,
            complete: function (response) {
                if (response.status !== 200) {
                    return;
                }

                $('#StackContainer').html(response.responseText);
                initializeComponent();
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
