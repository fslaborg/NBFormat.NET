module CLIArgs

open Argu

type OutputFormat =
    | HTML = 1

type CLIArgs =
    | [<Mandatory>] To of format: OutputFormat
    | [<EqualsAssignment>] Output_Dir of dir:string
    | [<MainCommand; ExactlyOnce; Last; Mandatory>] InputNotebook of nbpath: string
    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | To _ -> "Mandatory. Select the output format"
            | Output_Dir _ -> "Optional. Specify an output directory"
            | InputNotebook _ -> "Path of the notebook to convert"

let parser = ArgumentParser.Create<CLIArgs>(programName = "fs-nbconvert")