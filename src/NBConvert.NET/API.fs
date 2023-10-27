namespace NBConvert.NET

open NBFormat.NET
open NBFormat.NET.Domain
open NBConvert.NET

open Feliz.ViewEngine

type API() =

    static member convert (
        converter: NotebookConverter
    ) =
        fun (notebook: Notebook) ->
            match converter with
            | HTMLConverter converter ->
                converter.ConvertNotebook notebook
                |> Render.htmlDocument