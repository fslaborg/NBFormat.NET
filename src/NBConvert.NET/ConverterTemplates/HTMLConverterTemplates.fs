namespace NBConvert.NET

open Feliz.ViewEngine
open Feliz.Bulma.ViewEngine
open NBFormat.NET
open NBFormat.NET.Domain
open System.Text
open System.Text.Json
open System.Text.RegularExpressions

type DocumentTemplate(
    HeadTags: ReactElement list,
    ?FooterTags: ReactElement list
) =

    member val HeadTags = HeadTags with get,set
    member val FooterTags = defaultArg FooterTags [] with get,set

    member this.asHtmlNode(
        bodyNodes: ReactElement list
    ) = 
        Html.html [
            Html.head this.HeadTags
            Html.body bodyNodes
            if this.FooterTags <> [] then
                Html.footer this.FooterTags
        ]

type CellConverter(
    SourceConverter: CellType -> string list -> ReactElement,
    OutputConverter: Output -> ReactElement
) =

    member _.ConvertSource(source: string list, cellType: CellType) = SourceConverter cellType source
    member _.ConvertOutput(output: Output) = OutputConverter output

    member this.ConvertCell(cell: Cell) =
        let source = this.ConvertSource(cell.Source, cell.CellType)
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
    
    open type System.Environment

    let Default =
        HTMLConverterTemplate(
            DocumentTemplate = DocumentTemplate(
                HeadTags = [
                    Html.link [
                        prop.rel "stylesheet"
                        prop.href "https://cdnjs.cloudflare.com/ajax/libs/bulma/0.9.3/css/bulma.min.css"
                    ]
                ]
            ),
            CellConverter = CellConverter(
                SourceConverter = 
                    (fun cellType source -> 
                        match cellType with
                        | CellType.Markdown -> 
                            Bulma.container [yield! source |> List.map prop.text]
                        | CellType.Code ->
                            Bulma.container [
                                prop.children [
                                    Html.pre (
                                        source 
                                        |> List.map Bulma.text.p
                                    )
                                ]
                            ]
                        | CellType.Raw -> 
                             Bulma.container [yield! source |> List.map prop.text]
                    ),
                OutputConverter = 
                    (fun (output) -> 
                        match output.OutputType with
                        | OutputType.DisplayData -> 
                            Bulma.container [
                                yield! 
                                    output.Data
                                    |> Option.map(fun bundle -> 
                                        bundle 
                                        |> Map.toList
                                        |> List.map(fun (key, value) -> 
                                            Bulma.container [
                                                prop.dangerouslySetInnerHTML (
                                                    value
                                                        .EnumerateArray()
                                                        |> Seq.cast<System.Text.Json.JsonElement>
                                                        |> Seq.map (fun j -> j.GetString())
                                                    |> String.concat "\r\n"
                                                )
                                            ]
                                        )

                                    )
                                    |> Option.defaultValue []
                            ]
                        | _ -> Bulma.container [prop.text "other output type than DisplayData xd"]
                    )
            )
        )
