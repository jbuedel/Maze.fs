/// <reference path="~/Scripts/jquery-1.6.4-vsdoc.js"/>

$(function () {
    var circle = $("#circle");


    $("svg").click(function () {
        circle.attr('cx', circle.attr('cx')+3);
    });
});
