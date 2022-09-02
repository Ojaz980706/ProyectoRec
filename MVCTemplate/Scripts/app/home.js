
$(document).ready(function () {
    $('#spanHome').css({ 'background-color': '#ffa500', 'color': '#fff' });

});
const video = document.getElementById('video');


function StartVideo() {
    navigator.getUserMedia = (navigator.getUserMedia ||
        navigator.webkitGetUserMedia ||
        navigator.mozGetUserMedia ||
        navigator.msGetUserMedia);
    navigator.getUserMedia(
        { video: {} },
        stream => video.srcObjet = stream,
        err => console.log(err)
    )
}
StartVideo();
