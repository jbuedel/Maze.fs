/// <reference path="~/Scripts/jquery-1.6.4-vsdoc.js"/>
/// <reference path="http://d3js.org/d3.v2.js"/>

// Do line segments (x1, y1)--(x2, y2) and (x3, y3)--(x4, y4) intersect? 
function DoLineSegmentsIntersect(x1, y1, x2, y2, x3, y3, x4, y4) {
    var isOnSegment = function (xi, yi, xj, yj, xk, yk) {
        return (xi <= xk || xj <= xk) && (xk <= xi || xk <= xj) &&
            (yi <= yk || yj <= yk) && (yk <= yi || yk <= yj);
    };

    var computeDirection = function (xi, yi, xj, yj, xk, yk) {
        var a = (xk - xi) * (yj - yi);
        var b = (xj - xi) * (yk - yi);
        return a < b ? -1 : a > b ? 1 : 0;
    };

    var d1 = computeDirection(x3, y3, x4, y4, x1, y1);
    var d2 = computeDirection(x3, y3, x4, y4, x2, y2);
    var d3 = computeDirection(x1, y1, x2, y2, x3, y3);
    var d4 = computeDirection(x1, y1, x2, y2, x4, y4);
    return (((d1 > 0 && d2 < 0) || (d1 < 0 && d2 > 0)) &&
          ((d3 > 0 && d4 < 0) || (d3 < 0 && d4 > 0))) ||
         (d1 == 0 && isOnSegment(x3, y3, x4, y4, x1, y1)) ||
         (d2 == 0 && isOnSegment(x3, y3, x4, y4, x2, y2)) ||
         (d3 == 0 && isOnSegment(x1, y1, x2, y2, x3, y3)) ||
         (d4 == 0 && isOnSegment(x1, y1, x2, y2, x4, y4));
}

var pathsIntersect = function (path1, path2) {
    return DoLineSegmentsIntersect(path1.p1.x, path1.p1.y, path1.p2.x, path1.p2.y,
                                   path2.p1.x, path2.p1.y, path2.p2.x, path2.p2.y);
};


$(function () {
    d3.json('/Home/Maze', function(walls) {
        // draw the maze
        d3.select("#maze g").selectAll("line").data(walls).enter().append("line")
            .attr("x1", function (line) {
                return line.p1.x;
        })
        .attr("x2", function (line) {
            return line.p2.x;
        })
        .attr("y1", function (line) {
            return line.p1.y;
        })
        .attr("y2", function (line) {
            return line.p2.y;
        })
        .attr("stroke-width", 4).attr("stroke", "#000000").attr("fill", "none");

        //    lines.exit().remove();




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
            return intersectsAny(walls, { p1: cur_pos, p2: pos });
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
});





