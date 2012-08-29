namespace FsWeb.Controllers

open System.Web
open System.Web.Mvc

[<HandleError>]
type HomeController() =
    inherit Controller()
    member this.Index (seed:System.Nullable<int>) =
        this.View(seed) :> ActionResult
