namespace FSharp.Data.NBFormat

open System.Text.Json
open System.Text.Json.Serialization
open System

module Domain =
    open System.Text.Json.Serialization

    type MetadataName = string
    type MetadataTags = string list
    type MimeBundle = Map<string, string>

    type KernelSpec =
        { [<JsonPropertyName("name")>]
          Name: string
          [<JsonPropertyName("display_name")>]
          DisplayName: string }

    type LanguageInfo =
        { [<JsonPropertyName("name")>]
          Name: string
          [<JsonPropertyName("codemirror_mode")>]
          CodeMirrorMode: JsonElement option
          [<JsonPropertyName("file_extension")>]
          FileExtension: string option
          [<JsonPropertyName("mimetype")>]
          MimeType: string option
          [<JsonPropertyName("pygments_lexer")>]
          PygmentsLexer: string option }

    type Author =
        { [<JsonPropertyName("name")>]
          Name: string }

    type NotebookMetadata =
        { [<JsonPropertyName("kernelspec")>]
          KernelSpec: KernelSpec option
          [<JsonPropertyName("language_info")>]
          LanguageInfo: LanguageInfo option
          [<JsonPropertyName("orig_nbformat")>]
          OrigNbFormat: int option
          [<JsonPropertyName("title")>]
          Title: string option
          [<JsonPropertyName("authors")>]
          Authors: Author list option }

    type Cell =
        { [<JsonPropertyName("cell_type")>]
          CellType: string
          [<JsonPropertyName("metadata")>]
          Metadata: Map<string, obj>
          [<JsonPropertyName("source")>]
          Source: string list
          [<JsonPropertyName("attachments")>]
          Attachments: Map<string, MimeBundle> option
          [<JsonPropertyName("outputs")>]
          Outputs: JsonElement list option
          [<JsonPropertyName("execution_count")>]
          ExecutionCount: int option }

    type Notebook =
        { [<JsonPropertyName("metadata")>]
          Metadata: NotebookMetadata
          [<JsonPropertyName("nbformat_minor")>]
          NbFormatMinor: int
          [<JsonPropertyName("nbformat")>]
          NbFormat: int
          [<JsonPropertyName("cells")>]
          Cells: Cell list }