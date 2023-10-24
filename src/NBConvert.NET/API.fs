namespace NBConvert.NET

open NBFormat.NET
open NBFormat.NET.Domain
open NBConvert.NET

open Giraffe.ViewEngine

type API() =

    static member convert (
        notebook: Notebook,
        converter: NotebookConverter
    ) =
        match converter with
        | HTMLConverter converter ->
            converter.ConvertNotebook notebook
            |> RenderView.AsString.htmlDocument