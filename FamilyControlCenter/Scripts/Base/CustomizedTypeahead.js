﻿Window.CustomizedTypeahead = {};

(function () {
    function initializeComponent() {
        var typeaheadList = $('.typeahead');

        for (var i = 0; i < typeaheadList.length; i++)
        {
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
        var idField = $(typeahead).closest('span.twitter-typeahead').siblings('input[type="hidden"]');

        $(idField).val(suggestion);
        $(idField).change();
    }

    function validateTaInput(typeahead, suggestion) {
        var isEmptyOrInvalid = $(typeahead).siblings('div.tt-empty').length > 0 ? true : false;
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
                return $.ajax({
                    url: getApiRoute() + url + query,
                    type: 'GET',
                    dataType: 'json',
                    complete: function (response) {
                        return processAsync(response.responseJSON);
                    }
                });
            }
        };
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

    Window.CustomizedTypeahead = {
        Init: function () {
            initializeComponent();
        }
    };
})();