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
let Unk =  ' '
let Path = '.'
let Wall = '#'
type Pos = int*int
type Move = 
| Some of Pos*Pos
| None

let mazeSize = 10
let Maze = Array2D.init mazeSize mazeSize (fun x y -> null)

let printMaze (maze: char[,]) =
    printfn "----------"
    for x in seq {0 .. Array2D.length1 maze - 1 } do
        for y in seq {0 .. Array2D.length2 maze - 1 } do
            printf "%c" maze.[x,y]
        printfn ""

let onBoard (pos:Pos) = 
    let x,y = pos
    x >= 0 && x < mazeSize && y >= 0 && y < mazeSize

let canBePath (maze:Maze) (move:Move) =
    let _,(too) = move
    let to_x,to_y = too
    onBoard too &&  maze.[to_x,to_y] = null

let rec next (maze: Move[,]) (move:Move) =
    let pos,too = move
    let x,y = too
    maze.[x, y] <- move
    printMaze maze

    let up:Move =    too, (x+1, y)
    let down:Move =  too, (x-1, y)
    let left:Move =  too, (x, y+1)
    let right:Move = too, (x, y-1)

    let moves = left :: right :: up :: down :: []
    List.iter (fun move -> if canBePath maze move then next maze move) moves 


let final = next Maze (0,0)
printMaze Maze