// Learn more about F# at http://fsharp.net
//
//let Maze = [ [0;0;0;0;0;0;0;0];
//             [0;0;0;0;0;0;0;0];
//             [0;0;0;0;0;0;0;0];
//             [0;0;0;0;0;0;0;0];
//             [0;0;0;0;0;0;0;0];
//             [0;0;0;0;0;0;0;0];
//             [0;0;0;0;0;0;0;0];
//             [0;0;0;0;0;0;0;0] ]

let mazeSize = 10
let WallWidth = 5
type Cell = int*int
type Wall = Cell * Cell

let (takenCells) = ref []
let (removedWalls) = ref []


let rec isTaken cell takenCells = 
    match takenCells with
    | c :: rest when c=cell -> true
    | c :: rest -> isTaken cell rest
    | [] -> false
    
let adjcells cell =
    let x,y = cell
    (x+1,y) :: (x-1,y) :: (x, y-1) :: (x, y+1) :: []

let isOnBoard cell = 
    let x,y = cell
    x >= 0 && x < mazeSize && y >= 0 && y < mazeSize

let rec move curcell = 
    // a list of taken cells
    takenCells := curcell :: !takenCells
    for a in adjcells curcell do
        if isOnBoard a && isTaken a !takenCells = false then
            // a list of *removed* walls.
            removedWalls := (curcell, a) :: !removedWalls
            move a

move (3,3)

let projectToCoords allWalls =
    let rec project allWalls = 
        let scale = WallWidth * 2
        match allWalls with
        | ((x1,y1),(x2,y2)) :: rest -> ((x1*scale + WallWidth, y1*scale + WallWidth),(x2*scale + WallWidth, y2*scale + WallWidth)) :: project rest  
        | []                        -> []
    project allWalls
    
    
let rec printBoard allWalls = 
    printfn @"<svg width=""640"" height=""480"" xmlns=""http://www.w3.org/2000/svg""><g>"
    let rec print allWalls =
        match allWalls with
        | ((x1,y1),(x2,y2)) :: rest -> printfn @"<line x1=""%d"" y1=""%d"" x2=""%d"" y2=""%d"" stroke-width=""5"" stroke=""#000000"" fill=""none""/>" x1 y1 x2 y2
                                       print rest
        | []    -> ()
    print allWalls
    printfn @"</g></svg>"

printBoard (projectToCoords !removedWalls)