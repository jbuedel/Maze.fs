/// <reference path="~/Scripts/jquery-1.6.4-vsdoc.js"/>
/// <reference path="~/Scripts/d3.js"/>

$(function () {
    var move = function (attrName, change) {
        var attr = $("#circle").attr(attrName);
        attr.baseVal.value = attr.baseVal.value + change;
    };

    var pathsIntersect = function (path1, path2) {
        return false;
    };

    var intersectsAny = function(all_walls, path) {
        for (var i = 0; i < all_walls.length; i++) {
            if (pathsIntersect(all_walls[i], path)) return true;
        }
        return false;
    };

    var isAWall = function (pos) {
        return intersectsAny(all_walls, { p1: cur_pos, p2: pos });
    };

    var cur_pos = { x: 10, y: 10 };

    $("body").keydown(function (e) {

        var left = { x: cur_pos.x - 10, y: cur_pos.y };
        var up = { x: cur_pos.x, y: cur_pos.y - 10 };
        var right = { x: cur_pos.x + 10, y: cur_pos.y };
        var down = { x: cur_pos.x, y: cur_pos.y + 10 };

        if (e.keyCode === 37 && !isAWall(left)) { // left
            move("cx", -10);
            cur_pos = left;
        } else if (e.keyCode === 38 && !isAWall(up)) { // up
            move("cy", -10);
            cur_pos = up;
        } else if (e.keyCode === 39 && !isAWall(right)) { // right
            move("cx", 10);
            cur_pos = right;
        } else if (e.keyCode === 40 && !isAWall(down)) { // down
            move("cy", 10);
            cur_pos = down;
        }

        console.log(cur_pos.x + "," + cur_pos.y);

        e.stopPropagation();
    });
});