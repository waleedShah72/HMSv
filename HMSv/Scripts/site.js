
$(window).load(function () {
    var defaultTypeID = 2;
    $("div.accomodationTypeRow[data-id=" + defaultTypeID + "]").hide();
});

$(".changeAccomadtionType").click(function () {
    debugger;
    var typeID = $(this).attr("data-id");

    $("div.accomodationTypeRow").hide();
    $("div.accomodationTypeRow[data-id=" + typeID + "]").show();
});

