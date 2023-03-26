# FSharp.Data.NBFormat
A pure F# library for parsing the `.ipynb` notebook file format v4.

It uses [System.Text.Json]() in conjunction with the absolutely amazing [FSharp.SystemTextJson](https://github.com/Tarmil/FSharp.SystemTextJson) library.

## Develop

### build

Check the [build project](https://github.com/kMutagene/FSharp.Data.NBFormat/blob/main/build) to take a look at the  build targets. Here are some examples:

```shell
# Windows

# Build only
./build.cmd

# build and run tests
./build.cmd runTests

# Linux/mac

# Build only
build.sh

# build and run tests
build.sh runTests

```

## Usage

The library is not on nuget because i am not sure under which namespace to put it currently (e.g. `FSharp.Data` vs `FSharp.Formatting`)

So you currently have to clone the repo and build it yourself.
Then you can use it to deserialize a notebook file like this:

```fsharp
open FSharp.Data.NBFormat
open System.IO

"your/path/to/a/notebook.ipynb"
|> File.ReadAllText
|> Serialiation.deserializeNotebook
```