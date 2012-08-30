namespace FsWeb.Controllers

open System
open System.Web
open System.Web.Mvc

type IndexModel = { 
    Seed: int
    HallWidth: int
    Rooms: int
}

[<HandleError>]
type HomeController() =
    inherit Controller()
    member this.Index (seed:Nullable<int>, hallWidth:Nullable<int>, rooms:Nullable<int>) =
        let seed = if seed.HasValue then seed.Value else (int)DateTime.Now.Ticks
        let hallWidth = if hallWidth.HasValue then hallWidth.Value else 5
        let rooms = if rooms.HasValue then rooms.Value else 2
        this.View({Seed = seed; HallWidth = hallWidth; Rooms = rooms}) :> ActionResult


