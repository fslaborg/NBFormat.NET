namespace FSharp.Data.NBFormat


module Serialiation =
    open Domain
    open System.IO
    open System.Text.Json
    open System.Text.Json.Serialization

    // Deserialize the JSON file to the Notebook type
    let deserializeNotebook (jsonString: string) : Notebook =
        let options = 
            JsonFSharpOptions.Default()
                .WithSkippableOptionFields()
                .ToJsonSerializerOptions()
        options.PropertyNameCaseInsensitive <- true
        options.Converters.Add(JsonFSharpConverter())

        JsonSerializer.Deserialize<Notebook>(jsonString, options)
