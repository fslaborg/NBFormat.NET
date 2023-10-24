namespace NBFormat.NET

module Serialization =
    open Domain
    open System.IO
    open System.Text.Json
    open System.Text.Json.Serialization

    // Deserialize the JSON file to the Notebook type
    let deserializeNotebook (jsonString: string) : Notebook =
        let options = 
            JsonFSharpOptions.Default()
                .WithSkippableOptionFields()
                .WithTypes(JsonFSharpTypes.All)
                .WithUnionUnwrapFieldlessTags()
                .ToJsonSerializerOptions()
                
        options.PropertyNameCaseInsensitive <- true

        JsonSerializer.Deserialize<Notebook>(jsonString, options)

    let serializeNotebook (notebook: Notebook) : string =
        let options = 
            JsonFSharpOptions.Default()
                .WithSkippableOptionFields()
                .WithTypes(JsonFSharpTypes.All)
                .WithUnionUnwrapFieldlessTags()
                .ToJsonSerializerOptions()

        options.PropertyNameCaseInsensitive <- true

        JsonSerializer.Serialize<Notebook>(notebook, options)
