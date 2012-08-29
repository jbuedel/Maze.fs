module Maze

type Cell = int*int
type Wall = Cell * Cell

let public MakeMeAMaze seed = 
    let mazeSize = 50
    let WallWidth = 5

    let (takenCells) = ref []
    let (removedWalls) = ref []

    // Add a room.
    takenCells :=  (3,3) :: (3,4) :: (4,3) :: (4,4) :: (3,5) :: (4,5) :: (5,3) :: (5,4) :: (5,5) :: []

//let addRoom size topleft =
//let makeRoom size 

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

    let rand = new System.Random(seed)
    let randomize cells =
        cells |> Seq.sortBy (fun x -> rand.Next()%2) 
        
    let rec move curcell = 
        // a list of taken cells
        takenCells := curcell :: !takenCells
        for a in  randomize(adjcells curcell) do
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
        [((-1,-1),(-1,mazeSize/2 - 1)) 
         ((-1,mazeSize / 2), (-1,mazeSize))
         ((-1,-1),(mazeSize,-1))
         ((mazeSize,-1),(mazeSize,mazeSize))
         ((-1,mazeSize),(mazeSize,mazeSize))
         ]


    projectToCoords (!removedWalls @ border)
