#r "nuget: System.Text.Json, 7.0.2"
#r "nuget: FSharp.SystemTextJson, 1.1.23"
#r "nuget: Giraffe.ViewEngine, 1.4.0"
#r "nuget: Feliz.ViewEngine, 0.24.0"
#r "nuget: Feliz.Bulma.ViewEngine, 0.25.0"
#r "nuget: Markdig, 0.33.0"
#r "nuget: WebStoating.Markdig.Prism, 1.0.0"

#load "../NBFormat.NET/Domain.fs"
#load "../NBFormat.NET/Serialization.fs"

#load "InternalUtils.fs"
#load "ConverterTemplates/HTMLConverterTemplates.fs"
#load "NotebookConverter.fs"
#load "API.fs"

open System.IO
open NBFormat.NET
open NBConvert.NET

let nb = 
    File.ReadAllText @"C:\Users\schne\Source\repos\fslaborg\NBFormat.NET\tests\NBFormat.NET.Tests\data\simple\polyglot_notebook_simple_fsharp.ipynb"
    |> NBFormat.NET.Serialization.deserializeNotebook

nb
|> NBConvert.NET.API.convert (HTMLConverter HTMLConverterTemplates.Default)
|> fun f -> File.WriteAllText (@"C:\Users\schne\Source\repos\fslaborg\NBFormat.NET\tests\NBFormat.NET.Tests\data\simple\polyglot_notebook_simple_fsharp.html", f)

nb.Cells[1].Outputs.Value
|> List.map (fun x -> x.Data.Value["text/html"].EnumerateArray())
|> List.item 0
|> Seq.cast<System.Text.Json.JsonElement>
|> Seq.map (fun j -> j.GetString())
|> String.concat "\r\n"