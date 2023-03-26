module TestUtils

open FSharp.Data.NBFormat
open FSharp.Data.NBFormat.Domain
open Xunit

open System.Text.Json
open System.Reflection
open System.IO

let assembly = Assembly.GetExecutingAssembly()

let getEmbeddedResource file = 
    use str = assembly.GetManifestResourceStream($"FSharp.Data.NBFormat.Tests.{file}")
    use r = new StreamReader(str)
    r.ReadToEnd()

module MimeBundle =
    let hasValue<'T> (key:string) (expected:'T) (bundle:MimeBundle) =
        Assert.Equal(expected, bundle.[key].Deserialize<'T>())


module Output =

    let isDisplayData (o:Output) = Assert.Equal(OutputType.DisplayData, o.OutputType)
    
    let isExecuteResult (o:Output) = Assert.Equal(OutputType.ExecuteResult, o.OutputType)
    
    let isStream (o:Output) = Assert.Equal(OutputType.Stream, o.OutputType)
    
    let isError (o:Output) = Assert.Equal(OutputType.Error, o.OutputType)
    
    let hasExecutionCount (expected:int) (o:Output) =
        Assert.Equal<int>(expected, o.ExecutionCount.Value)
    
    let hasData (o:Output) =
        Assert.True(o.Data.IsSome)

    let forData (test: MimeBundle -> unit) (o:Output) =
        test o.Data.Value
    
    let hasMetadata (expected:OutputMetadata) (o:Output) =
        Assert.Equal<OutputMetadata>(expected, o.Metadata.Value)

    let hasName (expected:string) (o:Output) =
        Assert.Equal<string>(expected, o.Name.Value)
    
    let hasText (expected:string) (o:Output) =
        Assert.Equal<string>(expected, o.Text.Value)
    
    let hasEname (expected:string) (o:Output) =
        Assert.Equal<string>(expected, o.Ename.Value)

    let hasEvalue (expected: string) (o:Output) =
        Assert.Equal<string>(expected, o.Evalue.Value)

    let hasTraceback (expected:string list) (o:Output) =
        Assert.Equal<string list>(expected, o.Traceback.Value)

module Cell =

    let isMarkdownCell (c:Cell) = Assert.Equal(CellType.Markdown, c.CellType)

    let isCodeCell (c:Cell) = Assert.Equal(CellType.Code, c.CellType)

    let isRawCell (c:Cell) = Assert.Equal(CellType.Raw, c.CellType)

    let hasSource (expected:string list) (c:Cell) =
        Assert.Equal<string list>(expected, c.Source)

    let hasMetadata (expected:Map<string, JsonElement>) (c:Cell) =
        Assert.Equal<Map<string, JsonElement>>(expected, c.Metadata)

    let hasAttachments (expected:Map<string, MimeBundle>) (c:Cell) =
        Assert.Equal<Map<string, MimeBundle>>(expected, c.Attachments.Value)
        
    let forOutput (index:int) (test: Output -> unit) (c:Cell) =
        test c.Outputs.Value.[index]

    let forAllOutputs (test: Output -> unit) (c:Cell) =
        Assert.All(c.Outputs.Value, test)
