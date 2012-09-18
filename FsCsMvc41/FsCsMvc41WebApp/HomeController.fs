namespace FsWeb.Controllers

open System
open System.Web
open System.Web.Mvc

type IndexModel = { 
    Seed: int
    MazeHeight: int
    MazeWidth: int
    Maze: Maze.Wall list
}

[<HandleError>]
type HomeController() =
    inherit Controller()
    member this.Index (seed:Nullable<int>, hallWidth:Nullable<int>, rooms:Nullable<int>) =
        let seed = if seed.HasValue then seed.Value else (int)DateTime.Now.Ticks
        let rand = (new Random(seed)).Next

        let hallWidth = if hallWidth.HasValue then hallWidth.Value else 5
        let rooms = if rooms.HasValue then rooms.Value else rand(5)
        let maze = Maze.MakeMeAMaze seed hallWidth rooms
        this.View({Seed = seed; MazeHeight = 520; MazeWidth = 520; Maze = maze}) :> ActionResult


    member this.Maze (seed:Nullable<int>, hallWidth:Nullable<int>, rooms:Nullable<int>) = 
        let seed = if seed.HasValue then seed.Value else (int)DateTime.Now.Ticks
        let rand = (new Random(seed)).Next

        let hallWidth = if hallWidth.HasValue then hallWidth.Value else 5
        let rooms = if rooms.HasValue then rooms.Value else rand(5)
         
        let maze = Maze.MakeMeAMaze seed hallWidth rooms
        this.Json(maze, JsonRequestBehavior.AllowGet) :> ActionResult