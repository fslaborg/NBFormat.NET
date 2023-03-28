namespace FSharp.Data.NBConvert

open FSharp.Data.NBFormat
open FSharp.Data.NBFormat.Domain
open Giraffe.ViewEngine

module HTML =
    let convertNotebook (notebook: Notebook) = 
        HTMLTemplates.Default.renderNotebook notebook