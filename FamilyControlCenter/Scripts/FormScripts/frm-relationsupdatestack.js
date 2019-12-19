Window.FormScripts = {};

(function () {
    function initializeComponent() {
        $('select#PersonId').on('change', function (e) {
            loadPersonsWithRelations();
        });

        $('select#SelectedPersonIdWithRelations').on('change', function (e) {
            loadRelations();
        });

        //if no more relation avaible, then:
        //loadPossiblePersons()
    }

    function loadPossiblePersons() {
        $.ajax({
            url: getApiRoute() + 'person/personwithpossibilities',
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                $('select#PersonId').html(createOptionConstruct(response));
            }
        });
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
