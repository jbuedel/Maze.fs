/// <reference path="~/Scripts/jquery-1.6.4-vsdoc.js"/>
/// <reference path="~/Scripts/d3.js"/>

$(function () {
    // draw the maze
/*
    var maze = $("#maze g");
    for (var i = 0; i < all_walls.length; i++) {
        var wall = all_walls[i];
        //var line = '<line x1="' + wall.p1.x + '" y1="' + wall.p1.y + '" x2="' + wall.p2.x + '" y2="' + wall.p2.y + '" stroke-width="4" stroke="#000000" fill="none" />';
        var line = $("<line>").attr('y1', wall.p1.y).attr('x1', wall.p1.x).attr('x2', wall.p2.x).attr('y2', wall.p2.y).attr('stroke-width', 4).attr('stroke', "#000000").attr('fill', 'none');
        $(line).appendTo(maze);
    }
*/
});



function IsOnSegment(xi, yi, xj, yj,
                         xk, yk) {
    return (xi <= xk || xj <= xk) && (xk <= xi || xk <= xj) &&
         (yi <= yk || yj <= yk) && (yk <= yi || yk <= yj);
}

function ComputeDirection(xi, yi, xj, yj,
                              xk, yk) {
    var a = (xk - xi) * (yj - yi);
    var b = (xj - xi) * (yk - yi);
    return a < b ? -1 : a > b ? 1 : 0;
}

// Do line segments (x1, y1)--(x2, y2) and (x3, y3)--(x4, y4) intersect? 
function DoLineSegmentsIntersect(x1, y1, x2, y2,
                              x3, y3, x4, y4) {
    var d1 = ComputeDirection(x3, y3, x4, y4, x1, y1);
    var d2 = ComputeDirection(x3, y3, x4, y4, x2, y2);
    var d3 = ComputeDirection(x1, y1, x2, y2, x3, y3);
    var d4 = ComputeDirection(x1, y1, x2, y2, x4, y4);
    return (((d1 > 0 && d2 < 0) || (d1 < 0 && d2 > 0)) &&
          ((d3 > 0 && d4 < 0) || (d3 < 0 && d4 > 0))) ||
         (d1 == 0 && IsOnSegment(x3, y3, x4, y4, x1, y1)) ||
         (d2 == 0 && IsOnSegment(x3, y3, x4, y4, x2, y2)) ||
         (d3 == 0 && IsOnSegment(x1, y1, x2, y2, x3, y3)) ||
         (d4 == 0 && IsOnSegment(x1, y1, x2, y2, x4, y4));
}

var pathsIntersect = function (path1, path2) {
    return DoLineSegmentsIntersect(path1.p1.x,path1.p1.y,path1.p2.x,path1.p2.y, 
                                   path2.p1.x,path2.p1.y,path2.p2.x,path2.p2.y);
};

$(function () {

    var move = function (attrName, change) {
        var attr = $("#circle").attr(attrName);
        attr.baseVal.value = attr.baseVal.value + change;
    };

    var intersectsAny = function (all_walls, path) {
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

test("intersectionWorks", function () {
    var line2 = { p1: { x: 3, y: 3 }, p2: { x: 5, y: 5} };
    var path1 = { p1: { x: 5, y: 3 }, p2: { x: 3, y: 5} };

    equals(pathsIntersect(path1, line2), true, "These lines should form an x.");
    equals(pathsIntersect(path1, { p1: { x: 0, y: 0 }, p2: { x: 9, y: 0} }), false, "Do not intersect.");
    equals(pathsIntersect(path1, path1), true, "Same line. Incident. Considered an intersect.");
});


