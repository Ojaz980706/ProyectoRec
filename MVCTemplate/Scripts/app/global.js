mainUrl = '@Url.Content("~/")';
//Send error message
$(document).ready(function () {
    $('#table').dataTable({
        dom: 'Bfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ],
        pageLength: 10,
        responsive: true
    });

    var errorMsg = getUrlParameter("message");
    if (errorMsg.length > 0) {
        $('body').pgNotification({ style: 'flip', message: errorMsg, type: 'error' }).show();
    }
});
var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }
};
function test() {
    var MinpD = 32;
    var MaxpD = 32;


    var allchars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()_+|}{'<>/,./*-123456789";

    var password = "";

    var passwordLength = Math.floor((Math.random() * MinpD) + MaxpD)+1;

    while (passwordLength-- > 0)
        password += allchars[rdm.Next(allchars.Length)];
    for (var i = 0; i < passwordLength; i++) {

    }
    console.log(password.toString());
}