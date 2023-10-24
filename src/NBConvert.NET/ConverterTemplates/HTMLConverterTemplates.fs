namespace NBConvert.NET

open Giraffe.ViewEngine
open NBFormat.NET
open NBFormat.NET.Domain
open System.Text
open System.Text.Json
open System.Text.RegularExpressions

type DocumentTemplate(
    HeadTags: XmlNode list,
    ?FooterTags: XmlNode list
) =

    member val HeadTags = HeadTags with get,set
    member val FooterTags = defaultArg FooterTags [] with get,set

    member this.asHtmlNode(
        bodyNodes: XmlNode list
    ) = 
        html [] [
            head [] this.HeadTags
            body [] bodyNodes
            if this.FooterTags <> [] then
                footer [] this.FooterTags
        ]

type CellConverter(
    SourceConverter: string list -> XmlNode,
    OutputConverter: Output -> XmlNode
) =

    member _.ConvertSource(source: string list) = SourceConverter source
    member _.ConvertOutput(output: Output) = OutputConverter output

    member this.ConvertCell(cell: Cell) =
        let source = this.ConvertSource cell.Source
        let outputs = 
            cell.Outputs 
            |> Option.map (fun outputs -> outputs |> List.map this.ConvertOutput)
            |> Option.defaultValue []
        (source, outputs)

type HTMLConverterTemplate(
    DocumentTemplate: DocumentTemplate,
    CellConverter: CellConverter
) =

    member val DocumentTemplate = DocumentTemplate with get,set
    member val CellConverter = CellConverter with get,set

    member this.ConvertNotebook(notebook: Notebook) = 
        this.DocumentTemplate.asHtmlNode [
            for cell in notebook.Cells do
                let cell, outputs = this.CellConverter.ConvertCell(cell)
                yield cell
                yield! outputs
        ]

module HTMLConverterTemplates =
    
    let Default =
        HTMLConverterTemplate(
            DocumentTemplate = DocumentTemplate(
                HeadTags = []
            ),
            CellConverter = CellConverter(
                (fun (source) -> 
                    code [] [yield! source |> List.map (fun line -> str (Regex.Unescape(line)))]
                ),
                (fun (output) -> 
                    match output.OutputType with
                    | OutputType.DisplayData -> 
                        div [] [
                            yield! 
                                output.Data
                                |> Option.map(fun bundle -> 
                                    bundle 
                                    |> Map.toList
                                    |> List.map(fun (key, value) -> 
                                        div [] [
                                            str key
                                            str (value.ToString())
                                        ]
                                    )

                                )
                                |> Option.defaultValue []
                        ]
                    | _ -> div [] [str "other output type than DisplayData xd"]
                )
            )
        )
