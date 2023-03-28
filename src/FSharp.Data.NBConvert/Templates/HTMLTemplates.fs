namespace FSharp.Data.NBConvert

open Giraffe.ViewEngine
open FSharp.Data.NBFormat
open FSharp.Data.NBFormat.Domain
open System.Text
open System.Text.RegularExpressions

type HTMLTemplate(
    DocTemplate: XmlNode list -> XmlNode,
    CellTemplate: Cell -> XmlNode,
    OutputTemplate: Output -> XmlNode
) =

    member val DocTemplate = DocTemplate with get, set
    
    member val CellTemplate = CellTemplate with get, set

    member val OutputTemplate = OutputTemplate with get, set

    member this.renderNotebook(notebook: Notebook) = 
        
        let cellWithOutputs = notebook.Cells |> List.map (fun cell -> (cell, cell.Outputs))

        this.DocTemplate [
            for (cell, outputs) in cellWithOutputs do
                yield this.CellTemplate cell
                match outputs with
                | None -> ()
                | Some outputs -> 
                    for output in outputs do
                        yield this.OutputTemplate output
        ]

    static member renderNotebook (template:HTMLTemplate, notebook: Notebook) =
        template.renderNotebook(notebook)

module HTMLTemplates =
    let Default =
        let docTemplate (nodes: XmlNode list) =
            html [] [
                head [] [
                    title [] [ str "FSharp.Data.NBConvert" ]
                    link [ _rel "stylesheet"; _href "https://cdnjs.cloudflare.com/ajax/libs/github-markdown-css/2.10.0/github-markdown.min.css" ]
                    link [ _rel "stylesheet"; _href "https://cdnjs.cloudflare.com/ajax/libs/highlight.js/9.12.0/styles/default.min.css" ]
                ]
                body [] nodes
                
            ]
        let cellTemplate (cell: Cell) =
            code [] [for line in cell.Source ->
                str (Regex.Unescape(line))
            ]
        let outputTemplate (output: Output) =
            div [] []
            
        HTMLTemplate(docTemplate, cellTemplate, outputTemplate)