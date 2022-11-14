var listAudio = [];

$.ajax({
    type: "POST",
    url: "/Home/GetSongs",
    success: function (files) {
        var song = {};

        for (var i = 0; i < files.length; i++) {
            song.name = files[i];
            song.path = "/upload/" + files[i];
            song.duration = "";

            listAudio.push(song);

            alert("SHIT");
            var source = '<source src="' + "/upload/" + files[i]+ '" type="audio/mpeg"/>';
            var elem = document.getElementById('player');
            elem.append(source);
        }
    }
});

