namespace FsWeb.Controllers

open System
open System.Web
open System.Web.Mvc

type IndexModel = { 
    Seed: int
    MazeHeight: int
    MazeWidth: int
}

[<HandleError>]
type HomeController() =
    inherit Controller()
    member this.Index (seed:Nullable<int>, hallWidth:Nullable<int>, rooms:Nullable<int>) =
        let seed = if seed.HasValue then seed.Value else (int)DateTime.Now.Ticks

        this.View({Seed = seed; MazeHeight = 520; MazeWidth = 520}) :> ActionResult


    member this.Maze (seed:Nullable<int>) = 
        let seed = if seed.HasValue then seed.Value else (int)DateTime.Now.Ticks

        let maze = Maze.MakeMeAMaze seed
        this.Json(maze, JsonRequestBehavior.AllowGet) :> ActionResult