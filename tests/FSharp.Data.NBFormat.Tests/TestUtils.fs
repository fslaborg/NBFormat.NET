module TestUtils

open FSharp.Data.NBFormat
open FSharp.Data.NBFormat.Domain
open Xunit

open System.Text.Json

module Cell =

    let isMarkdownCell (c:Cell) = Assert.Equal("markdown", c.CellType)

    let isCodeCell (c:Cell) = Assert.Equal("code", c.CellType)

    let isRawCell (c:Cell) = Assert.Equal("raw", c.CellType)


    let hasSource (expected:string list) (c:Cell) =
        Assert.Equal<string list>(expected, c.Source)

    let hasMetadata (expected:Map<string, obj>) (c:Cell) =
        Assert.Equal<Map<string, obj>>(expected, c.Metadata)

    let hasAttachments (expected:Map<string, MimeBundle>) (c:Cell) =
        Assert.Equal<Map<string, MimeBundle>>(expected, c.Attachments.Value)
        
    let hasOutputs (expected:JsonElement list) (c:Cell) =
        Assert.Equal<JsonElement list>(expected, c.Outputs.Value)