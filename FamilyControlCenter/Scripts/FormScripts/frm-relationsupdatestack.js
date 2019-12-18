Window.FormScripts = {};

(function () {
    function initializeComponent() {

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
