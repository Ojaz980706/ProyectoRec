$(modalformInitialize());
function bindForm(dialog) {
    $('form', dialog).submit(function () {
        var result, render;
        var formdata = new FormData(this);
        //console.log(this);
        var xhr = new XMLHttpRequest();
        xhr.open('POST', this.action);
        xhr.send(formdata);
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4 && xhr.status === 200) {
                console.log(xhr);
                if (xhr.getResponseHeader('content-type').indexOf("json") !== -1) {
                    result = JSON.parse(xhr.responseText);
                    if (result.success) {
                        $('#ModalStickUp').modal('hide');
                    }
                    if (result.notify) {
                        notify(result.style, result.title, result.message, result.position, result.time, result.type, result.icon, result.where);
                    }
                    if (result.url !== null) {
                        if (result.redirect) {
                            window.location = result.url;
                        }
                        else {
                            render = $(result.target);
                            if (render !== null) {
                                $(render).load(result.url, function () {
                                    //Algo más
                                });
                            }
                        }
                    }
                }
                else {
                    $('#ModalStickUpContent').html(xhr.response);
                    bindForm(dialog);
                }
            }
            else {
                //console.log(xhr);
                if (xhr.readyState === 4 && xhr.status !== 409) {
                    var detailed = xhr.responseText.substring(xhr.responseText.indexOf('<title>'), xhr.responseText.indexOf('</title>')).substring(7);
                    notify('circle', 'Ha ocurrido un error:', detailed, 'top-left', 0, 'danger', '<img width="40" height="40" style="display: inline-block;" src="' + mainUrl + 'favicon.ico">');
                }
                else if (xhr.readyState === 2 && xhr.status === 409) {
                    result = JSON.parse(xhr.statusText);
                    if (result.success) {
                        $('#ModalStickUp').modal('hide');
                    }
                    if (result.notify) {
                        notify(result.style, result.title, result.message, result.position, result.time, result.type, result.icon, result.where);
                    }
                    if (result.url !== null) {
                        if (result.redirect) {
                            window.location = result.url;
                        }
                        else {
                            render = $(result.target);
                            if (render !== null) {
                                $(render).load(result.url, function () {
                                    //Algo más
                                });
                            }
                        }
                    }
                }
            }
        };
        modalformInitialize();
        return false;
    });
}
function modalformInitialize() {
    $.ajaxSetup({ cache: false });
    $("a[data-modal='layout']").off('click').on("click", function (e) {
        // hide dropdown if any (this is used wehen invoking modal from link in bootstrap dropdown )
        //$(e.target).closest('.btn-group').children('.dropdown-toggle').dropdown('toggle');
        $('#ModalStickUpContent').load(this.href, function (response, status, xhr) {
            //console.log(this);
            //console.log(status);
            //console.log(xhr);
            if (status === "error") {
                notify('circle', 'Ha ocurrido un error:', xhr.status + " - " + xhr.statusText, 'top-left', 0, 'danger', '<img width="40" height="40" style="display: inline-block;" src="' + mainUrl + 'favicon.ico">');
                return false;
            }
            $('#ModalStickUp').modal({
                backdrop: 'static',
                keyboard: true
            }, 'show');
            bindForm(this);
        });
        return false;
    });
}