Window.CustomizedTypeahead = {};

(function () {
    function initializeComponent() {
        var typeaheadList = $('.typeahead');

        for (var i = 0; i < typeaheadList.length; i++) {
            crtTa = typeaheadList[i];

            $(crtTa)
                .typeahead(
                    dftOptions,
                    dftDataset($(crtTa).attr('taname'), $(crtTa).attr('taurl')));
        }

        $('.typeahead').bind('typeahead:select', function (e, suggestion) {
            typeaheadSelect(e.currentTarget, suggestion.Key);
        });

        $('.typeahead').bind('typeahead:close', function (e) {
            validateTaInput($(e.currentTarget), $(e.currentTarget).prop('value'));
        });

        $('span.twitter-typeahead').append('<span class="wsc-tt-clear glyphicon glyphicon-remove"></span>');

        $('span.wsc-tt-clear').on('click', function (e) {
            var parentTa = $(e.currentTarget).siblings('.typeahead');
            parentTa.typeahead('val', '');
            typeaheadSelect(parentTa, '');
        });
    }

    function typeaheadSelect(typeahead, suggestion) {
        var idField = $(typeahead).closest('span.twitter-typeahead').prev('input[type="hidden"]');

        $(idField).val(suggestion);
        $(idField).change();
    }

    function validateTaInput(typeahead, suggestion) {
        var suggestions = $(typeahead).siblings('div.tt-menu').find('div.tt-selectable').length;
        var isEmptyOrInvalid = suggestions <= 0 ? true : false;

        if (isEmptyOrInvalid) {
            $(typeahead).typeahead('val', '');
            typeaheadSelect(typeahead, '');
        }
    }

    var dftOptions = {
        highlight: true,
        minLength: 0,
        dynamic: true
    };

    var dftDataset = function (name, url, limit = 20) {
        return dataset = {
            name: name,
            display: 'Value',
            limit: limit,
            source: function (query, processSync, processAsync) {
                return doAjax(url, query, processSync, processAsync);
            }
        };
    };

    function doAjax(url, query, processSync, processAsync) {
        var loadingSpinnerScaf = getLoadingSpinnerScaffold();
        $('body').append(loadingSpinnerScaf);

        return $.ajax({
            url: getApiRoute() + url + query,
            type: 'GET',
            dataType: 'json',
            complete: function (response) {
                $('.typeahead-fader').remove();

                return processAsync(response.responseJSON);
            }
        });
    }

    function getLoadingSpinnerScaffold() {
        var msg = 'Loading...';
        var html = '<div class="typeahead-fader">';
        html += '<div class="spinner-border text-primary typeahead-spinner-centre" role="status">';
        html += '<span class="sr-only">';
        html += msg;
        html += '</span>';
        html += '</div>';
        html += '</div>';

        return html;
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

    Window.CustomizedTypeahead = {
        Init: function () {
            initializeComponent();
        },

        InitElement: function (elem) {
            $(elem).typeahead(
                dftOptions,
                dftDataset($(elem).attr('taname'), $(elem).attr('taurl')));

            $(elem).bind('typeahead:select', function (e, suggestion) {
                typeaheadSelect(e.currentTarget, suggestion.Key);
            });

            $(elem).bind('typeahead:close', function (e) {
                validateTaInput($(e.currentTarget), $(e.currentTarget).prop('value'));
            });

            spantwitter = $(elem).closest('span.twitter-typeahead');
            $(spantwitter).append('<span class="wsc-tt-clear glyphicon glyphicon-remove"></span>');

            $(spantwitter).children('span.wsc-tt-clear').on('click', function (e) {
                var parentTa = $(e.currentTarget).siblings('.typeahead');
                parentTa.typeahead('val', '');
                typeaheadSelect(parentTa, '');
            });
        }
    };
})();