namespace FsWeb.Controllers

open System
open System.Web
open System.Web.Mvc

type IndexModel = { 
    Seed: int
    HallWidth: int
}

[<HandleError>]
type HomeController() =
    inherit Controller()
    member this.Index (seed:Nullable<int>, hallWidth:Nullable<int>) =
        let seed = if seed.HasValue then seed.Value else (int)DateTime.Now.Ticks
        let hallWidth = if hallWidth.HasValue then hallWidth.Value else 5
        this.View({Seed = seed; HallWidth = hallWidth}) :> ActionResult


