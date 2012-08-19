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
        let shift = WallWidth * 3
        match allWalls with
        | ((x1,y1),(x2,y2)) :: rest -> ((x1*scale + shift, y1*scale + shift),(x2*scale + shift, y2*scale + shift)) :: project rest  
        | []                        -> []
    project allWalls
    

let border = 
    [((-1,-1),(-1,mazeSize)) 
     ((-1,-1),(mazeSize,-1))
     ((mazeSize,-1),(mazeSize,mazeSize))
     ((-1,mazeSize),(mazeSize,mazeSize))
     ]
   
let printBoard allWalls = 
    let line x1 y1 x2 y2 = printfn @"<line x1=""%d"" y1=""%d"" x2=""%d"" y2=""%d"" stroke-width=""5"" stroke=""#000000"" fill=""none""/>" x1 y1 x2 y2
    let rec print allWalls = 
        printfn @"<svg width=""640"" height=""640"" xmlns=""http://www.w3.org/2000/svg""><g>"
        let rec print allWalls =
            match allWalls with
            | ((x1,y1),(x2,y2)) :: rest -> line x1 y1 x2 y2 
                                           print rest
            | []    -> ()
        print allWalls
        printfn @"</g></svg>"
    print (projectToCoords allWalls)

printBoard (!removedWalls @ border)
//printBoard border
