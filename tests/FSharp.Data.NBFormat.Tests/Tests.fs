module Tests

open System
open Xunit

open FSharp.Data.NBFormat
open System.Reflection
open System.IO
open TestUtils

let assembly = Assembly.GetExecutingAssembly()

let getEmbeddedResource file = 
    use str = assembly.GetManifestResourceStream($"FSharp.Data.NBFormat.Tests.{file}")
    use r = new StreamReader(str)
    r.ReadToEnd()

let notebookJson = getEmbeddedResource "data.polyglot_notebook_simple_fsharp.ipynb"

let simpleNotebook = Serialiation.deserializeNotebook notebookJson


[<Fact>]
let ``can read embedded resource`` () =
    Assert.NotNull notebookJson

[<Fact>]
let ``has 2 cells`` () =
    Assert.Equal(2, simpleNotebook.Cells.Length)

[<Fact>]
let ``first cell is a markdown cell`` () =
    Cell.isMarkdownCell simpleNotebook.Cells.[0]

[<Fact>]
let ``second cell is a code cell`` () =
    Cell.isCodeCell simpleNotebook.Cells.[1]

[<Fact>]
let ``first cell has correct source`` () =
    Cell.hasSource [
        "# Markdown"
    ] simpleNotebook.Cells.[0]

[<Fact>]
let ``second cell has correct source`` () =
    Cell.hasSource [
        "let a = 42\n"
        "\n"
        "a"
    ] simpleNotebook.Cells.[1]
