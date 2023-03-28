open CLIArgs
open System
open System.IO
open FSharp.Data
open FSharp.Data.NBConvert
open FSharp.Data.NBFormat
open FSharp.Data.NBFormat.Domain

[<EntryPoint>]
let main args =
    let parsedArgs = parser.ParseCommandLine args
    
    printfn "parsed args: %A" (parsedArgs.GetAllResults())

    let currentDir = System.Environment.CurrentDirectory

    let notebookPath = parsedArgs.GetResult InputNotebook

    let outputDir = 
         match parsedArgs.TryGetResult Output_Dir with
         | Some dir -> dir
         | None -> currentDir

    let toFormat = parsedArgs.GetResult To

    let parsedNotebook =
        notebookPath
        |> File.ReadAllText 
        |> Serialiation.deserializeNotebook

    match toFormat with
    | OutputFormat.HTML ->
        let outputPath = Path.Combine(outputDir, Path.GetFileNameWithoutExtension(notebookPath) + ".html")
        let convertedNotebook = NBConvert.API.convert(parsedNotebook, (HTMLConverter HTMLConverterTemplates.Default))
        File.WriteAllText(outputPath, convertedNotebook)
    | _ -> failwith "Invalid output format"
    0