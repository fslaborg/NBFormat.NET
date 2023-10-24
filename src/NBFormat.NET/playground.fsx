#r "nuget: System.Text.Json, 7.0.2"
#r "nuget: FSharp.SystemTextJson, 1.1.23"

#load "Domain.fs"
#load "Serialization.fs"

open System.IO
open NBFormat.NET

File.ReadAllText @"C:\Users\schne\Source\repos\kMutagene\NBFormat.NET\tests\NBFormat.NET.Tests\data\polyglot_notebook_plotly_fsharp.ipynb"
|> Serialiation.deserializeNotebook
|> Serialiation.serializeNotebook
|> Serialiation.deserializeNotebook
