function notify(style, title, message, position, time, type, icon, where) {
    if (where === undefined || where === null) {
        where = 'body';
        setTimeout(function () {
            var modal = $("#ModalStickUp").is(":visible");
            if (modal) {
                where = '#ModalStickUp';
            }
        }, 100);
    }
    if (style === 'bar') {
        // Show an bar notification attached to top and bottom of the screen
        $(where).pgNotification({
            style: 'bar',
            message: message,
            position: position,
            timeout: time,
            type: type
        }).show();
    } else if (style === 'flip') {
        // Show a flipping notification animated
        // using CSS3 transforms and animations
        $(where).pgNotification({
            style: 'flip',
            message: message,
            position: position,
            timeout: time,
            type: type
        }).show();
    } else if (style === 'circle') {
        // Slide-in a circle notification from sides
        // You have to provide the HTML for thumbnail
        $(where).pgNotification({
            style: 'circle',
            title: title,
            message: message,
            position: position,
            timeout: time,
            type: type,
            thumbnail: icon
        }).show();
    } else if (style === 'simple') {
        // Simple notification having bootstrap's .alert class
        $(where).pgNotification({
            style: 'simple',
            message: message,
            position: position,
            timeout: time,
            type: type
        }).show();
    } else {
        return;
    }
}