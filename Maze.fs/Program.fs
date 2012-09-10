open Maze 

let printBoard allWalls = 
    let line x1 y1 x2 y2 = printfn @"<line x1=""%d"" y1=""%d"" x2=""%d"" y2=""%d"" stroke-width=""5"" stroke=""#000000"" fill=""none""/>" x1 y1 x2 y2
    let rec print allWalls = 
        printfn @"<svg xmlns=""http://www.w3.org/2000/svg""><g>"
        let rec print allWalls =
            match allWalls with
            | ((x1,y1),(x2,y2)) :: rest -> line x1 y1 x2 y2 
                                           print rest
            | []    -> ()
        print allWalls
        printfn @"</g></svg>"
    print allWalls 


let seed = (new System.Random()).Next()
let maze = MakeMeAMaze seed 5 10
printBoard (maze) 
for p in WallPoints maze do
    let x,y = p
    printfn "{x:%d,y:%d}" x y

