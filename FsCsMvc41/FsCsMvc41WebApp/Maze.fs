module Maze

type Cell = int*int
type Wall = Cell * Cell

let public MakeMeAMaze seed hallWidth = 
    let mazeSize = 50
    let rand = new System.Random(seed)

    let (takenCells) = ref []
    let (removedWalls) = ref []

    let genRoom size position =
        let xp,yp = position
        seq {
            for x in 0..size-1 do
                for y in 0..size-1 do 
                    yield (x+xp, y+yp) 
        }

    // Add 0-10 random sized rooms
    for i in 0..rand.Next(10) do 
        let roomsize = rand.Next(mazeSize/7)
        let pos = (rand.Next(mazeSize - roomsize), rand.Next(mazeSize - roomsize))
        takenCells := List.append !takenCells ( genRoom roomsize pos |> Seq.toList )

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

    let projectToCoords allWalls =
        let rec project allWalls = 
            let scale = hallWidth * 2
            let shift = hallWidth * 3
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

    move (3,3)
    projectToCoords (!removedWalls @ border)
