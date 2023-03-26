#r "nuget: System.Text.Json, 7.0.2"
#r "nuget: FSharp.SystemTextJson, 1.1.23"

#load "Domain.fs"
#load "Serialization.fs"

open System.IO
open FSharp.Data.NBFormat

File.ReadAllText @"C:\Users\schne\Source\repos\kMutagene\FSharp.Data.NBFormat\tests\FSharp.Data.NBFormat.Tests\data\polyglot_notebook_plotly_fsharp.ipynb"
|> Serialiation.deserializeNotebook
|> Serialiation.serializeNotebook
|> Serialiation.deserializeNotebook
