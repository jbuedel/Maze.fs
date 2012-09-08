/// <reference path="~/Scripts/jquery-1.6.4-vsdoc.js"/>
/// <reference path="~/Scripts/d3.js"/>

$(function () {
    var move = function (attrName, change) {
        var attr = $("#circle").attr(attrName);
        attr.baseVal.value = attr.baseVal.value + change;
    };

    $("body").keydown(function (e) {


        if (e.keyCode === 37) { // left
            move("cx", -5);
        }
        else if (e.keyCode === 38) {// up
            move("cy", -5);
        }
        else if (e.keyCode === 39) { // right
            move("cx", 5);
        }
        else if (e.keyCode === 40) { // down
            move("cy", 5);
        }
        e.stopPropagation();
    });

    $("#circle").click(function () {
    });
});
