namespace NBConvert.NET

open NBFormat.NET
open NBFormat.NET.Domain
open NBConvert.NET

open Giraffe.ViewEngine

type API() =

    static member convert (
        converter: NotebookConverter
    ) =
        fun (notebook: Notebook) ->
            match converter with
            | HTMLConverter converter ->
                converter.ConvertNotebook notebook
                |> RenderView.AsString.htmlDocument