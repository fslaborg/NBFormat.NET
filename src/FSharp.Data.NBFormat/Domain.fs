namespace FSharp.Data.NBFormat

open System.Text.Json
open System.Text.Json.Serialization
open System

module Domain =
    open System.Text.Json.Serialization

    /// The cell's name. If present, must be a non-empty string. Cell names are expected to be unique across all the cells in a given notebook. This criterion cannot be checked by the json schema and must be established by an additional check.
    type MetadataName = string
    /// The cell's tags. Tags must be unique, and must not contain commas.
    type MetadataTags = string list
    type MimeBundle = Map<string, JsonElement>
            
    ///
    type KernelSpec = { 
        [<JsonPropertyName("name")>]
        Name: string
        [<JsonPropertyName("display_name")>]
        DisplayName: string 
    }

    ///
    type LanguageInfo = { 
        [<JsonPropertyName("name")>]
        Name: string
        [<JsonPropertyName("codemirror_mode")>]
        CodeMirrorMode: JsonElement option
        [<JsonPropertyName("file_extension")>]
        FileExtension: string option
        [<JsonPropertyName("mimetype")>]
        MimeType: string option
        [<JsonPropertyName("pygments_lexer")>]
        PygmentsLexer: string option 
    }

    ///
    type Author = { 
        [<JsonPropertyName("name")>]
        Name: string 
        [<JsonExtensionData>]
        AdditionalProperties: Map<string, JsonElement> option
    }

    type NotebookMetadata = { 
        [<JsonPropertyName("kernelspec")>]
        KernelSpec: KernelSpec option
        [<JsonPropertyName("language_info")>]
        LanguageInfo: LanguageInfo option
        [<JsonPropertyName("orig_nbformat")>]
        OrigNbFormat: int option
        [<JsonPropertyName("title")>]
        Title: string option
        [<JsonPropertyName("authors")>]
        Authors: Author list option
        [<JsonExtensionData>]
        AdditionalProperties: Map<string, JsonElement> option
    }

    [<RequireQualifiedAccess>]
    type CellType =
        | [<JsonName "raw">] Raw
        | [<JsonName "markdown">] Markdown
        | [<JsonName "code">] Code

    type JupyterMetadata = {
        [<JsonPropertyName("source_hidden")>]
        SourceHidde: bool
        [<JsonPropertyName("outputs_hidden")>]
        OutputsHidden: bool option
        [<JsonExtensionData>]
        AdditionalProperties: Map<string, JsonElement> option
    }

    type Execution = {
        [<JsonPropertyName("iopub.execute_input")>]
        IopubExecuteInput: string option
        [<JsonPropertyName("iopub.status.busy")>]
        IopubStatusBusy: string option
        [<JsonPropertyName("shell.execute_reply")>]
        ShellExecuteReply: string
        [<JsonPropertyName("iopub.status.idle")>]
        IopubStatusIdle: string
        [<JsonExtensionData>]
        AdditionalProperties: Map<string, JsonElement> option
    }

    [<RequireQualifiedAccess>]
    type Scrolled =
        | [<JsonName "auto">] Auto
        | [<JsonName true>] True
        | [<JsonName false>] False

    [<RequireQualifiedAccess>]
    type OutputType =
        | [<JsonName "execute_result">] ExecuteResult
        | [<JsonName "display_data">] DisplayData
        | [<JsonName "stream">] Stream
        | [<JsonName "error">] Error

    type OutputMetadata = {
        [<JsonExtensionData>]
        AdditionalProperties: Map<string, JsonElement> option
    }

    type Output = {
        [<JsonPropertyName("output_type")>]
        OutputType: OutputType
        [<JsonPropertyName("execution_count")>]
        ExecutionCount: int option
        [<JsonPropertyName("data")>]
        Data: MimeBundle option
        [<JsonPropertyName("metadata")>]
        Metadata: OutputMetadata option
        [<JsonPropertyName("name")>]
        Name: string option
        [<JsonPropertyName("text")>]
        Text: string option
        [<JsonPropertyName("ename")>]
        Ename: string option
        [<JsonPropertyName("evalue")>]
        Evalue: string option
        [<JsonPropertyName("traceback")>]
        Traceback: string list option
    }

    type CellMetadata = {
        [<JsonPropertyName("format")>]
        Format: string option
        [<JsonPropertyName("jupyter")>]
        Jupyter: JupyterMetadata option
        [<JsonPropertyName("execution")>]
        Execution: Execution option
        [<JsonPropertyName("collapsed")>]
        Collapsed: bool option
        [<JsonPropertyName("scrolled")>]
        Scrolled: Scrolled option
        [<JsonPropertyName("name")>]
        Name: MetadataName option
        [<JsonPropertyName("tags")>]
        Tags: MetadataTags option
        [<JsonExtensionData>]
        AdditionalProperties: Map<string, JsonElement> option
    }



    type Cell = { 
        [<JsonPropertyName("id")>]
        Id: string option
        [<JsonPropertyName("cell_type")>]
        CellType: CellType
        [<JsonPropertyName("metadata")>]
        Metadata: Map<string, JsonElement>
        [<JsonPropertyName("source")>]
        Source: string list
        [<JsonPropertyName("attachments")>]
        Attachments: Map<string, MimeBundle> option
        [<JsonPropertyName("outputs")>]
        Outputs: Output list option
        [<JsonPropertyName("execution_count")>]
        ExecutionCount: int option 
    }

    type Notebook = { 
        [<JsonPropertyName("metadata")>]
        Metadata: NotebookMetadata
        [<JsonPropertyName("nbformat_minor")>]
        NbFormatMinor: int
        [<JsonPropertyName("nbformat")>]
        NbFormat: int
        [<JsonPropertyName("cells")>]
        Cells: Cell list 
    }