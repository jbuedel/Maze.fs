/// <reference path="script.js"/>

test("intersectionWorks", function() {
    var line2 = { p1: { x: 3, y: 3 }, p2: { x: 5, y: 5 } };
    var path1 = { p1: { x: 5, y: 3 }, p2: { x: 3, y: 5 } };

    equals(pathsIntersect(path1, line2), true, "These lines should form an x.");
    equals(pathsIntersect(path1, { p1: { x: 0, y: 0 }, p2: { x: 9, y: 0 } }), false, "Do not intersect.");
    equals(pathsIntersect(path1, path1), true, "Same line. Incident. Considered an intersect.");
});