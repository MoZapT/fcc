Windows = {};

$('.accordion').on('click', function (e) {
    /* Toggle between adding and removing the "active" class,
    to highlight the button that controls the panel */
    this.classList.toggle("active");

    /* Toggle between hiding and showing the active panel */
    var panel = this.nextElementSibling;
    if (panel.style.display === "block") {
        panel.style.display = "none";
    }
    else {
        panel.style.display = "block";
    }
});

$('.open-person-modal-btn').on('click', function (e) {
    var modal = document.getElementById('person-picker');
    modal.style.display = "block";

    addCloseEvent();
})

$('.open-new-relation-modal').on('click', function (e) {
    var modal = document.getElementById('new-relation');
    modal.style.display = "block";

    addCloseEvent();
})

$('.create-new-relation').on('click', function (e) {
    var form = $('submitForm');

    var index = 0;
    var id = 0;

    var relation = "<input id=Model_Model_Relations_'" + index + "'_Id value='" + id + "' args='add'></input>";
});

function addCloseEvent() {
    $('.close').on('click', function (e) {
        var modal = $(e.currentTarget).closest('.modal')[0];
        modal.style.display = "none";
    });
}

// INIT ---------------------------------------------------------------------------
// --------------------------------------------------------------------------------
function InitializeComponent() {
    initDatepicker();
    initCrud();
}

Windows.PersonRelationshipPicker = {
    Init: function () {
        InitializeComponent();
    }
};