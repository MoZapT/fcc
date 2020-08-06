Models.Person = {};

(function () {
    var Firstname = "";
    var Lastname = "";
    var Patronym = "";
    var BirthDate = null;
    var BirthDate = null;
    var HasBirthDate = false;
    var HasDeathDate = false;
    var Sex = false;
    var IsMarried = false;
    var IsInPartnership = false;
    var FileContentId = "";

    //Base
    Models.Person = {
        Name: function () {
            var result = "";
            result += Firstname ? string.Empty : Firstname;
            result += result ? string.Empty : " ";
            result += Lastname ? string.Empty : Lastname;
            result += result ? string.Empty : " ";
            result += Patronym ? string.Empty : Patronym;
            return result;
        }
    };
})();
